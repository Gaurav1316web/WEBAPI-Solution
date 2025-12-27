Imports common
Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

Public Class FrmSendDBTToJanaadhar
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonTooltip As New ToolTip()
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnGenerateBill, "Press Alt+R for Refresh the Data")
        SetUserMgmtNew()
        LoadUnion()
    End Sub
    Public Sub LoadUnion()
        Dim qry As String = "select Code,Location_Name as Name from TSPL_MASTER.dbo.TSPL_APP_LOCATION where isnull(Union_Report,0)=1 order by Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.InsertAt(dr, 0)

        cboUnion.DataSource = dt
        cboUnion.ValueMember = "Code"
        cboUnion.DisplayMember = "Name"
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnGenerateBill.Visible = MyBase.isPostFlag

    End Sub
    Private Sub btnGenerateBill_Click(sender As Object, e As EventArgs) Handles btnGenerateBill.Click
        Try
            If clsCommon.myLen(cboUnion.SelectedValue) <= 0 Then
                cboUnion.Focus()
                Throw New Exception("Please select Union")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Sending DBT To JanAadhaar of [" + cboUnion.Text + "]  " + Environment.NewLine + "Are you sure ", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = DialogResult.Yes Then
                SendRemaingDBTToJanAadhaar("Sending DBT To JanAadhaar of [" + clsCommon.myCstr(cboUnion.Text) + "] ", clsCommon.myCstr(cboUnion.SelectedValue))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SendRemaingDBTToJanAadhaar(ByVal Task As String, ByVal PortNo As String)
        clsCommon.ProgressBarPercentShow()
        Try
            Dim qry As String = "select DataBase_Name  from TSPL_MASTER.dbo.TSPL_APP_LOCATION  where Code='" + PortNo + "'"
            Dim DBName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            Dim ResponceSucess As Integer = 0
            Dim ResponceFailure As Integer = 0
            qry = "select TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code,TSPL_MP_MASTER.JA_janaadhaarId,TSPL_MP_MASTER.JA_jan_mid,TSPL_DBT_NEFT_DETAIL.MP_Account_No,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No,TSPL_DBT_NEFT_DETAIL.PK_Id,convert(varchar,COALESCE(TSPL_DBT_NEFT.RCDF_Post_Date, TSPL_DBT_NEFT_BANK_RESPONSE.Created_Date),103) as Created_Date,TSPL_DBT_NEFT_DETAIL.Amount" & Environment.NewLine & ",(case when TSPL_DBT_NEFT_REJECT_DETAIL.PK_Id is not null then 'Failure' else (case when Bank_Response like 'STATUS : SUCCESS%' then 'Success' else 'Failure' end) end) as Bank_Response 
from " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL 
left outer join " + DBName + ".dbo.TSPL_DBT_NEFT on " + DBName + ".dbo.TSPL_DBT_NEFT.Document_Code = " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.Document_Code
left outer join " + DBName + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL on " + DBName + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join " + DBName + ".dbo.TSPL_MP_MASTER on  " + DBName + ".dbo.TSPL_MP_MASTER.MP_Code = " + DBName + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
inner join " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE on " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id = " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.PK_Id
left outer join " + DBName + ".dbo.TSPL_DBT_NEFT_REJECT_DETAIL on " + DBName + ".dbo.TSPL_DBT_NEFT_REJECT_DETAIL.Against_DBT_NEFT_TR= " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.PK_Id
where isnull(" + DBName + ".dbo.TSPL_MP_MASTER.Jan_Aadhar_No_Verified,0)=1 and ISNULL(" + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE.JA_Is_Saved,'N')='N' and len(TSPL_MP_MASTER.JA_jan_mid)>0 "
            'and TSPL_DBT_NEFT_DETAIL.PK_Id=11499"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ii As Int32 = 0

                For Each dr As DataRow In dt.Rows
                    Dim LastEx As String = ""
                    Try
                        ii += 1
                        qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(JA_Is_Saved,'N') as JA_Is_Saved from " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE where Ref_PK_Id=" & clsCommon.myCstr(dr("PK_Id")) & ""))
                        If clsCommon.CompairString(clsCommon.myCstr(qry), "N") = CompairStringResult.Equal Then
                            Dim obj As JSONClsSendDBTToJanaadhar = New JSONClsSendDBTToJanaadhar()
                            obj.TransactionId = PortNo & clsCommon.myCstr(dr("PK_Id"))
                            obj.SchemeCode = "CDUSY"
                            obj.AppCode = "JAN7351580"
                            obj.EntitlementId = PortNo & clsCommon.myCstr(dr("MP_Code"))
                            obj.EntitlementMemId = PortNo & clsCommon.myCstr(dr("MP_Code"))
                            obj.JanaadhaarId = clsCommon.myCstr(dr("JA_janaadhaarId"))
                            obj.JanaadhaarMemId = clsCommon.myCstr(dr("JA_jan_mid"))
                            obj.TransactionMode = "AC"
                            obj.AadharNo = ""
                            obj.BankAccNo = clsCommon.myCstr(dr("MP_Account_No"))
                            obj.IFSC = clsCommon.myCstr(dr("MP_IFSC_No"))
                            obj.MICR = ""
                            obj.PaymentAmount = clsCommon.myCstr(dr("Amount"))
                            obj.PaymentDate = clsCommon.myCstr(dr("Created_Date"))
                            clsSendDBTDataToJanAadhar20.SendData(obj)


                            'Dim flag As Boolean = False
                            'Dim objReturn As JSONClsSendDBTToJanaadharResponse = JSONClsSendDBTToJanaadharResponse.SendData(obj)
                            'If objReturn IsNot Nothing Then
                            '    Dim hashtable As Hashtable = New Hashtable()
                            '    clsCommon.AddColumnsForChange(hashtable, "JA_Request_ID", objReturn.RequestId)
                            '    clsCommon.AddColumnsForChange(hashtable, "JA_CMSG", objReturn.Cmsg)

                            '    If objReturn.Transaction IsNot Nothing Then
                            '        clsCommon.AddColumnsForChange(hashtable, "JA_Is_Saved", objReturn.Transaction.IsSaved)
                            '        clsCommon.AddColumnsForChange(hashtable, "JA_Msg", objReturn.Transaction.Msg)
                            '        flag = (clsCommon.CompairString(objReturn.Transaction.IsSaved, "Y") = CompairStringResult.Equal)
                            '    End If
                            '    clsCommon.AddColumnsForChange(hashtable, "JA_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
                            '    clsCommon.AddColumnsForChange(hashtable, "JA_Created_By", objCommonVar.CurrentUserCode)
                            '    clsCommonFunctionality.UpdateDataTable(hashtable, "" + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE", OMInsertOrUpdate.Update, "Ref_PK_Id=" & clsCommon.myCstr(dr("PK_Id")) & "")
                            'End If
                            'If flag Then
                            '    ResponceSucess += 1
                            'Else
                            '    ResponceFailure += 1
                            'End If
                        End If
                    Catch ex As Exception
                        ResponceFailure += 1
                        LastEx = ex.Message
                        'Throw New Exception(ex.Message)
                    End Try
                    clsCommon.ProgressBarPercentUpdate((ii + 1), dt.Rows.Count, "Sending DBT Data To JanAadhar Server.Success [" + clsCommon.myCstr(ResponceSucess) + "] And Failure [" + clsCommon.myCstr(ResponceFailure) + "]" + LastEx)
                Next
                clsCommon.ProgressBarPercentHide()
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Private Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub FrmDBTPayment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(cboUnion.SelectedValue) <= 0 Then
                cboUnion.Focus()
                Throw New Exception("Please select Union")
            End If
            ReceivePDAccountResponse("Getting PD Account Bank Response [" + clsCommon.myCstr(cboUnion.Text) + "] ", clsCommon.myCstr(cboUnion.SelectedValue))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReceivePDAccountResponse(ByVal Task As String, ByVal PortNo As String)
        Try
            Dim qry As String = "select PD_Account_Prefix,DataBase_Name,Apply_PD_Account_Date from TSPL_MASTER.dbo.TSPL_APP_LOCATION  where Code='" + PortNo + "' and Apply_PD_Account=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strPrefix As String = clsCommon.myCstr(dt.Rows(0)("PD_Account_Prefix"))
                Dim ApplyPDAccountDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Apply_PD_Account_Date")), "dd/MMM/yyyy")
                Dim DBName As String = clsCommon.myCstr(dt.Rows(0)("DataBase_Name"))
                Dim ResponceSucess As Integer = 0
                Dim ResponceFailure As Integer = 0
                qry = "select Refenceno,max(DocumentCode) as DocumentCode,max(FromDate) as FromDate,max(ToDate) as ToDate,max(Lot_No) as Lot_No from (
select '" + strPrefix + "'+FORMAT(TSPL_DBT_NEFT.UKID, '0000')+CAST(TSPL_DBT_NEFT_DETAIl.Lot_No as varchar) as Refenceno,TSPL_DBT_NEFT_DETAIl.Lot_No ,max(TSPL_DBT_NEFT.Document_Code) as DocumentCode,Max(TSPL_DBT_NEFT.From_Date) as FromDate,max(TSPL_DBT_NEFT.To_Date) as ToDate,1 as RI,1 as chk
from " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL
left outer join " + DBName + ".dbo.TSPL_DBT_NEFT on " + DBName + ".dbo.TSPL_DBT_NEFT.Document_Code=" + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.Document_Code
where TSPL_DBT_NEFT.Document_Date>'" + ApplyPDAccountDate + "' and isnull(TSPL_DBT_NEFT.RCDF_Status,0)=1 
and TSPL_DBT_NEFT.RCDF_Post_Date < DATEADD(DAY, -10, GETDATE())
group by TSPL_DBT_NEFT.UKID,TSPL_DBT_NEFT_DETAIl.Lot_No
union all
select '" + strPrefix + "'+FORMAT(TSPL_DBT_NEFT.UKID, '0000')+CAST(TSPL_DBT_NEFT_DETAIl.Lot_No as varchar) as Refenceno,TSPL_DBT_NEFT_DETAIl.Lot_No,max(TSPL_DBT_NEFT.Document_Code) as DocumentCode,Max(TSPL_DBT_NEFT.From_Date) as FromDate,max(TSPL_DBT_NEFT.To_Date) as ToDate
,-1 as RI,0 as chk
from " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL
left outer join " + DBName + ".dbo.TSPL_DBT_NEFT on " + DBName + ".dbo.TSPL_DBT_NEFT.Document_Code=" + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.Document_Code
inner join " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE on " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id = " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.PK_Id
group by TSPL_DBT_NEFT.UKID,TSPL_DBT_NEFT_DETAIl.Lot_No
)xx  group by Refenceno having sum(RI)>0 and sum(chk)>0 order by FromDate"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "found [" + clsCommon.myCstr(dt.Rows.Count) + "] Pending Document" + Environment.NewLine + "Do you want to continue...", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = DialogResult.Yes Then
                        clsCommon.ProgressBarPercentShow()
                        Try
                            Dim ii As Int32 = 0
                            For Each dr As DataRow In dt.Rows
                                Try
                                    Dim arrError As ArrayList = Nothing
                                    Dim arr As List(Of RejectionTmp) = clsPDAccountBankResponse.GetRejectionList(clsCommon.myCstr(dr("Refenceno")))
                                    'Dim arr As List(Of RejectionTmp) = clsPDAccountBankResponse.GetRejectionList("10006014")
                                    Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                                    Try
                                        Dim servDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm:ss tt")
                                        arrError = New ArrayList
                                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                                            For Each obj As RejectionTmp In arr
                                                Dim coll As New Hashtable()
                                                clsCommon.AddColumnsForChange(coll, "Ref_PK_Id", obj.Appid)
                                                clsCommon.AddColumnsForChange(coll, "Bank_Response", "STATUS : FAILED ," + obj.Reason)
                                                clsCommon.AddColumnsForChange(coll, "Created_Date", servDate)
                                                clsCommonFunctionality.UpdateDataTable(coll, "" + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE", OMInsertOrUpdate.Insert, "", tran)

                                                arrError.Add(obj.Appid)
                                            Next
                                        End If
                                        qry = "insert into " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE (Ref_PK_Id,Bank_Response,Created_Date)
select TSPL_DBT_NEFT_DETAIL.PK_ID,'STATUS : SUCCESS' as Bank_Response,GetDate() as Created_Date
from " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL
left outer join " + DBName + ".dbo.TSPL_DBT_NEFT on " + DBName + ".dbo.TSPL_DBT_NEFT.Document_Code=" + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.Document_Code
where 2=2 "
                                        If arrError IsNot Nothing AndAlso arrError.Count > 0 Then
                                            qry += " and TSPL_DBT_NEFT_DETAIL.PK_Id not in (" + clsCommon.GetMulcallString(arrError) + ")"
                                        End If
                                        qry += " And TSPL_DBT_NEFT.Document_Code ='" + clsCommon.myCstr(dr("DocumentCode")) + "' and TSPL_DBT_NEFT_DETAIL.Lot_No='" + clsCommon.myCstr(dr("Lot_No")) + "'"
                                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                                        tran.Commit()
                                        ResponceSucess += 1
                                    Catch ex As Exception
                                        tran.Rollback()
                                        ResponceFailure += 1
                                    End Try
                                    ii += 1
                                    clsCommon.ProgressBarPercentUpdate((ii + 1), dt.Rows.Count, "Getting PD Account Response [" + clsCommon.myCstr(dr("Refenceno")) + "] .Success [" + clsCommon.myCstr(ResponceSucess) + "] And Failure [" + clsCommon.myCstr(ResponceFailure) + "]")
                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            Next
                            clsCommon.ProgressBarPercentHide()
                        Catch ex As Exception
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                Else
                    Throw New Exception("No Pending data found")
                End If
            Else
                Throw New Exception("PD Account is not applied for the current union")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class




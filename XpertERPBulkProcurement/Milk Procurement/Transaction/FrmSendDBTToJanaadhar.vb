Imports common
Imports System.Data.SqlClient

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
            SendRemaingDBTToJanAadhaar("Sending DBT To JanAadhaar of [" + clsCommon.myCstr(cboUnion.Text) + "] ", clsCommon.myCstr(cboUnion.SelectedValue))
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
            qry = "select top 5 TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code,TSPL_MP_MASTER.JA_janaadhaarId,TSPL_MP_MASTER.JA_jan_mid,TSPL_DBT_NEFT_DETAIL.MP_Account_No,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No,TSPL_DBT_NEFT_DETAIL.PK_Id,convert(varchar, TSPL_DBT_NEFT_BANK_RESPONSE.Created_Date,103) as Created_Date,TSPL_DBT_NEFT_DETAIL.Amount" & Environment.NewLine & ",(case when TSPL_DBT_NEFT_REJECT_DETAIL.PK_Id is not null then 'Failure' else (case when Bank_Response like 'STATUS : SUCCESS%' then 'Success' else 'Failure' end) end) as Bank_Response 
from " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL 
left outer join " + DBName + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL on " + DBName + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join " + DBName + ".dbo.TSPL_MP_MASTER on  " + DBName + ".dbo.TSPL_MP_MASTER.MP_Code = " + DBName + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
inner join " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE on " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id = " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.PK_Id
left outer join " + DBName + ".dbo.TSPL_DBT_NEFT_REJECT_DETAIL on " + DBName + ".dbo.TSPL_DBT_NEFT_REJECT_DETAIL.Against_DBT_NEFT_TR= " + DBName + ".dbo.TSPL_DBT_NEFT_DETAIL.PK_Id
where isnull(" + DBName + ".dbo.TSPL_MP_MASTER.Jan_Aadhar_No_Verified,0)=1 and ISNULL(" + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE.JA_Is_Saved,'N')='N'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ii As Int32 = 0
                For Each dr As DataRow In dt.Rows
                    Try
                        ii += 1
                        qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(JA_Is_Saved,'N') as JA_Is_Saved from " + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE where Ref_PK_Id=" & clsCommon.myCstr(dr("PK_Id")) & ""))
                        If clsCommon.CompairString(clsCommon.myCstr(qry), "N") = CompairStringResult.Equal Then
                            Dim obj As clsXMLSendDBTDataToJanAadharHead = New clsXMLSendDBTDataToJanAadharHead()
                            obj.Transaction = New clsXMLSendDBTDataToJanAadharDetail()
                            obj.Transaction.EntitlementId = PortNo & clsCommon.myCstr(dr("MP_Code"))
                            obj.Transaction.EntitlementMemId = ""
                            obj.Transaction.JanaadhaarId = clsCommon.myCstr(dr("JA_janaadhaarId"))
                            obj.Transaction.JanaadhaarMemId = clsCommon.myCstr(dr("JA_jan_mid"))
                            obj.Transaction.TransactionId = PortNo & clsCommon.myCstr(dr("PK_Id"))
                            obj.Transaction.DueTransactionId = PortNo & clsCommon.myCstr(dr("PK_Id"))
                            obj.Transaction.AadharNo = ""
                            obj.Transaction.Eid = ""
                            obj.Transaction.BankAccNo = clsCommon.myCstr(dr("MP_Account_No"))
                            obj.Transaction.Ifsc = clsCommon.myCstr(dr("MP_IFSC_No"))
                            obj.Transaction.Micr = ""
                            obj.Transaction.PaymentAmount = clsCommon.myCstr(dr("Amount"))
                            obj.Transaction.PaymentDate = clsCommon.myCstr(dr("Created_Date"))
                            obj.Transaction.Status = clsCommon.myCstr(dr("Bank_Response"))
                            Dim flag As Boolean = False
                            Dim objReturn As clsXMLSendDBTDataToJanAadharResponseRoot = clsSendDBTDataToJanAadhar.SendData(obj)
                            If objReturn IsNot Nothing Then
                                Dim hashtable As Hashtable = New Hashtable()
                                clsCommon.AddColumnsForChange(hashtable, "JA_Request_ID", objReturn.RequestId)
                                clsCommon.AddColumnsForChange(hashtable, "JA_CMSG", objReturn.Cmsg)

                                If objReturn.Transaction IsNot Nothing Then
                                    clsCommon.AddColumnsForChange(hashtable, "JA_Is_Saved", objReturn.Transaction.IsSaved)
                                    clsCommon.AddColumnsForChange(hashtable, "JA_Msg", objReturn.Transaction.Msg)
                                    flag = (clsCommon.CompairString(objReturn.Transaction.IsSaved, "Y") = CompairStringResult.Equal)
                                End If
                                clsCommon.AddColumnsForChange(hashtable, "JA_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
                                clsCommon.AddColumnsForChange(hashtable, "JA_Created_By", objCommonVar.CurrentUserCode)
                                clsCommonFunctionality.UpdateDataTable(hashtable, "" + DBName + ".dbo.TSPL_DBT_NEFT_BANK_RESPONSE", OMInsertOrUpdate.Update, "Ref_PK_Id=" & clsCommon.myCstr(dr("PK_Id")) & "")
                            End If
                            If flag Then
                                ResponceSucess += 1
                            Else
                                ResponceFailure += 1
                            End If
                        End If
                        clsCommon.ProgressBarPercentUpdate((ii + 1), dt.Rows.Count, "Sending DBT Data To JanAadhar Server.Success [" + clsCommon.myCstr(ResponceSucess) + "] And Failure [" + clsCommon.myCstr(ResponceFailure) + "]")
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
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
End Class

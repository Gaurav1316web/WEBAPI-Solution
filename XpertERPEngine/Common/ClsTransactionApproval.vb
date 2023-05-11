Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class ClsTransactionApproval
#Region "variable"
    Public Form_ID As String = Nothing
    Public Screen_Name As String = Nothing
    Public Program_Code As String = Nothing
    Public Document_No As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Approval_Type As String = Nothing
    Public Approve As Integer = Nothing
    Public Cust_Code As String = Nothing
    Public Approval_Remarks As String = Nothing
    Public SMS_Content As String = Nothing
    Public Email_Content As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsTransactionApproval, ByVal isnewentry As Boolean, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isTranInitLocal As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isTranInitLocal = True
        Else
            isTranInitLocal = False
        End If

        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Screen_Name", obj.Screen_Name)
            clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Approval_Type", obj.Approval_Type)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
            clsCommon.AddColumnsForChange(coll, "Approval_Remarks", obj.Approval_Remarks)
            clsCommon.AddColumnsForChange(coll, "SMS_Content", obj.SMS_Content)
            clsCommon.AddColumnsForChange(coll, "Email_Content", obj.Email_Content)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMMM/yyyy "))
            If isnewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMMM/yyyy "))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSACTION_APPROVAL", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSACTION_APPROVAL", OMInsertOrUpdate.Update, "TSPL_TRANSACTION_APPROVAL.Program_Code='" & obj.Program_Code & "' AND Document_No='" & obj.Document_No & "'", trans)
            End If
            CreateSMSContent(obj, trans)
            CreateEmailContent(obj, trans)
            If isTranInitLocal Then
                trans.Commit()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Sub CreateSMSContent(ByVal obj As ClsTransactionApproval, ByVal trans As SqlTransaction)
        'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenODDEvenForm, clsFixedParameterCode.OpenODDEvenForm, trans)) = 1 Then
        Dim strSMSContent As String = obj.SMS_Content
        'SMSCode Start
        If clsCommon.myLen(strSMSContent) > 0 AndAlso clsCommon.myLen(obj.Cust_Code) > 0 Then
            'Dim MobileNo As String = ""
            'MobileNo = clsDBFuncationality.getSingleValue("select coalesce(Phone1,Phone2) as MobleNo from TSPL_CUSTOMER_MASTER where Cust_Code='" & obj.Cust_Code & "'", trans)
            'If clsCommon.myLen(MobileNo) >= 10 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.arrMobilNo = New List(Of String)()
            'objSMSH.arrMobilNo.Add(MobileNo)
            objSMSH.SaveData(obj.Form_ID, objSMSH, trans)
            objSMSH = Nothing
            'End If
        End If
        'SMSCode End Start
        'End If
    End Sub
    Public Shared Sub CreateEmailContent(ByVal obj As ClsTransactionApproval, ByVal trans As SqlTransaction)
        'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenODDEvenForm, clsFixedParameterCode.OpenODDEvenForm, trans)) = 1 Then
        Dim strEmailContent As String = obj.Email_Content
        'SMSCode Start
        If clsCommon.myLen(strEmailContent) > 0 AndAlso clsCommon.myLen(obj.Cust_Code) > 0 Then
            Dim qry As String = "SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.FrmTransactionApproval & "'"
            Dim EmailSubject As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.FrmTransactionApproval & "'", trans))

            'Dim Email As String = ""
            'Email = clsDBFuncationality.getSingleValue("select Email from TSPL_CUSTOMER_MASTER where Cust_Code='" & obj.Cust_Code & "'", trans)
            'If clsCommon.myLen(Email) >= 10 Then
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Text = strEmailContent
            objSMSH.Email_Subject = EmailSubject
            objSMSH.arrEMail = New List(Of String)()
            'objSMSH.arrEMail.Add(Email)
            objSMSH.SaveData(obj.Form_ID, objSMSH, trans)
            objSMSH = Nothing
            'End If
        End If
        'SMSCode End Start
        'End If
    End Sub
   

    Public Shared Function getdata(ByVal code As String) As ClsTransactionApproval
        Try
            Dim obj As ClsTransactionApproval = Nothing
            Dim qst As String = "select TSPL_TRANSACTION_APPROVAL.Program_Code ,Doc_Date ,Document_No ,Approval_Type,Screen_Name,TSPL_TRANSACTION_APPROVAL.Approval_Remarks,Cust_Code,TSPL_TRANSACTION_APPROVAL.SMS_Content,TSPL_TRANSACTION_APPROVAL.Email_Content    from TSPL_PROGRAM_MASTER inner join TSPL_TRANSACTION_APPROVAL on TSPL_PROGRAM_MASTER.Program_Code =TSPL_TRANSACTION_APPROVAL.Program_Code where TSPL_TRANSACTION_APPROVAL.Screen_Name ='" + code + "' and approve='0'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New ClsTransactionApproval
                obj.Screen_Name = clsCommon.myCstr(dt1.Rows(0)("Screen_Name"))
                obj.Program_Code = clsCommon.myCstr(dt1.Rows(0)("Program_code"))
                obj.Doc_Date = clsCommon.myCstr(dt1.Rows(0)("Doc_Date"))
                obj.Document_No = clsCommon.myCstr(dt1.Rows(0)("Document_No"))
                obj.Approval_Type = clsCommon.myCstr(dt1.Rows(0)("Approval_Type"))
                obj.Approval_Remarks = clsCommon.myCstr(dt1.Rows(0)("Approval_Remarks"))
                obj.SMS_Content = clsCommon.myCstr(dt1.Rows(0)("SMS_Content"))
                obj.Email_Content = clsCommon.myCstr(dt1.Rows(0)("Email_Content"))
                obj.Cust_Code = clsCommon.myCstr(dt1.Rows(0)("Cust_Code"))
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Microsoft.Office.Interop
Imports System
Imports System.Xml
Imports System.Net
Imports System.Net.WebRequest
Imports System.Net.WebResponse
Imports System.Web
Imports System.Configuration
Imports System.Data.SqlClient
Public Class clsSendBillToDCS
#Region "Variables"
    Public Code As String = Nothing
    Public Email_Subject As String = Nothing
    Public Email_Text As String = Nothing
    Public Attachment_1_Path As String = Nothing
    Public Attachment_2_Path As String = Nothing
    Public arrEMail As List(Of String) = Nothing
    Private Attachment_1_File_Name As String = Nothing
    Private Attachment_2_File_Name As String = Nothing
    Public Created_Date As DateTime? = Nothing
    Public IsBodyHtml As Integer = 0
    Public Against_PO_NO As String = Nothing
    Public Email_ID As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal FormID As String, ByVal obj As clsSendBillToDCS, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = " select max(Code) from TSPL_EMAIL_HEAD where Code like (select Description from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.SMSPrefix + "' and Type='" + clsFixedParameterType.SMSPrefix + "')+'%'"
            obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(obj.Code) > 0 Then
                obj.Code = clsCommon.incval(obj.Code)
            Else
                Dim SMSPrefix As String = clsFixedParameter.GetData(clsFixedParameterCode.SMSPrefix, clsFixedParameterType.SMSPrefix, trans)
                If clsCommon.myLen(SMSPrefix) <= 0 Then
                    Throw New System.Exception("Please ask you administrator to set SMS Prefix in Fixed parameter")
                End If
                obj.Code = SMSPrefix + "0000000000001"
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Email_Subject", obj.Email_Subject)
            clsCommon.AddColumnsForChange(coll, "Email_Text", obj.Email_Text)
            clsCommon.AddColumnsForChange(coll, "Email_ID", obj.Email_ID)
            clsCommon.AddColumnsForChange(coll, "Against_PO_NO", obj.Against_PO_NO, True)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            If clsCommon.CompairString(clsCommon.myCstr(obj.IsBodyHtml), "1") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "IsBodyHtml", obj.IsBodyHtml)
            End If
            If obj.Created_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm tt"))
            End If


            Dim FileInfo As FileInfo
            If clsCommon.myLen(obj.Attachment_1_Path) > 0 Then
                FileInfo = New FileInfo(obj.Attachment_1_Path)
                obj.Attachment_1_File_Name = FileInfo.Name
                clsCommon.AddColumnsForChange(coll, "Attachment_1_File_Name", obj.Attachment_1_File_Name)
            End If
            If clsCommon.myLen(obj.Attachment_2_Path) > 0 Then
                FileInfo = New FileInfo(obj.Attachment_2_Path)
                obj.Attachment_2_File_Name = FileInfo.Name
                clsCommon.AddColumnsForChange(coll, "Attachment_2_File_Name", obj.Attachment_2_File_Name)
            End If
            qry = "select TSPL_EMPLOYEE_MASTER.EMail_ID from TSPL_ES_Content_Emp_Detail inner join TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_ES_Content_Emp_Detail.Emp_code "
            qry += " where LEN(isnull( EMail_ID,''))>0 and TSPL_ES_Content_Emp_Detail.Form_Id='" + FormID + "' and Alert_Type='1'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If obj.arrEMail Is Nothing OrElse obj.arrEMail.Count <= 0 Then
                    obj.arrEMail = New List(Of String)
                End If
                For Each dr As DataRow In dt.Rows
                    obj.arrEMail.Add(clsCommon.myCstr(dr("EMail_ID")))
                Next
            End If
            Dim strEmail As String = ""
            If obj.arrEMail Is Nothing OrElse obj.arrEMail.Count <= 0 Then
                Throw New System.Exception("No Email ID  found to send EMail")
            End If
            For Each Item As String In obj.arrEMail
                strEmail += Item + ";"
            Next
            clsCommon.AddColumnsForChange(coll, "EMail_ID", strEmail)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMAIL_HEAD", OMInsertOrUpdate.Insert, "", trans)
            If clsCommon.myLen(obj.Attachment_1_Path) > 0 Then
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(obj.Attachment_1_Path))
                bData = br.ReadBytes(br.BaseStream.Length)
                Dim str As String = " UPDATE TSPL_EMAIL_HEAD set Attachment_1 = @BLOBData  where CODE='" + obj.Code + "'"
                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                Dim prm As New SqlParameter("@BLOBData", bData)
                cmd.Parameters.Add(prm)
                cmd.ExecuteNonQuery()
                br.Close() ' done by stuti reagrding memory leakage
            End If
            If clsCommon.myLen(obj.Attachment_2_Path) > 0 Then
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(obj.Attachment_2_Path))
                bData = br.ReadBytes(br.BaseStream.Length)
                Dim str As String = " UPDATE TSPL_EMAIL_HEAD set Attachment_2 = @BLOBData  where CODE='" + obj.Code + "'"
                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                Dim prm As New SqlParameter("@BLOBData", bData)
                cmd.Parameters.Add(prm)
                cmd.ExecuteNonQuery()
                br.Close() ' done by stuti reagrding memory leakage
            End If
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function
End Class

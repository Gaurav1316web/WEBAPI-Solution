Imports common
Imports System.Data.SqlClient
Public Class FrmSendSMSMultipleUser
    Inherits FrmMainTranScreen
    Public strMobileNowithComa As String = Nothing
    ' Ticket No : ERO/04/12/18-000425 By Prabhakar Create New Screen 
    Private Sub FrmSendSMSMultipleUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim qry As String = "alter table TSPL_SMS_HEAD alter column SMS_Text nvarchar(4000)  "
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSendSMS_Click(sender As Object, e As EventArgs) Handles btnSendSMS.Click
        Try
            If clsCommon.MyMessageBoxShow("Do you want to Send SMS?", "Send SMS confirmation", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                If txtMobileNo.Text = "" Then
                    txtMobileNo.Focus()
                    Throw New Exception("Please Enter Mobile No")
                End If
                If txtSMSText.Text = "" Then
                    txtSMSText.Focus()
                    Throw New Exception("Please Enter SMS Content")
                End If
                Dim objSMSH As New clsSMSHead()
                objSMSH.SMS_Text = txtSMSText.Text
                Dim Mobiles As String() = txtMobileNo.Text.Split(New Char() {","c})
                If Mobiles.Length <= 0 Then
                    Throw New Exception("Enter Valid Mobile No")
                Else
                    objSMSH.arrMobilNo = New List(Of String)
                    For Each Mobile In Mobiles
                        If (clsCommon.myLen(Mobile) < 10) Or (clsCommon.myLen(Mobile) > 10) Then
                            Throw New Exception("Invalid Mobile No : " + Mobile + ".Mobile No Should be 10 digits.")
                        End If
                        If IsNumeric(Mobile) = False Then
                            Throw New Exception("Invalid Mobile No : " + Mobile + ".Mobile Number Should be Numeric")
                        End If
                        objSMSH.arrMobilNo.Add(Mobile)
                    Next
                End If
                objSMSH.SaveData(clsUserMgtCode.FrmSendSMSMultipleUser, objSMSH, Nothing)
                objSMSH = Nothing
                clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    ' for Export
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = "select '' as [Name],'' as [Mobile No]"
        transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)
    End Sub

    ' For Import
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0
            Dim MobileNoWithComa As String = Nothing
            Dim Strs As List(Of String) = New List(Of String)
            Strs.Add("Name")
            Strs.Add("Mobile No")
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim strMobileNo As String = clsCommon.myCstr(grow.Cells("Mobile No").Value)
                        If clsCommon.myLen(strMobileNo) > 0 Then
                            If (clsCommon.myLen(clsCommon.myCstr(strMobileNo)) < 10) Or (clsCommon.myLen(clsCommon.myCstr(strMobileNo)) > 15) Then
                                Throw New Exception("Invalid Mobile No : " + clsCommon.myCstr(strMobileNo) + "at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        If String.IsNullOrEmpty(MobileNoWithComa) = True Then
                            MobileNoWithComa = strMobileNo
                        Else
                            MobileNoWithComa = MobileNoWithComa + "," + strMobileNo
                        End If
                        linno += 1
                    Next
                    If clsCommon.myLen(MobileNoWithComa) > 0 Then
                        txtMobileNo.Text = MobileNoWithComa
                    Else
                        Throw New Exception("Please Enter Valid Mobile No In Sheet.")
                    End If
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtMobileNo.Text = ""
        txtSMSText.Text = ""
        lblNoOfCharater.Text = "0"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtSMSText_TextChanged(sender As Object, e As EventArgs) Handles txtSMSText.TextChanged
        lblNoOfCharater.Text = txtSMSText.TextLength.ToString()
    End Sub
End Class

'Public Class clsSMSHeadForScreen
'#Region "Variables"
'    Public Code As String = Nothing
'    Public SMS_Text As String = Nothing
'    Public MobileNowithcoma As String = ""
'    Public arrMobilNo As List(Of String) = Nothing
'#End Region
'    Public Function SaveData(ByVal FormID As String, ByVal obj As clsSMSHeadForScreen, ByVal trans As SqlTransaction) As Boolean
'        Try
'            Dim qry As String = " select max(Code) from TSPL_SMS_HEAD where  Code like (select Description from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.SMSPrefix + "' and Type='" + clsFixedParameterType.SMSPrefix + "')+'%'"
'            obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
'            If clsCommon.myLen(obj.Code) > 0 Then
'                obj.Code = clsCommon.incval(obj.Code)
'            Else
'                Dim SMSPrefix As String = clsFixedParameter.GetData(clsFixedParameterCode.SMSPrefix, clsFixedParameterType.SMSPrefix, trans)
'                If clsCommon.myLen(SMSPrefix) <= 0 Then
'                    Throw New System.Exception("Please ask you administrator to set SMS Prefix")
'                End If
'                obj.Code = SMSPrefix + "0000000000001"
'            End If

'            Dim coll As New Hashtable()
'            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
'            clsCommon.AddColumnsForChange(coll, "SMS_Text", obj.SMS_Text)
'            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
'            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
'            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SMS_HEAD", OMInsertOrUpdate.Insert, "", trans)
'            Dim strMobileNo As String = obj.MobileNowithcoma
'            Dim Mobiles As String() = strMobileNo.Split(New Char() {","c})
'            Dim Mobile As String
'            If Mobiles.Length > 0 Then
'                If obj.arrMobilNo Is Nothing OrElse obj.arrMobilNo.Count <= 0 Then
'                    obj.arrMobilNo = New List(Of String)
'                End If
'                For Each Mobile In Mobiles
'                    If (clsCommon.myLen(clsCommon.myCstr(Mobile)) < 10) Or (clsCommon.myLen(clsCommon.myCstr(Mobile)) > 10) Then
'                        clsCommon.MyMessageBoxShow("Invalid Mobile No : " + clsCommon.myCstr(Mobile) + ".Mobile No Should be 10 digits.")
'                        Return False
'                    End If
'                    If IsNumeric(clsCommon.myCstr(Mobile)) = False Then
'                        clsCommon.MyMessageBoxShow("Invalid Mobile No : " + clsCommon.myCstr(Mobile) + ".Mobile Number Should be Numeric")
'                        Return False
'                    End If
'                    Dim count As Integer = 0
'                    For Each Mobile2 In Mobiles
'                        If clsCommon.CompairString(clsCommon.myCstr(Mobile2), clsCommon.myCstr(Mobile)) = CompairStringResult.Equal Then
'                            count += 1
'                        End If
'                    Next
'                    If count > 1 Then
'                        clsCommon.MyMessageBoxShow("Mobile No : " + clsCommon.myCstr(Mobile) + " Entered more than one time.Please Remove duplicate Mobile Number.")
'                        Return False
'                    End If
'                    obj.arrMobilNo.Add(clsCommon.myCstr(Mobile))
'                Next
'            End If
'            If obj.arrMobilNo.Count <= 0 Then
'                clsCommon.MyMessageBoxShow("Enter Valid Mobile No")
'                Return False
'            End If
'            clsSMSDetail.SaveData(obj.Code, obj.arrMobilNo, trans)
'        Catch err As System.Exception
'            Throw New System.Exception(err.Message)
'        End Try
'        Return True
'    End Function

'End Class

'Public Class clsSMSDetailFroScreen
'#Region "Variables"
'    Public Code As String = Nothing
'    Public Mobile_No As String = Nothing
'#End Region

'    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of String), ByVal trans As SqlTransaction) As Boolean
'        Try
'            Dim arrRepeat As New List(Of String)
'            For Each Item As String In Arr
'                If arrRepeat.Contains(Item) Then
'                    Continue For
'                Else
'                    arrRepeat.Add(Item)
'                End If

'                Dim coll As New Hashtable()
'                clsCommon.AddColumnsForChange(coll, "Code", strCode)
'                clsCommon.AddColumnsForChange(coll, "Mobile_No", Item)
'                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SMS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
'            Next
'            arrRepeat = Nothing
'        Catch err As System.Exception
'            Throw New System.Exception(err.Message)
'        End Try
'        Return True
'    End Function
'End Class

Imports System.Data.SqlClient
Imports common
Imports System.Net.NetworkInformation
Public Class clsLoginInfo
    Dim Login_Code As String
    Dim LoginDateTime As DateTime
    Dim LogOutDateTime As DateTime
    Dim User_Code As String
    Dim IP_Address As String
    Dim MAC_Address As String
    Dim Machine_Name As String
    Dim Comp_Code As String
    Private Function getDate() As clsLoginInfo
        Dim obj As clsLoginInfo = New clsLoginInfo()
        Return obj
    End Function
    Public Function SaveData() As Boolean
        Dim IsSaved As Boolean = True
        ''Not saving data more than two table.
        ''Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim str_qry As String = ""
        str_qry += " select Login_Code  from  TSPL_UserLogin_Info "
        str_qry += " where Logout_DateTime Is null and User_Code = '" + objCommonVar.CurrentUserCode + "' "
        str_qry += " and login_code in (select MAX(Login_Code ) from TSPL_UserLogin_Info group by User_Code,MAC_Address  ) order by Login_DateTime "
        Dim str_SessionId As String = clsDBFuncationality.getSingleValue(str_qry)

        If clsCommon.myLen(str_SessionId) > 0 Then
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Logout_DateTime", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_UserLogin_Info", OMInsertOrUpdate.Update, "Login_Code ='" + str_SessionId + "'")
        End If
        
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Login_Code", fnAutoGenerateNo())
            objCommonVar.CurrentLoginID = fnAutoGenerateNo()
            clsCommon.AddColumnsForChange(coll, "User_Code", objCommonVar.CurrentUserCode)
            Machine_Name = System.Net.Dns.GetHostName()
            clsCommon.AddColumnsForChange(coll, "Login_DateTime", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "IP_Address", clsPortSetting.getMachineIP())
            Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            clsCommon.AddColumnsForChange(coll, "MAC_Address", networkcard(0).GetPhysicalAddress.ToString())
            clsCommon.AddColumnsForChange(coll, "Machine_Name", My.Computer.Name)
            clsCommon.AddColumnsForChange(coll, "Window_User_Name", My.User.Name)
            clsCommon.AddColumnsForChange(coll, "Connection_SP_ID", clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select @@SPID")))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_UserLogin_Info", OMInsertOrUpdate.Insert, "")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function Logout() As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Logout_DateTime", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_UserLogin_Info", OMInsertOrUpdate.Update, "Login_Code ='" + objCommonVar.CurrentLoginID + "'")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function fnAutoGenerateNo() As String
        Dim Sql As String = "SELECT MAX(Login_Code) as MaxValue from TSPL_UserLogin_Info  where Login_Code like '%U%' "
        Dim Maxvlu As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql))
        If clsCommon.myLen(Maxvlu) > 0 Then
            Maxvlu = clsCommon.incval(Maxvlu)
        Else
            Maxvlu = "U0000000001"
        End If
        Return Maxvlu
    End Function

    Public Shared Function funGetActiveUserQuery(ByVal isNotConsiderCurrentSPID As Boolean) As String
        Return funGetActiveUserQuery(isNotConsiderCurrentSPID, "")
    End Function
    Public Shared Function funGetActiveUserQuery(ByVal isNotConsiderCurrentSPID As Boolean, ByVal strCurrentUserCode As String) As String
        Dim qry As String = "select (ROW_NUMBER() over (Partition by RowNO order by RowNO desc))as SNo,Login_Code as [Login Code],Connection_SP_ID as [SP ID],replace(Window_User_Name,Machine_Name+'\','') as [Window User Name],IP_Address as [IP Address],MAC_Address as [MAC Address],Machine_Name as [Machine Name],User_Code as [ERP User Code],Login_DateTime as [Login At] from ( " + Environment.NewLine +
            "select 1 as RowNO,TSPL_USERLOGIN_INFO. *, (ROW_NUMBER() over (Partition by connection_sp_id,machine_name order by Login_DateTime desc))as SNo   " + Environment.NewLine +
            " from TSPL_USERLOGIN_INFO " + Environment.NewLine +
            "inner join(SELECT 	spid,hostname FROM sys.sysprocesses WHERE  dbid > 0   and DB_NAME(dbid) in (select DataBase_Name from TSPL_COMPANY_MASTER where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "') and sys.sysprocesses.program_name='.Net SqlClient Data Provider') ActiveConn on ActiveConn.spid=TSPL_USERLOGIN_INFO.Connection_SP_ID  and ActiveConn.hostname=TSPL_USERLOGIN_INFO.Machine_Name COLLATE SQL_Latin1_General_CP1_CI_AS" + Environment.NewLine +
            ")xx where SNo=1 and User_Code not in ('XpertSMSApp','XpertSyncApp','XpertAlertApp','XpertBioMetric','XpertBookingSchedularApp','XpertDispatchSchedularApp') and DATEDIFF(hour,Login_DateTime,GETDATE())<24"
        If isNotConsiderCurrentSPID Then
            qry += " and Connection_SP_ID not in (select @@SPID)"
        End If
        If clsCommon.myLen(strCurrentUserCode) > 0 Then
            qry += " and User_Code ='" + strCurrentUserCode + "'"
        End If
        Dim FinalQry As String = qry
        If clsCommon.myLen(strCurrentUserCode) <= 0 AndAlso (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SameuserCanNotloginmultipletimes, clsFixedParameterCode.SameuserCanNotloginmultipletimes, Nothing)) = 1) Then ''ERO/14/11/19-001100 by balwinder on 19/11/2019
            FinalQry = "select * from (" + qry + Environment.NewLine + " union " + Environment.NewLine + " select xxx.SNo, case when xxx.[Login Code] is null then 'Reserved Licence' else xxx.[Login Code] end as  [Login Code],xxx.[SP ID],xxx.[Window User Name] , xxx.[IP Address],xxx.[MAC Address],xxx.[Machine Name],TSPL_USER_MASTER.User_Code as [ERP User Code],xxx.[Login At]" + Environment.NewLine + _
            " from TSPL_USER_MASTER" + Environment.NewLine + _
            " left outer join  (" + qry + ")xxx on xxx.[ERP User Code]=TSPL_USER_MASTER.User_Code where Licence_Reserved=1)xxxxx"
        Else
            FinalQry = qry
        End If
        FinalQry += " order by [Login At]"
        Return FinalQry

    End Function
End Class

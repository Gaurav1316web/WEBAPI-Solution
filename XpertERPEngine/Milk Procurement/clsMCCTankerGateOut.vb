Imports common
Imports System.Data.SqlClient
Public Class clsMCCTankerGateOut
#Region "Variable"
    Public GATE_OUT_NO As String = Nothing
    Public GATE_OUT_DATE As Date = Nothing
    Public MCC_CODE As String = Nothing
    Public LOCATION_CODE As String = Nothing
    Public IS_POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public MCC_DESC As String = Nothing
    Public LOCATION_DESC As String = Nothing
    Public TANKER_NO As String = Nothing
    Public TO_LOCATION_CODE As String = Nothing
    Public Mcc_Plant As String = Nothing
    Public Storage_Capacity As Decimal = Nothing
    Public PhoneNo As String = Nothing
    Public DriverName As String = Nothing
    Public Remarks As String = Nothing
    Public IsCancel As Integer = Nothing
    Public MCC_CODE2 As String = Nothing
    Public MCC_CODE3 As String = Nothing
    Public Distance_of_Route As Decimal = 0
    Public Bulk_Route_Code As String = Nothing
    Public Opening_Km As Decimal = 0
    Public TollAmount As Decimal = 0
    Public IsContractor As Integer = 0

#End Region
    Public Shared Function saveData(ByVal obj As clsMCCTankerGateOut, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim issaved As Boolean = True
        Try
            Dim qry As String = "Select 1 from TSPL_MCC_TANKER_GATE_OUT Where is_posted=1 and GATE_OUT_NO='" + obj.GATE_OUT_NO + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + obj.GATE_OUT_NO + " is already posted.")
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTankerWithoutCheckingAnyValidation, clsFixedParameterCode.ShowTankerWithoutCheckingAnyValidation, trans)) = 1 Then
            Else
                qry = "select 1 from (" + GetPendingTankerNoQry(obj.GATE_OUT_NO) + ")xxxx where xxxx.TankerNo='" + obj.TANKER_NO + "' "
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Tanker No -" + obj.TANKER_NO + " is in use.")
                End If
                dt = Nothing
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TANKER_NO", obj.TANKER_NO)
            clsCommon.AddColumnsForChange(coll, "GATE_OUT_DATE", clsCommon.GetPrintDate(obj.GATE_OUT_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Mcc_Plant", obj.Mcc_Plant, True)
            clsCommon.AddColumnsForChange(coll, "Storage_Capacity", obj.Storage_Capacity)
            clsCommon.AddColumnsForChange(coll, "TO_LOCATION_CODE", obj.TO_LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PhoneNo", obj.PhoneNo, True)
            clsCommon.AddColumnsForChange(coll, "DriverName", obj.DriverName, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE2", obj.MCC_CODE2, True)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE3", obj.MCC_CODE3, True)
            clsCommon.AddColumnsForChange(coll, "Distance_of_Route", obj.Distance_of_Route)
            clsCommon.AddColumnsForChange(coll, "Bulk_Route_Code", obj.Bulk_Route_Code, True)
            clsCommon.AddColumnsForChange(coll, "Opening_Km", obj.Opening_Km)
            clsCommon.AddColumnsForChange(coll, "TollAmount", obj.TollAmount)
            clsCommon.AddColumnsForChange(coll, "IsContractor", obj.IsContractor)
            If isNewEntry Then
                obj.GATE_OUT_NO = clsERPFuncationality.GetNextCode(trans, obj.GATE_OUT_DATE, clsDocType.MCCTankerGateOut, "", obj.LOCATION_CODE)
                If (clsCommon.myLen(obj.GATE_OUT_NO) <= 0) Then
                    Throw New Exception("Error in Gate Out No Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "GATE_OUT_NO", obj.GATE_OUT_NO)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT", OMInsertOrUpdate.Update, "TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO='" + obj.GATE_OUT_NO + "'", trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsMCCTankerGateOut
        Dim obj As New clsMCCTankerGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "Select TSPL_MCC_TANKER_GATE_OUT.*,tspl_Mcc_Master.Mcc_Name , tspl_Location_Master.Location_Desc  from  TSPL_MCC_TANKER_GATE_OUT  LEFT OUTER JOIN tspl_Mcc_Master On tspl_Mcc_Master.MCC_CODE =TSPL_MCC_TANKER_GATE_OUT.MCC_CODE  LEFT OUTER JOIN tspl_Location_Master On tspl_Location_Master .LOCATION_CODE = TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE where 2=2 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO in (select min(GATE_OUT_NO ) from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO in (select MIN(GATE_OUT_NO ) from TSPL_MCC_TANKER_GATE_OUT where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO in (select Max(GATE_OUT_NO ) from TSPL_MCC_TANKER_GATE_OUT where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO in (select Max(GATE_OUT_NO ) from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.GATE_OUT_NO = clsCommon.myCstr(dt.Rows(0)("GATE_OUT_NO"))
                obj.GATE_OUT_DATE = clsCommon.myCDate(dt.Rows(0)("Gate_Out_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
                obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")) ' 
                obj.IS_POSTED = clsCommon.myCdbl(dt.Rows(0)("IS_POSTED"))
                obj.MCC_DESC = clsCommon.myCstr(dt.Rows(0)("Mcc_Name"))
                obj.LOCATION_DESC = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.TANKER_NO = clsCommon.myCstr(dt.Rows(0)("TANKER_NO"))
                obj.Mcc_Plant = clsCommon.myCstr(dt.Rows(0)("Mcc_Plant"))
                obj.Storage_Capacity = clsCommon.myCdbl(dt.Rows(0)("Storage_Capacity"))
                obj.TO_LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("TO_LOCATION_CODE"))
                obj.PhoneNo = clsCommon.myCstr(dt.Rows(0)("PhoneNo"))
                obj.DriverName = clsCommon.myCstr(dt.Rows(0)("DriverName"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.IsCancel = clsCommon.myCdbl(dt.Rows(0)("IsCancel"))
                obj.MCC_CODE2 = clsCommon.myCstr(dt.Rows(0)("MCC_CODE2"))
                obj.MCC_CODE3 = clsCommon.myCstr(dt.Rows(0)("MCC_CODE3"))
                obj.Distance_of_Route = clsCommon.myCdbl(dt.Rows(0)("Distance_of_Route"))
                obj.Bulk_Route_Code = clsCommon.myCstr(dt.Rows(0)("Bulk_Route_Code"))
                obj.Opening_Km = clsCommon.myCdbl(dt.Rows(0)("Opening_Km"))
                obj.TollAmount = clsCommon.myCdbl(dt.Rows(0)("TollAmount"))
                obj.IsContractor = clsCommon.myCdbl(dt.Rows(0)("IsContractor"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_MCC_TANKER_GATE_OUT Where is_posted=1 and GATE_OUT_NO='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + strDocNo + " is already posted.")
            End If

            qry = "delete from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getGateOutFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "  select TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO,TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE  " &
                ",TANKER_NO,TSPL_MCC_TANKER_GATE_OUT.mcc_code AS [MCC Code],COALESCE(tspl_mcc_master.MCC_NAME,tspl_location_master.LOCATION_DESC) as [MCC Name]" &
                ",TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE as [Loaction Code],COALESCE(LOC.LOCATION_DESC,MCCLOC.MCC_NAME) as [Location Name],case when TSPL_MCC_TANKER_GATE_OUT.IsCancel=1 then 'Y' else 'N' end as [Is Cancel] From TSPL_MCC_TANKER_GATE_OUT " &
                " left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MCC_TANKER_GATE_OUT.mcc_code " &
                " Left outer join tspl_location_master On tspl_location_master.Location_Code=TSPL_MCC_TANKER_GATE_OUT.mcc_code " &
            " Left outer join tspl_mcc_master MCCLOC on MCCLOC.mcc_code=TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE " &
            " Left outer join tspl_location_master LOC on LOC.Location_Code=TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE "
            str = customFinder.getFinder("MCC@GATEOUT12", qry, "", curcode, "", "GATE_OUT_NO")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_MCC_TANKER_GATE_OUT Where is_posted=1 And GATE_OUT_NO='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + strDocNo + " is already posted.")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "IS_POSTED", 1)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT", OMInsertOrUpdate.Update, "GATE_OUT_NO='" + strDocNo + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function Cancel(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_MCC_TANKER_GATE_OUT Where IsCancel=1 and GATE_OUT_NO='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + strDocNo + " is already cancel.")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "IsCancel", 1)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT", OMInsertOrUpdate.Update, "GATE_OUT_NO='" + strDocNo + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPendingTankerNoQry(ByVal strDocNo As String) As String
        Dim qry As String = "select * from ( select TankerNo  ,max(tanker_transporter_Code) as [Tanker Transporter Code],max(Description) as [Tanker Transporter Name]  from (" + Environment.NewLine +
        "select Tanker_No as TankerNo, tanker_transporter_Code,Description,1 as RI ,1 as chk" + Environment.NewLine +
        "from tspl_tanker_master " + Environment.NewLine +
        "where isGateOut=1   AND ISNULL(Ref_Doc_No,'')='' " + Environment.NewLine +
        "union all" + Environment.NewLine +
        "select tanker_no,'' as tanker_transporter_Code,'' as Description,-1 as RI,0 as chk " + Environment.NewLine +
        "from TSPL_MCC_TANKER_GATE_OUT" + Environment.NewLine +
        "where isnull(IS_POSTED,0)=0 and GATE_OUT_NO not in ('" + strDocNo + "')" + Environment.NewLine +
        "union all" + Environment.NewLine +
        "select tanker_no,'' as tanker_transporter_Code,'' as Description,-1 as RI,0 as chk " + Environment.NewLine +
        "from TSPL_MCC_TANKER_GATE_OUT" + Environment.NewLine +
        "where isnull(IS_POSTED,0)=1 " + Environment.NewLine +
        "and not exists(select 1 from TSPL_MCC_Dispatch_Challan where TSPL_MCC_Dispatch_Challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO)  " + Environment.NewLine +
        ")xx  group by TankerNo having sum(chk)>0 and sum(ri)>0)xxx"
        Return qry
        ''BHA/11/07/18-000144 by balwinder added isnull(IS_POSTED,0)=0 becuase only unposted document should come.
        ''BHA/09/07/18-000141 not found the reason when tanker master gateout=0.
    End Function

    'Public Shared Function GetPendingGateOutQry(ByVal strDocNo As String, ByVal tran As SqlTransaction) As String
    '    Dim qry As String = "select * from (" + Environment.NewLine +
    '    "select GATE_OUT_NO,max(GATE_OUT_DATE) as GATE_OUT_DATE,max(xx.MCC_CODE) as MCC_CODE,max(COALESCE(TSPL_MCC_MASTER.MCC_NAME,LOC.LOCATION_DESC)) as MCC_NAME,max(xx.LOCATION_CODE) as LOCATION_CODE,max(COALESCE(TSPL_LOCATION_MASTER.Location_Desc,MCCLOC.MCC_NAME)) as Location_Desc,max(TANKER_NO) as TANKER_NO,max(Tanker_Transporter_Code) as Tanker_Transporter_Code,max(Description) as [Tanker Transporter Name] from (" + Environment.NewLine +
    '    "select TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO,TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE,TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE,TSPL_MCC_TANKER_GATE_OUT.TANKER_NO,TSPL_TANKER_MASTER.Tanker_Transporter_Code,TSPL_TANKER_MASTER.Description ,(case when len(isnull(MCC_CODE3,''))>0 then 3 else (case when len(isnull(MCC_CODE2,''))>0 then 2 else 1 end) end) as RI,1 as chk" + Environment.NewLine +
    '    "from TSPL_MCC_TANKER_GATE_OUT" + Environment.NewLine +
    '    "left outer join TSPL_TANKER_MASTER on tspl_tanker_master.tanker_no=TSPL_MCC_TANKER_GATE_OUT.TANKER_NO" + Environment.NewLine +
    '    " left outer join TSPL_MCC_TANKER_GATE_OUT_SECURITY on TSPL_MCC_TANKER_GATE_OUT_SECURITY.GATE_OUT_NO =TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO" + Environment.NewLine +
    '    "where TSPL_MCC_TANKER_GATE_OUT.is_posted=1 and TSPL_MCC_TANKER_GATE_OUT.IsCancel=0 " + Environment.NewLine
    '    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCBulkProcumentSecurityGateOut, clsFixedParameterCode.MCCBulkProcumentSecurityGateOut, tran)) = 1 Then
    '        qry += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Is_Posted=1"
    '    End If
    '    qry += " union all" + Environment.NewLine +
    '    "select Against_Gate_Out as GATE_OUT_NO,null as GATE_OUT_DATE,null as MCC_CODE,null as LOCATION_CODE,null as TANKER_NO,null as Tanker_Transporter_Code,null as Description ,-1 as RI,0 as chk from TSPL_MCC_Dispatch_Challan where Against_Gate_Out is not null and TSPL_MCC_Dispatch_Challan.Chalan_NO not in ('" + strDocNo + "')" + Environment.NewLine +
    '    ") xx " + Environment.NewLine +
    '    "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xx.MCC_CODE left outer join tspl_location_master LOC on LOC.Location_Code=xx.MCC_CODE " + Environment.NewLine +
    '    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.LOCATION_CODE left outer join tspl_mcc_master MCCLOC on MCCLOC.mcc_code=xx.LOCATION_CODE " + Environment.NewLine +
    '    "group by GATE_OUT_NO having sum(chk)>0 and sum(RI)>0" + Environment.NewLine +
    '    ")xxx"
    '    Return qry
    'End Function

    Public Shared Function GetPendingGateOutQry(ByVal strDocNo As String, ByVal tran As SqlTransaction) As String
        Dim qry As String = " select XXXX.GATE_OUT_NO,XXXX.GATE_OUT_DATE, XXXX.MCC_CODE,COALESCE(TSPL_MCC_MASTER.MCC_NAME,LOC.LOCATION_DESC) as MCC_NAME,XXXX.LOCATION_CODE,COALESCE(TSPL_LOCATION_MASTER.Location_Desc,MCCLOC.MCC_NAME) as Location_Desc,XXXX.TANKER_NO,XXXX.Tanker_Transporter_Code,XXXX.[Tanker Transporter Name] from (" + Environment.NewLine +
        "Select XXX.GATE_OUT_NO,XXX.GATE_OUT_DATE,(case when XXX.CreatedDispatch=2 then XXX.MCC_CODE3 else (case when XXX.CreatedDispatch=1 then XXX.MCC_CODE2 else XXX.MCC_CODE end) end ) as MCC_CODE,XXX.LOCATION_CODE,XXX.TANKER_NO,XXX.Tanker_Transporter_Code,XXX.[Tanker Transporter Name] from (" + Environment.NewLine +
        "Select GATE_OUT_NO,max(GATE_OUT_DATE) as GATE_OUT_DATE,max(xx.MCC_CODE) as MCC_CODE,max(xx.MCC_CODE2) as MCC_CODE2,max(xx.MCC_CODE3) as MCC_CODE3,max(xx.LOCATION_CODE) as LOCATION_CODE,max(TANKER_NO) as TANKER_NO,max(Tanker_Transporter_Code) as Tanker_Transporter_Code,max(Description) as [Tanker Transporter Name], abs(sum(RI * case when RI<0 then 1 else 0 end)) as CreatedDispatch from (" + Environment.NewLine +
        "Select TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO,TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE2,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE3,TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE,TSPL_MCC_TANKER_GATE_OUT.TANKER_NO,TSPL_TANKER_MASTER.Tanker_Transporter_Code,TSPL_TANKER_MASTER.Description ,(case when len(isnull(MCC_CODE3,''))>0 then 3 else (case when len(isnull(MCC_CODE2,''))>0 then 2 else 1 end) end) as RI,1 as chk" + Environment.NewLine +
        "From TSPL_MCC_TANKER_GATE_OUT" + Environment.NewLine +
        "Left outer join TSPL_TANKER_MASTER on tspl_tanker_master.tanker_no=TSPL_MCC_TANKER_GATE_OUT.TANKER_NO" + Environment.NewLine +
        " Left outer join TSPL_MCC_TANKER_GATE_OUT_SECURITY on TSPL_MCC_TANKER_GATE_OUT_SECURITY.GATE_OUT_NO =TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO" + Environment.NewLine +
        "where TSPL_MCC_TANKER_GATE_OUT.is_posted=1 and TSPL_MCC_TANKER_GATE_OUT.IsCancel=0 " + Environment.NewLine
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCBulkProcumentSecurityGateOut, clsFixedParameterCode.MCCBulkProcumentSecurityGateOut, tran)) = 1 Then
            qry += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Is_Posted=1"
        End If
        qry += " union all" + Environment.NewLine +
        "select Against_Gate_Out as GATE_OUT_NO,null as GATE_OUT_DATE,null as MCC_CODE,null as MCC_CODE2,null as MCC_CODE3,null as LOCATION_CODE,null as TANKER_NO,null as Tanker_Transporter_Code,null as Description ,-1 as RI,0 as chk from TSPL_MCC_Dispatch_Challan  " + Environment.NewLine +
        "where Against_Gate_Out is not null and TSPL_MCC_Dispatch_Challan.Chalan_NO not in('" + strDocNo + "')" + Environment.NewLine +
        ") xx " + Environment.NewLine +
        "group by GATE_OUT_NO having sum(chk)>0 and sum(RI)>0" + Environment.NewLine +
        ")XXX" + Environment.NewLine +
        ")XXXX" + Environment.NewLine +
        "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=XXXX.MCC_CODE " + Environment.NewLine +
        "left outer join tspl_location_master LOC on LOC.Location_Code=XXXX.MCC_CODE " + Environment.NewLine +
        "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=XXXX.LOCATION_CODE " + Environment.NewLine +
        "left outer join tspl_mcc_master MCCLOC on MCCLOC.mcc_code=XXXX.LOCATION_CODE"
        Return qry
    End Function
End Class


Public Class clsMCCTankerGateOutSecurity
#Region "Variable"
    Public Doc_No As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Gate_Out_No As String = Nothing
    Public Is_Posted As ERPTransactionStatus = ERPTransactionStatus.Pending

#End Region
    Public Shared Function saveData(ByVal obj As clsMCCTankerGateOutSecurity, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim issaved As Boolean = True
        Try
            Dim qry As String = "Select 1 from TSPL_MCC_TANKER_GATE_OUT_SECURITY Where is_posted=1 and GATE_OUT_NO='" + obj.GATE_OUT_NO + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + obj.Doc_No + " is already posted.")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "GATE_OUT_NO", obj.GATE_OUT_NO)
            If isNewEntry Then
                Dim LocCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select LOCATION_CODE from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO='" + obj.GATE_OUT_NO + "' ", trans))
                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.MCCTankerGateOutSecurity, "", LocCode)
                If (clsCommon.myLen(obj.Doc_No) <= 0) Then
                    Throw New Exception("Error in Gate Out No Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT_SECURITY", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT_SECURITY", OMInsertOrUpdate.Update, "TSPL_MCC_TANKER_GATE_OUT_SECURITY.GATE_OUT_NO='" + obj.GATE_OUT_NO + "'", trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsMCCTankerGateOutSecurity
        Dim obj As New clsMCCTankerGateOutSecurity
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "Select TSPL_MCC_TANKER_GATE_OUT_SECURITY.* from  TSPL_MCC_TANKER_GATE_OUT_SECURITY  where 2=2 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No in (select min(Doc_No ) from TSPL_MCC_TANKER_GATE_OUT_SECURITY where Doc_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No in (select MIN(Doc_No ) from TSPL_MCC_TANKER_GATE_OUT_SECURITY where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No in (select Max(Doc_No ) from TSPL_MCC_TANKER_GATE_OUT_SECURITY where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No in (select Max(Doc_No ) from TSPL_MCC_TANKER_GATE_OUT_SECURITY where Doc_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.GATE_OUT_NO = clsCommon.myCstr(dt.Rows(0)("GATE_OUT_NO"))
                obj.IS_POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("IS_POSTED")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_MCC_TANKER_GATE_OUT_SECURITY Where is_posted=1 and GATE_OUT_NO='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + strDocNo + " is already posted.")
            End If

            qry = "delete from TSPL_MCC_TANKER_GATE_OUT_SECURITY where Doc_NO='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No,TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_Date,TSPL_MCC_TANKER_GATE_OUT_SECURITY.GATE_OUT_NO,case when TSPL_MCC_TANKER_GATE_OUT_SECURITY.is_posted=1 then 'Approved' else 'Pending' end as Status " + Environment.NewLine +
            "From TSPL_MCC_TANKER_GATE_OUT_SECURITY " + Environment.NewLine +
            "left outer join TSPL_MCC_TANKER_GATE_OUT on TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO=TSPL_MCC_TANKER_GATE_OUT_SECURITY.GATE_OUT_NO  "
            str = customFinder.getFinder("MCC@SGTEOUT", qry, whrcls, curcode, "", "Doc_No")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_MCC_TANKER_GATE_OUT_SECURITY Where is_posted=1 and Doc_No='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This document-" + strDocNo + " is already posted.")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "IS_POSTED", 1)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_TANKER_GATE_OUT_SECURITY", OMInsertOrUpdate.Update, "Doc_No='" + strDocNo + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
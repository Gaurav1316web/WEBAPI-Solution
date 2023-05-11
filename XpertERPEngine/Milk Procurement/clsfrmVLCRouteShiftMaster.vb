Imports common
Imports System.Data.SqlClient
Public Class clsfrmVLCRouteShiftMaster
#Region "Variable"
    Public docno As String = Nothing
    Public ddocdate As String = Nothing
    Public desc As String = Nothing
    Public status As String = Nothing
    Public vlccode As String = Nothing
    Public vlcname As String = Nothing
    Public exroutecode As String = Nothing
    Public exroutename As String = Nothing
    Public exvillcode As String = Nothing
    Public exvillname As String = Nothing
    Public newroutecode As String = Nothing
    Public newroutename As String = Nothing
    Public newvillcode As String = Nothing
    Public newvillname As String = Nothing
    Public Route_Code As String = Nothing
    'Public yesno As String = Nothing
    Public griddate As String = Nothing
    Public sno As Integer = Nothing
    Public gridstatus As String = Nothing
    Public mcccode As String = Nothing
    Public mccname As String = Nothing
    Public plantcode As String = Nothing
    Public plantname As String = Nothing
    Public arr As List(Of clsfrmVLCRouteShiftMaster) = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsfrmVLCRouteShiftMaster), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If clsCommon.myLen(strCode) <= 0 Then
                strCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCROUTESHIFT, "", "")
            End If

            Dim qry As String = "select count(*) from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsfrmVLCRouteShiftMaster In arr
                    coll = New Hashtable()

                    obj.docno = strCode

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.ddocdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
                    clsCommon.AddColumnsForChange(coll, "Status", obj.status)
                    clsCommon.AddColumnsForChange(coll, "SNO", obj.sno)
                    clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.vlccode)
                    clsCommon.AddColumnsForChange(coll, "Existing_Route_Code", obj.exroutecode)
                    'clsCommon.AddColumnsForChange(coll, "Existing_Vill_Code", obj.exvillcode)
                    clsCommon.AddColumnsForChange(coll, "Route_Status", obj.gridstatus)
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.griddate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "New_Route_Code", obj.newroutecode)
                    clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                    'clsCommon.AddColumnsForChange(coll, "New_Vill_Code", obj.newvillcode)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Update, " TSPL_VLC_ROUTE_SHIFT_MASTER.doc_no='" + strCode + "'", trans)
                    End If

                    '----------------new route and village updates in vlc master---------------------------------
                    If clsCommon.myLen(obj.newroutecode) > 0 Then
                        Dim coll1 As New Hashtable()
                        'clsCommon.AddColumnsForChange(coll1, "village_code", obj.newvillcode)
                        clsCommon.AddColumnsForChange(coll1, "route_code", obj.newroutecode)
                        clsCommon.AddColumnsForChange(coll1, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll1, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.VLC_Code='" + obj.vlccode + "' and TSPL_VLC_MASTER_HEAD.route_code='" + obj.exroutecode + "'", trans)

                        Dim coll2 As New Hashtable()
                        'clsCommon.AddColumnsForChange(coll1, "village_code", obj.newvillcode)
                        clsCommon.AddColumnsForChange(coll2, "route_code", obj.newroutecode)
                        clsCommon.AddColumnsForChange(coll2, "vlc_code", obj.vlccode)
                        clsCommon.AddColumnsForChange(coll2, "Is_active", 1)
                        qry = "update TSPL_MCC_ROUTE_VLC_MAPPING set Is_active=0 where TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code<>'" + obj.newroutecode + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code='" + obj.newroutecode + "'"
                        Dim checkvlc As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If checkvlc < 0 Then
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, " TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code='" + obj.exroutecode + "'", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code='" + obj.exroutecode + "'", trans)
                        End If
                    End If
                Next
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsfrmVLCRouteShiftMaster
        Try
            Dim qry As String = "select TSPL_VLC_ROUTE_SHIFT_MASTER.Doc_Date,TSPL_VLC_ROUTE_SHIFT_MASTER.Doc_NO as [Route Id],TSPL_VLC_ROUTE_SHIFT_MASTER.Description,TSPL_VLC_ROUTE_SHIFT_MASTER.Status,TSPL_VLC_ROUTE_SHIFT_MASTER.SNO,TSPL_VLC_ROUTE_SHIFT_MASTER.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Desc],TSPL_VLC_ROUTE_SHIFT_MASTER.existing_route_code as [Old Route Code],tspl_mcc_route_master.route_name as [Route Desc],TSPL_VLC_ROUTE_SHIFT_MASTER.route_status as [Status Yes/No],TSPL_VLC_ROUTE_SHIFT_MASTER.effective_date as [Effective Date],TSPL_VLC_ROUTE_SHIFT_MASTER.new_route_code as [New Route Code],a.route_name as [New Route Desc],TSPL_VLC_ROUTE_SHIFT_MASTER.Route_Code ,TSPL_MCC_ROUTE_MASTER.mcc_code,tspl_mcc_master.mcc_name,tspl_mcc_master.Plant_Code,TSPL_LOCATION_MASTER.Location_Desc AS Plant_Name from TSPL_VLC_ROUTE_SHIFT_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_ROUTE_SHIFT_MASTER.vlc_code left outer join tspl_mcc_route_master on tspl_mcc_route_master.route_code=TSPL_VLC_ROUTE_SHIFT_MASTER.existing_route_code left outer join tspl_mcc_route_master a on a.route_code=TSPL_VLC_ROUTE_SHIFT_MASTER.new_route_code left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC left outer join tspl_location_master on tspl_location_master.location_code=tspl_mcc_master.plant_code"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_VLC_ROUTE_SHIFT_MASTER.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_VLC_ROUTE_SHIFT_MASTER.doc_no in (select min(doc_no) from TSPL_VLC_ROUTE_SHIFT_MASTER)"
                Case NavigatorType.Last
                    qry += " where TSPL_VLC_ROUTE_SHIFT_MASTER.doc_no in (select max(doc_no) from TSPL_VLC_ROUTE_SHIFT_MASTER)"
                Case NavigatorType.Next
                    qry += " where TSPL_VLC_ROUTE_SHIFT_MASTER.doc_no in (select min(doc_no) from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_VLC_ROUTE_SHIFT_MASTER.doc_no in (select max(doc_no) from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no<'" + strCode + "')"
            End Select
            qry += " order by TSPL_VLC_ROUTE_SHIFT_MASTER.sno"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim obj As clsfrmVLCRouteShiftMaster = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsfrmVLCRouteShiftMaster()

                obj.arr = New List(Of clsfrmVLCRouteShiftMaster)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsfrmVLCRouteShiftMaster()

                    objtr.docno = clsCommon.myCstr(dr("Route Id"))
                    objtr.ddocdate = clsCommon.myCstr(dr("Doc_Date"))
                    objtr.desc = clsCommon.myCstr(dr("Description"))
                    objtr.status = clsCommon.myCstr(dr("Status"))
                    objtr.sno = CInt(dr("SNO"))
                    objtr.vlccode = clsCommon.myCstr(dr("VLC Code"))
                    objtr.vlcname = clsCommon.myCstr(dr("VLC Desc"))
                    objtr.exroutecode = clsCommon.myCstr(dr("Old Route Code"))
                    objtr.exroutename = clsCommon.myCstr(dr("Route Desc"))
                    'objtr.exvillcode = clsCommon.myCstr(dr("Old Village Code"))
                    'objtr.exvillname = clsCommon.myCstr(dr("Village Name"))
                    objtr.gridstatus = clsCommon.myCstr(dr("Status Yes/No"))
                    objtr.griddate = clsCommon.myCstr(dr("Effective Date"))
                    objtr.newroutecode = clsCommon.myCstr(dr("New Route Code"))
                    objtr.newroutename = clsCommon.myCstr(dr("New Route Desc"))
                    objtr.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    'objtr.newvillcode = clsCommon.myCstr(dr("New Village Code"))
                    'objtr.newvillname = clsCommon.myCstr(dr("New Village Name"))
                    objtr.mcccode = clsCommon.myCstr(dr("mcc_code"))
                    objtr.mccname = clsCommon.myCstr(dr("mcc_name"))
                    objtr.plantcode = clsCommon.myCstr(dr("Plant_Code"))
                    objtr.plantname = clsCommon.myCstr(dr("Plant_Name"))
                    obj.arr.Add(objtr)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select count(*) from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If check <= 0 Then
                Throw New Exception("No Data Found For Deletion")
            End If

            qry = "delete from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveHistoryData(ByVal strCode As String, ByVal arr As List(Of clsfrmVLCRouteShiftMaster), ByVal trans As SqlTransaction) As Boolean
        trans = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsfrmVLCRouteShiftMaster In arr
                    coll = New Hashtable()

                    obj.docno = strCode
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.ddocdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
                    clsCommon.AddColumnsForChange(coll, "Status", obj.status)
                    clsCommon.AddColumnsForChange(coll, "SNO", obj.sno)
                    clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.vlccode)
                    clsCommon.AddColumnsForChange(coll, "Existing_Route_Code", obj.exroutecode)
                    clsCommon.AddColumnsForChange(coll, "Existing_Vill_Code", obj.exvillcode)
                    clsCommon.AddColumnsForChange(coll, "Route_Status", obj.gridstatus)
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.griddate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "New_Route_Code", obj.newroutecode)
                    clsCommon.AddColumnsForChange(coll, "New_Vill_Code", obj.newvillcode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_ROUTE_SHIFT_HISTORY_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

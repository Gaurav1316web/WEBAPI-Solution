Imports common
Imports System.Data.SqlClient
Public Class clsfrmCustomerRouteShiftMaster
#Region "Variable"
    Public docno As String = Nothing
    Public ddocdate As String = Nothing
    Public desc As String = Nothing
    'Public status As String = Nothing
    Public Custcode As String = Nothing
    Public Custname As String = Nothing
    Public exroutecode As String = Nothing
    Public exroutename As String = Nothing
    'Public exvillcode As String = Nothing
    'Public exvillname As String = Nothing
    Public newroutecode As String = Nothing
    Public newroutename As String = Nothing
    'Public newvillcode As String = Nothing
    'Public newvillname As String = Nothing
    Public Route_Code As String = Nothing
    'Public yesno As String = Nothing
    Public griddate As String = Nothing
    Public sno As Integer = Nothing
    'Public gridstatus As String = Nothing
    Public shift As String = Nothing
    'Public mcccode As String = Nothing
    'Public mccname As String = Nothing
    'Public plantcode As String = Nothing
    'Public plantname As String = Nothing
    Public arr As List(Of clsfrmCustomerRouteShiftMaster) = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsfrmCustomerRouteShiftMaster), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If clsCommon.myLen(strCode) <= 0 Then
                strCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.CustomerRouteShift, "", "")
            End If

            Dim qry As String = "select count(*) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsfrmCustomerRouteShiftMaster In arr
                    coll = New Hashtable()

                    obj.docno = strCode

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.ddocdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
                    'clsCommon.AddColumnsForChange(coll, "Status", obj.status)
                    clsCommon.AddColumnsForChange(coll, "SNO", obj.sno)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Custcode)
                    clsCommon.AddColumnsForChange(coll, "Existing_Route_Code", obj.exroutecode)
                    'clsCommon.AddColumnsForChange(coll, "Existing_Vill_Code", obj.exvillcode)
                    clsCommon.AddColumnsForChange(coll, "shift", obj.shift)
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.griddate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "New_Route_Code", obj.newroutecode)
                    clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                    'clsCommon.AddColumnsForChange(coll, "New_Vill_Code", obj.newvillcode)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Update, " TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no='" + strCode + "'", trans)
                    End If

                    '----------------new route and village updates in vlc master---------------------------------
                    If clsCommon.myLen(obj.newroutecode) > 0 Then
                        Dim coll1 As New Hashtable()
                        'clsCommon.AddColumnsForChange(coll1, "village_code", obj.newvillcode)
                        clsCommon.AddColumnsForChange(coll1, "route_no", obj.newroutecode)
                        clsCommon.AddColumnsForChange(coll1, "route_desc", obj.newroutename)
                        clsCommon.AddColumnsForChange(coll1, "Modify_By", objCommonVar.CurrentUserCode)
                        'clsCommon.AddColumnsForChange(coll1, "Modify_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        clsCommon.AddColumnsForChange(coll1, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Update, " TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.Custcode + "' and TSPL_CUSTOMER_MASTER.route_no='" + obj.exroutecode + "'", trans)

                        'Dim coll2 As New Hashtable()
                        ''clsCommon.AddColumnsForChange(coll1, "village_code", obj.newvillcode)
                        'clsCommon.AddColumnsForChange(coll2, "route_code", obj.newroutecode)
                        'clsCommon.AddColumnsForChange(coll2, "vlc_code", obj.vlccode)
                        'clsCommon.AddColumnsForChange(coll2, "Is_active", 1)
                        'qry = "update TSPL_MCC_ROUTE_VLC_MAPPING set Is_active=0 where TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code<>'" + obj.newroutecode + "'"
                        'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        'qry = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code='" + obj.newroutecode + "'"
                        'Dim checkvlc As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        'If checkvlc < 0 Then
                        '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, " TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code='" + obj.exroutecode + "'", trans)
                        'Else
                        '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " TSPL_MCC_ROUTE_VLC_MAPPING.VLC_Code='" + obj.vlccode + "' and TSPL_MCC_ROUTE_VLC_MAPPING.route_code='" + obj.exroutecode + "'", trans)
                        'End If
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

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsfrmCustomerRouteShiftMaster
        Try
            Dim qry As String = "select TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Doc_Date,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Doc_NO,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Description,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.shift,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.SNO,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Cust_Code as [Cust Code],TSPL_Customer_MASTER.Customer_Name as [Cust Desc],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.existing_route_code as [Old Route Code],tspl_route_master.route_desc as [Route Desc],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.effective_date as [Effective Date],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.new_route_code as [New Route Code],a.route_desc as [New Route Desc],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Route_Code from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER left outer join tspl_route_master on tspl_route_master.route_no=TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.existing_route_code left outer join tspl_route_master a on a.route_no=TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.new_route_code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Cust_Code"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no in (select min(doc_no) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER)"
                Case NavigatorType.Last
                    qry += " where TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no in (select max(doc_no) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER)"
                Case NavigatorType.Next
                    qry += " where TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no in (select min(doc_no) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no in (select max(doc_no) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no<'" + strCode + "')"
            End Select
            qry += " order by TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.sno"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim obj As clsfrmCustomerRouteShiftMaster = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsfrmCustomerRouteShiftMaster()

                obj.arr = New List(Of clsfrmCustomerRouteShiftMaster)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsfrmCustomerRouteShiftMaster()

                    objtr.docno = clsCommon.myCstr(dr("Doc_NO"))
                    objtr.ddocdate = clsCommon.myCstr(dr("Doc_Date"))
                    objtr.desc = clsCommon.myCstr(dr("Description"))
                    objtr.shift = clsCommon.myCstr(dr("shift"))
                    objtr.sno = CInt(dr("SNO"))
                    objtr.Custcode = clsCommon.myCstr(dr("Cust Code"))
                    objtr.Custname = clsCommon.myCstr(dr("Cust Desc"))
                    objtr.exroutecode = clsCommon.myCstr(dr("Old Route Code"))
                    objtr.exroutename = clsCommon.myCstr(dr("Route Desc"))
                    'objtr.exvillcode = clsCommon.myCstr(dr("Old Village Code"))
                    'objtr.exvillname = clsCommon.myCstr(dr("Village Name"))
                    'objtr.gridstatus = clsCommon.myCstr(dr("Status Yes/No"))
                    objtr.griddate = clsCommon.myCstr(dr("Effective Date"))
                    objtr.newroutecode = clsCommon.myCstr(dr("New Route Code"))
                    objtr.newroutename = clsCommon.myCstr(dr("New Route Desc"))
                    objtr.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    'objtr.newvillcode = clsCommon.myCstr(dr("New Village Code"))
                    'objtr.newvillname = clsCommon.myCstr(dr("New Village Name"))
                    'objtr.mcccode = clsCommon.myCstr(dr("mcc_code"))
                    'objtr.mccname = clsCommon.myCstr(dr("mcc_name"))
                    'objtr.plantcode = clsCommon.myCstr(dr("Plant_Code"))
                    'objtr.plantname = clsCommon.myCstr(dr("Plant_Name"))
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
            Dim qry As String = "select count(*) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If check <= 0 Then
                Throw New Exception("No Data Found For Deletion")
            End If

            qry = "delete from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Shared Function SaveHistoryData(ByVal strCode As String, ByVal arr As List(Of clsfrmCustomerRouteShiftMaster), ByVal trans As SqlTransaction) As Boolean
    '    trans = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim isSaved As Boolean = True
    '        Dim coll As New Hashtable()

    '        If arr IsNot Nothing AndAlso arr.Count > 0 Then
    '            For Each obj As clsfrmCustomerRouteShiftMaster In arr
    '                coll = New Hashtable()

    '                obj.docno = strCode
    '                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
    '                clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
    '                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.ddocdate, "dd/MMM/yyyy"))
    '                clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
    '                'clsCommon.AddColumnsForChange(coll, "Status", obj.status)
    '                clsCommon.AddColumnsForChange(coll, "SNO", obj.sno)
    '                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Custcode)
    '                clsCommon.AddColumnsForChange(coll, "Existing_Route_Code", obj.exroutecode)
    '                'clsCommon.AddColumnsForChange(coll, "Existing_Vill_Code", obj.exvillcode)
    '                clsCommon.AddColumnsForChange(coll, "Shift", obj.shift)
    '                clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.griddate, "dd/MMM/yyyy"))
    '                clsCommon.AddColumnsForChange(coll, "New_Route_Code", obj.newroutecode)
    '                'clsCommon.AddColumnsForChange(coll, "New_Vill_Code", obj.newvillcode)
    '                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
    '                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
    '                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
    '                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

    '                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ROUTE_SHIFT_HISTORY_MASTER", OMInsertOrUpdate.Insert, "", trans)
    '            Next
    '        End If
    '        trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
End Class

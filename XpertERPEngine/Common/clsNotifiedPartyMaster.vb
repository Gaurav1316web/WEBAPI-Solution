'================BM00000003442
Imports common
Imports System.Data.SqlClient

Public Class clsNotifiedPartyMaster

#Region "variables"
    Public GridArr As List(Of clsNotifiedPartyMasterDetail) = Nothing
    Public Arr As List(Of clsNotifiedPartyMasterDetail) = Nothing
    Public ArrMain As List(Of clsNotifiedPartyMaster) = Nothing
    Public docno As String = Nothing
    Public descrptn As String = Nothing
    Public cust_code As String = Nothing
    Public cust_name As String = Nothing
    Public ship_code As String = Nothing
    Public ship_name As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Desc As String = Nothing
    Public add1 As String = Nothing
    Public add2 As String = Nothing
    Public add3 As String = Nothing
    Public countrycode As String = Nothing
    Public countryname As String = Nothing
    Public statecode As String = Nothing
    Public statename As String = Nothing
    Public citycode As String = Nothing
    Public cityname As String = Nothing
    Public postalcode As String = Nothing
    Public tin_no As String = Nothing
    Public cst_no As String = Nothing
    Public email As String = Nothing
    Public tel_no As String = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_NOTIFY_PARTY_HEAD.Doc_No as Code,TSPL_NOTIFY_PARTY_HEAD.Description,TSPL_NOTIFY_PARTY_HEAD.cust_code as [Customer Code],tspl_customer_master.customer_name as [Customer] from TSPL_NOTIFY_PARTY_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_NOTIFY_PARTY_HEAD.cust_code "
        str = clsCommon.ShowSelectForm("PATFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsNotifiedPartyMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.docno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.NOTIFYPARTY, "", "")
            End If

            Dim qry As String = "delete from TSPL_NOTIFY_PARTY_SHIP_DETAIL where doc_no='" + obj.docno + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '===============header-=================================
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.docno)
            clsCommon.AddColumnsForChange(coll, "Description", obj.descrptn)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.cust_code)
            clsCommon.AddColumnsForChange(coll, "modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFY_PARTY_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFY_PARTY_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.docno + "'", trans)
            End If
            '===============================================

            If obj.ArrMain IsNot Nothing AndAlso obj.ArrMain.Count > 0 Then
                For Each objtr As clsNotifiedPartyMaster In obj.ArrMain
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", obj.docno)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", objtr.cust_code)
                    clsCommon.AddColumnsForChange(coll, "Ship_To_Location_Code", objtr.ship_code)
                    clsCommon.AddColumnsForChange(coll, "Ship_To_Location_Desc", objtr.ship_name)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Loc_Code)
                    clsCommon.AddColumnsForChange(coll, "Add1", objtr.add1)
                    clsCommon.AddColumnsForChange(coll, "Add2", objtr.add2)
                    clsCommon.AddColumnsForChange(coll, "Add3", objtr.add3)
                    clsCommon.AddColumnsForChange(coll, "Country_Code", objtr.countrycode)
                    clsCommon.AddColumnsForChange(coll, "State_Code", objtr.statecode)
                    clsCommon.AddColumnsForChange(coll, "City_Code", objtr.citycode)
                    clsCommon.AddColumnsForChange(coll, "Postal_Code", objtr.postalcode)
                    clsCommon.AddColumnsForChange(coll, "TIN_No", objtr.tin_no)
                    clsCommon.AddColumnsForChange(coll, "CST_No", objtr.cst_no)
                    clsCommon.AddColumnsForChange(coll, "Email_ID", objtr.email)
                    clsCommon.AddColumnsForChange(coll, "TEL_No", objtr.tel_no)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFY_PARTY_SHIP_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    isSaved = isSaved AndAlso clsNotifiedPartyMasterDetail.SaveData(obj.docno, objtr.GridArr, trans)
                Next
            End If

            isSaved = isSaved AndAlso clsNotifiedPartyMasterDetail.SaveData(obj.docno, obj.Arr, trans)
            'isSaved = isSaved AndAlso SHIPTOLOCATIONSAVE(obj, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SHIPTOLOCATIONSAVE(ByVal obj As clsNotifiedPartyMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_SHIP_TO_LOCATION where ship_to_type_code='" + obj.cust_code + "' and trans_type='EXPORT SALE'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If obj.ArrMain IsNot Nothing AndAlso obj.ArrMain.Count > 0 Then
                For Each objtr As clsNotifiedPartyMaster In obj.ArrMain
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "ship_to_type_code", objtr.cust_code)
                    clsCommon.AddColumnsForChange(coll, "ship_to_type_desc", objtr.cust_name)
                    clsCommon.AddColumnsForChange(coll, "ship_to_code", objtr.ship_code)
                    clsCommon.AddColumnsForChange(coll, "ship_to_desc", objtr.ship_name)
                    clsCommon.AddColumnsForChange(coll, "ship_to_type", "S")
                    clsCommon.AddColumnsForChange(coll, "Add1", objtr.add1)
                    clsCommon.AddColumnsForChange(coll, "Add2", objtr.add2)
                    clsCommon.AddColumnsForChange(coll, "Add3", objtr.add3)
                    clsCommon.AddColumnsForChange(coll, "Country", objtr.countrycode)
                    clsCommon.AddColumnsForChange(coll, "State", objtr.statecode)
                    clsCommon.AddColumnsForChange(coll, "City_Code", objtr.citycode)
                    clsCommon.AddColumnsForChange(coll, "Pin_Code", objtr.postalcode)
                    clsCommon.AddColumnsForChange(coll, "TIN_No", objtr.tin_no)
                    clsCommon.AddColumnsForChange(coll, "CST_No", objtr.cst_no)
                    clsCommon.AddColumnsForChange(coll, "Email", objtr.email)
                    clsCommon.AddColumnsForChange(coll, "TELphone", objtr.tel_no)
                    clsCommon.AddColumnsForChange(coll, "Loc_Code", objtr.Loc_Code)
                    clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modify_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "trans_type", "EXPORT SALE")


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIP_TO_LOCATION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Dim counter As Integer = 0

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsNotifiedPartyMasterDetail In obj.Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "cust_code", obj.cust_code)
                    clsCommon.AddColumnsForChange(coll, "Ship_To_Code", objtr.ship_to_code)
                    clsCommon.AddColumnsForChange(coll, "trans_type", "EXPORT SALE")
                    clsCommon.AddColumnsForChange(coll, "Loc_Code", objtr.loc_code)

                    If counter = 0 Then
                        qry = "delete from TSPL_SHIP_TO_LOCATION_LOCATIONS where cust_code='" + obj.cust_code + "' and ship_to_code='" + objtr.ship_to_code + "' and trans_type='EXPORT SALE'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIP_TO_LOCATION_LOCATIONS", OMInsertOrUpdate.Insert, "", trans)
                    counter += 1
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal custcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_NOTIFY_PARTY_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_NOTIFY_PARTY_SHIP_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_NOTIFY_PARTY_HEAD where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_SHIP_TO_LOCATION_LOCATIONS where trans_type='EXPORT SALE' and cust_code='" + custcode + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_SHIP_TO_LOCATION where ship_to_type_code='" + custcode + "' and trans_type='EXPORT SALE'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsNotifiedPartyMaster
        Try
            Dim obj As New clsNotifiedPartyMaster()
            obj.ArrMain = New List(Of clsNotifiedPartyMaster)
            obj.Arr = New List(Of clsNotifiedPartyMasterDetail)
            obj.GridArr = New List(Of clsNotifiedPartyMasterDetail)

            Dim qry As String = "select TSPL_NOTIFY_PARTY_HEAD.Doc_No,TSPL_NOTIFY_PARTY_HEAD.Description,TSPL_NOTIFY_PARTY_HEAD.cust_code,tspl_customer_master.customer_name from TSPL_NOTIFY_PARTY_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_NOTIFY_PARTY_HEAD.cust_code "
            qry += " where 2=2"
            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_NOTIFY_PARTY_HEAD.Doc_No='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_NOTIFY_PARTY_HEAD.Doc_No in (select min(doc_no) from TSPL_NOTIFY_PARTY_HEAD )"
                Case NavigatorType.Last
                    qry += " and TSPL_NOTIFY_PARTY_HEAD.Doc_No in (select max(doc_no) from TSPL_NOTIFY_PARTY_HEAD )"
                Case NavigatorType.Next
                    qry += " and TSPL_NOTIFY_PARTY_HEAD.Doc_No in (select min(doc_no) from TSPL_NOTIFY_PARTY_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_NOTIFY_PARTY_HEAD.Doc_No in (select max(doc_no) from TSPL_NOTIFY_PARTY_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.descrptn = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.cust_code = clsCommon.myCstr(dt.Rows(0)("cust_code"))
                obj.cust_name = clsCommon.myCstr(dt.Rows(0)("customer_name"))

                Dim ship_to_loc_code As String = ""
                Dim cust_code As String = ""
                obj.GridArr = New List(Of clsNotifiedPartyMasterDetail)

                qry = "select TSPL_NOTIFY_PARTY_SHIP_DETAIL.*,tspl_customer_master.customer_name from TSPL_NOTIFY_PARTY_SHIP_DETAIL left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_NOTIFY_PARTY_SHIP_DETAIL.cust_code where TSPL_NOTIFY_PARTY_SHIP_DETAIL.doc_no='" + obj.docno + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

                obj.ArrMain = New List(Of clsNotifiedPartyMaster)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsNotifiedPartyMaster()

                        objtr.cust_code = clsCommon.myCstr(dr("cust_code"))
                        cust_code = objtr.cust_code
                        objtr.cust_name = clsCommon.myCstr(dr("customer_name"))
                        objtr.ship_code = clsCommon.myCstr(dr("Ship_To_Location_Code"))
                        ship_to_loc_code = clsCommon.myCstr(dr("Ship_To_Location_Code"))
                        objtr.ship_name = clsCommon.myCstr(dr("ship_to_location_desc"))
                        objtr.Loc_Code = clsCommon.myCstr(dr("location_code"))
                        objtr.Loc_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + objtr.Loc_Code + "'"))
                        objtr.add1 = clsCommon.myCstr(dr("add1"))
                        objtr.add2 = clsCommon.myCstr(dr("add2"))
                        objtr.add3 = clsCommon.myCstr(dr("add3"))
                        objtr.countrycode = clsCommon.myCstr(dr("country_code"))
                        objtr.countryname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + objtr.countrycode + "'"))
                        objtr.statecode = clsCommon.myCstr(dr("state_code"))
                        objtr.statename = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + objtr.statecode + "'"))
                        objtr.citycode = clsCommon.myCstr(dr("city_code"))
                        objtr.cityname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code='" + objtr.citycode + "'"))
                        objtr.postalcode = clsCommon.myCstr(dr("Postal_Code"))
                        objtr.tin_no = clsCommon.myCstr(dr("TIN_No"))
                        objtr.cst_no = clsCommon.myCstr(dr("CST_No"))
                        objtr.email = clsCommon.myCstr(dr("Email_ID"))
                        objtr.tel_no = clsCommon.myCstr(dr("TEL_No"))
                        objtr.GridArr = GetComboLocations(obj.docno, objtr.cust_code, objtr.ship_code)

                        obj.ArrMain.Add(objtr)
                    Next
                End If
               

                obj.Arr = New List(Of clsNotifiedPartyMasterDetail)

                qry = "select TSPL_NOTIFY_PARTY_DETAIL.*,tspl_customer_master.customer_name from TSPL_NOTIFY_PARTY_DETAIL left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_NOTIFY_PARTY_DETAIL.cust_code where TSPL_NOTIFY_PARTY_DETAIL.doc_no='" + obj.docno + "' and TSPL_NOTIFY_PARTY_DETAIL.cust_code='" + cust_code + "' and TSPL_NOTIFY_PARTY_DETAIL.Ship_To_Location_Code='" + ship_to_loc_code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsNotifiedPartyMasterDetail()

                        objtr.custcode = clsCommon.myCstr(dr("cust_code"))
                        objtr.loc_code = clsCommon.myCstr(dr("Location_Code"))
                        objtr.loc_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + objtr.loc_code + "'"))

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetComboLocations(ByVal strCode As String, ByVal CustCode As String, ByVal Ship_To_Location_Code As String) As List(Of clsNotifiedPartyMasterDetail)
        Try
            Dim obj As New clsNotifiedPartyMaster()
            obj.Arr = New List(Of clsNotifiedPartyMasterDetail)

            Dim qry As String = "select * from TSPL_NOTIFY_PARTY_DETAIL where doc_no='" + strCode + "' and cust_code='" + CustCode + "' and Ship_To_Location_Code='" + Ship_To_Location_Code + "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    Dim objtr1 As New clsNotifiedPartyMasterDetail()
                    objtr1.custcode = clsCommon.myCstr(dr("cust_code"))
                    objtr1.loc_code = clsCommon.myCstr(dr("Location_Code"))
                    objtr1.ship_to_code = clsCommon.myCstr(dr("Ship_To_Location_Code"))
                    objtr1.loc_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + objtr1.loc_code + "'"))

                    obj.Arr.Add(objtr1)
                Next
            End If

            Return obj.Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsNotifiedPartyMasterDetail
#Region "variables"
    Public ship_to_code As String = Nothing
    Public loc_code As String = Nothing
    Public loc_name As String = Nothing
    Public custcode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsNotifiedPartyMasterDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            Dim counter As Integer = 0

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsNotifiedPartyMasterDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "Ship_To_Location_Code", objtr.ship_to_code)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", objtr.custcode)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.loc_code)

                    If counter = 0 Then
                        Dim qry As String = "delete from TSPL_NOTIFY_PARTY_DETAIL where doc_no='" + strCode + "' and Cust_Code='" + objtr.custcode + "' and Ship_To_Location_Code='" + objtr.ship_to_code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFY_PARTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    counter += 1
                Next
            End If

            'counter = 0
            'If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '    For Each objtr As clsNotifiedPartyMasterDetail In arr
            '        coll = New Hashtable()

            '        clsCommon.AddColumnsForChange(coll, "cust_code", objtr.custcode)
            '        clsCommon.AddColumnsForChange(coll, "Ship_To_Code", objtr.ship_to_code)
            '        clsCommon.AddColumnsForChange(coll, "trans_type", "EXPORT SALE")
            '        clsCommon.AddColumnsForChange(coll, "Loc_Code", objtr.loc_code)

            '        If counter = 0 Then
            '            Dim qry As String = "delete from TSPL_SHIP_TO_LOCATION_LOCATIONS where cust_code='" + objtr.custcode + "' and Ship_To_Code='" + objtr.ship_to_code + "' and trans_type='EXPORT SALE'"
            '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '        End If

            '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIP_TO_LOCATION_LOCATIONS", OMInsertOrUpdate.Insert, "", trans)
            '        counter += 1
            '    Next
            'End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class clscomplaintdetail
#Region "Variables"
    Public primarycode As String = Nothing
    Public primarydesc As String = Nothing
    Public secdrycode As String = Nothing
    Public pendcode As String = Nothing
    Public DocDate As Date
    Public post As String = Nothing
    Public comp_id As String = Nothing
    Public comp_date As String = Nothing
    Public description As String = Nothing
    Public outltcode As String = Nothing
    Public outltdesc As String = Nothing
    Public outlttype As String = Nothing
    Public city As String = Nothing
    Public state As String = Nothing
    Public country As String = Nothing
    Public location As String = Nothing
    Public phnno As String = Nothing
    Public itemcode As String = Nothing
    Public itemdesc As String = Nothing
    Public itemmake As String = Nothing
    Public modelno As String = Nothing
    Public size As String = Nothing
    Public serialno As String = Nothing
    Public tagno As String = Nothing
    Public apexno As String = Nothing
    Public compltypecode As String = Nothing
    Public compltypedesc As String = Nothing
    Public complgivnby As String = Nothing
    Public complgivnto As String = Nothing
    Public tdmcode As String = Nothing
    Public tdmdesc As String = Nothing
    Public remarks As String = Nothing
    Public compl_status As String = Nothing
    Public status_date As String = Nothing
    Public responsetym As String = Nothing
    Public executivecode As String = Nothing
    Public executivedesc As String = Nothing
    Public billno As String = Nothing
    Public billamt As String = Nothing
    Public addcharge As String = Nothing
    Public sparepart As String = Nothing
    Public secresn As String = Nothing
    Public pendresn As String = Nothing
    Public workno As String = Nothing
    Public franchiseyn As String = Nothing
    Public spare_Qty As Integer = 0
    ''additional for manual
    Public OutletNameManual As String = Nothing
    Public CityManual As String = Nothing
    Public StateManual As String = Nothing
    Public CountryManual As String = Nothing
    Public LocationManual As String = Nothing
    Public Asset_Type_NameManual As String = Nothing
    Public Primary_Reason_DescManual As String = Nothing
    Public Secondary_Reason_DescManual As String = Nothing
    Public Franchise_DescManual As String = Nothing
    Public Service_Executive_DescManual As String = Nothing
    Public Pending_Reason_DescManual As String = Nothing
    Public OutletTypeManual As String = Nothing
    Public ComplaintTypeManual As String = Nothing
    Public IsManual As Integer = 0
    Public ObjList As List(Of clscomplaintItemdetail)

#End Region

    Public Shared Function SaveData(ByVal obj As clscomplaintdetail, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True


        Try
            If isNewEntry Then
                obj.comp_id = clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.complaintdetail, "", "")

            End If
            Dim qry As String = "delete from TSPL_COMPLAINT_ITEMS_DETAIL where Comp_id='" + obj.comp_id + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If (clsCommon.myLen(obj.comp_id) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "comp_date", clsCommon.GetPrintDate(obj.comp_date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "description", obj.description)
            clsCommon.AddColumnsForChange(coll, "cust_code", obj.outltcode)
            clsCommon.AddColumnsForChange(coll, "phone_no", obj.phnno)
            clsCommon.AddColumnsForChange(coll, "item_code", obj.itemcode)
            clsCommon.AddColumnsForChange(coll, "item_make", obj.itemmake)
            clsCommon.AddColumnsForChange(coll, "model_no", obj.modelno)
            clsCommon.AddColumnsForChange(coll, "size", obj.size)
            clsCommon.AddColumnsForChange(coll, "serial_no", obj.serialno)
            clsCommon.AddColumnsForChange(coll, "tag_no", obj.tagno)
            clsCommon.AddColumnsForChange(coll, "apex_no", obj.apexno)
            clsCommon.AddColumnsForChange(coll, "compl_type_code", obj.compltypecode)
            clsCommon.AddColumnsForChange(coll, "compl_given_by", obj.complgivnby)
            clsCommon.AddColumnsForChange(coll, "compl_given_to", obj.complgivnto)
            clsCommon.AddColumnsForChange(coll, "tdm_code", obj.tdmcode)
            clsCommon.AddColumnsForChange(coll, "remarks", obj.remarks)
            clsCommon.AddColumnsForChange(coll, "compl_status", obj.compl_status)
            clsCommon.AddColumnsForChange(coll, "executive_code", obj.executivecode)
            clsCommon.AddColumnsForChange(coll, "bill_no", obj.billno)
            clsCommon.AddColumnsForChange(coll, "bill_amount", obj.billamt)
            clsCommon.AddColumnsForChange(coll, "add_chrge", obj.addcharge)
            clsCommon.AddColumnsForChange(coll, "spare_parts", obj.sparepart)
            clsCommon.AddColumnsForChange(coll, "sec_reason_code", obj.secdrycode)
            clsCommon.AddColumnsForChange(coll, "sec_reason", obj.secresn)
            clsCommon.AddColumnsForChange(coll, "pend_reason_code", obj.pendcode)
            clsCommon.AddColumnsForChange(coll, "pend_reason", obj.pendresn)
            clsCommon.AddColumnsForChange(coll, "primary_reason_code", obj.primarycode)
            clsCommon.AddColumnsForChange(coll, "primary_reason", obj.primarydesc)
            clsCommon.AddColumnsForChange(coll, "spare_Qty", obj.spare_Qty)
            ''Additional for manual
            clsCommon.AddColumnsForChange(coll, "OutletNameManual", obj.OutletNameManual)
            clsCommon.AddColumnsForChange(coll, "CityManual", obj.CityManual)
            clsCommon.AddColumnsForChange(coll, "StateManual", obj.StateManual)
            clsCommon.AddColumnsForChange(coll, "CountryManual", obj.CountryManual)
            clsCommon.AddColumnsForChange(coll, "LocationManual", obj.LocationManual)
            clsCommon.AddColumnsForChange(coll, "Asset_Type_NameManual", obj.Asset_Type_NameManual)
            clsCommon.AddColumnsForChange(coll, "Primary_Reason_DescManual", obj.Primary_Reason_DescManual)
            clsCommon.AddColumnsForChange(coll, "Secondary_Reason_DescManual", obj.Secondary_Reason_DescManual)
            clsCommon.AddColumnsForChange(coll, "Franchise_DescManual", obj.Franchise_DescManual)
            clsCommon.AddColumnsForChange(coll, "Service_Executive_DescManual", obj.Service_Executive_DescManual)
            clsCommon.AddColumnsForChange(coll, "Pending_Reason_DescManual", obj.Pending_Reason_DescManual)
            clsCommon.AddColumnsForChange(coll, "OutletTypeManual", obj.OutletTypeManual)
            clsCommon.AddColumnsForChange(coll, "ComplaintTypeManual", obj.ComplaintTypeManual)
            clsCommon.AddColumnsForChange(coll, "IsManual", obj.IsManual)
            '============
            clsCommon.AddColumnsForChange(coll, "status_date", clsCommon.GetPrintDate(obj.status_date, "dd/MMM/yyyy hh:mm:ss tt"))
            If clsCommon.myLen(obj.responsetym) > 0 Then
                clsCommon.AddColumnsForChange(coll, "response_time", clsCommon.myCstr(obj.responsetym))
            End If
            clsCommon.AddColumnsForChange(coll, "work_order_no", obj.workno)
            clsCommon.AddColumnsForChange(coll, "franchise_yn", obj.franchiseyn)

            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "comp_id", obj.comp_id)
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                Dim qry1 As String
                qry1 = "select count(*) from tspl_complaint_detail where comp_id='" + obj.comp_id + "'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                If check1 = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_complaint_detail", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    'Exit Function
                    Return False
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_complaint_detail", OMInsertOrUpdate.Update, "tspl_complaint_detail.comp_id='" + obj.comp_id + "'", trans)
            End If
            isSaved = isSaved AndAlso clscomplaintItemdetail.SaveData(obj.comp_id, obj.ObjList, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strcompid As String, ByVal Navtype As NavigatorType) As clscomplaintdetail
        Dim obj As clscomplaintdetail = Nothing
        Dim qry As String = "select tspl_customer_master.Customer_Name,(tspl_customer_master.add1+' '+tspl_customer_master.add2+' '+tspl_customer_master.add3) as Address,tspl_complaint_detail.*,tspl_item_master.item_desc,TSPL_COMPLAINT_GROUP_MASTER.description as compldesc from tspl_complaint_detail left outer join tspl_item_master on tspl_item_master.item_code=Tspl_complaint_detail.item_code left outer join TSPL_COMPLAINT_GROUP_MASTER on TSPL_COMPLAINT_GROUP_MASTER.complaint_code=tspl_complaint_detail.compl_type_code left outer join tspl_customer_master on tspl_customer_master.cust_code=tspl_complaint_detail.cust_code where tspl_complaint_detail.comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case Navtype
            Case NavigatorType.Current
                qry += "  tspl_complaint_detail.comp_id='" + strcompid + "'"
            Case NavigatorType.Next
                qry += " tspl_complaint_detail.comp_id in (select min(t.comp_id) from tspl_complaint_detail  as t where t.comp_id  >'" + strcompid + "')"
            Case NavigatorType.First
                qry += " tspl_complaint_detail.comp_id in (select min(t.comp_id ) from tspl_complaint_detail  as t )"
            Case NavigatorType.Last
                qry += " tspl_complaint_detail.comp_id in (select max(t.comp_id ) from tspl_complaint_detail  as t )"
            Case NavigatorType.Previous
                qry += " tspl_complaint_detail.comp_id in (select max(t.comp_id ) from tspl_complaint_detail  as t where t.comp_id  <'" + strcompid + "')"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clscomplaintdetail
            obj.comp_id = clsCommon.myCstr(dt.Rows(0)("comp_id"))
            obj.comp_date = clsCommon.myCDate(dt.Rows(0)("comp_date")).ToString("dd/MM/yyyy h:mm:ss")
            obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
            obj.outltcode = clsCommon.myCstr(dt.Rows(0)("cust_code"))

            obj.outltdesc = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.outlttype = clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_desc from TSPL_CUSTOMER_CATEGORY_MASTER where cust_category_code in (select distinct cust_category_code from tspl_customer_master where cust_code='" + obj.outltcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "')")
            obj.location = clsCommon.myCstr(dt.Rows(0)("Address"))
            obj.city = clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code in (select distinct city_code from tspl_customer_master where cust_code='" + obj.outltcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "')")
            obj.state = clsDBFuncationality.getSingleValue("select distinct state from tspl_customer_master where cust_code='" + obj.outltcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'")
            obj.country = "India"

            obj.phnno = clsCommon.myCstr(dt.Rows(0)("phone_no"))
            obj.itemcode = clsCommon.myCstr(dt.Rows(0)("item_code"))
            obj.itemdesc = clsCommon.myCstr(dt.Rows(0)("item_desc"))
            obj.itemmake = clsCommon.myCstr(dt.Rows(0)("item_make"))
            obj.modelno = clsCommon.myCstr(dt.Rows(0)("model_no"))
            obj.size = clsCommon.myCstr(dt.Rows(0)("size"))
            obj.tagno = clsCommon.myCstr(dt.Rows(0)("tag_no"))
            obj.serialno = clsCommon.myCstr(dt.Rows(0)("serial_no"))
            obj.apexno = clsCommon.myCstr(dt.Rows(0)("apex_no"))
            obj.compltypecode = clsCommon.myCstr(dt.Rows(0)("compl_type_code"))
            obj.compltypedesc = clsCommon.myCstr(dt.Rows(0)("compldesc"))
            obj.complgivnby = clsCommon.myCstr(dt.Rows(0)("compl_given_by"))
            obj.complgivnto = clsCommon.myCstr(dt.Rows(0)("compl_given_to"))
            obj.tdmcode = clsCommon.myCstr(dt.Rows(0)("tdm_code"))

            obj.tdmdesc = clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + obj.tdmcode + "'")

            obj.remarks = clsCommon.myCstr(dt.Rows(0)("remarks"))
            obj.compl_status = clsCommon.myCstr(dt.Rows(0)("compl_status"))
            obj.executivecode = clsCommon.myCstr(dt.Rows(0)("executive_code"))

            obj.executivedesc = clsDBFuncationality.getSingleValue("select emp_name from TSPL_employee_MASTER where emp_code='" + obj.executivecode + "'")

            obj.billno = clsCommon.myCstr(dt.Rows(0)("bill_no"))
            obj.billamt = clsCommon.myCdbl(dt.Rows(0)("bill_amount"))
            obj.addcharge = clsCommon.myCdbl(dt.Rows(0)("add_chrge"))
            obj.sparepart = clsCommon.myCstr(dt.Rows(0)("spare_parts"))
            obj.spare_Qty = clsCommon.myCstr(dt.Rows(0)("spare_Qty"))
            obj.primarycode = clsCommon.myCstr(dt.Rows(0)("primary_reason_code"))
            obj.secdrycode = clsCommon.myCstr(dt.Rows(0)("sec_reason_code"))
            obj.pendcode = clsCommon.myCstr(dt.Rows(0)("pend_reason_code"))
            obj.secresn = clsDBFuncationality.getSingleValue("select description from TSPL_COMPLAINT_MASTER where complaint_code='" + obj.secdrycode + "'")
            obj.pendresn = clsDBFuncationality.getSingleValue("select description from TSPL_PENDING_REASON_MASTER where pending_reason_code='" + obj.pendcode + "'")
            obj.primarydesc = clsDBFuncationality.getSingleValue("select description from TSPL_PRIMARY_REASON_MASTER where reason_code='" + obj.primarycode + "'")

            obj.status_date = clsCommon.myCDate(dt.Rows(0)("status_date")).ToString("dd/MM/yyyy h:mm:ss")
            obj.responsetym = clsCommon.myCstr(dt.Rows(0)("response_time"))
            obj.workno = clsCommon.myCstr(dt.Rows(0)("work_order_no"))
            obj.franchiseyn = clsCommon.myCstr(dt.Rows(0)("franchise_yn"))
            obj.post = clsCommon.myCstr(dt.Rows(0)("post_status"))
            '' for manual
            obj.OutletNameManual = clsCommon.myCstr(dt.Rows(0)("OutletNameManual"))
            obj.CityManual = clsCommon.myCstr(dt.Rows(0)("CityManual"))
            obj.StateManual = clsCommon.myCstr(dt.Rows(0)("StateManual"))
            obj.CountryManual = clsCommon.myCstr(dt.Rows(0)("CountryManual"))
            obj.LocationManual = clsCommon.myCstr(dt.Rows(0)("LocationManual"))
            obj.Asset_Type_NameManual = clsCommon.myCstr(dt.Rows(0)("Asset_Type_NameManual"))
            obj.Primary_Reason_DescManual = clsCommon.myCstr(dt.Rows(0)("Primary_Reason_DescManual"))
            obj.Secondary_Reason_DescManual = clsCommon.myCstr(dt.Rows(0)("Secondary_Reason_DescManual"))
            obj.Franchise_DescManual = clsCommon.myCstr(dt.Rows(0)("Franchise_DescManual"))
            obj.Service_Executive_DescManual = clsCommon.myCstr(dt.Rows(0)("Service_Executive_DescManual"))
            obj.Pending_Reason_DescManual = clsCommon.myCstr(dt.Rows(0)("Pending_Reason_DescManual"))
            obj.OutletTypeManual = clsCommon.myCstr(dt.Rows(0)("OutletTypeManual"))
            obj.ComplaintTypeManual = clsCommon.myCstr(dt.Rows(0)("ComplaintTypeManual"))
            obj.IsManual = clsCommon.myCdbl(dt.Rows(0)("IsManual"))
        End If
        qry = "SELECT Comp_id,TSPL_COMPLAINT_ITEMS_DETAIL.item_code,tspl_item_master.item_desc,Unit_Cost,Qty FROM TSPL_COMPLAINT_ITEMS_DETAIL left join tspl_item_master on tspl_item_master.item_Code=TSPL_COMPLAINT_ITEMS_DETAIL.Item_Code where Comp_id='" & strcompid & "'"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim objtr As New clscomplaintItemdetail()
        Dim ObjList As New List(Of clscomplaintItemdetail)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clscomplaintItemdetail()
                objtr.Comp_id = clsCommon.myCstr(dr("Comp_id"))
                objtr.item_code = clsCommon.myCstr(dr("item_code"))
                objtr.item_desc = clsCommon.myCstr(dr("item_desc"))
                objtr.Qty = clsCommon.myCstr(dr("Qty"))
                objtr.Unit_Cost = clsCommon.myCdbl(dr("Unit_Cost"))
                ObjList.Add(objtr)
            Next
        End If
        If ObjList.Count > 0 Then
            obj.ObjList = ObjList
        End If

        Return obj



    End Function

    Public Shared Function DeleteDate(ByVal strcompid As String) As Boolean
        Try
            Dim qry As String = "delete from tspl_complaint_detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and comp_id='" + strcompid + "'"
            Return clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPendingData(ByVal Status As String) As List(Of clscomplaintdetail)
        Dim ObjList As New List(Of clscomplaintdetail)
        Dim obj As clscomplaintdetail = Nothing
        'Dim qry As String = "select tspl_customer_master.Customer_Name,(tspl_customer_master.add1+' '+tspl_customer_master.add2+' '+tspl_customer_master.add3) as Address" _
        '                    & " ,tspl_complaint_detail.*,tspl_item_master.item_desc,TSPL_COMPLAINT_GROUP_MASTER.description as compldesc from tspl_complaint_detail " _
        '                    & " left outer join tspl_item_master on tspl_item_master.item_code=Tspl_complaint_detail.item_code left outer join TSPL_COMPLAINT_GROUP_MASTER " _
        '                    & " on TSPL_COMPLAINT_GROUP_MASTER.complaint_code=tspl_complaint_detail.compl_type_code left outer join tspl_customer_master on " _
        '                    & " tspl_customer_master.cust_code=tspl_complaint_detail.cust_code where coalesce(post_status,'N')<>'Y' and tspl_complaint_detail.compl_status='" & Status & "' "
        Dim qry As String = " select comp_id ,comp_date ,tspl_complaint_detail.DESCRIPTION,tspl_complaint_detail.cust_code ,case when tspl_complaint_detail.ismanual=1 then OutletNameManual else tspl_customer_master.Customer_Name end as Customer_Name,case when IsManual = 1 then LocationManual else (tspl_customer_master.add1+' '+tspl_customer_master.add2+' '+tspl_customer_master.add3) end as Address,phone_no,tspl_complaint_detail.item_code ,case when IsManual =1 then Asset_Type_NameManual else  tspl_item_master.Item_Desc end as Item_Desc,item_make ,model_no ,size ,tag_no ,serial_no ,apex_no ,compl_type_code ,case when IsManual =1 then ComplaintTypeManual else TSPL_COMPLAINT_GROUP_MASTER.description end as compldesc,compl_given_by,compl_given_to,tspl_complaint_detail.tdm_code,case when ismanual=1 then Franchise_DescManual else vendor_name end as vendor_name,remarks,compl_status,tspl_complaint_detail.executive_code ,case when ismanual=1 then Service_Executive_DescManual else emp_name end as emp_name,bill_no,bill_amount, add_chrge,spare_parts,primary_reason_code , case when ismanual=1 then primary_reason_descManual else TSPL_PRIMARY_REASON_MASTER.description end as primary_reason_descManual,sec_reason_code,case when ismanual=1 then Secondary_Reason_DescManual else TSPL_COMPLAINT_MASTER.description end as Secondary_Reason_DescManual,pend_reason_code,case when IsManual =1 then Pending_Reason_DescManual else TSPL_PENDING_REASON_MASTER.PENDING_REASON_CODE end as Pending_Reason_DescManual,status_date,response_time,work_order_no,tspl_complaint_detail.franchise_yn,post_status,ismanual,OutletTypeManual,citymanual,statemanual  from tspl_complaint_detail left outer join tspl_item_master on tspl_item_master.item_code=Tspl_complaint_detail.item_code left outer join TSPL_COMPLAINT_GROUP_MASTER  on TSPL_COMPLAINT_GROUP_MASTER.complaint_code=tspl_complaint_detail.compl_type_code left outer join tspl_customer_master on  tspl_customer_master.cust_code=tspl_complaint_detail.cust_code " & _
                            " left join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_complaint_detail.tdm_code" & _
                            " left join TSPL_employee_MASTER on TSPL_employee_MASTER.emp_Code=tspl_complaint_detail.executive_code" & _
                            " left join TSPL_PRIMARY_REASON_MASTER on TSPL_PRIMARY_REASON_MASTER.REASON_CODE =tspl_complaint_detail.primary_reason_code" & _
                            " left join TSPL_PENDING_REASON_MASTER on TSPL_PENDING_REASON_MASTER.pending_reason_code=tspl_complaint_detail.pend_reason_code" & _
                            " left join TSPL_COMPLAINT_MASTER on TSPL_COMPLAINT_MASTER.complaint_code=tspl_complaint_detail.sec_reason_code where coalesce(post_status,'N')<>'Y' and tspl_complaint_detail.compl_status='" & Status & "' "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                obj = New clscomplaintdetail
                obj.comp_id = clsCommon.myCstr(row("comp_id"))
                obj.comp_date = clsCommon.myCDate(row("comp_date")).ToString("dd/MM/yyyy h:mm:ss")
                obj.description = clsCommon.myCstr(row("description"))
                obj.outltcode = clsCommon.myCstr(row("cust_code"))
                obj.outltdesc = clsCommon.myCstr(row("Customer_Name"))
                obj.IsManual = clsCommon.myCdbl(row("ismanual"))
                If (obj.IsManual) = 0 Then
                    obj.outlttype = clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_desc from TSPL_CUSTOMER_CATEGORY_MASTER where cust_category_code in (select distinct cust_category_code from tspl_customer_master where cust_code='" + obj.outltcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "')")

                Else
                    obj.outlttype = clsCommon.myCstr(row("OutletTypeManual"))
                End If
                obj.location = clsCommon.myCstr(row("Address"))
                If (obj.IsManual) = 0 Then
                    obj.city = clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code in (select distinct city_code from tspl_customer_master where cust_code='" + obj.outltcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "')")

                Else
                    obj.city = clsCommon.myCstr(row("citymanual"))
                End If
                If (obj.IsManual) = 0 Then
                    obj.state = clsDBFuncationality.getSingleValue("select distinct state from tspl_customer_master where cust_code='" + obj.outltcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'")

                Else
                    obj.state = clsCommon.myCstr(row("statemanual"))
                End If
                obj.country = "India"
                obj.phnno = clsCommon.myCstr(row("phone_no"))
                obj.itemcode = clsCommon.myCstr(row("item_code"))
                obj.itemdesc = clsCommon.myCstr(row("item_desc"))
                obj.itemmake = clsCommon.myCstr(row("item_make"))
                obj.modelno = clsCommon.myCstr(row("model_no"))
                obj.size = clsCommon.myCstr(row("size"))
                obj.tagno = clsCommon.myCstr(row("tag_no"))
                obj.serialno = clsCommon.myCstr(row("serial_no"))
                obj.apexno = clsCommon.myCstr(row("apex_no"))
                obj.compltypecode = clsCommon.myCstr(row("compl_type_code"))
                obj.compltypedesc = clsCommon.myCstr(row("compldesc"))
                obj.complgivnby = clsCommon.myCstr(row("compl_given_by"))
                obj.complgivnto = clsCommon.myCstr(row("compl_given_to"))
                obj.tdmcode = clsCommon.myCstr(row("tdm_code"))
                obj.tdmdesc = clsCommon.myCstr(row("vendor_name"))
                obj.remarks = clsCommon.myCstr(row("remarks"))
                obj.compl_status = clsCommon.myCstr(row("compl_status"))
                obj.executivecode = clsCommon.myCstr(row("executive_code"))
                obj.executivedesc = clsCommon.myCstr(row("emp_name"))
                obj.billno = clsCommon.myCstr(row("bill_no"))
                obj.billamt = clsCommon.myCdbl(row("bill_amount"))
                obj.addcharge = clsCommon.myCdbl(row("add_chrge"))
                obj.sparepart = clsCommon.myCstr(row("spare_parts"))
                obj.primarycode = clsCommon.myCstr(row("primary_reason_code"))
                obj.secdrycode = clsCommon.myCstr(row("sec_reason_code"))
                obj.pendcode = clsCommon.myCstr(row("pend_reason_code"))
                obj.secresn = clsCommon.myCstr(row("Secondary_Reason_DescManual"))
                obj.pendresn = clsCommon.myCstr(row("Pending_Reason_DescManual"))
                obj.primarydesc = clsCommon.myCstr(row("primary_reason_descManual"))
                obj.status_date = clsCommon.myCDate(row("status_date")).ToString("dd/MM/yyyy h:mm:ss")
                obj.responsetym = clsCommon.myCstr(row("response_time"))
                obj.workno = clsCommon.myCstr(row("work_order_no"))
                obj.franchiseyn = clsCommon.myCstr(row("franchise_yn"))
                obj.post = clsCommon.myCstr(row("post_status"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
End Class
'' shivani======>against ticket[BM00000007997]
Public Class clscomplaintItemdetail
#Region "Variables"
    Public Comp_id As String
    Public item_code As String
    Public item_desc As String
    Public Qty As Integer
    Public Unit_Cost As Decimal

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal ObjList As List(Of clscomplaintItemdetail), ByVal trans As SqlTransaction) As Boolean
        If (ObjList IsNot Nothing AndAlso ObjList.Count > 0) Then
            For Each obj As clscomplaintItemdetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Comp_id", strDocNo)
                clsCommon.AddColumnsForChange(coll, "item_code", obj.item_code)
                clsCommon.AddColumnsForChange(coll, "item_desc", obj.item_desc)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Cost", obj.Unit_Cost)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COMPLAINT_ITEMS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class


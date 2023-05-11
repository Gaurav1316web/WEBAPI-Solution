'=========BM00000003488==========created by Monika
Imports common
Imports System.Data.SqlClient

Public Class clsCSAPriceMaster

#Region "variables"
    Public docno As String = Nothing
    Public Doc_Date As DateTime? = Nothing
    Public csatype As String = Nothing
    Public csauom As String = Nothing
    Public rate As Decimal = Nothing
    Public taxtype As String = Nothing
    Public Description As String = Nothing
    Public Posted As Decimal = Nothing

    Public ForOtherItem As Boolean = Nothing
    Public Expiry_Date As DateTime? = Nothing
    Public Revision_No As String = Nothing

    Public Arr As List(Of clsCSAPriceDetail) = Nothing
    Public Arr_Loc As List(Of clsCSALocationDetail) = Nothing
    Public Arr_State As List(Of clsCSAPriceStateDetail) = Nothing
    Public Arr_CSA As List(Of clsCSAPriceCustomerDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim qry As String = "select doc_no as [Code], (case when Posted = 0 then 'Pending'  when Posted = 1 then 'Posted' end ) As Status ,[Description],csa_type as [CSA Type],csa_uom as [CSA UOM],csa_rate as Rate,TAX,created_by as [Created By],created_Date as [Created Date],modified_by as [Modified By],modified_date as [Modified Date] from TSPL_CSA_PRICE_HEAD"
            Dim str As String = ""
            str = clsCommon.myCstr(clsCommon.ShowSelectForm("CSAFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked))

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetLastDescription() As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_CSA_PRICE_HEAD where isnull(description,'')<>'' order by convert(date,Created_Date,103) desc"))

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSAPriceMaster, ByVal isNewentry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewentry, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSAPriceMaster, ByVal isNewentry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If isNewentry Then
                obj.docno = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CSAPRICEMAASTER, "", ""))
            Else
                Dim qry As String = "select Revision_No from TSPL_CSA_PRICE_head where doc_no='" + obj.docno + "'"
                obj.Revision_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                If clsCommon.myLen(obj.Revision_No) > 0 Then
                    obj.Revision_No = clsCommon.incval(obj.Revision_No)
                ElseIf clsCommon.myLen(obj.Revision_No) <= 0 Then
                    If obj.Arr_CSA IsNot Nothing AndAlso obj.Arr_CSA.Count > 0 Then
                        If clsCommon.myLen(obj.Arr_CSA(0).Cust_Code) > 3 Then
                            obj.Revision_No = clsCommon.myCstr(obj.Arr_CSA(0).Cust_Code).Substring(0, 3) + "0000000001"
                        Else
                            obj.Revision_No = clsCommon.myCstr(obj.Arr_CSA(0).Cust_Code) + "0000000001"
                        End If
                    ElseIf obj.Arr_Loc IsNot Nothing AndAlso obj.Arr_Loc.Count > 0 Then
                        obj.Revision_No = ""
                        If clsCommon.myLen(obj.Arr_State(0).State_Code) > 3 Then
                            obj.Revision_No += clsCommon.myCstr(obj.Arr_State(0).State_Code).Substring(0, 3)
                        Else
                            obj.Revision_No += clsCommon.myCstr(obj.Arr_State(0).State_Code)
                        End If
                        If clsCommon.myLen(obj.Arr_Loc(0).loc_code) > 3 Then
                            obj.Revision_No += clsCommon.myCstr(obj.Arr_Loc(0).loc_code).Substring(0, 3)
                        Else
                            obj.Revision_No += clsCommon.myCstr(obj.Arr_Loc(0).loc_code)
                        End If

                        obj.Revision_No += "0000000001"
                    End If ''end cond
                End If
            End If

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "doc_no", obj.docno)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "CSA_Type", obj.csatype)
            clsCommon.AddColumnsForChange(coll, "CSA_UOM", obj.csauom)
            clsCommon.AddColumnsForChange(coll, "CSA_Rate", obj.rate)
            clsCommon.AddColumnsForChange(coll, "TAX", obj.taxtype)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            clsCommon.AddColumnsForChange(coll, "For_Other_Item", obj.ForOtherItem)
            clsCommon.AddColumnsForChange(coll, "Revision_No", obj.Revision_No)

            If obj.Expiry_Date IsNot Nothing AndAlso clsCommon.myLen(obj.Expiry_Date) > 0 AndAlso IsDate(obj.Expiry_Date) Then
                clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"))
            End If

            If obj.Doc_Date IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Date) > 0 AndAlso IsDate(obj.Doc_Date) Then
                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            End If


            If isNewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.docno + "'", trans)
            End If

            isSaved = isSaved AndAlso clsCSAPriceDetail.SaveData(obj.docno, obj.Arr, trans)
            isSaved = isSaved AndAlso clsCSALocationDetail.SaveData(obj.docno, obj.Arr_Loc, trans)
            isSaved = isSaved AndAlso clsCSAPriceStateDetail.SaveData(obj.docno, obj.Arr_State, trans)
            isSaved = isSaved AndAlso clsCSAPriceCustomerDetail.SaveData(obj.docno, obj.Arr_CSA, trans)

            ''============history======================
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_PRICE_HEAD", "doc_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_PRICE_DETAIL", "doc_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_LOCATION_DETAIL", "doc_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_PRICE_STATE_DETAIL", "doc_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_PRICE_OTHER_ITEM_DETAIL", "doc_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_PRICE_CSA_DETAIL", "doc_no", trans)
            ''========================================================

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsCSAPriceMaster
        Try
            Dim obj As New clsCSAPriceMaster()
            obj.Arr = New List(Of clsCSAPriceDetail)
            obj.Arr_Loc = New List(Of clsCSALocationDetail)
            obj.Arr_State = New List(Of clsCSAPriceStateDetail)
            obj.Arr_CSA = New List(Of clsCSAPriceCustomerDetail)

            Dim qry As String = "select * from TSPL_CSA_PRICE_HEAD where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and doc_no in (select min(doc_no) from TSPL_CSA_PRICE_HEAD)"
                Case NavigatorType.Last
                    qry += " and doc_no in (select max(doc_no) from TSPL_CSA_PRICE_HEAD)"
                Case NavigatorType.Next
                    qry += " and doc_no in (select min(doc_no) from TSPL_CSA_PRICE_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and doc_no in (select max(doc_no) from TSPL_CSA_PRICE_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.csatype = clsCommon.myCstr(dt.Rows(0)("csa_type"))
                obj.csauom = clsCommon.myCstr(dt.Rows(0)("csa_uom"))
                obj.rate = clsCommon.myCdbl(dt.Rows(0)("csa_rate"))
                obj.taxtype = clsCommon.myCstr(dt.Rows(0)("tax"))
                obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))

                obj.ForOtherItem = clsCommon.myCBool(dt.Rows(0)("for_other_item"))
                obj.Revision_No = clsCommon.myCstr(dt.Rows(0)("Revision_No"))

                If dt.Rows(0)("Expiry_Date") IsNot Nothing AndAlso clsCommon.myLen(dt.Rows(0)("Expiry_Date")) > 0 AndAlso IsDate(dt.Rows(0)("Expiry_Date")) Then
                    obj.Expiry_Date = clsCommon.myCDate(dt.Rows(0)("Expiry_Date"))
                Else
                    obj.Expiry_Date = Nothing
                End If

                If dt.Rows(0)("Doc_Date") IsNot Nothing AndAlso clsCommon.myLen(dt.Rows(0)("Doc_Date")) > 0 AndAlso IsDate(dt.Rows(0)("Doc_Date")) Then
                    obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                Else
                    obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("created_date"))
                End If


                qry = "select TSPL_CSA_PRICE_DETAIL.*,tspl_item_master.item_desc,tspl_item_master.Is_MRP from TSPL_CSA_PRICE_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_PRICE_DETAIL.item_code where TSPL_CSA_PRICE_DETAIL.doc_no='" + obj.docno + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsCSAPriceDetail()

                        objtr.lineno = clsCommon.myCdbl(dr("line_no"))
                        objtr.itemcode = clsCommon.myCstr(dr("Item_Code"))
                        objtr.itemname = clsCommon.myCstr(dr("item_desc"))
                        objtr.uom = clsCommon.myCstr(dr("UOM"))
                        objtr.diffrate = clsCommon.myCdbl(dr("Diff_Rate"))
                        objtr.org_rate = clsCommon.myCdbl(dr("Ltr_Rate"))
                        objtr.case_rate = clsCommon.myCdbl(dr("Case_Rate"))
                        objtr.pcs_rate = clsCommon.myCdbl(dr("Pcs_Rate"))
                        objtr.ltr_per_case = clsCommon.myCdbl(dr("Ltr_Per_Case"))
                        objtr.pcs_per_case = clsCommon.myCdbl(dr("Pcs_Per_Case"))
                        objtr.Case_UOM = clsCommon.myCstr(dr("CASE_UOM"))
                        objtr.MRP = clsCommon.myCdbl(dr("MRP"))
                        objtr.IS_MRP = clsCommon.myCdbl(dr("Is_MRP"))

                        objtr.Arr_OtherItem = New List(Of clsCSAPriceDetail)
                        If obj.ForOtherItem Then
                            objtr.Arr_OtherItem = clsCSAPriceDetail.GetOtherItemData(obj.docno, objtr.itemcode, trans)
                        End If

                        obj.Arr.Add(objtr)
                    Next
                End If

                qry = "select * from TSPL_CSA_LOCATION_DETAIL where doc_no='" + obj.docno + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsCSALocationDetail()

                        objtr.loc_code = clsCommon.myCstr(dr("Location_Code"))

                        obj.Arr_Loc.Add(objtr)
                    Next
                End If

                qry = "select * from TSPL_CSA_PRICE_STATE_DETAIL where doc_no='" + obj.docno + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsCSAPriceStateDetail()

                        objtr.State_Code = clsCommon.myCstr(dr("State_Code"))

                        obj.Arr_State.Add(objtr)
                    Next
                End If

                obj.Arr_CSA = clsCSAPriceCustomerDetail.Getdata(obj.docno, trans)
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("CSA Price No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update TSPL_CSA_PRICE_HEAD set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetCSAState(ByVal CSA_Code As String) As String
        Dim state As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_customer_master where cust_code='" + CSA_Code + "'"))

        Return state
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CSA_PRICE_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_LOCATION_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_PRICE_STATE_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_PRICE_CSA_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_PRICE_HEAD where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    '' changed by : prabhakar 

    Public Function PrintData(ByVal code As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim query = "select TSPL_CSA_PRICE_DETAIL.Doc_No,TSPL_CSA_PRICE_DETAIL.Item_Code,TSPL_CSA_PRICE_DETAIL.UOM,TSPL_CSA_PRICE_DETAIL.Diff_Rate,TSPL_CSA_PRICE_DETAIL.Ltr_Rate,TSPL_CSA_PRICE_DETAIL.Case_Rate,TSPL_CSA_PRICE_DETAIL.Pcs_Rate,TSPL_CSA_PRICE_DETAIL.Ltr_Per_Case,TSPL_CSA_PRICE_DETAIL.Pcs_Per_Case,TSPL_CSA_PRICE_DETAIL.CASE_UOM ,tspl_item_master.item_desc ,TSPL_CSA_PRICE_HEAD.CSA_Type,TSPL_CSA_PRICE_HEAD.CSA_UOM,TSPL_CSA_PRICE_HEAD.CSA_Rate,TSPL_CSA_PRICE_HEAD.TAX,TSPL_CSA_PRICE_HEAD.Created_By,TSPL_CSA_PRICE_HEAD.Created_Date,TSPL_CSA_PRICE_HEAD.Modified_By,TSPL_CSA_PRICE_HEAD.Modified_Date,TSPL_CSA_PRICE_HEAD.Description,TSPL_COMPANY_MASTER.Comp_Name,Logo_Img,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end as Comp_Address from TSPL_CSA_PRICE_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_PRICE_DETAIL.item_code left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.Doc_No =TSPL_CSA_PRICE_DETAIL.Doc_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_CSA_PRICE_HEAD.Comp_Code where TSPL_CSA_PRICE_DETAIL.doc_no='" + code + "'"
            dt = clsDBFuncationality.GetDataTable(query)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

End Class

Public Class clsCSAPriceDetail
#Region "variables"
    Public lineno As Decimal = Nothing
    Public itemcode As String = Nothing
    Public itemname As String = Nothing
    Public uom As String = Nothing
    Public Case_UOM As String = Nothing
    Public diffrate As Decimal = Nothing
    Public ltr_per_case As Decimal = Nothing
    Public pcs_per_case As Decimal = Nothing
    Public org_rate As Decimal = Nothing
    Public pcs_rate As Decimal = Nothing
    Public case_rate As Decimal = Nothing
    Public MRP As Decimal = Nothing
    Public IS_MRP As Decimal = Nothing
    Public ForOtherItem As Boolean = Nothing
    Public Arr_OtherItem As New List(Of clsCSAPriceDetail)
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCSAPriceDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_CSA_PRICE_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each objtr As clsCSAPriceDetail In Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "doc_no", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_no", objtr.lineno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.itemcode)
                    clsCommon.AddColumnsForChange(coll, "UOM", objtr.uom)
                    clsCommon.AddColumnsForChange(coll, "Diff_Rate", objtr.diffrate)
                    clsCommon.AddColumnsForChange(coll, "Ltr_Rate", objtr.org_rate)
                    clsCommon.AddColumnsForChange(coll, "Case_Rate", objtr.case_rate)
                    clsCommon.AddColumnsForChange(coll, "Pcs_Rate", objtr.pcs_rate)
                    clsCommon.AddColumnsForChange(coll, "Ltr_Per_Case", objtr.ltr_per_case)
                    clsCommon.AddColumnsForChange(coll, "Pcs_Per_Case", objtr.pcs_per_case)
                    clsCommon.AddColumnsForChange(coll, "CASE_UOM", objtr.Case_UOM, True)
                    clsCommon.AddColumnsForChange(coll, "MRP", objtr.MRP)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    ''==========if for other items then save in 1 more table===================
                    If objtr.ForOtherItem Then
                        If objtr.Arr_OtherItem IsNot Nothing AndAlso objtr.Arr_OtherItem.Count > 0 Then
                            For Each objO As clsCSAPriceDetail In objtr.Arr_OtherItem
                                coll = New Hashtable()

                                clsCommon.AddColumnsForChange(coll, "doc_no", strCode)
                                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.itemcode)
                                clsCommon.AddColumnsForChange(coll, "UOM", objO.uom)
                                clsCommon.AddColumnsForChange(coll, "Unit_Rate", objO.diffrate)
                                clsCommon.AddColumnsForChange(coll, "MRP", objO.MRP)

                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_OTHER_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                            Next
                        End If
                    End If
                    ''===============================================================================
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetOtherItemData(ByVal strDocNo As String, ByVal ItemCode As String, ByVal trans As SqlTransaction) As List(Of clsCSAPriceDetail)
        Dim Arr As New List(Of clsCSAPriceDetail)
        Dim dt As New DataTable()
        Dim obj As New clsCSAPriceDetail()
        Try
            Dim qry As String = "select * from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where doc_no='" + strDocNo + "' and item_code='" + ItemCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsCSAPriceDetail()

                    obj.itemcode = clsCommon.myCstr(dr("item_code"))
                    obj.uom = clsCommon.myCstr(dr("uom"))
                    obj.diffrate = clsCommon.myCdbl(dr("unit_rate"))
                    obj.MRP = clsCommon.myCdbl(dr("MRP"))

                    Arr.Add(obj)
                Next
            End If

            Return Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            obj = Nothing
        End Try
    End Function
End Class

Public Class clsCSALocationDetail
#Region "Variables"
    Public docno As String = Nothing
    Public loc_code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal docno As String, ByVal arr As List(Of clsCSALocationDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_CSA_LOCATION_DETAIL where doc_no='" + docno + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsCSALocationDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", docno)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.loc_code)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_LOCATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCSAPriceStateDetail
#Region "Variables"
    Public DocNo As String = Nothing
    Public State_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal docno As String, ByVal arr As List(Of clsCSAPriceStateDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_CSA_PRICE_STATE_DETAIL where doc_no='" + docno + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsCSAPriceStateDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", docno)
                    clsCommon.AddColumnsForChange(coll, "State_Code", objtr.State_Code, True)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_STATE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCSAPriceCustomerDetail
#Region "variable"
    Public Doc_No As String = Nothing
    Public Cust_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCSAPriceCustomerDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_CSA_PRICE_CSA_DETAIL where doc_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsCSAPriceCustomerDetail In Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_CSA_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function Getdata(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsCSAPriceCustomerDetail)
        Dim Arr As New List(Of clsCSAPriceCustomerDetail)
        Dim dt As New DataTable()
        Dim obj As New clsCSAPriceCustomerDetail()
        Try
            Dim qry As String = "select * from TSPL_CSA_PRICE_CSA_DETAIL where doc_no='" + strDocNo + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsCSAPriceCustomerDetail()

                    obj.Doc_No = clsCommon.myCstr(dr("doc_no"))
                    obj.Cust_Code = clsCommon.myCstr(dr("cust_code"))

                    Arr.Add(obj)
                Next
            End If

            Return Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            dt = Nothing
        End Try
    End Function
End Class
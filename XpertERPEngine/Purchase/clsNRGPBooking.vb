'KUNAL >TICKET TASK : DEVELOPED  NEW CLASS > DATE : 15-NOV-2016
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class cls_TSPL_NRGP_REQUEST_HEAD
#Region "Variables"
    Public BOOKING_NO As String
    Public DESCRIPTION As String
    Public BOOKING_DATE As Date
    Public CSA_CODE As String = Nothing
    Public VEN_CODE As String = Nothing
    Public CSA_NAME As String
    Public Booking_Type As String = Nothing
    Public Trans_Type As String = Nothing
    Public Request_Mode As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public CREATED_BY As String
    Public Modified_By As String
    Public POSTED As Boolean
    Public Posting_Date As Date? = Nothing
    Public Shared ObjList As List(Of cls_TSPL_NRGP_REQUEST_DETAIL)
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal strTransType As String) As cls_TSPL_NRGP_REQUEST_HEAD
        Return GetData(strCode, arrloc, NavType, Nothing, strTransType)
    End Function

    Public Shared Function GetBookingDescrptn(ByVal strTransType As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 description from TSPL_NRGP_REQUEST_HEAD where trans_type='" + strTransType + "' and isnull(description,'')<>'' order by booking_date desc"))

        Return str
    End Function

    Public Shared Function GetTaxPaid() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Yes"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "No"
        dr("Name") = "No"
        dt.Rows.Add(dr)
        dt.AcceptChanges()
        Return dt
    End Function
    Public Shared Function GetTaxCalcType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Backward"
        dr("Name") = "Backward"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Forward"
        dr("Name") = "Forward"
        dt.Rows.Add(dr)
        dt.AcceptChanges()
        Return dt
    End Function
    Public Shared Function LoadItemCSAType() As DataTable
        Dim qry As String = "select distinct code,Name  from( select 'None' as Code,'None' as Name union all select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name union all select 'BULK -DESI GHEE' as Code,'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name union all select distinct CSA_TYPE as Code,CSA_TYPE as Name  from TSPL_ITEM_MASTER ) xx"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))
        'Dim dr As DataRow = Nothing


        'dr = dt.NewRow()
        'dr("Code") = "CPD-DESI GHEE"
        'dr("Name") = "CPD-DESI GHEE"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "BULK-DESI GHEE"
        'dr("Name") = "BULK-DESI GHEE"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "CPD-OTHER"
        'dr("Name") = "CPD-OTHER"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "BULK-OTHER"
        'dr("Name") = "BULK-OTHER"
        'dt.Rows.Add(dr)
        'dt.AcceptChanges()
        Return dt
    End Function

    Public Shared Function GetCSAType(ByVal ItemCode As String) As String
        Dim csatype As String = clsDBFuncationality.getSingleValue("select isnull(csa_type,'None') as csa_type from tspl_item_master where item_code='" + ItemCode + "'")

        Return csatype
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal Trans_Type As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            DeleteData(strCode, Trans_Type, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal Trans_Type As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_NRGP_REQUEST_DETAIL where BOOKING_NO ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_NRGP_REQUEST_HEAD where BOOKING_NO ='" + strCode + "' and trans_type='" + Trans_Type + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strTransType As String) As cls_TSPL_NRGP_REQUEST_HEAD
        Dim obj As New cls_TSPL_NRGP_REQUEST_HEAD()
        Dim objtr As New cls_TSPL_NRGP_REQUEST_DETAIL()

        ObjList = New List(Of cls_TSPL_NRGP_REQUEST_DETAIL)

        Dim qry As String = " select TSPL_NRGP_REQUEST_HEAD.VEN_CODE, TSPL_NRGP_REQUEST_HEAD.Booking_Type,TSPL_NRGP_REQUEST_HEAD.BOOKING_NO,TSPL_NRGP_REQUEST_HEAD.DESCRIPTION,TSPL_NRGP_REQUEST_HEAD.BOOKING_DATE, " & _
        " TSPL_NRGP_REQUEST_HEAD.CSA_CODE,TSPL_CUSTOMER_MASTER.Customer_Name as CSA_NAME,TSPL_NRGP_REQUEST_HEAD.Posted,TSPL_NRGP_REQUEST_HEAD.Request_Mode,TSPL_NRGP_REQUEST_HEAD.Location_Code," & _
        " TSPL_NRGP_REQUEST_HEAD.POSTING_DATE,TSPL_NRGP_REQUEST_HEAD.Created_By,TSPL_NRGP_REQUEST_HEAD.Modified_By " & _
        " from TSPL_NRGP_REQUEST_HEAD left join TSPL_CUSTOMER_MASTER on TSPL_NRGP_REQUEST_HEAD.CSA_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_NRGP_REQUEST_HEAD.COMP_CODE='" & objCommonVar.CurrentCompanyCode & "'"

        Dim whrcond As String = ""

        qry += " and TSPL_NRGP_REQUEST_HEAD.Trans_Type='" + strTransType + "' "
        If clsCommon.CompairString(clsCommon.myCstr(strTransType), "Request") = CompairStringResult.Equal AndAlso clsCommon.myLen(arrloc) > 0 Then
            qry += " and TSPL_NRGP_REQUEST_HEAD.Location_Code in (" + arrloc + ") "
            whrcond = " and Location_Code in (" + arrloc + ") "
        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " AND BOOKING_NO = (select MIN(BOOKING_NO) from TSPL_NRGP_REQUEST_HEAD where trans_type='" + strTransType + "' " + whrcond + ") "
            Case NavigatorType.Last
                qry += " AND BOOKING_NO = (select Max(BOOKING_NO) from TSPL_NRGP_REQUEST_HEAD where trans_type='" + strTransType + "' " + whrcond + ")"
            Case NavigatorType.Next
                qry += " AND BOOKING_NO = (select Min(BOOKING_NO) from TSPL_NRGP_REQUEST_HEAD where BOOKING_NO>'" + strCode + "' and trans_type='" + strTransType + "' " + whrcond + ") "
            Case NavigatorType.Previous
                qry += " AND BOOKING_NO = (select Max(BOOKING_NO) from TSPL_NRGP_REQUEST_HEAD where BOOKING_NO<'" + strCode + "' and trans_type='" + strTransType + "' " + whrcond + ") "
            Case NavigatorType.Current
                qry += " AND BOOKING_NO = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.BOOKING_NO = dt.Rows(0)("BOOKING_NO")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.BOOKING_DATE = clsCommon.GetPrintDate(dt.Rows(0)("BOOKING_DATE"), "dd/MMM/yyyy")
            obj.CSA_CODE = clsCommon.myCstr(dt.Rows(0)("CSA_CODE"))
            obj.CSA_NAME = clsCommon.myCstr(dt.Rows(0)("CSA_NAME"))
            obj.VEN_CODE = clsCommon.myCstr(dt.Rows(0)("VEN_CODE"))

            obj.Booking_Type = clsCommon.myCstr(dt.Rows(0)("Booking_Type"))
            obj.Request_Mode = clsCommon.myCstr(dt.Rows(0)("Request_Mode"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))


            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
            If IsDBNull(dt.Rows(0)("Posting_Date")) = False Then
                obj.Posting_Date = clsCommon.GetPrintDate(dt.Rows(0)("Posting_Date"), "dd/MMM/yyyy")
            Else
                obj.Posting_Date = Nothing
            End If
            strCode = dt.Rows(0)("BOOKING_NO")

        End If

        cls_TSPL_NRGP_REQUEST_HEAD.ObjList = cls_TSPL_NRGP_REQUEST_DETAIL.GetCSADetailDetail(strCode, trans)
        Return obj
    End Function

    Public Function SaveData(ByVal obj As cls_TSPL_NRGP_REQUEST_HEAD, ByVal objList As List(Of cls_TSPL_NRGP_REQUEST_DETAIL), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, isNewEntry, trans, strCode)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SaveData(ByVal obj As cls_TSPL_NRGP_REQUEST_HEAD, ByVal objList As List(Of cls_TSPL_NRGP_REQUEST_DETAIL), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Try
            If isNewEntry Then
                If strCode = "" Then
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Booking") = CompairStringResult.Equal Then
                        obj.BOOKING_NO = clsERPFuncationality.GetNextCode(trans, obj.BOOKING_DATE, clsDocType.NRGPREQUEST, "", "")
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Request") = CompairStringResult.Equal Then
                        obj.BOOKING_NO = clsERPFuncationality.GetNextCode(trans, obj.BOOKING_DATE, clsDocType.NRGPREQUEST, "", obj.Location_Code)
                    End If
                Else
                    obj.BOOKING_NO = strCode
                End If
            End If


            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.BOOKING_NO) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "BOOKING_NO", obj.BOOKING_NO)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "BOOKING_DATE", clsCommon.GetPrintDate(obj.BOOKING_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "CSA_CODE", obj.CSA_CODE, True)
            clsCommon.AddColumnsForChange(coll, "VEN_CODE", obj.VEN_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Booking_Type", obj.Booking_Type)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
            If clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Request") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Request_Mode", obj.Request_Mode)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            End If
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_NRGP_REQUEST_HEAD where BOOKING_NO = '" & obj.BOOKING_NO & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NRGP_REQUEST_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.BOOKING_NO + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NRGP_REQUEST_HEAD", OMInsertOrUpdate.Update, "TSPL_NRGP_REQUEST_HEAD.BOOKING_NO='" + obj.BOOKING_NO + "'", trans)
            End If

            isSaved = isSaved AndAlso cls_TSPL_NRGP_REQUEST_DETAIL.SaveDetailData(obj.BOOKING_NO, obj, objList, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal strTransType As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            PostData(strDocNo, arrloc, isCheckForPosted, trans, strTransType)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal strTranstype As String) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As cls_TSPL_NRGP_REQUEST_HEAD = cls_TSPL_NRGP_REQUEST_HEAD.GetData(strDocNo, arrloc, NavigatorType.Current, trans, strTranstype)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.BOOKING_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim isSaved As Boolean = True
            Dim qry As String = "Update TSPL_NRGP_REQUEST_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where BOOKING_NO ='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_NRGP_REQUEST_HEAD where BOOKING_NO='" & Doc_No & "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select TSPL_NRGP_REQUEST_HEAD.BOOKING_NO as Code,TSPL_NRGP_REQUEST_HEAD.DESCRIPTION,TSPL_NRGP_REQUEST_HEAD.BOOKING_DATE, " & _
        " TSPL_NRGP_REQUEST_HEAD.CSA_CODE,TSPL_CUSTOMER_MASTER.Customer_Name as CSA_NAME,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_NRGP_REQUEST_HEAD.Posted," & _
        " TSPL_NRGP_REQUEST_HEAD.POSTING_DATE,TSPL_NRGP_REQUEST_HEAD.Created_By,TSPL_NRGP_REQUEST_HEAD.Modified_By " & _
        " from TSPL_NRGP_REQUEST_HEAD left join TSPL_CUSTOMER_MASTER on TSPL_NRGP_REQUEST_HEAD.CSA_CODE=TSPL_CUSTOMER_MASTER.Cust_Code "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_NRGP_REQUEST_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_NRGP_REQUEST_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If

        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function

End Class


Public Class cls_TSPL_NRGP_REQUEST_DETAIL
#Region "Variables"
    '' grid columns details
    Public Itemcode As String = Nothing
    Public Itemname As String = Nothing
    Public BOOKING_NO As String
    Public Line_No As Integer
    Public CSA_ITEM_TYPE As String
    Public BOOK_QTY_UOM As String
    Public TAX_PAID As String
    Public BOOK_QTY As Decimal
    Public BOOK_RATE_UOM As String
    Public BOOK_Rate As Decimal
    Public TOTAL_QTY As Decimal
    Public Bal_Qty As Decimal = Nothing
#End Region

    Public Shared Function SaveDetailData(ByVal strDocNo As String, ByVal objRec As cls_TSPL_NRGP_REQUEST_HEAD, ByVal Arr As List(Of cls_TSPL_NRGP_REQUEST_DETAIL), ByVal trans As SqlTransaction) As Boolean

        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim qry As String = "DELETE FROM TSPL_NRGP_REQUEST_DETAIL WHERE BOOKING_NO='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                For Each obj As cls_TSPL_NRGP_REQUEST_DETAIL In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "BOOKING_NO", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "CSA_ITEM_TYPE", obj.CSA_ITEM_TYPE)
                    clsCommon.AddColumnsForChange(coll, "BOOK_QTY", obj.BOOK_QTY)
                    clsCommon.AddColumnsForChange(coll, "BOOK_QTY_UOM", obj.BOOK_QTY_UOM)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Itemcode, True)
                    clsCommon.AddColumnsForChange(coll, "BOOK_Rate", obj.BOOK_Rate)
                    clsCommon.AddColumnsForChange(coll, "BOOK_RATE_UOM", obj.BOOK_RATE_UOM)

                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "TAX_PAID", obj.TAX_PAID)
                    clsCommon.AddColumnsForChange(coll, "TOTAL_QTY", obj.TOTAL_QTY)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NRGP_REQUEST_DETAIL", OMInsertOrUpdate.Insert, "TSPL_NRGP_REQUEST_DETAIL.BOOKING_NO='" + strDocNo + "' ", trans)

                Next


            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try

        Return True
    End Function
    Public Shared Function GetCSADetailDetail(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of cls_TSPL_NRGP_REQUEST_DETAIL)
        Dim qry As String
        qry = "select TSPL_NRGP_REQUEST_DETAIL.Line_No,TSPL_NRGP_REQUEST_DETAIL.BOOKING_NO,TSPL_NRGP_REQUEST_DETAIL.CSA_ITEM_TYPE,TSPL_NRGP_REQUEST_DETAIL.BOOK_QTY,TSPL_NRGP_REQUEST_DETAIL.BOOK_QTY_UOM,TSPL_NRGP_REQUEST_DETAIL.TAX_PAID,TSPL_NRGP_REQUEST_DETAIL.BOOK_Rate,TSPL_NRGP_REQUEST_DETAIL.BOOK_RATE_UOM,TSPL_NRGP_REQUEST_DETAIL.TOTAL_QTY,TSPL_NRGP_REQUEST_DETAIL.item_code " & _
              " from TSPL_NRGP_REQUEST_DETAIL WHERE 2=2 " & _
              " AND TSPL_NRGP_REQUEST_DETAIL.BOOKING_NO = '" + strCode + "' ORDER BY Line_No"

        Dim objtr As New cls_TSPL_NRGP_REQUEST_DETAIL
        Dim ObjList As New List(Of cls_TSPL_NRGP_REQUEST_DETAIL)
        Dim dt As New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New cls_TSPL_NRGP_REQUEST_DETAIL()
                objtr.CSA_ITEM_TYPE = clsCommon.myCstr(dr("CSA_ITEM_TYPE"))
                objtr.Itemcode = clsCommon.myCstr(dr("item_code"))
                objtr.Itemname = clsItemMaster.GetItemName(objtr.Itemcode, trans)
                objtr.BOOK_QTY = clsCommon.myCdbl(dr("BOOK_QTY"))
                objtr.BOOK_QTY_UOM = clsCommon.myCstr(dr("BOOK_QTY_UOM"))

                objtr.BOOK_Rate = clsCommon.myCdbl(dr("BOOK_Rate"))
                objtr.BOOK_RATE_UOM = clsCommon.myCstr(dr("BOOK_RATE_UOM"))
                objtr.BOOKING_NO = clsCommon.myCstr(dr("BOOKING_NO"))
                objtr.CSA_ITEM_TYPE = clsCommon.myCstr(dr("CSA_ITEM_TYPE"))
                objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objtr.TAX_PAID = clsCommon.myCstr(dr("TAX_PAID"))
                objtr.TOTAL_QTY = clsCommon.myCdbl(dr("TOTAL_QTY"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function


End Class


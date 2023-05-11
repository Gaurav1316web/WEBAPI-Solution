Imports common
Imports System.Data.SqlClient

Public Class clsPriceGroupMapping
#Region "Variables"
    Public vendorcode As String = Nothing
    Public Price_Code As String
    Public Price_Code_Desc As String
    Public Remarks As String
    Public Price_Comp_Code As String
    Public Price_Comp_Desc As String
    Public Status As String = Nothing
    Public Amount As Double
    Public Discount_Percent As Double
    Public Price_Calculation_Method As String
    Public CurrentDate As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select distinct TSPL_PRICE_GROUP_MAPPING_HEAD.Price_Group_Code as [Code] ,TSPL_PRICE_GROUP_MAPPING_HEAD.remarks as [Remarks] ,TSPL_PRICE_GROUP_MAPPING_HEAD.Price_Group_Desc as [Price Group Description],TSPL_PRICE_GROUP_MAPPING_HEAD.created_by as [Created By] ,TSPL_PRICE_GROUP_MAPPING_HEAD.created_date as [Created Date] ,TSPL_PRICE_GROUP_MAPPING_HEAD.modify_by as [Modify By] ,TSPL_PRICE_GROUP_MAPPING_HEAD.modify_date as [Modify Date]  From TSPL_PRICE_GROUP_MAPPING_HEAD   "
        str = clsCommon.ShowSelectForm("PRCGRPMAP", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function SaveData(ByVal strPriceCode As String, ByVal arr As List(Of clsPriceGroupMapping)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            'Price_Code, Price_Code_Desc, Remarks, Price_Comp_Code, Price_Comp_Desc, Amount, Discount_Percent, Price_Calculation_Method
            Dim Del As String = "delete from TSPL_PRICE_GROUP_MAPPING where price_group_code='" + strPriceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Del, trans)

            Del = "delete from TSPL_PRICE_GROUP_MAPPING_HEAD where price_group_code='" + strPriceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Del, trans)
            'TEC/30/05/19-000511 Save Created and modify date
            If (arr IsNot Nothing AndAlso arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsPriceGroupMapping In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "price_group_code", obj.Price_Code)
                    clsCommon.AddColumnsForChange(coll, "price_group_desc", obj.Price_Code_Desc)
                    clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendorcode)

                    clsCommon.AddColumnsForChange(coll, "remarks", obj.Remarks)


                    If counter = 1 Then

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_GROUP_MAPPING_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    End If

                    clsCommon.AddColumnsForChange(coll, "price_code", obj.Price_Comp_Code)
                    clsCommon.AddColumnsForChange(coll, "price_code_desc", obj.Price_Comp_Desc)
                    clsCommon.AddColumnsForChange(coll, "status", obj.Status)
                    counter += 1

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_GROUP_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            trans.Rollback()
        End Try
        Return isSaved
    End Function

    '----------------------------------Deletes the Price Code Component................
    Public Shared Function DeleteData(ByVal strPriceCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            ' Dim obj As clsPriceGroupMapping
            If clsCommon.myLen(strPriceCode) > 0 Then
                'obj = clsPriceComponentMapping.GetData(strPriceCode, NavigatorType.Current)
                Dim Del As String = "delete from TSPL_PRICE_GROUP_MAPPING_HEAD where price_group_code='" + strPriceCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Del)

                Del = "delete from TSPL_PRICE_GROUP_MAPPING where price_group_code='" + strPriceCode + "'"
                Return clsDBFuncationality.ExecuteNonQuery(Del)
            Else
                Throw New Exception("Document not found to delete.")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As List(Of clsPriceGroupMapping)
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As List(Of clsPriceGroupMapping)
        Dim obj As clsPriceGroupMapping = Nothing
        Dim qry As String = "select * from TSPL_PRICE_GROUP_MAPPING_HEAD where 1=1 "
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_PRICE_GROUP_MAPPING_HEAD.Price_group_code in ('" + strCode + "')"
            Case NavigatorType.Next
                qry += " and TSPL_PRICE_GROUP_MAPPING_HEAD .Price_group_code in (select min(Price_group_code ) from TSPL_PRICE_GROUP_MAPPING_HEAD where Price_group_code  >'" + strCode + "')"
            Case NavigatorType.First
                qry += " and TSPL_PRICE_GROUP_MAPPING_HEAD .Price_group_code in (select MIN(Price_group_code ) from TSPL_PRICE_GROUP_MAPPING_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_PRICE_GROUP_MAPPING_HEAD .Price_group_code in (select Max(Price_group_code ) from TSPL_PRICE_GROUP_MAPPING_HEAD)"
            Case NavigatorType.Previous
                qry += " and TSPL_PRICE_GROUP_MAPPING_HEAD .Price_group_code in (select Max(Price_group_code ) from TSPL_PRICE_GROUP_MAPPING_HEAD where Price_group_code  <'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        strCode = clsCommon.myCstr(dt.Rows(0)("Price_group_code"))

        Dim vendorcode As String = ""
        Try
            vendorcode = clsCommon.myCstr(dt.Rows(0)("vendor_code"))

            If vendorcode IsNot Nothing AndAlso clsCommon.myCstr(vendorcode) > 0 Then
                vendorcode = " and XXX.vendor_code=TSPL_PRICE_COMPONENT_MAPPING.vendor_code"
            End If
        Catch eex As Exception
        End Try

        qry = "Select distinct XXX.price_group_code, XXX.price_group_desc, TSPL_PRICE_COMPONENT_MAPPING.Price_Code, XXX.status, XXX.remarks" & _
                " , TSPL_PRICE_COMPONENT_MAPPING.[Price_Code_Desc] from (" & _
                "select TSPL_PRICE_GROUP_MAPPING.vendor_code,TSPL_PRICE_GROUP_MAPPING.status,TSPL_PRICE_GROUP_MAPPING_HEAD.remarks,TSPL_PRICE_GROUP_MAPPING_HEAD.price_group_code,TSPL_PRICE_GROUP_MAPPING_HEAD.price_group_desc,TSPL_PRICE_GROUP_MAPPING.Price_Code from TSPL_PRICE_GROUP_MAPPING_HEAD right outer join TSPL_PRICE_GROUP_MAPPING on TSPL_PRICE_GROUP_MAPPING.Price_group_Code=TSPL_PRICE_GROUP_MAPPING_HEAD.price_group_code where TSPL_PRICE_GROUP_MAPPING_HEAD.Price_group_code in ('" + strCode + "')"
        qry += " ) XXX RIGHT outer join TSPL_PRICE_COMPONENT_MAPPING on TSPL_PRICE_COMPONENT_MAPPING.price_code=XXX.price_code where 1=1 " + vendorcode + ""
        Dim arr As New List(Of clsPriceGroupMapping)
        Try
            If dt IsNot Nothing Then
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then

                    'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    For Each dr As DataRow In dt1.Rows
                        obj = New clsPriceGroupMapping()
                        obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_group_code"))
                        obj.Price_Code_Desc = clsCommon.myCstr(dt.Rows(0)("price_group_desc"))
                        obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                        obj.vendorcode = clsCommon.myCstr(dt.Rows(0)("vendor_code"))

                        obj.Price_Comp_Code = clsCommon.myCstr(dr("Price_Code"))
                        obj.Price_Comp_Desc = clsCommon.myCstr(dr("Price_code_Desc"))
                        obj.Status = clsCommon.myCstr(dr("status"))
                        arr.Add(obj)
                    Next
                End If
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

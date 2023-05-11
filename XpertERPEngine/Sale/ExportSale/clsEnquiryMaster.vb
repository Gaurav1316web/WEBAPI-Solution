'===================created by monika=====BM00000003440===================
Imports common
Imports System.Data.SqlClient

Public Class clsEnquiryMaster
#Region "Variables"
    Public code As String = Nothing
    Public docdate As Date = Nothing
    Public descrptn As String = Nothing
    Public add1 As String = Nothing
    Public add2 As String = Nothing
    Public add3 As String = Nothing
    Public country_code As String = Nothing
    Public country_name As String = Nothing
    Public state_code As String = Nothing
    Public state_name As String = Nothing
    Public city_code As String = Nothing
    Public city_name As String = Nothing
    Public trans_type As String = Nothing
    Public cust_code As String = Nothing
    Public cust_name As String = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Code,[Date],[Name],(add1+' '+add2+' '+add3) as Address,Country_code as [Country Code],state_code as [State Code],city_code as [City Code],Trans_type as [Transaction Type],customer_code as [Customer Code] from TSPL_ENQUIRY_MASTER"
        str = clsCommon.ShowSelectForm("ENQFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsEnquiryMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.docdate, "dd/MM/yyyy"), clsDocType.Enquiry_Master, "", ""))
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "code", obj.code)
            clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "name", obj.descrptn)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.add3)
            clsCommon.AddColumnsForChange(coll, "Country_Code", obj.country_code)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.state_code)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.city_code)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.trans_type)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.cust_code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENQUIRY_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENQUIRY_MASTER", OMInsertOrUpdate.Update, " code='" + obj.code + "'", trans)
            End If

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEnquiryMaster
        Try
            Dim obj As New clsEnquiryMaster()
            Dim qry As String = "select * from tspl_enquiry_master where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and code in (select min(code) from tspl_enquiry_master)"
                Case NavigatorType.Last
                    qry += " and code in (select max(code) from tspl_enquiry_master)"
                Case NavigatorType.Next
                    qry += " and code in (select min(code) from tspl_enquiry_master where code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and code in (select max(code) from tspl_enquiry_master where code<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.docdate = clsCommon.myCDate(dt.Rows(0)("Date"))
                obj.descrptn = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.country_code = clsCommon.myCstr(dt.Rows(0)("Country_Code"))
                obj.country_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + obj.country_code + "'"))
                obj.state_code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
                obj.state_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + obj.state_code + "' and country_code='" + obj.country_code + "'"))
                obj.city_code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                obj.city_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where state_code='" + obj.state_code + "' and city_code='" + obj.city_code + "'"))
                obj.trans_type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
                obj.cust_code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
                obj.cust_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + obj.cust_code + "'"))
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Document Not Found.")
            End If

            Dim qry As String = "delete from TSPL_ENQUIRY_MASTER where code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

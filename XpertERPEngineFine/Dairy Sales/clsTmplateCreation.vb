Imports common
Imports System.Data.SqlClient
Public Class clsTmplateCreation
#Region "Variables"
    Public TmplateId As String = Nothing
    Public Description As String = Nothing
    Public StartDate As String = Nothing
    Public Route_No As String = Nothing
    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public CustAddress As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strTmplateId As String, ByVal Arr As List(Of clsTmplateCreation)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_TEMPLATE_MASTER where Tmplate_Id='" + strTmplateId + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsTmplateCreation In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Tmplate_Id", strTmplateId)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.StartDate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Cust_Id", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")).ToString())
                clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TEMPLATE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Next
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function DeleteData(ByVal TmplateId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = False
            If (clsCommon.myLen(TmplateId) <= 0) Then
                Throw New Exception("Template Id not found to Delete")
            End If

            Dim qry As String = "delete from TSPL_CUSTOMER_TEMPLATE_MASTER Where Tmplate_Id='" + TmplateId + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal CategoryCode As String, ByVal NavType As common.NavigatorType) As clsTmplateCreation
        Return GetData(CategoryCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal TmplateId As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsTmplateCreation
        Dim obj As clsTmplateCreation = Nothing
        Dim Qry As String = "select TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id, TSPL_CUSTOMER_TEMPLATE_MASTER.Description, TSPL_CUSTOMER_TEMPLATE_MASTER.start_Date, TSPL_CUSTOMER_MASTER.Route_No, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, Case when len(TSPL_CUSTOMER_MASTER.Add1)>0 then TSPL_CUSTOMER_MASTER.Add1 else '' end +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then TSPL_CUSTOMER_MASTER.Add2  else  '' end + case when len(TSPL_CUSTOMER_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_CUSTOMER_MASTER.Add3)>0 then TSPL_CUSTOMER_MASTER.Add3  else  '' end as CustAddress   from TSPL_CUSTOMER_TEMPLATE_MASTER Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id=TSPL_CUSTOMER_MASTER.Cust_Code Where 1=1 "
        Select Case NavType
            Case NavigatorType.First
                Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select MIN(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER)"
            Case NavigatorType.Last
                Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select Max(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER)"
            Case NavigatorType.Next
                Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select Min(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER where Tmplate_Id > '" + TmplateId + "')"
            Case NavigatorType.Previous
                Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id=(select Max(Tmplate_Id) from TSPL_CUSTOMER_TEMPLATE_MASTER where Tmplate_Id < '" + TmplateId + "')"
            Case NavigatorType.Current
                Qry += " and TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id='" + TmplateId + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTmplateCreation()
            obj.TmplateId = clsCommon.myCstr(dt.Rows(0)("Tmplate_Id"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.StartDate = clsCommon.myCstr(dt.Rows(0)("start_Date"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.CustAddress = clsCommon.myCstr(dt.Rows(0)("CustAddress"))
        End If

        Return obj
    End Function

    Public Shared Function AddDataInGrid(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsTmplateCreation
        Dim obj As New clsTmplateCreation
        Try
            Dim Arr As List(Of clsTmplateCreation) = Nothing
            For Each obj In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Tmplate_Id", obj.TmplateId)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.StartDate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Cust_Id", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")).ToString())
                clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            Next


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
       
    End Function

End Class
Public Class clsFilterCustomer
    Public custCode As String = ""
End Class

Public Class clsCustomerToVisi
    Public VisiId As String = Nothing
    Public CustCode As String = Nothing
    Public CustName As String = Nothing
End Class

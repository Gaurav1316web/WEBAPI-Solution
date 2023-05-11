'========================================================
'----Created By--Pankaj Kumar
'----Date-11/02/2013-Monday
'----Table Used--TSPL_SD_USERCUSTOMER_RATE
'========================================================
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.Enumerations

Public Class ClsUserCustomerSettings
#Region "variables"

    Public User_Code As String = Nothing
    Public CustomerCode As String = Nothing
    Public Is_Editable As Char = "0"
    Public Shared RateEditable As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal IsRateEditable As Boolean, ByVal Arr As List(Of ClsUserCustomerSettings)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(trans)
            clsFixedParameter.UpdateData(clsFixedParameterType.SalesRateEditable, clsFixedParameterCode.SalesRateEditable, IIf(IsRateEditable, "1", "0"), trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objSeg As ClsUserCustomerSettings In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "User_Code", objSeg.User_Code)
                    clsCommon.AddColumnsForChange(coll, "Customer_Code", objSeg.CustomerCode)
                    clsCommon.AddColumnsForChange(coll, "Is_Editable", objSeg.Is_Editable)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_USERCUSTOMER_RATE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData()
        Dim Arr As New List(Of ClsUserCustomerSettings)
        Dim qry As String = "Select User_Code, Customer_Code, Is_Editable from TSPL_SD_USERCUSTOMER_RATE WHERE 1=1"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Dim objTr As New ClsUserCustomerSettings()
                objTr.User_Code = clsCommon.myCstr(dr("User_Code"))
                objTr.CustomerCode = clsCommon.myCstr(dr("Customer_Code"))
                objTr.Is_Editable = clsCommon.myCstr(dr("Is_Editable"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function GetUserCustomerRateSetting(ByVal strCustomerCode As String) As ToggleState
        Dim qry As String = "select Is_Editable  from TSPL_SD_USERCUSTOMER_RATE where User_Code='" + objCommonVar.CurrentUserCode + "' and Customer_Code='" + strCustomerCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Return ToggleState.Indeterminate
        ElseIf clsCommon.myCdbl(dt.Rows(0)("Is_Editable")) = 1 Then
            Return ToggleState.On
        Else
            Return ToggleState.Off
        End If
    End Function

    Public Shared Sub DeleteData(ByVal trans As SqlTransaction)
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_SD_USERCUSTOMER_RATE", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class


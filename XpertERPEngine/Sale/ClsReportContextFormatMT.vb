'--------Created By Richa 15/01/2015
Imports System.Data.SqlClient
Imports common
Public Class ClsReportContextFormatMT

#Region "variables"
    
    Public SGS_Waiver_Context As String = Nothing
    Public Merchant_Dec_Context As String = Nothing
    Public Merchant_Dec_Context_Format2 As String = Nothing
    Public LC_Issuing_Application_Context As String = Nothing
    Public Trust_Receipt_Context As String = Nothing
    Public Acceptance_Letter_Context As String = Nothing
#End Region
  
  
    Public Shared Function SaveData(ByVal obj As ClsReportContextFormatMT, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try


            Dim coll As New Hashtable()
         
            clsCommon.AddColumnsForChange(coll, "SGS_Waiver_Context", obj.SGS_Waiver_Context)
          
            clsCommon.AddColumnsForChange(coll, "Merchant_Dec_Context", obj.Merchant_Dec_Context)
            clsCommon.AddColumnsForChange(coll, "Merchant_Dec_Context_Format2", obj.Merchant_Dec_Context_Format2)
            clsCommon.AddColumnsForChange(coll, "LC_Issuing_Application_Context", obj.LC_Issuing_Application_Context)
            clsCommon.AddColumnsForChange(coll, "Trust_Receipt_Context", obj.Trust_Receipt_Context)
            clsCommon.AddColumnsForChange(coll, "Acceptance_Letter_Context", obj.Acceptance_Letter_Context)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REPORT_FORMAT_DECLARATION_MT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REPORT_FORMAT_DECLARATION_MT", OMInsertOrUpdate.Update, "", trans)
            End If

            trans.Commit()

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData() As ClsReportContextFormatMT
        Return GetData(Nothing)
    End Function
    Public Shared Function GetData(ByVal trans As SqlTransaction) As ClsReportContextFormatMT
        Dim obj As ClsReportContextFormatMT = Nothing
        Dim Arr As List(Of ClsReportContextFormatMT) = Nothing
        Dim qry As String = ""
        qry = "Select TSPL_REPORT_FORMAT_DECLARATION_MT.Trust_Receipt_Context,TSPL_REPORT_FORMAT_DECLARATION_MT.Acceptance_Letter_Context,TSPL_REPORT_FORMAT_DECLARATION_MT.SGS_Waiver_Context,TSPL_REPORT_FORMAT_DECLARATION_MT.LC_Issuing_Application_Context,TSPL_REPORT_FORMAT_DECLARATION_MT.Merchant_Dec_Context,TSPL_REPORT_FORMAT_DECLARATION_MT.Merchant_Dec_Context_Format2 from TSPL_REPORT_FORMAT_DECLARATION_MT where 2=2 "
        Dim whrclas As String = ""

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsReportContextFormatMT()
            obj.SGS_Waiver_Context = clsCommon.myCstr(dt.Rows(0)("SGS_Waiver_Context"))
            obj.Merchant_Dec_Context = clsCommon.myCstr(dt.Rows(0)("Merchant_Dec_Context"))
            obj.Merchant_Dec_Context_Format2 = clsCommon.myCstr(dt.Rows(0)("Merchant_Dec_Context_Format2"))
            obj.LC_Issuing_Application_Context = clsCommon.myCstr(dt.Rows(0)("LC_Issuing_Application_Context"))
            obj.Trust_Receipt_Context = clsCommon.myCstr(dt.Rows(0)("Trust_Receipt_Context"))
            obj.Acceptance_Letter_Context = clsCommon.myCstr(dt.Rows(0)("Acceptance_Letter_Context"))

        End If
        Return obj
    End Function
   
End Class

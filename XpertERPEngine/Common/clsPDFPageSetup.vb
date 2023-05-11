Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Collections

Public Class clsPDFPageSetup
    Public PageSetupReport_ID As String = ""
    Public Page_Size As String = ""
    Public Page_Style As String = ""
    Public Width As Double = 0
    Public Height As Double = 0

    Public Sub SaveData(ByVal obj As clsPDFPageSetup)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strQ As String = "delete from TSPL_PDF_PAGE_SIZE where ReportID='" + obj.PageSetupReport_ID + "' and UserID='" + objCommonVar.CurrentUserCode + "'"
            clsDBFuncationality.ExecuteNonQuery(strQ, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ReportID", obj.PageSetupReport_ID)
            clsCommon.AddColumnsForChange(coll, "UserID", objCommonVar.CurrentUserCode, True)
            clsCommon.AddColumnsForChange(coll, "Page_Size", obj.Page_Size, True)
            clsCommon.AddColumnsForChange(coll, "Page_Style", obj.Page_Style, True)
            clsCommon.AddColumnsForChange(coll, "Page_Width", obj.Width, True)
            clsCommon.AddColumnsForChange(coll, "Page_Hight", obj.Height, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PDF_PAGE_SIZE", OMInsertOrUpdate.Insert, "", trans)
           
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
    End Sub

    Public Shared Function GetData(ByVal PageSetupReport_ID As String) As clsPDFPageSetup
        Dim obj As clsPDFPageSetup = Nothing
        Dim qry As String = "select isnull(Page_Width,0) as Page_Width,isnull(Page_Hight,0) AS Page_Hight,isnull(Page_Size,'') as Page_Size ,isnull(Page_Style,'') as Page_Style from TSPL_PDF_PAGE_SIZE where ReportID='" + PageSetupReport_ID + "' and UserID='" + objCommonVar.CurrentUserCode + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPDFPageSetup()
            obj.Page_Size = clsCommon.myCstr(dt.Rows(0)("Page_Size"))
            obj.Page_Style = clsCommon.myCstr(dt.Rows(0)("Page_Style"))
            obj.Width = clsCommon.myCdbl(dt.Rows(0)("Page_Width"))
            obj.Height = clsCommon.myCdbl(dt.Rows(0)("Page_Hight"))
            obj.PageSetupReport_ID = clsCommon.myCstr(PageSetupReport_ID)
           
        End If

        Return obj
    End Function


End Class

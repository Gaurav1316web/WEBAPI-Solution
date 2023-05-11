Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsTemplateExpImp

#Region "Variables"
    Public Export_Code As String
    Public Program_Code As String
    'Public Report_Type As String
    Public Template_Name As String
    Public Arr As New List(Of clsTemplateExpImpDetail)
#End Region
    Public Shared Function GetData(ByVal strCode As String, ByVal Program_Code As String, ByVal NavType As NavigatorType) As clsTemplateExpImp
        Return GetData(strCode, Program_Code, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_TEMPLATE_EXPIMP_DETAIL where Export_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_TEMPLATE_EXPIMP_HEAD where Export_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal Program_Code As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTemplateExpImp
        Dim obj As New clsTemplateExpImp()
        Dim objtr As New clsTemplateExpImpDetail()

        Dim qry As String = "select * from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & Program_Code & "' "
        'If clsCommon.myLen(Report_Type) > 0 Then
        '    qry = qry & " and Report_Type='" & Report_Type & "'"
        'End If
        Select Case NavType
            Case NavigatorType.First
                qry += " AND Export_Code = (select MIN(Export_Code) from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & Program_Code & "' )"
            Case NavigatorType.Last
                qry += " AND Export_Code = (select Max(Export_Code) from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & Program_Code & "' )"
            Case NavigatorType.Next
                qry += " AND Export_Code = (select Min(Export_Code) from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & Program_Code & "' and Export_Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " AND Export_Code = (select Max(Export_Code) from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & Program_Code & "' and Export_Code<'" + strCode + "' )"
            Case NavigatorType.Current
                qry += " AND Export_Code = '" + strCode + "'"
            Case Else
                qry += " AND Export_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.Export_Code = dt.Rows(0)("Export_Code")
            obj.Program_Code = dt.Rows(0)("Program_Code")
            'obj.Report_Type = dt.Rows(0)("Report_Type")
            obj.Template_Name = clsCommon.myCstr(dt.Rows(0)("Template_Name"))
            strCode = dt.Rows(0)("Export_Code")
        End If

        obj.Arr = clsTemplateExpImpDetail.GetDetail(strCode, trans)

        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsTemplateExpImp, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String
            ''where Program_Code='" & obj.Program_Code & "'  " & If(clsCommon.myLen(obj.Report_Type) > 0, " and Report_Type='" & obj.Report_Type & "'", "") & "
            If isNewEntry Then
                qry = "select max(Export_Code) as Export_Code from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & obj.Program_Code & "'"
                obj.Export_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.Export_Code) <= 0 Then
                    obj.Export_Code = "" & obj.Program_Code & "" & "/EXPT000001"
                Else
                    obj.Export_Code = clsCommon.incval(obj.Export_Code)
                End If
            End If
            If (clsCommon.myLen(obj.Export_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            qry = "delete from TSPL_TEMPLATE_EXPIMP_DETAIL where Export_Code='" & obj.Export_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Export_Code", obj.Export_Code)
            clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
            clsCommon.AddColumnsForChange(coll, "Template_Name", obj.Template_Name)
            'clsCommon.AddColumnsForChange(coll, "Report_Type", obj.Report_Type)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_TEMPLATE_EXPIMP_HEAD where Export_Code = '" & obj.Export_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMPLATE_EXPIMP_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.Export_Code + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMPLATE_EXPIMP_HEAD", OMInsertOrUpdate.Update, "TSPL_TEMPLATE_EXPIMP_HEAD.Export_Code='" + obj.Export_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsTemplateExpImpDetail.SaveDetailData(obj, trans)

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " SELECT TSPL_TEMPLATE_EXPIMP_HEAD.Export_Code AS Code,TSPL_TEMPLATE_EXPIMP_HEAD.Template_Name,TSPL_TEMPLATE_EXPIMP_HEAD.Program_Code as [Report ID]," &
                            " TSPL_TEMPLATE_EXPIMP_HEAD.Modify_By,TSPL_TEMPLATE_EXPIMP_HEAD.Created_By FROM TSPL_TEMPLATE_EXPIMP_HEAD " &
                            " left join TSPL_PROGRAM_MASTER on TSPL_TEMPLATE_EXPIMP_HEAD.Program_Code=TSPL_PROGRAM_MASTER.Program_Code"
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_TEMPLATE_EXPIMP_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_TEMPLATE_EXPIMP_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("Expt", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function
    Public Shared Function GetName(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Template_Name from TSPL_TEMPLATE_EXPIMP_HEAD where Export_Code='" & Code & "'"
            Dim name As String = clsCommon.myCstr(clsDBFuncationality.ExecuteNonQuery(qry, trans))
            Return name
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsTemplateExpImpDetail
#Region "Variables"
    '' grid columns details
    Public Export_Code As String
    Public Seq_No As Integer
    Public Column_Name As String
    Public Column_Header As String
    Public IsMandatory As Integer = 0
#End Region

    Public Shared Function SaveDetailData(ByVal obj As clsTemplateExpImp, ByVal trans As SqlTransaction) As Boolean

        Try
            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                Dim qry As String = "DELETE FROM TSPL_TEMPLATE_EXPIMP_DETAIL WHERE Export_Code='" + obj.Export_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                For Each objTr As clsTemplateExpImpDetail In obj.Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Export_Code", obj.Export_Code)
                    clsCommon.AddColumnsForChange(coll, "Seq_No", objTr.Seq_No)
                    clsCommon.AddColumnsForChange(coll, "Column_Name", objTr.Column_Name)
                    clsCommon.AddColumnsForChange(coll, "Column_Header", objTr.Column_Header)
                    clsCommon.AddColumnsForChange(coll, "IsMandatory", objTr.IsMandatory)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMPLATE_EXPIMP_DETAIL", OMInsertOrUpdate.Insert, "TSPL_TEMPLATE_EXPIMP_DETAIL.Export_Code='" + obj.Export_Code + "' ", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetDetail(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsTemplateExpImpDetail)
        Dim qry As String
        qry = "select Export_Code,Seq_No,Column_Name,Column_Header,IsMandatory from TSPL_TEMPLATE_EXPIMP_DETAIL WHERE Export_Code = '" & strCode & "' ORDER BY Seq_No"

        Dim objtr As New clsTemplateExpImpDetail
        Dim ObjList As New List(Of clsTemplateExpImpDetail)
        Dim dt As New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsTemplateExpImpDetail()
                objtr.Export_Code = clsCommon.myCstr(dr("Export_Code"))
                objtr.Seq_No = clsCommon.myCdbl(dr("Seq_No"))
                objtr.Column_Name = clsCommon.myCstr(dr("Column_Name"))
                objtr.Column_Header = clsCommon.myCstr(dr("Column_Header"))
                objtr.IsMandatory = clsCommon.myCdbl(dr("IsMandatory"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

'sanjay
Public Class clsManageTemplateExpImp
    Public ReportId As String
    Public TemplateName As String
    Public GridLayout As String
    Public GridColumns As Integer


    Public Shared Function DeleteData(ByVal ReportId As String, ByVal TemplateName As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(ReportId) <= 0 And clsCommon.myLen(TemplateName) <= 0) Then
                Throw New Exception("Report Id not found to Delete")
            End If
            If (clsCommon.myLen(TemplateName) <= 0) Then
                Throw New Exception("Template not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_MANAGE_TEMPLATE where ReportId ='" + ReportId + "' and TemplateName='" + TemplateName + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal ReportId As String, ByVal TemplateName As String) As clsManageTemplateExpImp ', ByVal trans As SqlTransaction
        Dim obj As New clsManageTemplateExpImp()
        Try


            Dim qry As String = "select * from TSPL_MANAGE_TEMPLATE where ReportId='" & ReportId & "' and TemplateName='" & TemplateName & "' "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry) ', trans
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj.ReportId = clsCommon.myCstr(dt.Rows(0)("ReportId"))
                obj.TemplateName = clsCommon.myCstr(dt.Rows(0)("TemplateName"))
                obj.GridLayout = clsCommon.myCstr(dt.Rows(0)("GridLayout"))
                obj.GridColumns = clsCommon.myCdbl(dt.Rows(0)("GridColumns"))

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return obj
    End Function


    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Try

            Dim qry As String = " SELECT TemplateName AS Name FROM TSPL_MANAGE_TEMPLATE"
            Dim str As String = ""

            str = clsCommon.ShowSelectForm("Exptemplate", qry, "Name", whrCls, currCode, "Name", isButtonClicked)

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
    End Function


    Public Shared Function SaveData(ByVal obj As clsManageTemplateExpImp) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String
            qry = "delete from TSPL_MANAGE_TEMPLATE where TemplateName='" & obj.TemplateName & "' and ReportId='" & obj.ReportId & "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ReportId", obj.ReportId)
            clsCommon.AddColumnsForChange(coll, "TemplateName", obj.TemplateName)
            clsCommon.AddColumnsForChange(coll, "GridLayout", obj.GridLayout)
            clsCommon.AddColumnsForChange(coll, "GridColumns", obj.GridColumns)
            clsCommon.AddColumnsForChange(coll, "UserID", objCommonVar.CurrentUserCode)

            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MANAGE_TEMPLATE", OMInsertOrUpdate.Insert, "", trans)


            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


End Class

Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsStandardScheme

#Region "Variables"
    Public StdSchCode As String
    Public FomeDate As DateTime
    Public Valied_Date As DateTime
    Public IsValied_Date As Boolean
    Public Cust_Code As String
    Public Descraption As String
    Public Shared ObjList As List(Of clsStandardSchemeDetail)
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_STANDARD_SCHEME.StdSchCode as [Code] ,TSPL_STANDARD_SCHEME.FomeDate as [From Date] ,TSPL_STANDARD_SCHEME.Valied_Date as [Valid Date] ,TSPL_STANDARD_SCHEME.IsValied_Date as [Isvalid Date] ,TSPL_STANDARD_SCHEME.Cust_Code as [Customer Code] ,TSPL_STANDARD_SCHEME.Descraption as [Description] ,TSPL_STANDARD_SCHEME.Created_By as [Created By] ,TSPL_STANDARD_SCHEME.Created_Date as [Created Date] ,TSPL_STANDARD_SCHEME.Modified_By as [Modified By] ,TSPL_STANDARD_SCHEME.Modified_Date as [Modified Date]  From TSPL_STANDARD_SCHEME  "
        str = clsCommon.ShowSelectForm("STDSCHMFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_STANDARD_SCHEME_DETAIL where StdSchCode ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_STANDARD_SCHEME where StdSchCode ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsStandardScheme
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsStandardScheme
        Dim obj As New clsStandardScheme()
        Dim objtr As New clsStandardSchemeDetail()
        ObjList = New List(Of clsStandardSchemeDetail)
        Dim qry As String = " SELECT * FROM TSPL_STANDARD_SCHEME " _
                            & " where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and StdSchCode = (select MIN(StdSchCode) from TSPL_STANDARD_SCHEME)"
            Case NavigatorType.Last
                qry += " and StdSchCode = (select Max(StdSchCode) from TSPL_STANDARD_SCHEME)"
            Case NavigatorType.Next
                qry += " and StdSchCode = (select Min(StdSchCode) from TSPL_STANDARD_SCHEME where  StdSchCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and StdSchCode = (select Max(StdSchCode) from TSPL_STANDARD_SCHEME where StdSchCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and StdSchCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.StdSchCode = clsCommon.myCstr(dt.Rows(0)("StdSchCode"))
            obj.FomeDate = clsCommon.myCDate(dt.Rows(0)("FomeDate"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Descraption = clsCommon.myCstr(dt.Rows(0)("Descraption"))
            If clsCommon.myCBool(dt.Rows(0)("IsValied_Date")) Then
                obj.IsValied_Date = clsCommon.myCBool(dt.Rows(0)("IsValied_Date"))
                obj.Valied_Date = clsCommon.myCDate(dt.Rows(0)("Valied_Date"))
            Else
                obj.IsValied_Date = clsCommon.myCBool(dt.Rows(0)("IsValied_Date"))
                obj.Valied_Date = Nothing
            End If
            clsStandardScheme.ObjList = objtr.GetData(obj.StdSchCode, trans)
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsStandardScheme, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        If isNewEntry Then
            If clsCommon.myLen(obj.StdSchCode) < 1 Then
                obj.StdSchCode = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.FomeDate, "dd/MMM/yyyy"), clsDocType.StandardScheme, "", "")
                If (clsCommon.myLen(obj.StdSchCode) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FomeDate", clsCommon.GetPrintDate(obj.FomeDate, "dd/MMM/yyyy"))
            If obj.Valied_Date.Year > 1900 Then
                clsCommon.AddColumnsForChange(coll, "Valied_Date", clsCommon.GetPrintDate(obj.Valied_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Valied_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "IsValied_Date", obj.IsValied_Date)
            clsCommon.AddColumnsForChange(coll, "Descraption", obj.Descraption)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "StdSchCode", obj.StdSchCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_SCHEME", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_SCHEME", OMInsertOrUpdate.Update, "TSPL_STANDARD_SCHEME.StdSchCode  = '" + obj.StdSchCode + "'", trans)
            End If
            isSaved = isSaved AndAlso clsStandardSchemeDetail.SaveData(obj.StdSchCode, clsStandardScheme.ObjList, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
End Class

Public Class clsStandardSchemeDetail
#Region "Variables"

    Public StdSchCode As String
    Public Line_No As String
    Public MainItem_Code As String
    Public MainDescraption As String
    Public MainUnit As String
    Public MainMRP As Decimal
    Public MainRate As Decimal
    Public SchItem_Code As String
    Public SchDescraption As String
    Public SchUnit As String
    Public SchMRP As Decimal
    Public SchRate As Decimal
    Public ObjList As List(Of clsStandardSchemeDetail)
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsStandardSchemeDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = " delete from TSPL_STANDARD_SCHEME_DETAIL where StdSchCode ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsStandardSchemeDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "StdSchCode", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "MainItem_Code", obj.MainItem_Code)
                    clsCommon.AddColumnsForChange(coll, "MainDescraption", obj.MainDescraption)
                    clsCommon.AddColumnsForChange(coll, "MainUnit ", obj.MainUnit)
                    clsCommon.AddColumnsForChange(coll, "MainMRP", obj.MainMRP)
                    clsCommon.AddColumnsForChange(coll, "MainRate", obj.MainRate)
                    clsCommon.AddColumnsForChange(coll, "SchItem_Code", obj.SchItem_Code)
                    clsCommon.AddColumnsForChange(coll, "SchDescraption", obj.SchDescraption)
                    clsCommon.AddColumnsForChange(coll, "SchUnit ", obj.SchUnit)
                    clsCommon.AddColumnsForChange(coll, "SchMRP", obj.SchMRP)
                    clsCommon.AddColumnsForChange(coll, "SchRate", obj.SchRate)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_SCHEME_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsStandardSchemeDetail)
        Dim qry As String = " "

        qry += " select * FROM TSPL_STANDARD_SCHEME_DETAIL "
        qry += " where StdSchCode = '" + strDocNo + "'"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsStandardSchemeDetail
        ObjList = New List(Of clsStandardSchemeDetail)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsStandardSchemeDetail()
                objtr.StdSchCode = clsCommon.myCstr(dr("StdSchCode"))
                objtr.Line_No = clsCommon.myCstr(dr("Line_No"))
                objtr.MainItem_Code = clsCommon.myCstr(dr("MainItem_Code"))
                objtr.MainDescraption = clsCommon.myCstr(dr("MainDescraption"))
                objtr.MainUnit = clsCommon.myCstr(dr("MainUnit"))
                objtr.MainMRP = clsCommon.myCdbl(dr("MainMRP"))
                objtr.MainRate = clsCommon.myCdbl(dr("MainRate"))
                objtr.SchItem_Code = clsCommon.myCstr(dr("SchItem_Code"))
                objtr.SchDescraption = clsCommon.myCstr(dr("SchDescraption"))
                objtr.SchUnit = clsCommon.myCstr(dr("SchUnit"))
                objtr.SchMRP = clsCommon.myCdbl(dr("SchMRP"))
                objtr.SchRate = clsCommon.myCdbl(dr("SchRate"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
End Class

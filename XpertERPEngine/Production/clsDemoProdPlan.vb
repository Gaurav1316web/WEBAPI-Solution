Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsDemoProdPlan

#Region "Variables"

    Public Document_No As String = Nothing
    Public Cust_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public TotalSec As Double = 0
    Public With_TP As Double = 0
    Public Hours_Reqd As Double = 0
    Public Days_Reqd As Double = 0
    Public Labour As Double = 0
    Public Labour_hour As Double = 0
    Public Labour_Sec As Double = 0
    Public Labour_Part As Double = 0
    Public Description As String = Nothing
    Public Reference As String = Nothing
    Public TotalQty As Double = 0
    Public Arr As List(Of ClsDemoProdPlanDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsDemoProdPlan, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsDemoProdPlan, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_DEMO_PRODPLAN_DETAIL where DOCUMENT_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If isNewEntry Then
                Dim strCode As String = clsDBFuncationality.getSingleValue("select isnull(max(DOCUMENT_No),'') from TSPL_DEMO_PRODPLAN_HEAD", trans)
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.Document_No = "PROD000000001"
                Else
                    obj.Document_No = clsCommon.incval(strCode)
                End If

            End If


            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable

            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "TotalSec", obj.TotalSec)
            clsCommon.AddColumnsForChange(coll, "With_TP", obj.With_TP)
            clsCommon.AddColumnsForChange(coll, "Hours_Reqd", obj.Hours_Reqd)
            clsCommon.AddColumnsForChange(coll, "Days_Reqd", obj.Days_Reqd)
            clsCommon.AddColumnsForChange(coll, "Labour", obj.Labour)
            clsCommon.AddColumnsForChange(coll, "Labour_hour", obj.Labour_hour)
            clsCommon.AddColumnsForChange(coll, "Labour_Sec", obj.Labour_Sec)
            clsCommon.AddColumnsForChange(coll, "Labour_Part", obj.Labour_Part)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "TotalQty", obj.TotalQty)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMO_PRODPLAN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMO_PRODPLAN_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsDemoProdPlanDetail.SaveData(obj.Document_No, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsDemoProdPlan()
        obj = clsDemoProdPlan.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try

                Dim qry As String = "delete from TSPL_DEMO_PRODPLAN_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_DEMO_PRODPLAN_HEAD where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDemoProdPlan

        Dim obj As clsDemoProdPlan = Nothing
        Dim qry As String = "SELECT * from TSPL_DEMO_PRODPLAN_HEAD where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEMO_PRODPLAN_HEAD.Document_No = (select MIN(Document_No) from TSPL_DEMO_PRODPLAN_HEAD where 1=1)"
            Case NavigatorType.Last
                qry += " and TSPL_DEMO_PRODPLAN_HEAD.Document_No = (select Max(Document_No) from TSPL_DEMO_PRODPLAN_HEAD where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_DEMO_PRODPLAN_HEAD.Document_No = (select Min(Document_No) from TSPL_DEMO_PRODPLAN_HEAD where Document_No>'" + strDocNo + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_DEMO_PRODPLAN_HEAD.Document_No = (select Max(Document_No) from TSPL_DEMO_PRODPLAN_HEAD where Document_No<'" + strDocNo + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_DEMO_PRODPLAN_HEAD.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDemoProdPlan()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.TotalSec = clsCommon.myCdbl(dt.Rows(0)("TotalSec"))
            obj.With_TP = clsCommon.myCdbl(dt.Rows(0)("With_TP"))
            obj.Hours_Reqd = clsCommon.myCdbl(dt.Rows(0)("Hours_Reqd"))
            obj.Days_Reqd = clsCommon.myCdbl(dt.Rows(0)("Days_Reqd"))
            obj.Labour = clsCommon.myCdbl(dt.Rows(0)("Labour"))
            obj.Labour_hour = clsCommon.myCdbl(dt.Rows(0)("Labour_hour"))
            obj.Labour_Sec = clsCommon.myCdbl(dt.Rows(0)("Labour_Sec"))
            obj.Labour_Part = clsCommon.myCdbl(dt.Rows(0)("Labour_Part"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.TotalQty = clsCommon.myCdbl(dt.Rows(0)("TotalQty"))
            qry = "SELECT  * from TSPL_DEMO_PRODPLAN_DETAIL where  Document_No='" + obj.Document_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsDemoProdPlanDetail)
                Dim objTr As ClsDemoProdPlanDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsDemoProdPlanDetail()
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Document_Line_No = clsCommon.myCdbl(dr("Document_Line_No"))
                    objTr.Labour_Strenth = clsCommon.myCdbl(dr("Labour_Strenth"))
                    objTr.Process_Code = clsCommon.myCstr(dr("Process_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Capacity = clsCommon.myCstr(dr("Capacity"))
                    objTr.Reqd = clsCommon.myCdbl(dr("Reqd"))
                    objTr.Requirement = clsCommon.myCdbl(dr("Requirement"))
                    objTr.Labour_Capacity = clsCommon.myCdbl(dr("Labour_Capacity"))
                    objTr.Second_Piece = clsCommon.myCdbl(dr("Second_Piece"))
                    objTr.Required_Time = clsCommon.myCdbl(dr("Required_Time"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
End Class
Public Class ClsDemoProdPlanDetail
#Region "Variable"

    Public Document_No As String = Nothing
    Public Document_Line_No As Integer = 0
    Public Labour_Strenth As Integer = 0
    Public Process_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Capacity As String = Nothing
    Public Reqd As Integer = 0
    Public Requirement As Integer = 0
    Public Labour_Capacity As Integer = 0
    Public Second_Piece As Double = 0
    Public Required_Time As Double = 0

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsDemoProdPlanDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsDemoProdPlanDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Document_Line_No", counter)
                counter += 1
                clsCommon.AddColumnsForChange(coll, "Labour_Strenth", objtr.Labour_Strenth)
                clsCommon.AddColumnsForChange(coll, "Process_Code", objtr.Process_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Capacity", objtr.Capacity)
                clsCommon.AddColumnsForChange(coll, "Reqd", objtr.Reqd)
                clsCommon.AddColumnsForChange(coll, "Requirement", objtr.Requirement)
                clsCommon.AddColumnsForChange(coll, "Labour_Capacity", objtr.Labour_Capacity)
                clsCommon.AddColumnsForChange(coll, "Second_Piece", objtr.Second_Piece)
                clsCommon.AddColumnsForChange(coll, "Required_Time", objtr.Required_Time)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMO_PRODPLAN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class


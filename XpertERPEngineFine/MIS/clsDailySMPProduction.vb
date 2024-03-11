Imports System.Data.SqlClient
Public Class clsDailySMPProduction
#Region "Variables"
    Public Document_No As String = Nothing
    Public Report_Date As DateTime = Nothing
    Public Reporting_Date As DateTime = Nothing
    Public Status As Integer = 0
    Public Party_Name1 As String = Nothing
    Public Party_Name2 As String = Nothing
    Public Party_Name3 As String = Nothing
    Public Party_Name4 As String = Nothing
    Public Party_Name5 As String = Nothing
    Public Arr As List(Of clsDailySMPProductionDetails) = Nothing
    Public ArrPw As List(Of clsDailySMPProductionDetailsPowder) = Nothing
#End Region


    Public Shared Function getFinder(ByVal whrcls As String, ByVal docuno As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Document_No as [Document],Reporting_Date as [Reporting Date],Report_Date as [Report Date] from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD "
        str = clsCommon.ShowSelectForm("ITEMGPFND", qry, "Document", whrcls, docuno, "Document", isButtonClicked)
        Return str
    End Function
    Public Function SaveData(ByVal obj As clsDailySMPProduction, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsDailySMPProduction, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL where Document_No ='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Reporting_Date), clsDocType.frmDailySMPProduction, "", "")
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim colm As New Hashtable

            clsCommon.AddColumnsForChange(colm, "Report_Date", clsCommon.GetPrintDate(obj.Report_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(colm, "Reporting_Date", clsCommon.GetPrintDate(obj.Reporting_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(colm, "Party_Name1", obj.Party_Name1)
            clsCommon.AddColumnsForChange(colm, "Party_Name2", obj.Party_Name2)
            clsCommon.AddColumnsForChange(colm, "Party_Name3", obj.Party_Name3)
            clsCommon.AddColumnsForChange(colm, "Party_Name4", obj.Party_Name4)
            clsCommon.AddColumnsForChange(colm, "Party_Name5", obj.Party_Name5)
            clsCommon.AddColumnsForChange(colm, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(colm, "Modify_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(colm, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(colm, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(colm, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD", OMInsertOrUpdate.Update, "TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsDailySMPProductionDetails.SavaData(obj.Document_No, obj.Arr, trans)
            isSaved = isSaved AndAlso clsDailySMPProductionDetailsPowder.SavaData(obj.Document_No, obj.ArrPw, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved

    End Function

    Public Shared Function GetData(ByVal docno As String, ByVal navtype As NavigatorType) As clsDailySMPProduction
        Dim obj As clsDailySMPProduction = Nothing
        Dim qry As String = "SELECT * from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where Document_No=  '" + docno + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDailySMPProduction()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Report_Date = clsCommon.GetPrintDate(dt.Rows(0)("Reporting_Date"), "dd/MMM/yyyy")
            obj.Reporting_Date = clsCommon.GetPrintDate(dt.Rows(0)("Report_Date"), "dd/MMM/yyyy")
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
            qry = "SELECT * from TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL where Document_No=  '" + docno + "'"
            dt = New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDailySMPProductionDetails)
                Dim objtr As clsDailySMPProductionDetails
                For Each dr As DataRow In dt.Rows
                    objtr = New clsDailySMPProductionDetails
                    objtr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objtr.App_Code = clsCommon.myCstr(dr("App_Code"))
                    objtr.Source_Name = clsDBFuncationality.getSingleValue("SELECT [TSPL_APP_LOCATION].Location_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Code= '" + objtr.App_Code + "' ")
                    objtr.Trans_Type = clsCommon.myCstr(dr("Trans_Type"))
                    objtr.Qty = clsCommon.myCstr(dr("Qty"))
                    objtr.FAT = clsCommon.myCstr(dr("FAT"))
                    objtr.SNF = clsCommon.myCstr(dr("SNF"))
                    objtr.Total_Job_Work = clsCommon.myCstr(dr("Total_Job_Work"))
                    objtr.Party_Name1 = clsCommon.myCstr(dr("Party_Name1"))
                    obj.Arr.Add(objtr)
                Next

            End If
            qry = "SELECT * from TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL_POWDER where Document_No=  '" + docno + "'"
            dt = New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrPw = New List(Of clsDailySMPProductionDetailsPowder)
                Dim objtr As clsDailySMPProductionDetailsPowder
                For Each dr As DataRow In dt.Rows
                    objtr = New clsDailySMPProductionDetailsPowder
                    objtr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objtr.Powder_Type = clsCommon.myCstr(dr("Powder_Type"))
                    objtr.Production = clsCommon.myCstr(dr("Production"))
                    objtr.Sale = clsCommon.myCstr(dr("Sale"))
                    objtr.Closing_Stock = clsCommon.myCstr(dr("Closing_Stock"))
                    obj.ArrPw.Add(objtr)
                Next

            End If
        End If
        Return obj
    End Function

    Public Shared Function DocNO_Navigation(ByVal NavType As NavigatorType, ByVal docNo As String)
        Dim sql As String = "select Document_No from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
            Case NavigatorType.Next
                sql += "and Document_No in (select min(Document_No) from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where Document_No>'" + docNo + "')"
            Case NavigatorType.First
                sql += "and Document_No in (select MIN(Document_No) from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD)"
            Case NavigatorType.Last
                sql += "and Document_No in (select MAX(Document_No) from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD)"
            Case NavigatorType.Previous
                sql += "and Document_No in (select max(Document_No) from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where Document_No<'" + docNo + "')"
        End Select
        Return clsDBFuncationality.getSingleValue(sql)
    End Function

    Public Shared Function DeleteData(ByVal docno As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(docno, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal docno As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(docno) <= 0) Then
            Throw New Exception("Document no not found for delete")
        End If
        Dim qry As String = " "

        qry = "delete from TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL_POWDER where Document_No='" + docno + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "delete from TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL where Document_No='" + docno + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "delete from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where Document_No='" + docno + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = "Update TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD set Status=1,Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsDailySMPProductionDetails
    Public Document_No As String = Nothing
    Public App_Code As String = Nothing
    Public Source_Name As String = Nothing
    Public Trans_Type As String = Nothing
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public Total_Job_Work As Decimal = 0
    Public arrParty_Name As List(Of ArrayList) = Nothing
    Public columnCount As Integer
    Public Party_Name1 As Decimal = 0
    Public Party_Name2 As Decimal = 0
    Public Party_Name3 As Decimal = 0
    Public Party_Name4 As Decimal = 0
    Public Party_Name5 As Decimal = 0

    Public Shared Function SavaData(ByVal DocNo As String, ByVal Array As List(Of clsDailySMPProductionDetails), ByVal trans As SqlTransaction) As Boolean
        If (Array IsNot Nothing AndAlso Array.Count > 0) Then
            For Each obj As clsDailySMPProductionDetails In Array
                Dim colm As New Hashtable
                clsCommon.AddColumnsForChange(colm, "Document_No", DocNo)
                clsCommon.AddColumnsForChange(colm, "App_Code", obj.App_Code)
                clsCommon.AddColumnsForChange(colm, "Trans_Type", obj.Trans_Type)
                clsCommon.AddColumnsForChange(colm, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(colm, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(colm, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(colm, "Total_Job_Work", obj.Total_Job_Work)
                clsCommon.AddColumnsForChange(colm, "Party_Name1", obj.Party_Name1)
                clsCommon.AddColumnsForChange(colm, "Party_Name2", obj.Party_Name2)
                clsCommon.AddColumnsForChange(colm, "Party_Name3", obj.Party_Name3)
                clsCommon.AddColumnsForChange(colm, "Party_Name4", obj.Party_Name4)
                clsCommon.AddColumnsForChange(colm, "Party_Name5", obj.Party_Name5)
                'Dim columnIndex As Integer = 2
                'For ii As Integer = 0 To obj.columnCount - 1

                '    clsCommon.AddColumnsForChange(colm, "Party_Name" & columnIndex & "", obj.arrParty_Name(ii)(ii))
                '    columnIndex = columnIndex + 1

                'Next

                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
Public Class clsDailySMPProductionDetailsPowder
    Public Document_No As String = Nothing
    Public Powder_Type As String = Nothing
    Public Production As Decimal = 0
    Public Sale As Decimal = 0
    Public Closing_Stock As Decimal = 0

    Public Shared Function SavaData(ByVal DocNo As String, ByVal Array As List(Of clsDailySMPProductionDetailsPowder), ByVal trans As SqlTransaction) As Boolean
        If (Array IsNot Nothing AndAlso Array.Count > 0) Then
            For Each obj As clsDailySMPProductionDetailsPowder In Array
                Dim colm As New Hashtable
                clsCommon.AddColumnsForChange(colm, "Document_No", DocNo)
                clsCommon.AddColumnsForChange(colm, "Powder_Type", obj.Powder_Type)
                clsCommon.AddColumnsForChange(colm, "Production", obj.Production)
                clsCommon.AddColumnsForChange(colm, "Sale", obj.Sale)
                clsCommon.AddColumnsForChange(colm, "Closing_Stock", obj.Closing_Stock)
                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_SMP_PRODUCTION_DETAIL_POWDER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

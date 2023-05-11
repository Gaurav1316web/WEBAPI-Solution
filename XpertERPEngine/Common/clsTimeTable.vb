
Imports System.Data
Imports System.Data.SqlClient
Imports common


Imports Telerik.WinControls
Public Class clsTimeTable
#Region "Variable"
    Public Document_Code As String
    Public Shift As String
    Public Type As String
    Public Document_Date As String
    Public Order_From_Time As String
    Public Order_To_Time As String
    Public Payment_From_Time As String
    Public Payment_To_Time As String


    'Public DealerCode As String
    'Public DealerType As String
    'Public BookingFromTime As String
    'Public BookingToTime As String
    'Public PaymentFromTime As String
    'Public PaymentToTime As String




#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Document_Code as Code,Convert (varchar, Document_Date,103) as Document_Date,[Type] ,Format( [Order_From_Time],'hh:mm tt') as Order_From_Time,Format( Order_To_Time,'hh:mm tt') as Order_To_Time,Format( Payment_From_Time,'hh:mm tt') as Payment_From_Time,Format( Payment_To_Time,'hh:mm tt') as Payment_To_Time from TSPL_Ondemand_Distatch_Detail "
        str = clsCommon.ShowSelectForm("OrderPaymentTime", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsTimeTable
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTimeTable
        Dim obj As clsTimeTable = Nothing
        Dim qry As String = "select Document_Code,Type,Document_Date,Order_From_Time,Order_To_Time,Payment_From_Time,Payment_To_Time from TSPL_Ondemand_Distatch_Detail where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Document_Code =  (select MIN(Document_Code) from TSPL_Ondemand_Distatch_Detail)"
            Case NavigatorType.Last
                qry += " and Document_Code = (select Max(Document_Code) from TSPL_Ondemand_Distatch_Detail)"
            Case NavigatorType.Next
                qry += " and Document_Code = (select Min(Document_Code) from TSPL_Ondemand_Distatch_Detail where  Document_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Document_Code = (select Max(Document_Code) from TSPL_Ondemand_Distatch_Detail where Document_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTimeTable()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Order_From_Time = clsCommon.myCstr(dt.Rows(0)("Order_From_Time"))
            obj.Order_To_Time = clsCommon.myCstr(dt.Rows(0)("Order_To_Time"))
            obj.Payment_From_Time = clsCommon.myCstr(dt.Rows(0)("Payment_From_Time"))
            obj.Payment_To_Time = clsCommon.myCstr(dt.Rows(0)("Payment_To_Time"))

        End If
        Return obj
        'Dim obj As clsTimeTable = Nothing
        'Dim qry As String = "select Document_Code,Shift,Type,Document_Date,From_Time,To_Time from TSPL_Ondemand_Distatch_Detail where 2=2"
        'Select Case NavType
        '    Case NavigatorType.First
        '        qry += " and Document_Code = (select MIN(Document_Code) from TSPL_Ondemand_Distatch_Detail)"
        '    Case NavigatorType.Last
        '        qry += " and Document_Code = (select Max(Document_Code) from TSPL_Ondemand_Distatch_Detail)"
        '    Case NavigatorType.Next
        '        qry += " and Document_Code = (select Min(Document_Code) from TSPL_Ondemand_Distatch_Detail where  Document_Code>'" + strCode + "')"
        '    Case NavigatorType.Previous
        '        qry += " and Document_Code = (select Max(Document_Code) from TSPL_Ondemand_Distatch_Detail where Document_Code<'" + strCode + "')"
        '    Case NavigatorType.Current
        '        qry += " and Document_Code = '" + strCode + "'"
        'End Select
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    obj = New clsTimeTable()
        '    obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
        '    obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
        '    obj.Shift = clsCommon.myCstr(dt.Rows(0)("Shift"))
        '    obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
        '    obj.From_Time = clsCommon.myCstr(dt.Rows(0)("From_Time"))
        '    obj.To_Time = clsCommon.myCstr(dt.Rows(0)("To_Time"))

        'End If
        'Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim obj As clsTimeTable = clsTimeTable.GetData(strCode, NavigatorType.Current)

            Dim qry As String
            qry = "delete from TSPL_Ondemand_Distatch_Detail where Document_Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsTimeTable, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SaveData(ByVal obj As clsTimeTable, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "MM/dd/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Order_From_Time", clsCommon.GetPrintDate(obj.Order_From_Time, "hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Order_To_Time", clsCommon.GetPrintDate(obj.Order_To_Time, "hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Payment_From_Time", clsCommon.GetPrintDate(obj.Payment_From_Time, "hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Payment_To_Time", clsCommon.GetPrintDate(obj.Payment_To_Time, "hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "MM/dd/yyyy"))

            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Ondemand_Distatch_Detail where Document_Code='" & obj.Document_Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.Document_Date, "MM/dd/yyyy"), clsDocType.TimeTable, "", "")
                        If clsCommon.myLen(obj.Document_Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "MM/dd/yyyy"))


                Dim qry As String = "SELECT Count(*)   FROM TSPL_Ondemand_Distatch_Detail  where Document_Code= '" & obj.Document_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                Dim qry1 As String = "SELECT Count(*)   FROM TSPL_Ondemand_Distatch_Detail  where Document_Date= '" & clsCommon.GetPrintDate(obj.Document_Date, "MM/dd/yyyy") & "' and Type= '" & obj.Type & "'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                If check = 0 Then
                    If (check1 = 0) Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Ondemand_Distatch_Detail", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("Document Type already exist on this date")
                    End If

                Else
                    Throw New Exception("This document Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Ondemand_Distatch_Detail", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "' ", trans)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_Ondemand_Distatch_Detail", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class


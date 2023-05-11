Imports common
Imports System.Data.SqlClient

Public Class clsSalesmanTargetHeader
   
#Region "Variables"
    Public Code As String = Nothing
    Public Salesman_Code As String = Nothing
    Public Target_Type As String = "I"
    Public MonthYear As Date = Nothing
    Public Amount As Double = 0
    Public Arr As List(Of clsSalesmanTargetDetail) = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select Code, [Sales Man Code] ,[Salesman] , [Target Type] , Month+' - '+CONVERT(VARCHAR,Year,103) as MonthYear,[Amount],[Created By],[Modified By],[Created Date],[Modified Date] ,[Company Code] from (Select TSPL_SD_SALESMAN_TARGET_HEADER.Code as [Code] ,TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code as [Sales Man Code], TSPL_EMPLOYEE_MASTER.Emp_Name as [Salesman], Case When DATEPART(MM ,MonthYear)=1 Then 'JAN' When DATEPART(MM ,MonthYear)=2 Then 'FEB' When DATEPART(MM ,MonthYear)=3 Then 'MAR' When DATEPART(MM ,MonthYear)=4 Then 'APR' When DATEPART(MM ,MonthYear)=5 Then 'MAY' When DATEPART(MM ,MonthYear)=6 Then 'JUN' When DATEPART(MM ,MonthYear)=7 Then 'JUL' When DATEPART(MM ,MonthYear)=8 Then 'AUG' When DATEPART(MM ,MonthYear)=9 Then 'SEP' When DATEPART(MM ,MonthYear)=10 Then 'OCT' When DATEPART(MM ,MonthYear)=11 Then 'NOV' When DATEPART(MM ,MonthYear)=12 Then 'DEC' End as [Month],  DATEPART(YYYY, MonthYear ) as Year, Case When Target_Type='A' Then 'Amount Wise' Else 'Item Wise' END as [Target Type],Amount as [Amount],TSPL_SD_SALESMAN_TARGET_HEADER.Comp_Code as [Company Code] ,TSPL_SD_SALESMAN_TARGET_HEADER.Created_By as [Created By] ,TSPL_SD_SALESMAN_TARGET_HEADER.Created_Date as [Created Date] ,TSPL_SD_SALESMAN_TARGET_HEADER.Modified_By as [Modified By] ,TSPL_SD_SALESMAN_TARGET_HEADER.Modified_Date as [Modified Date] from TSPL_SD_SALESMAN_TARGET_HEADER Left Outer Join TSPL_EMPLOYEE_MASTER ON TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE)xxx "
        str = clsCommon.ShowSelectForm("SLSTRGTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function getAotoTargetNo(Optional ByVal tran1 As SqlTransaction = Nothing) As String
        Dim Maxvlu, strQuery As String
        Dim NxtMaxNo As Int32
        strQuery = "select max(Code) as Code From TSPL_SD_SALESMAN_TARGET_HEADER WHERE Code like '%STN%'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery, tran1)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)(0).ToString <> "" Then
                Maxvlu = clsCommon.myCstr(dt.Rows(0)("Code"))
                Maxvlu = Maxvlu.Remove(0, 3)
                NxtMaxNo = Convert.ToInt32(Maxvlu.ToString())
                NxtMaxNo = NxtMaxNo + 1
                Dim strCount As String
                strCount = NxtMaxNo.ToString()
                If strCount.Length = 1 Then
                    Maxvlu = "STN" & "000" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 2 Then
                    Maxvlu = "STN" & "00" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 3 Then
                    Maxvlu = "STN" & "0" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 4 Then
                    Maxvlu = "STN" & NxtMaxNo.ToString()
                End If
                Return Maxvlu
            Else
                Maxvlu = "STN0001"
                Return Maxvlu
            End If
        Else
            Maxvlu = "STN0001"
            Return Maxvlu
        End If
        Return Maxvlu
    End Function

    Public Function SaveData(ByVal obj As clsSalesmanTargetHeader, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction
        trans = clsDBFuncationality.GetTransactin()
        Try

            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsSalesmanTargetHeader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim Qry As String

            Qry = "Delete From TSPL_SD_SALESMAN_TARGET_DETAIL WHERE Code='" + obj.Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            clsCommon.AddColumnsForChange(coll, "Target_Type", obj.Target_Type)
            clsCommon.AddColumnsForChange(coll, "MonthYear", clsCommon.GetPrintDate(obj.MonthYear, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = getAotoTargetNo(trans)
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALESMAN_TARGET_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALESMAN_TARGET_HEADER", OMInsertOrUpdate.Update, "TSPL_SD_SALESMAN_TARGET_HEADER.Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSalesmanTargetDetail.SaveData(obj.Code, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType) As clsSalesmanTargetHeader
        Dim obj As clsSalesmanTargetHeader = Nothing
        Dim qry As String = "Select TSPL_SD_SALESMAN_TARGET_HEADER.Code, TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code, TSPL_SD_SALESMAN_TARGET_HEADER.Target_Type, TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear, TSPL_SD_SALESMAN_TARGET_HEADER.Amount from TSPL_SD_SALESMAN_TARGET_HEADER WHERE 1=1 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Code = (select MIN(Code) from TSPL_SD_SALESMAN_TARGET_HEADER)"
            Case NavigatorType.Last
                qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Code = (select Max(Code) from TSPL_SD_SALESMAN_TARGET_HEADER)"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Code = (select Min(Code) from TSPL_SD_SALESMAN_TARGET_HEADER where Code>'" + strPONo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Code = (select Max(Code) from TSPL_SD_SALESMAN_TARGET_HEADER where Code<'" + strPONo + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSalesmanTargetHeader()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Target_Type = clsCommon.myCstr(dt.Rows(0)("Target_Type"))
            obj.MonthYear = clsCommon.myCDate(dt.Rows(0)("MonthYear"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))


            qry = "Select TSPL_SD_SALESMAN_TARGET_DETAIL.Code, TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code, TSPL_SD_SALESMAN_TARGET_DETAIL.Qty, TSPL_SD_SALESMAN_TARGET_DETAIL.Cost, TSPL_SD_SALESMAN_TARGET_DETAIL.Amount from TSPL_SD_SALESMAN_TARGET_DETAIL"
            qry += " WHERE TSPL_SD_SALESMAN_TARGET_DETAIL.Code='" + obj.Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsSalesmanTargetDetail)
                Dim objTr As clsSalesmanTargetDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSalesmanTargetDetail
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Cost = clsCommon.myCdbl(dr("Cost"))
                    objTr.Amount = clsCommon.myCstr(dr("Amount"))
                    obj.Arr.Add(objTr)
                Next
            End If

        End If
        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Target not found to Delete.")
        End If
        Dim obj As clsSalesmanTargetHeader = clsSalesmanTargetHeader.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                Dim qry As String = "delete from TSPL_SD_SALESMAN_TARGET_DETAIL where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SALESMAN_TARGET_HEADER where Code='" + strCode + "'"
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
End Class

Public Class clsSalesmanTargetDetail

#Region "Variables"
    Public Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Qty As Double = 0
    Public Cost As Double = 0
    Public Amount As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSalesmanTargetDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSalesmanTargetDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Cost", obj.Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALESMAN_TARGET_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class

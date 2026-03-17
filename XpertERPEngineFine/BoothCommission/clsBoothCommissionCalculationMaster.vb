Imports System.Data.SqlClient

Public Class clsBoothCommissionCalculationMaster
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Month_Year As Date = Nothing
    Public Is_Mobile_User As Integer = 0
    Public Remark As String = Nothing
    Public Comment As String = Nothing
    Public Posted As Integer = Nothing
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsBoothCommissionCalculationDetail)
    Public Arr_Monthly As List(Of clsBoothCommissionMonthlyDetail)
#End Region
    Public Function SaveData(ByVal obj As clsBoothCommissionCalculationMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsBoothCommissionCalculationMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BOOTH_COMMISSION_CALCULATION_DETAIL where Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BOOTH_COMMISSION_Monthly_Detail where Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim MonthYear As DateTime = New DateTime(obj.Month_Year.Year, obj.Month_Year.Month, 1)

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Month_Year", clsCommon.GetPrintDate(MonthYear, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Is_Mobile_User", obj.Is_Mobile_User)
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark, True)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.BoothCalculation, "", "")
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", OMInsertOrUpdate.Update, "Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'", trans)
            End If

            clsBoothCommissionCalculationDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsBoothCommissionMonthlyDetail.SaveData(obj.Document_Code, obj.Arr_Monthly, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", "Document_Code", "TSPL_BOOTH_COMMISSION_CALCULATION_Detail", "Document_Code", "TSPL_BOOTH_COMMISSION_Monthly_Detail", "Document_Code", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBoothCommissionCalculationMaster
        Dim obj As clsBoothCommissionCalculationMaster = Nothing

        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select * from TSPL_BOOTH_COMMISSION_CALCULATION_Master  where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    strQry += " and  TSPL_BOOTH_COMMISSION_CALCULATION_Master.Document_Code = (select MIN(Document_Code) from  TSPL_BOOTH_COMMISSION_CALCULATION_Master where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and  TSPL_BOOTH_COMMISSION_CALCULATION_Master.Document_Code = (select Max(Document_Code) from  TSPL_BOOTH_COMMISSION_CALCULATION_Master where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and  TSPL_BOOTH_COMMISSION_CALCULATION_Master.Document_Code = (select Min(Document_Code) from  TSPL_BOOTH_COMMISSION_CALCULATION_Master where Document_Code>'" + clsCommon.myCstr(strCode) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and  TSPL_BOOTH_COMMISSION_CALCULATION_Master.Document_Code = (select Max(Document_Code) from  TSPL_BOOTH_COMMISSION_CALCULATION_Master where Document_Code<'" + clsCommon.myCstr(strCode) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and  TSPL_BOOTH_COMMISSION_CALCULATION_Master.Document_Code = '" + clsCommon.myCstr(strCode) + "'  " + Whrcls + " "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj = New clsBoothCommissionCalculationMaster()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))

                obj.Month_Year = clsCommon.myCDate(dt.Rows(0)("Month_Year"))
                obj.Is_Mobile_User = clsCommon.myCdbl(dt.Rows(0)("Is_Mobile_User"))
                obj.Remark = clsCommon.myCstr(dt.Rows(0)("Remark"))
                obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
                obj.Posted = IIf(clsCommon.myCDecimal(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If

                obj.Arr = clsBoothCommissionCalculationDetail.GetData(obj.Document_Code, trans)
                obj.Arr_Monthly = clsBoothCommissionMonthlyDetail.GetData(obj.Document_Code, trans)


            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsBoothCommissionCalculationMaster = clsBoothCommissionCalculationMaster.GetData(strDocNo, NavigatorType.Current, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_CALCULATION_MASTER", OMInsertOrUpdate.Update, "Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", "Document_Code", "TSPL_BOOTH_COMMISSION_CALCULATION_Detail", "Document_Code", "TSPL_BOOTH_COMMISSION_Monthly_Detail", "Document_Code", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsBoothCommissionCalculationMaster()
        Try

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", "Document_Code", "TSPL_BOOTH_COMMISSION_CALCULATION_Detail", "Document_Code", "TSPL_BOOTH_COMMISSION_Monthly_Detail", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BOOTH_COMMISSION_CALCULATION_Master", "Document_Code", "TSPL_BOOTH_COMMISSION_CALCULATION_Detail", "Document_Code", "TSPL_BOOTH_COMMISSION_Monthly_Detail", "Document_Code", trans)

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_BOOTH_COMMISSION_CALCULATION_Master where Document_Code = '" & strCode & "' and Posted=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            Dim qry As String


            qry = "DELETE FROM TSPL_BOOTH_COMMISSION_CALCULATION_Detail WHERE Document_Code='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BOOTH_COMMISSION_Monthly_Detail where Document_Code ='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BOOTH_COMMISSION_CALCULATION_Master where Document_Code ='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
End Class
Public Class clsBoothCommissionCalculationDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As String = Nothing
    Public Booth_Code As String = Nothing
    Public Booth_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Commission_Code As String = Nothing
    Public Commission_PK_ID As Integer = Nothing
    Public Min_Per_Day_Qty As Double = Nothing
    Public Commission_UOM As String = Nothing
    Public Total_Qty As Double = Nothing
    Public Commission_Rate As Double = Nothing
    Public Commission_Amt As Double = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBoothCommissionCalculationDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBoothCommissionCalculationDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Booth_Code", obj.Booth_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Commission_Code", obj.Commission_Code)
                    clsCommon.AddColumnsForChange(coll, "Commission_PK_ID", obj.Commission_PK_ID)
                    clsCommon.AddColumnsForChange(coll, "Min_Per_Day_Qty", obj.Min_Per_Day_Qty)
                    clsCommon.AddColumnsForChange(coll, "Commission_UOM", obj.Commission_UOM)
                    clsCommon.AddColumnsForChange(coll, "Total_Qty", obj.Total_Qty)
                    clsCommon.AddColumnsForChange(coll, "Commission_Rate", obj.Commission_Rate)
                    clsCommon.AddColumnsForChange(coll, "Commission_Amt", obj.Commission_Amt)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_CALCULATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsBoothCommissionCalculationDetail)
        Dim arr As List(Of clsBoothCommissionCalculationDetail) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select * from TSPL_BOOTH_COMMISSION_CALCULATION_DETAIL where Document_Code='" & strCode & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsBoothCommissionCalculationDetail)
                Dim objTr As clsBoothCommissionCalculationDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBoothCommissionCalculationDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Booth_Code = clsCommon.myCdbl(dr("Booth_Code"))
                    objTr.Booth_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & objTr.Booth_Code & "'", trans))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'", trans))
                    objTr.Commission_Code = clsCommon.myCstr(dr("Commission_Code"))
                    objTr.Commission_PK_ID = clsCommon.myCdbl(dr("Commission_PK_ID"))
                    objTr.Min_Per_Day_Qty = clsCommon.myCdbl(dr("Min_Per_Day_Qty"))
                    objTr.Commission_UOM = clsCommon.myCstr(dr("Commission_UOM"))
                    objTr.Commission_Rate = clsCommon.myCdbl(dr("Commission_Rate"))
                    objTr.Commission_Amt = clsCommon.myCdbl(dr("Commission_Amt"))
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function

End Class
Public Class clsBoothCommissionMonthlyDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Demand_Date As String = Nothing
    Public Booth_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Qty As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBoothCommissionMonthlyDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBoothCommissionMonthlyDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Demand_Date", clsCommon.GetPrintDate(obj.Demand_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Booth_Code", obj.Booth_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_Monthly_Detail", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsBoothCommissionMonthlyDetail)
        Dim arr As List(Of clsBoothCommissionMonthlyDetail) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select * from TSPL_BOOTH_COMMISSION_Monthly_Detail where Document_Code='" & strCode & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsBoothCommissionMonthlyDetail)
                Dim objTr As clsBoothCommissionMonthlyDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBoothCommissionMonthlyDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Demand_Date = clsCommon.myCDate(dr("Demand_Date"))
                    objTr.Booth_Code = clsCommon.myCstr(dr("Booth_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function

End Class

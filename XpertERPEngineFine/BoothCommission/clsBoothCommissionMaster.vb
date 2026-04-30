Imports System.Data.SqlClient

Public Class clsBoothCommissionMaster
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public From_Date As Date = Nothing
    Public To_Date As Date? = Nothing
    Public Is_Mobile_User As Integer = 0
    Public Inactive As Integer = 0
    Public InActive_date As DateTime = Nothing
    Public Commision_UOM As String = Nothing
    Public Min_Per_Day_Qty As Double = 0
    Public Remark As String = Nothing
    Public Comment As String = Nothing
    Public Posted As Integer = Nothing
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsBoothCommissionDetail)

#End Region
    Public Function SaveData(ByVal obj As clsBoothCommissionMaster, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsBoothCommissionMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BOOTH_COMMISSION_DETAIL where Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim FromDate As DateTime = New DateTime(obj.From_Date.Year, obj.From_Date.Month, 1)

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy"))
            If obj.To_Date.HasValue Then
                Dim ToDate As DateTime = New DateTime(obj.To_Date?.Year, obj.To_Date?.Month, DateTime.DaysInMonth(obj.To_Date?.Year, obj.To_Date?.Month))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "To_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Commision_UOM", obj.Commision_UOM)
            clsCommon.AddColumnsForChange(coll, "Is_Mobile_User", obj.Is_Mobile_User)
            clsCommon.AddColumnsForChange(coll, "Inactive", obj.Inactive)
            clsCommon.AddColumnsForChange(coll, "Min_Per_Day_Qty", obj.Min_Per_Day_Qty)
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark, True)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment, True)
            If obj.Inactive = 1 Then
                clsCommon.AddColumnsForChange(coll, "InActive_Date", clsCommon.GetPrintDate(obj.InActive_date, "dd/MMM/yyyy"), True)

            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.BoothCommission, "", "")
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_MASTER", OMInsertOrUpdate.Update, "Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'", trans)
            End If

            clsBoothCommissionDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BOOTH_COMMISSION_MASTER", "Document_Code", "TSPL_BOOTH_COMMISSION_Detail", "Document_Code", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBoothCommissionMaster
        Dim obj As clsBoothCommissionMaster = Nothing

        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_Code,Document_Date,From_Date,To_Date,Is_Mobile_User,Inactive,Commision_UOM,Min_Per_Day_Qty,Remark,Comment,Posted,Posted_Date from TSPL_BOOTH_COMMISSION_MASTER  where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    strQry += " and  TSPL_BOOTH_COMMISSION_MASTER.Document_Code = (select MIN(Document_Code) from  TSPL_BOOTH_COMMISSION_MASTER where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and  TSPL_BOOTH_COMMISSION_MASTER.Document_Code = (select Max(Document_Code) from  TSPL_BOOTH_COMMISSION_MASTER where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and  TSPL_BOOTH_COMMISSION_MASTER.Document_Code = (select Min(Document_Code) from  TSPL_BOOTH_COMMISSION_MASTER where Document_Code>'" + clsCommon.myCstr(strCode) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and  TSPL_BOOTH_COMMISSION_MASTER.Document_Code = (select Max(Document_Code) from  TSPL_BOOTH_COMMISSION_MASTER where Document_Code<'" + clsCommon.myCstr(strCode) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and  TSPL_BOOTH_COMMISSION_MASTER.Document_Code = '" + clsCommon.myCstr(strCode) + "'  " + Whrcls + " "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj = New clsBoothCommissionMaster()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")

                obj.From_Date = clsCommon.GetPrintDate(dt.Rows(0)("From_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("To_Date") IsNot DBNull.Value Then
                    obj.To_Date = clsCommon.GetPrintDate(dt.Rows(0)("To_Date"), "dd/MMM/yyyy")
                End If
                obj.Commision_UOM = clsCommon.myCstr(dt.Rows(0)("Commision_UOM"))
                obj.Is_Mobile_User = clsCommon.myCstr(dt.Rows(0)("Is_Mobile_User"))
                obj.Inactive = clsCommon.myCstr(dt.Rows(0)("Inactive"))
                obj.Min_Per_Day_Qty = clsCommon.myCdbl(dt.Rows(0)("Min_Per_Day_Qty"))
                obj.Remark = clsCommon.myCstr(dt.Rows(0)("Remark"))
                obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
                obj.Posted = IIf(clsCommon.myCDecimal(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If

                obj.Arr = clsBoothCommissionDetail.GetData(obj.Document_Code, trans)


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
            Dim obj As clsBoothCommissionMaster = clsBoothCommissionMaster.GetData(strDocNo, NavigatorType.Current, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_MASTER", OMInsertOrUpdate.Update, "Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_BOOTH_COMMISSION_MASTER", "Document_Code", trans)

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
        Dim obj As New clsDistributorCommission()
        Try

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_BOOTH_COMMISSION_MASTER", "Document_Code", "TSPL_BOOTH_COMMISSION_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_BOOTH_COMMISSION_MASTER", "Document_Code", "TSPL_BOOTH_COMMISSION_DETAIL", "Document_Code", trans)

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_BOOTH_COMMISSION_MASTER where Document_Code = '" & strCode & "' and Posted=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            Dim qry As String


            qry = "DELETE FROM TSPL_BOOTH_COMMISSION_Detail WHERE Document_Code='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BOOTH_COMMISSION_MASTER where Document_Code ='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
End Class
Public Class clsBoothCommissionDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Commission_Rate As Decimal = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBoothCommissionDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBoothCommissionDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Commission_Rate", obj.Commission_Rate)


                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_COMMISSION_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsBoothCommissionDetail)
        Dim arr As List(Of clsBoothCommissionDetail) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select Document_Code,Line_No,Item_Code,Commission_Rate from TSPL_BOOTH_COMMISSION_DETAIL where Document_Code='" & strCode & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsBoothCommissionDetail)
                Dim objTr As clsBoothCommissionDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBoothCommissionDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'", trans))
                    objTr.Commission_Rate = clsCommon.myCDecimal(dr("Commission_Rate"))

                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function

End Class

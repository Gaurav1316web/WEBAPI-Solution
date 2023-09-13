Imports System.Data.SqlClient
Public Class clsDistributorCommission
#Region "Variables"
    Public Doc_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Items As ArrayList = Nothing
    Public Applicable_Date As DateTime = Nothing
    Public Commision_UOM As String = Nothing
    Public IsPosted As Integer = 0
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsDistributorCommissionDetails)
#End Region
    Public Function SaveData(ByVal obj As clsDistributorCommission, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDistributorCommission, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Distributor_Commission_Detail where Doc_No='" + clsCommon.myCstr(obj.Doc_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Distributor_Commission_Items where Doc_No='" + clsCommon.myCstr(obj.Doc_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Applicable_Date", clsCommon.GetPrintDate(obj.Applicable_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Commision_UOM", obj.Commision_UOM)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.DistributorCommission, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Distributor_Commission_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Distributor_Commission_Head", OMInsertOrUpdate.Update, "Doc_No='" + clsCommon.myCstr(obj.Doc_No) + "'", trans)
            End If
            clsCommisionItems.SaveData(obj.Doc_No, obj.Items, trans)
            clsDistributorCommissionDetails.SaveData(obj.Doc_No, obj.Arr, False, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal Doc_No As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDistributorCommission
        Dim obj As clsDistributorCommission = Nothing

        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Doc_No,Document_Date,Applicable_Date,Commision_UOM,IsPosted,Posted_Date from TSPL_Distributor_Commission_Head  where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_Distributor_Commission_Head.Doc_No = (select MIN(Doc_No) from TSPL_Distributor_Commission_Head where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_Distributor_Commission_Head.Doc_No = (select Max(Doc_No) from TSPL_Distributor_Commission_Head where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_Distributor_Commission_Head.Doc_No = (select Min(Doc_No) from TSPL_Distributor_Commission_Head where Doc_No>'" + clsCommon.myCstr(Doc_No) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_Distributor_Commission_Head.Doc_No = (select Max(Doc_No) from TSPL_Distributor_Commission_Head where Doc_No<'" + clsCommon.myCstr(Doc_No) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_Distributor_Commission_Head.Doc_No = '" + clsCommon.myCstr(Doc_No) + "'  " + Whrcls + " "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj = New clsDistributorCommission()
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")

                obj.Applicable_Date = clsCommon.GetPrintDate(dt.Rows(0)("Applicable_Date"), "dd/MMM/yyyy")
                obj.Commision_UOM = clsCommon.myCstr(dt.Rows(0)("Commision_UOM"))
                obj.IsPosted = IIf(clsCommon.myCDecimal(dt.Rows(0)("IsPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                strQry = "select Item_code from TSPL_Distributor_Commission_Items where Doc_No='" & obj.Doc_No & "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(strQry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Items = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        obj.Items.Add(clsCommon.myCstr(dr("Item_Code")))
                    Next
                End If
                obj.Arr = clsDistributorCommissionDetails.GetData(obj.Doc_No, trans)


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
            Dim obj As clsDistributorCommission = clsDistributorCommission.GetData(strDocNo, NavigatorType.Current, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.IsPosted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "IsPosted", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Distributor_Commission_Head", OMInsertOrUpdate.Update, "Doc_No='" + clsCommon.myCstr(obj.Doc_No) + "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function





End Class
Public Class clsDistributorCommissionDetails

#Region "Variables"
    Public Doc_No As String = Nothing
    Public Route_Code As String = Nothing
    Public Distributor_Code As String = Nothing
    Public Rate As Double = 0
#End Region

    Public Shared Function SaveData(ByVal Doc_No As String, ByVal Arr As List(Of clsDistributorCommissionDetails), ByVal IsUpdatedFromCorrection As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsDistributorCommissionDetails In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                    clsCommon.AddColumnsForChange(coll, "Distributor_Code", obj.Distributor_Code)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Distributor_Commission_Detail", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsDistributorCommissionDetails)
        Dim arr As List(Of clsDistributorCommissionDetails) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select Doc_No,Route_Code,Distributor_Code,Rate from TSPL_Distributor_Commission_Detail where Doc_No='" & strDocNo & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsDistributorCommissionDetails)
                Dim objTr As clsDistributorCommissionDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDistributorCommissionDetails
                    objTr.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    objTr.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    objTr.Distributor_Code = clsCommon.myCstr(dr("Distributor_Code"))
                    objTr.Rate = clsCommon.myCDecimal(dr("Rate"))
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function


End Class
Public Class clsCommisionItems
    Public Doc_No As String = Nothing
    Public Item_Code As String = Nothing

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each strMCC As String In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", strMCC)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Distributor_Commission_Items", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

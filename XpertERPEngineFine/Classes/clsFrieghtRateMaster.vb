Imports System.Data.SqlClient

Public Class clsFrieghtRateMaster
#Region "Variables"
    Public PK_ID As Integer = 0
    Public From_Date As DateTime = Nothing
    Public To_Date As DateTime = Nothing
    Public Description As String = ""
    Public Inactive As Integer = 0
    Public Location_Code As String = ""
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr_FrieghtDetail As List(Of clsFrieghtRateDetail) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsFrieghtRateMaster, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsFrieghtRateMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "delete from TSPL_DCS_FOR_SALE_Frieght_Detail where REF_PK_ID='" + obj.PK_ID + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PK_ID", obj.PK_ID)
            clsCommon.AddColumnsForChange(coll, "From_Date", obj.From_Date)
            clsCommon.AddColumnsForChange(coll, "To_Date", obj.To_Date)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Inactive", obj.Inactive)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "PK_ID", obj.PK_ID)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FOR_SALE_Frieght", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FOR_SALE_Frieght", OMInsertOrUpdate.Update, "TSPL_DCS_FOR_SALE_Frieght.PK_ID='" + obj.PK_ID + "'", trans)
            End If
            clsFrieghtRateDetail.SaveData(obj.PK_ID, obj.Arr_FrieghtDetail, False, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal PK_ID As Integer, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strDetailWhrlCls As String) As List(Of clsFrieghtRateMaster)
        Dim Arr As List(Of clsFrieghtRateMaster) = Nothing
        Try
            Dim obj As clsFrieghtRateMaster = Nothing
            Dim obj_Trip As New clsFrieghtRateDetail()
            Arr = New List(Of clsFrieghtRateMaster)
            Dim strQry As String = ""
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_DCS_FOR_SALE_Frieght.PK_ID = (select MIN(PK_ID) from TSPL_DCS_FOR_SALE_Frieght where 1=1  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_DCS_FOR_SALE_Frieght.PK_ID = (select Max(PK_ID) from TSPL_DCS_FOR_SALE_Frieght where 1=1  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_DCS_FOR_SALE_Frieght.PK_ID = (select Min(PK_ID) from TSPL_DCS_FOR_SALE_Frieght where PK_ID>'" + PK_ID + "'  )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_DCS_FOR_SALE_Frieght.PK_ID = (select Max(PK_ID) from TSPL_DCS_FOR_SALE_Frieght where PK_ID<'" + PK_ID + "'  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_DCS_FOR_SALE_Frieght.PK_ID = '" + PK_ID + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsFrieghtRateMaster()
                    obj.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                    obj.From_Date = clsCommon.GetPrintDate(dr("From_Date"), "dd/MMM/yyyy")
                    obj.To_Date = clsCommon.GetPrintDate(dr("To_Date"), "dd/MMM/yyyy")
                    obj.Inactive = clsCommon.myCstr(dr("Inactive"))
                    obj.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                    obj.Description = clsCommon.myCDecimal(dr("Description"))

                    obj.Arr_FrieghtDetail = clsFrieghtRateDetail.GetData(obj.PK_ID)
                    Arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Arr
    End Function
    'Public Shared Function DeleteData(ByVal strCode As String) As Boolean
    '    If (clsCommon.myLen(strCode) <= 0) Then
    '        Throw New Exception("Document No not found to Delete")
    '    End If

    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MILK_COLLECTION_MCC.MCC_Code from TSPL_MILK_COLLECTION_MCC where Document_No='" + strCode + "'", trans)
    '        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        '    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
    '        'End If

    '        Dim obj As clsFrieghtRateMaster = clsFrieghtRateMaster.GetData(strCode, NavigatorType.Current, trans)
    '        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
    '            Throw New Exception("Document No: " + strCode + " not found to Delete")
    '        End If

    '        If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
    '            Throw New Exception("Already Posted on :" + obj.Posting_Date)
    '        End If
    '        HistoryUpdate(strCode, trans)
    '        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No='" + strCode + "'", trans)
    '        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_MCC where Document_No='" + strCode + "'", trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

End Class
Public Class clsFrieghtRateDetail
#Region "Veriables"
    Public REF_PK_ID As Integer = 0
    Public PK_ID As Integer = 0
    Public Customer_Code As String = ""
    Public Zone_Code As String = ""
    Public UOM_Code As String = ""
    Public Frieght_Rate As String = ""
#End Region
    Public Shared Function SaveData(ByVal REF_PK_ID As Integer, ByVal Arr As List(Of clsFrieghtRateDetail), ByVal IsUpdatedFromCorrection As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsFrieghtRateDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "REF_PK_ID", REF_PK_ID)
                    'clsCommon.AddColumnsForChange(coll, "PK_ID", obj.PK_ID)
                    clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                    clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM_Code", obj.UOM_Code)
                    clsCommon.AddColumnsForChange(coll, "Frieght_Rate", obj.Frieght_Rate)
                    If obj.PK_ID > 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FOR_SALE_Frieght_Detail", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(obj.PK_ID) + "' ", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FOR_SALE_Frieght_Detail", OMInsertOrUpdate.Insert, "", trans)
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

        Return True
    End Function


    Public Shared Function GetData(ByVal PK_ID As Integer) As List(Of clsFrieghtRateDetail)
        Dim obj As clsFrieghtRateMaster = New clsFrieghtRateMaster()
        Try
            Dim dt As DataTable
            Dim strQry As String = ""

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr_FrieghtDetail = New List(Of clsFrieghtRateDetail)
                Dim obj_FRDetail As clsFrieghtRateDetail
                For Each dr As DataRow In dt.Rows
                    obj_FRDetail = New clsFrieghtRateDetail

                    obj_FRDetail.REF_PK_ID = clsCommon.myCDecimal(dr("REF_PK_ID"))
                    obj_FRDetail.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                    obj_FRDetail.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                    obj_FRDetail.Zone_Code = clsCommon.myCstr(dr("Zone_Code"))
                    obj_FRDetail.UOM_Code = clsCommon.myCstr(dr("UOM_Code"))
                    obj_FRDetail.Frieght_Rate = clsCommon.myCDecimal(dr("Frieght_Rate"))

                    obj.Arr_FrieghtDetail.Add(obj_FRDetail)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj.Arr_FrieghtDetail
    End Function
    Public Shared Function DeleteData(ByVal PKID As Integer) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = ""
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(qry) > 0 Then
                Throw New Exception("DCS Truck sheet entered [" + qry + "].Cant Delete it")
            End If

            qry = "select Document_No from  TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No in ( select Document_No from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id = " + clsCommon.myCstr(PKID) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid PK ID")
            End If
            qry = "Delete from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id=" + clsCommon.myCstr(PKID) + ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If dt.Rows.Count = 1 Then
                qry = "Delete from TSPL_MILK_COLLECTION_MCC where Document_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


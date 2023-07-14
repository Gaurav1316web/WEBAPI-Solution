Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsDCSforSale
#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public Uploader_No As String = Nothing
    'Public Location As String = Nothing
    Public Zone As String = Nothing
    Public Location As String = Nothing
    Public Customer As String = Nothing
    Public Location_Name As String = Nothing
    Public Customer_Name As String = Nothing
    Public Zone_Name As String = Nothing

#End Region

    Public Function SaveData(ByVal obj As ClsDCSforSale, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As ClsDCSforSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            'Dim objCommonVar As Object = New ClsDCSforSale()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Zone", obj.Zone)
            clsCommon.AddColumnsForChange(coll, "Uploader_No", obj.Uploader_No)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Customer", obj.Customer)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.DCSSale, "", obj.Location)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FOR_SALE", OMInsertOrUpdate.Insert, "", trans)
            Else
                '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_DCS_FOR_SALE", "Code", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FOR_SALE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsDCSforSale
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsDCSforSale
        Dim obj As ClsDCSforSale = Nothing
        Dim qry As String = "SELECT TSPL_DCS_FOR_SALE.*,TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Name,TSPL_CUSTOMER_LOCATION_MAPPING.Location_Name,TSPL_ZONE_MASTER.Description as Zone_Name FROM TSPL_DCS_FOR_SALE
left join TSPL_CUSTOMER_LOCATION_MAPPING on TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code=TSPL_DCS_FOR_SALE.Customer
left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_DCS_FOR_SALE.Zone where  2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls += " AND Location in (" + objCommonVar.strCurrUserLocations + ")"

        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DCS_FOR_SALE.Code=(select MIN(Code) from TSPL_DCS_FOR_SALE where 1=1 " + whrCls + " )"
            Case NavigatorType.Last
                qry += " and TSPL_DCS_FOR_SALE.Code=(select Max(Code) from TSPL_DCS_FOR_SALE where 1=1  " + whrCls + " )"
            Case NavigatorType.Next
                qry += " and TSPL_DCS_FOR_SALE.Code=(select Min(Code) from TSPL_DCS_FOR_SALE where Code > '" + strCode + "'  " + whrCls + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_DCS_FOR_SALE.Code=(select Max(Code) from TSPL_DCS_FOR_SALE where Code < '" + strCode + "' " + whrCls + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DCS_FOR_SALE.Code='" + strCode + "'  " + whrCls + ""
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDCSforSale()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.Uploader_No = clsCommon.myCstr(dt.Rows(0)("Uploader_No"))
            obj.Zone = clsCommon.myCstr(dt.Rows(0)("Zone"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.Customer = clsCommon.myCstr(dt.Rows(0)("Customer"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Zone_Name = clsCommon.myCstr(dt.Rows(0)("Zone_Name"))
        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("DCS Sale not found to Delete")
        End If
        Dim obj As ClsDCSforSale = ClsDCSforSale.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try

                Dim qry As String = "delete from TSPL_DCS_FOR_SALE where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

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

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_DCS_FOR_SALE.CODE from TSPL_DCS_FOR_SALE where TSPL_DCS_FOR_SALE.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function GetFinder(ByVal curcode As String, ByVal CustCode As String, ByVal isButtonClicked As Boolean) As String
        'Dim obj As ClsDCSforSale = Nothing
        'Dim qry As String = "select Code,Name,Uploader_No,Zone from TSPL_DCS_FOR_SALE"
        'strCode = clsCommon.ShowSelectForm("DCSFSFnd", qry, "Code", "", strCode, "Code", isButtonClicked)
        'If clsCommon.myLen(strCode) > 0 Then
        '    obj = ClsDCSforSale.GetData(strCode, NavigatorType.Current)
        'End If
        'Return obj
        Dim WhrCls As String = "2=2"
        If clsCommon.myLen(CustCode) > 0 Then
            WhrCls += " and Customer in('" + clsCommon.myCstr(CustCode) + "')"

        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim str As String = ""
        Dim qry As String = "select Code as [Code],Name as [Name],Zone as [Zone],Uploader_No as [Uploader No],Location as [Location],Customer as [Customer],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date] from TSPL_DCS_FOR_SALE  "
        str = clsCommon.ShowSelectForm("DCSFSFnd", qry, "Code", WhrCls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal curcode As String, ByVal CustCode As String, ByVal isButtonClicked As Boolean) As ClsDCSforSale
        Dim obj As ClsDCSforSale = Nothing
        Dim strCode As String = GetFinder(curcode, CustCode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

End Class

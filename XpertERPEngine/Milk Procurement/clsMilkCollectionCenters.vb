Imports System.Data.SqlClient
Imports common
Public Class clsMilkCollectionCenters
#Region "variables"
    Public COLLECTION_CENTER_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public LEVEL_CODE As String = Nothing
    Public EMAIL_ADDRESS As String = Nothing
    Public City As String = Nothing
    Public State_Code As String = Nothing
    Public State_Name As String = Nothing
    Public Zip As String = Nothing
    Public Country As String = Nothing
    Public Telphone As String = Nothing
    Public Phone1 As String = Nothing
    Public Phone2 As String = Nothing
    Public ADDRESS1 As String = Nothing
    Public ADDRESS2 As String = Nothing
    Public ADDRESS3 As String = Nothing
    Public ADDRESS4 As String = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String

#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkCollectionCenters, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim qry As String = ""

        Try
            If isNewEntry Then
                If clsCommon.myLen(obj.COLLECTION_CENTER_CODE) <= 0 Then
                    obj.COLLECTION_CENTER_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.CollectionCenter, "", "")
                Else
                    obj.COLLECTION_CENTER_CODE = obj.COLLECTION_CENTER_CODE
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "LEVEL_CODE", obj.LEVEL_CODE, True)
            clsCommon.AddColumnsForChange(coll, "EMAIL_ADDRESS", obj.EMAIL_ADDRESS)
            clsCommon.AddColumnsForChange(coll, "City", obj.City)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.State_Code)
            clsCommon.AddColumnsForChange(coll, "State_Name", obj.State_Name)
            clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
            clsCommon.AddColumnsForChange(coll, "Zip", obj.Zip)
            clsCommon.AddColumnsForChange(coll, "Telphone", obj.Telphone)
            clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
            clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
            clsCommon.AddColumnsForChange(coll, "ADDRESS1", obj.ADDRESS1)
            clsCommon.AddColumnsForChange(coll, "ADDRESS2", obj.ADDRESS2)
            clsCommon.AddColumnsForChange(coll, "ADDRESS3", obj.ADDRESS3)
            clsCommon.AddColumnsForChange(coll, "ADDRESS4", obj.ADDRESS4)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "COLLECTION_CENTER_CODE", obj.COLLECTION_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MilkCollectionCenter", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MilkCollectionCenter", OMInsertOrUpdate.Update, "TSPL_MilkCollectionCenter.COLLECTION_CENTER_CODE='" + obj.COLLECTION_CENTER_CODE + "'", trans)
            End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkCollectionCenters
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionCenters
        Dim obj As clsMilkCollectionCenters = Nothing
        Dim Arr As List(Of clsMilkCollectionCenters) = Nothing
        Dim qry As String = "select * from TSPL_MilkCollectionCenter where 2=2 "
        Dim whrclas As String = " and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MilkCollectionCenter.COLLECTION_CENTER_CODE = (select MIN(COLLECTION_CENTER_CODE) from TSPL_MilkCollectionCenter WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_MilkCollectionCenter.COLLECTION_CENTER_CODE = (select Max(COLLECTION_CENTER_CODE) from TSPL_MilkCollectionCenter WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MilkCollectionCenter.COLLECTION_CENTER_CODE = (select top 1 COLLECTION_CENTER_CODE from TSPL_MilkCollectionCenter WHERE 1=1 " + whrclas + " and COLLECTION_CENTER_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_MilkCollectionCenter.COLLECTION_CENTER_CODE = (select Min(COLLECTION_CENTER_CODE) from TSPL_MilkCollectionCenter where COLLECTION_CENTER_CODE>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MilkCollectionCenter.COLLECTION_CENTER_CODE = (select Max(COLLECTION_CENTER_CODE) from TSPL_MilkCollectionCenter where COLLECTION_CENTER_CODE<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionCenters()
            obj.COLLECTION_CENTER_CODE = clsCommon.myCstr(dt.Rows(0)("COLLECTION_CENTER_CODE"))
            obj.LEVEL_CODE = clsCommon.myCstr(dt.Rows(0)("LEVEL_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.EMAIL_ADDRESS = clsCommon.myCstr(dt.Rows(0)("EMAIL_ADDRESS"))
            obj.City = clsCommon.myCstr(dt.Rows(0)("City"))
            obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.State_Name = clsCommon.myCstr(dt.Rows(0)("State_Name"))
            obj.Zip = clsCommon.myCstr(dt.Rows(0)("EMAIL_ADDRESS"))
            obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
            obj.Telphone = clsCommon.myCstr(dt.Rows(0)("Telphone"))
            obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
            obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
            obj.ADDRESS1 = clsCommon.myCstr(dt.Rows(0)("ADDRESS1"))
            obj.ADDRESS2 = clsCommon.myCstr(dt.Rows(0)("ADDRESS2"))
            obj.ADDRESS3 = clsCommon.myCstr(dt.Rows(0)("ADDRESS3"))
            obj.ADDRESS4 = clsCommon.myCstr(dt.Rows(0)("ADDRESS4"))

        End If
        Return obj
    End Function
End Class

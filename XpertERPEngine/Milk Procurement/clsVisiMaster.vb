Imports common
Imports System.Data.SqlClient

Public Class clsVisiMaster
#Region "variables"
    Public Visi_Id As String = Nothing
    Public VisiMake As String = Nothing
    Public Customer_Id As String = Nothing
    Public Visi_Installation_date As Date?
    Public Asset_No As String = Nothing
    Public Visi_Size As String = Nothing
    Public Visi_Chasis_No As String = Nothing
    Public Model_No As String = Nothing
    Public Type As String = Nothing
    Public Location As String = Nothing
    Public Route As String = Nothing
    Public Pull_Out_Date As Date?
    Public Status As String = Nothing
    Public Serial_No As String = Nothing
    Public Tag_No As String = Nothing

    Public arrVisi As New List(Of clsVisiMaster)

#End Region

    Public Shared Function SaveData(ByVal ArrVisi As List(Of clsVisiMaster), ByVal ArrDB As List(Of String), ByVal IsNewEntry As Boolean) As Boolean
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            SaveData(ArrVisi, ArrDB, trans, IsNewEntry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function SaveData(ByVal ArrVisi As List(Of clsVisiMaster), ByVal ArrDB As List(Of String), ByVal trans As SqlTransaction, ByVal IsNewEntry As Boolean) As Boolean
        Try
            If (ArrVisi IsNot Nothing AndAlso ArrVisi.Count > 0) Then
                For Each obj As clsVisiMaster In ArrVisi
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "VisiMake", obj.VisiMake)
                    If clsCommon.myLen(obj.Asset_No) <= 0 Then
                        obj.Asset_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Asset_Code ) from (Select Asset_Code from TSPL_ACQUISITION_DETAIL Union Select Asset_No from TSPL_VISI_MASTER) XXX ", trans))
                        If clsCommon.myLen(obj.Asset_No) <= 0 Then
                            obj.Asset_No = "AS00000001"
                        Else
                            obj.Asset_No = clsCommon.incval(obj.Asset_No)
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Asset_No", obj.Asset_No)
                    clsCommon.AddColumnsForChange(coll, "Visi_Size", obj.Visi_Size)
                    clsCommon.AddColumnsForChange(coll, "Model_No", obj.Model_No)
                    clsCommon.AddColumnsForChange(coll, "Customer_Id", obj.Customer_Id)
                    clsCommon.AddColumnsForChange(coll, "Customer_Name", clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Id + "'", trans)))
                    'clsCommon.AddColumnsForChange(coll, "Visi_Installation_date", obj.Visi_Installation_date)
                    'clsCommon.AddColumnsForChange(coll, "Visi_Chasis_No", obj.Visi_Chasis_No)
                    'clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                    'clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    'clsCommon.AddColumnsForChange(coll, "Route", obj.Route)
                    'clsCommon.AddColumnsForChange(coll, "Pull_Out_Date", obj.Pull_Out_Date)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No)
                    clsCommon.AddColumnsForChange(coll, "Tag_No", obj.Tag_No)
                    If IsNewEntry Then       '----New Entry----
                        clsCommon.AddColumnsForChange(coll, "Visi_Id", obj.Visi_Id)
                        clsCommon.AddColumnsForChange(coll, "Type", "N")
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_VISI_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else                                                                                '-----Update Entry-----
                        clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_id = '" + obj.Visi_Id + "'", trans)
                    End If
                Next
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strVisiId As String) As Boolean
        Try
            If (clsCommon.myLen(strVisiId) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String = "delete from TSPL_VISI_MASTER where Visi_Id='" + strVisiId + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    '-------------This Finction Retreives Data From [Visi Master]
    Public Shared Function GetData(ByVal strVisiId As String, ByVal NavType As NavigatorType) As clsVisiMaster
        Try
            Dim obj As New clsVisiMaster()
            Dim qry As String = "Select TSPL_VISI_MASTER.Visi_Id, TSPL_VISI_MASTER.VisiMake, TSPL_VISI_MASTER.Visi_Installation_date, TSPL_VISI_MASTER.Customer_Id, TSPL_VISI_MASTER.Asset_No, TSPL_VISI_MASTER.Visi_Size, TSPL_VISI_MASTER.Visi_Chasis_No, TSPL_VISI_MASTER.Model_No, TSPL_VISI_MASTER.Type, TSPL_VISI_MASTER.Location, TSPL_VISI_MASTER.Route, TSPL_VISI_MASTER.Pull_Out_Date,TSPL_VISI_MASTER.Serial_No,TSPL_VISI_MASTER.Tag_No  from TSPL_VISI_MASTER Where 1=1"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_VISI_MASTER.Visi_Id = (select MIN(TSPL_VISI_MASTER.Visi_Id) from TSPL_VISI_MASTER )"
                Case NavigatorType.Last
                    qry += " and TSPL_VISI_MASTER.Visi_Id = (select MAX(TSPL_VISI_MASTER.Visi_Id) from TSPL_VISI_MASTER )"
                Case NavigatorType.Next
                    qry += " and TSPL_VISI_MASTER.Visi_Id = (select MIN(TSPL_VISI_MASTER.Visi_Id) from TSPL_VISI_MASTER where TSPL_VISI_MASTER.Visi_Id > '" + strVisiId + "' )"
                Case NavigatorType.Previous
                    qry += " and TSPL_VISI_MASTER.Visi_Id = (select MAX(TSPL_VISI_MASTER.Visi_Id) from TSPL_VISI_MASTER where TSPL_VISI_MASTER.Visi_Id < '" + strVisiId + "')"
                Case NavigatorType.Current
                    qry += " and TSPL_VISI_MASTER.Visi_Id = '" + strVisiId + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            For Each dr As DataRow In dt.Rows
                obj.Visi_Id = clsCommon.myCstr(dr("Visi_Id"))
                obj.VisiMake = clsCommon.myCstr(dr("VisiMake"))
                obj.Customer_Id = clsCommon.myCstr(dr("Customer_Id"))
                If clsCommon.myLen(dr("Visi_Installation_date")) > 0 Then
                    obj.Visi_Installation_date = dr("Visi_Installation_date")
                Else
                    obj.Visi_Installation_date = Nothing
                End If
                obj.Asset_No = clsCommon.myCstr(dr("Asset_No"))
                obj.Visi_Size = clsCommon.myCstr(dr("Visi_Size"))
                obj.Visi_Chasis_No = clsCommon.myCstr(dr("Visi_Chasis_No"))
                obj.Model_No = clsCommon.myCstr(dr("Model_No"))
                obj.Serial_No = clsCommon.myCstr(dr("Serial_No"))
                obj.Tag_No = clsCommon.myCstr(dr("Tag_No"))
                'obj.Type = clsCommon.myCstr(dr("Type"))
                'obj.Location = clsCommon.myCstr(dr("Location"))
                'obj.Route = clsCommon.myCstr(dr("Route"))
                'obj.Pull_Out_Date = clsCommon.myCstr(dr("Pull_Out_Date"))
            Next
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''----This Finction Retreives Data For Screen [Visi Install/Pullout]
    Public Shared Function GetDataForVisiInstallPullout(ByVal strCustCode As String, ByVal NavType As NavigatorType) As DataTable
        Try
            Dim obj As New clsVisiMaster()
            Dim qry As String = "Select TSPL_CUSTOMER_MASTER.Cust_Code, Customer_Name, Route_No, Route_Desc from TSPL_CUSTOMER_MASTER Where 1=1 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = (select MIN(TSPL_CUSTOMER_MASTER.Cust_Code) from TSPL_CUSTOMER_MASTER )"
                Case NavigatorType.Last
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = (select MAX(TSPL_VISI_MASTER.Cust_Code) from TSPL_CUSTOMER_MASTER )"
                Case NavigatorType.Next
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = (select MIN(TSPL_CUSTOMER_MASTER.Cust_Code) from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code > '" + strCustCode + "' )"
                Case NavigatorType.Previous
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = (select MAX(TSPL_CUSTOMER_MASTER.Cust_Code) from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code < '" + strCustCode + "')"
                Case NavigatorType.Current
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = '" + strCustCode + "'"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsVisiInstallPullout
#Region "variables"
    Public Visi_Id As String = Nothing
    Public VisiMake As String = Nothing
    Public Customer_Id As String = Nothing
    Public Location As String = Nothing
    Public Route As String = Nothing
    Public Trans_Type As String = "Installed"
    Public Trans_Date As Date
    Public Type As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal ArrVisi As List(Of clsVisiInstallPullout), ByVal ArrDB As List(Of String)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (ArrVisi IsNot Nothing AndAlso ArrVisi.Count > 0) Then
                For Each obj As clsVisiInstallPullout In ArrVisi
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Customer_Id", obj.Customer_Id)
                    clsCommon.AddColumnsForChange(coll, "Customer_Name", clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Id + "'", trans)))
                    clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
                    clsCommon.AddColumnsForChange(coll, "Visi_Type", obj.Type)
                    clsCommon.AddColumnsForChange(coll, "Trans_Date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    clsCommon.AddColumnsForChange(coll, "Route", obj.Route)
                    clsCommon.AddColumnsForChange(coll, "VisiMake", obj.VisiMake)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim whrcls As String = " Visi_Id='" + obj.Visi_Id + "' AND Customer_Id='" + obj.Customer_Id + "' AND Trans_Date='" + clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy") + "' AND Trans_Type='" + obj.Trans_Type + "'"
                    Dim qry As String = "Select COunt(*) from TSPL_VISI_INSTALL_PULLOUT WHERE " + whrcls + ""
                    If clsDBFuncationality.getSingleValue(qry, trans) <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Visi_Id", obj.Visi_Id)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                        clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_VISI_INSTALL_PULLOUT", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_VISI_INSTALL_PULLOUT", OMInsertOrUpdate.Update, whrcls, trans)
                    End If

                    '-------------------------Updates Data In Visi Master-------------------------------------------
                    Dim coll1 As New Hashtable()
                    If clsCommon.CompairString(obj.Type, "New") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll1, "Type", "N")
                    Else
                        clsCommon.AddColumnsForChange(coll1, "Type", "R")
                    End If
                    clsCommon.AddColumnsForChange(coll1, "Location", obj.Location)
                    clsCommon.AddColumnsForChange(coll1, "Route", obj.Route)
                    If Not clsCommon.CompairString(obj.Trans_Type, "Installed") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll1, "Customer_Id", Nothing)
                        clsCommon.AddColumnsForChange(coll1, "Customer_Name", Nothing)
                        clsCommon.AddColumnsForChange(coll1, "Visi_Installation_date", Nothing, True)
                        clsCommon.AddColumnsForChange(coll1, "Pull_Out_Date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy"))
                    Else
                        clsCommon.AddColumnsForChange(coll1, "Customer_Id", obj.Customer_Id)
                        clsCommon.AddColumnsForChange(coll1, "Customer_Name", clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Id + "'", trans)))
                        clsCommon.AddColumnsForChange(coll1, "Visi_Installation_date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "Pull_Out_Date", Nothing, True)
                    End If

                    clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll1, ArrDB, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_id = '" + obj.Visi_Id + "'", trans)
                    '------------------------------------------------------------------------------------------------
                Next
                trans.Commit()

            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsAssetIssueReturn
#Region "variables"
    Public Asset_Id As String = Nothing
    Public Trans_Date As DateTime
    Public Trans_Type As String = Nothing
    Public From_Entity As String = Nothing
    Public To_Entity As String = Nothing
    Public CostCenter_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal ArrVisi As List(Of clsAssetIssueReturn), ByVal ArrDB As List(Of String)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (ArrVisi IsNot Nothing AndAlso ArrVisi.Count > 0) Then
                For Each obj As clsAssetIssueReturn In ArrVisi
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Asset_Id", obj.Asset_Id)
                    clsCommon.AddColumnsForChange(coll, "Trans_Date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
                    clsCommon.AddColumnsForChange(coll, "From_Entity", obj.From_Entity)
                    clsCommon.AddColumnsForChange(coll, "To_Entity", obj.To_Entity)
                    clsCommon.AddColumnsForChange(coll, "CostCenter_Code", obj.CostCenter_Code)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_ASSET_ISSUE_RETURN", OMInsertOrUpdate.Insert, "", trans)

                    '-------------------------Updates Data In Asset Acquisition-------------------------------------------
                    Dim coll1 As New Hashtable()
                    'Select is_Issued, Issue_Return_Date, Issue_Return_Entity
                    If Not clsCommon.CompairString(obj.Trans_Type, "Issue") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll1, "is_Issued", "N")
                    Else
                        clsCommon.AddColumnsForChange(coll1, "is_Issued", "Y")
                    End If

                    clsCommon.AddColumnsForChange(coll1, "Issue_Return_Date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll1, "From_Entity", obj.From_Entity)
                    clsCommon.AddColumnsForChange(coll1, "To_Entity", obj.To_Entity)

                    clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll1, ArrDB, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Update, "TSPL_ACQUISITION_DETAIL.Asset_Code = '" + obj.Asset_Id + "'", trans)
                    '------------------------------------------------------------------------------------------------
                Next
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
End Class




'=============BM00000003002,Updated By Rohit june 30 , 2014 on 4:25 PM.==========================
'==============BM00000003063,Updated By Rohit===========
Imports common
Imports System.Data.SqlClient

Public Class clsAssetInstallPullOut
#Region "variables"
    Public Asset_Id As String = Nothing
    Public Item_Id As String = Nothing
    Public PulloutCustomer_Id As String = Nothing
    Public InstallCustomer_Id As String = Nothing
    Public Trans_Type As String = "Installed"
    Public Trans_Date As Date
    Public installDate As Date = Nothing
    Public pulloutDate As Date = Nothing
    Public Type As String = Nothing
    Public ClosingDate As Date = Nothing
    Public Refund_Amount As Double = Nothing
    Public Cheque_No As String = Nothing
    Public Cheque_date As String = Nothing
    Public FOC As String = Nothing
    Public Agreement_No As String = Nothing
    Public Agreement_date As Date = Nothing

    Public Cheque_No_sec As String = Nothing
    Public Cheque_date_sec As Date = Nothing
    Public FOC_sec As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal ArrAsset As List(Of clsAssetInstallPullOut), ByVal ArrDB As List(Of String)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If Save_Data(ArrAsset, trans) Then
            trans.Commit()
        Else
            trans.Rollback()
        End If
        Return True
    End Function
    Public Shared Function Save_Data(ByVal ArrAsset As List(Of clsAssetInstallPullOut), Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            If (ArrAsset IsNot Nothing AndAlso ArrAsset.Count > 0) Then
                For Each obj As clsAssetInstallPullOut In ArrAsset
                    Dim coll As New Hashtable()
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Installed") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Install_Customer_Id", obj.InstallCustomer_Id)
                        clsCommon.AddColumnsForChange(coll, "Asset_Installation_Date", clsCommon.GetPrintDate(obj.installDate, "dd/MMM/yyyy"))

                        clsCommon.AddColumnsForChange(coll, "agreement_no", obj.Agreement_No)
                        clsCommon.AddColumnsForChange(coll, "agreement_date", clsCommon.GetPrintDate(obj.Agreement_date, "dd/MMM/yyyy"))
                        ''richa agarwal 27/11/2014
                        'clsCommon.AddColumnsForChange(coll, "FOC_sec", obj.FOC)
                        clsCommon.AddColumnsForChange(coll, "FOC_sec", obj.FOC_sec)
                        ''-------------------
                        clsCommon.AddColumnsForChange(coll, "Item_code", IIf(obj.Item_Id = "", obj.Asset_Id, obj.Item_Id))
                        ''richa agarwal 27/11/2014
                        If clsCommon.myCstr(obj.Cheque_No_sec) <> "" Then
                            clsCommon.AddColumnsForChange(coll, "cheque_no_sec", obj.Cheque_No_sec)
                        End If
                        If clsCommon.myCstr(obj.Cheque_date_sec) <> "" Then
                            clsCommon.AddColumnsForChange(coll, "cheque_date_sec", clsCommon.GetPrintDate(obj.Cheque_date_sec, "dd/MMM/yyyy"))
                        End If
                        'If clsCommon.myCstr(obj.Cheque_No) <> "" Then
                        '    clsCommon.AddColumnsForChange(coll, "cheque_no_sec", obj.Cheque_No)
                        'End If
                        'If clsCommon.myCstr(obj.Cheque_date) <> "" Then
                        '    clsCommon.AddColumnsForChange(coll, "cheque_date_sec", clsCommon.GetPrintDate(obj.Cheque_date, "dd/MMM/yyyy"))
                        'End If
                        ''------------------
                        clsCommon.AddColumnsForChange(coll, "sec_Amount", obj.Refund_Amount)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "PulledOut") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Pullout_Customer_Id", obj.PulloutCustomer_Id)
                        clsCommon.AddColumnsForChange(coll, "Asset_Pullout_Date", clsCommon.GetPrintDate(obj.pulloutDate, "dd/MMM/yyyy"))

                        clsCommon.AddColumnsForChange(coll, "Agreement_Closing_Date", clsCommon.GetPrintDate(obj.ClosingDate, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "refund_amount", obj.Refund_Amount)
                        clsCommon.AddColumnsForChange(coll, "FOC", obj.FOC)
                        clsCommon.AddColumnsForChange(coll, "cheque_no", obj.Cheque_No)
                        clsCommon.AddColumnsForChange(coll, "Item_code", obj.Item_Id)
                        clsCommon.AddColumnsForChange(coll, "cheque_date", clsCommon.GetPrintDate(obj.Cheque_date, "dd/MMM/yyyy"))
                    Else
                        clsCommon.AddColumnsForChange(coll, "Install_Customer_Id", obj.InstallCustomer_Id)
                        clsCommon.AddColumnsForChange(coll, "Pullout_Customer_Id", obj.PulloutCustomer_Id)
                        clsCommon.AddColumnsForChange(coll, "Asset_Installation_Date", clsCommon.GetPrintDate(obj.installDate, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Asset_Pullout_Date", clsCommon.GetPrintDate(obj.pulloutDate, "dd/MMM/yyyy"))

                        clsCommon.AddColumnsForChange(coll, "agreement_no", obj.Agreement_No)
                        clsCommon.AddColumnsForChange(coll, "agreement_date", clsCommon.GetPrintDate(obj.Agreement_date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "FOC_sec", obj.FOC_sec)
                        clsCommon.AddColumnsForChange(coll, "cheque_no_sec", obj.Cheque_No_sec)
                        clsCommon.AddColumnsForChange(coll, "Item_code", obj.Item_Id)
                        clsCommon.AddColumnsForChange(coll, "cheque_date_sec", clsCommon.GetPrintDate(obj.Cheque_date_sec, "dd/MMM/yyyy"))
                        'clsCommon.AddColumnsForChange(coll, "Agreement_Closing_Date", clsCommon.GetPrintDate(obj.ClosingDate, "dd/MMM/yyyy"))
                        'clsCommon.AddColumnsForChange(coll, "refund_amount", obj.Refund_Amount)
                        'clsCommon.AddColumnsForChange(coll, "FOC", obj.FOC)
                        'clsCommon.AddColumnsForChange(coll, "cheque_no", obj.Cheque_No)
                        'clsCommon.AddColumnsForChange(coll, "cheque_date", clsCommon.GetPrintDate(obj.Cheque_date, "dd/MMM/yyyy"))
                    End If
                    clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
                    clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.Type)
                    clsCommon.AddColumnsForChange(coll, "Trans_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    'clsCommon.AddColumnsForChange(coll, "Customer_Name", clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Id + "'", trans)))
                    'clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    'clsCommon.AddColumnsForChange(coll, "Route", obj.Route)
                    'clsCommon.AddColumnsForChange(coll, "Asset_Make", obj.AssetMake)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim InsCust As String = ""
                    Dim PulCst As String = ""
                    Dim puldt As String = ""
                    Dim insdt As String = ""
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Installed") = CompairStringResult.Equal Then
                        PulCst = ""
                        puldt = ""
                        InsCust = " and Install_Customer_Id='" & obj.InstallCustomer_Id & "'"
                        insdt = " and Asset_Installation_Date='" & clsCommon.GetPrintDate(obj.installDate, "dd/MMM/yyyy") & "'"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "PulledOut") = CompairStringResult.Equal Then
                        PulCst = " and Pullout_Customer_Id='" & obj.PulloutCustomer_Id & "'"
                        InsCust = ""
                        insdt = ""
                        puldt = " and Asset_Pullout_Date='" & clsCommon.GetPrintDate(obj.pulloutDate, "dd/MMM/yyyy") & "'"
                    Else
                        InsCust = " and Install_Customer_Id='" & obj.InstallCustomer_Id & "'"
                        PulCst = " and Pullout_Customer_Id='" & obj.PulloutCustomer_Id & "'"
                        puldt = " and Asset_Pullout_Date='" & clsCommon.GetPrintDate(obj.pulloutDate, "dd/MMM/yyyy") & "'"
                        insdt = " and Asset_Installation_Date='" & clsCommon.GetPrintDate(obj.installDate, "dd/MMM/yyyy") & "'"
                    End If
                    Dim whrcls As String = "Asset_Id='" + obj.Asset_Id + "'  AND Trans_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' AND Trans_Type='" + obj.Trans_Type + "' " + PulCst + InsCust + insdt + puldt
                    Dim qry As String = "Select COunt(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW WHERE " + whrcls + ""
                    If obj.Item_Id <> "" Then
                        whrcls = "Item_code ='" & obj.Item_Id & "' and Asset_Id='" + obj.Asset_Id + "'  AND Trans_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' AND Trans_Type='" + obj.Trans_Type + "' " + PulCst + InsCust + insdt + puldt
                    End If
                    If clsDBFuncationality.getSingleValue(qry, trans) <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Asset_Id", obj.Asset_Id)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                        ' clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_ASSET_INSTALL_PULLOUT_NEW", OMInsertOrUpdate.Insert, "", trans)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_INSTALL_PULLOUT_NEW", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        'clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDB, "TSPL_ASSET_INSTALL_PULLOUT_NEW", OMInsertOrUpdate.Update, whrcls, trans)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_INSTALL_PULLOUT_NEW", OMInsertOrUpdate.Update, whrcls, trans)
                    End If
                    '================Update Data in Asset Install PullOut in Install and pullout Case========
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Both") = CompairStringResult.Equal Then
                        Dim sQuery As String = "update TSPL_ASSET_INSTALL_PULLOUT_NEW set agreement_closing_date='" & clsCommon.GetPrintDate(obj.Agreement_date, "dd/MMM/yyyy") & "'," _
                    & " refund_amount='" & obj.Refund_Amount & "',foc='" & obj.FOC & "',cheque_no='" & obj.Cheque_No & "',cheque_date='" & clsCommon.GetPrintDate(obj.Cheque_date, "dd/MMM/yyyy") & "' where install_customer_id='" & obj.PulloutCustomer_Id & "' and Asset_id='" & obj.Asset_Id & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    End If
                    '=========================================================
                    '-------------------------Updates Data In Visi Master-------------------------------------------
                    Dim coll1 As New Hashtable()
                    If (clsCommon.CompairString(obj.Type, "New") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Type, "N") = CompairStringResult.Equal) Then
                        clsCommon.AddColumnsForChange(coll1, "Type", "N")
                    Else
                        clsCommon.AddColumnsForChange(coll1, "Type", "R")
                    End If
                    'clsCommon.AddColumnsForChange(coll1, "Location", obj.Location)
                    'clsCommon.AddColumnsForChange(coll1, "Route", obj.Route)
                    If clsCommon.CompairString(obj.Trans_Type, "Installed") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll1, "Customer_Id", obj.InstallCustomer_Id)
                        clsCommon.AddColumnsForChange(coll1, "Customer_Name", clsDBFuncationality.getSingleValue("select customer_Name from tspl_customer_master where cust_code='" & obj.InstallCustomer_Id & "'", trans))
                        clsCommon.AddColumnsForChange(coll1, "visi_Installation_date", clsCommon.GetPrintDate(clsCommon.myCDate(obj.installDate), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "Pull_Out_Date", Nothing, True)
                        clsCommon.AddColumnsForChange(coll1, "agreement_no", obj.Agreement_No)
                        If clsCommon.myCstr(obj.Cheque_No) <> "" Then
                            clsCommon.AddColumnsForChange(coll1, "CHEQUE_NO", obj.Cheque_No)
                        End If
                        If clsCommon.myCstr(obj.Cheque_date) <> "" Then
                            clsCommon.AddColumnsForChange(coll1, "cHEQUE_DATE", clsCommon.GetPrintDate(obj.Cheque_date, "dd/MMM/yyyy"))
                        End If
                        clsCommon.AddColumnsForChange(coll1, "Security_Amount", obj.Refund_Amount)
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "PulledOut") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll1, "Customer_Id", Nothing, True)
                        clsCommon.AddColumnsForChange(coll1, "Customer_Name", Nothing, True)
                        clsCommon.AddColumnsForChange(coll1, "Pull_out_date", clsCommon.GetPrintDate(clsCommon.myCDate(obj.pulloutDate), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "visi_Installation_date", Nothing, True)
                    Else
                        clsCommon.AddColumnsForChange(coll1, "Customer_Id", obj.InstallCustomer_Id)
                        clsCommon.AddColumnsForChange(coll1, "Customer_Name", clsDBFuncationality.getSingleValue("select customer_Name from tspl_customer_master where cust_code='" & obj.InstallCustomer_Id & "'", trans))
                        clsCommon.AddColumnsForChange(coll1, "visi_Installation_date", clsCommon.GetPrintDate(clsCommon.myCDate(obj.installDate), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "Pull_out_date", Nothing, True)
                    End If

                    ' clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll1, ArrDB, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_id = '" + obj.Asset_Id + "' and TSPL_VISI_MASTER.Asset_No = '" + obj.Item_Id + "'", trans)
                    clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_id = '" + obj.Asset_Id + "' and TSPL_VISI_MASTER.Asset_No = '" + obj.Item_Id + "'", trans)
                    '------------------------------------------------------------------------------------------------
                Next
                'trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

End Class

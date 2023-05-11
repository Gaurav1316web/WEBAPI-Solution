'--------Created By Richa 28/07/2014 Against Ticket No BM00000003158,BM00000006545,BM00000005381
Imports System.Data.SqlClient
Imports common
Public Class ClsLoadingTanker
#Region "variables"
    Public Arr As List(Of clsloadingChemberNoDetails) = Nothing
    Public LoadingTanker_No As String = Nothing
    Public LoadingTanker_Date As Date
    Public Weighment_No As String = Nothing
    Public Tanker_No As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Silo_No As String = Nothing
    Public Quantity As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Modified_Date As Date?
    Public Created_Date As Date?
    Public QC_Code As String = String.Empty
    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public SalesOrder_Code As String = String.Empty
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'Dim qry As String = " Select LoadingTanker_No as Code ,LoadingTanker_Date as Date from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
        Dim qry As String = "Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as Code,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date  as Date,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No as [Weighment No],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc],case when TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_LOADING_TANKER_DETAIL_BULKSALE  Left Outer Join TSPL_TANKER_MASTER  on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No  =SubLocationMaster.Location_Code"
        str = clsCommon.ShowSelectForm("LoadingTanker", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No as [TankerNo],TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as [LoadingNo],Convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103)  as [LoadingDate],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No as [Weighment No],TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc],case when TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_LOADING_TANKER_DETAIL_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No  =SubLocationMaster.Location_Code Left Outer Join TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No "
            str = customFinder.getFinder("TNKRFNDTMS", qry, whrcls, "TankerNo", curcode, "LoadingNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsLoadingTanker, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsLoadingTanker, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE where Loading_No='" + clsCommon.myCstr(obj.LoadingTanker_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmLoadingTanker, obj.Location_Code, obj.LoadingTanker_Date, trans)
            If isNewEntry Then
                obj.LoadingTanker_No = clsERPFuncationality.GetNextCode(trans, obj.LoadingTanker_Date, clsDocType.LoadingTankerBulkSale, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            ''added by shivani
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "LoadingTanker_Date", clsCommon.GetPrintDate(obj.LoadingTanker_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "LoadingTanker_Date", clsCommon.GetPrintDate(obj.LoadingTanker_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Silo_No", obj.Silo_No, True)
            clsCommon.AddColumnsForChange(coll, "Quantity", obj.Quantity)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code, True)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOADING_TANKER_DETAIL_BULKSALE where Weighment_No ='" & obj.Weighment_No & "' ", trans) < 1 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "LoadingTanker_No", obj.LoadingTanker_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOADING_TANKER_DETAIL_BULKSALE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("Document already created for Weighment No " & obj.Weighment_No & "")

                End If

            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOADING_TANKER_DETAIL_BULKSALE", OMInsertOrUpdate.Update, "TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No='" + obj.LoadingTanker_No + "'", trans)
            End If

            clsloadingChemberNoDetails.SaveData(obj.LoadingTanker_No, obj.Arr, trans, obj.Weighment_No)
            'trans.Commit()
        Catch err As Exception
            ' trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsLoadingTanker, ByVal trans As SqlTransaction, ByVal isHistory As Boolean) As Boolean
        Dim qry As String = String.Empty
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "LoadingTanker_Date", clsCommon.GetPrintDate(obj.LoadingTanker_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Silo_No", obj.Silo_No, True)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code, True)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Quantity", obj.Quantity)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            If clsCommon.myLen(obj.Posting_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Posting_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy"))
            If isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "LoadingTanker_No", obj.LoadingTanker_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOADING_TANKER_DETAIL_BULKSALE_HISTORY", OMInsertOrUpdate.Insert, "", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsLoadingTanker
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsLoadingTanker
        Dim obj As ClsLoadingTanker = Nothing
        Dim Arr As List(Of ClsLoadingTanker) = Nothing
        Dim qry As String = "Select SalesOrder_Code,Tare_Weight,Gross_Weight,Net_Weight,QC_Code,LoadingTanker_No ,LoadingTanker_Date,Weighment_No,Tanker_No,Location_Code,Item_Code,Silo_No,Quantity,Posted,Posting_Date,Created_Date,Modified_Date  from TSPL_LOADING_TANKER_DETAIL_BULKSALE where 2=2  "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ")  "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No = (select MIN(LoadingTanker_No) from TSPL_LOADING_TANKER_DETAIL_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No = (select Max(LoadingTanker_No) from TSPL_LOADING_TANKER_DETAIL_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No = (select Min(LoadingTanker_No) from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No>'" + strCode + "' " + whrclas + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No = (select Max(LoadingTanker_No) from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No<'" + strCode + "' " + whrclas + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsLoadingTanker()
            obj.LoadingTanker_No = clsCommon.myCstr(dt.Rows(0)("LoadingTanker_No"))
            obj.LoadingTanker_Date = clsCommon.myCstr(dt.Rows(0)("LoadingTanker_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Silo_No = clsCommon.myCstr(dt.Rows(0)("Silo_No"))
            obj.Quantity = clsCommon.myCdbl(dt.Rows(0)("Quantity"))
            obj.QC_Code = clsCommon.myCstr(dt.Rows(0)("QC_Code"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            obj.Arr = clsloadingChemberNoDetails.GetData(obj.LoadingTanker_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select LoadingTanker_Date,Location_Code from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmLoadingTanker, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("LoadingTanker_Date")), trans)


        End If

        Try
            Dim qry As String = "delete from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE where Loading_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Loading No not found to Post")
            End If
            Dim obj As ClsLoadingTanker = ClsLoadingTanker.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmLoadingTanker, obj.Location_Code, obj.LoadingTanker_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.LoadingTanker_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_LOADING_TANKER_DETAIL_BULKSALE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where LoadingTanker_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ' ''richa Ticket No.BM00000003617 on 25/08/2014
    'Public Shared Function GetTolerane(ByVal dblBalanceQty As Double, ByVal dblQty As Double) As Double
    '    Dim dblToleanceQty As Double = 0
    '    Dim dblQtyForTolerance As Double = 0
    '    Dim dblAllowedDispatchQty As Double = 0
    '    If dblBalanceQty < dblQty Then
    '        dblQtyForTolerance = dblQty - dblBalanceQty
    '        Dim dblTolerance As Double = clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing)
    '        If dblTolerance > 0 Then
    '            dblTolerance = (dblBalanceQty * dblTolerance) / 100
    '            If dblBalanceQty < 0 Then
    '                If -(dblBalanceQty) < dblTolerance Then
    '                    dblToleanceQty = dblTolerance + dblBalanceQty
    '                Else
    '                    dblToleanceQty = 0
    '                End If
    '            Else
    '                dblToleanceQty = dblTolerance + dblBalanceQty
    '            End If
    '            'dblToleanceQty = dblQtyForTolerance * dblTolerance / 100
    '            dblAllowedDispatchQty = dblToleanceQty
    '        End If
    '    Else
    '        dblAllowedDispatchQty = dblBalanceQty
    '    End If

    '    Return dblAllowedDispatchQty
    'End Function
    ''richa Ticket No.BM00000003617 on 25/08/2014
    Public Shared Function GetTolerane(ByVal dblBalanceQty As Double, ByVal dblQty As Double, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim dblToleranceQty As Double = 0
        Dim dblAllowedDispatchQty As Double = 0
        If dblBalanceQty < dblQty Then
            Dim dblTolerance As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StockToleranceLimit, clsFixedParameterCode.StockToleranceLimit, trans))
            If dblTolerance >= 0 Then
                dblToleranceQty = (dblBalanceQty * dblTolerance) / 100
                dblAllowedDispatchQty = dblBalanceQty + dblToleranceQty
            End If
        Else
            dblAllowedDispatchQty = dblBalanceQty
        End If

        Return dblAllowedDispatchQty
    End Function
    Public Shared Function GetbalanceQuery(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String)
        Dim strCondition As String = ""
        Dim strCondition1 As String = ""
        Dim strCondition2 As String = ""
        Dim strCondition3 As String = String.Empty
        Dim strCondition4 As String = String.Empty
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition1 = "  and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            strCondition1 = " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' "
        End If

        ''richa agarwal changes done on  29 march 2016
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition2 = "  and TSPL_ADJUSTMENT_HEADER.MainLocationCode='" + strLocation + "' and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + strSubLocation + "'"
        Else
            strCondition2 = " and TSPL_ADJUSTMENT_HEADER.MainLocationCode='" + strLocation + "' "
        End If
        ''---------

        ''richa agarwal changes done on 17 Apr,2018 for TSPL_MCC_Dispatch_Challan_Stock_Detail
        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition3 = " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strLocation + "' and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition3 = " and (TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strSubLocation + "' or TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strSubLocation + "') "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition3 = " and (TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strLocation + "' or (TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strLocation + "' and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code ='')) "
        End If

        ''richa agarwal changes done on 17 Apr,2018 for TSPL_Dispatch_BulkSale
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition4 = "  and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            strCondition4 = " and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' "
        End If

        ''---------

        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition = " and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strSubLocation + "') "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' or (TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code ='')) "
        End If

        Dim qry As String = "select (((case when Minimum_Bal.Minimum_Balance is null then  xx.Qty else (case when Minimum_Bal.Minimum_Balance>xx.Qty then xx.Qty else Minimum_Bal.Minimum_Balance end)  end))/FinalUOM.Conversion_Factor) as Qty" + _
        ",((case when Minimum_Bal.Minimum_FATKG is null then  xx.Fat_KG else (case when Minimum_Bal.Minimum_FATKG>xx.Fat_KG then xx.Fat_KG else Minimum_Bal.Minimum_FATKG end)  end)) as Fat_KG" + Environment.NewLine + _
        ",((case when Minimum_Bal.Minimum_SNFKG is null then  xx.SNF_KG else (case when Minimum_Bal.Minimum_SNFKG>xx.SNF_KG then xx.SNF_KG else Minimum_Bal.Minimum_SNFKG end)  end)) as SNF_KG" + Environment.NewLine + _
        "from(" + Environment.NewLine
        qry += " select xxx.ICode,xxx.Location,SUM(qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as Qty,SUM(Fat_KG *RI) as Fat_KG ,SUM(SNF_KG *RI) as SNF_KG  from( " + Environment.NewLine
        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,SUM(Fat_KG*RI) as Fat_KG,SUM(SNF_KG*RI) as SNF_KG  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,Fat_KG,SNF_KG from("
        qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code AS Location_Code, "
        qry += " TSPL_INVENTORY_MOVEMENT_NEW.InOut,case when Custom_UOM='" + strUOM + "' and Custom_Coversion_Factor>0 then cast(Stock_Qty /Custom_Coversion_Factor as decimal(18,2)) else TSPL_inventory_Movement_New.Stock_Qty end as Qty,case when Custom_UOM='" + strUOM + "' and Custom_Coversion_Factor>0 then Custom_UOM else  TSPL_inventory_Movement_New.Stock_UOM end as UOMNew,Fat_KG,SNF_KG "
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
        qry += " where TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + strICode + "' " + strCondition + " "
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance,null as Minimum_FATKG,null as Minimum_SNFKG "
        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance,min(Closing_FATKG) as Minimum_FATKG,min(Closing_SNFKG) as Minimum_SNFKG from (" & _
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " & _
                        " ,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*fat_KG)) over(order by cast(Punching_Date as date)) as Closing_FATKG " + Environment.NewLine + _
                        ",sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*SNF_KG)) over(order by cast(Punching_Date as date)) as Closing_SNFKG " + Environment.NewLine + _
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & strICode & "' " & strCondition & " " & _
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' " & _
                        " group by Item_Code,Location_Code "
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderUnpostedDocForBalance, clsFixedParameterCode.ConsiderUnpostedDocForBalance, trans)) > 0 Then
            qry += Environment.NewLine + " union all " + Environment.NewLine

            qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,case when ISNULL(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No,'')<>'' then TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No else TSPL_Dispatch_BulkSale.Location_Code  end  as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom,TSPL_Dispatch_Detail_BulkSale.Fat_KG,TSPL_Dispatch_Detail_BulkSale.SNF_KG " & _
            " from TSPL_Dispatch_Detail_BulkSale " & _
            " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No" & _
            " LEFT OUTER JOIN TSPL_Quality_Check_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No" & _
            " LEFT OUTER JOIN TSPL_LOADING_TANKER_DETAIL_BULKSALE ON TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No" & _
            " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code='" + strICode + "'  " + strCondition4 + " and TSPL_Dispatch_Detail_BulkSale.Qty<>0 " & _
            " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"

            ''can sale
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_CANSALE_DISPATCH_DETAIL.ItemCode as ICode,TSPL_CANSALE_DISPATCH_HEAD.Location_Code as Locaion,TSPL_CANSALE_DISPATCH_DETAIL.Qty,-1 as RI,TSPL_CANSALE_DISPATCH_DETAIL.UOM AS Uom ,TSPL_CANSALE_DISPATCH_DETAIL.Fat_KG,TSPL_CANSALE_DISPATCH_DETAIL.SNF_KG " & _
            " from TSPL_CANSALE_DISPATCH_DETAIL " & _
            " left outer join TSPL_CANSALE_DISPATCH_HEAD on TSPL_CANSALE_DISPATCH_HEAD.Document_No=TSPL_CANSALE_DISPATCH_DETAIL.Document_No" & _
            " where TSPL_CANSALE_DISPATCH_HEAD.Posted=0 and TSPL_CANSALE_DISPATCH_DETAIL.ItemCode='" + strICode + "' and TSPL_CANSALE_DISPATCH_HEAD.Location_Code='" + strLocation + "' and TSPL_CANSALE_DISPATCH_DETAIL.Qty<>0  " & _
            " and TSPL_CANSALE_DISPATCH_DETAIL.Document_No not in ('" + strDocumentNo + "')"

            ''SILO MILK TRANSFER
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code  as ICode,TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code as Locaion,TSPL_SILO_MILK_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_SILO_MILK_TRANSFER_DETAIL.UOM AS Uom ,TSPL_SILO_MILK_TRANSFER_DETAIL.Fat_KG,TSPL_SILO_MILK_TRANSFER_DETAIL.SNF_KG " & _
            " from TSPL_SILO_MILK_TRANSFER_DETAIL " & _
            " left outer join TSPL_SILO_MILK_TRANSFER_HEAD on TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code =TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code " & _
            " where TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 and TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code='" + strICode + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Qty<>0  " & _
            " and TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code not in ('" + strDocumentNo + "')"

            If clsCommon.myLen(strSubLocation) > 0 Then
                qry += "  and TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code='" + strLocation + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + strSubLocation + "'"
            Else
                qry += " and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + strLocation + "' "
            End If

            qry += Environment.NewLine + " union all " + Environment.NewLine & _
                " select TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code as ICode,TSPL_Dispatch_BulkSale_Trade.Location_Code as Locaion,  " + Environment.NewLine & _
                " TSPL_Dispatch_Detail_BulkSale_Trade.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale_Trade.Unit_code AS Uom,TSPL_Dispatch_Detail_BulkSale_Trade.Fat_KG,TSPL_Dispatch_Detail_BulkSale_Trade.SNF_KG  from TSPL_Dispatch_Detail_BulkSale_Trade  " + Environment.NewLine & _
                " left outer join TSPL_Dispatch_BulkSale_Trade  on TSPL_Dispatch_BulkSale_Trade.Document_No=TSPL_Dispatch_Detail_BulkSale_Trade.Document_No  " + Environment.NewLine & _
                " where TSPL_Dispatch_BulkSale_Trade.Posted=0 and TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale_Trade.Location_Code='" + strLocation + "'" + Environment.NewLine & _
                " and TSPL_Dispatch_Detail_BulkSale_Trade.Qty<>0   and TSPL_Dispatch_Detail_BulkSale_Trade.Document_No not in ('" + strDocumentNo + "')"

            qry += Environment.NewLine + " union all " + Environment.NewLine

            qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom,TSPL_PP_ISSUE_ITEM_DETAIL.Fat_KG,TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG "
            qry += " from TSPL_PP_ISSUE_ITEM_DETAIL "
            qry += " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code"
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code "
            qry += " where TSPL_PP_ISSUE_HEAD.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  "
            qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"

            '' to be added
            qry += Environment.NewLine + " union all " + Environment.NewLine

            qry += " select TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Locaion,TSPL_CSA_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_CSA_TRANSFER_DETAIL.Unit_code AS Uom ,0 as Fat_KG,0 as SNF_KG"
            qry += " from TSPL_CSA_TRANSFER_DETAIL "
            qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code"
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code "
            qry += " where TSPL_CSA_TRANSFER_HEAD.status=0 and TSPL_CSA_TRANSFER_DETAIL.Item_Code='" + strICode + "' and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + strLocation + "' and TSPL_CSA_TRANSFER_DETAIL.Qty<>0  "
            qry += " and TSPL_CSA_TRANSFER_DETAIL.doc_code not in ('" + strDocumentNo + "')"

            qry += Environment.NewLine + " union all " + Environment.NewLine

            qry += " select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as Locaion,TSPL_SD_SALE_INVOICE_DETAIL.Qty,-1 as RI,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code AS Uom,0 as Fat_KG,0 as SNF_KG "
            qry += " from TSPL_SD_SALE_INVOICE_DETAIL "
            qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code"
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
            qry += " where TSPL_SD_SALE_INVOICE_HEAD.status=0 and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code='" + strICode + "' and TSPL_SD_SALE_INVOICE_HEAD.bill_to_location='" + strLocation + "' and TSPL_SD_SALE_INVOICE_DETAIL.Qty<>0  "
            qry += " and TSPL_SD_SALE_INVOICE_DETAIL.document_code not in ('" + strDocumentNo + "')"

            qry += Environment.NewLine + " union all " + Environment.NewLine

            qry += " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.bill_to_location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom,0 as Fat_KG,0 as SNF_KG "
            qry += " from TSPL_SD_SHIPMENT_DETAIL "
            qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code"
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code "
            qry += " where TSPL_SD_SHIPMENT_HEAD.status=0 and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + strICode + "' and TSPL_SD_SHIPMENT_HEAD.bill_to_location='" + strLocation + "' and TSPL_SD_SHIPMENT_DETAIL.Qty<>0  "
            qry += " and TSPL_SD_SHIPMENT_DETAIL.document_code not in ('" + strDocumentNo + "')"

            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
            qry += " BUILD_ITEM_UNIT_CODE as UnitCode,TSPL_PROD_ASSEMBLIES.FAT_KG as Fat_KG,TSPL_PROD_ASSEMBLIES.SNF_KG as SNF_KG from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE='Disassembly' and TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + strICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
            If clsCommon.myLen(strSubLocation) > 0 Then
                qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strSubLocation & "'"
            Else
                qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
            End If

            qry += " union all  "

            qry += " select  TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
            qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY as Qty,"
            qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
            qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.Fat_KG,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.SNF_KG from TSPL_PJC_ASSEMBLIES "
            qry += " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE "
            qry += " where TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly'  and  TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + strICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
            If clsCommon.myLen(strSubLocation) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strSubLocation & "'"
            Else
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
            End If

            qry += " union all  "

            qry += " select  TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
            qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
            qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode,TSPL_WRECKAGE_BOOKING.Avail_FAT_KG as Fat_KG,TSPL_WRECKAGE_BOOKING.Avail_SNF_KG as SNF_KG from TSPL_WRECKAGE_ENTRY "
            qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
            qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + strICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + strDocumentNo + "')"
            If clsCommon.myLen(strSubLocation) > 0 Then
                qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strSubLocation & "'"
            Else
                qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strLocation & "'"
            End If


            '' to be added
            qry += Environment.NewLine + " union all " + Environment.NewLine

            qry += " select TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code as ICode,case when ISNULL(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No,'')<>'' then TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No else TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code end  as Locaion,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity as Qty,-1 as RI,TSPL_ITEM_MASTER.Unit_code AS Uom ,0 as Fat_KG,0 as SNF_KG"
            qry += " from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
            qry += " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code='" + strICode + "' " + strCondition1 + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity<>0  "
            qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in ('" + strDocumentNo + "')"
            qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in (select LoadingTanker_No FROM TSPL_Quality_Check_BulkSale LEFT OUTER JOIN TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No WHERE ISNULL(TSPL_Dispatch_BulkSale.QC_Code,'')<>'')"

            '' added by richa agarwal to include transactions of store adjustment whose trans type is Out and milk type is 1 
            qry += "union all " + Environment.NewLine & _
                "  select TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,"
            If clsCommon.myLen(strSubLocation) > 0 Then
                qry += "  TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion, "
            Else
                qry += " TSPL_ADJUSTMENT_HEADER.MainLocationCode as Locaion, "
            End If
            qry += " TSPL_ADJUSTMENT_DETAIL.Item_Quantity ,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_code AS Uom,TSPL_ADJUSTMENT_DETAIL.FAT_KG as Fat_KG,TSPL_ADJUSTMENT_DETAIL.SNF_KG as SNF_KG " & _
                " from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_DETAIL.Adjustment_No  " & _
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' " & _
                " where TSPL_ADJUSTMENT_HEADER.Posted ='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + strICode + "' " + strCondition2 + " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity <>0 " & _
                " and TSPL_ADJUSTMENT_HEADER.Trans_Type  ='Out' and TSPL_ADJUSTMENT_HEADER .IsMilkType =1 and TSPL_ADJUSTMENT_DETAIL.Adjustment_No  not in ('" + strDocumentNo + "') " + Environment.NewLine


            qry += Environment.NewLine + " union all " + Environment.NewLine


            '' save data into detail table to check qty with main and silo location
            qry += "  select  TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code ,  TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code AS Location_Code , TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty  as Qty,  -1 as RI, TSPL_MCC_Dispatch_Challan_Stock_Detail.UOM  as UOMNew,TSPL_MCC_Dispatch_Challan_Stock_Detail.Fat_KG as Fat_KG,TSPL_MCC_Dispatch_Challan_Stock_Detail.SNF_KG as SNF_KG  from TSPL_MCC_Dispatch_Challan_Stock_Detail  where TSPL_MCC_Dispatch_Challan_Stock_Detail.IsPosted=0 and TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty<>0 " & _
                " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code='" + strICode + "'  " + strCondition3 + " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Chalan_No not in ('" + strDocumentNo + "')"

            '' query for add/remove items durng Process production Standardization
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
            qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
            qry += " (case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
            qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG as Fat_KG,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG as SNF_KG from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL "
            qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
            qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + strICode + "' "
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code not in ('" + strDocumentNo + "')"
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

            '' query for  Process production Standardization
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code,"
            qry += " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty,"
            qry += " 1 as RI,"
            qry += " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.UNIT_CODE,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG as Fat_KG,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG as SNF_KG from TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL "
            qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
            qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code='" + strICode + "' "
            qry += " and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code not in ('" + strDocumentNo + "')"
            qry += " and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code='" & strLocation & "' "

            '' PRODUCTION CONSUMPTION 
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," & _
                  " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY,-1 as RI," & _
                  " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG as Fat_KG,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG as SNF_KG from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " & _
                  " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
                  " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE='" & strICode & "' " & _
                  " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "') " & _
                  " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE='" & strLocation & "'"

            '' query for add/remove items durng Process production STAGE PROCESS
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
            qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
            qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
            qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Kg as Fat_KG,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Kg as SNF_KG from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
            qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
            qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + strICode + "' "
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + strDocumentNo + "')"
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

            '' PRODUCTION ENTRY 
            qry += Environment.NewLine + " union all " + Environment.NewLine
            qry += " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE," & _
                   " TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,1 as RI," & _
                   " TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG as Fat_KG,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as SNF_KG from TSPL_PP_PRODUCTION_ENTRY_DETAIL " & _
                   " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
                   " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE='" & strICode & "' " & _
                   " and TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "')" & _
                   " and TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE='" & strLocation & "'"

        End If
        
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " )xxx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UOM where xxx.Location ='" & strSubLocation & "' group by ICode,Location"
        Else
            qry += " )xxx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UOM group by ICode,Location"
        End If
        qry += " )xx" + Environment.NewLine + _
        " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code " + Environment.NewLine + _
        " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "'"
        Return qry
    End Function
    Public Shared Function getBalance(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As Double
        Return getBalance(strICode, strLocation, strSubLocation, strDocumentNo, dtDocumentDate, trans, strUOM, 0, 0)
    End Function
    Public Shared Function getBalance(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String, ByRef FATKg As Decimal, ByRef SNFKg As Decimal) As Double
        FATKg = 0
        SNFKg = 0
        Dim BalQty As Double = 0
        Dim qry As String = GetbalanceQuery(strICode, strLocation, strSubLocation, strDocumentNo, dtDocumentDate, trans, strUOM)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select sum(qty) as qty,sum(Fat_KG) as Fat_KG,sum(SNF_KG) as SNF_KG from ( " & qry & " )zzz ", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            BalQty = clsCommon.myCdbl(dt.Rows(0)("qty"))
            FATKg = clsCommon.myCdbl(dt.Rows(0)("Fat_KG"))
            SNFKg = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
        End If
        Return BalQty
    End Function
End Class

Public Class clsloadingChemberNoDetails
#Region "Variables"
    Public loading_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public Loading_Status As Integer = 0
    Public loading_Sequence As Integer = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsloadingChemberNoDetails), ByVal trans As SqlTransaction, ByVal strWeighmentNo As String) As Boolean
        Dim intCount As Integer = 0
        Dim intLineNo As Integer = 0
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE where loading_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsloadingChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "loading_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "Loading_Status", obj.Loading_Status)
                clsCommon.AddColumnsForChange(coll, "loading_Sequence", obj.loading_Sequence)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsloadingChemberNoDetails)
        Dim arr As List(Of clsloadingChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE where TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE.loading_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsloadingChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsloadingChemberNoDetails = New clsloadingChemberNoDetails()
                obj.loading_No = clsCommon.myCstr(dr("loading_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.Loading_Status = clsCommon.myCstr(dr("Loading_Status"))
                obj.loading_Sequence = clsCommon.myCdbl(dr("loading_Sequence"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

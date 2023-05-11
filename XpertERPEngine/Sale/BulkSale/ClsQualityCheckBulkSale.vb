'--------Created By Richa 28/07/2014 Against Ticket No BM00000003246
''changes by richa agarwal against ticket no. BM00000006545,BM00000005380
Imports System.Data.SqlClient
Imports common
Public Class ClsQualityCheckBulkSale
#Region "variables"
    Public QC_No As String = Nothing
    Public QC_Date As Date
    Public LoadingTanker_No As String = Nothing
    Public GateEntry_Document_No As String = Nothing
    Public Weighment_No As String = Nothing
    Public Tanker_No As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Silo_No As String = Nothing
    Public Correction_Factor As Double = 0
    Public Unit_code As String = Nothing
    Public Qty As Double = 0
    Public Fat As Double = 0
    Public CLR As Double = 0
    Public SNF As Double = 0
    Public Remarks As String = Nothing
    Public Customer_Code As String = Nothing
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Modified_Date As Date?
    Public Created_Date As Date?
    Public ApprovalRequired As String = Nothing
    Public Approved As String = Nothing
    Public SalesOrder_Code As String = Nothing
    Public arrQcParamDetail As List(Of clsQcParamBulkSale) = Nothing

#End Region


    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        ' Dim qry As String = "Select TSPL_Quality_Check_BulkSale.QC_No as Code ,TSPL_Quality_Check_BulkSale.QC_Date as Date from TSPL_Quality_Check_BulkSale  "
        Dim qry As String = "Select TSPL_QUALITY_CHECK_BULKSALE.QC_No as Code,Convert(varchar,TSPL_QUALITY_CHECK_BULKSALE.QC_Date,103) as Date,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No as [Loading No],TSPL_QUALITY_CHECK_BULKSALE.Weighment_No as [Weighment No],TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_QUALITY_CHECK_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Quality_Check_BulkSale.Tanker_No as [Tanker No],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_Quality_Check_BulkSale.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc] from TSPL_QUALITY_CHECK_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_QUALITY_CHECK_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  Left Outer Join TSPL_TANKER_MASTER  on TSPL_Quality_Check_BulkSale.Tanker_No=TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER SubLocationMaster on TSPL_Quality_Check_BulkSale.Silo_No=SubLocationMaster.Location_Code "
        str = clsCommon.ShowSelectForm("QualityCheck", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_Quality_Check_BulkSale.Tanker_No as [TankerNo],TSPL_Quality_Check_BulkSale.QC_No as [QCNo],TSPL_Quality_Check_BulkSale.QC_Date as [QC Date],TSPL_Quality_Check_BulkSale.Weighment_No as [Weighment No],TSPL_Quality_Check_BulkSale.LoadingTanker_No as [Loading Tanker No],TSPL_Quality_Check_BulkSale.GateEntry_Document_No as [Gate Entry No],TSPL_Quality_Check_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Quality_Check_BulkSale.Silo_No as [Sub Location],SubLocationMaster.Location_Desc as [Sub Location Name],TSPL_Quality_Check_BulkSale.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name],TSPL_Quality_Check_BulkSale.Unit_code as [UOM] from TSPL_Quality_Check_BulkSale Left Outer Join TSPL_Dispatch_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code Left Outer Join  TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_Quality_Check_BulkSale.Weighment_No = TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_Quality_Check_BulkSale.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on SubLocationMaster .Location_Code =TSPL_Quality_Check_BulkSale.Silo_No LEft Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_Quality_Check_BulkSale.Item_Code"
            str = customFinder.getFinder("TNKRFNDQCS", qry, whrcls, "TankerNo", curcode, "QCNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getSalesOrderFinderForPavitra(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_Quality_Check_BulkSale.SalesOrder_Code as [SalesOrder],TSPL_Quality_Check_BulkSale.Tanker_No as [TankerNo],TSPL_Quality_Check_BulkSale.GateEntry_Document_No as [Gate Entry No],TSPL_Quality_Check_BulkSale.Weighment_No as [WeighmentEntryNo],TSPL_Quality_Check_BulkSale.QC_No as [QC_No], convert(varchar,TSPL_Quality_Check_BulkSale.QC_Date,103) as [QC Date],TSPL_Quality_Check_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_Quality_Check_BulkSale.Silo_No as [Sub Location],SubLocationMaster.Location_Desc as [Sub Location Name],TSPL_Quality_Check_BulkSale.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name],TSPL_Quality_Check_BulkSale.Unit_code as [UOM],case when TSPL_Quality_Check_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Quality_Check_BulkSale Left Outer Join TSPL_LOCATION_MASTER on TSPL_Quality_Check_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on SubLocationMaster .Location_Code =TSPL_Quality_Check_BulkSale.Silo_No LEft Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_Quality_Check_BulkSale.Item_Code"
            str = customFinder.getFinder("SALEsOrderFNDQCS", qry, whrcls, "SalesOrder", curcode, "QC_No")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsQualityCheckBulkSale, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As ClsQualityCheckBulkSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmQualityCheckBulkSale, obj.Location_Code, obj.QC_Date, trans)
            qry = "delete from TSPL_QC_Parameter_Detail_BulKSALE where QC_No='" & obj.QC_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.QC_No = clsERPFuncationality.GetNextCode(trans, obj.QC_Date, clsDocType.QualityCheckBulkSale, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "QC_Date", clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "QC_Date", clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "LoadingTanker_No", obj.LoadingTanker_No, True)
            clsCommon.AddColumnsForChange(coll, "GateEntry_Document_No", obj.GateEntry_Document_No, True)
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Silo_No", obj.Silo_No, True)
            clsCommon.AddColumnsForChange(coll, "Correction_Factor", obj.Correction_Factor)
            clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code, True)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Fat", obj.Fat)
            clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
            clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ApprovalRequired", obj.ApprovalRequired)
            If isNewEntry Then
                'If clsCommon.CompairString(obj.ApprovalRequired, "Y") = CompairStringResult.Equal Then
                '    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                '    "values ('Bulk Quality Check','" & clsUserMgtCode.FrmQualityCheckBulkSale & "','" & obj.QC_No & "','" & clsCommon.GetPrintDate(obj.QC_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'End If
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_QUALITY_CHECK_BULKSALE where LoadingTanker_No ='" & obj.LoadingTanker_No & "' ", trans) < 1 Then

                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "QC_No", obj.QC_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUALITY_CHECK_BULKSALE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("Document already created for Loading Tanker No " & obj.LoadingTanker_No & "")

                End If
            Else
                'If clsCommon.CompairString(obj.ApprovalRequired, "Y") = CompairStringResult.Equal Then
                '    Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.FrmQualityCheckBulkSale & "' and Document_No='" & obj.QC_No & "' ", trans))
                '    If intExist = 0 Then
                '        qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                '     "values ('Bulk Quality Check','" & clsUserMgtCode.FrmQualityCheckBulkSale & "','" & obj.QC_No & "','" & clsCommon.GetPrintDate(obj.QC_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '    End If
                'End If
                If clsCommon.CompairString(obj.ApprovalRequired, "N") = CompairStringResult.Equal Then
                    Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.FrmQualityCheckBulkSale & "' and Document_No='" & obj.QC_No & "' ", trans))
                    If intExist <> 0 Then
                        qry = "delete from TSPL_TRANSACTION_APPROVAL where screen_name='Bulk Quality Check' and Program_Code ='" & clsUserMgtCode.FrmQualityCheckBulkSale & "' and Document_No ='" & obj.QC_No & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUALITY_CHECK_BULKSALE", OMInsertOrUpdate.Update, "TSPL_QUALITY_CHECK_BULKSALE.QC_No='" + obj.QC_No + "'", trans)
            End If
            clsQcParamBulkSale.saveData(obj.arrQcParamDetail, obj.QC_No, trans)
            'trans.Commit()
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsQualityCheckBulkSale, ByVal trans As SqlTransaction, ByVal isHistory As Boolean) As Boolean
        Dim qry As String = String.Empty
        Try
            'qry = "delete from TSPL_QC_Parameter_Detail_BulKSALE_History where QC_No='" & obj.QC_No & "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QC_Date", clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "LoadingTanker_No", obj.LoadingTanker_No, True)
            clsCommon.AddColumnsForChange(coll, "GateEntry_Document_No", obj.GateEntry_Document_No, True)
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Silo_No", obj.Silo_No, True)
            clsCommon.AddColumnsForChange(coll, "Correction_Factor", obj.Correction_Factor)
            clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code, True)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Fat", obj.Fat)
            clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
            clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "ApprovalRequired", obj.ApprovalRequired)
            clsCommon.AddColumnsForChange(coll, "Approved", obj.Approved)
            If clsCommon.myLen(obj.Posting_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Posting_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy"))
            If isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "QC_No", obj.QC_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUALITY_CHECK_BULKSALE_History", OMInsertOrUpdate.Insert, "", trans)
            End If
            clsQcParamBulkSale.saveData(obj.arrQcParamDetail, obj.QC_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsQualityCheckBulkSale
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsQualityCheckBulkSale
        Dim obj As ClsQualityCheckBulkSale = Nothing
        Dim Arr As List(Of ClsQualityCheckBulkSale) = Nothing
        Dim qry As String = "Select ApprovalRequired,Approved,QC_No,QC_Date,SalesOrder_Code,LoadingTanker_No,GateEntry_Document_No,Customer_Code,Weighment_No,Tanker_No,Location_Code,Silo_No,Correction_Factor,Item_Code,Unit_code,Qty,Fat,CLR,SNF,Remarks,Posted,Posting_Date,Created_Date,Modified_Date from TSPL_QUALITY_CHECK_BULKSALE where 2=2  "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_QUALITY_CHECK_BULKSALE.QC_No = (select MIN(QC_No) from TSPL_QUALITY_CHECK_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_QUALITY_CHECK_BULKSALE.QC_No = (select Max(QC_No) from TSPL_QUALITY_CHECK_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_QUALITY_CHECK_BULKSALE.QC_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_QUALITY_CHECK_BULKSALE.QC_No = (select Min(QC_No) from TSPL_QUALITY_CHECK_BULKSALE where QC_No>'" + strCode + "' " + whrclas + " and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_QUALITY_CHECK_BULKSALE.QC_No = (select Max(QC_No) from TSPL_QUALITY_CHECK_BULKSALE where QC_No<'" + strCode + "' " + whrclas + " and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsQualityCheckBulkSale()
            obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
            obj.QC_Date = clsCommon.myCstr(dt.Rows(0)("QC_Date"))
            obj.LoadingTanker_No = clsCommon.myCstr(dt.Rows(0)("LoadingTanker_No"))
            obj.Approved = clsCommon.myCstr(dt.Rows(0)("Approved"))
            obj.ApprovalRequired = clsCommon.myCstr(dt.Rows(0)("ApprovalRequired"))
            obj.GateEntry_Document_No = clsCommon.myCstr(dt.Rows(0)("GateEntry_Document_No"))
            obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Silo_No = clsCommon.myCstr(dt.Rows(0)("Silo_No"))
            obj.Correction_Factor = clsCommon.myCdbl(dt.Rows(0)("Correction_Factor"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Unit_code = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
            obj.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
            obj.Fat = clsCommon.myCdbl(dt.Rows(0)("Fat"))
            obj.SNF = clsCommon.myCdbl(dt.Rows(0)("SNF"))
            obj.CLR = clsCommon.myCdbl(dt.Rows(0)("CLR"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
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
            obj.arrQcParamDetail = clsQcParamBulkSale.getData(obj.QC_No, trans)

        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select QC_Date,Location_Code from TSPL_QUALITY_CHECK_BULKSALE where QC_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmQualityCheckBulkSale, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("QC_Date")), trans)


        End If

        Try
            'clsQcParamBulkSale.deleteData(strDocNo)
            Dim qry As String = ""
            qry = "delete from TSPL_TRANSACTION_APPROVAL where screen_name='Bulk Quality Check' and Program_Code ='" & clsUserMgtCode.FrmQualityCheckBulkSale & "' and Document_No ='" & strDocNo & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_QC_Parameter_Detail_BulKSALE where QC_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_QUALITY_CHECK_BULKSALE where QC_No='" + strDocNo + "'"
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
                Throw New Exception("QC No not found to Post")
            End If
            Dim obj As ClsQualityCheckBulkSale = ClsQualityCheckBulkSale.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmQualityCheckBulkSale, obj.Location_Code, obj.QC_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_Quality_Check_BulkSale set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where QC_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


Public Class clsQcParamBulkSale
    Public QC_No As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public Item_code As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public Fat As Double = 0
    Public CLR As Double = 0
    Public SNF As Double = 0
    Public Remarks As String = Nothing
    Public Shared Function saveData(ByVal arrObj As List(Of clsQcParamBulkSale), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As clsQcParamBulkSale In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Item_code", obj.Item_code, True)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Fat", obj.Fat)
                    clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                    clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_Parameter_Detail_BulKSALE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
            arrObj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function saveDataHistory(ByVal arrObj As List(Of clsQcParamBulkSale), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As clsQcParamBulkSale In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_code", obj.Item_code)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Fat", obj.Fat)
                    clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                    clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_Parameter_Detail_BulKSALE_History", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
            arrObj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsQcParamBulkSale)
        Try
            Dim arrObj As List(Of clsQcParamBulkSale) = Nothing
            Dim obj As clsQcParamBulkSale = Nothing
            Dim qry As String = "select * from TSPL_QC_Parameter_Detail_BulKSALE where QC_No='" & strQCNo & "'  order by item_code,chamber_desc,Param_Field_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsQcParamBulkSale)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsQcParamBulkSale()
                    obj.QC_No = clsCommon.myCstr(dt.Rows(i)("QC_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.Item_code = clsCommon.myCstr(dt.Rows(i)("Item_code"))
                    obj.Chamber_Desc = clsCommon.myCstr(dt.Rows(i)("Chamber_Desc"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    obj.Fat = clsCommon.myCstr(dt.Rows(i)("Fat"))
                    obj.CLR = clsCommon.myCstr(dt.Rows(i)("CLR"))
                    obj.SNF = clsCommon.myCstr(dt.Rows(i)("SNF"))
                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Shared Function deleteData(ByVal strQCNo As String) As Boolean
    '    Try
    '        Dim isDeleted As Boolean = True
    '        Dim qry As String = "delete from TSPL_QC_Parameter_Detail_BulKSALE where QC_No='" & strQCNo & "'"
    '        isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
    '        Return isDeleted
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Function

End Class

'--------Created By Richa 28/07/2014 Against Ticket No BM00000003157
''changes by richa agarwal against ticket no. BM00000006545
Imports System.Data.SqlClient
Imports common
Public Class ClsWeighmentEntry

#Region "variables"
    Public Weighment_No As String = Nothing
    Public Weighment_Date As Date
    Public GateEntry_Document_No As String = Nothing
    Public Tanker_No As String = Nothing
    Public Location_Code As String = Nothing
    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Modified_Date As Date?
    Public Created_Date As Date?
    Public SalesOrder_Code As String = Nothing
    Public Arr As List(Of clsWeighmentEntryChemberNoDetails) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'Dim qry As String = "Select Weighment_No as Code ,Weighment_Date as Date from TSPL_WEIGHMENT_DETAIL_BULKSALE  "
        Dim qry As String = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No as Code,convert(varchar,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) as Date,TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],case when TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code "
        str = clsCommon.ShowSelectForm("Weighment", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No as [TankerNo],TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No as [WeighmentEntryNo],convert(varchar,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) as Date,TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],case when TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
            str = customFinder.getFinder("TNKRFNDWES", qry, whrcls, "TankerNo", curcode, "WeighmentEntryNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As ClsWeighmentEntry, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsWeighmentEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmWeighmentEntry, obj.Location_Code, obj.Weighment_Date, trans)
            If isNewEntry Then
                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.WeighmentEntryBulkSale, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            ''added by shivani
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "GateEntry_Document_No", obj.GateEntry_Document_No, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_WEIGHMENT_DETAIL_BULKSALE where GateEntry_Document_No ='" & obj.GateEntry_Document_No & "' ", trans) < 1 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHMENT_DETAIL_BULKSALE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("Document already created for Gate Entry No " & obj.GateEntry_Document_No & "")
                End If
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHMENT_DETAIL_BULKSALE", OMInsertOrUpdate.Update, "TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" + obj.Weighment_No + "'", trans)
            End If
            clsWeighmentEntryChemberNoDetails.SaveData(obj.Weighment_No, obj.Arr, trans)
            '  trans.Commit()
        Catch err As Exception
            '  trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsWeighmentEntry, ByVal trans As SqlTransaction, ByVal isHistory As Boolean) As Boolean
        Dim qry As String = ""
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "GateEntry_Document_No", obj.GateEntry_Document_No, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
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
                clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHMENT_DETAIL_BULKSALE_HISTORY", OMInsertOrUpdate.Insert, "", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsWeighmentEntry
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsWeighmentEntry
        Dim obj As ClsWeighmentEntry = Nothing
        Dim Arr As List(Of ClsWeighmentEntry) = Nothing
        Dim qry As String = "Select SalesOrder_Code,Weighment_No,Weighment_Date,Location_Code,Tanker_No,Tare_Weight,Gross_Weight,Net_Weight,GateEntry_Document_No,Posted,Posting_Date,Created_Date,Modified_Date from TSPL_WEIGHMENT_DETAIL_BULKSALE where 2=2  "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select MIN(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select Max(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No ='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select Min(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No>'" + strCode + "' " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select Max(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No<'" + strCode + "' " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsWeighmentEntry()
            obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            obj.Weighment_Date = clsCommon.myCstr(dt.Rows(0)("Weighment_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.GateEntry_Document_No = clsCommon.myCstr(dt.Rows(0)("GateEntry_Document_No"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
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
            obj.Arr = clsWeighmentEntryChemberNoDetails.GetData(obj.Weighment_No, trans)
        End If

        Return obj
    End Function
    Public Shared Function GetDataforpendingGrossweight(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsWeighmentEntry
        Return GetDataforpendingGrossweight(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetDataforpendingGrossweight(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsWeighmentEntry
        Dim obj As ClsWeighmentEntry = Nothing
        Dim Arr As List(Of ClsWeighmentEntry) = Nothing
        Dim qry As String = "Select SalesOrder_Code,Weighment_No,Weighment_Date,Location_Code,Tanker_No,Tare_Weight,Gross_Weight,Net_Weight,GateEntry_Document_No,Posted  from TSPL_WEIGHMENT_DETAIL_BULKSALE where 2=2 and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ")  "
        Dim whrclas As String = " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select MIN(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select Max(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE WHERE 1=1 " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No ='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select Min(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No>'" + strCode + "' " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No = (select Max(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No<'" + strCode + "' " + whrclas + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsWeighmentEntry()
            obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            obj.Weighment_Date = clsCommon.myCstr(dt.Rows(0)("Weighment_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.GateEntry_Document_No = clsCommon.myCstr(dt.Rows(0)("GateEntry_Document_No"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
            obj.Arr = clsWeighmentEntryChemberNoDetails.GetData(obj.Weighment_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Weighment_Date,Location_Code from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmWeighmentEntry, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Weighment_Date")), trans)

        End If

        Try
            Dim qry As String = "delete from TSPL_WeighmentBulkSale_Chember_Details where Weighment_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
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
                Throw New Exception("Weighment No not found to Post")
            End If
            Dim obj As ClsWeighmentEntry = ClsWeighmentEntry.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmWeighmentEntry, obj.Location_Code, obj.Weighment_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_WEIGHMENT_DETAIL_BULKSALE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Weighment_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsWeighmentEntryChemberNoDetails
#Region "Variables"
    Public Weighment_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Weighment_Sequence As Integer = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsWeighmentEntryChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_WeighmentBulkSale_Chember_Details where Weighment_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsWeighmentEntryChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Weighment_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                clsCommon.AddColumnsForChange(coll, "Weighment_Sequence", obj.Weighment_Sequence)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WeighmentBulkSale_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsWeighmentEntryChemberNoDetails)
        Dim arr As List(Of clsWeighmentEntryChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_WeighmentBulkSale_Chember_Details where TSPL_WeighmentBulkSale_Chember_Details.Weighment_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsWeighmentEntryChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsWeighmentEntryChemberNoDetails = New clsWeighmentEntryChemberNoDetails()
                obj.Weighment_No = clsCommon.myCstr(dr("Weighment_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dr("Net_Weight"))
                obj.Weighment_Sequence = clsCommon.myCdbl(dr("Weighment_Sequence"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

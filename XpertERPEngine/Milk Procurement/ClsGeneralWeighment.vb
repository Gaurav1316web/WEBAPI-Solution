'--------Created By Sanjay Ticket No - BHA/08/08/18-000397 Date - 08/Aug/2018, Client - Bharat Dairy 
Imports System.Data.SqlClient
Imports common
Public Class ClsGeneralWeighment

#Region "variables"
    Public Weighment_No As String = Nothing
    Public Weighment_Date As Date
    Public Transporter_Name_Manual As String = ""
    Public Vehicle_No_Manual As String = ""
    Public Location_Code As String = ""
    Public Remarks As String = ""
    Public Comments As String = ""
    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Modified_Date As Date?
    Public Created_Date As Date?
    Public Item_Code As String = Nothing
    Public IsJobWork As Integer = 0
    Public Is_Empty_Vehicle As Boolean = False
    Public Gate_Entry_No As String = Nothing
    Public Is_Scrap As Boolean = False
    Public Tare_Weight_date As Date?
    Public Gross_Weight_date As Date?
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No as Code,convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) as Date,TSPL_GENERAL_WEIGHMENT_DETAIL.Vehicle_No_Manual as [Vehicle No],TSPL_GENERAL_WEIGHMENT_DETAIL.Transporter_Name_Manual as [Transporter Name],TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork,case when TSPL_GENERAL_WEIGHMENT_DETAIL.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_GENERAL_WEIGHMENT_DETAIL " & _
            "Left Outer Join TSPL_LOCATION_MASTER on TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code =TSPL_LOCATION_MASTER.Location_Code " & _
            "left join tspl_item_master on TSPL_GENERAL_WEIGHMENT_DETAIL.item_code=tspl_item_master.item_code "
        str = clsCommon.ShowSelectForm("Weighment", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

  

    Public Shared Function SaveData(ByVal obj As ClsGeneralWeighment, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsGeneralWeighment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If obj.Gross_Weight > 0 AndAlso obj.Tare_Weight > 0 Then
                If obj.Net_Weight <= 0 Then
                    Throw New Exception("Net Weight should be Positive")
                End If
            ElseIf obj.Gross_Weight <= 0 AndAlso obj.Tare_Weight <= 0 Then
                Throw New Exception("Gross/Tare Weight should be above zero")
            End If

            If isNewEntry Then
                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.GeneralWeighment, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", clsCommon.GetPrintDate(obj.Tare_Weight_date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Gross_Weight_date", clsCommon.GetPrintDate(obj.Gross_Weight_date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", clsCommon.GetPrintDate(obj.Tare_Weight_date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Gross_Weight_date", clsCommon.GetPrintDate(obj.Gross_Weight_date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Transporter_Name_Manual", obj.Transporter_Name_Manual)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No_Manual", obj.Vehicle_No_Manual)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "IsJobWork", obj.IsJobWork)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Is_Empty_Vehicle", IIf(obj.Is_Empty_Vehicle, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No, True)
            clsCommon.AddColumnsForChange(coll, "Is_Scrap", IIf(obj.Is_Scrap, 1, 0))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERAL_WEIGHMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERAL_WEIGHMENT_DETAIL", OMInsertOrUpdate.Update, "TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No='" + obj.Weighment_No + "'", trans)
            End If

            '  trans.Commit()
        Catch err As Exception
            '  trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
   

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsGeneralWeighment
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsGeneralWeighment
        Dim obj As ClsGeneralWeighment = Nothing

        Dim qry As String = "Select TSPL_GENERAL_WEIGHMENT_DETAIL.* from TSPL_GENERAL_WEIGHMENT_DETAIL where 2=2  "

        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No = (select MIN(Weighment_No) from TSPL_GENERAL_WEIGHMENT_DETAIL WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No = (select Max(Weighment_No) from TSPL_GENERAL_WEIGHMENT_DETAIL WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No ='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No = (select Min(Weighment_No) from TSPL_GENERAL_WEIGHMENT_DETAIL where Weighment_No>'" + strCode + "' " + whrclas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No = (select Max(Weighment_No) from TSPL_GENERAL_WEIGHMENT_DETAIL where Weighment_No<'" + strCode + "' " + whrclas + " )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsGeneralWeighment()
            obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            obj.Weighment_Date = clsCommon.myCstr(dt.Rows(0)("Weighment_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.Vehicle_No_Manual = clsCommon.myCstr(dt.Rows(0)("Vehicle_No_Manual"))
            obj.Transporter_Name_Manual = clsCommon.myCstr(dt.Rows(0)("Transporter_Name_Manual"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.IsJobWork = clsCommon.myCdbl(dt.Rows(0)("IsJobWork"))
            obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
            obj.Is_Scrap = (clsCommon.myCdbl(dt.Rows(0)("Is_Scrap")) = 1)
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            obj.Is_Empty_Vehicle = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Empty_Vehicle")) = 1, True, False)
            If dt.Rows(0)("Tare_Weight_date") IsNot DBNull.Value Then
                obj.Tare_Weight_date = clsCommon.myCDate(dt.Rows(0)("Tare_Weight_date"))
            End If
            If dt.Rows(0)("Gross_Weight_date") IsNot DBNull.Value Then
                obj.Gross_Weight_date = clsCommon.myCDate(dt.Rows(0)("Gross_Weight_date"))
            End If
        End If
        Return obj
    End Function
   
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            
            Dim qry As String = "delete from TSPL_GENERAL_WEIGHMENT_DETAIL where Weighment_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function


    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Weighment No not found to Post")
            End If
            Dim obj As ClsGeneralWeighment = ClsGeneralWeighment.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If obj.Gross_Weight <= 0 OrElse obj.Net_Weight <= 0 Or obj.Tare_Weight <= 0 Then
                Throw New Exception("Gross/Tare/Net Weight should be Positive")
            End If

            Dim qry = "Update TSPL_GENERAL_WEIGHMENT_DETAIL set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Weighment_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a Weighment Entry No")
            End If
            Dim Qry As String = "select Posted from TSPL_GENERAL_WEIGHMENT_DETAIL where Weighment_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            'Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & strDocNo & "') xx ", trans)
            'If isUsed > 0 Then
            '    Throw New Exception("Weighment Entry No is in use")
            'End If
            ' select count (*) from TSPL_JWI_ESTIMATION_WEIGHMENT where Weighment_Code = ''
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_JWI_ESTIMATION_WEIGHMENT where Weighment_Code = '" + strDocNo + "'  ", trans)
            If isUsed > 0 Then
                Throw New Exception("Weighment Entry No is in use")
            End If
            Qry = "Update TSPL_GENERAL_WEIGHMENT_DETAIL set Posted = 0,Posting_Date=null where Weighment_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class



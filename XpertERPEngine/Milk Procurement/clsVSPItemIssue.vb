
Imports common
Imports System.Data.SqlClient
Public Class clsVSPItemIssue

#Region "Variables"
    Public Doc_No As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Doc_Type As String = Nothing
    Public From_Location As String = Nothing
    Public From_LocationName As String = Nothing
    Public To_Location As String = Nothing
    Public To_LocationName As String = Nothing
    Public Remarks As String = Nothing
    Public Comment As String = Nothing
    Public Issue_To As String = Nothing
    Public Issue_No As String = Nothing

    Public Issue_ToName As String = Nothing
    Public Request_By As String = Nothing
    Public Request_ByName As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Posting_Date As DateTime?
    Public Req_IssueNo As String = Nothing
    Public RequisitionNo As String = Nothing

    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Tax_Group As String = Nothing
    Public Tax_Desc As String = Nothing

    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public BeforeTax_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public doc_Amt As Double = 0
    Public Vehicle_Id As String = Nothing
    Public Machine_Id As String = Nothing
    Public Rate_Status As Integer = 1

    Public Arr As List(Of clsVSPItemIssueDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsVSPItemIssue, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPItemIssue, obj.From_Location, obj.Doc_Date, trans)
            clsSerializeInvenotry.DeleteData("VSPISSUE", obj.Doc_No, trans)
            clsBatchInventory.DeleteData("MCC-IISSUE", obj.Doc_No, trans)
            Dim qry As String = "delete from TSPL_VSPItem_DETAIL where Doc_No='" + obj.Doc_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.VSPItemIssue, clsDocTransactionType.ItemIssue, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.VSPItemIssue, clsDocTransactionType.ItemReturn, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Transfer") = CompairStringResult.Equal Then
                    Dim strlocation As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
                    Dim chk As String = ""
                    Dim transType As String = clsDocTransactionType.SaleInvoiceExcise
                    chk = clsDBFuncationality.getSingleValue(strlocation, trans)
                    If chk = "T" Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.SaleInvoice, transType, obj.From_Location)
                    Else
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.VSPItemIssue, clsDocTransactionType.ItemTransfer, obj.From_Location)
                    End If

                Else
                    Throw New Exception("Document Type is not correct")
                End If
            End If

            If (clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.From_Location, True)
            clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Location, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Issue_To", obj.Issue_To)
            clsCommon.AddColumnsForChange(coll, "Issue_No", obj.Issue_No)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.Req_IssueNo)
            clsCommon.AddColumnsForChange(coll, "RequisitionNo", obj.RequisitionNo)

            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Desc", obj.Tax_Desc)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "BeforeTax_Amt", obj.BeforeTax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "doc_Amt", obj.doc_Amt)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            'clsCommon.AddColumnsForChange(coll, "Machine_Id", obj.Machine_Id)


            'clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            'clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSPItem_HEAD", OMInsertOrUpdate.Insert, "", trans)
                If obj.Rate_Status = 2 Then
                    'commented by stuti on 01/12/2016 for approval work on shifting
                    'qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                    '"values ('VSP Item Issue','" & clsUserMgtCode.frmVSPItemIssue & "','" & obj.Doc_No & "','" & clsCommon.GetPrintDate(obj.Doc_Date, "dd-MMM-yyyy hh:mm:ss") & "','Rate',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    'end here
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSPItem_HEAD", OMInsertOrUpdate.Update, "TSPL_VSPItem_HEAD.Doc_No='" + obj.Doc_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsVSPItemIssueDetail.SaveData(obj.Doc_No, obj.Doc_Type, obj.From_Location, obj.Doc_Date, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Doc_No, obj.arrCustomFields, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsVSPItemIssue
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsVSPItemIssue
        Dim obj As clsVSPItemIssue = Nothing
        Dim qry As String = "SELECT TSPL_VSPItem_HEAD.Doc_No,TSPL_VSPItem_HEAD.Doc_Date,TSPL_VSPItem_HEAD.Doc_Type,TSPL_VSPItem_HEAD.From_Location,FLocation.Location_Desc as FromLocationName,TSPL_VSPItem_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_VSPItem_HEAD.Status,TSPL_VSPItem_HEAD.On_Hold,TSPL_VSPItem_HEAD.Comment,TSPL_VSPItem_HEAD.Remarks,TSPL_VSPItem_HEAD.Posting_Date,TSPL_VSPItem_HEAD.Issue_To,IssueEmp.Emp_Name as IssueToName ,ISNULL(TSPL_VSPItem_HEAD.Issue_No,'' ) As Issue_No,TSPL_VSPItem_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_VSPItem_HEAD.Dept,TSPL_VSPItem_HEAD.Dept_Desc,TSPL_VSPItem_HEAD.Tax_Group,TSPL_VSPItem_HEAD.tax_desc,TSPL_VSPItem_HEAD.TAX1,TSPL_VSPItem_HEAD.TAX1_Rate,TSPL_VSPItem_HEAD.TAX1_Amt,TSPL_VSPItem_HEAD.TAX1_Base_Amt,TSPL_VSPItem_HEAD.TAX2,TSPL_VSPItem_HEAD.TAX2_Rate,TSPL_VSPItem_HEAD.TAX2_Amt,TSPL_VSPItem_HEAD.TAX2_Base_Amt,TSPL_VSPItem_HEAD.TAX3,TSPL_VSPItem_HEAD.TAX3_Rate,TSPL_VSPItem_HEAD.TAX3_Amt,TSPL_VSPItem_HEAD.TAX3_Base_Amt,TSPL_VSPItem_HEAD.TAX4,TSPL_VSPItem_HEAD.TAX4_Rate,TSPL_VSPItem_HEAD.TAX4_Amt,TSPL_VSPItem_HEAD.TAX4_Base_Amt,TSPL_VSPItem_HEAD.TAX5,TSPL_VSPItem_HEAD.TAX5_Rate,TSPL_VSPItem_HEAD.TAX5_Amt,TSPL_VSPItem_HEAD.TAX5_Base_Amt,TSPL_VSPItem_HEAD.TAX6,TSPL_VSPItem_HEAD.TAX6_Rate,TSPL_VSPItem_HEAD.TAX6_Amt,TSPL_VSPItem_HEAD.TAX6_Base_Amt,TSPL_VSPItem_HEAD.TAX7,TSPL_VSPItem_HEAD.TAX7_Rate,TSPL_VSPItem_HEAD.TAX7_Amt,TSPL_VSPItem_HEAD.TAX7_Base_Amt,TSPL_VSPItem_HEAD.TAX8,TSPL_VSPItem_HEAD.TAX8_Rate,TSPL_VSPItem_HEAD.TAX8_Amt,TSPL_VSPItem_HEAD.TAX8_Base_Amt,TSPL_VSPItem_HEAD.TAX9,TSPL_VSPItem_HEAD.TAX9_Rate,TSPL_VSPItem_HEAD.TAX9_Amt,TSPL_VSPItem_HEAD.TAX9_Base_Amt,TSPL_VSPItem_HEAD.TAX10,TSPL_VSPItem_HEAD.TAX10_Rate,TSPL_VSPItem_HEAD.TAX10_Amt,TSPL_VSPItem_HEAD.TAX10_Base_Amt,TSPL_VSPItem_HEAD.BeforeTax_Amt,TSPL_VSPItem_HEAD.Total_Tax_Amt,TSPL_VSPItem_HEAD.doc_Amt, TSPL_VSPItem_HEAD.vehicle_Id, TSPL_VSPItem_HEAD.Machine_Id,Req_IssueNo,RequisitionNo  FROM TSPL_VSPItem_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on FLocation.Location_Code=TSPL_VSPItem_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on TLocation.Location_Code=TSPL_VSPItem_HEAD.To_Location left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on IssueEmp.EMP_CODE= TSPL_VSPItem_HEAD.issue_To left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_VSPItem_HEAD.Request_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VSPItem_HEAD.Doc_No = (select MIN(Doc_No) from TSPL_VSPItem_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_VSPItem_HEAD.Doc_No = (select Max(Doc_No) from TSPL_VSPItem_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_VSPItem_HEAD.Doc_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_VSPItem_HEAD.Doc_No = (select Min(Doc_No) from TSPL_VSPItem_HEAD where Doc_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_VSPItem_HEAD.Doc_No = (select Max(Doc_No) from TSPL_VSPItem_HEAD where Doc_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVSPItemIssue()
            obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.Doc_Date = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
            obj.From_LocationName = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
            obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
            obj.To_LocationName = clsCommon.myCstr(dt.Rows(0)("ToLocationName"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
            obj.Issue_To = clsCommon.myCstr(dt.Rows(0)("Issue_To"))
            obj.Issue_No = clsCommon.myCstr(dt.Rows(0)("Issue_No"))
            obj.Issue_ToName = clsCommon.myCstr(dt.Rows(0)("IssueToName"))
            obj.Request_By = clsCommon.myCstr(dt.Rows(0)("Request_By"))
            obj.Request_ByName = clsCommon.myCstr(dt.Rows(0)("RequestByName"))
            obj.Req_IssueNo = clsCommon.myCstr(dt.Rows(0)("Req_IssueNo"))
            obj.RequisitionNo = clsCommon.myCstr(dt.Rows(0)("RequisitionNo"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            'obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            'obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))

            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Tax_Desc = clsCommon.myCstr(dt.Rows(0)("Tax_Desc"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.BeforeTax_Amt = clsCommon.myCdbl(dt.Rows(0)("BeforeTax_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.doc_Amt = clsCommon.myCdbl(dt.Rows(0)("doc_Amt"))
            obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
            'obj.Machine_Id = clsCommon.myCstr(dt.Rows(0)("Machine_Id"))






            qry = "SELECT TSPL_VSPItem_DETAIL.Doc_No,TSPL_VSPItem_DETAIL.Line_No,TSPL_VSPItem_DETAIL.Item_Code,TSPL_VSPItem_DETAIL.Item_Desc,TSPL_VSPItem_DETAIL.Required_Qty,TSPL_VSPItem_DETAIL.Unit_code,TSPL_VSPItem_DETAIL.Issued_Qty , TSPL_VSPItem_DETAIL.TAX1,TSPL_VSPItem_DETAIL.TAX1_Rate,TSPL_VSPItem_DETAIL.TAX1_Amt,TSPL_VSPItem_DETAIL.TAX2,TSPL_VSPItem_DETAIL.TAX2_Rate,TSPL_VSPItem_DETAIL.TAX2_Amt,TSPL_VSPItem_DETAIL.TAX3,TSPL_VSPItem_DETAIL.TAX3_Rate,TSPL_VSPItem_DETAIL.TAX3_Amt,TSPL_VSPItem_DETAIL.TAX4,TSPL_VSPItem_DETAIL.TAX4_Rate,TSPL_VSPItem_DETAIL.TAX4_Amt,TSPL_VSPItem_DETAIL.TAX5,TSPL_VSPItem_DETAIL.TAX5_Rate,TSPL_VSPItem_DETAIL.TAX5_Amt,TSPL_VSPItem_DETAIL.TAX6,TSPL_VSPItem_DETAIL.TAX6_Rate,TSPL_VSPItem_DETAIL.TAX6_Amt,TSPL_VSPItem_DETAIL.TAX7,TSPL_VSPItem_DETAIL.TAX7_Rate,TSPL_VSPItem_DETAIL.TAX7_Amt,TSPL_VSPItem_DETAIL.TAX8,TSPL_VSPItem_DETAIL.TAX8_Rate,TSPL_VSPItem_DETAIL.TAX8_Amt,TSPL_VSPItem_DETAIL.TAX9,TSPL_VSPItem_DETAIL.TAX9_Rate,TSPL_VSPItem_DETAIL.TAX9_Amt,TSPL_VSPItem_DETAIL.TAX10,TSPL_VSPItem_DETAIL.TAX10_Rate,TSPL_VSPItem_DETAIL.TAX10_Amt,TSPL_VSPItem_DETAIL.TAX1_Base_Amt,TSPL_VSPItem_DETAIL.TAX2_Base_Amt,TSPL_VSPItem_DETAIL.TAX3_Base_Amt,TSPL_VSPItem_DETAIL.TAX4_Base_Amt,TSPL_VSPItem_DETAIL.TAX5_Base_Amt,TSPL_VSPItem_DETAIL.TAX6_Base_Amt,TSPL_VSPItem_DETAIL.TAX7_Base_Amt,TSPL_VSPItem_DETAIL.TAX8_Base_Amt,TSPL_VSPItem_DETAIL.TAX9_Base_Amt,TSPL_VSPItem_DETAIL.TAX10_Base_Amt ,TSPL_VSPItem_DETAIL.Amount,TSPL_VSPItem_DETAIL.Total_Tax_Amt,TSPL_VSPItem_DETAIL.Item_Net_Amt,TSPL_VSPItem_DETAIL.Unit_Cost,Issued_Qty_AgainstRet, TSPL_VSPItem_DETAIL.Req_IssueNo, TSPL_VSPItem_DETAIL.Cost_Code FROM TSPL_VSPItem_DETAIL where TSPL_VSPItem_DETAIL.Doc_No='" + obj.Doc_No + "' ORDER BY TSPL_VSPItem_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsVSPItemIssueDetail)
                Dim objTr As clsVSPItemIssueDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsVSPItemIssueDetail
                    objTr.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Cost_Code = clsCommon.myCstr(dr("Cost_Code"))
                    objTr.Required_Qty = clsCommon.myCdbl(dr("Required_Qty"))
                    objTr.Issued_Qty = clsCommon.myCdbl(dr("Issued_Qty"))
                    objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(dr("Issued_Qty_AgainstRet"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Unit_Cost = clsCommon.myCdbl(dr("Unit_Cost"))
                    objTr.Req_IssueNo = clsCommon.myCstr(dr("Req_IssueNo"))

                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("VSPISSUE", objTr.Doc_No, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("MCC-IISSUE", objTr.Doc_No, objTr.Item_Code, objTr.Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isTransOutSide As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isTransOutSide = False
        Else
            isTransOutSide = True
        End If

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            Dim PostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            Dim obj As clsVSPItemIssue = clsVSPItemIssue.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPItemIssue, obj.From_Location, obj.Doc_Date, trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            'If (obj.On_Hold) Then
            '    Throw New Exception("Document No " + obj.Doc_No + " Is On Hold.Can't Post it")
            'End If
            '--------------For From Location (Only Out Entry For DocType Issue)-----------------------------------------------------------------------------
            If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Issue") = CompairStringResult.Equal Then
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                For Each objTr As clsVSPItemIssueDetail In obj.Arr
                    Dim objLocationDetails As New clsItemLocationDetails()
                    objLocationDetails.Item_Code = objTr.Item_Code
                    objLocationDetails.Item_Desc = objTr.Item_Desc
                    objLocationDetails.Location_Code = obj.From_Location
                    objLocationDetails.Location_Desc = obj.From_LocationName
                    objLocationDetails.Item_Qty = IIf(obj.Doc_Type = "Return", -1 * objTr.Issued_Qty_AgainstRet, -1 * objTr.Issued_Qty) ' -1 * (objTr.Issued_Qty)
                    objLocationDetails.Amount = -1 * (objTr.Amount)

                    'objLocationDetails.MRP = 0
                    'If objTr.MFG_Date.HasValue Then
                    '    objLocationDetails.MFG_Date = objTr.MFG_Date
                    'End If
                    'objLocationDetails.Batch_No = objTr.Batch_No
                    'If objTr.Expiry_Date.HasValue Then
                    '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                    'End If

                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objLocationDetails.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objLocationDetails.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objLocationDetails.ItemType = "FT"
                    End If
                    'objLocationDetails.ItemType = itemtype
                    ArrLocationDetails.Add(objLocationDetails)
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "O"
                    objInventoryMovemnt.Location_Code = obj.From_Location

                    objInventoryMovemnt.Vendor_Code = obj.Issue_To
                    objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Issue_To, trans)

                    objInventoryMovemnt.Other_Location_Code = obj.To_Location
                    objInventoryMovemnt.Other_Location_Desc = obj.To_LocationName

                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty)
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                    'objInventoryMovemnt.FIFO_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, objTr.Item_Code, obj.From_Location, objTr.Issued_Qty, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    'objInventoryMovemnt.LIFO_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, objTr.Item_Code, obj.From_Location, objTr.Issued_Qty, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    'objInventoryMovemnt.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.From_Location, objTr.Issued_Qty, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), True, trans)

                    objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                    objInventoryMovemnt.Net_Cost = objTr.Item_Net_Amt
                    'If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                    '    objInventoryMovemnt.ItemType = "RM"
                    'ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                    '    objInventoryMovemnt.ItemType = "OT"
                    'ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                    '    objInventoryMovemnt.ItemType = "FT"
                    'End If
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    ElseIf clsCommon.CompairString(type, "A") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "A"
                    End If
                    'objInventoryMovemnt.ItemType = itemtype
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                Next
                'isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(PostDate, ArrLocationDetails, trans)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-IISSUE", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement, trans)
            End If
            'For To Location (Only In Entry For DocType Return)-----------------------------------------------------------------------------------------------------------------------
            If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Return") = CompairStringResult.Equal Then
                Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                For Each objTr As clsVSPItemIssueDetail In obj.Arr
                    Dim objLocationDetails1 As New clsItemLocationDetails()
                    objLocationDetails1.Item_Code = objTr.Item_Code
                    objLocationDetails1.Item_Desc = objTr.Item_Desc
                    objLocationDetails1.Location_Code = obj.From_Location 'obj.To_Location
                    objLocationDetails1.Location_Desc = obj.From_LocationName 'obj.To_LocationName
                   
                    objLocationDetails1.Item_Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) ' objTr.Issued_Qty
                    objLocationDetails1.Amount = objTr.Amount
                    'objLocationDetails.MRP = 0
                    'If objTr.MFG_Date.HasValue Then
                    '    objLocationDetails.MFG_Date = objTr.MFG_Date
                    'End If
                    'objLocationDetails.Batch_No = objTr.Batch_No
                    'If objTr.Expiry_Date.HasValue Then
                    '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                    'End If

                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objLocationDetails1.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objLocationDetails1.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objLocationDetails1.ItemType = "FT"
                    End If
                    'objLocationDetails.ItemType = itemtype
                    ArrLocationDetails1.Add(objLocationDetails1)


                    Dim objInventoryMovemnt1 As New clsInventoryMovement()
                    objInventoryMovemnt1.InOut = "I"
                    objInventoryMovemnt1.Location_Code = obj.From_Location 'obj.To_Location
                    objInventoryMovemnt1.Item_Code = objTr.Item_Code
                    objInventoryMovemnt1.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt1.Other_Location_Code = obj.To_Location
                    objInventoryMovemnt1.Other_Location_Desc = obj.To_LocationName

                    objInventoryMovemnt1.Vendor_Code = obj.Issue_To
                    objInventoryMovemnt1.Vendor_Name = clsVendorMaster.GetName(obj.Issue_To, trans)


                    objInventoryMovemnt1.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) 'objTr.Issued_Qty
                    objInventoryMovemnt1.UOM = objTr.Unit_code
                    objInventoryMovemnt1.Basic_Cost = objTr.Unit_Cost
                    ''''objInventoryMovemnt.Rec_Cost= objTr.MRP
                    objInventoryMovemnt1.Add_Cost = objTr.Total_Tax_Amt
                    objInventoryMovemnt1.Net_Cost = objTr.Item_Net_Amt
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt1.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt1.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt1.ItemType = "FT"
                    End If
                    ' objInventoryMovemnt.ItemType = itemtype
                    ArrInventoryMovement1.Add(objInventoryMovemnt1)
                Next
                isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(PostDate, ArrLocationDetails1, trans)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-IISSUE", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement1, trans)
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                If obj.Doc_Type = "Transfer" Then
                    Dim Sql As String = ""
                    Dim strFromInvAcc As String = ""
                    Dim strFromInvAccDesc As String = ""
                    Dim strToInvAcc As String = ""
                    Dim strToInvAccDesc As String = ""
                    Dim strShpClrAcc As String = ""
                    Dim strShpClrAccDesc As String = ""
                    Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
                    Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
                    Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.To_Location) + "'"
                    Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                   " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
                    strFromInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                    strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
                    strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
                    If strFromInvAccDesc Is Nothing Then
                        Throw New Exception("Inventory Control Account " + strFromInvAcc + " not found.")
                    End If
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
                    strToInvAccDesc = connectSql.RunScalar(trans, Sql)
                    If strToInvAccDesc Is Nothing Then
                        Throw New Exception("Inventory Control Account " + strToInvAcc + " not found.")
                    End If
                    strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, obj.From_Location, trans)
                    Dim objSeg As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
                    ' connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    Dim arrlist As New ArrayList()
                    Dim BankAcc() As String = {strFromInvAcc, obj.BeforeTax_Amt * (-1)}
                    arrlist.Add(BankAcc)


                    Dim taxAmt As Decimal
                    Dim ttlTotalTaxAmt As Decimal = 0
                    Dim strTaxCode As String
                    Dim strNetPayAcc As String = ""
                    Dim strNetPayAccDesc As String = ""

                    '''' Tax1 ''''''***********************
                    strTaxCode = obj.TAX1
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX1_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX1_Amt), 2)
                        Else
                            If obj.TAX1_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX1_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax1

                    '''' Tax2 ''''''***********************
                    strTaxCode = obj.TAX2
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX2_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX2_Amt), 2)
                        Else
                            If obj.TAX2_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX2_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax2

                    '''' Tax3 ''''''***********************
                    strTaxCode = obj.TAX3
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX3_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX3_Amt), 2)
                        Else
                            If obj.TAX3_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX3_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax3

                    '''' Tax4 ''''''***********************
                    strTaxCode = obj.TAX4
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX4_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX4_Amt), 2)
                        Else
                            If obj.TAX4_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX4_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            ' connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax4

                    '''' Tax5 ''''''***********************
                    strTaxCode = obj.TAX5
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX5_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX5_Amt), 2)
                        Else
                            If obj.TAX5_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX5_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax5

                    '''' Tax6 ''''''***********************
                    strTaxCode = obj.TAX6
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX6_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX6_Amt), 2)
                        Else
                            If obj.TAX5_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX6_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax6

                    '''' Tax7 ''''''***********************
                    strTaxCode = obj.TAX7
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        If obj.TAX7_Amt.ToString().Substring(0, 1) = "-" Then
                            taxAmt = Math.Round(CDec(obj.TAX7_Amt), 2)
                        Else
                            If obj.TAX5_Amt = 0 Then
                                taxAmt = 0
                            Else
                                taxAmt = Math.Round(CDec(obj.TAX7_Amt), 2)
                            End If
                        End If
                        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                        End If
                        If Not strNetPayAcc = "" Then
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                        End If
                        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                            'lineNo = lineNo + 1
                            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                            arrlist.Add(Tax1)
                        End If
                        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                    End If
                    '*********** End Tax7

                    ' fromShipmentCogs = fromShipmentCogs + ttlTotalTaxAmt

                    strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.To_Location, trans)
                    objSeg = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
                    'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    'lineNo = lineNo + 1

                    Dim DocAmtWithTax() As String = {strToInvAcc, obj.doc_Amt}
                    arrlist.Add(DocAmtWithTax)


                    Dim strFrmFilledAcc As String = Nothing
                    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
                    Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmFilledAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
                    Else
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                        Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                        'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                        'lineNo = lineNo + 1
                        Dim FrmFilledAcc() As String = {strFrmFilledAcc, obj.doc_Amt}
                        arrlist.Add(FrmFilledAcc)
                    End If
                    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
                    Dim strToFilledAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strToFilledAcc Is Nothing Then
                        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
                    Else
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                        Dim strTOFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                        'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                        'lineNo = lineNo + 1
                        Dim ToFilledAcc() As String = {strToFilledAcc, obj.doc_Amt * -1}
                        arrlist.Add(ToFilledAcc)
                    End If
                    '' BHA/30/10/18-000646 RICHA AGARWAL SEND vENDOR CODE AND VENDOR NAME INTO JOURNAL ENTRY AND TYPE V instead of C 30 Oct,2018
                    transportSql.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "VSP-TF", "VSP Transfer", obj.Doc_No, "", "V", obj.Issue_To, clsVendorMaster.GetName(obj.Issue_To, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                End If
            End If
            
            Dim qry As String = "Update TSPL_VSPItem_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isTransOutSide = False Then
                If isSaved Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveDebitNoteEntry(ByVal strDocNo As String, ByVal DebitCredit As String, ByVal trans As SqlTransaction)
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        Dim obj As clsVSPItemIssue = clsVSPItemIssue.GetData(strDocNo, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        'Dim RejLoc As Integer = clsDBFuncationality.getSingleValue("select count(Rejected_Location) from TSPL_LOCATION_MASTER where Location_Code='" & obj.from_location & "'", trans)
        'If RejLoc <= 0 Then
        '    Throw New Exception("Rejected Location Not filled of [" + obj.From_Location + "]")
        'End If
        'Dim Rej_loc As String = clsDBFuncationality.getSingleValue("select Rejected_Location from TSPL_LOCATION_MASTER where Location_Code='" & obj.from_location & "'", trans)
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.Issue_To & "'", trans)
        objVendorInvHead.Document_No = obj.Doc_No 'clsDBFuncationality.getSingleValue("select document_no from tspl_vendor_invoice_head where against_Poinvoice_no='" & obj.PI_No & "' and document_type='D'", trans) 'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = obj.Issue_To
        objVendorInvHead.Vendor_Name = vendor_name 'obj.Issue_ToName
        objVendorInvHead.Vendor_Invoice_No = obj.Doc_No '
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy")
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.From_Location, trans)
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Issue_To + "'", trans))
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Issue_To)
        End If
        If clsCommon.CompairString(DebitCredit, "D") = CompairStringResult.Equal Then
            objVendorInvHead.Document_Type = "D" ''Purchase Return will make Debit Note of PIInvoice
            objVendorInvHead.RefDocType = "V_I_Issue"
            objVendorInvHead.Description = "Against VSP Item Issue No " + obj.Doc_No
        ElseIf clsCommon.CompairString(DebitCredit, "C") = CompairStringResult.Equal Then
            objVendorInvHead.Document_Type = "C" ''Purchase Return will make Debit Note of PIInvoice
            objVendorInvHead.RefDocType = "V_I_Issue_Return"
            objVendorInvHead.Description = "Against VSP Item Issue Return No " + obj.Doc_No
        Else
            Throw New Exception("Document Type must be D or C")
        End If

        objVendorInvHead.RefDocNo = obj.Doc_No
        '' '' priti ends here
        'objVendorInvHead.Order_No = txtOrderNo.Text
        objVendorInvHead.Total_Tax = obj.Total_Tax_Amt '* rejqty

        objVendorInvHead.On_Hold = False
        'objVendorInvHead.Description = "Against VSP Item Issue No " + obj.Doc_No
        objVendorInvHead.Tax_Calculation_Type = 0
        objVendorInvHead.Tax_Group = obj.Tax_Group
        If (clsCommon.myLen(obj.TAX1) > 0) Then
            objVendorInvHead.TAX1 = obj.TAX1
            objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
        End If
        If (clsCommon.myLen(obj.TAX2) > 0) Then
            objVendorInvHead.TAX2 = obj.TAX2
            objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
        End If
        If (clsCommon.myLen(obj.TAX3) > 0) Then
            objVendorInvHead.TAX3 = obj.TAX3
            objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
        End If
        If (clsCommon.myLen(obj.TAX4) > 0) Then
            objVendorInvHead.TAX4 = obj.TAX4
            objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
        End If
        If (clsCommon.myLen(obj.TAX5) > 0) Then
            objVendorInvHead.TAX5 = obj.TAX5
            objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
        End If
        If (clsCommon.myLen(obj.TAX6) > 0) Then
            objVendorInvHead.TAX6 = obj.TAX6
            objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
        End If
        If (clsCommon.myLen(obj.TAX7) > 0) Then
            objVendorInvHead.TAX7 = obj.TAX7
            objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
        End If
        If (clsCommon.myLen(obj.TAX8) > 0) Then
            objVendorInvHead.TAX8 = obj.TAX8
            objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
        End If
        If (clsCommon.myLen(obj.TAX9) > 0) Then
            objVendorInvHead.TAX9 = obj.TAX9
            objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
        End If
        If (clsCommon.myLen(obj.TAX10) > 0) Then
            objVendorInvHead.TAX10 = obj.TAX10
            objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
        End If

        objVendorInvHead.Terms_Code = Nothing 'obj.Terms_Code
        objVendorInvHead.Terms_Description = Nothing 'obj.TermsName
        objVendorInvHead.Due_Date = obj.Doc_Date
        objVendorInvHead.Discount_Base = Nothing ' obj.Discount_Base
        objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
        objVendorInvHead.Amount_Less_Discount = obj.doc_Amt
        objVendorInvHead.Document_Total = obj.doc_Amt
        objVendorInvHead.Balance_Amt = obj.doc_Amt

        objVendorInvHead.Against_VSPItemIssue_No = obj.Doc_No
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
            End If
        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

        'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

        'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

        'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

        'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

        'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

        'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

        'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

        'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

        'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

        'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

        objVendorInvHead.Empty_Amount = 0

        Dim ii As Integer = 0
        For Each objPIDetail As clsVSPItemIssueDetail In obj.Arr
            ''Fill VendorInvoice details Data
            ''qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"
            qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ")")
            End If
            ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If
            ''Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))

            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.From_Location, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))



            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.From_Location, trans)
            End If

            ''If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
            ''    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
            ''    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PR_Qty
            ''End If

            ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
            ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
            ''End If

            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            objVendorInvDetail.Amount = objPIDetail.Amount
            objVendorInvDetail.Discount_Per = 0 'objPIDetail.Disc_Per
            objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
            objVendorInvDetail.Amount_less_Discount = objPIDetail.Amount 'objPIDetail.Amt_Less_Discount
            objVendorInvDetail.TAX1 = objPIDetail.TAX1
            objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
            objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt
            objVendorInvDetail.TAX2 = objPIDetail.TAX2
            objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
            objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt
            objVendorInvDetail.TAX3 = objPIDetail.TAX3
            objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
            objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt
            objVendorInvDetail.TAX4 = objPIDetail.TAX4
            objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
            objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt
            objVendorInvDetail.TAX5 = objPIDetail.TAX5
            objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
            objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt
            objVendorInvDetail.TAX6 = objPIDetail.TAX6
            objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
            objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt
            objVendorInvDetail.TAX7 = objPIDetail.TAX7
            objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
            objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt
            objVendorInvDetail.TAX8 = objPIDetail.TAX8
            objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
            objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt
            objVendorInvDetail.TAX9 = objPIDetail.TAX9
            objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
            objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt
            objVendorInvDetail.TAX10 = objPIDetail.TAX10
            objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
            objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt
            objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt
            objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt
            objVendorInvDetail.Landed_Amount = objPIDetail.Amount 'objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
            ''End of Fill Vendor Invoice Detail Data
        Next



        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If

        'If obj.objPIRemittance IsNot Nothing Then
        '    objVendorInvHead.RemittanceObject = New clsRemittance()
        '    objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
        '    objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
        '    objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
        '    objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
        '    objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
        '    objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
        '    objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
        '    objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
        '    objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
        '    objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
        '    objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
        '    objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
        '    objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
        '    objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
        '    objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
        '    objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
        '    objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
        '    objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
        '    objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
        '    objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
        '    objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
        '    objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
        '    objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
        '    objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
        '    objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
        '    objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
        '    objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
        '    objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
        '    objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
        '    objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
        '    objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

        '    objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
        '    objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
        '    objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
        '    objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
        '    objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
        '    objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
        '    objVendorInvHead.Balance_Amt = obj.PI_Total_Amt - obj.objPIRemittance.Actual_Total_TDS
        '    objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
        'End If
        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            Throw New Exception("No GL Account Found For AP Invoice")
        End If
        isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsVSPItemIssue = clsVSPItemIssue.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPItemIssue, obj.From_Location, obj.Doc_Date, trans)

                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("VSPISSUE", strCode, trans)
                clsBatchInventory.DeleteData("MCC-IISSUE", strCode, trans)
                Dim qry As String = "delete from TSPL_VSPItem_DETAIL where Doc_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_VSPItem_HEAD where Doc_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
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

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String
            Dim obj As clsVSPItemIssue = clsVSPItemIssue.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Reverse")
            End If

            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPItemIssue, obj.From_Location, obj.Doc_Date, trans)

            If Not obj.Status = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                For Each objtr As clsVSPItemIssueDetail In obj.Arr
                    If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                        For Each objBatch As clsBatchInventory In objtr.arrBatchItem
                            Dim dblBalance As Double = clsBatchInventory.GetBatchBalance(objBatch.Item_Code, objBatch.Location_Code, objBatch.Batch_No, objBatch.MRP, objBatch.UOM, objBatch.Document_Code, objBatch.Document_Type, trans)
                            If dblBalance < objBatch.Qty Then
                                Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objBatch.Qty) + Environment.NewLine + "Item : " + objBatch.Item_Code + Environment.NewLine + "Batch : " + objBatch.Batch_No + Environment.NewLine + "MRP : " + clsCommon.myCstr(objBatch.MRP) + Environment.NewLine + "Unit : " + objBatch.UOM)
                            End If
                        Next
                    End If
                Next
            End If

            qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='MCC-IISSUE' and document_code='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update tspl_serial_item set against_inv_movement_trans_id=NULL where document_type='VSPISSUE' and document_code='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_INVENTORY_MOVEMENT_NEW WHERE Trans_Type = 'MCC-IISSUE' AND Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_INVENTORY_MOVEMENT WHERE Trans_Type = 'MCC-IISSUE' AND Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim CurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            For Each objTr As clsVSPItemIssueDetail In obj.Arr
                '--------------For From Location-----------------------------------------------------------------------------
                qry = "update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=Item_Qty+" + clsCommon.myCstr(objTr.Issued_Qty) + ", Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + CurrentDate + "'"
                qry += " WHERE Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'For To Location-----------------------------------------------------------------------------------------------------------------------
                qry = "update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=Item_Qty-" + clsCommon.myCstr(objTr.Issued_Qty) + ", Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + CurrentDate + "'"
                qry += " WHERE Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.To_Location + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next


            If obj.Doc_Type = "Transfer" Then
                qry = "Delete TSPL_JOURNAL_DETAILS WHERE Voucher_No=(Select Voucher_No from TSPL_JOURNAL_MASTER WHERE Source_Doc_No='" + obj.Doc_No + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "Delete from TSPL_JOURNAL_MASTER WHERE Source_Doc_No='" + obj.Doc_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            qry = "Update TSPL_VSPItem_HEAD set Status=0, Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + strCode + "' and trans_code='" + clsCommon.myCstr(clsUserMgtCode.frmVSPItemIssue) + "' and is_reverse=0"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsVSPItemIssueDetail
#Region "Variables"
    Public Doc_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Cost_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Required_Qty As Double = 0
    Public Issued_Qty As Double = 0
    Public Issued_Qty_AgainstRet As Double = 0
    Public Unit_code As String = Nothing
    Public Unit_Cost As Double = 0
    Public Req_IssueNo As String = Nothing

    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Amount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing


#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strDocType As String, ByVal strFromLocation As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsVSPItemIssueDetail), ByVal trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsVSPItemIssueDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                If clsCommon.myLen(obj.Cost_Code) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Cost_Code", obj.Cost_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Required_Qty", obj.Required_Qty)
                clsCommon.AddColumnsForChange(coll, "Issued_Qty", obj.Issued_Qty)
                clsCommon.AddColumnsForChange(coll, "Issued_Qty_AgainstRet", obj.Issued_Qty_AgainstRet)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Unit_Cost", obj.Unit_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.Req_IssueNo)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSPItem_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                If clsCommon.CompairString(strDocType, "Issue") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.SaveData("VSPISSUE", strDocNo, dtDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)
                    clsBatchInventory.SaveData("MCC-IISSUE", strDocNo, dtDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                ElseIf clsCommon.CompairString(strDocType, "Return") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.SaveData("VSPISSUE", strDocNo, dtDocDate, "I", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)
                    clsBatchInventory.SaveData("MCC-IISSUE", strDocNo, dtDocDate, "I", obj.Item_Code, strFromLocation, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                End If


            Next
        End If
        Return True
    End Function
End Class

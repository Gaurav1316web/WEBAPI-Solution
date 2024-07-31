''--27/08/2012--Updation By--[Pankaj Kumar]--Applied GL security While Navigating Document Finder-------Fwd By--Ranjana Mam
Imports common
Imports System.Data.SqlClient
Public Class clsIssueReturnHead

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
    Public Issue_To_Franchise As String = Nothing
    Public Issue_To_Franchise_Name As String = Nothing
    Public Against_Month_End As Boolean = False
    Public Against_Month_End_Retun_No As String

    Public Is_Reject As Boolean = False
    Public Reject_Vendor_Code As String = Nothing

    Public Arr As List(Of clsIssueReturnDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public Is_Skip_Dept_Indent_Balance As Boolean = False

    Public PurchaseInvoice_No As String = Nothing
    Public Is_Reprocess As Boolean = False

    Public Capex_Code As String = Nothing
    Public Capex_SubCode As String = Nothing
    Public CosCenter_Unit As String = Nothing
    Public CostCenter_Type As String = Nothing
    Public Againt_Cleaning_No As String = Nothing
    Public Tanker_Cleaning_Item_No As String = Nothing
#End Region

    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_IssueReturn_HEAD", "Doc_No", "TSPL_IssueReturn_DETAIL", "Doc_No", trans)
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsIssueReturnHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsIssueReturnHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnIssueReturn, obj.From_Location, obj.Doc_Date, trans)
            clsSerializeInvenotry.DeleteData("ISSTRAN", obj.Doc_No, trans)

            clsBatchInventory.DeleteData("ISSTRAN", obj.Doc_No, trans)

            If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                If Not obj.Is_Skip_Dept_Indent_Balance Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LinkDepartmentBetweenIndentAndIssue, clsFixedParameterCode.LinkDepartmentBetweenIndentAndIssue, trans)) = 1 Then
                        For Each objtr As clsIssueReturnDetail In obj.Arr
                            Dim depBalanceQty As Double = clsIssueReturnHead.GetDeptBalanceAgainstDept(obj.From_Location, obj.Dept, obj.Doc_No, objtr.Item_Code, objtr.Unit_code, obj.Doc_Date, trans)
                            If objtr.Issued_Qty > depBalanceQty Then
                                Throw New Exception("Department : " + obj.Dept + " Indent Balance quatity : " + clsCommon.myCstr(depBalanceQty) + " and Issue quatity : " + clsCommon.myCstr(objtr.Issued_Qty))
                            End If
                        Next
                    End If
                End If
            End If
            If Not isNewEntry Then
                HistoryUpdate(obj.Doc_No, trans)
            End If

            Dim qry As String = "delete from TSPL_IssueReturn_DETAIL where Doc_No='" + obj.Doc_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    If obj.Is_Reprocess Then ''new document series is generated in case of reprocess
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.IssueReturn, clsDocTransactionType.ItemIssue_Reprocess, obj.From_Location)
                    Else
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.IssueReturn, clsDocTransactionType.ItemIssue, obj.From_Location)
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.IssueReturn, clsDocTransactionType.ItemReturn, obj.To_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.IssueReturn, clsDocTransactionType.TransferCapex, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Transfer") = CompairStringResult.Equal Then
                    Dim strlocation As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
                    Dim chk As String = ""
                    Dim transType As String = clsDocTransactionType.SaleInvoiceExcise
                    chk = clsDBFuncationality.getSingleValue(strlocation, trans)
                    If chk = "T" Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.SaleInvoice, transType, obj.From_Location)
                    Else
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.IssueReturn, clsDocTransactionType.ItemTransfer, obj.From_Location)
                    End If

                Else
                    Throw New Exception("Document Type is not correct")
                End If
            End If

            If (clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.From_Location)
            clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Location)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Issue_To", obj.Issue_To)
            clsCommon.AddColumnsForChange(coll, "Issue_To_Franchise", obj.Issue_To_Franchise)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.Req_IssueNo)
            clsCommon.AddColumnsForChange(coll, "RequisitionNo", obj.RequisitionNo)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Is_Skip_Dept_Indent_Balance", IIf(obj.Is_Skip_Dept_Indent_Balance, 1, 0))
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
            clsCommon.AddColumnsForChange(coll, "Against_Month_End", IIf(obj.Against_Month_End, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Against_Month_End_Retun_No", obj.Against_Month_End_Retun_No)
            clsCommon.AddColumnsForChange(coll, "Is_Reject", IIf(obj.Is_Reject, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Reject_Vendor_Code", obj.Reject_Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
            clsCommon.AddColumnsForChange(coll, "Is_Reprocess", IIf(obj.Is_Reprocess = True, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
            clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
            clsCommon.AddColumnsForChange(coll, "Cost_Center_Unit", obj.CosCenter_Unit, True)
            clsCommon.AddColumnsForChange(coll, "Cost_Center_Type", obj.CostCenter_Type, True)
            clsCommon.AddColumnsForChange(coll, "Againt_Cleaning_No", obj.Againt_Cleaning_No, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_Cleaning_Item_No", obj.Tanker_Cleaning_Item_No, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_HEAD", OMInsertOrUpdate.Update, "TSPL_IssueReturn_HEAD.Doc_No='" + obj.Doc_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsIssueReturnDetail.SaveData(obj.Doc_No, obj.From_Location, obj.To_Location, obj.Doc_Date, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Doc_No, obj.arrCustomFields, trans)
            If isNewEntry AndAlso clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Req_IssueNo) > 0 AndAlso (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOnlyOneIssueAgainstStoreRequisition, clsFixedParameterCode.AllowOnlyOneIssueAgainstStoreRequisition, trans)) = 1) Then
                isSaved = isSaved AndAlso clsRequistionHead.CloseprData(obj.Req_IssueNo, "Y", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsIssueReturnHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsIssueReturnHead
        Dim obj As clsIssueReturnHead = Nothing
        Dim qry As String = "SELECT TSPL_IssueReturn_HEAD.Capex_Code,TSPL_IssueReturn_HEAD.Capex_SubCode, TSPL_IssueReturn_HEAD.Is_Reprocess,TSPL_IssueReturn_HEAD.Issue_To_Franchise,TSPL_VENDOR_MASTER .Vendor_Name  ,TSPL_IssueReturn_HEAD.Doc_No,TSPL_IssueReturn_HEAD.Doc_Date,TSPL_IssueReturn_HEAD.Doc_Type,TSPL_IssueReturn_HEAD.From_Location,FLocation.Location_Desc as FromLocationName,TSPL_IssueReturn_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_IssueReturn_HEAD.Status,TSPL_IssueReturn_HEAD.On_Hold,TSPL_IssueReturn_HEAD.Comment,TSPL_IssueReturn_HEAD.Remarks,TSPL_IssueReturn_HEAD.Posting_Date,TSPL_IssueReturn_HEAD.Issue_To,IssueEmp.Emp_Name as IssueToName ,TSPL_IssueReturn_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_IssueReturn_HEAD.Dept,TSPL_IssueReturn_HEAD.Dept_Desc,TSPL_IssueReturn_HEAD.Tax_Group,TSPL_IssueReturn_HEAD.tax_desc,TSPL_IssueReturn_HEAD.TAX1,TSPL_IssueReturn_HEAD.TAX1_Rate,TSPL_IssueReturn_HEAD.TAX1_Amt,TSPL_IssueReturn_HEAD.TAX1_Base_Amt,TSPL_IssueReturn_HEAD.TAX2,TSPL_IssueReturn_HEAD.TAX2_Rate,TSPL_IssueReturn_HEAD.TAX2_Amt,TSPL_IssueReturn_HEAD.TAX2_Base_Amt,TSPL_IssueReturn_HEAD.TAX3,TSPL_IssueReturn_HEAD.TAX3_Rate,TSPL_IssueReturn_HEAD.TAX3_Amt,TSPL_IssueReturn_HEAD.TAX3_Base_Amt,TSPL_IssueReturn_HEAD.TAX4,TSPL_IssueReturn_HEAD.TAX4_Rate,TSPL_IssueReturn_HEAD.TAX4_Amt,TSPL_IssueReturn_HEAD.TAX4_Base_Amt,TSPL_IssueReturn_HEAD.TAX5,TSPL_IssueReturn_HEAD.TAX5_Rate,TSPL_IssueReturn_HEAD.TAX5_Amt,TSPL_IssueReturn_HEAD.TAX5_Base_Amt,TSPL_IssueReturn_HEAD.TAX6,TSPL_IssueReturn_HEAD.TAX6_Rate,TSPL_IssueReturn_HEAD.TAX6_Amt,TSPL_IssueReturn_HEAD.TAX6_Base_Amt,TSPL_IssueReturn_HEAD.TAX7,TSPL_IssueReturn_HEAD.TAX7_Rate,TSPL_IssueReturn_HEAD.TAX7_Amt,TSPL_IssueReturn_HEAD.TAX7_Base_Amt,TSPL_IssueReturn_HEAD.TAX8,TSPL_IssueReturn_HEAD.TAX8_Rate,TSPL_IssueReturn_HEAD.TAX8_Amt,TSPL_IssueReturn_HEAD.TAX8_Base_Amt,TSPL_IssueReturn_HEAD.TAX9,TSPL_IssueReturn_HEAD.TAX9_Rate,TSPL_IssueReturn_HEAD.TAX9_Amt,TSPL_IssueReturn_HEAD.TAX9_Base_Amt,TSPL_IssueReturn_HEAD.TAX10,TSPL_IssueReturn_HEAD.TAX10_Rate,TSPL_IssueReturn_HEAD.TAX10_Amt,TSPL_IssueReturn_HEAD.TAX10_Base_Amt,TSPL_IssueReturn_HEAD.BeforeTax_Amt,TSPL_IssueReturn_HEAD.Total_Tax_Amt,TSPL_IssueReturn_HEAD.doc_Amt, TSPL_IssueReturn_HEAD.vehicle_Id, TSPL_IssueReturn_HEAD.Machine_Id,Req_IssueNo,RequisitionNo,TSPL_IssueReturn_HEAD.Against_Month_End,TSPL_IssueReturn_HEAD.Against_Month_End_Retun_No,TSPL_IssueReturn_HEAD.Is_Reject,TSPL_IssueReturn_HEAD.Reject_Vendor_Code,TSPL_IssueReturn_HEAD.Is_Skip_Dept_Indent_Balance,TSPL_IssueReturn_HEAD.PurchaseInvoice_No,TSPL_IssueReturn_HEAD.Cost_Center_Unit,TSPL_IssueReturn_HEAD.Cost_Center_Type,TSPL_IssueReturn_HEAD.Againt_Cleaning_No,TSPL_IssueReturn_HEAD.Tanker_Cleaning_Item_No  FROM TSPL_IssueReturn_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on FLocation.Location_Code=TSPL_IssueReturn_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on TLocation.Location_Code=TSPL_IssueReturn_HEAD.To_Location left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on IssueEmp.EMP_CODE= TSPL_IssueReturn_HEAD.issue_To left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_IssueReturn_HEAD.Request_By left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code =TSPL_IssueReturn_HEAD.Issue_To_Franchise where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_IssueReturn_HEAD.Doc_No = (select MIN(Doc_No) from TSPL_IssueReturn_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_IssueReturn_HEAD.Doc_No = (select Max(Doc_No) from TSPL_IssueReturn_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_IssueReturn_HEAD.Doc_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_IssueReturn_HEAD.Doc_No = (select Min(Doc_No) from TSPL_IssueReturn_HEAD where Doc_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_IssueReturn_HEAD.Doc_No = (select Max(Doc_No) from TSPL_IssueReturn_HEAD where Doc_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsIssueReturnHead()
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
            obj.Issue_To_Franchise = clsCommon.myCstr(dt.Rows(0)("Issue_To_Franchise"))
            obj.Issue_To_Franchise_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Issue_ToName = clsCommon.myCstr(dt.Rows(0)("IssueToName"))
            obj.Request_By = clsCommon.myCstr(dt.Rows(0)("Request_By"))
            obj.Request_ByName = clsCommon.myCstr(dt.Rows(0)("RequestByName"))
            obj.Req_IssueNo = clsCommon.myCstr(dt.Rows(0)("Req_IssueNo"))
            obj.RequisitionNo = clsCommon.myCstr(dt.Rows(0)("RequisitionNo"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Is_Skip_Dept_Indent_Balance = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Skip_Dept_Indent_Balance")) = 1, True, False)
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Againt_Cleaning_No = clsCommon.myCstr(dt.Rows(0)("Againt_Cleaning_No"))
            obj.Tanker_Cleaning_Item_No = clsCommon.myCstr(dt.Rows(0)("Tanker_Cleaning_Item_No"))
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
            obj.Against_Month_End = IIf(clsCommon.myCdbl(dt.Rows(0)("Against_Month_End")) = 1, True, False)
            obj.Against_Month_End_Retun_No = clsCommon.myCstr(dt.Rows(0)("Against_Month_End_Retun_No"))

            obj.Is_Reject = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Reject")) = 1, True, False)
            obj.Reject_Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Reject_Vendor_Code"))

            obj.PurchaseInvoice_No = clsCommon.myCstr(dt.Rows(0)("PurchaseInvoice_No"))
            obj.Is_Reprocess = IIf(clsCommon.myCstr(dt.Rows(0)("Is_Reprocess")) = "1", True, False)

            obj.Capex_Code = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
            obj.Capex_SubCode = clsCommon.myCstr(dt.Rows(0)("Capex_SubCode"))
            obj.CosCenter_Unit = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Unit"))
            obj.CostCenter_Type = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Type"))

            qry = "SELECT TSPL_IssueReturn_DETAIL.Hirerachy_Code,TSPL_IssueReturn_DETAIL.Cost_Centre_Code, TSPL_IssueReturn_DETAIL.Doc_No,TSPL_IssueReturn_DETAIL.type,TSPL_IssueReturn_DETAIL.Line_No,TSPL_IssueReturn_DETAIL.Item_Code,TSPL_IssueReturn_DETAIL.Item_Desc,TSPL_IssueReturn_DETAIL.Required_Qty,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty , TSPL_IssueReturn_DETAIL.TAX1,TSPL_IssueReturn_DETAIL.TAX1_Rate,TSPL_IssueReturn_DETAIL.TAX1_Amt,TSPL_IssueReturn_DETAIL.TAX2,TSPL_IssueReturn_DETAIL.TAX2_Rate,TSPL_IssueReturn_DETAIL.TAX2_Amt,TSPL_IssueReturn_DETAIL.TAX3,TSPL_IssueReturn_DETAIL.TAX3_Rate,TSPL_IssueReturn_DETAIL.TAX3_Amt,TSPL_IssueReturn_DETAIL.TAX4,TSPL_IssueReturn_DETAIL.TAX4_Rate,TSPL_IssueReturn_DETAIL.TAX4_Amt,TSPL_IssueReturn_DETAIL.TAX5,TSPL_IssueReturn_DETAIL.TAX5_Rate,TSPL_IssueReturn_DETAIL.TAX5_Amt,TSPL_IssueReturn_DETAIL.TAX6,TSPL_IssueReturn_DETAIL.TAX6_Rate,TSPL_IssueReturn_DETAIL.TAX6_Amt,TSPL_IssueReturn_DETAIL.TAX7,TSPL_IssueReturn_DETAIL.TAX7_Rate,TSPL_IssueReturn_DETAIL.TAX7_Amt,TSPL_IssueReturn_DETAIL.TAX8,TSPL_IssueReturn_DETAIL.TAX8_Rate,TSPL_IssueReturn_DETAIL.TAX8_Amt,TSPL_IssueReturn_DETAIL.TAX9,TSPL_IssueReturn_DETAIL.TAX9_Rate,TSPL_IssueReturn_DETAIL.TAX9_Amt,TSPL_IssueReturn_DETAIL.TAX10,TSPL_IssueReturn_DETAIL.TAX10_Rate,TSPL_IssueReturn_DETAIL.TAX10_Amt,TSPL_IssueReturn_DETAIL.TAX1_Base_Amt,TSPL_IssueReturn_DETAIL.TAX2_Base_Amt,TSPL_IssueReturn_DETAIL.TAX3_Base_Amt,TSPL_IssueReturn_DETAIL.TAX4_Base_Amt,TSPL_IssueReturn_DETAIL.TAX5_Base_Amt,TSPL_IssueReturn_DETAIL.TAX6_Base_Amt,TSPL_IssueReturn_DETAIL.TAX7_Base_Amt,TSPL_IssueReturn_DETAIL.TAX8_Base_Amt,TSPL_IssueReturn_DETAIL.TAX9_Base_Amt,TSPL_IssueReturn_DETAIL.TAX10_Base_Amt ,TSPL_IssueReturn_DETAIL.Amount,TSPL_IssueReturn_DETAIL.Total_Tax_Amt,TSPL_IssueReturn_DETAIL.Item_Net_Amt,TSPL_IssueReturn_DETAIL.Unit_Cost,Issued_Qty_AgainstRet, TSPL_IssueReturn_DETAIL.Req_IssueNo, TSPL_IssueReturn_DETAIL.Cost_Code,TSPL_IssueReturn_DETAIL.PurchaseInvoice_Bal_Qty,TSPL_IssueReturn_DETAIL.purchaseinvoice_no,TSPL_IssueReturn_DETAIL.SRN_ID,TSPL_IssueReturn_DETAIL.MRN_ID,TSPL_IssueReturn_DETAIL.PO_ID,TSPL_IssueReturn_DETAIL.GRN_Id ,TSPL_IssueReturn_DETAIL.Hirerachy_Level3,TSPL_IssueReturn_DETAIL.Hirerachy_Level4 FROM TSPL_IssueReturn_DETAIL where TSPL_IssueReturn_DETAIL.Doc_No='" + obj.Doc_No + "' ORDER BY TSPL_IssueReturn_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsIssueReturnDetail)
                Dim objTr As clsIssueReturnDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsIssueReturnDetail
                    objTr.arrBatchItem = New List(Of clsBatchInventory)

                    objTr.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.type = clsCommon.myCstr(dr("type"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Cost_Code = clsCommon.myCstr(dr("Cost_Code"))
                    objTr.Required_Qty = clsCommon.myCdbl(dr("Required_Qty"))
                    objTr.Issued_Qty = clsCommon.myCdbl(dr("Issued_Qty"))
                    objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(dr("Issued_Qty_AgainstRet"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Unit_Cost = clsCommon.myCdbl(dr("Unit_Cost"))
                    objTr.Req_IssueNo = clsCommon.myCstr(dr("Req_IssueNo"))
                    ''richa
                    objTr.Hirerachy_Code = clsCommon.myCstr(dr("Hirerachy_Code"))
                    objTr.Cost_Centre_Code = clsCommon.myCstr(dr("Cost_Centre_Code"))
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

                    objTr.PurchaseInvoice_Bal_Qty = clsCommon.myCdbl(dr("PurchaseInvoice_Bal_Qty"))
                    objTr.PurchaseInvoice_No = clsCommon.myCstr(dr("PurchaseInvoice_No"))
                    objTr.SRN_ID = clsCommon.myCstr(dr("SRN_ID"))
                    objTr.MRN_ID = clsCommon.myCstr(dr("MRN_ID"))
                    objTr.GRN_Id = clsCommon.myCstr(dr("GRN_Id"))
                    objTr.PO_ID = clsCommon.myCstr(dr("PO_ID"))

                    objTr.arrSrItem = clsSerializeInvenotry.GetData("ISSTRAN", objTr.Doc_No, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("ISSTRAN", obj.Doc_No, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.HirerachyLevelCode3 = clsCommon.myCstr(dr("Hirerachy_Level3"))
                    objTr.HirerachyLevelCode4 = clsCommon.myCstr(dr("Hirerachy_Level4"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return PostData(strDocNo, "", trans)
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal strVoucherNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim PostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            Dim obj As clsIssueReturnHead = clsIssueReturnHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnIssueReturn, obj.From_Location, obj.Doc_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.Doc_No + " Is On Hold.Can't Post it")
            End If
            '--------------For From Location-----------------------------------------------------------------------------

            If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal AndAlso obj.Against_Month_End Then
                Dim objMonthEnd As clsIssueReturnHead = clsIssueReturnHead.GetData(strDocNo, NavigatorType.Current, trans)
                objMonthEnd.Doc_No = ""
                objMonthEnd.Doc_Date = clsCommon.myCDate(objMonthEnd.Doc_Date).AddDays(1)
                objMonthEnd.Doc_Type = "Issue"
                objMonthEnd.Against_Month_End_Retun_No = obj.Doc_No
                objMonthEnd.Against_Month_End = False
                objMonthEnd.SaveData(objMonthEnd, True, trans)
                clsIssueReturnHead.PostData(objMonthEnd.Doc_No, trans)
            End If



            ''richa agarwal 11/11/2014
            If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Doc_Type, "Transfer") = CompairStringResult.Equal Then
                Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                For Each objTr As clsIssueReturnDetail In obj.Arr
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "O"
                    objInventoryMovemnt.Location_Code = obj.From_Location

                    objInventoryMovemnt.Other_Location_Code = obj.To_Location
                    objInventoryMovemnt.Other_Location_Desc = obj.To_LocationName

                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.Issued_Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                    objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                    objInventoryMovemnt.Net_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt.FIFO_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt.LIFO_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt.Avg_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt.CalculateAvgCost = False


                    '' Work done by Parteek on Against ticket no. on 01/02/2019
                    Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'", trans))
                    Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
                    Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    If clsCommon.myLen(item_Purchase_Class) > 0 Then
                        Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                        If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                            objInventoryMovemnt.Inventory_CrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, strLocatinSegment, True, trans) ''UDL/24/07/19-000308 By Balwinder  on 24/08/2019
                        End If
                    End If

                    '' end

                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)

                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    Else
                        objInventoryMovemnt.ItemType = type
                    End If
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                Next
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("ISSTRAN", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement, trans)
            End If
            'For To Location-----------------------------------------------------------------------------------------------------------------------
            ''richa agarwal 11/11/2014
            If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Doc_Type, "Transfer") = CompairStringResult.Equal Then
                Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                For Each objTr As clsIssueReturnDetail In obj.Arr
                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                    Dim objInventoryMovemnt1 As New clsInventoryMovement()
                    objInventoryMovemnt1.InOut = "I"
                    objInventoryMovemnt1.CalculateAvgCost = False
                    objInventoryMovemnt1.Location_Code = obj.To_Location
                    objInventoryMovemnt1.Other_Location_Code = obj.From_Location
                    objInventoryMovemnt1.Other_Location_Desc = obj.From_LocationName
                    objInventoryMovemnt1.Item_Code = objTr.Item_Code
                    objInventoryMovemnt1.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt1.Qty = objTr.Issued_Qty
                    objInventoryMovemnt1.UOM = objTr.Unit_code
                    objInventoryMovemnt1.Basic_Cost = objTr.Unit_Cost
                    objInventoryMovemnt1.Add_Cost = objTr.Total_Tax_Amt
                    objInventoryMovemnt1.Net_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt1.FIFO_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt1.LIFO_Cost = objTr.Item_Net_Amt
                    objInventoryMovemnt1.Avg_Cost = objTr.Item_Net_Amt
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt1.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt1.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt1.ItemType = "FT"
                    Else
                        objInventoryMovemnt1.ItemType = type
                    End If

                    '' Work done by Parteek on Against ticket no. on 01/02/2019
                    Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'", trans))
                    Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'"
                    Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    If clsCommon.myLen(item_Purchase_Class) > 0 Then
                        Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                        If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                            objInventoryMovemnt1.Inventory_DrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, strLocatinSegment, True, trans)
                        End If
                    End If

                    '' end


                    ArrInventoryMovement1.Add(objInventoryMovemnt1)
                Next
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("ISSTRAN", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement1, trans)
            End If

            CreateJournalEntry(obj, trans, strVoucherNo)

            ''if purchasereturn is ON and purchase invoice is selected then auto purchase return created=======================
            Dim AutoPurchaseReturn As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoPurchaseReturnFromIssueReturn, clsFixedParameterCode.AutoPurchaseReturnFromIssueReturn, trans)) = 1, True, False)

            If AutoPurchaseReturn AndAlso clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.PurchaseInvoice_No) > 0 Then
                SavePurchaseReturn(obj, trans)
            End If
            ''======================================================================================================================
            Dim qry As String = "Update TSPL_IssueReturn_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_IssueReturn_HEAD", "Doc_No", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function DeleteVoucher(ByVal strTransferNo As String, ByVal trans As SqlTransaction) As Boolean
        ''BHA/09/10/18-000610 by balwinder on 09/10/2018
        ''BHA/15/10/18-000626 by balwinder on 15/10/2018
        Dim Sql As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code in ('PU-IS','PU-RE','PU-TC','PU-TF') and Source_Doc_No='" + strTransferNo + "' )"
        clsDBFuncationality.ExecuteNonQuery(Sql, trans)

        Sql = "delete from TSPL_JOURNAL_MASTER WHERE Source_Code in ('PU-IS','PU-RE','PU-TC','PU-TF') and Source_Doc_No='" + strTransferNo + "' "
        clsDBFuncationality.ExecuteNonQuery(Sql, trans)
        Return True
    End Function

    Public Shared Function CreateJournalEntry(ByVal obj As clsIssueReturnHead, ByVal trans As SqlTransaction, ByVal strVoucherNo As String) As Boolean
        ''richa agarwal 11/11/2014
        ''If obj.Doc_Type = "Transfer" Then
        ''-------------------------------

        Dim Sql As String = ""
        If clsCommon.myLen(strVoucherNo) > 0 Then
            DeleteVoucher(obj.Doc_No, trans)
        End If

        If clsCommon.myLen(obj.Issue_To_Franchise) <= 0 Then
            Dim CreateGLAccToItem As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateGLAccToItem, clsFixedParameterCode.CreateGLAccToItem, trans)) = 1, True, False)
            Dim strFromInvAcc As String = ""
            Dim strFromInvAccDesc As String = ""
            Dim strToInvAcc As String = ""
            Dim strToInvAccDesc As String = ""
            Dim strShpClrAcc As String = ""
            Dim strShpClrAccDesc As String = ""
            ''richa agarwal 11/11/2014
            If obj.Doc_Type = "Return" Then
                obj.From_Location = obj.To_Location
            End If
            ''--------------
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
            Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
            Dim objSeg As Accountsegment = Nothing
            Dim arrlist As New ArrayList()

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                For Each objTr As clsIssueReturnDetail In obj.Arr
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                 " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                 " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objTr.Item_Code + "'"
                    strFromInvAcc = connectSql.RunScalar(trans, Sql)
                    strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, obj.From_Location, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
                    strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
                    If strFromInvAccDesc Is Nothing Then
                        Throw New Exception("Inventory Control Account not found for Item " & objTr.Item_Desc & ".")
                    End If
                    strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, obj.From_Location, trans)
                    objSeg = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
                    'Dim arrlist As New ArrayList()
                    ''richa agarwal 11/11/2014
                    If obj.Doc_Type = "Return" Then
                        Dim BankAcc() As String = {strFromInvAcc, objTr.Amount, "", "", "", "", "", "", "I"}
                        If obj.Total_Tax_Amt > 0 AndAlso clsCommon.myLen(obj.PurchaseInvoice_No) > 0 Then
                            BankAcc = {strFromInvAcc, obj.doc_Amt, "", "", "", "", "", "", "I"}
                        End If
                        arrlist.Add(BankAcc)
                        ''richa agarwal 7 Dec,2018 BHA/27/11/18-000721
                        clsInventoryMovement.UpdateInvControlAccount(obj.Doc_No, "ISSTRAN", objTr.Item_Code, strFromInvAcc, "", "", trans)
                        '------------------

                    Else
                        Dim BankAcc() As String = {strFromInvAcc, objTr.Amount * -1, "", "", "", "", "", "", "I"}
                        arrlist.Add(BankAcc)

                        ''richa agarwal 7 Dec,2018 BHA/27/11/18-000721
                        clsInventoryMovement.UpdateInvControlAccount(obj.Doc_No, "ISSTRAN", objTr.Item_Code, "", strFromInvAcc, "", trans)
                        '------------------
                    End If
                Next

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
                        strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
                    End If
                    If Not strNetPayAcc = "" Then
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                        strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
                    End If
                    If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                        strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                        Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
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
                        Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
                        arrlist.Add(Tax1)
                    End If
                    ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
                End If
                '*********** End Tax7

                ''richa agarwal 11/11/2014
                If obj.Doc_Type = "Transfer" Then
                    ''------------------
                    strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.To_Location, trans)
                    objSeg = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
                    Dim DocAmtWithTax() As String = {strToInvAcc, obj.doc_Amt}
                    arrlist.Add(DocAmtWithTax)
                End If
                Dim strFrmFilledAcc As String = Nothing
                ''richa agarwal 11/11/2014
                If obj.Doc_Type = "Transfer" Then
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

                    ''==================Done By Monika========for recreation of voucher
                    If clsCommon.myLen(strVoucherNo) < 0 Then
                        Dim qry1 As String = "select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code='PU-TF' and Source_Doc_No='" + obj.Doc_No + "' "
                        strVoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    End If
                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, strVoucherNo, trans, obj.Doc_Date, obj.Comment, "PU-TF", "Purchase Transfer", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "PU-TF", "Purchase Transfer", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    End If

                ElseIf clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    For Each objTr As clsIssueReturnDetail In obj.Arr
                        If CreateGLAccToItem = True Then
                            '' Anubhooti 23-Jan-2015 (Pick GL Account From Item Master Based On Settings)
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableItemGroupGLMapping, clsFixedParameterCode.EnableItemGroupGLMapping, trans)) = 0 Then
                                Sql = "Select ISNULL(TSPL_ITEM_MASTER.GL_Account,'') AS [GL Account] FROM TSPL_ITEM_MASTER WHERE Item_Code ='" + objTr.Item_Code + "'"
                            Else
                                Sql = "select isnull(TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc,'') AS [GL Account] from TSPL_PURCHASE_ACCOUNTS left outer join tspl_item_master on tspl_item_master.Purchase_Class_Code= TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where item_code='" + objTr.Item_Code + "'"
                            End If
                            Dim ItemGLAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                            If ItemGLAcc Is Nothing OrElse clsCommon.myLen(ItemGLAcc) <= 0 Then
                                Throw New Exception("GL Account not mapped for item (" & clsCommon.myCstr(objTr.Item_Code) + "-" + clsCommon.myCstr(objTr.Item_Desc) & ") in item master.")
                            Else
                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(ItemGLAcc, obj.From_Location, trans)
                                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))

                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                                objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                                Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                                arrlist.Add(FrmFilledAcc)
                            End If
                        Else
                            Sql = "Select GL_Account_Code from TSPL_CostCenter_MASTER where Cost_Code ='" + objTr.Cost_Code + "'"
                            Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                            If clsCommon.myLen(strFrmFilledAccFirst) <= 0 Then
                                Throw New Exception("Stock Issue filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
                            Else
                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAccFirst, obj.From_Location, trans)
                                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                                objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                                Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                                arrlist.Add(FrmFilledAcc)
                            End If
                        End If
                    Next

                    ''==================Done By Monika========for recreation of voucher
                    If clsCommon.myLen(strVoucherNo) < 0 Then
                        Dim qry1 As String = "select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code='PU-IS' and Source_Doc_No='" + obj.Doc_No + "' "
                        strVoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    End If
                    

                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, strVoucherNo, trans, obj.Doc_Date, obj.Comment, "PU-IS", "Purchase Issue", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "PU-IS", "Purchase Issue", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    ''richa agarwal 2 Dec,2016
                    For Each objTr As clsIssueReturnDetail In obj.Arr
                        If CreateGLAccToItem = True Then

                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableItemGroupGLMapping, clsFixedParameterCode.EnableItemGroupGLMapping, trans)) = 0 Then
                                Sql = "Select ISNULL(TSPL_ITEM_MASTER.GL_Account,'') AS [GL Account] FROM TSPL_ITEM_MASTER WHERE Item_Code ='" + objTr.Item_Code + "'"
                            Else
                                Sql = "select isnull(TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc,'') AS [GL Account] from TSPL_PURCHASE_ACCOUNTS left outer join tspl_item_master on tspl_item_master.Purchase_Class_Code= TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where item_code='" + objTr.Item_Code + "'"
                            End If

                            Dim ItemGLAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                            If ItemGLAcc Is Nothing OrElse clsCommon.myLen(ItemGLAcc) <= 0 Then
                                Throw New Exception("GL Account not mapped for item (" & clsCommon.myCstr(objTr.Item_Code) + "-" + clsCommon.myCstr(objTr.Item_Desc) & ") in item master.")
                            Else
                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(ItemGLAcc, obj.From_Location, trans)
                                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))

                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                                objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                                Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost * -1, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                                arrlist.Add(FrmFilledAcc)
                            End If
                        Else

                            Sql = "Select GL_Account_Code from TSPL_CostCenter_MASTER where Cost_Code ='" + objTr.Cost_Code + "'"
                            Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                            If clsCommon.myLen(strFrmFilledAccFirst) <= 0 Then
                                Throw New Exception("Stock Issue filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
                            Else
                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAccFirst, obj.From_Location, trans)
                                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                                objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                                Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost * -1, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                                arrlist.Add(FrmFilledAcc)
                            End If

                        End If
                    Next

                    If clsCommon.myLen(strVoucherNo) < 0 Then
                        Dim qry1 As String = "select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code='PU-RE' and Source_Doc_No='" + obj.Doc_No + "' "
                        strVoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    End If
                    

                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, strVoucherNo, trans, obj.Doc_Date, obj.Comment, "PU-RE", "Purchase Return", obj.Doc_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "PU-RE", "Purchase Return", obj.Doc_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                    arrlist = New ArrayList()
                    For Each objTr As clsIssueReturnDetail In obj.Arr
                        Sql = "select TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code, TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC from TSPL_PURCHASE_ACCOUNTS left outer join tspl_item_master on tspl_item_master.Purchase_Class_Code= TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where item_code='" + objTr.Item_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                            Throw New Exception("Please set purchase account set for item " + objTr.Item_Code)
                        End If

                        Dim strINVCtrlAccount As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                        If strINVCtrlAccount Is Nothing OrElse clsCommon.myLen(strINVCtrlAccount) <= 0 Then
                            Throw New Exception("Invenory Control Account not mapped for purchase account set - " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " .")
                        End If
                        strINVCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strINVCtrlAccount, obj.From_Location, trans)
                        Dim Acc1() As String = {strINVCtrlAccount, -1 * (objTr.Issued_Qty * objTr.Unit_Cost), "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code, "", "", "I"}
                        arrlist.Add(Acc1)

                        ''richa agarwal 7 Dec,2018 BHA/27/11/18-000721 not required because inventory not created.
                        'If clsCommon.CompairString(clsCommon.myCstr(clsItemMaster.GetItemProductType(objTr.Item_Code, trans)), "MI") = CompairStringResult.Equal Then
                        '    clsInventoryMovementNew.UpdateInvControlAccount(obj.Doc_No, "ISSTRAN", objTr.Item_Code, "", strINVCtrlAccount, "", trans)
                        'Else
                        '    clsInventoryMovement.UpdateInvControlAccount(obj.Doc_No, "ISSTRAN", objTr.Item_Code, "", strINVCtrlAccount, "", trans)
                        'End If
                        '------------------

                        Dim strFAClearingAccount As String = clsCommon.myCstr(dt.Rows(0)("FA_CLEARING_AC"))
                        If strFAClearingAccount Is Nothing OrElse clsCommon.myLen(strFAClearingAccount) <= 0 Then
                            Throw New Exception("FA Control Account not mapped for purchase account set - " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " .")
                        End If
                        strFAClearingAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strFAClearingAccount, obj.From_Location, trans)

                        Dim Acc2() As String = {strFAClearingAccount, (objTr.Issued_Qty * objTr.Unit_Cost), "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                        arrlist.Add(Acc2)
                    Next
                    
                    If clsCommon.myLen(strVoucherNo) < 0 Then
                        Dim qry1 As String = "select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code='PU-TC' and Source_Doc_No='" + obj.Doc_No + "' "
                        strVoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    End If
                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, strVoucherNo, trans, obj.Doc_Date, obj.Comment, "PU-TC", "Transfer Capex", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "PU-TC", "Transfer Capex", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    End If
                End If
            End If
        End If
        Return True
    End Function

#Region "Purchase Return"
    Public Shared Function SavePurchaseReturn(ByVal obj As clsIssueReturnHead, ByVal trans As SqlTransaction) As Boolean
        Dim objPR As New clsPurchasReturnHead()
        Dim objPI As New clsPurchaseInvoiceHead()
        Dim objPR_D As New clsPurchasReturnDetail()
        Try
            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                objPR = New clsPurchasReturnHead()

                objPI = clsPurchaseInvoiceHead.GetData(obj.PurchaseInvoice_No, NavigatorType.Current, trans, "")

                objPR.PR_No = Nothing
                objPR.PR_Date = obj.Doc_Date
                objPR.Vendor_Code = obj.Reject_Vendor_Code
                objPR.Vendor_Name = clsVendorMaster.GetName(obj.Reject_Vendor_Code, trans)
                objPR.Ref_No = obj.Doc_No
                objPR.Remarks = "Auto Purchase Return from Issue/Return screen,Ref. Doc No.: " + clsCommon.myCstr(obj.Doc_No) + " Dated: " + clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy") + ""
                objPR.Bill_To_Location = obj.To_Location
                objPR.Ship_To_Location = Nothing
                objPR.Comments = obj.Comment
                objPR.On_Hold = obj.On_Hold
                objPR.Description = obj.Remarks
                objPR.Vendor_Invoice_No = Nothing
                objPR.is_Reject_Item = obj.Is_Reject
                objPR.Against_PI = obj.PurchaseInvoice_No
                objPR.Tax_Group = obj.Tax_Group
                objPR.Total_Tax_Amt = obj.Total_Tax_Amt
                objPR.TrType = "R" 'Return
                objPR.NoteType = "D" 'debit note


                If objPI IsNot Nothing AndAlso clsCommon.myLen(objPI.PI_No) > 0 Then
                    objPR.Item_Type = objPI.Item_Type
                    objPR.Project_Id = objPI.PROJECT_ID
                    objPR.Terms_Code = objPI.Terms_Code
                    objPR.Due_Date = objPI.Due_Date
                    objPR.Carrier = objPI.Carrier
                    objPR.VehicleNo = objPI.VehicleNo
                    objPR.GRNo = objPI.GRNo
                    objPR.GENo = objPI.GENo
                    objPR.GEDate = objPI.GEDate
                End If ''pi cond

                If clsCommon.myLen(objPR.Against_PI) > 0 Then
                    objPR.Against_SRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_SRN FROM TSPL_PI_HEAD WHERE PI_No='" + objPR.Against_PI + "'", trans))
                End If
                If clsCommon.myLen(objPR.Against_SRN) > 0 Then
                    objPR.Against_MRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + objPR.Against_SRN + "'", trans))
                End If
                If clsCommon.myLen(objPR.Against_MRN) > 0 Then
                    objPR.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + objPR.Against_MRN + "'", trans))
                End If
                If clsCommon.myLen(objPR.Against_GRN) > 0 Then
                    objPR.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + objPR.Against_GRN + "'", trans))
                End If
                If clsCommon.myLen(objPR.Against_PO) > 0 Then
                    objPR.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + objPR.Against_PO + "'", trans))
                End If

                If (obj.TAX1_Amt > 0) Then
                    objPR.TAX1 = obj.TAX1
                    objPR.TAX1_Rate = obj.TAX1_Rate
                    objPR.TAX1_Base_Amt = obj.TAX1_Base_Amt
                    objPR.TAX1_Amt = obj.TAX1_Amt
                    objPR.AssessableAmt = obj.TAX1_Base_Amt
                End If
                If (obj.TAX2_Amt > 0) Then
                    objPR.TAX2 = obj.TAX2
                    objPR.TAX2_Rate = obj.TAX2_Rate
                    objPR.TAX2_Base_Amt = obj.TAX2_Base_Amt
                    objPR.TAX2_Amt = obj.TAX2_Amt
                    objPR.AssessableAmt = obj.TAX2_Base_Amt
                End If
                If (obj.TAX3_Amt > 0) Then
                    objPR.TAX3 = obj.TAX3
                    objPR.TAX3_Rate = obj.TAX3_Rate
                    objPR.TAX3_Base_Amt = obj.TAX3_Base_Amt
                    objPR.TAX3_Amt = obj.TAX3_Amt
                    objPR.AssessableAmt = obj.TAX3_Base_Amt
                End If
                If (obj.TAX4_Amt > 0) Then
                    objPR.TAX4 = obj.TAX4
                    objPR.TAX4_Rate = obj.TAX4_Rate
                    objPR.TAX4_Base_Amt = obj.TAX4_Base_Amt
                    objPR.TAX4_Amt = obj.TAX4_Amt
                    objPR.AssessableAmt = obj.TAX4_Base_Amt
                End If
                If (obj.TAX5_Amt > 0) Then
                    objPR.TAX5 = obj.TAX5
                    objPR.TAX5_Rate = obj.TAX5_Rate
                    objPR.TAX5_Base_Amt = obj.TAX5_Base_Amt
                    objPR.TAX5_Amt = obj.TAX5_Amt
                    objPR.AssessableAmt = obj.TAX5_Base_Amt
                End If
                If (obj.TAX6_Amt > 0) Then
                    objPR.TAX6 = obj.TAX6
                    objPR.TAX6_Rate = obj.TAX6_Rate
                    objPR.TAX6_Base_Amt = obj.TAX6_Base_Amt
                    objPR.TAX6_Amt = obj.TAX6_Amt
                    objPR.AssessableAmt = obj.TAX6_Base_Amt
                End If
                If (obj.TAX7_Amt > 0) Then
                    objPR.TAX7 = obj.TAX7
                    objPR.TAX7_Rate = obj.TAX7_Rate
                    objPR.TAX7_Base_Amt = obj.TAX7_Base_Amt
                    objPR.TAX7_Amt = obj.TAX7_Amt
                    objPR.AssessableAmt = obj.TAX7_Base_Amt
                End If
                If (obj.TAX8_Amt > 0) Then
                    objPR.TAX8 = obj.TAX8
                    objPR.TAX8_Rate = obj.TAX8_Rate
                    objPR.TAX8_Base_Amt = obj.TAX8_Base_Amt
                    objPR.TAX8_Amt = obj.TAX8_Amt
                    objPR.AssessableAmt = obj.TAX8_Base_Amt
                End If
                If (obj.TAX9_Amt > 0) Then
                    objPR.TAX9 = obj.TAX9
                    objPR.TAX9_Rate = obj.TAX9_Rate
                    objPR.TAX9_Base_Amt = obj.TAX9_Base_Amt
                    objPR.TAX9_Amt = obj.TAX9_Amt
                    objPR.AssessableAmt = obj.TAX9_Base_Amt
                End If
                If (obj.TAX10_Amt > 0) Then
                    objPR.TAX10 = obj.TAX10
                    objPR.TAX10_Rate = obj.TAX10_Rate
                    objPR.TAX10_Base_Amt = obj.TAX10_Base_Amt
                    objPR.TAX10_Amt = obj.TAX10_Amt
                    objPR.AssessableAmt = obj.TAX10_Base_Amt
                End If
                objPR.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

                objPR.Discount_Base = obj.BeforeTax_Amt
                objPR.Discount_Amt = 0
                objPR.Amount_Less_Discount = obj.BeforeTax_Amt
                objPR.PR_Total_Amt = obj.doc_Amt
                ''================additional qty remains========================
                objPR.Total_Add_Charge = 0
                objPR.is_Excise_On_Qty = False

                Dim dblTotAmtAfterDiscount As Double = CalLandAmt(obj, trans)

                objPR.Arr = New List(Of clsPurchasReturnDetail)

                For Each objtr As clsIssueReturnDetail In obj.Arr
                    objPR_D = New clsPurchasReturnDetail()

                    ''=================for getting capex code from purchase invoice
                    For Each objpi_d As clsPurchaseInvoiceDetail In objPI.Arr
                        If clsCommon.CompairString(objpi_d.Item_Code, objtr.Item_Code) = CompairStringResult.Equal Then
                            objPR_D.Category = objpi_d.Category
                            objPR_D.Emergency = objpi_d.Emergency
                            objPR_D.Capex_Code = objpi_d.Capex_Code
                            objPR_D.Capex_SubCode = objpi_d.Capex_SubCode
                        End If
                    Next


                    objPR_D.Line_No = objtr.Line_No
                    objPR_D.Row_Type = "Item"
                    objPR_D.Item_Code = objtr.Item_Code
                    objPR_D.Item_Desc = objtr.Item_Desc
                    objPR_D.PR_Qty = objtr.Issued_Qty
                    objPR_D.Balance_Qty = objtr.PurchaseInvoice_Bal_Qty
                    objPR_D.OrgPIQty = objtr.PurchaseInvoice_Bal_Qty
                    If clsCommon.myLen(objtr.PurchaseInvoice_No) > 0 Then
                        objPR_D.Balance_Qty = clsPurchaseInvoiceDetail.GetBalancePIQty(objtr.PurchaseInvoice_No, objtr.Item_Code, Nothing, objtr.Unit_code, Nothing, objtr.Amount, obj.Is_Reject, obj.Doc_No, trans)
                    End If
                    objPR_D.Unit_code = objtr.Unit_code
                    objPR_D.PI_Id = objtr.PurchaseInvoice_No
                    objPR_D.SRN_Id = objtr.SRN_ID
                    objPR_D.MRN_ID = objtr.MRN_ID
                    objPR_D.GRN_ID = objtr.GRN_Id
                    objPR_D.PO_ID = objtr.PO_ID

                    'If clsCommon.myLen(objPR_D.PI_Id) > 0 Then
                    '    objPR_D.SRN_Id = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_SRN FROM TSPL_PI_HEAD WHERE PI_No='" + objPR_D.PI_Id + "'", trans))
                    'End If
                    'If clsCommon.myLen(objPR_D.SRN_Id) > 0 Then
                    '    objPR_D.MRN_ID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + objPR_D.SRN_Id + "'", trans))
                    'End If
                    'If clsCommon.myLen(objPR_D.MRN_ID) > 0 Then
                    '    objPR_D.GRN_ID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + objPR_D.MRN_ID + "'", trans))
                    'End If
                    'If clsCommon.myLen(objPR_D.GRN_ID) > 0 Then
                    '    objPR_D.PO_ID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + objPR_D.GRN_ID + "'", trans))
                    'End If

                    objPR_D.Item_Cost = objtr.Unit_Cost
                    objPR_D.Amount = objtr.Amount
                    objPR_D.Disc_Per = 0
                    objPR_D.Disc_Amt = 0
                    objPR_D.Amt_Less_Discount = objtr.Amount
                    objPR_D.TAX1 = objtr.TAX1
                    objPR_D.TAX1_Base_Amt = objtr.TAX1_Base_Amt
                    objPR_D.TAX1_Rate = objtr.TAX1_Rate
                    objPR_D.TAX1_Amt = objtr.TAX1_Amt
                    objPR_D.TAX2 = objtr.TAX2
                    objPR_D.TAX2_Base_Amt = objtr.TAX2_Base_Amt
                    objPR_D.TAX2_Rate = objtr.TAX2_Rate
                    objPR_D.TAX2_Amt = objtr.TAX2_Amt
                    objPR_D.TAX3 = objtr.TAX3
                    objPR_D.TAX3_Base_Amt = objtr.TAX3_Base_Amt
                    objPR_D.TAX3_Rate = objtr.TAX3_Rate
                    objPR_D.TAX3_Amt = objtr.TAX3_Amt
                    objPR_D.TAX4 = objtr.TAX4
                    objPR_D.TAX4_Base_Amt = objtr.TAX4_Base_Amt
                    objPR_D.TAX4_Rate = objtr.TAX4_Rate
                    objPR_D.TAX4_Amt = objtr.TAX4_Amt
                    objPR_D.TAX5 = objtr.TAX5
                    objPR_D.TAX5_Base_Amt = objtr.TAX5_Base_Amt
                    objPR_D.TAX5_Rate = objtr.TAX5_Rate
                    objPR_D.TAX5_Amt = objtr.TAX5_Amt
                    objPR_D.TAX6 = objtr.TAX6
                    objPR_D.TAX6_Base_Amt = objtr.TAX6_Base_Amt
                    objPR_D.TAX6_Rate = objtr.TAX6_Rate
                    objPR_D.TAX6_Amt = objtr.TAX6_Amt
                    objPR_D.TAX7 = objtr.TAX7
                    objPR_D.TAX7_Base_Amt = objtr.TAX7_Base_Amt
                    objPR_D.TAX7_Rate = objtr.TAX7_Rate
                    objPR_D.TAX7_Amt = objtr.TAX7_Amt
                    objPR_D.TAX8 = objtr.TAX8
                    objPR_D.TAX8_Base_Amt = objtr.TAX8_Base_Amt
                    objPR_D.TAX8_Rate = objtr.TAX8_Rate
                    objPR_D.TAX8_Amt = objtr.TAX8_Amt
                    objPR_D.TAX9 = objtr.TAX9
                    objPR_D.TAX9_Base_Amt = objtr.TAX9_Base_Amt
                    objPR_D.TAX9_Rate = objtr.TAX9_Rate
                    objPR_D.TAX9_Amt = objtr.TAX9_Amt
                    objPR_D.TAX10 = objtr.TAX10
                    objPR_D.TAX10_Base_Amt = objtr.TAX10_Base_Amt
                    objPR_D.TAX10_Rate = objtr.TAX10_Rate
                    objPR_D.TAX10_Amt = objtr.TAX10_Amt
                    objPR_D.Total_Tax_Amt = objtr.Total_Tax_Amt
                    objPR_D.Item_Net_Amt = objtr.Item_Net_Amt
                    objPR_D.Location = obj.To_Location
                    objPR_D.MRP = 0
                    objPR_D.AssessableAmt = objtr.Item_Net_Amt
                    objPR_D.Batch_No = Nothing
                    objPR_D.Bin_No = Nothing
                    'If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                    '    objPR_D.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                    'End If
                    'If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                    '    objPR_D.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                    'End If
                    objPR_D.Specification = "From Issue/Return Screen"
                    objPR_D.Remarks = "From Issue/Return Screen"


                    objPR_D.arrSrItem = objtr.arrSrItem

                    ''''callandcost()===============================landed cost
                    Dim dblLandedCost As Double = 0
                    Dim dblAdditionalAmt As Double = 0
                    If Not CheckTaxRecoverable(obj.Tax_Group, trans) Then
                        dblAdditionalAmt = dblAdditionalAmt + objPR_D.TAX1_Amt
                    End If
                    If Not CheckTaxRecoverable(obj.Tax_Group, trans) Then
                        dblAdditionalAmt = dblAdditionalAmt + objPR_D.TAX2_Amt
                    End If
                    If Not CheckTaxRecoverable(obj.Tax_Group, trans) Then
                        dblAdditionalAmt = dblAdditionalAmt + objPR_D.TAX3_Amt
                    End If
                    If Not CheckTaxRecoverable(obj.Tax_Group, trans) Then
                        dblAdditionalAmt = dblAdditionalAmt + objPR_D.TAX4_Amt
                    End If
                    If Not CheckTaxRecoverable(obj.Tax_Group, trans) Then
                        dblAdditionalAmt = dblAdditionalAmt + objPR_D.TAX5_Amt
                    End If
                    If Not CheckTaxRecoverable(obj.Tax_Group, trans) Then
                        dblAdditionalAmt = dblAdditionalAmt + objPR_D.TAX6_Amt
                    End If

                    If objPR_D.PR_Qty > 0 Then
                        dblLandedCost = objPR_D.Amount + IIf(dblTotAmtAfterDiscount > 0, ((dblAdditionalAmt * objPR_D.Amount) / dblTotAmtAfterDiscount), 0)

                        objPR_D.Landed_Cost_Amount = Math.Round(dblLandedCost, 2)
                        objPR_D.Landed_Cost_Rate = Math.Round(dblLandedCost / objPR_D.PR_Qty, 4)
                        objPR_D.Total_NonRecTax_PerUnit = Math.Round(dblAdditionalAmt / objPR_D.PR_Qty, 4)
                        objPR_D.Total_RecTax_PerUnit = Math.Round(dblAdditionalAmt / objPR_D.PR_Qty, 4)

                        If dblAdditionalAmt = 0 Then
                            objPR_D.Total_AddtionalCost_PerUnit = 0
                        Else
                            If dblLandedCost <> 0 Then
                                objPR_D.Total_AddtionalCost_PerUnit = Math.Round(dblLandedCost / objPR_D.PR_Qty, 6)
                            Else
                                objPR_D.Total_AddtionalCost_PerUnit = 0
                            End If
                        End If
                    End If
                    ''=======================================================

                    If clsItemMaster.IsItemTypeEmpty(objPR_D.Item_Code, objPR_D.Unit_code, trans) Then
                        Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPR_D.Unit_code, trans))
                        objPR_D.Empty_Amount = dblVal * objPR_D.PR_Qty
                        objPR.Tot_Empty_Amount += objPR_D.Empty_Amount
                    End If

                    ' ''-----------------19/10/2016---------additional charge itemwise------------------------------------------
                    'objPR_D.ItemAdd_Charge_Code1 = clsCommon.myCstr(grow.Cells(colItemACCode1).Value)
                    'objPR_D.ItemAdd_Charge_Code2 = clsCommon.myCstr(grow.Cells(colItemACCode2).Value)
                    'objPR_D.ItemAdd_Charge_Code3 = clsCommon.myCstr(grow.Cells(colItemACCode3).Value)
                    'objPR_D.ItemAdd_Charge_Code4 = clsCommon.myCstr(grow.Cells(colItemACCode4).Value)
                    'objPR_D.ItemAdd_Charge_Code5 = clsCommon.myCstr(grow.Cells(colItemACCode5).Value)
                    'objPR_D.ItemAdd_Charge_Code6 = clsCommon.myCstr(grow.Cells(colItemACCode6).Value)
                    'objPR_D.ItemAdd_Charge_Code7 = clsCommon.myCstr(grow.Cells(colItemACCode7).Value)
                    'objPR_D.ItemAdd_Charge_Code8 = clsCommon.myCstr(grow.Cells(colItemACCode8).Value)
                    'objPR_D.ItemAdd_Charge_Code9 = clsCommon.myCstr(grow.Cells(colItemACCode9).Value)
                    'objPR_D.ItemAdd_Charge_Code10 = clsCommon.myCstr(grow.Cells(colItemACCode10).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value)
                    'objPR_D.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACAmount1).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACAmount2).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACAmount3).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACAmount4).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACAmount5).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACAmount6).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACAmount7).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACAmount8).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACAmount9).Value)
                    'objPR_D.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACAmount10).Value)
                    'objPR_D.Total_ItemAdd_Charge = clsCommon.myCdbl(grow.Cells(colItemTotalAdditionalCharge).Value)
                    ' ''=======================================================================================

                    If (clsCommon.myLen(objPR_D.Item_Code) > 0) Then
                        objPR.Arr.Add(objPR_D)
                    End If
                Next

                If objPR.Arr IsNot Nothing AndAlso objPR.Arr.Count > 0 Then
                    objPR.SaveData(objPR, True, trans)

                    Dim qry As String = "update TSPL_IssueReturn_Head set PurchaseReturn_No='" + objPR.PR_No + "' where doc_no='" + obj.Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If ''end obj cond.

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objPR = Nothing
        End Try
    End Function

    Public Shared Function CheckTaxRecoverable(ByVal taxcode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select tax_recoverable from TSPL_TAX_MASTER where tax_code='" + taxcode + "'"
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

        Return IIf(clsCommon.CompairString(str, "Y") = CompairStringResult.Equal, True, False)
    End Function

    Public Shared Function CalLandAmt(ByVal obj As clsIssueReturnHead, ByVal trans As SqlTransaction) As Double
        Dim dblTotAmtAfterDiscount As Double = 0

        If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objtr As clsIssueReturnDetail In obj.Arr
                dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + objtr.Amount
            Next
        End If

        Return dblTotAmtAfterDiscount
    End Function

#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsIssueReturnHead = clsIssueReturnHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnIssueReturn, obj.From_Location, obj.Doc_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("ISSTRAN", strCode, trans)

                clsBatchInventory.DeleteData("ISSTRAN", obj.Doc_No, trans)

                HistoryUpdate(strCode, trans)

                Dim qry As String = "delete from TSPL_IssueReturn_DETAIL where Doc_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_IssueReturn_HEAD where Doc_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            Dim obj As clsIssueReturnHead = clsIssueReturnHead.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Reverse")
            End If

            ''===============check if purchase return is made against,this document,then no reverse
            qry = "select TSPL_PR_HEAD.pr_no from TSPL_IssueReturn_HEAD left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.pr_no=TSPL_IssueReturn_HEAD.purchasereturn_no where TSPL_IssueReturn_HEAD.doc_no='" + obj.Doc_No + "'"
            Dim prno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(prno) > 0 Then
                Throw New Exception("Issue/Return document is used in Purchase Return [" + prno + "].")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnIssueReturn, obj.From_Location, obj.Doc_Date, trans)

            If Not obj.Status = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            '===================================================
            qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='ISSTRAN' and document_code='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''===================================================================
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            qry = "Delete from TSPL_INVENTORY_MOVEMENT WHERE Trans_Type='ISSTRAN' AND Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim CurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            For Each objTr As clsIssueReturnDetail In obj.Arr
                '--------------For From Location-----------------------------------------------------------------------------
                qry = "update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=Item_Qty+" + clsCommon.myCstr(objTr.Issued_Qty) + ", Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + CurrentDate + "'"
                qry += " WHERE Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'For To Location-----------------------------------------------------------------------------------------------------------------------
                qry = "update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=Item_Qty-" + clsCommon.myCstr(objTr.Issued_Qty) + ", Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + CurrentDate + "'"
                qry += " WHERE Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.To_Location + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next


            DeleteVoucher(strCode, trans) ''By balwinder on 15/10/2018

            qry = "Update TSPL_IssueReturn_HEAD set Status=0, Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_IssueReturn_HEAD", "Doc_No", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetDeptBalanceAgainstDept(ByVal strLocation As String, ByVal strDept As String, ByVal currIssueNo As String, ByVal strICode As String, ByVal strUOMCode As String, ByVal DocDate As DateTime, ByVal trans As SqlTransaction)
        Dim qry As String = "select sum(FinalQty*RI) as FinalQty  from (" + Environment.NewLine +
            " select xxx.Location,xxx.Dept, xxx.Doc_No, xxx.Doc_Date, xxx.Item_Code, xxx.Qty,xxx.Unit_code,TSPL_ITEM_UOM_DETAIL.Conversion_Factor  " + Environment.NewLine +
            " ,Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/FinalUOM.Conversion_Factor as FinalQty,RI from (" + Environment.NewLine +
            " select (case when len(isnull( TSPL_SRN_HEAD.Ship_To_Location,''))>0 then TSPL_SRN_HEAD.Ship_To_Location else TSPL_SRN_HEAD.Bill_To_Location end) as Location, TSPL_REQUISITION_HEAD.Dept ,TSPL_SRN_DETAIL.SRN_No as Doc_No,TSPL_SRN_HEAD.SRN_Date as Doc_Date,  TSPL_SRN_DETAIL.Item_Code, TSPL_SRN_DETAIL.SRN_Qty as Qty,TSPL_SRN_DETAIL.Unit_code,1 As RI " + Environment.NewLine +
            " from TSPL_SRN_DETAIL" + Environment.NewLine +
            " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No" + Environment.NewLine +
            " inner join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_ID and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code" + Environment.NewLine +
            " inner join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id" + Environment.NewLine +
            " where 2=2" + Environment.NewLine +
            " and (case when len(isnull( TSPL_SRN_HEAD.Ship_To_Location,''))>0 then TSPL_SRN_HEAD.Ship_To_Location else TSPL_SRN_HEAD.Bill_To_Location end)='" + strLocation + "' " + Environment.NewLine +
            " and TSPL_REQUISITION_HEAD.Dept='" + strDept + "' and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and TSPL_SRN_HEAD.SRN_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SRN_HEAD.Status=1" + Environment.NewLine +
            " and not exists(select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No)" + Environment.NewLine +
            " union All" + Environment.NewLine +
            " select max(Location) as Location,max(Dept) as Dept, Doc_No,max(Doc_Date) as Doc_Date,max(Item_Code) as Item_Code,sum(Qty*RI) as Qty,max(Unit_code) as Unit_code,-1 as RI from (" + Environment.NewLine +
            " select TSPL_ISSUERETURN_HEAD.From_Location as Location,TSPL_ISSUERETURN_HEAD.Dept,TSPL_ISSUERETURN_HEAD.Doc_No,TSPL_ISSUERETURN_HEAD.Doc_Date,TSPL_IssueReturn_DETAIL.Item_Code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,TSPL_IssueReturn_DETAIL.Unit_code,1 as RI,1 as Chk from TSPL_IssueReturn_DETAIL " + Environment.NewLine +
            " left outer join TSPL_ISSUERETURN_HEAD on TSPL_ISSUERETURN_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No" + Environment.NewLine +
            " where TSPL_ISSUERETURN_HEAD.Doc_Type='Issue' and TSPL_ISSUERETURN_HEAD.Doc_No not in ('" + currIssueNo + "') " + Environment.NewLine +
            " and TSPL_ISSUERETURN_HEAD.From_Location='" + strLocation + "' and TSPL_ISSUERETURN_HEAD.Dept='" + strDept + "' and TSPL_IssueReturn_DETAIL.Item_Code='" + strICode + "' " + Environment.NewLine +
            " union all " + Environment.NewLine +
            " select '' as Location,'' as Dept,TSPL_ISSUERETURN_HEAD.Req_IssueNo as Doc_No,null as  Doc_Date,TSPL_IssueReturn_DETAIL.Item_Code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,null as Unit_code,-1 as RI,0 as chk from TSPL_IssueReturn_DETAIL " + Environment.NewLine +
            " left outer join TSPL_ISSUERETURN_HEAD on TSPL_ISSUERETURN_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No" + Environment.NewLine +
            " where TSPL_ISSUERETURN_HEAD.Doc_Type='Return' and TSPL_IssueReturn_DETAIL.Item_Code='" + strICode + "' " + Environment.NewLine +
            " )xx " + Environment.NewLine +
            " group by Doc_No having sum(chk)>0 and sum(Qty*RI)>0" + Environment.NewLine +
            " )xxx " + Environment.NewLine +
            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.Unit_code" + Environment.NewLine +
            " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xxx.Item_Code and FinalUOM.UOM_Code='" + strUOMCode + "'" + Environment.NewLine +
            " )xxxx"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean

        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsIssueReturnHead = clsIssueReturnHead.GetData(Doc_No, NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If
            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_IssueReturn_HEAD", "Doc_No", "TSPL_IssueReturn_DETAIL", "Doc_No", trans)

            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Code in ('PU-IS','PU-RE','PU-TC','PU-TF') and  Source_Doc_No='" & Doc_No & "'"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            clsIssueReturnHead.ReverseAndUnpost(Doc_No, trans)
            clsIssueReturnHead.DeleteData(Doc_No, trans)

            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsIssueReturnDetail
#Region "Variables"
    Public arrBatchItem As New List(Of clsBatchInventory)
    Public PurchaseInvoice_No As String = Nothing
    Public PurchaseInvoice_Bal_Qty As Double = Nothing
    Public GRN_Id As String = Nothing
    Public PO_ID As String = Nothing
    Public MRN_ID As String = Nothing
    Public SRN_ID As String = Nothing
    Public Hirerachy_Code As String = String.Empty
    Public Cost_Centre_Code As String = String.Empty
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
    Public type As String = Nothing
    Public HirerachyLevelCode3 As String = Nothing
    Public HirerachyLevelCode4 As String = Nothing


#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strFromLocation As String, ByVal strToLocation As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsIssueReturnDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsIssueReturnDetail In Arr
               
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "type", obj.type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                If clsCommon.myLen(obj.Cost_Code) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Cost_Code", obj.Cost_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", obj.Hirerachy_Code, True)
                clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", obj.Cost_Centre_Code, True)
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


                clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
                clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_Bal_Qty", obj.PurchaseInvoice_Bal_Qty)
                clsCommon.AddColumnsForChange(coll, "SRN_ID", obj.SRN_ID)
                clsCommon.AddColumnsForChange(coll, "MRN_ID", obj.MRN_ID)
                clsCommon.AddColumnsForChange(coll, "GRN_Id", obj.GRN_Id)
                clsCommon.AddColumnsForChange(coll, "PO_ID", obj.PO_ID)
                '====Sanjeet(12/01/2017)===========
                Dim GL_Account As String = Nothing
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level3", obj.HirerachyLevelCode3, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level4", obj.HirerachyLevelCode4, True)

                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableItemGroupGLMapping, clsFixedParameterCode.EnableItemGroupGLMapping, trans)) = 0 Then
                    GL_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_ITEM_MASTER.GL_Account,'') as GL_Account,* from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code='" + obj.Item_Code + "'", trans))
                Else
                    GL_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc,'') as GL_Account from TSPL_PURCHASE_ACCOUNTS left outer join tspl_item_master on tspl_item_master.Purchase_Class_Code= TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where item_code='" + obj.Item_Code + "'", trans))
                End If

                clsCommon.AddColumnsForChange(coll, "GL_Account", GL_Account)

                '' Add column Consumption_Ac and Inventory Account on 06/02/2019
                Dim Consumption_Ac As String = Nothing
                Consumption_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_account from tspl_item_master where item_code='" & obj.Item_Code & "'", trans))

                Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + strFromLocation + "'"
                Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))

                If clsCommon.myLen(Consumption_Ac) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Consumption_Ac", clsERPFuncationality.ChangeGLAccountLocationSegment(Consumption_Ac, strLocatinSegment, True, trans))
                End If


                Dim inventory_ac As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where Item_Code='" & obj.Item_Code & "'", trans))
                If clsCommon.myLen(inventory_ac) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "inventory_ac", clsERPFuncationality.ChangeGLAccountLocationSegment(inventory_ac, strLocatinSegment, True, trans))
                End If


                '==============
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim DocType As String = ""
                If clsCommon.myLen(strDocNo) > 0 Then
                    DocType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  ISNULL(Doc_Type,'') AS Doc_Type  From TSPL_IssueReturn_HEAD Where Doc_No ='" & strDocNo & "'", trans))
                End If

                If clsCommon.CompairString(DocType, "Issue") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.SaveData("ISSTRAN", strDocNo, dtDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)
                    ''=============================================
                    clsBatchInventory.SaveData("ISSTRAN", strDocNo, dtDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                    ''=============================================
                End If
                If clsCommon.CompairString(DocType, "Return") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.SaveData("ISSTRAN", strDocNo, dtDocDate, "I", obj.Item_Code, strToLocation, obj.Line_No, obj.arrSrItem, trans)
                    ''=============================================
                    clsBatchInventory.SaveData("ISSTRAN", strDocNo, dtDocDate, "I", obj.Item_Code, strToLocation, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                    ''=============================================
                End If

            Next
        End If
        Return True
    End Function

End Class

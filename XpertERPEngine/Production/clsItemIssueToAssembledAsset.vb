''--27/08/2012--Updation By--[Pankaj Kumar]--Applied GL security While Navigating Document Finder-------Fwd By--Ranjana Mam
Imports common
Imports System.Data.SqlClient
Public Class clsItemIssueToAssembledAsset

#Region "Variables"
    Public Doc_No As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Doc_Type As String = Nothing
    Public From_Location As String = Nothing
    Public From_LocationName As String = Nothing
    Public Asset_Code As String = Nothing
    Public Asset_Desc As String = Nothing
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
    Public Asset_Type As String = Nothing
    Public Arr As List(Of clsItemIssueToAssembledAssetDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Against_Return_No As String = Nothing
    Public H_Code As String = ""
    Public CC_Code As String = ""
#End Region

    Public Function SaveData(ByVal obj As clsItemIssueToAssembledAsset, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Issue Items to Assemble Assset", obj.From_Location, obj.Doc_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)
            clsSerializeInvenotry.DeleteData("ISSTRAN", obj.Doc_No, trans)
            Dim qry As String = "delete from TSPL_IssueItemToAssembledAsset_Detail where Doc_No='" + obj.Doc_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                For Each objtr As clsItemIssueToAssembledAssetDetail In obj.Arr
                    Dim dblBalanceQty As Decimal = clsItemIssueToAssembledAssetDetail.GetBalanceQtyReturnForTranfserCapex(obj.Doc_No, obj.Against_Return_No, objtr.Item_Code, trans)
                    If objtr.Issued_Qty > dblBalanceQty Then
                        Throw New Exception("Available return qty: " + clsCommon.myCstr(dblBalanceQty) + " And Entered Qty: " + objtr.Issued_Qty + " for item: " + objtr.Item_Code)
                    End If
                Next
            End If

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    If CheckAssetStatus(obj.Req_IssueNo, trans) = True Then
                        Throw New Exception("Asset has been posted. Issue Doc can not be created after posting of Asset.")
                    End If
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.ItemIssueReturnAsset, clsDocTransactionType.ItemIssueAsset, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    '' check for posted asset
                    If CheckAssetStatus(obj.Req_IssueNo, trans) = True Then
                        Throw New Exception("Asset has been posted. Return Doc can not be created after posting of Asset.")
                    End If
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.ItemIssueReturnAsset, clsDocTransactionType.ItemReturnAsset, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.ItemIssueReturnAsset, clsDocTransactionType.TransferCapex, obj.From_Location)
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
            clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.Asset_Type)
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.From_Location)

            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Issue_To", obj.Issue_To)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.Req_IssueNo)
            clsCommon.AddColumnsForChange(coll, "RequisitionNo", obj.RequisitionNo)
            clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code, True)

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
            clsCommon.AddColumnsForChange(coll, "H_Code", obj.H_Code, True)
            clsCommon.AddColumnsForChange(coll, "CC_Code", obj.CC_Code, True)
            'clsCommon.AddColumnsForChange(coll, "Machine_Id", obj.Machine_Id)


            'clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            'clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Against_Return_No", obj.Against_Return_No, True)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueItemToAssembledAsset_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueItemToAssembledAsset_Head", OMInsertOrUpdate.Update, "TSPL_IssueItemToAssembledAsset_Head.Doc_No='" + obj.Doc_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsItemIssueToAssembledAssetDetail.SaveData(obj.Doc_No, obj.From_Location, obj.Asset_Code, obj.Doc_Date, Arr, trans)
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
    Public Function CheckAssetStatus(ByVal strIssueNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select max(TSPL_ACQUISITION_HEAD.Status) as [Status] from TSPL_IssueItemToAssembledAsset_Detail IssueD" & _
                            " inner join TSPL_IssueItemToAssembledAsset_head IssueH  on IssueD.Doc_No=IssueH.Doc_No " & _
                            " inner join TSPL_ACQUISITION_DETAIL on IssueD.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code " & _
                            " inner join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code " & _
                            " where IssueH.Doc_No='" & strIssueNo & "'"
        Return If(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1, True, False)
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsItemIssueToAssembledAsset
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsItemIssueToAssembledAsset
        Dim obj As clsItemIssueToAssembledAsset = Nothing
        Dim qry As String = "SELECT TSPL_IssueItemToAssembledAsset_Head.Doc_No,TSPL_IssueItemToAssembledAsset_Head.Doc_Date,TSPL_IssueItemToAssembledAsset_Head.Doc_Type," & _
            " TSPL_IssueItemToAssembledAsset_Head.Asset_Type,TSPL_IssueItemToAssembledAsset_Head.From_Location,FLocation.Location_Desc as FromLocationName," & _
            " TSPL_IssueItemToAssembledAsset_Head.Asset_Code,TLocation.Location_Desc as ToLocationName,TSPL_IssueItemToAssembledAsset_Head.Status," & _
            " TSPL_IssueItemToAssembledAsset_Head.On_Hold,TSPL_IssueItemToAssembledAsset_Head.Comment,TSPL_IssueItemToAssembledAsset_Head.Remarks," & _
            " TSPL_IssueItemToAssembledAsset_Head.Posting_Date,TSPL_IssueItemToAssembledAsset_Head.Issue_To,IssueEmp.Emp_Name as IssueToName ," & _
            " TSPL_IssueItemToAssembledAsset_Head.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_IssueItemToAssembledAsset_Head.Dept," & _
            " TSPL_IssueItemToAssembledAsset_Head.Dept_Desc,TSPL_IssueItemToAssembledAsset_Head.Tax_Group,TSPL_IssueItemToAssembledAsset_Head.tax_desc," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX1,TSPL_IssueItemToAssembledAsset_Head.TAX1_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX1_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX1_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX2,TSPL_IssueItemToAssembledAsset_Head.TAX2_Rate," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX2_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX2_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX3," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX3_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX3_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX3_Base_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX4,TSPL_IssueItemToAssembledAsset_Head.TAX4_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX4_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX4_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX5,TSPL_IssueItemToAssembledAsset_Head.TAX5_Rate," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX5_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX5_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX6," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX6_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX6_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX6_Base_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX7,TSPL_IssueItemToAssembledAsset_Head.TAX7_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX7_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX7_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX8,TSPL_IssueItemToAssembledAsset_Head.TAX8_Rate," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX8_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX8_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX9," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX9_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX9_Amt,TSPL_IssueItemToAssembledAsset_Head.TAX9_Base_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX10,TSPL_IssueItemToAssembledAsset_Head.TAX10_Rate,TSPL_IssueItemToAssembledAsset_Head.TAX10_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.TAX10_Base_Amt,TSPL_IssueItemToAssembledAsset_Head.BeforeTax_Amt,TSPL_IssueItemToAssembledAsset_Head.Total_Tax_Amt," & _
            " TSPL_IssueItemToAssembledAsset_Head.doc_Amt, TSPL_IssueItemToAssembledAsset_Head.vehicle_Id, TSPL_IssueItemToAssembledAsset_Head.Machine_Id," & _
            " Req_IssueNo,RequisitionNo,TSPL_IssueItemToAssembledAsset_Head.Against_Return_No,TSPL_IssueItemToAssembledAsset_Head.H_Code,TSPL_IssueItemToAssembledAsset_Head.CC_Code " & _
            " FROM TSPL_IssueItemToAssembledAsset_Head left outer join TSPL_LOCATION_MASTER as FLocation on FLocation.Location_Code=TSPL_IssueItemToAssembledAsset_Head.From_Location " & _
            " left outer join TSPL_LOCATION_MASTER as TLocation on TLocation.Location_Code=TSPL_IssueItemToAssembledAsset_Head.Asset_Code " & _
            " left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on IssueEmp.EMP_CODE= TSPL_IssueItemToAssembledAsset_Head.issue_To " & _
            " left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_IssueItemToAssembledAsset_Head.Request_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_IssueItemToAssembledAsset_Head.Doc_No = (select MIN(Doc_No) from TSPL_IssueItemToAssembledAsset_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_IssueItemToAssembledAsset_Head.Doc_No = (select Max(Doc_No) from TSPL_IssueItemToAssembledAsset_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_IssueItemToAssembledAsset_Head.Doc_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_IssueItemToAssembledAsset_Head.Doc_No = (select Min(Doc_No) from TSPL_IssueItemToAssembledAsset_Head where Doc_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_IssueItemToAssembledAsset_Head.Doc_No = (select Max(Doc_No) from TSPL_IssueItemToAssembledAsset_Head where Doc_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemIssueToAssembledAsset()
            obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.Doc_Date = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Asset_Type = clsCommon.myCstr(dt.Rows(0)("Asset_Type"))
            obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
            obj.From_LocationName = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
            obj.Asset_Code = clsCommon.myCstr(dt.Rows(0)("Asset_Code"))
            obj.Asset_Desc = clsCommon.myCstr(dt.Rows(0)("ToLocationName"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
            obj.Issue_To = clsCommon.myCstr(dt.Rows(0)("Issue_To"))
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
            obj.Against_Return_No = clsCommon.myCstr(dt.Rows(0)("Against_Return_No"))
            obj.H_Code = clsCommon.myCstr(dt.Rows(0)("H_Code"))
            obj.CC_Code = clsCommon.myCstr(dt.Rows(0)("CC_Code"))

            qry = "SELECT TSPL_IssueItemToAssembledAsset_Detail.Doc_No,TSPL_IssueItemToAssembledAsset_Detail.Line_No,TSPL_IssueItemToAssembledAsset_Detail.Item_Code," & _
                " TSPL_IssueItemToAssembledAsset_Detail.Item_Desc,TSPL_IssueItemToAssembledAsset_Detail.Required_Qty,TSPL_IssueItemToAssembledAsset_Detail.Unit_code," & _
                " TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty , TSPL_IssueItemToAssembledAsset_Detail.TAX1,TSPL_IssueItemToAssembledAsset_Detail.TAX1_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX1_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX2,TSPL_IssueItemToAssembledAsset_Detail.TAX2_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX2_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX3,TSPL_IssueItemToAssembledAsset_Detail.TAX3_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX3_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX4,TSPL_IssueItemToAssembledAsset_Detail.TAX4_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX4_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX5,TSPL_IssueItemToAssembledAsset_Detail.TAX5_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX5_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX6,TSPL_IssueItemToAssembledAsset_Detail.TAX6_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX6_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX7,TSPL_IssueItemToAssembledAsset_Detail.TAX7_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX7_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX8,TSPL_IssueItemToAssembledAsset_Detail.TAX8_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX8_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX9,TSPL_IssueItemToAssembledAsset_Detail.TAX9_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX9_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX10,TSPL_IssueItemToAssembledAsset_Detail.TAX10_Rate," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX10_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX1_Base_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX2_Base_Amt," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX3_Base_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX4_Base_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX5_Base_Amt," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX6_Base_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX7_Base_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX8_Base_Amt," & _
                " TSPL_IssueItemToAssembledAsset_Detail.TAX9_Base_Amt,TSPL_IssueItemToAssembledAsset_Detail.TAX10_Base_Amt ,TSPL_IssueItemToAssembledAsset_Detail.Amount," & _
                " TSPL_IssueItemToAssembledAsset_Detail.Total_Tax_Amt,TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt,TSPL_IssueItemToAssembledAsset_Detail.Unit_Cost," & _
                " Issued_Qty_AgainstRet, TSPL_IssueItemToAssembledAsset_Detail.Req_IssueNo, TSPL_IssueItemToAssembledAsset_Detail.Cost_Code," & _
                " TSPL_IssueItemToAssembledAsset_Detail.Asset_Code ,TSPL_IssueItemToAssembledAsset_Detail.IsCapex,TSPL_IssueItemToAssembledAsset_Detail.CapexType," & _
                " CheckCapexLimit,TSPL_IssueItemToAssembledAsset_Detail.Capex_Code ,TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode ," & _
                " TSPL_IssueItemToAssembledAsset_Detail.CapexQty,TSPL_ACQUISITION_HEAD.Acquisition_Date FROM TSPL_IssueItemToAssembledAsset_Detail inner join TSPL_ACQUISITION_DETAIL on TSPL_IssueItemToAssembledAsset_Detail.asset_code=TSPL_ACQUISITION_DETAIL.Asset_Code " & _
                " INNER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code " & _
                " where TSPL_IssueItemToAssembledAsset_Detail.Doc_No='" + obj.Doc_No + "' ORDER BY TSPL_IssueItemToAssembledAsset_Detail.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsItemIssueToAssembledAssetDetail)
                Dim objTr As clsItemIssueToAssembledAssetDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsItemIssueToAssembledAssetDetail
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
                    objTr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                    objTr.Asset_Date = clsCommon.GetPrintDate(clsCommon.myCDate(dr("Acquisition_Date")), "dd-MMM-yyyy")
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
                    '======================added by preeti gupta==============================
                    objTr.IsCapex = clsCommon.myCdbl(dr("IsCapex"))
                    objTr.CapexType = clsCommon.myCstr(dr("CapexType"))
                    objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                    objTr.Capex_SubCode = clsCommon.myCstr(dr("Capex_SubCode"))
                    objTr.CapexQty = clsCommon.myCdbl(dr("CapexQty"))

                    objTr.CheckCapexLimit = clsCommon.myCdbl(dr("CheckCapexLimit"))

                    objTr.arrSrItem = clsSerializeInvenotry.GetData("ISSTRAN", objTr.Doc_No, objTr.Item_Code, objTr.Line_No, trans)

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
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim PostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            Dim obj As clsItemIssueToAssembledAsset = clsItemIssueToAssembledAsset.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.Doc_No + " Is On Hold.Can't Post it")
            End If
            If Not clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                '--------------For Invenotry movement-----------------------------------------------------------------------------
                Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                If Not IsNothing(obj.Arr) Then
                    For Each objTr As clsItemIssueToAssembledAssetDetail In obj.Arr
                        Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                        Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                        Dim objInventoryMovemnt As New clsInventoryMovement()
                        If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                            objInventoryMovemnt.InOut = "O"
                        ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                            objInventoryMovemnt.InOut = "I"
                        End If
                        objInventoryMovemnt.Location_Code = obj.From_Location
                        objInventoryMovemnt.Item_Code = objTr.Item_Code
                        objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                        objInventoryMovemnt.Qty = objTr.Issued_Qty
                        objInventoryMovemnt.UOM = objTr.Unit_code
                        objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                        'objInventoryMovemnt.Rec_Cost= objTr.MRP
                        objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                        objInventoryMovemnt.Net_Cost = objTr.Item_Net_Amt
                        If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "OT"
                        ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        End If
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                    Next
                End If
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("ISS-ASS", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement, trans)
            End If

            For Each objTr As clsItemIssueToAssembledAssetDetail In obj.Arr
                Dim qry As String = "Update TSPL_IssueItemToAssembledAsset_Head set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = " UPDATE TSPL_ACQUISITION_DETAIL SET Book_Source_value=ISSUE_AMT.Amount,Book_Source_Original_value=ISSUE_AMT.Amount FROM " & _
                      " (SELECT TSPL_IssueItemToAssembledAsset_Detail.Asset_Code,SUM(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Issue' then  TSPL_IssueItemToAssembledAsset_Detail.Amount else -TSPL_IssueItemToAssembledAsset_Detail.Amount end) AS Amount " & _
                      " FROM TSPL_IssueItemToAssembledAsset_Detail " & _
                      " INNER JOIN TSPL_IssueItemToAssembledAsset_Head ON TSPL_IssueItemToAssembledAsset_Detail.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Doc_No " & _
                      " WHERE TSPL_IssueItemToAssembledAsset_Detail.Asset_Code='" & objTr.Asset_Code & "' " & _
                      " group by TSPL_IssueItemToAssembledAsset_Detail.Asset_Code) AS ISSUE_AMT where TSPL_ACQUISITION_DETAIL.Asset_Code=ISSUE_AMT.Asset_Code"

                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = " UPDATE TSPL_ACQUISITION_HEAD SET Total_Amt=ISSUE_AMT.Amount,Net_Amt=ISSUE_AMT.Amount FROM " & _
                      " (SELECT SUM(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Issue' then  TSPL_IssueItemToAssembledAsset_Detail.Amount else -TSPL_IssueItemToAssembledAsset_Detail.Amount end) AS Amount " & _
                      " FROM TSPL_IssueItemToAssembledAsset_Detail " & _
                      " INNER JOIN TSPL_IssueItemToAssembledAsset_Head ON TSPL_IssueItemToAssembledAsset_Detail.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Doc_No " & _
                      " WHERE TSPL_IssueItemToAssembledAsset_Detail.Asset_Code='" & objTr.Asset_Code & "' " & _
                      " ) AS ISSUE_AMT where TSPL_ACQUISITION_HEAD.Acquisition_Code IN (SELECT Acquisition_Code FROM  TSPL_ACQUISITION_DETAIL WHERE Asset_Code='" & objTr.Asset_Code & "')"

                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next

            Dim ArryLst As ArrayList = New ArrayList()
            If clsCommon.CompairString(obj.Doc_Type, "TransferCX") = CompairStringResult.Equal Then
                For Each objTr As clsItemIssueToAssembledAssetDetail In obj.Arr
                    Dim qry As String = "select TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code, TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC from TSPL_PURCHASE_ACCOUNTS left outer join tspl_item_master on tspl_item_master.Purchase_Class_Code= TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where item_code='" + objTr.Item_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set purchase account set for item " + objTr.Item_Code)
                    End If

                    Dim strINVCtrlAccount As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    If strINVCtrlAccount Is Nothing OrElse clsCommon.myLen(strINVCtrlAccount) <= 0 Then
                        Throw New Exception("Invenory Control Account not mapped for purchase account set - " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " .")
                    End If
                    strINVCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strINVCtrlAccount, obj.From_Location, trans)
                    Dim Acc1() As String = {strINVCtrlAccount, (objTr.Item_Net_Amt), "", "", obj.H_Code, obj.CC_Code}
                    ArryLst.Add(Acc1)


                    Dim strFAClearingAccount As String = clsCommon.myCstr(dt.Rows(0)("FA_CLEARING_AC"))
                    If strFAClearingAccount Is Nothing OrElse clsCommon.myLen(strFAClearingAccount) <= 0 Then
                        Throw New Exception("FA Control Account not mapped for purchase account set - " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " .")
                    End If
                    strFAClearingAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strFAClearingAccount, obj.From_Location, trans)

                    Dim Acc2() As String = {strFAClearingAccount, -1 * (objTr.Item_Net_Amt), "", "", obj.H_Code, obj.CC_Code}
                    ArryLst.Add(Acc2)
                Next
            Else
                Dim strIcode As String = String.Empty
                Dim strPurchaseAccountSet As String = String.Empty
                Dim Amt As Double = 0
                Dim totAmt As Double = 0

                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select MAX(xxx.Item_Code ) as Item_Code,SUM(xxx.Amount ) as Amount,xxx.Purchase_account as Purchase_account  from (select TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC as Purchase_account   ,TSPL_IssueItemToAssembledAsset_Detail.Item_Code,TSPL_IssueItemToAssembledAsset_Detail.Amount  from TSPL_IssueItemToAssembledAsset_Detail left outer join  TSPL_ITEM_MASTER on TSPL_IssueItemToAssembledAsset_Detail.Item_Code=TSPL_ITEM_MASTER.Item_Code  left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code   where TSPL_IssueItemToAssembledAsset_Detail.Doc_No='" & obj.Doc_No & "') as xxx  group by xxx.Purchase_account  ", trans)
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    For Each objTr As clsItemIssueToAssembledAssetDetail In obj.Arr
                        Amt = clsCommon.myCdbl(objTr.Item_Net_Amt)
                        totAmt += Amt
                        Dim StrwipAcSet As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_Dep_AccountSet.wip_ac from TSPL_Dep_AccountSet where AcSet_Code =(select TSPL_ACQUISITION_DETAIL.AcSet_Code from  TSPL_ACQUISITION_DETAIL where TSPL_ACQUISITION_DETAIL.Asset_Code='" & objTr.Asset_Code & "')", trans))
                        StrwipAcSet = clsERPFuncationality.ChangeGLAccountLocationSegment(StrwipAcSet, obj.From_Location, trans)
                        If clsCommon.myLen(StrwipAcSet) > 0 Then
                            ArryLst.Add(New String() {StrwipAcSet, IIf(clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal, Amt, -1 * Amt), "", "", obj.H_Code, obj.CC_Code})
                        End If
                    Next
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If clsCommon.myLen(dt.Rows(i)("Purchase_account")) > 0 Then
                            'strPurchaseAccountSet = dt.Rows(i)("Purchase_account")
                            strPurchaseAccountSet = clsERPFuncationality.ChangeGLAccountLocationSegment(dt.Rows(i)("Purchase_account"), obj.From_Location, trans)
                            Amt = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                            totAmt += Amt
                            ArryLst.Add(New String() {strPurchaseAccountSet, IIf(clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal, -1 * Amt, Amt), "", "", obj.H_Code, obj.CC_Code})
                        End If
                    Next
                End If
            End If
            '---------------------End Of Code Of Financial Entry
            If ArryLst IsNot Nothing AndAlso ArryLst.Count > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, "Against Component Issue/Return For Assembling Doc No: " & obj.Doc_No, "IS-AA", "Issue Item To Assemble Asset", obj.Doc_No, obj.Comment, "V", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            End If




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

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsItemIssueToAssembledAsset = clsItemIssueToAssembledAsset.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("ISSTRAN", strCode, trans)
                Dim qry As String = "delete from TSPL_IssueItemToAssembledAsset_Detail where Doc_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_IssueItemToAssembledAsset_Head where Doc_No='" + strCode + "'"
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
            Dim obj As clsItemIssueToAssembledAsset = clsItemIssueToAssembledAsset.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Reverse")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

            If Not obj.Status = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            'qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Against_Inv_Movement_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + obj.Doc_No + "' and Trans_Type='ISSTRAN')"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "Delete from TSPL_INVENTORY_MOVEMENT WHERE Trans_Type='ISS-ASS' AND Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim CurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            For Each objTr As clsItemIssueToAssembledAssetDetail In obj.Arr
                '--------------For From Location-----------------------------------------------------------------------------
                qry = "update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=Item_Qty+" + clsCommon.myCstr(objTr.Issued_Qty) + ", Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + CurrentDate + "'"
                qry += " WHERE Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'For To Location-----------------------------------------------------------------------------------------------------------------------
                qry = "update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=Item_Qty-" + clsCommon.myCstr(objTr.Issued_Qty) + ", Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + CurrentDate + "'"
                qry += " WHERE Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.Asset_Code + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next

            qry = "Delete TSPL_JOURNAL_DETAILS WHERE Voucher_No=(Select Voucher_No from TSPL_JOURNAL_MASTER WHERE Source_Doc_No='" + obj.Doc_No + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER WHERE Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_IssueItemToAssembledAsset_Head set Status=0, Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Shared UDLCapexAcquisionEntry As Boolean = False
    Public Shared Globaltrans As SqlTransaction = Nothing
    Public Shared Function GetAssembledInfo(ByVal lstAsset As ArrayList, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = ""

        qry = GetAssembleQuery(lstAsset, False)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Sub SettingBasedQuery()
        UDLCapexAcquisionEntry = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLCapexAcquisionEntry, clsFixedParameterCode.UDLCapexAcquisionEntry, Nothing)) = "1", True, False))

    End Sub

    Public Shared Function GetAssembleQuery(ByVal lstAsset As ArrayList, ByVal ShowPendingAlso As Boolean) As String
        Dim qry As String = String.Empty
        'lstAsset.Add("-")

        '=================Added by preeti gupta=======================
        '', Optional UDLCapexAcquisionEntry As Boolean = False

        '' commented by Panch Raj because no need of the setting and seprate query for udl : depreciation calcultion will become wrong
        'If UDLCapexAcquisionEntry = True Then
        '    qry = "  SELECT [Asset Code],MAX([Document No]) AS [Document No],MAX([Doc Date]) AS [Doc Date],MAX([Item Code ]) AS [Item Code ],MAX([Item Name]) AS [Item Name],MAX([Cost Center]) AS [Cost Center]" & _
        '          " ,MAX([UOM]) AS [UOM],SUM([Issued Qty]) AS [Issued Qty],SUM([Rate]) AS [Rate],SUM(Amount) AS Amount,SUM([Tax Amount]) AS [Tax Amount],SUM([Net Amount]) AS [Net Amount],Acquisition_Code AS Acquisition_Code" & _
        '          " FROM (Select Assemble.*,ACQD.Acquisition_Code from ( " & _
        '          " select TSPL_IssueItemToAssembledAsset_Detail.Asset_Code as [Asset Code],TSPL_IssueItemToAssembledAsset_Head.Doc_No as [Document No],TSPL_IssueItemToAssembledAsset_Head.Doc_Date as [Doc Date],Item_Code as [Item Code ] ,Item_Desc as [Item Name] ,Cost_Code as [Cost Center] , Unit_code as [UOM] ,(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then -TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty else TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty end) as [Issued Qty] ,Unit_Cost as [Rate] ,(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then -TSPL_IssueItemToAssembledAsset_Detail.Amount else TSPL_IssueItemToAssembledAsset_Detail.Amount end) as Amount,TSPL_IssueItemToAssembledAsset_Detail.Total_Tax_Amt as  [Tax Amount] ,(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then -TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt else TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt end) as [Net Amount]    from TSPL_IssueItemToAssembledAsset_Detail left join TSPL_IssueItemToAssembledAsset_Head on  TSPL_IssueItemToAssembledAsset_Head.Doc_No =TSPL_IssueItemToAssembledAsset_Detail.Doc_No  where TSPL_IssueItemToAssembledAsset_Detail.Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") and status=1 " & _
        '          " union all " & _
        '          " select TSPL_ASSET_WORK_HEAD.Asset_Code as [Asset Code],TSPL_ASSET_WORK_HEAD.Document_Code as [Document No],TSPL_ASSET_WORK_HEAD.Document_Date  as [Doc Date],'' as [Item Code ] ,'' as [Item Name] ,'' as [Cost Center] , '' as [UOM] ,0 as [Issued Qty] ,0 as [Rate] ,TSPL_ASSET_WORK_DETAIL.Amount   as Amount,TSPL_ASSET_WORK_DETAIL.Total_Tax_Amt as  [Tax Amount] ,TSPL_ASSET_WORK_DETAIL.Item_Net_Amt  as [Net Amount]   " & _
        '          " from TSPL_ASSET_WORK_DETAIL left join TSPL_ASSET_WORK_HEAD  on  TSPL_ASSET_WORK_DETAIL.Document_Code =TSPL_ASSET_WORK_HEAD.Document_Code  where TSPL_ASSET_WORK_DETAIL.Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") and status=1 " & _
        '          " UNION ALL" & _
        '          " select DTL.Asset_Code,Head.Document_No as [Document No],convert(date,HEAD.Vendor_Invoice_Date,103) AS Document_Date,'' as Item_Code,'' as Item_Desc,'' as Cost_Code,'' as UOM,0 as Issue_Qty,Item_Rate,(case when Head.Document_Type in ('I','C') then DTL.Amount_less_Discount else -DTL.Amount_less_Discount end) as Amount_less_Discount,DTL.Total_Tax,(case when Head.Document_Type in ('I','C') then DTL.Total_Amount else -DTL.Total_Amount end) as Total_Amount from TSPL_VENDOR_INVOICE_DETAIL DTL  inner join TSPL_VENDOR_INVOICE_HEAD HEAD on DTL.Document_No=HEAD.Document_No  WHERE HEAD.Invoice_Type='VS' AND DTL.Item_Type='A' AND DTL.Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") AND LEN(COALESCE(HEAD.Posting_Date,''))>0" & _
        '           " ) Assemble  left join TSPL_ACQUISITION_DETAIL ACQD on Assemble.[Asset Code]=ACQD.Asset_Code ) AS FINAL GROUP BY [Asset Code] ,Acquisition_Code"
        'Else
        qry = " Select Assemble.*,ACQD.Acquisition_Code from ( select TSPL_IssueItemToAssembledAsset_Detail.Asset_Code as [Asset Code],TSPL_IssueItemToAssembledAsset_Head.Doc_No as [Document No],TSPL_IssueItemToAssembledAsset_Head.Doc_Date as [Doc Date],Item_Code as [Item Code ] ,Item_Desc as [Item Name] ,Cost_Code as [Cost Center] ," & _
         " Unit_code as [UOM] ,(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then -TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty else TSPL_IssueItemToAssembledAsset_Detail.Issued_Qty end) as [Issued Qty] ,Unit_Cost as [Rate] ,(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then -TSPL_IssueItemToAssembledAsset_Detail.Amount else TSPL_IssueItemToAssembledAsset_Detail.Amount end) as Amount,TSPL_IssueItemToAssembledAsset_Detail.Total_Tax_Amt as  [Tax Amount] ,(CASE WHEN TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' then -TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt else TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt end) as [Net Amount]   " & _
         " from TSPL_IssueItemToAssembledAsset_Detail left join TSPL_IssueItemToAssembledAsset_Head on " & _
         " TSPL_IssueItemToAssembledAsset_Head.Doc_No =TSPL_IssueItemToAssembledAsset_Detail.Doc_No  where TSPL_IssueItemToAssembledAsset_Detail.Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") " & If(ShowPendingAlso = True, "", " and TSPL_IssueItemToAssembledAsset_Head.status=1") & "  " & _
         " union all " & _
         " select Asset_Code,Document_Code,Document_Date,Item_No,Item_Desc,'' as Cost_Code,'' as Unit_Code,0 as Quantity,0 as Unit_Rate,Amount,Total_Tax_Amt,Item_Net_Amt from TSPL_ASSET_ASSEMBLE_DETAIL where Distribute='Y' and Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") " & _
         " union all " & _
         " select DTL.Asset_Code,Head.Document_No as [Document No],convert(date,HEAD.Vendor_Invoice_Date,103) AS Document_Date,'' as Item_Code,'' as Item_Desc,'' as Cost_Code,'' as UOM,0 as Issue_Qty,Item_Rate,(case when Head.Document_Type in ('I','C') then DTL.Amount_less_Discount else -DTL.Amount_less_Discount end) as Amount_less_Discount,DTL.Total_Tax,(case when Head.Document_Type in ('I','C') then DTL.Total_Amount else -DTL.Total_Amount end) as Total_Amount from TSPL_VENDOR_INVOICE_DETAIL DTL " & _
        " inner join TSPL_VENDOR_INVOICE_HEAD HEAD on DTL.Document_No=HEAD.Document_No " & _
        " WHERE HEAD.Invoice_Type='VS' AND DTL.Item_Type='A' AND DTL.Asset_Code in (" & clsCommon.GetMulcallString(lstAsset) & ") " & If(ShowPendingAlso = True, "", " AND LEN(COALESCE(HEAD.Posting_Date,''))>0") & " ) Assemble " & _
        " left join TSPL_ACQUISITION_DETAIL ACQD on Assemble.[Asset Code]=ACQD.Asset_Code "
        'End If


        Return qry
    End Function
    Public Shared Function GetAssembleQuery(ByVal ACQ_Code As String, ByVal ShowPendingAlso As Boolean, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Asset_Code from TSPL_ACQUISITION_DETAIL where  Acquisition_Code='" & ACQ_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim ArryList As New ArrayList
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each strasset As DataRow In dt.Rows
                ArryList.Add(clsCommon.myCstr(strasset.Item("Asset_Code")))
            Next
            qry = GetAssembleQuery(ArryList, ShowPendingAlso)
        Else
            'Throw New Exception("No Assets found for Acqusition- " & ACQ_Code & "")
            qry = GetAssembleQuery(ArryList, ShowPendingAlso)
        End If
        Return qry
    End Function

End Class

Public Class clsItemIssueToAssembledAssetDetail
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
    Public Asset_Code As String = Nothing
    Public Asset_Date As String = Nothing
    '====================added by preeti gupta===============
    Public Capex_Code As String = Nothing
    Public Capex_SubCode As String = Nothing
    Public CapexType As String = Nothing
    Public IsCapex As Double = 0
    Public CapexQty As Double = 0
    Public CheckCapexLimit As Integer = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strFromLocation As String, ByVal strAssetCode As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsItemIssueToAssembledAssetDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsItemIssueToAssembledAssetDetail In Arr
                '' check capex budget limit and purchase value limit

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
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                '=================added by preeti gupta======================
                clsCommon.AddColumnsForChange(coll, "IsCapex", obj.IsCapex)
                clsCommon.AddColumnsForChange(coll, "CapexType", obj.CapexType)
                clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
                clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
                clsCommon.AddColumnsForChange(coll, "CapexQty", obj.CapexQty)
                clsCommon.AddColumnsForChange(coll, "CheckCapexLimit", obj.CheckCapexLimit)
                '=============================================================
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueItemToAssembledAsset_Detail", OMInsertOrUpdate.Insert, "", trans)
                '' validate iisue against purchase value done by Panch raj on 23-Jan-2018
                If clsCommon.myLen(obj.Capex_SubCode) > 0 Then
                    clsCapexBudget.CheckNegativeIssueAgainstCapexPurchaseValue(obj.Capex_SubCode, "", trans)
                    clsCapexBudget.CheckNegativeSubCapexBalance(obj.Capex_SubCode, trans)
                End If
                
                '' end validation of budget amount
                clsSerializeInvenotry.SaveData("ISSTRAN", strDocNo, dtDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)
                clsSerializeInvenotry.SaveData("ISSTRAN", strDocNo, dtDocDate, "I", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceQtyReturnForTranfserCapex(ByVal strCurrDocNo As String, ByVal strReturnNo As String, ByVal strICode As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = "select sum(Qty*RI) from (" + Environment.NewLine + _
         " select  Issued_Qty as Qty,1 as RI from TSPL_IssueItemToAssembledAsset_Detail" + Environment.NewLine + _
         " left outer join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Head.Doc_No=TSPL_IssueItemToAssembledAsset_Detail.Doc_No" + Environment.NewLine + _
         " where TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' and TSPL_IssueItemToAssembledAsset_Detail.Doc_No='" + strReturnNo + "' and TSPL_IssueItemToAssembledAsset_Detail.Item_Code='" + strICode + "'" + Environment.NewLine + _
         " union all" + Environment.NewLine + _
         " select Issued_Qty as Qty,-1 as RI from TSPL_IssueItemToAssembledAsset_Detail" + Environment.NewLine + _
         " left outer join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Head.Doc_No=TSPL_IssueItemToAssembledAsset_Detail.Doc_No" + Environment.NewLine + _
         " where TSPL_IssueItemToAssembledAsset_Head.Doc_Type='TransferCX' " + Environment.NewLine + _
         " and TSPL_IssueItemToAssembledAsset_Detail.Item_Code='" + strICode + "'" + Environment.NewLine + _
         " and TSPL_IssueItemToAssembledAsset_Head.Against_Return_No='" + strReturnNo + "' and TSPL_IssueItemToAssembledAsset_Head.Doc_No not in ('" + strCurrDocNo + "')" + Environment.NewLine + _
         " )xx  "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class

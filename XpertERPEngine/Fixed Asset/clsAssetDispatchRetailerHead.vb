
Imports common
Imports System.Data.SqlClient
Public Class clsAssetDispatchRetailerHead

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
    Public Distributor_Code As String = String.Empty
    Public IsItemWise As Boolean = Nothing
    Public Issue_ToName As String = Nothing
    Public Request_By As String = Nothing
    Public Request_ByName As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public IS_LOST As Boolean = Nothing
    Public Posting_Date As DateTime?
    Public Req_IssueNo As String = Nothing
    Public RequisitionNo As String = Nothing
    Public Route_No As String = String.Empty
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
    Public Alternate_Vehicle_Id As String = Nothing
    Public Machine_Id As String = Nothing

    Public Arr As List(Of clsAssetDispatchRetailerDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region



    Public Function SaveData(ByVal obj As clsAssetDispatchRetailerHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsAssetDispatchRetailerHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Asset Dispatch Retailer", obj.From_Location, obj.Doc_Date, trans)
            If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                clsSerializeInvenotry.DeleteData("MCC-DRISSUE", obj.Doc_No, trans)
            ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                clsSerializeInvenotry.DeleteData("MCC-DRRETURN", obj.Doc_No, trans) ''By Preeti gupta on 05/09/2016
            End If
            ''By Preeti gupta on 05/09/2016


            Dim qry As String = "delete from TSPL_ASSET_DISPATCH_RETAILER_DETAIL where Doc_No='" + obj.Doc_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.AssetDispatchRetailer, clsDocTransactionType.ItemIssue, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.AssetDispatchRetailer, clsDocTransactionType.ItemReturn, obj.To_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Transfer") = CompairStringResult.Equal Then
                    Dim strlocation As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
                    Dim chk As String = ""
                    Dim transType As String = clsDocTransactionType.SaleInvoiceExcise
                    chk = clsDBFuncationality.getSingleValue(strlocation, trans)
                    If chk = "T" Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.SaleInvoice, transType, obj.From_Location)
                    Else
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.AssetDispatchRetailer, clsDocTransactionType.ItemTransfer, obj.From_Location)
                    End If
                Else
                    Throw New Exception("Document Type is not correct")
                End If
            End If

            If (clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.From_Location, True)
            clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Location, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Issue_To", obj.Issue_To)
            clsCommon.AddColumnsForChange(coll, "Issue_No", obj.Issue_No)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
            clsCommon.AddColumnsForChange(coll, "Distributor_Code", obj.Distributor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No, True)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IS_LOST", IIf(obj.IS_LOST, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IsItemWise", IIf(obj.IsItemWise, 1, 0))
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
            clsCommon.AddColumnsForChange(coll, "Alternate_Vehicle_Id", obj.Alternate_Vehicle_Id)
            'clsCommon.AddColumnsForChange(coll, "Machine_Id", obj.Machine_Id)
            'clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            'clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_DISPATCH_RETAILER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_DISPATCH_RETAILER_HEAD", OMInsertOrUpdate.Update, "TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + obj.Doc_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsAssetDispatchRetailerDetail.SaveData(obj.Doc_No, obj.Doc_Type, obj.From_Location, obj.Doc_Date, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Doc_No, obj.arrCustomFields, trans)

            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAssetDispatchRetailerHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetDispatchRetailerHead
        Dim obj As clsAssetDispatchRetailerHead = Nothing
        Dim qry As String = "SELECT TSPL_ASSET_DISPATCH_RETAILER_HEAD.Alternate_Vehicle_Id,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type,TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location,FLocation.Location_Desc as FromLocationName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status,TSPL_ASSET_DISPATCH_RETAILER_HEAD.On_Hold,TSPL_ASSET_DISPATCH_RETAILER_HEAD.IS_LOST,TSPL_ASSET_DISPATCH_RETAILER_HEAD.IsItemWise,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Comment,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Remarks,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Posting_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To,IssueEmp.Customer_Name as IssueToName ,ISNULL(TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No,'' ) As Issue_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_Code,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Route_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Dept,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Dept_Desc,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Tax_Group,TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax_desc,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX1,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX1_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX1_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX1_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX2,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX2_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX2_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX2_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX3,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX3_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX3_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX3_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX4,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX4_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX4_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX4_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX5,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX5_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX5_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX5_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX6,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX6_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX6_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX6_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX7,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX7_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX7_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX7_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX8,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX8_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX8_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX8_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX9,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX9_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX9_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX9_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX10,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX10_Rate,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX10_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX10_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.BeforeTax_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Total_Tax_Amt,TSPL_ASSET_DISPATCH_RETAILER_HEAD.doc_Amt, TSPL_ASSET_DISPATCH_RETAILER_HEAD.vehicle_Id,  TSPL_ASSET_DISPATCH_RETAILER_HEAD.Machine_Id,Req_IssueNo,RequisitionNo  FROM TSPL_ASSET_DISPATCH_RETAILER_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on FLocation.Location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on TLocation.Location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location left outer join TSPL_SECONDARY_CUSTOMER_MASTER as IssueEmp on IssueEmp.Cust_Code= TSPL_ASSET_DISPATCH_RETAILER_HEAD.issue_To left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = (select MIN(Doc_No) from TSPL_ASSET_DISPATCH_RETAILER_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = (select Max(Doc_No) from TSPL_ASSET_DISPATCH_RETAILER_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = (select Min(Doc_No) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = (select Max(Doc_No) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAssetDispatchRetailerHead()
            obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.Doc_Date = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
            obj.From_LocationName = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
            obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
            obj.To_LocationName = clsCommon.myCstr(dt.Rows(0)("ToLocationName"))
            obj.Distributor_Code = clsCommon.myCstr(dt.Rows(0)("Distributor_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
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
            obj.IS_LOST = IIf(clsCommon.myCdbl(dt.Rows(0)("IS_LOST")) = 1, True, False)
            obj.IsItemWise = IIf(clsCommon.myCdbl(dt.Rows(0)("IsItemWise")) = 1, True, False)
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
            obj.Alternate_Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Alternate_Vehicle_Id"))
            'obj.Machine_Id = clsCommon.myCstr(dt.Rows(0)("Machine_Id"))






            qry = "SELECT TSPL_ASSET_DISPATCH_RETAILER_detail.DepositType,TSPL_ASSET_DISPATCH_RETAILER_detail.DepositReceiptNo,TSPL_ASSET_DISPATCH_RETAILER_detail.DepositValue,TSPL_ASSET_DISPATCH_RETAILER_detail.BrandName,TSPL_ASSET_DISPATCH_RETAILER_detail.PurchaseInvoiceDate,TSPL_ASSET_DISPATCH_RETAILER_detail.PurchaseInvoiceNo,TSPL_ASSET_DISPATCH_RETAILER_detail.Capacity,TSPL_ASSET_DISPATCH_RETAILER_detail.SerialNo,TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Line_No,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Desc,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.AssetID,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Required_Qty,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty , TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX1,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX1_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX1_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX2,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX2_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX2_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX3,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX3_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX3_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX4,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX4_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX4_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX5,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX5_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX5_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX6,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX6_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX6_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX7,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX7_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX7_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX8,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX8_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX8_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX9,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX9_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX9_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX10,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX10_Rate,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX10_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX1_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX2_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX3_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX4_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX5_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX6_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX7_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX8_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX9_Base_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX10_Base_Amt ,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Amount,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Total_Tax_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Net_Amt,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_Cost,Issued_Qty_AgainstRet, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Req_IssueNo, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Cost_Code,EMI_Asset_Value,EMI_No_Of_Payment_Cycle FROM TSPL_ASSET_DISPATCH_RETAILER_DETAIL where TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No='" + obj.Doc_No + "' ORDER BY TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsAssetDispatchRetailerDetail)
                Dim objTr As clsAssetDispatchRetailerDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsAssetDispatchRetailerDetail
                    objTr.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.AssetID = clsCommon.myCstr(dr("AssetID"))
                    objTr.Cost_Code = clsCommon.myCstr(dr("Cost_Code"))
                    objTr.Required_Qty = clsCommon.myCdbl(dr("Required_Qty"))
                    objTr.Issued_Qty = clsCommon.myCdbl(dr("Issued_Qty"))
                    objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(dr("Issued_Qty_AgainstRet"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Unit_Cost = clsCommon.myCdbl(dr("Unit_Cost"))
                    objTr.Req_IssueNo = clsCommon.myCstr(dr("Req_IssueNo"))
                    objTr.DepositType = clsCommon.myCstr(dr("DepositType"))
                    objTr.DepositReceiptNo = clsCommon.myCstr(dr("DepositReceiptNo"))
                    objTr.BrandName = clsCommon.myCstr(dr("BrandName"))
                    objTr.PurchaseInvoiceNo = clsCommon.myCstr(dr("PurchaseInvoiceNo"))
                    objTr.SerialNo = clsCommon.myCstr(dr("SerialNo"))
                    objTr.AssetCode = clsCommon.myCstr(dr("AssetCode"))

                    If dr("PurchaseInvoiceDate") IsNot DBNull.Value Then
                        objTr.PurchaseInvoiceDate = clsCommon.myCstr(dr("PurchaseInvoiceDate"))
                    End If
                    objTr.DepositValue = clsCommon.myCdbl(dr("DepositValue"))
                    objTr.Capacity = clsCommon.myCdbl(dr("Capacity"))

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

                    objTr.EMI_Asset_Value = clsCommon.myCdbl(dr("EMI_Asset_Value"))
                    objTr.EMI_No_Of_Payment_Cycle = clsCommon.myCdbl(dr("EMI_No_Of_Payment_Cycle"))
                    Dim strTranType As String = "MCC-AISSUE"
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Return") = CompairStringResult.Equal Then
                        strTranType = "MCC-ARETURN"
                    End If
                    objTr.arrSrItem = clsSerializeInvenotry.GetData(strTranType, objTr.Doc_No, objTr.Item_Code, objTr.Line_No, trans)  ''By Preeti gupta on 05/09/2016

                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function


    Public Shared Function GetAssetDepVale(ByVal DocDate As DateTime, ByVal StrLocation As String, ByVal strAssetCode As String) As Double
        Dim dblDepValueofAsset As Double = 0
        Dim strQry As String = " with cte as (   select XXXFinal.SNO,  XXXFinal.Asset_Code ,XXXFinal.Asset_Name , XXXFinal.Asset_Specification, XXXFinal.Loc_Code,XXXFinal.Location_Desc,XXXFinal.Vendor_Code,XXXFinal.Vendor_Name ,   XXXfinal.Acquisition_Date, XXXfinal.Acquisition_Code, XXXfinal.Templete_Code,XXXfinal.Category_Code,XXXfinal.CatDesc,XXXfinal.Group_Code,XXXfinal.GrpDesc,   XXXfinal.AcSet_Code,XXXfinal.CostCenter_Code,XXXfinal.CostCenter_Name,XXXfinal.Dep_Method_Code,XXXfinal.Dep_period_Code,XXXfinal.Start_Date,XXXfinal.Dep_Rate,XXXfinal.Dep_Tax_Rate,   XXXfinal.Book_Estimated_Life,XXXfinal.OriginalBookValue,XXXfinal.SRN_No,XXXFinal.Document_Code,XXXFinal.Document_Date,XXXFinal.BookValue, case when XXXFinal.SNO = 1 then  XXXFinal.[Opening Depreciation]  Else 0 End [Opening Depreciation],XXXFinal.BookValue- XXXFinal.[Opening Depreciation]+XXXFinal.Addition_amount As [Opening_Value] , XXXFinal.Addition_amount  ,XXXFinal. Dep_Amount , XXXFinal. Dep_Amount + XXXFinal.[Opening Depreciation] As [Total Depreciation],  XXXfinal.DepRate,XXXFinal.Asset_Value - XXXFinal.[Opening Depreciation] - XXXFinal.dep_amount As 'Asset_Value',   case when XXXFinal.SNO = 1 then XXXFinal.Asset_Value - XXXFinal.[Opening Depreciation] - XXXFinal.dep_amount else -1*XXXFinal.dep_amount end as AssetValueFinal  ,case when XXXFinal.SNO = 1 then  XXXFinal.[Opening Depreciation]+XXXFinal.Dep_Amount  Else XXXFinal. Dep_Amount End as DepValueFinal  from (  select ROW_NUMBER() OVER(PARTITION BY TSPL_ACQUISITION_DETAIL.Asset_Code ORDER BY TSPL_ACQUISITION_DETAIL.Asset_Code,DepBook.Document_Date  ASC) as SNO,   TSPL_ACQUISITION_DETAIL.Asset_Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ACQUISITION_DETAIL.Asset_Specification,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LOCATION_MASTER.Location_Desc, DepBook.Document_Code,DepBook.Document_Date,TSPL_ACQUISITION_HEAD.Acquisition_Date,TSPL_ACQUISITION_HEAD.Acquisition_Code,TSPL_ACQUISITION_DETAIL.Templete_Code,TSPL_ACQUISITION_DETAIL.Category_code,TSPL_ASSET_CATEGORY.Description as CatDesc,  TSPL_ACQUISITION_DETAIL.Group_Code,TSPL_ASSET_GROUP.Description as GrpDesc ,TSPL_ACQUISITION_DETAIL.AcSet_Code,TSPL_ACQUISITION_DETAIL.CostCenter_Code,TSPL_FA_COST_CENTER_MASTER.CostCenter_Name,TSPL_ACQUISITION_DETAIL.Dep_Method_Code,TSPL_ACQUISITION_DETAIL.Dep_Period_Code,TSPL_ACQUISITION_DETAIL.Start_Date,TSPL_ACQUISITION_DETAIL.Dep_Rate, TSPL_ACQUISITION_DETAIL.Dep_Tax_Rate,TSPL_ACQUISITION_DETAIL.Book_Estimated_Life,TSPL_ACQUISITION_DETAIL.Book_Source_value as BookValue,TSPL_ACQUISITION_DETAIL.Book_Source_Original_value as OriginalBookValue,TSPL_ACQUISITION_DETAIL.SRN_No,isnull(TSPL_ACQUISITION_DETAIL.depreciated_value,0) as [Opening Depreciation],0 as Final_Dep_Amount,0+TSPL_ACQUISITION_DETAIL.Depreciated_Value as [Total Final Depreeciated],  0 as Final_Dep_Rate,0 as Final_Dep_Rate_tax,0 as Final_Dep_Amount_Tax,coalesce(AW.Addition_Amount,0) as Addition_Amount,(TSPL_ACQUISITION_DETAIL.Book_Source_value+coalesce(AW.Addition_Amount,0)) as Asset_Value,(TSPL_ACQUISITION_DETAIL.Book_Source_value+coalesce(AW.Addition_Amount,0)-0) as Asset_Value_Tax,  TSPL_ACQUISITION_DETAIL.Total_Tax_Amt,TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate,TSPL_ACQUISITION_DETAIL.Book_Salvage_Value,TSPL_ACQUISITION_DETAIL.Is_Assembled,TSPL_ACQUISITION_HEAD.Acquisition_Type ,isnull(DepBook.Additional_Amount,0) as Additional_Amount,isnull( DepBook.Dep_Amount,0) as Dep_Amount,   isnull(DepBook.DepRate,0) as  DepRate  from TSPL_ACQUISITION_DETAIL   inner join TSPL_ACQUISITION_HEAD  on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code   left outer join TSPL_LOCATION_MASTER on TSPL_ACQUISITION_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code        left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_ACQUISITION_DETAIL.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code        left outer join TSPL_ASSET_CATEGORY on TSPL_ACQUISITION_DETAIL.Category_code=TSPL_ASSET_CATEGORY.Category_Code     left outer join TSPL_ASSET_GROUP on TSPL_ACQUISITION_DETAIL.Group_Code=TSPL_ASSET_GROUP.Group_Code    left outer join TSPL_VENDOR_MASTER on TSPL_ACQUISITION_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code   left outer Join   (select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense as Additional_Amount,Dep_Amount,deprate,Asset_value from ( select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value, Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep from TSPL_ASSET_DEPRECIATION  where TSPL_ASSET_DEPRECIATION.Document_Date<='18/Nov/2021 11:59:59 PM'  and  TSPL_ASSET_DEPRECIATION.Location_Code='" & StrLocation & "' 
    and TSPL_ASSET_DEPRECIATION.Asset_Code ='" & strAssetCode & "'
    ) DepBook) DepBook on TSPL_ACQUISITION_DETAIL.Asset_Code = DepBook.Asset_Code      left join (select Asset_Code,sum(Net_Amt) as Addition_Amount from  TSPL_ASSET_WORK_HEAD where Status=1   and  TSPL_ASSET_WORK_HEAD.Location_Code ='" & StrLocation & "'
    and TSPL_ASSET_WORK_HEAD.Asset_Code ='" & strAssetCode & "'
    group by Asset_Code) as AW on TSPL_ACQUISITION_DETAIL.Asset_Code=AW.Asset_Code     where 2=2    and  TSPL_ACQUISITION_HEAD.Loc_Code ='" & StrLocation & "' 
    and TSPL_ACQUISITION_DETAIL.Asset_Code ='" & strAssetCode & "'
    and Convert(date, TSPL_ACQUISITION_HEAD.Acquisition_Date,103) <= Convert(date, '" & DocDate & "' ,103)   )  XXXFinal   )     select min ( Asset_Value) as Asset_Value from (  select  Asset_Code ,Asset_Name , Asset_Specification, Loc_Code,Location_Desc,Vendor_Code,Vendor_Name ,convert(varchar,Acquisition_Date,103) as Acquisition_Date, Acquisition_Code, Templete_Code,Category_Code,CatDesc,Group_Code,GrpDesc,AcSet_Code,CostCenter_Code,CostCenter_Name,Dep_Method_Code,Dep_period_Code,convert(varchar,[Start_Date],103) as [Start_Date],Dep_Rate,Dep_Tax_Rate, Book_Estimated_Life,OriginalBookValue,SRN_No,Document_Code as  Document_Code,case when len (isnull(Document_Code,'')) > 0 then  convert (varchar,Document_Date,103) else null end as [Document Date],BookValue,  [Opening Depreciation],case when SNO = 1 then  Opening_Value else Asset_Value + Dep_Amount end Opening_Value , Addition_amount ,Dep_Amount , case when SNO = 1 then [Total Depreciation] else Dep_Amount end as [Total Depreciation] ,DepValueFinal ,DepRate, Asset_Value  from (   SELECT SNO, Asset_Code ,Asset_Name , Asset_Specification, Loc_Code,Location_Desc,Vendor_Code,Vendor_Name ,   Acquisition_Date, Acquisition_Code, Templete_Code,Category_Code,CatDesc,Group_Code,GrpDesc,   AcSet_Code,CostCenter_Code,CostCenter_Name,Dep_Method_Code,Dep_period_Code,[Start_Date],Dep_Rate,Dep_Tax_Rate, Book_Estimated_Life,OriginalBookValue,SRN_No,   Document_Code,Document_Date,BookValue,  [Opening Depreciation],  BookValue + Addition_amount - [Opening Depreciation] as Opening_Value , Addition_amount ,Dep_Amount , [Total Depreciation],DepRate,   (SELECT SUM(AssetValueFinal)   FROM cte b WHERE b.SNO <= a.SNO and b.Asset_code = a.Asset_Code) AS Asset_Value  ,(SELECT SUM(DepValueFinal) FROM cte b WHERE b.SNO <= a.SNO and b.Asset_code = a.Asset_Code) AS DepValueFinal  FROM cte a   ) Final   )XFinal group by XFinal.Asset_Code  order by XFinal.Asset_Code "

        dblDepValueofAsset = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return dblDepValueofAsset
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal obj As clsAssetDispatchRetailerHead, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False, Optional ByVal IsDairyModule As Boolean = False, Optional ByVal FromDateForAvg As Date? = Nothing, Optional ByVal ExtraWhrForAvg As String = Nothing) As Boolean
        Dim TransType_Str As String = ""
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        If UpdateInventory = True Then
            clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Doc_No & "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.Doc_No & "'", trans)
        End If
        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        Dim isSaved As Boolean = True
        '--------------For From Location (Only Out Entry For DocType Issue)-----------------------------------------------------------------------------
        If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Issue") = CompairStringResult.Equal Then
            For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr

                Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = obj.From_Location
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) 'objTr.Issued_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code

                objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                objInventoryMovemnt.Other_Location_Code = obj.To_Location
                objInventoryMovemnt.Other_Location_Desc = obj.To_LocationName

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
            isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-DRISSUE", obj.Doc_No, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        End If

        ''For To Location (Only In Entry For DocType Return)-----------------------------------------------------------------------------------------------------------------------
        If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Return") = CompairStringResult.Equal AndAlso obj.IS_LOST = False Then
            Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr

                Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)


                Dim objInventoryMovemnt1 As New clsInventoryMovement()
                objInventoryMovemnt1.InOut = "I"
                objInventoryMovemnt1.Location_Code = obj.To_Location
                objInventoryMovemnt1.Other_Location_Code = obj.From_Location
                objInventoryMovemnt1.Other_Location_Desc = obj.From_LocationName
                objInventoryMovemnt1.Item_Code = objTr.Item_Code
                objInventoryMovemnt1.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt1.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) ' objTr.Issued_Qty
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


            isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-DRRETURN", obj.Doc_No, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)

        End If

        Return True
    End Function
    Public Shared Function CreateJournalEntry(ByVal obj As clsAssetDispatchRetailerHead , ByVal trans As SqlTransaction, ByVal strVoucherNo As String) As Boolean

        Dim Sql As String = ""


        Dim CreateGLAccToItem As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateGLAccToItem, clsFixedParameterCode.CreateGLAccToItem, trans)) = 1, True, False)
        Dim strFromInvAcc As String = ""
            Dim strFromInvAccDesc As String = ""
            Dim strToInvAcc As String = ""
            Dim strToInvAccDesc As String = ""
            Dim strShpClrAcc As String = ""
            Dim strShpClrAccDesc As String = ""
        If obj.Doc_Type = "Return" Then
            obj.From_Location = obj.To_Location
        End If
        ''--------------
        Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
            Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
            Dim objSeg As Accountsegment = Nothing
            Dim arrlist As New ArrayList()

            ''If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                 " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
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
                        arrlist.Add(BankAcc)
                        clsInventoryMovement.UpdateInvControlAccount(obj.Doc_No, "MCC-DRISSUE", objTr.Item_Code, strFromInvAcc, "", "", trans)

                    Else
                        Dim BankAcc() As String = {strFromInvAcc, objTr.Amount * -1, "", "", "", "", "", "", "I"}
                        arrlist.Add(BankAcc)

                        clsInventoryMovement.UpdateInvControlAccount(obj.Doc_No, "MCC-DRISSUE", objTr.Item_Code, "", strFromInvAcc, "", trans)
                    End If
                Next
                Dim strFrmFilledAcc As String = Nothing

                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr
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
                            Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost}
                            arrlist.Add(FrmFilledAcc)
                        End If
                    Next

                    ''==================Done By Monika========for recreation of voucher
                    If clsCommon.myLen(strVoucherNo) < 0 Then
                        Dim qry1 As String = "select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code='AD-IS' and Source_Doc_No='" + obj.Doc_No + "' "
                        strVoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    End If


                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        transportSql.FunGrnlEntryWithTrans(obj.From_Location, False, strVoucherNo, trans, obj.Doc_Date, obj.Comment, "AD-IS", "Asset Dispatch Retailer Issue", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    Else
                        transportSql.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "AD-IS", "Asset Dispatch Retailer Issue", obj.Doc_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    ''richa agarwal 2 Dec,2016
                    For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr
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
                            Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost * -1, "", ""}
                            arrlist.Add(FrmFilledAcc)
                        End If
                    Next

                    If clsCommon.myLen(strVoucherNo) < 0 Then
                        Dim qry1 As String = "select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code='AD-RE' and Source_Doc_No='" + obj.Doc_No + "' "
                        strVoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                    End If


                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        transportSql.FunGrnlEntryWithTrans(obj.From_Location, False, strVoucherNo, trans, obj.Doc_Date, obj.Comment, "AD-RE", "Asset Dispatch Retailer Return", obj.Doc_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    Else
                        transportSql.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "AD-RE", "Asset Dispatch Retailer Return", obj.Doc_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
                    End If

                End If
            ''End If
            Return True
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

            Dim obj As clsAssetDispatchRetailerHead = clsAssetDispatchRetailerHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.Doc_No + " Is On Hold.Can't Post it")
            End If
            If obj.IsItemWise Then
                UpdateInventoryMovement(obj, trans, False)
                CreateJournalEntry(obj, trans, "")
            End If
            '--------------For From Location (Only Out Entry For DocType Issue)-----------------------------------------------------------------------------
            'If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Issue") = CompairStringResult.Equal Then
            '    Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            '    Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            '    For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr
            '        Dim objLocationDetails As New clsItemLocationDetails()
            '        objLocationDetails.Item_Code = objTr.Item_Code
            '        objLocationDetails.Item_Desc = objTr.Item_Desc
            '        objLocationDetails.Location_Code = obj.From_Location
            '        objLocationDetails.Location_Desc = obj.From_LocationName
            '        objLocationDetails.Item_Qty = IIf(obj.Doc_Type = "Return", -1 * objTr.Issued_Qty_AgainstRet, -1 * objTr.Issued_Qty) '-1 * (objTr.Issued_Qty)
            '        objLocationDetails.Amount = -1 * (objTr.Amount)

            '        'objLocationDetails.MRP = 0
            '        'If objTr.MFG_Date.HasValue Then
            '        '    objLocationDetails.MFG_Date = objTr.MFG_Date
            '        'End If
            '        'objLocationDetails.Batch_No = objTr.Batch_No
            '        'If objTr.Expiry_Date.HasValue Then
            '        '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
            '        'End If

            '        Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
            '        Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
            '        If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
            '            objLocationDetails.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
            '            objLocationDetails.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
            '            objLocationDetails.ItemType = "FT"
            '        End If
            '        'objLocationDetails.ItemType = itemtype
            '        ArrLocationDetails.Add(objLocationDetails)
            '        Dim objInventoryMovemnt As New clsInventoryMovement()
            '        objInventoryMovemnt.InOut = "O"
            '        objInventoryMovemnt.Location_Code = obj.From_Location
            '        objInventoryMovemnt.Item_Code = objTr.Item_Code
            '        objInventoryMovemnt.Item_Desc = objTr.Item_Desc
            '        objInventoryMovemnt.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) 'objTr.Issued_Qty
            '        objInventoryMovemnt.UOM = objTr.Unit_code



            '        If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.createDebitNoteOnAssetIssue, clsFixedParameterCode.createDebitNoteOnAssetIssue, trans)) = 1, True, False)) = True Then
            '            If obj.IS_LOST = False AndAlso obj.On_Hold = False Then
            '                If objTr.EMI_Asset_Value > 0 Then
            '                    objInventoryMovemnt.Basic_Cost = objTr.EMI_Asset_Value
            '                Else
            '                    objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
            '                End If
            '            Else
            '                objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
            '            End If
            '        Else
            '            objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
            '        End If



            '        objInventoryMovemnt.Other_Location_Code = obj.To_Location
            '        objInventoryMovemnt.Other_Location_Desc = obj.To_LocationName

            '        'objInventoryMovemnt.FIFO_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, objTr.Item_Code, obj.From_Location, objTr.Issued_Qty, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), True, trans)
            '        'objInventoryMovemnt.LIFO_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, objTr.Item_Code, obj.From_Location, objTr.Issued_Qty, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), True, trans)
            '        'objInventoryMovemnt.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.From_Location, objTr.Issued_Qty, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), True, trans)

            '        objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
            '        objInventoryMovemnt.Net_Cost = objTr.Item_Net_Amt
            '        'If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
            '        '    objInventoryMovemnt.ItemType = "RM"
            '        'ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
            '        '    objInventoryMovemnt.ItemType = "OT"
            '        'ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
            '        '    objInventoryMovemnt.ItemType = "FT"
            '        'End If
            '        If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "FT"
            '        ElseIf clsCommon.CompairString(type, "A") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "A"
            '        End If
            '        'objInventoryMovemnt.ItemType = itemtype
            '        ArrInventoryMovement.Add(objInventoryMovemnt)
            '    Next
            '    'isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(PostDate, ArrLocationDetails, trans)
            '    'isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-AISSUE", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement, trans)
            '    'If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CtreateJEOfVspAssetIssueAndReturn, clsFixedParameterCode.CtreateJEOfVspAssetIssueAndReturn, trans)) = 1, True, False)) = True Then
            '    '    CreateJournalEntry(obj, trans, "")
            '    'End If

            '    'If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.createDebitNoteOnAssetIssue, clsFixedParameterCode.createDebitNoteOnAssetIssue, trans)) = 1, True, False)) = True Then
            '    '    If obj.IS_LOST = False AndAlso obj.On_Hold = False Then
            '    '        CreateAPDebitNoteEntry(obj, "D", trans)
            '    '    End If
            '    'End If
            'End If

            ''For To Location (Only In Entry For DocType Return)-----------------------------------------------------------------------------------------------------------------------
            'If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Return") = CompairStringResult.Equal AndAlso obj.IS_LOST = False Then
            '    Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            '    Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            '    For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr
            '        If objTr.EMI_Asset_Value > 0 Then
            '            Dim dtBal As DataTable = clsDBFuncationality.GetDataTable(clsAPInvoiceAssetEMIDetails.GetVSPAssetEMIQuery(obj.Issue_No, objTr.Item_Code), trans)
            '            Dim dblBalanceEMIAmt As Double = 0
            '            If dtBal IsNot Nothing AndAlso dtBal.Rows.Count > 0 Then
            '                dblBalanceEMIAmt = clsCommon.myCdbl(dtBal.Rows(0)("Installment_Amt"))
            '            End If
            '            If objTr.EMI_Asset_Value > dblBalanceEMIAmt Then
            '                Throw New Exception("Installment Balance Amount is " + clsCommon.myFormat(dblBalanceEMIAmt) + "You cannot enter more than Balance Amount in Asset Value")
            '            End If
            '        End If

            '        Dim objLocationDetails1 As New clsItemLocationDetails()
            '        objLocationDetails1.Item_Code = objTr.Item_Code
            '        objLocationDetails1.Item_Desc = objTr.Item_Desc
            '        objLocationDetails1.Location_Code = obj.To_Location
            '        objLocationDetails1.Location_Desc = obj.To_LocationName
            '        objLocationDetails1.Item_Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) ' objTr.Issued_Qty
            '        objLocationDetails1.Amount = objTr.Amount
            '        'objLocationDetails.MRP = 0
            '        'If objTr.MFG_Date.HasValue Then
            '        '    objLocationDetails.MFG_Date = objTr.MFG_Date
            '        'End If
            '        'objLocationDetails.Batch_No = objTr.Batch_No
            '        'If objTr.Expiry_Date.HasValue Then
            '        '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
            '        'End If

            '        Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
            '        Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
            '        If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
            '            objLocationDetails1.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
            '            objLocationDetails1.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
            '            objLocationDetails1.ItemType = "FT"
            '        End If
            '        'objLocationDetails.ItemType = itemtype
            '        ArrLocationDetails1.Add(objLocationDetails1)


            '        Dim objInventoryMovemnt1 As New clsInventoryMovement()
            '        objInventoryMovemnt1.InOut = "I"
            '        objInventoryMovemnt1.Location_Code = obj.To_Location
            '        objInventoryMovemnt1.Other_Location_Code = obj.From_Location
            '        objInventoryMovemnt1.Other_Location_Desc = obj.From_LocationName
            '        objInventoryMovemnt1.Item_Code = objTr.Item_Code
            '        objInventoryMovemnt1.Item_Desc = objTr.Item_Desc
            '        objInventoryMovemnt1.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) ' objTr.Issued_Qty
            '        objInventoryMovemnt1.UOM = objTr.Unit_code
            '        objInventoryMovemnt1.Basic_Cost = objTr.Unit_Cost
            '        ''''objInventoryMovemnt.Rec_Cost= objTr.MRP
            '        objInventoryMovemnt1.Add_Cost = objTr.Total_Tax_Amt
            '        objInventoryMovemnt1.Net_Cost = objTr.Item_Net_Amt
            '        If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
            '            objInventoryMovemnt1.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
            '            objInventoryMovemnt1.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
            '            objInventoryMovemnt1.ItemType = "FT"
            '        End If
            '        ' objInventoryMovemnt.ItemType = itemtype
            '        ArrInventoryMovement1.Add(objInventoryMovemnt1)
            '    Next

            '    isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(PostDate, ArrLocationDetails1, trans)
            '    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-ARETURN", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement1, trans)
            '    ''If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CtreateJEOfVspAssetIssueAndReturn, clsFixedParameterCode.CtreateJEOfVspAssetIssueAndReturn, trans)) = 1, True, False)) = True Then
            '    ''    CreateJournalEntry(obj, trans, "")
            '    ''End If

            'End If
            'If obj.Doc_Type = "Transfer" Then

            '    Dim Sql As String = ""
            '    Dim strFromInvAcc As String = ""
            '    Dim strFromInvAccDesc As String = ""
            '    Dim strToInvAcc As String = ""
            '    Dim strToInvAccDesc As String = ""
            '    Dim strShpClrAcc As String = ""
            '    Dim strShpClrAccDesc As String = ""
            '    Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
            '    Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
            '    Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.To_Location) + "'"
            '    Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)
            '    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
            '   " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
            '    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
            '    strFromInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '    strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            '    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
            '    strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
            '    If strFromInvAccDesc Is Nothing Then
            '        Throw New Exception("Inventory Control Account " + strFromInvAcc + " not found.")
            '    End If
            '    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
            '    strToInvAccDesc = connectSql.RunScalar(trans, Sql)
            '    If strToInvAccDesc Is Nothing Then
            '        Throw New Exception("Inventory Control Account " + strToInvAcc + " not found.")
            '    End If
            '    strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, obj.From_Location, trans)
            '    Dim objSeg As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
            '    ' connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
            '    Dim arrlist As New ArrayList()
            '    Dim BankAcc() As String = {strFromInvAcc, obj.BeforeTax_Amt * (-1)}
            '    arrlist.Add(BankAcc)


            '    Dim taxAmt As Decimal
            '    Dim ttlTotalTaxAmt As Decimal = 0
            '    Dim strTaxCode As String
            '    Dim strNetPayAcc As String = ""
            '    Dim strNetPayAccDesc As String = ""

            '    '''' Tax1 ''''''***********************
            '    strTaxCode = obj.TAX1
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX1_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX1_Amt), 2)
            '        Else
            '            If obj.TAX1_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX1_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax1

            '    '''' Tax2 ''''''***********************
            '    strTaxCode = obj.TAX2
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX2_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX2_Amt), 2)
            '        Else
            '            If obj.TAX2_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX2_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax2

            '    '''' Tax3 ''''''***********************
            '    strTaxCode = obj.TAX3
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX3_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX3_Amt), 2)
            '        Else
            '            If obj.TAX3_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX3_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax3

            '    '''' Tax4 ''''''***********************
            '    strTaxCode = obj.TAX4
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX4_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX4_Amt), 2)
            '        Else
            '            If obj.TAX4_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX4_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            ' connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax4

            '    '''' Tax5 ''''''***********************
            '    strTaxCode = obj.TAX5
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX5_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX5_Amt), 2)
            '        Else
            '            If obj.TAX5_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX5_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax5

            '    '''' Tax6 ''''''***********************
            '    strTaxCode = obj.TAX6
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX6_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX6_Amt), 2)
            '        Else
            '            If obj.TAX5_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX6_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax6

            '    '''' Tax7 ''''''***********************
            '    strTaxCode = obj.TAX7
            '    If clsCommon.myLen(strTaxCode) > 0 Then
            '        If obj.TAX7_Amt.ToString().Substring(0, 1) = "-" Then
            '            taxAmt = Math.Round(CDec(obj.TAX7_Amt), 2)
            '        Else
            '            If obj.TAX5_Amt = 0 Then
            '                taxAmt = 0
            '            Else
            '                taxAmt = Math.Round(CDec(obj.TAX7_Amt), 2)
            '            End If
            '        End If
            '        Sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            '        If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
            '            strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            '        End If
            '        If Not strNetPayAcc = "" Then
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
            '            strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            '        End If
            '        If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
            '            strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
            '            Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
            '            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
            '            'lineNo = lineNo + 1
            '            Dim Tax1() As String = {strNetPayAcc, taxAmt * (-1)}
            '            arrlist.Add(Tax1)
            '        End If
            '        ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            '    End If
            '    '*********** End Tax7

            '    ' fromShipmentCogs = fromShipmentCogs + ttlTotalTaxAmt

            '    strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.To_Location, trans)
            '    objSeg = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
            '    'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
            '    'lineNo = lineNo + 1

            '    Dim DocAmtWithTax() As String = {strToInvAcc, obj.doc_Amt}
            '    arrlist.Add(DocAmtWithTax)


            '    Dim strFrmFilledAcc As String = Nothing
            '    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
            '    Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            '    If strFrmFilledAccFirst Is Nothing Then
            '        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
            '    Else
            '        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
            '        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
            '        Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            '        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
            '        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

            '        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
            '        objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
            '        'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
            '        'lineNo = lineNo + 1
            '        Dim FrmFilledAcc() As String = {strFrmFilledAcc, obj.doc_Amt}
            '        arrlist.Add(FrmFilledAcc)
            '    End If
            '    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
            '    Dim strToFilledAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            '    If strToFilledAcc Is Nothing Then
            '        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
            '    Else
            '        strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
            '        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
            '        Dim strTOFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            '        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
            '        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
            '        strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
            '        objSeg = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
            '        'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
            '        'lineNo = lineNo + 1
            '        Dim ToFilledAcc() As String = {strToFilledAcc, obj.doc_Amt * -1}
            '        arrlist.Add(ToFilledAcc)
            '    End If
            '    '' BHA/30/10/18-000646 RICHA AGARWAL SEND vENDOR CODE AND VENDOR NAME INTO JOURNAL ENTRY AND TYPE V instead of C 30 Oct,2018
            '    transportSql.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "VSP-TF", "VSP Transfer", obj.Doc_No, "", "V", obj.Issue_To, clsVendorMaster.GetName(obj.Issue_To, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
            'End If

            Dim qry As String = "Update TSPL_ASSET_DISPATCH_RETAILER_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
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

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsAssetDispatchRetailerHead = clsAssetDispatchRetailerHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.DeleteData("MCC-DRISSUE", strCode, trans)  ''By Preeti gupta on 05/09/2016
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.DeleteData("MCC-DRRETURN", strCode, trans)  ''By Preeti gupta on 05/09/2016
                End If
                Dim qry As String = "delete from TSPL_ASSET_DISPATCH_RETAILER_DETAIL where Doc_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No='" + strCode + "'"
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
            Dim obj As clsAssetDispatchRetailerHead = clsAssetDispatchRetailerHead.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Reverse")
            End If

            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

            If Not obj.Status = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            If obj.IsItemWise Then
                qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type in ('MCC-DRISSUE','MCC-DRRETURN')  and document_code='" + obj.Doc_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                ''===================================================================

                qry = "Delete from TSPL_INVENTORY_MOVEMENT WHERE Trans_Type in ('MCC-DRISSUE','MCC-DRRETURN') AND Source_Doc_No='" + obj.Doc_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)



                Dim Sql As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select voucher_no from TSPL_JOURNAL_MASTER WHERE Source_Code in ('AD-IS','AD-RE') and Source_Doc_No='" + obj.Doc_No + "' )"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_JOURNAL_MASTER WHERE Source_Code in ('AD-IS','AD-RE') and Source_Doc_No='" + obj.Doc_No + "' "
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

            End If
            qry = "Update TSPL_ASSET_DISPATCH_RETAILER_HEAD set Status=0, Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsAssetDispatchRetailerDetail
#Region "Variables"
    Public Doc_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Cost_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public AssetID As String = String.Empty
    Public Required_Qty As Double = 0
    Public Issued_Qty As Double = 0
    Public Issued_Qty_AgainstRet As Double = 0
    Public Unit_code As String = Nothing
    Public Unit_Cost As Double = 0
    Public Req_IssueNo As String = Nothing

    Public DepositType As String = Nothing
    Public DepositReceiptNo As String = Nothing
    Public BrandName As String = Nothing
    Public Capacity As Double = 0
    Public DepositValue As Double = 0
    Public PurchaseInvoiceNo As String = Nothing
    Public SerialNo As String = Nothing
    Public AssetCode As String = Nothing
    Public PurchaseInvoiceDate As DateTime? = Nothing
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
    Public EMI_Asset_Value As Double = 0
    Public EMI_No_Of_Payment_Cycle As Double = 0

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing



#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strDocType As String, ByVal strFromLocation As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsAssetDispatchRetailerDetail), ByVal trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetDispatchRetailerDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "AssetID", obj.AssetID)
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

                clsCommon.AddColumnsForChange(coll, "EMI_Asset_Value", obj.EMI_Asset_Value)
                clsCommon.AddColumnsForChange(coll, "EMI_No_Of_Payment_Cycle", obj.EMI_No_Of_Payment_Cycle)


                clsCommon.AddColumnsForChange(coll, "AssetCode", obj.AssetCode)
                clsCommon.AddColumnsForChange(coll, "SerialNo", obj.SerialNo)
                clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
                clsCommon.AddColumnsForChange(coll, "PurchaseInvoiceNo", obj.PurchaseInvoiceNo)

                clsCommon.AddColumnsForChange(coll, "DepositValue", obj.DepositValue)
                clsCommon.AddColumnsForChange(coll, "BrandName", obj.BrandName)
                clsCommon.AddColumnsForChange(coll, "DepositReceiptNo", obj.DepositReceiptNo)
                clsCommon.AddColumnsForChange(coll, "DepositType", obj.DepositType)

                If clsCommon.myLen(obj.PurchaseInvoiceDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "PurchaseInvoiceDate", clsCommon.GetPrintDate(obj.PurchaseInvoiceDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "PurchaseInvoiceDate", Nothing, True)
                End If

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_DISPATCH_RETAILER_DETAIL", OMInsertOrUpdate.Insert, "", trans)



                If clsCommon.CompairString(strDocType, "Issue") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.SaveData("MCC-AISSUE", strDocNo, dtDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)  ''By Preeti gupta on 05/09/2016
                ElseIf clsCommon.CompairString(strDocType, "Return") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.SaveData("MCC-ARETURN", strDocNo, dtDocDate, "I", obj.Item_Code, strFromLocation, obj.Line_No, obj.arrSrItem, trans)  ''By Preeti gupta on 05/09/2016
                End If


            Next
        End If
        Return True
    End Function
End Class

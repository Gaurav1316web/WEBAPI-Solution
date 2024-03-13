
Imports common
Imports System.Data.SqlClient
Public Class clsMCCIssueReturnHead

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
    Public IS_LOST As Boolean = Nothing
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

    Public Arr As List(Of clsMCCIssueReturnDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region



    Public Function SaveData(ByVal obj As clsMCCIssueReturnHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsMCCIssueReturnHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPAssetIssue, obj.From_Location, obj.Doc_Date, trans)
            If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                clsSerializeInvenotry.DeleteData("MCC-AISSUE", obj.Doc_No, trans)
            ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                clsSerializeInvenotry.DeleteData("MCC-ARETURN", obj.Doc_No, trans) ''By Preeti gupta on 05/09/2016
            End If
            ''By Preeti gupta on 05/09/2016


            Dim qry As String = "delete from TSPL_VSPAsset_DETAIL where Doc_No='" + obj.Doc_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.VSPAssetIssue, clsDocTransactionType.ItemIssue, obj.From_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.VSPAssetIssue, clsDocTransactionType.ItemReturn, obj.To_Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Transfer") = CompairStringResult.Equal Then
                    Dim strlocation As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
                    Dim chk As String = ""
                    Dim transType As String = clsDocTransactionType.SaleInvoiceExcise
                    chk = clsDBFuncationality.getSingleValue(strlocation, trans)
                    If chk = "T" Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.SaleInvoice, transType, obj.From_Location)
                    Else
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Doc_Date), clsDocType.VSPAssetIssue, clsDocTransactionType.ItemTransfer, obj.From_Location)
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
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IS_LOST", IIf(obj.IS_LOST, 1, 0))
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
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSPAsset_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSPAsset_HEAD", OMInsertOrUpdate.Update, "TSPL_VSPAsset_HEAD.Doc_No='" + obj.Doc_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMCCIssueReturnDetail.SaveData(obj.Doc_No, obj.Doc_Type, obj.From_Location, obj.Doc_Date, Arr, trans)
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

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMCCIssueReturnHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMCCIssueReturnHead
        Dim obj As clsMCCIssueReturnHead = Nothing
        Dim qry As String = "SELECT TSPL_VSPAsset_HEAD.Doc_No,TSPL_VSPAsset_HEAD.Doc_Date,TSPL_VSPAsset_HEAD.Doc_Type,TSPL_VSPAsset_HEAD.From_Location,FLocation.Location_Desc as FromLocationName,TSPL_VSPAsset_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_VSPAsset_HEAD.Status,TSPL_VSPAsset_HEAD.On_Hold,TSPL_VSPAsset_HEAD.IS_LOST,TSPL_VSPAsset_HEAD.Comment,TSPL_VSPAsset_HEAD.Remarks,TSPL_VSPAsset_HEAD.Posting_Date,TSPL_VSPAsset_HEAD.Issue_To,IssueEmp.Emp_Name as IssueToName ,ISNULL(TSPL_VSPAsset_HEAD.Issue_No,'' ) As Issue_No,TSPL_VSPAsset_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_VSPAsset_HEAD.Dept,TSPL_VSPAsset_HEAD.Dept_Desc,TSPL_VSPAsset_HEAD.Tax_Group,TSPL_VSPAsset_HEAD.tax_desc,TSPL_VSPAsset_HEAD.TAX1,TSPL_VSPAsset_HEAD.TAX1_Rate,TSPL_VSPAsset_HEAD.TAX1_Amt,TSPL_VSPAsset_HEAD.TAX1_Base_Amt,TSPL_VSPAsset_HEAD.TAX2,TSPL_VSPAsset_HEAD.TAX2_Rate,TSPL_VSPAsset_HEAD.TAX2_Amt,TSPL_VSPAsset_HEAD.TAX2_Base_Amt,TSPL_VSPAsset_HEAD.TAX3,TSPL_VSPAsset_HEAD.TAX3_Rate,TSPL_VSPAsset_HEAD.TAX3_Amt,TSPL_VSPAsset_HEAD.TAX3_Base_Amt,TSPL_VSPAsset_HEAD.TAX4,TSPL_VSPAsset_HEAD.TAX4_Rate,TSPL_VSPAsset_HEAD.TAX4_Amt,TSPL_VSPAsset_HEAD.TAX4_Base_Amt,TSPL_VSPAsset_HEAD.TAX5,TSPL_VSPAsset_HEAD.TAX5_Rate,TSPL_VSPAsset_HEAD.TAX5_Amt,TSPL_VSPAsset_HEAD.TAX5_Base_Amt,TSPL_VSPAsset_HEAD.TAX6,TSPL_VSPAsset_HEAD.TAX6_Rate,TSPL_VSPAsset_HEAD.TAX6_Amt,TSPL_VSPAsset_HEAD.TAX6_Base_Amt,TSPL_VSPAsset_HEAD.TAX7,TSPL_VSPAsset_HEAD.TAX7_Rate,TSPL_VSPAsset_HEAD.TAX7_Amt,TSPL_VSPAsset_HEAD.TAX7_Base_Amt,TSPL_VSPAsset_HEAD.TAX8,TSPL_VSPAsset_HEAD.TAX8_Rate,TSPL_VSPAsset_HEAD.TAX8_Amt,TSPL_VSPAsset_HEAD.TAX8_Base_Amt,TSPL_VSPAsset_HEAD.TAX9,TSPL_VSPAsset_HEAD.TAX9_Rate,TSPL_VSPAsset_HEAD.TAX9_Amt,TSPL_VSPAsset_HEAD.TAX9_Base_Amt,TSPL_VSPAsset_HEAD.TAX10,TSPL_VSPAsset_HEAD.TAX10_Rate,TSPL_VSPAsset_HEAD.TAX10_Amt,TSPL_VSPAsset_HEAD.TAX10_Base_Amt,TSPL_VSPAsset_HEAD.BeforeTax_Amt,TSPL_VSPAsset_HEAD.Total_Tax_Amt,TSPL_VSPAsset_HEAD.doc_Amt, TSPL_VSPAsset_HEAD.vehicle_Id, TSPL_VSPAsset_HEAD.Machine_Id,Req_IssueNo,RequisitionNo  FROM TSPL_VSPAsset_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on FLocation.Location_Code=TSPL_VSPAsset_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on TLocation.Location_Code=TSPL_VSPAsset_HEAD.To_Location left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on IssueEmp.EMP_CODE= TSPL_VSPAsset_HEAD.issue_To left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_VSPAsset_HEAD.Request_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VSPAsset_HEAD.Doc_No = (select MIN(Doc_No) from TSPL_VSPAsset_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_VSPAsset_HEAD.Doc_No = (select Max(Doc_No) from TSPL_VSPAsset_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_VSPAsset_HEAD.Doc_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_VSPAsset_HEAD.Doc_No = (select Min(Doc_No) from TSPL_VSPAsset_HEAD where Doc_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_VSPAsset_HEAD.Doc_No = (select Max(Doc_No) from TSPL_VSPAsset_HEAD where Doc_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMCCIssueReturnHead()
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
            obj.IS_LOST = IIf(clsCommon.myCdbl(dt.Rows(0)("IS_LOST")) = 1, True, False)
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






            qry = "SELECT TSPL_VSPAsset_DETAIL.Doc_No,TSPL_VSPAsset_DETAIL.Line_No,TSPL_VSPAsset_DETAIL.Item_Code,TSPL_VSPAsset_DETAIL.Item_Desc,TSPL_VSPAsset_DETAIL.Required_Qty,TSPL_VSPAsset_DETAIL.Unit_code,TSPL_VSPAsset_DETAIL.Issued_Qty , TSPL_VSPAsset_DETAIL.TAX1,TSPL_VSPAsset_DETAIL.TAX1_Rate,TSPL_VSPAsset_DETAIL.TAX1_Amt,TSPL_VSPAsset_DETAIL.TAX2,TSPL_VSPAsset_DETAIL.TAX2_Rate,TSPL_VSPAsset_DETAIL.TAX2_Amt,TSPL_VSPAsset_DETAIL.TAX3,TSPL_VSPAsset_DETAIL.TAX3_Rate,TSPL_VSPAsset_DETAIL.TAX3_Amt,TSPL_VSPAsset_DETAIL.TAX4,TSPL_VSPAsset_DETAIL.TAX4_Rate,TSPL_VSPAsset_DETAIL.TAX4_Amt,TSPL_VSPAsset_DETAIL.TAX5,TSPL_VSPAsset_DETAIL.TAX5_Rate,TSPL_VSPAsset_DETAIL.TAX5_Amt,TSPL_VSPAsset_DETAIL.TAX6,TSPL_VSPAsset_DETAIL.TAX6_Rate,TSPL_VSPAsset_DETAIL.TAX6_Amt,TSPL_VSPAsset_DETAIL.TAX7,TSPL_VSPAsset_DETAIL.TAX7_Rate,TSPL_VSPAsset_DETAIL.TAX7_Amt,TSPL_VSPAsset_DETAIL.TAX8,TSPL_VSPAsset_DETAIL.TAX8_Rate,TSPL_VSPAsset_DETAIL.TAX8_Amt,TSPL_VSPAsset_DETAIL.TAX9,TSPL_VSPAsset_DETAIL.TAX9_Rate,TSPL_VSPAsset_DETAIL.TAX9_Amt,TSPL_VSPAsset_DETAIL.TAX10,TSPL_VSPAsset_DETAIL.TAX10_Rate,TSPL_VSPAsset_DETAIL.TAX10_Amt,TSPL_VSPAsset_DETAIL.TAX1_Base_Amt,TSPL_VSPAsset_DETAIL.TAX2_Base_Amt,TSPL_VSPAsset_DETAIL.TAX3_Base_Amt,TSPL_VSPAsset_DETAIL.TAX4_Base_Amt,TSPL_VSPAsset_DETAIL.TAX5_Base_Amt,TSPL_VSPAsset_DETAIL.TAX6_Base_Amt,TSPL_VSPAsset_DETAIL.TAX7_Base_Amt,TSPL_VSPAsset_DETAIL.TAX8_Base_Amt,TSPL_VSPAsset_DETAIL.TAX9_Base_Amt,TSPL_VSPAsset_DETAIL.TAX10_Base_Amt ,TSPL_VSPAsset_DETAIL.Amount,TSPL_VSPAsset_DETAIL.Total_Tax_Amt,TSPL_VSPAsset_DETAIL.Item_Net_Amt,TSPL_VSPAsset_DETAIL.Unit_Cost,Issued_Qty_AgainstRet, TSPL_VSPAsset_DETAIL.Req_IssueNo, TSPL_VSPAsset_DETAIL.Cost_Code,EMI_Asset_Value,EMI_No_Of_Payment_Cycle FROM TSPL_VSPAsset_DETAIL where TSPL_VSPAsset_DETAIL.Doc_No='" + obj.Doc_No + "' ORDER BY TSPL_VSPAsset_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMCCIssueReturnDetail)
                Dim objTr As clsMCCIssueReturnDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMCCIssueReturnDetail
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

            Dim obj As clsMCCIssueReturnHead = clsMCCIssueReturnHead.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPAssetIssue, obj.From_Location, obj.Doc_Date, trans)


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
            '--------------For From Location (Only Out Entry For DocType Issue)-----------------------------------------------------------------------------
            If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Issue") = CompairStringResult.Equal Then
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                For Each objTr As clsMCCIssueReturnDetail In obj.Arr
                    Dim objLocationDetails As New clsItemLocationDetails()
                    objLocationDetails.Item_Code = objTr.Item_Code
                    objLocationDetails.Item_Desc = objTr.Item_Desc
                    objLocationDetails.Location_Code = obj.From_Location
                    objLocationDetails.Location_Desc = obj.From_LocationName
                    objLocationDetails.Item_Qty = IIf(obj.Doc_Type = "Return", -1 * objTr.Issued_Qty_AgainstRet, -1 * objTr.Issued_Qty) '-1 * (objTr.Issued_Qty)
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
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = IIf(obj.Doc_Type = "Return", objTr.Issued_Qty_AgainstRet, objTr.Issued_Qty) 'objTr.Issued_Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code



                    If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.createDebitNoteOnAssetIssue, clsFixedParameterCode.createDebitNoteOnAssetIssue, trans)) = 1, True, False)) = True Then
                        If obj.IS_LOST = False AndAlso obj.On_Hold = False Then
                            If objTr.EMI_Asset_Value > 0 Then
                                objInventoryMovemnt.Basic_Cost = objTr.EMI_Asset_Value
                            Else
                                objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                            End If
                        Else
                            objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                        End If
                    Else
                        objInventoryMovemnt.Basic_Cost = objTr.Unit_Cost
                    End If



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
                'isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(PostDate, ArrLocationDetails, trans)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-AISSUE", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement, trans)
                If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CtreateJEOfVspAssetIssueAndReturn, clsFixedParameterCode.CtreateJEOfVspAssetIssueAndReturn, trans)) = 1, True, False)) = True Then
                    CreateJournalEntry(obj, trans, "")
                End If

                If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.createDebitNoteOnAssetIssue, clsFixedParameterCode.createDebitNoteOnAssetIssue, trans)) = 1, True, False)) = True Then
                    If obj.IS_LOST = False AndAlso obj.On_Hold = False Then
                        CreateAPDebitNoteEntry(obj, "D", trans)
                    End If
                End If
            End If

            'For To Location (Only In Entry For DocType Return)-----------------------------------------------------------------------------------------------------------------------
            If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Return") = CompairStringResult.Equal AndAlso obj.IS_LOST = False Then
                Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                For Each objTr As clsMCCIssueReturnDetail In obj.Arr
                    If objTr.EMI_Asset_Value > 0 Then
                        Dim dtBal As DataTable = clsDBFuncationality.GetDataTable(clsAPInvoiceAssetEMIDetails.GetVSPAssetEMIQuery(obj.Issue_No, objTr.Item_Code), trans)
                        Dim dblBalanceEMIAmt As Double = 0
                        If dtBal IsNot Nothing AndAlso dtBal.Rows.Count > 0 Then
                            dblBalanceEMIAmt = clsCommon.myCdbl(dtBal.Rows(0)("Installment_Amt"))
                        End If
                        If objTr.EMI_Asset_Value > dblBalanceEMIAmt Then
                            Throw New Exception("Installment Balance Amount is " + clsCommon.myFormat(dblBalanceEMIAmt) + "You cannot enter more than Balance Amount in Asset Value")
                        End If
                    End If

                    Dim objLocationDetails1 As New clsItemLocationDetails()
                    objLocationDetails1.Item_Code = objTr.Item_Code
                    objLocationDetails1.Item_Desc = objTr.Item_Desc
                    objLocationDetails1.Location_Code = obj.To_Location
                    objLocationDetails1.Location_Desc = obj.To_LocationName
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

                isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(PostDate, ArrLocationDetails1, trans)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MCC-ARETURN", obj.Doc_No, obj.Doc_Date, PostDate, ArrInventoryMovement1, trans)
                If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CtreateJEOfVspAssetIssueAndReturn, clsFixedParameterCode.CtreateJEOfVspAssetIssueAndReturn, trans)) = 1, True, False)) = True Then
                    CreateJournalEntry(obj, trans, "")
                End If

            End If
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
                Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
               " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
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
                clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location, False, trans, obj.Doc_Date, obj.Comment, "VSP-TF", "VSP Transfer", obj.Doc_No, "", "V", obj.Issue_To, clsVendorMaster.GetName(obj.Issue_To, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , obj.Remarks, obj.Comment)
            End If
            Dim qry As String = "Update TSPL_VSPAsset_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
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
    Public Shared Function CreateAPDebitNoteEntry(ByVal obj As clsMCCIssueReturnHead, ByVal DebitCredit As String, ByVal trans As SqlTransaction)
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        'Dim obj As clsMCCIssueReturnHead = clsMCCIssueReturnHead.GetData(strDocNo, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.Issue_To & "'", trans)
        'objVendorInvHead.Document_No = obj.Doc_No 'clsDBFuncationality.getSingleValue("select document_no from tspl_vendor_invoice_head where against_Poinvoice_no='" & obj.PI_No & "' and document_type='D'", trans) 'ToBeGenerated
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
            objVendorInvHead.Document_Type = "D"
            objVendorInvHead.RefDocType = "V_A_Issue"
            objVendorInvHead.Description = "Against VSP Asset Issue No " + obj.Doc_No
        Else
            Throw New Exception("Document Type must be D")
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
        'objVendorInvHead.Amount_Less_Discount = obj.doc_Amt
        'objVendorInvHead.Document_Total = obj.doc_Amt
        'objVendorInvHead.Balance_Amt = obj.doc_Amt

        objVendorInvHead.Against_VSP_Asset_Issue = obj.Doc_No
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


        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

        objVendorInvHead.Empty_Amount = 0
        Dim dbldocumentAmt As Double = 0
        Dim dbldetailamt As Double = 0
        Dim ii As Integer = 0
        For Each objPIDetail As clsMCCIssueReturnDetail In obj.Arr
            ''Fill VendorInvoice details Data
            ''qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"
            qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ")")
            End If
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))

            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.From_Location, trans)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))



            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.From_Location, trans)
            End If


            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            ii = ii + 1
            If objPIDetail.EMI_Asset_Value > 0 Then
                dbldetailamt = objPIDetail.EMI_Asset_Value
            Else
                dbldetailamt = objPIDetail.Amount
            End If
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
            objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
            objVendorInvDetail.Amount = dbldetailamt
            objVendorInvDetail.Discount_Per = 0 'objPIDetail.Disc_Per
            objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
            objVendorInvDetail.Amount_less_Discount = dbldetailamt 'objPIDetail.Amt_Less_Discount
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
            objVendorInvDetail.Total_Amount = dbldetailamt + objPIDetail.Total_Tax_Amt
            objVendorInvDetail.Landed_Amount = dbldetailamt 'objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
            objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
            dbldocumentAmt = dbldocumentAmt + dbldetailamt
            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If
            ''End of Fill Vendor Invoice Detail Data
        Next

        objVendorInvHead.Amount_Less_Discount = dbldocumentAmt
        objVendorInvHead.Document_Total = dbldocumentAmt
        objVendorInvHead.Balance_Amt = dbldocumentAmt


        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        If objVendorInvHead.Empty_Amount > 0 Then
            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        End If


        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            Throw New Exception("No GL Account Found For AP Invoice")
        End If
        isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

        Return isSaved
    End Function
    Public Shared Function CreateJournalEntry(ByVal obj As clsMCCIssueReturnHead, ByVal trans As SqlTransaction, ByVal strVoucherNo As String) As Boolean
        ''richa agarwal 11/11/2014 clsIssueReturnHead
        ''If obj.Doc_Type = "Transfer" Then
        ''-------------------------------

        Dim Sql As String = ""
        If clsCommon.myLen(strVoucherNo) > 0 Then
            'DeleteVoucher(obj.Doc_No, trans)
        End If

        'If clsCommon.myLen(obj.Issue_To_Franchise) <= 0 Then
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

        'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
        For Each objTr As clsMCCIssueReturnDetail In obj.Arr 'clsIssueReturnDetail
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
                    If obj.Total_Tax_Amt > 0 Then ' AndAlso clsCommon.myLen(obj.PurchaseInvoice_No) > 0
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
                    For Each objTr As clsMCCIssueReturnDetail In obj.Arr ' clsIssueReturnDetail
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
                            'Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                            Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost}
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
                            'Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                            Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost}
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
                    For Each objTr As clsMCCIssueReturnDetail In obj.Arr
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
                        'Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost * -1, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                        Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty_AgainstRet * objTr.Unit_Cost * -1}
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
                            'Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost * -1, "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                            Dim FrmFilledAcc() As String = {strFrmFilledAcc, objTr.Issued_Qty * objTr.Unit_Cost * -1}
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
                    For Each objTr As clsMCCIssueReturnDetail In obj.Arr
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
                    'Dim Acc1() As String = {strINVCtrlAccount, -1 * (objTr.Issued_Qty * objTr.Unit_Cost), "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code, "", "", "I"}
                    Dim Acc1() As String = {strINVCtrlAccount, -1 * (objTr.Issued_Qty * objTr.Unit_Cost), "", "", "", "", "", "", "I"}
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

                    'Dim Acc2() As String = {strFAClearingAccount, (objTr.Issued_Qty * objTr.Unit_Cost), "", "", objTr.Hirerachy_Code, objTr.Cost_Centre_Code}
                    Dim Acc2() As String = {strFAClearingAccount, (objTr.Issued_Qty * objTr.Unit_Cost)}
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
            'End If
            'End If
            Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsMCCIssueReturnHead = clsMCCIssueReturnHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPAssetIssue, obj.From_Location, obj.Doc_Date, trans)

                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.DeleteData("MCC-AISSUE", strCode, trans)  ''By Preeti gupta on 05/09/2016
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    clsSerializeInvenotry.DeleteData("MCC-ARETURN", strCode, trans)  ''By Preeti gupta on 05/09/2016
                End If
                Dim qry As String = "delete from TSPL_VSPAsset_DETAIL where Doc_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_VSPAsset_HEAD where Doc_No='" + strCode + "'"
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
            Dim obj As clsMCCIssueReturnHead = clsMCCIssueReturnHead.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Reverse")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVSPAssetIssue, obj.From_Location, obj.Doc_Date, trans)

            If Not obj.Status = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            qry = "select TSPL_VENDOR_INVOICE_HEAD.Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocType='ASL-DED' and TSPL_VENDOR_INVOICE_HEAD.Order_No='" + obj.Doc_No + "'"
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(qry) > 0 Then
                Throw New Exception("This Document No is used in AP Debit Note no [" + qry + "]")
            End If

            ''richa agarwal 01 Oct,2021

            Dim APInvoiceNo As String = clsDBFuncationality.getSingleValue("select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_VSP_Asset_Issue ,'')='" + strCode + "'", trans)
            If clsCommon.myLen(APInvoiceNo) > 0 Then

                qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No='" + APInvoiceNo + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = ""
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Reverse_Code,'')  from TSPL_BANK_REVERSE WHERE DOCUMENT_NO = '" & clsCommon.myCstr(dr("Payment_No")) & "' AND isnull(TSPL_BANK_REVERSE.POST,'')='P'", trans))) <= 0 Then
                            qry += Environment.NewLine + clsCommon.myCstr(dr("Payment_No"))
                        End If
                    Next
                    If clsCommon.myLen(qry) > 0 Then
                        Throw New Exception("Current AP-Invoice is used in following Payment -" & qry)
                    End If

                End If


                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No ='" + APInvoiceNo + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No ='" + APInvoiceNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                qry = "delete from TSPL_VENDOR_INVOICE_detail  where document_no='" + APInvoiceNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_VENDOR_INVOICE_HEAD where document_no='" + APInvoiceNo + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If





            qry = "update tspl_serial_item set against_inv_movement_trans_id=NULL where document_type in ('MCC-AISSUE','MCC-ARETURN') and document_code='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_INVENTORY_MOVEMENT WHERE Trans_Type in ('MCC-AISSUE','MCC-ARETURN') AND Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_INVENTORY_MOVEMENT_NEW WHERE Trans_Type in ('MCC-AISSUE','MCC-ARETURN') AND Source_Doc_No='" + obj.Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim CurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            For Each objTr As clsMCCIssueReturnDetail In obj.Arr
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
            qry = "Update TSPL_VSPAsset_HEAD set Status=0, Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMCCIssueReturnDetail
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
    Public EMI_Asset_Value As Double = 0
    Public EMI_No_Of_Payment_Cycle As Double = 0

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing



#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strDocType As String, ByVal strFromLocation As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsMCCIssueReturnDetail), ByVal trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMCCIssueReturnDetail In Arr
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

                clsCommon.AddColumnsForChange(coll, "EMI_Asset_Value", obj.EMI_Asset_Value)
                clsCommon.AddColumnsForChange(coll, "EMI_No_Of_Payment_Cycle", obj.EMI_No_Of_Payment_Cycle)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSPAsset_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                 

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

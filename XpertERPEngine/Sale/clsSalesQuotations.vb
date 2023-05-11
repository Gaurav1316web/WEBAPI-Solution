Imports common
Imports System.Data.SqlClient
Public Class clsSalesQuotationsHead

#Region "Variables"
    Public Price_Code As String = Nothing
    Public Price_Group_Code As String = Nothing

    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Cust_OrderNo As String = Nothing
    Public Expire_Date As String = Nothing
    Public Require_Date As String = Nothing
    Public Status As ERPTransactionStatus = 0
    Public On_Hold As Integer = 0
    Public Manual_Complete As String = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Location As String = Nothing
    Public LocationName As String = Nothing 'Not a table field
    Public Detail_Total_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public Mode_Of_Transport As String = Nothing
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Posting_Date As String = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Request_By As String = Nothing
    Public close_yn As String
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = ""
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing

    Public Level1_Approval_Status As Integer = 0
    Public Level2_Approval_Status As Integer = 0
    Public Level3_Approval_Status As Integer = 0
    Public Approval_Level_Required As Integer = 0
    Public ArrTr As List(Of clsSalesQuotationsDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public PROJECT_ID As String = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsSalesQuotationsHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim UserLevel As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ApprovalLevel from TSPL_USER_MASTER WHERE User_Code='" + objCommonVar.CurrentUserCode + "'", trans))
            '-----------------------------------------------
            If Not UserLevel = 0 Then
                If Not isNewEntry Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Level1_Approval_Status, Level2_Approval_Status, Level3_Approval_Status from TSPL_SD_QUOTATION_HEAD WHERE Document_Code ='" + obj.Document_Code + "'", trans)
                    If dt.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dt.Rows(0)("Level1_Approval_Status")) = 1 And UserLevel = 1 Then
                            Throw New Exception("This Document is already posted by Level 1 User")
                        ElseIf clsCommon.myCdbl(dt.Rows(0)("Level2_Approval_Status")) = 1 And UserLevel = 2 Then
                            Throw New Exception("This Document is already posted by Level 2 User")
                        ElseIf clsCommon.myCdbl(dt.Rows(0)("Level3_Approval_Status")) = 1 And UserLevel = 3 Then
                            Throw New Exception("This Document is already posted by Level 3 User")
                        End If
                        obj.Level1_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level1_Approval_Status"))
                        obj.Level2_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level2_Approval_Status"))
                        obj.Level3_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level3_Approval_Status"))
                    End If
                Else
                    If UserLevel <> 1 Then
                        Throw New Exception("You are not a vaild user for create this document.")
                    End If
                End If
            End If
            '-----------------------------------------------
            Dim qry As String = "delete from TSPL_SD_QUOTATION_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If (isNewEntry) Then
                If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSalesQuotation, clsDocTransactionType.SNQuotationFinishedGoods, obj.Location)
                Else
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSalesQuotation, clsDocTransactionType.SNQuotationOther, obj.Location)
                End If

                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Cust_OrderNo", obj.Cust_OrderNo)

            If clsCommon.myLen(obj.Expire_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Expire_Date", clsCommon.GetPrintDate(obj.Expire_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Expire_Date", Nothing, True)
            End If
            If clsCommon.myLen(obj.Require_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Require_Date", clsCommon.GetPrintDate(obj.Require_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Require_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            clsCommon.AddColumnsForChange(coll, "close_yn", obj.close_yn)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "On_Hold", obj.On_Hold)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Detail_Total_Amt", obj.Detail_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)

            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept, True)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            Dim s As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_QUOTATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_QUOTATION_HEAD", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSalesQuotationsDetail.SaveData(obj.Document_Code, obj.Customer_Code, obj.ArrTr, trans)

            '------------------Approval_Level_Required-----------------------------
            qry = "Update TSPL_SD_QUOTATION_HEAD SET Approval_Level_Required=(Select MAX(Approval_Level_Required) from TSPL_SD_QUOTATION_DETAIL Where Document_Code=TSPL_SD_QUOTATION_HEAD.Document_Code) WHere TSPL_SD_QUOTATION_HEAD.Document_Code='" + obj.Document_Code + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '------------------------------------------------------------------------

            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)

            'Dim Subject As String = "Approval Required for the Sale Quotation:" + obj.Document_Code + " dated :" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + ""
            'Dim Msg As String = "Please Click the below link for the Approval of Sale Quotation:" + obj.Document_Code + " dated :" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + ""

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_QUOTATION_HEAD", trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsSalesQuotationsHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsSalesQuotationsHead
        Dim UserLevel As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ApprovalLevel from TSPL_USER_MASTER WHERE User_Code='" + objCommonVar.CurrentUserCode + "'", trans))
        Dim whrclas As String = " "
        Dim obj As clsSalesQuotationsHead = Nothing
        Dim qry As String
        qry = "SELECT TSPL_SD_QUOTATION_HEAD.close_yn,TSPL_SD_QUOTATION_HEAD.Document_Code,TSPL_SD_QUOTATION_HEAD.Document_Date,TSPL_SD_QUOTATION_HEAD.Cust_OrderNo," & _
        " TSPL_SD_QUOTATION_HEAD.Expire_Date,TSPL_SD_QUOTATION_HEAD.Require_Date,TSPL_SD_QUOTATION_HEAD.Status,TSPL_SD_QUOTATION_HEAD.On_Hold, " & _
        " TSPL_SD_QUOTATION_HEAD.Manual_Complete,TSPL_SD_QUOTATION_HEAD.Ref_No,TSPL_SD_QUOTATION_HEAD.Description,TSPL_SD_QUOTATION_HEAD.Remarks, " & _
        " TSPL_SD_QUOTATION_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_SD_QUOTATION_HEAD.Detail_Total_Amt, " & _
        " TSPL_SD_QUOTATION_HEAD.Total_Amt,TSPL_SD_QUOTATION_HEAD.Mode_Of_Transport,TSPL_SD_QUOTATION_HEAD.Comments,TSPL_SD_QUOTATION_HEAD.Created_By, " & _
        " TSPL_SD_QUOTATION_HEAD.Created_Date,TSPL_SD_QUOTATION_HEAD.Modify_By,TSPL_SD_QUOTATION_HEAD.Modify_Date,TSPL_SD_QUOTATION_HEAD.Comp_Code, " & _
        " TSPL_SD_QUOTATION_HEAD.Posting_Date,TSPL_SD_QUOTATION_HEAD.Dept,TSPL_SD_QUOTATION_HEAD.Dept_Desc,TSPL_SD_QUOTATION_HEAD.Item_Type, " & _
        " TSPL_SD_QUOTATION_HEAD.Request_By,TSPL_SD_QUOTATION_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_QUOTATION_HEAD.Salesman_Code, " & _
        " TSPL_SD_QUOTATION_HEAD.Salesman_Name, Level1_Approval_Status, Level2_Approval_Status, Level3_Approval_Status, Approval_Level_Required, " & _
        " TSPL_SD_QUOTATION_HEAD.CURRENCY_CODE,TSPL_SD_QUOTATION_HEAD.CONVRATE,TSPL_SD_QUOTATION_HEAD.APPLICABLEFROM,TSPL_SD_QUOTATION_HEAD.PROJECT_ID " & _
        " FROM TSPL_SD_QUOTATION_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_QUOTATION_HEAD.Location " & _
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_QUOTATION_HEAD.Customer_Code where  2=2"

        If UserLevel = 2 Then
            whrclas = " and Level1_Approval_Status=1"
        ElseIf UserLevel = 3 Then
            whrclas = " AND Level2_Approval_Status=1"
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_QUOTATION_HEAD.Document_Code=(select MIN(Document_Code) from TSPL_SD_QUOTATION_HEAD Where 1=1 " + whrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_QUOTATION_HEAD.Document_Code=(select Max(Document_Code) from TSPL_SD_QUOTATION_HEAD Where 1=1 " + whrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_SD_QUOTATION_HEAD.Document_Code=(select Min(Document_Code) from TSPL_SD_QUOTATION_HEAD where Document_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_QUOTATION_HEAD.Document_Code=(select Max(Document_Code) from TSPL_SD_QUOTATION_HEAD where Document_Code < '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_QUOTATION_HEAD.Document_Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSalesQuotationsHead()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Cust_OrderNo = clsCommon.myCstr(dt.Rows(0)("Cust_OrderNo"))

            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))

            If dt.Rows(0)("Expire_Date") Is DBNull.Value Then
                obj.Expire_Date = Nothing
            Else
                obj.Expire_Date = clsCommon.myCDate(dt.Rows(0)("Expire_Date"))
            End If
            If dt.Rows(0)("Require_Date") Is DBNull.Value Then
                obj.Expire_Date = Nothing
            Else
                obj.Require_Date = clsCommon.myCDate(dt.Rows(0)("Require_Date"))
            End If
            If (clsCommon.myCdbl(dt.Rows(0)("Status")) = 0) Then
                obj.Status = ERPTransactionStatus.Pending
            Else
                obj.Status = ERPTransactionStatus.Approved
            End If
            obj.On_Hold = clsCommon.myCdbl(dt.Rows(0)("On_Hold"))
            obj.Manual_Complete = clsCommon.myCstr(dt.Rows(0)("Manual_Complete"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("LocationName"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Detail_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Detail_Total_Amt"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Cust_OrderNo = clsCommon.myCstr(dt.Rows(0)("Cust_OrderNo"))
            obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            obj.close_yn = clsCommon.myCstr(dt.Rows(0)("close_yn"))
            obj.Level1_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level1_Approval_Status"))
            obj.Level2_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level2_Approval_Status"))
            obj.Level3_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level3_Approval_Status"))
            obj.Approval_Level_Required = clsCommon.myCdbl(dt.Rows(0)("Approval_Level_Required"))

            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Request_By = clsCommon.myCstr(dt.Rows(0)("Request_By"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 

            qry = "SELECT TSPL_SD_QUOTATION_DETAIL.Document_Code,TSPL_SD_QUOTATION_DETAIL.Line_No , TSPL_SD_QUOTATION_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_QUOTATION_DETAIL.Qty,TSPL_SD_QUOTATION_DETAIL.Balance_Qty,TSPL_SD_QUOTATION_DETAIL.Unit_Code,TSPL_SD_QUOTATION_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc as LocationName ,TSPL_SD_QUOTATION_DETAIL.Item_Cost,TSPL_SD_QUOTATION_DETAIL.Item_Net_Amt,TSPL_SD_QUOTATION_DETAIL.Status,TSPL_SD_QUOTATION_DETAIL.Order_No,TSPL_SD_QUOTATION_DETAIL.Vendor_ItemNo,TSPL_SD_QUOTATION_DETAIL.Specification,TSPL_SD_QUOTATION_DETAIL.Remarks, Discount_Per, Discount_Amt, Amount_After_Discount FROM TSPL_SD_QUOTATION_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SD_QUOTATION_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_QUOTATION_DETAIL.Location where TSPL_SD_QUOTATION_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_QUOTATION_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrTr = New List(Of clsSalesQuotationsDetail)
                Dim objTr As clsSalesQuotationsDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSalesQuotationsDetail()
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))

                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.LocationName = clsCommon.myCstr(dr("LocationName"))
                    objTr.Item_Cost = clsCommon.myCstr(dr("Item_Cost"))
                    objTr.Status = clsCommon.myCstr(dr("Status"))
                    objTr.Order_No = clsCommon.myCstr(dr("Order_No"))
                    objTr.Vendor_ItemNo = clsCommon.myCstr(dr("Vendor_ItemNo"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))

                    objTr.Discount_Per = clsCommon.myCdbl(dr("Discount_Per"))
                    objTr.Discount_Amt = clsCommon.myCdbl(dr("Discount_Amt"))
                    objTr.Amount_After_Discount = clsCommon.myCdbl(dr("Amount_After_Discount"))

                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))

                    obj.ArrTr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal strItemCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            'Dim UserLevel As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ApprovalLevel from TSPL_USER_MASTER WHere User_Code='" + objCommonVar.CurrentUserCode + "'", trans))
            'If Not (UserLevel >= 1 And UserLevel <= 3) Then ''by pankaj 7-Jene-2013
            '    Throw New Exception("Invalid  user's level i.e. " + clsCommon.myCstr(UserLevel) + "")
            'End If
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsSalesQuotationsHead = clsSalesQuotationsHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold = 1) Then
                Throw New Exception("Document No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If

            ''------------------------------------------------------------
            'If UserLevel = 1 Then
            '    If obj.Level1_Approval_Status = 1 Then
            '        Throw New Exception("Level-1 user already posted this document")
            '    End If
            'ElseIf UserLevel = 2 Then
            '    If obj.Level1_Approval_Status = 0 Then
            '        Throw New Exception("Requires Level-1 approval first.")
            '    ElseIf obj.Level2_Approval_Status = 1 Then
            '        Throw New Exception("Level-2 user already posted this document")
            '    End If
            'ElseIf UserLevel = 3 Then
            '    If obj.Level2_Approval_Status = 0 Then
            '        Throw New Exception("Requires Level-2 approval first.")
            '    ElseIf obj.Level3_Approval_Status = 1 Then
            '        Throw New Exception("Level-3 user already posted this document")
            '    End If
            'End If


            'Dim Count As Integer = 0
            'Dim PostCount As Integer = 0
            'Dim dt As DataTable

            'If clsCommon.myLen(strItemCode) <= 0 Then
            '    Dim ItemQry As String = "Select Item_Code from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' And Approval_Level_Required >= " + clsCommon.myCstr(UserLevel) + ""
            '    dt = clsDBFuncationality.GetDataTable(ItemQry, trans)
            '    For Each dr As DataRow In dt.Rows
            '        Dim ItemCode As String = clsCommon.myCstr(dr("Item_Code"))

            '        qry = "Select Approval_Level_Required, Level1_Approval_Status, Level2_Approval_Status, Level3_Approval_Status, Is_Approved from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + obj.Document_Code + "' AND Item_Code='" + ItemCode + "'"
            '        dt = clsDBFuncationality.GetDataTable(qry, trans)

            '        If dt.Rows.Count > 0 Then
            '            qry = ""
            '            If UserLevel = 1 Then
            '                qry = "Update TSPL_SD_QUOTATION_DETAIL Set Level1_Approval_Status=1, Level1_Approval_On='" + strPostDate + "', Level1_Approval_By='" + objCommonVar.CurrentUserCode + "' "
            '            ElseIf UserLevel = 2 Then
            '                qry = "Update TSPL_SD_QUOTATION_DETAIL Set Level2_Approval_Status=1, Level2_Approval_On='" + strPostDate + "', Level2_Approval_By='" + objCommonVar.CurrentUserCode + "' "
            '            ElseIf UserLevel = 3 Then
            '                qry = "Update TSPL_SD_QUOTATION_DETAIL Set Level3_Approval_Status=1, Level3_Approval_On='" + strPostDate + "', Level3_Approval_By='" + objCommonVar.CurrentUserCode + "' "
            '            End If

            '            If UserLevel = 3 Then
            '                Dim DiscPercent As Double = clsSalesQuotationsDetail.GetMaximumDiscount(UserLevel, obj.Customer_Code, ItemCode, trans)
            '                Dim RequiredDiscQry As String = "Select Discount_Per from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' And IteM_Code='" + ItemCode + "' "
            '                Dim RequiredDiscPercent As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(RequiredDiscQry, trans))
            '                If RequiredDiscPercent > DiscPercent Then
            '                    If common.clsCommon.MyMessageBoxShow("Required discount against item '" + ItemCode + "' is " + clsCommon.myCstr(RequiredDiscPercent) + ". do you want to continue?", "Alert", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            '                        trans.Rollback()
            '                        Return False
            '                    End If
            '                End If
            '            End If

            '            If UserLevel = clsCommon.myCdbl(dt.Rows(0)("Approval_Level_Required")) Then
            '                qry += " , Is_Approved=1"
            '            End If
            '            qry += " Where Document_Code='" + obj.Document_Code + "' And Item_Code='" + ItemCode + "'"
            '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '            Count += 1
            '        End If
            '    Next
            'Else
            '    qry = "Select Approval_Level_Required, Level1_Approval_Status, Level2_Approval_Status, Level3_Approval_Status, Is_Approved from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + obj.Document_Code + "' AND Item_Code='" + strItemCode + "'"
            '    dt = clsDBFuncationality.GetDataTable(qry, trans)

            '    If dt.Rows.Count > 0 Then
            '        If UserLevel = 1 Then
            '            qry = "Update TSPL_SD_QUOTATION_DETAIL Set Level1_Approval_Status=1, Level1_Approval_On='" + strPostDate + "', Level1_Approval_By='" + objCommonVar.CurrentUserCode + "' "
            '        ElseIf UserLevel = 2 Then
            '            qry = "Update TSPL_SD_QUOTATION_DETAIL Set Level2_Approval_Status=1, Level2_Approval_On='" + strPostDate + "', Level2_Approval_By='" + objCommonVar.CurrentUserCode + "' "
            '        ElseIf UserLevel = 3 Then
            '            qry = "Update TSPL_SD_QUOTATION_DETAIL Set Level3_Approval_Status=1, Level3_Approval_On='" + strPostDate + "', Level3_Approval_By='" + objCommonVar.CurrentUserCode + "' "
            '        End If

            '        If UserLevel = 3 Then
            '            Dim DiscPercent As Double = clsSalesQuotationsDetail.GetMaximumDiscount(UserLevel, obj.Customer_Code, strItemCode, trans)
            '            Dim RequiredDiscQry As String = "Select Discount_Per from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' And IteM_Code='" + strItemCode + "' "
            '            Dim RequiredDiscPercent As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(RequiredDiscQry, trans))
            '            If RequiredDiscPercent > DiscPercent Then
            '                If common.clsCommon.MyMessageBoxShow("Required discount against item '" + strItemCode + "' is " + clsCommon.myCstr(RequiredDiscPercent) + ". do you want to continue?", "Alert", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            '                    trans.Rollback()
            '                    Return False
            '                End If
            '            End If
            '        End If

            '        If UserLevel = clsCommon.myCdbl(dt.Rows(0)("Approval_Level_Required")) Then
            '            qry += " , Is_Approved=1"
            '        End If
            '        qry += " Where Document_Code='" + obj.Document_Code + "' And Item_Code='" + strItemCode + "'"
            '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    End If
            '    If UserLevel = 1 Then
            '        qry = "Select Count(*) from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' AND Approval_Level_Required >= " + clsCommon.myCstr(UserLevel) + " And Level1_Approval_Status = 1 "
            '    ElseIf UserLevel = 2 Then
            '        qry = "Select Count(*) from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' AND Approval_Level_Required >= " + clsCommon.myCstr(UserLevel) + " And Level2_Approval_Status = 1 "
            '    ElseIf UserLevel = 3 Then
            '        qry = "Select Count(*) from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' AND Approval_Level_Required >= " + clsCommon.myCstr(UserLevel) + " And Level3_Approval_Status = 1 "
            '    End If

            '    Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            'End If

            'qry = "Select Count(*) from TSPL_SD_QUOTATION_DETAIL WHERE Document_Code='" + strDocNo + "' AND Approval_Level_Required>= " + clsCommon.myCstr(UserLevel) + " "
            'PostCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

            'If PostCount = Count Then
            '    If UserLevel = 1 Then
            '        qry = "Update TSPL_SD_QUOTATION_HEAD set Level1_Approval_Status=1, Level1_Approval_On='" + strPostDate + "', Level1_Approval_By='" + objCommonVar.CurrentUserCode + "' WHERE Document_Code='" + obj.Document_Code + "'"
            '    ElseIf UserLevel = 2 Then
            '        qry = "Update TSPL_SD_QUOTATION_HEAD set Level2_Approval_Status=1, Level2_Approval_On='" + strPostDate + "', Level2_Approval_By='" + objCommonVar.CurrentUserCode + "' WHERE Document_Code='" + obj.Document_Code + "'"
            '    ElseIf UserLevel = 3 Then
            '        qry = "Update TSPL_SD_QUOTATION_HEAD set Level3_Approval_Status=1, Level3_Approval_On='" + strPostDate + "', Level3_Approval_By='" + objCommonVar.CurrentUserCode + "', Is_Approved=1,  Posting_Date='' WHERE Document_Code='" + obj.Document_Code + "'"
            '    End If
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '    If UserLevel = obj.Approval_Level_Required Then
            '        qry = "Update TSPL_SD_QUOTATION_HEAD SET Is_Approved=1, Status=1, Posting_Date='" + strPostDate + "' WHERE Document_Code='" + obj.Document_Code + "'"
            '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    End If
            'End If

            ''------------------------------------------------------------
            qry = "Update TSPL_SD_QUOTATION_HEAD SET Is_Approved=1, Status=1, Posting_Date='" + strPostDate + "' WHERE Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SQDATACLOSE(ByVal strDocNo As String, ByVal cls As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document not found to Close")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            qry = "Update TSPL_SD_QUOTATION_HEAD SET close_yn='" + cls + "' WHERE Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Quotation No not found to Delete")
        End If
        Dim obj As clsSalesQuotationsHead = clsSalesQuotationsHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SD_QUOTATION_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_QUOTATION_HEAD where Document_Code='" + strCode + "'"
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

    Public Shared Function IsValidVendorForRequitionItem(ByVal strReqNo As String, ByVal strICode As String, ByVal strVendorCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_SD_QUOTATION_DETAIL left outer join TSPL_SD_QUOTATION_HEAD on TSPL_SD_QUOTATION_HEAD.Document_Code=TSPL_SD_QUOTATION_DETAIL.Document_Code where TSPL_SD_QUOTATION_HEAD.Document_Code ='" + strReqNo + "' and Item_Code='" + strICode + "' and TSPL_SD_QUOTATION_HEAD.Customer_Code not in ('','" + strVendorCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
    Public Shared Function IsValidProjectForQuotationItem(ByVal strReqNo As String, ByVal strProject As String) As Boolean
        Dim qry As String = "select 1 from TSPL_SD_QUOTATION_HEAD where Document_Code ='" + strReqNo + "' and PROJECT_ID='" & strProject & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        Else
            Return False
        End If
        Return True
    End Function
    Public Shared Function IsValidProjectForQuotation(ByVal strReqNo As String, ByVal strProject As String) As String
        Dim strProj As String = clsDBFuncationality.getSingleValue("select isnull(PROJECT_ID,'')  as PROJECT_ID from TSPL_SD_QUOTATION_HEAD where Document_Code ='" + strReqNo + "'")
        Return strProj
    End Function
End Class

Public Class clsSalesQuotationsDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
   
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing ''Not a Table Column.
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Unit_Code As String = Nothing
    Public Location As String = Nothing
    Public LocationName As String = Nothing ''Not a Table Column.
    Public Item_Cost As String = Nothing
    Public Item_Net_Amt As Double = 0
    Public Status As String = Nothing
    Public Order_No As String = Nothing
    Public Vendor_ItemNo As String = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Discount_Per As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_After_Discount As Double = 0
    Public Approval_Level_Required As Double = 0

    Public MRP As Double = 0
    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public Cust_Discount As Double = 0
    Public Total_Cust_Discount As Double = 0
    Public ActualRate As Double = 0
    Public Cust_DiscountQty As Double = 0
    Public Price_code As String = Nothing
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0
    Public FOC_Item As Double = 0
    Public Price_Date As String = Nothing
    Public Item_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Markup_On As String = Nothing
    Public Markup_Percent As Double = 0
    Public Landing_Cost As Double = 0
    Public HeadDiscAmt As Double = 0
    Public CustDiscPer As Double = 0
    Public CasdDiscScheme_Code As String = Nothing
    Public Purchase_Cost As Double = 0
    Public OrgRate As Double = 0
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public Bin_No As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strReqNo As String, ByVal strCustCode As String, ByVal Arr As List(Of clsSalesQuotationsDetail), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSalesQuotationsDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strReqNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                clsCommon.AddColumnsForChange(coll, "Order_No", obj.Order_No)
                clsCommon.AddColumnsForChange(coll, "Vendor_ItemNo", obj.Vendor_ItemNo)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Discount_Per", obj.Discount_Per)
                obj.Discount_Amt = (obj.Item_Net_Amt * obj.Discount_Per) / 100
                clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
                clsCommon.AddColumnsForChange(coll, "Amount_After_Discount", obj.Item_Net_Amt - obj.Discount_Amt)
                '--------------------------------------------------Approval Level-----------------
                obj.Approval_Level_Required = GetApprovalLevel(strCustCode, obj.Item_Code, obj.Discount_Per, trans)
                '---------------------------------------------------------------------------------
                clsCommon.AddColumnsForChange(coll, "Approval_Level_Required", obj.Approval_Level_Required)

                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", "N")
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", "N")
                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "ActualRate", obj.ActualRate)
                clsCommon.AddColumnsForChange(coll, "Cust_DiscountQty", obj.Cust_DiscountQty)
                clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)

                clsCommon.AddColumnsForChange(coll, "Item_Weight", obj.Item_Weight)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                clsCommon.AddColumnsForChange(coll, "Markup_On", obj.Markup_On)
                clsCommon.AddColumnsForChange(coll, "Markup_Percent", obj.Markup_Percent)
                clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                clsCommon.AddColumnsForChange(coll, "CustDiscPer", obj.CustDiscPer)
                clsCommon.AddColumnsForChange(coll, "CasdDiscScheme_Code", obj.CasdDiscScheme_Code)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "PrincipleCode", obj.PrincipleCode)
                clsCommon.AddColumnsForChange(coll, "PrincipleDesc", obj.PrincipleDesc)
                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_QUOTATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function

    Public Shared Function GetApprovalLevel(ByVal CustCode As String, ByVal itemCode As String, ByVal DiscPer As Double, ByVal trans As SqlTransaction) As Integer
        Try
            Dim Qry As String = "Select item_no, Discount_Per, 1 as Level from TSPL_CUSTOMER_ITEM_DETAIL WHERE Customer_Code='" + CustCode + "' AND item_no='" + itemCode + "' AND ISNULL(Discount_Per,0)<>0"
            Qry += " Union"
            Qry += " Select item_no, Discount_Per_Level2, 2 as Level from TSPL_CUSTOMER_ITEM_DETAIL WHERE Customer_Code='" + CustCode + "' AND item_no='" + itemCode + "' AND ISNULL(Discount_Per_Level2 ,0)<>0"
            Qry += " Union"
            Qry += " Select Item_Code, Discount_Per, 3 as level from TSPL_ITEM_PRICE_LIST3 WHERE Item_Code='" + itemCode + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            For Each dr As DataRow In dt.Rows
                If clsCommon.myCdbl(dr("Level")) < 3 Then
                    If DiscPer <= clsCommon.myCdbl(dr("Discount_Per")) Then
                        Return clsCommon.myCdbl(dr("Level"))
                    End If
                Else
                    Return 3
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return 0
    End Function

    Public Shared Function GetMaximumDiscount(ByVal UserLevel As Integer, ByVal CustCode As String, ByVal itemCode As String, ByVal trans As SqlTransaction) As Double
        Try
            Dim Qry As String = "Select item_no, Discount_Per, 1 as Level from TSPL_CUSTOMER_ITEM_DETAIL WHERE Customer_Code='" + CustCode + "' AND item_no='" + itemCode + "' AND ISNULL(Discount_Per,0)<>0"
            Qry += " Union"
            Qry += " Select item_no, Discount_Per_Level2, 2 as Level from TSPL_CUSTOMER_ITEM_DETAIL WHERE Customer_Code='" + CustCode + "' AND item_no='" + itemCode + "' AND ISNULL(Discount_Per_Level2 ,0)<>0"
            Qry += " Union"
            Qry += " Select Item_Code, Discount_Per, 3 as level from TSPL_ITEM_PRICE_LIST3 WHERE Item_Code='" + itemCode + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            For Each dr As DataRow In dt.Rows
                If clsCommon.myCdbl(dr("Level")) = objCommonVar.CurrUserLevel Then
                    Return clsCommon.myCdbl(dr("Discount_Per"))
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return 0
    End Function

    Public Shared Function GetBalanceRequitionQty(ByVal strReqCode As String, ByVal strICode As String, ByVal strCurrPONo As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from( " & _
            " select TSPL_SD_QUOTATION_DETAIL.Item_Code as ICode,TSPL_SD_QUOTATION_DETAIL.Qty as Qty,1 as RI from TSPL_SD_QUOTATION_DETAIL left outer join TSPL_SD_QUOTATION_HEAD on TSPL_SD_QUOTATION_HEAD.Document_Code=TSPL_SD_QUOTATION_DETAIL.Document_Code where TSPL_SD_QUOTATION_DETAIL.Status='N' and TSPL_SD_QUOTATION_HEAD.Status=1 and TSPL_SD_QUOTATION_DETAIL.Document_Code='" + strReqCode + "' and TSPL_SD_QUOTATION_DETAIL.Item_Code='" + strICode + "'" & _
            " union all " & _
            "select TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_DETAIL.Qty as Qty,-1 as RI from TSPL_SD_SALES_ORDER_DETAIL left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code where TSPL_SD_SALES_ORDER_DETAIL.Document_Code='" + strReqCode + "'   and TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + strICode + "' and TSPL_SD_SALES_ORDER_DETAIL.Document_Code not in ('" + strCurrPONo + "')  " & _
            ")Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteRequition(ByVal strReqCode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SD_QUOTATION_DETAIL set Status ='Y' where Document_Code='" + strReqCode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    

End Class

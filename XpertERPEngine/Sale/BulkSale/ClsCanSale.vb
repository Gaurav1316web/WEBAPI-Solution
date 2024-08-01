Imports System.Data.SqlClient
Imports common

Public Class ClsCanSale

#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = String.Empty
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Price_Code As String = Nothing
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public Standard_Rate As Double = 0
    Public TolerancePerPlus As Double = 0
    Public TolerancePerMinus As Double = 0
    Public DocumentAmount As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Created_Date As Date?
    Public Modified_Date As Date?
    Public CanInventoryType As Integer = 0
    Public TotalNoofCans As Double = 0
    Public CanItemCode As String = String.Empty
    Public CanItemUOM As String = String.Empty
    Public CanItemRate As Double = 0
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
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
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public arrCanSaleDetail As List(Of ClsCanSaleDetail) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    'Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
    '    Dim qry As String = "Select TSPL_CAN_SALE_HEAD.Document_No As Code ,TSPL_CAN_SALE_HEAD.Document_Date as Date from TSPL_CAN_SALE_HEAD "
    '    Return clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    'End Function
    Public Shared Function SaveData(ByVal obj As ClsCanSale, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsCanSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        '  Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim ApplyTSPriceAtBulkSale As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, trans)) = 1, True, False))

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanSale, obj.Location_Code, obj.Document_Date, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_CAN_SALE_HEAD", "Document_No", "TSPL_CAN_SALE_DETAIL", "Document_No", trans)
            End If
            qry = "delete from TSPL_CAN_SALE_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CanSale, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Name", obj.Location_Name)
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
            clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
            clsCommon.AddColumnsForChange(coll, "TolerancePerPlus", obj.TolerancePerPlus)
            clsCommon.AddColumnsForChange(coll, "TolerancePerMinus", obj.TolerancePerMinus)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "CanInventoryType", obj.CanInventoryType)
            clsCommon.AddColumnsForChange(coll, "TotalNoofCans", obj.TotalNoofCans)
            clsCommon.AddColumnsForChange(coll, "CanItemCode", obj.CanItemCode, True)
            clsCommon.AddColumnsForChange(coll, "CanItemUOM", obj.CanItemUOM)
            clsCommon.AddColumnsForChange(coll, "CanItemRate", obj.CanItemRate)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
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
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAN_SALE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAN_SALE_HEAD", OMInsertOrUpdate.Update, "TSPL_CAN_SALE_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            ClsCanSaleDetail.saveData(obj.arrCanSaleDetail, obj.Document_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String) As ClsCanSale
        Return GetData(strCode, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String) As ClsCanSale
        Return GetData(strCode, arrLoc, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsCanSale
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCanSale
        Dim obj As ClsCanSale = Nothing
        Dim Arr As List(Of ClsCanSale) = Nothing
        Dim qry As String = "Select *  from TSPL_CAN_SALE_HEAD where 2=2 "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_CAN_SALE_HEAD.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CAN_SALE_HEAD.Document_No = (select MIN(Document_No) from TSPL_CAN_SALE_HEAD WHERE 1=1 " + whrclas + " and TSPL_CAN_SALE_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_CAN_SALE_HEAD.Document_No = (select Max(Document_No) from TSPL_CAN_SALE_HEAD WHERE 1=1 " + whrclas + " and TSPL_CAN_SALE_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_CAN_SALE_HEAD.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_CAN_SALE_HEAD.Document_No = (select Min(Document_No) from TSPL_CAN_SALE_HEAD where Document_No>'" + strCode + "' " + whrclas + " and TSPL_CAN_SALE_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_CAN_SALE_HEAD.Document_No = (select Max(Document_No) from TSPL_CAN_SALE_HEAD where Document_No<'" + strCode + "' " + whrclas + " and TSPL_CAN_SALE_HEAD.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCanSale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If

            obj.Fat_Weightage = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
            obj.Snf_Weightage = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
            obj.Fat_Ratio = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
            obj.Snf_Ratio = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
            obj.Standard_Rate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            obj.TolerancePerMinus = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
            obj.TolerancePerPlus = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            obj.CanInventoryType = clsCommon.myCdbl(dt.Rows(0)("CanInventoryType"))
            obj.TotalNoofCans = clsCommon.myCdbl(dt.Rows(0)("TotalNoofCans"))
            obj.CanItemRate = clsCommon.myCdbl(dt.Rows(0)("CanItemRate"))
            obj.CanItemCode = clsCommon.myCstr(dt.Rows(0)("CanItemCode"))
            obj.CanItemUOM = clsCommon.myCstr(dt.Rows(0)("CanItemUOM"))
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
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
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.arrCanSaleDetail = ClsCanSaleDetail.getData(obj.Document_No, trans)
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
            Dim obj As New ClsCanSale()
            obj = ClsCanSale.GetData(strDocNo)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanSale, obj.Location_Code, obj.Document_Date, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_CAN_SALE_HEAD", "Document_No", "TSPL_CAN_SALE_DETAIL", "Document_No", trans)
            Dim qry As String = ""
                qry = "delete from TSPL_CAN_SALE_DETAIL where Document_No='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_CAN_SALE_HEAD where Document_No='" + strDocNo + "'"
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
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As ClsCanSale = ClsCanSale.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim objCSD As ClsCanSaleDispatch = ConvertCanSaleToDispatch(obj)
            ClsCanSaleDispatch.SaveData(objCSD, True, trans)
            ClsCanSaleDispatch.PostData("", arrLoc, objCSD.Document_No, trans)

            Dim qry = "Update TSPL_CAN_SALE_HEAD set Posted=1, " &
           "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " &
           " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_CAN_SALE_HEAD", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Sub updateJournalEntry(ByVal Source_type As String, ByVal Doc_No As String, ByVal amount As Double, ByVal trans As SqlTransaction)
        Dim sQuery As String = String.Empty
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_journal_master where source_code='" & Source_type & "' and source_doc_no='" & Doc_No & "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
            sQuery = "update tspl_journal_master set total_debit_amt=" & amount & ",total_credit_amt=" & amount & ",modify_by='" & objCommonVar.CurrentUserCode & "',modify_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "',posting_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss") & "' where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            sQuery = "update tspl_journal_details set amount=case when coalesce(amount,0)<0 then -1 else 1 end*" & amount & " where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        End If
    End Sub
    Private Shared Function ConvertCanSaleToDispatch(ByVal objDispatch As ClsCanSale) As ClsCanSaleDispatch
        Dim obj As New ClsCanSaleDispatch()
        obj = New ClsCanSaleDispatch()
        'obj.Document_No = objDispatch.Document_No
        obj.Document_Date = objDispatch.Document_Date
        obj.Customer_Code = objDispatch.Customer_Code
        obj.Location_Code = objDispatch.Location_Code
        obj.Customer_Name = objDispatch.Customer_Name
        obj.Location_Name = objDispatch.Location_Name
        obj.Price_Code = objDispatch.Price_Code
        obj.Fat_Ratio = objDispatch.Fat_Ratio
        obj.Fat_Weightage = objDispatch.Fat_Weightage
        obj.Snf_Ratio = objDispatch.Snf_Ratio
        obj.Snf_Weightage = objDispatch.Snf_Weightage
        obj.Standard_Rate = objDispatch.Standard_Rate
        obj.TolerancePerMinus = objDispatch.TolerancePerMinus
        obj.TolerancePerPlus = objDispatch.TolerancePerPlus
        obj.CanSale_Doc_No = objDispatch.Document_No
        obj.DocumentAmount = objDispatch.DocumentAmount
        obj.CanInventoryType = objDispatch.CanInventoryType
        obj.TotalNoofCans = objDispatch.TotalNoofCans
        obj.CanItemRate = objDispatch.CanItemRate
        obj.CanItemCode = objDispatch.CanItemCode
        obj.CanItemUOM = objDispatch.CanItemUOM

        obj.Tax_Group = objDispatch.Tax_Group
        obj.TAX1 = objDispatch.TAX1
        obj.TAX1_Rate = objDispatch.TAX1_Rate
        obj.TAX1_Base_Amt = objDispatch.TAX1_Base_Amt
        obj.TAX1_Amt = objDispatch.TAX1_Amt
        obj.TAX2 = objDispatch.TAX2
        obj.TAX2_Rate = objDispatch.TAX2_Rate
        obj.TAX2_Base_Amt = objDispatch.TAX2_Base_Amt
        obj.TAX2_Amt = objDispatch.TAX2_Amt
        obj.TAX3 = objDispatch.TAX3
        obj.TAX3_Base_Amt = objDispatch.TAX3_Base_Amt
        obj.TAX3_Rate = objDispatch.TAX3_Rate
        obj.TAX3_Amt = objDispatch.TAX3_Amt
        obj.TAX4 = objDispatch.TAX4
        obj.TAX4_Rate = objDispatch.TAX4_Rate
        obj.TAX4_Base_Amt = objDispatch.TAX4_Base_Amt
        obj.TAX4_Amt = objDispatch.TAX4_Amt
        obj.TAX5 = objDispatch.TAX5
        obj.TAX5_Rate = objDispatch.TAX5_Rate
        obj.TAX5_Base_Amt = objDispatch.TAX5_Base_Amt
        obj.TAX5_Amt = objDispatch.TAX5_Amt
        obj.Total_Tax_Amt = objDispatch.Total_Tax_Amt
        obj.Total_Amt = objDispatch.Total_Amt
        obj.Tax_Calculation_Type = IIf(objDispatch.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
        obj.ActualTCSBaseAmount = objDispatch.ActualTCSBaseAmount
        obj.ChangedTCSBaseAmount = objDispatch.ChangedTCSBaseAmount

        Dim introw As Integer = 0
        If (objDispatch.arrCanSaleDetail IsNot Nothing AndAlso objDispatch.arrCanSaleDetail.Count > 0) Then
            obj.arrCanSaleDispatchDetail = New List(Of ClsCanSaleDispatchDetail)
            Dim objTr As ClsCanSaleDispatchDetail
            For Each objDispatchDetail As ClsCanSaleDetail In objDispatch.arrCanSaleDetail
                objTr = New ClsCanSaleDispatchDetail
                'objTr.Document_No = objDispatchDetail.Document_No
                introw = introw + 1
                objTr.SL_No = introw
                objTr.NoOfCans = objDispatchDetail.NoOfCans
                objTr.Qty = objDispatchDetail.Qty
                objTr.FatPer = objDispatchDetail.FatPer
                objTr.ItemCode = objDispatchDetail.ItemCode
                objTr.ItemName = objDispatchDetail.ItemName
                objTr.UOM = objDispatchDetail.UOM
                objTr.SNFPer = objDispatchDetail.SNFPer
                objTr.Fat_KG = objDispatchDetail.Fat_KG
                objTr.SNF_KG = objDispatchDetail.SNF_KG
                objTr.MilkRate = objDispatchDetail.MilkRate
                objTr.MilkAmt = objDispatchDetail.MilkAmt
                objTr.PriceRate = objDispatchDetail.PriceRate
                objTr.FatRate = objDispatchDetail.FatRate
                objTr.SNFRate = objDispatchDetail.SNFRate
                objTr.FatAmount = objDispatchDetail.FatAmount
                objTr.SNFAmount = objDispatchDetail.SNFAmount
                objTr.TotalAmount = objDispatchDetail.TotalAmount
                objTr.Diff = objDispatchDetail.Diff
                objTr.TAX1 = objDispatchDetail.TAX1
                objTr.TAX1_Base_Amt = objDispatchDetail.TAX1_Base_Amt
                objTr.TAX1_Rate = objDispatchDetail.TAX1_Rate
                objTr.TAX1_Amt = objDispatchDetail.TAX1_Amt
                objTr.TAX2 = objDispatchDetail.TAX2
                objTr.TAX2_Base_Amt = objDispatchDetail.TAX2_Base_Amt
                objTr.TAX2_Rate = objDispatchDetail.TAX2_Rate
                objTr.TAX2_Amt = objDispatchDetail.TAX2_Amt
                objTr.TAX3 = objDispatchDetail.TAX3
                objTr.TAX3_Base_Amt = objDispatchDetail.TAX3_Base_Amt
                objTr.TAX3_Rate = objDispatchDetail.TAX3_Rate
                objTr.TAX3_Amt = objDispatchDetail.TAX3_Amt
                objTr.TAX4 = objDispatchDetail.TAX4
                objTr.TAX4_Base_Amt = objDispatchDetail.TAX4_Base_Amt
                objTr.TAX4_Rate = objDispatchDetail.TAX4_Rate
                objTr.TAX4_Amt = objDispatchDetail.TAX4_Amt
                objTr.TAX5 = objDispatchDetail.TAX5
                objTr.TAX5_Base_Amt = objDispatchDetail.TAX5_Base_Amt
                objTr.TAX5_Rate = objDispatchDetail.TAX5_Rate
                objTr.TAX5_Amt = objDispatchDetail.TAX5_Amt
                objTr.Total_Tax_Amt = objDispatchDetail.Total_Tax_Amt
                objTr.Item_Net_Amt = objDispatchDetail.Item_Net_Amt

                obj.arrCanSaleDispatchDetail.Add(objTr)
            Next
        End If
        Return obj
    End Function
    Public Shared Function CheckCustomerOutstandingAmount(ByVal strCode As String, ByVal strCustomer As String, ByVal TotAmt As Double) As Double
        Dim dblAmt As Double = 0
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0


            ' dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            '"select SUM(isnull(TSPL_CAN_SALE_HEAD.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_CAN_SALE_HEAD " & _
            '"where TSPL_CAN_SALE_HEAD.Posted=1  and TSPL_CAN_SALE_HEAD.Customer_Code='" & strCustomer & "' " & _
            '" union all " & _
            '"select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
            '"TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
            '"where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> '' and Customer_Code='" & strCustomer & "' ) xxx "))
            ''richa 10/10/2014
            Dim qry As String = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " &
            "select SUM(isnull(TSPL_CAN_SALE_HEAD.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_CAN_SALE_HEAD " &
            "where TSPL_CAN_SALE_HEAD.Posted=1  and TSPL_CAN_SALE_HEAD.Customer_Code='" & strCustomer & "' " &
            " union all " &
            "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " &
            "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " &
            "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> '' and Customer_Code='" & strCustomer & "' " &
          " union all " &
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " &
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " &
          " union all " &
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " &
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " &
          " union all " &
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " &
          "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "' ) xxx "
            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            ''=================
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'"))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and SecurityDeposit='Y' and SecurityDepositType not in ('C') and isnull(Posted,'')='Y' and Cust_Code='" & strCustomer & "'"))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_CAN_SALE_HEAD where  Posted=0 and Customer_Code='" & strCustomer & "' and Document_No<>'" + strCode + "'"))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
            Return dblAmt

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dblAmt
    End Function
End Class
Public Class ClsCanSaleDetail
    Public Document_No As String = Nothing
    Public SL_No As Double = 0
    Public NoOfCans As Double = 0
    Public ItemCode As String = String.Empty
    Public ItemName As String = String.Empty
    Public UOM As String = String.Empty
    Public Qty As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public Fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public MilkRate As Double = 0
    Public MilkAmt As Double = 0
    Public PriceRate As Double = 0
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
    Public TotalAmount As Double = 0
    Public Diff As Double = 0
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
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0

    Public Shared Function saveData(ByVal arrObj As List(Of ClsCanSaleDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As ClsCanSaleDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "SL_No", obj.SL_No)
                    clsCommon.AddColumnsForChange(coll, "NoOfCans", obj.NoOfCans)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "ItemCode", obj.ItemCode)
                    clsCommon.AddColumnsForChange(coll, "ItemName", obj.ItemName)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "MilkRate", obj.MilkRate)
                    clsCommon.AddColumnsForChange(coll, "MilkAmt", obj.MilkAmt)
                    clsCommon.AddColumnsForChange(coll, "PriceRate", obj.PriceRate)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "TotalAmount", obj.TotalAmount)
                    clsCommon.AddColumnsForChange(coll, "Diff", obj.Diff)
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
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAN_SALE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function

    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of ClsCanSaleDetail)
        Try
            Dim arrObj As List(Of ClsCanSaleDetail) = Nothing
            Dim obj As ClsCanSaleDetail = Nothing
            Dim qry As String = "Select * from TSPL_CAN_SALE_DETAIL where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsCanSaleDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsCanSaleDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SL_No = clsCommon.myCdbl(dt.Rows(i)("SL_No"))
                    obj.NoOfCans = clsCommon.myCdbl(dt.Rows(i)("NoOfCans"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.ItemCode = clsCommon.myCstr(dt.Rows(0)("ItemCode"))
                    obj.ItemName = clsCommon.myCstr(dt.Rows(0)("ItemName"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))
                    obj.Fat_KG = clsCommon.myCdbl(dt.Rows(i)("Fat_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.MilkRate = clsCommon.myCdbl(dt.Rows(i)("MilkRate"))
                    obj.MilkAmt = clsCommon.myCdbl(dt.Rows(i)("MilkAmt"))
                    obj.PriceRate = clsCommon.myCdbl(dt.Rows(i)("PriceRate"))
                    obj.FatRate = clsCommon.myCdbl(dt.Rows(i)("FatRate"))
                    obj.SNFRate = clsCommon.myCdbl(dt.Rows(i)("SNFRate"))
                    obj.FatAmount = clsCommon.myCdbl(dt.Rows(i)("FatAmount"))
                    obj.SNFAmount = clsCommon.myCdbl(dt.Rows(i)("SNFAmount"))
                    obj.TotalAmount = clsCommon.myCdbl(dt.Rows(i)("TotalAmount"))
                    obj.Diff = clsCommon.myCdbl(dt.Rows(i)("Diff"))
                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))
                    arrObj.Add(obj)

                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
'' CAN SALE DISPACTH TABLES

Public Class ClsCanSaleDispatch
#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = String.Empty
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Price_Code As String = Nothing
    Public CanSale_Doc_No As String = String.Empty
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public Standard_Rate As Double = 0
    Public TolerancePerPlus As Double = 0
    Public TolerancePerMinus As Double = 0
    Public DocumentAmount As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?

    Public Created_Date As Date?
    Public Modified_Date As Date?
    Public CanInventoryType As Integer = 0
    Public TotalNoofCans As Double = 0
    Public CanItemCode As String = String.Empty
    Public CanItemUOM As String = String.Empty
    Public CanItemRate As Double = 0
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
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
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public arrCanSaleDispatchDetail As List(Of ClsCanSaleDispatchDetail) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsCanSaleDispatch, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsCanSaleDispatch, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim ApplyTSPriceAtBulkSale As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, trans)) = 1, True, False))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanSale, obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_CANSALE_DISPATCH_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CanSaleDispatch, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "CanSale_Doc_No", obj.CanSale_Doc_No)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Name", obj.Location_Name)
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
            clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
            clsCommon.AddColumnsForChange(coll, "TolerancePerPlus", obj.TolerancePerPlus)
            clsCommon.AddColumnsForChange(coll, "TolerancePerMinus", obj.TolerancePerMinus)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "CanInventoryType", obj.CanInventoryType)
            clsCommon.AddColumnsForChange(coll, "TotalNoofCans", obj.TotalNoofCans)
            clsCommon.AddColumnsForChange(coll, "CanItemCode", obj.CanItemCode, True)
            clsCommon.AddColumnsForChange(coll, "CanItemUOM", obj.CanItemUOM)
            clsCommon.AddColumnsForChange(coll, "CanItemRate", obj.CanItemRate)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
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
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)

            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_DISPATCH_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_DISPATCH_HEAD", OMInsertOrUpdate.Update, "TSPL_CANSALE_DISPATCH_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            ClsCanSaleDispatchDetail.saveData(obj.arrCanSaleDispatchDetail, obj.Document_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
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
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As ClsCanSaleDispatch = ClsCanSaleDispatch.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            Dim settTankerDispatchAvgFATSNFPer As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, trans)) = 1)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            CreateInventoryMovement(obj, trans, settTankerDispatchAvgFATSNFPer)
            CreateJournalEntry(obj.Document_No, arrLoc, trans, "")

            Dim qry = "Update TSPL_CANSALE_DISPATCH_HEAD set Posted=1, " &
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "'  " &
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If settTankerDispatchAvgFATSNFPer Then
                ''Becuase FAT,SNF Changed when inventory hits.
                obj = ClsCanSaleDispatch.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            End If

            Dim objSI As ClsCanSaleInvoice = ConvertDispatchToSaleInvoice(obj)
            ClsCanSaleInvoice.SaveData(objSI, True, trans)
            ClsCanSaleInvoice.PostData("", arrLoc, objSI.Document_No, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Sub CreateInventoryMovement(ByVal obj As ClsCanSaleDispatch, ByVal trans As SqlTransaction, ByVal settTankerDispatchAvgFATSNFPer As Boolean)
        Try
            Dim isSaved As Boolean = True
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0


            For Each objTr As ClsCanSaleDispatchDetail In obj.arrCanSaleDispatchDetail
                intCounter = intCounter + 1
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.ItemCode, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                End If

                ''-----------------------------------------
                Dim strsublocation As String = String.Empty
                Dim strqry As String = String.Empty
                strqry = "select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where is_sub_location='Y'  and TSPL_Location_MASTER.Rejected_Type='N' and Main_Location_Code='" & clsCommon.myCstr(obj.Location_Code) & "' order by TSPL_Location_MASTER.Location_Code "
                Dim balqtyofvl As Decimal = 0.0
                Dim balqtyfortotallocation As Decimal = 0.0
                Dim fatqtyfortotallocation As Decimal = 0.0
                Dim snfqtyfortotallocation As Decimal = 0.0
                Dim CurBalOFVL_SNF As Decimal = 0
                Dim CurBalOFVL_FAT As Decimal = 0
                Dim totalamt As Decimal = 0
                Dim amtdifference As Decimal = 0
                Dim strtransactionLocation As String = clsCommon.myCstr(obj.Location_Code)
                Dim IsSubLocation As Boolean = False
                balqtyfortotallocation = objTr.Qty
                fatqtyfortotallocation = objTr.Fat_KG
                snfqtyfortotallocation = objTr.SNF_KG

                ''richa TEC/23/07/19-000954 for silo qty
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                        If clsCommon.myLen(strsublocation) > 0 Then
                            balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(objTr.ItemCode, obj.Location_Code, strsublocation, obj.Document_No, obj.Document_Date, trans, objTr.UOM))
                        End If
                        'If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                        If balqtyfortotallocation > 0 Then
                            Dim objInventoryMovemnt1 As New clsInventoryMovementNew()
                            Dim ArrInventoryMovement1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                            objInventoryMovemnt1.InOut = "O"
                            ''balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(objTr.ItemCode, obj.Location_Code, strsublocation, obj.Document_No, obj.Document_Date, trans, objTr.UOM))
                            If balqtyofvl >= balqtyfortotallocation Then
                                balqtyofvl = balqtyfortotallocation
                            ElseIf balqtyofvl < 0 Then
                                balqtyofvl = 0
                            End If
                            balqtyofvl = Math.Round(balqtyofvl, 3)
                            If balqtyofvl > 0 Then
                                objInventoryMovemnt1.main_location = obj.Location_Code
                                objInventoryMovemnt1.Location_Code = strsublocation
                                objInventoryMovemnt1.Cust_Code = obj.Customer_Code
                                objInventoryMovemnt1.Cust_Name = clsCustomerMaster.GetName(obj.Customer_Code, trans)
                                objInventoryMovemnt1.Item_Code = objTr.ItemCode
                                objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(objTr.ItemCode, trans)
                                objInventoryMovemnt1.Qty = balqtyofvl
                                objInventoryMovemnt1.UOM = objTr.UOM
                                objInventoryMovemnt1.MRP = objTr.MilkAmt / balqtyofvl
                                objInventoryMovemnt1.Add_Cost = 0
                                If settTankerDispatchAvgFATSNFPer Then
                                    Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", "MI", objTr.ItemCode, strsublocation, 1, objTr.UOM, 1, 1, obj.Document_Date, obj.Document_Date, False, trans, obj.Document_No)
                                    objInventoryMovemnt1.FAT_Per = Math.Round(objMCT.FAT_Per, 2, MidpointRounding.ToEven)
                                    objInventoryMovemnt1.SNF_Per = Math.Round(objMCT.SNF_Per, 2, MidpointRounding.ToEven)
                                    objInventoryMovemnt1.FAT_KG = Math.Round(objMCT.FAT_Per * balqtyofvl / 100, 3, MidpointRounding.ToEven)
                                    objInventoryMovemnt1.SNF_KG = Math.Round(objMCT.SNF_Per * balqtyofvl / 100, 3, MidpointRounding.ToEven)
                                    objInventoryMovemnt1.Net_Cost = Math.Round((objMCT.FAT_Cost * objInventoryMovemnt1.FAT_KG) + (objMCT.SNF_Cost * objInventoryMovemnt1.SNF_KG), 2)
                                    objInventoryMovemnt1.Fat_Rate = objMCT.FAT_Cost
                                    objInventoryMovemnt1.SNF_Rate = objMCT.SNF_Cost
                                    objInventoryMovemnt1.Fat_Amt = clsCommon.myFormat(objMCT.FAT_Cost * objInventoryMovemnt1.FAT_KG)
                                    objInventoryMovemnt1.SNF_Amt = clsCommon.myFormat(objMCT.SNF_Cost * objInventoryMovemnt1.SNF_KG)
                                    objInventoryMovemnt1.DonNotCalculateAvgFATSNFCost = True
                                Else
                                    objInventoryMovemnt1.FAT_Per = objTr.FatPer
                                    objInventoryMovemnt1.SNF_Per = objTr.SNFPer
                                    objInventoryMovemnt1.FAT_KG = objTr.FatPer * balqtyofvl / 100
                                    objInventoryMovemnt1.SNF_KG = objTr.SNFPer * balqtyofvl / 100
                                    objInventoryMovemnt1.Net_Cost = objTr.MilkAmt
                                    objInventoryMovemnt1.Fat_Rate = objTr.FatRate
                                    objInventoryMovemnt1.SNF_Rate = objTr.SNFRate
                                    objInventoryMovemnt1.Fat_Amt = objTr.FatAmount
                                    objInventoryMovemnt1.SNF_Amt = objTr.SNFAmount
                                End If
                                objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt1.Net_Cost / balqtyofvl
                                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                    objInventoryMovemnt1.ItemType = "RM"
                                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                    objInventoryMovemnt1.ItemType = "OT"
                                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                    objInventoryMovemnt1.ItemType = "FT"
                                End If
                                objInventoryMovemnt1.ItemType = strItemTypeToSave

                                ArrInventoryMovement1.Add(objInventoryMovemnt1)

                                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DisCanSale", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
                            End If
                            balqtyfortotallocation = balqtyfortotallocation - balqtyofvl
                            fatqtyfortotallocation = fatqtyfortotallocation - Math.Round((objTr.FatPer * balqtyofvl / 100), 3)
                            snfqtyfortotallocation = snfqtyfortotallocation - Math.Round((objTr.SNFPer * balqtyofvl / 100), 3)

                            fatqtyfortotallocation = Math.Round(fatqtyfortotallocation, 3)
                            snfqtyfortotallocation = Math.Round(snfqtyfortotallocation, 3)
                        End If
                    Next
                End If

                ''-- to insert data into main locations when sub locations has no value
                'If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                If balqtyfortotallocation > 0 Then
                    totalamt = 0
                    amtdifference = 0
                    Dim objInventoryMovemnt1 As New clsInventoryMovementNew()
                    Dim ArrInventoryMovement1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                    objInventoryMovemnt1.InOut = "O"

                    balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(objTr.ItemCode, obj.Location_Code, "", obj.Document_No, obj.Document_Date, trans, objTr.UOM))
                    If balqtyofvl >= balqtyfortotallocation Then
                        balqtyofvl = balqtyfortotallocation
                    End If
                    balqtyofvl = Math.Round(balqtyofvl, 3)
                    If balqtyofvl > 0 Then
                        objInventoryMovemnt1.main_location = obj.Location_Code
                        objInventoryMovemnt1.Location_Code = obj.Location_Code
                        objInventoryMovemnt1.Cust_Code = obj.Customer_Code
                        objInventoryMovemnt1.Cust_Name = clsCustomerMaster.GetName(obj.Customer_Code, trans)
                        objInventoryMovemnt1.Item_Code = objTr.ItemCode
                        objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(objTr.ItemCode, trans)
                        objInventoryMovemnt1.Qty = balqtyofvl
                        objInventoryMovemnt1.UOM = objTr.UOM
                        objInventoryMovemnt1.MRP = objTr.MilkAmt / balqtyofvl
                        objInventoryMovemnt1.Add_Cost = 0
                        If settTankerDispatchAvgFATSNFPer Then
                            Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", "MI", objTr.ItemCode, obj.Location_Code, 1, objTr.UOM, 1, 1, obj.Document_Date, obj.Document_Date, False, trans, obj.Document_No)
                            objInventoryMovemnt1.FAT_Per = Math.Round(objMCT.FAT_Per, 2, MidpointRounding.ToEven)
                            objInventoryMovemnt1.SNF_Per = Math.Round(objMCT.SNF_Per, 2, MidpointRounding.ToEven)
                            objInventoryMovemnt1.FAT_KG = Math.Round(objMCT.FAT_Per * balqtyofvl / 100, 3, MidpointRounding.ToEven)
                            objInventoryMovemnt1.SNF_KG = Math.Round(objMCT.SNF_Per * balqtyofvl / 100, 3, MidpointRounding.ToEven)
                            objInventoryMovemnt1.Net_Cost = Math.Round((objMCT.FAT_Cost * objInventoryMovemnt1.FAT_KG) + (objMCT.SNF_Cost * objInventoryMovemnt1.SNF_KG), 2)
                            objInventoryMovemnt1.Fat_Rate = objMCT.FAT_Cost
                            objInventoryMovemnt1.SNF_Rate = objMCT.SNF_Cost
                            objInventoryMovemnt1.Fat_Amt = clsCommon.myFormat(objMCT.FAT_Cost * objInventoryMovemnt1.FAT_KG)
                            objInventoryMovemnt1.SNF_Amt = clsCommon.myFormat(objMCT.SNF_Cost * objInventoryMovemnt1.SNF_KG)
                            objInventoryMovemnt1.DonNotCalculateAvgFATSNFCost = True
                        Else
                            objInventoryMovemnt1.FAT_Per = objTr.FatPer
                            objInventoryMovemnt1.SNF_Per = objTr.SNFPer
                            objInventoryMovemnt1.FAT_KG = objTr.FatPer * balqtyofvl / 100
                            objInventoryMovemnt1.SNF_KG = objTr.SNFPer * balqtyofvl / 100
                            objInventoryMovemnt1.Fat_Rate = objTr.FatRate
                            objInventoryMovemnt1.SNF_Rate = objTr.SNFRate
                            objInventoryMovemnt1.Fat_Amt = objTr.FatAmount
                            objInventoryMovemnt1.SNF_Amt = objTr.SNFAmount
                            objInventoryMovemnt1.Net_Cost = objTr.MilkAmt
                        End If
                        objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt1.Net_Cost / balqtyofvl
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt1.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objInventoryMovemnt1.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt1.ItemType = "FT"
                        End If
                        objInventoryMovemnt1.ItemType = strItemTypeToSave
                        ArrInventoryMovement1.Add(objInventoryMovemnt1)

                        isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DisCanSale", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
                    End If
                    balqtyfortotallocation = balqtyfortotallocation - balqtyofvl
                    fatqtyfortotallocation = fatqtyfortotallocation - Math.Round((objTr.FatPer * balqtyofvl / 100), 3)
                    snfqtyfortotallocation = snfqtyfortotallocation - Math.Round((objTr.SNFPer * balqtyofvl / 100), 3)

                    fatqtyfortotallocation = Math.Round(fatqtyfortotallocation, 3)
                    snfqtyfortotallocation = Math.Round(snfqtyfortotallocation, 3)
                End If

                ''-----------------------------------------
                If balqtyfortotallocation > 0 Then
                    Throw New Exception("Qty is Not Available")
                End If
                If Not settTankerDispatchAvgFATSNFPer Then
                    If fatqtyfortotallocation > 0 Then
                        Throw New Exception("Excess FAT Kg [" + clsCommon.myCstr(fatqtyfortotallocation) + "]")
                    End If
                    If snfqtyfortotallocation > 0 Then
                        Throw New Exception("Excess SNF Kg [" + clsCommon.myCstr(snfqtyfortotallocation) + "]")
                    End If
                End If
            Next
            'If settTankerDispatchAvgFATSNFPer Then
            '    Dim qry As String = " "
            '    qry = "select Item_Code,sum(Qty) as Qty,sum(Stock_Qty) as Stock_Qty,(case when sum(Qty)=0 then 0 else sum(Fat_KG)*100/sum(Qty) end) as Fat_Per,case when sum(Fat_KG)=0 then 0 else sum(Fat_Amt)/sum(Fat_KG) end as Fat_Rate,sum(Fat_KG) as Fat_KG,sum(Fat_Amt) as Fat_Amt,(case when sum(Qty)=0 then 0 else sum(SNF_KG)*100/sum(Qty) end) as SNF_Per,  case when sum(SNF_KG)=0 then 0 else sum(SNF_Amt)/sum(SNF_KG) end as SNF_Rate,sum(SNF_KG) as SNF_KG,sum(SNF_Amt) as SNF_Amt,sum(Amount) as Amount from (" + Environment.NewLine + _
            '    "select Location_Code,Item_Code,Qty,UOM,Stock_Qty,Stock_UOM,Fat_Per,Fat_Rate,Fat_KG,Fat_Amt,SNF_Per,SNF_Rate,SNF_KG,SNF_Amt,(FAT_amt+ SNF_Amt) as Amount from tspl_inventory_movement_new where Trans_Type='DisCanSale' and Source_Doc_No='" + obj.Document_No + "'" + Environment.NewLine + _
            '    ")xx group by Item_Code"
            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        Dim coll As New Hashtable()
            '        Dim strCanSaleNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CanSale_Doc_No from TSPL_CANSALE_DISPATCH_HEAD where Document_No='" + obj.Document_No + "'", trans))
            '        Dim strInvNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_CANSALE_INVOICE_HEAD where CanSale_Dispatch_No='" + obj.Document_No + "'", trans))
            '        For Each drSumm As DataRow In dt.Rows
            '            coll = New Hashtable()
            '            clsCommon.AddColumnsForChange(coll, "FatPer", Math.Round(clsCommon.myCdbl(drSumm("Fat_Per")), 2, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "SNFPer", Math.Round(clsCommon.myCdbl(drSumm("SNF_Per")), 2, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "Fat_KG", Math.Round(clsCommon.myCdbl(drSumm("Fat_KG")), 3, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "SNF_KG", Math.Round(clsCommon.myCdbl(drSumm("SNF_KG")), 3, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "FatRate", Math.Round(clsCommon.myCdbl(drSumm("Fat_Rate")), 2, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "SNFRate", Math.Round(clsCommon.myCdbl(drSumm("SNF_Rate")), 2, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "FatAmount", Math.Round(clsCommon.myCdbl(drSumm("Fat_Amt")), 2, MidpointRounding.ToEven))
            '            clsCommon.AddColumnsForChange(coll, "SNFAmount", Math.Round(clsCommon.myCdbl(drSumm("SNF_Amt")), 2, MidpointRounding.ToEven))
            '            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_DISPATCH_DETAIL", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "' and ItemCode='" + clsCommon.myCstr(drSumm("Item_Code")) + "'", trans)
            '            If clsCommon.myLen(strInvNo) > 0 Then
            '                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_INVOICE_DETAIL", OMInsertOrUpdate.Update, "Document_No='" + strInvNo + "' and ItemCode='" + clsCommon.myCstr(drSumm("Item_Code")) + "'", trans)
            '            End If
            '            If clsCommon.myLen(strCanSaleNo) > 0 Then
            '                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAN_SALE_DETAIL", OMInsertOrUpdate.Update, "Document_No='" + strCanSaleNo + "' and ItemCode='" + clsCommon.myCstr(drSumm("Item_Code")) + "'", trans)
            '            End If
            '        Next
            '    End If
            'End If

            ''richa 25 May,2015 BHA/09/05/18-000021
            If obj.CanInventoryType = 1 Then
                Dim strCanItem = obj.CanItemCode
                If clsCommon.myLen(strCanItem) > 0 Then
                    Dim dblRate As Integer = clsCommon.myCdbl(obj.CanItemRate)
                    Dim strCanUOM = obj.CanItemUOM
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strCanItem, obj.Location_Code, obj.CanSale_Doc_No, obj.Document_Date, trans, strCanUOM, 0)
                    Dim dblEnteredQty As Double = obj.TotalNoofCans
                    If dblEnteredQty > dblBalQty Then
                        Throw New Exception("Document No - " + obj.Document_No + Environment.NewLine + "Item - " + strCanUOM + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        isSaved = False
                    Else
                        Dim strItemType As String = clsItemMaster.GetItemType(strCanItem, trans)
                        Dim strItemTypeToSave As String = ""
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                        End If
                        Dim objInventoryMovemnt As New clsInventoryMovementNew()
                        Dim ArrInventoryMovementCrate As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.Location_Code = obj.Location_Code

                        objInventoryMovemnt.Cust_Code = obj.Customer_Code
                        objInventoryMovemnt.Cust_Name = obj.Customer_Name

                        objInventoryMovemnt.Item_Code = strCanItem
                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCanItem, trans)
                        objInventoryMovemnt.Qty = obj.TotalNoofCans
                        objInventoryMovemnt.UOM = strCanUOM
                        objInventoryMovemnt.Basic_Cost = dblRate
                        objInventoryMovemnt.MRP = 0
                        objInventoryMovemnt.Add_Cost = 0
                        objInventoryMovemnt.Net_Cost = 0
                        objInventoryMovemnt.ItemType = strItemTypeToSave
                        ArrInventoryMovementCrate.Add(objInventoryMovemnt)
                        isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DisCanSale", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

                    End If
                Else
                    Throw New Exception("Please Create item as Can type Item.")
                    isSaved = False
                End If
            End If
            ''-------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    ''richa 19 Sep, 2018
    'Public Shared Sub CreateInventoryMovement(ByVal obj As ClsCanSaleDispatch, ByVal trans As SqlTransaction)
    '    Try
    '        Dim isSaved As Boolean = True
    '        Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
    '        Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

    '        Dim strRgpNo As String = Nothing
    '        Dim intCounter As Integer = 0
    '        For Each objTr As ClsCanSaleDispatchDetail In obj.arrCanSaleDispatchDetail
    '            intCounter = intCounter + 1
    '            Dim strItemType As String = clsItemMaster.GetItemType(objTr.ItemCode, trans)
    '            Dim strItemTypeToSave As String = ""
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            Else
    '                strItemTypeToSave = strItemType
    '            End If

    '            Dim objLocationDetails As New clsItemLocationDetails()

    '            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.ItemCode, objTr.UOM, trans)
    '            If ConvFac = 0 Then
    '                Throw New Exception("Conversion Factor found zero for item :" + objTr.ItemCode + " and Uom:'" + objTr.UOM)
    '            End If
    '            objLocationDetails.Item_Code = objTr.ItemCode
    '            objLocationDetails.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.ItemCode + "' ", trans)
    '            objLocationDetails.Location_Code = obj.Location_Code
    '            objLocationDetails.Location_Desc = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "' ", trans)
    '            objLocationDetails.Item_Qty = -1 * objTr.Qty
    '            objLocationDetails.Amount = -1 * objTr.MilkAmt
    '            objLocationDetails.MRP = 0 * ConvFac

    '            objLocationDetails.ItemType = strItemTypeToSave
    '            ArrLocationDetails.Add(objLocationDetails)

    '            Dim SubLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   WHERE  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" & clsCommon.myCstr(obj.Location_Code) & "' ", trans))

    '            Dim objInventoryMovemnt As New clsInventoryMovementNew()
    '            objInventoryMovemnt.InOut = "O"
    '            If clsCommon.myLen(SubLocation) > 0 Then
    '                objInventoryMovemnt.Location_Code = SubLocation
    '                objInventoryMovemnt.main_location = obj.Location_Code
    '            Else
    '                objInventoryMovemnt.Location_Code = obj.Location_Code
    '            End If
    '            objInventoryMovemnt.Cust_Code = obj.Customer_Code
    '            objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(obj.Customer_Code, trans)

    '            objInventoryMovemnt.Item_Code = objTr.ItemCode
    '            objInventoryMovemnt.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.ItemCode + "' ", trans)
    '            objInventoryMovemnt.Qty = objTr.Qty
    '            objInventoryMovemnt.UOM = objTr.UOM
    '            objInventoryMovemnt.MRP = objTr.MilkAmt / objTr.Qty
    '            objInventoryMovemnt.Add_Cost = 0
    '            objInventoryMovemnt.FAT_Per = objTr.FatPer
    '            objInventoryMovemnt.SNF_Per = objTr.SNFPer
    '            objInventoryMovemnt.FAT_KG = objTr.Fat_KG
    '            objInventoryMovemnt.SNF_KG = objTr.SNF_KG
    '            objInventoryMovemnt.Net_Cost = objTr.MilkAmt
    '            objInventoryMovemnt.Basic_Cost = objTr.MilkAmt / objTr.Qty

    '            '' cost columns
    '            objInventoryMovemnt.Fat_Rate = objTr.FatRate
    '            objInventoryMovemnt.SNF_Rate = objTr.SNFRate
    '            objInventoryMovemnt.Fat_Amt = objTr.FatAmount
    '            objInventoryMovemnt.SNF_Amt = objTr.SNFAmount

    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                objInventoryMovemnt.ItemType = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                objInventoryMovemnt.ItemType = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                objInventoryMovemnt.ItemType = "FT"
    '            End If
    '            objInventoryMovemnt.ItemType = strItemTypeToSave
    '            ArrInventoryMovement.Add(objInventoryMovemnt)
    '        Next
    '        isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DisCanSale", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

    '        ''richa 25 May,2015 BHA/09/05/18-000021
    '        If obj.CanInventoryType = 1 Then
    '            Dim strCanItem = obj.CanItemCode
    '            If clsCommon.myLen(strCanItem) > 0 Then
    '                Dim dblRate As Integer = clsCommon.myCdbl(obj.CanItemRate)
    '                Dim strCanUOM = obj.CanItemUOM
    '                Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strCanItem, obj.Location_Code, obj.CanSale_Doc_No, obj.Document_Date, trans, strCanUOM, 0)
    '                Dim dblEnteredQty As Double = obj.TotalNoofCans
    '                If dblEnteredQty > dblBalQty Then
    '                    Throw New Exception("Document No - " + obj.Document_No + Environment.NewLine + "Item - " + strCanUOM + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
    '                    isSaved = False
    '                Else
    '                    Dim strItemType As String = clsItemMaster.GetItemType(strCanItem, trans)
    '                    Dim strItemTypeToSave As String = ""
    '                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "RM"
    '                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "OT"
    '                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "FT"
    '                    Else
    '                        strItemTypeToSave = strItemType
    '                    End If
    '                    Dim objInventoryMovemnt As New clsInventoryMovementNew()
    '                    Dim ArrInventoryMovementCrate As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
    '                    objInventoryMovemnt.InOut = "O"
    '                    objInventoryMovemnt.Location_Code = obj.Location_Code

    '                    objInventoryMovemnt.Cust_Code = obj.Customer_Code
    '                    objInventoryMovemnt.Cust_Name = obj.Customer_Name

    '                    objInventoryMovemnt.Item_Code = strCanItem
    '                    objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCanItem, trans)
    '                    objInventoryMovemnt.Qty = obj.TotalNoofCans
    '                    objInventoryMovemnt.UOM = strCanUOM
    '                    objInventoryMovemnt.Basic_Cost = dblRate
    '                    objInventoryMovemnt.MRP = 0
    '                    objInventoryMovemnt.Add_Cost = 0
    '                    objInventoryMovemnt.Net_Cost = 0
    '                    objInventoryMovemnt.ItemType = strItemTypeToSave
    '                    ArrInventoryMovementCrate.Add(objInventoryMovemnt)
    '                    isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DisCanSale", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

    '                End If
    '            Else
    '                Throw New Exception("Please Create item as Can type Item.")
    '                isSaved = False
    '            End If
    '        End If
    '        ''-------------

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
    ''---------

    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal strVourcherNoForRecreateOnly As String, Optional ByVal isGLUpdate As Boolean = False)
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
            Dim obj As New ClsCanSaleDispatch
            obj = ClsCanSaleDispatch.GetData(strCode, arrLoc, NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strInventoryControlAc As String = ""
            Dim strShipmentClearingAC As String = ""
            Dim dblTotalCost As Double = 0
            Dim dblCogsCost As Double = 0

            strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.arrCanSaleDispatchDetail.Item(0).ItemCode + "'", trans)
            strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location_Code, trans)

            If clsCommon.myLen(strShipmentClearingAC) = 0 Then
                Throw New Exception("Please set Shipment clearing Account for first item")
            End If

            dblCogsCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_No & "'", trans))

            Dim Acc() As String = {strShipmentClearingAC, dblCogsCost, "", "", "", "", "", "", "H"}
            ArryLstGLAC.Add(Acc)



            Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_No & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                    strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Location_Code, trans)

                    If clsCommon.myLen(strInventoryControlAc) = 0 Then
                        Throw New Exception("Please set Inventory Control Account for first item")
                    End If
                    Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost"))}
                    ArryLstGLAC.Add(Acc1)
                Next
            End If
            '' BHA/30/10/18-000646 RICHA AGARWAL SEND CUSTOMER CODE AND CUSTOMER NAME INTO JOURNAL ENTRY AND TYPE C instead of O 30 Oct,2018
            ''richa 09/03/2015
            If isGLUpdate Then
                ClsCanSale.updateJournalEntry("DS-CS", obj.Document_No, dblCogsCost, trans)
            ElseIf clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, strVourcherNoForRecreateOnly, trans, obj.Document_Date, "Journal Entry Against Dispatch Can Sale for Document No " + obj.Document_No + " ", "DS-CS", "DISPATCH Can Sale", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, "Journal Entry Against Dispatch Can Sale for Document No " + obj.Document_No + " ", "DS-CS", "DISPATCH Can Sale", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")
            End If

        End If
    End Sub
    Private Shared Function ConvertDispatchToSaleInvoice(ByVal objDispatch As ClsCanSaleDispatch) As ClsCanSaleInvoice
        Dim obj As New ClsCanSaleInvoice()
        obj = New ClsCanSaleInvoice()
        'obj.Document_No = objDispatch.Document_No
        obj.Document_Date = objDispatch.Document_Date
        obj.Customer_Code = objDispatch.Customer_Code
        obj.Location_Code = objDispatch.Location_Code
        obj.Customer_Name = objDispatch.Customer_Name
        obj.Location_Name = objDispatch.Location_Name
        obj.Price_Code = objDispatch.Price_Code
        obj.Fat_Ratio = objDispatch.Fat_Ratio
        obj.Fat_Weightage = objDispatch.Fat_Weightage
        obj.Snf_Ratio = objDispatch.Snf_Ratio
        obj.Snf_Weightage = objDispatch.Snf_Weightage
        obj.Standard_Rate = objDispatch.Standard_Rate
        obj.TolerancePerMinus = objDispatch.TolerancePerMinus
        obj.TolerancePerPlus = objDispatch.TolerancePerPlus
        obj.CanSale_Doc_No = objDispatch.CanSale_Doc_No
        obj.CanSale_Dispatch_No = objDispatch.Document_No
        obj.CanInventoryType = objDispatch.CanInventoryType
        obj.TotalNoofCans = objDispatch.TotalNoofCans
        obj.CanItemRate = objDispatch.CanItemRate
        obj.CanItemCode = objDispatch.CanItemCode
        obj.CanItemUOM = objDispatch.CanItemUOM

        obj.Tax_Group = objDispatch.Tax_Group
        obj.TAX1 = objDispatch.TAX1
        obj.TAX1_Rate = objDispatch.TAX1_Rate
        obj.TAX1_Base_Amt = objDispatch.TAX1_Base_Amt
        obj.TAX1_Amt = objDispatch.TAX1_Amt
        obj.TAX2 = objDispatch.TAX2
        obj.TAX2_Rate = objDispatch.TAX2_Rate
        obj.TAX2_Base_Amt = objDispatch.TAX2_Base_Amt
        obj.TAX2_Amt = objDispatch.TAX2_Amt
        obj.TAX3 = objDispatch.TAX3
        obj.TAX3_Base_Amt = objDispatch.TAX3_Base_Amt
        obj.TAX3_Rate = objDispatch.TAX3_Rate
        obj.TAX3_Amt = objDispatch.TAX3_Amt
        obj.TAX4 = objDispatch.TAX4
        obj.TAX4_Rate = objDispatch.TAX4_Rate
        obj.TAX4_Base_Amt = objDispatch.TAX4_Base_Amt
        obj.TAX4_Amt = objDispatch.TAX4_Amt
        obj.TAX5 = objDispatch.TAX5
        obj.TAX5_Rate = objDispatch.TAX5_Rate
        obj.TAX5_Base_Amt = objDispatch.TAX5_Base_Amt
        obj.TAX5_Amt = objDispatch.TAX5_Amt
        obj.Total_Tax_Amt = objDispatch.Total_Tax_Amt
        obj.Total_Amt = objDispatch.Total_Amt
        obj.Tax_Calculation_Type = IIf(objDispatch.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
        obj.ActualTCSBaseAmount = objDispatch.ActualTCSBaseAmount
        obj.ChangedTCSBaseAmount = objDispatch.ChangedTCSBaseAmount
        obj.DocumentAmount = objDispatch.DocumentAmount
        If Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0) > clsCommon.myCdbl(objDispatch.Total_Amt) Then
            obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0) - clsCommon.myCdbl(objDispatch.Total_Amt), 2)
            obj.Total_Amt = Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)
        Else
            obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt)) - clsCommon.myCdbl(objDispatch.Total_Amt), 2)
            obj.Total_Amt = Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)

        End If

        If (objDispatch.arrCanSaleDispatchDetail IsNot Nothing AndAlso objDispatch.arrCanSaleDispatchDetail.Count > 0) Then
            obj.arrCanSaleInvoiceDetail = New List(Of ClsCanSaleInvoiceDetail)
            Dim objTr As ClsCanSaleInvoiceDetail
            For Each objDispatchDetail As ClsCanSaleDispatchDetail In objDispatch.arrCanSaleDispatchDetail
                objTr = New ClsCanSaleInvoiceDetail
                objTr.Document_No = objDispatchDetail.Document_No
                objTr.SL_No = objDispatchDetail.SL_No
                objTr.NoOfCans = objDispatchDetail.NoOfCans
                objTr.Qty = objDispatchDetail.Qty
                objTr.ItemCode = objDispatchDetail.ItemCode
                objTr.ItemName = objDispatchDetail.ItemName
                objTr.UOM = objDispatchDetail.UOM
                objTr.FatPer = objDispatchDetail.FatPer
                objTr.SNFPer = objDispatchDetail.SNFPer
                objTr.Fat_KG = objDispatchDetail.Fat_KG
                objTr.SNF_KG = objDispatchDetail.SNF_KG
                objTr.MilkRate = objDispatchDetail.MilkRate
                objTr.MilkAmt = objDispatchDetail.MilkAmt
                objTr.PriceRate = objDispatchDetail.PriceRate
                objTr.FatRate = objDispatchDetail.FatRate
                objTr.SNFRate = objDispatchDetail.SNFRate
                objTr.FatAmount = objDispatchDetail.FatAmount
                objTr.SNFAmount = objDispatchDetail.SNFAmount
                objTr.TotalAmount = objDispatchDetail.TotalAmount
                objTr.Diff = objDispatchDetail.Diff
                objTr.TAX1 = objDispatchDetail.TAX1
                objTr.TAX1_Base_Amt = objDispatchDetail.TAX1_Base_Amt
                objTr.TAX1_Rate = objDispatchDetail.TAX1_Rate
                objTr.TAX1_Amt = objDispatchDetail.TAX1_Amt
                objTr.TAX2 = objDispatchDetail.TAX2
                objTr.TAX2_Base_Amt = objDispatchDetail.TAX2_Base_Amt
                objTr.TAX2_Rate = objDispatchDetail.TAX2_Rate
                objTr.TAX2_Amt = objDispatchDetail.TAX2_Amt
                objTr.TAX3 = objDispatchDetail.TAX3
                objTr.TAX3_Base_Amt = objDispatchDetail.TAX3_Base_Amt
                objTr.TAX3_Rate = objDispatchDetail.TAX3_Rate
                objTr.TAX3_Amt = objDispatchDetail.TAX3_Amt
                objTr.TAX4 = objDispatchDetail.TAX4
                objTr.TAX4_Base_Amt = objDispatchDetail.TAX4_Base_Amt
                objTr.TAX4_Rate = objDispatchDetail.TAX4_Rate
                objTr.TAX4_Amt = objDispatchDetail.TAX4_Amt
                objTr.TAX5 = objDispatchDetail.TAX5
                objTr.TAX5_Base_Amt = objDispatchDetail.TAX5_Base_Amt
                objTr.TAX5_Rate = objDispatchDetail.TAX5_Rate
                objTr.TAX5_Amt = objDispatchDetail.TAX5_Amt
                objTr.Total_Tax_Amt = objDispatchDetail.Total_Tax_Amt
                objTr.Item_Net_Amt = objDispatchDetail.Item_Net_Amt
                obj.arrCanSaleInvoiceDetail.Add(objTr)
            Next
        End If
        Return obj
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsCanSaleDispatch
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCanSaleDispatch
        Dim obj As ClsCanSaleDispatch = Nothing
        Dim Arr As List(Of ClsCanSaleDispatch) = Nothing
        Dim qry As String = "Select *  from TSPL_CANSALE_DISPATCH_HEAD where 2=2 "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_CANSALE_DISPATCH_HEAD.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CANSALE_DISPATCH_HEAD.Document_No = (select MIN(Document_No) from TSPL_CANSALE_DISPATCH_HEAD WHERE 1=1 " + whrclas + " and TSPL_CANSALE_DISPATCH_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_CANSALE_DISPATCH_HEAD.Document_No = (select Max(Document_No) from TSPL_CANSALE_DISPATCH_HEAD WHERE 1=1 " + whrclas + " and TSPL_CANSALE_DISPATCH_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_CANSALE_DISPATCH_HEAD.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_CANSALE_DISPATCH_HEAD.Document_No = (select Min(Document_No) from TSPL_CANSALE_DISPATCH_HEAD where Document_No>'" + strCode + "' " + whrclas + " and TSPL_CANSALE_DISPATCH_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_CANSALE_DISPATCH_HEAD.Document_No = (select Max(Document_No) from TSPL_CANSALE_DISPATCH_HEAD where Document_No<'" + strCode + "' " + whrclas + " and TSPL_CANSALE_DISPATCH_HEAD.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCanSaleDispatch()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.CanSale_Doc_No = clsCommon.myCstr(dt.Rows(0)("CanSale_Doc_No"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If

            obj.Fat_Weightage = clsCommon.myCstr(dt.Rows(0)("Fat_Weightage"))
            obj.Snf_Weightage = clsCommon.myCstr(dt.Rows(0)("Snf_Weightage"))
            obj.Fat_Ratio = clsCommon.myCstr(dt.Rows(0)("Fat_Ratio"))
            obj.Snf_Ratio = clsCommon.myCstr(dt.Rows(0)("Snf_Ratio"))
            obj.Standard_Rate = clsCommon.myCstr(dt.Rows(0)("Standard_Rate"))
            obj.TolerancePerMinus = clsCommon.myCstr(dt.Rows(0)("TolerancePerMinus"))
            obj.TolerancePerPlus = clsCommon.myCstr(dt.Rows(0)("TolerancePerPlus"))
            obj.DocumentAmount = clsCommon.myCstr(dt.Rows(0)("DocumentAmount"))
            obj.CanInventoryType = clsCommon.myCdbl(dt.Rows(0)("CanInventoryType"))
            obj.TotalNoofCans = clsCommon.myCdbl(dt.Rows(0)("TotalNoofCans"))
            obj.CanItemRate = clsCommon.myCdbl(dt.Rows(0)("CanItemRate"))
            obj.CanItemCode = clsCommon.myCstr(dt.Rows(0)("CanItemCode"))
            obj.CanItemUOM = clsCommon.myCstr(dt.Rows(0)("CanItemUOM"))
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
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
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))

            obj.arrCanSaleDispatchDetail = ClsCanSaleDispatchDetail.getData(obj.Document_No, trans)
        End If
        Return obj
    End Function



End Class
Public Class ClsCanSaleDispatchDetail
    Public Document_No As String = Nothing
    Public SL_No As Double = 0
    Public NoOfCans As Double = 0
    Public Qty As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public ItemCode As String = String.Empty
    Public ItemName As String = String.Empty
    Public UOM As String = String.Empty
    Public Fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public MilkRate As Double = 0
    Public MilkAmt As Double = 0
    Public PriceRate As Double = 0
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
    Public TotalAmount As Double = 0
    Public Diff As Double = 0
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
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Shared Function saveData(ByVal arrObj As List(Of ClsCanSaleDispatchDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As ClsCanSaleDispatchDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "SL_No", obj.SL_No)
                    clsCommon.AddColumnsForChange(coll, "NoOfCans", obj.NoOfCans)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "ItemCode", obj.ItemCode)
                    clsCommon.AddColumnsForChange(coll, "ItemName", obj.ItemName)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "MilkRate", obj.MilkRate)
                    clsCommon.AddColumnsForChange(coll, "MilkAmt", obj.MilkAmt)
                    clsCommon.AddColumnsForChange(coll, "PriceRate", obj.PriceRate)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "TotalAmount", obj.TotalAmount)
                    clsCommon.AddColumnsForChange(coll, "Diff", obj.Diff)
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
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_DISPATCH_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function


    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of ClsCanSaleDispatchDetail)
        Try
            Dim arrObj As List(Of ClsCanSaleDispatchDetail) = Nothing
            Dim obj As ClsCanSaleDispatchDetail = Nothing
            Dim qry As String = "Select  * from TSPL_CANSALE_DISPATCH_DETAIL where Document_No='" & strQCNo & "' order by SL_No "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsCanSaleDispatchDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsCanSaleDispatchDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SL_No = clsCommon.myCdbl(dt.Rows(i)("SL_No"))
                    obj.NoOfCans = clsCommon.myCdbl(dt.Rows(i)("NoOfCans"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.ItemCode = clsCommon.myCstr(dt.Rows(0)("ItemCode"))
                    obj.ItemName = clsCommon.myCstr(dt.Rows(0)("ItemName"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))
                    obj.Fat_KG = clsCommon.myCdbl(dt.Rows(i)("Fat_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.MilkRate = clsCommon.myCdbl(dt.Rows(i)("MilkRate"))
                    obj.MilkAmt = clsCommon.myCdbl(dt.Rows(i)("MilkAmt"))
                    obj.PriceRate = clsCommon.myCdbl(dt.Rows(i)("PriceRate"))
                    obj.FatRate = clsCommon.myCdbl(dt.Rows(i)("FatRate"))
                    obj.SNFRate = clsCommon.myCdbl(dt.Rows(i)("SNFRate"))
                    obj.FatAmount = clsCommon.myCdbl(dt.Rows(i)("FatAmount"))
                    obj.SNFAmount = clsCommon.myCdbl(dt.Rows(i)("SNFAmount"))
                    obj.TotalAmount = clsCommon.myCdbl(dt.Rows(i)("TotalAmount"))
                    obj.Diff = clsCommon.myCdbl(dt.Rows(i)("Diff"))
                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))

                    arrObj.Add(obj)

                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class



''---------------- CAN SALE INVOICE TABLES

Public Class ClsCanSaleInvoice
#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = String.Empty
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Price_Code As String = Nothing
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public Standard_Rate As Double = 0
    Public TolerancePerPlus As Double = 0
    Public TolerancePerMinus As Double = 0
    Public DocumentAmount As Double = 0
    Public RoundOffAmount As Double = 0
    Public CanSale_Doc_No As String = String.Empty
    Public CanSale_Dispatch_No As String = String.Empty
    Public Posted As Integer = 0
    Public Posting_Date As Date?

    Public Created_Date As Date?
    Public Modified_Date As Date?
    Public CanInventoryType As Integer = 0
    Public TotalNoofCans As Double = 0
    Public CanItemCode As String = String.Empty
    Public CanItemUOM As String = String.Empty
    Public CanItemRate As Double = 0
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
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
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public arrCanSaleInvoiceDetail As List(Of ClsCanSaleInvoiceDetail) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsCanSaleInvoice, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsCanSaleInvoice, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim ApplyTSPriceAtBulkSale As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, trans)) = 1, True, False))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanSale, obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_CANSALE_INVOICE_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CanSaleInvoice, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "CanSale_Doc_No", obj.CanSale_Doc_No)
            clsCommon.AddColumnsForChange(coll, "CanSale_Dispatch_No", obj.CanSale_Dispatch_No)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Name", obj.Location_Name)
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
            clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
            clsCommon.AddColumnsForChange(coll, "TolerancePerPlus", obj.TolerancePerPlus)
            clsCommon.AddColumnsForChange(coll, "TolerancePerMinus", obj.TolerancePerMinus)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "CanInventoryType", obj.CanInventoryType)
            clsCommon.AddColumnsForChange(coll, "TotalNoofCans", obj.TotalNoofCans)
            clsCommon.AddColumnsForChange(coll, "CanItemCode", obj.CanItemCode, True)
            clsCommon.AddColumnsForChange(coll, "CanItemUOM", obj.CanItemUOM)
            clsCommon.AddColumnsForChange(coll, "CanItemRate", obj.CanItemRate)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
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
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)

            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_INVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_CANSALE_INVOICE_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            ClsCanSaleInvoiceDetail.saveData(obj.arrCanSaleInvoiceDetail, obj.Document_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsCanSaleInvoice
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCanSaleInvoice
        Dim obj As ClsCanSaleInvoice = Nothing
        Dim Arr As List(Of ClsCanSaleInvoice) = Nothing
        Dim qry As String = "Select *  from TSPL_CANSALE_INVOICE_HEAD where 2=2 "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_CANSALE_INVOICE_HEAD.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CANSALE_INVOICE_HEAD.Document_No = (select MIN(Document_No) from TSPL_CANSALE_INVOICE_HEAD WHERE 1=1 " + whrclas + " and TSPL_CANSALE_INVOICE_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_CANSALE_INVOICE_HEAD.Document_No = (select Max(Document_No) from TSPL_CANSALE_INVOICE_HEAD WHERE 1=1 " + whrclas + " and TSPL_CANSALE_INVOICE_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_CANSALE_INVOICE_HEAD.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_CANSALE_INVOICE_HEAD.Document_No = (select Min(Document_No) from TSPL_CANSALE_INVOICE_HEAD where Document_No>'" + strCode + "' " + whrclas + " and TSPL_CANSALE_INVOICE_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_CANSALE_INVOICE_HEAD.Document_No = (select Max(Document_No) from TSPL_CANSALE_INVOICE_HEAD where Document_No<'" + strCode + "' " + whrclas + " and TSPL_CANSALE_INVOICE_HEAD.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCanSaleInvoice()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.CanSale_Doc_No = clsCommon.myCstr(dt.Rows(0)("CanSale_Doc_No"))
            obj.CanSale_Dispatch_No = clsCommon.myCstr(dt.Rows(0)("CanSale_Dispatch_No"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.CanInventoryType = clsCommon.myCdbl(dt.Rows(0)("CanInventoryType"))
            obj.TotalNoofCans = clsCommon.myCdbl(dt.Rows(0)("TotalNoofCans"))
            obj.CanItemRate = clsCommon.myCdbl(dt.Rows(0)("CanItemRate"))
            obj.CanItemCode = clsCommon.myCstr(dt.Rows(0)("CanItemCode"))
            obj.CanItemUOM = clsCommon.myCstr(dt.Rows(0)("CanItemUOM"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If

            obj.Fat_Weightage = clsCommon.myCstr(dt.Rows(0)("Fat_Weightage"))
            obj.Snf_Weightage = clsCommon.myCstr(dt.Rows(0)("Snf_Weightage"))
            obj.Fat_Ratio = clsCommon.myCstr(dt.Rows(0)("Fat_Ratio"))
            obj.Snf_Ratio = clsCommon.myCstr(dt.Rows(0)("Snf_Ratio"))
            obj.Standard_Rate = clsCommon.myCstr(dt.Rows(0)("Standard_Rate"))
            obj.TolerancePerMinus = clsCommon.myCstr(dt.Rows(0)("TolerancePerMinus"))
            obj.TolerancePerPlus = clsCommon.myCstr(dt.Rows(0)("TolerancePerPlus"))
            obj.DocumentAmount = clsCommon.myCstr(dt.Rows(0)("DocumentAmount"))
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
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
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))

            obj.arrCanSaleInvoiceDetail = ClsCanSaleInvoiceDetail.getData(obj.Document_No, trans)
        End If
        Return obj
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
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As ClsCanSaleInvoice = ClsCanSaleInvoice.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If


            createARInvoice(obj, "", "", trans)

            Dim qry = "Update TSPL_CANSALE_INVOICE_HEAD set Posted=1, " &
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " &
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function createARInvoice(ByVal obj As ClsCanSaleInvoice, ByVal strARNoForRecreate As String, ByVal strVoucherForRecreate As String, ByVal trans As SqlTransaction) As Boolean
        ''''''''''''''''''''''''''''''''''For Making AR Invoice
        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objCustInv.Document_Date = obj.Document_Date

        objCustInv.Trans_Type = "CSL"

        objCustInv.Document_Type = "I"
        objCustInv.Document_Total = obj.Total_Amt
        objCustInv.Customer_Code = obj.Customer_Code
        objCustInv.RoundOffAmount = obj.RoundOffAmount
        objCustInv.Customer_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'", trans))
        objCustInv.Posting_Date = obj.Document_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
        objCustInv.On_Hold = 0
        objCustInv.Remarks = ""
        objCustInv.Description = ""
        objCustInv.Tax_Group = obj.Tax_Group
        objCustInv.TAX1 = obj.TAX1
        objCustInv.TAX1_Rate = obj.TAX1_Rate
        objCustInv.TAX1_Amt = obj.TAX1_Amt
        objCustInv.TAX2 = obj.TAX2
        objCustInv.TAX2_Rate = obj.TAX2_Rate
        objCustInv.TAX2_Amt = obj.TAX2_Amt
        objCustInv.TAX3 = obj.TAX3
        objCustInv.TAX3_Rate = obj.TAX3_Rate
        objCustInv.TAX3_Amt = obj.TAX3_Amt
        objCustInv.TAX4 = obj.TAX4
        objCustInv.TAX4_Rate = obj.TAX4_Rate
        objCustInv.TAX4_Amt = obj.TAX4_Amt
        objCustInv.TAX5 = obj.TAX5
        objCustInv.TAX5_Rate = obj.TAX5_Rate
        objCustInv.TAX5_Amt = obj.TAX5_Amt
        objCustInv.TAX6 = ""
        objCustInv.TAX6_Rate = 0
        objCustInv.TAX6_Amt = 0
        objCustInv.TAX7 = ""
        objCustInv.TAX7_Rate = 0
        objCustInv.TAX7_Amt = 0
        objCustInv.TAX8 = ""
        objCustInv.TAX8_Rate = 0
        objCustInv.TAX8_Amt = 0
        objCustInv.TAX9 = ""
        objCustInv.TAX9_Rate = 0
        objCustInv.TAX9_Amt = 0
        objCustInv.TAX10 = ""
        objCustInv.TAX10_Rate = 0
        objCustInv.TAX10_Amt = 0
        objCustInv.Total_Tax = obj.Total_Tax_Amt
        objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
        objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
        objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
        objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
        objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
        objCustInv.Tax6_BAmount = 0
        objCustInv.Tax7_BAmount = 0
        objCustInv.Tax8_BAmount = 0
        objCustInv.Tax9_BAmount = 0
        objCustInv.Tax10_BAmount = 0
        objCustInv.Balance_Amt = obj.Total_Amt
        objCustInv.Terms_Code = ""
        objCustInv.PROJECT_ID = ""

        objCustInv.CURRENCY_CODE = ""
        objCustInv.ConvRate = 0
        objCustInv.ApplicableFrom = Nothing

        objCustInv.Discount_Percentage = 0
        objCustInv.Discount_Base = obj.DocumentAmount
        objCustInv.Discount_Amount = 0
        objCustInv.Amount_Less_Discount = obj.DocumentAmount

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))

        End If
        If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
            objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
            objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
            objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
            objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
            objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Location_Code, trans)
        End If

        objCustInv.Add_Charge_Code1 = ""
        objCustInv.Add_Charge_Name1 = ""
        objCustInv.Add_Charge_Amt1 = 0
        objCustInv.Add_Charge_Code2 = ""
        objCustInv.Add_Charge_Name2 = ""
        objCustInv.Add_Charge_Amt2 = 0
        objCustInv.Add_Charge_Code3 = ""
        objCustInv.Add_Charge_Name3 = ""
        objCustInv.Add_Charge_Amt3 = 0
        objCustInv.Add_Charge_Code4 = ""
        objCustInv.Add_Charge_Name4 = ""
        objCustInv.Add_Charge_Amt4 = 0
        objCustInv.Add_Charge_Code5 = ""
        objCustInv.Add_Charge_Name5 = ""
        objCustInv.Add_Charge_Amt5 = 0
        objCustInv.Add_Charge_Code6 = ""
        objCustInv.Add_Charge_Name6 = ""
        objCustInv.Add_Charge_Amt6 = 0
        objCustInv.Add_Charge_Code7 = ""
        objCustInv.Add_Charge_Name7 = ""
        objCustInv.Add_Charge_Amt7 = 0
        objCustInv.Add_Charge_Code8 = ""
        objCustInv.Add_Charge_Name8 = ""
        objCustInv.Add_Charge_Amt8 = 0
        objCustInv.Add_Charge_Code9 = ""
        objCustInv.Add_Charge_Name9 = ""
        objCustInv.Add_Charge_Amt9 = 0
        objCustInv.Add_Charge_Code10 = ""
        objCustInv.Add_Charge_Name10 = ""
        objCustInv.Add_Charge_Amt10 = 0
        objCustInv.Total_Add_Charge = 0
        objCustInv.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

        objCustInv.Against_Sale_No = obj.Document_No
        Dim counter As Integer = 1
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
        For Each objTr As ClsCanSaleInvoiceDetail In obj.arrCanSaleInvoiceDetail


            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            objCustInvTR.SNo = counter
            dt = clsItemMaster.GetSaleAccGLAC(objTr.ItemCode, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set sale account for item" + objTr.ItemCode)
            End If
            objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
            objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans)
            objCustInvTR.Reco_Control_Account = "S"
            objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)


            objCustInvTR.Amount = objTr.MilkAmt
            objCustInvTR.Discount_Per = 0
            objCustInvTR.Discount = 0

            objCustInvTR.Amount_less_Discount = objTr.MilkAmt
            objCustInvTR.TAX1 = objTr.TAX1
            objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
            objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
            objCustInvTR.TAX2 = objTr.TAX2
            objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
            objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
            objCustInvTR.TAX3 = objTr.TAX3
            objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
            objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
            objCustInvTR.TAX4 = objTr.TAX4
            objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
            objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
            objCustInvTR.TAX5 = objTr.TAX5
            objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
            objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
            objCustInvTR.TAX6 = ""
            objCustInvTR.TAX6_Rate = 0
            objCustInvTR.TAX6_Amt = 0
            objCustInvTR.TAX7 = ""
            objCustInvTR.TAX7_Rate = 0
            objCustInvTR.TAX7_Amt = 0
            objCustInvTR.TAX8 = ""
            objCustInvTR.TAX8_Rate = 0
            objCustInvTR.TAX8_Amt = 0
            objCustInvTR.TAX9 = ""
            objCustInvTR.TAX9_Rate = 0
            objCustInvTR.TAX9_Amt = 0
            objCustInvTR.TAX10 = ""
            objCustInvTR.TAX10_Rate = 0
            objCustInvTR.TAX10_Amt = 0
            objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
            objCustInvTR.Total_Amount = objTr.Item_Net_Amt

            objCustInvTR.Remarks = ""
            objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
            objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
            objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
            objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
            objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
            objCustInvTR.TAX6_Base_Amt = 0
            objCustInvTR.TAX7_Base_Amt = 0
            objCustInvTR.TAX8_Base_Amt = 0
            objCustInvTR.TAX9_Base_Amt = 0
            objCustInvTR.TAX10_Base_Amt = 0
            objCustInv.Arr.Add(objCustInvTR)

            counter += 1

        Next
        ''richa GKD/14/09/18-000159
        objCustInv.SaveData(objCustInv, True, trans, "CanSaleInvoice", strVoucherForRecreate, strARNoForRecreate)
        clsCustomerInvoiceHead.PostData("CanSaleInvoice", objCustInv.Document_No, "", trans, strVoucherForRecreate)
        Return True
    End Function


End Class
Public Class ClsCanSaleInvoiceDetail
    Public Document_No As String = Nothing
    Public SL_No As Double = 0
    Public NoOfCans As Double = 0
    Public Qty As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public Fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public ItemCode As String = String.Empty
    Public ItemName As String = String.Empty
    Public UOM As String = String.Empty
    Public MilkRate As Double = 0
    Public MilkAmt As Double = 0
    Public PriceRate As Double = 0
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
    Public TotalAmount As Double = 0
    Public Diff As Double = 0
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
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0

    Public Shared Function saveData(ByVal arrObj As List(Of ClsCanSaleInvoiceDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As ClsCanSaleInvoiceDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "SL_No", obj.SL_No)
                    clsCommon.AddColumnsForChange(coll, "NoOfCans", obj.NoOfCans)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "ItemCode", obj.ItemCode)
                    clsCommon.AddColumnsForChange(coll, "ItemName", obj.ItemName)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "MilkRate", obj.MilkRate)
                    clsCommon.AddColumnsForChange(coll, "MilkAmt", obj.MilkAmt)
                    clsCommon.AddColumnsForChange(coll, "PriceRate", obj.PriceRate)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "TotalAmount", obj.TotalAmount)
                    clsCommon.AddColumnsForChange(coll, "Diff", obj.Diff)
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
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CANSALE_INVOICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of ClsCanSaleInvoiceDetail)
        Try
            Dim arrObj As List(Of ClsCanSaleInvoiceDetail) = Nothing
            Dim obj As ClsCanSaleInvoiceDetail = Nothing
            Dim qry As String = "Select * from TSPL_CANSALE_INVOICE_DETAIL where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsCanSaleInvoiceDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsCanSaleInvoiceDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SL_No = clsCommon.myCdbl(dt.Rows(i)("SL_No"))
                    obj.NoOfCans = clsCommon.myCdbl(dt.Rows(i)("NoOfCans"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.ItemCode = clsCommon.myCstr(dt.Rows(0)("ItemCode"))
                    obj.ItemName = clsCommon.myCstr(dt.Rows(0)("ItemName"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))
                    obj.Fat_KG = clsCommon.myCdbl(dt.Rows(i)("Fat_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.MilkRate = clsCommon.myCdbl(dt.Rows(i)("MilkRate"))
                    obj.MilkAmt = clsCommon.myCdbl(dt.Rows(i)("MilkAmt"))
                    obj.PriceRate = clsCommon.myCdbl(dt.Rows(i)("PriceRate"))
                    obj.FatRate = clsCommon.myCdbl(dt.Rows(i)("FatRate"))
                    obj.SNFRate = clsCommon.myCdbl(dt.Rows(i)("SNFRate"))
                    obj.FatAmount = clsCommon.myCdbl(dt.Rows(i)("FatAmount"))
                    obj.SNFAmount = clsCommon.myCdbl(dt.Rows(i)("SNFAmount"))
                    obj.TotalAmount = clsCommon.myCdbl(dt.Rows(i)("TotalAmount"))
                    obj.Diff = clsCommon.myCdbl(dt.Rows(i)("Diff"))
                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))

                    arrObj.Add(obj)

                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

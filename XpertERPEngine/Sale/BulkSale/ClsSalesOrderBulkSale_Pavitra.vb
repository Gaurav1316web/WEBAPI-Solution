'--------Created By Richa 21/08/2015 Against Ticket No BM00000005377
Imports System.Data.SqlClient
Imports common

Public Class ClsSalesOrderBulkSale_Pavitra
#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date? = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public TERMS_Code As String = Nothing
    Public PO_NO As String = Nothing
    Public PO_Date As Date? = Nothing
    Public Price_Code As String = Nothing
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public Standard_Rate As Double = 0
    Public TolerancePerPlus As Double = 0
    Public TolerancePerMinus As Double = 0
    Public Posted As Integer
    Public Posting_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
    Public DocumentAmount As Double = 0
    Public close_yn As String = Nothing
    Public close_remarks As String = Nothing
    Public arrSalesOrderDetailBulkSale As List(Of clsSalesOrderDetailBulkSale_Pavitra) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "Select TSPL_SALES_ORDER_MASTER_BulkSale.Document_No As Code ,TSPL_SALES_ORDER_MASTER_BulkSale.Document_Date as Date from TSPL_SALES_ORDER_MASTER_BulkSale "
        Return clsCommon.ShowSelectForm("SalesOrderBulkSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function
    Public Shared Function SaveData(ByVal obj As ClsSalesOrderBulkSale_Pavitra, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = String.Empty
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmSalesOrderBS, obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_SALES_ORDER_DETAIL_BulkSale where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SalesOrderBulkSale, "", obj.Location_Code)
            End If
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Name", obj.Location_Name)
            clsCommon.AddColumnsForChange(coll, "TERMS_Code", obj.TERMS_Code)
            clsCommon.AddColumnsForChange(coll, "PO_NO", obj.PO_NO)
            clsCommon.AddColumnsForChange(coll, "PO_Date", clsCommon.GetPrintDate(obj.PO_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code, True)
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
            clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
            clsCommon.AddColumnsForChange(coll, "TolerancePerPlus", obj.TolerancePerPlus)
            clsCommon.AddColumnsForChange(coll, "TolerancePerMinus", obj.TolerancePerMinus)
            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "close_yn", obj.close_yn)
            clsCommon.AddColumnsForChange(coll, "close_remarks", obj.close_remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ORDER_MASTER_BulkSale", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ORDER_MASTER_BulkSale", OMInsertOrUpdate.Update, "TSPL_SALES_ORDER_MASTER_BulkSale.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsSalesOrderDetailBulkSale_Pavitra.saveData(obj.arrSalesOrderDetailBulkSale, obj.Document_No, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsSalesOrderBulkSale_Pavitra
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsSalesOrderBulkSale_Pavitra
        Dim obj As ClsSalesOrderBulkSale_Pavitra = Nothing
        Dim Arr As List(Of ClsSalesOrderBulkSale_Pavitra) = Nothing
        Dim qry As String = "Select TSPL_SALES_ORDER_MASTER_BulkSale.* from TSPL_SALES_ORDER_MASTER_BulkSale where 2=2 "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_SALES_ORDER_MASTER_BulkSale.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALES_ORDER_MASTER_BulkSale.Document_No = (select MIN(Document_No) from TSPL_SALES_ORDER_MASTER_BulkSale WHERE 1=1 " + whrclas + " and TSPL_SALES_ORDER_MASTER_BulkSale.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_SALES_ORDER_MASTER_BulkSale.Document_No = (select Max(Document_No) from TSPL_SALES_ORDER_MASTER_BulkSale WHERE 1=1 " + whrclas + " and TSPL_SALES_ORDER_MASTER_BulkSale.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_SALES_ORDER_MASTER_BulkSale.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_SALES_ORDER_MASTER_BulkSale.Document_No = (select Min(Document_No) from TSPL_SALES_ORDER_MASTER_BulkSale where Document_No>'" + strCode + "' " + whrclas + " and TSPL_SALES_ORDER_MASTER_BulkSale.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_SALES_ORDER_MASTER_BulkSale.Document_No = (select Max(Document_No) from TSPL_SALES_ORDER_MASTER_BulkSale where Document_No<'" + strCode + "' " + whrclas + " and TSPL_SALES_ORDER_MASTER_BulkSale.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSalesOrderBulkSale_Pavitra()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.TERMS_Code = clsCommon.myCstr(dt.Rows(0)("TERMS_Code"))
            obj.PO_NO = clsCommon.myCstr(dt.Rows(0)("PO_NO"))
            obj.PO_Date = clsCommon.myCDate(dt.Rows(0)("PO_Date"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Fat_Weightage = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
            obj.Snf_Weightage = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
            obj.Fat_Ratio = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
            obj.Snf_Ratio = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            obj.Standard_Rate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            obj.TolerancePerPlus = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
            obj.TolerancePerMinus = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.close_yn = clsCommon.myCstr(dt.Rows(0)("close_yn"))
            obj.arrSalesOrderDetailBulkSale = clsSalesOrderDetailBulkSale_Pavitra.getData(obj.Document_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function closepodata(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal arrLoc As String, ByVal strRemarks As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Sale Order No not found to Close")
            End If

            Dim strClosedDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As ClsSalesOrderBulkSale_Pavitra = ClsSalesOrderBulkSale_Pavitra.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Close")
            End If

            obj.close_remarks = strRemarks
            Dim qry As String = "Update TSPL_SALES_ORDER_MASTER_BulkSale set close_yn='Y',close_remarks='" + strRemarks + "',Closed_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Closed_Date='" + strClosedDate + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_SALES_ORDER_MASTER_BulkSale.Location_Code,TSPL_SALES_ORDER_MASTER_BulkSale.Document_Date from TSPL_SALES_ORDER_MASTER_BulkSale
       where Document_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmSalesOrderBS, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_SALES_ORDER_DETAIL_BulkSale where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SALES_ORDER_MASTER_BulkSale where Document_No='" + strDocNo + "'"
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
                Throw New Exception("SO No not found to Post")
            End If
            Dim obj As ClsSalesOrderBulkSale_Pavitra = ClsSalesOrderBulkSale_Pavitra.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmSalesOrderBS, obj.Location_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_SALES_ORDER_MASTER_BULKSALE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsSalesOrderDetailBulkSale_Pavitra
    Public Document_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Unit_code As String = Nothing
    Public Price_Code As String = Nothing
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public Standard_Rate As Double = 0
    Public TolerancePerPlus As Double = 0
    Public TolerancePerMinus As Double = 0
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public Fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
    Public StandardMilkRate As Double = 0
    Public ActualMilkRate As Double = 0
    Public Amount As Double = 0
    Public Shared Function saveData(ByVal arrObj As List(Of clsSalesOrderDetailBulkSale_Pavitra), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsSalesOrderDetailBulkSale_Pavitra In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Name", obj.Item_Name)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                    clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                    clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
                    clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
                    clsCommon.AddColumnsForChange(coll, "TolerancePerPlus", obj.TolerancePerPlus)
                    clsCommon.AddColumnsForChange(coll, "TolerancePerMinus", obj.TolerancePerMinus)
                    clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "StandardMilkRate", obj.StandardMilkRate)
                    clsCommon.AddColumnsForChange(coll, "ActualMilkRate", obj.ActualMilkRate)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ORDER_DETAIL_BulkSale", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
   
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsSalesOrderDetailBulkSale_Pavitra)
        Try
            Dim arrObj As List(Of clsSalesOrderDetailBulkSale_Pavitra) = Nothing
            Dim obj As clsSalesOrderDetailBulkSale_Pavitra = Nothing
            Dim qry As String = "Select * from TSPL_SALES_ORDER_DETAIL_BulkSale where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsSalesOrderDetailBulkSale_Pavitra)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsSalesOrderDetailBulkSale_Pavitra()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dt.Rows(i)("Item_Name"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    obj.Price_Code = clsCommon.myCstr(dt.Rows(i)("Price_Code"))
                    obj.Fat_Weightage = clsCommon.myCdbl(dt.Rows(i)("Fat_Weightage"))
                    obj.Snf_Weightage = clsCommon.myCdbl(dt.Rows(i)("Snf_Weightage"))
                    obj.Fat_Ratio = clsCommon.myCdbl(dt.Rows(i)("Fat_Ratio"))
                    obj.Snf_Ratio = clsCommon.myCdbl(dt.Rows(i)("Snf_Ratio"))
                    obj.Standard_Rate = clsCommon.myCdbl(dt.Rows(i)("Standard_Rate"))
                    obj.TolerancePerPlus = clsCommon.myCdbl(dt.Rows(i)("TolerancePerPlus"))
                    obj.TolerancePerMinus = clsCommon.myCdbl(dt.Rows(i)("TolerancePerMinus"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(i)("Rate"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))
                    obj.FatRate = clsCommon.myCdbl(dt.Rows(i)("FatRate"))
                    obj.SNFRate = clsCommon.myCdbl(dt.Rows(i)("SNFRate"))
                    obj.Fat_KG = clsCommon.myCdbl(dt.Rows(i)("Fat_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.FatAmount = clsCommon.myCdbl(dt.Rows(i)("FatAmount"))
                    obj.SNFAmount = clsCommon.myCdbl(dt.Rows(i)("SNFAmount"))
                    obj.StandardMilkRate = clsCommon.myCdbl(dt.Rows(i)("StandardMilkRate"))
                    obj.ActualMilkRate = clsCommon.myCdbl(dt.Rows(i)("ActualMilkRate"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

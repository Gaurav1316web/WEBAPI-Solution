Imports System.Data.SqlClient

Public Class clsDailyMilkProducts
#Region "Variables"
    Public Document_No As String = Nothing
    Public Report_Date As DateTime = Nothing
    Public Reporting_Date As DateTime = Nothing
    Public Status As Integer = 0
    Public MILK_PROCUREMENT As Decimal = 0
    Public MILK_RECEIPT As Decimal = 0
    Public LOCAL_MILK As Decimal = 0
    Public SUPPLIEST_NMG As Decimal = 0
    Public SUPPLIEST_RMG As Decimal = 0
    Public MILK_ISSUED As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public MILK_RECEIVED As Decimal = 0
    Public MILK_DISPATCH As Decimal = 0
    Public OWN_MILK As Decimal = 0
    Public RCDF_UNIT_FIRST As Decimal = 0
    Public RCDF_UNIT_SECOND As Decimal = 0
    Public RCDF_UNIT_THIRD As Decimal = 0
    Public RCDF_UNIT_FOURTH As Decimal = 0
    Public RCDF_UNIT_FIFTH As Decimal = 0
    Public RCDF_NAME_FIRST As String = Nothing
    Public RCDF_NAME_SECOND As String = Nothing
    Public RCDF_NAME_THIRD As String = Nothing
    Public RCDF_NAME_FOURTH As String = Nothing
    Public RCDF_NAME_FIFTH As String = Nothing
    Public GHEE_PURCHASE As Decimal = 0
    Public GHEE_RECEIPT As Decimal = 0
    Public SMP_PURCHASE As Decimal = 0
    Public SMP_RECEIPT As Decimal = 0
    Public TABLE_BUTTER As Decimal = 0
    Public Remarks As String = Nothing
    Public Arr As List(Of clsDailyMilkProductsDetails) = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal docuno As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Document_No as [Document],MILK_PROCUREMENT as [ MILK PROCUREMENT],MILK_RECEIPT as [MILK RECEIPT], FAT,SNF,Reporting_Date as [Reporting Date], Report_Date as [Report Date] from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD "
        str = clsCommon.ShowSelectForm("ITEMGPFND", qry, "Document", whrcls, docuno, "Document", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal obj As clsDailyMilkProducts, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsDailyMilkProducts, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_MIS_DAILY_MILK_PRODUCT_DETAIL where Document_No ='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where Document_No='" + obj.Document_No + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Reporting_Date), clsDocType.frmDailyMilkProducts, "", "")
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim colm As New Hashtable

            clsCommon.AddColumnsForChange(colm, "Report_Date", clsCommon.GetPrintDate(obj.Report_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(colm, "Reporting_Date", clsCommon.GetPrintDate(obj.Reporting_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(colm, "MILK_PROCUREMENT", obj.MILK_PROCUREMENT)
            clsCommon.AddColumnsForChange(colm, "MILK_RECEIPT", obj.MILK_RECEIPT)
            clsCommon.AddColumnsForChange(colm, "LOCAL_MILK", obj.LOCAL_MILK)
            clsCommon.AddColumnsForChange(colm, "SUPPLIEST_NMG", obj.SUPPLIEST_NMG)
            clsCommon.AddColumnsForChange(colm, "SUPPLIEST_RMG", obj.SUPPLIEST_RMG)
            clsCommon.AddColumnsForChange(colm, "MILK_ISSUED", obj.MILK_ISSUED)
            clsCommon.AddColumnsForChange(colm, "FAT", obj.FAT)
            clsCommon.AddColumnsForChange(colm, "SNF", obj.SNF)
            clsCommon.AddColumnsForChange(colm, "MILK_RECEIVED", obj.MILK_RECEIVED)
            clsCommon.AddColumnsForChange(colm, "MILK_DISPATCH", obj.MILK_DISPATCH)
            clsCommon.AddColumnsForChange(colm, "OWN_MILK", obj.OWN_MILK)
            clsCommon.AddColumnsForChange(colm, "RCDF_UNIT_FIRST", obj.RCDF_UNIT_FIRST)
            clsCommon.AddColumnsForChange(colm, "RCDF_UNIT_SECOND", obj.RCDF_UNIT_SECOND)
            clsCommon.AddColumnsForChange(colm, "RCDF_UNIT_THIRD", obj.RCDF_UNIT_THIRD)
            clsCommon.AddColumnsForChange(colm, "RCDF_UNIT_FOURTH", obj.RCDF_UNIT_FOURTH)
            clsCommon.AddColumnsForChange(colm, "RCDF_UNIT_FIFTH", obj.RCDF_UNIT_FIFTH)
            clsCommon.AddColumnsForChange(colm, "RCDF_NAME_FIRST", obj.RCDF_NAME_FIRST)
            clsCommon.AddColumnsForChange(colm, "RCDF_NAME_SECOND", obj.RCDF_NAME_SECOND)
            clsCommon.AddColumnsForChange(colm, "RCDF_NAME_THIRD", obj.RCDF_NAME_THIRD)
            clsCommon.AddColumnsForChange(colm, "RCDF_NAME_FOURTH", obj.RCDF_NAME_FOURTH)
            clsCommon.AddColumnsForChange(colm, "RCDF_NAME_FIFTH", obj.RCDF_NAME_FIFTH)
            clsCommon.AddColumnsForChange(colm, "GHEE_PURCHASE", obj.GHEE_PURCHASE)
            clsCommon.AddColumnsForChange(colm, "GHEE_RECEIPT", obj.GHEE_RECEIPT)
            clsCommon.AddColumnsForChange(colm, "SMP_PURCHASE", obj.SMP_PURCHASE)
            clsCommon.AddColumnsForChange(colm, "SMP_RECEIPT", obj.SMP_RECEIPT)
            clsCommon.AddColumnsForChange(colm, "TABLE_BUTTER", obj.TABLE_BUTTER)
            clsCommon.AddColumnsForChange(colm, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(colm, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(colm, "Modify_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(colm, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(colm, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(colm, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_MILK_PRODUCT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_MILK_PRODUCT_HEAD", OMInsertOrUpdate.Update, "TSPL_MIS_DAILY_MILK_PRODUCT_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsDailyMilkProductsDetails.SavaData(obj.Document_No, obj.Arr, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved

    End Function

    Public Shared Function DocNO_Navigation(ByVal NavType As NavigatorType, ByVal docNo As String)
        Dim sql As String = "select Document_No from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
            Case NavigatorType.Next
                sql += "and Document_No in (select min(Document_No) from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where Document_No>'" + docNo + "')"
            Case NavigatorType.First
                sql += "and Document_No in (select MIN(Document_No) from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD)"
            Case NavigatorType.Last
                sql += "and Document_No in (select MAX(Document_No) from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD)"
            Case NavigatorType.Previous
                sql += "and Document_No in (select max(Document_No) from TSPL_PO where TSPL_MIS_DAILY_MILK_PRODUCT_HEAD<'" + docNo + "')"
        End Select
        Return clsDBFuncationality.getSingleValue(sql)
    End Function

    Public Shared Function GetData(ByVal docno As String, ByVal navtype As NavigatorType) As clsDailyMilkProducts
        Dim obj As clsDailyMilkProducts = Nothing
        Dim qry As String = "SELECT * from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where Document_No=  '" + docno + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDailyMilkProducts()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Report_Date = clsCommon.GetPrintDate(dt.Rows(0)("Reporting_Date"), "dd/MMM/yyyy")
            obj.Reporting_Date = clsCommon.GetPrintDate(dt.Rows(0)("Report_Date"), "dd/MMM/yyyy")
            obj.MILK_RECEIPT = clsCommon.myCstr(dt.Rows(0)("MILK_RECEIPT"))
            obj.MILK_PROCUREMENT = clsCommon.myCstr(dt.Rows(0)("MILK_PROCUREMENT"))
            obj.MILK_RECEIPT = clsCommon.myCstr(dt.Rows(0)("MILK_RECEIPT"))
            obj.LOCAL_MILK = clsCommon.myCstr(dt.Rows(0)("LOCAL_MILK"))
            obj.SUPPLIEST_NMG = clsCommon.myCstr(dt.Rows(0)("SUPPLIEST_NMG"))
            obj.SUPPLIEST_RMG = clsCommon.myCstr(dt.Rows(0)("SUPPLIEST_RMG"))
            obj.SNF = clsCommon.myCstr(dt.Rows(0)("SNF"))
            obj.FAT = clsCommon.myCstr(dt.Rows(0)("FAT"))
            obj.MILK_RECEIVED = clsCommon.myCstr(dt.Rows(0)("MILK_RECEIVED"))
            obj.MILK_DISPATCH = clsCommon.myCstr(dt.Rows(0)("MILK_DISPATCH"))
            obj.OWN_MILK = clsCommon.myCstr(dt.Rows(0)("OWN_MILK"))
            obj.RCDF_UNIT_FIRST = clsCommon.myCstr(dt.Rows(0)("RCDF_UNIT_FIRST"))
            obj.RCDF_UNIT_SECOND = clsCommon.myCstr(dt.Rows(0)("RCDF_UNIT_SECOND"))
            obj.RCDF_UNIT_THIRD = clsCommon.myCstr(dt.Rows(0)("RCDF_UNIT_THIRD"))
            obj.RCDF_UNIT_FOURTH = clsCommon.myCstr(dt.Rows(0)("RCDF_UNIT_FOURTH"))
            obj.RCDF_UNIT_FIFTH = clsCommon.myCstr(dt.Rows(0)("RCDF_UNIT_FIFTH"))
            obj.RCDF_NAME_FIRST = clsCommon.myCstr(dt.Rows(0)("RCDF_NAME_FIRST"))
            obj.RCDF_NAME_SECOND = clsCommon.myCstr(dt.Rows(0)("RCDF_NAME_SECOND"))
            obj.RCDF_NAME_THIRD = clsCommon.myCstr(dt.Rows(0)("RCDF_NAME_THIRD"))
            obj.RCDF_NAME_FOURTH = clsCommon.myCstr(dt.Rows(0)("RCDF_NAME_FOURTH"))
            obj.RCDF_NAME_FIFTH = clsCommon.myCstr(dt.Rows(0)("RCDF_NAME_FIFTH"))
            obj.GHEE_PURCHASE = clsCommon.myCstr(dt.Rows(0)("GHEE_PURCHASE"))
            obj.GHEE_RECEIPT = clsCommon.myCstr(dt.Rows(0)("GHEE_RECEIPT"))
            obj.SMP_PURCHASE = clsCommon.myCstr(dt.Rows(0)("SMP_PURCHASE"))
            obj.SMP_RECEIPT = clsCommon.myCstr(dt.Rows(0)("SMP_RECEIPT"))
            obj.TABLE_BUTTER = clsCommon.myCstr(dt.Rows(0)("TABLE_BUTTER"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))

            qry = "SELECT * from TSPL_MIS_DAILY_MILK_PRODUCT_DETAIL where Document_No=  '" + docno + "'"
            dt = New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDailyMilkProductsDetails)
                Dim objtr As clsDailyMilkProductsDetails
                For Each dr As DataRow In dt.Rows
                    objtr = New clsDailyMilkProductsDetails
                    objtr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objtr.Products = clsCommon.myCstr(dr("Item_Name"))
                    objtr.Item_UOM = clsCommon.myCstr(dr("Item_UOM"))
                    objtr.Production = clsCommon.myCstr(dr("Production"))
                    objtr.Total = clsCommon.myCstr(dr("Total"))
                    objtr.Inter_Unit = clsCommon.myCstr(dr("Inter_Unit"))
                    objtr.Own_Stock = clsCommon.myCstr(dr("Own_Stock"))
                    objtr.Stock_At_Other_Units = clsCommon.myCstr(dr("Stock_At_Other_Units"))
                    objtr.Stock_Of_Other_Units = clsCommon.myCstr(dr("Stock_Of_Other_Units"))
                    objtr.Reconstitution = clsCommon.myCstr(dr("Reconstitution"))
                    objtr.Out_State_Ghee_Sale = clsCommon.myCstr(dr("Out_State_Ghee_Sale"))

                    obj.Arr.Add(objtr)
                Next

            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal docno As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(docno, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal docno As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(docno) <= 0) Then
            Throw New Exception("Document no not found for delete")
        End If
        Dim qry As String = " "

        qry = "delete from TSPL_MIS_DAILY_MILK_PRODUCT_DETAIL where Document_No='" + docno + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "delete from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where Document_No='" + docno + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = "Update TSPL_MIS_DAILY_MILK_PRODUCT_HEAD set Status=1,Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsDailyMilkProductsDetails
    Public Document_No As String = Nothing
    Public Products As String = Nothing
    Public Item_UOM As String = Nothing
    Public Production As Decimal = 0
    Public Total As Decimal = 0
    Public Inter_Unit As Decimal = 0
    Public Own_Stock As Decimal = 0
    Public Stock_At_Other_Units As Decimal = 0
    Public Stock_Of_Other_Units As Decimal = 0
    Public Reconstitution As Decimal = 0
    Public Out_State_Ghee_Sale As Decimal = 0


    Public Shared Function SavaData(ByVal DocNo As String, ByVal Array As List(Of clsDailyMilkProductsDetails), ByVal trans As SqlTransaction) As Boolean
        If (Array IsNot Nothing AndAlso Array.Count > 0) Then
            For Each obj As clsDailyMilkProductsDetails In Array
                Dim colm As New Hashtable
                clsCommon.AddColumnsForChange(colm, "Document_No", DocNo)
                clsCommon.AddColumnsForChange(colm, "Item_Name", obj.Products)
                clsCommon.AddColumnsForChange(colm, "Item_UOM", obj.Item_UOM)
                clsCommon.AddColumnsForChange(colm, "Production", obj.Production)
                clsCommon.AddColumnsForChange(colm, "Total", obj.Total)
                clsCommon.AddColumnsForChange(colm, "Inter_Unit", obj.Inter_Unit)
                clsCommon.AddColumnsForChange(colm, "Own_Stock", obj.Own_Stock)
                clsCommon.AddColumnsForChange(colm, "Stock_At_Other_Units", obj.Stock_At_Other_Units)
                clsCommon.AddColumnsForChange(colm, "Stock_Of_Other_Units", obj.Stock_Of_Other_Units)
                clsCommon.AddColumnsForChange(colm, "Reconstitution", obj.Reconstitution)
                clsCommon.AddColumnsForChange(colm, "Out_State_Ghee_Sale", obj.Out_State_Ghee_Sale)
                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_MIS_DAILY_MILK_PRODUCT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class

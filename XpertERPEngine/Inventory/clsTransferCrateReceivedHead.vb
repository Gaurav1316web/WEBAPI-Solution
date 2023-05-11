Imports common
Imports System.Data.SqlClient
Public Class clsTransferCrateReceivedHead
#Region "Variable"
    Public Vehicle_Code As String = Nothing
    Public Document_No As String = Nothing
    Public Tagged_DocNo As String = Nothing
    Public Document_Date As String = Nothing
    Public Trans_Date As String = Nothing
    Public Location_Code As String = Nothing
    Public Comments As String = Nothing
    Public Type As String = Nothing
    Public Posted As Integer
    Public Posting_Date As String = Nothing
    Public Arr As List(Of clsTransferCrateReceivedDetail) = Nothing

    Dim qry As String

#End Region
    Public Function SaveData(ByVal obj As clsTransferCrateReceivedHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsTransferCrateReceivedHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Transfer", "Transfer Crate Received", obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)
        Dim isSaved As Boolean = True

        qry = "delete from TSPL_CRATE_RECEIVED_DETAIL_TRANSFER where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""
        If isNewEntry Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmCreateReceivedTransfer, "", obj.Location_Code)
        End If

        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Dim coll As New Hashtable()

        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Trans_Date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
        clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
        clsCommon.AddColumnsForChange(coll, "Tagged_DocNo", obj.Tagged_DocNo)

        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRATE_RECEIVED_Head_TRANSFER", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRATE_RECEIVED_Head_TRANSFER", OMInsertOrUpdate.Update, "TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No='" + obj.Document_No + "'", trans)
        End If

        isSaved = isSaved AndAlso clsTransferCrateReceivedDetail.SaveData(obj.Document_No, Arr, trans)
        Return isSaved

    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsTransferCrateReceivedHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTransferCrateReceivedHead
        Dim obj As clsTransferCrateReceivedHead = Nothing
        Dim qry = "select TSPL_CRATE_RECEIVED_Head_TRANSFER.type,TSPL_CRATE_RECEIVED_Head_TRANSFER.Trans_Date,TSPL_CRATE_RECEIVED_Head_TRANSFER.Vehicle_Code,TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No,TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_Date, " & _
        "TSPL_CRATE_RECEIVED_Head_TRANSFER.Location_Code,TSPL_CRATE_RECEIVED_Head_TRANSFER.Posted,TSPL_CRATE_RECEIVED_Head_TRANSFER.Posting_Date, " & _
        "TSPL_CRATE_RECEIVED_Head_TRANSFER.Comments,TSPL_CRATE_RECEIVED_Head_TRANSFER.Comp_Code,TSPL_CRATE_RECEIVED_Head_TRANSFER.Created_By, " & _
        "TSPL_CRATE_RECEIVED_Head_TRANSFER.Created_Date,TSPL_CRATE_RECEIVED_Head_TRANSFER.Modified_By,TSPL_CRATE_RECEIVED_Head_TRANSFER.Modified_Date " & _
        "From TSPL_CRATE_RECEIVED_Head_TRANSFER where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No = (select MIN(Document_No) from TSPL_CRATE_RECEIVED_Head_TRANSFER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No = (select Max(Document_No) from TSPL_CRATE_RECEIVED_Head_TRANSFER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No = (select Min(Document_No) from TSPL_CRATE_RECEIVED_Head_TRANSFER where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CRATE_RECEIVED_Head_TRANSFER.Document_No = (select Max(Document_No) from TSPL_CRATE_RECEIVED_Head_TRANSFER where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTransferCrateReceivedHead()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Trans_Date = clsCommon.myCDate(dt.Rows(0)("Trans_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))

            qry = "select TSPL_CRATE_RECEIVED_Detail_TRANSFER.CrateQtyManual ,TSPL_CRATE_RECEIVED_Detail_TRANSFER.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_Detail_TRANSFER.BoxQtyRecd ,TSPL_CRATE_RECEIVED_Detail_TRANSFER.jaaliOutQty ,TSPL_CRATE_RECEIVED_Detail_TRANSFER.boxOutQty ,TSPL_CRATE_RECEIVED_Detail_TRANSFER.jaaliAdjustment ,TSPL_CRATE_RECEIVED_Detail_TRANSFER.boxAdjustment, TSPL_CRATE_RECEIVED_Detail_TRANSFER.jaali,TSPL_CRATE_RECEIVED_Detail_TRANSFER.box,TSPL_CRATE_RECEIVED_Detail_TRANSFER.OutQty,TSPL_CRATE_RECEIVED_Detail_TRANSFER.Adjustment,TSPL_CRATE_RECEIVED_Detail_TRANSFER.CrateQty,TSPL_CRATE_RECEIVED_Detail_TRANSFER.Balance,TSPL_CRATE_RECEIVED_Detail_TRANSFER.Document_No,TSPL_CRATE_RECEIVED_Detail_TRANSFER.Line_No, " & _
            "TSPL_CRATE_RECEIVED_Detail_TRANSFER.Branch_Code, " & _
            " TSPL_CRATE_RECEIVED_Detail_TRANSFER.Vehicle_Code, " & _
            "TSPL_CRATE_RECEIVED_Detail_TRANSFER.VehicleNo,TSPL_CRATE_RECEIVED_Detail_TRANSFER.CrateQtyRecd, " & _
            "TSPL_CRATE_RECEIVED_Detail_TRANSFER.Remarks From TSPL_CRATE_RECEIVED_Detail_TRANSFER where Document_No='" & obj.Document_No & "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsTransferCrateReceivedDetail)
                Dim objTr As clsTransferCrateReceivedDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsTransferCrateReceivedDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Branch_Code = clsCommon.myCstr(dr("Branch_Code"))
                    'objTr.Sale_Invoice_No = clsCommon.myCstr(dr("Sale_Invoice_No"))
                    'objTr.Sale_Invoice_Date = clsCommon.myCDate(dr("Sale_Invoice_Date"))
                    'objTr.Salesman_Code = clsCommon.myCstr(dr("Salesman_Code"))
                    'objTr.Salesman_Name = clsCommon.myCstr(dr("Salesman_Name"))
                    objTr.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_Code"))
                    objTr.VehicleNo = clsCommon.myCstr(dr("VehicleNo"))
                    objTr.CrateQty = clsCommon.myCdbl(dr("CrateQty"))
                    objTr.CrateQtyRecd = clsCommon.myCdbl(dr("CrateQtyRecd"))
                    objTr.Balance = clsCommon.myCdbl(dr("Balance"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.OutQty = clsCommon.myCdbl(dr("OutQty"))
                    objTr.Adjustment = clsCommon.myCdbl(dr("Adjustment"))
                    objTr.Jaali = clsCommon.myCdbl(dr("jaali"))
                    objTr.Box = clsCommon.myCdbl(dr("box"))

                    objTr.CrateQtyManual = clsCommon.myCdbl(dr("CrateQtyManual"))
                    objTr.JaaliQtyRecd = clsCommon.myCdbl(dr("JaaliQtyRecd"))
                    objTr.BoxQtyRecd = clsCommon.myCdbl(dr("BoxQtyRecd"))
                    objTr.jaaliOutQty = clsCommon.myCdbl(dr("jaaliOutQty"))
                    objTr.boxOutQty = clsCommon.myCdbl(dr("boxOutQty"))
                    objTr.jaaliAdjustment = clsCommon.myCdbl(dr("jaaliAdjustment"))
                    objTr.boxAdjustment = clsCommon.myCdbl(dr("boxAdjustment"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsTransferCrateReceivedHead = clsTransferCrateReceivedHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try

                Dim qry = "delete from TSPL_CRATE_RECEIVED_Detail_TRANSFER where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_CRATE_RECEIVED_Head_TRANSFER where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

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
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsTransferCrateReceivedHead = clsTransferCrateReceivedHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If


            Dim qry = "Update TSPL_CRATE_RECEIVED_Head_TRANSFER set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
            "Modified_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsTransferCrateReceivedDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Sale_Invoice_Date As String = Nothing
    Public Branch_Code As String = Nothing
    Public Sale_Invoice_No As String = Nothing
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public VehicleNo As String = Nothing
    Public CrateQtyRecd As Double = 0
    Public CrateQty As Double = 0
    Public Balance As Double = 0

    Public OutQty As Double = 0
    Public Adjustment As Double = 0
    Public Remarks As String = Nothing
    '===============Added by preeti gupta==============
    Public Jaali As Double = 0
    Public Box As Double = 0

    Public CrateQtyManual As Double = 0
    Public JaaliQtyRecd As Double = 0
    Public BoxQtyRecd As Double = 0
    Public jaaliOutQty As Double = 0
    Public boxOutQty As Double = 0
    Public jaaliAdjustment As Double = 0
    Public boxAdjustment As Double = 0


#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTransferCrateReceivedDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTransferCrateReceivedDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
                'clsCommon.AddColumnsForChange(coll, "Sale_Invoice_No", obj.Sale_Invoice_No, True)
                'clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", clsCommon.GetPrintDate(obj.Sale_Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
                'clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
                'clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
                clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
                clsCommon.AddColumnsForChange(coll, "CrateQtyRecd", obj.CrateQtyRecd)
                clsCommon.AddColumnsForChange(coll, "Balance", obj.Balance)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "OutQty", obj.OutQty)
                clsCommon.AddColumnsForChange(coll, "Adjustment", obj.Adjustment)
                clsCommon.AddColumnsForChange(coll, "jaali", obj.Jaali)
                clsCommon.AddColumnsForChange(coll, "box", obj.Box)

                clsCommon.AddColumnsForChange(coll, "CrateQtyManual", obj.CrateQtyManual)
                clsCommon.AddColumnsForChange(coll, "JaaliQtyRecd", obj.JaaliQtyRecd)
                clsCommon.AddColumnsForChange(coll, "BoxQtyRecd", obj.BoxQtyRecd)
                clsCommon.AddColumnsForChange(coll, "jaaliOutQty", obj.jaaliOutQty)
                clsCommon.AddColumnsForChange(coll, "boxOutQty", obj.boxOutQty)
                clsCommon.AddColumnsForChange(coll, "jaaliAdjustment", obj.jaaliAdjustment)
                clsCommon.AddColumnsForChange(coll, "boxAdjustment", obj.boxAdjustment)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRATE_RECEIVED_Detail_TRANSFER", OMInsertOrUpdate.Insert, "", trans)

            Next

        End If
        Return True
    End Function

End Class




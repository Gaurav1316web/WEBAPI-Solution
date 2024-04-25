Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class clsItemConversion
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Conv_Type As String = Nothing
    Public Description As String = Nothing
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime = Nothing
    Dim Created_By As String = Nothing
    Dim Created_Date As DateTime = Nothing
    Dim Modify_By As String = Nothing
    Dim Modify_Date As DateTime = Nothing
    Dim Comp_Code As String = Nothing
    Public UOMMO As String = Nothing
    Public Arr As List(Of clsItemConversionDetails) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsItemConversion, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsItemConversion, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_Item_Conversion_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            If clsCommon.myLen(strAdjustmentNoTemp) > 0 Then
                obj.Doc_Code = strAdjustmentNoTemp
                'isNewEntry = True
            Else
                isNewEntry = True
                If isNewEntry Then
                    obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.Item_Conversion, "", "")
                End If


            End If


            If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt ss"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "isPosted", "1")
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Conv_Type", obj.Conv_Type)
             clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Conversion_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Conversion_Head", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsItemConversionDetails.SaveData(obj.Doc_Code, Arr, isNewEntry, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsItemConversion

        Dim obj As clsItemConversion = Nothing
        Dim qry As String = "SELECT ch.*,item_DESC from TSPL_Item_Conversion_HEAD ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and ch.Doc_Code = (select MIN(Doc_Code) from TSPL_Item_Conversion_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and ch.Doc_Code = (select Max(Doc_Code) from TSPL_Item_Conversion_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and ch.Doc_Code = (select Min(Doc_Code) from TSPL_Item_Conversion_HEAD where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and ch.Doc_Code = (select Max(Doc_Code) from TSPL_Item_Conversion_HEAD where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and ch.Doc_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemConversion()

            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))

            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("item_Desc"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Conv_Type = clsCommon.myCstr(dt.Rows(0)("Conv_type"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("isPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            '        strCode = dt.Rows(0)("DOC_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            qry = "SELECT ch.*,item_DESC,Unit_Code from TSPL_Item_Conversion_detail ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code where  Doc_Code='" + obj.Doc_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsItemConversionDetails)
                Dim objTr As clsItemConversionDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New clsItemConversionDetails()
                    objTr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))

                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.UOM = clsCommon.myCstr(dr("Unit_Code"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function GetData_via_Item(ByVal strItem_Code As String, ByVal NavType As NavigatorType) As clsItemConversion

        Dim obj As clsItemConversion = Nothing
        Dim qry As String = "SELECT ch.*,item_DESC from TSPL_Item_Conversion_HEAD ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code where 2=2"
        Dim whrClas As String = ""
        qry += " and ch.Item_Code = '" + strItem_Code + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemConversion()

            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))

            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("item_Desc"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Conv_Type = clsCommon.myCstr(dt.Rows(0)("Conv_type"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("isPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            '        strCode = dt.Rows(0)("DOC_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            qry = "SELECT ch.*,item_DESC,Unit_Code from TSPL_Item_Conversion_detail ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code where  Doc_Code='" + obj.Doc_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsItemConversionDetails)
                Dim objTr As clsItemConversionDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New clsItemConversionDetails()
                    objTr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))

                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.UOM = clsCommon.myCstr(dr("Unit_Code"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    '' Anubhooti 10-Dec-2014 (Get Data For Many To One Case)
    Public Shared Function GetData_via_ItemForMTO(ByVal strItem_Code As String, ByVal NavType As NavigatorType, ByVal ItemCount As Integer) As clsItemConversion
        '' IF GRID ANY ONE ITEM IS CONSIDERED AS MAIN ITEM AGAINST HEADER ITEM(MANY TO ONE)
        'Dim DetailQry As String
        'Dim Detaildt As DataTable
        'DetailQry = "SELECT TOP 1 ch.*,item_DESC,Unit_Code from TSPL_Item_Conversion_detail ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code LEFT OUTER JOIN TSPL_Item_Conversion_Head ON TSPL_Item_Conversion_Head.Doc_Code =CH.Doc_CODE where  ch.item_code='" + strItem_Code + "' ORDER BY convert(Datetime,TSPL_Item_Conversion_Head.Modified_Date,104) DESC"
        'Detaildt = New DataTable()
        'Detaildt = clsDBFuncationality.GetDataTable(DetailQry)

        'Dim obj As clsItemConversion = Nothing
        'If (Detaildt IsNot Nothing AndAlso Detaildt.Rows.Count > 0) Then

        '    Dim qry As String = "SELECT ch.*,item_DESC from TSPL_Item_Conversion_HEAD ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code where 2=2"
        '    Dim whrClas As String = ""
        '    qry += " and ch.Doc_Code = '" + clsCommon.myCstr(Detaildt.Rows(0)("Doc_Code")) + "' "

        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '        obj = New clsItemConversion()
        '        obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
        '        obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        '        obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_Code"))
        '        obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("item_Desc"))
        '        obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
        '        obj.Conv_Type = clsCommon.myCstr(dt.Rows(0)("Conv_type"))
        '        obj.UOMMO = clsCommon.myCstr(Detaildt.Rows(0)("Unit_Code"))
        '        obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("isPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        '        'strCode = dt.Rows(0)("DOC_CODE")

        '        If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
        '            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
        '        Else
        '            obj.Posting_Date = Nothing
        '        End If
        '    End If
        'End If

        '' COMBINATION OF ALL ITEMS ARE AGAINST HEADER ITEM
        Dim DetailQry As String
        Dim Detaildt As DataTable
        DetailQry = "SELECT TOP 1 ch.*,item_DESC,Unit_Code from TSPL_Item_Conversion_detail ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code LEFT OUTER JOIN TSPL_Item_Conversion_Head ON TSPL_Item_Conversion_Head.Doc_Code =CH.Doc_CODE WHERE ch.Doc_CODE in (select inn.Doc_CODE from TSPL_Item_Conversion_detail as inn  Where  inn.Item_Code in (" & strItem_Code & ")group by inn.Doc_CODE having SUM(1)=" & ItemCount & ") ORDER BY convert(Datetime,TSPL_Item_Conversion_Head.Modified_Date,104) DESC"
        Detaildt = New DataTable()
        Detaildt = clsDBFuncationality.GetDataTable(DetailQry)

        Dim obj As clsItemConversion = Nothing
        If (Detaildt IsNot Nothing AndAlso Detaildt.Rows.Count > 0) Then

            Dim qry As String = "SELECT ch.*,item_DESC from TSPL_Item_Conversion_HEAD ch left join TSPL_ITEM_Master itm on itm.Item_Code=ch.item_code where 2=2"
            Dim whrClas As String = ""
            qry += " and ch.Doc_Code = '" + clsCommon.myCstr(Detaildt.Rows(0)("Doc_Code")) + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsItemConversion()

                obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))

                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("item_Desc"))
                obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
                obj.Conv_Type = clsCommon.myCstr(dt.Rows(0)("Conv_type"))
                obj.UOMMO = clsCommon.myCstr(Detaildt.Rows(0)("Unit_Code"))
                obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("isPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                'strCode = dt.Rows(0)("DOC_CODE")

                If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                Else
                    obj.Posting_Date = Nothing
                End If
            End If
        End If

        Return obj
    End Function
    ''

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsItemConversion()
        obj = clsItemConversion.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If
                Dim qry As String = "delete from TSPL_Item_Conversion_DETAIL where Doc_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Item_Conversion_HEAD where Doc_Code='" + strCode + "'"
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
    ''To be Uncomment
    'Public Shared Sub PrintData(ByVal strDocNo As String, ByVal IsPreprinted As Boolean)
    '    Try

    '        Dim qry As String
    '        Dim dt As DataTable
    '        qry = "select * from TSPL_Item_Conversion_HEAD left outer  join TSPL_Item_Conversion_DETAIL on TSPL_Item_Conversion_HEAD.Doc_Code=TSPL_Item_Conversion_DETAIL.Doc_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Item_Conversion_HEAD.loc_code left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Item_Conversion_HEAD.comp_code " & _
    '             " where TSPL_Item_Conversion_HEAD.Doc_Code='" + strDocNo + "' ORDER by document_line_no"
    '        dt = clsDBFuncationality.GetDataTable(qry)
    '        If IsPreprinted Then
    '            InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "crptExpiryDetails", "Expired Item Entry")
    '        Else
    '            InventryViewer.funreport(dt, EnumTecxpertPaperSize.NA, "crptExpiryDetails", "Expired Item Entry")
    '        End If

    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsItemConversion = clsItemConversion.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_Item_Conversion_HEAD set isPOSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsItemConversionDetails
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Line_No As String = Nothing
    Public Item_Desc As String = Nothing
    Public Description As String = Nothing
    Public UOM As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsItemConversionDetails), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
    
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objtr As clsItemConversionDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                clsCommon.AddColumnsForChange(coll, "Description", objtr.Description)
                'If isNewEntry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Conversion_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                'Else
                '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Conversion_DETAIL", OMInsertOrUpdate.Update, "TSPL_Item_Conversion_DETAIL.Doc_Code='" & objtr.Doc_Code & "' and Item_Code='" & objtr.Item_Code & "'", trans)
                'End If
            Next
        End If
        Return True
    End Function
End Class


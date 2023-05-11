Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class clsExpiryDate

#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Customer_CODE As String = Nothing
    Public Customer_NAME As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Desc As String = Nothing
    Public Reference As String = Nothing
    Public Description As String = Nothing
    Public Posted As String = Nothing
    Public Posting_Date As DateTime = Nothing
    Public EntryDateTime As DateTime = Nothing
    Public Is_Imported As Integer = 0
    Dim Created_By As String = Nothing
    Dim Created_Date As DateTime = Nothing
    Dim Modify_By As String = Nothing
    Dim Modify_Date As DateTime = Nothing
    Dim Comp_Code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public Reference_Document As String = Nothing
    Public Shell_Qty As Double = 0
    Public Shell_Amount As Double = 0
    Public isBySaleInvoice As Boolean = False ''Not a table columns
    Public Arr As List(Of ClsExpiryDetails) = Nothing
    Public chkthirdparty As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsExpiryDate, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsExpiryDate, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_EXPIRY_DETAIL where DOCUMENT_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            If clsCommon.myLen(strAdjustmentNoTemp) > 0 Then
                obj.Document_No = strAdjustmentNoTemp
                isNewEntry = True
            Else
                If isNewEntry Then
                    Dim strDoc As String = ""
                    Dim strDocTrans As String = ""

                    strDoc = clsDocType.ExpiryTransaction
                    If clsCommon.myLen(strDoc) <= 0 Then
                        Throw New Exception("Document type not found")
                    End If

                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, strDoc, "", obj.Loc_Code)

                End If


            End If


            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EntryDateTime", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Customer_CODE", obj.Customer_CODE)
            clsCommon.AddColumnsForChange(coll, "Customer_NAME", obj.Customer_NAME)
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Desc", obj.Loc_Desc)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "Reference_Document", obj.Reference_Document)
            clsCommon.AddColumnsForChange(coll, "Shell_Qty", obj.Shell_Qty)
            clsCommon.AddColumnsForChange(coll, "Shell_Amount", obj.Shell_Amount)
            clsCommon.AddColumnsForChange(coll, "Third_Party_Location", obj.chkthirdparty)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EXPIRY_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EXPIRY_HEADER", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsExpiryDetails.SaveData(obj.Document_No, obj.Loc_Code, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsExpiryDate

        Dim obj As clsExpiryDate = Nothing
        Dim qry As String = "SELECT * from TSPL_EXPIRY_HEADER where 2=2"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EXPIRY_HEADER.Document_No = (select MIN(Document_No) from TSPL_EXPIRY_HEADER where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_EXPIRY_HEADER.Document_No = (select Max(Document_No) from TSPL_EXPIRY_HEADER where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_EXPIRY_HEADER.Document_No = (select Min(Document_No) from TSPL_EXPIRY_HEADER where Document_No>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_EXPIRY_HEADER.Document_No = (select Max(Document_No) from TSPL_EXPIRY_HEADER where Document_No<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_EXPIRY_HEADER.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsExpiryDate()
            obj.chkthirdparty = clsCommon.myCstr(dt.Rows(0)("Third_Party_Location"))

            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.Customer_CODE = clsCommon.myCstr(dt.Rows(0)("Customer_CODE"))
            obj.Customer_NAME = clsCommon.myCstr(dt.Rows(0)("Customer_NAME"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Desc = clsCommon.myCstr(dt.Rows(0)("Loc_Desc"))
            obj.EntryDateTime = clsCommon.myCDate(dt.Rows(0)("EntryDateTime"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Reference_Document = clsCommon.myCstr(dt.Rows(0)("Reference_Document"))
            obj.Shell_Qty = clsCommon.myCdbl(dt.Rows(0)("Shell_Qty"))
            obj.Shell_Amount = clsCommon.myCdbl(dt.Rows(0)("Shell_Amount"))
            'obj.Is_Imported = clsCommon.myCdbl(dt.Rows(0)("Is_Imported"))

            qry = "SELECT  * from TSPL_EXPIRY_DETAIL where  Document_No='" + obj.Document_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsExpiryDetails)
                Dim objTr As ClsExpiryDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsExpiryDetails()
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Document_Line_No = clsCommon.myCdbl(dr("Document_Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Description = clsCommon.myCstr(dr("Item_Description"))
                    objTr.Item_Quantity = clsCommon.myCdbl(dr("Item_Quantity"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.mrp = clsCommon.myCdbl(dr("mrp"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comments = clsCommon.myCstr(dr("Comments"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Breakage_Qty = clsCommon.myCdbl(dr("Breakage_Qty"))
                    objTr.Leakage_Qty = clsCommon.myCdbl(dr("Leakage_Qty"))
                    objTr.Liquid_Rate = clsCommon.myCdbl(dr("Liquid_Rate"))
                    objTr.Liquid_Amount = clsCommon.myCdbl(dr("Liquid_Amount"))
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
        Dim obj As New clsExpiryDate()
        obj = clsExpiryDate.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If
                Dim qry As String = "delete from TSPL_EXPIRY_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_EXPIRY_HEADER where Document_No='" + strCode + "'"
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

    Shared Function PostData(ByVal DocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(DocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Shared Function PostData(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim blnShell As Boolean = False
        Dim obj As New clsExpiryDate()
        obj = clsExpiryDate.GetData(StrDocNo, NavigatorType.Current, trans)
        If obj Is Nothing Then
            Throw New Exception("No Data Found to Post")
        End If
        If clsCommon.CompairString("Y", obj.Posted) = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Transaction :" + StrDocNo)
        End If

        Try
            ' Dim conversion As Decimal
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            For Each objtr As ClsExpiryDetails In obj.Arr


                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If

                Dim objLocationDetails As New clsItemLocationDetails()
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_Code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objtr.Item_Code + " and Uom:'" + objtr.Unit_Code)
                End If
                Dim RI As Integer = 1

                objLocationDetails.Item_Code = objtr.Item_Code
                objLocationDetails.Item_Desc = objtr.Item_Description
                objLocationDetails.Location_Code = obj.Loc_Code
                objLocationDetails.Location_Desc = obj.Loc_Desc
                objLocationDetails.Item_Qty = RI * (objtr.Item_Quantity)
                objLocationDetails.Amount = 0
                objLocationDetails.MRP = objtr.mrp
                objLocationDetails.Batch_No = ""
                objLocationDetails.ItemType = "E"
                ArrLocationDetails.Add(objLocationDetails)




                Dim objInventoryMovemnt As New clsInventoryMovement()

                objInventoryMovemnt.InOut = "O"

                objInventoryMovemnt.Location_Code = obj.Loc_Code
                objInventoryMovemnt.Item_Code = objtr.Item_Code
                objInventoryMovemnt.Item_Desc = objtr.Item_Description
                objInventoryMovemnt.Qty = objtr.Item_Quantity
                objInventoryMovemnt.UOM = objtr.Unit_Code
                objInventoryMovemnt.Basic_Cost = objtr.Item_Cost
                objInventoryMovemnt.MRP = objtr.mrp
                objInventoryMovemnt.Add_Cost = objtr.Item_Cost
                objInventoryMovemnt.Net_Cost = objtr.Item_Cost
                objInventoryMovemnt.ItemType = strItemTypeToSave

                objInventoryMovemnt.Cust_Code = obj.Customer_CODE
                objInventoryMovemnt.Cust_Name = obj.Customer_NAME
                ArrInventoryMovement.Add(objInventoryMovemnt)

            Next


            CreateJE(obj, trans)

            If ArrInventoryMovement IsNot Nothing AndAlso ArrInventoryMovement.Count > 0 Then
                clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
                clsInventoryMovement.SaveData("ExpiredItem", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                '--------------------------------------------------------------------------------------------------------------------------------------
                ''--- GL End
            End If
            Dim qry = " update TSPL_EXPIRY_HEADER  set Posted='Y' where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
    Public Shared Function CreateJE(ByVal obj As clsExpiryDate, ByVal trans As SqlTransaction) As Boolean
        Dim desc As String
        Dim blnAddShell As Boolean = False
        Dim strInvAcc, strDamageAcct As String
        Dim ArryLstFinal As ArrayList = New ArrayList()
        Dim strsegment As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.Loc_Code + "'", trans)
        desc = "Empty transaction Against " + obj.Document_No

        Dim ArryLst As ArrayList = New ArrayList()
        For Each objtr As ClsExpiryDetails In obj.Arr
            Dim ItemCost As Double = 0
            Dim BreakageCost As Double = 0
            Dim LeakageCost As Double = 0

            Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Non_Stock_Clearing  AS Inv_Control_Account , Adjustment_Account as Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
            If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
            End If


            ''--- GL Begins Now

            ItemCost = objtr.Item_Quantity * objtr.Item_Cost


            '-----------------------------------------------------------

            strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
            strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)


            Dim dt As DataTable = clsItemMaster.GetDamageAccGLAC(objtr.Item_Code, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Damage account for item" + objtr.Item_Code)
            End If
            strDamageAcct = clsCommon.myCstr(dt.Rows(0)("Damaged_Goods"))
            strDamageAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strDamageAcct, strsegment, trans)
            Dim Acc1() As String = {strInvAcc, ItemCost}
            ArryLst.Add(Acc1)

            Dim Acc2() As String = {strDamageAcct, -1 * ItemCost}
            ArryLst.Add(Acc2)


            'For Each Str() As String In ArryLst
            '    Dim strNew() As String = {Str(0), Str(1)}
            '    ArryLstFinal.Add(strNew)
            'Next

        Next

        If ArryLst IsNot Nothing AndAlso ArryLst.Count > 0 Then
            Dim strRemarks As String = "Vehicle code:" + obj.Vehicle_Code + "  Vehicle No:" + obj.Vehicle_No + " Remarks:" + obj.Description + "  "
            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Document_Date, "", "EX-AD", "EX Adjustments", obj.Document_No, obj.Reference, "C", obj.Customer_CODE, obj.Customer_NAME, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, strRemarks, obj.Reference, "")
        End If

        Return True
    End Function

    ''To be Uncomment
    'Public Shared Sub PrintData(ByVal strDocNo As String, ByVal IsPreprinted As Boolean)
    '    Try

    '        Dim qry As String
    '        Dim dt As DataTable
    '        qry = "select * from TSPL_EXPIRY_HEADER left outer  join TSPL_EXPIRY_DETAIL on TSPL_EXPIRY_HEADER.DOcument_no=TSPL_EXPIRY_DETAIL.DOcument_no left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EXPIRY_HEADER.loc_code left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_EXPIRY_HEADER.comp_code " & _
    '             " where TSPL_EXPIRY_HEADER.DOcument_no='" + strDocNo + "' ORDER by document_line_no"
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

End Class
Public Class ClsExpiryDetails
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Description As String = Nothing
    Public Item_Quantity As Double = 0
    Public Item_Cost As Double = 0
    Public Unit_Code As String = Nothing
    Public mrp As Double = 0
    Public Amount As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Breakage_Qty As Double = 0
    Public Leakage_Qty As Double = 0
    Public Liquid_Rate As Double = 0
    Public Liquid_Amount As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocationCode As String, ByVal Arr As List(Of ClsExpiryDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsExpiryDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Document_Line_No", counter)
                counter += 1
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Description", objtr.Item_Description)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Quantity", objtr.Item_Quantity)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "mrp", objtr.mrp)
                clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comments", objtr.Comments)
                clsCommon.AddColumnsForChange(coll, "Breakage_Qty", objtr.Breakage_Qty)
                clsCommon.AddColumnsForChange(coll, "Leakage_Qty", objtr.Leakage_Qty)
                clsCommon.AddColumnsForChange(coll, "Liquid_Rate", objtr.Liquid_Rate)
                clsCommon.AddColumnsForChange(coll, "Liquid_Amount", objtr.Liquid_Amount)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EXPIRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class


Public Class ClsExpiryDateHistory

#Region "variable"
    Public Screen_Name As String = Nothing
    Public Program_Code As String = Nothing
    Public Document_No As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Expiry_Date As Date? = Nothing
    Public New_Expiry_Date As Date = Nothing
    ' Dim older_Expiry_date As Date = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As ClsExpiryDateHistory, ByVal isnewentry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Dim ExpEntryForPO As Double = 0
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Screen_Name", obj.Screen_Name)
            clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            'If clsCommon.myLen(obj.Expiry_Date) > 0 Then

            If clsCommon.myLen(obj.Expiry_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Expiry_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "New_Expiry_Date", clsCommon.GetPrintDate(obj.New_Expiry_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMMM/yyyy "))
            If isnewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMMM/yyyy "))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EXPIRY_DATE", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EXPIRY_DATE", OMInsertOrUpdate.Update, "TSPL_EXPIRY_DATE.Screen_Name='" + obj.Screen_Name + "' AND Document_No='" & obj.Document_No & "'", trans)
            End If
            '' Anubhooti 03-Nov-2014
            If clsCommon.CompairString(obj.Program_Code, "PO-ODR") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_PURCHASE_ORDER_HEAD SET Expiry_Date='" & clsCommon.GetPrintDate(obj.New_Expiry_Date, "dd/MMM/yyyy") & "' WHERE PurchaseOrder_No='" & obj.Document_No & "'", trans)
            ElseIf clsCommon.CompairString(obj.Program_Code, clsUserMgtCode.frmSalesOrderProductSale) = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_SD_SALES_ORDER_HEAD SET Delivery_date='" & clsCommon.GetPrintDate(obj.New_Expiry_Date, "dd/MMM/yyyy") & "' WHERE Document_Code='" & obj.Document_No & "'", trans)
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE SET Delivery_date='" & clsCommon.GetPrintDate(obj.New_Expiry_Date, "dd/MMM/yyyy") & "' WHERE Against_Sales_Order='" & obj.Document_No & "'", trans)

            ElseIf clsCommon.CompairString(obj.Program_Code, clsUserMgtCode.frmDeliveryPrderProductSale) = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE SET Delivery_date='" & clsCommon.GetPrintDate(obj.New_Expiry_Date, "dd/MMM/yyyy") & "' WHERE Document_Code='" & obj.Document_No & "'", trans)
            ElseIf clsCommon.CompairString(obj.Program_Code, clsUserMgtCode.frmBookingProductSale) = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BOOKING_MASTER_PRODUCTSALE SET bookvalidity_date='" & clsCommon.GetPrintDate(obj.New_Expiry_Date, "dd/MMM/yyyy") & "' WHERE Document_Code='" & obj.Document_No & "'", trans)
            End If
            ''
            trans.Commit()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        Return IsSaved
    End Function
    Public Shared Function DeleteData(ByVal strcode As String, ByVal trans As SqlTransaction)
        Try

            If clsCommon.myLen(strcode) >= 0 Then
                Dim qry As String = "delete Document_No from TSPL_EXPIRY_DATE where Screen_Name='" + strcode + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getdata(ByVal code As String) As ClsExpiryDateHistory
        Try
            Dim obj As ClsExpiryDateHistory = Nothing
            Dim qst As String = "select TSPL_EXPIRY_DATE.Program_Code,Doc_Date ,Document_No,Expiry_Date ,New_Expiry_Date ,Screen_Name    from TSPL_PROGRAM_MASTER inner join TSPL_EXPIRY_DATE on  TSPL_PROGRAM_MASTER.Program_Code =TSPL_EXPIRY_DATE.Program_Code where TSPL_EXPIRY_DATE.Screen_Name='" + code + "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New ClsExpiryDateHistory
                obj.Screen_Name = clsCommon.myCstr(dt1.Rows(0)("Screen_Name"))
                obj.Program_Code = clsCommon.myCstr(dt1.Rows(0)("Program_code"))
                obj.Doc_Date = clsCommon.myCstr(dt1.Rows(0)("Doc_Date"))
                obj.Document_No = clsCommon.myCstr(dt1.Rows(0)("Document_No"))
                If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Expiry_Date"))) > 0 Then
                    obj.Expiry_Date = clsCommon.myCstr(dt1.Rows(0)("Expiry_Date"))
                Else
                    obj.Expiry_Date = Nothing
                End If
                If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("New_Expiry_Date"))) > 0 Then
                    obj.New_Expiry_Date = clsCommon.myCstr(dt1.Rows(0)("New_Expiry_Date"))
                Else
                    obj.New_Expiry_Date = Nothing
                End If

            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Imports System.Data.SqlClient
Imports common

Public Class clsBullVaccinationEntry

#Region "Variables"

    Public Document_Code As String = Nothing
    Public Document_date As DateTime? = Nothing
    Public Status As Integer = 0
    Public Remarks As String = Nothing
    Public Bull_Code As String = Nothing
    Public BullAliasName As String = Nothing
    Public RegDate As Date? = Nothing
    Public PreBullId As String = Nothing
    Public SSBullId As String = Nothing
    Public SSCentre As String = Nothing
    Public DOB As String = Nothing
    Public Breed As String = Nothing
    Public Arr As List(Of clsBullVaccinationEntryDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsBullVaccinationEntry, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsBullVaccinationEntry, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_BULL_VACCINE_ENTRY_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bull_Code", clsCommon.myCstr(obj.Bull_Code))
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                If clsCommon.CompairString(AutoSave, False) = CompairStringResult.Equal Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.BullVaccinationEntry, "", "")
                End If
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_VACCINE_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_VACCINE_ENTRY", OMInsertOrUpdate.Update, "TSPL_BULL_VACCINE_ENTRY.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBullVaccinationEntryDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BULL_VACCINE_ENTRY", "Document_Code", "TSPL_BULL_VACCINE_ENTRY_DETAIL", "Document_Code", trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsBullVaccinationEntry
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsBullVaccinationEntry
        Dim obj As clsBullVaccinationEntry = Nothing
        Dim qry As String = "select Document_Code,Remarks ,TSPL_BULL_VACCINE_ENTRY.Bull_Code, Document_date ,ISNULL( Status,0) as Status ,Bull_Alia_Name as Name,Prev_Bull_Id,Registration_Date as [Registration Date] ,SS_Bull_Id as [SS Bull Id],Breed_Code as Breed ,SS_Centre_Code as [SS Centre],Date_Of_Birth as [Date of Birth]
         from TSPL_BULL_VACCINE_ENTRY left outer join tspl_bull_master on tspl_bull_master.Bull_Code = TSPL_BULL_VACCINE_ENTRY.Bull_Code   where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BULL_VACCINE_ENTRY.Document_Code = (select MIN(Document_Code) from TSPL_BULL_VACCINE_ENTRY)"
            Case NavigatorType.Last
                qry += " and TSPL_BULL_VACCINE_ENTRY.Document_Code = (select Max(Document_Code) from TSPL_BULL_VACCINE_ENTRY)"
            Case NavigatorType.Next
                qry += " and TSPL_BULL_VACCINE_ENTRY.Document_Code = (select Min(Document_Code) from TSPL_BULL_VACCINE_ENTRY where Document_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_BULL_VACCINE_ENTRY.Document_Code = (select Max(Document_Code) from TSPL_BULL_VACCINE_ENTRY where Document_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_BULL_VACCINE_ENTRY.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBullVaccinationEntry()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Bull_Code = clsCommon.myCstr(dt.Rows(0)("Bull_Code"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.BullAliasName = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.PreBullId = clsCommon.myCstr(dt.Rows(0)("Prev_Bull_Id"))
            obj.SSBullId = clsCommon.myCstr(dt.Rows(0)("SS Bull Id"))
            obj.SSCentre = clsCommon.myCstr(dt.Rows(0)("SS Centre"))
            obj.RegDate = clsCommon.myCstr(dt.Rows(0)("Registration Date"))
            obj.DOB = clsCommon.myCstr(dt.Rows(0)("Date of Birth"))
            obj.Breed = clsCommon.myCstr(dt.Rows(0)("Breed"))

            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "	 select TSPL_BULL_VACCINE_ENTRY_DETAIL.*,tspl_item_master.Item_Desc  from TSPL_BULL_VACCINE_ENTRY_DETAIL
		 left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_BULL_VACCINE_ENTRY_DETAIL.Item_Code where Document_Code='" + obj.Document_Code + "' order by PK_Id "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBullVaccinationEntryDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsBullVaccinationEntryDetail
                    objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If


        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_Code as DocumentNo ,Bull_Code as [Bull Code],convert(varchar(12),Document_date,103) as DocumentDate,case when Status = 1 then 'Posted' else 'Unposted' end as Posted from TSPL_BULL_VACCINE_ENTRY"
        str = clsCommon.ShowSelectForm("BullVaccinationEntry", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function


    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsBullVaccinationEntry = clsBullVaccinationEntry.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If
            UpdateInventoryMovement(obj, trans, False)

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_BULL_VACCINE_ENTRY set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_Code='" & obj.Document_Code & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateInventoryMovement(ByVal obj As clsBullVaccinationEntry, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean
        Try
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            If UpdateInventory = True Then
                clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Document_Code & "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.Document_Code & "'", trans)
            End If
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            For Each objTr As clsBullVaccinationEntryDetail In obj.Arr
                intCounter = intCounter + 1

                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
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
                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where User_Code = '" & objCommonVar.CurrentUserCode & "'", trans))

                'objInventoryMovemnt.Cust_Code = obj.Customer_Code
                'objInventoryMovemnt.Cust_Name = obj.Customer_Name
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                'objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                'objInventoryMovemnt.MRP = objTr.MRP
                'objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                'objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next
            clsInventoryMovement.SaveData(clsUserMgtCode.frmBullVaccinationEntry, obj.Document_Code, obj.Document_date, clsCommon.GetPrintDate(obj.Document_date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsBullVaccinationEntry = clsBullVaccinationEntry.GetData(strCode, NavigatorType.Current, Nothing, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_VACCINE_ENTRY", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsBullVaccinationEntry = clsBullVaccinationEntry.GetData(strCode, NavigatorType.Current, "", trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_BULL_VACCINE_ENTRY_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BULL_VACCINE_ENTRY where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsBullVaccinationEntryDetail

#Region "Variables"
    Public Document_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public Qty As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBullVaccinationEntryDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBullVaccinationEntryDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_VACCINE_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class




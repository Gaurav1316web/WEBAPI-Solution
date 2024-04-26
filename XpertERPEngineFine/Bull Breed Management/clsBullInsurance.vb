Imports System.Data.SqlClient
Imports common

Public Class clsBullInsurance

#Region "Variables"

    Public Document_Code As String = Nothing
    Public Policy_Code As String = Nothing
    Public Policy_Date As DateTime? = Nothing
    Public Status As Integer = 0
    Public Ins_Comp_Code As String = Nothing
    Public Ins_Comp_Name As String = Nothing
    Public Ins_Type_Code As String = Nothing
    Public Ins_Type_Name As String = Nothing
    Public Policy_Start_Date As Date? = Nothing
    Public Policy_End__Date As Date? = Nothing
    Public Insured_Amount As Double = 0
    Public Premium_Per As Double = 0
    Public Premium_Amount As Double = 0
    Public Ser_Charge_Per As Double = 0
    Public Ser_Charge_Amount As Double = 0
    Public Total_Amount As Double = 0

    Public Arr As List(Of clsBullInsuranceDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsBullInsurance, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsBullInsurance, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_BULL_INSURANCE_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Policy_Code", clsCommon.myCstr(obj.Policy_Code))
            clsCommon.AddColumnsForChange(coll, "Policy_Date", clsCommon.GetPrintDate(obj.Policy_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Ins_Comp_Code", clsCommon.myCstr(obj.Ins_Comp_Code))
            clsCommon.AddColumnsForChange(coll, "Ins_Type_Code", clsCommon.myCstr(obj.Ins_Type_Code))
            clsCommon.AddColumnsForChange(coll, "Policy_Start_Date", clsCommon.GetPrintDate(obj.Policy_Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Policy_End__Date", clsCommon.GetPrintDate(obj.Policy_End__Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Insured_Amount", clsCommon.myCdbl(obj.Insured_Amount))
            clsCommon.AddColumnsForChange(coll, "Premium_Per", clsCommon.myCDecimal(obj.Premium_Per))
            clsCommon.AddColumnsForChange(coll, "Premium_Amount", clsCommon.myCdbl(obj.Premium_Amount))
            clsCommon.AddColumnsForChange(coll, "Ser_Charge_Per", clsCommon.myCDecimal(obj.Ser_Charge_Per))
            clsCommon.AddColumnsForChange(coll, "Ser_Charge_Amount", clsCommon.myCdbl(obj.Ser_Charge_Amount))
            clsCommon.AddColumnsForChange(coll, "Total_Amount", clsCommon.myCdbl(obj.Total_Amount))

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                If clsCommon.CompairString(AutoSave, False) = CompairStringResult.Equal Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Policy_Date, clsDocType.BullInsurance, "", "")
                End If
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_INSURANCE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_INSURANCE", OMInsertOrUpdate.Update, "TSPL_BULL_INSURANCE.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBullInsuranceDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BULL_INSURANCE", "Document_Code", "TSPL_BULL_INSURANCE_DETAIL", "Document_Code", trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsBullInsurance
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsBullInsurance
        Dim obj As clsBullInsurance = Nothing
        Dim qry As String = "select Document_Code,Policy_Code,Policy_Date,Policy_Start_Date,Policy_End__Date,TSPL_BULL_INSURANCE.Ins_Comp_Code,TSPL_BULL_INSURANCE.Ins_Type_Code,Insured_Amount,Premium_Per,Premium_Amount,Ser_Charge_Per,Ser_Charge_Amount,Total_Amount ,ISNULL( Status,0) as Status ,TSPL_BULL_INSURANCE_MASTER.Name as Ins_Comp_Name ,TSPL_BULL_INSURANCE_TYPE.Name as Ins_Type_Name
        from TSPL_BULL_INSURANCE left outer join TSPL_BULL_INSURANCE_MASTER on TSPL_BULL_INSURANCE_MASTER.Code = TSPL_BULL_INSURANCE.Ins_Comp_Code  left outer join TSPL_BULL_INSURANCE_TYPE on TSPL_BULL_INSURANCE_TYPE.Code = TSPL_BULL_INSURANCE.Ins_Type_Code   where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BULL_INSURANCE.Document_Code = (select MIN(Document_Code) from TSPL_BULL_INSURANCE)"
            Case NavigatorType.Last
                qry += " and TSPL_BULL_INSURANCE.Document_Code = (select Max(Document_Code) from TSPL_BULL_INSURANCE)"
            Case NavigatorType.Next
                qry += " and TSPL_BULL_INSURANCE.Document_Code = (select Min(Document_Code) from TSPL_BULL_INSURANCE where Document_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_BULL_INSURANCE.Document_Code = (select Max(Document_Code) from TSPL_BULL_INSURANCE where Document_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_BULL_INSURANCE.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBullInsurance()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Policy_Code = clsCommon.myCstr(dt.Rows(0)("Policy_Code"))
            obj.Policy_Date = clsCommon.myCDate(dt.Rows(0)("Policy_Date"))
            obj.Ins_Comp_Code = clsCommon.myCstr(dt.Rows(0)("Ins_Comp_Code"))
            obj.Ins_Comp_Name = clsCommon.myCstr(dt.Rows(0)("Ins_Comp_Name"))
            obj.Ins_Type_Code = clsCommon.myCstr(dt.Rows(0)("Ins_Type_Code"))
            obj.Ins_Type_Name = clsCommon.myCstr(dt.Rows(0)("Ins_Type_Name"))
            obj.Policy_Start_Date = clsCommon.myCDate(dt.Rows(0)("Policy_Start_Date"))
            obj.Policy_End__Date = clsCommon.myCDate(dt.Rows(0)("Policy_End__Date"))
            obj.Insured_Amount = clsCommon.myCdbl(dt.Rows(0)("Insured_Amount"))
            obj.Premium_Per = clsCommon.myCDecimal(dt.Rows(0)("Premium_Per"))
            obj.Premium_Amount = clsCommon.myCdbl(dt.Rows(0)("Premium_Amount"))
            obj.Ser_Charge_Per = clsCommon.myCDecimal(dt.Rows(0)("Ser_Charge_Per"))
            obj.Ser_Charge_Amount = clsCommon.myCdbl(dt.Rows(0)("Ser_Charge_Amount"))
            obj.Total_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Amount"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "	select *  from TSPL_BULL_INSURANCE_DETAIL where Document_Code='" + obj.Document_Code + "' order by PK_Id "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBullInsuranceDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsBullInsuranceDetail
                    objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objtr.Tag_No = clsCommon.myCstr(dr("Tag_No"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If


        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_Code as DocumentCode,Policy_Code as PolicyCode,convert(varchar(12),Policy_Date,103) as [Policy Date],Policy_Start_Date as [Policy Start Date],Policy_End__Date as [Policy End Date] ,Ins_Comp_Code as [Insurance Company Code],Ins_Type_Code as [Insurance Type Code],case when Status = 1 then 'Posted' else 'Unposted' end as Posted from TSPL_BULL_INSURANCE"
        str = clsCommon.ShowSelectForm("BullInsurance", sql, "DocumentCode", "", strCode, "DocumentCode", isButtonClicked)
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
            Dim obj As clsBullInsurance = clsBullInsurance.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If
            'UpdateInventoryMovement(obj, trans, False)

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_BULL_INSURANCE set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_Code='" & obj.Document_Code & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function UpdateInventoryMovement(ByVal obj As clsBullInsurance, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean
    '    Try
    '        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
    '        If UpdateInventory = True Then
    '            clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Document_Code & "'", trans)
    '            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.Document_Code & "'", trans)
    '        End If
    '        Dim strRgpNo As String = Nothing
    '        Dim intCounter As Integer = 0
    '        For Each objTr As clsBullInsuranceDetail In obj.Arr
    '            intCounter = intCounter + 1

    '            Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
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
    '            Dim objInventoryMovemnt As New clsInventoryMovement()
    '            objInventoryMovemnt.InOut = "O"
    '            objInventoryMovemnt.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where User_Code = '" & objCommonVar.CurrentUserCode & "'", trans))

    '            'objInventoryMovemnt.Cust_Code = obj.Customer_Code
    '            'objInventoryMovemnt.Cust_Name = obj.Customer_Name
    '            objInventoryMovemnt.Item_Code = objTr.Item_Code
    '            objInventoryMovemnt.Item_Desc = objTr.Item_Desc
    '            objInventoryMovemnt.Qty = objTr.Qty
    '            objInventoryMovemnt.UOM = objTr.Unit_code
    '            'objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
    '            'objInventoryMovemnt.MRP = objTr.MRP
    '            'objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
    '            'objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
    '            objInventoryMovemnt.ItemType = strItemTypeToSave
    '            ArrInventoryMovement.Add(objInventoryMovemnt)
    '        Next
    '        clsInventoryMovement.SaveData(clsUserMgtCode.frmBullInsurance, obj.Document_Code, obj.Policy_Date, clsCommon.GetPrintDate(obj.Policy_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
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

            Dim obj As clsBullInsurance = clsBullInsurance.GetData(strCode, NavigatorType.Current, Nothing, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_INSURANCE", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)

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
        Dim obj As clsBullInsurance = clsBullInsurance.GetData(strCode, NavigatorType.Current, "", trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_BULL_INSURANCE_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BULL_INSURANCE where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsBullInsuranceDetail

#Region "Variables"
    Public Document_Code As String = Nothing
    Public Tag_No As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBullInsuranceDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBullInsuranceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Tag_No", obj.Tag_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_INSURANCE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class




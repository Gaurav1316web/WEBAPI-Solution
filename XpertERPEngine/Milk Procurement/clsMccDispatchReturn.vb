Imports common
Imports System.Data.SqlClient
Public Class clsMCCDispatchReturn
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Challan_No As String = Nothing
    Public Remarks As String = Nothing

    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMCCDispatchReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim objSRN As clsMccDispatch = clsMccDispatch.getData(obj.Challan_No, NavigatorType.Current)
            AllowToSave(obj)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Tanker Dispatch Return", objSRN.MCC_Code, obj.Document_Date, trans)
            Try
                If isNewEntry Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.MccDispatchChallanReturn, "", objSRN.MCC_Code)
                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                'date : 28 april 2017 , changes done as per monika madam and pachraj sir.
                Dim qry1 As String = "update TSPL_TANKER_MASTER set isGateOut=1,Ref_Doc_No=NULL where Tanker_No in (select Tanker_No from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_CHALLAN_RETURN", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_CHALLAN_RETURN", OMInsertOrUpdate.Update, "TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No='" + obj.Document_No + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)

                ''Revese Inventory movement
                Dim qry As String = "select * from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='DispChallan' and Source_Doc_No='" + obj.Challan_No + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArrInventoryMovement As New List(Of clsInventoryMovementNew)
                    Dim objInvMov As clsInventoryMovementNew
                    For Each dr As DataRow In dt.Rows
                        objInvMov = New clsInventoryMovementNew
                        objInvMov.InOut = "I"
                        objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objInvMov.main_location = clsCommon.myCstr(dr("main_location"))
                        objInvMov.Other_Location_Code = clsCommon.myCstr(dr("Other_Location_Code"))
                        objInvMov.Other_Location_Desc = clsCommon.myCstr(dr("Other_Location_Desc"))
                        objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objInvMov.Qty = clsCommon.myCdbl(dr("Qty"))
                        objInvMov.UOM = clsCommon.myCstr(dr("UOM"))
                        objInvMov.MRP = clsCommon.myCdbl(dr("MRP"))
                        objInvMov.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))

                        objInvMov.FAT_Per = clsCommon.myCdbl(dr("FAT_Per"))
                        objInvMov.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                        objInvMov.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                        objInvMov.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))

                        objInvMov.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                        objInvMov.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                        objInvMov.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                        objInvMov.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                        objInvMov.ItemType = clsCommon.myCstr(dr("ItemType"))
                        objInvMov.Source_Doc_No = obj.Document_No
                        objInvMov.Source_Doc_Date = obj.Document_Date
                        objInvMov.Entry_Date = clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy")

                        objInvMov.Basic_Cost = clsCommon.myCdbl(dr("Basic_Cost"))
                        objInvMov.Rec_Cost = clsCommon.myCdbl(dr("Rec_Cost"))
                        objInvMov.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))
                        objInvMov.Net_Cost = clsCommon.myCdbl(dr("Net_Cost"))

                        objInvMov.Punching_Date = obj.Document_Date

                        objInvMov.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objInvMov.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                        objInvMov.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                        objInvMov.Avg_Cost = clsCommon.myCdbl(dr("Avg_Cost"))
                        objInvMov.Posting_Date = obj.Document_Date
                        objInvMov.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
                        objInvMov.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                        If dr("MFG_Date") IsNot DBNull.Value Then
                            objInvMov.MFG_Date = clsCommon.myCstr(dr("MFG_Date"))
                        End If
                        If dr("Expiry_Date") IsNot DBNull.Value Then
                            objInvMov.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        End If
                        objInvMov.IS_CONSUMPTION = clsCommon.myCdbl(dr("IS_CONSUMPTION"))
                        objInvMov.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                        objInvMov.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                        objInvMov.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                        objInvMov.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                        ''TEC/14/02/19-000426 by Richa on 15/02/2019
                        objInvMov.Inventory_DrAcc = clsCommon.myCstr(dr("Inventory_CrAcc"))
                        objInvMov.Inventory_CrAcc = clsCommon.myCstr(dr("Inventory_DrAcc"))
                        ArrInventoryMovement.Add(objInvMov)
                    Next
                    isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DispChallan-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                ''Journal Entry
                qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount,TSPL_JOURNAL_DETAILS.Reco_Control_Account from TSPL_JOURNAL_DETAILS " & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
                " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.Challan_No + "'  and Source_Code in ('DI-CH')"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArryLstGLAC As ArrayList = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        Dim AccCr2() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount")), "", "", "", "", "", "", clsCommon.myCstr(dr("Reco_Control_Account"))}
                        ArryLstGLAC.Add(AccCr2)

                        'Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
                        'ArryLstGLAC.Add(Acc)
                    Next
                    clsJournalMaster.FunGrnlEntryWithTrans(objSRN.MCC_Code, False, trans, obj.Document_Date, "Against MCC Dispatch Return " + obj.Document_No, "CH-RT", "MCC Dispatch Return", obj.Document_No, obj.Remarks, "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                End If
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Private Shared Function AllowToSave(ByVal obj As clsMCCDispatchReturn) As Boolean

        Dim Qry As String = "select Document_No from TSPL_MCC_DISPATCH_CHALLAN_RETURN where Challan_No='" + obj.Challan_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("SRN Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
        End If
        Qry = "select distinct Gate_Entry_No from Tspl_Gate_Entry_Details where Tspl_Gate_Entry_Details.Challan_No = '" + obj.Challan_No + "'"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Tanker Dispatch is used in Gete Entery no " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Gate_Entry_No")) + " can not return it."))
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMCCDispatchReturn
        Dim obj As clsMCCDispatchReturn = Nothing
        Dim qry As String = "SELECT TSPL_MCC_DISPATCH_CHALLAN_RETURN.* from TSPL_MCC_DISPATCH_CHALLAN_RETURN  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No = (select MIN(Document_No) from TSPL_MCC_DISPATCH_CHALLAN_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No = (select Max(Document_No) from TSPL_MCC_DISPATCH_CHALLAN_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No = (select Min(Document_No) from TSPL_MCC_DISPATCH_CHALLAN_RETURN where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No = (select Max(Document_No) from TSPL_MCC_DISPATCH_CHALLAN_RETURN where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMCCDispatchReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If
        Return obj
    End Function
End Class
Public Class clsMccTankerDispatchReturn
    Public isNewEntry As Boolean = False
    Public MCC_Code As String = String.Empty
    Public MCC_Name As String = String.Empty
    Public Dispatch_Date As Date = Nothing
    Public Return_NO As String = String.Empty
    Public Return_Date As Date = Nothing
    Public Chalan_NO As String = String.Empty
    Public Tanker_Dispatch_To As String = String.Empty
    Public Mcc_Or_Plant_Code As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public Tanker_KM_Reading As Double = 0
    Public Drip_Marking As String = String.Empty
    Public Tanker_Full As String = String.Empty
    Public Control_Sample As String = String.Empty
    Public Name_Of_Custodian As String = String.Empty
    Public Seal_No1 As String = String.Empty
    Public Seal_No2 As String = String.Empty
    Public Seal_No3 As String = String.Empty
    Public Seal_No4 As String = String.Empty
    Public Seal_No5 As String = String.Empty
    Public Seal_No6 As String = String.Empty
    Public Seal_No7 As String = String.Empty
    Public Seal_No8 As String = String.Empty
    Public Seal_No9 As String = String.Empty
    Public Seal_No10 As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public control_sample_fat As Double = 0
    Public control_sample_snf As Double = 0
    Public Net_Qty As Double = 0
    Public Transfer_Price As Double = 0
    Public Comp_Code As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modified_By As String = String.Empty
    Public Modified_Date As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public arrParmValue As List(Of Mcc_Dispatch_Chalan_Return_Parameter) = Nothing
    Public arrPaperSeal As List(Of clsMccDispatchPaperSealDetail) = Nothing
    Public Tanker_Transporter_Name As String = String.Empty
    Public Payment_Type As String = String.Empty
    Public Payment_Rate As String = String.Empty
    Public Charge_For As String = String.Empty
    Public Payment_Amount As Double = 0
    Public Chemist_Code As String = String.Empty
    Public Chemist_Name As String = String.Empty
    Public UOM_Code As String = String.Empty
    Public UOM_desc As String = String.Empty
    Public Remarks As String = String.Empty
    Public isReversed As Double = 0
    Public PriceCode As String = ""
    Public FAT_W As Double = 0
    Public SNF_W As Double = 0
    Public FAT_R As Double = 0
    Public SNF_R As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public FAT_RATE As Double = 0
    Public SNF_RATE As Double = 0
    Public Amount As Double = 0
    Public isIntermittent As Double = 0
    Public CurrentLevel As Integer = 0
    Public FinalLoc As String = ""
    Public Level1ChallanNo As String = ""
    Public ReachedAtFinal As Integer = 0
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public isBulkSaleData As Boolean = False
    Public RefDocType As String = String.Empty
    Public RefDocNo As String = String.Empty
    Public MCC_Weighment_No As String = String.Empty
    Public arr As List(Of clsMCCTankerDispatchReturnDetail) = Nothing

    Public Shared Function GetReachedAtFinalLoc(ByVal Gate_Entry_No As String, ByVal trans As SqlTransaction) As Integer
        Dim ReachedAtFinal As Integer
        Dim qry As String = " select ReachedAtFinal from tspl_mcc_dispatch_challan inner join Tspl_Gate_Entry_Details on tspl_mcc_dispatch_challan.Chalan_NO=Tspl_Gate_Entry_Details.Challan_No" & _
                            " where Tspl_Gate_Entry_Details.Gate_Entry_No='" & Gate_Entry_No & "'"
        ReachedAtFinal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return ReachedAtFinal

    End Function
    Public Shared Function getFATPer(ByVal Return_no As String, ByVal trans As SqlTransaction) As Double
        Dim fatPer As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='FAT' and Return_no='" & Return_no & "'"
        fatPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return fatPer

    End Function
    Public Shared Function getSNFPer(ByVal Return_no As String, ByVal trans As SqlTransaction) As Double
        Dim snfPer As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='SNF' and Return_no='" & Return_no & "'"
        snfPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfPer

    End Function
    Public Shared Function getCLRPer(ByVal Return_no As String, ByVal trans As SqlTransaction) As Double
        Dim clrPer As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='CLR' and Return_no='" & Return_no & "'"
        clrPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return clrPer

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_MCC_Tanker_Dispatch_Return_head.Return_No as ReturnNo,TSPL_MCC_Tanker_Dispatch_Return_head.Tanker_No as [TankerNo], TSPL_MCC_Tanker_Dispatch_Return_head.Chalan_NO as [ChallanNo],TSPL_MCC_Tanker_Dispatch_Return_head.MCC_Code as [Mcc Code] ,TSPL_MCC_Tanker_Dispatch_Return_head.MCC_Name as [Mcc Name] ,TSPL_MCC_Tanker_Dispatch_Return_head.Dispatch_Date as [Dispatch Date]  ,TSPL_MCC_Tanker_Dispatch_Return_head.Tanker_Dispatch_To as [Tanker Dispatch To] ,TSPL_MCC_Tanker_Dispatch_Return_head.Mcc_Or_Plant_Code as [Mcc Or Plant Code] , case when TSPL_MCC_Tanker_Dispatch_Return_head.isPosted=0 then 'NO' else 'YES' end as [Posting Status] ,isnull(TSPL_MCC_Tanker_Dispatch_Return_head.Tanker_KM_Reading,0) as [Tanker Km Reading] ,TSPL_MCC_Tanker_Dispatch_Return_head.Drip_Marking as [Drip Marking] ,TSPL_MCC_Tanker_Dispatch_Return_head.Tanker_Full as [Tanker Full] ,TSPL_MCC_Tanker_Dispatch_Return_head.Control_Sample as [Control Sample] ,TSPL_MCC_Tanker_Dispatch_Return_head.Name_Of_Custodian as [Name Of Custodian],TSPL_MCC_Tanker_Dispatch_Return_head.item_Code as [Item Code], TSPL_MCC_Tanker_Dispatch_Return_head.Item_Desc as [Item Description],TSPL_MCC_Tanker_Dispatch_Return_head.Tare_Weight as [Tare Weight] ,TSPL_MCC_Tanker_Dispatch_Return_head.Gross_Weight as [Gross Weight] ,TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty as [Net Qty] ,TSPL_MCC_Tanker_Dispatch_Return_head.Transfer_Price as [Transfer Price] ,  TSPL_MCC_Tanker_Dispatch_Return_head.Comp_Code as [Company Code] ,TSPL_MCC_Tanker_Dispatch_Return_head.Created_By as [Created By] ,TSPL_MCC_Tanker_Dispatch_Return_head.Created_Date as [Created Date] ,TSPL_MCC_Tanker_Dispatch_Return_head.Modified_By as [Modified By] ,TSPL_MCC_Tanker_Dispatch_Return_head.Modified_Date as [Modified Date],TSPL_MCC_Tanker_Dispatch_Return_head.RefDocType as [Ref Doc Type],TSPL_MCC_Tanker_Dispatch_Return_head.RefDocNo as [Ref Doc No]  From TSPL_MCC_Tanker_Dispatch_Return_head "
            str = clsCommon.ShowSelectForm("MCCDISPATCHRet", qry, "ReturnNo", whrcls, curcode, "ReturnNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_mcc_dispatch_challan.Tanker_No as [TankerNo] , tspl_mcc_dispatch_challan.Chalan_NO as [ChallanNo],tspl_mcc_dispatch_challan.MCC_Code as [Mcc Code] ,tspl_mcc_dispatch_challan.MCC_Name as [Mcc Name] ,tspl_mcc_dispatch_challan.Dispatch_Date as [Dispatch Date]  ,tspl_mcc_dispatch_challan.Tanker_Dispatch_To as [Tanker Dispatch To] ,tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code as [Mcc Or Plant Code] ,isnull(tspl_mcc_dispatch_challan.Tanker_KM_Reading,0) as [Tanker Km Reading] ,tspl_mcc_dispatch_challan.Drip_Marking as [Drip Marking] ,tspl_mcc_dispatch_challan.Tanker_Full as [Tanker Full] ,tspl_mcc_dispatch_challan.Control_Sample as [Control Sample] ,tspl_mcc_dispatch_challan.Name_Of_Custodian as [Name Of Custodian] ,tspl_mcc_dispatch_challan.Seal_No1 as [Seal No1] ,tspl_mcc_dispatch_challan.Seal_No2 as [Seal No2] ,tspl_mcc_dispatch_challan.Seal_No3 as [Seal No3] ,tspl_mcc_dispatch_challan.Seal_No4 as [Seal No4] ,tspl_mcc_dispatch_challan.Seal_No5 as [Seal No5] ,tspl_mcc_dispatch_challan.Seal_No6 as [Seal No6] ,tspl_mcc_dispatch_challan.Seal_No7 as [Seal No7] ,tspl_mcc_dispatch_challan.Seal_No8 as [Seal No8] ,tspl_mcc_dispatch_challan.Seal_No9 as [Seal No9] ,tspl_mcc_dispatch_challan.Seal_No10 as [Seal No10] ,tspl_mcc_dispatch_challan.item_Code as [Item Code], tspl_mcc_dispatch_challan.Item_Desc as [Item Description],tspl_mcc_dispatch_challan.Tare_Weight as [Tare Weight] ,tspl_mcc_dispatch_challan.Gross_Weight as [Gross Weight] ,tspl_mcc_dispatch_challan.Net_Qty as [Net Qty] ,tspl_mcc_dispatch_challan.Transfer_Price as [Transfer Price] ,tspl_mcc_dispatch_challan.Comp_Code as [Company Code] ,tspl_mcc_dispatch_challan.Created_By as [Created By] ,tspl_mcc_dispatch_challan.Created_Date as [Created Date] ,tspl_mcc_dispatch_challan.Modified_By as [Modified By] ,tspl_mcc_dispatch_challan.Modified_Date as [Modified Date]  From tspl_mcc_dispatch_challan "
            str = customFinder.getFinder("DispTnkrFnd", qry, whrcls, "TankerNo", curcode, "ChallanNo")
            'str = clsCommon.ShowSelectForm("MCCDISPATCH", qry, "ChallanNo", whrcls, curcode, "ChallanNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As clsMccTankerDispatchReturn, ByVal tran As SqlTransaction, Optional PostingStatus As Integer = 0, Optional chkAlreadyPosted As Boolean = True) As Boolean
        Dim PrevTanker As String = ""
        Try
            If chkAlreadyPosted Then
                If clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_MCC_Tanker_Dispatch_Return_head", "Return_NO", obj.Return_NO, "isposted=1", tran) Then
                    Throw New Exception("Document is Already Posted, Please reload the document")
                End If
            End If
            Dim issaved As Boolean = True
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Tanker Dispatch", obj.MCC_Code, obj.Dispatch_Date, tran)

            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_MCC_Tanker_Dispatch_Return_Detail where Return_NO='" + obj.Return_NO + "'", tran)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "MCC_Name", obj.MCC_Name)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Date", clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Return_Date", clsCommon.GetPrintDate(obj.Return_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Chalan_NO", obj.Chalan_NO)
            clsCommon.AddColumnsForChange(coll, "Return_NO", obj.Return_NO)
            clsCommon.AddColumnsForChange(coll, "Tanker_Dispatch_To", obj.Tanker_Dispatch_To)
            clsCommon.AddColumnsForChange(coll, "Mcc_Or_Plant_Code", obj.Mcc_Or_Plant_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Tanker_KM_Reading", obj.Tanker_KM_Reading)
            clsCommon.AddColumnsForChange(coll, "Drip_Marking", obj.Drip_Marking)
            clsCommon.AddColumnsForChange(coll, "Tanker_Full", obj.Tanker_Full)
            clsCommon.AddColumnsForChange(coll, "Control_Sample", obj.Control_Sample)
            clsCommon.AddColumnsForChange(coll, "Name_Of_Custodian", obj.Name_Of_Custodian)
            clsCommon.AddColumnsForChange(coll, "Seal_No1", obj.Seal_No1)
            clsCommon.AddColumnsForChange(coll, "Seal_No2", obj.Seal_No2)
            clsCommon.AddColumnsForChange(coll, "Seal_No3", obj.Seal_No3)
            clsCommon.AddColumnsForChange(coll, "Seal_No4", obj.Seal_No4)
            clsCommon.AddColumnsForChange(coll, "Seal_No5", obj.Seal_No5)
            clsCommon.AddColumnsForChange(coll, "Seal_No6", obj.Seal_No6)
            clsCommon.AddColumnsForChange(coll, "Seal_No7", obj.Seal_No7)
            clsCommon.AddColumnsForChange(coll, "Seal_No8", obj.Seal_No8)
            clsCommon.AddColumnsForChange(coll, "Seal_No9", obj.Seal_No9)
            clsCommon.AddColumnsForChange(coll, "Seal_No10", obj.Seal_No10)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)

            clsCommon.AddColumnsForChange(coll, "control_sample_fat", clsCommon.myCdbl(obj.control_sample_fat))
            clsCommon.AddColumnsForChange(coll, "control_sample_snf", clsCommon.myCdbl(obj.control_sample_snf))

            clsCommon.AddColumnsForChange(coll, "Net_Qty", obj.Net_Qty)
            clsCommon.AddColumnsForChange(coll, "Transfer_Price", obj.Transfer_Price)
            clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", obj.Modified_Date)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "isPosted", PostingStatus)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_Transporter_Name", obj.Tanker_Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_Type)
            clsCommon.AddColumnsForChange(coll, "Payment_Rate", obj.Payment_Rate)
            clsCommon.AddColumnsForChange(coll, "Charge_For", obj.Charge_For)
            clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Payment_Amount)
            clsCommon.AddColumnsForChange(coll, "Chemist_Code", clsCommon.myCstr(obj.Chemist_Code))
            clsCommon.AddColumnsForChange(coll, "Chemist_Name", clsCommon.myCstr(obj.Chemist_Name))
            clsCommon.AddColumnsForChange(coll, "UOM_Code", clsCommon.myCstr(obj.UOM_Code))
            clsCommon.AddColumnsForChange(coll, "UOM_desc", clsCommon.myCstr(obj.UOM_desc))
            clsCommon.AddColumnsForChange(coll, "isIntermittent", clsCommon.myCdbl(obj.isIntermittent))
            If obj.isIntermittent = 1 Then
                clsCommon.AddColumnsForChange(coll, "CurrentLevel", clsCommon.myCdbl(obj.CurrentLevel))
                'If obj.CurrentLevel > 1 Then
                clsCommon.AddColumnsForChange(coll, "Level1ChallanNo", clsCommon.myCstr(obj.Level1ChallanNo))
                'End If
                clsCommon.AddColumnsForChange(coll, "FinalLoc", clsCommon.myCstr(obj.FinalLoc))
                If clsCommon.CompairString(obj.Mcc_Or_Plant_Code, obj.FinalLoc) = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "ReachedAtFinal", 1)
                    obj.ReachedAtFinal = 1
                Else
                    obj.ReachedAtFinal = 0
                    clsCommon.AddColumnsForChange(coll, "ReachedAtFinal", 0)
                End If
            End If

            clsCommon.AddColumnsForChange(coll, "PriceCode", clsCommon.myCstr(obj.PriceCode))
            clsCommon.AddColumnsForChange(coll, "FAT_W", clsCommon.myCdbl(obj.FAT_W))
            clsCommon.AddColumnsForChange(coll, "SNF_W", clsCommon.myCdbl(obj.SNF_W))
            clsCommon.AddColumnsForChange(coll, "FAT_R", clsCommon.myCdbl(obj.FAT_R))
            clsCommon.AddColumnsForChange(coll, "SNF_R", clsCommon.myCdbl(obj.SNF_R))
            clsCommon.AddColumnsForChange(coll, "FAT_KG", clsCommon.myCdbl(obj.FAT_KG))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(obj.SNF_KG))
            clsCommon.AddColumnsForChange(coll, "FAT_RATE", clsCommon.myCdbl(obj.FAT_RATE))
            clsCommon.AddColumnsForChange(coll, "SNF_RATE", clsCommon.myCdbl(obj.SNF_RATE))
            clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.Amount))
            clsCommon.AddColumnsForChange(coll, "MCC_Weighment_No", obj.MCC_Weighment_No, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
            'If obj.isReversed = 1 Then
            clsCommon.AddColumnsForChange(coll, "isReversed", obj.isReversed)
            'End If
            clsCommon.AddColumnsForChange(coll, "RefDocType", obj.RefDocType)
            clsCommon.AddColumnsForChange(coll, "RefDocNo", obj.RefDocNo)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd-MMM-yyyy  hh:mm tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Tanker_Dispatch_Return_Head", OMInsertOrUpdate.Insert, "", tran)
            Else
                PrevTanker = clsMccTankerDispatchReturn.GetTankerNO(obj.Chalan_NO, tran)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Tanker_Dispatch_Return_Head", OMInsertOrUpdate.Update, "TSPL_MCC_Tanker_Dispatch_Return_Head.Return_NO='" + obj.Return_NO + "'", tran)

            End If
            issaved = issaved And Mcc_Dispatch_Chalan_Return_Parameter.SaveData(obj.arrParmValue, tran, obj.isReversed)
            'If obj.arrPaperSeal IsNot Nothing AndAlso obj.arrPaperSeal.Count >= 0 Then
            '    issaved = issaved And clsMccDispatchPaperSealDetail.SaveData(obj.arrPaperSeal, tran, obj.isReversed)
            'End If

            'If Not obj.isBulkSaleData Then
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(obj.Modified_By))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran, "dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "isGateOut", 1)
            clsCommon.AddColumnsForChange(coll, "Ref_Doc_No", "", True)
            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "tspl_tanker_master.tanker_no='" + obj.Tanker_No + "'", tran)
            'If clsCommon.CompairString(obj.Tanker_No, PrevTanker) <> CompairStringResult.Equal Then
            '    coll = New Hashtable()
            '    clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(obj.Modified_By))
            '    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran, "dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt"))
            '    clsCommon.AddColumnsForChange(coll, "isGateOut", 1)
            '    clsCommon.AddColumnsForChange(coll, "Ref_Doc_No", "", True)
            '    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "tspl_tanker_master.tanker_no='" + PrevTanker + "'", tran)
            'End If

            issaved = issaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Return_NO, obj.arrCustomFields, tran)
            'End If
            issaved = issaved AndAlso clsMCCTankerDispatchReturnDetail.SaveData(obj.Return_NO, obj.arr, tran)
            'UpdateProvisionAmount(obj, tran)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Shared Function UpdateProvisionAmount(ByVal obj As clsMccTankerDispatchReturn, ByVal trans As SqlTransaction) As Boolean
        Dim Actual_KM As Double = 0
        obj.Payment_Amount = 0
        Dim isPriceFromTankerMaster As Boolean = False
        Dim qry As String = "select TSPL_FREIGHT_CHARGES_MASTER.Freight_Code,TSPL_FREIGHT_CHARGES_MASTER.Status,TSPL_FREIGHT_CHARGES_MASTER.Shift_Charges,TSPL_FREIGHT_CHARGES_MASTER.Avg_Km_Ltr,TSPL_FREIGHT_CHARGES_MASTER.Diesel_Rate,TSPL_FREIGHT_CHARGES_MASTER.Price_KM,TSPL_FREIGHT_CHARGES_MASTER.Price_Ltr_KG,TSPL_FREIGHT_CHARGES_MASTER.Rate_Type,TSPL_FREIGHT_CHARGES_MASTER.Rental_Type,TSPL_FREIGHT_CHARGES_MASTER.Rental_Amount,TSPL_FREIGHT_CHARGES_MASTER.Is_Additional ,TSPL_TANKER_MASTER.Provision_Min_Qty  from TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING " + Environment.NewLine + _
        " left outer join TSPL_FREIGHT_CHARGES_MASTER on TSPL_FREIGHT_CHARGES_MASTER.Freight_Code=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Freight_Code " + Environment.NewLine + _
        " left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Tanker_No " + Environment.NewLine + _
        " where TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.MCC_Code='" + obj.MCC_Code + "' and TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Tanker_No='" + obj.Tanker_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            qry = "select '' as Freight_Code,TSPL_TANKER_MASTER.Status,TSPL_TANKER_MASTER.Shift_Charges,TSPL_TANKER_MASTER.Avg_Km_Ltr,TSPL_TANKER_MASTER.Diesel_Rate,TSPL_TANKER_MASTER.Price_KM,TSPL_TANKER_MASTER.Price_Ltr_KG,TSPL_TANKER_MASTER.Rate_Type,TSPL_TANKER_MASTER.Rental_Type,TSPL_TANKER_MASTER.Rental_Amount,0 as  Is_Additional,Provision_Min_Qty  from TSPL_TANKER_MASTER where TSPL_TANKER_MASTER.Tanker_No='" + obj.Tanker_No + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            isPriceFromTankerMaster = True
        End If

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim strICode As String = objCommonVar.DefaultMilkItemCode
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                strICode = obj.arr(0).Item_Code
            End If

            obj.Payment_Type = clsCommon.myCstr(dt.Rows(0)("Status"))
            Actual_KM = getDistance(obj.MCC_Code, obj.Mcc_Or_Plant_Code, trans)
            If Actual_KM < 0 Then
                Throw New Exception("Please map the distance between " + obj.MCC_Code + " and " + obj.Mcc_Or_Plant_Code)
            End If
            Dim NetQty As Double = clsCommon.myCdbl(dt.Rows(0)("Provision_Min_Qty"))
            If NetQty <= obj.Net_Qty Then
                NetQty = obj.Net_Qty
            End If
            If clsCommon.CompairString(obj.Payment_Type, "Day/Diesel") = CompairStringResult.Equal Then
                If Actual_KM < 0 Then
                    Throw New Exception("Please map the distance between " + obj.MCC_Code + " and " + obj.Mcc_Or_Plant_Code)
                End If
                obj.Payment_Amount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Shift_Charges")) + ((Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))) / clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))), 2, MidpointRounding.ToEven)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "Rate/K.M") = CompairStringResult.Equal Then
                If Actual_KM < 0 Then
                    Throw New Exception("Please map the distance between " + obj.MCC_Code + " and " + obj.Mcc_Or_Plant_Code)
                End If
                obj.Payment_Amount = Math.Round(Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Price_KM")), 2, MidpointRounding.ToEven)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "Rate/Ltr") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rate_Type")), "LTR") = CompairStringResult.Equal Then
                    Dim convFactor As Double = clsWeightConversionInfo.GetWeightConverionFactorMilkType(strICode, "KG", "LTR", trans)
                    obj.Payment_Amount = Math.Round((NetQty * clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")) * convFactor), 2, MidpointRounding.ToEven)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rate_Type")), "KG") = CompairStringResult.Equal Then
                    obj.Payment_Amount = Math.Round((NetQty * clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG"))), 2, MidpointRounding.ToEven)
                Else
                    Throw New Exception("Wrong Rate Type of " + obj.Payment_Type + " and Tanker no " + obj.Tanker_No)
                End If
            ElseIf clsCommon.CompairString(obj.Payment_Type, "Rental") = CompairStringResult.Equal Then
                Dim Days As Integer = 0
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Year") = CompairStringResult.Equal Then
                    Days = IIf(DateTime.IsLeapYear(obj.Dispatch_Date.Year), 366, 365)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Month") = CompairStringResult.Equal Then
                    Days = System.DateTime.DaysInMonth(obj.Dispatch_Date.Year, obj.Dispatch_Date.Month)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Day") = CompairStringResult.Equal Then
                    Days = 1
                Else
                    Throw New Exception("Wrong Rental Type of " + obj.Payment_Type + " and Tanker no " + obj.Tanker_No)
                End If
                obj.Payment_Amount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Rental_Amount") / (Days * 2)), 2, MidpointRounding.ToEven)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "Rental/Diesel") = CompairStringResult.Equal Then
                If Actual_KM < 0 Then
                    Throw New Exception("Please map the distance between " + obj.MCC_Code + " and " + obj.Mcc_Or_Plant_Code)
                End If
                obj.Payment_Amount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Rental_Amount") / (System.DateTime.DaysInMonth(obj.Dispatch_Date.Year, obj.Dispatch_Date.Month) * 2)), 2, MidpointRounding.ToEven)
                obj.Payment_Amount += Math.Round(((Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))) / clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))), 2, MidpointRounding.ToEven)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "KM_Range") = CompairStringResult.Equal Then
                If Actual_KM < 0 Then
                    Throw New Exception("Please map the distance between " + obj.MCC_Code + " and " + obj.Mcc_Or_Plant_Code)
                End If
                'Today do work
                obj.Payment_Amount = 0
                Dim dblRemainingKM As Double = Actual_KM
                If isPriceFromTankerMaster Then
                    qry = "select Slab_Upto,Slab_Rate from tspl_slab_range_detail where Trans_ID='" + obj.Tanker_No + "' and Form_ID='" + clsUserMgtCode.frmTankerMaster + "' order by Slab_Upto desc"
                Else
                    qry = "select Slab_Upto,Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where  Freight_Code= '" + clsCommon.myCstr(dt.Rows(0)("Freight_Code")) + "' order by Slab_Upto desc"
                End If
                Dim dtSlab As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
                    If dtSlab.Rows.Count = 1 Then
                        obj.Payment_Amount = Math.Round((clsCommon.myCdbl(dtSlab.Rows(0)("Slab_Rate")) * (Actual_KM)), 2, MidpointRounding.ToEven)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Is_Additional")), "T") = CompairStringResult.Equal Then
                        For ii As Integer = 0 To dtSlab.Rows.Count - 1
                            Dim previousRange As Double = 0
                            If (dtSlab.Rows.Count - (ii + 1)) > 0 Then
                                previousRange = clsCommon.myCdbl(dtSlab.Rows(ii + 1)("Slab_Upto"))
                            End If
                            If dblRemainingKM >= clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Upto")) Then
                                obj.Payment_Amount += (clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Rate")) * (dblRemainingKM))
                                Exit For
                            ElseIf dblRemainingKM > previousRange AndAlso dblRemainingKM <= clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Upto")) Then
                                obj.Payment_Amount += (clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Rate")) * (dblRemainingKM - previousRange))
                                dblRemainingKM = previousRange
                            End If
                        Next
                        obj.Payment_Amount = Math.Round(obj.Payment_Amount, 2, MidpointRounding.ToEven)
                    Else
                        For Each drSlab As DataRow In dtSlab.Rows
                            If Actual_KM >= clsCommon.myCdbl(drSlab("Slab_Upto")) Then
                                obj.Payment_Amount = Math.Round((clsCommon.myCdbl(drSlab("Slab_Rate")) * (Actual_KM)), 2, MidpointRounding.ToEven)
                                Exit For
                            End If
                        Next
                    End If
                End If
            Else
                Throw New Exception("Wrong method " + obj.Payment_Type + " for Tanker No " + obj.Tanker_No)
            End If
        End If

        obj.Payment_Amount = Math.Round(obj.Payment_Amount, 2, MidpointRounding.ToEven)
        obj.Payment_Rate = IIf(Actual_KM = 0, 0, Math.Round(obj.Payment_Amount / Actual_KM, 3, MidpointRounding.AwayFromZero))


        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_Type)
        clsCommon.AddColumnsForChange(coll, "Payment_Rate", obj.Payment_Rate)
        clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Payment_Amount)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_CHALLAN", OMInsertOrUpdate.Update, "TSPL_MCC_DISPATCH_CHALLAN.CHALAN_NO='" + obj.Chalan_NO + "'", trans)

        Return True
    End Function
    Shared Function getDistance(ByVal fromLoc As String, ByVal toLoc As String, ByVal tran As SqlTransaction) As Double
        Dim Distance As Double = 0
        Dim qry As String = " select Distance  from tspl_location_distance_master  where (From_Location_code ='" & fromLoc & "' and to_Location_Code ='" & toLoc & "' ) or (From_Location_code ='" & toLoc & "' and to_Location_Code ='" & fromLoc & "' ) "
        Distance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran))
        If Distance = 0 Then
            Distance = -1
        End If
        Return Distance
    End Function
    Public Shared Function CheckTankerGateOut(Tanker_No As String, ByVal Ref_Doc_No As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = ""
        qry = "select Ref_Doc_No from tspl_tanker_master where  tanker_no='" & Tanker_No & "' "
        Dim Ref_Doc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        If clsCommon.myLen(Ref_Doc) <= 0 Then
            Return True
        ElseIf clsCommon.CompairString(Ref_Doc, Ref_Doc_No) = CompairStringResult.Equal Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function GetTankerNO(Return_No As String, Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        Try
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_No  from TSPL_MCC_Tanker_Dispatch_Return_Head  where Return_No='" & Return_No & "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue

    End Function
    Public Shared Sub UpdateTankerStatus(strTankerNo As String, status As Integer)
        Dim coll As Hashtable
        coll = New Hashtable
        clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "isGateOut", status)
        clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "tspl_tanker_master.tanker_no='" + strTankerNo + "'", Nothing)
    End Sub
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsMccTankerDispatchReturn
        Return getData(strCode, navtype, Nothing)
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal trans As SqlTransaction) As clsMccTankerDispatchReturn
        Dim obj As New clsMccTankerDispatchReturn
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strCode) <= 0 Then
                    whrCls = " and mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Return_Parameter)
            Dim qst As String = " select *   From TSPL_MCC_Tanker_Dispatch_Return_Head   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_Head.Return_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_Head.Return_No in (select min(Return_No ) from TSPL_MCC_Tanker_Dispatch_Return_Head where Return_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_Head.Return_No in (select MIN(Return_No ) from TSPL_MCC_Tanker_Dispatch_Return_Head where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_Head.Return_No in (select Max(Return_No ) from TSPL_MCC_Tanker_Dispatch_Return_Head where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_Head.Return_No in (select Max(Return_No ) from TSPL_MCC_Tanker_Dispatch_Return_Head where Return_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
                obj.Dispatch_Date = clsCommon.myCDate(dt.Rows(0)("Dispatch_Date"))
                obj.Return_Date = clsCommon.myCDate(dt.Rows(0)("Return_Date"))
                obj.Chalan_NO = clsCommon.myCstr(dt.Rows(0)("Chalan_NO"))
                obj.Return_NO = clsCommon.myCstr(dt.Rows(0)("Return_No"))
                obj.Tanker_Dispatch_To = clsCommon.myCstr(dt.Rows(0)("Tanker_Dispatch_To"))
                obj.Mcc_Or_Plant_Code = clsCommon.myCstr(dt.Rows(0)("Mcc_Or_Plant_Code"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                ''=============Richa Ticket No. BM00000003712 on 08/09/2014
                obj.Tanker_KM_Reading = clsCommon.myCdbl(dt.Rows(0)("Tanker_KM_Reading"))
                ''===============================================

                obj.Drip_Marking = clsCommon.myCstr(dt.Rows(0)("Drip_Marking"))
                obj.Tanker_Full = clsCommon.myCstr(dt.Rows(0)("Tanker_Full"))
                obj.Control_Sample = clsCommon.myCstr(dt.Rows(0)("Control_Sample"))
                obj.Name_Of_Custodian = clsCommon.myCstr(dt.Rows(0)("Name_Of_Custodian"))
                obj.Seal_No1 = clsCommon.myCstr(dt.Rows(0)("Seal_No1"))
                obj.Seal_No2 = clsCommon.myCstr(dt.Rows(0)("Seal_No2"))
                obj.Seal_No3 = clsCommon.myCstr(dt.Rows(0)("Seal_No3"))
                obj.Seal_No4 = clsCommon.myCstr(dt.Rows(0)("Seal_No4"))
                obj.Seal_No5 = clsCommon.myCstr(dt.Rows(0)("Seal_No5"))
                obj.Seal_No6 = clsCommon.myCstr(dt.Rows(0)("Seal_No6"))
                obj.Seal_No7 = clsCommon.myCstr(dt.Rows(0)("Seal_No7"))
                obj.Seal_No8 = clsCommon.myCstr(dt.Rows(0)("Seal_No8"))
                obj.Seal_No9 = clsCommon.myCstr(dt.Rows(0)("Seal_No9"))
                obj.Seal_No10 = clsCommon.myCstr(dt.Rows(0)("Seal_No10"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))

                obj.control_sample_fat = clsCommon.myCdbl(dt.Rows(0)("control_sample_fat"))
                obj.control_sample_snf = clsCommon.myCdbl(dt.Rows(0)("control_sample_snf"))

                obj.Net_Qty = clsCommon.myCdbl(dt.Rows(0)("Net_Qty"))
                obj.Transfer_Price = clsCommon.myCdbl(dt.Rows(0)("Transfer_Price"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))

                obj.UOM_Code = clsCommon.myCstr(dt.Rows(0)("Uom_Code"))
                obj.UOM_desc = clsCommon.myCstr(dt.Rows(0)("Uom_Desc"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Tanker_Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Tanker_Transporter_Name"))
                obj.Payment_Type = clsCommon.myCstr(dt.Rows(0)("Payment_Type"))
                obj.Payment_Rate = clsCommon.myCstr(dt.Rows(0)("Payment_Rate"))
                obj.Charge_For = clsCommon.myCstr(dt.Rows(0)("Charge_For"))
                obj.Payment_Amount = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
                obj.Chemist_Code = clsCommon.myCstr(dt.Rows(0)("Chemist_Code"))
                obj.Chemist_Name = clsCommon.myCstr(dt.Rows(0)("Chemist_Name"))
                obj.isIntermittent = clsCommon.myCdbl(dt.Rows(0)("isIntermittent"))
                obj.ReachedAtFinal = clsCommon.myCdbl(dt.Rows(0)("ReachedAtFinal"))
                If obj.isIntermittent = 1 Then
                    obj.CurrentLevel = clsCommon.myCdbl(dt.Rows(0)("CurrentLevel"))
                    'If obj.CurrentLevel > 1 Then
                    obj.Level1ChallanNo = clsCommon.myCstr(dt.Rows(0)("Level1ChallanNo"))
                    'End If
                    obj.FinalLoc = clsCommon.myCstr(dt.Rows(0)("FinalLoc"))
                End If

                obj.PriceCode = clsCommon.myCstr(dt.Rows(0)("PriceCode"))
                obj.FAT_W = clsCommon.myCdbl(dt.Rows(0)("FAT_W"))
                obj.FAT_R = clsCommon.myCdbl(dt.Rows(0)("FAT_R"))
                obj.FAT_KG = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
                obj.FAT_RATE = clsCommon.myCdbl(dt.Rows(0)("FAT_RATE"))
                obj.SNF_W = clsCommon.myCdbl(dt.Rows(0)("SNF_W"))
                obj.SNF_R = clsCommon.myCdbl(dt.Rows(0)("SNF_R"))
                obj.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                obj.SNF_RATE = clsCommon.myCdbl(dt.Rows(0)("SNF_RATE"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.RefDocType = clsCommon.myCstr(dt.Rows(0)("RefDocType"))
                obj.RefDocNo = clsCommon.myCstr(dt.Rows(0)("RefDocNo"))
                obj.isBulkSaleData = IIf(clsCommon.CompairString(obj.RefDocType, clsUserMgtCode.FrmBulkSaleReturn) = CompairStringResult.Equal, True, False)
                obj.MCC_Weighment_No = clsCommon.myCstr(dt.Rows(0)("MCC_Weighment_No"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.arrParmValue = Mcc_Dispatch_Chalan_Return_Parameter.getData(obj.Return_NO, trans)
                obj.arrPaperSeal = clsMccDispatchPaperSealDetail.getData(obj.Return_NO, trans)
                obj.arr = clsMCCTankerDispatchReturnDetail.GetData(obj.Return_NO, trans)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getDataDispatchReturn(ByVal strCode As String, ByVal navtype As NavigatorType) As clsMccTankerDispatchReturn
        Return getData(strCode, navtype, Nothing)
    End Function
    Public Shared Function getDataDispatchReturn(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal trans As SqlTransaction) As clsMccTankerDispatchReturn
        Dim obj As New clsMccTankerDispatchReturn
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strCode) <= 0 Then
                    whrCls = " and mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Return_Parameter)
            Dim qst As String = " select *   From TSPL_MCC_Tanker_Dispatch_Return_head   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_head.Return_no in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_head.Return_no in (select min(Return_no ) from TSPL_MCC_Tanker_Dispatch_Return_head where Return_no  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_head.Return_no in (select MIN(Return_no ) from TSPL_MCC_Tanker_Dispatch_Return_head where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_head.Return_no in (select Max(Return_no ) from TSPL_MCC_Tanker_Dispatch_Return_head where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_MCC_Tanker_Dispatch_Return_head.Return_no in (select Max(Return_no ) from TSPL_MCC_Tanker_Dispatch_Return_head where Return_no  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
                obj.Dispatch_Date = clsCommon.myCDate(dt.Rows(0)("Dispatch_Date"))
                obj.Return_Date = clsCommon.myCDate(dt.Rows(0)("Return_Date"))
                obj.Chalan_NO = clsCommon.myCstr(dt.Rows(0)("Chalan_NO"))
                obj.Return_NO = clsCommon.myCstr(dt.Rows(0)("Return_NO"))
                obj.Tanker_Dispatch_To = clsCommon.myCstr(dt.Rows(0)("Tanker_Dispatch_To"))
                obj.Mcc_Or_Plant_Code = clsCommon.myCstr(dt.Rows(0)("Mcc_Or_Plant_Code"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                ''=============Richa Ticket No. BM00000003712 on 08/09/2014
                obj.Tanker_KM_Reading = clsCommon.myCdbl(dt.Rows(0)("Tanker_KM_Reading"))
                ''===============================================

                obj.Drip_Marking = clsCommon.myCstr(dt.Rows(0)("Drip_Marking"))
                obj.Tanker_Full = clsCommon.myCstr(dt.Rows(0)("Tanker_Full"))
                obj.Control_Sample = clsCommon.myCstr(dt.Rows(0)("Control_Sample"))
                obj.Name_Of_Custodian = clsCommon.myCstr(dt.Rows(0)("Name_Of_Custodian"))
                obj.Seal_No1 = clsCommon.myCstr(dt.Rows(0)("Seal_No1"))
                obj.Seal_No2 = clsCommon.myCstr(dt.Rows(0)("Seal_No2"))
                obj.Seal_No3 = clsCommon.myCstr(dt.Rows(0)("Seal_No3"))
                obj.Seal_No4 = clsCommon.myCstr(dt.Rows(0)("Seal_No4"))
                obj.Seal_No5 = clsCommon.myCstr(dt.Rows(0)("Seal_No5"))
                obj.Seal_No6 = clsCommon.myCstr(dt.Rows(0)("Seal_No6"))
                obj.Seal_No7 = clsCommon.myCstr(dt.Rows(0)("Seal_No7"))
                obj.Seal_No8 = clsCommon.myCstr(dt.Rows(0)("Seal_No8"))
                obj.Seal_No9 = clsCommon.myCstr(dt.Rows(0)("Seal_No9"))
                obj.Seal_No10 = clsCommon.myCstr(dt.Rows(0)("Seal_No10"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))

                obj.control_sample_fat = clsCommon.myCdbl(dt.Rows(0)("control_sample_fat"))
                obj.control_sample_snf = clsCommon.myCdbl(dt.Rows(0)("control_sample_snf"))

                obj.Net_Qty = clsCommon.myCdbl(dt.Rows(0)("Net_Qty"))
                obj.Transfer_Price = clsCommon.myCdbl(dt.Rows(0)("Transfer_Price"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))

                obj.UOM_Code = clsCommon.myCstr(dt.Rows(0)("Uom_Code"))
                obj.UOM_desc = clsCommon.myCstr(dt.Rows(0)("Uom_Desc"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Tanker_Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Tanker_Transporter_Name"))
                obj.Payment_Type = clsCommon.myCstr(dt.Rows(0)("Payment_Type"))
                obj.Payment_Rate = clsCommon.myCstr(dt.Rows(0)("Payment_Rate"))
                obj.Charge_For = clsCommon.myCstr(dt.Rows(0)("Charge_For"))
                obj.Payment_Amount = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
                obj.Chemist_Code = clsCommon.myCstr(dt.Rows(0)("Chemist_Code"))
                obj.Chemist_Name = clsCommon.myCstr(dt.Rows(0)("Chemist_Name"))
                obj.isIntermittent = clsCommon.myCdbl(dt.Rows(0)("isIntermittent"))
                obj.ReachedAtFinal = clsCommon.myCdbl(dt.Rows(0)("ReachedAtFinal"))
                If obj.isIntermittent = 1 Then
                    obj.CurrentLevel = clsCommon.myCdbl(dt.Rows(0)("CurrentLevel"))
                    'If obj.CurrentLevel > 1 Then
                    obj.Level1ChallanNo = clsCommon.myCstr(dt.Rows(0)("Level1ChallanNo"))
                    'End If
                    obj.FinalLoc = clsCommon.myCstr(dt.Rows(0)("FinalLoc"))
                End If

                obj.PriceCode = clsCommon.myCstr(dt.Rows(0)("PriceCode"))
                obj.FAT_W = clsCommon.myCdbl(dt.Rows(0)("FAT_W"))
                obj.FAT_R = clsCommon.myCdbl(dt.Rows(0)("FAT_R"))
                obj.FAT_KG = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
                obj.FAT_RATE = clsCommon.myCdbl(dt.Rows(0)("FAT_RATE"))
                obj.SNF_W = clsCommon.myCdbl(dt.Rows(0)("SNF_W"))
                obj.SNF_R = clsCommon.myCdbl(dt.Rows(0)("SNF_R"))
                obj.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                obj.SNF_RATE = clsCommon.myCdbl(dt.Rows(0)("SNF_RATE"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.RefDocType = clsCommon.myCstr(dt.Rows(0)("RefDocType"))
                obj.RefDocNo = clsCommon.myCstr(dt.Rows(0)("RefDocNo"))
                obj.isBulkSaleData = IIf(clsCommon.CompairString(obj.RefDocType, clsUserMgtCode.FrmBulkSaleReturn) = CompairStringResult.Equal, True, False)
                obj.MCC_Weighment_No = clsCommon.myCstr(dt.Rows(0)("MCC_Weighment_No"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.arrParmValue = Mcc_Dispatch_Chalan_Return_Parameter.getData(obj.Chalan_NO, trans)
                obj.arrPaperSeal = clsMccDispatchPaperSealDetail.getData(obj.Chalan_NO, trans)
                obj.arr = clsMCCTankerDispatchReturnDetail.GetData(obj.Chalan_NO, trans)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select isPosted from TSPL_MCC_Tanker_Dispatch_Return_Head where Return_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            'Qry = "select Gate_Entry_No  from Tspl_Gate_Entry_Details  where Doc_Type='MccProc' and Challan_No='" + strCode + "'"
            'Dim gtno As String = clsDBFuncationality.getSingleValue(Qry, trans)
            'If clsCommon.myLen(gtno) > 0 Then
            '    Qry = "CURRENT Challan IS USED IN Gate Entry-" & gtno
            '    Throw New Exception(Qry)
            'End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='DS-RT' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No='" + strCode + "' and Trans_Type='DispChallanRet'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_MCC_Tanker_Dispatch_Return_Head set isPosted = 0,isreversed='1' where Return_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim tnkrNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_No  from TSPL_MCC_Tanker_Dispatch_Return_Head where Return_No='" & strCode & "'", trans))
            Qry = "Update tspl_tanker_master set isGateOut = 0,Ref_Doc_No='" & strCode & "' where tanker_no='" + tnkrNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isChildTrans As Boolean = False
      
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Return No not found to Post")
            End If
            If trans Is Nothing Then
                trans = clsDBFuncationality.GetTransactin()
                isChildTrans = True
            Else
                isChildTrans = False
            End If
            Dim obj As clsMccTankerDispatchReturn = clsMccTankerDispatchReturn.getData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Return_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement", "MCC Dispatch Return", obj.MCC_Code, obj.Return_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_MCC_Tanker_Dispatch_Return_Head", "Return_No", obj.Return_NO, trans)
            If isResult = False Then
                If isChildTrans Then
                    trans.Commit()
                End If
                Return False
            End If

            For Each objtr As clsMCCTankerDispatchReturnDetail In obj.arr

                Dim qry As String = ""
                Dim totalamt As Double = 0
                Dim amtdifference As Double = 0
                Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "A"
                Else
                    strItemTypeToSave = strItemType
                End If
                Dim strItemUnitCode As String = objtr.Item_UOM



                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                ''richa agarwal 06/09/2016 code for going data into inventory movement
                objInventoryMovemnt.InOut = "I"
                Dim strsublocation As String = String.Empty
                '' sub location should be vitual type in case of intermettent else silo should be physical type
                'If clsCommon.myLen(obj.Level1ChallanNo) > 0 AndAlso obj.isIntermittent = 1 AndAlso obj.CurrentLevel > 1 Then
                '    strsublocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(obj.Level1ChallanNo) & "'  and CurrentLevel =" & clsCommon.myCdbl(obj.CurrentLevel) - 1 & " and isnull(isIntermittent,0) =1))", trans))
                'Else
                '    strsublocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(obj.MCC_Code) & "' and Location_Type='Physical'", trans))
                'End If
                strsublocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='DispChallan' and Source_Doc_No='" & obj.Chalan_NO & "'", trans))
                objInventoryMovemnt.Location_Code = strsublocation
                objInventoryMovemnt.Other_Location_Code = obj.Mcc_Or_Plant_Code
                objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)

                objInventoryMovemnt.Item_Code = objtr.Item_Code
                objInventoryMovemnt.Item_Desc = objtr.Item_Name

                'Dim balqtyofvl As Double = 0
                'If clsCommon.myLen(strsublocation) > 0 Then
                '    balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MCC_Code, strsublocation, obj.Chalan_NO, obj.Dispatch_Date, trans, "KG"))
                'End If

                'If balqtyofvl = 0 Then

                'If balqtyofvl >= obj.Net_Qty Then
                '    balqtyofvl = obj.Net_Qty
                'End If
                objInventoryMovemnt.Qty = objtr.Qty_KG  'Net_Qty
                objInventoryMovemnt.UOM = strItemUnitCode
                objInventoryMovemnt.MRP = 0
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.FAT_Per = clsMccTankerDispatchReturn.getFATPerChamberWise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.SNF_Per = clsMccTankerDispatchReturn.getSNFPerChamberWise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.FAT_KG = clsMccTankerDispatchReturn.getFATKGChamberWise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.SNF_KG = clsMccTankerDispatchReturn.getSNFKGChamberWise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.Net_Cost = clsMccTankerDispatchReturn.getTransferAmountChamberwise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.Fat_Rate = clsMccTankerDispatchReturn.getFATRateChamberWise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.SNF_Rate = clsMccTankerDispatchReturn.getSNFRateChamberWise(obj.Return_NO, trans, objtr.SNo)
                objInventoryMovemnt.Fat_Amt = clsCommon.myFormat(objInventoryMovemnt.Fat_Rate * objInventoryMovemnt.FAT_KG)
                objInventoryMovemnt.SNF_Amt = clsCommon.myFormat(objInventoryMovemnt.SNF_Rate * objInventoryMovemnt.SNF_KG)
                totalamt = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt 'done by stuti on 12/05/2017
                amtdifference = objInventoryMovemnt.Net_Cost - totalamt
                objInventoryMovemnt.SNF_Amt += amtdifference
                'If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                '    objInventoryMovemnt.FAT_Per = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.FAT_KG * 100 / obj.Net_Qty), 2, MidpointRounding.ToEven)
                '    objInventoryMovemnt.SNF_Per = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.SNF_KG * 100 / obj.Net_Qty), 2, MidpointRounding.ToEven)
                '    objInventoryMovemnt.FAT_KG = obj.FAT_KG * obj.Net_Qty / 100
                '    objInventoryMovemnt.SNF_KG = obj.SNF_KG * obj.Net_Qty / 100
                'Else
                '    objInventoryMovemnt.FAT_Per = clsMccTankerDispatchReturn.getFATPer(obj.Return_NO, trans)
                '    objInventoryMovemnt.SNF_Per = clsMccTankerDispatchReturn.getSNFPer(obj.Return_NO, trans)
                '    objInventoryMovemnt.FAT_KG = clsMccTankerDispatchReturn.getFATPer(obj.Return_NO, trans) * obj.Net_Qty / 100
                '    objInventoryMovemnt.SNF_KG = clsMccTankerDispatchReturn.getSNFPer(obj.Return_NO, trans) * obj.Net_Qty / 100
                'End If

                'objInventoryMovemnt.Net_Cost = Math.Round((obj.FAT_RATE * objInventoryMovemnt.FAT_KG) + (obj.SNF_RATE * objInventoryMovemnt.SNF_KG), 2)
                'objInventoryMovemnt.Fat_Rate = obj.FAT_RATE
                'objInventoryMovemnt.SNF_Rate = obj.SNF_RATE
                'objInventoryMovemnt.Fat_Amt = clsCommon.myFormat(obj.FAT_RATE * objInventoryMovemnt.FAT_KG)
                'objInventoryMovemnt.SNF_Amt = clsCommon.myFormat(obj.SNF_RATE * objInventoryMovemnt.SNF_KG)

                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                'If (balqtyofvl) = 0 Then
                '    Throw New Exception("You cannot post this entry because basic cost is not calcualted")
                'End If
                objInventoryMovemnt.Basic_Cost = IIf(objInventoryMovemnt.Qty = 0, 0, objInventoryMovemnt.Net_Cost / objInventoryMovemnt.Qty) 'objInventoryMovemnt.Net_Cost / IIf(obj.Net_Qty = 0, 1, obj.Net_Qty)
                ArrInventoryMovement.Add(objInventoryMovemnt)


                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DispChallanRet", obj.Return_NO, obj.Return_Date, clsCommon.GetPrintDate(obj.Return_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            Next

            'old code comment start----------

            'Dim qry As String = ""
            'Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            'Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
            'Dim strItemTypeToSave As String = ""
            'If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "RM"
            'ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "OT"
            'ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "FT"
            'ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "A"
            'Else
            '    strItemTypeToSave = strItemType
            'End If
            'Dim strItemUnitCode As String = obj.UOM_Code



            'Dim objInventoryMovemnt As New clsInventoryMovementNew()
            ' ''richa agarwal 06/09/2016 code for going data into inventory movement
            'objInventoryMovemnt.InOut = "I"
            'Dim strsublocation As String = String.Empty
            ' '' sub location should be vitual type in case of intermettent else silo should be physical type
            ''If clsCommon.myLen(obj.Level1ChallanNo) > 0 AndAlso obj.isIntermittent = 1 AndAlso obj.CurrentLevel > 1 Then
            ''    strsublocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(obj.Level1ChallanNo) & "'  and CurrentLevel =" & clsCommon.myCdbl(obj.CurrentLevel) - 1 & " and isnull(isIntermittent,0) =1))", trans))
            ''Else
            ''    strsublocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(obj.MCC_Code) & "' and Location_Type='Physical'", trans))
            ''End If
            'strsublocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='DispChallan' and Source_Doc_No='" & obj.Chalan_NO & "'", trans))
            'objInventoryMovemnt.Location_Code = strsublocation
            'objInventoryMovemnt.Other_Location_Code = obj.Mcc_Or_Plant_Code
            'objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)

            'objInventoryMovemnt.Item_Code = obj.Item_Code
            'objInventoryMovemnt.Item_Desc = obj.Item_Desc

            ''Dim balqtyofvl As Double = 0
            ''If clsCommon.myLen(strsublocation) > 0 Then
            ''    balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MCC_Code, strsublocation, obj.Chalan_NO, obj.Dispatch_Date, trans, "KG"))
            ''End If

            ''If balqtyofvl = 0 Then

            ''If balqtyofvl >= obj.Net_Qty Then
            ''    balqtyofvl = obj.Net_Qty
            ''End If
            'objInventoryMovemnt.Qty = obj.Net_Qty
            'objInventoryMovemnt.UOM = strItemUnitCode
            'objInventoryMovemnt.MRP = 0
            'objInventoryMovemnt.Add_Cost = 0
            'If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
            '    objInventoryMovemnt.FAT_Per = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.FAT_KG * 100 / obj.Net_Qty), 2, MidpointRounding.ToEven)
            '    objInventoryMovemnt.SNF_Per = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.SNF_KG * 100 / obj.Net_Qty), 2, MidpointRounding.ToEven)
            '    objInventoryMovemnt.FAT_KG = obj.FAT_KG * obj.Net_Qty / 100
            '    objInventoryMovemnt.SNF_KG = obj.SNF_KG * obj.Net_Qty / 100
            'Else
            '    objInventoryMovemnt.FAT_Per = clsMccTankerDispatchReturn.getFATPer(obj.Return_NO, trans)
            '    objInventoryMovemnt.SNF_Per = clsMccTankerDispatchReturn.getSNFPer(obj.Return_NO, trans)
            '    objInventoryMovemnt.FAT_KG = clsMccTankerDispatchReturn.getFATPer(obj.Return_NO, trans) * obj.Net_Qty / 100
            '    objInventoryMovemnt.SNF_KG = clsMccTankerDispatchReturn.getSNFPer(obj.Return_NO, trans) * obj.Net_Qty / 100
            'End If

            'objInventoryMovemnt.Net_Cost = Math.Round((obj.FAT_RATE * objInventoryMovemnt.FAT_KG) + (obj.SNF_RATE * objInventoryMovemnt.SNF_KG), 2)
            'objInventoryMovemnt.Fat_Rate = obj.FAT_RATE
            'objInventoryMovemnt.SNF_Rate = obj.SNF_RATE
            'objInventoryMovemnt.Fat_Amt = clsCommon.myFormat(obj.FAT_RATE * objInventoryMovemnt.FAT_KG)
            'objInventoryMovemnt.SNF_Amt = clsCommon.myFormat(obj.SNF_RATE * objInventoryMovemnt.SNF_KG)
            'If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '    objInventoryMovemnt.ItemType = "RM"
            'ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '    objInventoryMovemnt.ItemType = "OT"
            'ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '    objInventoryMovemnt.ItemType = "FT"
            'End If
            'objInventoryMovemnt.ItemType = strItemTypeToSave
            ''If (balqtyofvl) = 0 Then
            ''    Throw New Exception("You cannot post this entry because basic cost is not calcualted")
            ''End If
            'objInventoryMovemnt.Basic_Cost = objInventoryMovemnt.Net_Cost / IIf(obj.Net_Qty = 0, 1, obj.Net_Qty)
            'ArrInventoryMovement.Add(objInventoryMovemnt)


            'isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DispChallanRet", obj.Return_NO, obj.Return_Date, clsCommon.GetPrintDate(obj.Return_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)


            'old code comment end----------







            'isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DispChallanRet", obj.Chalan_NO, obj.Dispatch_Date, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
            'End If
            'End If

            ''-------------------------- end of inventory movement 


            'Dim strItemCode As String = String.Empty
            'strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='PS' ", trans))
            'Dim SealQty As Integer = 0
            'SealQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where chalan_no='" & obj.Chalan_NO & "' ", trans))
            'If SealQty > 0 Then
            '    Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            '    Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            '    strItemType = clsItemMaster.GetItemType(strItemCode, trans)
            '    'Dim strItemTypeToSave As String = ""
            '    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "RM"
            '    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "OT"
            '    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "FT"
            '    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "A"
            '    Else
            '        strItemTypeToSave = strItemType
            '        'Throw New Exception("Item Type not found: " + strItemType)
            '    End If
            '    strItemUnitCode = clsItemMaster.GetStockUnit(strItemCode, trans)

            '    Dim objInventoryMovemnt1 As clsInventoryMovement = New clsInventoryMovement()
            '    objInventoryMovemnt1.InOut = "O"
            '    objInventoryMovemnt1.Location_Code = obj.MCC_Code
            '    objInventoryMovemnt1.Other_Location_Code = obj.Mcc_Or_Plant_Code
            '    objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
            '    objInventoryMovemnt1.Item_Code = strItemCode
            '    objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
            '    objInventoryMovemnt1.Qty = SealQty
            '    objInventoryMovemnt1.UOM = strItemUnitCode
            '    objInventoryMovemnt1.MRP = 0
            '    objInventoryMovemnt1.Add_Cost = 0
            '    objInventoryMovemnt1.Net_Cost = 0
            '    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '        objInventoryMovemnt1.ItemType = "RM"
            '    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '        objInventoryMovemnt1.ItemType = "OT"
            '    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '        objInventoryMovemnt1.ItemType = "FT"
            '    End If
            '    objInventoryMovemnt1.ItemType = strItemTypeToSave
            '    objInventoryMovemnt1.Basic_Cost = 0
            '    ArrInventoryMovement1.Add(objInventoryMovemnt1)
            '    If Not obj.isBulkSaleData Then
            '        isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrLocationDetails1, trans)
            '        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("DispChallanRet", obj.Chalan_NO, obj.Dispatch_Date, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
            '    End If
            'End If

            'strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='MS' ", trans))
            'SealQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select COUNT(*) from(select Seal_No1 as seal_No,Chalan_NO   from (select Seal_No1,Chalan_NO   from TSPL_MCC_Dispatch_Challan union all select Seal_No2,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No3,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No4,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No5,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No6,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No7,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No8,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No9,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No10,Chalan_NO  from TSPL_MCC_Dispatch_Challan) xx where Seal_No1 <>'' ) yyy where yyy.Chalan_NO ='" & obj.Chalan_NO & "'", trans))
            'If SealQty > 0 Then
            '    Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            '    Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            '    strItemType = clsItemMaster.GetItemType(strItemCode, trans)
            '    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "RM"
            '    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "OT"
            '    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "FT"
            '    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
            '        strItemTypeToSave = "A"
            '    Else
            '        strItemTypeToSave = strItemType
            '    End If
            '    strItemUnitCode = clsItemMaster.GetStockUnit(strItemCode, trans)

            '    Dim objInventoryMovemnt1 As clsInventoryMovement = New clsInventoryMovement()
            '    objInventoryMovemnt1.InOut = "O"
            '    objInventoryMovemnt1.Location_Code = obj.MCC_Code
            '    objInventoryMovemnt1.Other_Location_Code = obj.Mcc_Or_Plant_Code
            '    objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
            '    objInventoryMovemnt1.Item_Code = strItemCode
            '    objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
            '    objInventoryMovemnt1.Qty = SealQty
            '    objInventoryMovemnt1.UOM = strItemUnitCode
            '    objInventoryMovemnt1.MRP = 0
            '    objInventoryMovemnt1.Add_Cost = 0
            '    objInventoryMovemnt1.Net_Cost = 0
            '    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '        objInventoryMovemnt1.ItemType = "RM"
            '    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '        objInventoryMovemnt1.ItemType = "OT"
            '    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '        objInventoryMovemnt1.ItemType = "FT"
            '    End If
            '    objInventoryMovemnt1.ItemType = strItemTypeToSave
            '    objInventoryMovemnt1.Basic_Cost = 0
            '    ArrInventoryMovement1.Add(objInventoryMovemnt1)
            '    If Not obj.isBulkSaleData Then
            '        isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrLocationDetails1, trans)
            '        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("DispChallanRet", obj.Chalan_NO, obj.Dispatch_Date, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
            '    End If
            'End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.CreateTankerDispatchGL, trans)) = 1 Then
                isSaved = CreateTransferReturnJE(obj, "", trans)
            End If

            Dim strQry As String = " update TSPL_MCC_Tanker_Dispatch_Return_Head set isPosted='1', isReversed='0',Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Return_No='" & obj.Return_NO & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            If obj.ReachedAtFinal = 1 Then
                strQry = "  update TSPL_MCC_Tanker_Dispatch_Return_Head set ReachedAtFinal='1' where tanker_no='" & obj.Tanker_No & "' "
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            End If
            'If Not obj.isBulkSaleData Then
            '    If isSaved AndAlso clsCommon.myCdbl(obj.Payment_Amount) > 0 AndAlso clsCommon.myLen(obj.Payment_Type) > 0 Then
            '        If obj.isIntermittent = 1 Then
            '        Else
            '            Dim objProv As clsProvisionEntry = New clsProvisionEntry()
            '            objProv.isNewEntry = True
            '            objProv.Doc_Date = obj.Dispatch_Date
            '            Dim strTransporterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_Transporter_Code  from tspl_tanker_master where Tanker_No ='" & obj.Tanker_No & "'", trans))
            '            Dim strTransporterName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from tspl_tanker_master where Tanker_No ='" & obj.Tanker_No & "'", trans))
            '            objProv.Vendor_Code = strTransporterCode
            '            objProv.Vendor_Desc = strTransporterName
            '            objProv.Vendor_Type = "Secondary Transporter"
            '            objProv.Prov_type = "Freight"
            '            objProv.Status = "No"
            '            objProv.Ref_Doc_No = obj.Chalan_NO
            '            objProv.Amount = obj.Payment_Amount
            '            objProv.Prog_Code = FormId
            '            objProv.Prov_Month = Month(obj.Dispatch_Date)
            '            objProv.Prov_Year = Year(obj.Dispatch_Date)
            '            objProv.Comp_Code = obj.Comp_Code
            '            objProv.Created_By = obj.Created_By
            '            objProv.Created_Date = obj.Created_Date
            '            objProv.Modified_By = obj.Modified_By
            '            objProv.Loc_Code = obj.MCC_Code
            '            objProv.Loc_Desc = obj.MCC_Name
            '            isSaved = isSaved AndAlso clsProvisionEntry.SaveData(objProv, trans)
            '            If isSaved Then
            '                isSaved = isSaved AndAlso clsProvisionEntry.PostData(objProv.Doc_No, trans)
            '            End If
            '        End If
            '    End If
            'End If

            If isSaved Then
                If isChildTrans Then
                    trans.Commit()
                End If
            Else
                If isChildTrans Then
                    trans.Rollback()
                End If
            End If
            Return isSaved
        Catch ex As Exception
            If isChildTrans Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function deleteData(ByVal Return_No As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            'Dim qry1 As String = "update TSPL_TANKER_MASTER set isGateOut=1,Ref_Doc_No=NULL where Tanker_No in (select Tanker_No from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & challano & "')"
            'isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, tran)
            Dim qry As String = "delete from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where  Return_No='" & Return_No & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            'qry = "delete from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where  chalan_No='" & challano & "'"
            'isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "delete from TSPL_MCC_Tanker_Dispatch_Return_Detail where  Return_No='" & Return_No & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "delete from TSPL_MCC_Tanker_Dispatch_Return_Head where  Return_No='" & Return_No & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)

            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    '' Created By Panch Raj on 16-02-2016 For Creating Journal Entry For Tanker Dispatch
    'Public Shared Function CreateJournalEntryForDispatch(ByVal obj As clsMccTankerDispatchReturn, ByVal strVoucherNo As String, Optional trans As SqlTransaction = Nothing) As Boolean
    '    Dim isCreated As Boolean = False
    '    Dim isTransLocallyInit As Boolean = False
    '    ' Checking Transaction is been Passed to Argument or Not, if Not then being initialized locally, 
    '    'and Maintaining its State that it Initialized Locally Or Passed as Parameter
    '    If trans Is Nothing Then
    '        trans = clsDBFuncationality.GetTransactin()
    '        isTransLocallyInit = True
    '    Else
    '        isTransLocallyInit = False
    '    End If
    '    Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

    '    Dim COGT_AC As String = String.Empty
    '    Dim Stock_Transfer_Ac As String = String.Empty
    '    Dim Inventory_Control_Ac As String = String.Empty
    '    Dim Branch_Ac As String = String.Empty
    '    Dim Inventory_Control_Ac_FromLoc As String = String.Empty
    '    Dim Inventory_Control_Ac_GITLoc As String = String.Empty

    '    Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
    '    Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
    '    Dim GIT_LOC As String = String.Empty
    '    Dim CostingMethod As Integer = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
    '    Dim dt As Date = clsCommon.GETSERVERDATE(trans)
    '    Dim FromLocSeg As String = String.Empty
    '    Dim ToLocSeg As String = String.Empty
    '    If obj.isPosted = 1 Then
    '        dt = obj.Posting_Date
    '    End If
    '    Dim CostOfItem As Double = 0
    '    If Not isSkipCogsGL Then    '' Done By Panch Raj For Skipping Cogs GL

    '        CostOfItem = clsInventoryMovement.GetCost(CostingMethod, obj.Item_Code, obj.MCC_Code, obj.Net_Qty, obj.Dispatch_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement_new")
    '    Else
    '        CostOfItem = 0
    '    End If  '' Done By Panch Raj For Skipping Cogs GL
    '    FromLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & obj.MCC_Code & "'", trans))
    '    ToLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & obj.Mcc_Or_Plant_Code & "'", trans))


    '    Try
    '        If clsCommon.myLen(FromLocSeg) <= 0 Then
    '            Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & obj.MCC_Code)
    '        End If

    '        If clsCommon.myLen(ToLocSeg) <= 0 Then
    '            Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & obj.Mcc_Or_Plant_Code)
    '        End If

    '        Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '        If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
    '            Throw New Exception("Please Map Inv. Control A/C From Purchase Account Set For Item : " & obj.Item_Code)
    '        End If
    '        If CostOfItem > 0 Then
    '            COGT_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select COGT_AC from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '            If clsCommon.myLen(COGT_AC) <= 0 Then
    '                Throw New Exception("Please Map Cost Of Goods Transfer A/C From Sales Account Set For Item : " & obj.Item_Code)
    '            End If
    '            COGT_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(COGT_AC, FromLocSeg, True, trans)
    '            Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocSeg, True, trans)
    '        End If
    '        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & FromLocSeg & "' and to_location='" & ToLocSeg & "'", trans))
    '        If clsCommon.myLen(Branch_Ac) <= 0 Then
    '            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocSeg & " To " & ToLocSeg)
    '        End If

    '        Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '        If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
    '            Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & obj.Item_Code)
    '        End If

    '        GIT_LOC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where location_code='" & obj.MCC_Code & "'", trans))
    '        If clsCommon.myLen(GIT_LOC) <= 0 Then
    '            Throw New Exception("Please Map GIT Location For Location : " & obj.MCC_Code)
    '        End If
    '        Dim GIT_LOC_SEG As String = String.Empty
    '        GIT_LOC_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & GIT_LOC & "'", trans))
    '        If clsCommon.myLen(GIT_LOC_SEG) <= 0 Then
    '            Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & GIT_LOC)
    '        End If
    '        GIT_LOC = GIT_LOC_SEG
    '        Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, trans)
    '        Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, trans)
    '        Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocSeg, True, trans)
    '        Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocSeg, True, trans)

    '        Dim Amt As Double = clsCommon.myCdbl(obj.Amount)
    '        Dim ArryLst As ArrayList = New ArrayList()
    '        ArryLst.Add(New String() {Branch_Ac, Amt})
    '        ArryLst.Add(New String() {Stock_Transfer_Ac_FromLoc, Amt * -1})
    '        ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt})
    '        ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt * -1})

    '        If CostOfItem > 0 Then
    '            ArryLst.Add(New String() {COGT_AC, CostOfItem})
    '            ArryLst.Add(New String() {Inventory_Control_Ac_FromLoc, CostOfItem * -1})
    '        End If
    '        isCreated = clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, strVoucherNo, trans, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy"), " Against Dispatch Challan No  -" + obj.Chalan_NO + " For Milk transfer from " + obj.MCC_Code + " to " + obj.Mcc_Or_Plant_Code, "DI-CH", "Dispatch Challan", obj.Chalan_NO, "", "C", obj.Item_Code, obj.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, " ", " Stock Transfer From " & clsLocation.GetName(obj.MCC_Code, trans) & "  to " & clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans))
    '    Catch ex As Exception
    '        'Rollbacking Transaction on exception if Transaction Is Locally Initialized
    '        If isTransLocallyInit Then
    '            trans.Rollback()
    '        End If
    '        Throw New Exception(ex.Message)
    '    End Try

    '    'Commiting/Rollbacking Transaction if Transaction Is Locally Initialized
    '    If isTransLocallyInit Then
    '        If isCreated Then
    '            trans.Commit()
    '        Else
    '            trans.Rollback()
    '        End If
    '    End If


    '    Return isCreated
    'End Function
    Public Shared Function CreateTransferReturnJE(obj As clsMccTankerDispatchReturn, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        Dim rValue As Boolean = False
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        Try
            Dim ArryLst As ArrayList = New ArrayList()
            Dim Branch_Ac As String = String.Empty
            Dim Inventory_Control_Ac As String = String.Empty
            Dim FromLocSeg As String = obj.MCC_Code
            Dim ToLocSeg As String = obj.Mcc_Or_Plant_Code
            Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
            Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferEntryOnInvCtrlAccount, clsFixedParameterCode.TransferEntryOnInvCtrlAccount, trans)) = 1 Then
                If obj.arr Is Nothing OrElse obj.arr.Count <= 0 Then
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                    End If
                    Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                    ArryLst.Add(New String() {Branch_Ac, -1 * obj.Amount})


                    Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_control_account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                    If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                        Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                    End If
                    Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
                    ArryLst.Add(New String() {Inventory_Control_Ac, obj.Amount})
                Else
                    For Each objtr As clsMCCTankerDispatchReturnDetail In obj.arr
                        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_Ac) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                        End If
                        Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                        ArryLst.Add(New String() {Branch_Ac, -1 * objtr.Amount})


                        Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_control_account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objtr.Item_Code & "') ", trans))
                        If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                            Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & objtr.Item_Code & " (" & objtr.Item_Name & ")")
                        End If
                        Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
                        ArryLst.Add(New String() {Inventory_Control_Ac, objtr.Amount})
                    Next
                End If
            Else
                Dim COGT_AC As String = String.Empty
                Dim Stock_Transfer_Ac As String = String.Empty


                Dim Inventory_Control_Ac_FromLoc As String = String.Empty
                Dim Inventory_Control_Ac_GITLoc As String = String.Empty
                Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
                Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
                Dim CostingMethod As Integer = 0
                Dim CostOfItem As Double = 0
                Dim dt As Date = clsCommon.GETSERVERDATE(trans)

                If obj.isPosted = 1 Then
                    dt = obj.Posting_Date
                End If
                Dim GIT_LOC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where location_code='" & obj.MCC_Code & "'", trans))
                If clsCommon.myLen(GIT_LOC) <= 0 Then
                    Throw New Exception("Please Map GIT Location For Location : " & obj.MCC_Code)
                End If
                Dim GIT_LOC_SEG As String = String.Empty
                GIT_LOC_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & GIT_LOC & "'", trans))
                If clsCommon.myLen(GIT_LOC_SEG) <= 0 Then
                    Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & GIT_LOC)
                End If
                GIT_LOC = GIT_LOC_SEG

                If obj.arr Is Nothing OrElse obj.arr.Count <= 0 Then

                    If Not isSkipCogsGL Then    '' Done By Panch Raj For Skipping Cogs GL
                        CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
                        CostOfItem = clsInventoryMovement.GetCost(CostingMethod, obj.Item_Code, obj.MCC_Code, obj.Net_Qty, obj.Dispatch_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "TSPL_INVENTORY_MOVEMENT_NEW")
                    Else
                        CostOfItem = 0
                    End If  '' Done By Panch Raj For Skipping Cogs GL
                    Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Acc from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                    If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                        Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                    End If
                    If CostOfItem > 0 Then
                        COGT_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select COGT_AC from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                        If clsCommon.myLen(COGT_AC) <= 0 Then
                            Throw New Exception("Please Map Cost Of Goods Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                        End If
                        COGT_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(COGT_AC, FromLocationSegment, True, trans)
                        Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
                    End If
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                    End If

                    Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                    If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                        Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                    End If
                    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                    Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)

                    Dim Amt As Double = obj.Amount

                    ArryLst.Add(New String() {Branch_Ac, -1 * Amt})
                    Dim AccStockTransfer() As String = {Stock_Transfer_Ac_FromLoc, Amt, "", "", "", "", "", "", "S"}
                    ArryLst.Add(AccStockTransfer)
                    'ArryLst.Add(New String() {Stock_Transfer_Ac_FromLoc, Amt})
                    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, -1 * Amt})
                    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})

                    If CostOfItem > 0 Then
                        ArryLst.Add(New String() {COGT_AC, -1 * CostOfItem})
                        Dim AccInventory() As String = {Inventory_Control_Ac_FromLoc, CostOfItem, "", "", "", "", "", "", "S"}
                        ArryLst.Add(AccInventory)
                        'ArryLst.Add(New String() {Inventory_Control_Ac_FromLoc, CostOfItem})
                    End If
                Else
                    For Each objtr As clsMCCTankerDispatchReturnDetail In obj.arr
                        If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                            CostingMethod = clsInventoryMovementNew.getCostingMethod(objtr.Item_Code, trans)
                            CostOfItem = clsInventoryMovement.GetCost(CostingMethod, objtr.Item_Code, obj.MCC_Code, objtr.Qty_KG, obj.Dispatch_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "TSPL_INVENTORY_MOVEMENT_NEW")
                        Else
                            CostOfItem = 0
                        End If  '' Done By Pankaj Jha For Skipping Cogs GL
                        Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Acc from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objtr.Item_Code & "') ", trans))
                        If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                            Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & objtr.Item_Code & " (" & objtr.Item_Name & ")")
                        End If
                        If CostOfItem > 0 Then
                            COGT_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select COGT_AC from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objtr.Item_Code & "') ", trans))
                            If clsCommon.myLen(COGT_AC) <= 0 Then
                                Throw New Exception("Please Map Cost Of Goods Transfer A/C From Sales Account Set For Item : " & objtr.Item_Code & " (" & objtr.Item_Name & ")")
                            End If
                            COGT_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(COGT_AC, FromLocationSegment, True, trans)
                            Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
                        End If
                        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_Ac) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                        End If

                        Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objtr.Item_Code & "') ", trans))
                        If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                            Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objtr.Item_Code & " (" & objtr.Item_Name & ")")
                        End If
                        Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                        Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                        Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)

                        Dim Amt As Double = objtr.Amount

                        ArryLst.Add(New String() {Branch_Ac, -1 * Amt})
                        Dim AccStockTransfer() As String = {Stock_Transfer_Ac_FromLoc, Amt, "", "", "", "", "", "", "S"}
                        ArryLst.Add(AccStockTransfer)
                        ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, -1 * Amt})
                        ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})

                        If CostOfItem > 0 Then
                            ArryLst.Add(New String() {COGT_AC, -1 * CostOfItem})
                            Dim AccInventory() As String = {Inventory_Control_Ac_FromLoc, CostOfItem, "", "", "", "", "", "", "S"}
                            ArryLst.Add(AccInventory)
                        End If
                    Next
                End If
            End If


            '===========BM00000008259
            Dim GLDesc As String = "Journal Entry Against MCC Tanker Dispatch Return - Doc No." & obj.Return_NO & " and Chalan No." & obj.Chalan_NO & " "
            Dim Remarks As String = "Journal Entry against MCC Tanker Dispatch Return from location -" & obj.MCC_Code & " to location- " & obj.Mcc_Or_Plant_Code & " For  Doc No." & obj.Return_NO & " and Chalan No." & obj.Chalan_NO & ""
            AddTransferReturnDiffAccount(obj, ArryLst, trans)
            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then ''because if voucher no known then recreate it with same no.
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, strVoucherNoForRecreateOnly, trans, obj.Return_Date, GLDesc, "DS-RT", "Dispatch Challan Return", obj.Return_NO, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Remarks)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, trans, obj.Return_Date, GLDesc, "DS-RT", "Dispatch Challan Return", obj.Return_NO, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Remarks)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateTransferReturnDiffJE(obj As clsMccTankerDispatchReturn, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        '' Developed by Panch Raj on 01/07/2016
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))

        Dim rValue As Boolean = False
        Try
            Dim COGT_AC As String = String.Empty
            'Dim Stock_Transfer_Ac As String = String.Empty
            'Dim Inventory_Control_Ac As String = String.Empty
            'Dim Branch_Ac As String = String.Empty
            'Dim Inventory_Control_Ac_FromLoc As String = String.Empty
            'Dim Inventory_Control_Ac_ToLoc As String = String.Empty
            'Dim Inventory_Control_Ac_GITLoc As String = String.Empty
            'Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
            'Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
            'Dim GIT_LOC As String = String.Empty
            'Dim CostingMethod As Integer = 0
            'Dim CostOfItem As Double = 0
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)
            Dim FromLocSeg As String = String.Empty
            Dim ToLocSeg As String = String.Empty
            If obj.isPosted = 1 Then
                dt = obj.Posting_Date
            End If
            Dim FromLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Chalan_NO & "'", trans))
            FromLocSeg = FromLocation   'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & obj.From_Location & "'", trans))
            ToLocSeg = obj.Mcc_Or_Plant_Code    'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & obj.To_Location & "'", trans))

            Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
            Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

            Dim TransitLossAc As String = ""
            Dim Branch_AcFromLoc As String = ""
            'Dim IGnoreGITAccount As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IGnoreGITAccount, clsFixedParameterCode.IGnoreGITAccount, trans))
            'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
            '    GIT_LOC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where location_code='" & FromLocation & "'", trans))
            '    If clsCommon.myLen(GIT_LOC) <= 0 Then
            '        Throw New Exception("Please Map GIT Location For Location : " & FromLocation)
            '    End If
            '    Dim GIT_LOC_SEG As String = String.Empty
            '    GIT_LOC_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & GIT_LOC & "'", trans))
            '    If clsCommon.myLen(GIT_LOC_SEG) <= 0 Then
            '        Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & GIT_LOC)
            '    End If
            '    GIT_LOC = GIT_LOC_SEG
            'End If


            Dim qry As String = ""
            'Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, NavigatorType.Current, False, trans)
            'Dim objDisp As clsMccTankerDispatchReturn = clsMccTankerDispatchReturn.getData(obj.Return_NO, NavigatorType.Current, trans)
            Dim ArryLst As ArrayList = New ArrayList()

            If TankerFromMaster = 0 Then
                'If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                '    CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
                '    qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement where source_doc_no='" & obj.Return_NO & "' and Item_Code='" & objW.Item_Code & "' and InOut='O' and Trans_Type='Transfer' "
                '    CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) 'clsInventoryMovement.GetCost(CostingMethod, obj.Arr(i).Item_Code, obj.From_Location, obj.Arr(i).Out_Qty, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement")
                'Else
                '    CostOfItem = 0
                'End If  '' Done By Pankaj Jha For Skipping Cogs GL
                'Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                'If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                '    Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                'End If

                'Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)
                'Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, False, trans)
                'Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                'If clsCommon.myLen(Branch_Ac) <= 0 Then
                '    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                'End If

                'Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                'If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                '    Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & obj.Item_Code)
                'End If

                'Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                'If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                '    Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                'End If
                'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                '    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                '    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                'End If

                'Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocSeg, True, trans)
                'Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocSeg, True, trans)

                'Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                'Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))

                Dim FatValue As Double = obj.FAT_RATE * obj.FAT_KG ''(objW.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                Dim SnfValue As Double = obj.SNF_RATE * obj.SNF_KG ''(objW.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                Dim rcptAmount As Double = FatValue + SnfValue

                Dim Amt As Double = rcptAmount
                Dim OutAmt As Double = clsMccDispatch.getTransferAmount(obj.Chalan_NO, trans)

                'ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                'ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt})
                'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                '    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                '    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                'End If

                'TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                Dim DiffAmt As Double = 0

                'If CostOfItem > OutAmt Then
                '    DiffAmt = Math.Abs(CostOfItem - OutAmt)
                '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt})
                '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                'ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                '    DiffAmt = Math.Abs(OutAmt - CostOfItem)
                '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1})
                '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                'End If
               
                If rcptAmount <> OutAmt Then
                    If OutAmt < rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & obj.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                        End If
                        DiffAmt = rcptAmount - OutAmt
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                        'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                        '    ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                        'End If
                    ElseIf OutAmt > rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & obj.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                        End If
                        DiffAmt = OutAmt - rcptAmount
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})

                        'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                        '    ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                        'End If
                    End If
                End If
            Else
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    '' entry for chamber wise
                    'If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                    '    CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
                    '    qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement where source_doc_no='" & obj.Dispatch_Challan_No & "' and Item_Code='" & objW.Item_Code & "' and InOut='O' and Trans_Type='Transfer' "
                    '    CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) 'clsInventoryMovement.GetCost(CostingMethod, obj.Arr(i).Item_Code, obj.From_Location, obj.Arr(i).Out_Qty, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement")
                    'Else
                    '    CostOfItem = 0
                    'End If  '' Done By Pankaj Jha For Skipping Cogs GL
                    For Each objTr As clsMCCTankerDispatchReturnDetail In obj.arr
                        'Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        'Dim Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
                        'If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                        '    Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        'End If

                        'Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)
                        'Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, False, trans)
                        'Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                        'If clsCommon.myLen(Branch_Ac) <= 0 Then
                        '    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                        'End If

                        'Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        'If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                        '    Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & objTr.Item_Code)
                        'End If

                        'Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        'If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                        '    Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        'End If
                        'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                        '    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                        '    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                        'End If


                        'Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' and line_no='" & objTr.Line_No & "' ", trans))
                        'Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' and line_no='" & objTr.Line_No & "' ", trans))

                        Dim FatValue As Double = objTr.FAT_KG * objTr.FAT_Rate ''(objTr.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                        Dim SnfValue As Double = objTr.SNF_KG * objTr.SNF_Rate ''(objTr.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                        Dim rcptAmount As Double = FatValue + SnfValue

                        Dim Amt As Double = rcptAmount
                        Dim OutAmt As Double = clsMccDispatch.getTransferAmountChamberwise(obj.Chalan_NO, trans, objTr.Chamber_No)

                        'ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                        'ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt})
                        'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                        '    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                        'End If

                        'TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                        Dim DiffAmt As Double = 0

                        'If CostOfItem > OutAmt Then
                        '    DiffAmt = Math.Abs(CostOfItem - OutAmt)
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt})
                        '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                        'ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                        '    DiffAmt = Math.Abs(OutAmt - CostOfItem)
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1})
                        '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                        'End If

                        If rcptAmount <> OutAmt Then
                            If OutAmt < rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & objTr.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                                Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                End If
                                DiffAmt = rcptAmount - OutAmt
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                            ElseIf OutAmt > rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & obj.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                                Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                End If
                                DiffAmt = OutAmt - rcptAmount
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})
                            End If
                        End If
                    Next
                    'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                    '    ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                    'End If
                End If
            End If

            Dim GLDesc As String = "Journal Entry Against MCC Tanker Dispatch Return- Doc No." & obj.Return_NO & " "
            Dim Remarks As String = "Journal Entry against MCC Tanker Dispatch Return from location -" & FromLocation & " For Doc No. " & obj.Return_NO & ", Transfer Out Doc No: " & obj.Chalan_NO

            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, strVoucherNoForRecreateOnly, trans, obj.Return_Date, GLDesc, "DS-RT", "Milk Transfer In", obj.Return_NO, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Return_NO)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, trans, obj.Return_Date, GLDesc, "DS-RT", "Milk Transfer In", obj.Return_NO, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Return_NO)
            End If
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function AddTransferReturnDiffAccount(obj As clsMccTankerDispatchReturn, ArryLst As ArrayList, trans As SqlTransaction) As ArrayList
        '' Developed by Panch Raj on 01/07/2016
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))

        Dim rValue As Boolean = False
        Try
            Dim COGT_AC As String = String.Empty
            'Dim Stock_Transfer_Ac As String = String.Empty
            'Dim Inventory_Control_Ac As String = String.Empty
            'Dim Branch_Ac As String = String.Empty
            'Dim Inventory_Control_Ac_FromLoc As String = String.Empty
            'Dim Inventory_Control_Ac_ToLoc As String = String.Empty
            'Dim Inventory_Control_Ac_GITLoc As String = String.Empty
            'Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
            'Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
            'Dim GIT_LOC As String = String.Empty
            'Dim CostingMethod As Integer = 0
            'Dim CostOfItem As Double = 0
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)
            Dim FromLocSeg As String = String.Empty
            Dim ToLocSeg As String = String.Empty
            If obj.isPosted = 1 Then
                dt = obj.Posting_Date
            End If
            Dim FromLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Chalan_NO & "'", trans))
            FromLocSeg = FromLocation   'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & obj.From_Location & "'", trans))
            ToLocSeg = obj.Mcc_Or_Plant_Code    'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & obj.To_Location & "'", trans))

            Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
            Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

            Dim TransitLossAc As String = ""
            Dim Branch_AcFromLoc As String = ""
            'Dim IGnoreGITAccount As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IGnoreGITAccount, clsFixedParameterCode.IGnoreGITAccount, trans))
            'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
            '    GIT_LOC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where location_code='" & FromLocation & "'", trans))
            '    If clsCommon.myLen(GIT_LOC) <= 0 Then
            '        Throw New Exception("Please Map GIT Location For Location : " & FromLocation)
            '    End If
            '    Dim GIT_LOC_SEG As String = String.Empty
            '    GIT_LOC_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & GIT_LOC & "'", trans))
            '    If clsCommon.myLen(GIT_LOC_SEG) <= 0 Then
            '        Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & GIT_LOC)
            '    End If
            '    GIT_LOC = GIT_LOC_SEG
            'End If


            Dim qry As String = ""
            'Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, NavigatorType.Current, False, trans)
            'Dim objDisp As clsMccTankerDispatchReturn = clsMccTankerDispatchReturn.getData(obj.Return_NO, NavigatorType.Current, trans)
            'Dim ArryLst As ArrayList = New ArrayList()

            If TankerFromMaster = 0 Then
                'If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                '    CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
                '    qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement where source_doc_no='" & obj.Return_NO & "' and Item_Code='" & objW.Item_Code & "' and InOut='O' and Trans_Type='Transfer' "
                '    CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) 'clsInventoryMovement.GetCost(CostingMethod, obj.Arr(i).Item_Code, obj.From_Location, obj.Arr(i).Out_Qty, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement")
                'Else
                '    CostOfItem = 0
                'End If  '' Done By Pankaj Jha For Skipping Cogs GL
                'Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                'If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                '    Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                'End If

                'Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)
                'Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, False, trans)
                'Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                'If clsCommon.myLen(Branch_Ac) <= 0 Then
                '    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                'End If

                'Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                'If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                '    Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & obj.Item_Code)
                'End If

                'Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                'If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                '    Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                'End If
                'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                '    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                '    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                'End If

                'Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocSeg, True, trans)
                'Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocSeg, True, trans)

                'Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                'Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))

                Dim FatValue As Double = obj.FAT_RATE * obj.FAT_KG ''(objW.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                Dim SnfValue As Double = obj.SNF_RATE * obj.SNF_KG ''(objW.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                Dim rcptAmount As Double = FatValue + SnfValue

                Dim Amt As Double = rcptAmount
                Dim OutAmt As Double = clsMccDispatch.getTransferAmount(obj.Chalan_NO, trans)

                'ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                'ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt})
                'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                '    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                '    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                'End If

                'TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                Dim DiffAmt As Double = 0

                'If CostOfItem > OutAmt Then
                '    DiffAmt = Math.Abs(CostOfItem - OutAmt)
                '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt})
                '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                'ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                '    DiffAmt = Math.Abs(OutAmt - CostOfItem)
                '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1})
                '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                'End If

                If rcptAmount <> OutAmt Then
                    If OutAmt < rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & obj.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                        End If
                        DiffAmt = rcptAmount - OutAmt
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                        'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                        '    ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                        'End If
                    ElseIf OutAmt > rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & obj.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                        End If
                        DiffAmt = OutAmt - rcptAmount
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})

                        'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                        '    ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                        'End If
                    End If
                End If
            Else
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    '' entry for chamber wise
                    'If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                    '    CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
                    '    qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement where source_doc_no='" & obj.Dispatch_Challan_No & "' and Item_Code='" & objW.Item_Code & "' and InOut='O' and Trans_Type='Transfer' "
                    '    CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) 'clsInventoryMovement.GetCost(CostingMethod, obj.Arr(i).Item_Code, obj.From_Location, obj.Arr(i).Out_Qty, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement")
                    'Else
                    '    CostOfItem = 0
                    'End If  '' Done By Pankaj Jha For Skipping Cogs GL
                    For Each objTr As clsMCCTankerDispatchReturnDetail In obj.arr
                        'Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        'Dim Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
                        'If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                        '    Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        'End If

                        'Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)
                        'Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, False, trans)
                        'Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                        'If clsCommon.myLen(Branch_Ac) <= 0 Then
                        '    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                        'End If

                        'Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        'If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                        '    Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & objTr.Item_Code)
                        'End If

                        'Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        'If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                        '    Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        'End If
                        'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                        '    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                        '    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                        'End If


                        'Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' and line_no='" & objTr.Line_No & "' ", trans))
                        'Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' and line_no='" & objTr.Line_No & "' ", trans))

                        Dim FatValue As Double = objTr.FAT_KG * objTr.FAT_Rate ''(objTr.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                        Dim SnfValue As Double = objTr.SNF_KG * objTr.SNF_Rate ''(objTr.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                        Dim rcptAmount As Double = FatValue + SnfValue

                        Dim Amt As Double = rcptAmount
                        Dim OutAmt As Double = clsMccDispatch.getTransferAmountChamberwise(obj.Chalan_NO, trans, objTr.Chamber_No)

                        'ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                        'ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt})
                        'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                        '    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                        'End If

                        'TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                        Dim DiffAmt As Double = 0

                        'If CostOfItem > OutAmt Then
                        '    DiffAmt = Math.Abs(CostOfItem - OutAmt)
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt})
                        '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                        'ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                        '    DiffAmt = Math.Abs(OutAmt - CostOfItem)
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1})
                        '    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                        'End If

                        If rcptAmount <> OutAmt Then
                            If OutAmt < rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & objTr.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                                Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                End If
                                DiffAmt = rcptAmount - OutAmt
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                            ElseIf OutAmt > rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & obj.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                                Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                End If
                                DiffAmt = OutAmt - rcptAmount
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})
                            End If
                        End If
                    Next
                    'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                    '    ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                    'End If
                End If
            End If

            'Dim GLDesc As String = "Journal Entry Against MCC Tanker Dispatch Return- Doc No." & obj.Return_NO & " "
            'Dim Remarks As String = "Journal Entry against MCC Tanker Dispatch Return from location -" & FromLocation & " For Doc No. " & obj.Return_NO & ", Transfer Out Doc No: " & obj.Chalan_NO

            'If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
            '    clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, strVoucherNoForRecreateOnly, trans, obj.Return_Date, GLDesc, "DS-RT", "Milk Transfer In", obj.Return_NO, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Return_NO)
            'Else
            '    clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, trans, obj.Return_Date, GLDesc, "DS-RT", "Milk Transfer In", obj.Return_NO, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Return_NO)
            'End If
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ArryLst
    End Function
    Public Shared Function JE_Excisable_Common(ByVal obj As clsTransferDCC, ByVal arrlist As ArrayList, ByVal strLocationSegment As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        qry = " select Doc.Document_No,TaxM1.Tax_Liability_Account as Tax1_GLAC,TaxM2.Tax_Liability_Account as Tax2_GLAC," & _
                 " TaxM3.Tax_Liability_Account as Tax3_GLAC,TaxM4.Tax_Liability_Account as Tax4_GLAC," & _
                 " TaxM5.Tax_Liability_Account as Tax5_GLAC,TaxM6.Tax_Liability_Account as Tax6_GLAC, " & _
                 " TaxM7.Tax_Liability_Account as Tax7_GLAC,TaxM8.Tax_Liability_Account as Tax8_GLAC, " & _
                 " TaxM9.Tax_Liability_Account as Tax9_GLAC,TaxM10.Tax_Liability_Account as Tax10_GLAC, " & _
                 " TaxM1.Tax_Net_Payable as Tax1_GLAC_Payable,TaxM2.Tax_Net_Payable as Tax2_GLAC_Payable, " & _
                 " TaxM3.Tax_Net_Payable as Tax3_GLAC_Payable,TaxM4.Tax_Net_Payable as Tax4_GLAC_Payable, " & _
                 " TaxM5.Tax_Net_Payable as Tax5_GLAC_Payable,TaxM6.Tax_Net_Payable as Tax6_GLAC_Payable, " & _
                 " TaxM7.Tax_Net_Payable as Tax7_GLAC_Payable,TaxM8.Tax_Net_Payable as Tax8_GLAC_Payable, " & _
                 " TaxM9.Tax_Net_Payable as Tax9_GLAC_Payable,TaxM10.Tax_Net_Payable as Tax10_GLAC_Payable from TSPL_TRANSFER_ORDER_HEAD doc " & _
                 " left join TSPL_TAX_MASTER TaxM1 on Doc.TAX1=TaxM1.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM2 on Doc.TAX2=TaxM2.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM3 on Doc.TAX3=TaxM3.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM4 on Doc.TAX4=TaxM4.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM5 on Doc.TAX5=TaxM5.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM6 on Doc.TAX6=TaxM6.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM7 on Doc.TAX7=TaxM7.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM8 on Doc.TAX8=TaxM8.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM9 on Doc.TAX9=TaxM9.Tax_Code " & _
                 " left join TSPL_TAX_MASTER TaxM10 on Doc.TAX10=TaxM10.Tax_Code where doc.Document_No='" & obj.Document_No & "'"
        Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtTax.Rows.Count = 0 Then
            Throw New Exception("Tax details of transfer document not found.")
        End If
        Dim TAX1_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax1_GLAC"))
        Dim TAX2_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax2_GLAC"))
        Dim TAX3_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax3_GLAC"))
        Dim TAX4_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax4_GLAC"))
        Dim TAX5_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax5_GLAC"))
        Dim TAX6_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax6_GLAC"))
        Dim TAX7_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax7_GLAC"))
        Dim TAX8_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax8_GLAC"))
        Dim TAX9_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax9_GLAC"))
        Dim TAX10_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax10_GLAC"))

        Dim TAX1_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX1_GLAC_Payable"))
        Dim TAX2_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX2_GLAC_Payable"))
        Dim TAX3_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX3_GLAC_Payable"))
        Dim TAX4_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX4_GLAC_Payable"))
        Dim TAX5_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX5_GLAC_Payable"))
        Dim TAX6_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX6_GLAC_Payable"))
        Dim TAX7_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX7_GLAC_Payable"))
        Dim TAX8_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX8_GLAC_Payable"))
        Dim TAX9_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX9_GLAC_Payable"))
        Dim TAX10_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX10_GLAC_Payable"))

        '' validation for gl
        If obj.TAX1_Amt > 0 Then
            If clsCommon.myLen(TAX1_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX1)
            End If
            If clsCommon.myLen(TAX1_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX1)
            End If
        End If
        If obj.TAX2_Amt > 0 Then
            If clsCommon.myLen(TAX2_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX2)
            End If
            If clsCommon.myLen(TAX2_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX2)
            End If
        End If
        If obj.TAX3_Amt > 0 Then
            If clsCommon.myLen(TAX3_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX3)
            End If
            If clsCommon.myLen(TAX3_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX3)
            End If
        End If
        If obj.TAX4_Amt > 0 Then
            If clsCommon.myLen(TAX4_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX4)
            End If
            If clsCommon.myLen(TAX4_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX4)
            End If
        End If
        If obj.TAX5_Amt > 0 Then
            If clsCommon.myLen(TAX5_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX5)
            End If
            If clsCommon.myLen(TAX5_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX5)
            End If
        End If
        If obj.TAX6_Amt > 0 Then
            If clsCommon.myLen(TAX6_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX6)
            End If
            If clsCommon.myLen(TAX6_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX6)
            End If
        End If

        If obj.TAX7_Amt > 0 Then
            If clsCommon.myLen(TAX7_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX7)
            End If
            If clsCommon.myLen(TAX7_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX7)
            End If
        End If

        If obj.TAX8_Amt > 0 Then
            If clsCommon.myLen(TAX8_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX8)
            End If
            If clsCommon.myLen(TAX8_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX8)
            End If
        End If

        If obj.TAX9_Amt > 0 Then
            If clsCommon.myLen(TAX9_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX9)
            End If
            If clsCommon.myLen(TAX9_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX9)
            End If
        End If


        If obj.TAX10_Amt > 0 Then
            If clsCommon.myLen(TAX10_GLAC) <= 0 Then
                Throw New Exception("Tax Liability Acount not found for" + obj.TAX10)
            End If
            If clsCommon.myLen(TAX10_GLAC_Payable) <= 0 Then
                Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX10)
            End If
        End If
        '' taxes - from locaton
        '' richa agarwal net payable a/c should be debit and tax liablity a/c should be credit BM00000009115.
        If obj.TAX1_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX1_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX1_Amt}
            arrlist.Add(accCR)
        End If
        If obj.TAX2_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX2_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX2_Amt}
            arrlist.Add(accCR)
        End If
        If obj.TAX3_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX3_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX3_Amt}
            arrlist.Add(accCR)
        End If
        If obj.TAX4_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX4_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX4_Amt}
            arrlist.Add(accCR)
        End If
        If obj.TAX5_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX5_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX5_Amt}
            arrlist.Add(accCR)
        End If
        If obj.TAX6_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX6_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX6_Amt}
            arrlist.Add(accCR)
        End If

        If obj.TAX7_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX7_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX7_Amt}
            arrlist.Add(accCR)
        End If

        If obj.TAX8_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX8_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX8_Amt}
            arrlist.Add(accCR)
        End If

        If obj.TAX9_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX9_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX9_Amt}
            arrlist.Add(accCR)
        End If


        If obj.TAX10_Amt > 0 Then
            '' debit
            Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC_Payable, strLocationSegment, True, trans)
            Dim accDR() As String = {strTemp, obj.TAX10_Amt}
            arrlist.Add(accDR)

            ''credit
            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC, strLocationSegment, True, trans)
            Dim accCR() As String = {strTemp, -1 * obj.TAX10_Amt}
            arrlist.Add(accCR)
        End If
        Return True
    End Function
    Public Shared Function getFATRate(ByVal Return_No As String, ByVal trans As SqlTransaction) As Double
        Dim fatRate As Double = 0
        Dim qry As String = "select FAT_RATE from TSPL_MCC_Tanker_Dispatch_Return_Head  where Return_No='" & Return_NO & "'"
        fatRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return fatRate

    End Function
    Public Shared Function getSNFRate(ByVal Return_No As String, ByVal trans As SqlTransaction) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select SNF_RATE from TSPL_MCC_Tanker_Dispatch_Return_Head where Return_No='" & Return_NO & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function
    Public Shared Function getTransferAmount(ByVal Return_NO As String, ByVal trans As SqlTransaction) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select Amount from TSPL_MCC_Tanker_Dispatch_Return_Head where Return_NO='" & Return_NO & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function

    Public Shared Function getFATPerChamberWise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim FATPer As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='FAT' and Return_No='" & Return_NO & "' and sno='" & line_No & "'"
        FATPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return FATPer
    End Function

    Public Shared Function getSNFPerChamberWise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim snfPer As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='SNF' and Return_No='" & Return_NO & "' and sno='" & line_No & "'"
        snfPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfPer
    End Function

    Public Shared Function getFATRateChamberWise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim fatRate As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='colFatRate' and Return_No='" & Return_NO & "' and sno='" & line_No & "'"
        'Dim qry As String = "select FAT_RATE from TSPL_MCC_Tanker_Dispatch_Return_Detail  where Return_NO='" & Return_NO & "' and sno='" & line_No & "'"
        fatRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return fatRate
    End Function
    Public Shared Function getSNFRateChamberWise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select Param_Field_Value  from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Param_Type='colSNFRate' and Return_No='" & Return_NO & "' and sno='" & line_No & "'"
        'Dim qry As String = "select SNF_RATE from TSPL_MCC_Tanker_Dispatch_Return_Detail where Return_NO='" & Return_NO & "' and sno='" & line_No & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function
    Public Shared Function getFATKGChamberWise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim FAT_KG As Double = 0
        Dim qry As String = "select FAT_KG from TSPL_MCC_Tanker_Dispatch_Return_Detail where Return_NO='" & Return_NO & "' and sno='" & line_No & "'"
        FAT_KG = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return FAT_KG
    End Function
    Public Shared Function getSNFKGChamberWise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim SNF_KG As Double = 0
        Dim qry As String = "select SNF_KG from TSPL_MCC_Tanker_Dispatch_Return_Detail where Return_NO='" & Return_NO & "' and sno='" & line_No & "'"
        SNF_KG = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return SNF_KG
    End Function
    Public Shared Function getTransferAmountChamberwise(ByVal Return_NO As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim Amount As Double = 0
        Dim qry As String = "select Amount from TSPL_MCC_Tanker_Dispatch_Return_Detail where Return_NO='" & Return_NO & "' and sno='" & line_No & "'"
        Amount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Amount
    End Function
End Class
Public Class clsMCCTankerDispatchReturnDetail
#Region "Variables"
    Public SNo As Integer = 0
    Public Return_NO As String
    Public Chamber_No As Integer = 0
    Public Chamber_Description As String = ""
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Item_UOM As String = Nothing
    Public Qty_KG As Double
    Public FAT_KG As Double
    Public SNF_KG As Double
    Public FAT_Rate As Double
    Public SNF_Rate As Double
    Public Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal arr As List(Of clsMCCTankerDispatchReturnDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsMCCTankerDispatchReturnDetail In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "Return_NO", strDocNo)
                    'clsCommon.AddColumnsForChange(coll, "Chalan_No", obj.Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Chamber_No", obj.Chamber_No)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Description", obj.Chamber_Description)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_UOM", obj.Item_UOM)
                    clsCommon.AddColumnsForChange(coll, "Qty_KG", obj.Qty_KG)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "FAT_Rate", obj.FAT_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Tanker_Dispatch_Return_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strReturnNo As String, ByVal trans As SqlTransaction) As List(Of clsMCCTankerDispatchReturnDetail)
        Dim arr As List(Of clsMCCTankerDispatchReturnDetail) = Nothing
        Try
            Dim qry As String = "select TSPL_MCC_Tanker_Dispatch_Return_detail.*,TSPL_ITEM_MASTER.Item_Desc FROM  TSPL_MCC_Tanker_Dispatch_Return_detail left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_MCC_Tanker_Dispatch_Return_detail.item_Code where Return_no='" + strReturnNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsMCCTankerDispatchReturnDetail)
                For Each dr As DataRow In dt.Rows
                    Dim obj As New clsMCCTankerDispatchReturnDetail
                    obj.SNo = clsCommon.myCdbl(dr("SNo"))
                    obj.Return_NO = clsCommon.myCstr(dr("Return_NO"))
                    obj.Chamber_No = clsCommon.myCstr(dr("Chamber_No"))
                    obj.Chamber_Description = clsCommon.myCstr(dr("Chamber_Description"))
                    obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                    obj.Item_UOM = clsCommon.myCstr(dr("Item_UOM"))
                    obj.Qty_KG = clsCommon.myCdbl(dr("Qty_KG"))
                    obj.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                    obj.FAT_Rate = clsCommon.myCdbl(dr("FAT_Rate"))
                    obj.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                    obj.Amount = clsCommon.myCdbl(dr("Amount"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class
Public Class Mcc_Dispatch_Chalan_Return_Parameter
    Public SNO As Integer
    Public Return_No As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of Mcc_Dispatch_Chalan_Return_Parameter), ByVal tran As SqlTransaction) As Boolean
        Return SaveData(arr, tran, False)
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of Mcc_Dispatch_Chalan_Return_Parameter), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where  Return_No='" & arr.Item(0).Return_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SNo", arr.Item(i).SNO)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", arr.Item(i).Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", arr.Item(i).Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", arr.Item(i).Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Return_No", arr.Item(i).Return_No)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", arr.Item(i).Param_Type)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail", OMInsertOrUpdate.Insert, "", tran)
                    If isReversed Then
                        issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail_History", OMInsertOrUpdate.Insert, "", tran)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal chalanNo As String) As List(Of Mcc_Dispatch_Chalan_Return_Parameter)
        Return getData(chalanNo, Nothing)
    End Function
    Public Shared Function getData(ByVal Return_No As String, ByVal trans As SqlTransaction) As List(Of Mcc_Dispatch_Chalan_Return_Parameter)
        Dim arr As New List(Of Mcc_Dispatch_Chalan_Return_Parameter)
        Try
            Dim obj As New Mcc_Dispatch_Chalan_Return_Parameter
            Dim q As String = "select * from TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail where Return_No='" & Return_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New Mcc_Dispatch_Chalan_Return_Parameter
                    obj.SNO = clsCommon.myCstr(dtbl.Rows(i)("SNO"))
                    obj.Return_No = clsCommon.myCstr(dtbl.Rows(i)("Return_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dtbl.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dtbl.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dtbl.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dtbl.Rows(i)("Param_Type"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

End Class
'Public Class clsMccDispatchReturnPaperSealDetail
'    Public Chalan_No As String = String.Empty
'    Public Seal_No As String = String.Empty
'    Public Shared Function SaveData(ByVal arr As List(Of clsMccDispatchReturnPaperSealDetail), ByVal tran As SqlTransaction) As Boolean        
'        Return SaveData(arr, tran, False)
'    End Function
'    Public Shared Function SaveData(ByVal arr As List(Of clsMccDispatchReturnPaperSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
'        Dim issaved As Boolean = True
'        Try
'            Dim i As Integer = 0
'            If arr.Count > 0 Then
'                Dim qry As String = "delete from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
'                clsDBFuncationality.ExecuteNonQuery(qry, tran)
'                For i = 0 To arr.Count - 1
'                    Dim coll As New Hashtable()
'                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
'                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
'                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
'                    If isReversed Then
'                        issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details_History", OMInsertOrUpdate.Insert, "", tran)
'                    End If
'                Next
'            End If

'        Catch ex As Exception
'            clsCommon.MyMessageBoxShow(ex.Message)
'        End Try
'        Return issaved
'    End Function
'    Public Shared Function getData(ByVal chalanNo As String) As List(Of clsMccDispatchReturnPaperSealDetail)
'        Return getData(chalanNo, Nothing)
'    End Function
'    Public Shared Function getData(ByVal chalanNo As String, ByVal trans As SqlTransaction) As List(Of clsMccDispatchReturnPaperSealDetail)
'        Dim arr As New List(Of clsMccDispatchReturnPaperSealDetail)
'        Try
'            Dim obj As New clsMccDispatchReturnPaperSealDetail
'            Dim q As String = "select * from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where chalan_no='" & chalanNo & "'"
'            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
'            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
'                For i As Integer = 0 To dtbl.Rows.Count - 1
'                    obj = New clsMccDispatchReturnPaperSealDetail
'                    obj.Chalan_No = clsCommon.myCstr(dtbl.Rows(i)("Chalan_No"))
'                    obj.Seal_No = clsCommon.myCstr(dtbl.Rows(i)("Seal_No"))
'                    arr.Add(obj)
'                Next
'            End If
'        Catch ex As Exception
'            clsCommon.MyMessageBoxShow(ex.Message)
'        End Try
'        Return arr
'    End Function

'End Class

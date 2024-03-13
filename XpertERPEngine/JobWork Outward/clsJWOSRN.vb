Imports System.Data.SqlClient

Public Class clsJWOSRNHead
#Region "Variables"
    Public Document_No As String = String.Empty
    Public Document_Date As Date = Nothing
    Public Document_Type As String
    Public Loc_Code As String = String.Empty
    Public Loc_Name As String = String.Empty
    Public Job_Loc_Code As String = String.Empty
    Public Job_Loc_Name As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Vendor_Name As String = String.Empty
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public Gate_Entry_No As String = String.Empty
    Public Gate_Entry_Date As Date?
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
    Public Document_Amt As Decimal = 0
    Public Total_Job_Amt As Decimal = 0
    Public Unloading_No As String = String.Empty
    Public Against_Gate_Entry_No As String = String.Empty
    Public Against_Estimate As String = String.Empty

    Public TransferFATKG As Decimal
    Public TransferFATRate As Decimal
    Public TransferFATAmt As Decimal
    Public TransferSNFKG As Decimal
    Public TransferSNFRate As Decimal
    Public TransferSNFAmt As Decimal
    Public Arr As List(Of clsJWOSRNDetail) = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsJWOSRNHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork SRN", obj.Loc_Code, obj.Document_Date, trans)

            If Not isNewEntry Then
                clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_JWO_SRN_HEAD", "Document_No", obj.Document_No, "Posted=1", trans)
            End If
            'clsBatchInventory.DeleteData("JWO-SRN", obj.Document_No, trans)
            'Dim qry As String = "Delete from TSPL_JWO_SRN_DETAIL where Document_No='" + obj.Document_No + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Job_Loc_Code", obj.Job_Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            If obj.Challan_No IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Challan_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Unloading_No", obj.Unloading_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Estimate", obj.Against_Estimate, True)
            clsCommon.AddColumnsForChange(coll, "Against_Gate_Entry_No", obj.Against_Gate_Entry_No, True)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            If obj.Gate_Entry_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date", clsCommon.GetPrintDate(obj.Gate_Entry_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Document_Amt", obj.Document_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Job_Amt", obj.Total_Job_Amt)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.JWOSRN, obj.Document_Type, obj.Loc_Code)
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in document generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_SRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_JWO_SRN_HEAD", "DOCUMENT_NO", "TSPL_JWO_SRN_DETAIL", "DOCUMENT_NO", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_JWO_SRN_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsJWOSRNDetail.saveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsJWOSRNHead
        Dim obj As clsJWOSRNHead = Nothing
        Try
            Dim qry As String = " select TSPL_JWO_SRN_HEAD.*,TSPL_LOCATION_MASTER.Location_Desc as Loc_Name,TableJobLocation.Location_Desc as Job_Loc_Name,TSPL_VENDOR_MASTER.Vendor_Name  as Vendor_Name  From TSPL_JWO_SRN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= TSPL_JWO_SRN_HEAD.Loc_Code left outer join TSPL_LOCATION_MASTER as TableJobLocation on TableJobLocation.Location_Code= TSPL_JWO_SRN_HEAD.Job_Loc_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_JWO_SRN_HEAD.Vendor_Code where 2=2"
            Dim whrCls As String = "  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            qry = qry & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qry += " and TSPL_JWO_SRN_HEAD.Document_No in ('" + strDocumentCode + "') "
                Case NavigatorType.Next
                    qry += " and TSPL_JWO_SRN_HEAD.Document_No in (select min(Document_No ) from TSPL_JWO_SRN_HEAD where Document_No  >'" + strDocumentCode + "' " & whrCls & " )"
                Case NavigatorType.First
                    qry += " and TSPL_JWO_SRN_HEAD.Document_No in (select MIN(Document_No ) from TSPL_JWO_SRN_HEAD  where 1=1 " & whrCls & " )"
                Case NavigatorType.Last
                    qry += " and TSPL_JWO_SRN_HEAD.Document_No in (select Max(Document_No ) from TSPL_JWO_SRN_HEAD  where 1=1 " & whrCls & " )"
                Case NavigatorType.Previous
                    qry += " and TSPL_JWO_SRN_HEAD.Document_No in (select Max(Document_No ) from TSPL_JWO_SRN_HEAD where Document_No  <'" + strDocumentCode + "'  " & whrCls & " )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsJWOSRNHead()
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
                obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                obj.Loc_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
                obj.Job_Loc_Code = clsCommon.myCstr(dt.Rows(0)("Job_Loc_Code"))
                obj.Job_Loc_Name = clsCommon.myCstr(dt.Rows(0)("Job_Loc_Name"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("challan_no"))
                If dt.Rows(0)("Challan_Date") IsNot DBNull.Value Then
                    obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                End If
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.Unloading_No = clsCommon.myCstr(dt.Rows(0)("Unloading_No"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Against_Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Against_Gate_Entry_No"))
                If dt.Rows(0)("Gate_Entry_Date") IsNot DBNull.Value Then
                    obj.Gate_Entry_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Entry_Date"))
                End If
                obj.Against_Estimate = clsCommon.myCstr(dt.Rows(0)("Against_Estimate"))
                obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If obj.Posted = ERPTransactionStatus.Approved Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Document_Amt = clsCommon.myCdbl(dt.Rows(0)("Document_Amt"))
                obj.Total_Job_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Job_Amt"))
                obj.Arr = clsJWOSRNDetail.GetData(obj.Document_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_JWO_SRN_HEAD", "Document_No", strDocNo, "Posted=1", trans)
            clsBatchInventory.DeleteData("JWO-SRN", strDocNo, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_JWO_SRN_HEAD", "DOCUMENT_NO", "TSPL_JWO_SRN_DETAIL", "DOCUMENT_NO", trans)
            Dim qry As String = "delete from TSPL_JWO_SRN_DETAIL where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JWO_SRN_HEAD where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_JWO_SRN_HEAD.Document_No as [SrnNo] ,TSPL_JWO_SRN_HEAD.Document_Date as [SRN Date],TSPL_JWO_SRN_HEAD.gate_entry_no as [Gate Entry No] ,TSPL_JWO_SRN_HEAD.Vendor_Code as [Vendor Code] ,TSPL_JWO_SRN_HEAD.Loc_Code as [Location Code] ,TSPL_JWO_SRN_HEAD.Challan_No as [Challan No] ,TSPL_JWO_SRN_HEAD.Challan_Date as [Challan Date] ,TSPL_JWO_SRN_HEAD.Tanker_No as [Tanker No] ,case when isnull(TSPL_JWO_SRN_HEAD.Posted,0)=0 then 'No' else 'Yes' end as Status ,TSPL_JWO_SRN_HEAD.Posted_Date as [Posting Date] ,TSPL_JWO_SRN_HEAD.Created_By as [Created By] ,TSPL_JWO_SRN_HEAD.Created_Date as [Created Date] ,TSPL_JWO_SRN_HEAD.Modified_By as [Modify By] ,TSPL_JWO_SRN_HEAD.Modified_Date as [Modify Date]   From TSPL_JWO_SRN_HEAD"
            str = clsCommon.ShowSelectForm("JWOSRNFND", qry, "SrnNo", whrcls, curcode, "SrnNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function postData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_JWO_SRN_HEAD", "Document_No", StrDocNo, "Posted=1", trans)
            Dim obj As clsJWOSRNHead = clsJWOSRNHead.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork SRN", obj.Loc_Code, obj.Document_Date, trans)
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            Dim allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.allowMilkJWOutowordWithAvgFatSNFPerAtInventory, clsFixedParameterCode.allowMilkJWOutowordWithAvgFatSNFPerAtInventory, trans)) = 1)
            Dim settJobWorkOutwardComsumeItemAccordingToBOM As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.JobWorkOutwardComsumeItemAccordingToBOM, clsFixedParameterCode.JobWorkOutwardComsumeItemAccordingToBOM, trans)) = 1)
            Dim dblTotFATKGConsuption As Decimal = 0
            Dim dblTotSNFKGConsuption As Decimal = 0
            Dim dblTotFATKGSRN As Decimal = 0
            Dim dblTotSNFKGSRN As Decimal = 0
            Dim dblTotFATKGExtra As Decimal = 0
            Dim dblTotSNFKGExtra As Decimal = 0
            Dim arrBOMItem As List(Of String) = Nothing
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim isMilkType As Boolean = False
            Dim qry As String
            Dim arrDistinctItemCode As New List(Of String)
            Dim objEst As New clsJWOEstimate
            If clsCommon.myLen(obj.Against_Estimate) > 0 Then
                arrBOMItem = New List(Of String)
                obj.TransferFATKG = 0
                obj.TransferFATAmt = 0
                obj.TransferFATRate = 0
                obj.TransferSNFKG = 0
                obj.TransferSNFRate = 0
                obj.TransferSNFAmt = 0

                objEst = clsJWOEstimate.GetData(obj.Against_Estimate, NavigatorType.Current, trans)
                If objEst.ArrWeighment IsNot Nothing AndAlso objEst.ArrWeighment.Count > 0 Then
                    For Each objtr As clsJWOEstimateTransfer In objEst.ArrWeighment
                        If Not arrDistinctItemCode.Contains(objtr.Item_Code) Then
                            arrDistinctItemCode.Add(objtr.Item_Code)
                        End If
                        qry = "select * from TSPL_INVENTORY_MOVEMENT_new where source_doc_no='" + objtr.Transfer_Code + "' and inout='I' and Trans_Type='MilkTransferJobWork'"
                        Dim dtInv As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtInv IsNot Nothing AndAlso dtInv.Rows.Count > 0 Then
                            For Each drInve As DataRow In dtInv.Rows
                                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                                objInventoryMovemnt.InOut = "O"
                                objInventoryMovemnt.main_location = clsCommon.myCstr(drInve("main_location"))
                                objInventoryMovemnt.Location_Code = clsCommon.myCstr(drInve("Location_Code"))
                                objInventoryMovemnt.Item_Code = clsCommon.myCstr(drInve("Item_Code"))
                                objInventoryMovemnt.Item_Desc = clsCommon.myCstr(drInve("Item_Desc"))
                                objInventoryMovemnt.Qty = clsCommon.myCdbl(drInve("Qty"))
                                objInventoryMovemnt.UOM = clsCommon.myCstr(drInve("UOM"))
                                objInventoryMovemnt.MRP = Nothing
                                objInventoryMovemnt.Add_Cost = Nothing
                                objInventoryMovemnt.Net_Cost = Nothing
                                objInventoryMovemnt.ItemType = clsCommon.myCstr(drInve("ItemType"))

                                objInventoryMovemnt.Basic_Cost = Nothing

                                objInventoryMovemnt.MFG_Date = Nothing
                                objInventoryMovemnt.Expiry_Date = Nothing

                                objInventoryMovemnt.FAT_KG = clsCommon.myCdbl(drInve("FAT_KG"))
                                objInventoryMovemnt.FAT_Per = clsCommon.myCdbl(drInve("FAT_Per"))
                                objInventoryMovemnt.SNF_KG = clsCommon.myCdbl(drInve("SNF_KG"))
                                objInventoryMovemnt.SNF_Per = clsCommon.myCdbl(drInve("SNF_Per"))

                                objInventoryMovemnt.Fat_Rate = clsCommon.myCdbl(drInve("Fat_Rate"))
                                objInventoryMovemnt.SNF_Rate = clsCommon.myCdbl(drInve("SNF_Rate"))
                                objInventoryMovemnt.Fat_Amt = clsCommon.myCdbl(drInve("Fat_Amt"))
                                objInventoryMovemnt.SNF_Amt = clsCommon.myCdbl(drInve("SNF_Amt"))

                                objInventoryMovemnt.FIFO_Cost = clsCommon.myCdbl(drInve("FIFO_Cost"))
                                objInventoryMovemnt.Avg_Cost = clsCommon.myCdbl(drInve("Avg_Cost"))
                                objInventoryMovemnt.LIFO_Cost = clsCommon.myCdbl(drInve("LIFO_Cost"))
                                objInventoryMovemnt.CalculateAvgCost = False
                                objInventoryMovemnt.DonNotCalculateAvgFATSNFCost = True

                                'objInventoryMovemnt.Batch_No = objtr.Item_Code
                                'objInventoryMovemnt.Ref_Line_No = objtr.SNo
                                objInventoryMovemnt.IS_CONSUMPTION = 1

                                ArrInventoryMovementNew.Add(objInventoryMovemnt)

                                obj.TransferFATKG += clsCommon.myCdbl(drInve("FAT_KG"))
                                obj.TransferFATAmt += clsCommon.myCdbl(drInve("Fat_Amt"))

                                obj.TransferSNFKG += clsCommon.myCdbl(drInve("SNF_KG"))
                                obj.TransferSNFAmt += clsCommon.myCdbl(drInve("SNF_Amt"))
                            Next
                        End If
                    Next
                    If obj.TransferFATKG > 0 Then
                        obj.TransferFATRate = obj.TransferFATAmt / obj.TransferFATKG
                    End If
                    If obj.TransferSNFKG > 0 Then
                        obj.TransferSNFRate = obj.TransferSNFAmt / obj.TransferSNFKG
                    End If
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TransferFATKG", obj.TransferFATKG)
                clsCommon.AddColumnsForChange(coll, "TransferFATRate", obj.TransferFATRate)
                clsCommon.AddColumnsForChange(coll, "TransferFATAmt", obj.TransferFATAmt)
                clsCommon.AddColumnsForChange(coll, "TransferSNFKG", obj.TransferSNFKG)
                clsCommon.AddColumnsForChange(coll, "TransferSNFRate", obj.TransferSNFRate)
                clsCommon.AddColumnsForChange(coll, "TransferSNFAmt", obj.TransferSNFAmt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_JWO_SRN_HEAD.Document_No='" + obj.Document_No + "'", trans)

                For ii As Integer = 0 To obj.Arr.Count - 1
                    obj.Arr(ii).CAL_Raw_Item_Cost = obj.Arr(ii).Job_Amount
                Next


                If objEst.ArrRawItem IsNot Nothing AndAlso objEst.ArrRawItem.Count > 0 Then
                    For Each objtr As clsJWOEstimateRawItem In objEst.ArrRawItem
                        Dim SRNIndex As Integer = -1
                        For ii As Integer = 0 To obj.Arr.Count - 1
                            If clsCommon.CompairString(obj.Arr(ii).Item_Code, objtr.Main_Item_Code) = CompairStringResult.Equal Then
                                SRNIndex = ii
                                Exit For
                            End If
                        Next
                        If SRNIndex < 0 Then
                            Throw New Exception("Estimate item [" + objtr.Main_Item_Code + "] should be in SRN")
                        End If

                        If arrDistinctItemCode.Contains(objtr.Raw_Item_Code) Then
                            Dim cost As Decimal = 0
                            If clsCommon.CompairString(objtr.Parent_Type, "FAT") = CompairStringResult.Equal Then
                                For Each objTRFatPro As clsJWOEstimateFATProduction In objEst.ArrFATProduction
                                    If clsCommon.CompairString(objTRFatPro.Item_Code, objtr.Main_Item_Code) = CompairStringResult.Equal Then
                                        cost = Math.Round((objTRFatPro.FAT_KG * obj.TransferFATRate), 2, MidpointRounding.ToEven)
                                        obj.Arr(SRNIndex).CAL_Raw_Item_Cost += cost
                                        Exit For
                                    End If
                                Next
                            ElseIf clsCommon.CompairString(objtr.Parent_Type, "SNF") = CompairStringResult.Equal Then
                                For Each objTRSNFPro As clsJWOEstimateSNFProduction In objEst.ArrSNFProducion
                                    If clsCommon.CompairString(objTRSNFPro.Item_Code, objtr.Main_Item_Code) = CompairStringResult.Equal Then
                                        cost = Math.Round((objTRSNFPro.SNF_KG * obj.TransferSNFRate), 2, MidpointRounding.ToEven)
                                        obj.Arr(SRNIndex).CAL_Raw_Item_Cost += cost
                                        Exit For
                                    End If
                                Next
                            End If
                        Else
                            If Not arrBOMItem.Contains(objtr.Raw_Item_Code) Then
                                arrBOMItem.Add(objtr.Raw_Item_Code)
                            End If

                            Dim strItemType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.Raw_Item_Code + "'", trans))
                            qry = "select Product_Type from tspl_Item_Master where Item_Code ='" & objtr.Raw_Item_Code & "'"
                            Dim Pr_Type As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If clsCommon.CompairString(Pr_Type, "MI") = CompairStringResult.Equal Then
                                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                                objInventoryMovemnt.InOut = "O"
                                objInventoryMovemnt.main_location = obj.Loc_Code
                                objInventoryMovemnt.Location_Code = obj.Job_Loc_Code
                                'objInventoryMovemnt.Other_Location_Code = obj.Arr(ii).to_loc_code
                                'objInventoryMovemnt.Other_Location_Desc = obj.Arr(ii).to_loc_desc
                                objInventoryMovemnt.Item_Code = objtr.Raw_Item_Code
                                objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objtr.Raw_Item_Code, trans)
                                objInventoryMovemnt.Qty = objtr.Raw_Item_Qty
                                objInventoryMovemnt.UOM = objtr.Raw_Item_UOM
                                objInventoryMovemnt.MRP = Nothing
                                objInventoryMovemnt.Add_Cost = Nothing
                                objInventoryMovemnt.Net_Cost = Nothing

                                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "RM"
                                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "FT"
                                Else
                                    objInventoryMovemnt.ItemType = strItemType
                                End If
                                objInventoryMovemnt.Basic_Cost = Nothing
                                objInventoryMovemnt.Batch_No = ""
                                objInventoryMovemnt.MFG_Date = Nothing
                                objInventoryMovemnt.Expiry_Date = Nothing
                                Dim cost As Decimal
                                Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(Pr_Type, objtr.Raw_Item_Code, obj.Job_Loc_Code, objtr.Raw_Item_Qty, objtr.Raw_Item_UOM, objtr.Raw_Item_FAT_KG, objtr.Raw_Item_SNF_KG, obj.Document_Date, obj.Document_Date, True, trans, obj.Document_No)
                                If objMCT IsNot Nothing Then
                                    objInventoryMovemnt.FAT_KG = objtr.Raw_Item_FAT_KG
                                    objInventoryMovemnt.FAT_Per = objtr.Raw_Item_FAT_Per
                                    objInventoryMovemnt.SNF_KG = objtr.Raw_Item_SNF_KG
                                    objInventoryMovemnt.SNF_Per = objtr.Raw_Item_SNF_Per

                                    objInventoryMovemnt.Fat_Rate = Math.Abs(Math.Round(If(objtr.Raw_Item_FAT_KG <= 0, 0, objMCT.FAT_Cost / objtr.Raw_Item_FAT_KG), 2))
                                    objInventoryMovemnt.SNF_Rate = Math.Abs(Math.Round(If(objtr.Raw_Item_SNF_KG <= 0, 0, objMCT.SNF_Cost / objtr.Raw_Item_SNF_KG), 2))
                                    objInventoryMovemnt.Fat_Amt = objMCT.FAT_Cost
                                    objInventoryMovemnt.SNF_Amt = objMCT.SNF_Cost
                                    cost = objMCT.FAT_Cost + objMCT.SNF_Cost
                                Else
                                    Throw New Exception("Avg cost is found for item " + objtr.Raw_Item_Code)
                                End If

                                objInventoryMovemnt.FIFO_Cost = cost
                                objInventoryMovemnt.Avg_Cost = cost
                                objInventoryMovemnt.LIFO_Cost = cost
                                objInventoryMovemnt.CalculateAvgCost = False
                                objInventoryMovemnt.DonNotCalculateAvgFATSNFCost = True

                                objInventoryMovemnt.Ref_Line_No = obj.Arr(SRNIndex).SNo
                                objInventoryMovemnt.Batch_No = objtr.Main_Item_Code
                                objInventoryMovemnt.IS_CONSUMPTION = 1
                                ArrInventoryMovementNew.Add(objInventoryMovemnt)

                                obj.Arr(SRNIndex).CAL_Raw_Item_Cost += cost
                            Else
                                Dim objInventoryMovemnt As New clsInventoryMovement()
                                objInventoryMovemnt.InOut = "O"
                                objInventoryMovemnt.Location_Code = obj.Job_Loc_Code
                                'objInventoryMovemnt.Other_Location_Code = obj.Arr(ii).to_loc_code
                                'objInventoryMovemnt.Other_Location_Desc = obj.Arr(ii).to_loc_desc
                                objInventoryMovemnt.Item_Code = objtr.Raw_Item_Code
                                objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objtr.Raw_Item_Code, trans)
                                objInventoryMovemnt.Qty = objtr.Raw_Item_Qty
                                objInventoryMovemnt.UOM = objtr.Raw_Item_UOM
                                objInventoryMovemnt.MRP = Nothing
                                objInventoryMovemnt.Add_Cost = Nothing
                                objInventoryMovemnt.Net_Cost = Nothing
                                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "RM"
                                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "FT"
                                Else
                                    objInventoryMovemnt.ItemType = strItemType
                                End If
                                'objInventoryMovemnt.Batch_No = obj.Arr(ii).Item_Code
                                objInventoryMovemnt.MFG_Date = Nothing
                                objInventoryMovemnt.Expiry_Date = Nothing
                                Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(Pr_Type, objtr.Raw_Item_Code, obj.Job_Loc_Code, objtr.Raw_Item_Qty, objtr.Raw_Item_UOM, objtr.Raw_Item_FAT_KG, objtr.Raw_Item_SNF_KG, obj.Document_Date, obj.Document_Date, True, trans, obj.Document_No)
                                Dim cost As Decimal = 0
                                If objMCT IsNot Nothing Then
                                    If clsCommon.CompairString(Pr_Type, "MP") = CompairStringResult.Equal Then
                                        objInventoryMovemnt.FAT_KG = objtr.Raw_Item_FAT_KG
                                        objInventoryMovemnt.FAT_Per = objtr.Raw_Item_FAT_Per
                                        objInventoryMovemnt.SNF_KG = objtr.Raw_Item_SNF_KG
                                        objInventoryMovemnt.SNF_Per = objtr.Raw_Item_SNF_Per

                                        objInventoryMovemnt.Fat_Rate = Math.Abs(Math.Round(If(objtr.Raw_Item_FAT_KG <= 0, 0, objMCT.FAT_Cost / objtr.Raw_Item_FAT_KG), 2))
                                        objInventoryMovemnt.SNF_Rate = Math.Abs(Math.Round(If(objtr.Raw_Item_SNF_KG <= 0, 0, objMCT.SNF_Cost / objtr.Raw_Item_SNF_KG), 2))
                                        objInventoryMovemnt.Fat_Amt = objMCT.FAT_Cost
                                        objInventoryMovemnt.SNF_Amt = objMCT.SNF_Cost
                                    End If
                                    cost = objMCT.FAT_Cost + objMCT.SNF_Cost
                                Else
                                    Throw New Exception("Avg cost is not found for item " + objtr.Raw_Item_Code)
                                End If
                                objInventoryMovemnt.FIFO_Cost = cost
                                objInventoryMovemnt.Avg_Cost = cost
                                objInventoryMovemnt.LIFO_Cost = cost
                                objInventoryMovemnt.Basic_Cost = If(objtr.Raw_Item_Qty <= 0, 0, cost / objtr.Raw_Item_Qty)
                                objInventoryMovemnt.CalculateAvgCost = False
                                objInventoryMovemnt.Ref_Line_No = obj.Arr(SRNIndex).SNo
                                objInventoryMovemnt.Batch_No = objtr.Main_Item_Code
                                objInventoryMovemnt.IS_CONSUMPTION = 1
                                ArrInventoryMovement.Add(objInventoryMovemnt)
                                obj.Arr(SRNIndex).CAL_Raw_Item_Cost += cost
                            End If
                        End If
                    Next
                End If
            Else
                If settJobWorkOutwardComsumeItemAccordingToBOM Then
                    If allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory Then
                        Dim arrADJByProduct As New List(Of ClsAdjustmentsDetails)
                        arrBOMItem = New List(Of String)
                        For ii As Integer = 0 To obj.Arr.Count - 1
                            obj.Arr(ii).CAL_Raw_Item_Cost = obj.Arr(ii).Job_Amount
                            dblTotFATKGSRN += obj.Arr(ii).FAT_KG
                            dblTotSNFKGSRN += obj.Arr(ii).SNF_KG
                            Dim arrRecuItem As New List(Of clsRecursiveitems)
                            Dim dblTotFATKGExtraInner As Decimal = 0
                            Dim dblTotSNFKGExtraInner As Decimal = 0
                            clsRecursiveitems.GetItemOfBOM(dblTotFATKGExtraInner, dblTotSNFKGExtraInner, arrRecuItem, obj.Arr(ii).Item_Code, obj.Arr(ii).Qty, obj.Arr(ii).UOM, obj.Job_Loc_Code, obj.Vendor_Code, obj.Document_Date, trans, 1)
                            dblTotFATKGExtra += dblTotFATKGExtraInner
                            dblTotSNFKGExtra += dblTotSNFKGExtraInner
                            If arrRecuItem IsNot Nothing AndAlso arrRecuItem.Count > 0 Then
                                For Each objRecuItem As clsRecursiveitems In arrRecuItem
                                    dblTotFATKGConsuption += objRecuItem.FAT_KG
                                    dblTotSNFKGConsuption += objRecuItem.SNF_KG
                                    If Not arrBOMItem.Contains(objRecuItem.ITEM_CODE) Then
                                        arrBOMItem.Add(objRecuItem.ITEM_CODE)
                                    End If
                                    Dim strItemType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objRecuItem.ITEM_CODE + "'", trans))
                                    qry = "select Product_Type from tspl_Item_Master where Item_Code ='" & objRecuItem.ITEM_CODE & "'"
                                    Dim Pr_Type As String = clsDBFuncationality.getSingleValue(qry, trans)
                                    If clsCommon.CompairString(Pr_Type, "MI") = CompairStringResult.Equal Then
                                        Dim objInventoryMovemnt As New clsInventoryMovementNew()
                                        objInventoryMovemnt.InOut = "O"
                                        objInventoryMovemnt.main_location = obj.Loc_Code
                                        objInventoryMovemnt.Location_Code = obj.Job_Loc_Code
                                        'objInventoryMovemnt.Other_Location_Code = obj.Arr(ii).to_loc_code
                                        'objInventoryMovemnt.Other_Location_Desc = obj.Arr(ii).to_loc_desc
                                        objInventoryMovemnt.Item_Code = objRecuItem.ITEM_CODE
                                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objRecuItem.ITEM_CODE, trans)
                                        objInventoryMovemnt.Qty = objRecuItem.QUANTITY
                                        objInventoryMovemnt.UOM = objRecuItem.UNIT_CODE
                                        objInventoryMovemnt.MRP = Nothing
                                        objInventoryMovemnt.Add_Cost = Nothing
                                        objInventoryMovemnt.Net_Cost = Nothing

                                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                            objInventoryMovemnt.ItemType = "RM"
                                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                            objInventoryMovemnt.ItemType = "FT"
                                        Else
                                            objInventoryMovemnt.ItemType = strItemType
                                        End If
                                        objInventoryMovemnt.Basic_Cost = Nothing
                                        objInventoryMovemnt.Batch_No = obj.Arr(ii).Item_Code
                                        objInventoryMovemnt.MFG_Date = Nothing
                                        objInventoryMovemnt.Expiry_Date = Nothing
                                        Dim cost As Decimal
                                        Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(Pr_Type, objRecuItem.ITEM_CODE, obj.Job_Loc_Code, objRecuItem.QUANTITY, objRecuItem.UNIT_CODE, objRecuItem.FAT_KG, objRecuItem.SNF_KG, obj.Document_Date, obj.Document_Date, True, trans, obj.Document_No)
                                        If objMCT IsNot Nothing Then
                                            objInventoryMovemnt.FAT_KG = objRecuItem.FAT_KG
                                            objInventoryMovemnt.FAT_Per = objRecuItem.FAT
                                            objInventoryMovemnt.SNF_KG = objRecuItem.SNF_KG
                                            objInventoryMovemnt.SNF_Per = objRecuItem.SNF

                                            objInventoryMovemnt.Fat_Rate = Math.Abs(Math.Round(If(objRecuItem.FAT_KG <= 0, 0, objMCT.FAT_Cost / objRecuItem.FAT_KG), 2))
                                            objInventoryMovemnt.SNF_Rate = Math.Abs(Math.Round(If(objRecuItem.SNF_KG <= 0, 0, objMCT.SNF_Cost / objRecuItem.SNF_KG), 2))
                                            objInventoryMovemnt.Fat_Amt = objMCT.FAT_Cost
                                            objInventoryMovemnt.SNF_Amt = objMCT.SNF_Cost

                                            cost = objMCT.FAT_Cost + objMCT.SNF_Cost

                                            obj.Arr(ii).CAL_Raw_Item_Cost += cost
                                        Else
                                            Throw New Exception("Avg cost is found for item " + objRecuItem.ITEM_CODE)
                                        End If

                                        objInventoryMovemnt.FIFO_Cost = cost
                                        objInventoryMovemnt.Avg_Cost = cost
                                        objInventoryMovemnt.LIFO_Cost = cost
                                        objInventoryMovemnt.CalculateAvgCost = False
                                        objInventoryMovemnt.DonNotCalculateAvgFATSNFCost = True
                                        objInventoryMovemnt.Ref_Line_No = obj.Arr(ii).SNo
                                        objInventoryMovemnt.IS_CONSUMPTION = 1
                                        ArrInventoryMovementNew.Add(objInventoryMovemnt)
                                    Else
                                        Dim objInventoryMovemnt As New clsInventoryMovement()
                                        objInventoryMovemnt.InOut = "O"
                                        objInventoryMovemnt.Location_Code = obj.Job_Loc_Code
                                        'objInventoryMovemnt.Other_Location_Code = obj.Arr(ii).to_loc_code
                                        'objInventoryMovemnt.Other_Location_Desc = obj.Arr(ii).to_loc_desc
                                        objInventoryMovemnt.Item_Code = objRecuItem.ITEM_CODE
                                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objRecuItem.ITEM_CODE, trans)
                                        objInventoryMovemnt.Qty = objRecuItem.QUANTITY
                                        objInventoryMovemnt.UOM = objRecuItem.UNIT_CODE
                                        objInventoryMovemnt.MRP = Nothing
                                        objInventoryMovemnt.Add_Cost = Nothing
                                        objInventoryMovemnt.Net_Cost = Nothing
                                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                            objInventoryMovemnt.ItemType = "RM"
                                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                            objInventoryMovemnt.ItemType = "FT"
                                        Else
                                            objInventoryMovemnt.ItemType = strItemType
                                        End If
                                        objInventoryMovemnt.Batch_No = obj.Arr(ii).Item_Code
                                        objInventoryMovemnt.MFG_Date = Nothing
                                        objInventoryMovemnt.Expiry_Date = Nothing
                                        Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(Pr_Type, objRecuItem.ITEM_CODE, obj.Job_Loc_Code, objRecuItem.QUANTITY, objRecuItem.UNIT_CODE, objRecuItem.FAT_KG, objRecuItem.SNF_KG, obj.Document_Date, obj.Document_Date, True, trans, obj.Document_No)
                                        Dim cost As Decimal = 0
                                        If objMCT IsNot Nothing Then
                                            If clsCommon.CompairString(Pr_Type, "MP") = CompairStringResult.Equal Then
                                                objInventoryMovemnt.FAT_KG = objRecuItem.FAT_KG
                                                objInventoryMovemnt.FAT_Per = objRecuItem.FAT
                                                objInventoryMovemnt.SNF_KG = objRecuItem.SNF_KG
                                                objInventoryMovemnt.SNF_Per = objRecuItem.SNF

                                                objInventoryMovemnt.Fat_Rate = Math.Abs(Math.Round(If(objRecuItem.FAT_KG <= 0, 0, objMCT.FAT_Cost / objRecuItem.FAT_KG), 2))
                                                objInventoryMovemnt.SNF_Rate = Math.Abs(Math.Round(If(objRecuItem.SNF_KG <= 0, 0, objMCT.SNF_Cost / objRecuItem.SNF_KG), 2))
                                                objInventoryMovemnt.Fat_Amt = objMCT.FAT_Cost
                                                objInventoryMovemnt.SNF_Amt = objMCT.SNF_Cost
                                            End If
                                            cost = objMCT.FAT_Cost + objMCT.SNF_Cost
                                            obj.Arr(ii).CAL_Raw_Item_Cost += cost
                                        Else
                                            Throw New Exception("Avg cost is found for item " + objRecuItem.ITEM_CODE)
                                        End If
                                        objInventoryMovemnt.FIFO_Cost = cost
                                        objInventoryMovemnt.Avg_Cost = cost
                                        objInventoryMovemnt.LIFO_Cost = cost
                                        objInventoryMovemnt.Basic_Cost = If(objRecuItem.QUANTITY <= 0, 0, cost / objRecuItem.QUANTITY)
                                        objInventoryMovemnt.CalculateAvgCost = False
                                        objInventoryMovemnt.Ref_Line_No = obj.Arr(ii).SNo
                                        objInventoryMovemnt.IS_CONSUMPTION = 1
                                        ArrInventoryMovement.Add(objInventoryMovemnt)
                                    End If
                                Next
                            Else
                                Throw New Exception("Please create BOM form item-" + obj.Arr(ii).Item_Code + ",vendor-" + obj.Vendor_Code + ",Location-" + obj.Loc_Code)
                            End If
                        Next

                    Else
                        ''Make stock Adjustment Entry to reduce stock from job location
                        Dim objAdj As New ClsAdjustments
                        objAdj.Trans_Type = "Out"
                        objAdj.Adjustment_Date = obj.Document_Date
                        objAdj.Posting_Date = obj.Document_Date
                        objAdj.EntryDateTime = obj.Document_Date
                        objAdj.Loc_Code = obj.Job_Loc_Code
                        objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                        objAdj.Description = "Adjustment for Stock Out against JWO SRN No :" + obj.Document_No + ""
                        objAdj.Reference_Document = "JWO-SRN-JLO"
                        objAdj.Document_No = obj.Document_No
                        objAdj.MainLocationCode = obj.Loc_Code
                        objAdj.MainLocationDesc = obj.Loc_Name
                        objAdj.Arr = New List(Of ClsAdjustmentsDetails)

                        Dim arrADJMilk As New List(Of ClsAdjustmentsDetails)
                        Dim arrADJOther As New List(Of ClsAdjustmentsDetails)
                        Dim arrADJByProduct As New List(Of ClsAdjustmentsDetails)
                        arrBOMItem = New List(Of String)
                        For ii As Integer = 0 To obj.Arr.Count - 1
                            obj.Arr(ii).CAL_Raw_Item_Cost = obj.Arr(ii).Amount
                            dblTotFATKGSRN += obj.Arr(ii).FAT_KG
                            dblTotSNFKGSRN += obj.Arr(ii).SNF_KG
                            Dim arrRecuItem As New List(Of clsRecursiveitems)
                            Dim dblTotFATKGExtraInner As Decimal = 0
                            Dim dblTotSNFKGExtraInner As Decimal = 0
                            clsRecursiveitems.GetItemOfBOM(dblTotFATKGExtraInner, dblTotSNFKGExtraInner, arrRecuItem, obj.Arr(ii).Item_Code, obj.Arr(ii).Qty, obj.Arr(ii).UOM, obj.Job_Loc_Code, obj.Vendor_Code, obj.Document_Date, trans, 1)
                            dblTotFATKGExtra += dblTotFATKGExtraInner
                            dblTotSNFKGExtra += dblTotSNFKGExtraInner
                            If arrRecuItem IsNot Nothing AndAlso arrRecuItem.Count > 0 Then
                                For Each objRecuItem As clsRecursiveitems In arrRecuItem
                                    Dim objAdjTR As New ClsAdjustmentsDetails()
                                    objAdjTR.Item_Code = objRecuItem.ITEM_CODE
                                    arrBOMItem.Add(objAdjTR.Item_Code)
                                    objAdjTR.Item_Description = clsCommon.myCstr(clsItemMaster.GetItemName(objAdjTR.Item_Code, trans))
                                    objAdjTR.Adjustment_Type = "BD"
                                    objAdjTR.Item_Quantity = objRecuItem.QUANTITY
                                    objAdjTR.mrp = 0
                                    objAdjTR.Unit_Code = objRecuItem.UNIT_CODE
                                    objAdjTR.Unit_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objAdjTR.Item_Code, obj.Job_Loc_Code, 1, objAdj.Adjustment_Date, objAdj.Adjustment_Date, True, trans)
                                    objAdjTR.Item_Cost = objAdjTR.Unit_Cost * objAdjTR.Item_Quantity
                                    objAdjTR.fat_pers = objRecuItem.FAT
                                    objAdjTR.fat_kg = objRecuItem.FAT_KG
                                    objAdjTR.snf_pers = objRecuItem.SNF
                                    objAdjTR.snf_kg = objRecuItem.SNF_KG

                                    dblTotFATKGConsuption += objRecuItem.FAT_KG
                                    dblTotSNFKGConsuption += objRecuItem.SNF_KG

                                    'objAdj.Arr.Add(objAdjTR)
                                    qry = "select Product_Type from tspl_Item_Master where Item_Code ='" & objRecuItem.ITEM_CODE & "'"
                                    Dim Pr_Type As String = clsDBFuncationality.getSingleValue(qry, trans)
                                    If clsCommon.CompairString(Pr_Type, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(Pr_Type, "MP") = CompairStringResult.Equal Then
                                        Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost("MI", objAdjTR.Item_Code, obj.Job_Loc_Code, 1, objAdjTR.Unit_Code, 1, 1, objAdj.Adjustment_Date, objAdj.Adjustment_Date, True, trans, objAdj.Adjustment_No)
                                        If objMCT IsNot Nothing Then
                                            objAdjTR.fat_Rate = objMCT.FAT_Cost
                                            objAdjTR.snf_Rate = objMCT.SNF_Cost
                                            objAdjTR.fat_Amt = objAdjTR.fat_kg * objAdjTR.fat_Rate
                                            objAdjTR.snf_Amt = objAdjTR.snf_kg * objAdjTR.snf_Rate
                                        End If
                                    End If

                                    If clsCommon.CompairString(Pr_Type, "MI") = CompairStringResult.Equal Then
                                        objAdjTR.Adjustment_Line_No = arrADJMilk.Count + 1
                                        arrADJMilk.Add(objAdjTR)
                                    Else
                                        objAdjTR.Adjustment_Line_No = arrADJMilk.Count + 1
                                        arrADJOther.Add(objAdjTR)
                                    End If
                                    If clsCommon.myLen(objRecuItem.Byproduct_Item_Code) > 0 Then
                                        objAdjTR = New ClsAdjustmentsDetails()
                                        objAdjTR.Item_Code = objRecuItem.Byproduct_Item_Code
                                        objAdjTR.Item_Description = clsCommon.myCstr(clsItemMaster.GetItemName(objAdjTR.Item_Code, trans))
                                        objAdjTR.Adjustment_Type = "BI"
                                        objAdjTR.Item_Quantity = objRecuItem.Byproduct_Item_Qty
                                        objAdjTR.mrp = 0
                                        objAdjTR.Unit_Code = objRecuItem.Byproduct_Item_UOM
                                        objAdjTR.Unit_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objAdjTR.Item_Code, obj.Job_Loc_Code, 1, objAdj.Adjustment_Date, objAdj.Adjustment_Date, True, trans)
                                        objAdjTR.Item_Cost = objAdjTR.Unit_Cost * objAdjTR.Item_Quantity
                                        objAdjTR.fat_kg = Math.Abs(obj.Arr(ii).FAT_KG - objRecuItem.FAT_KG)
                                        objAdjTR.fat_pers = objAdjTR.fat_kg / objAdjTR.Item_Quantity
                                        objAdjTR.snf_kg = Math.Abs(obj.Arr(ii).SNF_KG - objRecuItem.SNF_KG)
                                        objAdjTR.snf_pers = objAdjTR.snf_kg / objAdjTR.Item_Quantity
                                        qry = "select Product_Type from tspl_Item_Master where Item_Code ='" & objRecuItem.Byproduct_Item_Code & "'"
                                        Pr_Type = clsDBFuncationality.getSingleValue(qry, trans)
                                        If clsCommon.CompairString(Pr_Type, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(Pr_Type, "MP") = CompairStringResult.Equal Then
                                            Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost("MI", objAdjTR.Item_Code, obj.Job_Loc_Code, 1, objAdjTR.Unit_Code, 1, 1, objAdj.Adjustment_Date, objAdj.Adjustment_Date, True, trans, objAdj.Adjustment_No)
                                            If objMCT IsNot Nothing Then
                                                objAdjTR.fat_Rate = objMCT.FAT_Cost
                                                objAdjTR.snf_Rate = objMCT.SNF_Cost
                                                objAdjTR.fat_Amt = objAdjTR.fat_kg * objAdjTR.fat_Rate
                                                objAdjTR.snf_Amt = objAdjTR.snf_kg * objAdjTR.snf_Rate
                                            End If
                                        End If
                                        objAdjTR.Adjustment_Line_No = arrADJByProduct.Count + 1
                                        arrADJByProduct.Add(objAdjTR)
                                    End If
                                Next
                            Else
                                Throw New Exception("Please create BOM form item-" + obj.Arr(ii).Item_Code + ",vendor-" + obj.Vendor_Code + ",Location-" + obj.Loc_Code)
                            End If
                        Next
                        If arrADJOther IsNot Nothing AndAlso arrADJOther.Count > 0 Then
                            objAdj.Adjustment_No = ""
                            objAdj.IsMilkType = 0
                            objAdj.Arr = arrADJOther
                            objAdj.SaveData(objAdj, True, "", trans)
                            ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                        End If

                        If arrADJMilk IsNot Nothing AndAlso arrADJMilk.Count > 0 Then
                            If dblTotFATKGExtra > 0 Or dblTotSNFKGExtra > 0 Then
                                If dblTotFATKGSRN > dblTotFATKGConsuption OrElse dblTotSNFKGSRN > dblTotSNFKGConsuption Then
                                    Dim objAdjTR As New ClsAdjustmentsDetails()
                                    objAdjTR.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
                                    If clsCommon.myLen(objAdjTR.Item_Code) <= 0 Then
                                        Throw New Exception("Please set MCC default milk item in fixed parameter")
                                    End If
                                    arrBOMItem.Add(objAdjTR.Item_Code)
                                    objAdjTR.Item_Description = clsCommon.myCstr(clsItemMaster.GetItemName(objAdjTR.Item_Code, trans))
                                    objAdjTR.Adjustment_Type = "BD"
                                    objAdjTR.Item_Quantity = 0
                                    objAdjTR.mrp = 0
                                    objAdjTR.Unit_Code = clsItemMaster.GetStockUnit(objAdjTR.Item_Code, trans)
                                    objAdjTR.Unit_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objAdjTR.Item_Code, obj.Job_Loc_Code, 1, objAdj.Adjustment_Date, objAdj.Adjustment_Date, True, trans)
                                    objAdjTR.Item_Cost = objAdjTR.Unit_Cost * objAdjTR.Item_Quantity
                                    objAdjTR.fat_pers = 0
                                    If dblTotFATKGSRN > dblTotFATKGConsuption Then
                                        objAdjTR.fat_kg = dblTotFATKGSRN - dblTotFATKGConsuption
                                    Else
                                        objAdjTR.fat_kg = 0
                                    End If
                                    objAdjTR.snf_pers = 0
                                    If dblTotSNFKGSRN > dblTotSNFKGConsuption Then
                                        objAdjTR.snf_kg = dblTotSNFKGSRN - dblTotSNFKGConsuption
                                    Else
                                        objAdjTR.snf_kg = 0
                                    End If
                                    objAdjTR.Remarks = "Extra Raw Milk"
                                    Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost("MI", objAdjTR.Item_Code, obj.Job_Loc_Code, 1, objAdjTR.Unit_Code, 1, 1, objAdj.Adjustment_Date, objAdj.Adjustment_Date, True, trans, objAdj.Adjustment_No)
                                    If objMCT IsNot Nothing Then
                                        objAdjTR.fat_Rate = objMCT.FAT_Cost
                                        objAdjTR.snf_Rate = objMCT.SNF_Cost
                                        objAdjTR.fat_Amt = objAdjTR.fat_kg * objAdjTR.fat_Rate
                                        objAdjTR.snf_Amt = objAdjTR.snf_kg * objAdjTR.snf_Rate
                                    End If
                                    objAdjTR.Adjustment_Line_No = arrADJMilk.Count + 1
                                    arrADJMilk.Add(objAdjTR)

                                    dblTotFATKGConsuption += objAdjTR.fat_kg
                                    dblTotSNFKGConsuption += objAdjTR.snf_kg
                                End If
                            End If
                            objAdj.Adjustment_No = ""
                            objAdj.IsMilkType = 1
                            objAdj.Arr = arrADJMilk
                            objAdj.SaveData(objAdj, True, "", trans)
                            ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                        End If

                        If arrADJByProduct IsNot Nothing AndAlso arrADJByProduct.Count > 0 Then
                            If arrADJByProduct.Count > 1 Then
                                Throw New Exception("There are more than one Byproduct item used in BOM of SRN No-" + StrDocNo)
                            End If

                            arrADJByProduct(0).fat_kg = dblTotFATKGConsuption - dblTotFATKGSRN
                            arrADJByProduct(0).snf_kg = dblTotSNFKGConsuption - dblTotSNFKGSRN
                            If arrADJByProduct(0).fat_kg < 0 Then
                                Throw New Exception("FAT % age definded at BOM is incorrect.Calculated FAT KG is going to " + clsCommon.myCstr(arrADJByProduct(0).fat_kg))
                            End If
                            If arrADJByProduct(0).snf_kg < 0 Then
                                Throw New Exception("SNF % age definded at BOM is incorrect.Calculated SNF KG is going to " + clsCommon.myCstr(arrADJByProduct(0).snf_kg))
                            End If
                            arrADJByProduct(0).fat_Amt = arrADJByProduct(0).fat_kg * arrADJByProduct(0).fat_Rate
                            arrADJByProduct(0).snf_Amt = arrADJByProduct(0).snf_kg * arrADJByProduct(0).snf_Rate

                            objAdj.Trans_Type = "In"
                            objAdj.Adjustment_Date = obj.Document_Date
                            objAdj.Posting_Date = obj.Document_Date
                            objAdj.EntryDateTime = obj.Document_Date
                            objAdj.Loc_Code = obj.Job_Loc_Code
                            objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                            objAdj.Description = "Adjustment for Stock In Of Byproduct against JWO SRN No :" + obj.Document_No + ""
                            objAdj.Reference_Document = "JWO-SRN-JLI"
                            objAdj.Document_No = obj.Document_No
                            objAdj.Adjustment_No = ""
                            objAdj.IsMilkType = 0
                            objAdj.Arr = arrADJByProduct
                            objAdj.SaveData(objAdj, True, "", trans)
                            ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                        End If

                        If arrBOMItem IsNot Nothing AndAlso arrBOMItem.Count > 0 Then
                            For Each strIcode As String In arrBOMItem
                                Dim bal As Decimal = clsItemLocationDetails.getBalance(strIcode, obj.Job_Loc_Code, "", obj.Document_Date, trans, clsItemMaster.GetStockUnit(strIcode, trans), 0)
                                If bal < 0 Then
                                    Throw New Exception("Stock Balance going to negative " + Environment.NewLine + " Item - " + strIcode + " and location - " + obj.Job_Loc_Code)
                                End If
                            Next
                        End If
                        ''End of Make stock Adjustment Entry to reduce stock from job location
                    End If
                End If
            End If

            For Each objtr As clsJWOSRNDetail In obj.Arr
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
                Dim strItemUnitCode As String = objtr.UOM

                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, strItemUnitCode, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objtr.Item_Code + " and Uom:'" + strItemUnitCode)
                End If
                qry = "select Product_Type from tspl_Item_Master where Item_Code ='" & objtr.Item_Code & "'"
                Dim Pr_Type As String = clsDBFuncationality.getSingleValue(qry, trans)
                If Pr_Type = "MI" Then
                    Dim objInventoryMovemnt As New clsInventoryMovementNew()
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.Loc_Code
                    objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Desc
                    objInventoryMovemnt.Qty = objtr.Qty
                    objInventoryMovemnt.UOM = objtr.UOM
                    objInventoryMovemnt.MRP = 0
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.FAT_Per = objtr.FAT_Per
                    objInventoryMovemnt.SNF_Per = objtr.SNF_Per
                    objInventoryMovemnt.FAT_KG = objtr.FAT_KG
                    objInventoryMovemnt.SNF_KG = objtr.SNF_KG
                    objInventoryMovemnt.Net_Cost = objtr.CAL_Raw_Item_Cost
                    objInventoryMovemnt.main_location = obj.Loc_Code
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    objInventoryMovemnt.Basic_Cost = objtr.CAL_Raw_Item_Cost / objtr.Qty
                    ArrInventoryMovementNew.Add(objInventoryMovemnt)
                Else
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.Loc_Code
                    objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Desc
                    objInventoryMovemnt.Qty = objtr.Qty
                    objInventoryMovemnt.UOM = objtr.UOM
                    'objInventoryMovemnt.MRP = objtr.Amount
                    objInventoryMovemnt.Add_Cost = objtr.CAL_Raw_Item_Cost
                    objInventoryMovemnt.Net_Cost = objtr.CAL_Raw_Item_Cost
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    objInventoryMovemnt.Basic_Cost = objtr.CAL_Raw_Item_Cost / objtr.Qty
                    objInventoryMovemnt.Batch_No = Nothing
                    objInventoryMovemnt.MFG_Date = Nothing
                    objInventoryMovemnt.Expiry_Date = Nothing
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
            Next

            If ArrInventoryMovementNew IsNot Nothing AndAlso ArrInventoryMovementNew.Count > 0 Then
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("JWO-SRN", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If
            If ArrInventoryMovement IsNot Nothing AndAlso ArrInventoryMovement.Count > 0 Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("JWO-SRN", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            If arrBOMItem IsNot Nothing AndAlso arrBOMItem.Count > 0 Then
                For Each strIcode As String In arrBOMItem
                    Dim bal As Decimal = clsItemLocationDetails.getBalance(strIcode, obj.Job_Loc_Code, "", obj.Document_Date, trans, clsItemMaster.GetStockUnit(strIcode, trans), 0)
                    If bal < 0 Then
                        Throw New Exception("Stock Balance going to negative " + Environment.NewLine + " Item - " + strIcode + " and location - " + obj.Job_Loc_Code)
                    End If
                Next
            End If



            'Create GL Entry
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                Dim ArryLst As ArrayList = New ArrayList()
                For Each objtr As clsJWOSRNDetail In obj.Arr
                    Dim InvAmt As Decimal = 0
                    If clsCommon.myLen(obj.Against_Estimate) > 0 Then
                        Dim cost As Decimal = -1
                        Dim strICode As String = ""

                        For Each objtrJWER As clsJWOEstimateRawItem In objEst.ArrRawItem
                            If clsCommon.CompairString(objtrJWER.Main_Item_Code, objtr.Item_Code) = CompairStringResult.Equal Then
                                If arrDistinctItemCode.Contains(objtrJWER.Raw_Item_Code) Then
                                    If clsCommon.CompairString(objtrJWER.Parent_Type, "FAT") = CompairStringResult.Equal Then
                                        For Each objTRFatPro As clsJWOEstimateFATProduction In objEst.ArrFATProduction
                                            If clsCommon.CompairString(objTRFatPro.Item_Code, objtrJWER.Main_Item_Code) = CompairStringResult.Equal Then
                                                cost = Math.Round((objTRFatPro.FAT_KG * obj.TransferFATRate), 2, MidpointRounding.ToEven)
                                                strICode = objtrJWER.Raw_Item_Code
                                                Exit For
                                            End If
                                        Next
                                    ElseIf clsCommon.CompairString(objtrJWER.Parent_Type, "SNF") = CompairStringResult.Equal Then
                                        For Each objTRSNFPro As clsJWOEstimateSNFProduction In objEst.ArrSNFProducion
                                            If clsCommon.CompairString(objTRSNFPro.Item_Code, objtrJWER.Main_Item_Code) = CompairStringResult.Equal Then
                                                cost = Math.Round((objTRSNFPro.SNF_KG * obj.TransferSNFRate), 2, MidpointRounding.ToEven)
                                                strICode = objtrJWER.Raw_Item_Code
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    If cost >= 0 Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If cost > 0 Then
                            qry = " select Purchase_JobWork  from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + strICode + "' "
                            Dim dtinner As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtinner Is Nothing OrElse dtinner.Rows.Count <= 0 Then
                                Throw New Exception("Please Map Purchase Account Set for item  " + strICode)
                            End If
                            Dim PurchaseJobWorkAcc As String = clsCommon.myCstr(dtinner.Rows(0)("Purchase_JobWork"))
                            If clsCommon.myLen(PurchaseJobWorkAcc) <= 0 Then
                                Throw New Exception("Please Map  Purchase Job Work A/C From Purchase Account Set For Item : " & clsCommon.myCstr(strICode) & " (" & clsItemMaster.GetItemName(strICode, trans) & ")")
                            End If
                            PurchaseJobWorkAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(PurchaseJobWorkAcc, obj.Loc_Code, trans)
                            ArryLst.Add(New String() {PurchaseJobWorkAcc, -1 * cost})
                            InvAmt += cost
                        End If
                    End If

                    qry = " select TSPL_PURCHASE_ACCOUNTS.Job_Work_Ac,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objtr.Item_Code + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory Then
                            qry = "select Item_Code,Avg_Cost  from tspl_inventory_movement_new where Source_Doc_No='" + obj.Document_No + "' and IS_CONSUMPTION=1 and Trans_Type='JWO-SRN' and Batch_No='" + objtr.Item_Code + "'" + Environment.NewLine +
                                "union all" + Environment.NewLine +
                                "select Item_Code,Avg_Cost  from tspl_inventory_movement where Source_Doc_No='" + obj.Document_No + "' and IS_CONSUMPTION=1 and Trans_Type='JWO-SRN' and Batch_No='" + objtr.Item_Code + "'"
                            Dim dtInvMov As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            For Each drInvMov As DataRow In dtInvMov.Rows
                                qry = " select Purchase_JobWork  from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(drInvMov("Item_Code")) + "' "
                                Dim dtinner As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtinner Is Nothing OrElse dtinner.Rows.Count <= 0 Then
                                    Throw New Exception("Please Map Purchase Account Set for item  " + clsCommon.myCstr(drInvMov("Item_Code")))
                                End If
                                Dim PurchaseJobWorkAcc As String = clsCommon.myCstr(dtinner.Rows(0)("Purchase_JobWork"))
                                If clsCommon.myLen(PurchaseJobWorkAcc) <= 0 Then
                                    Throw New Exception("Please Map  Purchase Job Work A/C From Purchase Account Set For Item : " & clsCommon.myCstr(drInvMov("Item_Code")) & " (" & clsItemMaster.GetItemName(clsCommon.myCstr(drInvMov("Item_Code")), trans) & ")")
                                End If
                                PurchaseJobWorkAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(PurchaseJobWorkAcc, obj.Loc_Code, trans)
                                ArryLst.Add(New String() {PurchaseJobWorkAcc, -1 * clsCommon.myCdbl(drInvMov("Avg_Cost"))})
                                InvAmt += clsCommon.myCdbl(drInvMov("Avg_Cost"))
                            Next

                            Dim strJobWorkAc As String = clsCommon.myCstr(dt.Rows(0)("Job_Work_Ac"))
                            If clsCommon.CompairString(strJobWorkAc, "") = CompairStringResult.Equal Then
                                Throw New Exception("Please Set Job Work Account")
                            End If
                            strJobWorkAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strJobWorkAc, obj.Loc_Code, trans)
                            ArryLst.Add(New String() {strJobWorkAc, -1 * objtr.Job_Amount})
                            InvAmt += objtr.Job_Amount

                            Dim strInvCntrlAc As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                            If clsCommon.CompairString(strInvCntrlAc, "") = CompairStringResult.Equal Then
                                Throw New Exception("Please Set Inventory Control Account")
                            End If
                            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, obj.Loc_Code, trans)
                            ArryLst.Add(New String() {strInvCntrlAc, InvAmt})

                        Else
                            Dim strInvCntrlAc As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                            If clsCommon.CompairString(strInvCntrlAc, "") = CompairStringResult.Equal Then
                                Throw New Exception("Please Set Inventory Control Account")
                            End If
                            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, obj.Loc_Code, trans)
                            ArryLst.Add(New String() {strInvCntrlAc, objtr.Amount})


                            Dim strJobWorkAc As String = clsCommon.myCstr(dt.Rows(0)("Job_Work_Ac"))
                            If clsCommon.CompairString(strJobWorkAc, "") = CompairStringResult.Equal Then
                                Throw New Exception("Please Set Job Work Account")
                            End If
                            strJobWorkAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strJobWorkAc, obj.Loc_Code, trans)
                            ArryLst.Add(New String() {strJobWorkAc, -1 * objtr.Amount})
                        End If
                    Else
                        Throw New Exception("Please set purchase account set for item " + objtr.Item_Code)
                    End If
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"), "Against Job Work order SRN No  -" + obj.Document_No + "", "JW-SR", "JWO SRN", obj.Document_No, "", "C", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
            End If

            Dim strQry As String = " update TSPL_JWO_SRN_HEAD set Posted='1', Posted_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Document_No='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrDocNo, "TSPL_JWO_SRN_HEAD", "DOCUMENT_NO", trans)

            'Throw New Exception("Balwinder singh premi")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpostData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Doc No not found to Post")
            End If
            Dim obj As clsJWOSRNHead = clsJWOSRNHead.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Pending) Then
                Throw New Exception("Transacation should be posted for reverse and unposted")
            End If
            Dim qry As String = "select Document_No from TSPL_JWO_SRN_RETURN where TSPL_JWO_SRN_RETURN.JWO_SRN_No='" + StrDocNo + "'"
            Dim strReturnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strReturnNo) > 0 Then
                Throw New Exception("Cannot Reverse SRN No - " + StrDocNo + " .JWO SRN Return- " + strReturnNo + " is created")
            End If
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document  in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + StrDocNo + "') and Source_Code='JW-SR')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document  in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + StrDocNo + "') and Source_Code='JW-SR'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsBatchInventory.ReverseAndUnpost("JWO-SRN", StrDocNo, trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT  where Source_Doc_No in ( select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document  in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + StrDocNo + "') and  Trans_Type='IC-AD'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in ( select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document  in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + StrDocNo + "') and  Trans_Type='IC-AD'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No in ( select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document  in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + StrDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ADJUSTMENT_HEADER where Reference_Document  in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + StrDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrDocNo, "TSPL_JOURNAL_MASTER", "Source_Doc_No", trans)
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + StrDocNo + "' and Source_Code='JW-SR')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" + StrDocNo + "' and Source_Code='JW-SR'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrDocNo, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrDocNo, "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + StrDocNo + "'  and  Trans_Type='JWO-SRN'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + StrDocNo + "' and  Trans_Type='JWO-SRN'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_JWO_SRN_HEAD set Posted=0,Posted_Date=null where Document_No='" + StrDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrDocNo, "TSPL_JWO_SRN_HEAD", "DOCUMENT_NO", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsJWOSRNDetail
#Region "Variables"
    Public Document_No As String = String.Empty
    Public SNo As Integer
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public HSN_Code As String = String.Empty
    Public UOM As String = String.Empty
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Estimate_Qty As Double = 0
    Public Qty As Double = 0
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public Job_Price_code As String = String.Empty
    Public Job_Rate As Double = 0
    Public Job_Amount As Double = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing

    Public CAL_Raw_Item_Cost As Double = 0 ''For Calculation only

#End Region

    Public Shared Function saveData(ByVal ObjHead As clsJWOSRNHead, ByVal trans As SqlTransaction) As Boolean
        clsBatchInventory.DeleteData("JWO-SRN", ObjHead.Document_No, trans)
        Dim qry As String = "Delete from TSPL_JWO_SRN_DETAIL where Document_No='" + ObjHead.Document_No + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If ObjHead.Arr IsNot Nothing Then
            For Each obj As clsJWOSRNDetail In ObjHead.Arr
                Dim coll As Hashtable = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", ObjHead.Document_No)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                clsCommon.AddColumnsForChange(coll, "Estimate_Qty", obj.Estimate_Qty)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                obj.FAT_KG = clsBOM.GetFatSNFKG_AfterConversion(obj.Item_Code, obj.UOM, obj.Qty, obj.FAT_Per, trans)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                obj.SNF_KG = clsBOM.GetFatSNFKG_AfterConversion(obj.Item_Code, obj.UOM, obj.Qty, obj.SNF_Per, trans)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Job_Price_code", obj.Job_Price_code)
                clsCommon.AddColumnsForChange(coll, "Job_Rate", obj.Job_Rate)
                clsCommon.AddColumnsForChange(coll, "Job_Amount", obj.Job_Amount)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_SRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                clsBatchInventory.SaveData("JWO-SRN", ObjHead.Document_No, ObjHead.Document_Date, "I", obj.Item_Code, ObjHead.Loc_Code, obj.SNo, 0, obj.UOM, obj.arrBatchItem, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWOSRNDetail)
        Dim arr As List(Of clsJWOSRNDetail) = Nothing
        Try
            Dim obj As clsJWOSRNDetail = Nothing
            Dim qry As String = "select TSPL_JWO_SRN_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code from TSPL_JWO_SRN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_JWO_SRN_DETAIL.Item_Code where Document_No='" & strDocumentNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsJWOSRNDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWOSRNDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.HSN_Code = clsCommon.myCstr(dt.Rows(i)("HSN_Code"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(i)("Gross_Weight"))
                    obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(i)("Tare_Weight"))
                    obj.Net_Weight = clsCommon.myCdbl(dt.Rows(i)("Net_Weight"))
                    obj.Estimate_Qty = clsCommon.myCdbl(dt.Rows(i)("Estimate_Qty"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.FAT_Per = clsCommon.myCdbl(dt.Rows(i)("FAT_Per"))
                    obj.SNF_Per = clsCommon.myCdbl(dt.Rows(i)("SNF_Per"))
                    obj.FAT_KG = clsCommon.myCdbl(dt.Rows(i)("FAT_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.Job_Price_code = clsCommon.myCstr(dt.Rows(i)("Job_Price_code"))
                    obj.Job_Rate = clsCommon.myCdbl(dt.Rows(i)("Job_Rate"))
                    obj.Job_Amount = clsCommon.myCdbl(dt.Rows(i)("Job_Amount"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(i)("Rate"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.arrBatchItem = clsBatchInventory.GetData("JWO-SRN", obj.Document_No, obj.Item_Code, obj.SNo, trans)
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class

Public Class clsRecursiveitems

#Region "Variables"
    Public ITEM_CODE As String
    Public QUANTITY As Decimal
    Public UNIT_CODE As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal

    Public Byproduct_Item_Code As String
    Public Byproduct_Item_UOM As String
    Public Byproduct_Item_Qty As Decimal
#End Region

    Public Shared Function GetItemOfBOM(ByRef Arr As List(Of clsRecursiveitems), ByVal strICode As String, ByVal dblQty As Double, ByVal strUOM As String, ByVal strJobLocationCode As String, ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction, ByVal intLvl As Integer, Optional ByVal Is_For_Production As Boolean = False, Optional ByVal BOM_Code As String = "", Optional ByVal RunRecursive As Boolean = True) As Boolean
        Return GetItemOfBOM(0, 0, Arr, strICode, dblQty, strUOM, strJobLocationCode, strVendorCode, TransDate, trans, intLvl, Is_For_Production, BOM_Code, RunRecursive)
    End Function
    Public Shared Function GetItemOfBOM(ByRef dclExtaFATKG As Decimal, ByRef dclExtaSNFKG As Decimal, ByRef Arr As List(Of clsRecursiveitems), ByVal strICode As String, ByVal dblQty As Double, ByVal strUOM As String, ByVal strJobLocationCode As String, ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction, ByVal intLvl As Integer, Optional ByVal Is_For_Production As Boolean = False, Optional ByVal BOM_Code As String = "", Optional ByVal RunRecursive As Boolean = True) As Boolean
        Return GetItemOfBOM("", dclExtaFATKG, dclExtaSNFKG, Arr, strICode, dblQty, strUOM, strJobLocationCode, strVendorCode, TransDate, trans, intLvl, Is_For_Production, BOM_Code, RunRecursive)
    End Function
    Public Shared Function GetItemOfBOM(ByRef strLastICode As String, ByRef dclExtaFATKG As Decimal, ByRef dclExtaSNFKG As Decimal, ByRef Arr As List(Of clsRecursiveitems), ByVal strICode As String, ByVal dblQty As Double, ByVal strUOM As String, ByVal strJobLocationCode As String, ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction, ByVal intLvl As Integer, Optional ByVal Is_For_Production As Boolean = False, Optional ByVal BOM_Code As String = "", Optional ByVal RunRecursive As Boolean = True) As Boolean
        Dim qry As String = "select ITEM_CODE,QUANTITY*ConvFactor as QUANTITY,UNIT_CODE,FAT,SNF,FAT_KG*ConvFactor as FAT_KG,SNF_KG*ConvFactor as SNF_KG,Byproduct_Item_Code,Byproduct_Item_UOM,Byproduct_Item_Qty*ConvFactor as Byproduct_Item_Qty  from (" + Environment.NewLine +
               "select (" + clsCommon.myCstr(dblQty) + "/TSPL_PP_BOM_HEAD.PROD_QUANTITY)*(MulConversion.Conversion_Factor/DivideConversion.Conversion_Factor) as ConvFactor ,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_QUANTITY,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE, TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.FAT,TSPL_PP_BOM_ITEM_DETAIL.SNF,TSPL_PP_BOM_ITEM_DETAIL.FAT_KG,TSPL_PP_BOM_ITEM_DETAIL.SNF_KG,TSPL_PP_BOM_HEAD.Byproduct_Item_Code,TSPL_PP_BOM_HEAD.Byproduct_Item_Qty,TSPL_PP_BOM_HEAD.Byproduct_Item_UOM from (" + Environment.NewLine +
               "select top 1 TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE where  TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + strICode + "' and TSPL_PP_BOM_HEAD.Is_Post=1 " + Environment.NewLine
        If intLvl = 1 Then
            If Is_For_Production Then
                qry += " and TSPL_PP_BOM_HEAD.BOM_CODE='" & BOM_Code & "' and TSPL_PP_BOM_HEAD.Valid_FROM_DATE<='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' " + Environment.NewLine +
                       " and 2=(case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE is null then 2 else case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE>='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' then 2 else 1 end end)  "
            End If
        End If
        If Not Is_For_Production Then
            qry += " and TSPL_PP_BOM_HEAD.JobWork_Loc='" + strJobLocationCode + "' and TSPL_PP_BOM_HEAD.Vendor_Code='" + strVendorCode + "' and TSPL_PP_BOM_HEAD.Valid_FROM_DATE<='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' " + Environment.NewLine +
                       " and 2=(case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE is null then 2 else case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE>='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' then 2 else 1 end end)  "
        End If


        qry += " order by TSPL_PP_BOM_HEAD.Valid_FROM_DATE desc ) TabBomNo " + Environment.NewLine +
               " inner join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TabBomNo.BOM_CODE " + Environment.NewLine +
               " left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE" + Environment.NewLine +
               " left outer join TSPL_ITEM_UOM_DETAIL as MulConversion on  MulConversion.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE  and MulConversion.UOM_Code='" + strUOM + "' " + Environment.NewLine +
               " left outer join TSPL_ITEM_UOM_DETAIL as DivideConversion on DivideConversion.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and DivideConversion.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE" + Environment.NewLine +
               " )xx"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        intLvl += 1
        Dim isFound As Boolean = False
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            isFound = True
            For Each dr As DataRow In dt.Rows
                Dim strBOMItemCode As String = clsCommon.myCstr(dr("ITEM_CODE"))
                Dim strBOMItemUOM As String = clsCommon.myCstr(dr("UNIT_CODE"))
                Dim dclBOMItemQty As Decimal = clsCommon.myCdbl(dr("QUANTITY"))
                Dim dclBOMFATKg As Decimal = clsCommon.myCdbl(dr("FAT_KG"))
                Dim dclBOMSNFKg As Decimal = clsCommon.myCdbl(dr("SNF_KG"))
                Dim dclBalItemQty As Decimal = 0
                Dim dclBalFATKg As Decimal = 0
                Dim dclBalSNFKg As Decimal = 0
                If Not Is_For_Production Then
                    qry = "select CL_QTY,CL_FAT_KG,CL_SNF_KG from TSPL_FUN_ITEM_LOC_BALANCE('" + strBOMItemCode + "','" + strJobLocationCode + "','" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy hh:mm tt") + "')"
                    Dim dtStock As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtStock IsNot Nothing AndAlso dtStock.Rows.Count > 0 Then
                        Dim dclStockQty As Decimal = clsCommon.myCdbl(dtStock.Rows(0)("CL_QTY"))
                        dclBalFATKg = clsCommon.myCdbl(dtStock.Rows(0)("CL_FAT_KG"))
                        dclBalSNFKg = clsCommon.myCdbl(dtStock.Rows(0)("CL_SNF_KG"))
                        Try
                            dclStockQty = dclStockQty / clsItemMaster.GetConvertionFactor(strBOMItemCode, strBOMItemUOM, trans)
                        Catch ex As Exception
                            dclStockQty = 0
                        End Try
                        If clsCommon.myCdbl(dclStockQty) > 0 Then
                            dclBalItemQty = dclStockQty - dclBOMItemQty
                            Dim obj As New clsRecursiveitems
                            obj.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                            If dclBalItemQty < 0 Then
                                obj.QUANTITY = dclStockQty
                                dclBOMFATKg = dclBOMFATKg / dclBOMItemQty * dclStockQty
                                dclBOMSNFKg = dclBOMSNFKg / dclBOMItemQty * dclStockQty

                                dclBOMItemQty = Math.Abs(dclBalItemQty)
                            Else
                                obj.QUANTITY = dclBOMItemQty
                                obj.Byproduct_Item_Code = clsCommon.myCstr(dr("Byproduct_Item_Code"))
                                obj.Byproduct_Item_UOM = clsCommon.myCstr(dr("Byproduct_Item_UOM"))
                                obj.Byproduct_Item_Qty = clsCommon.myCdbl(dr("Byproduct_Item_Qty"))
                            End If
                            obj.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                            obj.FAT = clsCommon.myCdbl(dr("FAT"))
                            obj.SNF = clsCommon.myCdbl(dr("SNF"))
                            If dclBOMFATKg > dclBalFATKg Then
                                obj.FAT_KG = dclBalFATKg
                                dclExtaFATKG += dclBOMFATKg - dclBalFATKg
                            Else
                                obj.FAT_KG = dclBOMFATKg
                            End If
                            If dclBOMSNFKg > dclBalSNFKg Then
                                obj.SNF_KG = dclBalSNFKg
                                dclExtaSNFKG += dclBOMSNFKg - dclBalSNFKg
                            Else
                                obj.SNF_KG = dclBOMSNFKg
                            End If


                            Arr.Add(obj)
                            If dclBalItemQty > 0 Then
                                Continue For
                            End If
                        End If
                    End If
                End If
                Dim isFoundInner As Boolean = True
                If RunRecursive Then
                    If (clsCommon.myLen(strLastICode) > 0 AndAlso clsCommon.CompairString(strLastICode, strBOMItemCode) = CompairStringResult.Equal) Then
                        isFoundInner = False
                    Else
                        isFoundInner = GetItemOfBOM(strLastICode, dclExtaFATKG, dclExtaSNFKG, Arr, strBOMItemCode, dclBOMItemQty, strBOMItemUOM, strJobLocationCode, strVendorCode, TransDate, trans, intLvl, Is_For_Production)
                    End If
                Else
                    isFoundInner = False
                End If
                If Not isFoundInner Then
                    Dim obj As New clsRecursiveitems
                    obj.ITEM_CODE = strBOMItemCode
                    obj.QUANTITY = dclBOMItemQty
                    obj.UNIT_CODE = strBOMItemUOM
                    obj.FAT = clsCommon.myCdbl(dr("FAT"))
                    obj.SNF = clsCommon.myCdbl(dr("SNF"))
                    obj.FAT_KG = dclBOMFATKg
                    obj.SNF_KG = dclBOMSNFKg

                    obj.Byproduct_Item_Code = clsCommon.myCstr(dr("Byproduct_Item_Code"))
                    obj.Byproduct_Item_UOM = clsCommon.myCstr(dr("Byproduct_Item_UOM"))
                    obj.Byproduct_Item_Qty = clsCommon.myCdbl(dr("Byproduct_Item_Qty"))
                    Arr.Add(obj)
                End If

            Next
        End If
        Return isFound
    End Function


End Class


 
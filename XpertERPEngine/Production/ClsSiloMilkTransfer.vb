'---Richa Agarwal---Ticket No.-BHA/24/07/18-000190--24/07/2018
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class ClsSiloMilkTransfer
#Region "Variables"
    Public FATKG As Decimal = 0
    Public SNFKG As Decimal = 0
    Public Document_Code As String = String.Empty
    Public Document_Date As DateTime? = Nothing
    Public Item_Code As String = String.Empty
    Public MainLocation_Code As String = String.Empty
    Public Silo_Code As String = String.Empty
    Public MainLocation_Desc As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public Silo_Code_Desc As String = String.Empty
    Public Description As String = String.Empty
    Public Posted As Integer = 0
    Public Item_UOM As String = String.Empty
    Public Posting_Date As DateTime?
    Public IsJobWork As Integer = 0
    Public IsCreatedByUploader As Integer = 0
    Public Arr As List(Of ClsSiloMilkTransferDetails) = Nothing
    Dim qry As String = String.Empty
#End Region
    Public Function SaveData(ByVal obj As ClsSiloMilkTransfer, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As ClsSiloMilkTransfer, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmSiloMilkTransfer, obj.MainLocation_Code, obj.Document_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, "", obj.MainLocation_Code, obj.Document_Date, trans)


            qry = "delete from TSPL_SILO_MILK_TRANSFER_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SiloMilkTransfer, "", obj.MainLocation_Code)
            End If

            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "MainLocation_Code", obj.MainLocation_Code)
            clsCommon.AddColumnsForChange(coll, "Silo_Code", obj.Silo_Code)
            clsCommon.AddColumnsForChange(coll, "Item_UOM", obj.Item_UOM)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "IsJobWork", obj.IsJobWork)
            clsCommon.AddColumnsForChange(coll, "IsCreatedByUploader", obj.IsCreatedByUploader)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILO_MILK_TRANSFER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                HistoryUpdate(obj.Document_Code, trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILO_MILK_TRANSFER_HEAD", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsSiloMilkTransferDetails.SaveData(obj.Document_Code, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_SILO_MILK_TRANSFER_HEAD", "Document_Code", "TSPL_SILO_MILK_TRANSFER_DETAIL", "Document_Code", trans)
        Return True
    End Function
    ''richa ERO/01/04/19-000537 create this function especially for Job work type silo milk transfer
    Shared Function PostData_JobWork(ByVal adjno As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData_JobWork(adjno, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Shared Function PostData_JobWork(ByVal StrAdjustmentNo As String, ByVal trans As SqlTransaction) As Boolean


        Dim obj As New ClsSiloMilkTransfer()
        obj = obj.GetData(StrAdjustmentNo, NavigatorType.Current, trans)
        If obj Is Nothing Then
            Throw New Exception("No Data Found to Post")
        End If


        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, "", obj.MainLocation_Code, obj.Document_Date, trans)
        If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Transaction :" + StrAdjustmentNo)
        End If


        Try
            '' ----- for detail
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            For Each objtr As ClsSiloMilkTransferDetails In obj.Arr
                Dim objInventoryMovemntnew As New clsInventoryMovementNew()
                objInventoryMovemntnew.InOut = "O"
                objInventoryMovemntnew.main_location = objtr.MainLoc_Code
                objInventoryMovemntnew.Location_Code = objtr.Silo_Code
                objInventoryMovemntnew.Item_Code = objtr.Item_Code
                objInventoryMovemntnew.Item_Desc = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                objInventoryMovemntnew.Qty = objtr.Qty
                objInventoryMovemntnew.UOM = objtr.UOM

                objInventoryMovemntnew.MRP = 0
                objInventoryMovemntnew.Add_Cost = 0
                
                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "A"
                End If

                objInventoryMovemntnew.FAT_KG = objtr.fat_kg
                objInventoryMovemntnew.FAT_Per = objtr.fat_pers
                objInventoryMovemntnew.SNF_KG = objtr.snf_kg
                objInventoryMovemntnew.SNF_Per = objtr.snf_pers

                objInventoryMovemntnew.Fat_Amt = objtr.FatAmount
                objInventoryMovemntnew.Fat_Rate = objtr.Rate
                objInventoryMovemntnew.SNF_Amt = objtr.SNFAmount
                objInventoryMovemntnew.SNF_Rate = objtr.Rate
                objInventoryMovemntnew.Avg_Cost = objtr.Amount
                objInventoryMovemntnew.DonNotCalculateAvgFATSNFCost = True
                objInventoryMovemntnew.CalculateAvgCost = False

                ArrInventoryMovementNew.Add(objInventoryMovemntnew)

            Next
            'If MakeGLEntry = True Then
            '    If obj.isAutoCreatedByMilkTransferIn = 1 Then
            '        CreateJETransferWithBranchAc(obj, strType, trans, strVourcherNoForRecreateOnly)
            '    Else
            '        CreateJE(obj, strType, trans, strVourcherNoForRecreateOnly)
            '    End If

            'End If

            If ArrInventoryMovementNew IsNot Nothing AndAlso ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData("SI-MT", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If


            '' ----- for header
            Dim ArrInventoryMovementNew1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select SUM(Qty) AS Qty,max(UOM) as UOM,cast (SUM(Fat_KG) as decimal(18,2)) AS Fat_KG,cast (SUM(SNF_KG) as decimal(18,2)) AS SNF_KG,cast (AVG(Fat_Per) as decimal(18,2)) AS Fat_Per,cast (AVG(SNF_Per) as decimal(18,2)) AS SNF_Per,cast (SUM(Avg_Cost) as decimal(18,2)) AS Avg_Cost,cast (AVG(Fat_Rate) as decimal(18,2)) AS Fat_Rate,cast (AVG(SNF_Rate) as decimal(18,2)) AS SNF_Rate,cast (SUM(SNF_Amt) as decimal(18,2)) AS SNF_Amt,cast (SUM(Fat_Amt) as decimal(18,2)) AS Fat_Amt  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" & obj.Document_Code & "' and InOut ='O' and Trans_Type ='SI-MT'", trans)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                ''richa BHA/27/07/18-000200
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderSiloCapicityForStockIn, clsFixedParameterCode.ConsiderSiloCapicityForStockIn, trans)), "1") = CompairStringResult.Equal Then
                    If clsCommon.myLen(obj.Silo_Code) > 0 Then
                        Dim balqtyofvl As Double = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MainLocation_Code, obj.Silo_Code, obj.Document_Code, obj.Document_Date, trans, "LTR"))
                        Dim itemQty As Double = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                        Dim DblFinalQty As Double = balqtyofvl + itemQty
                        Dim SiloCapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Silo_Capacity,0) from TSPL_LOCATION_MASTER where location_code='" & obj.Silo_Code & "'", trans))
                        If DblFinalQty > SiloCapacity Then
                            Throw New Exception("Silo Qty should be less than or equal to " & SiloCapacity & " ")
                        End If
                    Else
                        Throw New Exception("Please entre silo location on Unloading")
                    End If
                End If
                ''---------------
                Dim objInventoryMovemntnew As New clsInventoryMovementNew()
                objInventoryMovemntnew.InOut = "I"
                objInventoryMovemntnew.main_location = obj.MainLocation_Code
                objInventoryMovemntnew.Location_Code = obj.Silo_Code
                objInventoryMovemntnew.Item_Code = obj.Item_Code
                objInventoryMovemntnew.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                objInventoryMovemntnew.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                objInventoryMovemntnew.UOM = obj.Item_UOM
                objInventoryMovemntnew.MRP = 0
                objInventoryMovemntnew.Add_Cost = 0


                Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "A"
                End If

                objInventoryMovemntnew.FAT_KG = clsCommon.myCdbl(dt.Rows(0)("Fat_KG"))
                objInventoryMovemntnew.FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Fat_Per"))
                objInventoryMovemntnew.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                objInventoryMovemntnew.SNF_Per = clsCommon.myCdbl(dt.Rows(0)("SNF_Per"))

                objInventoryMovemntnew.Avg_Cost = clsCommon.myCdbl(dt.Rows(0)("Avg_Cost"))


                objInventoryMovemntnew.Fat_Rate = clsCommon.myCdbl(dt.Rows(0)("Fat_Rate"))
                objInventoryMovemntnew.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Rate"))
                objInventoryMovemntnew.Fat_Amt = clsCommon.myCdbl(dt.Rows(0)("Fat_Amt"))
                objInventoryMovemntnew.SNF_Amt = clsCommon.myCdbl(dt.Rows(0)("SNF_Amt"))
            
                objInventoryMovemntnew.DonNotCalculateAvgFATSNFCost = True
                objInventoryMovemntnew.CalculateAvgCost = False


                ''--------

              

                ArrInventoryMovementNew1.Add(objInventoryMovemntnew)

                If ArrInventoryMovementNew1 IsNot Nothing AndAlso ArrInventoryMovementNew1.Count > 0 Then
                    clsInventoryMovementNew.SaveData("SI-MT", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementNew1, trans)
                End If
            End If
            ''--------- header end

            ''create Journal entry
            CreateJE_JobWork(obj, trans)
            ''-----

            HistoryUpdate(obj.Document_Code, trans)
            Dim qry1 As String = " update TSPL_SILO_MILK_TRANSFER_HEAD  set Posted='1' ,Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "' where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function

    Public Shared Function CreateJE_JobWork(ByVal obj As ClsSiloMilkTransfer, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim strInventryAccForHeader As String = String.Empty
            Dim strInventryAccForDetail As String = String.Empty
            Dim strsegment As String = ""
            Dim strsegmentDetail As String = String.Empty
            strsegment = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.MainLocation_Code + "'", trans)

            Dim desc As String = String.Empty

            desc = "Journal entry created against Silo Milk Transfer for Document No " & obj.Document_Code & ""
            Dim ArryLst As ArrayList = New ArrayList()
            If clsCommon.myLen(obj.Item_Code) > 0 Then
                strInventryAccForHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + obj.Item_Code + "')", trans))
                If clsCommon.myLen(strInventryAccForHeader) <= 0 Then
                    Throw New Exception("Please set inventory Control Account of item " + obj.Item_Code)
                End If
                strInventryAccForHeader = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventryAccForHeader, strsegment, True, trans)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select cast (Avg_Cost as decimal(18,2)) AS Avg_Cost,Item_Code,Location_code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" & obj.Document_Code & "' and InOut ='O' and Trans_Type ='SI-MT'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows

                    strInventryAccForDetail = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Inv_Control_Account  AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "')", trans))
                    If clsCommon.myLen(strInventryAccForDetail) <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + clsCommon.myCstr(dr("Item_Code")))
                    End If

                    strsegmentDetail = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + clsCommon.myCstr(dr("Location_code")) + "'", trans)
                    strInventryAccForDetail = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventryAccForDetail, strsegmentDetail, True, trans)


                    Dim Acc1() As String = {strInventryAccForDetail, -1 * clsCommon.myCstr(dr("Avg_Cost")), "", "", "", "", "", "", "I"}
                    ArryLst.Add(Acc1)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                        clsInventoryMovement.UpdateInvControlAccount(obj.Document_Code, "SI-MT", clsCommon.myCstr(dr("Item_Code")), "", strInventryAccForDetail, "O", trans)
                    End If

                    '------branch account
                    Dim strTemp1 As String = ClsBranchAccountMapping.GetBranchAccount(strsegmentDetail, strsegment, trans)
                    If clsCommon.myLen(strTemp1) <= 0 Then
                        Throw New Exception("Please set Branch account mapping with from location " + strsegmentDetail + " and to location " + strsegment)
                    End If

                    Dim BranchAccDetail = New String() {strTemp1, 1 * clsCommon.myCstr(dr("Avg_Cost"))}
                    ArryLst.Add(BranchAccDetail)

                Next
            End If

            Dim dblAvg_cost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select cast (Avg_Cost as decimal(18,2)) AS Avg_Cost  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" & obj.Document_Code & "' and InOut ='I' and Trans_Type ='SI-MT'", trans))

            Dim Acc2() As String = {strInventryAccForHeader, 1 * dblAvg_cost, "", "", "", "", "", "", "I"}
            ArryLst.Add(Acc2)

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                clsInventoryMovement.UpdateInvControlAccount(obj.Document_Code, "SI-MT", clsCommon.myCstr(obj.Item_Code), strInventryAccForHeader, "", "I", trans)
                '------------------
            End If

            '------branch account
            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strsegment, strsegmentDetail, trans)
            If clsCommon.myLen(strTemp) <= 0 Then
                Throw New Exception("Please set Branch account mapping with from location " + strsegment + " and to location " + strsegmentDetail)
            End If

            Dim BranchAccHeader = New String() {strTemp, -1 * dblAvg_cost}
            ArryLst.Add(BranchAccHeader)

            transportSql.FunGrnlEntryWithTrans(obj.MainLocation_Code, False, trans, obj.Document_Date, desc, "SI-MT", "SILO Milk Transfer", obj.Document_Code, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", "")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReturnFATSNFKG(ByVal strLocation As String, ByVal strItem As String) As ClsSiloMilkTransfer
        Dim obj As ClsSiloMilkTransfer = Nothing
        Dim str = "select sum(case when InOut='I' then Fat_KG else -Fat_KG end) as FATKG,sum(case when InOut='I' then SNF_KG else -SNF_KG end) as SNfKG " & _
            "from TSPL_INVENTORY_MOVEMENT_new where Location_Code='" & strLocation & "' and Item_Code='" & strItem & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
        If (dt IsNot Nothing And dt.Rows.Count > 0) Then
            obj = New ClsSiloMilkTransfer()
            obj.FATKG = clsCommon.myCdbl(dt.Rows(0)("FATKG"))
            obj.SNFKG = clsCommon.myCdbl(dt.Rows(0)("SNfKG"))
            If obj.FATKG < 0 Then
                obj.FATKG = 0
            End If
            If obj.SNFKG < 0 Then
                obj.SNFKG = 0
            End If
        End If
        Return obj
    End Function
    Shared Function PostData(ByVal adjno As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(adjno, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Shared Function PostData(ByVal StrAdjustmentNo As String, ByVal trans As SqlTransaction) As Boolean


        Dim obj As New ClsSiloMilkTransfer()
        obj = obj.GetData(StrAdjustmentNo, NavigatorType.Current, trans)
        If obj Is Nothing Then
            Throw New Exception("No Data Found to Post")
        End If


        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmSiloMilkTransfer, obj.MainLocation_Code, obj.Document_Date, trans)
        If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Transaction :" + StrAdjustmentNo)
        End If



        ''to check Qty available or not GKD/02/09/19-000184
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, trans), "0") = CompairStringResult.Equal Then
            Dim arrItemCode As List(Of String) = New List(Of String)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then

                    For ii As Integer = 0 To obj.Arr.Count - 1
                        Dim strICode As String = clsCommon.myCstr(obj.Arr(ii).Item_Code)
                        Dim strIName As String = clsCommon.myCstr(obj.Arr(ii).Item_Desc)
                        Dim strUOM As String = clsCommon.myCstr(obj.Arr(ii).UOM)
                        Dim dblEnteredQty As Double = clsCommon.myCdbl(obj.Arr(ii).Qty)
                        Dim strSiloCode As String = clsCommon.myCstr(obj.Arr(ii).Silo_Code)
                        If clsCommon.myLen(strSiloCode) > 0 Then
                            'If arrItemCode.Contains(strSiloCode) Then
                            '    Throw New Exception("Duplicate Silo " & strSiloCode & " Found at Row No " & (ii + 1))
                            'Else
                            '    arrItemCode.Add(strSiloCode)
                            'End If
                            For jj As Integer = 0 To obj.Arr.Count - 1
                                If jj = ii Then
                                    Continue For
                                End If
                                Dim strInnerSiloCode As String = clsCommon.myCstr(obj.Arr(jj).Silo_Code)
                                Dim strInnerICode As String = clsCommon.myCstr(obj.Arr(jj).Item_Code)

                                If clsCommon.CompairString(strInnerSiloCode, strSiloCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strInnerICode, strICode) = CompairStringResult.Equal Then
                                    Throw New Exception("Silo " & strSiloCode & " and Item " & strICode & " Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1))
                                End If
                            Next

                            If clsCommon.myLen(strUOM) <= 0 Then
                                Throw New Exception("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                            End If

                            If clsCommon.myCdbl(dblEnteredQty) <= 0 Then
                                Throw New Exception("Please enter Qty of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                            End If

                            Dim objPer As MIlkComponentType = clsItemMaster.GetItemFatSNF(clsCommon.myCstr(obj.Arr(ii).Item_Code), trans)
                            Dim dblCurrentFatPerMax As Double = Math.Round(objPer.FAT_Per + (objPer.FAT_Per * 0.5), 3)
                            Dim dblCurrentFatPerMin As Double = Math.Round(objPer.FAT_Per - (objPer.FAT_Per * 0.5), 3)

                            Dim dblCurrentSNFPerMax As Double = Math.Round(objPer.SNF_Per + (objPer.SNF_Per * 0.5), 3)
                            Dim dblCurrentSNFPerMin As Double = Math.Round(objPer.SNF_Per - (objPer.SNF_Per * 0.5), 3)

                            If obj.Arr(ii).fat_pers > dblCurrentFatPerMax OrElse obj.Arr(ii).fat_pers < dblCurrentFatPerMin Then
                                obj.Arr(ii).fat_pers = objPer.FAT_Per
                            End If
                            If obj.Arr(ii).snf_pers > dblCurrentSNFPerMax OrElse obj.Arr(ii).snf_pers < dblCurrentSNFPerMin Then
                                obj.Arr(ii).snf_pers = objPer.SNF_Per
                            End If



                            Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)

                            Dim BalQty As Double = 0.0
                            ''richa agarwal changes when main and silo both are same than no need to pass silo into function to get silo qty 26 Feb,2020
                            If clsCommon.CompairString(obj.MainLocation_Code, strSiloCode) = CompairStringResult.Equal Then
                                BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strICode, obj.MainLocation_Code, "", obj.Document_Code, obj.Document_Date, trans, strUOM))
                            Else
                                BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strICode, obj.MainLocation_Code, strSiloCode, obj.Document_Code, obj.Document_Date, trans, strUOM))
                            End If
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, trans), "1") = CompairStringResult.Equal Then
                                If BalQty > 0 Then
                                    BalQty = ClsLoadingTanker.GetTolerane(BalQty, dblEnteredQty, trans)
                                End If
                            End If
                            If Math.Round(BalQty, 2) < Math.Round(dblEnteredQty, 2) Then
                                Throw New Exception("Item : " + strICode + " Available Qty is :     " & Math.Round(BalQty, 2) & Environment.NewLine & "Required Qty :     " & clsCommon.myCstr(dblEnteredQty) & " ")
                            End If

                            ''TO CHECK FAT AND SNF
                            Dim CurBal_SNF As Double = 0
                            Dim CurBal_FAT As Double = 0
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, trans), "1") = CompairStringResult.Equal Then
                                Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & strICode & "','" & strSiloCode & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "')", trans)
                                If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                                    CurBal_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 3, MidpointRounding.ToEven)
                                    CurBal_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 3, MidpointRounding.ToEven)
                                End If

                                If CurBal_SNF < clsCommon.myCdbl(obj.Arr(ii).snf_kg) Then
                                    Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(obj.Arr(ii).snf_kg) & " ")
                                End If
                                If CurBal_FAT < clsCommon.myCdbl(obj.Arr(ii).fat_kg) Then
                                    Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(obj.Arr(ii).fat_kg) & " ")
                                End If
                            End If



                        End If
                    Next
                End If
            End If
        End If

        ''---------------


        Try
            '' ----- for detail
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            For Each objtr As ClsSiloMilkTransferDetails In obj.Arr
                Dim objInventoryMovemntnew As New clsInventoryMovementNew()
                objInventoryMovemntnew.InOut = "O"
                objInventoryMovemntnew.main_location = obj.MainLocation_Code
                objInventoryMovemntnew.Location_Code = objtr.Silo_Code
                objInventoryMovemntnew.Item_Code = objtr.Item_Code
                objInventoryMovemntnew.Item_Desc = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                objInventoryMovemntnew.Qty = objtr.Qty
                objInventoryMovemntnew.UOM = objtr.UOM
                'objInventoryMovemntnew.Basic_Cost = objtr.Item_Cost / IIf(objtr.Item_Quantity = 0, 1, objtr.Item_Quantity)
                objInventoryMovemntnew.MRP = 0
                objInventoryMovemntnew.Add_Cost = 0
                'objInventoryMovemntnew.Net_Cost = objtr.Item_Cost
                'objInventoryMovemntnew.Net_Cost = objInventoryMovemntnew.Fat_Amt + objInventoryMovemntnew.SNF_Amt
                'objInventoryMovemntnew.Basic_Cost = IIf(objInventoryMovemntnew.Qty = 0, 0, objInventoryMovemntnew.Net_Cost / objInventoryMovemntnew.Qty)

                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "A"
                End If

                objInventoryMovemntnew.FAT_KG = objtr.fat_kg
                objInventoryMovemntnew.FAT_Per = objtr.fat_pers
                objInventoryMovemntnew.SNF_KG = objtr.snf_kg
                objInventoryMovemntnew.SNF_Per = objtr.snf_pers

                ArrInventoryMovementNew.Add(objInventoryMovemntnew)

            Next
            'If MakeGLEntry = True Then
            '    If obj.isAutoCreatedByMilkTransferIn = 1 Then
            '        CreateJETransferWithBranchAc(obj, strType, trans, strVourcherNoForRecreateOnly)
            '    Else
            '        CreateJE(obj, strType, trans, strVourcherNoForRecreateOnly)
            '    End If

            'End If

            If ArrInventoryMovementNew IsNot Nothing AndAlso ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData("SI-MT", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If


            '' ----- for header
            Dim ArrInventoryMovementNew1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select SUM(Qty) AS Qty,max(UOM) as UOM,cast (SUM(Fat_KG) as decimal(18,2)) AS Fat_KG,cast (SUM(SNF_KG) as decimal(18,2)) AS SNF_KG,cast (AVG(Fat_Per) as decimal(18,2)) AS Fat_Per,cast (AVG(SNF_Per) as decimal(18,2)) AS SNF_Per,cast (SUM(Avg_Cost) as decimal(18,2)) AS Avg_Cost,cast (AVG(Fat_Rate) as decimal(18,2)) AS Fat_Rate,cast (AVG(SNF_Rate) as decimal(18,2)) AS SNF_Rate,cast (SUM(SNF_Amt) as decimal(18,2)) AS SNF_Amt,cast (SUM(Fat_Amt) as decimal(18,2)) AS Fat_Amt  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" & obj.Document_Code & "' and InOut ='O' and Trans_Type ='SI-MT'", trans)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                ''richa BHA/27/07/18-000200
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderSiloCapicityForStockIn, clsFixedParameterCode.ConsiderSiloCapicityForStockIn, trans)), "1") = CompairStringResult.Equal Then
                    If clsCommon.myLen(obj.Silo_Code) > 0 Then
                        Dim balqtyofvl As Double = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MainLocation_Code, obj.Silo_Code, obj.Document_Code, obj.Document_Date, trans, "LTR"))
                        Dim itemQty As Double = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                        Dim DblFinalQty As Double = balqtyofvl + itemQty
                        Dim SiloCapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Silo_Capacity,0) from TSPL_LOCATION_MASTER where location_code='" & obj.Silo_Code & "'", trans))
                        If DblFinalQty > SiloCapacity Then
                            Throw New Exception("Silo Qty should be less than or equal to " & SiloCapacity & " ")
                        End If
                    Else
                        Throw New Exception("Please entre silo location on Unloading")
                    End If
                End If
                ''---------------
                Dim objInventoryMovemntnew As New clsInventoryMovementNew()
                objInventoryMovemntnew.InOut = "I"
                objInventoryMovemntnew.main_location = obj.MainLocation_Code
                objInventoryMovemntnew.Location_Code = obj.Silo_Code
                objInventoryMovemntnew.Item_Code = obj.Item_Code
                objInventoryMovemntnew.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                objInventoryMovemntnew.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                objInventoryMovemntnew.UOM = obj.Item_UOM
                objInventoryMovemntnew.MRP = 0
                objInventoryMovemntnew.Add_Cost = 0


                Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    objInventoryMovemntnew.ItemType = "A"
                End If

                objInventoryMovemntnew.FAT_KG = clsCommon.myCdbl(dt.Rows(0)("Fat_KG"))
                'objInventoryMovemntnew.FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Fat_Per"))
                objInventoryMovemntnew.FAT_Per = Math.Round((clsCommon.myCdbl(dt.Rows(0)("Fat_KG")) * 100) / clsCommon.myCdbl(dt.Rows(0)("Qty")), 3)
                objInventoryMovemntnew.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                'objInventoryMovemntnew.SNF_Per = clsCommon.myCdbl(dt.Rows(0)("SNF_Per"))
                objInventoryMovemntnew.SNF_Per = Math.Round((clsCommon.myCdbl(dt.Rows(0)("SNF_KG")) * 100) / clsCommon.myCdbl(dt.Rows(0)("Qty")), 3)

                objInventoryMovemntnew.Avg_Cost = clsCommon.myCdbl(dt.Rows(0)("Avg_Cost"))

                ''richa 5 Nov,2018 as per ranjana mam bha/05/11/18-000664
                'objInventoryMovemntnew.Fat_Rate = clsCommon.myCdbl(dt.Rows(0)("Fat_Rate"))
                'objInventoryMovemntnew.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Rate"))

                'Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                'Dim objCost As New MIlkComponentType
                'objCost = clsInventoryMovementNew.GetAvgCost("MI", obj.Item_Code, obj.Silo_Code, clsCommon.myCdbl(dt.Rows(0)("Qty")), obj.Item_UOM, clsCommon.myCdbl(dt.Rows(0)("Fat_KG")), clsCommon.myCdbl(dt.Rows(0)("SNF_KG")), obj.Document_Date, obj.Document_Date, isApplyCostOnPostDate, trans)
                'objInventoryMovemntnew.Fat_Rate = objCost.FAT_Cost / IIf(clsCommon.myCdbl(dt.Rows(0)("Fat_KG")) <= 0, 1, clsCommon.myCdbl(dt.Rows(0)("Fat_KG")))
                'objInventoryMovemntnew.SNF_Rate = objCost.SNF_Cost / IIf(clsCommon.myCdbl(dt.Rows(0)("SNF_KG")) <= 0, 1, clsCommon.myCdbl(dt.Rows(0)("SNF_KG")))
                'objInventoryMovemntnew.Fat_Amt = objCost.FAT_Cost
                'objInventoryMovemntnew.SNF_Amt = objCost.SNF_Cost


                ''--------
                objInventoryMovemntnew.Fat_Rate = clsCommon.myCdbl(dt.Rows(0)("Fat_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Fat_KG"))
                objInventoryMovemntnew.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Amt")) / clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                objInventoryMovemntnew.Fat_Amt = clsCommon.myCdbl(dt.Rows(0)("Fat_Amt"))
                objInventoryMovemntnew.SNF_Amt = clsCommon.myCdbl(dt.Rows(0)("SNF_Amt"))

                ArrInventoryMovementNew1.Add(objInventoryMovemntnew)

                If ArrInventoryMovementNew1 IsNot Nothing AndAlso ArrInventoryMovementNew1.Count > 0 Then
                    clsInventoryMovementNew.SaveData("SI-MT", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementNew1, trans)
                End If
            End If
            ''--------- header end

            ''create Journal entry
            CreateJE(obj, trans)
            ''-----

            HistoryUpdate(obj.Document_Code, trans)
            Dim qry1 As String = " update TSPL_SILO_MILK_TRANSFER_HEAD  set Posted='1' ,Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "' where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strCode, FormId, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select	Document_Date,MainLocation_Code from TSPL_SILO_MILK_TRANSFER_HEAD where Document_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmSiloMilkTransfer, clsCommon.myCstr(dt.Rows(0)("MainLocation_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, "", clsCommon.myCstr(dt.Rows(0)("MainLocation_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If
            Dim issaved As Boolean = True

            Dim qry As String
            ''RICHA AGARWAL 17 aUG,2018 BHA/17/08/18-000454
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            HistoryUpdate(strCode, trans)
            ''-----------
            qry = "update tspl_batch_Item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & strCode & "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='SI-MT' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_SILO_MILK_TRANSFER_HEAD set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, Optional ByVal isJobWorkTransType As Boolean = False) As ClsSiloMilkTransfer
        Dim obj As ClsSiloMilkTransfer = Nothing
        Try
            Dim qry As String = "SELECT TSPL_SILO_MILK_TRANSFER_HEAD.*,tspl_Location_master.Location_Desc as MainLocation_Name,Silo.Location_Desc as Silo_Name,tspl_Item_master.Item_desc  from TSPL_SILO_MILK_TRANSFER_HEAD left outer join tspl_Location_master on tspl_Location_master.Location_Code=TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code left outer join tspl_Location_master Silo on Silo.Location_Code=TSPL_SILO_MILK_TRANSFER_HEAD.Silo_Code left outer join tspl_Item_master on tspl_Item_master.Item_Code=TSPL_SILO_MILK_TRANSFER_HEAD.Item_Code where 2=2"
            Dim whrClas As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas = " AND MainLocation_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            If isJobWorkTransType = True Then
                whrClas += " and TSPL_SILO_MILK_TRANSFER_HEAD.IsJobWork=1 "
            Else
                whrClas += " and TSPL_SILO_MILK_TRANSFER_HEAD.IsJobWork=0 "
            End If

            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SILO_MILK_TRANSFER_HEAD where 1=1 " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SILO_MILK_TRANSFER_HEAD where 1=1 " + whrClas + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SILO_MILK_TRANSFER_HEAD where Document_Code>'" + strDocNo + "' " + whrClas + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SILO_MILK_TRANSFER_HEAD where Document_Code<'" + strDocNo + "' " + whrClas + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code = '" + strDocNo + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsSiloMilkTransfer()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.MainLocation_Code = clsCommon.myCstr(dt.Rows(0)("MainLocation_Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
                obj.IsJobWork = clsCommon.myCstr(dt.Rows(0)("IsJobWork"))
                obj.IsCreatedByUploader = clsCommon.myCdbl(dt.Rows(0)("IsCreatedByUploader"))
                obj.Silo_Code = clsCommon.myCstr(dt.Rows(0)("Silo_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_UOM = clsCommon.myCstr(dt.Rows(0)("Item_UOM"))
                obj.MainLocation_Desc = clsCommon.myCstr(dt.Rows(0)("MainLocation_Name"))
                obj.Silo_Code_Desc = clsCommon.myCstr(dt.Rows(0)("Silo_Name"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_desc"))
                If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                Else
                    obj.Posting_Date = Nothing
                End If

                qry = "SELECT  TSPL_SILO_MILK_TRANSFER_DETAIL.*,Silo.Location_Desc as Silo_Name,tspl_Item_master.Item_desc,MainLoc.Location_Desc as MainLoc_Name from TSPL_SILO_MILK_TRANSFER_DETAIL left outer join tspl_Location_master Silo on Silo.Location_Code=TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code left outer join tspl_Item_master on tspl_Item_master.Item_Code=TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code left outer join tspl_Location_master MainLoc on MainLoc.Location_Code=TSPL_SILO_MILK_TRANSFER_DETAIL.MainLoc_Code where  Document_Code='" + obj.Document_Code + "' order by Line_No "
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of ClsSiloMilkTransferDetails)
                    Dim objTr As ClsSiloMilkTransferDetails
                    For Each dr As DataRow In dt.Rows
                        objTr = New ClsSiloMilkTransferDetails()
                        objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                        objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_desc"))
                        objTr.Silo_Code = clsCommon.myCstr(dr("Silo_Code"))
                        objTr.Silo_Code_Desc = clsCommon.myCstr(dr("Silo_Name"))
                        objTr.MainLoc_Code = clsCommon.myCstr(dr("MainLoc_Code"))
                        objTr.MainLoc_Name = clsCommon.myCstr(dr("MainLoc_Name"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.Rate = clsCommon.myCdbl(dr("Rate"))
                        objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                        objTr.FatAmount = clsCommon.myCdbl(dr("FatAmount"))
                        objTr.SNFAmount = clsCommon.myCdbl(dr("SNFAmount"))
                        objTr.UOM = clsCommon.myCstr(dr("UOM"))
                        If dr("FAT_Pers") Is DBNull.Value Then
                            objTr.fat_pers = 0
                        Else
                            objTr.fat_pers = clsCommon.myCdbl(dr("FAT_Pers"))
                        End If
                        If dr("FAT_KG") Is DBNull.Value Then
                            objTr.fat_kg = 0
                        Else
                            objTr.fat_kg = clsCommon.myCdbl(dr("FAT_KG"))
                        End If
                        If dr("SNF_Pers") Is DBNull.Value Then
                            objTr.snf_pers = 0
                        Else
                            objTr.snf_pers = clsCommon.myCdbl(dr("SNF_Pers"))
                        End If
                        If dr("SNF_KG") Is DBNull.Value Then
                            objTr.snf_kg = 0
                        Else
                            objTr.snf_kg = clsCommon.myCdbl(dr("SNF_KG"))
                        End If

                        obj.Arr.Add(objTr)
                    Next
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New ClsSiloMilkTransfer()
        obj = obj.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmSiloMilkTransfer, obj.MainLocation_Code, obj.Document_Date, trans)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, "", obj.MainLocation_Code, obj.Document_Date, trans)

                HistoryUpdate(strCode, trans)
                If (clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If

                Dim qry As String = "delete from TSPL_SILO_MILK_TRANSFER_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SILO_MILK_TRANSFER_HEAD where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_SILO_MILK_TRANSFER_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function CreateJE(ByVal obj As ClsSiloMilkTransfer, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim strInventryAccForHeader As String = String.Empty
            Dim strInventryAccForDetail As String = String.Empty
            Dim strsegment As String = ""
            strsegment = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.MainLocation_Code + "'", trans)

            Dim desc As String = String.Empty

            desc = "Journal entry created against Silo Milk Transfer for Document No " & obj.Document_Code & ""
            Dim ArryLst As ArrayList = New ArrayList()
            If clsCommon.myLen(obj.Item_Code) > 0 Then
                strInventryAccForHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + obj.Item_Code + "')", trans))
                If clsCommon.myLen(strInventryAccForHeader) <= 0 Then
                    Throw New Exception("Please set inventory Control Account of item " + obj.Item_Code)
                End If
                strInventryAccForHeader = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventryAccForHeader, strsegment, True, trans)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select cast (Avg_Cost as decimal(18,2)) AS Avg_Cost,Item_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" & obj.Document_Code & "' and InOut ='O' and Trans_Type ='SI-MT'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows

                    strInventryAccForDetail = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Inv_Control_Account  AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "')", trans))
                    If clsCommon.myLen(strInventryAccForDetail) <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + clsCommon.myCstr(dr("Item_Code")))
                    End If
                    strInventryAccForDetail = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventryAccForDetail, strsegment, True, trans)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc1() As String = {strInventryAccForDetail, -1 * clsCommon.myCstr(dr("Avg_Cost")), "", "", "", "", "", "", ""}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc1() As String = {strInventryAccForDetail, -1 * clsCommon.myCstr(dr("Avg_Cost")), "", "", "", "", "", "", "I"}
                        ArryLst.Add(Acc1)
                        ''richa agarwal 14 Dec,2018 BHA/27/11/18-000728
                        clsInventoryMovement.UpdateInvControlAccount(obj.Document_Code, "SI-MT", clsCommon.myCstr(dr("Item_Code")), "", strInventryAccForDetail, "O", trans)
                        '------------------
                    End If

                Next
            End If

            Dim dblAvg_cost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select cast (Avg_Cost as decimal(18,2)) AS Avg_Cost  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" & obj.Document_Code & "' and InOut ='I' and Trans_Type ='SI-MT'", trans))

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                Dim Acc2() As String = {strInventryAccForHeader, 1 * dblAvg_cost, "", "", "", "", "", "", ""}
                ArryLst.Add(Acc2)
            Else
                Dim Acc2() As String = {strInventryAccForHeader, 1 * dblAvg_cost, "", "", "", "", "", "", "I"}
                ArryLst.Add(Acc2)
                ''richa agarwal 14 Dec,2018 BHA/27/11/18-000728
                clsInventoryMovement.UpdateInvControlAccount(obj.Document_Code, "SI-MT", clsCommon.myCstr(obj.Item_Code), strInventryAccForHeader, "", "I", trans)
                '------------------
            End If
            transportSql.FunGrnlEntryWithTrans(obj.MainLocation_Code, False, trans, obj.Document_Date, desc, "SI-MT", "SILO Milk Transfer", obj.Document_Code, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", "")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class ClsSiloMilkTransferDetails
#Region "Variables"

    Public Document_Code As String = String.Empty
    Public Line_No As Double = 0
    Public Silo_Code As String = String.Empty
    Public Item_Code As String = String.Empty
    Public UOM As String = String.Empty
    Public Qty As Double = 0
    Public fat_pers As Decimal = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public snf_pers As Decimal = Nothing
    Public Item_Desc As String = String.Empty
    Public Silo_Code_Desc As String = String.Empty
    Public MainLoc_Code As String = String.Empty
    Public MainLoc_Name As String = String.Empty
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsSiloMilkTransferDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsSiloMilkTransferDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                ''richa agarwal ERO/01/04/19-000537 
                clsCommon.AddColumnsForChange(coll, "MainLoc_Code", objtr.MainLoc_Code, True)
                clsCommon.AddColumnsForChange(coll, "Silo_Code", objtr.Silo_Code, True)
                clsCommon.AddColumnsForChange(coll, "Line_No", counter)

                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", objtr.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                clsCommon.AddColumnsForChange(coll, "FatAmount", objtr.FatAmount)
                clsCommon.AddColumnsForChange(coll, "SNFAmount", objtr.SNFAmount)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.fat_kg)
                clsCommon.AddColumnsForChange(coll, "FAT_Pers", objtr.fat_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_Pers", objtr.snf_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.snf_kg)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILO_MILK_TRANSFER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                counter += 1
            Next
        End If
        Return True

    End Function
End Class








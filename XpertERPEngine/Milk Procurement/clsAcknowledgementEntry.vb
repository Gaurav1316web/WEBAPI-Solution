
Imports common
Imports System.Data.SqlClient
Public Class clsAcknowledgementEntry
#Region "Variables"
    Public GSTStatus As String = Nothing
    Public isNewEntry As Boolean = False
    Public MCC_Code As String = String.Empty
    Public MCC_Name As String = String.Empty
    Public Dispatch_Date As Date = Nothing
    Public Chalan_NO As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public Tanker_Dispatch_To As String = String.Empty
    Public Mcc_Or_Plant_Code As String = String.Empty
    Public Dispatch_Document_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing

    Public Comp_Code As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modified_By As String = String.Empty
    Public Modified_Date As String = String.Empty

    Public arrParmValue As List(Of Acknowledment_Entry_Chalan_Parameter) = Nothing

    Public ArrImport As List(Of clsMccDispatch) = Nothing

    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty

    Public Document_No As String = String.Empty
    Public Document_Date As String = String.Empty

    Public Remarks As String = String.Empty




    Public arr As List(Of clsAcknowledgementEntryDetail) = Nothing

#End Region


    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_ACKNOWLEDGENT_ENTRY_Header.document_No as DocNo ,CONVERT(varchar(10), TSPL_ACKNOWLEDGENT_ENTRY_Header.Document_Date,103)+' '+ CONVERT(varchar(5), TSPL_ACKNOWLEDGENT_ENTRY_Header.Document_Date,114)   as [Document Date]  ,TSPL_ACKNOWLEDGENT_ENTRY_Header.Tanker_No as [TankerNo], TSPL_ACKNOWLEDGENT_ENTRY_Header.dispatch_Document_No as [Tanker Dispatch No],CONVERT(varchar(10), TSPL_ACKNOWLEDGENT_ENTRY_Header.Document_Date,103) [Tanker Dispatch Date],TSPL_ACKNOWLEDGENT_ENTRY_Header.MCC_Code as [Mcc Code] ,TSPL_ACKNOWLEDGENT_ENTRY_Header.MCC_Name as [Mcc Name] ,TSPL_ACKNOWLEDGENT_ENTRY_Header.Tanker_Dispatch_To as [Tanker Dispatch To] , case when TSPL_ACKNOWLEDGENT_ENTRY_Header.isPosted=0 then 'NO' else 'YES' end as [Posting Status]  From TSPL_ACKNOWLEDGENT_ENTRY_Header left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= TSPL_ACKNOWLEDGENT_ENTRY_Header.Mcc_Or_Plant_Code"
            str = clsCommon.ShowSelectForm("ACKEntry", qry, "DocNo", whrcls, curcode, "TSPL_ACKNOWLEDGENT_ENTRY_Header.Document_Date desc", isButtonClicked)
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

    Public Shared Function SaveData(ByVal obj As clsAcknowledgementEntry) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsAcknowledgementEntry, ByVal tran As SqlTransaction, Optional PostingStatus As Integer = 0, Optional chkAlreadyPosted As Boolean = True, Optional RefBulkDispatchUploader As String = "") As Boolean
        Dim PrevTanker As String = ""
        Try
            If chkAlreadyPosted Then
                If Not obj.isNewEntry Then
                    If clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_ACKNOWLEDGENT_ENTRY_HEADER", "Document_No", obj.Document_No, "isposted=1", tran) Then
                        Throw New Exception("Document is Already Posted, Please reload the document")
                    End If
                End If
            End If
            Dim qry As String = ""


            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "ACKNOWLEDGENT ENTRY", obj.MCC_Code, obj.Dispatch_Date, tran)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_ACKNOWLEDGENT_ENTRY_Detail where Document_No='" + obj.Document_No + "'", tran)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "MCC_Name", obj.MCC_Name)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Document_No", obj.Dispatch_Document_No)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Date", clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Tanker_Dispatch_To", obj.Tanker_Dispatch_To)

            clsCommon.AddColumnsForChange(coll, "Mcc_Or_Plant_Code", obj.Mcc_Or_Plant_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)

            clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", obj.Modified_Date)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)

            If obj.isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(tran, obj.Document_Date, clsDocType.AcknowledgementEntry, "", obj.MCC_Code)

                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error In Document code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACKNOWLEDGENT_ENTRY_HEADER", OMInsertOrUpdate.Insert, "", tran)
            Else
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACKNOWLEDGENT_ENTRY_HEADER", OMInsertOrUpdate.Update, "TSPL_ACKNOWLEDGENT_ENTRY_HEADER.Document_No='" + obj.Document_No + "'", tran)

            End If
            Acknowledment_Entry_Chalan_Parameter.SaveData(obj.Document_No, obj.arrParmValue, tran)


            clsAcknowledgementEntryDetail.SaveData(obj.Document_No, obj.arr, tran)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function UpdateAfterPosting(ByVal obj As clsMccDispatch, ByVal DocNO As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(DocNO) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "openingKM", obj.openingKM)
                clsCommon.AddColumnsForChange(coll, "closingKM", obj.closingKM)
                clsCommon.AddColumnsForChange(coll, "Toll_Amount", obj.Toll_Amount)
                If obj.Closing_Date IsNot Nothing And clsCommon.myLen(obj.Closing_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Closing_Date", clsCommon.GetPrintDate(obj.Closing_Date, "dd/MMM/yyyy hh:mm tt"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_mcc_dispatch_challan", OMInsertOrUpdate.Update, "tspl_mcc_dispatch_challan.chalan_no='" + DocNO + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Shared Function UpdateProvisionAmount(ByVal obj As clsMccDispatch, ByVal trans As SqlTransaction) As Boolean
        Dim Actual_KM As Double = 0
        obj.Payment_Amount = 0
        Dim isPriceFromTankerMaster As Boolean = False
        Dim qry As String = "select TSPL_FREIGHT_CHARGES_MASTER.Freight_Code,TSPL_FREIGHT_CHARGES_MASTER.Status,TSPL_FREIGHT_CHARGES_MASTER.Shift_Charges,TSPL_FREIGHT_CHARGES_MASTER.Avg_Km_Ltr,TSPL_FREIGHT_CHARGES_MASTER.Diesel_Rate,TSPL_FREIGHT_CHARGES_MASTER.Price_KM,TSPL_FREIGHT_CHARGES_MASTER.Price_Ltr_KG,TSPL_FREIGHT_CHARGES_MASTER.Rate_Type,TSPL_FREIGHT_CHARGES_MASTER.Rental_Type,TSPL_FREIGHT_CHARGES_MASTER.Rental_Amount,TSPL_FREIGHT_CHARGES_MASTER.Is_Additional ,TSPL_TANKER_MASTER.Provision_Min_Qty  from TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING " + Environment.NewLine +
        " left outer join TSPL_FREIGHT_CHARGES_MASTER on TSPL_FREIGHT_CHARGES_MASTER.Freight_Code=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Freight_Code " + Environment.NewLine +
        " left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Tanker_No " + Environment.NewLine +
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
            If obj.IsAgainstJobWork = 1 Then
                Actual_KM = getDistance(obj.MCC_Code, obj.Sublocation_Code, trans)
            Else
                Actual_KM = getDistance(obj.MCC_Code, obj.Mcc_Or_Plant_Code, trans)
            End If

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

    Public Shared Function GetTankerNO(ChallanNo As String, Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        Try
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_No  from TSPL_MCC_Dispatch_Challan  where Chalan_NO='" & ChallanNo & "'", trans))
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

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsAcknowledgementEntry
        Return getData(strCode, navtype, Nothing)
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal trans As SqlTransaction) As clsAcknowledgementEntry
        Dim obj As New clsAcknowledgementEntry
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strCode) <= 0 Then
                    whrCls = " and mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            obj.arrParmValue = New List(Of Acknowledment_Entry_Chalan_Parameter)
            Dim qst As String = " select *   From TSPL_ACKNOWLEDGENT_ENTRY_HEADER   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_ACKNOWLEDGENT_ENTRY_HEADER.Document_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_ACKNOWLEDGENT_ENTRY_HEADER.Document_No in (select min(Document_No ) from TSPL_ACKNOWLEDGENT_ENTRY_HEADER where Document_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_ACKNOWLEDGENT_ENTRY_HEADER.Document_No in (select MIN(Document_No ) from TSPL_ACKNOWLEDGENT_ENTRY_HEADER where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_ACKNOWLEDGENT_ENTRY_HEADER.Document_No in (select Max(Document_No ) from TSPL_ACKNOWLEDGENT_ENTRY_HEADER where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_ACKNOWLEDGENT_ENTRY_HEADER.Document_No in (select Max(Document_No ) from TSPL_ACKNOWLEDGENT_ENTRY_HEADER where Document_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
                obj.Dispatch_Date = clsCommon.myCDate(dt.Rows(0)("Dispatch_Date"))
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Tanker_Dispatch_To = clsCommon.myCstr(dt.Rows(0)("Tanker_Dispatch_To"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Mcc_Or_Plant_Code = clsCommon.myCstr(dt.Rows(0)("Mcc_Or_Plant_Code"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))

                obj.Dispatch_Document_No = clsCommon.myCstr(dt.Rows(0)("Dispatch_Document_No"))

                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If


                obj.arrParmValue = Acknowledment_Entry_Chalan_Parameter.getData(obj.Document_No, trans)

                obj.arr = clsAcknowledgementEntryDetail.GetData(obj.Document_No, trans)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean


        Try
            Dim Qry As String = "select isPosted from TSPL_ACKNOWLEDGENT_ENTRY_HEADER where  Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select Receipt_Challan_No from TSPL_MILK_TRANSFER_IN where AcknowEntryDocument_No='" + strCode + "'"
            Dim milktransferno As String = clsDBFuncationality.getSingleValue(Qry, trans)
            If clsCommon.myLen(milktransferno) > 0 Then
                clsMilkTransferIn.ReverseAndUnpost(milktransferno, trans)
                clsMilkTransferIn.deleteData(milktransferno, trans)
            End If


            Qry = "delete from TSPL_Gate_Out where AcknowEntryDocument_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            Qry = "select Unloading_no from TSPL_MILK_UNLOADING where AcknowEntryDocument_No='" + strCode + "'"
            milktransferno = clsDBFuncationality.getSingleValue(Qry, trans)
            If clsCommon.myLen(milktransferno) > 0 Then
                Qry = "delete from TSPL_Milk_unloading_Chember_Details where Unloading_No='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_MILK_UNLOADING where Unloading_No='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If


            Qry = "select qc_no from tspl_quality_check where AcknowEntryDocument_No='" + strCode + "'"
            milktransferno = clsDBFuncationality.getSingleValue(Qry, trans)
            If clsCommon.myLen(milktransferno) > 0 Then
                Qry = "delete from TSPL_QC_Parameter_Detail where qc_no='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_Quality_Chember_Details where qc_no='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from tspl_quality_check where qc_no='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "select Weighment_No from tspl_weighment_detail where AcknowEntryDocument_No='" + strCode + "'"
            milktransferno = clsDBFuncationality.getSingleValue(Qry, trans)
            If clsCommon.myLen(milktransferno) > 0 Then
                Qry = "delete from TSPL_Weighment_Chember_Details where Weighment_No='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from tspl_weighment_detail where Weighment_No='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            End If

            Qry = "select gate_entry_no from tspl_gate_entry_details where AcknowEntryDocument_No='" + strCode + "'"
            milktransferno = clsDBFuncationality.getSingleValue(Qry, trans)
            If clsCommon.myLen(milktransferno) > 0 Then
                Qry = "delete from TSPL_Gate_Entry_Price_Chart where  GE_Code='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_Gate_Entry_Chember_Details where  GE_Code='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_Bulk_Gate_Entry_Chember_Details where  GE_Code='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from tspl_gate_entry_details where  gate_entry_no='" & milktransferno & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If


            Qry = "Update TSPL_ACKNOWLEDGENT_ENTRY_Header set isPosted = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)



        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = clsAcknowledgementEntry.PostData(FormId, strDocNo, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isChildTrans As Boolean = False
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If

            Dim obj As clsAcknowledgementEntry = clsAcknowledgementEntry.getData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "ACKNOWLEDGENT ENTRY", obj.MCC_Code, obj.Dispatch_Date, trans)

            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If


            Dim strQry As String = " update TSPL_ACKNOWLEDGENT_ENTRY_Header set isPosted='1',Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            SaveAndPostFromGateEntryToMilkTransferIn(obj.Document_No, trans)



            Return isSaved
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateProvison(ByVal strDocNo As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As clsMccDispatch = clsMccDispatch.getData(strDocNo, NavigatorType.Current, trans)

        Dim objProv As clsProvisionEntry = New clsProvisionEntry()
        objProv.isNewEntry = True
        objProv.Doc_Date = obj.Dispatch_Date 'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")

        Dim strTransporterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_Transporter_Code  from tspl_tanker_master where Tanker_No ='" & obj.Tanker_No & "'", trans))
        Dim strTransporterName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from tspl_tanker_master where Tanker_No ='" & obj.Tanker_No & "'", trans))
        objProv.Vendor_Code = strTransporterCode
        objProv.Vendor_Desc = strTransporterName
        objProv.Vendor_Type = "Secondary Transporter"
        objProv.Prov_type = "Freight"
        objProv.Status = "No"
        objProv.Ref_Doc_No = obj.Chalan_NO


        Dim strRatePerKM As Double = clsDBFuncationality.getSingleValue("select isnull(Price_KM,0) as Price_KM from tspl_tanker_master where Tanker_No='" + obj.Tanker_No + "'", trans)

        If strRatePerKM <= 0 Then
            Throw New Exception("First Enter Price Per KM for Tanker No : " + obj.Tanker_No + " in Tanker Master.")
        End If

        Dim dclDistance As Decimal = clsDBFuncationality.getSingleValue("select isnull(distance,0) as distance from tspl_location_distance_master where From_Location_code = '" + obj.MCC_Code + "' and to_Location_Code= '" + obj.Mcc_Or_Plant_Code + "'", trans)
        If dclDistance <= 0 Then
            Throw New Exception("First Enter +ve Distance From : " + obj.MCC_Code + " To Location " + obj.Mcc_Or_Plant_Code + " in Location Distance Mapping.")
        End If

        'If (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, trans))) = 1) Then
        If dclDistance > (obj.closingKM - obj.openingKM) Then
            dclDistance = (obj.closingKM - obj.openingKM)
        End If
        'End If
        Dim dbltollAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Toll_Amt ,0) from  tspl_location_distance_master where From_Location_code ='" & obj.MCC_Code & "' and to_Location_Code ='" & obj.Mcc_Or_Plant_Code & "'", trans))
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, trans)) = 0, False, True) = False AndAlso IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOfTankerDispatchWithClosingKM, clsFixedParameterCode.CreateProvisionOfTankerDispatchWithClosingKM, trans)) = 0, False, True) = True Then
            dbltollAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Toll_Amount ,0) from  tspl_mcc_dispatch_challan where  Chalan_NO ='" & obj.Chalan_NO & "'", trans))

        End If

        objProv.Amount = strRatePerKM * dclDistance + dbltollAmt
        objProv.Prog_Code = FormId
        objProv.Prov_Month = Month(obj.Dispatch_Date)
        objProv.Prov_Year = Year(obj.Dispatch_Date)
        objProv.Comp_Code = obj.Comp_Code
        objProv.Created_By = obj.Created_By
        objProv.Created_Date = obj.Created_Date
        objProv.Modified_By = obj.Modified_By
        objProv.Loc_Code = obj.MCC_Code
        objProv.Loc_Desc = obj.MCC_Name
        objProv.Toll_Amt = dbltollAmt
        clsProvisionEntry.SaveData(objProv, trans)
        clsProvisionEntry.PostData(objProv.Doc_No, trans, False)

        Return True
    End Function

    Public Shared Function CreateTankerDispatchStockDetail(ByVal obj As clsMccDispatch, ByVal trans As SqlTransaction)
        Try
            Dim totalamt As Double = 0
            Dim amtdifference As Double = 0
            Dim qry As String = ""
            Dim ArrInventoryMovement As List(Of clsMCCDispatchStockDetail) = New List(Of clsMCCDispatchStockDetail)

            Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
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
            Dim strsublocation As String = String.Empty

            Dim strqry As String = String.Empty
            strqry = "select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(obj.MCC_Code) & "' order by TSPL_Location_MASTER.Location_Code "

            Dim balqtyofvl As Double = 0.0
            Dim balqtyfortotallocation As Double = 0.0
            Dim fatqtyfortotallocation As Double = 0.0
            Dim snfqtyfortotallocation As Double = 0.0
            Dim CurBalOFVL_SNF As Double = 0
            Dim CurBalOFVL_FAT As Double = 0


            Dim strtransactionLocation As String = obj.MCC_Code
            Dim IsSubLocation As Boolean = False
            balqtyfortotallocation = obj.Net_Qty
            fatqtyfortotallocation = obj.FAT_KG
            snfqtyfortotallocation = obj.SNF_KG

            Dim strItemUnitCode As String = obj.UOM_Code

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
            '' to add main location into datatable

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))

                    If clsCommon.myLen(strsublocation) > 0 Then
                        balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MCC_Code, strsublocation, obj.Chalan_NO, obj.Dispatch_Date, trans, "KG"))
                    End If

                    If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                        totalamt = 0
                        amtdifference = 0
                        Dim objInventoryMovemnt1 As New clsMCCDispatchStockDetail()
                        Dim ArrInventoryMovement1 As List(Of clsMCCDispatchStockDetail) = New List(Of clsMCCDispatchStockDetail)
                        objInventoryMovemnt1.InOut = "O"

                        balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MCC_Code, strsublocation, obj.Chalan_NO, obj.Dispatch_Date, trans, "KG"))

                        If balqtyofvl >= balqtyfortotallocation Then
                            balqtyofvl = balqtyfortotallocation
                        End If
                        objInventoryMovemnt1.main_location = obj.MCC_Code

                        objInventoryMovemnt1.Location_Code = strsublocation
                        objInventoryMovemnt1.Other_Location_Code = obj.Mcc_Or_Plant_Code
                        objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
                        objInventoryMovemnt1.Item_Code = obj.Item_Code
                        objInventoryMovemnt1.Item_Desc = obj.Item_Desc
                        objInventoryMovemnt1.Qty = balqtyofvl
                        objInventoryMovemnt1.UOM = strItemUnitCode

                        If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                            Dim dblTempFATPer As Decimal = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.FAT_KG * 100 / obj.Net_Qty), 10, MidpointRounding.ToEven)
                            objInventoryMovemnt1.FAT_Per = Math.Round(dblTempFATPer, 2, MidpointRounding.ToEven)

                            Dim dblTempSNFPer As Decimal = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.SNF_KG * 100 / obj.Net_Qty), 10, MidpointRounding.ToEven)
                            objInventoryMovemnt1.SNF_Per = Math.Round(dblTempSNFPer, 2, MidpointRounding.ToEven)
                            objInventoryMovemnt1.FAT_KG = Math.Round(dblTempFATPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                            objInventoryMovemnt1.SNF_KG = Math.Round(dblTempSNFPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                        Else

                            objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(obj.Chalan_NO, trans)
                            objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(obj.Chalan_NO, trans)
                            ''To save according to FAT AND SNF work 6 Dec,2012
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, trans), "1") = CompairStringResult.Equal Then

                                If clsCommon.myLen(strsublocation) > 0 Then
                                    Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & obj.Item_Code & "','" & strsublocation & "','" & clsCommon.GetPrintDate(obj.Dispatch_Date, "dd-MMM-yyyy") & "')", trans)
                                    If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                                        CurBalOFVL_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                                        CurBalOFVL_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                                    End If
                                End If
                                If CurBalOFVL_SNF <> 0 Or CurBalOFVL_FAT <> 0 Then

                                    If CurBalOFVL_SNF >= snfqtyfortotallocation Then
                                        CurBalOFVL_SNF = snfqtyfortotallocation
                                    End If
                                    If CurBalOFVL_FAT >= fatqtyfortotallocation Then
                                        CurBalOFVL_FAT = fatqtyfortotallocation
                                    End If

                                    objInventoryMovemnt1.FAT_KG = CurBalOFVL_FAT
                                    objInventoryMovemnt1.SNF_KG = CurBalOFVL_SNF
                                End If
                            Else
                                objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(obj.Chalan_NO, trans)
                                objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(obj.Chalan_NO, trans)
                                objInventoryMovemnt1.FAT_KG = clsMccDispatch.getFATPer(obj.Chalan_NO, trans) * balqtyofvl / 100
                                objInventoryMovemnt1.SNF_KG = clsMccDispatch.getSNFPer(obj.Chalan_NO, trans) * balqtyofvl / 100
                            End If

                        End If

                        objInventoryMovemnt1.Net_Cost = Math.Round((obj.FAT_RATE * objInventoryMovemnt1.FAT_KG) + (obj.SNF_RATE * objInventoryMovemnt1.SNF_KG), 2)
                        objInventoryMovemnt1.Fat_Rate = obj.FAT_RATE
                        objInventoryMovemnt1.SNF_Rate = obj.SNF_RATE
                        objInventoryMovemnt1.Fat_Amt = clsCommon.myFormat(obj.FAT_RATE * objInventoryMovemnt1.FAT_KG)
                        objInventoryMovemnt1.SNF_Amt = clsCommon.myFormat(obj.SNF_RATE * objInventoryMovemnt1.SNF_KG)

                        totalamt += objInventoryMovemnt1.Fat_Amt + objInventoryMovemnt1.SNF_Amt 'done by stuti on 12/05/2017
                        amtdifference = obj.Amount - totalamt
                        objInventoryMovemnt1.SNF_Amt += amtdifference

                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt1.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objInventoryMovemnt1.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt1.ItemType = "FT"
                        End If
                        objInventoryMovemnt1.ItemType = strItemTypeToSave

                        objInventoryMovemnt1.Chalan_Date = obj.Dispatch_Date
                        ArrInventoryMovement1.Add(objInventoryMovemnt1)
                        If balqtyofvl > 0 Or CurBalOFVL_FAT > 0 Or CurBalOFVL_SNF > 0 Then
                            clsMCCDispatchStockDetail.SaveData(obj.Chalan_NO, ArrInventoryMovement1, trans)
                        End If
                        balqtyfortotallocation = balqtyfortotallocation - balqtyofvl
                        fatqtyfortotallocation = fatqtyfortotallocation - CurBalOFVL_FAT
                        snfqtyfortotallocation = snfqtyfortotallocation - CurBalOFVL_SNF


                    End If

                Next
            End If

            ''-- to insert data into main locations when sub locations has no value
            If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                totalamt = 0
                amtdifference = 0
                Dim objInventoryMovemnt1 As New clsMCCDispatchStockDetail()
                Dim ArrInventoryMovement1 As List(Of clsMCCDispatchStockDetail) = New List(Of clsMCCDispatchStockDetail)
                objInventoryMovemnt1.InOut = "O"

                balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(obj.Item_Code, obj.MCC_Code, "", obj.Chalan_NO, obj.Dispatch_Date, trans, "KG"))

                If balqtyofvl >= balqtyfortotallocation Then
                    balqtyofvl = balqtyfortotallocation
                End If
                objInventoryMovemnt1.main_location = obj.MCC_Code

                objInventoryMovemnt1.Location_Code = obj.MCC_Code
                objInventoryMovemnt1.Other_Location_Code = obj.Mcc_Or_Plant_Code
                objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
                objInventoryMovemnt1.Item_Code = obj.Item_Code
                objInventoryMovemnt1.Item_Desc = obj.Item_Desc
                objInventoryMovemnt1.Qty = balqtyofvl
                objInventoryMovemnt1.UOM = strItemUnitCode
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    Dim dblTempFATPer As Decimal = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.FAT_KG * 100 / obj.Net_Qty), 10, MidpointRounding.ToEven)
                    objInventoryMovemnt1.FAT_Per = Math.Round(dblTempFATPer, 2, MidpointRounding.ToEven)

                    Dim dblTempSNFPer As Decimal = Math.Round(IIf(obj.Net_Qty = 0, 0, obj.SNF_KG * 100 / obj.Net_Qty), 10, MidpointRounding.ToEven)
                    objInventoryMovemnt1.SNF_Per = Math.Round(dblTempSNFPer, 2, MidpointRounding.ToEven)
                    objInventoryMovemnt1.FAT_KG = Math.Round(dblTempFATPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                    objInventoryMovemnt1.SNF_KG = Math.Round(dblTempSNFPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                Else

                    objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(obj.Chalan_NO, trans)
                    objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(obj.Chalan_NO, trans)
                    ''To save according to FAT AND SNF work 6 Dec,2012
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, trans), "1") = CompairStringResult.Equal Then

                        Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & obj.Item_Code & "','" & obj.MCC_Code & "','" & clsCommon.GetPrintDate(obj.Dispatch_Date, "dd-MMM-yyyy") & "')", trans)
                        If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                            CurBalOFVL_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                            CurBalOFVL_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                        End If

                        If CurBalOFVL_SNF <> 0 Or CurBalOFVL_FAT <> 0 Then

                            If CurBalOFVL_SNF >= snfqtyfortotallocation Then
                                CurBalOFVL_SNF = snfqtyfortotallocation
                            End If
                            If CurBalOFVL_FAT >= fatqtyfortotallocation Then
                                CurBalOFVL_FAT = fatqtyfortotallocation
                            End If

                            objInventoryMovemnt1.FAT_KG = CurBalOFVL_FAT
                            objInventoryMovemnt1.SNF_KG = CurBalOFVL_SNF
                        End If
                    Else
                        objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(obj.Chalan_NO, trans)
                        objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(obj.Chalan_NO, trans)
                        objInventoryMovemnt1.FAT_KG = clsMccDispatch.getFATPer(obj.Chalan_NO, trans) * balqtyofvl / 100
                        objInventoryMovemnt1.SNF_KG = clsMccDispatch.getSNFPer(obj.Chalan_NO, trans) * balqtyofvl / 100
                    End If
                    ''----------------end of To save according to FAT AND SNF work 6 Dec,2012

                End If

                objInventoryMovemnt1.Net_Cost = Math.Round((obj.FAT_RATE * objInventoryMovemnt1.FAT_KG) + (obj.SNF_RATE * objInventoryMovemnt1.SNF_KG), 2)
                objInventoryMovemnt1.Fat_Rate = obj.FAT_RATE
                objInventoryMovemnt1.SNF_Rate = obj.SNF_RATE
                objInventoryMovemnt1.Fat_Amt = clsCommon.myFormat(obj.FAT_RATE * objInventoryMovemnt1.FAT_KG)
                objInventoryMovemnt1.SNF_Amt = clsCommon.myFormat(obj.SNF_RATE * objInventoryMovemnt1.SNF_KG)

                totalamt += objInventoryMovemnt1.Fat_Amt + objInventoryMovemnt1.SNF_Amt 'done by stuti on 12/05/2017
                amtdifference = obj.Amount - totalamt
                objInventoryMovemnt1.SNF_Amt += amtdifference

                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "FT"
                End If
                objInventoryMovemnt1.ItemType = strItemTypeToSave
                objInventoryMovemnt1.Chalan_Date = obj.Dispatch_Date

                ArrInventoryMovement1.Add(objInventoryMovemnt1)
                If balqtyofvl > 0 Or CurBalOFVL_FAT > 0 Or CurBalOFVL_SNF > 0 Then
                    clsMCCDispatchStockDetail.SaveData(obj.Chalan_NO, ArrInventoryMovement1, trans)
                End If
                balqtyfortotallocation = balqtyfortotallocation - balqtyofvl
                fatqtyfortotallocation = fatqtyfortotallocation - CurBalOFVL_FAT
                snfqtyfortotallocation = snfqtyfortotallocation - CurBalOFVL_SNF
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJEAndInvenotryMovement(ByVal obj As clsMccDispatch, ByVal trans As SqlTransaction, ByVal objTransferIn As String, ByVal strVoucherNoForRecreateOnly As String)
        Dim qry As String = ""
        Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        Dim dclTransferAmount As Decimal = 0
        Dim dclInventoryAmount As Decimal = 0
        Dim settTankerDispatchIntermittentSingleGateIn As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, trans)) = 1)
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchFinancialImpactInTransferIn, clsFixedParameterCode.TankerDispatchFinancialImpactInTransferIn, trans)) = 1 Then
            qry = "select * from tspl_inventory_movement_new where trans_Type='MilkTransferIn' and Source_Doc_No='" + objTransferIn + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objInventoryMovemnt As New clsInventoryMovementNew()
                    objInventoryMovemnt.InOut = "O"
                    objInventoryMovemnt.Location_Code = obj.MCC_Code
                    objInventoryMovemnt.Other_Location_Code = obj.Mcc_Or_Plant_Code
                    objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
                    objInventoryMovemnt.Item_Code = obj.Item_Code
                    objInventoryMovemnt.Item_Desc = obj.Item_Desc
                    objInventoryMovemnt.Qty = clsCommon.myCdbl(dr("Qty"))
                    objInventoryMovemnt.UOM = clsCommon.myCstr(dr("UOM"))
                    objInventoryMovemnt.MRP = clsCommon.myCdbl(dr("MRP"))
                    objInventoryMovemnt.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))
                    objInventoryMovemnt.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                    objInventoryMovemnt.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                    objInventoryMovemnt.FAT_Per = clsCommon.myCdbl(dr("FAT_Per"))
                    objInventoryMovemnt.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                    objInventoryMovemnt.ItemType = clsCommon.myCstr(dr("ItemType"))
                    objInventoryMovemnt.Fat_Rate = obj.Avg_FAT_Rate
                    objInventoryMovemnt.SNF_Rate = obj.Avg_SNF_Rate
                    objInventoryMovemnt.Fat_Amt = obj.Avg_FAT_Rate * objInventoryMovemnt.FAT_KG
                    objInventoryMovemnt.SNF_Amt = obj.Avg_SNF_Rate * objInventoryMovemnt.SNF_KG
                    objInventoryMovemnt.Net_Cost = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                    objInventoryMovemnt.Basic_Cost = IIf(objInventoryMovemnt.Qty = 0, 0, objInventoryMovemnt.Net_Cost / objInventoryMovemnt.Qty)
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                    dclTransferAmount += clsCommon.myCdbl(dr("Net_Cost"))
                    dclInventoryAmount += objInventoryMovemnt.Net_Cost
                Next
                clsInventoryMovementNew.SaveData("DispChallan", obj.Chalan_NO, obj.Dispatch_Date, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            Else
                Throw New Exception("Transfer in inventory impact not found")
            End If
        Else
            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, trans)) = 1) Then
                CreateInvMovementSiloWise(obj.Chalan_NO, obj.Dispatch_Date, obj.MCC_Code, obj.Mcc_Or_Plant_Code, trans)
            ElseIf obj.arr Is Nothing OrElse obj.arr.Count <= 0 Then ''BHA/05/07/18-000131 by balwinder on 11/07/2018 
                CreateInvMovement(0, obj.Chalan_NO, obj.Dispatch_Date, obj.MCC_Code, obj.Mcc_Or_Plant_Code, obj.Item_Code, "KG", obj.Net_Qty, obj.FAT_KG, obj.SNF_KG, obj.FAT_RATE, obj.SNF_RATE, obj.Amount, trans, obj.Rejected_Only)
            Else
                For Each objtr As clsMCCDispatchDetail In obj.arr
                    Dim flag As Boolean = True
                    If settTankerDispatchIntermittentSingleGateIn AndAlso obj.isIntermittent Then
                        If clsCommon.myLen(objtr.Intermittent_Dispatch_No) > 0 Then
                            flag = False
                        Else
                            flag = True
                        End If
                    End If
                    If flag Then
                        CreateInvMovement(obj.arr.Count, obj.Chalan_NO, obj.Dispatch_Date, obj.MCC_Code, obj.Mcc_Or_Plant_Code, objtr.Item_Code, objtr.Item_UOM, objtr.Qty_KG, objtr.FAT_KG, objtr.SNF_KG, objtr.FAT_Rate, objtr.SNF_Rate, objtr.Amount, trans, obj.Rejected_Only)
                    End If
                Next
            End If
        End If
        ''-------------------------- end of inventory movement 



        '-----------------SEAL Qty
        Dim strItemType As String
        Dim strItemTypeToSave As String
        Dim strItemUnitCode As String
        Dim strItemCode As String = String.Empty
        strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='PS' ", trans))
        Dim SealQty As Integer = 0
        SealQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where chalan_no='" & obj.Chalan_NO & "' ", trans))
        If SealQty > 0 Then
            Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            strItemType = clsItemMaster.GetItemType(strItemCode, trans)
            'Dim strItemTypeToSave As String = ""
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
                'Throw New Exception("Item Type not found: " + strItemType)
            End If
            strItemUnitCode = clsItemMaster.GetStockUnit(strItemCode, trans)

            Dim objInventoryMovemnt1 As clsInventoryMovement = New clsInventoryMovement()
            objInventoryMovemnt1.InOut = "O"
            objInventoryMovemnt1.Location_Code = obj.MCC_Code
            objInventoryMovemnt1.Other_Location_Code = obj.Mcc_Or_Plant_Code
            objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
            objInventoryMovemnt1.Item_Code = strItemCode
            objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
            objInventoryMovemnt1.Qty = SealQty
            objInventoryMovemnt1.UOM = strItemUnitCode
            objInventoryMovemnt1.MRP = 0
            objInventoryMovemnt1.Add_Cost = 0
            objInventoryMovemnt1.Net_Cost = 0
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt1.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt1.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt1.ItemType = "FT"
            End If
            objInventoryMovemnt1.ItemType = strItemTypeToSave
            objInventoryMovemnt1.Basic_Cost = 0
            ArrInventoryMovement1.Add(objInventoryMovemnt1)
            If Not obj.isBulkSaleData Then
                clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrLocationDetails1, trans)
                clsInventoryMovement.SaveData("DispChallan", obj.Chalan_NO, obj.Dispatch_Date, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
            End If
        End If

        strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='MS' ", trans))
        SealQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select COUNT(*) from(select Seal_No1 as seal_No,Chalan_NO   from (select Seal_No1,Chalan_NO   from TSPL_MCC_Dispatch_Challan union all select Seal_No2,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No3,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No4,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No5,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No6,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No7,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No8,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No9,Chalan_NO  from TSPL_MCC_Dispatch_Challan union all select Seal_No10,Chalan_NO  from TSPL_MCC_Dispatch_Challan) xx where Seal_No1 <>'' ) yyy where yyy.Chalan_NO ='" & obj.Chalan_NO & "'", trans))
        If SealQty > 0 Then
            Dim ArrLocationDetails1 As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement1 As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            strItemType = clsItemMaster.GetItemType(strItemCode, trans)
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
            strItemUnitCode = clsItemMaster.GetStockUnit(strItemCode, trans)

            Dim objInventoryMovemnt1 As clsInventoryMovement = New clsInventoryMovement()
            objInventoryMovemnt1.InOut = "O"
            objInventoryMovemnt1.Location_Code = obj.MCC_Code
            objInventoryMovemnt1.Other_Location_Code = obj.Mcc_Or_Plant_Code
            objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans)
            objInventoryMovemnt1.Item_Code = strItemCode
            objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
            objInventoryMovemnt1.Qty = SealQty
            objInventoryMovemnt1.UOM = strItemUnitCode
            objInventoryMovemnt1.MRP = 0
            objInventoryMovemnt1.Add_Cost = 0
            objInventoryMovemnt1.Net_Cost = 0
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt1.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt1.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt1.ItemType = "FT"
            End If
            objInventoryMovemnt1.ItemType = strItemTypeToSave
            objInventoryMovemnt1.Basic_Cost = 0
            ArrInventoryMovement1.Add(objInventoryMovemnt1)
            If Not obj.isBulkSaleData Then
                clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrLocationDetails1, trans)
                clsInventoryMovement.SaveData("DispChallan", obj.Chalan_NO, obj.Dispatch_Date, clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
            End If
        End If
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.CreateTankerDispatchGL, trans)) = 1 Then
            CreateTransferOutJE(obj, "", trans, dclTransferAmount, dclInventoryAmount)
        End If
        '-----------------End of SEAL Qty
        Return True
    End Function
    Shared Sub CreateInvMovementSiloWise(ByVal strChalanceNo As String, ByVal dtChalanDate As DateTime, ByVal strMCC_Code As String, ByVal strMcc_Or_Plant_Code As String, ByVal trans As SqlTransaction)
        Dim arr As List(Of clsMCCDispatchSiloWise) = clsMCCDispatchSiloWise.GetData(strChalanceNo, trans)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            Dim ArrInventoryMovement1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            For Each objtr As clsMCCDispatchSiloWise In arr
                Dim objInventoryMovemnt1 As New clsInventoryMovementNew()
                objInventoryMovemnt1.InOut = "O"
                objInventoryMovemnt1.main_location = strMCC_Code

                objInventoryMovemnt1.Location_Code = objtr.Location_Code
                objInventoryMovemnt1.Other_Location_Code = strMcc_Or_Plant_Code
                objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(strMcc_Or_Plant_Code, trans)
                objInventoryMovemnt1.Item_Code = objtr.Item_Code
                objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                objInventoryMovemnt1.Qty = objtr.Qty
                objInventoryMovemnt1.UOM = objtr.UOM
                objInventoryMovemnt1.MRP = 0
                objInventoryMovemnt1.Add_Cost = 0

                objInventoryMovemnt1.FAT_Per = Math.Round(objtr.FAT_Per, 2, MidpointRounding.ToEven)
                objInventoryMovemnt1.SNF_Per = Math.Round(objtr.SNF_Per, 2, MidpointRounding.ToEven)
                objInventoryMovemnt1.FAT_KG = Math.Round(objtr.FAT_KG, 2, MidpointRounding.ToEven)
                objInventoryMovemnt1.SNF_KG = Math.Round(objtr.SNF_KG, 2, MidpointRounding.ToEven)

                objInventoryMovemnt1.Net_Cost = Math.Round(objtr.Amount, 2)
                objInventoryMovemnt1.Fat_Rate = objtr.Fat_Rate
                objInventoryMovemnt1.SNF_Rate = objtr.SNF_Rate
                objInventoryMovemnt1.Fat_Amt = objtr.Fat_Amt
                objInventoryMovemnt1.SNF_Amt = objtr.SNF_Amt
                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "A"
                Else
                    objInventoryMovemnt1.ItemType = strItemType
                End If

                If objtr.Stock_Qty = 0 Then
                    objInventoryMovemnt1.Basic_Cost = 0
                Else
                    objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt1.Net_Cost / objtr.Stock_Qty
                End If
                ArrInventoryMovement1.Add(objInventoryMovemnt1)
            Next
            clsInventoryMovementNew.SaveData("DispChallan", strChalanceNo, dtChalanDate, clsCommon.GetPrintDate(dtChalanDate, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
        End If
    End Sub
    Shared Sub CreateInvMovement(ByVal intArrCount As Integer, ByVal strChalanceNo As String, ByVal dtChalanDate As DateTime, ByVal strMCC_Code As String, ByVal strMcc_Or_Plant_Code As String, ByVal strItemCode As String, ByVal strUOMCode As String, ByVal dblNet_Qty As Decimal, ByVal dblFAT_KG As Decimal, ByVal dblSNF_KG As Decimal, ByVal dblFAT_Rate As Decimal, ByVal dblSNF_Rate As Decimal, ByVal dblAmount As Decimal, ByVal trans As SqlTransaction, ByVal IsRejectOnly As Boolean)

        Dim strItemType As String = clsItemMaster.GetItemType(strItemCode, trans)
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


        Dim strsublocation As String = String.Empty
        Dim strqry As String = String.Empty
        strqry = "select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(strMCC_Code) & "'"
        If IsRejectOnly Then
            strqry += " and TSPL_Location_MASTER.Rejected_Type='Y'"
        Else
            strqry += " and TSPL_Location_MASTER.Rejected_Type='N'"
        End If
        strqry += " order by TSPL_Location_MASTER.Location_Code "

        Dim balqtyofvl As Double = 0.0
        Dim balqtyfortotallocation As Double = 0.0
        Dim fatqtyfortotallocation As Double = 0.0
        Dim snfqtyfortotallocation As Double = 0.0
        Dim CurBalOFVL_SNF As Double = 0
        Dim CurBalOFVL_FAT As Double = 0
        Dim totalamt As Double = 0
        Dim amtdifference As Double = 0
        Dim strtransactionLocation As String = strMCC_Code
        Dim IsSubLocation As Boolean = False
        balqtyfortotallocation = dblNet_Qty
        fatqtyfortotallocation = dblFAT_KG
        snfqtyfortotallocation = dblSNF_KG

        Dim AllowtoNegativeStockInventoryAtTankerDispatch As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, trans)) = 0, False, True)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                If clsCommon.myLen(strsublocation) > 0 Then
                    If AllowtoNegativeStockInventoryAtTankerDispatch = False Then
                        balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strItemCode, strMCC_Code, strsublocation, strChalanceNo, dtChalanDate, trans, "KG"))
                    End If
                End If
                If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                    Dim objInventoryMovemnt1 As New clsInventoryMovementNew()
                    Dim ArrInventoryMovement1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                    objInventoryMovemnt1.InOut = "O"
                    If AllowtoNegativeStockInventoryAtTankerDispatch = False Then
                        balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strItemCode, strMCC_Code, strsublocation, strChalanceNo, dtChalanDate, trans, "KG"))
                        If balqtyofvl >= balqtyfortotallocation Then
                            balqtyofvl = balqtyfortotallocation
                            ''richa agarwal added on 09 May,2019 ERO/15/05/19-000601
                        ElseIf balqtyofvl < 0 Then
                            balqtyofvl = 0
                        End If
                    Else
                        balqtyofvl = balqtyfortotallocation
                    End If

                    objInventoryMovemnt1.main_location = strMCC_Code
                    objInventoryMovemnt1.Location_Code = strsublocation
                    objInventoryMovemnt1.Other_Location_Code = strMcc_Or_Plant_Code
                    objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(strMcc_Or_Plant_Code, trans)
                    objInventoryMovemnt1.Item_Code = strItemCode
                    objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
                    objInventoryMovemnt1.Qty = balqtyofvl
                    objInventoryMovemnt1.UOM = strUOMCode
                    objInventoryMovemnt1.MRP = 0
                    objInventoryMovemnt1.Add_Cost = 0
                    If intArrCount > 0 Then
                        Dim dblTempFATPer As Decimal = Math.Round(IIf(dblNet_Qty = 0, 0, dblFAT_KG * 100 / dblNet_Qty), 10, MidpointRounding.ToEven)
                        objInventoryMovemnt1.FAT_Per = Math.Round(dblTempFATPer, 2, MidpointRounding.ToEven)
                        Dim dblTempSNFPer As Decimal = Math.Round(IIf(dblNet_Qty = 0, 0, dblSNF_KG * 100 / dblNet_Qty), 10, MidpointRounding.ToEven)
                        objInventoryMovemnt1.SNF_Per = Math.Round(dblTempSNFPer, 2, MidpointRounding.ToEven)
                        objInventoryMovemnt1.FAT_KG = Math.Round(dblTempFATPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                        objInventoryMovemnt1.SNF_KG = Math.Round(dblTempSNFPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                    Else
                        objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(strChalanceNo, trans)
                        objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(strChalanceNo, trans)
                        ''To save according to FAT AND SNF work 6 Dec,2012
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, trans), "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strsublocation) > 0 Then
                                Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & strItemCode & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtChalanDate, "dd-MMM-yyyy") & "')", trans)
                                If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                                    CurBalOFVL_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                                    CurBalOFVL_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                                End If
                            End If
                            If CurBalOFVL_SNF <> 0 Or CurBalOFVL_FAT <> 0 Then
                                If CurBalOFVL_SNF >= snfqtyfortotallocation Then
                                    CurBalOFVL_SNF = snfqtyfortotallocation
                                End If
                                If CurBalOFVL_FAT >= fatqtyfortotallocation Then
                                    CurBalOFVL_FAT = fatqtyfortotallocation
                                End If
                                objInventoryMovemnt1.FAT_KG = CurBalOFVL_FAT
                                objInventoryMovemnt1.SNF_KG = CurBalOFVL_SNF
                            End If
                        Else
                            objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(strChalanceNo, trans)
                            objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(strChalanceNo, trans)
                            objInventoryMovemnt1.FAT_KG = clsMccDispatch.getFATPer(strChalanceNo, trans) * balqtyofvl / 100
                            objInventoryMovemnt1.SNF_KG = clsMccDispatch.getSNFPer(strChalanceNo, trans) * balqtyofvl / 100
                        End If
                        ''----------------end of To save according to FAT AND SNF work 6 Dec,2012
                    End If
                    objInventoryMovemnt1.Net_Cost = Math.Round((dblFAT_Rate * objInventoryMovemnt1.FAT_KG) + (dblSNF_Rate * objInventoryMovemnt1.SNF_KG), 2)
                    objInventoryMovemnt1.Fat_Rate = dblFAT_Rate
                    objInventoryMovemnt1.SNF_Rate = dblSNF_Rate
                    objInventoryMovemnt1.Fat_Amt = clsCommon.myFormat(dblFAT_Rate * objInventoryMovemnt1.FAT_KG)
                    objInventoryMovemnt1.SNF_Amt = clsCommon.myFormat(dblSNF_Rate * objInventoryMovemnt1.SNF_KG)
                    totalamt += objInventoryMovemnt1.Fat_Amt + objInventoryMovemnt1.SNF_Amt 'done by stuti on 12/05/2017
                    amtdifference = dblAmount - totalamt
                    objInventoryMovemnt1.SNF_Amt += amtdifference
                    objInventoryMovemnt1.ItemType = strItemTypeToSave
                    ''--------------
                    If balqtyofvl = 0 Then
                        objInventoryMovemnt1.Basic_Cost = 0
                    Else
                        objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt1.Net_Cost / balqtyofvl
                    End If
                    ArrInventoryMovement1.Add(objInventoryMovemnt1)
                    If balqtyofvl > 0 Or CurBalOFVL_FAT > 0 Or CurBalOFVL_SNF > 0 Then
                        clsInventoryMovementNew.SaveData("DispChallan", strChalanceNo, dtChalanDate, clsCommon.GetPrintDate(dtChalanDate, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
                    End If
                    balqtyfortotallocation = balqtyfortotallocation - balqtyofvl
                    fatqtyfortotallocation = fatqtyfortotallocation - CurBalOFVL_FAT
                    snfqtyfortotallocation = snfqtyfortotallocation - CurBalOFVL_SNF
                End If
            Next
        End If

        If IsRejectOnly Then
            If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                Throw New Exception("Balance is Not Avalibale at Rejected Locations")
            End If
        Else
            ''-- to insert data into main locations when sub locations has no value
            If balqtyfortotallocation > 0 Or fatqtyfortotallocation > 0 Or snfqtyfortotallocation > 0 Then
                totalamt = 0
                amtdifference = 0
                Dim objInventoryMovemnt1 As New clsInventoryMovementNew()
                Dim ArrInventoryMovement1 As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                objInventoryMovemnt1.InOut = "O"

                If AllowtoNegativeStockInventoryAtTankerDispatch = False Then
                    balqtyofvl = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strItemCode, strMCC_Code, "", strChalanceNo, dtChalanDate, trans, "KG"))

                    If balqtyofvl >= balqtyfortotallocation Then
                        balqtyofvl = balqtyfortotallocation
                    End If
                Else
                    balqtyofvl = balqtyfortotallocation

                End If
                objInventoryMovemnt1.main_location = strMCC_Code

                objInventoryMovemnt1.Location_Code = strMCC_Code
                objInventoryMovemnt1.Other_Location_Code = strMcc_Or_Plant_Code
                objInventoryMovemnt1.Other_Location_Desc = clsLocation.GetName(strMcc_Or_Plant_Code, trans)
                objInventoryMovemnt1.Item_Code = strItemCode
                objInventoryMovemnt1.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
                objInventoryMovemnt1.Qty = balqtyofvl
                objInventoryMovemnt1.UOM = strUOMCode
                objInventoryMovemnt1.MRP = 0
                objInventoryMovemnt1.Add_Cost = 0
                If intArrCount > 0 Then
                    Dim dblTempFATPer As Decimal = Math.Round(IIf(dblNet_Qty = 0, 0, dblFAT_KG * 100 / dblNet_Qty), 10, MidpointRounding.ToEven)
                    objInventoryMovemnt1.FAT_Per = Math.Round(dblTempFATPer, 2, MidpointRounding.ToEven)
                    Dim dblTempSNFPer As Decimal = Math.Round(IIf(dblNet_Qty = 0, 0, dblSNF_KG * 100 / dblNet_Qty), 10, MidpointRounding.ToEven)
                    objInventoryMovemnt1.SNF_Per = Math.Round(dblTempSNFPer, 2, MidpointRounding.ToEven)
                    objInventoryMovemnt1.FAT_KG = Math.Round(dblTempFATPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                    objInventoryMovemnt1.SNF_KG = Math.Round(dblTempSNFPer * balqtyofvl / 100, 2, MidpointRounding.ToEven)
                Else
                    objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(strChalanceNo, trans)
                    objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(strChalanceNo, trans)
                    ''To save according to FAT AND SNF work 6 Dec,2012
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, trans), "1") = CompairStringResult.Equal Then
                        Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & strItemCode & "','" & strMCC_Code & "','" & clsCommon.GetPrintDate(dtChalanDate, "dd-MMM-yyyy") & "')", trans)
                        If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                            CurBalOFVL_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                            CurBalOFVL_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                        End If
                        If CurBalOFVL_SNF <> 0 Or CurBalOFVL_FAT <> 0 Then
                            If CurBalOFVL_SNF >= snfqtyfortotallocation Then
                                CurBalOFVL_SNF = snfqtyfortotallocation
                            End If
                            If CurBalOFVL_FAT >= fatqtyfortotallocation Then
                                CurBalOFVL_FAT = fatqtyfortotallocation
                            End If
                            objInventoryMovemnt1.FAT_KG = CurBalOFVL_FAT
                            objInventoryMovemnt1.SNF_KG = CurBalOFVL_SNF
                        End If
                    Else
                        objInventoryMovemnt1.FAT_Per = clsMccDispatch.getFATPer(strChalanceNo, trans)
                        objInventoryMovemnt1.SNF_Per = clsMccDispatch.getSNFPer(strChalanceNo, trans)
                        objInventoryMovemnt1.FAT_KG = clsMccDispatch.getFATPer(strChalanceNo, trans) * balqtyofvl / 100
                        objInventoryMovemnt1.SNF_KG = clsMccDispatch.getSNFPer(strChalanceNo, trans) * balqtyofvl / 100
                    End If
                    ''----------------end of To save according to FAT AND SNF work 6 Dec,2012
                End If
                objInventoryMovemnt1.Net_Cost = Math.Round((dblFAT_Rate * objInventoryMovemnt1.FAT_KG) + (dblSNF_Rate * objInventoryMovemnt1.SNF_KG), 2)
                objInventoryMovemnt1.Fat_Rate = dblFAT_Rate
                objInventoryMovemnt1.SNF_Rate = dblSNF_Rate
                objInventoryMovemnt1.Fat_Amt = clsCommon.myFormat(dblFAT_Rate * objInventoryMovemnt1.FAT_KG)
                objInventoryMovemnt1.SNF_Amt = clsCommon.myFormat(dblSNF_Rate * objInventoryMovemnt1.SNF_KG)

                totalamt += objInventoryMovemnt1.Fat_Amt + objInventoryMovemnt1.SNF_Amt 'done by stuti on 12/05/2017
                amtdifference = dblAmount - totalamt
                objInventoryMovemnt1.SNF_Amt += amtdifference

                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "FT"
                End If
                objInventoryMovemnt1.ItemType = strItemTypeToSave
                If balqtyofvl = 0 Then
                    objInventoryMovemnt1.Basic_Cost = 0
                Else
                    objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt1.Net_Cost / balqtyofvl
                End If
                ArrInventoryMovement1.Add(objInventoryMovemnt1)
                If balqtyofvl > 0 Or CurBalOFVL_FAT > 0 Or CurBalOFVL_SNF > 0 Then
                    clsInventoryMovementNew.SaveData("DispChallan", strChalanceNo, dtChalanDate, clsCommon.GetPrintDate(dtChalanDate, "dd/MM/yyyy"), ArrInventoryMovement1, trans)
                End If
                balqtyfortotallocation = balqtyfortotallocation - balqtyofvl
                fatqtyfortotallocation = fatqtyfortotallocation - CurBalOFVL_FAT
                snfqtyfortotallocation = snfqtyfortotallocation - CurBalOFVL_SNF
            End If
        End If
    End Sub

    Public Shared Function CreateTransferOutJE(obj As clsMccDispatch, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        Return CreateTransferOutJE(obj, strVoucherNoForRecreateOnly, trans, 0, 0)
    End Function

    Public Shared Function CreateTransferOutJE(obj As clsMccDispatch, strVoucherNoForRecreateOnly As String, trans As SqlTransaction, ByVal TransferInAmount As Decimal, ByVal dclInventoryAmount As Decimal) As Boolean
        Dim rValue As Boolean = False
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        Dim PickTCAForStockTransferAndTankerDispatch As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickTCAForStockTransferAndTankerDispatch, clsFixedParameterCode.PickTCAForStockTransferAndTankerDispatch, trans)) = 0, False, True)
        Try
            Dim ArryLst As ArrayList = New ArrayList()
            Dim Branch_Ac As String = String.Empty
            Dim Inventory_Control_Ac As String = String.Empty
            Dim FromLocSeg As String = obj.MCC_Code
            Dim ToLocSeg As String = obj.Mcc_Or_Plant_Code
            Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
            Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

            'Skip JV ,Setting- DoNotCreateJVOnSameLocationSegmentInTanDisAndMTIn
            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
            Else
                If clsCommon.CompairString(FromLocationSegment, ToLocationSegment) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotCreateJVOnSameLocationSegmentInTanDisAndMTIn, clsFixedParameterCode.DoNotCreateJVOnSameLocationSegmentInTanDisAndMTIn, trans)) = 1 Then
                        Return True
                    End If
                End If
            End If

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchFinancialImpactInTransferIn, clsFixedParameterCode.TankerDispatchFinancialImpactInTransferIn, trans)) = 1 Then
                If TransferInAmount = 0 Then
                    Throw New Exception("Transfer in Amount cannot be zero")
                End If
                TransferInAmount = Math.Round(TransferInAmount, 2, MidpointRounding.ToEven)
                ''richa agarwal 16 Jan,2020
                If PickTCAForStockTransferAndTankerDispatch = True Then
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Item_Code + "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + obj.Item_Code + " (" & obj.Item_Desc & ")")
                    End If
                Else
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                    End If
                End If

                Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                ArryLst.Add(New String() {Branch_Ac, TransferInAmount})


                Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_control_account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                    Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                End If
                Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                    ArryLst.Add(New String() {Inventory_Control_Ac, -1 * dclInventoryAmount})
                Else
                    ArryLst.Add(New String() {Inventory_Control_Ac, -1 * dclInventoryAmount, "", "", "", "", "", "", "I"})

                    ''TEC/14/02/19-000426 by Richa on 14/02/2019
                    clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(obj.Chalan_NO), "DispChallan", clsCommon.myCstr(obj.Item_Code), "", Inventory_Control_Ac, "", trans)
                    ''------------------
                End If
                If TransferInAmount <> dclInventoryAmount Then
                    Dim DiffAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Difference_Account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                    If clsCommon.myLen(DiffAccount) <= 0 Then
                        Throw New Exception("Please Map  Difference Account From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                    End If
                    DiffAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(DiffAccount, FromLocationSegment, True, trans)
                    ArryLst.Add(New String() {DiffAccount, -1 * (TransferInAmount - dclInventoryAmount)})
                End If
            ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferEntryOnInvCtrlAccount, clsFixedParameterCode.TransferEntryOnInvCtrlAccount, trans)) = 1 Then
                If obj.arr Is Nothing OrElse obj.arr.Count <= 0 Then
                    ''richa agarwal 16 Jan,2020
                    If PickTCAForStockTransferAndTankerDispatch = True Then
                        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Item_Code + "'", trans))
                        If clsCommon.myLen(Branch_Ac) <= 0 Then
                            Throw New Exception("Please Map Transfer Clearing Account For  for item " + obj.Item_Code + " (" & obj.Item_Desc & ") ")
                        End If
                    Else
                        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_Ac) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                        End If
                    End If

                    Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                    ArryLst.Add(New String() {Branch_Ac, obj.Amount})

                    Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_control_account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                    If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                        Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                    End If
                    Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        ArryLst.Add(New String() {Inventory_Control_Ac, -1 * obj.Amount})
                    Else
                        ArryLst.Add(New String() {Inventory_Control_Ac, -1 * obj.Amount, "", "", "", "", "", "", "I"})

                        ''TEC/14/02/19-000426 by Richa on 14/02/2019
                        clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(obj.Chalan_NO), "DispChallan", clsCommon.myCstr(obj.Item_Code), "", Inventory_Control_Ac, "", trans)
                        ''------------------
                    End If
                Else
                    For Each objtr As clsMCCDispatchDetail In obj.arr
                        ''richa agarwal 16 Jan,2020
                        If PickTCAForStockTransferAndTankerDispatch = True Then
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objtr.Item_Code + "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Transfer Clearing Account For  for item " + objtr.Item_Code + " (" & objtr.Item_Name & ")")
                            End If
                        Else
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                            End If
                        End If

                        Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                        ArryLst.Add(New String() {Branch_Ac, objtr.Amount})


                        Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_control_account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objtr.Item_Code & "') ", trans))
                        If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                            Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & objtr.Item_Code & " (" & objtr.Item_Name & ")")
                        End If
                        Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)

                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            ArryLst.Add(New String() {Inventory_Control_Ac, -1 * objtr.Amount})
                        Else
                            ArryLst.Add(New String() {Inventory_Control_Ac, -1 * objtr.Amount, "", "", "", "", "", "", "I"})

                            ''TEC/14/02/19-000426 by Richa on 14/02/2019
                            clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(obj.Chalan_NO), "DispChallan", clsCommon.myCstr(objtr.Item_Code), "", Inventory_Control_Ac, "", trans)
                            ''------------------
                        End If
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
                    If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                        CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
                        CostOfItem = clsInventoryMovement.GetCost(CostingMethod, obj.Item_Code, obj.MCC_Code, obj.Net_Qty, obj.Dispatch_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "TSPL_INVENTORY_MOVEMENT_NEW")
                    Else
                        CostOfItem = 0
                    End If  '' Done By Pankaj Jha For Skipping Cogs GL
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

                    ''richa agarwal 16 Jan,2020
                    If PickTCAForStockTransferAndTankerDispatch = True Then
                        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Item_Code + "'", trans))
                        If clsCommon.myLen(Branch_Ac) <= 0 Then
                            Throw New Exception("Please Map Transfer Clearing Account For  for item " + obj.Item_Code + "")
                        End If
                        Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                    Else
                        Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_Ac) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                        End If
                    End If



                    Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
                    If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                        Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
                    End If
                    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                    Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)

                    Dim Amt As Double = obj.Amount

                    ArryLst.Add(New String() {Branch_Ac, Amt})
                    Dim AccStockTransfer() As String = {Stock_Transfer_Ac_FromLoc, Amt * -1, "", "", "", "", "", "", "S"}
                    ArryLst.Add(AccStockTransfer)
                    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt})
                    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt * -1})

                    If CostOfItem > 0 Then
                        ArryLst.Add(New String() {COGT_AC, CostOfItem})
                        Dim AccInventory() As String = {Inventory_Control_Ac_FromLoc, CostOfItem * -1, "", "", "", "", "", "", "S"}
                        ArryLst.Add(AccInventory)
                    End If
                Else
                    For Each objtr As clsMCCDispatchDetail In obj.arr
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


                        ''richa agarwal 16 Jan,2020
                        If PickTCAForStockTransferAndTankerDispatch = True Then
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objtr.Item_Code + "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Transfer Clearing Account For  for item " + objtr.Item_Code + " (" & objtr.Item_Name & ")")
                            End If
                            Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
                        Else
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
                            End If
                        End If

                        Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objtr.Item_Code & "') ", trans))
                        If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                            Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objtr.Item_Code & " (" & objtr.Item_Name & ")")
                        End If
                        Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                        Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                        Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)

                        Dim Amt As Double = objtr.Amount

                        ArryLst.Add(New String() {Branch_Ac, Amt})
                        Dim AccStockTransfer() As String = {Stock_Transfer_Ac_FromLoc, Amt * -1, "", "", "", "", "", "", "S"}
                        ArryLst.Add(AccStockTransfer)
                        ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt})
                        ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt * -1})

                        If CostOfItem > 0 Then
                            ArryLst.Add(New String() {COGT_AC, CostOfItem})
                            Dim AccInventory() As String = {Inventory_Control_Ac_FromLoc, CostOfItem * -1, "", "", "", "", "", "", "S"}
                            ArryLst.Add(AccInventory)
                        End If
                    Next
                End If




            End If
            '===========BM00000008259
            Dim GLDesc As String = "Journal Entry Against MCC Tanker Dispatch - Doc No." & obj.Chalan_NO & " "
            Dim Remarks As String = "Journal Entry against MCC Tanker Dispatch from location -" & obj.MCC_Code & " to location- " & obj.Mcc_Or_Plant_Code & " For Doc No. " & obj.Chalan_NO & ""

            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then ''because if voucher no known then recreate it with same no.
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, strVoucherNoForRecreateOnly, trans, obj.Dispatch_Date, GLDesc, "DI-CH", "Dispatch Challan", obj.Chalan_NO, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Remarks)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_Code, False, trans, obj.Dispatch_Date, GLDesc, "DI-CH", "Dispatch Challan", obj.Chalan_NO, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, obj.Remarks)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
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
    Public Shared Function DeleteData(ByVal challano As String, ByVal tran As SqlTransaction) As Boolean
        Try

            Dim qry As String = "delete from TSPL_Acknowlegement_Entry_Parameter_Detail where  Document_No='" & challano & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "delete from TSPL_ACKNOWLEDGENT_ENTRY_Detail where Document_No='" & challano & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "delete from TSPL_ACKNOWLEDGENT_ENTRY_HEADER where  Document_No='" & challano & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Function

    Public Shared Function JE_Excisable_Common(ByVal obj As clsTransferDCC, ByVal arrlist As ArrayList, ByVal strLocationSegment As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        qry = " select Doc.Document_No,TaxM1.Tax_Liability_Account as Tax1_GLAC,TaxM2.Tax_Liability_Account as Tax2_GLAC," &
                 " TaxM3.Tax_Liability_Account as Tax3_GLAC,TaxM4.Tax_Liability_Account as Tax4_GLAC," &
                 " TaxM5.Tax_Liability_Account as Tax5_GLAC,TaxM6.Tax_Liability_Account as Tax6_GLAC, " &
                 " TaxM7.Tax_Liability_Account as Tax7_GLAC,TaxM8.Tax_Liability_Account as Tax8_GLAC, " &
                 " TaxM9.Tax_Liability_Account as Tax9_GLAC,TaxM10.Tax_Liability_Account as Tax10_GLAC, " &
                 " TaxM1.Tax_Net_Payable as Tax1_GLAC_Payable,TaxM2.Tax_Net_Payable as Tax2_GLAC_Payable, " &
                 " TaxM3.Tax_Net_Payable as Tax3_GLAC_Payable,TaxM4.Tax_Net_Payable as Tax4_GLAC_Payable, " &
                 " TaxM5.Tax_Net_Payable as Tax5_GLAC_Payable,TaxM6.Tax_Net_Payable as Tax6_GLAC_Payable, " &
                 " TaxM7.Tax_Net_Payable as Tax7_GLAC_Payable,TaxM8.Tax_Net_Payable as Tax8_GLAC_Payable, " &
                 " TaxM9.Tax_Net_Payable as Tax9_GLAC_Payable,TaxM10.Tax_Net_Payable as Tax10_GLAC_Payable from TSPL_TRANSFER_ORDER_HEAD doc " &
                 " left join TSPL_TAX_MASTER TaxM1 on Doc.TAX1=TaxM1.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM2 on Doc.TAX2=TaxM2.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM3 on Doc.TAX3=TaxM3.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM4 on Doc.TAX4=TaxM4.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM5 on Doc.TAX5=TaxM5.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM6 on Doc.TAX6=TaxM6.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM7 on Doc.TAX7=TaxM7.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM8 on Doc.TAX8=TaxM8.Tax_Code " &
                 " left join TSPL_TAX_MASTER TaxM9 on Doc.TAX9=TaxM9.Tax_Code " &
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

    Public Shared Function getFATRate(ByVal challanNo As String, ByVal trans As SqlTransaction) As Double
        Dim fatRate As Double = 0
        Dim qry As String = "select FAT_RATE from TSPL_MCC_Dispatch_Challan  where Chalan_No='" & challanNo & "'"
        fatRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return fatRate

    End Function

    Public Shared Function getSNFRate(ByVal challanNo As String, ByVal trans As SqlTransaction) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select SNF_RATE from TSPL_MCC_Dispatch_Challan where Chalan_No='" & challanNo & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function

    Public Shared Function getTransferAmount(ByVal challanNo As String, ByVal trans As SqlTransaction) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select Amount from TSPL_MCC_Dispatch_Challan where Chalan_No='" & challanNo & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function

    Public Shared Function getFATRateChamberWise(ByVal challanNo As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim fatRate As Double = 0
        Dim qry As String = "select FAT_RATE from TSPL_MCC_DISPATCH_CHALLAN_DETAIL  where Chalan_No='" & challanNo & "' and sno='" & line_No & "'"
        fatRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return fatRate

    End Function

    Public Shared Function getSNFRateChamberWise(ByVal challanNo As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select SNF_RATE from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & challanNo & "' and sno='" & line_No & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function

    Public Shared Function getTransferAmountChamberwise(ByVal challanNo As String, ByVal trans As SqlTransaction, ByVal line_No As Integer) As Double
        Dim snfRate As Double = 0
        Dim qry As String = "select Amount from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & challanNo & "' and sno='" & line_No & "'"
        snfRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return snfRate
    End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsMccDispatch, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            If obj IsNot Nothing And clsCommon.myLen(strDocNo) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                If obj.EWayBillDate Is Nothing Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy hh:mm tt"), True)
                End If
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_CHALLAN", OMInsertOrUpdate.Update, "TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO='" + strDocNo + "'", trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    ''richa agarwal for intermedent tanker save data automatically from Gate Entry to Milk Transfer In 
    Public Shared Function SaveAndPostFromGateEntryToMilkTransferIn(ByVal DocumentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Try
            isSaved = clsAcknowledgementEntry.SaveAndPostGateEntryData(DocumentNo, trans)
            Dim strGateEntryNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_No from Tspl_Gate_Entry_Details where AcknowEntryDocument_No='" & DocumentNo & "'", trans))
            isSaved = clsAcknowledgementEntry.SaveAndPostWeighmentData(strGateEntryNo, trans)
            Dim strWeighmentEntryNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No from tspl_weighment_detail where AcknowEntryDocument_No='" & DocumentNo & "'", trans))
            isSaved = clsAcknowledgementEntry.SaveAndPostQualityCheckData(strWeighmentEntryNo, trans)
            isSaved = clsQualityCheck.SaveAndPostUnloadingData(strGateEntryNo, trans)

            Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
            Dim FirstGateOutProcessForMCCBulkProcument As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FirstGateOutProcessForMCCBulkProcument, clsFixedParameterCode.FirstGateOutProcessForMCCBulkProcument, trans))
            If MCCChamberwise = 1 AndAlso FirstGateOutProcessForMCCBulkProcument = 1 Then
            Else
                isSaved = clsQualityCheck.SaveGateOutData(strGateEntryNo, trans)
            End If

            isSaved = clsQualityCheck.SaveAndPostTransferInData(strGateEntryNo, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveAndPostGateEntryData(ByVal TankerDispatchNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim objGt As clsGateEntry = New clsGateEntry()
            Dim objTr As clsGateEntryChemberNoDetails = New clsGateEntryChemberNoDetails()
            Dim obj As clsAcknowledgementEntry = clsAcknowledgementEntry.getData(TankerDispatchNo, NavigatorType.Current, trans)

            ''to save data into gate entry header table
            objGt.Doc_Type = "MccProc"
            objGt.IsAgainstJobWork = 0
            objGt.Date_And_Time = clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt")
            objGt.Tanker_No = clsCommon.myCstr(obj.Tanker_No)
            objGt.Dispatched_From_Mcc = clsCommon.myCstr(obj.MCC_Code)
            'objGt.MIKL_TYPE_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TOP 1 case when type ='MCCDefaultMilkItem' then 'MIX' when type ='MCC Default Milk Item Cow' then 'CM' when type ='MCC Default Milk Item Buffalo' then 'BM' END from tspl_fixed_parameter where code='MilkSetting' and Description ='" & obj.Item_Code & "' ORDER BY TYPE DESC", trans))
            'If clsCommon.myLen(objGt.MIKL_TYPE_CODE) <= 0 Then
            '    Throw New Exception("Please map Item with default milk item for mcc in fixed parameter.")
            'End If
            objGt.Challan_No = clsCommon.myCstr(obj.Dispatch_Document_No)
            objGt.Challan_Date = clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy")
            objGt.AcknowEntryDocument_No = clsCommon.myCstr(obj.Document_No)
            'objGt.Item_Code = clsCommon.myCstr(obj.Item_Code)
            'objGt.Item_Desc = clsCommon.myCstr(obj.Item_Desc)
            'objGt.UOM = clsCommon.myCstr(obj.UOM_Code)
            'objGt.Qty_In_Kg = clsCommon.myCdbl(obj.Net_Qty)
            'objGt.fat_per = clsCommon.myCdbl(obj.FatPer)
            'objGt.snf_Per = clsCommon.myCdbl(obj.SNFPer)
            objGt.location_Code = clsCommon.myCstr(obj.Mcc_Or_Plant_Code)
            objGt.Location_Desc = clsCommon.myCstr(clsLocation.GetName(obj.Mcc_Or_Plant_Code, trans))
            objGt.isNewEntry = True
            objGt.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, objGt.Date_And_Time, clsDocType.GateEntry, clsDocTransactionType.BulkProcPurchase, objGt.location_Code)
            ''for detail table of gate entry chamber detail
            objGt.Arr = New List(Of clsGateEntryChemberNoDetails)
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                For Each objTrDD As clsAcknowledgementEntryDetail In obj.arr
                    objTr = New clsGateEntryChemberNoDetails()

                    objTr.Line_No = objTrDD.SNo
                    objTr.Item_Code = objTrDD.Item_Code
                    objTr.UOM = objTrDD.Item_UOM
                    objTr.Chamber_Desc = objTrDD.Chamber_Description
                    objTr.Chamber_Qty = objTrDD.Qty_KG
                    objTr.snf_Per = clsCommon.myCdbl(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_Acknowlegement_Entry_Parameter_Detail where param_type='SNF' and Document_No='" & obj.Document_No & "' and SNO='" & objTrDD.SNo & "'", trans)))
                    objTr.fat_per = clsCommon.myCdbl(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_Acknowlegement_Entry_Parameter_Detail where param_type='FAT' and Document_No='" & obj.Document_No & "' and SNO='" & objTrDD.SNo & "'", trans)))
                    objTr.MIKL_TYPE_CODE = objGt.MIKL_TYPE_CODE
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        objGt.Arr.Add(objTr)
                    End If
                    objGt.MIKL_TYPE_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TOP 1 case when type ='MCCDefaultMilkItem' then 'MIX' when type ='MCC Default Milk Item Cow' then 'CM' when type ='MCC Default Milk Item Buffalo' then 'BM' END from tspl_fixed_parameter where code='MilkSetting' and Description ='" & objTr.Item_Code & "' ORDER BY TYPE DESC", trans))
                    If clsCommon.myLen(objGt.MIKL_TYPE_CODE) <= 0 Then
                        Throw New Exception("Please map Item with default milk item for mcc in fixed parameter.")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(objTrDD.SNo), "1") = CompairStringResult.Equal Then
                        objGt.Item_Code = clsCommon.myCstr(objTr.Item_Code)
                        objGt.Item_Desc = clsItemMaster.GetItemName(clsCommon.myCstr(objTr.Item_Code), trans)
                        objGt.UOM = clsCommon.myCstr(objTr.UOM)
                        objGt.Qty_In_Kg = clsCommon.myCdbl(objTr.Chamber_Qty)
                        objGt.fat_per = clsCommon.myCdbl(objTr.fat_per)
                        objGt.snf_Per = clsCommon.myCdbl(objTr.snf_Per)
                    End If


                Next
            End If

            clsGateEntry.saveData(objGt, trans)
            clsGateEntry.postData(objGt.Gate_Entry_No, "MccProc", "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveAndPostWeighmentData(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsWeighment = New clsWeighment()
            Dim objTr As clsWeighmentChemberNoDetails = New clsWeighmentChemberNoDetails()
            Dim objGt As clsGateEntry = clsGateEntry.getData(GateEntryNo, NavigatorType.Current, trans)

            ''to save data into weighment header table
            obj.Doc_Type = "MccProc"
            obj.IsAgainstJobWork = 0
            obj.Weighment_Date = clsCommon.GetPrintDate(objGt.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(objGt.Tanker_No)
            obj.Date_And_Time = clsCommon.GetPrintDate(objGt.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Dispatched_From_Mcc = clsCommon.myCstr(objGt.Dispatched_From_Mcc)
            obj.Challan_No = clsCommon.myCstr(objGt.Challan_No)
            obj.AcknowEntryDocument_No = clsCommon.myCstr(objGt.AcknowEntryDocument_No)
            obj.Challan_Date = clsCommon.GetPrintDate(objGt.Challan_Date, "dd/MMM/yyyy")
            obj.Item_Code = clsCommon.myCstr(objGt.Item_Code)
            obj.Item_Desc = clsCommon.myCstr(objGt.Item_Desc)
            obj.UOM = clsCommon.myCstr(objGt.UOM)
            obj.Qty_In_Kg = clsCommon.myCdbl(objGt.Qty_In_Kg)
            obj.fat_per = clsCommon.myCdbl(objGt.fat_per)
            obj.snf_Per = clsCommon.myCdbl(objGt.snf_Per)
            obj.location_Code = clsCommon.myCstr(objGt.location_Code)
            obj.Location_Desc = clsCommon.myCstr(objGt.Location_Desc)
            obj.Gate_Entry_No = objGt.Gate_Entry_No
            obj.isNewEntry = True
            obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.Weighment, clsDocTransactionType.BulkProcPurchase, obj.location_Code)
            obj.Arr = New List(Of clsWeighmentChemberNoDetails)
            ''for detail table of weighment chamber detail

            If objGt.Arr IsNot Nothing AndAlso objGt.Arr.Count > 0 Then
                For Each objTrDD As clsGateEntryChemberNoDetails In objGt.Arr
                    objTr = New clsWeighmentChemberNoDetails()

                    objTr.Line_No = objTrDD.Line_No
                    objTr.Item_Code = objTrDD.Item_Code
                    objTr.UOM = objTrDD.UOM
                    objTr.Chamber_Desc = objTrDD.Chamber_Desc
                    objTr.Chamber_Qty = objTrDD.Chamber_Qty
                    objTr.snf_Per = objTrDD.snf_Per
                    objTr.fat_per = objTrDD.fat_per
                    objTr.Gross_Weight = objTrDD.Chamber_Qty
                    objTr.Tare_Weight = 0
                    objTr.Net_Weight = objTrDD.Chamber_Qty
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
            End If

            clsWeighment.saveData(obj, trans)
            clsWeighment.postData(obj.Weighment_No, "MccProc", "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveAndPostQualityCheckData(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = False
        Try
            Dim obj As clsQualityCheck = New clsQualityCheck()
            Dim objTr As clsQualityChemberNoDetails = New clsQualityChemberNoDetails()
            Dim objTrP As clsQcParam = New clsQcParam()
            Dim objWE As clsWeighment = clsWeighment.getData(GateEntryNo, NavigatorType.Current, False, trans)

            ''to save data into Quality Check  header table
            obj.Doc_Type = "MccProc"
            obj.IsAgainstJobWork = 0
            obj.QC_In_Date_Time = clsCommon.GetPrintDate(objWE.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt")
            obj.QC_Out_Date_Time = clsCommon.GetPrintDate(objWE.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(objWE.Tanker_No)
            obj.Gate_Entry_No = clsCommon.myCstr(objWE.Gate_Entry_No)
            obj.AcknowEntryDocument_No = clsCommon.myCstr(objWE.AcknowEntryDocument_No)
            obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(objWE.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Challan_No = clsCommon.myCstr(objWE.Challan_No)
            obj.Challan_Date = clsCommon.GetPrintDate(objWE.Challan_Date, "dd/MMM/yyyy")
            obj.Dispatched_From_Mcc_Code = clsCommon.myCstr(objWE.Dispatched_From_Mcc)
            obj.Dispatched_From_Mcc_Desc = clsCommon.myCstr(clsLocation.GetName(objWE.Dispatched_From_Mcc, trans))
            obj.location_Code = clsCommon.myCstr(objWE.location_Code)
            obj.Location_Desc = clsCommon.myCstr(objWE.Location_Desc)
            obj.Item_Code = clsCommon.myCstr(objWE.Item_Code)
            obj.Item_Desc = clsCommon.myCstr(objWE.Item_Desc)
            obj.UOM = clsCommon.myCstr(objWE.UOM)
            obj.Qty_In_Kg = clsCommon.myCdbl(objWE.Qty_In_Kg)
            obj.fat_per = clsCommon.myCdbl(objWE.fat_per)
            obj.snf_Per = clsCommon.myCdbl(objWE.snf_Per)
            obj.Weighment_No = objWE.Weighment_No
            obj.Weighment_Date = clsCommon.GetPrintDate(objWE.Weighment_Date, "dd/MMM/yyyy")
            obj.isNewEntry = True
            obj.QC_No = clsERPFuncationality.GetNextCode(trans, obj.QC_In_Date_Time, clsDocType.QualityCheck, clsDocTransactionType.BulkProcPurchase, obj.location_Code)
            obj.Arr = New List(Of clsQualityChemberNoDetails)

            ''for detail table of Quality Check chamber detail

            If objWE.Arr IsNot Nothing AndAlso objWE.Arr.Count > 0 Then
                For Each objTrDD As clsWeighmentChemberNoDetails In objWE.Arr
                    objTr = New clsQualityChemberNoDetails()
                    objTr.QC_No = obj.QC_No
                    objTr.Line_No = objTrDD.Line_No
                    objTr.Item_Code = objTrDD.Item_Code
                    objTr.UOM = objTrDD.UOM
                    objTr.Chamber_Desc = objTrDD.Chamber_Desc
                    objTr.Chamber_Qty = objTrDD.Chamber_Qty
                    objTr.snf_Per = objTrDD.snf_Per
                    objTr.fat_per = objTrDD.fat_per
                    objTr.MIKL_TYPE_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TOP 1 case when type ='MCCDefaultMilkItem' then 'MIX' when type ='MCC Default Milk Item Cow' then 'CM' when type ='MCC Default Milk Item Buffalo' then 'BM' END from tspl_fixed_parameter where code='MilkSetting' and Description ='" & obj.Item_Code & "' ORDER BY TYPE DESC", trans))

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
            End If

            '' FOR pARAMETER DETAIL TABLE OF QC

            obj.arrQcParam = New List(Of clsQcParam)

            ''for detail table of Quality Check chamber detail VIJ/31/08/21-001308
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Acknowlegement_Entry_Parameter_Detail where Param_Type in ('FAT','SNF','CF') and Document_No ='" & objWE.AcknowEntryDocument_No & "' order by SNO ", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    objTrP = New clsQcParam()
                    objTrP.QC_No = obj.QC_No
                    objTrP.Param_Field_Code = clsCommon.myCstr(dr("Param_Field_Code"))
                    objTrP.Param_Field_Desc = clsCommon.myCstr(dr("Param_Field_Desc"))
                    objTrP.Param_Field_Value = clsCommon.myCstr(dr("Param_Field_Value"))
                    objTrP.Param_Type = clsCommon.myCstr(dr("Param_Type"))
                    objTrP.LINE_NO = clsCommon.myCstr(dr("SNO"))
                    obj.arrQcParam.Add(objTrP)
                Next
                ''TO CALCULATE ONLY CLR
                Dim dtCount As DataTable = clsDBFuncationality.GetDataTable("select distinct SNO from TSPL_Acknowlegement_Entry_Parameter_Detail where Param_Type in ('FAT','SNF','CF') and Document_No ='" & objWE.AcknowEntryDocument_No & "' order by SNO ", trans)
                If dtCount IsNot Nothing AndAlso dtCount.Rows.Count > 0 Then
                    For Each dr As DataRow In dtCount.Rows
                        objTrP = New clsQcParam()
                        Dim snf As Double = 0
                        Dim fat As Double = 0
                        Dim cf As Double = 0
                        Dim CLR As Double = 0
                        fat = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_Acknowlegement_Entry_Parameter_Detail where Param_Type='FAT' and Document_No ='" & objWE.AcknowEntryDocument_No & "' and SNO ='" & clsCommon.myCstr(dr("SNO")) & "'", trans))
                        snf = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_Acknowlegement_Entry_Parameter_Detail where Param_Type='SNF' and Document_No ='" & objWE.AcknowEntryDocument_No & "' and SNO ='" & clsCommon.myCstr(dr("SNO")) & "'", trans))
                        cf = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, trans))
                        CLR = clsEkoPro.getClrOnCalculation(fat, snf, cf)
                        objTrP.QC_No = obj.QC_No
                        objTrP.Param_Field_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_PARAMETER_MASTER where type='CLR'", trans))
                        objTrP.Param_Field_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_PARAMETER_MASTER where type='CLR'", trans))
                        objTrP.Param_Field_Value = Math.Round(CLR, 2)
                        objTrP.Param_Type = "CLR"
                        objTrP.LINE_NO = clsCommon.myCstr(dr("SNO"))
                        obj.arrQcParam.Add(objTrP)
                    Next
                End If


            End If

            isSaved = clsQualityCheck.saveData(obj, trans)
            isSaved = clsQualityCheck.postData(obj.QC_No, "MccProc", "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class

Public Class Acknowledment_Entry_Chalan_Parameter
#Region "Variables"
    Public SNO As Integer
    Public Document_No As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
#End Region



    Public Shared Function SaveData(ByVal strChallanNo As String, ByVal arr As List(Of Acknowledment_Entry_Chalan_Parameter), ByVal tran As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Acknowlegement_Entry_Parameter_Detail where  Document_No='" & strChallanNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SNo", arr.Item(i).SNO)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", arr.Item(i).Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", arr.Item(i).Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", arr.Item(i).Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strChallanNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", arr.Item(i).Param_Type)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Acknowlegement_Entry_Parameter_Detail", OMInsertOrUpdate.Insert, "", tran)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal chalanNo As String) As List(Of Acknowledment_Entry_Chalan_Parameter)
        Return getData(chalanNo, Nothing)
    End Function

    Public Shared Function getData(ByVal chalanNo As String, ByVal trans As SqlTransaction) As List(Of Acknowledment_Entry_Chalan_Parameter)
        Dim arr As New List(Of Acknowledment_Entry_Chalan_Parameter)
        Try
            Dim obj As New Acknowledment_Entry_Chalan_Parameter
            Dim q As String = "select * from TSPL_Acknowlegement_Entry_Parameter_Detail where Document_No='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New Acknowledment_Entry_Chalan_Parameter
                    obj.SNO = clsCommon.myCstr(dtbl.Rows(i)("SNO"))
                    obj.Document_No = clsCommon.myCstr(dtbl.Rows(i)("Document_No"))
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



Public Class clsAcknowledgementEntryDetail
#Region "Variables"
    Public SNo As Integer
    Public Document_No As String
    Public Chamber_No As Integer = 0
    Public Chamber_Description As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Item_UOM As String = Nothing
    Public Qty_KG As Double
    Public FAT_KG As Double
    Public SNF_KG As Double
    Public FAT_Rate As Double
    Public SNF_Rate As Double
    Public Amount As Double
    Public Intermittent_Dispatch_No As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal arr As List(Of clsAcknowledgementEntryDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsAcknowledgementEntryDetail In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
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

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACKNOWLEDGENT_ENTRY_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strChalanNo As String, ByVal trans As SqlTransaction) As List(Of clsAcknowledgementEntryDetail)
        Dim arr As List(Of clsAcknowledgementEntryDetail) = Nothing
        Try
            Dim qry As String = "select TSPL_ACKNOWLEDGENT_ENTRY_Detail.*,TSPL_ITEM_MASTER.Item_Desc FROM  TSPL_ACKNOWLEDGENT_ENTRY_Detail left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_ACKNOWLEDGENT_ENTRY_Detail.item_Code where Document_No='" + strChalanNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsAcknowledgementEntryDetail)
                For Each dr As DataRow In dt.Rows
                    Dim obj As New clsAcknowledgementEntryDetail
                    obj.SNo = clsCommon.myCdbl(dr("SNo"))
                    obj.Document_No = clsCommon.myCstr(dr("Document_No"))
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




Imports common
Imports System.Data.SqlClient

Public Class clsfrmVLCMaster

#Region "Variables"
    Public VLC_CODE_VLC_UPLOADER As String = String.Empty
    Public vlcCode As String = Nothing
    Public vlcName As String = Nothing
    Public vehical As String = Nothing
    Public vspCode As String = Nothing
    Public VspName As String = Nothing
    Public MCCCOde As String = Nothing
    Public villagecode As String = Nothing
    Public villagename As String = Nothing
    Public routecode As String = Nothing
    Public routename As String = Nothing
    Public Active As Integer = 1              '' added by Panch Raj against ticket no: BM00000004032
    Public arr As List(Of clsfrmVLCMaster) = Nothing
    Public mainvillcode As String = Nothing
    Public mainvillname As String = Nothing
    Public Form_ID As String = ""
    Public Price_Code As String = String.Empty
    Public Milk_Receive_UOM As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Created_Date As Date?
    Public Apply_Price_Chart_Uploader As Boolean = False
    Public Short_Description As String = Nothing
    Public Auto_Fill_MP_Order As Integer
    Public Apply_Cow_Price As Boolean = False
    Public IsSuspense As Boolean = False
    Public ApplyCowPriceDate As Date?
    Public Loyalty_Rate As Decimal
    Public TFOwnBMC As Boolean = False
    Public OwnBMCDate As Date?
    Public OwnBMC As String = Nothing
    Public HeadLoad As Boolean = False
    Public HeadLoadRate As Decimal
    Public HeadLoadBasis As String = Nothing


#End Region

    Public Shared Function getVLcNameOnVLcCodeForVLCUplader(ByVal VLcCode As String, ByVal MccCode As String, ByVal trans As SqlTransaction) As String
        Dim VLcName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name  from TSPL_VLC_MASTER_HEAD inner join tspl_Mcc_Master on mcc=Mcc_Code  and mcc_Code_vlc_Uploader='" & MccCode & "' where VLc_Code_vlc_uploader='" & VLcCode & "'", trans))
        Return VLcName
    End Function

    Public Shared Function getVLcCodeForVLCUplader(ByVal VLcCode As String, ByVal MccCode As String, ByVal trans As SqlTransaction) As String
        Dim _VLcCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD inner join tspl_Mcc_Master on mcc=Mcc_Code  and mcc_Code_vlc_Uploader='" & MccCode & "' where VLc_Code_vlc_uploader='" & VLcCode & "'", trans))
        Return _VLcCode
    End Function

    Public Shared Function getVLCCodeForVLCUploaderCode(ByVal VLcUploaderCode As String, ByVal MccCode As String, ByVal trans As SqlTransaction) As String
        Dim _VLcCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD inner join tspl_Mcc_Master on tspl_Mcc_Master.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC where  VLc_Code_vlc_uploader='" & VLcUploaderCode & "' and TSPL_VLC_MASTER_HEAD.MCC='" & MccCode & "' ", trans))
        Return _VLcCode
    End Function

    Public Shared Function getMccCodeOnMccCodeForVLCUplader(ByVal MccCode As String, ByVal trans As SqlTransaction) As String
        Dim VLcName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Code  from TSPL_MCC_MASTER where Mcc_Code_vlc_uploader='" & MccCode & "'", trans))
        Return VLcName
    End Function

    Public Shared Function getVLcNameOnVLcCode(ByVal VLcCode As String, ByVal trans As SqlTransaction) As String
        Dim VLcName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name  from TSPL_VLC_MASTER_HEAD  where VLc_Code='" & VLcCode & "'", trans))
        Return VLcName
    End Function

    Public Shared Function CowPriceApplied(ByVal VLcCode As String, ByVal TransDate As Date, ByVal trans As SqlTransaction) As Boolean
        Dim Ret As Boolean = False
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_VLC_MASTER_HEAD  where VLc_Code='" & VLcCode & "' and Apply_Cow_Price=1 and ApplyCowPriceDate<='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' ", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Ret = True
        End If
        Return Ret
    End Function

    Public Shared Function IsOwnBMC(ByVal VLcCode As String, ByVal strMCCCode As String, ByVal trans As SqlTransaction) As Boolean
        Return (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isOwnBMC from TSPL_VLC_MASTER_HEAD  where VLC_CODE='" & VLcCode & "' and MCC='" + strMCCCode + "'", trans)) = 1)
    End Function

    Public Shared Function IsOwnBMCByVSPCode(ByVal VSPCode As String, ByVal trans As SqlTransaction) As Boolean
        Return (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isOwnBMC from TSPL_VLC_MASTER_HEAD  where VSP_Code='" & VSPCode & "' ", trans)) = 1)
    End Function

    Public Shared Function OwnBMCCode(ByVal VSPCode As String, ByVal trans As SqlTransaction) As String
        Dim strOwnBMCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCCOwnBMC  from TSPL_VLC_MASTER_HEAD  where VSP_Code='" & VSPCode & "'", trans))
        Return strOwnBMCCode
    End Function

    'Public Shared Function getRouteNoOnVLcCodeForVLCUplader(ByVal VLcCode As String, ByVal trans As SqlTransaction) As String
    '    Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_code  from TSPL_VLC_MASTER_HEAD  where VLc_Code_vlc_uploader='" & VLcCode & "'", trans))
    '    Return RouteNo
    'End Function

    Public Shared Function getRouteNoOnVLcCodeForVLCUplader(ByVal VLcCode As String, ByVal MccCode As String, ByVal trans As SqlTransaction) As String
        Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_code  from TSPL_VLC_MASTER_HEAD inner join tspl_Mcc_Master on mcc=Mcc_Code and mcc_Code_vlc_Uploader='" & MccCode & "' where VLc_Code_vlc_uploader='" & VLcCode & "'", trans))
        Return RouteNo
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal obj As clsfrmVLCMaster, ByVal arr As List(Of clsfrmVLCMaster), ByVal trans As SqlTransaction) As Boolean
        Dim IsTransactionExist As Boolean = IIf(trans Is Nothing, False, True)

        If IsTransactionExist = False Then
            trans = clsDBFuncationality.GetTransactin()
        End If
        Try
            Dim isSaved As Boolean = True

            If obj.IsSuspense = True Then
                isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_VLC_MASTER_HEAD SET IsSuspense=0 where IsSuspense=1", trans)
            End If

            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.vlcCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                obj.vlcCode = obj.MCCCOde + "/" + obj.vlcCode
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                    obj.VLC_CODE_VLC_UPLOADER = GetCodeNumPart(obj.vlcCode)
                End If
            End If

            strCode = obj.vlcCode
            '----------insert head block--------------------------------------------------

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "vlc_code", obj.vlcCode)
            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER))
            clsCommon.AddColumnsForChange(coll, "vlc_name", obj.vlcName)
            'clsCommon.AddColumnsForChange(coll, "vehical_name", obj.vehical)
            clsCommon.AddColumnsForChange(coll, "Village_Code", obj.mainvillcode)
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.routecode)
            clsCommon.AddColumnsForChange(coll, "VSP_Code", obj.vspCode)
            clsCommon.AddColumnsForChange(coll, "MCC", obj.MCCCOde)
            clsCommon.AddColumnsForChange(coll, "Active", obj.Active)
            clsCommon.AddColumnsForChange(coll, "Auto_Fill_MP_Order", obj.Auto_Fill_MP_Order)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Short_Description", obj.Short_Description)
            clsCommon.AddColumnsForChange(coll, "Apply_Price_Chart_Uploader", IIf(obj.Apply_Price_Chart_Uploader, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Apply_Cow_Price", IIf(obj.Apply_Cow_Price, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IsSuspense", IIf(obj.IsSuspense, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Milk_Receive_UOM", obj.Milk_Receive_UOM, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
            If obj.ApplyCowPriceDate IsNot Nothing AndAlso obj.Apply_Cow_Price Then
                clsCommon.AddColumnsForChange(coll, "ApplyCowPriceDate", clsCommon.GetPrintDate(obj.ApplyCowPriceDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplyCowPriceDate", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Loyalty_Rate", obj.Loyalty_Rate, True)
            If obj.TFOwnBMC = True Then
                clsCommon.AddColumnsForChange(coll, "isOwnBMC", IIf(obj.TFOwnBMC, 1, 0))
                If obj.OwnBMCDate IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "OwnBMCDate", clsCommon.GetPrintDate(obj.OwnBMCDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "OwnBMCDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "MCCOwnBMC", obj.OwnBMC)
            Else
                clsCommon.AddColumnsForChange(coll, "OwnBMCDate", Nothing, True)
            End If

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + obj.vlcCode + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.vlcCode), "TSPL_VLC_MASTER_HEAD", "vlc_code", trans)
            'isSaved = isSaved AndAlso SaveVLCPriceCode(obj.vlcCode, obj.vspCode, obj.MCCCOde, trans)
            SaveVLCPriceCode(obj.vlcCode, obj.vspCode, obj.MCCCOde, trans)
            '----------insert detail block--------------------------------------------------
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.vlcCode, obj.arrCustomFields, trans)

            coll = New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsfrmVLCMaster In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "vlc_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "village_code", objtr.villagecode)
                    'clsCommon.AddColumnsForChange(coll, "route_code", objtr.routecode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    Dim qry As String = "delete from TSPL_VLC_MASTER_DETAIL where vlc_code='" + obj.vlcCode + "' and village_code='" + obj.villagecode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.vlcCode), "TSPL_VLC_MASTER_DETAIL", "vlc_code", trans)
                    ' isSaved = isSaved AndAlso clsfrmVLCMaster.SaveVLCPriceCode(strCode, obj.vspCode, obj.MCCCOde, trans)
                Next
            End If
            If IsTransactionExist = False Then
                trans.Commit()
            End If
            Return True
        Catch ex As Exception
            If IsTransactionExist = False Then
                trans.Rollback()
            End If
            If clsCommon.myCstr(ex.Message).Contains("uk_VLC_Code_VLC_Uploader") Then
                Throw New Exception("Duplicate DCS Code for DCS Uploader ")
                Return False
            Else
                Throw New Exception(ex.Message)
                Return False
            End If
            'Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Sub SaveVLCPriceCode(ByVal vlc_code As String, ByVal Vsp_Code As String, ByVal Mcc_Code As String, ByVal trans As SqlTransaction)
        Dim squery As String = "select * from TSPL_FAT_SNF_UPLOADER_vlc where vlc_code='" & vlc_code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(squery, trans)
        If dt.Rows.Count <= 0 Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyLatestPriceChartWhilecreatingNewVSP, clsFixedParameterCode.ApplyLatestPriceChartWhilecreatingNewVSP, trans)) > 0 Then
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    Dim dtt As DataTable = clsMilkReceiptMCC.GetMilkType()
                    For Each dr As DataRow In dtt.Rows
                        squery = "insert into TSPL_FAT_SNF_UPLOADER_vlc(Code,VLC_Code,Added_By,Added_On) select *,'" & vlc_code & "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' from (select * from (select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join " _
                            & " TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & Mcc_Code & "' and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code " _
                            & " where TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='" & clsCommon.myCstr(dr("Code")) & "' and convert(date,date,103)<= convert(date,'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd-MMM-yyyy"), "dd-MMM-yyyy") & "',103) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) tt union select * from " _
                            & " (select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & Mcc_Code & "' " _
                            & " and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code  where TSPL_FAT_SNF_UPLOADER_MASTER.Dock_Collection_Milk_Type='" & clsCommon.myCstr(dr("Code")) & "' and convert(date,date,103)> convert(date,'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd-MMM-yyyy"), "dd-MMM-yyyy") & "',103) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) tt1) tt"

                        clsDBFuncationality.ExecuteNonQuery(squery, trans)
                    Next
                Else
                    squery = "insert into TSPL_FAT_SNF_UPLOADER_vlc(Code,VLC_Code,Added_By,Added_On) select *,'" & vlc_code & "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' from (select * from (select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join " _
                       & " TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & Mcc_Code & "' and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code " _
                       & " where  convert(date,date,103)<= convert(date,'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd-MMM-yyyy"), "dd-MMM-yyyy") & "',103) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) tt union select * from " _
                       & " (select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & Mcc_Code & "' " _
                       & " and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code  where  convert(date,date,103)> convert(date,'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd-MMM-yyyy"), "dd-MMM-yyyy") & "',103) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) tt1) tt"
                    clsDBFuncationality.ExecuteNonQuery(squery, trans)
                End If
            End If
        End If

        squery = "select * from TSPL_vendor_master where vendor_code='" & Vsp_Code & "' and PC_CODE is not null"
        dt = clsDBFuncationality.GetDataTable(squery, trans)
        If dt.Rows.Count <= 0 Then
            If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Cycle from tspl_mcc_Master where mcc_code='" & Mcc_Code & "'", trans))) <= 0 Then
                Throw New Exception("Please map payment Cycle Code for MCC:" + Mcc_Code)
            End If
            squery = "update tspl_vendor_master set PC_CODE=(select Payment_Cycle from tspl_mcc_Master where mcc_code='" & Mcc_Code & "') where vendor_code='" & Vsp_Code & "' and form_type='VSP'"
            clsDBFuncationality.ExecuteNonQuery(squery, trans)
        End If
    End Sub
    Public Shared Function getPriceCode(ByVal vlc_code As String, ByVal mcc_code As String, ByVal isButtonClick As Boolean, ByVal trans As SqlTransaction)
        Dim price_code As String
        Dim squery As String = "select * from (select top 1 TSPL_FAT_SNF_UPLOADER_MASTER.code from TSPL_FAT_SNF_UPLOADER_MASTER inner join " _
                    & " TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & mcc_code & "' and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code "
        If Not isButtonClick Then
            squery &= "  inner join TSPL_FAT_SNF_UPLOADER_VLC on TSPL_FAT_SNF_UPLOADER_VLC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.Code and VLC_Code='" & vlc_code & "' "
        End If
        squery &= " where  convert(date,date,103)<= convert(date,'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd-MMM-yyyy"), "dd-MMM-yyyy") & "',103) order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) tt"
        If isButtonClick Then
            price_code = clsCommon.ShowSelectForm("Pr_chart", squery, "code", "", "code")
        Else
            price_code = clsDBFuncationality.getSingleValue(squery)
        End If
        Return price_code
    End Function
    Public Shared Function getPriceCodeforVlc(ByVal vlc_code As String, ByVal mcc_code As String, ByVal isButtonClick As Boolean, ByVal trans As SqlTransaction)
        Dim price_code As String
        Dim squery As String = "select distinct TSPL_FAT_SNF_UPLOADER_MASTER.code,TSPL_FAT_SNF_UPLOADER_MASTER.Date ,TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code as [Price Chart],TSPL_MILK_PRICE_MASTER.Description   " _
                    & " from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & mcc_code & "' and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code " _
        & " Inner Join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code"
        If Not isButtonClick Then
            squery &= "  inner join TSPL_FAT_SNF_UPLOADER_VLC on TSPL_FAT_SNF_UPLOADER_VLC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.Code and VLC_Code='" & vlc_code & "' "
        End If
        If isButtonClick Then
            price_code = clsCommon.ShowSelectForm("Pr_chart", squery, "code", " convert(date,TSPL_FAT_SNF_UPLOADER_MASTER.date,103)<= convert(date,'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd-MMM-yyyy"), "dd-MMM-yyyy") & "',103) and Isnull(TSPL_FAT_SNF_UPLOADER_MASTER.Is_InActive,0) =0 ", "code")
        Else
            price_code = clsDBFuncationality.getSingleValue(squery)
        End If
        Return price_code
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsfrmVLCMaster
        Return GetData(strCode, arrLoc, NavType, False)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal isUploaderCode As Boolean) As clsfrmVLCMaster
        Try
            Dim obj As clsfrmVLCMaster = Nothing
            Dim whrcls As String = ""
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " and TSPL_VLC_MASTER_HEAD.mcc in (" + arrLoc + ")"
            End If

            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.IsSuspense,TSPL_VLC_MASTER_HEAD.Apply_Cow_Price, TSPL_VLC_MASTER_HEAD.Apply_Price_Chart_Uploader,TSPL_VLC_MASTER_HEAD.Short_Description, TSPL_VLC_MASTER_HEAD.Price_Code,TSPL_VLC_MASTER_HEAD.vlc_code as [Code],TSPL_VLC_MASTER_HEAD.vlc_name,TSPL_VLC_MASTER_HEAD.vehical_name,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,TSPL_VLC_MASTER_HEAD.vsp_code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VLC_MASTER_HEAD.mcc,TSPL_MCC_MASTER.mcc_name,TSPL_VLC_MASTER_HEAD.Village_Code,tspl_village_master.village_name,TSPL_VLC_MASTER_HEAD.route_code,tspl_mcc_route_master.route_name,TSPL_VLC_MASTER_HEAD.Active,convert(date,TSPL_VLC_MASTER_HEAD.Created_Date,103) as Created_Date,TSPL_VLC_MASTER_HEAD.Milk_Receive_UOM,TSPL_VLC_MASTER_HEAD.Auto_Fill_MP_Order,TSPL_VLC_MASTER_HEAD.ApplyCowPriceDate,TSPL_VLC_MASTER_HEAD.Loyalty_Rate,isOwnBMC,OwnBMCDate,MCCOwnBMC,TSPL_VENDOR_MASTER.Is_Head_Load AS HeadLoad,TSPL_VENDOR_MASTER.Rate_Head_Load As HeadLoadRate,TSPL_VENDOR_MASTER.Service_Basis_Head_Load As HeadLoadBasis from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join tspl_village_master on TSPL_VLC_MASTER_HEAD.village_code=tspl_village_master.village_code left outer join tspl_mcc_route_master on TSPL_VLC_MASTER_HEAD.route_code=tspl_mcc_route_master.route_code "
            Dim strVLCCol As String = ""
            If isUploaderCode Then
                strVLCCol = "VLC_Code_VLC_Uploader"
            Else
                strVLCCol = "vlc_code"
            End If
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_VLC_MASTER_HEAD." + strVLCCol + "='" + strCode + "' " + whrcls + ""
                Case NavigatorType.First
                    qry += " where TSPL_VLC_MASTER_HEAD." + strVLCCol + " in (select min(TSPL_VLC_MASTER_HEAD." + strVLCCol + ") from TSPL_VLC_MASTER_HEAD where 2=2 " + whrcls + ")"
                Case NavigatorType.Last
                    qry += " where TSPL_VLC_MASTER_HEAD." + strVLCCol + " in (select max(TSPL_VLC_MASTER_HEAD." + strVLCCol + ") from TSPL_VLC_MASTER_HEAD where 2=2 " + whrcls + ")"
                Case NavigatorType.Next
                    qry += " where TSPL_VLC_MASTER_HEAD." + strVLCCol + " in (select min(TSPL_VLC_MASTER_HEAD." + strVLCCol + ") from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD." + strVLCCol + ">'" + strCode + "' " + whrcls + ")"
                Case NavigatorType.Previous
                    qry += " where TSPL_VLC_MASTER_HEAD." + strVLCCol + " in (select max(TSPL_VLC_MASTER_HEAD." + strVLCCol + ") from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD." + strVLCCol + "<'" + strCode + "' " + whrcls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsfrmVLCMaster()

                obj.vlcCode = clsCommon.myCstr(dt.Rows(0)("code"))
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.vlcName = clsCommon.myCstr(dt.Rows(0)("vlc_name"))
                obj.VLC_CODE_VLC_UPLOADER = clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader"))
                'obj.vehical = clsCommon.myCstr(dt.Rows(0)("vehical_name"))
                obj.vspCode = clsCommon.myCstr(dt.Rows(0)("vsp_code"))
                obj.VspName = clsCommon.myCstr(dt.Rows(0)("vendor_name"))
                obj.MCCCOde = clsCommon.myCstr(dt.Rows(0)("mcc"))
                obj.Active = clsCommon.myCdbl(dt.Rows(0)("Active"))
                obj.mainvillcode = clsCommon.myCstr(dt.Rows(0)("village_code"))
                obj.mainvillname = clsCommon.myCstr(dt.Rows(0)("village_name"))
                obj.routecode = clsCommon.myCstr(dt.Rows(0)("route_code"))
                obj.routename = clsCommon.myCstr(dt.Rows(0)("route_name"))
                obj.Milk_Receive_UOM = clsCommon.myCstr(dt.Rows(0)("Milk_Receive_UOM"))
                obj.Auto_Fill_MP_Order = clsCommon.myCdbl(dt.Rows(0)("Auto_Fill_MP_Order"))
                obj.Short_Description = clsCommon.myCstr(dt.Rows(0)("Short_Description"))
                obj.Apply_Price_Chart_Uploader = (clsCommon.myCdbl(dt.Rows(0)("Apply_Price_Chart_Uploader")) > 0)
                obj.Apply_Cow_Price = (clsCommon.myCdbl(dt.Rows(0)("Apply_Cow_Price")) > 0)
                obj.IsSuspense = (clsCommon.myCdbl(dt.Rows(0)("IsSuspense")) > 0)
                If dt.Rows(0)("ApplyCowPriceDate") IsNot DBNull.Value Then
                    obj.ApplyCowPriceDate = clsCommon.myCDate(dt.Rows(0)("ApplyCowPriceDate"))
                End If
                If Not IsDBNull(dt.Rows(0)("Created_Date")) Then
                    obj.Created_Date = dt.Rows(0)("Created_Date")
                End If
                obj.Loyalty_Rate = clsCommon.myCDecimal(dt.Rows(0)("Loyalty_Rate"))
                obj.TFOwnBMC = clsCommon.myCBool(dt.Rows(0)("isOwnBMC"))
                If dt.Rows(0)("OwnBMCDate") IsNot DBNull.Value Then
                    obj.OwnBMCDate = clsCommon.myCDate(dt.Rows(0)("OwnBMCDate"))
                End If
                obj.OwnBMC = clsCommon.myCstr(dt.Rows(0)("MCCOwnBMC"))
                If clsCommon.myCstr(dt.Rows(0)("HeadLoad")) = "T" OrElse clsCommon.myCdbl(dt.Rows(0)("HeadLoad")) = 1 Then
                    obj.HeadLoad = True
                    obj.HeadLoadRate = clsCommon.myCDecimal(dt.Rows(0)("HeadLoadRate"))
                    obj.HeadLoadBasis = clsCommon.myCstr(dt.Rows(0)("HeadLoadBasis"))
                Else
                    obj.HeadLoad = False
                End If

                qry = "select TSPL_VLC_MASTER_DETAIL.village_code,TSPL_VILLAGE_MASTER.village_name from TSPL_VLC_MASTER_DETAIL left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.village_code=TSPL_VLC_MASTER_DETAIL.village_code where TSPL_VLC_MASTER_DETAIL.vlc_code='" + obj.vlcCode + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    obj.arr = New List(Of clsfrmVLCMaster)

                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsfrmVLCMaster()

                        objtr.villagecode = clsCommon.myCstr(dr("village_code"))
                        objtr.villagename = clsCommon.myCstr(dr("village_name"))
                        'objtr.routecode = clsCommon.myCstr(dr("route_code"))
                        'objtr.routename = clsCommon.myCstr(dr("route_name"))
                        obj.arr.Add(objtr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ExportDataTable(ByVal strDcsCode As String, ByVal frmMe As RadForm, ByVal exportSheet As String) As Boolean
        Try
            Dim whrQry As String = Nothing
            If clsCommon.myLen(strDcsCode) > 0 AndAlso strDcsCode IsNot Nothing Then
                whrQry = "where TSPL_VLC_MASTER_HEAD.vsp_code ='" + strDcsCode + "'"
            End If
            Dim strQry As String = Nothing
            If exportSheet.Contains("BlankSheet") Then
                strQry = "select ''  As 'DCS Code','' As 'DCS Name',''  As 'DCS Uploader Code','' As 'PAN No',
                        '' As 'DCS Route Code','' As Active,
                        '' As 'Created Date','' As  'Loyalty Rate','' As 'Own BMC','' 'Own BMC Date','' As 'Apply Cow Price','' As 'Apply Cow Price Date','' As 'Head Load','' As 'Head Load Service Basis','' As 'Head Load Rate',
                        '' As 'Registration No','' As 'Registration Date','' As 'Registered/PDCS/CLUSTER',
                        '' As 'Supervisor','' As 'District Code','' As 'Block Code','' As 'Zone Code','' As 'Revenue Village Code','' AS 'Grampanchayat Code','' As 'Panchayat Samiti Code','' As 'Vidhan Sabha Code',
                        '' As 'Company Bank','' As 'Bank Code 1','' As 'Bank Name 1','' As 'Branch Name 1','' As 'IFSC Code 1','' As 'Account No 1','' As 'Credit Limit 1',
                        '' As 'Account Type 1','' As 'Security Charges 1',
                        '' As 'Bank Code 2','' As 'Bank Name 2','' As 'Branch Name 2','' As 'IFSC Code 2','' As 'Credit Limit 2','' As 'Account Type 2','' As 'Security Charges 2'
                        "
            Else
                strQry = "select TSPL_VLC_MASTER_HEAD.vsp_code  As 'DCS Code',TSPL_VENDOR_MASTER.Vendor_Name As 'DCS Name',TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader  As 'DCS Uploader Code',TSPL_VENDOR_MASTER.PAN As 'PAN No',
                        TSPL_VLC_MASTER_HEAD.route_code As 'DCS Route Code',TSPL_VLC_MASTER_HEAD.Active,
                        convert(date,TSPL_VLC_MASTER_HEAD.Created_Date,103) As 'Created Date',TSPL_VLC_MASTER_HEAD.Loyalty_Rate 'Loyalty Rate',TSPL_VLC_MASTER_HEAD.isOwnBMC As 'Own BMC',OwnBMCDate As 'Own BMC Date',TSPL_VLC_MASTER_HEAD.Apply_Cow_Price As 'Apply Cow Price',TSPL_VLC_MASTER_HEAD.ApplyCowPriceDate As 'Apply Cow Price Date',TSPL_VENDOR_MASTER.Is_Head_Load As 'Head Load',TSPL_VENDOR_MASTER.Service_Basis_Head_Load As 'Head Load Service Basis',TSPL_VENDOR_MASTER.Rate_Head_Load As 'Head Load Rate',
                        TSPL_VENDOR_MASTER.RegistrationNo As 'Registration No',TSPL_VENDOR_MASTER.RegistrationDate As 'Registration Date',TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER As 'Registered/PDCS/CLUSTER',
                        TSPL_VENDOR_MASTER.Gender,TSPL_VENDOR_MASTER.SupervisorOrRP As 'Supervisor',TSPL_VENDOR_MASTER.DISTRICT_Code As 'District Code',TSPL_VENDOR_MASTER.BLOCK_CODE As 'Block Code',TSPL_VENDOR_MASTER.Zone_Code As 'Zone Code',TSPL_VENDOR_MASTER.REVENUE_VILLAGE_CODE As 'Revenue Village Code',TSPL_VENDOR_MASTER.GRAMPANCHAYAT_CODE AS 'Grampanchayat Code',TSPL_VENDOR_MASTER.PANCHAYAT_SAMITI_CODE As 'Panchayat Samiti Code',TSPL_VENDOR_MASTER.VIDHAN_SABHA_CODE As 'Vidhan Sabha Code',
                        TSPL_VENDOR_MASTER.Company_Bank As 'Company Bank',TSPL_VENDOR_MASTER.Bank_Code As 'Bank Code 1',TSPL_VENDOR_MASTER.Bank_Name As 'Bank Name 1',TSPL_VENDOR_MASTER.Branch_Name As 'Branch Name 1',TSPL_VENDOR_MASTER.IFSC_Code As 'IFSC Code 1',TSPL_VENDOR_MASTER.Account_No As 'Account No 1',TSPL_VENDOR_MASTER.Credit_Limit As 'Credit Limit 1',
                        TSPL_VENDOR_MASTER.Account_Type As 'Account Type 1',TSPL_VENDOR_MASTER.Security_Amount As 'Security Charges 1',
                        TSPL_VENDOR_MASTER.BankCode2 As 'Bank Code 2',TSPL_VENDOR_MASTER.BankName2 As 'Bank Name 2',TSPL_VENDOR_MASTER.BankBranch2 As 'Branch Name 2',TSPL_VENDOR_MASTER.IFSCCode2 As 'IFSC Code 2',TSPL_VENDOR_MASTER.AccNo2 As 'Account No 2',TSPL_VENDOR_MASTER.Credit2 As 'Credit Limit 2',TSPL_VENDOR_MASTER.AccountType2 As 'Account Type 2',TSPL_VENDOR_MASTER.SecurityCharges2 As 'Security Charges 2'
                        from TSPL_VLC_MASTER_HEAD 
                        left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' 
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc 
                        left outer join tspl_mcc_route_master on TSPL_VLC_MASTER_HEAD.route_code=tspl_mcc_route_master.route_code
                        " + whrQry + "
                        Order By Cast(TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader As int) Asc"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt.Rows.Count > 0 Then
                transportSql.ExporttoExcel(dt, frmMe)
                dt = Nothing
            Else
                Throw New Exception("No data found")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetCodeNumPart(ByVal Code As String) As Integer
        '' created by Panch Raj against Ticket No:BM00000009815 on date 23/09/2016
        Dim Num As String = ""
        Dim FinalNum As String = ""
        Dim intLen As Integer = Code.Length - 1
        Dim tempChar As String = ""
        For intLoop As Integer = intLen To 0 Step -1
            tempChar = Code.Chars(intLoop)
            If IsNumeric(tempChar) = True Then
                Num = Num + tempChar
            Else
                Exit For
            End If
        Next
        Dim Remain As Integer = 0
        For intLoop As Integer = Num.Length - 1 To 0 Step -1
            FinalNum = FinalNum + Num.Chars(intLoop)
            'Remain = clsCommon.myCdbl(Num) Mod 10
            'FinalNum = FinalNum * 10 + Remain
            'Num = Math.Truncate(clsCommon.myCdbl(Num) / 10)
        Next

        Return clsCommon.myCdbl(FinalNum)
    End Function

    Public Shared Function CreateNewVSP_VLC(ByVal VLCUploader As String, ByVal MCCCode1 As String) As Boolean
        Try
            Dim arrExistCols As New List(Of String)
            Dim dtDefault = New DataTable()
            Dim newBlankRow1 As DataRow = dtDefault.NewRow
            dtDefault.Rows.Add(newBlankRow1)
            Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData("MP-IMP-TMP")
            If (objDefaultTemplate IsNot Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) > 0) Then
                If objDefaultTemplate.Arr IsNot Nothing AndAlso objDefaultTemplate.Arr.Count > 0 Then
                    For Each objTr As clsExportTemplateDetail In objDefaultTemplate.Arr
                        If clsCommon.myLen(objTr.Column_Name) > 0 Then
                            arrExistCols.Add(objTr.Column_Name)
                            Dim newColumn As New DataColumn(clsCommon.myCstr(objTr.Column_Name), GetType(System.String))
                            dtDefault.Columns.Add(newColumn)
                            dtDefault.Rows(0).Item(clsCommon.myCstr(objTr.Column_Name)) = clsCommon.myCstr(objTr.Column_Header)
                        End If
                    Next
                End If
            End If


            ''Create VSP,VLC,PTM,Route etc 
            If True Then
                Dim qry As String = ""
                Dim trans As SqlTransaction = Nothing
                ''Primary Transport Master
                Dim strvendorNo As String = String.Empty
                Dim strvendorname1 As String = String.Empty
                Dim strvendorname As String = String.Empty
                Dim StrVdrNo As String = String.Empty
                Dim check As Integer = 0
                Dim i2 As Integer = 0
                Dim coll As New Hashtable()
                Dim strBrachName As String = String.Empty
                Dim strIFSCCode As String = String.Empty
                Dim strbank As String = String.Empty
                Dim statecode As String = String.Empty
                Dim state As String = String.Empty
                Dim country As String = String.Empty
                Dim closing_date As String = String.Empty
                Dim strgroupCode As String = String.Empty
                Dim strgroupDes As String = String.Empty
                Dim CityCode As String = String.Empty
                Dim CityName As String = String.Empty
                Dim PC_CODE As String = String.Empty
                Dim StrTempVSPName As String = String.Empty


                If objCommonVar.ApplyDefaultsInMaster = True Then
                    CityCode = clsCommon.myCstr(clsCityMaster.GetDefault(trans))
                    CityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + CityCode + "'", trans))
                    PC_CODE = clsCommon.myCstr(clsPaymentCycleMaster.GetDefault(trans))

                    country = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 COUNTRY_CODE from TSPL_COUNTRY_MASTER", trans))
                End If

                Dim isNewEntry As Boolean = True

                ''VSP Master

                trans = clsDBFuncationality.GetTransactin()
                Try
                    Dim VlcUploaderCode As String = ""
                    Dim dt As DataTable = Nothing
                    If arrExistCols.Contains(clsMasterDefault.colDCSUploaderCode) Then
                        VlcUploaderCode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSUploaderCode))
                        qry = "select VLC_Code,VLC_Name,VSP_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + VlcUploaderCode + "'"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If arrExistCols.Contains(clsMasterDefault.colDCSVLCCode) Then
                                dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCCode) = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                            End If
                            If arrExistCols.Contains(clsMasterDefault.colDCSVLCName) Then
                                dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCName) = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                            End If
                            If arrExistCols.Contains(clsMasterDefault.colDCSVSPCode) Then
                                dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPCode) = clsCommon.myCstr(dt.Rows(0)("VSP_Code"))
                            End If
                            If arrExistCols.Contains(clsMasterDefault.colDCSVSPName) Then
                                dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPName) = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                            End If
                        End If
                    End If

                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPCode) Then
                        strvendorNo = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPCode))
                    End If

                    If strvendorNo.Length > 12 Then
                        Throw New Exception("Check the length of VSP Code,")
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPName) Then
                        If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPName)) <= 0 Then
                            If arrExistCols.Contains(clsMasterDefault.colDCSVLCName) Then
                                dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPName) = dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCName)
                            End If
                        End If
                    End If

                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPName) Then
                        strvendorname1 = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPName))
                    End If

                    strvendorname = strvendorname1.Replace("'", "''")
                    If strvendorname.Length > 100 Then
                        Throw New Exception("Length of VSP Name can not be greater than 100.,")
                    End If

                    If String.IsNullOrEmpty(strvendorname) Then
                        strvendorname = VLCUploader
                        'Throw New Exception("VSP Name can not be blank")
                    End If
                    Dim add1 As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPAddress) Then
                        add1 = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPAddress))
                    End If

                    closing_date = System.DateTime.Now.Date
                    If arrExistCols.Contains(clsMasterDefault.colDCSState) Then

                        statecode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSState))
                        If clsCommon.myLen(statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                            statecode = clsStateMaster.GetDefault(trans)
                        End If
                        check = 0
                        If clsCommon.myLen(statecode) > 0 Then
                            qry = "select STATE_CODE,STATE_NAME from tspl_state_master where  state_code='" + statecode + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                            End If
                            statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                            state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                        End If
                    End If
                    Dim vsppaymnt As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colVSPPaymentType) Then
                        vsppaymnt = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colVSPPaymentType)).Replace("'", "`")
                    End If

                    Dim NameOfBank As String = ""
                    Dim AccountNo As String = ""

                    If arrExistCols.Contains(clsMasterDefault.colDCSBankCode) Then
                        strbank = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSBankCode))
                        If clsCommon.myLen(strbank) > 0 Then
                            If String.IsNullOrEmpty(strbank) Then
                                Throw New Exception("Bank Code can not be blank")
                            End If
                            Dim EnableBankFromMaster As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableBankFromMaster, clsFixedParameterCode.EnableBankFromMaster, trans)) = 1, True, False)
                            Dim i5 As String
                            If EnableBankFromMaster = True Then
                                Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                                i5 = connectSql.RunScalar(trans, qry7)
                                If i5 = 0 Then
                                    Throw New Exception("Bank code does not exist : " + strbank + "")
                                End If
                            End If
                            If strbank.Length > 30 Then
                                Throw New Exception("Check the length of bank code")
                            End If
                        End If

                    End If
                    Dim strAccNo As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSAccountNo) Then
                        strAccNo = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSAccountNo))
                    End If
                    Dim strBName As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSBankName) Then
                        strBName = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSBankName))
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colDCSIFSCCode) Then
                        strIFSCCode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSIFSCCode))
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colDCSBranchName) Then
                        strBrachName = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSBranchName))
                    End If

                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPGroupCode) Then
                        strgroupCode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPGroupCode))
                    End If

                    If String.IsNullOrEmpty(strgroupCode) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        strgroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ven_Group_Code from Tspl_vendor_group where Default_VSP=1", trans))
                    End If
                    If String.IsNullOrEmpty(strgroupCode) Then
                        Throw New Exception("VSP Group Code can not be blank")
                    End If
                    Dim i As Integer
                    qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                    i = connectSql.RunScalar(trans, qry)
                    If i = 0 Then
                        Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                    Else
                    End If
                    If strgroupCode.Length > 12 Then
                        Throw New Exception("Check the length of VSP Group Code")
                    End If

                    strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))


                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                    clsCommon.AddColumnsForChange(coll, "add1", add1)
                    clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                    clsCommon.AddColumnsForChange(coll, "State", state)
                    clsCommon.AddColumnsForChange(coll, "Country", country)
                    clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                    clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                    clsCommon.AddColumnsForChange(coll, "City_Code", CityCode, True)
                    clsCommon.AddColumnsForChange(coll, "City_Code_Desc", CityName, True)
                    clsCommon.AddColumnsForChange(coll, "vsp_payment", vsppaymnt, True)
                    clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName, True)
                    clsCommon.AddColumnsForChange(coll, "Account_No", strAccNo, True)
                    clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode, True)
                    clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                    clsCommon.AddColumnsForChange(coll, "Nature", "E")
                    clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                    clsCommon.AddColumnsForChange(coll, "Status", "N")
                    clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                    clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                    If arrExistCols.Contains(clsMasterDefault.colDCSBuffalowTIP) Then
                        clsCommon.AddColumnsForChange(coll, "Tip_Buffalo", clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colDCSBuffalowTIP)))
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colDCSCowTIP) Then
                        clsCommon.AddColumnsForChange(coll, "Tip_Cow", clsCommon.myCdbl(dtDefault.Rows(0).Item(clsMasterDefault.colDCSCowTIP)))
                    End If
                    clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    qry = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSType) Then
                        qry = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSType))
                    End If

                    If Not (clsCommon.CompairString(qry, "PDCS") = CompairStringResult.Equal OrElse clsCommon.CompairString(qry, "Registered") = CompairStringResult.Equal) Then
                        'Throw New Exception("DCS Type Should be Registered/PDCS ")
                        qry = "Registered"
                    End If
                    clsCommon.AddColumnsForChange(coll, "Registered_PDCS_CLUSTER", qry)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    StrVdrNo = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPCode) Then
                        StrVdrNo = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPCode))
                    End If
                    If (clsCommon.myLen(StrVdrNo) <= 0) Then
                        If arrExistCols.Contains(clsMasterDefault.colDCSType) Then
                            StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSType)), "PDCS") = CompairStringResult.Equal, clsDocTransactionType.PDCS, clsDocTransactionType.Registered), "")
                        Else
                            StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
                        End If

                        clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        If arrExistCols.Contains(clsMasterDefault.colDCSVSPCode) Then
                            dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPCode) = StrVdrNo
                        End If
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='VSP'", trans)
                    End If



                    '' End of VSP Master
                    ''create customer as VSP
                    If arrExistCols.Contains(clsMasterDefault.colDCSCreatecustomer) Then
                        Dim createCustomer = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSCreatecustomer))
                        If clsCommon.myLen(createCustomer) <= 0 Then
                            createCustomer = "0"
                        End If
                        If clsCommon.CompairString(createCustomer, "0") <> CompairStringResult.Equal And clsCommon.CompairString(createCustomer, "1") <> CompairStringResult.Equal Then
                            Throw New Exception("Please Fill Create customer And It Should Be 0 or 1 ")
                        End If
                        If clsCommon.CompairString(createCustomer, "1") = CompairStringResult.Equal Then
                            Dim objCustomer As New clsCustomerMaster()
                            objCustomer.Cust_Code = StrVdrNo
                            objCustomer.Customer_Name = strvendorname
                            objCustomer.Add1 = add1
                            objCustomer.State = statecode
                            objCustomer.CUSTOMER_FORM_TYPE = "VSP"
                            If arrExistCols.Contains(clsMasterDefault.colDCSCustomerGroupCode) Then
                                strgroupCode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSCustomerGroupCode))
                            End If
                            If String.IsNullOrEmpty(strgroupCode) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                strgroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Code from TSPL_CUSTOMER_GROUP_MASTER where Default_VSP=1", trans))
                            End If

                            If String.IsNullOrEmpty(strgroupCode) Then
                                Throw New Exception("Customer Group Code can not be blank")
                            End If

                            qry = "select Count(*) from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strgroupCode + "'"
                            i = connectSql.RunScalar(trans, qry)
                            If i = 0 Then
                                Throw New Exception("Customer Group Code does not exist : " + strgroupCode + "")
                            Else
                            End If
                            If strgroupCode.Length > 12 Then
                                Throw New Exception("Check the length of Customer Group Code")
                            End If

                            Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," &
                              " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + strgroupCode + "' "
                            Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
                            If myDs.Tables(0).Rows.Count > 0 Then
                                Dim row As DataRow = myDs.Tables(0).Rows(0)
                                objCustomer.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                                objCustomer.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                                objCustomer.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                                objCustomer.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
                            End If
                            objCustomer.Credit_Customer = "N"

                            objCustomer.LastInvoice_No = Nothing
                            objCustomer.LastInvoice_Date = Nothing
                            objCustomer.Inter_Branch = "N"

                            objCustomer.IsDistributor = "N"

                            objCustomer.prntcustyn = "N"

                            objCustomer.CSA_Type = "N"
                            objCustomer.ManualCustomer = "N"

                            objCustomer.Comp_Code = objCommonVar.CurrentCompanyCode

                            Dim arrDBName As New List(Of String)
                            arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))

                            qry = "select count(*) from TSPL_CUSTOMER_MASTER where cust_code ='" + StrVdrNo + "' and CUSTOMER_FORM_TYPE='VSP'"
                            i2 = CInt(connectSql.RunScalar(trans, qry))
                            If (i2 = 0) Then
                                objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, True, arrDBName, trans)
                            Else
                                objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, False, arrDBName, trans)
                            End If

                            'Customer Vendor mapping
                            i2 = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + StrVdrNo + "'", trans)
                            If i2 = 0 Then
                                qry = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + StrVdrNo + "','" + StrVdrNo + "') "
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        End If
                    End If


                    ''-----create customer as VSP



                    '' Village Master
                    Dim objVillage As New clsfrmVillageMaster
                    If arrExistCols.Contains(clsMasterDefault.colDCSVillageName) Then
                        objVillage.villname = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVillageName))
                    End If

                    If clsCommon.myLen(objVillage.villname) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        If arrExistCols.Contains(clsMasterDefault.colDCSVLCName) Then
                            objVillage.villname = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCName))
                        End If
                    End If
                    If clsCommon.myLen(objVillage.villname) <= 0 Then
                        'Throw New Exception("Please Fill Village Name")
                        objVillage.villname = strvendorname
                    End If
                    If clsCommon.myLen(objVillage.villname) > 150 Then
                        Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters")
                    End If
                    ' objVillage.citycode = clsCommon.myCstr(gv1.Rows(ii).Cells("city_code").Value)
                    If arrExistCols.Contains(clsMasterDefault.colDCSState) Then
                        objVillage.statecode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSState))
                    End If

                    If clsCommon.myLen(objVillage.statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                        objVillage.statecode = clsStateMaster.GetDefault(trans)
                    End If
                    If clsCommon.myLen(objVillage.statecode) > 0 Then
                        qry = "select state_code from tspl_state_master where state_code='" + objVillage.statecode + "'"
                        objVillage.statecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(objVillage.statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                            objVillage.statecode = clsStateMaster.GetDefault(trans)
                        End If
                        If clsCommon.myLen(objVillage.statecode) <= 0 Then
                            Throw New Exception("First Create State Master(" + objVillage.statecode + " Does Not Exist In Master)")
                        End If
                    End If
                    If objCommonVar.ApplyDefaultsInMaster = True Then
                        objVillage.citycode = clsCommon.myCstr(clsCityMaster.GetDefault(trans))
                    End If
                    isNewEntry = True
                    objVillage.villcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Village_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + objVillage.villname + "'", trans))
                    Dim objTState As clsStateMaster = clsStateMaster.GetData(objVillage.statecode, NavigatorType.Current, trans)
                    If objTState IsNot Nothing AndAlso clsCommon.myLen(objTState.Code) > 0 Then
                        objVillage.countrycode = objTState.COUNTRY_CODE
                    End If
                    If clsCommon.myLen(objVillage.villcode) > 0 Then
                        isNewEntry = False
                    End If
                    clsfrmVillageMaster.SaveData(objVillage, isNewEntry, trans)

                    '' End of Village MAster 




                    '' VLC Master
                    Dim mcccode As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colMCCCode) Then
                        mcccode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCode))
                    End If

                    If clsCommon.myLen(mcccode) <= 0 Then
                        'Throw New Exception("Please Fill MCC Code")
                        mcccode = MCCCode1
                    End If
                    If clsCommon.myLen(objVillage.villname) <= 0 Then
                        Throw New Exception("Please Fill Village Name At Line No.")
                    End If
                    If clsCommon.myLen(objVillage.villname) > 150 Then
                        Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters")
                    End If





                    If clsCommon.myLen(VlcUploaderCode) <= 0 Then
                        'Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters")
                        VlcUploaderCode = VLCUploader
                    End If
                    Dim villcode As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSVillageName) Then
                        villcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ViLLage_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVillageName)) + "'", trans))
                    End If
                    If clsCommon.myLen(villcode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        villcode = objVillage.villcode
                    End If

                    Dim vspcode As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSVSPName) Then
                        vspcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVSPName)) + "' and Form_type='VSP'", trans))
                    End If
                    If clsCommon.myLen(vspcode) <= 0 Then
                        vspcode = StrVdrNo
                    End If
                    Dim MilkRouteCode As String = clsDBFuncationality.getSingleValue("Select top 1 Route_Code from TSPL_MCC_ROUTE_MASTER ", trans)

                    If clsCommon.myLen(MilkRouteCode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        MilkRouteCode = clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER where route_name='" & StrTempVSPName & "' ", trans)
                    End If


                    Dim isSaved As Boolean = True
                    'qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSVLCName).Value) + "'"
                    'Dim VLCCode As String = clsDBFuncationality.getSingleValue(qry, trans)

                    Dim VLCCode As String = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSVLCCode) Then
                        VLCCode = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCCode))
                    End If

                    If clsCommon.myLen(VLCCode) <= 0 Then
                        VLCCode = mcccode & "/" & clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colDCSVLCCode) Then
                        dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCCode) = VLCCode
                    End If
                    Dim Tempvlc_name As String = ""
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)
                    If arrExistCols.Contains(clsMasterDefault.colDCSVLCName) Then
                        Tempvlc_name = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCName))
                    End If
                    If clsCommon.myLen(Tempvlc_name) <= 0 Then
                        Tempvlc_name = strvendorname
                    End If
                    clsCommon.AddColumnsForChange(coll, "vlc_name", Tempvlc_name)
                    clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                    clsCommon.AddColumnsForChange(coll, "village_code", villcode)

                    clsCommon.AddColumnsForChange(coll, "MCC", mcccode)
                    clsCommon.AddColumnsForChange(coll, "Route_Code", MilkRouteCode)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    qry = ""
                    If arrExistCols.Contains(clsMasterDefault.colDCSVLCName) Then
                        qry = "select count(VLC_Code) from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colDCSVLCName)) + "'"
                    Else
                        qry = "select count(VLC_Code) from TSPL_VLC_MASTER_HEAD where vlc_Name='" + Tempvlc_name + "'"
                    End If
                    check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Price_Code", Nothing, True)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                            VlcUploaderCode = clsfrmVLCMaster.GetCodeNumPart(VLCCode)
                            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                        Else
                            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                        End If
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)

                    Else

                        clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + VLCCode + "'", trans)
                    End If

                    'Create User
                    qry = "select count(User_Code) from TSPL_USER_MASTER where User_Code='" + VlcUploaderCode + "'"
                    check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                    If check <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "User_Code", VlcUploaderCode)
                        clsCommon.AddColumnsForChange(coll, "User_Name", strvendorname)
                        clsCommon.AddColumnsForChange(coll, "Password", clsCommon.EncryptString(VlcUploaderCode))
                        clsCommon.AddColumnsForChange(coll, "Default_Location", mcccode, True)
                        clsCommon.AddColumnsForChange(coll, "User_APP_Type", "V", True)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Code", vspcode, True)
                        clsCommon.AddColumnsForChange(coll, "User_Type", "")
                        clsCommon.AddColumnsForChange(coll, "EMP_CODE", "")
                        clsCommon.AddColumnsForChange(coll, "Emp_Name", "")
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    End If


                    clsfrmVLCMaster.SaveVLCPriceCode(VLCCode, vspcode, mcccode, trans)



                    '' End Of VLC Master



                    If clsCommon.myLen(MilkRouteCode) > 0 Then
                        ''MILK ROUTE VLC MAPPING DETAIL
                        qry = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Is_Active", 1)
                        clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)

                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "route_code", MilkRouteCode)
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'", trans)
                        End If
                        ''END OF MILK ROUTE VLC MAPPING DETAIL
                    End If




                    trans.Commit()

                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
            'Catch ex As Exception
            '    Return False
            '    myMessages.myExceptions(ex)
            'End Try
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

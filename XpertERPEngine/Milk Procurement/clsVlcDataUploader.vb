Imports common
Imports System.Data.SqlClient
Public Class clsVlcDataUploader
    Public Doc_No As String = Nothing
    Public Doc_Date As Date = Nothing
    Public MCC_Code As String = Nothing
    Public File_Date As Date = Nothing
    Public shift As String = Nothing
    Public VLC_CODE As String = Nothing
    Public Route_No As String = Nothing
    Public MP_CODE As String = Nothing
    Public Milk_Type As String = Nothing
    Public qty As Double = 0
    Public Uom_Code As String = Nothing
    Public fat As Double = 0
    Public snf As Double = 0
    Public fat_KG As Double = 0
    Public snf_KG As Double = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public water As Double = 0
    Public Comp_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public isNewEntry As Boolean = False

    Public Shared Function deleteData(ByVal arr As ArrayList) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each strDocNo As String In arr
                deleteData(strDocNo, trans)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 Doc_Date,MCC_Code from tspl_vlc_data_uploader where doc_no='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVlcdataUploadar, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)
        End If

        Dim isDeleted As Boolean = True
        Dim qry1 As String = "select Doc_Date,MCC_Code from tspl_vlc_data_uploader where doc_no='" + strDocNo + "'"


        Dim qry As String = "delete from tspl_vlc_data_uploader where doc_no='" & strDocNo & "'"
        isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '' LOG FOR SYNC DATA
        isDeleted = isDeleted AndAlso clsSyncHeadTables.SaveSyncDelete("tspl_vlc_data_uploader", strDocNo, trans)
        Return isDeleted
    End Function
    
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select distinct doc_no as DocNo,convert(varchar,doc_date,103) as [Doc Date],convert(varchar,file_date,103) as [file Date],shift as [Shift] " &
            ",tspl_vlc_data_uploader.MCC_Code as [MCC Code],TSPL_MCC_MASTER.MCC_NAME AS [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC NAME],TSPL_VLC_DATA_UPLOADER.VLC_Code as [VLC Uploader Code],tspl_vlc_data_uploader.route_no as [Route No]" &
            " from tspl_vlc_data_uploader Left Outer Join TSPL_VLC_MASTER_HEAD on tspl_vlc_data_uploader.vlc_code =TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader Left Outer Join TSPL_MCC_MASTER on tspl_vlc_data_uploader.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE"
            str = clsCommon.ShowSelectForm("VLCDATAUPLD", qry, "DocNo", whrcls, curcode, "DocNo", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function CheckBillStatusByVLC(ByVal arrVLC As ArrayList, ByVal Doc_Date As Date, ByVal trans As SqlTransaction) As ArrayList
        Dim Qry As String = "select VLC.VLC_Code,count(*) as Total_Rec from TSPL_MILK_PURCHASE_INVOICE_HEAD BILL INNER JOIN TSPL_VLC_MASTER_HEAD VLC ON BILL.VSP_CODE=VLC.VSP_CODE where VLC.VLC_CODE in (" & clsCommon.GetMulcallString(arrVLC) & ") and '" & clsCommon.GetPrintDate(Doc_Date, "dd-MMM-yyyy") & "'  Between BILL.FROM_DATE and BILL.TO_DATE group by VLC.VLC_Code having count(*)>0"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        Dim arr As New ArrayList
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr.Item("VLC_Code")))
            Next       
        End If
        Return arr
    End Function
    Public Shared Function CheckBillStatusByVSP(ByVal VSP_Code As String, ByVal Doc_Date As Date, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = "select count(*) as Total_Rec from TSPL_MILK_PURCHASE_INVOICE_HEAD where VSP_CODE='" & VSP_Code & "' and '" & clsCommon.GetPrintDate(Doc_Date, "dd-MMM-yyyy") & "'  between FROM_DATE and TO_DATE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        If dt.Rows.Count > 0 Then
            If clsCommon.myCdbl(dt.Rows(0).Item("Total_Rec")) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Shared Function saveData(ByVal arrObj As List(Of clsVlcDataUploader), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmVlcdataUploadar, arrObj(0).MCC_Code, arrObj(0).Doc_Date, trans)

            If arrObj.Count <= 0 Then
                Return False
            End If
            '' apply validaton for Farmer payment: If Farmer Payment is On and Bill is generated for the VSP for which milk data is to be uploaded
            '' get MPPayment setting
            Dim IsMP As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isFarmerPaymentCycle, clsFixedParameterCode.isFarmerPaymentCycle, trans))
            If IsMP = 1 Then
                Dim arrVLC As New ArrayList
                Dim Doc_Date As Date
                For Each rec As clsVlcDataUploader In arrObj
                    If arrVLC.Contains(clsCommon.myCstr(rec.VLC_CODE)) = False Then
                        arrVLC.Add(clsCommon.myCstr(rec.VLC_CODE))
                    End If
                    Doc_Date = rec.Doc_Date
                Next
                Dim arr As ArrayList = CheckBillStatusByVLC(arrVLC, Doc_Date, trans)
                If Not arr Is Nothing AndAlso arr.Count > 0 Then
                    Dim VLC As String = clsCommon.GetMulcallStringWithComma(arr)
                    Throw New Exception("Bill is generated for Some VLCs:" & Environment.NewLine & VLC)
                End If
            End If

            clsVlcDataUploader.deleteData(arrObj(0).Doc_No, trans)
            Dim coll As Hashtable
            Dim obj As clsVlcDataUploader = Nothing
            ' clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "VLC Data Uploader ", obj.MCC_Code, obj.Doc_Date, trans)
            For i As Integer = 0 To arrObj.Count - 1
                obj = arrObj(i)
                '' delete same data from manual vlc data collection                
                '' done by Panch raj as per kwality request : Request No :562
                Dim qry As String = " delete from TSPL_VLC_DATA_UPLOADER_DETAIL where Document_Code in (select Document_Code from TSPL_VLC_DATA_UPLOADER_MASTER where cast(Document_Date as date)='" & clsCommon.GetPrintDate(obj.File_Date, "dd/MMM/yyyy") & "' " &
                                    " and VLC_Code in (select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" & obj.VLC_CODE & "' and Route_Code='" & obj.Route_No & "' and MCC='" & obj.MCC_Code & "') and Shift like '" & obj.shift & "%')" &
                                    " and Farmer_Code in (select MP_Code from TSPL_MP_MASTER where MP_Code_VLC_Uploader='" & obj.MP_CODE & "' and VLC_Code in (select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" & obj.VLC_CODE & "' and Route_Code='" & obj.Route_No & "' and MCC='" & obj.MCC_Code & "'))"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))
                clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "file_Date", clsCommon.GetPrintDate(obj.File_Date, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "Shift", clsCommon.myCstr(obj.shift))
                clsCommon.AddColumnsForChange(coll, "Mcc_code", clsCommon.myCstr(obj.MCC_Code))
                clsCommon.AddColumnsForChange(coll, "Mp_code", clsCommon.myCstr(obj.MP_CODE))
                clsCommon.AddColumnsForChange(coll, "Vlc_code", clsCommon.myCstr(obj.VLC_CODE))
                clsCommon.AddColumnsForChange(coll, "Route_no", clsCommon.myCstr(obj.Route_No))
                clsCommon.AddColumnsForChange(coll, "Milk_type", clsCommon.myCstr(obj.Milk_Type))
                clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCdbl(obj.qty))
                clsCommon.AddColumnsForChange(coll, "Fat", clsCommon.myCdbl(obj.fat))
                clsCommon.AddColumnsForChange(coll, "snf", clsCommon.myCdbl(obj.snf))
                clsCommon.AddColumnsForChange(coll, "Fat_Kg", clsCommon.myCdbl(clsVlcDataUploader.calcFat_Snf_KG(obj.MCC_Code, obj.fat, obj.qty, trans, False)))
                clsCommon.AddColumnsForChange(coll, "snf_Kg", clsCommon.myCdbl(clsVlcDataUploader.calcFat_Snf_KG(obj.MCC_Code, obj.snf, obj.qty, trans, False)))
                arrObj(i).Uom_Code = clsCommon.myCstr(clsVlcDataUploader.calcFat_Snf_KG(obj.MCC_Code, obj.snf, obj.qty, trans, True))
                clsCommon.AddColumnsForChange(coll, "Uom_COde", arrObj(i).Uom_Code)
                If clsCommon.CompairString(arrObj(i).Uom_Code, "") = CompairStringResult.Equal Then
                    Throw New Exception("Unit code cannot be blank")
                End If
                clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCdbl(obj.Rate))
                clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.Amount))
                clsCommon.AddColumnsForChange(coll, "water", clsCommon.myCdbl(obj.water))
                clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(obj.Modified_By))
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(obj.Modified_Date))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.Comp_Code))
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(obj.Created_By))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(obj.Created_Date))
                '' update Sync Satatus
                clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)

                'Dim strVLCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where MCC='" + obj.MCC_Code + "' and VLC_Code_VLC_Uploader='" + obj.VLC_CODE + "'", trans))
                'Dim strMPCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MP_Code from TSPL_MP_MASTER where VLC_Code='" + strVLCCode + "' and MP_Code_VLC_Uploader='" + obj.MP_CODE + "'", trans))
                Dim objIncetive As clsMPIncetiveDetail = clsMPIncetive.GetIncentive(True, obj.snf, obj.MCC_Code, obj.VLC_CODE, obj.Doc_Date, trans)
                If objIncetive IsNot Nothing AndAlso clsCommon.myLen(objIncetive.TRCode) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Incentive_TRCode", objIncetive.TRCode)
                    clsCommon.AddColumnsForChange(coll, "Incentive_Amt", Math.Round(obj.qty * objIncetive.Slab_Value, 2))
                End If

                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_DATA_UPLOADER", OMInsertOrUpdate.Insert, "", trans)
            Next
            CreateSMSContentMP(arrObj, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            'clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function

    Shared Sub CreateSMSContentMP(ByVal Arr As List(Of clsVlcDataUploader), ByVal trans As SqlTransaction)
        'SMSCode Start
        Dim strSMSContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SMS_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmVlcdataUploadar + "'", trans))
        If clsCommon.myLen(strSMSContent) > 0 Then
            For Each objtr As clsVlcDataUploader In Arr
                Dim qry As String = "select TSPL_MCC_MASTER.Plant_Code,TSPL_LOCATION_MASTER.Location_Desc as PlantName,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, 
                    TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
                    TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Name,TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.Telphone  from TSPL_MP_MASTER 
                    inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + objtr.VLC_CODE + "'
                    left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code='" + objtr.MCC_Code + "'
                    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Plant_Code
                    where TSPL_MP_MASTER.MP_Code_VLC_Uploader ='" + objtr.MP_CODE + "' and TSPL_MP_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code and len(isnull(TSPL_MP_MASTER.Telphone,''))>0"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.SMS_Text = strSMSContent

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.PlantCode, clsCommon.myCstr(dt.Rows(0)("Plant_Code")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.PlantName, clsCommon.myCstr(dt.Rows(0)("PlantName")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dt.Rows(0)("MCC_Code")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dt.Rows(0)("MCC_NAME")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dt.Rows(0)("Mcc_Code_VLC_Uploader")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCCode, clsCommon.myCstr(dt.Rows(0)("VLC_Code")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCName, clsCommon.myCstr(dt.Rows(0)("VLC_Name")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCUploaderCode, clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.MPCode, clsCommon.myCstr(dt.Rows(0)("MP_Code")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.MPUploaderCode, clsCommon.myCstr(dt.Rows(0)("MP_Code_VLC_Uploader")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderMP, clsCommon.myCstr(dt.Rows(0)("MP_Name")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(objtr.Doc_Date, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderShift, objtr.shift)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderMilkType, objtr.Milk_Type)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderQty, clsCommon.myFormat(objtr.qty))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.UOM, objtr.Uom_Code)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myFormat(objtr.fat))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myFormat(objtr.snf))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myFormat(objtr.Rate))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myFormat(objtr.Amount))

                    objSMSH.arrMobilNo = New List(Of String)()
                    objSMSH.arrMobilNo.Add(clsCommon.myCstr(dt.Rows(0)("Telphone")))
                    objSMSH.SaveData(clsUserMgtCode.frmVlcdataUploadar, objSMSH, trans)
                    objSMSH = Nothing
                End If
            Next
        End If
        'SMSCode End Start
    End Sub


    Public Shared Function calcFat_Snf_KG(ByVal Mcc_Code_Uom As String, ByVal Fat_Or_Snf_Per As Decimal, ByVal qty As Decimal, ByVal trans As SqlTransaction, ByVal isUom As Boolean)
        Dim MccUOm As String = clsDBFuncationality.getSingleValue("select  upper(Uom_COde) as Uom_COde from tspl_Mcc_Uom_Detail where Stocking_Unit='Y' and mcc_COde='" & Mcc_Code_Uom & "'", trans)
        Dim Fat_Snf_Kg As Decimal = 0
        If clsCommon.CompairString(MccUOm, "KG") = CompairStringResult.Equal Then
            Fat_Snf_Kg = qty * Fat_Or_Snf_Per / 100
        ElseIf clsCommon.CompairString(MccUOm, "LTR") = CompairStringResult.Equal Then
            Fat_Snf_Kg = qty * Fat_Or_Snf_Per * 1.03 / 100
        Else
            Fat_Snf_Kg = 0
        End If
        If isUom Then
            Return MccUOm
        End If
        MccUOm = String.Empty
        Return Fat_Snf_Kg
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As List(Of clsVlcDataUploader)
        Dim arrObj As New List(Of clsVlcDataUploader)
        Try
            Dim obj As clsVlcDataUploader = Nothing
            Dim qst As String = " select *,(select mcc_Code_vlc_Uploader from tspl_Mcc_Master where tspl_Mcc_Master.mcc_Code=tspl_vlc_data_uploader.mcc_Code) as mcc_Code_vlc_Uploader  From tspl_vlc_data_uploader   where 1=1  "
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_vlc_data_uploader.Doc_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_vlc_data_uploader.Doc_no in (select min(Doc_no ) from tspl_vlc_data_uploader where Doc_No  >'" + strCode + "' and comp_code='" & objCommonVar.CurrentCompanyCode & "'  )"
                Case NavigatorType.First
                    qst += " and tspl_vlc_data_uploader.Doc_no in (select MIN(Doc_no ) from tspl_vlc_data_uploader where 1=1 and comp_code='" & objCommonVar.CurrentCompanyCode & "'  )"
                Case NavigatorType.Last
                    qst += " and tspl_vlc_data_uploader.Doc_no in (select Max(Doc_no ) from tspl_vlc_data_uploader where 1=1and comp_code='" & objCommonVar.CurrentCompanyCode & "'  )"
                Case NavigatorType.Previous
                    qst += " and tspl_vlc_data_uploader.Doc_no in (select Max(Doc_no ) from tspl_vlc_data_uploader where Doc_No  <'" + strCode + "' and comp_code='" & objCommonVar.CurrentCompanyCode & "'  )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsVlcDataUploader()
                    obj.Doc_No = clsCommon.myCstr(dt.Rows(i)("Doc_No"))
                    obj.Doc_Date = clsCommon.myCDate(dt.Rows(i)("doc_date"), "dd/MMM/yyyy")
                    obj.File_Date = clsCommon.myCDate(dt.Rows(i)("file_date"), "dd/MMM/yyyy")
                    obj.shift = clsCommon.myCstr(dt.Rows(i)("shift"))
                    obj.MCC_Code = clsCommon.myCstr(dt.Rows(i)("mcc_Code_vlc_Uploader"))
                    obj.VLC_CODE = clsCommon.myCstr(dt.Rows(i)("vlc_code"))
                    obj.MP_CODE = clsCommon.myCstr(dt.Rows(i)("mp_code"))
                    obj.Route_No = clsCommon.myCstr(dt.Rows(i)("route_no"))
                    obj.Milk_Type = clsCommon.myCstr(dt.Rows(i)("milk_type"))
                    obj.qty = clsCommon.myCdbl(dt.Rows(i)("qty"))
                    obj.fat = clsCommon.myCdbl(dt.Rows(i)("fat"))
                    obj.snf = clsCommon.myCdbl(dt.Rows(i)("snf"))
                    obj.fat_KG = clsCommon.myCdbl(dt.Rows(i)("fat_Kg"))
                    obj.snf_KG = clsCommon.myCdbl(dt.Rows(i)("snf_Kg"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(i)("Rate"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.water = clsCommon.myCdbl(dt.Rows(i)("water"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arrObj
    End Function

End Class

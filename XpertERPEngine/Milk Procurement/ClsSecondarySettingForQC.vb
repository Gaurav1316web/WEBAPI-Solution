Imports System.Data.SqlClient
Imports common
Public Class ClsSecondarySettingForQC

#Region "variables"
    Public Document_No As String = String.Empty
    Public Document_Date As Date = Nothing
    Public QC_No As String = String.Empty
    Public QC_In_Date_Time As Date = Nothing
    Public QC_Out_Date_Time As Date = Nothing
    Public Gate_Entry_No As String = String.Empty
    Public Gate_Entry_Date_And_Time As Date = Nothing
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public Dip_Value As Double = 0
    Public DeductionAmount As Double = 0
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
    Public location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Vendor_Desc As String = String.Empty
    Public Receipt_Control_FAT As Double = 0
    Public Receipt_Control_SNF As Double = 0
    Public Dispatch_Control_FAT As Double = 0
    Public Dispatch_Control_SNF As Double = 0
    Public Posted As Double = 0
    Public arrSecondarySettingDetail As List(Of ClsSecondarySettingForQCDetail) = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As ClsSecondarySettingForQC, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As ClsSecondarySettingForQC, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Bulk Procurement", "Secondary Setting for QC", obj.location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_SECONDARY_SETTING_QC_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SecondarySettingForQC, clsDocTransactionType.BulkProc, obj.location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "QC_In_Date_Time", clsCommon.GetPrintDate(obj.QC_In_Date_Time, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "QC_Out_Date_Time", clsCommon.GetPrintDate(obj.QC_Out_Date_Time, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date_And_Time", clsCommon.GetPrintDate(obj.Gate_Entry_Date_And_Time, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "QC_In_Date_Time", clsCommon.GetPrintDate(obj.QC_In_Date_Time, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "QC_Out_Date_Time", clsCommon.GetPrintDate(obj.QC_Out_Date_Time, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date_And_Time", clsCommon.GetPrintDate(obj.Gate_Entry_Date_And_Time, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "QC_No", obj.QC_No)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)

            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Desc", obj.Vendor_Desc)
           
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_FAT", obj.Receipt_Control_FAT)
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_SNF", obj.Receipt_Control_SNF)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Control_FAT", obj.Dispatch_Control_FAT)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Control_SNF", obj.Dispatch_Control_SNF)
            clsCommon.AddColumnsForChange(coll, "Dip_Value", obj.Dip_Value)
            clsCommon.AddColumnsForChange(coll, "DeductionAmount", obj.DeductionAmount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

           
           
            If isNewEntry Then
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_SECONDARY_SETTING_QC_HEAD where QC_No ='" & obj.QC_No & "' ", trans) < 1 Then

                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_SETTING_QC_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("Document already created for QC No " & obj.QC_No & "")

                End If
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_SETTING_QC_HEAD", OMInsertOrUpdate.Update, "TSPL_SECONDARY_SETTING_QC_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            ClsSecondarySettingForQCDetail.saveData(obj.arrSecondarySettingDetail, obj.Document_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsSecondarySettingForQC
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsSecondarySettingForQC
        Dim obj As ClsSecondarySettingForQC = Nothing
        Dim Arr As List(Of ClsSecondarySettingForQC) = Nothing
        Dim qry As String = "Select * from TSPL_SECONDARY_SETTING_QC_HEAD where 2=2  "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Document_No = (select MIN(Document_No) from TSPL_SECONDARY_SETTING_QC_HEAD WHERE 1=1 " + whrclas + " and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Document_No = (select Max(Document_No) from TSPL_SECONDARY_SETTING_QC_HEAD WHERE 1=1 " + whrclas + " and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Document_No = (select Min(Document_No) from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No>'" + strCode + "' " + whrclas + " and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Document_No = (select Max(Document_No) from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No<'" + strCode + "' " + whrclas + " and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj = New ClsSecondarySettingForQC()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
            obj.QC_In_Date_Time = clsCommon.myCDate(dt.Rows(0)("QC_In_Date_Time"))
            obj.QC_Out_Date_Time = clsCommon.myCDate(dt.Rows(0)("QC_Out_Date_Time"))

            obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
            obj.Gate_Entry_Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Gate_Entry_Date_And_Time"))

            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))

            obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"))

            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))

            obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))

            obj.Receipt_Control_FAT = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_FAT"))
            obj.Receipt_Control_SNF = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_SNF"))
            obj.Dispatch_Control_FAT = clsCommon.myCdbl(dt.Rows(0)("Dispatch_Control_FAT"))
            obj.Dispatch_Control_SNF = clsCommon.myCdbl(dt.Rows(0)("Dispatch_Control_SNF"))

            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.arrSecondarySettingDetail = ClsSecondarySettingForQCDetail.getData(obj.Document_No, trans)

        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
         
            qry = "delete from TSPL_SECONDARY_SETTING_QC_DETAIL where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Posted from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            ''richa  TEC/21/01/19-000404 as per shruti mam

            Dim obj As ClsSecondarySettingForQC = ClsSecondarySettingForQC.GetData(strCode, arrLoc, NavigatorType.Current, trans)

            Dim SRNNo As String = clsDBFuncationality.getSingleValue("select ISNULL(SRN_NO,'') from TSPL_Bulk_MILK_SRN where Gate_Entry_No='" + obj.Gate_Entry_No + "'", trans)

            Qry = "SELECT distinct DOC_NO FROM tspl_Bulk_milk_purchase_Invoice_Detail  where  SRN_NO='" + SRNNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current SRN is used in following invoice -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DOC_NO"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select top 1 SRN_Return_NO from TSPL_Bulk_Milk_SRN_Return  where  SRN_NO='" + SRNNo + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current SRN is used in following Bulk Milk Sale Return -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("SRN_Return_NO"))
                Next
                Throw New Exception(Qry)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='BM-SR' and Source_Doc_No='" + SRNNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If


            Dim strMilkRGP As String = clsDBFuncationality.getSingleValue("select top 1 rgp_no from TSPL_MILK_RGP_detail where Bulk_Milk_SRN_Code='" & SRNNo & "'", trans)
            If clsCommon.myLen(strMilkRGP) > 0 Then
                clsMilkRGPHead.ReverseAndUnpost(strMilkRGP, trans)
                Qry = "delete from TSPL_MILK_RGP_detail where rgp_no= '" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_MILK_RGP_HEAD where rgp_no='" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_MR_ISSUE_QC_DETAIL where Issue_Code='" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            Dim strMilkJobWorkTransfer As String = clsDBFuncationality.getSingleValue("select top 1 Document_Code from TSPL_MILK_JOBWORK_TRANSFER_HEAD where SRN_NO='" & SRNNo & "'", trans)
            If clsCommon.myLen(strMilkJobWorkTransfer) > 0 Then
                clsMilkJobworkTransfer.ReverseAndUnpost(strMilkJobWorkTransfer, trans)
            End If

            clsBatchInventoryNew.DeleteData("BulkSRN", SRNNo, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + SRNNo + "' and Trans_Type='BulkSRN'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_TRANSACTION_APPROVAL where Program_Code='M-SRN-B' and Document_No='" & SRNNo & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            Qry = "delete from TSPL_SRN_Parameter_Range_Detail where SRN_NO='" + SRNNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_BULK_MILK_SRN_CHEMBER_DETAILS where SRN_NO='" + SRNNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_Bulk_MILK_SRN where SRN_NO='" + SRNNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            Qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + SRNNo + "' and trans_code='" + clsCommon.myCstr(clsUserMgtCode.frmBulkMilkSRN) + "' and is_reverse=0"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            ''--------
            Qry = "update TSPL_SECONDARY_SETTING_QC_HEAD set Posted = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("QC No not found to Post")
            End If
            Dim obj As ClsSecondarySettingForQC = ClsSecondarySettingForQC.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_SECONDARY_SETTING_QC_HEAD set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class ClsSecondarySettingForQCDetail
    Public Document_No As String = String.Empty
    Public QCFatPer As Double = 0
    Public QCSNFPer As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public NetWeight As Double = 0
    Public AdditinalWeightper As Double = 0
    Public CalculatedAdditionalWeight As Double = 0
    Public TotalWeight As Double = 0
    Public LINE_NO As Integer = 0
    Public CHAMBER_DESC As String = ""
    Public Shared Function saveData(ByVal arrObj As List(Of ClsSecondarySettingForQCDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsSecondarySettingForQCDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.CHAMBER_DESC)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "QCFatPer", obj.QCFatPer)
                    clsCommon.AddColumnsForChange(coll, "QCSNFPer", obj.QCSNFPer)
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)

                    clsCommon.AddColumnsForChange(coll, "NetWeight", obj.NetWeight)
                    clsCommon.AddColumnsForChange(coll, "AdditinalWeightper", obj.AdditinalWeightper)
                    clsCommon.AddColumnsForChange(coll, "CalculatedAdditionalWeight", obj.CalculatedAdditionalWeight)
                    clsCommon.AddColumnsForChange(coll, "TotalWeight", obj.TotalWeight)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_SETTING_QC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
            arrObj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
   
    Public Shared Function getData(ByVal strDocument_No As String, ByVal trans As SqlTransaction) As List(Of ClsSecondarySettingForQCDetail)
        Try
            Dim arrObj As List(Of ClsSecondarySettingForQCDetail) = Nothing
            Dim obj As ClsSecondarySettingForQCDetail = Nothing
            Dim qry As String = "select * from TSPL_SECONDARY_SETTING_QC_DETAIL where Document_No='" & strDocument_No & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsSecondarySettingForQCDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsSecondarySettingForQCDetail()
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    obj.CHAMBER_DESC = clsCommon.myCstr(dt.Rows(i)("CHAMBER_DESC"))
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.QCFatPer = clsCommon.myCdbl(dt.Rows(i)("QCFatPer"))
                    obj.QCSNFPer = clsCommon.myCdbl(dt.Rows(i)("QCSNFPer"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))

                    obj.NetWeight = clsCommon.myCdbl(dt.Rows(i)("NetWeight"))
                    obj.AdditinalWeightper = clsCommon.myCdbl(dt.Rows(i)("AdditinalWeightper"))
                    obj.CalculatedAdditionalWeight = clsCommon.myCdbl(dt.Rows(i)("CalculatedAdditionalWeight"))
                    obj.TotalWeight = clsCommon.myCdbl(dt.Rows(i)("TotalWeight"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
   

End Class

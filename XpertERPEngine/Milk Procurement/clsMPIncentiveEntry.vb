Imports System.Data.SqlClient
Imports common
Public Class clsMPIncentiveEntry
#Region "variables"

    Public Document_Code As String = Nothing
    Public Document_Date As Date
    Public MCC_Code As String = ""
    Public MCC_Name As String = ""
    Public From_Date As Date
    Public To_Date As Date
    Public Incetive_Rate As Decimal = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As Date? = Nothing
    Public arr As List(Of clsMPIncentiveEntryDetail) = Nothing
    Public FATSNFPer As Integer = 0 ''0-FATSNFKg/1-FATSNFPer/2-NA

#End Region
    Public Shared Function SaveData(ByVal obj As clsMPIncentiveEntry, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsMPIncentiveEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MPIncentiveEntry, obj.MCC_Code, obj.Document_Date, trans)
            Dim arrPKID As New ArrayList
            For ii As Integer = 0 To obj.arr.Count - 1
                If obj.arr(ii).PK_Id > 0 Then
                    arrPKID.Add(obj.arr(ii).PK_Id)
                End If
            Next
            qry = "delete from TSPL_MP_INCENTIVE_ENTRY_DETAIL where Document_Code='" & obj.Document_Code & "' "
            If arrPKID IsNot Nothing AndAlso arrPKID.Count > 0 Then
                qry += " And PK_Id Not in (" + clsCommon.GetMulcallString(arrPKID) + ")"
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Incetive_Rate", obj.Incetive_Rate)
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "FATSNFPer", obj.FATSNFPer)
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MPIncentiveEntry, "", obj.MCC_Code)
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCENTIVE_ENTRY_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCENTIVE_ENTRY_HEAD", OMInsertOrUpdate.Update, "TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsMPIncentiveEntryDetail.saveData(obj.FATSNFPer, obj.arr, obj.Document_Code, obj.MCC_Code, obj.From_Date, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMPIncentiveEntry
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMPIncentiveEntry
        Return GetData(strCode, NavType, trans, True)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal PickDetailsData As Boolean) As clsMPIncentiveEntry
        Dim obj As clsMPIncentiveEntry = Nothing
        Dim Arr As List(Of clsMPIncentiveEntry) = Nothing
        Dim qry As String = "Select TSPL_MP_INCENTIVE_ENTRY_HEAD.*,TSPL_MCC_MASTER.MCC_NAME from TSPL_MP_INCENTIVE_ENTRY_HEAD " &
            " Left Outer Join TSPL_MCC_MASTER on TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code=TSPL_MCC_MASTER.MCC_CODE  where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_MP_INCENTIVE_ENTRY_HEAD WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code = (select Max(Document_Code) from TSPL_MP_INCENTIVE_ENTRY_HEAD WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code = (select Min(Document_Code) from TSPL_MP_INCENTIVE_ENTRY_HEAD where Document_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code = (select Max(Document_Code) from TSPL_MP_INCENTIVE_ENTRY_HEAD where Document_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMPIncentiveEntry()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Incetive_Rate = clsCommon.myCdbl(dt.Rows(0)("Incetive_Rate"))
            obj.FATSNFPer = clsCommon.myCdbl(dt.Rows(0)("FATSNFPer"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If PickDetailsData Then
                obj.arr = clsMPIncentiveEntryDetail.getData(obj.Document_Code, trans)
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 Document_Date,MCC_Code from TSPL_MP_INCENTIVE_ENTRY_HEAD  where Document_Code='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MPIncentiveEntry, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
            End If
            Dim qry As String = "select PK_Id from TSPL_MP_INCENTIVE_ENTRY_DETAIL where Document_Code ='" + strDocNo + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("PK_Id")), "TSPL_MP_INCENTIVE_ENTRY_DETAIL", "PK_Id", trans)
                Next
            End If


            qry = "delete from TSPL_MP_INCENTIVE_ENTRY_DETAIL where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MP_INCENTIVE_ENTRY_HEAD where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code as Code,Convert(varchar,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103) as Date
          ,TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC NAME] ,Convert(varchar,TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date,103) as [From Date],Convert(varchar,TSPL_MP_INCENTIVE_ENTRY_HEAD.To_Date,103) as [To Date]
          ,TSPL_MP_INCENTIVE_ENTRY_HEAD.Incetive_Rate as [Incetive Rate],case when isnull(Status,0)=0 then 'Pending' else 'Approved' end as Status 
          ,TSPL_MP_INCENTIVE_ENTRY_HEAD.Incetive_Rate 
          from TSPL_MP_INCENTIVE_ENTRY_HEAD 
          Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE=TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code  "
        str = clsCommon.ShowSelectForm("MPInc#F", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If


            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsMPIncentiveEntry = clsMPIncentiveEntry.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MPIncentiveEntry, obj.MCC_Code, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_MP_INCENTIVE_ENTRY_HEAD set Status=1, Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'If obj.FATSNFPer < 2 Then
            '    Dim arrVLC As New List(Of String)
            '    For Each objtr As clsMPIncentiveEntryDetail In obj.arr
            '        If Not arrVLC.Contains(objtr.VLC_Code) Then
            '            arrVLC.Add(objtr.VLC_Code)
            '        End If
            '    Next
            '    For Each strVLC As String In arrVLC
            '        Dim objMH As ClsVLCDataUploaderManual = Nothing
            '        For Each objtr As clsMPIncentiveEntryDetail In obj.arr
            '            ''Save Data in Uploader
            '            If clsCommon.CompairString(objtr.VLC_Code, strVLC) = CompairStringResult.Equal Then
            '                If objMH Is Nothing Then
            '                    objMH = New ClsVLCDataUploaderManual()
            '                    objMH.Document_Code = ""
            '                    objMH.Document_Date = obj.To_Date
            '                    objMH.Route_Code = clsDBFuncationality.getSingleValue("select Route_Code from TSPL_VLC_MASTER_HEAD where VLC_Code='" + strVLC + "'", trans)
            '                    objMH.VLC_Code = objtr.VLC_Code
            '                    objMH.Shift = "M"
            '                    objMH.Dock_Collection_Milk_Type = ""
            '                    objMH.arrVLCDetail = New List(Of ClsVLCDataUploaderManualdetail)
            '                End If
            '                Dim objMHTR As New ClsVLCDataUploaderManualdetail
            '                objMHTR = New ClsVLCDataUploaderManualdetail()
            '                objMHTR.Farmer_Code = objtr.MP_Code
            '                objMHTR.Unit_Code = objtr.UOM
            '                objMHTR.Qty = objtr.Qty
            '                objMHTR.FatPer = objtr.FAT
            '                objMHTR.SNFPer = objtr.SNF
            '                objMHTR.Amount = 0
            '                objMHTR.Rate = 0
            '                'objMHTR.Reject_Type = clsCommon.myCstr(grow.Cells(colRejectRejectType).Value)
            '                objMH.arrVLCDetail.Add(objMHTR)
            '            End If
            '            ''End of Uploader
            '        Next
            '        ClsVLCDataUploaderManual.SaveData(objMH, True, trans)
            '    Next
            'End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Sub MultipleDateSingleExport(ByRef frm As RadForm)
        Try
            Dim qry As String = "select '01/Dec/2022' as Date,'' as [DCS Uploader No],'' as [MP Uploader No],'' as [MP Name],'' as [IFSC],'' as [Account No],0.00 as Qty"
            Dim ListImpExpColumnsMandatory As List(Of String) = New List(Of String)({"Date", "DCS Uploader No", "MP Uploader No", "MP Name", "IFSC", "Account No", "Qty"})
            transportSql.ExporttoExcel(qry, "", "", frm, ListImpExpColumnsMandatory)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
        End Try
    End Sub
    Public Shared Sub MultipleDateSingleImport(ByRef frm As RadForm)
        Dim gv As New RadGridView()
        frm.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim ii As Integer = 1
        Dim indxSuccess As Integer = 0
        Dim indxError As Integer = 0
        Dim arr As New Dictionary(Of String, clsMPIncentiveEntry)
        Dim qry As String
        Dim dtInvalidRows As New DataTable()
        If transportSql.importExcel(gv, "Date", "DCS Uploader No", "MP Uploader No", "MP Name", "IFSC", "Account No", "Qty") Then
            Try
                clsCommon.ProgressBarPercentShow()
                Dim SettMPIncentiveEntryIncentiveRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryIncentiveRate, clsFixedParameterCode.MPIncentiveEntryIncentiveRate, Nothing))
                For kk As Integer = 0 To gv.Columns.Count - 1
                    dtInvalidRows.Columns.Add(gv.Columns(kk).Name, GetType(String))
                Next
                dtInvalidRows.Columns.Add("Error", GetType(String))
                For Each grow As GridViewRowInfo In gv.Rows
                    Try
                        ii += 1
                        clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (gv.Rows.Count)), "Validating Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(gv.Rows.Count) & "")
                        If clsCommon.myLen(grow.Cells("Date").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("DCS Uploader No").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("MP Uploader No").Value) > 0 Then
                            Dim objtemp As New clsTempFATSNFAmt
                            objtemp.IDate = clsCommon.myCDate(grow.Cells("Date").Value)
                            objtemp.VLCUploader = clsCommon.myCstr(grow.Cells("DCS Uploader No").Value)
                            objtemp.MPUploader = clsCommon.myCstr(grow.Cells("MP Uploader No").Value)
                            objtemp.MPName = clsCommon.myCstr(grow.Cells("MP Name").Value)
                            objtemp.IFSC = clsCommon.myCstr(grow.Cells("IFSC").Value)
                            objtemp.AccountNo = clsCommon.myCstr(grow.Cells("Account No").Value)
                            objtemp.Qty = clsCommon.myCDecimal(grow.Cells("Qty").Value)

                            qry = "select VLC_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + objtemp.VLCUploader + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Try
                                    qry = clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_MASTER  where IsDefault=1")
                                    If clsCommon.myLen(qry) <= 0 Then
                                        Throw New Exception("Please Define Default BMC")
                                    End If
                                    clsfrmVLCMaster.CreateNewVSP_VLC(objtemp.VLCUploader, qry)
                                Catch ex As Exception
                                    Throw New Exception("Error While Creating DCS " + objtemp.VLCUploader + " " + ex.Message)
                                End Try
                                qry = "select VLC_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + objtemp.VLCUploader + "'"
                                dt = clsDBFuncationality.GetDataTable(qry)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("Invalid DCS No " + objtemp.VLCUploader)
                                End If
                            End If
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                                Throw New Exception("DCS No " + objtemp.VLCUploader + " Mapped more than one MCC/BMC")
                            End If

                            objtemp.VLC = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                            objtemp.MCC = clsCommon.myCstr(dt.Rows(0)("MCC"))
                            If clsCommon.myLen(objtemp.MCC) <= 0 Then
                                Throw New Exception("DCSNo " + objtemp.VLCUploader + " is not mapped with any MCC/BMC")
                            End If

                            qry = "select MP_Code from TSPL_MP_MASTER where VLC_Code='" + objtemp.VLC + "' and MP_Code_VLC_Uploader='" + objtemp.MPUploader + "'"
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then

                                Try
                                    clsMpMaster.SaveDefaultMP(objtemp.MPUploader, objtemp.MPName, objtemp.IFSC, objtemp.AccountNo, objtemp.VLC)
                                Catch ex As Exception
                                    Throw New Exception("Error While Creating MP " + objtemp.MPUploader + " " + ex.Message)
                                End Try

                                qry = "select MP_Code from TSPL_MP_MASTER where VLC_Code='" + objtemp.VLC + "' and MP_Code_VLC_Uploader='" + objtemp.MPUploader + "'"
                                dt = clsDBFuncationality.GetDataTable(qry)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("Invalid MP No " + objtemp.MPUploader)
                                End If
                            End If
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                                Throw New Exception("MP No " + objtemp.MPUploader + " Defined more than ones")
                            End If
                            objtemp.MP = clsCommon.myCstr(dt.Rows(0)("MP_Code"))

                            Dim UniqueCombination As String = objtemp.MCC + clsCommon.GetPrintDate(objtemp.IDate, "dd/MM/yyyy")
                            If Not arr.ContainsKey(UniqueCombination) Then
                                qry = "select Document_Code from TSPL_MP_INCENTIVE_ENTRY_HEAD where From_Date ='" + clsCommon.GetPrintDate(objtemp.IDate, "dd/MMM/yyyy") + "' and MCC_Code='" + objtemp.MCC + "'"
                                dt = clsDBFuncationality.GetDataTable(qry)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    Throw New Exception("MP Incentive Entry  [" + clsCommon.myCstr(dt.Rows(0)("Document_Code")) + "] is already exist for Date [" + clsCommon.GetPrintDate(objtemp.IDate, "dd/MM/yyyy") + "]  and MCC [" + objtemp.MCC + "]")
                                End If

                                Dim obj As New clsMPIncentiveEntry()
                                obj.MCC_Code = objtemp.MCC
                                obj.From_Date = objtemp.IDate
                                obj.To_Date = clsPaymentCycleMaster.GetPaymentCycleToDate(objtemp.MCC, objtemp.IDate)
                                obj.Document_Date = obj.To_Date
                                obj.Incetive_Rate = SettMPIncentiveEntryIncentiveRate
                                obj.FATSNFPer = 2
                                obj.arr = New List(Of clsMPIncentiveEntryDetail)
                                arr.Add(UniqueCombination, obj)
                            End If

                            Dim objtr As New clsMPIncentiveEntryDetail
                            objtr.SNo = arr(UniqueCombination).arr.Count + 1
                            objtr.VLC_Code = objtemp.VLC
                            objtr.MP_Code = objtemp.MP

                            objtr.MP_Account_No = objtemp.AccountNo
                            objtr.MP_IFSC_No = objtemp.IFSC
                            objtr.Qty = objtemp.Qty
                            objtr.UOM = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & objtemp.MCC & "'")
                            objtr.Amount = Math.Ceiling(objtr.Qty * SettMPIncentiveEntryIncentiveRate)
                            objtr.Amount_Actual = (objtr.Qty * SettMPIncentiveEntryIncentiveRate)
                            objtr.Total_Amount = objtr.Amount
                            arr(UniqueCombination).arr.Add(objtr)
                            indxSuccess += 1
                        End If
                    Catch ex As Exception
                        Dim drE As DataRow = dtInvalidRows.NewRow
                        For kk As Integer = 0 To gv.Columns.Count - 1
                            drE(gv.Columns(kk).Name) = clsCommon.myCstr(grow.Cells(kk).Value)
                        Next
                        drE("Error") = ("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                        dtInvalidRows.Rows.Add(drE)

                        indxError += 1
                        'Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)

                    End Try
                Next

                clsCommon.ProgressBarPercentHide()

            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
            End Try

            Try
                If dtInvalidRows IsNot Nothing AndAlso dtInvalidRows.Rows.Count > 0 Then
                    qry = "Do You want to Save Invalid Data"
                    If clsCommon.MyMessageBoxShow(frm, qry, frm.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        transportSql.ExporttoExcel(dtInvalidRows, frm)
                    End If
                End If
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    qry = "Valid Row [" + clsCommon.myCstr(indxSuccess) + "]" + Environment.NewLine + "Invalid Rows [" + clsCommon.myCstr(indxError) + "] " + Environment.NewLine + "Total Documents To be Generate [" + clsCommon.myCstr(arr.Count) + "]" + Environment.NewLine + "Do You want to Proceed"
                    If clsCommon.MyMessageBoxShow(frm, qry, frm.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        clsCommon.ProgressBarPercentShow()
                        ii = 0
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            For Each key As String In arr.Keys
                                ii += 1
                                clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (arr.Count)), "Saving Document..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(arr.Count) & "")
                                Dim obj As clsMPIncentiveEntry = arr.Item(key)
                                clsMPIncentiveEntry.SaveData(obj, True, trans)
                            Next
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        Finally
                            clsCommon.ProgressBarPercentHide()
                        End Try
                    End If
                Else
                    Throw New Exception("No Valid Rows Found to Save")
                End If
                clsCommon.MyMessageBoxShow(frm, "Data Transfer Completed!", frm.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
            End Try
        End If
        frm.Controls.Remove(gv)
    End Sub

End Class

Public Class clsMPIncentiveEntryDetail
#Region "Variable"
    Public PK_Id As Integer = Nothing

    Public Document_Code As String = Nothing
    Public SNo As Integer

    Public VLC_Code As String = Nothing
    Public VLC_Uploader_Code As String = Nothing ''Not a Table Column
    Public VLC_Name As String = Nothing ''Not a Table Column

    Public MP_Code As String = Nothing
    Public MP_Uploader_Code As String = Nothing ''Not a Table Column
    Public MP_Name As String = Nothing ''Not a Table Column
    Public MP_Account_No As String = Nothing
    Public MP_Bank As String = Nothing

    Public Qty As Decimal = 0
    Public UOM As String = Nothing
    Public Amount As Decimal = 0
    Public Amount_Actual As Decimal = 0

    Public MP_IFSC_No As String = Nothing
    Public MP_Phone_No As String = Nothing
    Public MP_Aadhar_No As String = Nothing

    Public FAT As Decimal = 0
    Public FAT_Kg As Decimal = 0
    Public SNF As Decimal = 0
    Public SNF_Kg As Decimal = 0

    Public Pashu_Aahar_Qty As Decimal = 0
    Public Pashu_Aahar_Amount As Decimal = 0

    Public Mineral_Mixture_Qty As Decimal = 0
    Public Mineral_Mixture_Amount As Decimal = 0

    Public Sailej_Qty As Decimal = 0
    Public Sailej_Amount As Decimal = 0

    Public Rahat_Kampekat_Feed_Qty As Decimal = 0
    Public Rahat_Kampekat_Feed_Amount As Decimal = 0

    Public Total_Amount As Decimal = 0


#End Region
    Public Shared Function saveData(ByVal isFATSNFPer As Integer, ByVal arrObj As List(Of clsMPIncentiveEntryDetail), ByVal strDocNo As String, ByVal strMCCCode As String, ByVal FromDate As Date, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim SettMPIncentiveEntryMaxMilkLimit As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryMaxMilkLimit, clsFixedParameterCode.MPIncentiveEntryMaxMilkLimit, trans))
            Dim qry As String = ""
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsMPIncentiveEntryDetail In arrObj
                    If obj.Qty > SettMPIncentiveEntryMaxMilkLimit Then
                        Throw New Exception("Farmer [" + obj.MP_Code + "] Milk Qty cant be more than [" + clsCommon.myCstr(SettMPIncentiveEntryMaxMilkLimit) + "]")
                    End If
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                    clsCommon.AddColumnsForChange(coll, "MP_Code", obj.MP_Code)
                    clsCommon.AddColumnsForChange(coll, "MP_Account_No", obj.MP_Account_No)
                    clsCommon.AddColumnsForChange(coll, "MP_Bank", obj.MP_Bank)
                    If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryApplyMonthly, clsFixedParameterCode.MPIncentiveEntryApplyMonthly, trans)) > 0) Then
                        clsCommon.AddColumnsForChange(coll, "Cycle_No", 1)
                    Else
                        qry = "select TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE from TSPL_MCC_MASTER 
left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle
where MCC_Code='" + strMCCCode + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Please set Payment Cycle of MCC [" + strMCCCode + "]")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PC_TYPE")), "Day") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PC_TYPE")), "Week") = CompairStringResult.Equal Then
                            Dim NoOfDays As Integer = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                            Dim CycleNo As Integer = (FromDate.Day / NoOfDays) + 1
                            clsCommon.AddColumnsForChange(coll, "Cycle_No", CycleNo)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PC_TYPE")), "Month") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PC_TYPE")), "Year") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Cycle_No", 1)
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Cycle_Month", FromDate.Month)
                    clsCommon.AddColumnsForChange(coll, "Cycle_Year", FromDate.Year)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Amount_Actual", obj.Amount_Actual)
                    If isFATSNFPer = 1 Then
                        clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                        clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                        obj.FAT_Kg = Math.Round(obj.Qty * obj.FAT / 100, 3)
                        obj.SNF_Kg = Math.Round(obj.Qty * obj.SNF / 100, 3)
                        clsCommon.AddColumnsForChange(coll, "FAT_Kg", obj.FAT_Kg)
                        clsCommon.AddColumnsForChange(coll, "SNF_Kg", obj.SNF_Kg)
                    ElseIf isFATSNFPer = 0 Then
                        clsCommon.AddColumnsForChange(coll, "FAT_Kg", obj.FAT)
                        clsCommon.AddColumnsForChange(coll, "SNF_Kg", obj.SNF)

                        obj.FAT_Kg = Math.Round(clsCommon.myCDivide(obj.FAT * 100, obj.Qty), 1)
                        obj.SNF_Kg = Math.Round(clsCommon.myCDivide(obj.FAT * 100, obj.Qty), 2)

                        clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT_Kg)
                        clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF_Kg)
                    End If
                    clsCommon.AddColumnsForChange(coll, "MP_IFSC_No", obj.MP_IFSC_No)
                    clsCommon.AddColumnsForChange(coll, "MP_Phone_No", obj.MP_Phone_No)
                    clsCommon.AddColumnsForChange(coll, "MP_Aadhar_No", obj.MP_Aadhar_No)
                    clsCommon.AddColumnsForChange(coll, "Modified_Entry_Source", "ERP")
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))


                    clsCommon.AddColumnsForChange(coll, "Pashu_Aahar_Qty", obj.Pashu_Aahar_Qty)
                    clsCommon.AddColumnsForChange(coll, "Pashu_Aahar_Amount", obj.Pashu_Aahar_Amount)
                    clsCommon.AddColumnsForChange(coll, "Mineral_Mixture_Qty", obj.Mineral_Mixture_Qty)
                    clsCommon.AddColumnsForChange(coll, "Mineral_Mixture_Amount", obj.Mineral_Mixture_Amount)
                    clsCommon.AddColumnsForChange(coll, "Sailej_Qty", obj.Sailej_Qty)
                    clsCommon.AddColumnsForChange(coll, "Sailej_Amount", obj.Sailej_Amount)
                    clsCommon.AddColumnsForChange(coll, "Rahat_Kampekat_Feed_Qty", obj.Rahat_Kampekat_Feed_Qty)
                    clsCommon.AddColumnsForChange(coll, "Rahat_Kampekat_Feed_Amount", obj.Rahat_Kampekat_Feed_Amount)
                    clsCommon.AddColumnsForChange(coll, "Total_Amount", obj.Total_Amount)

                    If obj.PK_Id > 0 Then
                        qry = "select 1 from TSPL_DBT_NEFT_DETAIL where Against_MP_Incentive_TR='" + clsCommon.myCstr(obj.PK_Id) + "'
union all
select 1 from TSPL_DBT_NEFT_DETAIL_INVALID where Against_MP_Incentive_TR='" + clsCommon.myCstr(obj.PK_Id) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Continue For
                        End If
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCENTIVE_ENTRY_DETAIL", OMInsertOrUpdate.Update, "PK_Id=" + clsCommon.myCstr(obj.PK_Id) + " ", trans)
                    Else
                        clsCommon.AddColumnsForChange(coll, "Created_Entry_Source", "ERP")
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_INCENTIVE_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                        qry = "select max(PK_Id) from TSPL_MP_INCENTIVE_ENTRY_DETAIL where Document_Code ='" + strDocNo + "'"
                        obj.PK_Id = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    End If
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.PK_Id, "TSPL_MP_INCENTIVE_ENTRY_DETAIL", "PK_Id", trans)
                Next

                qry = "update tspl_mp_incentive_entry_Detail set MP_Bank=xx.BankName,MP_Account_No=xx.AccountNO,MP_Phone_No=xx.Telphone,MP_Aadhar_No=xx.Fax, MP_IFSC_No=xx.IFCICode from (
select MP_Code,BankName,AccountNO,Telphone,Fax,IFCICode from TSPL_MP_MASTER
)xx inner join tspl_mp_incentive_entry_Detail on tspl_mp_incentive_entry_Detail.MP_Code=xx.MP_Code
where tspl_mp_incentive_entry_Detail.Document_Code='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsMPIncentiveEntryDetail)
        Try
            Dim arrObj As List(Of clsMPIncentiveEntryDetail) = Nothing
            Dim obj As clsMPIncentiveEntryDetail = Nothing
            Dim qry As String = "Select TSPL_MP_INCENTIVE_ENTRY_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.MP_Name 
,TSPL_MP_MASTER.BankName as MPBankName,TSPL_MP_MASTER.AccountNO as MPAccountNO,TSPL_MP_MASTER.Telphone as MPTelphone,TSPL_MP_MASTER.Fax as MPFax,TSPL_MP_MASTER.IFCICode as MPIFCICode
from TSPL_MP_INCENTIVE_ENTRY_DETAIL 
Left Outer Join TSPL_MP_MASTER On TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code   
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
where TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMPIncentiveEntryDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMPIncentiveEntryDetail()
                    obj.PK_Id = clsCommon.myCdbl(dt.Rows(i)("PK_Id"))
                    obj.Document_Code = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
                    obj.SNo = i + 1
                    obj.VLC_Code = clsCommon.myCstr(dt.Rows(i)("VLC_Code"))
                    obj.VLC_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("VLC_Code_VLC_Uploader"))
                    obj.VLC_Name = clsCommon.myCstr(dt.Rows(i)("VLC_Name"))
                    obj.MP_Code = clsCommon.myCstr(dt.Rows(i)("MP_Code"))
                    obj.MP_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("MP_Code_VLC_Uploader"))
                    obj.MP_Name = clsCommon.myCstr(dt.Rows(i)("MP_Name"))
                    obj.MP_Account_No = clsCommon.myCstr(dt.Rows(i)("MPAccountNO"))
                    obj.MP_Bank = clsCommon.myCstr(dt.Rows(i)("MPBankName"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.Amount_Actual = clsCommon.myCdbl(dt.Rows(i)("Amount_Actual"))
                    obj.FAT = clsCommon.myCdbl(dt.Rows(i)("FAT"))
                    obj.FAT_Kg = clsCommon.myCdbl(dt.Rows(i)("FAT_Kg"))
                    obj.SNF = clsCommon.myCdbl(dt.Rows(i)("SNF"))
                    obj.SNF_Kg = clsCommon.myCdbl(dt.Rows(i)("SNF_Kg"))
                    obj.MP_IFSC_No = clsCommon.myCstr(dt.Rows(i)("MPIFCICode"))
                    obj.MP_Phone_No = clsCommon.myCstr(dt.Rows(i)("MPTelphone"))
                    obj.MP_Aadhar_No = clsCommon.myCstr(dt.Rows(i)("MPFax"))

                    obj.Pashu_Aahar_Qty = clsCommon.myCDecimal(dt.Rows(i)("Pashu_Aahar_Qty"))
                    obj.Pashu_Aahar_Amount = clsCommon.myCDecimal(dt.Rows(i)("Pashu_Aahar_Amount"))
                    obj.Mineral_Mixture_Qty = clsCommon.myCDecimal(dt.Rows(i)("Mineral_Mixture_Qty"))
                    obj.Mineral_Mixture_Amount = clsCommon.myCDecimal(dt.Rows(i)("Mineral_Mixture_Amount"))
                    obj.Sailej_Qty = clsCommon.myCDecimal(dt.Rows(i)("Sailej_Qty"))
                    obj.Sailej_Amount = clsCommon.myCDecimal(dt.Rows(i)("Sailej_Amount"))
                    obj.Rahat_Kampekat_Feed_Qty = clsCommon.myCDecimal(dt.Rows(i)("Rahat_Kampekat_Feed_Qty"))
                    obj.Rahat_Kampekat_Feed_Amount = clsCommon.myCDecimal(dt.Rows(i)("Rahat_Kampekat_Feed_Amount"))
                    obj.Total_Amount = clsCommon.myCDecimal(dt.Rows(i)("Total_Amount"))

                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getQry(ByVal strDocNo As String, ByVal UOM As String, ByRef arrVLC As ArrayList) As String
        ''Important Note - Add/Remove Any column in qry do changes in both if and else part is mandatory

        Dim qry As String = ""
        If clsCommon.myLen(strDocNo) > 0 Then
            qry = "Select PK_Id as [" + clsMPIncetiveEntryColumns.colPKID + "], ROW_NUMBER() over (order by PK_Id) As [" + clsMPIncetiveEntryColumns.colSlNo + "], TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code As [" + clsMPIncetiveEntryColumns.colVLCCode + "],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [" + clsMPIncetiveEntryColumns.colVLCUploaderCode + "],TSPL_VLC_MASTER_HEAD.VLC_Name As [" + clsMPIncetiveEntryColumns.colVLCName + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code As [" + clsMPIncetiveEntryColumns.colMPCode + "],TSPL_MP_MASTER.MP_Code_VLC_Uploader As [" + clsMPIncetiveEntryColumns.colMPUploaderCode + "],TSPL_MP_MASTER.MP_Name As [" + clsMPIncetiveEntryColumns.colMPName + "],TSPL_MP_MASTER.BankName As [" + clsMPIncetiveEntryColumns.colMPBank + "],TSPL_MP_MASTER.AccountNO As [" + clsMPIncetiveEntryColumns.colMPAccountNo + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty As [" + clsMPIncetiveEntryColumns.colQty + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.UOM As [" + clsMPIncetiveEntryColumns.colUOM + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Amount As [" + clsMPIncetiveEntryColumns.colAmount + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Amount_Actual As [" + clsMPIncetiveEntryColumns.colAmountActual + "],TSPL_MP_MASTER.Telphone As [" + clsMPIncetiveEntryColumns.colMPPhoneNo + "],TSPL_MP_MASTER.Fax As [" + clsMPIncetiveEntryColumns.colMPAadharNo + "],TSPL_MP_MASTER.IFCICode As [" + clsMPIncetiveEntryColumns.colMPIFSCCode + "]
,(case when TSPL_MP_INCENTIVE_ENTRY_HEAD.FATSNFPer=0 then TSPL_MP_INCENTIVE_ENTRY_DETAIL.FAT_Kg else (case when TSPL_MP_INCENTIVE_ENTRY_HEAD.FATSNFPer=1 then TSPL_MP_INCENTIVE_ENTRY_DETAIL.FAT else null end ) end) as [" + clsMPIncetiveEntryColumns.colFAT + "]
,(case when TSPL_MP_INCENTIVE_ENTRY_HEAD.FATSNFPer=0 then TSPL_MP_INCENTIVE_ENTRY_DETAIL.SNF_Kg else (case when TSPL_MP_INCENTIVE_ENTRY_HEAD.FATSNFPer=1 then TSPL_MP_INCENTIVE_ENTRY_DETAIL.SNF else null end ) end) as [" + clsMPIncetiveEntryColumns.colSNF + "]
,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Pashu_Aahar_Qty as [" + clsMPIncetiveEntryColumns.colPashuAaharQty + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Pashu_Aahar_Amount as [" + clsMPIncetiveEntryColumns.colPashuAaharAmt + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Mineral_Mixture_Qty as [" + clsMPIncetiveEntryColumns.colMineralMixtureQty + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Mineral_Mixture_Amount as [" + clsMPIncetiveEntryColumns.colMineralMixtureAmt + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Sailej_Qty as [" + clsMPIncetiveEntryColumns.colSailejQty + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Sailej_Amount as [" + clsMPIncetiveEntryColumns.colSailejAmt + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Rahat_Kampekat_Feed_Qty as [" + clsMPIncetiveEntryColumns.colRahatKampekatFeedQty + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Rahat_Kampekat_Feed_Amount as [" + clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Total_Amount as [" + clsMPIncetiveEntryColumns.colTotAmount + "]
,case when TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR is null then 'N' else 'Y' end as [" + clsMPIncetiveEntryColumns.colDBTProcessed + "]
from TSPL_MP_INCENTIVE_ENTRY_DETAIL 
left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
Left Outer Join TSPL_MP_MASTER On TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code   
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
left outer join (select Against_MP_Incentive_TR  from TSPL_DBT_NEFT_DETAIL group by Against_MP_Incentive_TR  ) as TSPL_DBT_NEFT_DETAIL on TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR=TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id
where TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code='" + strDocNo + "' order by TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id"
        Else

            qry = "Select 0 as [" + clsMPIncetiveEntryColumns.colPKID + "], ROW_NUMBER() over (order by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,MP_Code) As [" + clsMPIncetiveEntryColumns.colSlNo + "], TSPL_VLC_MASTER_HEAD.VLC_Code As [" + clsMPIncetiveEntryColumns.colVLCCode + "],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [" + clsMPIncetiveEntryColumns.colVLCUploaderCode + "],TSPL_VLC_MASTER_HEAD.VLC_Name As [" + clsMPIncetiveEntryColumns.colVLCName + "],TSPL_MP_MASTER.MP_Code As [" + clsMPIncetiveEntryColumns.colMPCode + "],TSPL_MP_MASTER.MP_Code_VLC_Uploader As [" + clsMPIncetiveEntryColumns.colMPUploaderCode + "],TSPL_MP_MASTER.MP_Name As [" + clsMPIncetiveEntryColumns.colMPName + "],TSPL_MP_MASTER.BankName As [" + clsMPIncetiveEntryColumns.colMPBank + "],TSPL_MP_MASTER.AccountNO As [" + clsMPIncetiveEntryColumns.colMPAccountNo + "],0.0 As [" + clsMPIncetiveEntryColumns.colQty + "],'" + UOM + "' As [" + clsMPIncetiveEntryColumns.colUOM + "],0.0 As [" + clsMPIncetiveEntryColumns.colAmount + "],0.0 As [" + clsMPIncetiveEntryColumns.colAmountActual + "],TSPL_MP_MASTER.Telphone As [" + clsMPIncetiveEntryColumns.colMPPhoneNo + "],TSPL_MP_MASTER.Fax As [" + clsMPIncetiveEntryColumns.colMPAadharNo + "],TSPL_MP_MASTER.IFCICode As [" + clsMPIncetiveEntryColumns.colMPIFSCCode + "]
,0.0 as [" + clsMPIncetiveEntryColumns.colFAT + "]
,0.0 as [" + clsMPIncetiveEntryColumns.colSNF + "]
,0.0 as [" + clsMPIncetiveEntryColumns.colPashuAaharQty + "],0.0 as [" + clsMPIncetiveEntryColumns.colPashuAaharAmt + "],0.0 as [" + clsMPIncetiveEntryColumns.colMineralMixtureQty + "],0.0 as [" + clsMPIncetiveEntryColumns.colMineralMixtureAmt + "],0.0 as [" + clsMPIncetiveEntryColumns.colSailejQty + "],0.0 as [" + clsMPIncetiveEntryColumns.colSailejAmt + "],0.0 as [" + clsMPIncetiveEntryColumns.colRahatKampekatFeedQty + "],0.0 as [" + clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt + "],0.0 as [" + clsMPIncetiveEntryColumns.colTotAmount + "]
,'N' as [" + clsMPIncetiveEntryColumns.colDBTProcessed + "]
from TSPL_MP_MASTER 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
where 2=2 "
            If arrVLC Is Nothing OrElse arrVLC.Count <= 0 Then
                qry += " and 2=3 "
            Else
                qry += " and TSPL_MP_MASTER.VLC_Code in (" + clsCommon.GetMulcallString(arrVLC) + ")  "
            End If
        End If

        Return qry

    End Function
End Class

Public Class clsMPIncetiveEntryColumns
    Public Const colPKID As String = "PKID."
    Public Const colSlNo As String = "SNo."
    Public Const colVLCCode As String = "VLC Code"
    Public Const colVLCUploaderCode As String = "VLC"
    Public Const colVLCName As String = "VLC Name"
    Public Const colMPCode As String = "Farmer Code"
    Public Const colMPUploaderCode As String = "Farmer"
    Public Const colMPName As String = "Farmer Name"
    Public Const colMPBank As String = "Bank"
    Public Const colMPAccountNo As String = "Account No"
    Public Const colMPPhoneNo As String = "Phone No"
    Public Const colMPIFSCCode As String = "IFSC"
    Public Const colMPAadharNo As String = "Aadhar No"
    Public Const colQty As String = "Qty"
    Public Const colFAT As String = "FAT"
    Public Const colSNF As String = "SNF"
    Public Const colUOM As String = "UOM"
    Public Const colAmount As String = "Amount"
    Public Const colAmountActual As String = "Amount Actual"
    Public Const colPashuAaharQty As String = "Pashu Aahar Qty"
    Public Const colPashuAaharAmt As String = "Pashu Aahar Amount"
    Public Const colMineralMixtureQty As String = "Mineral Mixture Qty"
    Public Const colMineralMixtureAmt As String = "Mineral Mixture Amount"
    Public Const colSailejQty As String = "Sailej Qty"
    Public Const colSailejAmt As String = "Sailej Amount"
    Public Const colRahatKampekatFeedQty As String = "Rahat Kampekat Feed Qty"
    Public Const colRahatKampekatFeedAmt As String = "Rahat Kampekat Feed Amount"
    Public Const colTotAmount As String = "Total Amount"
    Public Const colDBTProcessed As String = "DBT Processed"
End Class

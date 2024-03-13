Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMilkSampleMCC
    '' UPDATION BY RICHA AGARWAL 29/12/2015 AGAINST TICKET NO BM00000008434 ADD TRANSACTION IN CLASS SO CREATE SAVEDATA OVERRIDDEN FUNCTION
    ''MIL/30/01/19-000034 by balwinder on 31/01/2019
#Region "Variables"
    Public DOC_CODE As String
    Public MCC_CODE As String
    Public MILK_RECEIPT_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public TOTAL_QTY As Decimal
    Public TOTAL_FAT As Decimal = 0
    Public TOTAL_SNF As Decimal = 0
    Public Dock_Collection_Milk_Type As String
    Public DOCK_Code As String
    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of clsMilkSampleMCCDetail)
    Public Shared ObjListHistory As List(Of clsMilkSampleMCCDetailHistory)

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing


#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkSampleMCC, ByVal objList As List(Of clsMilkSampleMCCDetail), ByVal objListHistory As List(Of clsMilkSampleMCCDetailHistory)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, objListHistory, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkSampleMCC, ByVal objList As List(Of clsMilkSampleMCCDetail), ByVal objListHistory As List(Of clsMilkSampleMCCDetailHistory), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, obj.MCC_CODE, obj.DOC_DATE, trans)

            Dim isNewEntry As Boolean
            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                Dim qry As String = "select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)= convert(date, '" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and SHIFT='" + obj.SHIFT + "' and MCC_CODE='" + obj.MCC_CODE + "' and Dock_Collection_Milk_Type='" + obj.Dock_Collection_Milk_Type + "'"
                If clsCommon.myLen(obj.DOCK_Code) > 0 Then
                    qry += " and TSPL_MILK_SAMPLE_HEAD.DOCK_Code='" + obj.DOCK_Code + "'"
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Sample no: " + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + " is already generated")
                End If
                dt = Nothing
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkSample, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
            Else
                isNewEntry = False
            End If


            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "MILK_RECEIPT_CODE", obj.MILK_RECEIPT_CODE)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)

            clsCommon.AddColumnsForChange(coll, "TOTAL_QTY", clsCommon.myCdbl(obj.TOTAL_QTY))
            clsCommon.AddColumnsForChange(coll, "TOTAL_FAT", clsCommon.myCdbl(obj.TOTAL_FAT))
            clsCommon.AddColumnsForChange(coll, "TOTAL_SNF", clsCommon.myCdbl(obj.TOTAL_SNF))
            clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
            clsCommon.AddColumnsForChange(coll, "DOCK_Code", obj.DOCK_Code, True)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_SAMPLE_HEAD where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_SAMPLE_HEAD.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMilkSampleMCCDetail.SaveData(obj.DOC_CODE, objList, trans, isNewEntry, obj.MILK_RECEIPT_CODE)
            If objListHistory IsNot Nothing AndAlso objListHistory.Count > 0 Then
                isSaved = isSaved AndAlso clsMilkSampleMCCDetailHistory.SaveData(obj.DOC_CODE, objListHistory, trans, isNewEntry, obj.MILK_RECEIPT_CODE)
            End If
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.DOC_CODE, obj.arrCustomFields, trans)
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            ' trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkSampleMCC
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkSampleMCC
        Dim obj As New clsMilkSampleMCC()
        Dim objtr As New clsMilkSampleMCCDetail
        ObjList = New List(Of clsMilkSampleMCCDetail)
        Dim qry As String = "SELECT TSPL_MILK_SAMPLE_HEAD.DOC_CODE,TSPL_MILK_SAMPLE_HEAD.MCC_CODE,TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        qry += " TSPL_MILK_SAMPLE_HEAD.TOTAL_QTY,TSPL_MILK_SAMPLE_HEAD.TOTAL_SNF,TSPL_MILK_SAMPLE_HEAD.TOTAL_FAT,TSPL_MCC_MASTER.MCC_NAME, "
        qry += " TSPL_MILK_SAMPLE_HEAD.Created_By,TSPL_MILK_SAMPLE_HEAD.Posted,TSPL_MILK_SAMPLE_HEAD.POSTED,TSPL_MILK_SAMPLE_HEAD.POSTING_DATE,TSPL_MILK_SAMPLE_HEAD.Dock_Collection_Milk_Type,TSPL_MILK_SAMPLE_HEAD.DOCK_Code FROM TSPL_MILK_SAMPLE_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_SAMPLE_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.DOC_CODE = dt.Rows(0)("DOC_CODE")
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.MILK_RECEIPT_CODE = clsCommon.myCstr(dt.Rows(0)("MILK_RECEIPT_CODE"))
            obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy hh:mm:ss tt")
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.TOTAL_QTY = clsCommon.myCstr(dt.Rows(0)("TOTAL_QTY"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.TOTAL_FAT = clsCommon.myCdbl(dt.Rows(0)("TOTAL_FAT"))
            obj.TOTAL_SNF = clsCommon.myCdbl(dt.Rows(0)("TOTAL_SNF"))
            obj.Dock_Collection_Milk_Type = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
            obj.DOCK_Code = clsCommon.myCstr(dt.Rows(0)("DOCK_Code"))
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            strCode = dt.Rows(0)("DOC_CODE")
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        ',MILK_RECEIPT_CODE
        qry = " SELECT sd.DOC_CODE,VLC_DOC_CODE,vlc_code,SAMPLE_NO,VSP_CODE,Item_Code,QTY, "
        qry += " TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Uom_Code,Correction_Factor,CLR,RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,ACC_QTY,sd.FAT_ORG,sd.SNF_ORG,sd.FAT_CORRECTION,sd.SNF_CORRECTION,sd.price_code " _
        & " FROM TSPL_MILK_SAMPLE_DETAIL sd inner join TSPL_MILK_SAMPLE_HEAD sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code," _
        & " SAMPLE_NO_VALUES,VLC_DOC_CODE as vlc_d,eco_pro_name,No_Of_Cans as cans,vlc_code,sample_no as sn_no from  TSPL_MILK_RECEIPT_DETAIL  " _
        & " ) tt on Receipt_code=MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and sample_no=sn_no  WHERE 2=2"
        qry += " AND sd.DOC_CODE = '" + strCode + "' ORDER BY sd.SAMPLE_NO"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkSampleMCCDetail
                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.sample_No_Value = clsCommon.myCstr(dr("SAMPLE_NO_VALUES"))
                objtr.ITEm_CODE = clsCommon.myCstr(dr("Item_CODE"))
                ' objtr.MILK_Qty = clsCommon.myCdbl(dr("QTY"))
                objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                objtr.SRN_CODE = clsCommon.myCstr(dr("MILK_SRN_CODE"))
                objtr.UOM_Code = clsCommon.myCstr(dr("UOM_Code"))
                objtr.Cans = clsCommon.myCdbl(dr("cans"))
                objtr.TYPE = clsCommon.myCstr(dr("TYPE"))
                objtr.MILK_TYPE = clsCommon.myCstr(dr("MILK_TYPE"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.Correction_Factor = clsCommon.myCdbl(dr("Correction_factor"))
                'objtr.UOM_Code = clsCommon.myCdbl(dr("UOM_Code"))
                objtr.MILK_Qty = clsCommon.myCdbl(dr("QTY"))
                objtr.ACC_Qty = clsCommon.myCdbl(dr("ACC_QTY"))
                objtr.CLR = clsCommon.myCdbl(dr("CLR"))
                objtr.RATE = clsCommon.myCdbl(dr("RATE"))
                objtr.AMOUNT = clsCommon.myCdbl(dr("AMOUNT"))
                objtr.Is_Entered_Manualy = clsCommon.myCstr(dr("IS_MANUAL"))
                objtr.Eco_Pro_Name = clsCommon.myCstr(dr("Eco_pro_Name"))


                objtr.FAT_ORG = clsCommon.myCdbl(dr("FAT_ORG"))
                objtr.SNF_ORG = clsCommon.myCdbl(dr("SNF_ORG"))
                objtr.FAT_CORRECTION = clsCommon.myCdbl(dr("FAT_CORRECTION"))
                objtr.SNF_CORRECTION = clsCommon.myCdbl(dr("SNF_CORRECTION"))
                objtr.Price_Code = clsCommon.myCstr(dr("price_code"))
                ObjList.Add(objtr)
            Next
        End If

        clsMilkSampleMCC.ObjList = ObjList
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select MCC_CODE,DOC_DATE from TSPL_MILK_SAMPLE_HEAD where DOC_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, clsCommon.myCstr(dt.Rows(0)("MCC_CODE")), clsCommon.myCDate(dt.Rows(0)("DOC_DATE")), trans)


            End If

            Dim qry As String

            qry = "update TSPL_MILK_RECEIPT_DETAIL set Is_Sampleed='F' where VLC_DOC_CODE in (select VLC_DOC_CODE from TSPL_MILK_SAMPLE_DETAIL " _
            & " inner join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE where TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" _
            & " =MILK_Receipt_CODE AND TSPL_MILK_SAMPLE_DETAIL.DOC_CODE='" & strCode & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_SAMPLE_HEAD where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' LOG FOR SYNC DATA
            clsSyncHeadTables.SaveSyncDelete("TSPL_MILK_SAMPLE_HEAD", strCode, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetShiftisManual(ByVal Mcc_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim sQuery As String = "select coalesce(TSPL_OPEN_MCC_SHIFT.Is_Allow_Manual_Entry,'F') from TSPL_OPEN_MCC_SHIFT where lower(status)='o' and TSPL_OPEN_MCC_SHIFT.mcc_code='" & Mcc_Code & "'"
            Dim is_Manual As Boolean = IIf(clsDBFuncationality.getSingleValue(sQuery, trans) = "T", True, False)
            Return is_Manual
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return True
    End Function

    Public Sub GetSampleTable(ByVal strCode As String, ByVal trans As SqlTransaction)
        Dim qry As String = " SELECT sd.DOC_CODE,VLC_DOC_CODE,vlc_code,SAMPLE_NO,VSP_CODE,Item_Code,QTY, "
        qry += " TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Uom_Code,Correction_Factor,CLR,RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,ACC_QTY FROM TSPL_MILK_SAMPLE_DETAIL sd inner join TSPL_MILK_SAMPLE_HEAD sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code,SAMPLE_NO_VALUES,VLC_DOC_CODE as vlc_d,eco_pro_name,No_Of_Cans as cans,vlc_code,sample_No as sm_no from  TSPL_MILK_RECEIPT_DETAIL   ) tt on Receipt_code=MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and sample_No=sm_No  WHERE 2=2"
        qry += " AND sd.DOC_CODE = '" + strCode + "' ORDER BY sd.SAMPLE_NO"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
    End Sub

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String) As String
        Return GetDocCode(DocDate, MCC_Code, Shift, Nothing)
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal trans As SqlTransaction) As String
        Return GetDocCode(DocDate, MCC_Code, Shift, trans, "M", "")
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal trans As SqlTransaction, ByVal strDockCollectionMilkType As String, ByVal strDockCode As String) As String
        Dim qry As String
        qry = "SELECT DOC_CODE FROM TSPL_MILK_Sample_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Dock_Collection_Milk_Type ='" + strDockCollectionMilkType + "'"
        If clsCommon.myLen(strDockCode) > 0 Then
            qry += " and TSPL_MILK_Sample_HEAD.Dock_Code='" + strDockCode + "'"
        End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("DOC_CODE")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetDocArray(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal Sample_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        Dim arr_sam As New ArrayList
        qry = "SELECT TSPL_MILK_Sample_HEAD.DOC_CODE FROM TSPL_MILK_Sample_HEAD  left join TSPL_MILK_SAMPLE_DETAIL on tspl_milk_Sample_Head.doc_code=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and sample_no='" & Sample_No & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function



    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMilkSampleMCC = clsMilkSampleMCC.GetData(strDocNo, NavigatorType.Current)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, obj.MCC_CODE, obj.DOC_DATE, Nothing)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MILK_SAMPLE_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_SAMPLE_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class


Public Class clsMilkSampleMCCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_DETAIL_CODE As String
    Public VLC_DOC_CODE As String
    Public VLC_CODE As String
    Public sample_No_Value As String
    ' Public MILK_RECEIPT_CODE As String
    Public SAMPLE_NO As Integer
    Public VSP_CODE As String
    Public ITEm_CODE As String
    Public MILK_Qty As Decimal
    Public ACC_Qty As Decimal
    Public TYPE As String
    Public MILK_TYPE As String
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public Cans As Decimal = 0
    Public Correction_Factor As Decimal = 0
    Public CLR As Decimal = 0
    Public RATE As Decimal = 0
    Public AMOUNT As Decimal = 0
    Public Is_Entered_Manualy As String
    Public SRN_CODE As String
    Public Eco_Pro_Name As String
    Public UOM_Code As String
    Public Price_Code As String

    Public FAT_ORG As Decimal = 0
    Public SNF_ORG As Decimal = 0
    Public FAT_CORRECTION As Decimal = 0
    Public SNF_CORRECTION As Decimal = 0
    Public QAT_Rate As Decimal = 0
    Public Negative_Rate As Decimal = 0

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkSampleMCCDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Receipt_Code As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkSampleMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
                clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
                clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.ITEm_CODE)
                clsCommon.AddColumnsForChange(coll, "QTY", obj.MILK_Qty)
                clsCommon.AddColumnsForChange(coll, "ACC_QTY", obj.ACC_Qty)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                If objCommonVar.AddValidationofMilkTypeinsample Then
                    ''Do not change exception "Milk Type [" by balwinder used in form.
                    If clsCommon.CompairString(obj.TYPE, "M") = CompairStringResult.Equal Then
                        If obj.FAT < objCommonVar.FatMinMix OrElse obj.FAT > objCommonVar.FatMaxMix Then
                            Throw New Exception("Milk Type [" + obj.TYPE + "] " + Environment.NewLine + " FAT [" + clsCommon.myCstr(obj.FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinMix) + " - " + clsCommon.myCstr(objCommonVar.FatMaxMix) + "]")
                        ElseIf obj.SNF < objCommonVar.SNFMinMix OrElse obj.SNF > objCommonVar.SNFMaxMix Then
                            Throw New Exception("Milk Type [" + obj.TYPE + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(obj.SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinMix) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxMix) + "]")
                        End If
                    ElseIf clsCommon.CompairString(obj.TYPE, "C") = CompairStringResult.Equal Then
                        If obj.FAT < objCommonVar.FatMinCow OrElse obj.FAT > objCommonVar.FatMaxCow Then
                            Throw New Exception("Milk Type [" + obj.TYPE + "] " + Environment.NewLine + "FAT [" + clsCommon.myCstr(obj.FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinCow) + " - " + clsCommon.myCstr(objCommonVar.FatMaxCow) + "]")
                        ElseIf obj.SNF < objCommonVar.SNFMinCow OrElse obj.SNF > objCommonVar.SNFMaxCow Then
                            Throw New Exception("Milk Type [" + obj.TYPE + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(obj.SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinCow) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxCow) + "]")
                        End If
                    ElseIf clsCommon.CompairString(obj.TYPE, "B") = CompairStringResult.Equal Then
                        If obj.FAT < objCommonVar.FatMinBuff OrElse obj.FAT > objCommonVar.FatMaxBuff Then
                            Throw New Exception("Milk Type [" + obj.TYPE + "] " + Environment.NewLine + "FAT [" + clsCommon.myCstr(obj.FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinBuff) + " - " + clsCommon.myCstr(objCommonVar.FatMaxBuff) + "]")
                        ElseIf obj.SNF < objCommonVar.SNFMinBuff OrElse obj.SNF > objCommonVar.SNFMaxBuff Then
                            Throw New Exception("Milk Type [" + obj.TYPE + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(obj.SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinBuff) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxBuff) + "]")
                        End If
                    Else
                        Throw New Exception("Milk Type should be M/B/C")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "FAT_KG", clsERPFuncationality.myFloor(obj.ACC_Qty * obj.FAT / 100, objCommonVar.MilkSRNFATSNFDecimalPlaces))
                clsCommon.AddColumnsForChange(coll, "SNF_KG", clsERPFuncationality.myFloor(obj.ACC_Qty * obj.SNF / 100, objCommonVar.MilkSRNFATSNFDecimalPlaces))
                clsCommon.AddColumnsForChange(coll, "UOM_Code", obj.UOM_Code)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Correction_factor", obj.Correction_Factor)
                clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                clsCommon.AddColumnsForChange(coll, "RATE", obj.RATE)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.AMOUNT)
                clsCommon.AddColumnsForChange(coll, "TYPE", obj.TYPE)
                clsCommon.AddColumnsForChange(coll, "MILK_TYPE", obj.MILK_TYPE)
                clsCommon.AddColumnsForChange(coll, "IS_MANUAL", obj.Is_Entered_Manualy)
                clsCommon.AddColumnsForChange(coll, "FAT_ORG", obj.FAT_ORG)
                clsCommon.AddColumnsForChange(coll, "SNF_ORG", obj.SNF_ORG)
                clsCommon.AddColumnsForChange(coll, "FAT_CORRECTION", obj.FAT_CORRECTION)
                clsCommon.AddColumnsForChange(coll, "SNF_CORRECTION", obj.SNF_CORRECTION)
                clsCommon.AddColumnsForChange(coll, "QAT_Rate", obj.QAT_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Negative_Rate", obj.Negative_Rate, True)

                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_SAMPLE_Detail where DOC_CODE = '" & strDocNo & "' and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE='" & obj.VLC_DOC_CODE & "' and sample_No='" & obj.SAMPLE_NO & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_SAMPLE_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_SAMPLE_DETAIL.DOC_CODE='" + strDocNo + "' and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "'", trans)
                End If
                Dim strCustomUOM As String = clsItemMaster.GetCustomConversionUOM(obj.ITEm_CODE, trans)
                Dim strColToUpdate As String = ""
                If clsCommon.myLen(strCustomUOM) > 0 Then
                    Dim conv_fac As Decimal = 1 + (obj.CLR / 1000)
                    If clsCommon.CompairString(obj.UOM_Code, "KG") = CompairStringResult.Equal Then
                        'strColToUpdate += ",ACC_WEIGHT = '" + clsCommon.myCstr(obj.MILK_Qty) + "'"
                        strColToUpdate += ",ACC_WEIGHT_LTR = '" + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(obj.MILK_Qty / conv_fac), 3, MidpointRounding.ToEven)) + "'"
                    Else
                        'strColToUpdate += ",LTR_WEIGHT = '" + clsCommon.myCstr(obj.MILK_Qty) + "'"
                        strColToUpdate += ",ACC_WEIGHT = '" + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(obj.MILK_Qty * conv_fac), 3, MidpointRounding.ToEven)) + "'"
                    End If
                End If
                Dim Squery As String = "update tspl_Milk_receipt_Detail set is_sampleed ='T' " + strColToUpdate + " where Doc_Code='" & Receipt_Code & "' and vlc_doc_Code='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "'"
                clsDBFuncationality.ExecuteNonQuery(Squery, trans)
            Next
        End If

        Return True
    End Function

End Class

Public Class clsMilkSampleMCCDetailHistory
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_Date As String
    Public DOC_DETAIL_CODE As String
    Public VLC_DOC_CODE As String
    Public Prev_FAT As Decimal = 0
    Public Prev_SNF As Decimal = 0
    Public New_FAT As Decimal = 0
    Public New_SNF As Decimal = 0

    Public Prev_CLR As Decimal = 0
    Public New_CLR As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkSampleMCCDetailHistory), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Receipt_Code As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkSampleMCCDetailHistory In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
                clsCommon.AddColumnsForChange(coll, "Prev_FAT", obj.Prev_FAT)
                clsCommon.AddColumnsForChange(coll, "Prev_SNF", obj.Prev_SNF)
                clsCommon.AddColumnsForChange(coll, "New_Fat", obj.New_FAT)
                clsCommon.AddColumnsForChange(coll, "New_SNF", obj.New_SNF)
                clsCommon.AddColumnsForChange(coll, "Updated_By", objCommonVar.CurrentUserCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_Detail_History", OMInsertOrUpdate.Insert, "TSPL_MILK_SAMPLE_Detail_History.DOC_CODE='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function

End Class


Public Class clsMilkSRNMCC

#Region "Variables"

    Public DOC_CODE As String
    Public MCC_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public COMM_PORT As String
    Public VLC_DOC_CODE As String
    Public MILK_SAMPLE_CODE As String
    Public Against_Reject_No As String
    Public SAMPLE_NO As Integer
    Public VLC_CODE As String
    Public ROUTE_CODE As String
    Public VSP_CODE As String
    Public VEHICLE_CODE As String
    Public TransPorter As String
    Public TransPorter_name As String
    Public Dock_Collection_Milk_Type As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public Reason As String
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns
    Public Failed_Sample_Status As Boolean = False
    Public Retesting_FAT As Decimal = 0
    Public Retesting_SNF As Decimal = 0
    Public Retesting_Status As Integer = 0
    Public Correction_Status As Integer = 0
    Public Shared arrList As List(Of clsMilkSRNMCC) = New List(Of clsMilkSRNMCC)
    Public Shared ObjList As List(Of clsMilkSRNMCCDetail)
    Public Shared ObjVSPChargeList As List(Of clsMilkSRNVSpChargeDetail)
    Public Shared ObjPriceChargeList As List(Of clsMilkSRNPriceChargeDetail)


#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkSRNMCC, ByVal objList As List(Of clsMilkSRNMCCDetail), ByVal objVSPChargeList As List(Of clsMilkSRNVSpChargeDetail), ByVal objPriceChargeList As List(Of clsMilkSRNPriceChargeDetail)) As Boolean
        Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, objVSPChargeList, objPriceChargeList, Trans)
            Trans.Commit()
        Catch ex As Exception
            Trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkSRNMCC, ByVal objList As List(Of clsMilkSRNMCCDetail), ByVal objVSPChargeList As List(Of clsMilkSRNVSpChargeDetail), ByVal objPriceChargeList As List(Of clsMilkSRNPriceChargeDetail), ByVal Trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(obj.ROUTE_CODE) <= 0 Then
                Throw New Exception("Route not defined for VLC Code [" + obj.VLC_DOC_CODE + "]")
            End If
            If clsCommon.myLen(obj.TransPorter) <= 0 Then
                Throw New Exception("Transporter not defined for VLC Code [" + obj.VLC_DOC_CODE + "]")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Milk SRN", obj.MCC_CODE, obj.DOC_DATE, Trans)
            Dim isNewEntry As Boolean = True

            Dim Strqrys As String = "Update TSPL_MILK_SAMPLE_DETAIL set Milk_SRN_Code=null  where DOC_CODE='" & obj.MILK_SAMPLE_CODE & "' and sample_No='" & obj.SAMPLE_NO & "' "
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)

            Dim QrySRNDoc As String = "select DOC_CODE from TSPL_MILK_SRN_HEAD where  Milk_Sample_Code= '" & obj.MILK_SAMPLE_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "'"

            Strqrys = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in (" + QrySRNDoc + ")"
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)
            Strqrys = "Delete FROM TSPL_MILK_SRN_Detail where DOC_CODE in (" + QrySRNDoc + ") "
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)
            Strqrys = "Delete FROM TSPL_MILK_SRN_VSP_Charge_Detail where DOC_CODE in (" + QrySRNDoc + ") "
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)
            Strqrys = "Delete FROM TSPL_MILK_SRN_Price_Charge_Detail where DOC_CODE in (" + QrySRNDoc + ") "
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)
            ''UDL/03/07/18-000200 by balwinder Journal entry not delete
            Strqrys = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where source_code='MI-SR' and Source_doc_no in (" + QrySRNDoc + "))"
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)
            Strqrys = "delete from TSPL_JOURNAL_MASTER where source_code='MI-SR' and Source_doc_no in (" + QrySRNDoc + ")"
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)
            Strqrys = "Delete FROM TSPL_MILK_SRN_HEAD  where DOC_CODE in (" + QrySRNDoc + ")"
            clsDBFuncationality.ExecuteNonQuery(Strqrys, Trans)


            obj.DOC_CODE = clsERPFuncationality.GetNextCode(Trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkSRN, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
            clsCommon.AddColumnsForChange(coll, "COMM_PORT", obj.COMM_PORT)
            clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MILK_SAMPLE_CODE", obj.MILK_SAMPLE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Against_Reject_No", obj.Against_Reject_No, True)
            clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
            clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
            clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", obj.ROUTE_CODE)
            clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
            clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.TransPorter)
            clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
            clsCommon.AddColumnsForChange(coll, "POSTED", "1")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            clsCommon.AddColumnsForChange(coll, "Retesting", obj.Reason)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE = '" & obj.MILK_SAMPLE_CODE & "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, Trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Insert, "", Trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.DOC_CODE), "TSPL_MILK_SRN_HEAD", "DOC_CODE", "TSPL_MILK_SRN_DETAIL", "DOC_CODE", Trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE='" + obj.MILK_SAMPLE_CODE + "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "' and Retesting='" & obj.Reason & "' ", Trans)
            End If
            clsMilkSRNMCCDetail.SaveData(obj.DOC_CODE, obj.Dock_Collection_Milk_Type, objList, Trans, isNewEntry, obj.Against_Reject_No, obj.SAMPLE_NO)
            clsMilkSRNVSpChargeDetail.SaveData(obj.DOC_CODE, objVSPChargeList, Trans, isNewEntry)
            clsMilkSRNPriceChargeDetail.SaveData(obj.DOC_CODE, objPriceChargeList, Trans, isNewEntry)
            PostData("M-SRN", obj.DOC_CODE, Trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkSRNMCC
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkSRNMCC

        Dim obj As New clsMilkSRNMCC()
        Dim objtr As New clsMilkSRNMCCDetail

        ObjList = New List(Of clsMilkSRNMCCDetail)

        Dim qry As String = "SELECT vem.Vendor_Code as Vendor_C,vendor_name as Vendor_N,sd.* FROM TSPL_MILK_SRN_HEAD sd  " _
        & "  Left join TSPL_VENDOR_MASTER vem on vem.Vendor_Code=sd.Transporter where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_SRN_HEAD  )"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_SRN_HEAD  )"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_SRN_HEAD   where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_SRN_HEAD   where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.DOC_CODE = clsCommon.myCstr(dt.Rows(0)("DOC_CODE"))
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.DOC_DATE = clsCommon.myCDate(dt.Rows(0)("DOC_DATE"))
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.COMM_PORT = clsCommon.myCstr(dt.Rows(0)("COMM_PORT"))
            obj.VLC_DOC_CODE = clsCommon.myCstr(dt.Rows(0)("VLC_DOC_CODE"))
            obj.MILK_SAMPLE_CODE = clsCommon.myCstr(dt.Rows(0)("MILK_SAMPLE_CODE"))
            obj.Against_Reject_No = clsCommon.myCstr(dt.Rows(0)("Against_Reject_No"))
            obj.Dock_Collection_Milk_Type = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
            obj.SAMPLE_NO = clsCommon.myCdbl(dt.Rows(0)("SAMPLE_NO"))
            obj.VLC_CODE = clsCommon.myCstr(dt.Rows(0)("VLC_CODE"))
            obj.ROUTE_CODE = clsCommon.myCstr(dt.Rows(0)("ROUTE_CODE"))
            obj.VSP_CODE = clsCommon.myCstr(dt.Rows(0)("VSP_CODE"))
            obj.VEHICLE_CODE = clsCommon.myCstr(dt.Rows(0)("VEHICLE_CODE"))
            obj.TransPorter = clsCommon.myCstr(dt.Rows(0)("Vendor_C"))
            obj.TransPorter_name = clsCommon.myCstr(dt.Rows(0)("Vendor_N"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Failed_Sample_Status = (clsCommon.myCdbl(dt.Rows(0)("Failed_Sample_Status")) = 1)
            strCode = dt.Rows(0)("DOC_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = " SELECT TSPL_MILK_SRN_DETAIL.*,Item_Desc,TSPL_MILK_SRN_DETAIL.UOM_Code as Unit_Code FROM TSPL_MILK_SRN_DETAIL Inner join tspl_item_Master on tspl_item_Master.item_Code=TSPL_MILK_SRN_DETAIL.item_Code  WHERE 2=2"
        qry += " AND TSPL_MILK_SRN_DETAIL.DOC_CODE = '" + strCode + "' "

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkSRNMCCDetail

                objtr.DOC_CODE = strCode
                objtr.Item_CODE = clsCommon.myCstr(dr("Item_CODE"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.UOM = clsCommon.myCstr(dr("Unit_Code"))
                objtr.CLR = clsCommon.myCdbl(dr("CLR"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT_PER"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF_PER"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                objtr.Correction_Factor = clsCommon.myCdbl(dr("Correction_factor"))
                objtr.MILK_Qty = clsCommon.myCdbl(dr("QTY"))
                objtr.ACC_Qty = clsCommon.myCdbl(dr("ACC_QTY"))

                objtr.RATE = clsCommon.myCdbl(dr("RATE"))
                objtr.AMOUNT = clsCommon.myCdbl(dr("AMOUNT"))
                objtr.NET_AMOUNT = clsCommon.myCdbl(dr("NET_AMOUNT"))
                objtr.Round_Off = clsCommon.myCdbl(dr("Round_Off"))
                objtr.Service_Charge_Amount = clsCommon.myCdbl(dr("Service_Charge_Amount"))
                objtr.Commission = clsCommon.myCdbl(dr("Commission_Pers"))
                objtr.Commission_Amount = clsCommon.myCdbl(dr("Commission_Amount"))
                objtr.Payment_Commission = clsCommon.myCdbl(dr("EMP_Pers"))


                objtr.Head_Load_Amount = clsCommon.myCdbl(dr("Head_Load_Amount"))
                objtr.Own_Asset_Amount = clsCommon.myCdbl(dr("Own_Asset_Amount"))


                objtr.Head_Load_Rate = clsCommon.myCdbl(dr("Head_Load_Rate"))
                objtr.Own_Asset_Rate = clsCommon.myCdbl(dr("Own_Asset_Rate"))
                objtr.Head_Load_Type = clsCommon.myCdbl(dr("Head_Load_Type"))
                objtr.Own_Asset_Type = clsCommon.myCdbl(dr("Own_Asset_Type"))
                objtr.Emp_Amount = clsCommon.myCdbl(dr("EMP_Amount"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_COde"))
                objtr.COMM_PORT = clsCommon.myCstr(dr("COMM_PORT"))
                objtr.Service_Charge_Type = clsCommon.myCstr(dr("Service_Charge_Type"))
                objtr.Price_Code = clsCommon.myCstr(dr("Price_Code"))

                objtr.QAT_Rate = clsCommon.myCdbl(dr("QAT_Rate"))
                objtr.QAT_Amt = clsCommon.myCdbl(dr("QAT_Amt"))

                objtr.Negative_Rate = clsCommon.myCdbl(dr("Negative_Rate"))
                objtr.Negative_Amount = clsCommon.myCdbl(dr("Negative_Amount"))
                ObjList.Add(objtr)
            Next
        End If

        clsMilkSRNMCC.ObjList = ObjList
        Return obj
    End Function

    Public Shared Function ChecksavedTransaction(ByVal Code As String) As Boolean
        Dim sQuery As String = "select count(*) from tspl_milk_sample_detail where coalesce(milk_srn_Code,'')='' and doc_code='" & Code & "'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(sQuery)
        If count > 0 Then
            Throw New Exception("All samples are not posted .please save data and post it again.")
        End If
        Return True
    End Function

    Public Shared Function SaveSRN_History(ByVal Doc_Code As String, ByVal trans As SqlTransaction)
        Try
            'Dim sQuery As String = "insert into TSPL_MILK_SRN_DETAIL_History(Doc_code,item_Code,Qty,fat_Per,Snf_per,Mcc_Code,Comm_Port,Correction_factor," _
            '& " rate,Amount,Uom_Code,Acc_Qty,Service_Charge_Type,Commission_Pers,EMP_Pers,Commission_Amount,EMP_Amount,Net_Amount,fat_KG,SNF_kg,Price_Code," _
            '& " Head_Load_rate,Head_Load_Amount,Head_Load_type,Own_Asset_rate,Own_Asset_Amount,Own_Asset_type,History_By,History_date)  select Doc_code,item_Code,Qty,fat_Per,Snf_per," _
            '& " Mcc_Code,Comm_Port,Correction_factor,rate,Amount,Uom_Code,Acc_Qty,Service_Charge_Type,Commission_Pers,EMP_Pers,Commission_Amount,EMP_Amount," _
            '& " Net_Amount,fat_KG,SNF_kg,Price_Code,Head_Load_rate,Head_Load_Amount,Head_Load_type,Own_Asset_rate,Own_Asset_Amount,Own_Asset_type,'" & objCommonVar.CurrentUserCode & "','" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss") & "'" _
            '& " from TSPL_MILK_SRN_DETAIL where doc_code='" & Doc_Code & "'"
            'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(Doc_Code), "TSPL_MILK_SRN_HEAD", "DOC_CODE", "TSPL_MILK_SRN_DETAIL", "DOC_CODE", trans)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function SaveDataFromSRNFrom(ByVal obj As clsMilkSRNMCC, ByVal objList As List(Of clsMilkSRNMCCDetail), ByVal objVSPChargeList As List(Of clsMilkSRNVSpChargeDetail), ByVal objPriceChargeList As List(Of clsMilkSRNPriceChargeDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            SaveSRN_History(obj.DOC_CODE, trans)
            'If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
            '    isNewEntry = True
            '    obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkSRN, "", "")
            'Else
            isNewEntry = False
            'Dim Strqrys As String = "Update TSPL_MILK_SAMPLE_DETAIL set Milk_SRN_Code=null  where DOC_CODE='" & obj.MILK_SAMPLE_CODE & "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  "
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Strqrys, trans)

            'Strqrys = "Delete FROM TSPL_MILK_SRN_Detail where DOC_CODE = (select DOC_CODE from TSPL_MILK_SRN_HEAD where Milk_Sample_Code= '" & obj.MILK_SAMPLE_CODE & "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "') "
            'clsDBFuncationality.ExecuteNonQuery(Strqrys, trans)
            'Strqrys = "Delete FROM TSPL_MILK_SRN_VSP_Charge_Detail where DOC_CODE = (select DOC_CODE from TSPL_MILK_SRN_HEAD where Milk_Sample_Code= '" & obj.MILK_SAMPLE_CODE & "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "') "
            'clsDBFuncationality.ExecuteNonQuery(Strqrys, trans)
            'Strqrys = "Delete FROM TSPL_MILK_SRN_Price_Charge_Detail where DOC_CODE = (select DOC_CODE from TSPL_MILK_SRN_HEAD where Milk_Sample_Code= '" & obj.MILK_SAMPLE_CODE & "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "') "
            'clsDBFuncationality.ExecuteNonQuery(Strqrys, trans)
            'Strqrys = "Delete FROM TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE = '" & obj.MILK_SAMPLE_CODE & "'  and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  "
            'clsDBFuncationality.ExecuteNonQuery(Strqrys, trans)
            'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkSRN, "", "")
            ' End If
            Dim isInvoice_created As String = clsDBFuncationality.getSingleValue("select coalesce(is_incentive_created,'N') from tspl_milk_srn_Head where doc_code='" & obj.DOC_CODE & "'", trans)
            If isInvoice_created = "Y" Then
                Return True
            End If
            Dim invoice_Count As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_milk_Purchase_Invoice_Detail where Srn_code in (select Doc_Code from tspl_milk_srn_Head where convert(date,doc_date,103)=convert(date,'" & obj.DOC_DATE & "',103) and mcc_code='" & obj.MCC_CODE & "')", trans)
            If invoice_Count > 0 Then
                Return True
            End If


            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)

            clsCommon.AddColumnsForChange(coll, "COMM_PORT", obj.COMM_PORT)
            clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MILK_SAMPLE_CODE", obj.MILK_SAMPLE_CODE)
            clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
            clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
            clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", obj.ROUTE_CODE)
            clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
            clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.TransPorter)

            clsCommon.AddColumnsForChange(coll, "POSTED", "1")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_SRN_HEAD where doc_code='" & obj.DOC_CODE & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, " doc_code='" & obj.DOC_CODE & "'", trans)
                    'common.clsCommon.MyMessageBoxShow("This Code:" + obj.DOC_CODE + " Is Already Exist")
                    'Exit Function
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE='" + obj.MILK_SAMPLE_CODE + "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "' ", trans)
            End If
            isSaved = isSaved AndAlso clsMilkSRNMCCDetail.SaveData(obj.DOC_CODE, obj.Dock_Collection_Milk_Type, objList, trans, isNewEntry, obj.Against_Reject_No, obj.SAMPLE_NO)
            isSaved = isSaved AndAlso clsMilkSRNVSpChargeDetail.SaveData(obj.DOC_CODE, objVSPChargeList, trans, isNewEntry)
            isSaved = isSaved AndAlso clsMilkSRNPriceChargeDetail.SaveData(obj.DOC_CODE, objPriceChargeList, trans, isNewEntry)

            isSaved = isSaved AndAlso PostData("M-SRN", obj.DOC_CODE, trans)
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(err.Message)
            '  trans.Rollback()
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function UpdateSample(ByVal Doc_Code As String, ByVal sample_No As Integer, ByVal FAT As Double, ByVal SNF As Double, ByVal CLR As Double, ByVal Rate As Double, ByVal Amount As Double, ByVal trans As SqlTransaction, ByVal Price_Code As String, ByVal QATRAte As Decimal, ByVal NegativeRate As Decimal) As Boolean
        Try
            Dim sQuery As String = "update tspl_milk_sample_detail set fat=" & FAT & " , snf=" & SNF & ", CLR=" & CLR & ",rate=" & Rate & ",Amount=" & Amount & ",QAT_Rate=" & QATRAte & ",Negative_Rate=" & NegativeRate & ""
            If clsCommon.myLen(Price_Code) > 0 Then
                sQuery += " ,Price_Code='" + Price_Code + "'  "
            End If
            sQuery += " where doc_code='" & Doc_Code & "' and sample_No='" & sample_No & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function UpdateDataFromSRNFrom(ByVal obj As clsMilkSRNMCC, ByVal objList As List(Of clsMilkSRNMCCDetail), ByVal objVSPChargeList As List(Of clsMilkSRNVSpChargeDetail), ByVal objPriceChargeList As List(Of clsMilkSRNPriceChargeDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_CODE, obj.DOC_DATE, trans)

            SaveSRN_History(obj.DOC_CODE, trans)
            isNewEntry = False
            Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, trans)) = 1
            Dim coll As New Hashtable()
            If isPickPendingMilkSRNinNextPaymentCycle Then
                Dim InvNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_DETAIL where SRN_CODE='" + obj.DOC_CODE + "'", trans))
                If clsCommon.myLen(InvNo) > 0 Then
                    Throw New Exception("Invoice [" + InvNo + "] is Created for SRN [" & obj.DOC_CODE & "] can not be recreated.")
                End If
                clsCommon.AddColumnsForChange(coll, "is_incentive_created", "N") ''It will come in vsp bill and incetive calculation
            Else
                Dim isInvoice_created As String = clsDBFuncationality.getSingleValue("select coalesce(is_incentive_created,'N') from tspl_milk_srn_Head where doc_code='" & obj.DOC_CODE & "'", trans)
                If isInvoice_created = "Y" Then
                    Throw New Exception("Invoice is Created of this SRN" + obj.DOC_CODE)
                    'Return False
                End If
                Dim invoice_Count As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_milk_Purchase_Invoice_Detail where Srn_code in (select Doc_Code from tspl_milk_srn_Head where convert(date,doc_date,103)=convert(date,'" & obj.DOC_DATE & "',103) and mcc_code='" & obj.MCC_CODE & "')", trans)
                If invoice_Count > 0 Then
                    Throw New Exception("Invoice is Created of this Payment Cycle.SRN [" & obj.DOC_CODE & "] can not be recreated.")
                    'Return False
                End If
            End If
            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
            clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
            clsCommon.AddColumnsForChange(coll, "COMM_PORT", obj.COMM_PORT)
            clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MILK_SAMPLE_CODE", obj.MILK_SAMPLE_CODE)
            clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
            clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
            clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", obj.ROUTE_CODE)
            clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
            clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.TransPorter)

            clsCommon.AddColumnsForChange(coll, "POSTED", "1")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_SRN_HEAD where doc_code='" & obj.DOC_CODE & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, " doc_code='" & obj.DOC_CODE & "'", trans)
                    'common.clsCommon.MyMessageBoxShow("This Code:" + obj.DOC_CODE + " Is Already Exist")
                    'Exit Function
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE='" + obj.MILK_SAMPLE_CODE + "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "' ", trans)
            End If
            isSaved = isSaved AndAlso clsMilkSRNMCCDetail.SaveData(obj.DOC_CODE, obj.Dock_Collection_Milk_Type, objList, trans, isNewEntry, obj.Against_Reject_No, obj.SAMPLE_NO)
            isSaved = isSaved AndAlso clsMilkSRNVSpChargeDetail.SaveData(obj.DOC_CODE, objVSPChargeList, trans, isNewEntry)
            isSaved = isSaved AndAlso clsMilkSRNPriceChargeDetail.SaveData(obj.DOC_CODE, objPriceChargeList, trans, isNewEntry)

            '' done by Panch raj against duplicate inventory movement 
            For Each objTr As clsMilkSRNMCCDetail In objList
                Dim qry As String = "update TSPL_INVENTORY_MOVEMENT_NEW set Fat_Per='" & objTr.FAT & "',SNF_Per='" & objTr.SNF & "',Fat_KG='" & objTr.FAT * objTr.ACC_Qty / 100 & "',SNF_KG='" & objTr.SNF * objTr.ACC_Qty / 100 & "',Fat_Amt='" & (objTr.FAT * objTr.ACC_Qty / 100) & "'*Fat_Rate,SNF_Amt='" & (objTr.SNF * objTr.ACC_Qty / 100) & "'*SNF_Rate,Avg_Cost=('" & (objTr.FAT * objTr.ACC_Qty / 100) & "'*Fat_Rate+'" & (objTr.SNF * objTr.ACC_Qty / 100) & "'*SNF_Rate)  where Source_Doc_No='" & obj.DOC_CODE & "' and Item_Code='" & objTr.Item_CODE & "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next


        Catch err As Exception
            Throw New Exception(err.Message)
            'clsCommon.MyMessageBoxShow(err.Message)
            '  trans.Rollback()
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Sub updateJournalEntry(ByVal Source_type As String, ByVal Doc_No As String, ByVal amount As Double, ByVal trans As SqlTransaction)
        Dim sQuery As String
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_journal_master where source_code='" & Source_type & "' and source_doc_no='" & Doc_No & "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
            sQuery = "update tspl_journal_master set total_debit_amt=" & amount & ",total_credit_amt=" & amount & ",modify_by='" & objCommonVar.CurrentUserCode & "',modify_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "',posting_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss") & "' where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            sQuery = "update tspl_journal_details set amount=case when coalesce(amount,0)<0 then -1 else 1 end*" & amount & " where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        Else
            PostData("M-SRN", Doc_No, trans)
        End If
    End Sub

    Public Shared Sub updateJournalEntry(ByVal Source_type As String, ByVal Doc_No As String, ByVal amount As Double)
        Dim sQuery As String
        '=============BM00000008098()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'sQuery = "delete from TSPL_JOURNAL_DETAILS where Voucher_No=(select voucher_no from tspl_Journal_Master where source_doc_No='" & Doc_No & "')"
            'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            sQuery = "Select Voucher_No from TSPL_JOURNAL_MASTER where Voucher_No=(select voucher_no from tspl_Journal_Master where source_doc_No='" & Doc_No & "')"
            '  clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            If PostData("M-SRN", Doc_No, trans, clsDBFuncationality.getSingleValue(sQuery, trans)) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Public Shared Sub updateJournalEntryWithTran(ByVal Source_type As String, ByVal Doc_No As String, ByVal trans As SqlTransaction)
        Dim sQuery As String
        Try
            sQuery = "Select Voucher_No from TSPL_JOURNAL_MASTER where Voucher_No=(select voucher_no from tspl_Journal_Master where source_doc_No='" & Doc_No & "')"
            PostData("M-SRN", Doc_No, trans, clsDBFuncationality.getSingleValue(sQuery, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal create_same_voucher_journal_entry As String = "") As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim obj As clsMilkSRNMCC = clsMilkSRNMCC.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "MIlk Procurement", "Milk Store receipt Note", obj.MCC_CODE, obj.DOC_DATE, trans)
            Dim Mcc_Name As String = clsDBFuncationality.getSingleValue("select MCC_NAME from tspl_MCC_MASTER where MCC_CODE='" & obj.MCC_CODE & "'", trans)
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SRN_HEAD", "SRN_No", obj.DOC_CODE, trans)
            If isResult = False Then
                Throw New Exception("Approval Level")
            End If
            ''BHA/09/05/18-000020 by balwinder on 25/05/2018
            Dim settRejectedMilkSendToRejectLocation As Boolean = False
            Dim strRejectLocation As String = ""
            If clsCommon.myLen(obj.Against_Reject_No) > 0 Then
                settRejectedMilkSendToRejectLocation = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectedMilkSendToRejectLocation, clsFixedParameterCode.RejectedMilkSendToRejectLocation, trans)) = 1)
                If settRejectedMilkSendToRejectLocation Then
                    strRejectLocation = clsLocation.GetRejectedLocation(obj.MCC_CODE, trans)
                    If clsCommon.myLen(strRejectLocation) <= 0 Then
                        Throw New Exception("Please set rejected location for loaction : " + obj.MCC_CODE)
                    End If
                End If
            End If

            Dim qry As String = ""
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim IsRejectedItemFound As Boolean = False
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            ''"""""""""""""""""Done by Panch Raj against duplicate inventory movement issue raised in Kwality"""""""""""""""""""""""
            '' delete inventory movement and journal entry
            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='MCC-MSRN' and Source_Doc_No='" & strDocNo & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''"""""""""""""""""End Done by Panch Raj against duplicate inventory movement issue raised in Kwality"""""""""""""""""""""""

            For Each objTr As clsMilkSRNMCCDetail In clsMilkSRNMCC.ObjList
                intCounter = intCounter + 1
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_CODE, trans)
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

                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = IIf(settRejectedMilkSendToRejectLocation, strRejectLocation, objTr.MCC_CODE)
                objInventoryMovemnt.Vendor_Code = obj.VSP_CODE
                objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.VSP_CODE, trans)
                objInventoryMovemnt.Item_Code = objTr.Item_CODE
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.MILK_Qty
                objInventoryMovemnt.UOM = objTr.UOM
                objInventoryMovemnt.MRP = 0
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.FAT_Per = objTr.FAT
                objInventoryMovemnt.SNF_Per = objTr.SNF
                objInventoryMovemnt.FAT_KG = objTr.FAT * objTr.ACC_Qty / 100
                objInventoryMovemnt.SNF_KG = objTr.SNF * objTr.ACC_Qty / 100
                '-----------------------------------------------
                objInventoryMovemnt.Net_Cost = objTr.NET_AMOUNT ''objTr.RATE * objTr.MILK_Qty ''VIJ/29/11/19-000084 by balwinder on 24/01/2020
                objInventoryMovemnt.FIFO_Cost = objTr.NET_AMOUNT
                objInventoryMovemnt.LIFO_Cost = objTr.NET_AMOUNT
                objInventoryMovemnt.Avg_Cost = objTr.NET_AMOUNT
                objInventoryMovemnt.CustomCoversionCLR = objTr.CLR
                If clsCommon.myLen(objTr.Price_Code) > 0 Then
                    Dim arr As New clsFatSnfRateCalculator
                    If objCommonVar.PricePlan = 6 OrElse objCommonVar.PricePlan = 7 Then
                        qry = "select * from TSPL_MILK_PRICE_MASTER where Price_Code in (select Price_Chart_Code from TSPL_PRICE_CHART_PLANNING where Planning_Code='" & objTr.Price_Code & "')"
                    Else
                        qry = "select * from TSPL_MILK_PRICE_MASTER where Price_Code=" _
                                      & " (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master where code='" & objTr.Price_Code & "')"
                    End If

                    Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If objCommonVar.ApplyTransFATSNFRateForCalculateFATSNFRate Then
                        arr = clsFatSnfRateCalculator.CalculateFATSNFRatefromTransactionPer(objTr.ACC_Qty, (objTr.NET_AMOUNT), objTr.FAT, objTr.SNF, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")))
                    ElseIf objCommonVar.ApplyStdFATSNFRate Then
                        arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.ACC_Qty, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.FAT, objTr.SNF)
                    Else

                        If clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")) = objTr.FAT And clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Pers")) = objTr.SNF Then
                            arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.MILK_Qty, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                        Else
                            Try
                                arr = clsFatSnfRateCalculator.CalculateIn(objTr.MILK_Qty, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("SNF_Pers")), objTr.FAT, objTr.SNF, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.RATE)
                            Catch ex As Exception
                                If ex.Message.Contains("Same equation repeated") Then
                                    arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.MILK_Qty, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Snf_Ratio")), objTr.RATE)
                                    If objInventoryMovemnt.FAT_KG <> 0 Then
                                        arr.fatR = arr.FatAmt / objInventoryMovemnt.FAT_KG
                                    End If
                                    If objInventoryMovemnt.SNF_KG <> 0 Then
                                        arr.snfR = arr.snfAmt / objInventoryMovemnt.SNF_KG
                                    End If
                                Else
                                    Throw New Exception(ex.Message)
                                End If
                            End Try
                        End If
                    End If
                    objInventoryMovemnt.Fat_Rate = arr.fatR
                    objInventoryMovemnt.Fat_Amt = arr.FatAmt
                    objInventoryMovemnt.SNF_Rate = arr.snfR
                    objInventoryMovemnt.SNF_Amt = arr.snfAmt
                    dtMilkPrice = Nothing
                    arr = Nothing
                End If
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                objInventoryMovemnt.Basic_Cost = objTr.RATE
                objInventoryMovemnt.CalculateAvgCost = False
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next
            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("MCC-MSRN", obj.DOC_CODE, obj.DOC_DATE, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            ''By Balwinder on 29/06/2016 for give option to recreate JE .
            CreateJournalEntry(obj, create_same_voucher_journal_entry, trans)
            'CreateSMSContent(FormId, obj, create_same_voucher_journal_entry, trans)

            Dim strRMDANo As String = ""

            qry = "Update TSPL_MILK_SRN_HEAD set POSTED=1, Posting_Date='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm tt") + "',Modified_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where DOC_CODE='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_MILK_SAMPLE_DETAIL set Milk_SRN_Code='" & strDocNo & "'  where DOC_CODE='" & obj.MILK_SAMPLE_CODE & "' and vlc_Doc_COde='" & obj.VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "'  "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MILK_SRN_HEAD", "DOC_CODE", "TSPL_MILK_SRN_DETAIL", "DOC_CODE", trans)
            'Throw New Exception("Balwinder Singh Premi")
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntry(obj As clsMilkSRNMCC, create_same_voucher_journal_entry As String, trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" & create_same_voucher_journal_entry & "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim isAllowPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0, False, True)
        If Not isAllowPurchaseAccounting Then
            qry = "select TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code,TSPL_VENDOR_ACCOUNT_SET.Round_Off,TSPL_VENDOR_ACCOUNT_SET.Short_Excess from TSPL_VENDOR_MASTER " + Environment.NewLine +
                  "left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account" + Environment.NewLine +
                  "where TSPL_VENDOR_MASTER.Vendor_Code='" + obj.VSP_CODE + "'"
            Dim dtVAS As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            For Each objTr As clsMilkSRNMCCDetail In clsMilkSRNMCC.ObjList
                If objTr.AMOUNT <= 0 Then
                    Continue For
                End If
                qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_CODE + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + objTr.Item_CODE + "(" + objTr.Item_Desc + ")")
                End If
                ''1)
                Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_CODE + "(" + objTr.Item_Desc + ") ")
                End If
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.MCC_CODE, trans)

                Dim AccDr() As String = {strInvCtrlAC, Math.Round(((objTr.NET_AMOUNT)), 2, MidpointRounding.ToEven), "", "", "", "", "", "", "I"} ' Dim AccDr() As String = {strInvCtrlAC, Math.Round(((objTr.acc_Qty * objTr.RATE)), 2, MidpointRounding.ToEven)}
                ArryLstGLAC.Add(AccDr)
                'Ticket No- TEC/12/03/19-000442 sanjay TEC/12/03/19-000441
                clsInventoryMovement.UpdateInvControlAccount(obj.DOC_CODE, "MCC-MSRN", objTr.Item_CODE, strInvCtrlAC, "", "I", trans)
                'Ticket No- TEC/12/03/19-000442 sanjay
                ''2
                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                'skg
                If clsCommon.myLen(strPaybleCleanigCtrlAC) <= 0 Then
                    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_CODE + "(" + objTr.Item_Desc + ") ")
                End If
                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.MCC_CODE, trans)
                Dim AccCr() As String = {strPaybleCleanigCtrlAC, -1 * Math.Round((((objTr.NET_AMOUNT))), 2, MidpointRounding.ToEven), "", "", "", "", "", "", "Y"}
                ArryLstGLAC.Add(AccCr)
                qry = "select Mcc_Code From tspl_Milk_Sample_Head where Doc_COde='" & obj.MILK_SAMPLE_CODE & "'"
                If clsCommon.myLen(obj.Against_Reject_No) > 0 Then
                    qry = "select Mcc_Code From TSPL_MILK_REJECT_HEAD where Doc_COde = '" & obj.Against_Reject_No & "'"
                End If

                Dim Main_Mcc_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                If clsCommon.CompairString(Main_Mcc_Code, obj.MCC_CODE) <> CompairStringResult.Equal Then
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, Main_Mcc_Code, trans)
                    Dim AccCr1() As String = {strPaybleCleanigCtrlAC, -1 * Math.Round((((objTr.NET_AMOUNT))), 2, MidpointRounding.ToEven), "", "", "", "", "", "", "Y"}
                    ArryLstGLAC.Add(AccCr1)
                    Dim StrShipment As String = clsERPFuncationality.GetLocationSegment(obj.MCC_CODE, trans)
                    Dim strMainLoc As String = clsERPFuncationality.GetLocationSegment(Main_Mcc_Code, trans)
                    qry = "select Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING where From_Location='" + strMainLoc + "' and To_Location='" + StrShipment + "'"
                    Dim strBrachACofShiptoLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(strBrachACofShiptoLoc) <= 0 Then
                        Throw New Exception("Please set Brach account with From Location=" + StrShipment + " and To Location=" + strMainLoc + "")
                    End If
                    Dim AccCr2() As String = {strBrachACofShiptoLoc, Math.Round((((objTr.NET_AMOUNT))), 2, MidpointRounding.ToEven)}
                    ArryLstGLAC.Add(AccCr2)
                End If
                If objTr.Round_Off <> 0 Then
                    If dtVAS Is Nothing OrElse dtVAS.Rows.Count <= 0 Then
                        Throw New Exception("Please set account set for vendor " + obj.VSP_CODE)
                    End If
                    Dim strAccount As String = clsCommon.myCstr(dtVAS.Rows(0)("Round_Off"))
                    If clsCommon.myLen(strAccount) <= 0 Then
                        Throw New Exception("Please set round off account for vendor account set " + clsCommon.myCstr(dtVAS.Rows(0)("Acct_Set_Code")))
                    End If
                    strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, obj.MCC_CODE, trans)
                    Dim AccCr2() As String = {strAccount, objTr.Round_Off}
                    ArryLstGLAC.Add(AccCr2)

                    strAccount = clsCommon.myCstr(dtVAS.Rows(0)("Short_Excess"))
                    If clsCommon.myLen(strAccount) <= 0 Then
                        Throw New Exception("Please set Short Excess account for vendor account set " + clsCommon.myCstr(dtVAS.Rows(0)("Acct_Set_Code")))
                    End If
                    strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, obj.MCC_CODE, trans)
                    Dim AccCr3() As String = {strAccount, -1 * objTr.Round_Off}
                    ArryLstGLAC.Add(AccCr3)
                End If

            Next

            Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.VSP_CODE & "'", trans)
            '' BHA/30/10/18-000646 RICHA AGARWAL SEND Vendor NAME INTO JOURNAL ENTRY 30 Oct,2018
            If clsCommon.myLen(create_same_voucher_journal_entry) <= 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_CODE, False, trans, obj.DOC_DATE, "Against Milk SRN ( " & obj.DOC_CODE & ")  For Vsp : " & vendor_name, "MI-SR", "Milk Store Received Note", obj.DOC_CODE, "", "V", obj.VSP_CODE, vendor_name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.MCC_CODE, False, create_same_voucher_journal_entry, trans, obj.DOC_DATE, "Against Milk SRN ( " & obj.DOC_CODE & ")  For Vsp : " & vendor_name, "MI-SR", "Milk Store Received Note", obj.DOC_CODE, "", "V", obj.VSP_CODE, vendor_name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            End If
        End If
        Return True
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_SRN_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MILK_SRN_HEAD", "DOC_CODE", "TSPL_MILK_SRN_DETAIL", "DOC_CODE", trans)
            qry = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_CODE =(select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE='" + strCode + "')"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_SAMPLE_HEAD where MILK_SAMPLE_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' LOG FOR SYNC DATA
            clsSyncHeadTables.SaveSyncDelete("TSPL_MILK_SAMPLE_HEAD", strCode, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveFailedSampleApproveData(ByVal arr As Dictionary(Of String, Boolean), ByVal dtfrom As DateTime, ByVal dtto As DateTime) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strCurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm:ss tt")
            If arr Is Nothing OrElse arr.Count <= 0 Then
                Throw New Exception("No Data found to approve failed samples")
            End If
            For Each strSRNNo As String In arr.Keys
                Dim qry As String = "select doc_code  from TSPL_MILK_PURCHASE_INVOICE_HEAD  where " + Environment.NewLine +
                " exists( select 1 from TSPL_MILK_SRN_HEAD where TSPL_MILK_SRN_HEAD.MCC_CODE=TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE and TSPL_MILK_SRN_HEAD.VSP_CODE=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE and TSPL_MILK_SRN_HEAD.DOC_CODE='" + strSRNNo + "'  ) and VENDOR_INVOICE_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtfrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and VENDOR_INVOICE_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtto), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Purchase Invoice -" + clsCommon.myCstr(dt.Rows(0)("doc_code")) + " Generated for same payment cycle againist SRN No-" + clsCommon.myCstr(strSRNNo))
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Failed_Sample_Status", IIf(arr(strSRNNo), 1, 0))
                clsCommon.AddColumnsForChange(coll, "Failed_Sample_Approve_By", IIf(arr(strSRNNo), objCommonVar.CurrentUserCode, ""), True)
                clsCommon.AddColumnsForChange(coll, "Failed_Sample_Approve_Date", IIf(arr(strSRNNo), strCurrentDate, ""), True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, "DOC_CODE='" + strSRNNo + "'", tran)
            Next
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function



    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal dblQty As Decimal)
        clsMilkSRNMCC.Correction(strSRNNo, True, False, False, dblQty, "", 0, 0, "")
    End Sub

    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal)
        clsMilkSRNMCC.Correction(strSRNNo, False, True, False, 0, "", dblFAT, dblSNF, "")
    End Sub

    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal strType As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal)
        clsMilkSRNMCC.Correction(strSRNNo, False, True, False, 0, strType, dblFAT, dblSNF, "")
    End Sub

    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal strVLC As String)
        clsMilkSRNMCC.Correction(strSRNNo, False, False, True, 0, "", 0, 0, strVLC)
    End Sub

    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal CorrTypeSRNQty As Boolean, ByVal CorrTypeSRNFATSNF As Boolean, ByVal CorrTypeSRNVLC As Boolean, ByVal dblQty As Decimal, ByVal strType As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal, ByVal strVLCUploaderCode As String)
        clsMilkSRNMCC.Correction(strSRNNo, CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, dblQty, strType, dblFAT, dblSNF, strVLCUploaderCode, False, "")
    End Sub

    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal CorrTypeSRNQty As Boolean, ByVal CorrTypeSRNFATSNF As Boolean, ByVal CorrTypeSRNVLC As Boolean, ByVal dblQty As Decimal, ByVal strType As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal, ByVal strVLCUploaderCode As String, ByVal Form_ID As String)
        clsMilkSRNMCC.Correction(strSRNNo, CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, dblQty, strType, dblFAT, dblSNF, strVLCUploaderCode, False, Form_ID)
    End Sub

    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal CorrTypeSRNQty As Boolean, ByVal CorrTypeSRNFATSNF As Boolean, ByVal CorrTypeSRNVLC As Boolean, ByVal dblQty As Decimal, ByVal strType As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal, ByVal strVLCUploaderCode As String, ByVal IsCapping As Boolean, ByVal Form_ID As String)
        Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsMilkSRNMCC.Correction(strSRNNo, CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, dblQty, strType, dblFAT, dblSNF, strVLCUploaderCode, IsCapping, Trans, Form_ID)
            Trans.Commit()
        Catch ex As Exception
            Trans.Rollback()
        End Try
    End Sub
    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal CorrTypeSRNQty As Boolean, ByVal CorrTypeSRNFATSNF As Boolean, ByVal CorrTypeSRNVLC As Boolean, ByVal dblQty As Decimal, ByVal strType As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal, ByVal strVLCUploaderCode As String, ByVal IsCapping As Boolean, ByVal Trans As SqlTransaction, ByVal Form_ID As String)
        Correction(strSRNNo, CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, dblQty, strType, dblFAT, dblSNF, strVLCUploaderCode, IsCapping, Trans, False, Form_ID)
    End Sub



    Public Shared Sub Correction(ByVal strSRNNo As String, ByVal CorrTypeSRNQty As Boolean, ByVal CorrTypeSRNFATSNF As Boolean, ByVal CorrTypeSRNVLC As Boolean, ByVal dblQty As Decimal, ByVal strType As String, ByVal dblFAT As Decimal, ByVal dblSNF As Decimal, ByVal strVLCUploaderCode As String, ByVal IsCapping As Boolean, ByVal Trans As SqlTransaction, ByVal IsOwnBMCAdjustment As Boolean, ByVal Form_ID As String)
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Trans)) > 0)
        Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, Trans)) > 0)
        Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Trans)
        Dim MilkWeight_Setting As Double = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Trans)
        Dim settMaxReceiveSNFPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Trans))
        Dim settMaxFATPerLimit As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Trans))
        Dim settMaxSNFPerLimit As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Trans))
        If IsOwnBMCAdjustment Then
            settMaxReceiveSNFPer = 0
            settMaxFATPerLimit = 0
            settMaxSNFPerLimit = 0
        End If
        Dim IsRoundOffPaiseAmount As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Trans)) = 1
        Dim SettShowAllMCC As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllMCC, clsFixedParameterCode.ShowAllMCC, Trans)) = 1)
        Try
            Dim counter As Integer = 0
            Dim Net_amt As Double = 0
            Dim objHead As clsMilkSRNMCC = clsMilkSRNMCC.GetData(strSRNNo, NavigatorType.Current, Trans)
            Dim strMilkReceiptCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MILK_RECEIPT_CODE from tspl_milk_sample_Head where doc_code='" & objHead.MILK_SAMPLE_CODE & "'", Trans))
            If clsCommon.myLen(strMilkReceiptCode) <= 0 Then
                Throw New Exception("Milk Receipt No Not found")
            End If
            If objHead.Failed_Sample_Status Then
                Throw New Exception("SRN No -" + objHead.DOC_CODE + ".Approve failed sample so can't apply any correction on it")
            End If
            If IsCapping Then
                clsMilkSRNMCC.ObjList(0).Capping_FAT = clsMilkSRNMCC.ObjList(0).FAT
                clsMilkSRNMCC.ObjList(0).Capping_SNF = clsMilkSRNMCC.ObjList(0).SNF
            End If

            Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
            Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail
            Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
            Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail

            objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
            objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)

            Dim str As String = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & objHead.MCC_CODE & "' "
            Dim Unit_Code As String = clsDBFuncationality.getSingleValue(str, Trans)
            If Unit_Code = "" Then
                clsCommon.MyMessageBoxShow("Fill UOM of Current Mcc")
                Exit Sub
            End If
            str = "select UOM_Code from TSPL_Item_UOM_DETAIL where Item_CODE='" & clsMilkSRNMCC.ObjList(0).Item_CODE & "' and UOM_code='" & Unit_Code & "' "
            Dim Item_Unit_Code As String = clsDBFuncationality.getSingleValue(str, Trans)
            If Item_Unit_Code = "" Then
                clsCommon.MyMessageBoxShow("Fill " & Unit_Code & " UOM of Current Item")
                Exit Sub
            End If
            Dim conv_fac As Decimal = clsWeightConversionInfo.GetConversion_factor(clsMilkSRNMCC.ObjList(0).Item_CODE, Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), Trans)

            Dim qry As String


            Dim strMilkType As String = objHead.Dock_Collection_Milk_Type
            If objCommonVar.DisplayTypeInMilkReceipt Then
                qry = "select Type from tspl_milk_sample_detail where doc_code='" & objHead.MILK_SAMPLE_CODE & "' and sample_no='" & clsCommon.myCstr(objHead.SAMPLE_NO) & "' "
                strMilkType = clsDBFuncationality.getSingleValue(qry, Trans)
                If clsCommon.myLen(strMilkType) <= 0 Then
                    Throw New Exception("Type Not found for milk Sample Doc No [" + objHead.MILK_SAMPLE_CODE + "] and Sample No [" + clsCommon.myCstr(objHead.SAMPLE_NO) + "]")
                End If
                objHead.Dock_Collection_Milk_Type = strType
            End If

            If CorrTypeSRNQty Then
                Dim dblLTRQty As Decimal = 0
                Dim Unit_CodeApply As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Milk_Receive_UOM from TSPL_VLC_MASTER_HEAD where VLC_Code='" + objHead.VLC_CODE + "'", Trans))
                If clsCommon.myLen(Unit_CodeApply) > 0 Then
                    Unit_Code = Unit_CodeApply
                    conv_fac = clsWeightConversionInfo.GetConversion_factor(Unit_CodeApply, IIf(clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal, "LTR", "KG"), Trans)
                End If

                Dim strCustomUOM As String = clsItemMaster.GetCustomConversionUOM(clsMilkSRNMCC.ObjList(0).Item_CODE, Trans)
                If clsCommon.myLen(strCustomUOM) > 0 Then
                    conv_fac = 1 + (clsMilkSRNMCC.ObjList(0).CLR / 1000)
                End If



                If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).ACC_Qty = dblQty
                    clsMilkSRNMCC.ObjList(0).MILK_Qty = dblQty
                    dblLTRQty = dblQty * conv_fac
                Else
                    dblLTRQty = dblQty
                    clsMilkSRNMCC.ObjList(0).MILK_Qty = dblQty
                    clsMilkSRNMCC.ObjList(0).ACC_Qty = dblQty * conv_fac
                End If
                If clsCommon.myCdbl(MilkWeight_Setting) <= 0 Then
                    Throw New Exception("Please set Fix paratment vale of type-" + clsFixedParameterType.Milk_Can_Weight_Ratio + " and code-" + clsFixedParameterCode.MilkSetting)
                End If
                clsMilkSRNMCC.ObjList(0).UOM = Unit_Code
                Dim NoOfCans As Integer = Math.Ceiling(clsMilkSRNMCC.ObjList(0).ACC_Qty / MilkWeight_Setting)

                qry = "update TSPL_MILK_RECEIPT_DETAIL set NO_OF_CANS='" + clsCommon.myCstr(NoOfCans) + "', ACC_WEIGHT='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).ACC_Qty) + "',ACC_WEIGHT_LTR='" + clsCommon.myCstr(dblLTRQty) + "',MILK_WEIGHT='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).MILK_Qty) + "',UOM_Code='" + clsMilkSRNMCC.ObjList(0).UOM + "'  where DOC_CODE='" + strMilkReceiptCode + "' and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                qry = " update TSPL_MILK_SAMPLE_DETAIL set Qty='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).MILK_Qty) + "',ACC_Qty='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).ACC_Qty) + "',FAT_KG=" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).ACC_Qty) + "*FAT/100,SNF_KG=" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).ACC_Qty) + "*SNF/100,UOM_Code='" + clsMilkSRNMCC.ObjList(0).UOM + "' where doc_code= (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where MILK_RECEIPT_CODE='" + strMilkReceiptCode + "')and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
            End If
            If CorrTypeSRNVLC Then
                qry = "select VLC_Code,VSP_Code,Route_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader ='" + strVLCUploaderCode + "'   "
                If Not SettShowAllMCC Then
                    qry += " and MCC='" + objHead.MCC_CODE + "' "
                End If
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                    Throw New Exception("Not a Valid VLC Uploader Code:" + strVLCUploaderCode)
                End If
                objHead.VLC_CODE = clsCommon.myCstr(dtTemp.Rows(0)("VLC_Code"))
                objHead.VSP_CODE = clsCommon.myCstr(dtTemp.Rows(0)("VSP_Code"))
                objHead.ROUTE_CODE = clsCommon.myCstr(dtTemp.Rows(0)("Route_Code"))
                If clsCommon.myLen(objHead.VLC_CODE) <= 0 Then
                    Throw New Exception("Not a Valid VLC Uploader Code:" + strVLCUploaderCode)
                End If

                qry = "update TSPL_MILK_SAMPLE_DETAIL set VSP_CODE='" + objHead.VSP_CODE + "'  where doc_code= '" + objHead.MILK_SAMPLE_CODE + "' and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                qry = " update TSPL_MILK_RECEIPT_DETAIL set  VLC_CODE='" + objHead.VLC_CODE + "',VSP_CODE='" + objHead.VSP_CODE + "',ROUTE_CODE='" + objHead.ROUTE_CODE + "' where DOC_CODE=(select MILK_RECEIPT_CODE from TSPL_MILK_SAMPLE_HEAD where DOC_CODE='" + objHead.MILK_SAMPLE_CODE + "') and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                qry = "update TSPL_MILK_SRN_HEAD set VLC_CODE='" + objHead.VLC_CODE + "',VSP_CODE='" + objHead.VSP_CODE + "',ROUTE_CODE='" + objHead.ROUTE_CODE + "'  where DOC_CODE='" + objHead.DOC_CODE + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                '

                '
            End If
            If CorrTypeSRNFATSNF Then
                clsMilkSRNMCC.ObjList(0).FAT = Math.Truncate(dblFAT * 10) / 10
                If isPickCLRInsteadOfSNF Then
                    clsMilkSRNMCC.ObjList(0).CLR = clsEkoPro.getClrOnCalculation(dblFAT, dblSNF, corrFactor)
                End If
                If objCommonVar.MilkProcurementSNF2DecimalPlaces Then
                    clsMilkSRNMCC.ObjList(0).SNF = Math.Round(dblSNF, 2, MidpointRounding.AwayFromZero)
                Else
                    clsMilkSRNMCC.ObjList(0).SNF = Math.Truncate(dblSNF * 10) / 10
                End If


                If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Retesting_OR_Correction_Status = 1
                End If
                If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkProcurementCorrection) = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Retesting_OR_Correction_Status = 2
                End If
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    ''Do not change exception "Milk Type [" by balwinder used in form.
                    If clsCommon.CompairString(strType, "M") = CompairStringResult.Equal Then
                        If objCommonVar.AddValidationofMilkTypeinsample Then
                            If clsMilkSRNMCC.ObjList(0).FAT < objCommonVar.FatMinMix OrElse clsMilkSRNMCC.ObjList(0).FAT > objCommonVar.FatMaxMix Then
                                Throw New Exception("Milk Type [" + strType + "] " + Environment.NewLine + " FAT [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinMix) + " - " + clsCommon.myCstr(objCommonVar.FatMaxMix) + "]")
                            ElseIf clsMilkSRNMCC.ObjList(0).SNF < objCommonVar.SNFMinMix OrElse clsMilkSRNMCC.ObjList(0).SNF > objCommonVar.SNFMaxMix Then
                                Throw New Exception("Milk Type [" + strType + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinMix) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxMix) + "]")
                            End If
                        End If
                        clsMilkSRNMCC.ObjList(0).Item_CODE = objCommonVar.DefaultMilkItemCode
                    ElseIf clsCommon.CompairString(strType, "C") = CompairStringResult.Equal Then
                        If objCommonVar.AddValidationofMilkTypeinsample Then
                            If clsMilkSRNMCC.ObjList(0).FAT < objCommonVar.FatMinCow OrElse clsMilkSRNMCC.ObjList(0).FAT > objCommonVar.FatMaxCow Then
                                Throw New Exception("Milk Type [" + strType + "] " + Environment.NewLine + "FAT [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinCow) + " - " + clsCommon.myCstr(objCommonVar.FatMaxCow) + "]")
                            ElseIf clsMilkSRNMCC.ObjList(0).SNF < objCommonVar.SNFMinCow OrElse clsMilkSRNMCC.ObjList(0).SNF > objCommonVar.SNFMaxCow Then
                                Throw New Exception("Milk Type [" + strType + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinCow) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxCow) + "]")
                            End If
                        End If
                        clsMilkSRNMCC.ObjList(0).Item_CODE = objCommonVar.DefaultMilkItemCodeCow
                    ElseIf clsCommon.CompairString(strType, "B") = CompairStringResult.Equal Then
                        If objCommonVar.AddValidationofMilkTypeinsample Then
                            If clsMilkSRNMCC.ObjList(0).FAT < objCommonVar.FatMinBuff OrElse clsMilkSRNMCC.ObjList(0).FAT > objCommonVar.FatMaxBuff Then
                                Throw New Exception("Milk Type [" + strType + "] " + Environment.NewLine + "FAT [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinBuff) + " - " + clsCommon.myCstr(objCommonVar.FatMaxBuff) + "]")
                            ElseIf clsMilkSRNMCC.ObjList(0).SNF < objCommonVar.SNFMinBuff OrElse clsMilkSRNMCC.ObjList(0).SNF > objCommonVar.SNFMaxBuff Then
                                Throw New Exception("Milk Type [" + strType + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinBuff) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxBuff) + "]")
                            End If
                        End If
                        clsMilkSRNMCC.ObjList(0).Item_CODE = objCommonVar.DefaultMilkItemCodeBuffalo
                    Else
                        Throw New Exception("Milk Type should be M/B/C")
                    End If
                    strMilkType = strType
                    qry = "update TSPL_MILK_RECEIPT_DETAIL set Item_Code='" + clsMilkSRNMCC.ObjList(0).Item_CODE + "',type='" + strType + "' where DOC_CODE='" + strMilkReceiptCode + "' and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                    qry = " update TSPL_MILK_SAMPLE_DETAIL set item_code='" + clsMilkSRNMCC.ObjList(0).Item_CODE + "',type='" + strType + "'  where doc_code= (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where MILK_RECEIPT_CODE='" + strMilkReceiptCode + "')and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, Trans)


                End If


                If settMaxReceiveSNFPer > 0 And clsMilkSRNMCC.ObjList(0).SNF > settMaxReceiveSNFPer Then
                    Throw New Exception("SNF % Can't be more than -" + clsCommon.myCstr(settMaxReceiveSNFPer))
                End If
                If settMaxFATPerLimit > 0 And clsMilkSRNMCC.ObjList(0).FAT > settMaxFATPerLimit Then
                    Throw New Exception("FAT % Can't be more than -" + clsCommon.myCstr(settMaxFATPerLimit))
                End If
                If settMaxSNFPerLimit > 0 And clsMilkSRNMCC.ObjList(0).SNF > settMaxSNFPerLimit Then
                    Throw New Exception("SNF % Can't be more than -" + clsCommon.myCstr(settMaxSNFPerLimit))
                End If
            End If


            qry = "select rd.*,rh.*,Case when Nature='C' then Actual_charges end as  commision_pers," _
        & " Case when Nature='E' then Actual_charges end as payment_commision_pers,Service_Charge_Type,coalesce(Rate_Head_Load,0) as Rate_Head_Load" _
        & " ,coalesce(Rate_Own_Asset,0) as Rate_Own_Asset,Service_Basis_Head_Load,Service_Basis_Own_Asset,TSPL_VENDOR_MASTER.EMP_Type " _
        & " ,TSPL_VENDOR_MASTER.EMP_Fixed_Amount " _
        & " ,TSPL_VENDOR_MASTER.Actual_charges_Slab " _
        & " ,TSPL_VENDOR_MASTER.Actual_charges " _
        & ",TSPL_VENDOR_MASTER.Actual_charges_Slab2" _
        & ",TSPL_VENDOR_MASTER.Actual_charges2" _
        & ",TSPL_VENDOR_MASTER.Actual_charges_Slab3" _
        & ",TSPL_VENDOR_MASTER.Actual_charges3" _
        & ",TSPL_VENDOR_MASTER.Actual_charges_Slab4" _
        & ",TSPL_VENDOR_MASTER.Actual_charges4" _
        & ",TSPL_VENDOR_MASTER.Actual_charges_Slab5" _
        & ",TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix,TSPL_VENDOR_MASTER.DistanceKM_Head_Load 
            from TSPL_MILK_RECEIPT_HEAD rd 
            Inner join TSPL_MILK_RECEIPT_DETAIL rh on rh.DOC_CODE=" _
        & " rd.DOC_CODE left join TSPL_VENDOR_MASTER on Vendor_Code=VSP_CODE where rd.DOC_CODE='" & strMilkReceiptCode & "' and rh.vlc_DOC_Code='" & objHead.VLC_DOC_CODE & "'"
            Dim DtMilkReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)



            objHead.VEHICLE_CODE = clsCommon.myCstr(DtMilkReceipt.Rows(0)("VEHICLE_CODE"))
            objHead.VLC_CODE = clsCommon.myCstr(DtMilkReceipt.Rows(0)("VLC_CODE"))
            objHead.ROUTE_CODE = clsCommon.myCstr(DtMilkReceipt.Rows(0)("ROUTE_CODE"))

            Dim DtVehicle As DataTable = clsDBFuncationality.GetDataTable("SELECT vm.* FROM TSPL_Primary_Vehicle_Master vm where Vehicle_Code='" & clsCommon.myCstr(DtMilkReceipt.Rows(0)("VEHICLE_CODE")) & "'", Trans)
            If DtVehicle IsNot Nothing AndAlso DtVehicle.Rows.Count > 0 Then
                objHead.TransPorter = clsCommon.myCstr(DtVehicle.Rows(0)("Vendor_Code"))
            End If

            qry = "select max(QAT) as QAT from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE='" + strMilkReceiptCode + "' and SAMPLE_NO=" + clsCommon.myCstr(objHead.SAMPLE_NO) + ")"
            Dim MilkShiftUploderQAT As Boolean = IIf(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, Trans)) = 1, True, False)

            If isPickCLRInsteadOfSNF Then
                Dim strPriceCode As String = ""
                If PickPriceFromFATAndSNF Then
                    clsMilkSRNMCC.ObjList(0).RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsMilkSRNMCC.ObjList(0).MILK_Qty, clsMilkSRNMCC.ObjList(0).Price_Code, clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType, clsMilkSRNMCC.ObjList(0).QAT_Rate, clsMilkSRNMCC.ObjList(0).Negative_Rate)
                Else
                    clsMilkSRNMCC.ObjList(0).RATE = clsEkoPro.getRateFromUploaderShiftWiseCLR(clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).CLR, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType, strPriceCode)
                    clsMilkSRNMCC.ObjList(0).Price_Code = strPriceCode
                End If
            Else
                clsMilkSRNMCC.ObjList(0).RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsMilkSRNMCC.ObjList(0).MILK_Qty, clsMilkSRNMCC.ObjList(0).Price_Code, clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType, clsMilkSRNMCC.ObjList(0).QAT_Rate, clsMilkSRNMCC.ObjList(0).Negative_Rate)
            End If
            If Not MilkShiftUploderQAT Then
                clsMilkSRNMCC.ObjList(0).QAT_Rate = 0
            End If
            clsMilkSRNMCC.ObjList(0).QAT_Amt = clsMilkSRNMCC.ObjList(0).QAT_Rate * clsMilkSRNMCC.ObjList(0).MILK_Qty
            clsMilkSRNMCC.ObjList(0).Negative_Amount = clsMilkSRNMCC.ObjList(0).Negative_Rate * clsMilkSRNMCC.ObjList(0).MILK_Qty
            clsMilkSRNMCC.ObjList(0).AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).RATE * clsMilkSRNMCC.ObjList(0).MILK_Qty, 2, MidpointRounding.AwayFromZero)
            clsMilkSRNMCC.ObjList(0).Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("commision_pers"))
            clsMilkSRNMCC.ObjList(0).Own_Asset_Rate = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Rate_Own_Asset"))
            clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("payment_commision_pers"))
            If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * clsMilkSRNMCC.ObjList(0).Payment_Commission / 100, 2)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                Else
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = 0
                    'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                End If
                If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Emp_Amount += clsCommon.myCdbl(DtMilkReceipt.Rows(0)("EMP_Fixed_Amount"))
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges5"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges4"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges3"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges2"))
                    Else
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                    End If
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * clsMilkSRNMCC.ObjList(0).Payment_Commission / 100, 2)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges5"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges4"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges3"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges2"))
                    Else
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                    End If
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges5"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges4"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges3"))
                    ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) Then
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges2"))
                    Else
                        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                    End If
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                Else
                    clsMilkSRNMCC.ObjList(0).Emp_Amount = 0
                    'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                End If
                If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                    clsMilkSRNMCC.ObjList(0).Emp_Amount += clsCommon.myCdbl(DtMilkReceipt.Rows(0)("EMP_Fixed_Amount"))
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FPSP") = CompairStringResult.Equal Then
                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt(0)("Actual_charges"))
                Dim objSPR As clsStandardPrice = clsStandardPrice.GetStandartPrice(clsMilkSRNMCC.ObjList(0).Price_Code, Trans)
                If objSPR IsNot Nothing Then
                    If (objSPR.Std_Percent_FAT <> 0 AndAlso objSPR.Std_Percent_SNF <> 0) Then
                        If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round((Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            Dim qty As Decimal = clsMilkSRNMCC.ObjList(0).ACC_Qty
                            If conv_fac <> 0 Then
                                qty = clsMilkSRNMCC.ObjList(0).ACC_Qty / conv_fac
                            End If
                            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round((Math.Round(qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                        Else
                            clsMilkSRNMCC.ObjList(0).Emp_Amount = 0
                            'Throw New Exception("EMP Service Basis is Not valid of VSP " + clsMilkSRNMCC.ObjList(0).VlC_Code)
                        End If
                    End If
                End If
            Else
                Throw New Exception("EMP Type is Not a valid ")
            End If

            If clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                clsMilkSRNMCC.ObjList(0).TIP_Amount = Math.Round(clsCommon.myCdbl(DtMilkReceipt(0)("TIP_Cow")) * (clsMilkSRNMCC.ObjList(0).FAT + clsMilkSRNMCC.ObjList(0).SNF) * clsMilkSRNMCC.ObjList(0).ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
            ElseIf clsCommon.CompairString(strMilkType, "B") = CompairStringResult.Equal Then
                clsMilkSRNMCC.ObjList(0).TIP_Amount = Math.Round(clsCommon.myCdbl(DtMilkReceipt(0)("TIP_Buffalo")) * clsMilkSRNMCC.ObjList(0).FAT * clsMilkSRNMCC.ObjList(0).ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
            Else
                clsMilkSRNMCC.ObjList(0).TIP_Amount = Math.Round(clsCommon.myCdbl(DtMilkReceipt(0)("TIP_Mix")) * clsMilkSRNMCC.ObjList(0).FAT * clsMilkSRNMCC.ObjList(0).ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
            End If

            clsMilkSRNMCC.ObjList(0).Service_Charge_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type"))
            '==================Head Load==========================
            Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, Trans))
            Dim dclDistanceKM As Decimal = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("DistanceKM_Head_Load"))
            If dclDistanceKM = 0 Then
                dclDistanceKM = 1
            End If
            Dim objHeadLoad As New clsHeadLoadDCS()
            objHeadLoad = clsHeadLoadDCS.GetDcsData(objHead.VLC_CODE, objHead.DOC_DATE, Trans)
            clsMilkSRNMCC.ObjList(0).Head_Load_Rate = clsCommon.myCdbl(objHeadLoad.Head_Load_Rate)

            clsMilkSRNMCC.ObjList(0).Head_Load_Amount = 0
            If clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "K") = CompairStringResult.Equal Then
                If clsMilkSRNMCC.ObjList(0).ACC_Qty >= MinimumQtyForHeadLoad Then
                    clsMilkSRNMCC.ObjList(0).Head_Load_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "L") = CompairStringResult.Equal Then
                If clsCommon.myCDecimal(DtMilkReceipt.Rows(0)("ACC_WEIGHT_LTR")) >= MinimumQtyForHeadLoad Then
                    clsMilkSRNMCC.ObjList(0).Head_Load_Amount = Math.Round(clsCommon.myCDecimal(DtMilkReceipt.Rows(0)("ACC_WEIGHT_LTR")) * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                End If
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Head_Load")), "W") = CompairStringResult.Equal Then
                '    qry = "select Ratio,SNF_Ratio,FAT_Pers,SNF_Pers from TSPL_MILK_PRICE_MASTER where Price_Code=(select top 1 Price_Code from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsMilkSRNMCC.ObjList(0).Price_Code + "')"
                '    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                '    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                '        clsMilkSRNMCC.ObjList(0).FAT_KG = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 3)
                '        clsMilkSRNMCC.ObjList(0).SNF_KG = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 3)
                '        Dim dblFATRate As Decimal = clsMilkSRNMCC.ObjList(0).Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("FAT_Pers"))
                '        Dim dblSNFRate As Decimal = clsMilkSRNMCC.ObjList(0).Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Pers"))
                '        clsMilkSRNMCC.ObjList(0).Head_Load_Amount = Math.Round(((clsMilkSRNMCC.ObjList(0).FAT_KG * dblFATRate) + (clsMilkSRNMCC.ObjList(0).SNF_KG * dblSNFRate)) * dclDistanceKM, 2)
                '    End If
            End If
            clsMilkSRNMCC.ObjList(0).Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
            '============================================
            '==================Own Asset==========================
            If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                clsMilkSRNMCC.ObjList(0).Own_Asset_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Own_Asset_Rate, 2)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                clsMilkSRNMCC.ObjList(0).Own_Asset_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Own_Asset_Rate, 2)
            End If
            clsMilkSRNMCC.ObjList(0).Own_Asset_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Own_Asset"))
            '============================================

            clsMilkSRNMCC.ObjList(0).Service_Charge_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Service_Charge_Per_Unit")), 2)
            clsMilkSRNMCC.ObjList(0).NET_AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT + clsMilkSRNMCC.ObjList(0).Emp_Amount + clsMilkSRNMCC.ObjList(0).TIP_Amount - clsMilkSRNMCC.ObjList(0).Service_Charge_Amount, 2)
            If IsRoundOffPaiseAmount Then
                clsMilkSRNMCC.ObjList(0).Round_Off = (clsMilkSRNMCC.ObjList(0).NET_AMOUNT Mod 1)
                clsMilkSRNMCC.ObjList(0).NET_AMOUNT = clsMilkSRNMCC.ObjList(0).NET_AMOUNT - (clsMilkSRNMCC.ObjList(0).NET_AMOUNT Mod 1)
            End If

            '============VSp Charge Detail=====================
            Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING where Vsp_Code='" & objHead.VSP_CODE & "'", Trans)
            For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Rows
                objVSP_Charge1 = New clsMilkSRNVSpChargeDetail()
                objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objHead.VSP_CODE)
                objVSP_Charge1.Vlc_Doc_Code = clsCommon.myCstr(objHead.VLC_DOC_CODE)
                objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
                objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
                objVSP_Charge1.Service_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type"))
                If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
                    objVSP_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
                    objVSP_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(DtMilkReceipt.Rows(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                    objVSP_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
                End If
                objVSPChargeList.Add(objVSP_Charge1)
            Next
            '===========================================


            '============Price Charge Detail=====================
            Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail where Price_Code='" & clsMilkSRNMCC.ObjList(0).Price_Code & "'", Trans)


            For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Rows
                objPrice_Charge1 = New clsMilkSRNPriceChargeDetail()
                objPrice_Charge1.Price_Code = clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).Price_Code)
                objPrice_Charge1.Vlc_Doc_Code = objHead.VLC_DOC_CODE
                objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
                objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
                objPrice_Charge1.Service_type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type"))
                If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
                    objPrice_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
                    objPrice_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(DtMilkReceipt.Rows(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                    objPrice_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
                End If
                objPriceChargeList.Add(objPrice_Charge1)
            Next
            '===========================================
            clsMilkSRNMCC.ObjList(0).Std_Qty = clsInventoryMovementNew.GetStdQty(Trans, Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 2), Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 2), objHead.DOC_DATE)

            clsMilkSRNMCC.UpdateDataFromSRNFrom(objHead, clsMilkSRNMCC.ObjList, objVSPChargeList, objPriceChargeList, Trans)
            clsMilkSRNMCC.updateJournalEntryWithTran("MI-SR", objHead.DOC_CODE, Trans)
            clsMilkSRNMCC.UpdateSample(objHead.MILK_SAMPLE_CODE, objHead.SAMPLE_NO, clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, clsMilkSRNMCC.ObjList(0).CLR, clsMilkSRNMCC.ObjList(0).RATE, clsMilkSRNMCC.ObjList(0).AMOUNT, Trans, clsMilkSRNMCC.ObjList(0).Price_Code, clsMilkSRNMCC.ObjList(0).QAT_Rate, clsMilkSRNMCC.ObjList(0).Negative_Rate)

            CorrectBackDocs(CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, strMilkReceiptCode, objHead.SAMPLE_NO, objHead.VLC_CODE, dblQty, strType, dblFAT, IIf(isPickCLRInsteadOfSNF, clsMilkSRNMCC.ObjList(0).CLR, dblSNF), Trans)
            If IsCapping Then
                qry = "Update TSPL_MILK_SRN_HEAD set Capping_Apply=1 where DOC_CODE='" + objHead.DOC_CODE + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
            End If



            objVSPChargeList = Nothing
            objPriceChargeList = Nothing

            objHead = Nothing

            DtMilkReceipt = Nothing
            DtVehicle = Nothing
            DtVSPChargeDetail = Nothing
            DtPriceChargeDetail = Nothing
            strMilkReceiptCode = String.Empty
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Shared Function CorrectBackDocs(corrTypeSRNQty As Boolean, corrTypeSRNFATSNF As Boolean, corrTypeSRNVLC As Boolean, strMilkReceiptCode As String, sAMPLE_NO As Integer, strVLCCode As String, dblQty As Decimal, strType As String, dblFAT As Decimal, dblSNF As Decimal, trans As SqlTransaction) As Boolean
        Dim qry As String = "select TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No as Against_Shift_Uploader_DocNo,TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No,
TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No as Against_Uploader_DocNo ,TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No
from TSPL_MILK_RECEIPT_DETAIL 
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No
where TSPL_MILK_RECEIPT_DETAIL.DOC_CODE='" + strMilkReceiptCode + "' and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=" + clsCommon.myCstr(sAMPLE_NO) + ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim intAgainst_Milk_Collection_DCS_Detail As Integer = -1
            If clsCommon.myLen(dt.Rows(0)("Against_Shift_Uploader_TR_No")) > 0 Then
                Dim Arr As List(Of clsMilkShiftUploaderDetail) = clsMilkShiftUploaderDetail.GetData(clsCommon.myCstr(dt.Rows(0)("Against_Shift_Uploader_DocNo")), " TR_No='" + clsCommon.myCstr(dt.Rows(0)("Against_Shift_Uploader_TR_No")) + "'", trans)
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    If corrTypeSRNQty Then
                        Arr(0).Milk_Weight = dblQty
                    End If
                    If corrTypeSRNVLC Then
                        Arr(0).VLC_Code = strVLCCode
                    End If
                    If corrTypeSRNFATSNF Then
                        Arr(0).FAT = dblFAT
                        Arr(0).SNF = dblSNF
                    End If
                    intAgainst_Milk_Collection_DCS_Detail = Arr(0).Against_Milk_Collection_DCS_Detail
                    clsMilkShiftUploaderDetail.SaveData(Arr(0).Document_No, "", Arr, trans, Arr(0).TR_No)
                End If
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_Uploader_TR_No")) > 0 Then
                Dim Arr As List(Of clsMilkProcurementUploaderDetail) = clsMilkProcurementUploaderDetail.GetData(clsCommon.myCstr(dt.Rows(0)("Against_Uploader_DocNo")), " TR_No='" + clsCommon.myCstr(dt.Rows(0)("Against_Uploader_TR_No")) + "'", trans)
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    If corrTypeSRNQty Then
                        Arr(0).Milk_Weight = dblQty
                    End If
                    If corrTypeSRNVLC Then
                        Arr(0).VLC_Code = strVLCCode
                    End If
                    If corrTypeSRNFATSNF Then
                        Arr(0).FAT = dblFAT
                        Arr(0).SNF = dblSNF
                    End If
                    intAgainst_Milk_Collection_DCS_Detail = Arr(0).Against_Milk_Collection_DCS_Detail
                    clsMilkProcurementUploaderDetail.SaveData(Arr(0).Document_No, "", Arr, trans, Arr(0).TR_No)
                End If
            End If
            If intAgainst_Milk_Collection_DCS_Detail > 0 Then
                qry = "select Document_No from TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id=" + clsCommon.myCstr(intAgainst_Milk_Collection_DCS_Detail) + ""
                qry = clsDBFuncationality.getSingleValue(qry, trans)
                Dim Arr As List(Of clsMilkCollectionDCSDetail) = clsMilkCollectionDCSDetail.GetData(qry, " PK_Id=" + clsCommon.myCstr(intAgainst_Milk_Collection_DCS_Detail) + "", trans)
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    If corrTypeSRNQty Then
                        Arr(0).Qty = dblQty
                        Arr(0).FATKG = Math.Round(Arr(0).Qty * Arr(0).FAT / 100, 3, MidpointRounding.ToEven)
                        Arr(0).SNFKG = Math.Round(Arr(0).Qty * Arr(0).SNF / 100, 3, MidpointRounding.ToEven)
                    End If
                    If corrTypeSRNVLC Then
                        Arr(0).VLC_Code = strVLCCode
                    End If
                    If corrTypeSRNFATSNF Then
                        Arr(0).FAT = dblFAT
                        Arr(0).SNF = dblSNF
                        Arr(0).FATKG = Math.Round(Arr(0).Qty * Arr(0).FAT / 100, 3, MidpointRounding.ToEven)
                        Arr(0).SNFKG = Math.Round(Arr(0).Qty * Arr(0).SNF / 100, 3, MidpointRounding.ToEven)
                    End If
                    clsMilkCollectionDCSDetail.SaveData(Arr(0).Document_No, Arr, trans, Arr(0).PK_Id, False)
                End If
            End If
        End If
        Return True
    End Function
End Class


Public Class clsMilkSRNMCCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_DETAIL_CODE As String
    Public Item_CODE As String
    Public Item_Desc As String
    Public UOM As String
    Public Price_Code As String
    Public MCC_CODE As String
    Public COMM_PORT As String
    Public VlC_Code As String
    Public Invoice_Code As String
    Public VlC_Doc_Code As String
    Public MILK_Qty As Decimal
    Public ACC_Qty As Decimal
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public Retesting_FAT As Decimal = 0
    Public Retesting_SNF As Decimal = 0
    Public Retesting_OR_Correction_Status As Integer = 0
    Public Capping_FAT As Decimal = 0
    Public Capping_SNF As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public CLR As Decimal = 0
    Public Cans As Decimal = 0
    Public Service_Charge_Type As String
    Public Commission As Decimal = 0
    Public Payment_Commission As Decimal = 0
    Public Correction_Factor As Decimal = 0
    Public RATE As Decimal = 0
    Public AMOUNT As Decimal = 0
    Public Service_Charge_Amount As Decimal = 0
    Public NET_AMOUNT As Decimal = 0
    Public Round_Off As Decimal = 0
    Public Is_Entered_Manualy As String

    Public Commission_Amount As Decimal = 0
    Public Emp_Amount As Decimal = 0
    Public TIP_Amount As Decimal = 0
    Public Head_Load_Rate As Decimal = 0
    Public Own_Asset_Rate As Decimal = 0
    Public Head_Load_Amount As Decimal = 0
    Public Own_Asset_Amount As Decimal = 0
    Public Head_Load_Type As String
    Public Own_Asset_Type As String
    Public Std_Qty As Decimal

    Public VSP_Commission_Amount As Decimal
    Public VSP_Commission_Apply As Boolean
    Public VSP_Deduction_Amount As Decimal
    Public VSP_Deduction_Apply As Boolean
    Public QAT_Rate As Decimal
    Public QAT_Amt As Decimal
    Public Negative_Rate As Decimal
    Public Negative_Amount As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Dock_Collection_Milk_Type As String, ByVal Arr As List(Of clsMilkSRNMCCDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Against_Reject_No As String, ByVal SAMPLE_NO As Integer) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkSRNMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.Item_CODE)
                clsCommon.AddColumnsForChange(coll, "QTY", obj.MILK_Qty)
                clsCommon.AddColumnsForChange(coll, "ACC_QTY", obj.ACC_Qty)
                clsCommon.AddColumnsForChange(coll, "FAT_PER", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF_PER", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "Retesting_OR_Correction_Status", obj.Retesting_OR_Correction_Status, True)

                If obj.Retesting_OR_Correction_Status = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Retesting_FAT", obj.FAT, True)
                    clsCommon.AddColumnsForChange(coll, "Retesting_SNF", obj.SNF, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Capping_FAT", obj.Capping_FAT, True)
                clsCommon.AddColumnsForChange(coll, "Capping_SNF", obj.Capping_SNF, True)
                'clsERPFuncationality.myDclInZeroPointFive()
                clsCommon.AddColumnsForChange(coll, "FAT_KG", clsERPFuncationality.myFloor(obj.ACC_Qty * obj.FAT / 100, objCommonVar.MilkSRNFATSNFDecimalPlaces))
                clsCommon.AddColumnsForChange(coll, "SNF_KG", clsERPFuncationality.myFloor(obj.ACC_Qty * obj.SNF / 100, objCommonVar.MilkSRNFATSNFDecimalPlaces))
                clsCommon.AddColumnsForChange(coll, "Correction_factor", obj.Correction_Factor)
                clsCommon.AddColumnsForChange(coll, "RATE", obj.RATE)
                clsCommon.AddColumnsForChange(coll, "Uom_Code", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)


                clsCommon.AddColumnsForChange(coll, "Commission_Pers", obj.Commission)
                clsCommon.AddColumnsForChange(coll, "Commission_Amount", obj.Commission_Amount)
                clsCommon.AddColumnsForChange(coll, "EMP_Pers", obj.Payment_Commission)

                If objCommonVar.PricePlan = 4 Then
                    Dim dblFATRatio As Decimal = 0
                    Dim dtFATSNFUploader As DataTable = clsDBFuncationality.GetDataTable("select top 1 Price_Code,Planning_Code  from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + obj.Price_Code + "'", trans)
                    If dtFATSNFUploader IsNot Nothing AndAlso dtFATSNFUploader.Rows.Count > 0 Then
                        obj.Emp_Amount = clsEkoPro.GetRateCalculated(clsCommon.myCstr(dtFATSNFUploader.Rows(0)("Planning_Code")), clsCommon.myCstr(dtFATSNFUploader.Rows(0)("Price_Code")), obj.MILK_Qty, obj.FAT, obj.SNF, trans, obj.Payment_Commission, dblFATRatio)
                    End If
                    obj.AMOUNT = Math.Round(obj.AMOUNT, 0, MidpointRounding.ToEven)
                    obj.NET_AMOUNT = clsERPFuncationality.myFloor(obj.AMOUNT + obj.Emp_Amount, 0)
                    obj.Round_Off = 0
                    clsCommon.AddColumnsForChange(coll, "FAT_Ratio", dblFATRatio)
                End If
                clsCommon.AddColumnsForChange(coll, "Service_Charge_Amount", IIf(obj.AMOUNT = 0, 0, obj.Service_Charge_Amount))
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.AMOUNT)
                clsCommon.AddColumnsForChange(coll, "NET_AMOUNT", IIf(obj.AMOUNT = 0, 0, obj.NET_AMOUNT))
                clsCommon.AddColumnsForChange(coll, "Round_Off", obj.Round_Off)
                clsCommon.AddColumnsForChange(coll, "EMP_Amount", obj.Emp_Amount)
                clsCommon.AddColumnsForChange(coll, "TIP_Amount", obj.TIP_Amount)
                clsCommon.AddColumnsForChange(coll, "Service_Charge_Type", obj.Service_Charge_Type)
                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                If clsCommon.myLen(Against_Reject_No) > 0 Then
                    If (clsDBFuncationality.getSingleValue("select TSPL_MILK_REJECT_TYPE.Exclude_Head from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.Code=TSPL_MILK_REJECT_DETAIL.Reject_Type
where DOC_CODE='" + clsCommon.myCstr(Against_Reject_No) + "' and SAMPLE_NO=" + clsCommon.myCstr(SAMPLE_NO) + " and TSPL_MILK_REJECT_TYPE.Exclude_Head is not null", trans)) = 1 Then
                        obj.Head_Load_Rate = 0
                        obj.Head_Load_Amount = 0
                    End If
                End If

                clsCommon.AddColumnsForChange(coll, "Head_Load_Rate", obj.Head_Load_Rate)
                clsCommon.AddColumnsForChange(coll, "Head_Load_Amount", obj.Head_Load_Amount)


                clsCommon.AddColumnsForChange(coll, "Own_Asset_Amount", obj.Own_Asset_Amount)

                clsCommon.AddColumnsForChange(coll, "Own_Asset_Rate", obj.Own_Asset_Rate)
                clsCommon.AddColumnsForChange(coll, "Head_Load_Type", obj.Head_Load_Type)
                clsCommon.AddColumnsForChange(coll, "Own_Asset_Type", obj.Own_Asset_Type)
                clsCommon.AddColumnsForChange(coll, "Std_Qty", obj.Std_Qty)
                clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)

                clsCommon.AddColumnsForChange(coll, "QAT_Rate", obj.QAT_Rate, True)
                clsCommon.AddColumnsForChange(coll, "QAT_Amt", obj.QAT_Amt, True)

                clsCommon.AddColumnsForChange(coll, "Negative_Rate", obj.Negative_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Negative_Amount", obj.Negative_Amount, True)
                Dim flag As Boolean = True
                Dim settVSPDayWiseIncentiveAtSRN As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPDayWiseIncentiveAtSRN, clsFixedParameterCode.VSPDayWiseIncentiveAtSRN, trans)) > 0)
                If settVSPDayWiseIncentiveAtSRN Then
                    If clsCommon.CompairString(Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal Then
                        If obj.FAT <= clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdFatCow, clsFixedParameterCode.SubStdFatCow, trans)) _
                            OrElse obj.SNF <= clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdSNFCow, clsFixedParameterCode.SubStdSNFCow, trans)) Then
                            flag = False
                        End If
                    ElseIf clsCommon.CompairString(Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal Then
                        If obj.FAT <= clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdFatBuff, clsFixedParameterCode.SubStdFatBuff, trans)) _
                            OrElse obj.SNF <= clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdSNFBuff, clsFixedParameterCode.SubStdSNFBuff, trans)) Then
                            flag = False
                        End If
                    Else
                        If obj.FAT <= clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdFatMix, clsFixedParameterCode.SubStdFatMix, trans)) _
                            OrElse obj.SNF <= clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdSNFMix, clsFixedParameterCode.SubStdSNFMix, trans)) Then
                            flag = False
                        End If
                    End If
                    If flag Then
                        clsCommon.AddColumnsForChange(coll, "Sub_Standard", 0)
                        If obj.AMOUNT > 0 Then
                            Dim qry As String = "select TSPL_MCC_MASTER.incentive_Code,TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR as Qty,isnull( TSPL_INCENTIVE_DETAIL.Rate,0) as Rate " + Environment.NewLine +
                            "from TSPL_MILK_SRN_HEAD  " + Environment.NewLine +
                            "left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE" + Environment.NewLine +
                            "left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=tspl_milk_Srn_head.SAMPLE_NO" + Environment.NewLine +
                            "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=tspl_milk_Srn_head.MCC_CODE" + Environment.NewLine +
                            "left outer join TSPL_INCENTIVE_DETAIL on TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE=TSPL_MCC_MASTER.incentive_Code" + Environment.NewLine +
                            "left outer join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE " + Environment.NewLine +
                            "where len(isnull(TSPL_MCC_MASTER.incentive_Code,''))>0 and tspl_milk_Srn_head.doc_code='" + strDocNo + "' " + Environment.NewLine +
                            " and 2= (case when TSPL_INCENTIVE_MASTER_HEAD.START_DATE is null then 2 else (case when convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=TSPL_INCENTIVE_MASTER_HEAD.START_DATE then 2 else 3 end) end ) and 2= (case when TSPL_INCENTIVE_MASTER_HEAD.END_DATE is null then 2 else (case when convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)<=TSPL_INCENTIVE_MASTER_HEAD.END_DATE  then 2 else 3 end) end ) "
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                clsCommon.AddColumnsForChange(coll, "VSP_Day_Wise_Incentive_Rate", clsCommon.myCdbl(dt.Rows(0)("Rate")))
                                clsCommon.AddColumnsForChange(coll, "VSP_Day_Wise_Incentive", Math.Round(clsCommon.myCdbl(dt.Rows(0)("Rate")) * clsCommon.myCdbl(dt.Rows(0)("Qty")), 0))
                                flag = False
                            End If
                        End If
                    Else
                        flag = True
                        clsCommon.AddColumnsForChange(coll, "Sub_Standard", 1)
                    End If
                End If
                If flag Then
                    clsCommon.AddColumnsForChange(coll, "VSP_Day_Wise_Incentive_Rate", 0)
                    clsCommon.AddColumnsForChange(coll, "VSP_Day_Wise_Incentive", 0)
                End If
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "VSP_Mapping_Code", Nothing, True)
                    clsCommon.AddColumnsForChange(coll, "Farmer_Pro_Code", Nothing, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_SRN_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                End If
            Next
        End If
        Return True
    End Function

End Class


Public Class clsMilkSRNVSpChargeDetail
#Region "Variables"
    Public DOC_CODE As String
    Public Vsp_Code As String
    Public Vlc_Doc_Code As String
    Public Charge_Code As String
    Public Charge_Rate As String
    Public Service_Type As String
    Public AMOUNT As Decimal = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkSRNVSpChargeDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkSRNVSpChargeDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Vsp_Code", obj.Vsp_Code)
                clsCommon.AddColumnsForChange(coll, "Charge_Code", obj.Charge_Code)
                clsCommon.AddColumnsForChange(coll, "Charge_Rate", obj.Charge_Rate)
                clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_Type)
                clsCommon.AddColumnsForChange(coll, "Vlc_Doc_Code", obj.Vlc_Doc_Code)
                clsCommon.AddColumnsForChange(coll, "Charge_AMOUNT", obj.AMOUNT)
                If isNewEntry Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_VSP_Charge_Detail", OMInsertOrUpdate.Insert, "TSPL_MILK_SRN_VSP_Charge_Detail.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_VSP_Charge_Detail", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_VSP_Charge_Detail.DOC_CODE='" + strDocNo + "' and TSPL_MILK_SRN_VSP_Charge_Detail.Vlc_Doc_Code='" & obj.Vlc_Doc_Code & "'", trans)
                End If
            Next
        End If
        Return True
    End Function

End Class

Public Class clsMilkSRNPriceChargeDetail
#Region "Variables"
    Public DOC_CODE As String
    Public Price_Code As String
    Public Vlc_Doc_Code As String
    Public Charge_Code As String
    Public Charge_Rate As String
    Public Service_type As String
    Public AMOUNT As Decimal = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkSRNPriceChargeDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkSRNPriceChargeDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Charge_Code", obj.Charge_Code)
                clsCommon.AddColumnsForChange(coll, "Charge_Rate", obj.Charge_Rate)
                clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_type)
                clsCommon.AddColumnsForChange(coll, "Vlc_Doc_Code", obj.Vlc_Doc_Code)
                clsCommon.AddColumnsForChange(coll, "Charge_AMOUNT", obj.AMOUNT)
                If isNewEntry Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_Price_Charge_Detail", OMInsertOrUpdate.Insert, "TSPL_MILK_SRN_Price_Charge_Detail.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_Price_Charge_Detail", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_Price_Charge_Detail.DOC_CODE='" + strDocNo + "' and TSPL_MILK_SRN_Price_Charge_Detail.Vlc_Doc_Code='" & obj.Vlc_Doc_Code & "'", trans)
                End If
            Next
        End If
        Return True
    End Function

End Class


Public Class clsMilkSampleReadingLog
#Region "Variables"
    Public Sample_Code As String
    Public Dock_Code As String
    Public Sample_No As Integer
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
#End Region

    Public Function SaveData(ByVal obj As clsMilkSampleReadingLog) As Boolean
        If clsCommon.myLen(obj.Sample_Code) > 0 AndAlso obj.FAT > 0 And obj.SNF > 0 Then
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Sample_Code", obj.Sample_Code)
            clsCommon.AddColumnsForChange(coll, "Dock_Code", obj.Dock_Code, True)
            clsCommon.AddColumnsForChange(coll, "Sample_No", obj.Sample_No)
            clsCommon.AddColumnsForChange(coll, "FAT", Math.Truncate(obj.FAT * 10) / 10)
            clsCommon.AddColumnsForChange(coll, "SNF", Math.Truncate(obj.SNF * 10) / 10)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_READING_LOG", OMInsertOrUpdate.Insert, "")
        End If
        Return True
    End Function
End Class


Public Class clsMilkSampleQCParameterDetail
    Public Doc_Code As String = String.Empty
    Public Sample_No As Integer = 0
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty

    Public Shared Function saveData(ByVal strQCNo As String, ByVal arrObj As List(Of clsMilkSampleQCParameterDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal arrObj As List(Of clsMilkSampleQCParameterDetail), ByVal trans As SqlTransaction) As Boolean
        Return saveData(True, strQCNo, arrObj, trans)
    End Function
    Public Shared Function saveData(ByVal isRunDeleteQuery As Boolean, ByVal strQCNo As String, ByVal arrObj As List(Of clsMilkSampleQCParameterDetail), ByVal trans As SqlTransaction) As Boolean
        If isRunDeleteQuery Then
            Dim qry As String = "delete from TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL where Doc_Code='" & strQCNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsMilkSampleQCParameterDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "Sample_No", obj.Sample_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMilkSampleQCParameterDetail)
        Dim arrObj As List(Of clsMilkSampleQCParameterDetail) = Nothing
        Try
            Dim obj As clsMilkSampleQCParameterDetail = Nothing
            Dim qry As String = "select * from TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL where Doc_Code='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkSampleQCParameterDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkSampleQCParameterDetail()
                    obj.Doc_Code = clsCommon.myCstr(dt.Rows(i)("Doc_Code"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.Sample_No = clsCommon.myCdbl(dt.Rows(i)("Sample_No"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function

    Public Shared Function GetExtraQCParameters() As DataTable
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  where 1=1 and is_milk_sample=1 and Type not in ('FAT','SNF')  order by Ordering "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
End Class


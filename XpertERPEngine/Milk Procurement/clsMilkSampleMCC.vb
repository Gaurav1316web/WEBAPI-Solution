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
        Catch err As Exception
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


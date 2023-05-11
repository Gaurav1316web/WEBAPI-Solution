'=====================Created By Preeti Gupta===10/08/2015=========
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMCCSampleQCHead
#Region "Variables"

    Public DOC_CODE As String
    Public MCC_CODE As String
    Public MILK_RECEIPT_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public TOTAL_QTY As Decimal
    Public TOTAL_FAT As Decimal = 0
    Public TOTAL_SNF As Decimal = 0

    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of clsMCCSampleQCDetail)
    Public Shared ObjListParam As List(Of clsMCCSampleQCParameterDetail)
    Public arrObj As List(Of ClssampleQCParameterValue) = Nothing
    'Public Shared ObjListHistory As List(Of clsMCCSampleQCDetailHistory)

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMCCSampleQCHead

        Return GetData(strCode, NavType, Nothing)
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

            qry = "update TSPL_MILK_RECEIPT_DETAIL set Is_Sampleed='F' where VLC_DOC_CODE in (select VLC_DOC_CODE from TSPL_MCC_Sample_QC_detail " _
            & " inner join TSPL_MCC_Sample_QC_Head on TSPL_MCC_Sample_QC_detail.DOC_CODE=TSPL_MCC_Sample_QC_Head.DOC_CODE where TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" _
            & " =MILK_Receipt_CODE AND TSPL_MCC_Sample_QC_detail.DOC_CODE='" & strCode & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MCC_Sample_QC_detail where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MCC_Sample_QC_Head where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

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

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMCCSampleQCHead

        Dim obj As New clsMCCSampleQCHead()
        Dim objtr As New clsMCCSampleQCDetail

        ObjList = New List(Of clsMCCSampleQCDetail)
        ObjListParam = New List(Of clsMCCSampleQCParameterDetail)

        Dim qry As String = "SELECT TSPL_MCC_Sample_QC_Head.DOC_CODE,TSPL_MCC_Sample_QC_Head.MCC_CODE,TSPL_MCC_Sample_QC_Head.MILK_RECEIPT_CODE,TSPL_MCC_Sample_QC_Head.DOC_DATE,TSPL_MCC_Sample_QC_Head.SHIFT,"
        qry += " TSPL_MCC_Sample_QC_Head.TOTAL_QTY,TSPL_MCC_Sample_QC_Head.TOTAL_SNF,TSPL_MCC_Sample_QC_Head.TOTAL_FAT,TSPL_MCC_MASTER.MCC_NAME, "
        qry += " TSPL_MCC_Sample_QC_Head.Created_By,TSPL_MCC_Sample_QC_Head.Posted,TSPL_MCC_Sample_QC_Head.POSTED,TSPL_MCC_Sample_QC_Head.POSTING_DATE FROM TSPL_MCC_Sample_QC_Head  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MCC_Sample_QC_Head.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MCC_Sample_QC_Head)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MCC_Sample_QC_Head)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MCC_Sample_QC_Head where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MCC_Sample_QC_Head where DOC_CODE<'" + strCode + "')"
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
        qry += " TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Uom_Code,Correction_Factor,CLR,RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,ACC_QTY " _
        & " FROM TSPL_MCC_Sample_QC_detail sd inner join TSPL_MCC_Sample_QC_Head sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code," _
        & " SAMPLE_NO_VALUES,VLC_DOC_CODE as vlc_d,eco_pro_name,No_Of_Cans as cans,vlc_code,sample_no as sn_no,Vlc_Doc_Code from  TSPL_MILK_RECEIPT_DETAIL  " _
        & " ) tt on Receipt_code=MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and sample_no=sn_no  WHERE 2=2"
        qry += " AND sd.DOC_CODE = '" + strCode + "' ORDER BY sd.SAMPLE_NO"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMCCSampleQCDetail

                objtr.DOC_CODE = strCode
                objtr.QC_VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                ' objtr.MILK_RECEIPT_CODE = clsCommon.myCstr(dr("MILK_RECEIPT_CODE"))
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
                'objtr.Milk_Sample_CODE = clsCommon.myCstr(dr("Milk_Sample_CODE"))
                'objtr.Sample_VLC_DOC_CODE = clsCommon.myCstr(dr("Sample_VLC_DOC_CODE"))
                ObjList.Add(objtr)
            Next
        End If

        clsMCCSampleQCHead.ObjList = ObjList
        clsMCCSampleQCHead.ObjListParam = clsMCCSampleQCParameterDetail.getData(obj.DOC_CODE, trans)
        obj.arrObj = ClssampleQCParameterValue.getData(obj.DOC_CODE, trans)
        Return obj
    End Function

    Public Sub GetSampleTable(ByVal strCode As String, ByVal trans As SqlTransaction)
        Dim qry As String = " SELECT sd.DOC_CODE,VLC_DOC_CODE,vlc_code,SAMPLE_NO,VSP_CODE,Item_Code,QTY, "
        qry += " TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Uom_Code,Correction_Factor,CLR,RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,ACC_QTY FROM TSPL_MCC_Sample_QC_detail sd inner join TSPL_MCC_Sample_QC_Head sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code,SAMPLE_NO_VALUES,VLC_DOC_CODE as vlc_d,eco_pro_name,No_Of_Cans as cans,vlc_code,sample_No as sm_no from  TSPL_MILK_RECEIPT_DETAIL   ) tt on Receipt_code=MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and sample_No=sm_No  WHERE 2=2"
        qry += " AND sd.DOC_CODE = '" + strCode + "' ORDER BY sd.SAMPLE_NO"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
    End Sub

    Public Shared Function SaveData(ByVal obj As clsMCCSampleQCHead, ByVal objList As List(Of clsMCCSampleQCDetail), ByVal objListParam As List(Of clsMCCSampleQCParameterDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            obj.DOC_CODE = GetDocCode(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans)
            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkSample, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
            Else
                isNewEntry = False
                'Dim Strqrys As String = "SELECT Count(*) FROM TSPL_MILK_SAMPLE_HEAD where DOC_CODE = '" & obj.DOC_CODE & "' and posted=1"
                'Dim checks As Integer = clsDBFuncationality.getSingleValue(Strqrys, trans)
                'If checks = 0 Then
                'Else
                '    common.clsCommon.MyMessageBoxShow("This Code:" + obj.DOC_CODE + " Is Already Exist")
                '    Exit Function
                'End If    'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
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

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MCC_Sample_QC_Head where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sample_QC_Head", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sample_QC_Head", OMInsertOrUpdate.Update, "TSPL_MCC_Sample_QC_Head.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            isSaved = isSaved And clsMCCSampleQCParameterDetail.saveData(ObjListParam, obj.DOC_CODE, trans)
            isSaved = isSaved AndAlso clsMCCSampleQCDetail.SaveData(obj.DOC_CODE, objList, trans, isNewEntry, obj.MILK_RECEIPT_CODE)
            'If objListHistory.Count > 0 Then
            '    isSaved = isSaved AndAlso clsMCCSampleQCDetailHistory.SaveData(obj.DOC_CODE, objListHistory, trans, isNewEntry, obj.MILK_RECEIPT_CODE)
            'End If
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.DOC_CODE, obj.arrCustomFields, trans)
            If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                isSaved = isSaved AndAlso ClssampleQCParameterValue.saveData(obj.arrObj, obj.DOC_CODE, trans)
            End If
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT DOC_CODE FROM TSPL_MCC_Sample_QC_Head WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'" ' and coalesce(posted,0)=0"
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
        qry = "SELECT TSPL_MCC_Sample_QC_Head.DOC_CODE FROM TSPL_MCC_Sample_QC_Head  left join TSPL_MCC_Sample_QC_detail on TSPL_MCC_Sample_QC_Head.doc_code=TSPL_MCC_Sample_QC_detail.DOC_CODE WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and sample_no='" & Sample_No & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            'For Each row As DataRow In dt.Rows
            '    arr_sam.Add(clsCommon.myCstr(row.Item("Doc_Code")))
            'Next
            'Return arr_sam
            Return True
        Else
            Return False
            'Else
            'Return ""
        End If
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMCCSampleQCHead = clsMCCSampleQCHead.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MCC_Sample_QC_Head set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MCC_Sample_QC_Head WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


End Class
Public Class clsMCCSampleQCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_DETAIL_CODE As String
    Public QC_VLC_DOC_CODE As String
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
    Public Milk_Sample_CODE As String
    Public Sample_VLC_DOC_CODE As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMCCSampleQCDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Receipt_Code As String) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMCCSampleQCDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "QC_VLC_DOC_CODE", obj.QC_VLC_DOC_CODE)
                '  clsCommon.AddColumnsForChange(coll, "MILK_RECEIPT_CODE", obj.MILK_RECEIPT_CODE)
                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
                clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
                clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.ITEm_CODE)
                clsCommon.AddColumnsForChange(coll, "QTY", obj.MILK_Qty)
                clsCommon.AddColumnsForChange(coll, "ACC_QTY", obj.ACC_Qty)
                ' clsCommon.AddColumnsForChange(coll, "MILK_SRN_CODE", obj.SRN_CODE)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", Math.Round(obj.ACC_Qty * obj.FAT / 100, 2))
                clsCommon.AddColumnsForChange(coll, "SNF_KG", Math.Round(obj.ACC_Qty * obj.SNF / 100, 2))
                clsCommon.AddColumnsForChange(coll, "UOM_Code", obj.UOM_Code)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Correction_factor", obj.Correction_Factor)
                clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                clsCommon.AddColumnsForChange(coll, "RATE", obj.RATE)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.AMOUNT)
                clsCommon.AddColumnsForChange(coll, "TYPE", obj.TYPE)
                clsCommon.AddColumnsForChange(coll, "MILK_TYPE", obj.MILK_TYPE)
                clsCommon.AddColumnsForChange(coll, "IS_MANUAL", obj.Is_Entered_Manualy)
                'clsCommon.AddColumnsForChange(coll, "Milk_Sample_CODE", obj.Milk_Sample_CODE)
                'clsCommon.AddColumnsForChange(coll, "QC_VLC_DOC_CODE", obj.Sample_VLC_DOC_CODE)


                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MCC_Sample_QC_detail where DOC_CODE = '" & strDocNo & "' and TSPL_MCC_Sample_QC_detail.QC_VLC_DOC_CODE='" & obj.QC_VLC_DOC_CODE & "' and sample_No='" & obj.SAMPLE_NO & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sample_QC_detail", OMInsertOrUpdate.Insert, "TSPL_MCC_Sample_QC_detail.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sample_QC_detail", OMInsertOrUpdate.Update, "TSPL_MCC_Sample_QC_detail.DOC_CODE='" + strDocNo + "' and TSPL_MCC_Sample_QC_detail.QC_VLC_DOC_CODE='" & obj.QC_VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "'", trans)
                End If
                Dim Squery As String = "update tspl_Milk_receipt_Detail set is_sampleed ='T' where Doc_Code='" & Receipt_Code & "' and vlc_doc_Code='" & obj.QC_VLC_DOC_CODE & "'  and sample_No='" & obj.SAMPLE_NO & "'"
                clsDBFuncationality.ExecuteNonQuery(Squery, trans)
            Next

        End If

        Return True
    End Function


End Class
Public Class clsMCCSampleQCParameterDetail

    Public QC_No As String = String.Empty
    Public QC_Vlc_Doc_Code As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public Shared Function saveData(ByVal arrObj As List(Of clsMCCSampleQCParameterDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Try

            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                If Not isHistory Then
                    Dim qry As String = "delete from TSPL_MCC_Sample_QC_Parameter_Detail where Sample_QC_No='" & strQCNo & "'"
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                For Each obj As clsMCCSampleQCParameterDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Sample_QC_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "QC_Vlc_Doc_Code", obj.QC_Vlc_Doc_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_MCC_Sample_QC_Parameter_Detail_History", "TSPL_MCC_Sample_QC_Parameter_Detail"), OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMCCSampleQCParameterDetail)
        Dim arrObj As List(Of clsMCCSampleQCParameterDetail) = Nothing
        Try
            Dim obj As clsMCCSampleQCParameterDetail = Nothing
            Dim qry As String = "select * from TSPL_MCC_Sample_QC_Parameter_Detail where Sample_QC_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMCCSampleQCParameterDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMCCSampleQCParameterDetail()
                    obj.QC_No = clsCommon.myCstr(dt.Rows(i)("Sample_QC_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.QC_Vlc_Doc_Code = clsCommon.myCstr(dt.Rows(i)("QC_Vlc_Doc_Code"))
                    arrObj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
    Public Shared Function deleteData(ByVal strQCNo As String) As Boolean
        Dim isDeleted As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_MCC_Sample_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return isDeleted
    End Function


End Class
Public Class ClssampleQCParameterValue
    Public Sample_Code As String = String.Empty
    Public Parameter As String = String.Empty
    Public Lower_Range As String = String.Empty
    Public Upper_Range As String = String.Empty
    Public value As String = String.Empty
    Public Incen_Deduc As Double = 0
    Public QCValue As String = String.Empty
    Public Shared Function saveData(ByVal arrObj As List(Of ClssampleQCParameterValue), ByVal strSRNNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                Dim qry As String = "delete from TSPL_Sample_QC_Parameter_Range_Detail where Sample_Code='" & strSRNNo & "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As ClssampleQCParameterValue In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Sample_Code", obj.Sample_Code)
                    clsCommon.AddColumnsForChange(coll, "Parameter", obj.Parameter)
                    clsCommon.AddColumnsForChange(coll, "Lower_Range", obj.Lower_Range)
                    clsCommon.AddColumnsForChange(coll, "Upper_Range", obj.Upper_Range)
                    clsCommon.AddColumnsForChange(coll, "value", obj.value)
                    clsCommon.AddColumnsForChange(coll, "Incen_Deduc", obj.Incen_Deduc)
                    clsCommon.AddColumnsForChange(coll, "QCValue", obj.QCValue)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sample_QC_Parameter_Range_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strSRNNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of ClssampleQCParameterValue)
        Dim arrObj As List(Of ClssampleQCParameterValue) = Nothing
        Try
            Dim obj As ClssampleQCParameterValue = Nothing
            Dim qry As String = "select * from TSPL_Sample_QC_Parameter_Range_Detail where Sample_Code='" & strSRNNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClssampleQCParameterValue)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClssampleQCParameterValue()
                    obj.Sample_Code = clsCommon.myCstr(dt.Rows(i)("Sample_Code"))
                    obj.Parameter = clsCommon.myCstr(dt.Rows(i)("Parameter"))
                    obj.Lower_Range = clsCommon.myCstr(dt.Rows(i)("Lower_Range"))
                    obj.Upper_Range = clsCommon.myCstr(dt.Rows(i)("Upper_Range"))
                    obj.value = clsCommon.myCstr(dt.Rows(i)("value"))
                    obj.QCValue = clsCommon.myCstr(dt.Rows(i)("QCValue"))
                    obj.Incen_Deduc = clsCommon.myCdbl(dt.Rows(i)("Incen_Deduc"))
                    arrObj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
    Public Shared Function deleteData(ByVal strSRNNo As String) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_Sample_QC_Parameter_Range_Detail where Sample_Code='" & strSRNNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class





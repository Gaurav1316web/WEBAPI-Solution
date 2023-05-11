Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMilkShiftClosingMCC

#Region "Variables"

    Public DOC_CODE As String
    Public MCC_CODE As String
    Public DOC_DATE As Date
    Public SHIFT As String
    Public COMM_PORT As String
    Public MACHINE_NO As String
    Public TOTAL_WEIGHT As Decimal
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0

    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of clsMilkShiftClosingMCCDetail)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkShiftClosingMCC
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
            qry = "delete from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_RECEIPT_HEAD where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkShiftClosingMCC

        Dim obj As New clsMilkShiftClosingMCC()
        Dim objtr As New clsMilkShiftClosingMCCDetail

        ObjList = New List(Of clsMilkShiftClosingMCCDetail)

        Dim qry As String = "SELECT TSPL_MILK_RECEIPT_HEAD.DOC_CODE,TSPL_MILK_RECEIPT_HEAD.MCC_CODE,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,TSPL_MILK_RECEIPT_HEAD.SHIFT,TSPL_MILK_RECEIPT_HEAD.COMM_PORT,"
        qry += " TSPL_MILK_RECEIPT_HEAD.MACHINE_NO,TSPL_MILK_RECEIPT_HEAD.TOTAL_WEIGHT,TSPL_MCC_MASTER.MCC_NAME, "
        qry += " TSPL_MILK_RECEIPT_HEAD.Created_By,TSPL_MILK_RECEIPT_HEAD.Posted,TSPL_MILK_RECEIPT_HEAD.POSTED,TSPL_MILK_RECEIPT_HEAD.POSTING_DATE,FAT,SNF FROM TSPL_MILK_RECEIPT_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_RECEIPT_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.DOC_CODE = dt.Rows(0)("DOC_CODE")
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy")
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.COMM_PORT = clsCommon.myCstr(dt.Rows(0)("COMM_PORT"))
            obj.MACHINE_NO = clsCommon.myCstr(dt.Rows(0)("MACHINE_NO"))
            obj.TOTAL_WEIGHT = clsCommon.myCstr(dt.Rows(0)("TOTAL_WEIGHT"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.FAT = clsCommon.myCdbl(dt.Rows(0)("FAT"))
            obj.SNF = clsCommon.myCdbl(dt.Rows(0)("SNF"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            '        strCode = dt.Rows(0)("DOC_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = " SELECT DOC_CODE,VLC_DOC_CODE,SAMPLE_NO,VLC_CODE,ROUTE_CODE,VSP_CODE,Item_Code,VEHICLE_CODE, "
        qry += " NO_OF_CANS,MILK_WEIGHT,TYPE,MILK_TYPE,FAT,SNF,SAMPLE_NO_VALUES,MCC_CODE,DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,IS_MANUAL FROM TSPL_MILK_RECEIPT_DETAIL   WHERE 2=2"
        qry += " AND TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkShiftClosingMCCDetail

                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.ROUTE_CODE = clsCommon.myCstr(dr("ROUTE_CODE"))
                objtr.NO_OF_CANS = clsCommon.myCdbl(dr("NO_OF_CANS"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.Item_CODE = clsCommon.myCstr(dr("Item_CODE"))
                objtr.MILK_WEIGHT = clsCommon.myCdbl(dr("MILK_WEIGHT"))
                objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.TYPE = clsCommon.myCstr(dr("TYPE"))
                objtr.MILK_TYPE = clsCommon.myCstr(dr("MILK_TYPE"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.SAMPLE_NO_VALUES = clsCommon.myCstr(dr("SAMPLE_NO_VALUES"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))

                objtr.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr.SHIFT = clsCommon.myCstr(dr("SHIFT"))
                objtr.COMM_PORT = clsCommon.myCstr(dr("COMM_PORT"))
                objtr.MACHINE_NO = clsCommon.myCstr(dr("MACHINE_NO"))
                objtr.IS_ENTERED_MANUAL = clsCommon.myCstr(dr("IS_MANUAL"))
                ObjList.Add(objtr)
            Next
        End If

        clsMilkShiftClosingMCC.ObjList = ObjList
        Return obj
    End Function

    Public Function Gettable(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = " SELECT DOC_CODE as [DOC CODE],VLC_DOC_CODE as [VLC DOC CODE],SAMPLE_NO as [SAMPLE NO],VLC_CODE as [VLC CODE],TSPL_ITEM_MASTER.Item_Code + '(' + Item_Desc + ')' as [Item],ROUTE_CODE as [ROUTE CODE],VSP_CODE as [VSP CODE],VEHICLE_CODE as [VEHICLE CODE], "
        qry += " NO_OF_CANS as [NO OF CANS],MILK_WEIGHT as [MILK WEIGHT],TYPE,MILK_TYPE as [MILK TYPE],FAT,SNF,SAMPLE_NO_VALUES as [SAMPLE NO VALUES],MCC_CODE as [MCC CODE],DOC_DATE as [DOC DATE],SHIFT,COMM_PORT as [COM PORT],MACHINE_NO as [MACHINE NO],Case when IS_MANUAL='N' then 'Auto' else 'Manual' end as [Entry Type] FROM TSPL_MILK_RECEIPT_DETAIL inner join tspl_item_master on tspl_item_master.item_Code=TSPL_MILK_RECEIPT_DETAIL.item_Code  WHERE 2=2"
        qry += " AND TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function LoadDataFromDetails(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkShiftClosingMCC
        Dim obj As New clsMilkShiftClosingMCC()
        Dim objtr As New clsMilkShiftClosingMCCDetail

        ObjList = New List(Of clsMilkShiftClosingMCCDetail)
        Dim qry As String = " SELECT DOC_CODE,VLC_DOC_CODE,SAMPLE_NO,VLC_CODE,ROUTE_CODE,VSP_CODE,Item_Code,VEHICLE_CODE, "
        qry += " NO_OF_CANS,MILK_WEIGHT,TYPE,MILK_TYPE,FAT,SNF,SAMPLE_NO_VALUES,MCC_CODE,DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,is_Manual FROM TSPL_MILK_RECEIPT_DETAIL   WHERE 2=2"
        qry += " AND TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkShiftClosingMCCDetail

                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.ROUTE_CODE = clsCommon.myCstr(dr("ROUTE_CODE"))
                objtr.NO_OF_CANS = clsCommon.myCdbl(dr("NO_OF_CANS"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.Item_CODE = clsCommon.myCstr(dr("Item_CODE"))
                objtr.MILK_WEIGHT = clsCommon.myCdbl(dr("MILK_WEIGHT"))
                objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.TYPE = clsCommon.myCstr(dr("TYPE"))
                objtr.MILK_TYPE = clsCommon.myCstr(dr("MILK_TYPE"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.SAMPLE_NO_VALUES = clsCommon.myCstr(dr("SAMPLE_NO_VALUES"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))

                objtr.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr.SHIFT = clsCommon.myCstr(dr("SHIFT"))
                objtr.COMM_PORT = clsCommon.myCstr(dr("COMM_PORT"))
                objtr.MACHINE_NO = clsCommon.myCstr(dr("MACHINE_NO"))
                objtr.IS_ENTERED_MANUAL = clsCommon.myCstr(dr("IS_MANUAL"))
                ObjList.Add(objtr)
            Next
        End If
        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As clsMilkShiftClosingMCC, ByVal objList As List(Of clsMilkShiftClosingMCCDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean

            obj.DOC_CODE = GetDocCode(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans)
            If GetPost(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans) Then
                Throw New Exception("This Code:" + obj.DOC_CODE + " Is Posted.")
            End If
            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
            Else
                isNewEntry = False
                'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
            End If


            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)

            clsCommon.AddColumnsForChange(coll, "COMM_PORT", clsCommon.myCstr(obj.COMM_PORT))
            'clsCommon.AddColumnsForChange(coll, "ATTACHED_DOC", obj.ATTACHED_DOC)
            clsCommon.AddColumnsForChange(coll, "MACHINE_NO", clsCommon.myCstr(obj.MACHINE_NO))
            clsCommon.AddColumnsForChange(coll, "TOTAL_WEIGHT", clsCommon.myCstr(obj.TOTAL_WEIGHT))
            clsCommon.AddColumnsForChange(coll, "FAT", clsCommon.myCdbl(obj.FAT))
            clsCommon.AddColumnsForChange(coll, "SNF", clsCommon.myCdbl(obj.SNF))

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_RECEIPT_HEAD where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_RECEIPT_HEAD.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMilkShiftClosingMCCDetail.SaveData(obj.DOC_CODE, objList, trans)

            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMilkShiftClosingMCC = clsMilkShiftClosingMCC.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MILK_RECEIPT_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT DOC_CODE FROM TSPL_MILK_RECEIPT_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND DOC_DATE='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("DOC_CODE")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_RECEIPT_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND DOC_DATE='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class


Public Class clsMilkShiftClosingMCCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public VLC_DOC_CODE As String
    Public VLC_DOC_NUM As Integer
    Public SAMPLE_NO As Integer
    Public VLC_CODE As String
    Public ROUTE_CODE As String
    Public VSP_CODE As String
    Public Item_CODE As String
    Public VEHICLE_CODE As String
    Public NO_OF_CANS As Integer
    Public MILK_WEIGHT As Decimal
    Public TYPE As String
    Public MILK_TYPE As String
    Public SAMPLE_NO_VALUES As String

    Public FAT As Decimal = 0
    Public SNF As Decimal = 0

    Public MCC_CODE As String
    Public DOC_DATE As Date
    Public SHIFT As String
    Public COMM_PORT As String
    Public MACHINE_NO As String
    Public IS_ENTERED_MANUAL As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkShiftClosingMCCDetail), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkShiftClosingMCCDetail In Arr
                Dim coll As New Hashtable()
                Dim vlcDoc(1) As String
                vlcDoc = GetVLC_DOC_CODE(obj.VLC_CODE, trans, obj.VLC_DOC_CODE)
                Dim Sample(1) As String
                Sample = GetMilkSampleNo(obj, trans, obj.VLC_DOC_CODE)
                If obj.VLC_DOC_CODE = "" Then

                End If
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", vlcDoc(0))
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_NUM", vlcDoc(1))
                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", Sample(0))
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
                clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", obj.ROUTE_CODE)
                clsCommon.AddColumnsForChange(coll, "NO_OF_CANS", obj.NO_OF_CANS)
                clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
                clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.Item_CODE)
                clsCommon.AddColumnsForChange(coll, "MILK_WEIGHT", obj.MILK_WEIGHT)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
                clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)

                clsCommon.AddColumnsForChange(coll, "TYPE", obj.TYPE)
                clsCommon.AddColumnsForChange(coll, "MILK_TYPE", obj.MILK_TYPE)
                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO_VALUES", Sample(1))

                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
                'clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
                clsCommon.AddColumnsForChange(coll, "COMM_PORT", obj.COMM_PORT)
                clsCommon.AddColumnsForChange(coll, "MACHINE_NO", obj.MACHINE_NO)
                clsCommon.AddColumnsForChange(coll, "IS_MANUAL", obj.IS_ENTERED_MANUAL)
                If obj.VLC_DOC_CODE = "" Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_RECEIPT_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_RECEIPT_DETAIL.DOC_CODE='" + strDocNo + "' and TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE='" & vlcDoc(0) & "'", trans)
                End If

            Next

        End If

        Return True
    End Function
    Public Shared Function GetVLC_DOC_CODE(ByVal VLC_Code As String, ByVal trans As SqlTransaction, Optional ByVal vlc_doc_code As String = "") As String()
        Dim arr(1) As String
        Dim qry As String
        If vlc_doc_code = "" Then
            qry = "select (coalesce(MAX(VLC_DOC_NUM),0)+1) AS VLC_DOC_NUM from TSPL_MILK_RECEIPT_DETAIL  WHERE VLC_CODE='" & VLC_Code & "' "
        Else
            qry = "select  VLC_DOC_NUM from TSPL_MILK_RECEIPT_DETAIL  WHERE VLC_DOC_CODE='" & vlc_doc_code & "' "
        End If
        Dim dtVLC As DataTable
        dtVLC = clsDBFuncationality.GetDataTable(qry, trans)
        If dtVLC.Rows.Count > 0 Then
            arr(0) = IIf(vlc_doc_code = "", VLC_Code & "/" & dtVLC.Rows(0).Item("VLC_DOC_NUM"), vlc_doc_code)
            arr(1) = dtVLC.Rows(0).Item("VLC_DOC_NUM")
        Else
            arr(0) = VLC_Code & "/" & "0001"
            arr(1) = "1"
        End If
        Return arr
    End Function
    Public Shared Function GetMilkSampleNo(ByVal objDtl As clsMilkShiftClosingMCCDetail, ByVal trans As SqlTransaction, Optional ByVal vlc_doc_code As String = "") As String()
        Dim arr(1) As String
        Dim qry As String
        If vlc_doc_code = "" Then
            qry = "select (coalesce(MAX(SAMPLE_NO),0)+1) AS SAMPLE_NO from TSPL_MILK_RECEIPT_DETAIL WHERE VLC_CODE='" & objDtl.VLC_CODE & "' and MCC_CODE='" & objDtl.MCC_CODE & "' and DOC_DATE='" & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & "'  and SHIFT='" & objDtl.SHIFT & "' and COMM_PORT='" & objDtl.COMM_PORT & "' and  MACHINE_NO='" & objDtl.MACHINE_NO & "' "
        Else
            qry = "select  SAMPLE_NO from TSPL_MILK_RECEIPT_DETAIL  WHERE VLC_DOC_CODE='" & vlc_doc_code & "' "
        End If
        Dim dtSample As DataTable
        dtSample = clsDBFuncationality.GetDataTable(qry, trans)
        If dtSample.Rows.Count > 0 Then
            arr(0) = dtSample.Rows(0).Item("SAMPLE_NO")
            arr(1) = "VLC_CODE='" & objDtl.VLC_CODE & "' and MCC_CODE='" & objDtl.MCC_CODE & "' and DOC_DATE='" & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & "'  and SHIFT='" & objDtl.SHIFT & "' and COMM_PORT='" & objDtl.COMM_PORT & "' and  MACHINE_NO='" & objDtl.MACHINE_NO & "'"
        Else
            arr(0) = "1"
            arr(1) = "VLC_CODE='" & objDtl.VLC_CODE & "' and MCC_CODE='" & objDtl.MCC_CODE & "' and DOC_DATE='" & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & "'  and SHIFT='" & objDtl.SHIFT & "' and COMM_PORT='" & objDtl.COMM_PORT & "' and  MACHINE_NO='" & objDtl.MACHINE_NO & "'"
        End If
        Return arr
    End Function
End Class




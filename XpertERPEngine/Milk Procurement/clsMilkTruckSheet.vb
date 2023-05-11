Imports Common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMilk_Truck_Sheet

#Region "Variables"

    Public DOC_CODE As String
    Public MCC_CODE As String
    Public MILK_RECEIPT_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String

    
    Public Route_Code As String
    Public Route_Name As String
    Public Superviser_Name As String
    Public Vehicle_No As String
    Public Mcc_Arival_Time As String
    Public UnLoading_Time As String

    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of clsMilk_Truck_SheetDetail)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal strRoute_Code As String, ByVal NavType As NavigatorType) As clsMilk_Truck_Sheet
        Return GetData(strCode, strRoute_Code, NavType, Nothing)
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

            qry = "update TSPL_MILK_RECEIPT_DETAIL set Is_Truck_Sheet_Uploaded='F' where VLC_DOC_CODE in (select VLC_DOC_CODE from Tspl_Milk_Truck_Sheet_detail " _
            & " where TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" _
            & " =Tspl_Milk_Truck_Sheet_detail.MILK_Receipt_CODE AND Tspl_Milk_Truck_Sheet_detail.DOC_CODE='" & strCode & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from Tspl_Milk_Truck_Sheet_detail where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from Tspl_Milk_Truck_Sheet_Head where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' LOG FOR SYNC DATA
            isSaved = isSaved AndAlso clsSyncHeadTables.SaveSyncDelete("Tspl_Milk_Truck_Sheet_Head", strCode, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        'Dim dr As DataRow = Nothing
        Dim str As String = ""
        Try

            Dim qry As String = "select tspl_milk_truck_sheet_Head.DOC_CODE as [Code] ,tspl_milk_truck_sheet_Head.MCC_CODE as [Mcc Code] ,tspl_milk_truck_sheet_Head.DOC_DATE as [Doc Date] ,tspl_milk_truck_sheet_Head.SHIFT as [Shift] ,tspl_milk_truck_sheet_Head.Route_Code as [Route Code] ,tspl_milk_truck_sheet_Head.Superviser_Name as [Superviser Name] ,tspl_milk_truck_sheet_Head.Mcc_Arival_Time as [Mcc Arival Time] ,tspl_milk_truck_sheet_Head.UnLoading_Time as [Unloading Time] ,tspl_milk_truck_sheet_Head.Posted as [Posted] ,tspl_milk_truck_sheet_Head.Posting_Date as [Posting Date] ,tspl_milk_truck_sheet_Head.Created_By as [Created By] ,tspl_milk_truck_sheet_Head.Created_Date as [Created Date] ,tspl_milk_truck_sheet_Head.Modified_By as [Modified By] ,tspl_milk_truck_sheet_Head.Modified_Date as [Modified Date] ,tspl_milk_truck_sheet_Head.Comp_Code as [Comp Code] ,tspl_milk_truck_sheet_Head.milk_receipt_code as [Milk Receipt Code]  From tspl_milk_truck_sheet_Head "
            'str = clsCommon.ShowSelectForm("MCCMST-R", qry) ', "Code", whrcls, curcode, "Code", isButtonClicked)
            str = clsCommon.ShowSelectForm("MCCMST-R", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal strRoute_Code As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilk_Truck_Sheet

        Dim obj As New clsMilk_Truck_Sheet()
        Dim objtr As New clsMilk_Truck_SheetDetail

        ObjList = New List(Of clsMilk_Truck_SheetDetail)

        Dim qry As String = "select * from tspl_milk_receipt_detail where doc_code='" & strCode & "' and coalesce(is_sampleed,'F')='F'"
        Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If Dt.Rows.Count > 0 Then
            Throw New Exception("Some Receipt have not sampled please sample those Receipts First.")
            Return Nothing
        End If


        qry = "select * from tspl_milk_Sample_Head where milk_receipt_code='" & strCode & "' and coalesce(posted,'0')='0'"
        Dt = clsDBFuncationality.GetDataTable(qry)
        If Dt.Rows.Count > 0 Then
            Throw New Exception("Some sample have not been posted. please post those sample First.")
            Return Nothing
        End If

        qry = "SELECT distinct sh.DOC_CODE,TSPL_MILK_SAMPLE_HEAD.MCC_CODE,TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE,sh.DOC_DATE,TSPL_MILK_SAMPLE_HEAD.SHIFT," _
        & " sh.route_code,sh.superviser_name,sh.vehicle_no,sh.Mcc_Arival_time,sh.Unloading_time,TSPL_MCC_MASTER.MCC_NAME,  sh.Created_By,sh.Posted,sh.POSTED,sh.POSTING_DATE,Route_Name FROM " _
        & " TSPL_MILK_SAMPLE_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_SAMPLE_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE Left join tspl_milk_truck_sheet_head sh " _
        & " on sh.mcc_code=TSPL_MILK_SAMPLE_HEAD.MCC_CODE and convert(date,sh.doc_date,103)=convert(date,TSPL_MILK_SAMPLE_HEAD.doc_date,103) and  sh.shift=" _
        & " TSPL_MILK_SAMPLE_HEAD.shift left join TSPL_MCC_ROUTE_MASTER rm on rm.Route_Code=sh.route_code where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD where  TSPL_MILK_SAMPLE_HEAD.Milk_receipt_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_SAMPLE_HEAD where TSPL_MILK_SAMPLE_HEAD.Milk_receipt_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND TSPL_MILK_SAMPLE_HEAD.Milk_receipt_Code = '" + strCode + "'"
        End Select
        Dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.DOC_CODE = clsCommon.myCstr(dt.Rows(0)("DOC_CODE"))
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.MILK_RECEIPT_CODE = clsCommon.myCstr(dt.Rows(0)("MILK_RECEIPT_CODE"))
            'If clsCommon.myCstr(dt.Rows(0)("DOC_DATE")) <> "" Then
            '    obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy hh:mm:ss tt")
            'Else
            '    obj.DOC_DATE = Nothing
            'End If
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.UnLoading_Time = clsCommon.myCstr(dt.Rows(0)("UnLoading_Time"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.Superviser_Name = clsCommon.myCstr(dt.Rows(0)("Superviser_Name"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
            obj.Mcc_Arival_Time = clsCommon.myCstr(dt.Rows(0)("Mcc_Arival_Time"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If

            If clsCommon.myLen(strRoute_Code) > 0 Then
                qry = "SELECT vmh.vlc_code,sh.shift,vmh.vlc_name, '' as Doc_Code,null as Doc_Date,sh.Milk_receipt_code,sd.DOC_CODE,sd.VLC_DOC_CODE,sd.SAMPLE_NO,VSP_CODE,Item_Code,QTY,  TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Correction_Factor,CLR," _
                    & " RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,Route_Code,0 as mcc_fat,0 as Mcc_Snf,0 as Mcc_Qty,cans,'' as remarks FROM TSPL_MILK_SAMPLE_DETAIL " _
                    & " sd inner join TSPL_MILK_SAMPLE_HEAD sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code,SAMPLE_NO_VALUES,VLC_DOC_CODE " _
                    & " as vlc_d,eco_pro_name,No_Of_Cans as cans,Route_Code,vlc_code,coalesce(is_truck_sheet_uploaded,'F') as is_truck_sheet_uploaded from  TSPL_MILK_RECEIPT_DETAIL   ) tt on Receipt_code=sh.MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and is_truck_sheet_uploaded='F' " _
                    & "  left join (select vlc_code,vlc_Name from TSPL_VLC_MASTER_HEAD) vmh on vmh.vlc_code=tt.vlc_code WHERE 2=2" _
                    & " AND sh.Milk_receipt_code = '" + strCode + "' and route_Code='" & strRoute_Code & "' ORDER BY sd.VLC_DOC_CODE"
            Else
                qry = "SELECT vmh.vlc_code,sh.shift,vmh.vlc_name,tsd.Doc_Code,tsd.Doc_Date,sh.Milk_receipt_code,sd.DOC_CODE,sd.VLC_DOC_CODE,sd.SAMPLE_NO,VSP_CODE,Item_Code,QTY,  TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Correction_Factor,CLR," _
                    & " RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,Route_Code,mcc_fat,Mcc_Snf,Mcc_Qty,no_of_cans,remarks FROM TSPL_MILK_SAMPLE_DETAIL " _
                    & " sd inner join TSPL_MILK_SAMPLE_HEAD sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code,SAMPLE_NO_VALUES,VLC_DOC_CODE " _
                    & " as vlc_d,eco_pro_name,No_Of_Cans as cans,Route_Code,vlc_code,coalesce(is_truck_sheet_uploaded,'F') as is_truck_sheet_uploaded from  TSPL_MILK_RECEIPT_DETAIL   ) tt on Receipt_code=sh.MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and is_truck_sheet_uploaded='T' " _
                    & " left join tspl_milk_truck_sheet_detail tsd on tsd.mcc_code=sh.MCC_CODE and convert(date,sh.doc_date,103)=convert(date,tsd.doc_date,103) and " _
                    & " sh.shift=tsd.shift  and tsd.VLC_DOC_CODE=vlc_d  left join (select vlc_code,vlc_Name from TSPL_VLC_MASTER_HEAD) vmh on vmh.vlc_code=tt.vlc_code WHERE 2=2" _
                    & " AND tsd.doc_code = '" + strCode + "' and route_code is not null ORDER BY sd.VLC_DOC_CODE"
            End If
            

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMilk_Truck_SheetDetail

                    objtr.DOC_CODE = clsCommon.myCstr(dr("DOC_CODE"))
                    objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))

                    objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                    objtr.Cans = clsCommon.myCdbl(dr("cans"))
                    objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                    objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                    objtr.MCC_FAT = clsCommon.myCdbl(dr("MCC_FAT"))
                    objtr.MCC_Qty = clsCommon.myCdbl(dr("MCC_Qty"))
                    objtr.MCC_SNF = clsCommon.myCdbl(dr("MCC_SNF"))
                    objtr.Vlc_Code = clsCommon.myCstr(dr("VLc_CODE"))
                    objtr.Vlc_name = clsCommon.myCstr(dr("VLc_name"))
                    objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objtr.MILK_Qty = clsCommon.myCdbl(dr("QTY"))
                    objtr.Mcc_Code = clsCommon.myCstr(obj.MCC_CODE)
                    objtr.Doc_Date = clsCommon.myCstr(dr("Doc_Date"))
                    objtr.Shift = clsCommon.myCdbl(dr("Shift"))

                    objtr.Milk_Receipt_Code = clsCommon.myCdbl(dr("Milk_Receipt_Code"))


                    ObjList.Add(objtr)
                Next
            End If

            clsMilk_Truck_Sheet.ObjList = ObjList
        End If
        Return obj
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilk_Truck_Sheet

        Dim obj As New clsMilk_Truck_Sheet()
        Dim objtr As New clsMilk_Truck_SheetDetail

        ObjList = New List(Of clsMilk_Truck_SheetDetail)

        Dim qry As String = "SELECT distinct sh.DOC_CODE,TSPL_MILK_SAMPLE_HEAD.MCC_CODE,TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE,sh.DOC_DATE,TSPL_MILK_SAMPLE_HEAD.SHIFT," _
        & " sh.route_code,sh.superviser_name,sh.vehicle_No,sh.Mcc_Arival_time,sh.Unloading_time,TSPL_MCC_MASTER.MCC_NAME,  sh.Created_By,sh.Posted,sh.POSTED,sh.POSTING_DATE,Route_Name FROM " _
        & " TSPL_MILK_SAMPLE_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_SAMPLE_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE Inner join tspl_milk_truck_sheet_head sh " _
        & " on sh.mcc_code=TSPL_MILK_SAMPLE_HEAD.MCC_CODE and convert(date,sh.doc_date,103)=convert(date,TSPL_MILK_SAMPLE_HEAD.doc_date,103) and  sh.shift=" _
        & " TSPL_MILK_SAMPLE_HEAD.shift left join TSPL_MCC_ROUTE_MASTER rm on rm.Route_Code=sh.route_code where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " AND sh.DOC_CODE = (select MIN(DOC_CODE) from tspl_milk_truck_sheet_head)"
            Case NavigatorType.Last
                qry += " AND sh.DOC_CODE = (select Max(DOC_CODE) from tspl_milk_truck_sheet_head)"
            Case NavigatorType.Next
                qry += " AND sh.DOC_CODE = (select Min(DOC_CODE) from tspl_milk_truck_sheet_head where  tspl_milk_truck_sheet_head.Doc_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND sh.DOC_CODE = (select Max(DOC_CODE) from tspl_milk_truck_sheet_head where tspl_milk_truck_sheet_head.Doc_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND sh.Doc_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.DOC_CODE = clsCommon.myCstr(dt.Rows(0)("DOC_CODE"))
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.MILK_RECEIPT_CODE = clsCommon.myCstr(dt.Rows(0)("MILK_RECEIPT_CODE"))
            If clsCommon.myCstr(dt.Rows(0)("DOC_DATE")) <> "" Then
                obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy hh:mm:ss tt")
            Else
                obj.DOC_DATE = Nothing
            End If
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.UnLoading_Time = clsCommon.myCstr(dt.Rows(0)("UnLoading_Time"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.Superviser_Name = clsCommon.myCstr(dt.Rows(0)("Superviser_Name"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
            obj.Mcc_Arival_Time = clsCommon.myCstr(dt.Rows(0)("Mcc_Arival_Time"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If clsCommon.myLen(dt.Rows(0)("DOC_CODE")) Then
                strCode = clsCommon.myCstr(dt.Rows(0)("DOC_CODE"))
            End If

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If

            
            qry = "SELECT vmh.vlc_code,sh.shift,vmh.vlc_name,tsd.Doc_Code,tsd.Doc_Date,sh.Milk_receipt_code,sd.DOC_CODE,sd.VLC_DOC_CODE,sd.SAMPLE_NO,VSP_CODE,Item_Code,QTY,  TYPE,MILK_TYPE,FAT,MILK_SRN_CODE,SNF,Correction_Factor,CLR," _
                & " RATE,AMOUNT,IS_MANUAL,SAMPLE_NO_VALUES,Eco_pro_Name,cans,Route_Code,mcc_fat,Mcc_Snf,Mcc_Qty,no_of_cans,remarks FROM TSPL_MILK_SAMPLE_DETAIL " _
                & " sd inner join TSPL_MILK_SAMPLE_HEAD sh on sh.DOC_CODE=sd.DOC_CODE inner join (select DOC_CODE as Receipt_code,SAMPLE_NO_VALUES,VLC_DOC_CODE " _
                & " as vlc_d,eco_pro_name,No_Of_Cans as cans,Route_Code,vlc_code,coalesce(is_truck_sheet_uploaded,'F') as is_truck_sheet_uploaded from  TSPL_MILK_RECEIPT_DETAIL   ) tt on Receipt_code=sh.MILK_RECEIPT_CODE and VLC_DOC_CODE=vlc_d and is_truck_sheet_uploaded='T' " _
                & " left join tspl_milk_truck_sheet_detail tsd on tsd.mcc_code=sh.MCC_CODE and convert(date,sh.doc_date,103)=convert(date,tsd.doc_date,103) and " _
                & " sh.shift=tsd.shift  and tsd.VLC_DOC_CODE=vlc_d  left join (select vlc_code,vlc_Name from TSPL_VLC_MASTER_HEAD) vmh on vmh.vlc_code=tt.vlc_code WHERE 2=2" _
                & " AND tsd.doc_code = '" + strCode + "' and route_code is not null ORDER BY sd.VLC_DOC_CODE"



            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMilk_Truck_SheetDetail

                    objtr.DOC_CODE = clsCommon.myCstr(dr("DOC_CODE"))
                    objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))

                    objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                    objtr.Cans = clsCommon.myCdbl(dr("cans"))
                    objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                    objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                    objtr.MCC_FAT = clsCommon.myCdbl(dr("MCC_FAT"))
                    objtr.MCC_Qty = clsCommon.myCdbl(dr("MCC_Qty"))
                    objtr.MCC_SNF = clsCommon.myCdbl(dr("MCC_SNF"))
                    objtr.Vlc_Code = clsCommon.myCstr(dr("VLc_CODE"))
                    objtr.Vlc_name = clsCommon.myCstr(dr("VLc_name"))
                    objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objtr.MILK_Qty = clsCommon.myCdbl(dr("QTY"))
                    objtr.Mcc_Code = clsCommon.myCstr(obj.MCC_CODE)
                    objtr.Doc_Date = clsCommon.myCstr(dr("Doc_Date"))
                    objtr.Shift = clsCommon.myCdbl(dr("Shift"))

                    objtr.Milk_Receipt_Code = clsCommon.myCdbl(dr("Milk_Receipt_Code"))


                    ObjList.Add(objtr)
                Next
            End If

            clsMilk_Truck_Sheet.ObjList = ObjList
        End If
        Return obj
    End Function
    ''richa agarwal  handle transaction on class not on form
    Public Shared Function SaveData(ByVal obj As clsMilk_Truck_Sheet, ByVal objList As List(Of clsMilk_Truck_SheetDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilk_Truck_Sheet, ByVal objList As List(Of clsMilk_Truck_SheetDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Milk Truck Sheet", obj.MCC_CODE, obj.DOC_DATE, trans)
            Dim isNewEntry As Boolean

            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MILKTruckSheet, "", obj.MCC_CODE)
            Else
                isNewEntry = False
                Dim Strqrys As String = "SELECT Count(*) FROM Tspl_Milk_Truck_Sheet_Head where DOC_CODE = '" & obj.DOC_CODE & "' and posted=1"
                Dim checks As Integer = clsDBFuncationality.getSingleValue(Strqrys, trans)
                If checks = 0 Then
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                    Exit Function
                End If    'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
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

            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
            clsCommon.AddColumnsForChange(coll, "Superviser_Name", obj.Superviser_Name)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            If clsCommon.myLen(obj.Mcc_Arival_Time) > 0 Then
                clsCommon.AddColumnsForChange(coll, "MCC_Arival_time", clsCommon.GetPrintDate(obj.Mcc_Arival_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            End If
            If clsCommon.myLen(obj.UnLoading_Time) > 0 Then
                clsCommon.AddColumnsForChange(coll, "UnLoading_Time", clsCommon.GetPrintDate(obj.UnLoading_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            End If


            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM Tspl_Milk_Truck_Sheet_Head where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Milk_Truck_Sheet_Head", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                    Exit Function
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Milk_Truck_Sheet_Head", OMInsertOrUpdate.Update, "Tspl_Milk_Truck_Sheet_Head.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMilk_Truck_SheetDetail.SaveData(obj.DOC_CODE, objList, trans, isNewEntry, obj.MILK_RECEIPT_CODE)

            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            '  trans.Rollback()
            Throw New Exception(err.Message)
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
            Dim obj As clsMilk_Truck_Sheet = clsMilk_Truck_Sheet.GetData(strDocNo, NavigatorType.Current, Nothing)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MILK_Truck_Sheet_Head set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
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


Public Class clsMilk_Truck_SheetDetail
#Region "Variables"
    Public DOC_CODE As String
    Public VLC_DOC_CODE As String
    Public Vlc_Code As String
    Public Cans As Decimal = 0
    Public SAMPLE_NO As Integer
    Public MILK_Qty As Decimal
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public MCC_FAT As Decimal = 0
    Public MCC_Qty As Decimal = 0
    Public MCC_SNF As Decimal = 0
    Public Remarks As String

    Public Mcc_Code As String
    Public Doc_Date As String
    ' Public MILK_RECEIPT_CODE As String

    Public Shift As String
    Public Milk_Receipt_Code As String
    Public Vlc_name As String
   

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilk_Truck_SheetDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Receipt_Code As String) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilk_Truck_SheetDetail In Arr
                Dim coll As New Hashtable()


                clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.Vlc_Code)

                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
                clsCommon.AddColumnsForChange(coll, "NO_OF_CANS", obj.Cans)

                clsCommon.AddColumnsForChange(coll, "Vlc_Qty", obj.MILK_Qty)
                clsCommon.AddColumnsForChange(coll, "Vlc_FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "Vlc_SNF", obj.SNF)

                clsCommon.AddColumnsForChange(coll, "Mcc_Qty", obj.MCC_Qty)
                clsCommon.AddColumnsForChange(coll, "Mcc_FAT", obj.MCC_FAT)
                clsCommon.AddColumnsForChange(coll, "Mcc_SNF", obj.MCC_SNF)

                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.Mcc_Code)
                clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.Doc_Date, "dd-MMM-yyyy"))
                clsCommon.AddColumnsForChange(coll, "SHIFT", obj.Shift)

                clsCommon.AddColumnsForChange(coll, "Milk_Receipt_Code", obj.Milk_Receipt_Code)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
               

                Dim Strqry As String = "SELECT Count(*) FROM Tspl_Milk_Truck_Sheet_detail where DOC_CODE = '" & obj.DOC_CODE & "' and Tspl_Milk_Truck_Sheet_detail.VLC_DOC_CODE='" & obj.VLC_DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Milk_Truck_Sheet_detail", OMInsertOrUpdate.Insert, "Tspl_Milk_Truck_Sheet_detail.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Milk_Truck_Sheet_detail", OMInsertOrUpdate.Update, "Tspl_Milk_Truck_Sheet_detail.DOC_CODE='" + strDocNo + "' and Tspl_Milk_Truck_Sheet_detail.VLC_DOC_CODE='" & obj.VLC_DOC_CODE & "'", trans)
                End If
                Dim Squery As String = "update tspl_Milk_receipt_Detail set is_Truck_Sheet_Uploaded ='T' where Doc_Code='" & Receipt_Code & "' and vlc_doc_Code='" & obj.VLC_DOC_CODE & "'"
                clsDBFuncationality.ExecuteNonQuery(Squery, trans)
            Next

        End If

        Return True
    End Function

End Class



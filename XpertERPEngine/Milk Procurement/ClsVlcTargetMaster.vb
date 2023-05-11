Imports Common
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsVlcTargetMaster

#Region "Variables"

    Public DOC_CODE As String
    Public MCC_CODE As String

    Public DOC_DATE As DateTime
    Public From_DATE As DateTime
    Public To_DATE As DateTime



    Public Route_Code As String
    Public Route_Name As String
    Public VLC_Code As String
    Public VLC_Uplader_Code As String
    Public VLC_Name As String
    Public VSP_Code As String
    Public VSP_Name As String
    Public MP_Code As String
    Public MP_Name As String
    Public Day_Target As String
    Public Morning_target As String
    Public Evening_target As String
    Public Remarks As String

    Public MCC_NAME As String
    Public Is_Saved_Vlc As String

    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of ClsVlcTargetMaster)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal strRoute_Code As String, ByVal NavType As NavigatorType) As List(Of ClsVlcTargetMaster)
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


            qry = "delete from TSPL_Vlc_Target_Detail where DOCument_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)



            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As DataRow
        Dim dr As DataRow = Nothing
        Try
            Dim str As String = ""
            Dim qry As String = "select Distinct TSPL_Vlc_Target_Detail.document_code as [Code] ,TSPL_Vlc_Target_Detail.MCC_CODE as [Mcc Code] ,TSPL_Vlc_Target_Detail.DOCument_DATE as [Doc Date],TSPL_Vlc_Target_Detail.Route_Code as [Route Code] ,TSPL_Vlc_Target_Detail.Posted as [Posted] ,TSPL_Vlc_Target_Detail.Posting_Date as [Posting Date]  From TSPL_Vlc_Target_Detail "
            dr = clsCommon.ShowSelectFormForRow("VLC_TR_MST-R", qry) ', "Code", whrcls, curcode, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dr
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal strRoute_Code As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, Optional strVLC_Code As String = "") As List(Of ClsVlcTargetMaster)

        Dim obj As New ClsVlcTargetMaster()
        Dim objtr As New ClsVlcTargetMaster

        ObjList = New List(Of ClsVlcTargetMaster)

        Dim qry As String = ""
        Dim Dt As DataTable

        If clsCommon.myLen(clsCommon.myCstr(strCode)) > 0 Then
            qry = "select  TSPL_Vlc_Target_Detail.Mp_code,TSPL_MP_MASTER.MP_Name ,TSPL_Vlc_Target_Detail.DOCUMENT_cODE AS doc_code,TSPL_Vlc_Target_Detail.DOCUMENT_DATE AS doc_date,TSPL_Vlc_Target_Detail.frm_date,TSPL_Vlc_Target_Detail.To_date,TSPL_VLC_MASTER_HEAD.vlc_code,vlc_code_vlc_uploader,vlc_name,day_target,Morning_Target,Evening_target," _
                       & " TSPL_MCC_MASTER.Mcc_code,Mcc_name,TSPL_Vlc_Target_Detail.Remarks,TSPL_MCC_ROUTE_MASTER.Route_Code,Route_name,TSPL_VLC_MASTER_HEAD.vsp_code,vendor_name ,Posted,TSPL_Vlc_Target_Detail.created_by,TSPL_Vlc_Target_Detail.posting_date from " _
                       & " TSPL_Vlc_Target_Detail rIGHT   join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_Vlc_Target_Detail.vlc_code LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  left join TSPL_Mcc_master " _
                       & " on TSPL_Mcc_master.Mcc_code=TSPL_MCC_ROUTE_MASTER.MCC_code left join TSPL_Vendor_master  on TSPL_Vendor_master.Vendor_code=TSPL_VLC_MASTER_HEAD.VSP_code" _
                       & "  left join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code =TSPL_Vlc_Target_Detail.MP_Code " _
                       & " where 2=2 "

            Select Case NavType
                Case NavigatorType.First
                    qry += " AND DOCUMENT_CODE = (select MIN(DOCUMENT_CODE) from TSPL_Vlc_Target_Detail) order by  coalesce(TSPL_Vlc_Target_Detail.DOCUMENT_cODE,'') desc"
                Case NavigatorType.Last
                    qry += " AND DOCUMENT_CODE = (select Max(DOCUMENT_CODE) from TSPL_Vlc_Target_Detail)"
                Case NavigatorType.Next
                    qry += " AND DOCUMENT_CODE = (select Min(DOCUMENT_CODE) from TSPL_Vlc_Target_Detail where  TSPL_Vlc_Target_Detail.DOCUMENT_CODE>'" + strCode + "') order by  coalesce(TSPL_Vlc_Target_Detail.DOCUMENT_cODE,'') desc"
                Case NavigatorType.Previous
                    qry += " AND DOCUMENT_CODE = (select Max(DOCUMENT_CODE) from TSPL_Vlc_Target_Detail where TSPL_Vlc_Target_Detail.DOCUMENT_CODE<'" + strCode + "') order by  coalesce(TSPL_Vlc_Target_Detail.DOCUMENT_cODE,'') desc"
                Case NavigatorType.Current
                    qry += " AND TSPL_Vlc_Target_Detail.DOCUMENT_CODE = '" + strCode + "' and (TSPL_VLC_MASTER_HEAD.rOUTE_CODE = '" + strRoute_Code + "' " & IIf(strVLC_Code = "", "", " and TSPL_VLC_MASTER_HEAD.vlc_CODE='" & strVLC_Code & "'") & ") order by  coalesce(TSPL_Vlc_Target_Detail.DOCUMENT_cODE,'') desc"
            End Select
        Else
            If clsCommon.myLen(strVLC_Code) > 0 Then
                qry = "select '' as doc_code,null as doc_date,null as frm_date,null as To_Date,TSPL_VLC_MASTER_HEAD.vlc_code,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,vlc_name,0 as day_target,0 as Morning_Target,0 as Evening_target," _
                  & " TSPL_MP_MASTER.MP_Code ,TSPL_MP_MASTER.MP_Name ,TSPL_Mcc_master.Mcc_code,Mcc_name,'' as Remarks,tspl_mcc_route_master.Route_Code,Route_name,TSPL_Vendor_master.vendor_code as Vsp_code,vendor_name ,0 as Posted,null as CREATED_BY,null as Posting_date from " _
                  & "  TSPL_VLC_MASTER_HEAD  inner join tspl_mcc_route_master on tspl_mcc_route_master.route_code=TSPL_VLC_MASTER_HEAD.route_code inner join TSPL_Mcc_master " _
                  & " on TSPL_Mcc_master.Mcc_code=TSPL_VLC_MASTER_HEAD.MCC inner join TSPL_Vendor_master  on TSPL_Vendor_master.Vendor_code=TSPL_VLC_MASTER_HEAD.VSP_code" _
                  & "    left join TSPL_MP_MASTER on  TSPL_MP_MASTER.VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_Code " _
                  & " where 2=2 "

                Select Case NavType
                    Case NavigatorType.Current
                        qry += " AND (tspl_mcc_route_master.route_code= '" + strRoute_Code + "' " & IIf(strVLC_Code = "", "", "and TSPL_VLC_MASTER_HEAD.vlc_CODE='" & strVLC_Code & "'") & ") "
                End Select
            Else

                qry = "select '' as doc_code,null as doc_date,null as frm_date,null as To_Date,TSPL_VLC_MASTER_HEAD.vlc_code,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,vlc_name,0 as day_target,0 as Morning_Target,0 as Evening_target," _
                 & " '' as MP_Code ,'' as MP_Name ,TSPL_Mcc_master.Mcc_code,Mcc_name,'' as Remarks,tspl_mcc_route_master.Route_Code,Route_name,TSPL_Vendor_master.vendor_code as Vsp_code,vendor_name ,0 as Posted,null as CREATED_BY,null as Posting_date from " _
                 & "  TSPL_VLC_MASTER_HEAD  inner join tspl_mcc_route_master on tspl_mcc_route_master.route_code=TSPL_VLC_MASTER_HEAD.route_code inner join TSPL_Mcc_master " _
                 & " on TSPL_Mcc_master.Mcc_code=TSPL_VLC_MASTER_HEAD.MCC inner join TSPL_Vendor_master  on TSPL_Vendor_master.Vendor_code=TSPL_VLC_MASTER_HEAD.VSP_code" _
                 & " where 2=2 "

                Select Case NavType
                    Case NavigatorType.Current
                        qry += " AND (tspl_mcc_route_master.route_code= '" + strRoute_Code + "' ) "
                End Select

            End If



        End If
        Dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (Dt IsNot Nothing AndAlso Dt.Rows.Count > 0) Then
            For Each row As DataRow In Dt.Rows
                obj = New ClsVlcTargetMaster()
                obj.DOC_CODE = clsCommon.myCstr(row.Item("DOC_CODE"))
                If clsCommon.myLen(clsCommon.myCstr(row.Item("frm_Date"))) > 0 Then
                    obj.DOC_DATE = clsCommon.myCDate(row.Item("DOC_Date"))
                    obj.From_DATE = clsCommon.myCDate(row.Item("frm_Date"))
                    obj.To_DATE = clsCommon.myCDate(row.Item("To_Date"))
                End If
                obj.MCC_CODE = clsCommon.myCstr(row.Item("MCC_CODE"))
                obj.Day_Target = clsCommon.myCdbl(row.Item("Day_Target"))
                obj.Morning_target = clsCommon.myCdbl(row.Item("Morning_Target"))
                obj.Evening_target = clsCommon.myCdbl(row.Item("Evening_Target"))
                'If clsCommon.myCstr(row.item("DOC_DATE")) <> "" Then
                '    obj.DOC_DATE = clsCommon.GetPrintDate(row.item("DOC_DATE"), "dd/MMM/yyyy hh:mm:ss tt")
                'Else
                '    obj.DOC_DATE = Nothing
                'End If
                obj.MCC_NAME = clsCommon.myCstr(row.Item("MCC_NAME"))
                obj.Route_Code = clsCommon.myCstr(row.Item("Route_Code"))
                obj.Route_Name = clsCommon.myCstr(row.Item("Route_Name"))
                obj.VLC_Code = clsCommon.myCstr(row.Item("VLC_Code"))
                obj.VLC_Uplader_Code = clsCommon.myCstr(row.Item("VLC_Code_vlc_Uploader"))
                obj.VLC_Name = clsCommon.myCstr(row.Item("VLC_Name"))
                obj.VSP_Code = clsCommon.myCstr(row.Item("VSP_Code"))
                obj.VSP_Name = clsCommon.myCstr(row.Item("Vendor_Name"))
                obj.Remarks = clsCommon.myCstr(row.Item("Remarks"))
                obj.MP_Code = clsCommon.myCstr(row.Item("MP_Code"))
                obj.MP_Name = clsCommon.myCstr(row.Item("MP_Name"))
                obj.CREATED_BY = clsCommon.myCstr(row.Item("CREATED_BY"))
                obj.POSTED = IIf(clsCommon.myCdbl(row.Item("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)


                If clsCommon.myLen(row.Item("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCDate(row.Item("Posting_Date"))
                Else
                    obj.Posting_Date = Nothing
                End If
                ObjList.Add(obj)
            Next

        End If
        Return ObjList
    End Function



    Public Shared Function SaveData(ByVal objList As List(Of ClsVlcTargetMaster), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            Dim txtcode As String = String.Empty
            For Each obj As ClsVlcTargetMaster In objList
                If clsCommon.myLen(txtcode) > 0 Then
                    obj.DOC_CODE = txtcode
                End If
                If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                    isNewEntry = True
                    txtcode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.VLC_Target, "", obj.MCC_CODE)
                    obj.DOC_CODE = txtcode
                Else
                    isNewEntry = False
                    Dim Strqrys As String = "SELECT Count(*) FROM TSPL_Vlc_Target_Detail where Document_Code = '" & obj.DOC_CODE & "' and posted=1"
                    Dim checks As Integer = clsDBFuncationality.getSingleValue(Strqrys, trans)
                    If checks = 0 Then
                    Else
                        Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                    End If    'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
                End If


                If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.DOC_CODE)
                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                clsCommon.AddColumnsForChange(coll, "Day_Target", obj.Day_Target)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "Frm_Date", clsCommon.GetPrintDate(obj.From_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "Morning_Target", obj.Morning_target)
                clsCommon.AddColumnsForChange(coll, "Evening_target", obj.Evening_target)
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                clsCommon.AddColumnsForChange(coll, "VSP_Code", obj.VSP_Code)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "MP_Code", obj.MP_Code, True)


                clsCommon.AddColumnsForChange(coll, "POSTED", "0")
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                'Dim Strqry As String = "SELECT Count(*) FROM TSPL_Vlc_Target_Detail where Document_Code = '" & obj.DOC_CODE & "' and vlc_code='" & obj.VLC_Code & "' and MP_Code='" & obj.MP_Code & "'"
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_Vlc_Target_Detail where Document_Code = '" & obj.DOC_CODE & "' and vlc_code='" & obj.VLC_Code & "' " & IIf(obj.MP_Code = "", "", " and MP_Code='" & obj.MP_Code & "'") & ""
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vlc_Target_Detail", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vlc_Target_Detail", OMInsertOrUpdate.Update, "TSPL_Vlc_Target_Detail.Document_Code='" + obj.DOC_CODE + "' and vlc_code='" & obj.VLC_Code & "'  " & IIf(obj.MP_Code = "", "", " and MP_Code='" & obj.MP_Code & "'") & " ", trans)
                End If
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal strRouteNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As List(Of ClsVlcTargetMaster) = ClsVlcTargetMaster.GetData(strDocNo, strRouteNo, NavigatorType.Current, Nothing)
            For Each objl As ClsVlcTargetMaster In obj
                If (obj Is Nothing OrElse clsCommon.myLen(objl.DOC_CODE) <= 0) Then
                    Throw New Exception("No Data found to Post")
                End If
                If (isCheckForPosted AndAlso objl.POSTED = 1) Then
                    Throw New Exception("Already Post on :" + objl.Posting_Date)
                End If

                Dim qry As String = "Update TSPL_Vlc_Target_Detail set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOCument_CODE ='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                Exit For
            Next
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



Imports System.Data.SqlClient
Imports common

Public Class clsHeadLoadMaster

#Region "Variables"

    Public Document_No As String = Nothing
    Public Start_Date As Date? = Nothing
    Public Document_date As Date? = Nothing
    Public Description As String = Nothing
    Public Status As Integer = 0
    Public Arr As List(Of clsHeadLoadDCS) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsHeadLoadMaster, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub SaveAutoData()
        Try
            Dim obj As New clsHeadLoadMaster()
            obj.Document_No = "Default"
            obj.Description = "Auto generated"
            obj.Document_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            obj.Start_Date = clsCommon.GetPrintDate("2023-10-01", "dd/MMM/yyyy")
            objCommonVar.CurrentUserCode = "ADMIN"
            obj.Arr = New List(Of clsHeadLoadDCS)

            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VENDOR_MASTER.Service_Basis_Head_Load , TSPL_VENDOR_MASTER.Rate_Head_Load  from TSPL_VLC_MASTER_HEAD
            left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_VLC_MASTER_HEAD.isOwnBMC = 0"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objTr As New clsHeadLoadDCS()
                    objTr.VLC_CODE = clsCommon.myCstr(dr("VLC_Code"))
                    objTr.Head_Load_Basis = clsCommon.myCstr(dr("Service_Basis_Head_Load"))
                    objTr.Head_Load_Rate = clsCommon.myCDecimal(dr("Rate_Head_Load"))
                    obj.Arr.Add(objTr)
                Next
            End If
            Dim AutoSave As Boolean = True
            If (obj.SaveData(obj, True, Nothing, AutoSave)) Then
                obj.PostData(clsUserMgtCode.frmHeadLoadMaster, obj.Document_No)
                objCommonVar.CurrentUserCode = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function SaveData(ByVal obj As clsHeadLoadMaster, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_HEAD_LOAD_DCS where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                'Dim isRecordExist As Integer = clsDBFuncationality.getSingleValue("select count(1) from TSPL_HEAD_LOAD", trans)
                If clsCommon.CompairString(AutoSave, False) = CompairStringResult.Equal Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.HeadLoadDCS, "", "")
                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HEAD_LOAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HEAD_LOAD", OMInsertOrUpdate.Update, "TSPL_HEAD_LOAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsHeadLoadDCS.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsHeadLoadMaster
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsHeadLoadMaster
        Dim obj As clsHeadLoadMaster = Nothing
        Dim qry As String = "select Document_No ,Description, Document_date,Start_Date ,ISNULL( Status,0) as Status from TSPL_HEAD_LOAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HEAD_LOAD.Document_No = (select MIN(Document_No) from TSPL_HEAD_LOAD)"
            Case NavigatorType.Last
                qry += " and TSPL_HEAD_LOAD.Document_No = (select Max(Document_No) from TSPL_HEAD_LOAD)"
            Case NavigatorType.Next
                qry += " and TSPL_HEAD_LOAD.Document_No = (select Min(Document_No) from TSPL_HEAD_LOAD where Document_No >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HEAD_LOAD.Document_No = (select Max(Document_No) from TSPL_HEAD_LOAD where Document_No <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HEAD_LOAD.Document_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsHeadLoadMaster()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,Description,convert(varchar(12),Start_Date,103) AS [Start Date],convert(varchar(12),Document_date,103) as DocumentDate,case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_HEAD_LOAD"
        str = clsCommon.ShowSelectForm("HeadLoad", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function


    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Docume nt No not found to Post")
            End If
            Dim obj As clsHeadLoadMaster = clsHeadLoadMaster.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_HEAD_LOAD set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsHeadLoadMaster = clsHeadLoadMaster.GetData(strCode, NavigatorType.Current, Nothing, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HEAD_LOAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsHeadLoadMaster = clsHeadLoadMaster.GetData(strCode, NavigatorType.Current, "", trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_HEAD_LOAD_DCS where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_HEAD_LOAD where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsHeadLoadDCS

#Region "Variables"
    Public PK_Id As Integer
    Public Document_No As String = Nothing
    Public Dcs_Uploader_No As String = Nothing
    Public VLC_CODE As String = Nothing
    Public DCS_Name As String = Nothing
    Public BMC_Uploader_No As String = Nothing
    Public BMC_Code As String = Nothing
    Public BMC_Name As String = Nothing
    Public Head_Load_Basis As String = Nothing
    Public Head_Load_Rate As Decimal


#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsHeadLoadDCS), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsHeadLoadDCS In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
                clsCommon.AddColumnsForChange(coll, "Head_Load_Basis", obj.Head_Load_Basis)
                clsCommon.AddColumnsForChange(coll, "Head_Load_Rate", obj.Head_Load_Rate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HEAD_LOAD_DCS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsHeadLoadDCS)
        Dim arr As List(Of clsHeadLoadDCS) = Nothing
        Dim qry As String = "select TSPL_HEAD_LOAD_DCS.Document_No, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as Dcs_Uploader_No, TSPL_VLC_MASTER_HEAD.VLC_CODE as VLC_CODE, TSPL_VLC_MASTER_HEAD.VLC_Name as DCS_Name ,
        TSPL_MCC_MASTER.MCC_Code_VLC_Uploader as BMC_Uploader_No ,TSPL_MCC_MASTER.MCC_Code as BMC_Code , TSPL_MCC_MASTER.MCC_NAME as BMC_Name , TSPL_HEAD_LOAD_DCS.Head_Load_Basis , 
        TSPL_HEAD_LOAD_DCS.Head_Load_Rate from TSPL_HEAD_LOAD_DCS  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_VLC_MASTER_HEAD.VLC_CODE
         left  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC where  TSPL_HEAD_LOAD_DCS.Document_No = '" + strCode + "' order by Document_No "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsHeadLoadDCS)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsHeadLoadDCS = New clsHeadLoadDCS()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.Dcs_Uploader_No = clsCommon.myCstr(dr("Dcs_Uploader_No"))
                obj.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                obj.DCS_Name = clsCommon.myCstr(dr("DCS_Name"))
                obj.BMC_Uploader_No = clsCommon.myCstr(dr("BMC_Uploader_No"))
                obj.BMC_Code = clsCommon.myCstr(dr("BMC_Code"))
                obj.BMC_Name = clsCommon.myCstr(dr("BMC_Name"))
                obj.Head_Load_Basis = clsCommon.myCstr(dr("Head_Load_Basis"))
                obj.Head_Load_Rate = clsCommon.myCstr(dr("Head_Load_Rate"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

    Public Shared Function GetDcsData(ByVal VLC_CODE As String, ByVal DcsDate As Date, ByVal trans As SqlTransaction) As clsHeadLoadDCS
        Dim obj As New clsHeadLoadDCS()
        Try

            Dim qry As String = "select top 1 TSPL_HEAD_LOAD.Start_Date ,TSPL_HEAD_LOAD_DCS .PK_Id, TSPL_HEAD_LOAD_DCS.Head_Load_Basis ,TSPL_HEAD_LOAD_DCS.Head_Load_Rate,TSPL_HEAD_LOAD_DCS.Cycle_Frequency from TSPL_HEAD_LOAD 
            left outer join TSPL_HEAD_LOAD_DCS on TSPL_HEAD_LOAD_DCS.Document_No = TSPL_HEAD_LOAD.Document_No where  TSPL_HEAD_LOAD.status = 1 and TSPL_HEAD_LOAD_DCS.VLC_CODE  = '" & VLC_CODE & "'  
            and TSPL_HEAD_LOAD.Start_Date <= '" & clsCommon.GetPrintDate(DcsDate, "dd/MMM/yyyy") & "' order by TSPL_HEAD_LOAD.Start_Date desc,PK_Id desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                obj.PK_Id = (dt.Rows(0)("PK_Id"))
                obj.Head_Load_Basis = clsCommon.myCstr(dt.Rows(0)("Head_Load_Basis"))
                obj.Head_Load_Rate = clsCommon.myCstr(dt.Rows(0)("Head_Load_Rate"))
                obj.Cycle_Frequency = clsCommon.myCstr(dt.Rows(0)("Cycle_Frequency"))
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

End Class





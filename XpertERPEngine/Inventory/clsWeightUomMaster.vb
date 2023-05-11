Imports System.Data.SqlClient
Imports common

Public Class clsWeightUomMaster
#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Category As String = Nothing
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public POSTING_DATE As Date? = Nothing
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_WEIGHT_UOM_MASTER.Code,TSPL_WEIGHT_UOM_MASTER.Description, TSPL_WEIGHT_UOM_MASTER.Category from TSPL_WEIGHT_UOM_MASTER   "
        str = clsCommon.ShowSelectForm("WeightUom@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsWeightUomMaster), ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            SaveData(arr, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsWeightUomMaster), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        ' Dim trans As SqlTransaction = Nothing
        Try
            'trans = clsDBFuncationality.GetTransactin()
            For Each obj As clsWeightUomMaster In arr
                Dim coll As New Hashtable()
                clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_WEIGHT_UOM_MASTER", "Code", obj.Code, "POSTED=1", trans)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Category", obj.Category)
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                If clsDBFuncationality.getSingleValue("Select count(*) from  TSPL_WEIGHT_UOM_MASTER where Code='" + obj.Code + "' ", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHT_UOM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHT_UOM_MASTER", OMInsertOrUpdate.Update, "TSPL_WEIGHT_UOM_MASTER.Code='" + obj.Code + "'", trans)
                End If
            Next
            If isSaved Then
                ' trans.Commit()
            End If
        Catch err As Exception
            ' trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsWeightUomMaster
        Dim obj As clsWeightUomMaster = Nothing
        Dim qry As String = "select TSPL_WEIGHT_UOM_MASTER.* from TSPL_WEIGHT_UOM_MASTER   where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_WEIGHT_UOM_MASTER.Code = (select MIN(Code) from TSPL_WEIGHT_UOM_MASTER where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_WEIGHT_UOM_MASTER.Code = (select Max(Code) from TSPL_WEIGHT_UOM_MASTER where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_WEIGHT_UOM_MASTER.Code = (select Min(Code) from TSPL_WEIGHT_UOM_MASTER where Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_WEIGHT_UOM_MASTER.Code = (select Max(Code) from TSPL_WEIGHT_UOM_MASTER where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_WEIGHT_UOM_MASTER.Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsWeightUomMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Category = clsCommon.myCstr(dt.Rows(0)("Category"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("POSTED")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function

   
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        Dim qry As String = "delete from TSPL_WEIGHT_UOM_MASTER where Code='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)

    End Function
    Public Shared Function postData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            postData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim count As Integer = 0
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception(" Incentive No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_WEIGHT_UOM_MASTER", "Code", strDocNo, "POSTED=1", trans)
            Dim obj As clsWeightUomMaster = clsWeightUomMaster.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.POSTED = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.POSTING_DATE, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update TSPL_WEIGHT_UOM_MASTER set POSTED='1', POSTING_DATE='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' , Post_By = '" + objCommonVar.CurrentUserCode + "' where Code='" & strDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            '================ Update in unit master================================
            Dim sql1 As String = "select count(*) from TSPL_UNIT_MASTER where Unit_Code='" + obj.Code + "'"
            count = CInt(connectSql.RunScalar(trans, sql1))
            Dim isLtrType As Char = "N"
            Dim strCategory As Char = "K"
            If clsCommon.CompairString(obj.Category, "LTR") = CompairStringResult.Equal Then
                isLtrType = "Y"
                strCategory = "L"
            End If
            If (count = 0) Then
                strQry = "insert into TSPL_UNIT_MASTER (Unit_Code,Unit_Desc,Conv_Factor,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Weight_Type,Item_Category) values ('" + obj.Code + "','" + obj.Description + "','1','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','Y','" + strCategory + "')"
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Else
                strQry = "UPDATE tspl_unit_master SET Weight_Type='Y', Modify_By = '" + objCommonVar.CurrentUserCode + "',Modify_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' , Item_Category = '" + strCategory + "' WHERE Unit_Code='" + obj.Code + "'"
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            End If
            '=========================================================================
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_WEIGHT_UOM_MASTER where Code='" + strCode + "'"))
    End Function
End Class

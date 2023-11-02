Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsRCDFStanardization
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public Batch_No As String = Nothing
    Public Comment As String = Nothing
    Public Remarks As String = Nothing

    Public Tot_Produce_Qty As Decimal
    Public Tot_Produce_FATKG As Decimal
    Public Tot_Produce_SNFKG As Decimal

    Public Tot_Issue_Qty As Decimal
    Public Tot_Issue_FATKG As Decimal
    Public Tot_Issue_SNFKG As Decimal

    Public Tot_Difference_Qty As Decimal
    Public Tot_Difference_FATKG As Decimal
    Public Tot_Difference_SNFKG As Decimal

    Public Tot_Added_Qty As Decimal
    Public Tot_Added_FATKG As Decimal
    Public Tot_Added_SNFKG As Decimal

    Public Tot_Removed_Qty As Decimal
    Public Tot_Removed_FATKG As Decimal
    Public Tot_Removed_SNFKG As Decimal

    Public Tot_AddRemove_Qty As Decimal
    Public Tot_AddRemove_FATKG As Decimal
    Public Tot_AddRemove_SNFKG As Decimal

    Public Tot_Net_Qty As Decimal
    Public Tot_Net_FATKG As Decimal
    Public Tot_Net_SNFKG As Decimal

    Public ArrProduce As List(Of clsRCDFStanardizationProduce) = Nothing
    Public ArrIssue As List(Of clsRCDFStanardizationIssue) = Nothing
    Public ArrARItem As List(Of clsRCDFStanardizationAddRemove) = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending



#End Region
    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsRCDFStanardization) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(isNewEntry, obj, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsRCDFStanardization, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        If isNewEntry Then
            obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.PPSTANDARDIZATION, "", obj.Location_Code)
        End If
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStandardization, obj.Location_Code, obj.Doc_Date, trans)

        clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
        clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Tot_Produce_Qty", obj.Tot_Produce_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Produce_FATKG", obj.Tot_Produce_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Produce_SNFKG", obj.Tot_Produce_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Issue_Qty", obj.Tot_Issue_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Issue_FATKG", obj.Tot_Issue_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Issue_SNFKG", obj.Tot_Issue_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Difference_Qty", obj.Tot_Difference_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Difference_FATKG", obj.Tot_Difference_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Difference_SNFKG", obj.Tot_Difference_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Added_Qty", obj.Tot_Added_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Added_FATKG", obj.Tot_Added_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Added_SNFKG", obj.Tot_Added_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Removed_Qty", obj.Tot_Removed_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Removed_FATKG", obj.Tot_Removed_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Removed_SNFKG", obj.Tot_Removed_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Tot_AddRemove_Qty", obj.Tot_AddRemove_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_AddRemove_FATKG", obj.Tot_AddRemove_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_AddRemove_SNFKG", obj.Tot_AddRemove_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Net_Qty", obj.Tot_Net_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Net_FATKG", obj.Tot_Net_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Net_SNFKG", obj.Tot_Net_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_STD", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_STD", OMInsertOrUpdate.Update, "TSPL_RCDF_STD.Doc_Code='" + obj.Doc_Code + "'", trans)
        End If
        HistoryUpdate(obj.Doc_Code, trans)
        clsRCDFStanardizationProduce.SaveData(obj.Doc_Code, obj, obj.ArrProduce, trans)
        clsRCDFStanardizationIssue.SaveData(obj.Doc_Code, obj, obj.ArrIssue, trans)
        clsRCDFStanardizationAddRemove.SaveData(obj, trans)

        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strCode, "TSPL_RCDF_STD", "Doc_Code", "TSPL_RCDF_STD_PRODUCE", "Doc_Code", "TSPL_RCDF_STD_ISSUE", "Doc_Code", "TSPL_RCDF_STD_ADD_REMOVE", "Doc_Code", "", "", "", "", "", "", trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRCDFStanardization
        Dim obj As clsRCDFStanardization = Nothing
        Try

            Dim LocCond As String = " where 1=1 "
            If clsCommon.myLen(arrloc) > 0 Then
                LocCond = LocCond & " and location_code in (" + arrloc + ")"
            End If
            Dim qry As String = "select TSPL_RCDF_STD.*,TSPL_LOCATION_MASTER.Location_Desc from TSPL_RCDF_STD 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RCDF_STD.Location_Code"

            qry += " where 2=2 "
            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_RCDF_STD.and Doc_Code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_RCDF_STD.and Doc_Code in (select min(Doc_Code) from TSPL_RCDF_STD " + LocCond + " )"
                Case NavigatorType.Last
                    qry += " and TSPL_RCDF_STD.and Doc_Code in (select max(Doc_Code) from TSPL_RCDF_STD " + LocCond + " )"
                Case NavigatorType.Next
                    qry += " and TSPL_RCDF_STD.and Doc_Code in (select min(Doc_Code) from TSPL_RCDF_STD " + LocCond + " and Doc_Code>'" + strCode + "' )"
                Case NavigatorType.Previous
                    qry += " and TSPL_RCDF_STD.and Doc_Code in (select max(Doc_Code) from TSPL_RCDF_STD " + LocCond + " and Doc_Code<'" + strCode + "' )"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsRCDFStanardization()
                obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))

                obj.Location_Code = clsCommon.myCDate(dt.Rows(0)("Location_Code"))
                obj.Location_Desc = clsCommon.myCDate(dt.Rows(0)("Location_Desc"))
                obj.Batch_No = clsCommon.myCstr(dt.Rows(0)("Batch_No"))
                obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Tot_Produce_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Produce_Qty"))
                obj.Tot_Produce_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Produce_FATKG"))
                obj.Tot_Produce_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Produce_SNFKG"))
                obj.Tot_Issue_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Issue_Qty"))
                obj.Tot_Issue_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Issue_FATKG"))
                obj.Tot_Issue_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Issue_SNFKG"))
                obj.Tot_Difference_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Difference_Qty"))
                obj.Tot_Difference_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Difference_FATKG"))
                obj.Tot_Difference_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Difference_SNFKG"))
                obj.Tot_Added_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Added_Qty"))
                obj.Tot_Added_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Added_FATKG"))
                obj.Tot_Added_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Added_SNFKG"))
                obj.Tot_Removed_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Removed_Qty"))
                obj.Tot_Removed_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Removed_FATKG"))
                obj.Tot_Removed_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Removed_SNFKG"))
                obj.Tot_AddRemove_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_AddRemove_Qty"))
                obj.Tot_AddRemove_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_AddRemove_FATKG"))
                obj.Tot_AddRemove_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_AddRemove_SNFKG"))
                obj.Tot_Net_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Net_Qty"))
                obj.Tot_Net_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Net_FATKG"))
                obj.Tot_Net_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Net_SNFKG"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.ArrProduce = clsRCDFStanardizationProduce.GetData(obj.Doc_Code, trans)
                obj.ArrIssue = clsRCDFStanardizationIssue.GetData(obj.Doc_Code, trans)
                obj.ArrARItem = clsRCDFStanardizationAddRemove.GetData(obj.Doc_Code, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_Date,Location_Code from TSPL_RCDF_STD where Doc_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)
            End If

            HistoryUpdate(strCode, trans)

            Dim qry As String = "delete from TSPL_RCDF_STD_PRODUCE where and Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RCDF_STD_ISSUE where and Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RCDF_STD_ADD_REMOVE where and Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RCDF_STD where and Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_RCDF_STD.Doc_Code as Code,TSPL_RCDF_STD.Doc_Date as [Doc Date],  [Item Description],TSPL_RCDF_STD.Created_By as [Created By],TSPL_RCDF_STD.Created_Date as [Created Date],TSPL_RCDF_STD.Modified_By as [Modified By],TSPL_RCDF_STD.Modified_Date as [Modified Date],case when isnull(TSPL_RCDF_STD.Posted,0)=1 then 'Approved' else 'Pending' end as [Status] " &
        " from TSPL_RCDF_STD 
          left join (select * from (   select ROW_NUMBER()  over(partition by Doc_Code order by Product_Type desc, TSPL_RCDF_STD_PRODUCE.Item_Code) as S_no ,Doc_Code as STD_Main,  TSPL_RCDF_STD_PRODUCE.Item_Code as [Main Item Code],Item_Desc as [Item Description],Product_Type as [Product Type]  from TSPL_RCDF_STD_PRODUCE left join TSPL_ITEM_MASTER on TSPL_RCDF_STD_PRODUCE.Item_Code=TSPL_ITEM_MASTER.Item_Code  ) as M_Inner where S_no=1 ) as Main on TSPL_RCDF_STD.Doc_Code=Main.STD_Main"
        Dim str As String = ""
        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = PostData(Form_Id, strCode, arrLoc, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Return PostData(Form_Id, strCode, arrLoc, trans, "")
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_Date,Location_Code from TSPL_RCDF_STD where Doc_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)

            End If
            HistoryUpdate(strCode, trans)
            Dim qry As String = "update TSPL_RCDF_STD set Posted='1',Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where and Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'PostInventoryMovementANDJE(Form_Id, strCode, arrLoc, trans, VoucherNo)
        Catch ex As Exception
            Throw New Exception("Production Stdardization No [" + strCode + "]" + ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strCode, FormId, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_Date,Location_Code from TSPL_RCDF_STD where Doc_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)

            End If
            Dim qry As String = "select count(*) from TSPL_RCDF_STD where Posted='0' and Doc_Code='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                Throw New Exception("Current document [" + strCode + "] is not posted.")
            End If

            HistoryUpdate(strCode, trans)


            qry = "delete  from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from tspl_journal_master where Source_Doc_No='" & strCode & "' and Source_Code='PR-ER')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete  from tspl_journal_master where Source_Doc_No='" & strCode & "' and Source_Code='PR-ER'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where Doc_Code='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_RCDF_STD set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function PostInventoryMovementANDJE(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String)
    '    Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
    '    If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredFinalQCofstandardization, clsFixedParameterCode.RequiredFinalQCofstandardization, trans)) > 0) Then
    '        Dim objRec As clsRCDFStanardization = clsRCDFStanardization.GetData(strCode, arrLoc, NavigatorType.Current, trans)
    '        For Each objtr As clsRCDFStanardizationAddRemove In objRec.ArrARItem
    '            If Not settAllowNegativeStockInDairyProduction Then
    '                If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
    '                    Dim CheckStockServerDate As Boolean
    '                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
    '                        CheckStockServerDate = True
    '                    Else
    '                        CheckStockServerDate = False
    '                    End If
    '                    Dim loc_type As Integer = 0
    '                    Dim qry As String = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + objtr.Location_Code + "'"
    '                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "MAIN") = CompairStringResult.Equal Then
    '                        loc_type = 2
    '                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "SUB") = CompairStringResult.Equal Then
    '                        loc_type = 1
    '                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "SEC") = CompairStringResult.Equal Then
    '                        loc_type = 0
    '                    End If
    '                    Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objtr.Item_Code, objRec.Location_Code, objtr.Location_Code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), objRec.Doc_Date), trans, objtr.Unit_Code, loc_type, False)
    '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                        If objtr.Qty > clsCommon.myCdbl(dt.Rows(0)("qty")) Then
    '                            If Math.Abs(objtr.Qty - clsCommon.myCdbl(dt.Rows(0)("qty"))) > 0.01 Then
    '                                Throw New Exception("Item [" + objtr.Item_Code + "] Location [" + objtr.Location_Code + "] Added Qty [" + clsCommon.myCstr(objtr.Qty) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("qty"))) + "]")
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next

    '        If Not objRec.Is_Job_Work_Inward Then
    '            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0) Then
    '                clsStandardizationRM.UpdateCost(objRec.Doc_Code, objRec.Doc_Date, objRec.ArrProduce, trans, objRec.Is_Job_Work_Inward)
    '                Dim ArrProdItem As List(Of clsRCDFStanardizationProduce) = clsRCDFStanardizationProduce.GetPPSTDBatchDetail(objRec.Doc_Code, trans)
    '                If ArrProdItem IsNot Nothing AndAlso ArrProdItem.Count > 0 Then
    '                    Dim SettTollFATRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATRateTollerance, clsFixedParameterCode.ProductionFATRateTollerance, trans))
    '                    Dim SettTollSNFRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionSNFRateTollerance, clsFixedParameterCode.ProductionSNFRateTollerance, trans))
    '                    For Each obj As clsRCDFStanardizationProduce In ArrProdItem
    '                        Dim objIMQCP As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(obj.Item_Code, trans)
    '                        If Math.Round(obj.Fat_Rate, 2, MidpointRounding.ToEven) > Math.Round(objIMQCP.FATRate + SettTollFATRate, 2, MidpointRounding.ToEven) OrElse Math.Round(obj.Fat_Rate, 2, MidpointRounding.ToEven) < Math.Round(objIMQCP.FATRate - SettTollFATRate, 2, MidpointRounding.ToEven) Then
    '                            Throw New Exception("Correct Your Produce Qty or Add/Remove Item." + Environment.NewLine + "Document [" + strCode + "] Item [" + obj.Item_Code + "] FAT Rate  [" + clsCommon.myCstr(Math.Round(obj.Fat_Rate, 2, MidpointRounding.ToEven)) + "] and FAT Tollerance [" + clsCommon.myCstr((Math.Round(objIMQCP.FATRate - SettTollFATRate, 2, MidpointRounding.ToEven))) + "-" + clsCommon.myCstr((Math.Round(objIMQCP.FATRate + SettTollFATRate, 2, MidpointRounding.ToEven))) + "]")
    '                        End If
    '                        If Math.Round(obj.SNF_Rate, 2, MidpointRounding.ToEven) > Math.Round(objIMQCP.SNFRate + SettTollSNFRate, 2, MidpointRounding.ToEven) OrElse Math.Round(obj.SNF_Rate, 2, MidpointRounding.ToEven) < Math.Round(objIMQCP.SNFRate - SettTollSNFRate, 2, MidpointRounding.ToEven) Then
    '                            Throw New Exception("Correct Your Produce Qty or Add/Remove Item." + Environment.NewLine + "Document [" + strCode + "] Item [" + obj.Item_Code + "] SNF Rate  [" + clsCommon.myCstr(Math.Round(obj.SNF_Rate, 2, MidpointRounding.ToEven)) + "] and SNF Tollerance [" + clsCommon.myCstr(Math.Round(objIMQCP.SNFRate - SettTollSNFRate, 2, MidpointRounding.ToEven)) + "-" + clsCommon.myCstr(Math.Round(objIMQCP.SNFRate + SettTollSNFRate, 2, MidpointRounding.ToEven)) + "]")
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End If
    '    Else

    '        clsStandardizationRM.SaveRM(strCode, arrLoc, trans)
    '        SendInventoryMovementAddedRemoved(Form_Id, strCode, arrLoc, trans)
    '        SendInventoryMovementMilk(Form_Id, strCode, arrLoc, trans)
    '        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans), "1") = CompairStringResult.Equal Then
    '            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ActivateSFGProduction, clsFixedParameterCode.ActivateSFGProduction, trans), "1") = CompairStringResult.Equal Then
    '                JournalEntrySFGProduction(strCode, trans, VoucherNo)
    '            Else
    '                JournalEntryWIP(trans, strCode, VoucherNo)
    '            End If
    '        End If
    '    End If
    '    Return True
    'End Function

    'Public Shared Function SendInventoryMovementAddedRemoved(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
    '    '----------inventory movement entry for added removed items from milk--------------------------------------------------
    '    Dim isSaved As Boolean = True
    '    Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(strCode, arrLoc, NavigatorType.Current, trans)
    '    Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
    '    Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

    '    If obj.ArrARItem IsNot Nothing AndAlso obj.ArrARItem.Count > 0 Then
    '        For Each objtr As clsRCDFStanardizationAddRemove In obj.ArrARItem
    '            Dim objInventoryMovemnt As New clsInventoryMovement()
    '            Dim objInventoryMovemntNew As New clsInventoryMovementNew
    '            Dim strProductType As String
    '            strProductType = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
    '            If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
    '                If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
    '                    objInventoryMovemntNew.InOut = "O"
    '                Else
    '                    objInventoryMovemntNew.InOut = "I"
    '                End If

    '                objInventoryMovemntNew.Location_Code = objtr.Location_Code
    '                objInventoryMovemntNew.Item_Code = objtr.Item_Code
    '                objInventoryMovemntNew.Item_Desc = objtr.Item_Desc
    '                objInventoryMovemntNew.Qty = objtr.Qty
    '                objInventoryMovemntNew.UOM = objtr.Unit_Code
    '                objInventoryMovemntNew.MRP = Nothing
    '                objInventoryMovemntNew.Add_Cost = Nothing
    '                objInventoryMovemntNew.Net_Cost = Nothing

    '                objInventoryMovemntNew.FAT_Per = objtr.FAT
    '                objInventoryMovemntNew.FAT_KG = objtr.FAT_KG
    '                objInventoryMovemntNew.SNF_KG = objtr.SNF_KG
    '                objInventoryMovemntNew.SNF_Per = objtr.SNF

    '                '' UPDATE PRODUCTION COST
    '                objInventoryMovemntNew.Fat_Rate = objtr.Fat_Rate
    '                objInventoryMovemntNew.SNF_Rate = objtr.SNF_Rate
    '                objInventoryMovemntNew.Fat_Amt = objtr.Fat_Amt
    '                objInventoryMovemntNew.SNF_Amt = objtr.SNF_Amt

    '                objInventoryMovemntNew.CalculateAvgCost = False
    '                objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = obj.Is_Job_Work_Inward

    '                objInventoryMovemntNew.Avg_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                objInventoryMovemntNew.FIFO_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                objInventoryMovemntNew.LIFO_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                If clsCommon.CompairString(objInventoryMovemntNew.InOut, "I") = CompairStringResult.Equal Then
    '                    objInventoryMovemntNew.Basic_Cost = If(objtr.Qty <= 0, 0, (objtr.Fat_Amt + objtr.SNF_Amt) / objtr.Qty)
    '                    objInventoryMovemntNew.Net_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                End If


    '                If clsCommon.CompairString(objtr.Item_Type, "R") = CompairStringResult.Equal Then
    '                    objInventoryMovemntNew.ItemType = "RM"
    '                ElseIf clsCommon.CompairString(objtr.Item_Type, "F") = CompairStringResult.Equal Then
    '                    objInventoryMovemntNew.ItemType = "FT"
    '                Else
    '                    objInventoryMovemntNew.ItemType = objtr.Item_Type
    '                End If
    '                'objInventoryMovemntNew.Basic_Cost = Nothing
    '                objInventoryMovemntNew.Batch_No = obj.Child_Batch_Code
    '                objInventoryMovemntNew.MFG_Date = Nothing
    '                objInventoryMovemntNew.Expiry_Date = Nothing
    '                ArrInventoryMovementNew.Add(objInventoryMovemntNew)
    '            Else
    '                If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
    '                    objInventoryMovemnt.InOut = "O"
    '                Else
    '                    objInventoryMovemnt.InOut = "I"
    '                End If

    '                objInventoryMovemnt.Location_Code = objtr.Location_Code
    '                objInventoryMovemnt.Item_Code = objtr.Item_Code
    '                objInventoryMovemnt.Item_Desc = objtr.Item_Desc
    '                objInventoryMovemnt.Qty = objtr.Qty
    '                objInventoryMovemnt.UOM = objtr.Unit_Code
    '                objInventoryMovemnt.MRP = Nothing
    '                objInventoryMovemnt.Add_Cost = Nothing

    '                objInventoryMovemnt.FAT_Per = objtr.FAT
    '                objInventoryMovemnt.FAT_KG = objtr.FAT_KG
    '                objInventoryMovemnt.SNF_KG = objtr.SNF_KG
    '                objInventoryMovemnt.SNF_Per = objtr.SNF

    '                '' UPDATE PRODUCTION COST
    '                objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
    '                objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
    '                objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
    '                objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt

    '                objInventoryMovemnt.CalculateAvgCost = False

    '                'objInventoryMovemnt.Net_Cost = Nothing
    '                objInventoryMovemnt.Avg_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                objInventoryMovemnt.FIFO_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                objInventoryMovemnt.LIFO_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
    '                    objInventoryMovemnt.Basic_Cost = If(objtr.Qty <= 0, 0, (objtr.Fat_Amt + objtr.SNF_Amt) / objtr.Qty)
    '                    objInventoryMovemnt.Net_Cost = objtr.Fat_Amt + objtr.SNF_Amt
    '                End If

    '                If clsCommon.CompairString(objtr.Item_Type, "R") = CompairStringResult.Equal Then
    '                    objInventoryMovemnt.ItemType = "RM"
    '                ElseIf clsCommon.CompairString(objtr.Item_Type, "F") = CompairStringResult.Equal Then
    '                    objInventoryMovemnt.ItemType = "FT"
    '                Else
    '                    objInventoryMovemnt.ItemType = objtr.Item_Type
    '                End If
    '                'objInventoryMovemnt.Basic_Cost = Nothing
    '                objInventoryMovemnt.Batch_No = obj.Child_Batch_Code
    '                objInventoryMovemnt.MFG_Date = Nothing
    '                objInventoryMovemnt.Expiry_Date = Nothing
    '                ArrInventoryMovement.Add(objInventoryMovemnt)

    '            End If
    '        Next
    '        '-----------other than milk product in inventory table
    '        If ArrInventoryMovement.Count > 0 Then
    '            isSaved = isSaved AndAlso clsInventoryMovement.SaveData(Form_Id, obj.Doc_Code, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '        End If

    '        If ArrInventoryMovementNew.Count > 0 Then
    '            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData(Form_Id, obj.Doc_Code, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
    '        End If


    '    End If
    '    Return isSaved
    '    '----------------------------------------------------------------------------------------

    'End Function
    'Public Shared Function SendInventoryMovementMilk(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
    '    Try
    '        Dim obj As clsInventoryMovement
    '        Dim objNew As clsInventoryMovementNew
    '        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
    '        Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
    '        'Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()

    '        Dim strq As String = ""
    '        Dim objSTD As clsRCDFStanardization = clsRCDFStanardization.GetData(strCode, arrLoc, NavigatorType.Current, trans)
    '        Dim objListProd As List(Of clsRCDFStanardizationProduce) = objSTD.ArrProduce

    '        If (objListProd IsNot Nothing AndAlso objListProd.Count > 0) Then
    '            For Each objProd As clsRCDFStanardizationProduce In objListProd
    '                Dim strItemTypeToSave As String = ""
    '                Dim strItemType As String
    '                Dim strProductType As String
    '                '' in produced item
    '                strProductType = clsItemMaster.GetItemProductType(objProd.Item_Code, trans)
    '                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
    '                    objNew = New clsInventoryMovementNew
    '                    objNew.Trans_Type = "Standardization"
    '                    objNew.InOut = "I"
    '                    objNew.Location_Code = objProd.In_LocationCode 'objSTD.Location_Code
    '                    objNew.main_location = objSTD.Location_Code
    '                    objNew.Item_Code = objProd.Item_Code
    '                    objNew.Item_Desc = clsItemMaster.GetItemName(objProd.Item_Code, trans)
    '                    objNew.Qty = objProd.Qty
    '                    objNew.UOM = objProd.Unit_Code
    '                    objNew.Source_Doc_No = objSTD.Doc_Code
    '                    objNew.Source_Doc_Date = objSTD.Doc_Date

    '                    objNew.CalculateAvgCost = False
    '                    objNew.DonNotCalculateAvgFATSNFCost = objSTD.Is_Job_Work_Inward

    '                    objNew.FAT_Per = objProd.FAT
    '                    objNew.SNF_Per = objProd.SNF
    '                    objNew.FAT_KG = objProd.FAT_KG
    '                    objNew.SNF_KG = objProd.SNF_KG

    '                    '' UPDATE PRODUCTION COST
    '                    objNew.Fat_Rate = objProd.Fat_Rate
    '                    objNew.SNF_Rate = objProd.SNF_Rate
    '                    objNew.Fat_Amt = objProd.Fat_Amt
    '                    objNew.SNF_Amt = objProd.SNF_Amt

    '                    objNew.Avg_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    objNew.FIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    objNew.LIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    If clsCommon.CompairString(objNew.InOut, "I") = CompairStringResult.Equal Then
    '                        objNew.Basic_Cost = If(objProd.Qty <= 0, 0, (objProd.Fat_Amt + objProd.SNF_Amt) / objProd.Qty)
    '                        objNew.Net_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    End If

    '                    strItemType = clsItemMaster.GetItemType(objProd.Item_Code, trans)
    '                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "RM"
    '                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "OT"
    '                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "FT"
    '                    Else
    '                        strItemTypeToSave = strItemType
    '                        'Throw New Exception("Item Type not found: " + strItemType)
    '                    End If
    '                    objNew.ItemType = strItemTypeToSave
    '                    'objNew.Basic_Cost = 0
    '                    objNew.MRP = 0
    '                    objNew.Add_Cost = 0
    '                    objNew.MRP = 0
    '                    objNew.MFG_Date = objSTD.Doc_Date

    '                    ArrInventoryMovementNew.Add(objNew)
    '                Else
    '                    obj = New clsInventoryMovement
    '                    obj.Trans_Type = "STD-Production"
    '                    obj.InOut = "I"
    '                    obj.Location_Code = objProd.In_LocationCode 'objSTD.Location_Code

    '                    obj.Item_Code = objProd.Item_Code
    '                    obj.Item_Desc = clsItemMaster.GetItemName(objProd.Item_Code, trans)
    '                    obj.Qty = objProd.Qty
    '                    obj.UOM = objProd.Unit_Code
    '                    obj.Source_Doc_No = objSTD.Doc_Code
    '                    obj.Source_Doc_Date = objSTD.Doc_Date
    '                    obj.CalculateAvgCost = False

    '                    obj.FAT_Per = objProd.FAT
    '                    obj.SNF_Per = objProd.SNF
    '                    obj.FAT_KG = objProd.FAT_KG
    '                    obj.SNF_KG = objProd.SNF_KG

    '                    obj.Fat_Rate = objProd.Fat_Rate
    '                    obj.SNF_Rate = objProd.SNF_Rate
    '                    obj.Fat_Amt = objProd.Fat_Amt
    '                    obj.SNF_Amt = objProd.SNF_Amt

    '                    obj.Avg_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    obj.FIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    obj.LIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
    '                        obj.Basic_Cost = If(objProd.Qty <= 0, 0, (objProd.Fat_Amt + objProd.SNF_Amt) / objProd.Qty)
    '                        obj.Net_Cost = objProd.Fat_Amt + objProd.SNF_Amt
    '                    End If
    '                    strItemType = clsItemMaster.GetItemType(objProd.Item_Code, trans)
    '                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "RM"
    '                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "OT"
    '                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                        strItemTypeToSave = "FT"
    '                    Else
    '                        strItemTypeToSave = strItemType
    '                    End If
    '                    obj.ItemType = strItemTypeToSave
    '                    obj.MRP = 0
    '                    obj.Add_Cost = 0
    '                    obj.MRP = 0
    '                    obj.MFG_Date = objSTD.Doc_Date
    '                    ArrInventoryMovement.Add(obj)
    '                End If
    '            Next
    '        End If

    '        '' out consumed data
    '        UpdateInventoryMovementConsumption(Form_Id, ArrInventoryMovement, ArrInventoryMovementNew, objSTD, Nothing, trans)

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '        Return False

    '    End Try
    '    Return True
    'End Function

    'Public Shared Function UpdateInventoryMovementConsumption(ByVal Form_Id As String, ByRef ArrInventoryMovement As List(Of clsInventoryMovement), ByRef ArrInventoryMovementNew As List(Of clsInventoryMovementNew), ByVal objSTD As clsRCDFStanardization, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim obj As clsInventoryMovement
    '    Dim objNew As clsInventoryMovementNew
    '    Dim objData As List(Of clsRCDFStanardizationIssue) = objSTD.ArrIssue ''By balwinder on 07/06/2019 For kwality limited.
    '    For Each dr As clsRCDFStanardizationIssue In objData
    '        Dim strItemTypeToSave As String = ""
    '        Dim strItemType As String
    '        Dim strProductType As String
    '        '' out consumed item
    '        strProductType = clsItemMaster.GetItemProductType(dr.Item_Code, trans)
    '        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
    '            objNew = New clsInventoryMovementNew
    '            objNew.Trans_Type = "STD-Consumption"
    '            objNew.InOut = "O"
    '            objNew.Location_Code = dr.Location_Code 'objSTD.Location_Code
    '            objNew.main_location = objSTD.Location_Code
    '            objNew.Item_Code = dr.Item_Code
    '            objNew.Item_Desc = clsItemMaster.GetItemName(dr.Item_Code, trans)
    '            objNew.Qty = dr.Qty
    '            objNew.UOM = dr.Unit_Code
    '            objNew.Source_Doc_No = objSTD.Doc_Code
    '            objNew.Source_Doc_Date = objSTD.Doc_Date

    '            objNew.FAT_Per = dr.FAT
    '            objNew.SNF_Per = dr.SNF
    '            objNew.FAT_KG = dr.FAT_KG
    '            objNew.SNF_KG = dr.SNF_KG

    '            '' UPDATE PRODUCTION COST
    '            objNew.Fat_Rate = dr.Fat_Rate
    '            objNew.SNF_Rate = dr.SNF_Rate
    '            objNew.Fat_Amt = dr.Fat_Amt
    '            objNew.SNF_Amt = dr.SNF_Amt
    '            objNew.CalculateAvgCost = False
    '            objNew.DonNotCalculateAvgFATSNFCost = True
    '            objNew.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            objNew.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            objNew.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            objNew.CalculateAvgCost = False
    '            strItemType = clsItemMaster.GetItemType(dr.Item_Code, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            objNew.ItemType = strItemTypeToSave
    '            objNew.Basic_Cost = 0
    '            objNew.Add_Cost = 0
    '            objNew.MRP = 0
    '            objNew.IS_CONSUMPTION = 1
    '            ArrInventoryMovementNew.Add(objNew)

    '        Else
    '            obj = New clsInventoryMovement
    '            obj.Trans_Type = "STD-Consumption"
    '            obj.InOut = "O"
    '            obj.Location_Code = dr.Location_Code 'objSTD.Location_Code
    '            obj.Item_Code = dr.Item_Code
    '            obj.Item_Desc = clsItemMaster.GetItemName(dr.Item_Code, trans)
    '            obj.Qty = dr.Qty
    '            obj.UOM = dr.Unit_Code
    '            obj.Source_Doc_No = objSTD.Doc_Code
    '            obj.Source_Doc_Date = objSTD.Doc_Date
    '            obj.CalculateAvgCost = False
    '            obj.FAT_Per = dr.FAT
    '            obj.SNF_Per = dr.SNF
    '            obj.FAT_KG = dr.FAT_KG
    '            obj.SNF_KG = dr.SNF_KG

    '            obj.Fat_Rate = dr.Fat_Rate
    '            obj.SNF_Rate = dr.SNF_Rate
    '            obj.Fat_Amt = dr.Fat_Amt
    '            obj.SNF_Amt = dr.SNF_Amt

    '            obj.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            obj.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            obj.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            obj.CalculateAvgCost = False
    '            strItemType = clsItemMaster.GetItemType(dr.Item_Code, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            obj.ItemType = strItemTypeToSave
    '            obj.Basic_Cost = 0
    '            obj.Add_Cost = 0
    '            obj.MRP = 0
    '            obj.IS_CONSUMPTION = 1
    '            ArrInventoryMovement.Add(obj)
    '        End If

    '    Next
    '    If ArrInventoryMovement.Count > 0 Then
    '        clsInventoryMovement.SaveData(Form_Id, objSTD.Doc_Code, clsCommon.GetPrintDate(objSTD.Doc_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objSTD.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '    End If
    '    If ArrInventoryMovementNew.Count > 0 Then
    '        clsInventoryMovementNew.SaveData(Form_Id, objSTD.Doc_Code, clsCommon.GetPrintDate(objSTD.Doc_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objSTD.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
    '    End If
    '    Return True
    'End Function





    'Public Shared Function GetAutoARQty(ByVal ItemCode As String, ByVal LocationCode As String, ByVal TransDate As String, ByVal DiffFatKg As String, DiffSnfKg As String, ByVal UOMCode As String) As String
    '    Dim qry As String = "select case when isnull(xx.[Fat Qty],0)<=isnull(xx.[SNF Qty],0) then isnull(xx.[Fat Qty],0) else isnull(xx.[SNF Qty],0) end as Qty  from(select " &
    '                        " ((((300*100)/((balance.CL_FAT_KG/(balance.CL_QTY*isnull(kgunitconv.Conversion_Factor,1))/isnull(unitconv.Conversion_Factor,1))*100))*isnull(Itemunitconv.Conversion_Factor,1))/isnull(kgunitconv.Conversion_Factor,1)) as [Fat Qty]," &
    '                        " ((((400*100)/((balance.CL_SNF_KG/(balance.CL_QTY*isnull(kgunitconv.Conversion_Factor,1))/isnull(unitconv.Conversion_Factor,1))*100))*isnull(Itemunitconv.Conversion_Factor,1))/isnull(kgunitconv.Conversion_Factor,1)) as [SNF Qty]" &
    '                        " from TSPL_FUN_ITEM_LOC_BALANCE('FG0000081','003','10-MAY-2017') as balance" &
    '                        " left outer join TSPL_ITEM_UOM_DETAIL unitconv ON unitconv.ITEM_Code=balance.item_code and unitconv.UOM_Code=balance.Stock_UOM" &
    '                        " left outer join TSPL_ITEM_UOM_DETAIL kgunitconv  ON kgunitconv.ITEM_Code=balance.item_code and kgunitconv.UOM_Code='KG'" &
    '                        " left outer join TSPL_ITEM_UOM_DETAIL Itemunitconv  ON Itemunitconv.ITEM_Code=balance.item_code and Itemunitconv.UOM_Code='Pouch')xx"
    '    Dim str As String = Nothing
    '    str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Nothing))
    '    Return str
    'End Function

    'Public Shared Function GetFinder_PendingBatchQuantity(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean, ByVal strStdCode As String) As String
    '    Dim ProductionOrStandAccordingToItemType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionOrStandAccordingToItemType, clsFixedParameterCode.ProductionOrStandAccordingToItemType, Nothing))
    '    Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.batch_code as Code,TSPL_PP_BATCH_ORDER_HEAD.batch_date as [Date], " &
    '        " TSPL_PP_BATCH_ORDER_HEAD.Description,  TSPL_PP_BATCH_ORDER_HEAD.Status,(case when TSPL_PP_BATCH_ORDER_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status], " &
    '        " TSPL_PP_BATCH_ORDER_HEAD.location_code as [Location Code],tspl_location_master.Location_Desc as [Location],  TSPL_PP_BATCH_ORDER_HEAD.structure_code as [Production Structure], " &
    '        " tspl_structure_master.structure_descq as [Structure],  TSPL_PP_BATCH_ORDER_HEAD.plan_code as [Plan Code], " &
    '        " (case when len(isnull(TSPL_PP_BATCH_ORDER_HEAD.main_batch_code,''))>0 then 'Child BO' else 'Main BO' end) as [BO Type], " &
    '        " TSPL_PP_BATCH_ORDER_HEAD.main_batch_code as [Main Batch Code],  TSPL_PP_BATCH_ORDER_HEAD.sub_batch_code as [Sub Batch Code],[Main Item Code],[Item Description]," &
    '        " coalesce(Prod.Unit_Code,Main.Unit_Code) as Unit_Code,coalesce(prod.Quantity,Main.Quantity) as Quantity, " &
    '        " prod.Qty as [Production Quantity],[Product Type] from TSPL_PP_BATCH_ORDER_HEAD  " &
    '        " left outer join tspl_location_master on TSPL_PP_BATCH_ORDER_HEAD.location_code=tspl_location_master.location_code  " &
    '        " left outer join tspl_structure_master on TSPL_PP_BATCH_ORDER_HEAD.structure_code=tspl_structure_master.structure_code " &
    '        " left join (select * from (   select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code) as S_no, " &
    '        " Batch_Code as Batch_Code_Main,  TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code as [Main Item Code],Item_Desc as [Item Description],Product_Type as [Product Type], " &
    '        " TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code, Quantity from TSPL_PP_BATCH_ORDER_BOM_DETAIL " &
    '        " left join TSPL_ITEM_MASTER on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code   where TSPL_ITEM_MASTER.Product_Type='MI'  ) as M_Inner where S_no=1 ) as Main on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Main.Batch_Code_Main" &
    '        " left join (select * from ( " &
    '        " select Batch_Code,UNIT_CODE,Quantity,Qty from (select ROW_NUMBER()  over(partition by Child_Batch_Code order by Product_Type desc, STD_Dtl.Item_Code) as S_no,STD.Child_Batch_Code as Batch_Code, " &
    '        " STD_Dtl.Item_Code,STD_Dtl.Unit_Code,sum(STD_Dtl.Quantity) as Quantity,sum(STD_Dtl.Qty) as Qty " &
    '        " from TSPL_RCDF_STD STD left join TSPL_RCDF_STD_PRODUCE STD_Dtl on STD.Doc_Code=STD_Dtl.Doc_Code " &
    '        " left join TSPL_ITEM_MASTER Item on STD_Dtl.Item_Code= Item.Item_Code  where Item.Product_Type='MI' and STD.Doc_Code not in ('" + strStdCode + "')  " &
    '        " group by STD.Child_Batch_Code,STD_Dtl.Item_Code,STD_Dtl.Unit_Code,Item.Product_Type " &
    '        " ) as Prod_STD where S_no=1 " &
    '        " union all " &
    '        " select  Batch_Code,UNIT_CODE,sum(Quantity) as Quantity,sum(Qty) as Qty from ( " &
    '        " select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, PE_Dtl.Item_Code) as S_no,PE.Batch_Code, " &
    '        " PE_Dtl.Item_Code,PE_Dtl.Unit_Code,sum(PE_Dtl.BATCH_QTY) as Quantity,sum(PE_Dtl.FINAL_PRODUCTION_QTY) as Qty " &
    '        " from TSPL_PP_PRODUCTION_ENTRY PE left join TSPL_PP_PRODUCTION_ENTRY_DETAIL PE_Dtl on PE.PROD_ENTRY_CODE=PE_Dtl.PROD_ENTRY_CODE " &
    '        " left join TSPL_ITEM_MASTER Item on PE_Dtl.Item_Code= Item.Item_Code  where Item.Product_Type='MI'  " &
    '        " group by PE.Batch_Code,PE_Dtl.Item_Code,PE_Dtl.Unit_Code,Item.Product_Type" &
    '        " ) as Prod_Main group by Batch_Code,UNIT_CODE " &
    '        " ) as Final_Prod) as Prod " &
    '        " on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Prod.Batch_Code "
    '    '" ) as Final_Table where Quantity>[Production Quantity]"
    '    Dim str As String = ""
    '    If ProductionOrStandAccordingToItemType = 1 Then
    '        qry = "select  Code, [Date],  Description,  Status, [Post Status],   [Location Code], [Location],  [Production Structure],  [Structure],   [Plan Code],   [BO Type],   [Main Batch Code],  [Sub Batch Code],[Main Item Code],[Item Description],aa.Unit_Code, Quantity,   [Production Quantity],[Product Type] from ( " & qry & " Where " & whrCls & " ) aa left join TSPL_ITEM_MASTER on aa.[Main Item Code]=TSPL_ITEM_MASTER.Item_Code "
    '        whrCls = "TSPL_ITEM_MASTER.Item_Type='S' "
    '    End If
    '    str = clsCommon.ShowSelectForm("BTCHFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

    '    Return str
    'End Function

    'Public Shared Function GetKG_AfterConversion(ByVal Item_Code As String, ByVal Unit_Code As String, ByVal Qty As Decimal, ByVal trans As SqlTransaction) As Decimal
    '    Dim Kg_Value As Decimal = 0
    '    Dim Wt_uom As String = clsCommon.myCstr(clsItemMaster.GetItemWeightUnit(Item_Code, trans))
    '    Dim Wt_Value As Decimal = clsCommon.myCdbl(clsItemMaster.GetItemWeightValue(Item_Code, trans))
    '    Dim Cnvsrn_Factr As Decimal = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans))
    '    Dim Weight_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
    '    Dim KG_Cnvrsn_Value As Decimal = Nothing
    '    Dim qry As String = ""
    '    If clsCommon.CompairString(Wt_uom, Weight_KG_Unit) = CompairStringResult.Equal Then
    '        KG_Cnvrsn_Value = 1
    '    Else
    '        ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
    '        Dim ItemStructureMandatoryOnWeightConversion As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, trans)) = 1, True, False))
    '        qry = "select top 1 CF from (Select (case when (Container_UOM='" & Wt_uom & "' and Contained_UOM='" & Weight_KG_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_KG_Unit & "' and Contained_UOM='" & Wt_uom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(Item_Code, trans) + "')  " & IIf(ItemStructureMandatoryOnWeightConversion = True, " and isnull(Structure_Code,'') =(select Structure_Code  from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(Item_Code) & "')", "") & " )aa where isnull(cast(CF as float),0)<>0 order by Product_Type desc"
    '        KG_Cnvrsn_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    '    End If

    '    If KG_Cnvrsn_Value > 0 Then
    '        Kg_Value = ((Wt_Value * Qty * Cnvsrn_Factr) * KG_Cnvrsn_Value)
    '    Else
    '        Kg_Value = 0
    '    End If

    '    Return Kg_Value
    'End Function
    'Public Shared Function GetMilkAndALLItemStockBalance_With_FATSNFKG(ByVal icode As String, ByVal strLocation As String, ByVal strSubLocCode As String, ByVal dtDocumentDate As Date, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal Empty_Stock_Loc_Allowed As Boolean) As String
    '    Dim qty As Double = 0
    '    Dim qry As String = ""

    '    qry = "select ICode,Location,SUM(qty*RI) as Qty,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg from ("
    '    qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ("
    '    qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
    '    qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
    '    qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
    '    qry += ",0 as fat_kg,0 as snf_kg"
    '    qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
    '    qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
    '    qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "') " ' and tspl_location_master.location_code='" + strLocation + "'

    '    Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
    '    If intSettingType = 1 Then
    '        qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
    '    ElseIf intSettingType = 0 Then
    '        qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
    '    End If
    '    qry += " union all "

    '    qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.UOM as UOMNew "
    '    qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
    '    qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
    '    qry += " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + icode + "' "
    '    qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

    '    If intSettingType = 1 Then
    '        qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
    '    ElseIf intSettingType = 0 Then
    '        qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
    '    End If

    '    qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "')axx group by axx.Icode,axx.Location "

    '    If Empty_Stock_Loc_Allowed Then
    '        qry += " union all " + Environment.NewLine + Environment.NewLine
    '        qry += "select '' as ICode,axx1.Location,SUM(axx1.qty * axx1.RI) as Qty,sum(axx1.fat_kg * axx1.RI) as fat_kg,sum(axx1.snf_kg * axx1.RI) as snf_kg from ("
    '        qry += " select xx1.ICode,xx1.Location, xx1.Qty as OldQty,xx1.fat_kg as old_fatkg,xx1.snf_kg as old_snfkg,xx1.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as snf_kg from ("
    '        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
    '        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
    '        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
    '        qry += ",0 as fat_kg,0 as snf_kg"
    '        qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
    '        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code <> '" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
    '        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "') " ' and tspl_location_master.location_code='" + strLocation + "'

    '        If intSettingType = 1 Then
    '            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
    '        ElseIf intSettingType = 0 Then
    '            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
    '        End If
    '        qry += " union all "

    '        qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.UOM as UOMNew "
    '        qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
    '        qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
    '        qry += " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code <> '" + icode + "' "
    '        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

    '        If intSettingType = 1 Then
    '            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
    '        ElseIf intSettingType = 0 Then
    '            qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
    '        End If

    '        qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx1 left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx1.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx1.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM1 on FinalUOM1.Item_Code=xx1.ICode and FinalUOM1.UOM_Code='" + strUOM + "')axx1 group by axx1.Location having SUM(axx1.qty * axx1.RI)=0 " + Environment.NewLine + Environment.NewLine
    '    End If

    '    Dim strr As String = "select Fnl.* from (select final.icode as [Item Code],tspl_item_master.item_desc as [Item Name],TSPL_LOCATION_MASTER.main_location_code as [Main Location],final.location as [Code],TSPL_LOCATION_MASTER.location_desc as [Location Name],(case when TSPL_LOCATION_MASTER.is_section='Y' then 'Section' when TSPL_LOCATION_MASTER.is_sub_location='Y' then 'Sub Location' else 'Main Location' end) as [Type],SUM(final.qty) as [Stock Qty],sum(final.fat_kg) as [Fat Kg],sum(final.snf_kg) as [SNF Kg],max(tspl_location_master.is_Consumption_Location) as is_Consumption_Location  from (" + qry + ")final left outer join tspl_item_master on tspl_item_master.item_code=final.icode left outer join tspl_location_master on tspl_location_master.location_code=final.location where 1=1 " &
    '        " group by final.icode,tspl_item_master.item_desc,TSPL_LOCATION_MASTER.main_location_code,final.location,TSPL_LOCATION_MASTER.location_desc,TSPL_LOCATION_MASTER.is_section,TSPL_LOCATION_MASTER.is_sub_location)Fnl "

    '    Return strr

    'End Function




    'Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStandardization + "'", trans))
    '    Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStandardization + "'", trans))
    '    Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStandardization + "'", trans))
    '    Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStandardization + "'", trans))
    '    ' Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + StrDocNo + "'", trans))

    '    '' work to be done agiast ticket no. BHA/11/09/18-000535  date 13/09/2018 

    '    Dim qry As String = " select TSPL_RCDF_STD.Doc_Code,TSPL_RCDF_STD.Doc_Date,TSPL_RCDF_STD.Main_Batch_Code"
    '    qry += " ,TSPL_PP_STD_QC_DETAIL.Item_code,TSPL_ITEM_MASTER.Item_Desc"
    '    qry += " ,TSPL_RCDF_STD.Batch_No from TSPL_RCDF_STD"
    '    qry += " left outer join TSPL_PP_STD_QC_DETAIL on TSPL_PP_STD_QC_DETAIL.Doc_Code=TSPL_RCDF_STD.Doc_Code"
    '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_QC_DETAIL.Item_Code "
    '    qry += " where 2=2 and TSPL_RCDF_STD.Doc_Code='" + StrDocNo + "' "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    '    If clsCommon.myLen(strNotifiContent) > 0 Then
    '        Dim objNotification As New clsNotificationHead()
    '        objNotification.Notification_Text = strNotifiContent
    '        objNotification.Notification_Caption = strNotifiCaption
    '        objNotification.Notification_On = strNotificationOn
    '        objNotification.Notification_Detail_Text = strNotifi_DetalContent
    '        objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
    '        objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(dt.Rows(0)("Doc_Date"))))
    '        objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Item_Code, clsCommon.myCstr(dt.Rows(0)("Item_Code")))
    '        objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Item_Name, clsCommon.myCstr(dt.Rows(0)("Item_Desc")))
    '        objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Batch_No, clsCommon.myCstr(dt.Rows(0)("Batch_No")))
    '        objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Batch_Order_No, clsCommon.myCstr(dt.Rows(0)("Main_Batch_Code")))
    '        objNotification.SaveData(clsUserMgtCode.frmProcessProductionStandardization, objNotification, trans)
    '        objNotification = Nothing
    '        Return True
    '    End If
    '    Return False
    'End Function




    'Public Shared Function GetItemQCParameter(ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
    '    Dim qry As String = "select TSPL_ITEM_QC_PARAMETER_MASTER.code,tspl_parameter_master.Type,TSPL_ITEM_QC_PARAMETER_MASTER.lower_range, " &
    '    " TSPL_ITEM_QC_PARAMETER_MASTER.upper_range,tspl_parameter_master.description,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range, " &
    '    " TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status from TSPL_ITEM_QC_PARAMETER_MASTER " &
    '    " left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code " &
    '    " where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" & Item_Code & "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    Return dt
    'End Function
    'Public Shared Function GetIssueAgainstBatch(ByVal Batch_Code As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
    '    'Dim qry As String = "select Issue.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc,Issue.To_Location_Code " & _
    '    '" from (select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,TSPL_RCDF_STD_ISSUE.To_Location_Code,sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Issue_Qty, " & _
    '    '" sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG) as Issued_FAT_KG,(sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 as Issued_FAT_Pers, " & _
    '    '" sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG) as Issued_SNF_KG,(sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 as Issued_SNF_Pers, " & _
    '    '" (sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)) as Issued_FAT_Rate,(sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)) as Issued_SNF_Rate,sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt) as Issued_FAT_Amt,sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt) as Issued_SNF_Amt " & _
    '    '" from TSPL_PP_ISSUE_ITEM_DETAIL " & _
    '    '" inner join TSPL_RCDF_STD_ISSUE on TSPL_RCDF_STD_ISSUE.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code " & _

    '    Dim qry As String = " select Issue.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc,Issue.To_Location_Code  from (select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,TSPL_RCDF_STD_ISSUE.To_Location_Code,sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Issue_Qty,  sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG) as Issued_FAT_KG,case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 end as Issued_FAT_Pers,  sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG) as Issued_SNF_KG,case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 end as Issued_SNF_Pers,  case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)) end as Issued_FAT_Rate,case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)) end as Issued_SNF_Rate,sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt) as Issued_FAT_Amt,sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt) as Issued_SNF_Amt  from TSPL_PP_ISSUE_ITEM_DETAIL  inner join TSPL_RCDF_STD_ISSUE on TSPL_RCDF_STD_ISSUE.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code  where TSPL_RCDF_STD_ISSUE.Batch_Code='" & Batch_Code & "' and TSPL_RCDF_STD_ISSUE.Is_post=1 and (TSPL_RCDF_STD_ISSUE.Doc_Code is null or TSPL_RCDF_STD_ISSUE.Doc_Code='" & Doc_Code & "') group by TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,TSPL_RCDF_STD_ISSUE.To_Location_Code) as Issue " &
    '    " left join TSPL_ITEM_MASTER on Issue.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
    '    " left join TSPL_UNIT_MASTER on Issue.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "

    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    Return dt
    'End Function
    'Public Shared Function GetQCParameters(ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
    '    'Dim qry As String = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code, " & _
    '    '" TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc," & _
    '    '" TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else " & _
    '    '" case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature," & _
    '    '" TSPL_PARAMETER_MASTER.Nature as Nature_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Lower_range,TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range," & _
    '    '" TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status,'' AS QC_Status " & _
    '    '" from TSPL_ITEM_QC_PARAMETER_MASTER " & _
    '    '" left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code " & _
    '    '" and tspl_item_master.comp_code=tspl_item_qc_parameter_master.comp_code " & _
    '    '" left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code " & _
    '    '" and tspl_parameter_master.comp_code=tspl_item_qc_parameter_master.comp_code " & _
    '    '" where tspl_item_qc_parameter_master." & _
    '    '" and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in (select distinct Item_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
    '    '" inner join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & _
    '    '" where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code='" & Batch_Code & "')"
    '    Dim qry As String = GetQCParametersQry(trans)
    '    qry = qry & " and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in (select distinct Item_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " &
    '    " inner join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " &
    '    " where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code='" & Batch_Code & "')"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    Return dt
    'End Function
    'Public Shared Function GetQCParametersForItem(ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
    '    Dim qry As String = GetQCParametersQry(trans)
    '    qry = qry & " and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code ='" & Item_Code & "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    Return dt
    'End Function
    'Public Shared Function GetQCParametersQry(Optional ByVal trans As SqlTransaction = Nothing) As String
    '    Dim qry As String = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code, " &
    '    " TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc," &
    '    " TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else " &
    '    " case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature," &
    '    " TSPL_PARAMETER_MASTER.Nature as Nature_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Lower_range,TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range," &
    '    " TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status,'' AS QC_Status " &
    '    " from TSPL_ITEM_QC_PARAMETER_MASTER " &
    '    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code   " &
    '    " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code   " &
    '    " where tspl_item_qc_parameter_master."
    '    Return qry
    'End Function
    'Public Shared Function OpenParameterValueList(ByVal code As String) As DataTable
    '    Dim dt As DataTable

    '    Dim qry As String = " select '' as Code union all  select  Value as Code from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "

    '    dt = clsDBFuncationality.GetDataTable(qry)


    '    Return dt
    'End Function
    'Public Shared Function GetFinderParameterValueList(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
    '    Dim qry As String = "select Parameter_CODE as Parameter_Code,Value  from TSPL_PARAMETER_VALUE_MASTER  "
    '    Dim str As String = ""

    '    str = clsCommon.ShowSelectForm("STD", qry, "Value", whrCls, currCode, "Value", isButtonClicked)

    '    Return str
    'End Function
    'Public Shared Function FillStageDetail(ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As ClsSectionStageMapping
    '    Dim qry As String = ""
    '    Dim obj As ClsSectionStageMapping = Nothing
    '    qry = " select distinct top 1 TSPL_SECTION_STAGE_MAPPING.Doc_Code  from TSPL_SECTION_STAGE_MAPPING inner join TSPL_SECTION_STAGE_MAPPING_HEAD on " &
    '          " TSPL_SECTION_STAGE_MAPPING_HEAD.Doc_Code = TSPL_SECTION_STAGE_MAPPING.Doc_Code " &
    '          " left join TSPL_STAGE_MASTER on TSPL_SECTION_STAGE_MAPPING.Stage_Code=TSPL_STAGE_MASTER.Stage_Code " &
    '          " where TSPL_SECTION_STAGE_MAPPING_HEAD.Structure_Code in " &
    '          " (select Structure_Code from TSPL_PP_BATCH_ORDER_HEAD where Batch_Code='" & Batch_Code & "') " &
    '          " and TSPL_SECTION_STAGE_MAPPING_HEAD.Section_Code in (select Section_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " &
    '          " where Batch_Code= '" & Batch_Code & "') "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    If dt.Rows.Count > 0 Then
    '        If clsCommon.myLen(dt.Rows(0).Item("Doc_Code")) Then
    '            obj = ClsSectionStageMapping.GetData(dt.Rows(0).Item("Doc_Code"), NavigatorType.Current)
    '        End If
    '    End If
    '    Return obj
    'End Function
    'Public Shared Function GetStageQCStatus() As DataTable
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    Dim dr As DataRow
    '    dr = dt.NewRow()
    '    dr("Code") = "0"
    '    dr("Name") = "Not Complete"
    '    dt.Rows.Add(dr)


    '    dr = dt.NewRow()
    '    dr("Code") = "1"
    '    dr("Name") = "Complete"
    '    dt.Rows.Add(dr)
    '    If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToSkipStageQLLogSheetInProd, clsFixedParameterCode.AllowToSkipStageQLLogSheetInProd, Nothing)) > 0) Then
    '        dr = dt.NewRow()
    '        dr("Code") = "2"
    '        dr("Name") = "Skip"
    '        dt.Rows.Add(dr)
    '    End If


    '    Return dt
    'End Function
    'Public Shared Function ReturnQueryForOtherItemStock(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As String
    '    Dim strCondition As String = ""
    '    Dim strCondition1 As String = ""

    '    If clsCommon.myLen(strSubLocation) > 0 Then
    '        '    strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
    '        strCondition1 = "  and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
    '    Else
    '        '   strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' "
    '        strCondition1 = " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' "
    '    End If
    '    If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
    '        strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
    '    ElseIf clsCommon.myLen(strSubLocation) > 0 Then
    '        strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strSubLocation + "' "
    '    ElseIf clsCommon.myLen(strLocation) > 0 Then
    '        strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' "
    '    End If


    '    Dim qry As String = "select SUM(qty*RI) as Qty,ICode,Location from(" + Environment.NewLine
    '    qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty" + Environment.NewLine
    '    qry += " from (" + Environment.NewLine

    '    qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
    '    qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
    '    qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code , TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Qty   ,TSPL_INVENTORY_MOVEMENT_NEW.UOM as UOMNew "
    '    qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
    '    qry += " where TSPL_INVENTORY_MOVEMENT_NEW.Qty<>0 and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code<>'" + strICode + "' " + strCondition + " "
    '    'If dblMRP > 0 Then
    '    '    qry += " and TSPL_INVENTORY_MOVEMENT_NEW.MRP='" + clsCommon.myCstr(dblMRP) + "'"
    '    'End If

    '    Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
    '    If intSettingType = 1 Then
    '        qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
    '    ElseIf intSettingType = 0 Then
    '        qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
    '    End If
    '    qry += " )xxx  "
    '    qry += " )xxxx group by Item_Code,Location_Code,UOMNew "


    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_Dispatch_BulkSale.Location_Code as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom "
    '    qry += " from TSPL_Dispatch_Detail_BulkSale "
    '    qry += " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No"
    '    qry += " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code<>'" + strICode + "' and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' and TSPL_Dispatch_Detail_BulkSale.Qty<>0  "
    '    qry += " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"
    '    'If dblMRP > 0 Then
    '    '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
    '    'End If

    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom "
    '    qry += " from TSPL_PP_ISSUE_ITEM_DETAIL "
    '    qry += " left outer join TSPL_RCDF_STD_ISSUE on TSPL_RCDF_STD_ISSUE.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code"
    '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code "
    '    qry += " where TSPL_RCDF_STD_ISSUE.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code<>'" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  "
    '    qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"

    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Locaion,TSPL_CSA_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_CSA_TRANSFER_DETAIL.Unit_code AS Uom "
    '    qry += " from TSPL_CSA_TRANSFER_DETAIL "
    '    qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code"
    '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code "
    '    qry += " where TSPL_CSA_TRANSFER_HEAD.status=0 and TSPL_CSA_TRANSFER_DETAIL.Item_Code<>'" + strICode + "' and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + strLocation + "' and TSPL_CSA_TRANSFER_DETAIL.Qty<>0  "
    '    qry += " and TSPL_CSA_TRANSFER_DETAIL.doc_code not in ('" + strDocumentNo + "')"

    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as Locaion,TSPL_SD_SALE_INVOICE_DETAIL.Qty,-1 as RI,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code AS Uom "
    '    qry += " from TSPL_SD_SALE_INVOICE_DETAIL "
    '    qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code"
    '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
    '    qry += " where TSPL_SD_SALE_INVOICE_HEAD.status=0 and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code<>'" + strICode + "' and TSPL_SD_SALE_INVOICE_HEAD.bill_to_location='" + strLocation + "' and TSPL_SD_SALE_INVOICE_DETAIL.Qty<>0  "
    '    qry += " and TSPL_SD_SALE_INVOICE_DETAIL.document_code not in ('" + strDocumentNo + "')"

    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.bill_to_location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom "
    '    qry += " from TSPL_SD_SHIPMENT_DETAIL "
    '    qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code"
    '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code "
    '    qry += " where TSPL_SD_SHIPMENT_HEAD.status=0 and TSPL_SD_SHIPMENT_DETAIL.Item_Code<>'" + strICode + "' and TSPL_SD_SHIPMENT_HEAD.bill_to_location='" + strLocation + "' and TSPL_SD_SHIPMENT_DETAIL.Qty<>0  "
    '    qry += " and TSPL_SD_SHIPMENT_DETAIL.document_code not in ('" + strDocumentNo + "')"
    '    'If dblMRP > 0 Then
    '    '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
    '    'End If


    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code as ICode,case when ISNULL(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No,'')<>'' then TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No else TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code end  as Locaion,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity as Qty,-1 as RI,TSPL_ITEM_MASTER.Unit_code AS Uom "
    '    qry += " from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
    '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
    '    qry += " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code<>'" + strICode + "' " + strCondition1 + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity<>0  "
    '    qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in ('" + strDocumentNo + "')"
    '    qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in (select LoadingTanker_No FROM TSPL_Quality_Check_BulkSale LEFT OUTER JOIN TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No WHERE ISNULL(TSPL_Dispatch_BulkSale.QC_Code,'')<>'')"
    '    'qry += " and not exists (select 1 from TSPL_LOADING_TANKER_DETAIL_BULKSALE Left outer Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No)"

    '    qry += " union all " + Environment.NewLine

    '    qry += " select TSPL_MCC_Dispatch_Challan.Item_Code as ICode,TSPL_MCC_Dispatch_Challan.MCC_Code as Locaion,TSPL_MCC_Dispatch_Challan.Net_Qty,-1 as RI,'' AS Uom "
    '    qry += " from TSPL_MCC_Dispatch_Challan "
    '    qry += " where TSPL_MCC_Dispatch_Challan.IsPosted=0 and TSPL_MCC_Dispatch_Challan.Item_Code<>'" + strICode + "' and TSPL_MCC_Dispatch_Challan.MCC_Code='" + strLocation + "' and TSPL_MCC_Dispatch_Challan.Net_Qty<>0  "
    '    qry += " and TSPL_MCC_Dispatch_Challan.Chalan_No not in ('" + strDocumentNo + "')"

    '    '' query for add/remove items durng Process production Standardization
    '    qry += " union all " + Environment.NewLine
    '    qry += " select TSPL_RCDF_STD_ADD_REMOVE.Item_Code,TSPL_RCDF_STD_ADD_REMOVE.Location_Code,"
    '    qry += " TSPL_RCDF_STD_ADD_REMOVE.Qty,"
    '    qry += " (case when TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
    '    qry += " TSPL_RCDF_STD_ADD_REMOVE.UNIT_CODE from TSPL_RCDF_STD_ADD_REMOVE "
    '    qry += " inner join TSPL_RCDF_STD on TSPL_RCDF_STD_ADD_REMOVE.Doc_Code = TSPL_RCDF_STD.Doc_Code "
    '    qry += " where TSPL_RCDF_STD.Posted=0 and TSPL_RCDF_STD_ADD_REMOVE.Item_Code<>'" + strICode + "' "
    '    qry += " and TSPL_RCDF_STD_ADD_REMOVE.Doc_Code not in ('" + strDocumentNo + "')"
    '    qry += " and TSPL_RCDF_STD_ADD_REMOVE.Location_Code='" & strLocation & "' "

    '    '' query for  Process production Standardization
    '    qry += " union all " + Environment.NewLine
    '    qry += " select TSPL_RCDF_STD_PRODUCE.Item_Code,TSPL_RCDF_STD_PRODUCE.In_LocationCode,"
    '    qry += " TSPL_RCDF_STD_PRODUCE.Qty,"
    '    qry += " 1 as RI,"
    '    qry += " TSPL_RCDF_STD_PRODUCE.UNIT_CODE from TSPL_RCDF_STD_PRODUCE "
    '    qry += " inner join TSPL_RCDF_STD on TSPL_RCDF_STD_PRODUCE.Doc_Code = TSPL_RCDF_STD.Doc_Code "
    '    qry += " where TSPL_RCDF_STD.Posted=0 and TSPL_RCDF_STD_PRODUCE.Item_Code<>'" + strICode + "' "
    '    qry += " and TSPL_RCDF_STD_PRODUCE.Doc_Code not in ('" + strDocumentNo + "')"
    '    qry += " and TSPL_RCDF_STD_PRODUCE.In_LocationCode='" & strLocation & "' "

    '    '' PRODUCTION CONSUMPTION 
    '    qry += " union all " + Environment.NewLine
    '    qry += " select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," &
    '          " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY,-1 as RI," &
    '          " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " &
    '          " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
    '          " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE<>'" & strICode & "' " &
    '          " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "') " &
    '          " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE='" & strLocation & "'"

    '    '' query for add/remove items durng Process production STAGE PROCESS
    '    qry += " union all " + Environment.NewLine
    '    qry += " select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Location_Code,"
    '    qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Qty,"
    '    qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
    '    qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
    '    qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
    '    qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code<>'" + strICode + "' "
    '    qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + strDocumentNo + "')"
    '    qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Location_Code='" & strLocation & "' "

    '    '' PRODUCTION ENTRY 
    '    qry += " union all " + Environment.NewLine
    '    qry += " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE," &
    '           " TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,1 as RI," &
    '           " TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE from TSPL_PP_PRODUCTION_ENTRY_DETAIL " &
    '           " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
    '           " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE<>'" & strICode & "' " &
    '           " and TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "')" &
    '           " and TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE='" & strLocation & "'"


    '    qry += " )xx" + Environment.NewLine
    '    qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM"
    '    qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "'"
    '    qry += " )xxx group by ICode,Location having SUM(qty*RI)>0"
    '    Return qry
    'End Function
    'Public Shared Function JournalEntry(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
    '    Dim isSaved As Boolean = True
    '    Dim VoucherDesc As String = ""
    '    Dim SourceDocDesc As String = ""
    '    Dim SourceDocNo As String
    '    Dim Comments As String
    '    Dim Remarks As String

    '    Dim i As Integer = 0
    '    Try
    '        'Dim JRNL_DATE As Date = clsCommon.GETSERVERDATE(trans)
    '        Dim Count As Integer = 0
    '        Dim qry As String
    '        Dim dtGL As DataTable
    '        Dim TotalDebitAmt As Decimal = 0
    '        Dim TotalCreditAmt As Decimal = 0
    '        Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(Doc_Code, "", NavigatorType.Current, trans)
    '        Dim ArryLstGLAC As ArrayList = New ArrayList()
    '        VoucherDesc = "Financial Entry for Production Standardization -" & obj.Doc_Code & " "
    '        SourceDocDesc = "Production Standardization"
    '        SourceDocNo = obj.Doc_Code
    '        Comments = "Production Standardization"
    '        Remarks = "Production Standardization"

    '        '' credit wip account of consumption items
    '        qry = " SELECT Consm.CONSM_ITEM_CODE,Consm.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount " &
    '              " FROM TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL  Consm " &
    '              " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
    '              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
    '              " WHERE Consm.Doc_Code='" & obj.Doc_Code & "'"

    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            '' check for account setting  exist or not
    '            If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '                Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '            End If
    '            Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(CreditAcc) > 0 Then
    '                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If

    '            TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        Next

    '        qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account as CreditAccount,FE.Cost as Avg_Cost from ( " &
    '                          " select TSPL_RCDF_STD_ADD_REMOVE.Item_Code,((case when TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' then  TSPL_RCDF_STD_ADD_REMOVE.Fat_Amt  else -TSPL_RCDF_STD_ADD_REMOVE.Fat_Amt end) " &
    '                          " +(case when TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' then  TSPL_RCDF_STD_ADD_REMOVE.SNF_Amt  else -TSPL_RCDF_STD_ADD_REMOVE.SNF_Amt end)) as Cost from TSPL_RCDF_STD_ADD_REMOVE " &
    '                          " inner join TSPL_RCDF_STD on TSPL_RCDF_STD_ADD_REMOVE.Doc_Code=TSPL_RCDF_STD.Doc_Code " &
    '                          " where TSPL_RCDF_STD.Doc_Code='" & obj.Doc_Code & "' ) as FE " &
    '                          " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
    '                          " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            '' check for account setting  exist or not
    '            If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '                Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '            End If
    '            Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(CreditAcc) > 0 Then
    '                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If

    '            TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        Next

    '        ' '' credit wip account of overhead cost
    '        'qry = " select Cost.COST_CODE,Cost.OverHead_Cost as Avg_Cost,TSPL_OVERHEAD_COST.GL_Acc as CreditAccount from TSPL_PP_COST_WITHOUT_BATCH Cost " & _
    '        '      " left join TSPL_OVERHEAD_COST on Cost.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE " & _
    '        '      " WHERE Cost.PROD_ENTRY_CODE='" & obj.Doc_Code & "'"

    '        'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        'For Each grow As DataRow In dtGL.Rows
    '        '    '' check for account setting  exist or not
    '        '    If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '        '        Throw New Exception("GL Account not found for Cost Code " & grow.Item("COST_CODE") & "")
    '        '    End If
    '        '    Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '        '    If clsCommon.myLen(CreditAcc) > 0 Then
    '        '        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '        '        ArryLstGLAC.Add(Acc2)
    '        '    End If

    '        '    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        'Next

    '        '' credit wip account of production items
    '        qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,PED.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount from TSPL_RCDF_STD_PRODUCE PED " &
    '              " left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
    '              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
    '              " WHERE PED.Doc_Code='" & obj.Doc_Code & "'"

    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            '' check for account setting  exist or not
    '            If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
    '                Throw New Exception("Inventory Control account not found for Item Code " & grow.Item("ITEM_CODE") & "")
    '            End If
    '            Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(DebitAcc) > 0 Then
    '                Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If

    '            TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        Next

    '        Dim GLDesc As String = "Journal Entry Against Production Standardization- Doc No." & obj.Doc_Code & " "

    '        Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-ST' and Source_Doc_No='" & obj.Doc_Code & "'", trans))
    '        If clsCommon.myLen(VoucherNo) > 0 Then
    '            isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, VoucherNo, trans, obj.Doc_Date, GLDesc, "PR-ST", "Production Standardization", obj.Doc_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
    '        Else
    '            isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Doc_Date, GLDesc, "PR-ST", "Production Standardization", obj.Doc_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
    '        End If

    '        Return isSaved
    '    Catch ex As Exception

    '        Throw New Exception(ex.Message)
    '        Return False
    '    End Try
    'End Function
    'Public Shared Function JournalEntryWIP(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
    '    Dim isSaved As Boolean = True
    '    Dim VoucherDesc As String = ""
    '    Dim SourceDocDesc As String = ""
    '    Dim SourceDocNo As String
    '    Dim Comments As String
    '    Dim Remarks As String

    '    Dim i As Integer = 0
    '    Try
    '        'Dim JRNL_DATE As Date = clsCommon.GETSERVERDATE(trans)
    '        Dim Count As Integer = 0
    '        Dim qry As String
    '        Dim dtGL As DataTable
    '        Dim TotalDebitAmt As Decimal = 0
    '        Dim TotalCreditAmt As Decimal = 0
    '        Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(Doc_Code, "", NavigatorType.Current, trans)
    '        Dim ArryLstGLAC As ArrayList = New ArrayList()
    '        VoucherDesc = "Financial Entry for Production Standardization -" & obj.Doc_Code & " "
    '        SourceDocDesc = "Production Standardization"
    '        SourceDocNo = obj.Doc_Code
    '        Comments = "Production Standardization"
    '        Remarks = "Production Standardization"

    '        ' '' credit wip account of consumption items
    '        'qry = " SELECT Consm.CONSM_ITEM_CODE,Consm.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount " & _
    '        '      " FROM TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL  Consm " & _
    '        '      " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
    '        '      " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
    '        '      " WHERE Consm.Doc_Code='" & obj.Doc_Code & "'"

    '        'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        'For Each grow As DataRow In dtGL.Rows
    '        '    '' check for account setting  exist or not
    '        '    If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '        '        Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '        '    End If
    '        '    Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '        '    If clsCommon.myLen(CreditAcc) > 0 Then
    '        '        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '        '        ArryLstGLAC.Add(Acc2)
    '        '    End If

    '        '    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        'Next

    '        qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as CreditAccount,TSPL_PURCHASE_ACCOUNTS.WIP_Account as DebitAccount,FE.Cost as Avg_Cost from ( " &
    '                          " select TSPL_RCDF_STD_ADD_REMOVE.Item_Code,((case when TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' then  TSPL_RCDF_STD_ADD_REMOVE.Fat_Amt  else -TSPL_RCDF_STD_ADD_REMOVE.Fat_Amt end) " &
    '                          " +(case when TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' then  TSPL_RCDF_STD_ADD_REMOVE.SNF_Amt  else -TSPL_RCDF_STD_ADD_REMOVE.SNF_Amt end)) as Cost from TSPL_RCDF_STD_ADD_REMOVE " &
    '                          " inner join TSPL_RCDF_STD on TSPL_RCDF_STD_ADD_REMOVE.Doc_Code=TSPL_RCDF_STD.Doc_Code " &
    '                          " where TSPL_RCDF_STD.Doc_Code='" & obj.Doc_Code & "' ) as FE " &
    '                          " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
    '                          " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            '' check for account setting  exist or not
    '            If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
    '                Throw New Exception("Inventory Control Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '            End If
    '            If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '                Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '            End If
    '            Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(CreditAcc) > 0 Then
    '                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If

    '            Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(DebitAcc) > 0 Then
    '                Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If
    '            TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '            TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        Next

    '        ' '' credit wip account of overhead cost
    '        'qry = " select Cost.COST_CODE,Cost.OverHead_Cost as Avg_Cost,TSPL_OVERHEAD_COST.GL_Acc as CreditAccount from TSPL_PP_COST_WITHOUT_BATCH Cost " & _
    '        '      " left join TSPL_OVERHEAD_COST on Cost.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE " & _
    '        '      " WHERE Cost.PROD_ENTRY_CODE='" & obj.Doc_Code & "'"

    '        'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        'For Each grow As DataRow In dtGL.Rows
    '        '    '' check for account setting  exist or not
    '        '    If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '        '        Throw New Exception("GL Account not found for Cost Code " & grow.Item("COST_CODE") & "")
    '        '    End If
    '        '    Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '        '    If clsCommon.myLen(CreditAcc) > 0 Then
    '        '        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '        '        ArryLstGLAC.Add(Acc2)
    '        '    End If

    '        '    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        'Next

    '        ' '' credit wip account of production items
    '        'qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,PED.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount from TSPL_RCDF_STD_PRODUCE PED " & _
    '        '      " left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
    '        '      " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
    '        '      " WHERE PED.Doc_Code='" & obj.Doc_Code & "'"

    '        'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        'For Each grow As DataRow In dtGL.Rows
    '        '    '' check for account setting  exist or not
    '        '    If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
    '        '        Throw New Exception("Inventory Control account not found for Item Code " & grow.Item("ITEM_CODE") & "")
    '        '    End If
    '        '    Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Location_Code, trans)
    '        '    If clsCommon.myLen(DebitAcc) > 0 Then
    '        '        Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '        '        ArryLstGLAC.Add(Acc2)
    '        '    End If

    '        '    TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
    '        'Next

    '        Dim GLDesc As String = "Journal Entry Against Production Standardization- Doc No." & obj.Doc_Code & " "

    '        Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-ST' and Source_Doc_No='" & obj.Doc_Code & "'", trans))
    '        If clsCommon.myLen(VoucherNo) > 0 Then
    '            isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, VoucherNo, trans, obj.Doc_Date, GLDesc, "PR-ST", "Production Standardization", obj.Doc_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
    '        Else
    '            isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Doc_Date, GLDesc, "PR-ST", "Production Standardization", obj.Doc_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
    '        End If

    '        Return isSaved
    '    Catch ex As Exception

    '        Throw New Exception(ex.Message)
    '        Return False
    '    End Try
    'End Function
    'Public Shared Function JournalEntrySFGProduction(ByVal Doc_Code As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
    '    Try
    '        Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(Doc_Code, "", NavigatorType.Current, trans)
    '        Dim VoucherNo As String = ""
    '        If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
    '            VoucherNo = strVourcherNoForRecreateOnly
    '        Else
    '            VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-ER' and Source_Doc_No='" & obj.Doc_Code & "'", trans))
    '        End If
    '        If obj.Is_Job_Work_Inward Then
    '            If clsCommon.myLen(VoucherNo) > 0 Then
    '                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "' ", trans)
    '                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "' ", trans)
    '            End If
    '            Return True  ''Journal Entry will not create is job work type.
    '        End If

    '        Dim Count As Integer = 0
    '        Dim qry As String
    '        Dim dtGL As DataTable

    '        Dim ArryLstGLAC As ArrayList = New ArrayList()
    '        Dim VoucherDesc As String = "Financial Entry for Production Standardization -" & obj.Doc_Code & " "
    '        Dim SourceDocDesc As String = "Production Standardization in Bulk"
    '        Dim SourceDocNo As String = obj.Doc_Code
    '        Dim Comments As String = VoucherDesc
    '        Dim Remarks As String = VoucherDesc
    '        Dim i As Integer = 0
    '        qry = " SELECT Consm.CONSM_ITEM_CODE,Consm.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account AS CreditAccount " &
    '              " FROM TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL  Consm " &
    '              " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
    '              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
    '              " WHERE Consm.Doc_Code='" & obj.Doc_Code & "'"
    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '                Throw New Exception("Inventory Control Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '            End If
    '            Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(CreditAcc) > 0 Then
    '                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
    '                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                    ArryLstGLAC.Add(Acc2)
    '                Else
    '                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost")), "", "", "", "", "", "", "I"}
    '                    ArryLstGLAC.Add(Acc2)

    '                    ''BHA/27/11/18-000724 by Balwinder on 18/01/2019
    '                    clsInventoryMovement.UpdateInvControlAccount(Doc_Code, clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(grow("CONSM_ITEM_CODE")), "", CreditAcc, "", trans)
    '                    ''------------------
    '                End If
    '            End If
    '        Next


    '        qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account as CreditAccount,FE.Cost as Avg_Cost,FE.IsConsumeItem,TSPL_PURCHASE_ACCOUNTS.WIP_Account from ( " &
    '              " select TSPL_RCDF_STD_ADD_REMOVE.Item_Code, " &
    '              " TSPL_RCDF_STD_ADD_REMOVE.Total_Amount*(case when TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' then 1 else -1 end) as Cost  " &
    '              " ,(select 1 from TSPL_RCDF_STD_ISSUE where TSPL_RCDF_STD_ISSUE.Doc_Code=TSPL_RCDF_STD.Doc_Code and TSPL_RCDF_STD_ISSUE.Item_Code=TSPL_RCDF_STD_ADD_REMOVE.Item_Code and TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE='Add' ) as IsConsumeItem " + Environment.NewLine +
    '              "  from TSPL_RCDF_STD_ADD_REMOVE  " &
    '              " inner join   TSPL_RCDF_STD on TSPL_RCDF_STD.Doc_Code=TSPL_RCDF_STD_ADD_REMOVE.Doc_Code " &
    '              " where TSPL_RCDF_STD.Doc_Code='" & obj.Doc_Code & "'  " +
    '              " ) as FE  " &
    '              " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
    '              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            '' check for account setting  exist or not
    '            If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
    '                Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '            End If
    '            Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(CreditAcc) > 0 Then
    '                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If
    '            If clsCommon.myCdbl(grow("IsConsumeItem")) = 1 Then
    '                If clsCommon.myLen(grow.Item("WIP_Account")) <= 0 Then
    '                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
    '                End If
    '                CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("WIP_Account")), obj.Location_Code, trans)

    '                Dim Acc2() As String = {CreditAcc, clsCommon.myCdbl(grow("Avg_Cost"))}
    '                ArryLstGLAC.Add(Acc2)
    '            End If
    '        Next


    '        qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount,PED.Avg_Cost " &
    '              " from TSPL_RCDF_STD_PRODUCE PED     left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
    '              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
    '              " WHERE PED.Doc_Code='" & obj.Doc_Code & "'"
    '        dtGL = clsDBFuncationality.GetDataTable(qry, trans)
    '        For Each grow As DataRow In dtGL.Rows
    '            If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
    '                Throw New Exception("Inventory Control account not found for Item Code " & grow.Item("ITEM_CODE") & "")
    '            End If
    '            Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Location_Code, trans)
    '            If clsCommon.myLen(DebitAcc) > 0 Then
    '                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
    '                    Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
    '                    ArryLstGLAC.Add(Acc2)
    '                Else
    '                    Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost")), "", "", "", "", "", "", "I"}
    '                    ArryLstGLAC.Add(Acc2)

    '                    ''BHA/27/11/18-000724 by Balwinder on 18/01/2019
    '                    clsInventoryMovement.UpdateInvControlAccount(Doc_Code, clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(grow("ITEM_CODE")), DebitAcc, "", "", trans)
    '                    ''------------------
    '                End If
    '            End If
    '        Next

    '        Dim GLDesc As String = "Journal Entry Against Production Standardization- Doc No." & obj.Doc_Code & " "
    '        If clsCommon.myLen(VoucherNo) > 0 Then
    '            transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, VoucherNo, trans, obj.Doc_Date, GLDesc, "PR-ER", "Production Standardization", obj.Doc_Code, Comments, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
    '        Else
    '            transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Doc_Date, GLDesc, "PR-ER", "Production Standardization", obj.Doc_Code, Comments, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    'Public Shared Function UpdateConsumption(ByVal Doc_Code As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
    '    Try
    '        UpdateConsumption(Doc_Code, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    'Public Shared Function UpdateConsumption(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Boolean
    '    If clsCommon.myLen(Doc_Code) <= 0 Then
    '        Throw New Exception("Document No can not be blank")
    '    End If
    '    Dim objRec As clsRCDFStanardization
    '    objRec = clsRCDFStanardization.GetData(Doc_Code, "", NavigatorType.Current, trans)
    '    If objRec Is Nothing Then
    '        Throw New Exception("Document not found")
    '    End If
    '    If clsCommon.myLen(objRec.Doc_Code) <= 0 Then
    '        Throw New Exception("Document not found")
    '    End If
    '    Dim isSaved As Boolean = True
    '    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
    '    Try
    '        '' query for consumption on batch order bom basis
    '        Dim qry As String = "delete from TSPL_INVENTORY_MOVEMENT_NEW where SOURCE_DOC_NO='" & objRec.Doc_Code & "' and IS_CONSUMPTION=1"
    '        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_INVENTORY_MOVEMENT where SOURCE_DOC_NO='" & objRec.Doc_Code & "' and IS_CONSUMPTION=1"
    '        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE='" & objRec.Doc_Code & "'"
    '        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        '' update consumption

    '        isSaved = isSaved AndAlso clsStandardizationRM.SaveRM(objRec.Doc_Code, Nothing, trans)
    '        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
    '        Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
    '        isSaved = isSaved AndAlso clsRCDFStanardization.UpdateInventoryMovementConsumption("PP_STDN", ArrInventoryMovement, ArrInventoryMovementNew, objRec, Nothing, trans)

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    'Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
    '    '' created by Panch Raj against ticket No- KDI/21/05/18-000324 on date 31-05-2018
    '    Dim qry As String = ""
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        '' table list 
    '        ''1. TSPL_RCDF_STD
    '        ''2. TSPL_RCDF_STD_PRODUCE
    '        ''3. TSPL_RCDF_STD_ISSUE
    '        ''4. TSPL_RCDF_STD_ADD_REMOVE
    '        ''5. TSPL_PP_STD_QC_DETAIL
    '        ''6. TSPL_PP_STD_QC_LOG_SHEET
    '        ''7. TSPL_PP_STD_STAGE_DETAIL
    '        ''8. TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL
    '        ''9. TSPL_CUSTOM_FIELD_VALUES
    '        ''10. TSPL_INVENTORY_MOVEMENT_NEW ( no need of history)
    '        ''11. TSPL_INVENTORY_MOVEMENT     ( no need of history)
    '        ''12. TSPL_JOURNAL_DETAILS
    '        ''13. TSPL_JOURNAL_MASTER
    '        '' steps for checking the items stock and batch wise stock
    '        Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(Doc_No, "", NavigatorType.Current, trans)
    '        If obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0 Then
    '            Throw New Exception("Document- " & Doc_No & " not found")
    '        End If

    '        qry = "select QC_Code from TSPL_PP_STD_FINALQC_HEAD where Against_STD_Code='" + Doc_No + "'"
    '        Dim strFutureDoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    '        If clsCommon.myLen(strFutureDoc) > 0 Then
    '            Throw New Exception("Cannot cancel document [" + strFutureDoc + "].It is used in Std. Final QC.")
    '        End If

    '        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredFinalQCofstandardization, clsFixedParameterCode.RequiredFinalQCofstandardization, trans)) = 0) Then
    '            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
    '        End If

    '        '' transfer data into cancel table

    '        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_RCDF_STD", "Doc_Code", "TSPL_RCDF_STD_PRODUCE", "Doc_Code", trans)
    '        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_RCDF_STD_ISSUE", "Doc_Code", "TSPL_RCDF_STD_ADD_REMOVE", "Doc_Code", trans)
    '        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STD_QC_DETAIL", "Doc_Code", "TSPL_PP_STD_QC_LOG_SHEET", "Doc_Code", trans)
    '        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STD_STAGE_DETAIL", "Doc_Code", "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL", "Doc_Code", trans)

    '        qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
    '        Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    '        If clsCommon.myLen(Voucher_No) > 0 Then
    '            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
    '        End If


    '        '' cancel custom field data
    '        clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)
    '        '' release issue involved in standardization process
    '        qry = "update TSPL_RCDF_STD_ISSUE set Doc_Code=null where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        clsBatchInventory.DeleteData(clsUserMgtCode.frmProcessProductionStandardization, Doc_No, trans)
    '        clsBatchInventoryNew.DeleteData(clsUserMgtCode.frmProcessProductionStandardization, Doc_No, trans)

    '        qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "')"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_RCDF_STD_PRODUCE where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_RCDF_STD_ISSUE where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_RCDF_STD_ADD_REMOVE where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_PP_STD_QC_DETAIL where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_PP_STD_QC_LOG_SHEET where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_PP_STD_STAGE_DETAIL where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where Doc_Code='" & Doc_No & "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        qry = "delete from TSPL_RCDF_STD where Doc_Code='" & Doc_No & "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '        trans.Commit()
    '        '' release objects 
    '        obj = Nothing
    '        qry = Nothing

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

End Class

Public Class clsRCDFStanardizationProduce
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public SNO As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing ''not a table field
    Public BOM_Code As String = Nothing
    Public BOM_Desc As String = Nothing ''not a table field
    Public Unit_Code As String = Nothing
    Public Product_Type As String ''not a table field
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing  ''not a table field


#End Region

    Public Shared Function SaveData(ByVal Doc_Code As String, ByVal obj As clsRCDFStanardization, ByVal arr As List(Of clsRCDFStanardizationProduce), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_RCDF_STD_PRODUCE where and Doc_Code='" + Doc_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsRCDFStanardizationProduce In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", Doc_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.BOM_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "FAT", objtr.FAT)
                    clsCommon.AddColumnsForChange(coll, "SNF", objtr.SNF)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_STD_PRODUCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As List(Of clsRCDFStanardizationProduce)
        Dim objIssueList As New List(Of clsRCDFStanardizationProduce)
        Dim qry As String = "select TSPL_RCDF_STD_PRODUCE.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.Product_Type from TSPL_RCDF_STD_PRODUCE " &
        " left join TSPL_ITEM_MASTER on TSPL_RCDF_STD_PRODUCE.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " &
        " left join TSPL_PP_BOM_HEAD on TSPL_RCDF_STD_PRODUCE.BOM_Code=TSPL_PP_BOM_HEAD.BOM_CODE " &
        " left join TSPL_LOCATION_MASTER on TSPL_RCDF_STD_PRODUCE.In_LocationCode=TSPL_LOCATION_MASTER.Location_Code " &
        " where TSPL_RCDF_STD_PRODUCE.and Doc_Code='" + Doc_Code + "' order by TSPL_RCDF_STD_PRODUCE.PK_ID"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim sno As Integer = 0
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsRCDFStanardizationProduce()
                sno += 1
                objtr.SNO = sno
                objtr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                objtr.BOM_Desc = clsCommon.myCstr(dr("BOM_Desc"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                objIssueList.Add(objtr)
            Next
        End If
        Return objIssueList
    End Function

End Class
Public Class clsRCDFStanardizationIssue
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public SNO As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing ''not a table field
    Public Product_Type As String ''not a table field
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing ''not a table field
    Public Unit_Code As String = Nothing
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF As Decimal = 0
    Public SNF_KG As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal Doc_Code As String, ByVal obj As clsRCDFStanardization, ByVal arr As List(Of clsRCDFStanardizationIssue), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_RCDF_STD_ISSUE where and Doc_Code='" + Doc_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsRCDFStanardizationIssue In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", Doc_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "FAT", objtr.FAT)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF", objtr.SNF)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.SNF_KG)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_STD_ISSUE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As List(Of clsRCDFStanardizationIssue)
        Dim objIssueList As New List(Of clsRCDFStanardizationIssue)
        Dim qry As String = "select TSPL_RCDF_STD_ISSUE.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.Product_Type from TSPL_RCDF_STD_ISSUE " &
        " left join TSPL_ITEM_MASTER on TSPL_RCDF_STD_ISSUE.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " &
        " left join TSPL_LOCATION_MASTER on TSPL_RCDF_STD_ISSUE.Location_Code=TSPL_LOCATION_MASTER.Location_Code " &
        " where TSPL_RCDF_STD_ISSUE.and Doc_Code='" + Doc_Code + "' order by TSPL_RCDF_STD_ISSUE.PK_ID"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim sno As Integer = 0
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsRCDFStanardizationIssue()
                sno += 1
                objtr.SNO = sno

                objtr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                objIssueList.Add(objtr)
            Next
        End If
        Return objIssueList
    End Function

End Class
Public Class clsRCDFStanardizationAddRemove
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public SNO As Integer = 0
    Public ADD_REMOVE_TYPE As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing ''not a table field
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing ''not a table field
    Public Unit_Code As String = Nothing
    Public Product_Type As String ''not a table field
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public Remarks As String = Nothing




#End Region

    Public Shared Function SaveData(ByVal objStd As clsRCDFStanardization, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_RCDF_STD_ADD_REMOVE where and Doc_Code='" + objStd.Doc_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            If objStd.ArrARItem IsNot Nothing AndAlso objStd.ArrARItem.Count > 0 Then
                For Each objtr As clsRCDFStanardizationAddRemove In objStd.ArrARItem
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", objStd.Doc_Code)
                    clsCommon.AddColumnsForChange(coll, "ADD_REMOVE_TYPE", objtr.ADD_REMOVE_TYPE)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "FAT", objtr.FAT)
                    clsCommon.AddColumnsForChange(coll, "SNF", objtr.SNF)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_STD_ADD_REMOVE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function



    Public Shared Function GetData(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As List(Of clsRCDFStanardizationAddRemove)
        Dim objARList As New List(Of clsRCDFStanardizationAddRemove)
        Dim qry As String = "select TSPL_RCDF_STD_ADD_REMOVE.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.Product_Type,TSPL_LOCATION_MASTER.Location_Desc from TSPL_RCDF_STD_ADD_REMOVE " &
        " left join TSPL_ITEM_MASTER on TSPL_RCDF_STD_ADD_REMOVE.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " &
        " left join TSPL_LOCATION_MASTER on TSPL_RCDF_STD_ADD_REMOVE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  " &
        " where TSPL_RCDF_STD_ADD_REMOVE.and Doc_Code='" + Doc_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim sno As Integer = 0
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsRCDFStanardizationAddRemove()
                sno += 1
                objtr.SNO = sno

                objtr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                objtr.ADD_REMOVE_TYPE = clsCommon.myCstr(dr("ADD_REMOVE_TYPE"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                objARList.Add(objtr)
            Next
        End If
        Return objARList
    End Function
End Class


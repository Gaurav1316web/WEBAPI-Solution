'-27/08/2012--Updation by--Pankaj Kumar---Applied GL Security while navigationg Document Finder----Fwd By---Ranjana mam
Imports common
Imports System.Data.SqlClient
Public Class clsRequistionHead
#Region "Variables"

    Public Emergency As Integer = Nothing
    Public Is_Open_PO As Integer = Nothing
    Public Requisition_Id As String = Nothing
    Public Requisition_Date As DateTime = Nothing
    Public Cust_OrderNo As String = Nothing
    Public Expire_Date As String = Nothing
    Public Require_Date As String = Nothing
    Public close_yn As String
    Public Status As ERPTransactionStatus = 0
    Public On_Hold As Integer = 0
    Public Manual_Complete As String = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Location As String = Nothing
    Public LocationName As String = Nothing 'Not a table field
    Public RQ_Detail_Total_Amt As Double = 0
    Public Total_RQ_Amt As Double = 0
    Public Mode_Of_Transport As String = Nothing
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Posting_Date As DateTime?
    Public Is_Internal As String = "N"
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public unit As String = Nothing
    Public unit_Desc As String = Nothing
    Public Cost As String = Nothing
    Public Cost_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Request_By As String = Nothing
    Public Requisition_Type As String = Nothing

    Public Level1_Approval_Status As Integer = 0
    Public Level2_Approval_Status As Integer = 0
    Public Level3_Approval_Status As Integer = 0

    Public Level4_Approval_Status As Integer = 0
    Public Level5_Approval_Status As Integer = 0
    Public SubRequest As String = Nothing
    Public Request_Type As Integer = 0
    Public Approvel_Level_Required As Integer = 0
    Public PROJECT_ID As String = Nothing
    Public Category As String = Nothing
    Public Capex_Code As String = Nothing
    Public Capex_SubCode As String = Nothing
    Public Approval_Date As DateTime = Nothing

    Public ArrTr As List(Of clsRequistionDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public ItemReorder As String = Nothing
    Public CosCenter_Unit As String = Nothing
    Public CostCenter_Type As String = Nothing
    Public SubCapex_Amount As Integer = 0
    Public SubCapex_AmountWithTol As Integer = 0
    Public SubCapex_BalAmount As Integer = 0
    Public SubCapex_BalAmountWithTol As Integer = 0
    Public Is_Tender As String = "N"
    Public EmailID As String = Nothing
    Public WO_To As String = Nothing
    Public WO_Subject As String = Nothing
    Public WO_Content As String = Nothing
    Public WO_CopySubmittedTo As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsRequistionHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsRequistionHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Requisition", obj.Location, obj.Requisition_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Store Requisition", obj.Location, obj.Requisition_Date, trans)
            '-----------------------------------------------
            If Not isNewEntry AndAlso objCommonVar.IsDemoERP Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Level1_Approval_Status, Level2_Approval_Status, Level3_Approval_Status,Level4_Approval_Status,Level5_Approval_Status from TSPL_REQUISITION_HEAD WHERE Requisition_Id ='" + obj.Requisition_Id + "'", trans)
                If dt.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dt.Rows(0)("Level1_Approval_Status")) = 1 Then
                        Throw New Exception("This Document is already posted by Budgetory User")
                    ElseIf clsCommon.myCdbl(dt.Rows(0)("Level2_Approval_Status")) = 1 Then
                        Throw New Exception("This Document is already posted by Vertical head User")
                    ElseIf clsCommon.myCdbl(dt.Rows(0)("Level3_Approval_Status")) = 1 Then
                        Throw New Exception("This Document is already posted by Finance Level 1 User")
                    ElseIf clsCommon.myCdbl(dt.Rows(0)("Level4_Approval_Status")) = 1 Then
                        Throw New Exception("This Document is already posted by Finance Level 2 User")
                    ElseIf clsCommon.myCdbl(dt.Rows(0)("Level5_Approval_Status")) = 1 Then
                        Throw New Exception("This Document is already posted by Finance Level 3 User")
                    End If
                End If
            End If
            '-----------------------------------------------
            Dim qry As String = "delete from TSPL_REQUISITION_DETAIL where Requisition_Id='" + obj.Requisition_Id + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If (isNewEntry) Then
                'If clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                '    obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, clsDocType.PurchaserRegusitsion, clsDocTransactionType.PORawMaterial, obj.Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, clsDocType.PurchaserRegusitsion, clsDocTransactionType.POFinishedGoods, obj.Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal Then
                '    obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, clsDocType.PurchaserRegusitsion, clsDocTransactionType.POPromotionalItem, obj.Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                '    obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, clsDocType.PurchaserRegusitsion, clsDocTransactionType.POOther, obj.Location)
                'Else
                '    Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                'End If

                'Separate Prefix for Asset Store Requistion and Store Requirtion  Ticket No- UDL/07/05/18-000155 
                If obj.Form_ID = "FA-REQUI" Then
                    obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.AssetRequisition, "", obj.Location)
                ElseIf obj.Form_ID = "WRE-T" Then
                    obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.WorkRequisitionEng, clsDocTransactionType.OtherExternal, obj.Location)
                Else
                    If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "Y") = CompairStringResult.Equal Then
                        obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.frmStoreRequistion, clsDocTransactionType.FinishedGoodInternal, obj.Location)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "N") = CompairStringResult.Equal Then
                        obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.PurchaserRegusitsion, clsDocTransactionType.FinishedGoodExternal, obj.Location)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "Y") = CompairStringResult.Equal Then
                        obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.frmStoreRequistion, clsDocTransactionType.SemiFinishedGoodInternal, obj.Location)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Internal, "N") = CompairStringResult.Equal Then
                        obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.PurchaserRegusitsion, clsDocTransactionType.SemiFinishedGoodExternal, obj.Location)
                    ElseIf (clsCommon.CompairString(obj.Item_Type, "O") Or clsCommon.CompairString(obj.Item_Type, "R") Or clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal) And clsCommon.CompairString(obj.Is_Internal, "Y") = CompairStringResult.Equal Then
                        obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.frmStoreRequistion, clsDocTransactionType.OtherInternal, obj.Location)
                    ElseIf (clsCommon.CompairString(obj.Item_Type, "O") Or clsCommon.CompairString(obj.Item_Type, "R") Or clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal) And clsCommon.CompairString(obj.Is_Internal, "N") = CompairStringResult.Equal Then
                        obj.Requisition_Id = clsERPFuncationality.GetNextCode(trans, obj.Requisition_Date, clsDocType.PurchaserRegusitsion, clsDocTransactionType.OtherExternal, obj.Location)
                    Else
                        Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                    End If
                End If
                If (clsCommon.myLen(obj.Requisition_Id) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Requisition_Date", clsCommon.GetPrintDate(obj.Requisition_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Cust_OrderNo", obj.Cust_OrderNo)

            If clsCommon.myLen(obj.Expire_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Expire_Date", clsCommon.GetPrintDate(obj.Expire_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Expire_Date", Nothing, True)
            End If
            If clsCommon.myLen(obj.Require_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Require_Date", clsCommon.GetPrintDate(obj.Require_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Require_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "On_Hold", obj.On_Hold)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "RQ_Detail_Total_Amt", obj.RQ_Detail_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_RQ_Amt", obj.Total_RQ_Amt)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Is_Internal", obj.Is_Internal)
            clsCommon.AddColumnsForChange(coll, "Is_Tender", obj.Is_Tender)
            clsCommon.AddColumnsForChange(coll, "EMailID", clsCommon.myCstr(obj.EmailID), True)
            clsCommon.AddColumnsForChange(coll, "Requisition_Type", obj.Requisition_Type)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept, True)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.Request_By)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "Is_Open_PO", obj.Is_Open_PO)
            clsCommon.AddColumnsForChange(coll, "close_yn", obj.close_yn)
            clsCommon.AddColumnsForChange(coll, "Category", clsCommon.myCstr(obj.Category))
            clsCommon.AddColumnsForChange(coll, "Capex_Code", clsCommon.myCstr(obj.Capex_Code), True)
            clsCommon.AddColumnsForChange(coll, "Capex_SubCode", clsCommon.myCstr(obj.Capex_SubCode), True)
            clsCommon.AddColumnsForChange(coll, "Cost_Center_Unit", obj.CosCenter_Unit, True)
            clsCommon.AddColumnsForChange(coll, "Cost_Center_Type", obj.CostCenter_Type, True)
            clsCommon.AddColumnsForChange(coll, "Emergency", CInt(obj.Emergency))

            clsCommon.AddColumnsForChange(coll, "SubCapex_Amount", clsCommon.myCdbl(obj.SubCapex_Amount))
            clsCommon.AddColumnsForChange(coll, "SubCapex_AmountWithTol", clsCommon.myCdbl(obj.SubCapex_AmountWithTol))
            clsCommon.AddColumnsForChange(coll, "SubCapex_BalAmount", clsCommon.myCdbl(obj.SubCapex_BalAmount))
            clsCommon.AddColumnsForChange(coll, "SubCapex_BalAmountWithTol", clsCommon.myCdbl(obj.SubCapex_BalAmountWithTol))
            clsCommon.AddColumnsForChange(coll, "From_Screen_Code", clsCommon.myCstr(obj.Form_ID))
            clsCommon.AddColumnsForChange(coll, "WO_To", clsCommon.myCstr(obj.WO_To), True)
            clsCommon.AddColumnsForChange(coll, "WO_Subject", clsCommon.myCstr(obj.WO_Subject), True)
            clsCommon.AddColumnsForChange(coll, "WO_Content", clsCommon.myCstr(obj.WO_Content), True)
            clsCommon.AddColumnsForChange(coll, "WO_CopySubmittedTo ", clsCommon.myCstr(obj.WO_CopySubmittedTo), True)
            If objCommonVar.IsDemoERP Then
                clsCommon.AddColumnsForChange(coll, "Approvel_Level_Required", 2 + obj.Approvel_Level_Required) ''2 is Added for Budgetry and Function Approval

            End If


            Dim s As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_HEAD", OMInsertOrUpdate.Update, "Requisition_Id='" + obj.Requisition_Id + "'", trans)
            End If
            isSaved = isSaved AndAlso clsRequistionDetail.SaveData(obj.Requisition_Id, obj.ArrTr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Requisition_Id, obj.arrCustomFields, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Requisition_Id), "TSPL_REQUISITION_HEAD", "Requisition_Id", "TSPL_REQUISITION_DETAIL", "Requisition_Id", trans)


            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseRequistion + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "S") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(obj.Description), "Auto Indent") = CompairStringResult.Equal Then
                    Dim objnotify As New clsNotificationReplace
                    objnotify.DocNo = obj.Requisition_Id
                    objnotify.DocDate = obj.Requisition_Date
                    objnotify.DocAmt = obj.Total_RQ_Amt
                    clsNotificationHead.SendNotification(clsUserMgtCode.mbtnPurchaseRequistion, objnotify, "S", trans)
                End If
            End If
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal IsInternal As String) As clsRequistionHead
        Return GetData(strCode, NavType, Nothing, IsInternal)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction, ByVal IsInternal As String) As clsRequistionHead
        Return GetData(strCode, NavType, trans, IsInternal, "")
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction, ByVal IsInternal As String, ByVal FORMTYPE As String) As clsRequistionHead
        Dim obj As clsRequistionHead = Nothing
        Dim whrclas As String = ""
        Dim qry As String = "select TSPL_REQUISITION_HEAD.close_yn,TSPL_REQUISITION_HEAD.Requisition_Id,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Requisition_Date,TSPL_REQUISITION_HEAD.Cust_OrderNo,TSPL_REQUISITION_HEAD.Expire_Date,TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Status,TSPL_REQUISITION_HEAD.On_Hold,TSPL_REQUISITION_HEAD.Manual_Complete,TSPL_REQUISITION_HEAD.Ref_No,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_REQUISITION_HEAD.RQ_Detail_Total_Amt,TSPL_REQUISITION_HEAD.Total_RQ_Amt,TSPL_REQUISITION_HEAD.Mode_Of_Transport,TSPL_REQUISITION_HEAD.Comments,TSPL_REQUISITION_HEAD.Created_By,TSPL_REQUISITION_HEAD.Created_Date,TSPL_REQUISITION_HEAD.Modify_By,TSPL_REQUISITION_HEAD.Modify_Date,TSPL_REQUISITION_HEAD.Comp_Code,TSPL_REQUISITION_HEAD.Posting_Date,TSPL_REQUISITION_HEAD.Dept,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Item_Type,TSPL_REQUISITION_HEAD.Request_By,Is_internal, Is_Tender,EMailID,TSPL_REQUISITION_HEAD.Approvel_Level_Required,TSPL_REQUISITION_HEAD.Level1_Approval_Status ,TSPL_REQUISITION_HEAD.Level2_Approval_Status,TSPL_REQUISITION_HEAD.SubRequest,TSPL_REQUISITION_HEAD.CatalogueType,TSPL_REQUISITION_HEAD.Vendor_Code ,TSPL_REQUISITION_HEAD.Level3_Approval_Status,TSPL_REQUISITION_HEAD.Level4_Approval_Status,TSPL_REQUISITION_HEAD.Level5_Approval_Status ,TSPL_REQUISITION_HEAD.Requisition_Type,TSPL_REQUISITION_HEAD.Request_Type,TSPL_REQUISITION_HEAD.PROJECT_ID,TSPL_REQUISITION_HEAD.Category,TSPL_REQUISITION_HEAD.Capex_Code,TSPL_REQUISITION_HEAD.Capex_SubCode,TSPL_REQUISITION_HEAD.Emergency,TSPL_REQUISITION_HEAD.Cost_Center_Unit,TSPL_REQUISITION_HEAD.Cost_Center_Type " &
        ",isnull(SubCapex_Amount,0) as SubCapex_Amount,isnull(SubCapex_AmountWithTol,0) as SubCapex_AmountWithTol,isnull(SubCapex_BalAmount,0) as SubCapex_BalAmount,isnull(SubCapex_BalAmountWithTol,0) as SubCapex_BalAmountWithTol"
        qry += ",From_Screen_Code,WO_To,WO_Subject,WO_Content,WO_CopySubmittedTo FROM TSPL_REQUISITION_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_REQUISITION_HEAD.Location where  2=2"
        If clsCommon.myLen(IsInternal) > 0 Then
            whrclas = " and Is_Internal='" & IsInternal & "' "
        End If
        If clsCommon.myLen(FORMTYPE) > 0 Then
            whrclas = " and From_Screen_Code='" & FORMTYPE & "' "
        End If
        If clsCommon.CompairString(clsCommon.myCstr(IsInternal), "Y") = CompairStringResult.Equal Then
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyDepartmentWiseDataVisibleInDepartmentIndent, clsFixedParameterCode.ApplyDepartmentWiseDataVisibleInDepartmentIndent, Nothing)) = 1, True, False) = True Then
                whrclas = " and TSPL_REQUISITION_HEAD.Dept in (select Segment_code from tspl_User_Master where User_Code = '" + objCommonVar.CurrentUserCode + "') "
            End If
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_REQUISITION_HEAD.Requisition_Id=(select MIN(Requisition_Id) from TSPL_REQUISITION_HEAD Where 1=1 " + whrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_REQUISITION_HEAD.Requisition_Id=(select Max(Requisition_Id) from TSPL_REQUISITION_HEAD Where 1=1 " + whrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_REQUISITION_HEAD.Requisition_Id=(select Min(Requisition_Id) from TSPL_REQUISITION_HEAD where Requisition_Id > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_REQUISITION_HEAD.Requisition_Id=(select Max(Requisition_Id) from TSPL_REQUISITION_HEAD where Requisition_Id < '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_REQUISITION_HEAD.Requisition_Id='" + strCode + "'  "
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRequistionHead()
            obj.Requisition_Id = clsCommon.myCstr(dt.Rows(0)("Requisition_Id"))
            obj.Requisition_Date = clsCommon.myCDate(dt.Rows(0)("Requisition_Date"))
            obj.Cust_OrderNo = clsCommon.myCstr(dt.Rows(0)("Cust_OrderNo"))
            'obj.Is_Open_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("is_open_po")))
            obj.close_yn = clsCommon.myCstr(dt.Rows(0)("close_yn"))

            If dt.Rows(0)("Expire_Date") Is DBNull.Value Then
                obj.Expire_Date = Nothing
            Else
                obj.Expire_Date = clsCommon.myCDate(dt.Rows(0)("Expire_Date"))
            End If
            '' Anubhooti 11-Oct-2014 BM00000004220 (Expiry Date not showing)
            If dt.Rows(0)("Require_Date") Is DBNull.Value Then
                obj.Require_Date = Nothing
            Else
                obj.Require_Date = clsCommon.myCDate(dt.Rows(0)("Require_Date"))
            End If
            If (clsCommon.myCdbl(dt.Rows(0)("Status")) = 0) Then
                obj.Status = ERPTransactionStatus.Pending
            Else
                obj.Status = ERPTransactionStatus.Approved
            End If
            obj.On_Hold = clsCommon.myCdbl(dt.Rows(0)("On_Hold"))
            obj.Manual_Complete = clsCommon.myCstr(dt.Rows(0)("Manual_Complete"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))

            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("LocationName"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.RQ_Detail_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("RQ_Detail_Total_Amt"))
            obj.Total_RQ_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_RQ_Amt"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Cust_OrderNo = clsCommon.myCstr(dt.Rows(0)("Cust_OrderNo"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Request_By = clsCommon.myCstr(dt.Rows(0)("Request_By"))
            obj.Is_Internal = clsCommon.myCstr(dt.Rows(0)("Is_Internal"))
            obj.Is_Tender = clsCommon.myCstr(dt.Rows(0)("Is_Tender"))
            obj.EmailID = clsCommon.myCstr(dt.Rows(0)("EMailID"))
            obj.Requisition_Type = clsCommon.myCstr(dt.Rows(0)("Requisition_Type"))

            obj.SubRequest = clsCommon.myCstr(dt.Rows(0)("SubRequest"))
            obj.Level1_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level1_Approval_Status"))
            obj.Level2_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level2_Approval_Status"))
            obj.Level3_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level3_Approval_Status"))
            obj.Level4_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level4_Approval_Status"))
            obj.Level5_Approval_Status = clsCommon.myCdbl(dt.Rows(0)("Level5_Approval_Status"))

            obj.Approvel_Level_Required = clsCommon.myCdbl(dt.Rows(0)("Approvel_Level_Required"))
            obj.Request_Type = clsCommon.myCdbl(dt.Rows(0)("Request_Type"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            obj.Category = clsCommon.myCstr(dt.Rows(0)("Category"))
            obj.Capex_Code = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
            obj.Capex_SubCode = clsCommon.myCstr(dt.Rows(0)("Capex_SubCode"))
            obj.Emergency = CInt(clsCommon.myCstr(dt.Rows(0)("Emergency")))
            obj.CosCenter_Unit = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Unit"))
            obj.CostCenter_Type = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Type"))

            obj.SubCapex_Amount = clsCommon.myCdbl(dt.Rows(0)("SubCapex_Amount"))
            obj.SubCapex_AmountWithTol = clsCommon.myCdbl(dt.Rows(0)("SubCapex_AmountWithTol"))
            obj.SubCapex_BalAmount = clsCommon.myCdbl(dt.Rows(0)("SubCapex_BalAmount"))
            obj.SubCapex_BalAmountWithTol = clsCommon.myCdbl(dt.Rows(0)("SubCapex_BalAmountWithTol"))

            obj.Form_ID = clsCommon.myCstr(dt.Rows(0)("From_Screen_Code"))
            obj.WO_To = clsCommon.myCstr(dt.Rows(0)("WO_To"))
            obj.WO_Subject = clsCommon.myCstr(dt.Rows(0)("WO_Subject"))
            obj.WO_Content = clsCommon.myCstr(dt.Rows(0)("WO_Content"))
            obj.WO_CopySubmittedTo = clsCommon.myCstr(dt.Rows(0)("WO_CopySubmittedTo"))

            qry = "SELECT TSPL_REQUISITION_DETAIL.Hirerachy_Code,TSPL_REQUISITION_DETAIL.Cost_Centre_Code,TSPL_REQUISITION_DETAIL.Requisition_Id,TSPL_REQUISITION_DETAIL.Line_No,TSPL_REQUISITION_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name as VendorName, TSPL_REQUISITION_DETAIL.Item_Code,TSPL_REQUISITION_DETAIL.Item_Desc,TSPL_REQUISITION_DETAIL.Requisition_Qty,TSPL_REQUISITION_DETAIL.Balance_Qty,TSPL_REQUISITION_DETAIL.Unit_Code,TSPL_REQUISITION_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc as LocationName ,TSPL_REQUISITION_DETAIL.Item_Cost,TSPL_REQUISITION_DETAIL.Item_Net_Amt,TSPL_REQUISITION_DETAIL.Status,TSPL_REQUISITION_DETAIL.Order_No,TSPL_REQUISITION_DETAIL.Vendor_ItemNo,TSPL_REQUISITION_DETAIL.Specification,TSPL_REQUISITION_DETAIL.Remarks,TSPL_REQUISITION_DETAIL.Row_Type,TSPL_REQUISITION_DETAIL.Capacity,TSPL_REQUISITION_DETAIL.Make,TSPL_REQUISITION_DETAIL.Cost_Code,TSPL_REQUISITION_DETAIL.Model,TSPL_REQUISITION_DETAIL.Hirerachy_Level3,TSPL_REQUISITION_DETAIL.Hirerachy_Level4 FROM TSPL_REQUISITION_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_REQUISITION_DETAIL.Location left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_REQUISITION_DETAIL.Vendor_Code where TSPL_REQUISITION_DETAIL.Requisition_Id='" + obj.Requisition_Id + "' ORDER BY TSPL_REQUISITION_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrTr = New List(Of clsRequistionDetail)
                Dim objTr As clsRequistionDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsRequistionDetail()
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Requisition_Id = clsCommon.myCstr(dr("Requisition_Id"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objTr.VendorName = clsCommon.myCstr(dr("VendorName"))
                    objTr.Requisition_Qty = clsCommon.myCdbl(dr("Requisition_Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.LocationName = clsCommon.myCstr(dr("LocationName"))
                    objTr.Item_Cost = clsCommon.myCstr(dr("Item_Cost"))
                    objTr.Status = clsCommon.myCstr(dr("Status"))
                    objTr.Order_No = clsCommon.myCstr(dr("Order_No"))
                    objTr.Vendor_ItemNo = clsCommon.myCstr(dr("Vendor_ItemNo"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))

                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Capacity = clsCommon.myCstr(dr("Capacity"))
                    objTr.Make = clsCommon.myCstr(dr("Make"))
                    objTr.Model = clsCommon.myCstr(dr("Model"))
                    objTr.CostCode = clsCommon.myCstr(dr("Cost_Code"))
                    ''richa
                    objTr.Hirerachy_Code = clsCommon.myCstr(dr("Hirerachy_Code"))
                    objTr.Cost_Centre_Code = clsCommon.myCstr(dr("Cost_Centre_Code"))
                    objTr.HirerachyLevelCode3 = clsCommon.myCstr(dr("Hirerachy_Level3"))
                    objTr.HirerachyLevelCode4 = clsCommon.myCstr(dr("Hirerachy_Level4"))
                    obj.ArrTr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function
    Public Shared Function CloseprData(ByVal strDocNo As String, ByVal cls As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'If (clsCommon.myLen(strDocNo) <= 0) Then
            '    Throw New Exception("Requistion No not found to close")
            'End If
            'Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            'Dim obj As clsRequistionHead = clsRequistionHead.GetData(strDocNo, NavigatorType.Current, trans, "")
            'Dim totDrAmt As Double = 0
            'Dim totCrAmt As Double = 0

            'If (obj Is Nothing OrElse clsCommon.myLen(obj.Requisition_Id) <= 0) Then
            '    Throw New Exception("No Data found to close")
            'End If
            'Dim qry As String
            'qry = "Update TSPL_REQUISITION_HEAD set close_yn='" + cls + "' where Requisition_Id='" + strDocNo + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            CloseprData(strDocNo, cls, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CloseprData(ByVal strDocNo As String, ByVal cls As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Requistion No not found to close")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsRequistionHead = clsRequistionHead.GetData(strDocNo, NavigatorType.Current, trans, "")
            Dim totDrAmt As Double = 0
            Dim totCrAmt As Double = 0

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Requisition_Id) <= 0) Then
                Throw New Exception("No Data found to close")
            End If
            Dim qry As String
            qry = "Update TSPL_REQUISITION_HEAD set close_yn='" + cls + "' where Requisition_Id='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean 'BM00000008148
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Requistion No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsRequistionHead = clsRequistionHead.GetData(strDocNo, NavigatorType.Current, trans, "")
            Dim totDrAmt As Double = 0
            Dim totCrAmt As Double = 0

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Requisition_Id) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Requisition", obj.Location, obj.Requisition_Date, trans)

            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            '' commented by priti 
            'If (obj.On_Hold = 1) Then
            '    Throw New Exception("Requistion No " + obj.Requisition_Id + " Is On Hold.Can't Post it")
            'End If
            Dim Approvallevel As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Case When COUNT(*)=0 Then 0 Else MAX(Approval_level) end From TSPL_REQUISITION_APPROVAL", trans))
            Dim qry As String = ""
            If objCommonVar.IsDemoERP AndAlso Approvallevel <> 0 Then
                qry = "Update TSPL_REQUISITION_HEAD set "
                If Approvallevel <> 0 Then
                    If objCommonVar.CurrUserLevel = 1 Then
                        If obj.Level1_Approval_Status = 1 And Approvallevel = 1 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is already approved by Budgetory User")
                        End If
                        qry += "Level1_Approval_Status=1,Level1_Approval_On='" + strPostDate + "',Level1_Approval_By='" + objCommonVar.CurrentUserCode + "'"
                    ElseIf objCommonVar.CurrUserLevel = 2 Then
                        If obj.Level1_Approval_Status = 0 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is need to approved by Budgetary User")
                        End If
                        If obj.Level2_Approval_Status = 1 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is already approved by Finance User")
                        End If
                        qry += "Level2_Approval_Status=1,Level2_Approval_On='" + strPostDate + "',Level2_Approval_By='" + objCommonVar.CurrentUserCode + "'"
                    ElseIf objCommonVar.CurrUserLevel = 3 Then
                        If obj.Level2_Approval_Status = 0 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is need to approved by Finance User")
                        End If
                        If obj.Level3_Approval_Status = 1 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is already approved by Level 1 User")
                        End If
                        qry += "Level3_Approval_Status=1,Level3_Approval_On='" + strPostDate + "',Level3_Approval_By='" + objCommonVar.CurrentUserCode + "'"

                    ElseIf objCommonVar.CurrUserLevel = 4 Then
                        If obj.Level3_Approval_Status = 0 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is need to approved by Level 1 User")
                        End If
                        If obj.Level4_Approval_Status = 1 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is already approved by Level 2 User")
                        End If
                        qry += "Level4_Approval_Status=1,Level4_Approval_On='" + strPostDate + "',Level4_Approval_By='" + objCommonVar.CurrentUserCode + "'"
                    ElseIf objCommonVar.CurrUserLevel = 5 Then
                        If obj.Level4_Approval_Status = 0 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is need to approved by Level 2 User")
                        End If
                        If obj.Level5_Approval_Status = 1 Then
                            Throw New Exception("Requistion No " + obj.Requisition_Id + " is already approved by Level 3 User")
                        End If
                        qry += "Level5_Approval_Status=1,Level5_Approval_On='" + strPostDate + "',Level5_Approval_By='" + objCommonVar.CurrentUserCode + "'"

                    Else
                        Throw New Exception("Invalid Level of User")
                    End If
                End If
                'If objCommonVar.CurrUserLevel = obj.Approvel_Level_Required Then
                If objCommonVar.CurrUserLevel = Approvallevel Then
                    qry += " ,Status=1,Is_Approved=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
                End If
                qry += " where Requisition_Id='" + strDocNo + "'"
            Else
                qry = "Update TSPL_REQUISITION_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Requisition_Id='" + strDocNo + "'"
            End If

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '===Sanjeet(03/01/2017) for notifiaction====
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseRequistion + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(obj.Description), "Auto Indent") = CompairStringResult.Equal Then
                    Dim objnotify As New clsNotificationReplace
                    objnotify.DocNo = obj.Requisition_Id
                    objnotify.DocDate = obj.Requisition_Date
                    objnotify.DocAmt = obj.Total_RQ_Amt
                    clsNotificationHead.SendNotification(clsUserMgtCode.mbtnPurchaseRequistion, objnotify, "P", trans)
                    'trans.Commit()
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(obj.Description), "Auto Indent") = CompairStringResult.Equal Then
                clsRequistionDetail.UpdateAutoIndentReorderQty(strDocNo, obj.ArrTr, trans)
            End If

            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS(obj, trans)
            End If
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Private Shared Sub CreateInternalEmailSMS(ByVal obj As clsRequistionHead, ByVal trans As SqlTransaction)
        Dim qry As String = ""
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseRequistion + "2" + "'", trans)

        qry = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER " & _
                " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code " & _
                " where TSPL_REQUISITION_HEAD.Requisition_Id='" + obj.Requisition_Id + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            If (obj.ArrTr IsNot Nothing AndAlso obj.ArrTr.Count > 0) Then
                For Each objdetail As clsRequistionDetail In obj.ArrTr
                    itemName = clsCommon.myCstr(objdetail.Item_Desc)
                    UOM = clsCommon.myCstr(objdetail.Unit_Code)
                    qty = clsCommon.myCstr(objdetail.Requisition_Qty)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Requisition_Id)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Requisition_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.Total_RQ_Amt))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                objEmailH.SaveData(clsUserMgtCode.mbtnPurchaseRequistion, objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo
                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Requisition_Id)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Requisition_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.Total_RQ_Amt))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                objSMSH.SaveData(clsUserMgtCode.mbtnPurchaseRequistion, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If
    End Sub

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
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Requisition No not found to Delete")
        End If
        Dim obj As clsRequistionHead = clsRequistionHead.GetData(strCode, NavigatorType.Current, trans, "")
        ' trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Requisition_Id) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Requisition", obj.Location, obj.Requisition_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_REQUISITION_DETAIL where Requisition_Id='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_REQUISITION_HEAD where Requisition_Id='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                'If (isSaved) Then
                '    trans.Commit()
                'Else
                '    trans.Rollback()
                'End If
            Catch ex As Exception
                'trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select 1 from TSPL_REQUISITION_HEAD where Requisition_Id='" + strDocNo + "' and Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If
            qry = "select distinct PurchaseOrder_No from TSPL_PURCHASE_ORDER_DETAIL where Requisition_Id='" + strDocNo + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Purchase Requisition used in following PO"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + clsCommon.myCstr(dr("PurchaseOrder_No"))
                Next
                'qry += Environment.NewLine + "Can't unpost it"
                Throw New Exception(qry)
            End If

            ''''''''''''''
            qry = "select distinct Doc_No from TSPL_IssueReturn_DETAIL where Req_IssueNo='" + strDocNo + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Requisition used in following Issue/Return"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                Next
                'qry += Environment.NewLine + "Can't unpost it"
                Throw New Exception(qry)
            End If
            ''''''''''''''

            qry = "update TSPL_REQUISITION_HEAD set Status=0,Posting_Date=null where Requisition_Id='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Doc_Type As String = Nothing

            Dim obj As clsRequistionHead = clsRequistionHead.GetData(Doc_No, NavigatorType.Current, trans, "")
            If obj Is Nothing OrElse clsCommon.myLen(obj.Requisition_Id) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_REQUISITION_HEAD", "Requisition_Id", "TSPL_REQUISITION_DETAIL", "Requisition_Id", trans)

            clsRequistionHead.ReverseAndUnpost(Doc_No, trans)
            clsRequistionHead.DeleteData(Doc_No, trans)

            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function IsValidVendorForRequitionItem(ByVal strReqNo As String, ByVal strICode As String, ByVal strVendorCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_REQUISITION_DETAIL where Requisition_Id ='" + strReqNo + "' and Item_Code='" + strICode + "' and Vendor_Code not in ('','" + strVendorCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
    Public Shared Function IsValidProjectForRequitionItem(ByVal strReqNo As String, ByVal strProject As String) As Boolean
        Dim qry As String = "select 1 from TSPL_REQUISITION_Head where Requisition_Id ='" + strReqNo + "' and PROJECT_ID='" & strProject & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        Else
            Return False
        End If
        Return True
    End Function
    Public Shared Function IsValidProjectForRequition(ByVal strReqNo As String, ByVal strProject As String) As String
        Dim strProj As String = clsDBFuncationality.getSingleValue("select ISNULL(PROJECT_ID,'') from TSPL_REQUISITION_Head where Requisition_Id ='" + strReqNo + "'")
        Return strProj
    End Function

End Class


Public Class clsRequistionDetail
#Region "Variables"
    Public Capacity As String = Nothing
    Public Make As String = Nothing
    Public Model As String = Nothing
    Public Line_No As Integer = 0
    Public Requisition_Id As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Vendor_Code As String = Nothing
    Public VendorName As String = Nothing
    Public Requisition_Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Location As String = Nothing
    Public LocationName As String = Nothing
    Public Item_Cost As String = Nothing
    Public Status As String = Nothing
    Public Order_No As String = Nothing
    Public Vendor_ItemNo As String = Nothing
    Public Unit_Code As String = Nothing
    Public Row_Type As String = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public CostCode As String = Nothing
    Public Hirerachy_Code As String = String.Empty
    Public Cost_Centre_Code As String = String.Empty
    Public Requisition_Date As Date?
    Public ItemReorder As String = Nothing
    Public HirerachyLevelCode3 As String = String.Empty
    Public HirerachyLevelCode4 As String = String.Empty

#End Region

    Public Shared Function SaveData(ByVal strReqNo As String, ByVal Arr As List(Of clsRequistionDetail), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRequistionDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Requisition_Id", strReqNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Requisition_Qty", obj.Requisition_Qty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                clsCommon.AddColumnsForChange(coll, "Order_No", obj.Order_No)
                clsCommon.AddColumnsForChange(coll, "Vendor_ItemNo", obj.Vendor_ItemNo)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
                clsCommon.AddColumnsForChange(coll, "Make", obj.Make)
                clsCommon.AddColumnsForChange(coll, "Model", obj.Model)
                clsCommon.AddColumnsForChange(coll, "Cost_Code", obj.CostCode, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", obj.Hirerachy_Code, True)
                clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", obj.Cost_Centre_Code, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level3", obj.HirerachyLevelCode3, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level4", obj.HirerachyLevelCode4, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function

    Public Shared Function UpdateAutoIndentReorderQty(ByVal strReqNo As String, ByVal Arr As List(Of clsRequistionDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRequistionDetail In Arr
                Dim ReorderQty As Integer = 0
                Dim Qty As String = clsDBFuncationality.getSingleValue("select Reorder_Qty from TSPL_ITEM_REORDER_LEVEL_NEW where Item_Code='" & obj.Item_Code & "'", trans)
                If clsCommon.myCdbl(Qty) < clsCommon.myCdbl(obj.Requisition_Qty) Then
                    Throw New Exception("Indent Qty Cannot be more then for Reorder level Qty.")
                End If
                ReorderQty = Qty - obj.Requisition_Qty
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_ITEM_REORDER_LEVEL_NEW set Reorder_Qty='" & ReorderQty & "' where Item_Code='" & obj.Item_Code & "'", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceRequitionQty(ByVal strReqCode As String, ByVal strICode As String, ByVal strCurrPONo As String, ByVal strCurrSRNNo As String, ByVal SettingIndendFreePOClose As Boolean) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from( " & _
            " select TSPL_REQUISITION_DETAIL.Item_Code as ICode,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,1 as RI from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id where TSPL_REQUISITION_DETAIL.Status='N' and TSPL_REQUISITION_HEAD.Status=1 and TSPL_REQUISITION_DETAIL.Requisition_Id='" + strReqCode + "' and TSPL_REQUISITION_DETAIL.Item_Code='" + strICode + "'" & _
            " union all " & _
            "select TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,-1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id='" + strReqCode + "'   and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + strICode + "' and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No not in ('" + strCurrPONo + "')  " & _
            " union all " & _
            " select TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.SRN_Qty as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Req_No='" + strReqCode + "'   and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and len(isnull(PO_Id,''))<=0 and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrSRNNo + "')  "
        If SettingIndendFreePOClose Then
            qry += " union all " + Environment.NewLine + _
                "select ICode,sum(Qty*RI) as Qty,1 as RI from (" + Environment.NewLine + _
                "select  TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode ,case when TSPL_PURCHASE_ORDER_HEAD.Status=0 then 0 else TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty end as Qty ,1 as RI,1 as Chk  " + Environment.NewLine + _
                "from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No not in ('')   and TSPL_PURCHASE_ORDER_HEAD.IsCancel=0 " + Environment.NewLine + _
                "and (TSPL_PURCHASE_ORDER_HEAD.close_yn='Y' or TSPL_PURCHASE_ORDER_DETAIL.Status=1) " + Environment.NewLine + _
                "and  TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id='" + strReqCode + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + strICode + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_GRN_DETAIL.PO_ID as PurchaseOrder_No, TSPL_GRN_DETAIL.Requisition_Id as Code,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.GRN_Qty  Qty ,-1 as RI,0 as Chk  " + Environment.NewLine + _
                "from TSPL_GRN_DETAIL " + Environment.NewLine + _
                "left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where len(isnull(TSPL_GRN_DETAIL.Requisition_Id,''))>0 and len(isnull( TSPL_GRN_DETAIL.PO_Id,''))>0  " + Environment.NewLine + _
                "and TSPL_GRN_DETAIL.Requisition_Id='" + strReqCode + "' and TSPL_GRN_DETAIL.Item_Code='" + strICode + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_MRN_DETAIL.PO_ID as PurchaseOrder_No, TSPL_MRN_DETAIL.Requisition_Id as Code,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Reject_Qty as Qty ,1 as RI,0 as chk" + Environment.NewLine + _
                "from TSPL_MRN_DETAIL " + Environment.NewLine + _
                "left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where len(isnull(TSPL_MRN_DETAIL.Requisition_Id,''))>0 and len(isnull( TSPL_MRN_DETAIL.PO_ID,''))>0  " + Environment.NewLine + _
                " and TSPL_MRN_HEAD.Status=1 and not exists(select 1 from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.MRN_Id =TSPL_MRN_DETAIL.MRN_No and  TSPL_SRN_DETAIL.PO_ID=TSPL_MRN_DETAIL.PO_ID and TSPL_SRN_DETAIL.Req_No =TSPL_MRN_DETAIL.Requisition_Id and TSPL_SRN_DETAIL.Item_Code =TSPL_MRN_DETAIL.Item_Code)" + Environment.NewLine + _
                " and TSPL_MRN_DETAIL.Requisition_Id ='" + strReqCode + "' and TSPL_MRN_DETAIL.Item_Code='" + strICode + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select   TSPL_SRN_DETAIL.PO_ID as PurchaseOrder_No, TSPL_SRN_DETAIL.Req_No as Code,TSPL_SRN_DETAIL.Item_Code as ICode ,TSPL_SRN_DETAIL.Rejected_Qty+TSPL_SRN_DETAIL.Short_Qty as Qty   ,1 as RI,0 as chk" + Environment.NewLine + _
                "from TSPL_SRN_DETAIL " + Environment.NewLine + _
                "left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where len(isnull(TSPL_SRN_DETAIL.Req_No,''))>0 and len(isnull( PO_Id,''))>0  " + Environment.NewLine + _
                "and TSPL_SRN_HEAD.Status=1 and TSPL_SRN_DETAIL.Req_No='" + strReqCode + "' and TSPL_SRN_DETAIL.Item_Code ='" + strICode + "'" + Environment.NewLine + _
                ")x group by PurchaseOrder_No,Code,ICode having sum(chk)>0 and sum(Qty*RI)>0"
        End If
        qry += ")Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteRequition(ByVal strReqCode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_REQUISITION_DETAIL set Status ='Y' where Requisition_Id='" + strReqCode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

End Class

'=================SMS AT POST-================================
Public Class clsSMSAtPost_Purchase

    Public Shared Function SMSATPOST_PUR() As Boolean
        Try
            Dim isSend As Boolean = True

            isSend = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.Purchase_SMSATPOST, clsFixedParameterCode.Purchase_SMSATPOST, Nothing) = "1", True, False))

            Return isSend
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsSMSAtPost_Sales

    Public Shared Function SMSATPOST_SALE() As Boolean
        Try
            Dim isSend As Boolean = True

            isSend = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.Sale_SMSATPOST, clsFixedParameterCode.Sale_SMSATPOST, Nothing) = "1", True, False))

            Return isSend
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

''BM00000008148
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Net.Mime.MediaTypeNames
Imports Microsoft.Office.Interop
Imports System.Windows.Forms

Public Class clsApprovalScreen

#Region "Variable Declaration"
    Public Module_Code As String = Nothing
    Public Trans_Code As String = Nothing
    Public No_Of_Level As Integer = Nothing
    Public Level As String = Nothing
    Public User_Code As String = Nothing
    Public User_Name As String = Nothing
    Public Department_Code As String = Nothing

    Public Approval_Type As String = Nothing
    Public Amount_Limit As Decimal = Nothing
    Public Qty_Limit As Decimal = Nothing
    Public Amount_Qty_Type As String = Nothing
    Public Auto_Post As Boolean = False
    Public Is_DepartmentWise As Boolean = False
    Public All_Level_Approval_Required As Boolean = False
    Public Loc_Code As String = Nothing
    Public Capex_Category As String = Nothing

    Public Arr As New List(Of clsApprovalScreen)
#End Region

    Public Shared Function SaveData(ByVal obj As clsApprovalScreen) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsApprovalScreen, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                DeleteData(obj.Arr(0).Module_Code, obj.Arr(0).Trans_Code, obj.Arr(0).Loc_Code, obj.Arr(0).Capex_Category, trans)
                For Each obj1 As clsApprovalScreen In obj.Arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Module_Code", obj1.Module_Code)
                    clsCommon.AddColumnsForChange(coll, "Trans_Code", obj1.Trans_Code)
                    clsCommon.AddColumnsForChange(coll, "No_Of_Level", obj1.No_Of_Level)
                    clsCommon.AddColumnsForChange(coll, "Level", obj1.Level)
                    clsCommon.AddColumnsForChange(coll, "User_Code", obj1.User_Code)
                    clsCommon.AddColumnsForChange(coll, "Approval_Type", obj1.Approval_Type)
                    clsCommon.AddColumnsForChange(coll, "Amount_Limit", obj1.Amount_Limit)
                    clsCommon.AddColumnsForChange(coll, "Qty_Limit", obj1.Qty_Limit)
                    clsCommon.AddColumnsForChange(coll, "Amount_Qty_Type", obj1.Amount_Qty_Type)
                    clsCommon.AddColumnsForChange(coll, "Auto_Post", obj1.Auto_Post)
                    clsCommon.AddColumnsForChange(coll, "Is_DepartmentWise", obj1.Is_DepartmentWise)
                    clsCommon.AddColumnsForChange(coll, "All_Level_Approval_Required", IIf(obj1.All_Level_Approval_Required, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Department_Code", obj1.Department_Code)
                    clsCommon.AddColumnsForChange(coll, "Loc_Code", obj1.Loc_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Capex_Category", obj1.Capex_Category)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APPROVAL_LEVEL_SCREEN", OMInsertOrUpdate.Insert, "", trans)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APPROVAL_LEVEL_SCREEN_HISTORY", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal Module_Code As String, ByVal Screen_Code As String, ByVal strLocCode As String, ByVal strCaptex As String, Optional ByVal trans As SqlTransaction = Nothing) As clsApprovalScreen
        Dim obj As New clsApprovalScreen()
        Dim obj1 As New clsApprovalScreen()
        Dim dt As New DataTable()
        Try
            obj.Arr = New List(Of clsApprovalScreen)

            Dim qry As String = "select TSPL_APPROVAL_LEVEL_SCREEN.*,tspl_user_master.user_name from TSPL_APPROVAL_LEVEL_SCREEN left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_SCREEN.user_code where TSPL_APPROVAL_LEVEL_SCREEN.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_APPROVAL_LEVEL_SCREEN.module_code='" + Module_Code + "' and TSPL_APPROVAL_LEVEL_SCREEN.trans_code='" + Screen_Code + "'"
            If clsCommon.myLen(strLocCode) > 0 Then
                qry += " and Loc_Code='" + strLocCode + "'"
            End If
            If clsCommon.myLen(strCaptex) > 0 Then
                qry += " and Capex_Category='" + strCaptex + "'"
            End If
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj1 = New clsApprovalScreen()

                    obj1.Amount_Limit = clsCommon.myCdbl(dr("Amount_Limit"))
                    obj1.Amount_Qty_Type = clsCommon.myCstr(dr("Amount_Qty_Type"))
                    obj1.Approval_Type = clsCommon.myCstr(dr("Approval_Type"))
                    obj1.Level = clsCommon.myCstr(dr("Level"))
                    obj1.Module_Code = clsCommon.myCstr(dr("Module_Code"))
                    obj1.No_Of_Level = clsCommon.myCdbl(dr("No_Of_Level"))
                    obj1.Qty_Limit = clsCommon.myCdbl(dr("Qty_Limit"))
                    obj1.Trans_Code = clsCommon.myCstr(dr("Trans_Code"))
                    obj1.User_Code = clsCommon.myCstr(dr("User_Code"))
                    obj1.User_Name = clsCommon.myCstr(dr("user_name"))
                    obj1.Auto_Post = clsCommon.myCBool(dr("Auto_Post"))
                    obj1.Is_DepartmentWise = clsCommon.myCBool(dr("Is_DepartmentWise"))
                    obj1.All_Level_Approval_Required = (clsCommon.myCdbl(dr("All_Level_Approval_Required")) > 0)
                    obj1.Department_Code = clsCommon.myCstr(dr("Department_Code"))
                    obj1.Loc_Code = clsCommon.myCstr(dr("Loc_Code"))
                    obj1.Capex_Category = clsCommon.myCstr(dr("Capex_Category"))
                    obj.Arr.Add(obj1)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            obj1 = Nothing
        End Try
    End Function

    Public Shared Function DeleteData(ByVal Module_Name As String, ByVal Screen_Name As String, ByVal strLocCode As String, ByVal strCaptex As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(Module_Name, Screen_Name, strLocCode, strCaptex, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal Module_Name As String, ByVal Screen_Name As String, ByVal strLocCode As String, ByVal strCaptex As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code='" + Module_Name + "' and Trans_Code='" + Screen_Name + "'"
            If clsCommon.myLen(strLocCode) > 0 Then
                qry += " and Loc_Code='" + strLocCode + "'"
            End If
            If clsCommon.myLen(strCaptex) > 0 Then
                qry += " and Capex_Category='" + strCaptex + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    '================================ X ==================================== X ===================================== X ========================
#Region "Commented Function"
    Public Shared Function SaveApprovalAtTransLevel(ByVal TransID As String, ByVal ColName As String, ByVal ColValue As String, ByVal TableName As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        'Dim qry As String = ""
        'Dim dt As DataTable = Nothing

        'Try

        '    'If String.IsNullOrEmpty(TransID) Then
        '    '    Throw New Exception("Transaction ID is blank")
        '    'End If

        '    qry = "Select MAX(LEVEL)Level from TSPL_APPROVAL_LEVEL_SCREEN where Trans_Code='" + TransID + "' "
        '    dt = clsDBFuncationality.GetDataTable(qry, trans)

        '    Dim whereCls = "" + ColName + " ='" + ColValue + "' "
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        If clsCommon.myLen(dt.Rows(0)("Level")) > 0 Then
        '            Dim coll As New Hashtable()

        '            clsCommon.AddColumnsForChange(coll, "Approval_Level", clsCommon.myCstr(dt.Rows(0)("Level")))
        '            clsCommonFunctionality.UpdateDataTable(coll, TableName, OMInsertOrUpdate.Update, whereCls, trans)
        '        End If
        '    End If
        Return True
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    'Public Sub sendEMailThroughOUTLOOK(ByVal lstReceiptents As List(Of String), ByVal TransCode As String, ByVal DocumentCode As String, ByVal Subject As String, ByVal Msg As String)
    '    Try
    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '        oMsg.Subject = Subject

    '        oMsg.Body = "Dear Sir/Madam, " + vbCrLf + Msg & vbCrLf & vbCrLf & "http://localhost/OMS/Login.aspx?TransCode='" + TransCode + "'&DocCode='" + DocumentCode + "' " & vbCrLf & "Best Regards" & vbCrLf & "" + objCommonVar.CurrentUser + ""

    '        'Approval required for Requisition No:
    '        Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1

    '        For ii As Integer = 0 To lstReceiptents.Count - 1
    '            oMsg.Recipients.Add(lstReceiptents(ii))
    '        Next
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Public Shared Function CheckApprovalLevel(ByVal FormId As String, ByVal tableName As String, ByVal ColName As String, ByVal DocId As String, ByVal trans As SqlTransaction) As Boolean
        'Try
        '    Dim isReturn As Boolean = False
        '    Dim msg As String = ""

        '    Dim qry As String = "Select count(*)count from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + FormId + "' "
        '    Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)

        '    If count > 0 Then
        '        qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + FormId + "' "
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '            Dim Userlevel As String = dt.Rows(0)("Level").ToString()
        '            Dim NoOfLevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))

        '            qry = "Select ISNULL(Approval_Level,'')Approval_Level, Level1_User, Level2_User, Level3_User from " + tableName + " where " + ColName + "='" + DocId + "' "
        '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        '            If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level3_User"))) > 0 Then
        '                isReturn = True
        '            Else
        '                Dim strApprovalLevel As String = clsDBFuncationality.getSingleValue(qry, trans)
        '                If clsCommon.CompairString(Userlevel, "Level1") = CompairStringResult.Equal Then
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level1_User"))) > 0 Then
        '                        Throw New Exception("Level 1 Approval Already Done. Level 2 Approval Required.")
        '                    End If
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level2_User"))) > 0 Then
        '                        Throw New Exception("Level 2 Approval Already Done. Level 3 Approval Required.")
        '                    End If
        '                    qry = "Update " + tableName + " set Level1_User='" + objCommonVar.CurrentUserCode + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' "
        '                    If NoOfLevel = 1 Then
        '                        qry += ", Is_Approved=1 "
        '                    End If
        '                    qry += "where " + ColName + "='" + DocId + "' "

        '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '                ElseIf clsCommon.CompairString(Userlevel, "Level2") = CompairStringResult.Equal Then
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level2_User"))) > 0 Then
        '                        Throw New Exception("Level 2 Approval Already Done. Level 3 Approval Required.")
        '                    End If
        '                    qry = "Update " + tableName + " set Level2_User='" + objCommonVar.CurrentUserCode + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' "
        '                    If NoOfLevel = 2 Then
        '                        qry += ", Is_Approved=1 "
        '                    End If
        '                    qry += "where " + ColName + "='" + DocId + "' "

        '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                Else
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level3_User"))) > 0 Then
        '                        msg = "Level 3 Approval Already Done. Document Already approved. "
        '                    End If
        '                    qry = "Update " + tableName + " set Level3_User='" + objCommonVar.CurrentUserCode + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' "
        '                    If NoOfLevel = 3 Then
        '                        qry += ", Is_Approved=1 "
        '                    End If
        '                    qry += "where " + ColName + "='" + DocId + "' "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                End If

        '                'clsCommon.MyMessageBoxShow(msg)
        '                If clsCommon.CompairString(Userlevel, strApprovalLevel) <> CompairStringResult.Equal Then
        '                    isReturn = False
        '                Else
        '                    isReturn = True
        '                End If
        '            End If
        '        Else
        '            Throw New Exception("UnAuthorized User!!!")
        '            isReturn = False
        '        End If
        '    Else
        '        isReturn = True
        '    End If

        Return True

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function
    Public Shared Function CheckApprovalLevel(ByVal FormId As String, ByVal tableName As String, ByVal ColName As String, ByVal DocId As String, ByVal Modify_By_Col As String, ByVal Modified_date_col As String, ByVal trans As SqlTransaction) As Boolean
        'Try
        '    Dim isReturn As Boolean = False
        '    Dim msg As String = ""

        '    Dim qry As String = "Select count(*)count from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + FormId + "' "
        '    Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)

        '    If count > 0 Then
        '        qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + FormId + "' "
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '            Dim Userlevel As String = dt.Rows(0)("Level").ToString()
        '            Dim NoOfLevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))

        '            qry = "Select ISNULL(Approval_Level,'')Approval_Level, Level1_User, Level2_User, Level3_User from " + tableName + " where " + ColName + "='" + DocId + "' "
        '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        '            If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level3_User"))) > 0 Then
        '                isReturn = True
        '            Else
        '                Dim strApprovalLevel As String = clsDBFuncationality.getSingleValue(qry, trans)
        '                If clsCommon.CompairString(Userlevel, "Level1") = CompairStringResult.Equal Then
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level1_User"))) > 0 Then
        '                        Throw New Exception("Level 1 Approval Already Done. Level 2 Approval Required.")
        '                    End If
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level2_User"))) > 0 Then
        '                        Throw New Exception("Level 2 Approval Already Done. Level 3 Approval Required.")
        '                    End If
        '                    qry = "Update " + tableName + " set Level1_User='" + objCommonVar.CurrentUserCode + "'," & Modify_By_Col & "='" + objCommonVar.CurrentUserCode + "', " & Modified_date_col & "='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' "
        '                    If NoOfLevel = 1 Then
        '                        qry += ", Is_Approved=1 "
        '                    End If
        '                    qry += "where " + ColName + "='" + DocId + "' "

        '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '                ElseIf clsCommon.CompairString(Userlevel, "Level2") = CompairStringResult.Equal Then
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level2_User"))) > 0 Then
        '                        Throw New Exception("Level 2 Approval Already Done. Level 3 Approval Required.")
        '                    End If
        '                    qry = "Update " + tableName + " set Level2_User='" + objCommonVar.CurrentUserCode + "'," & Modify_By_Col & "='" + objCommonVar.CurrentUserCode + "', " & Modified_date_col & "='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' "
        '                    If NoOfLevel = 2 Then
        '                        qry += ", Is_Approved=1 "
        '                    End If
        '                    qry += "where " + ColName + "='" + DocId + "' "

        '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                Else
        '                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Level3_User"))) > 0 Then
        '                        msg = "Level 3 Approval Already Done. Document Already approved. "
        '                    End If
        '                    qry = "Update " + tableName + " set Level3_User='" + objCommonVar.CurrentUserCode + "'," & Modify_By_Col & "='" + objCommonVar.CurrentUserCode + "', " & Modified_date_col & "='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' "
        '                    If NoOfLevel = 3 Then
        '                        qry += ", Is_Approved=1 "
        '                    End If
        '                    qry += "where " + ColName + "='" + DocId + "' "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                End If

        '                'clsCommon.MyMessageBoxShow(msg)
        '                If clsCommon.CompairString(Userlevel, strApprovalLevel) <> CompairStringResult.Equal Then
        '                    isReturn = False
        '                Else
        '                    isReturn = True
        '                End If
        '            End If
        '        Else
        '            Throw New Exception("UnAuthorized User!!!")
        '            isReturn = False
        '        End If
        '    Else
        '        isReturn = True
        '    End If

        Return True

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function
#End Region
End Class

Public Class clsApply_Approval
#Region "variables"
    Public Item_Rate As Double = Nothing
    Public Comparision_Rate As Double = Nothing
    Public Other As Boolean = False
    Public ToLoc As String = Nothing
    Public CustCode As String = Nothing
    Public TotAmt As Double = Nothing
    Public DocCode As String = Nothing
    Public DocDate As Date = Nothing
    Public LCRequestNo As String = Nothing
    Public DeliveryCode As String = Nothing
    Public Advance_Approval_Reqd As Double = 0
    Public Is_Advance_Approved As Double = 0
    Shared documentsendforApprovalScreenfromQuickBookEntry As Boolean = True
#End Region

    'function created by stuti on 05/12/2016 for approval work
    Public Shared Function AllowNlevelonScreen(ByVal Form_Id As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, trans) = "1", True, False))
        If OpenWorkFlowInERP Then
            Dim qry As String = "select max(1) from TSPL_APPROVAL_LEVEL_SCREEN where trans_code='" + Form_Id + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    ''below function is call after save on all screens 
    '========update by Preeti Gupta Against Ticket No [BM00000008295]
    Public Shared Function CheckApprovalRequired(ByVal strLocationCode As String, ByVal strCapexCategory As String, ByVal Form_Id As String, ByVal Doc_Code As String, ByVal Doc_Date As DateTime?, ByVal Description As String, ByVal Remarks As String, ByVal Doc_Amount As Double, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal documentCounter As Integer = 0, Optional ByVal isAmendment As Boolean = False) As String
        Return CheckApprovalRequired(strLocationCode, strCapexCategory, Form_Id, Doc_Code, Doc_Date, Description, Remarks, Doc_Amount, Doc_Qty, DepartmentCode, Nothing, trans, documentCounter, isAmendment)
        'Return ""
    End Function

    Public Shared Function CheckApprovalRequired(ByVal Form_Id As String, ByVal Doc_Code As String, ByVal Doc_Date As DateTime?, ByVal Description As String, ByVal Remarks As String, ByVal Doc_Amount As Double, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal documentCounter As Integer = 0) As String
        Return CheckApprovalRequired(Form_Id, Doc_Code, Doc_Date, Description, Remarks, Doc_Amount, Doc_Qty, DepartmentCode, Nothing, trans, documentCounter)
        'Return ""
    End Function

    Public Shared Function CheckApprovalRequired(ByVal Form_Id As String, ByVal Doc_Code As String, ByVal Doc_Date As DateTime?, ByVal Description As String, ByVal Remarks As String, ByVal Doc_Amount As Double, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, ByVal obj As clsApply_Approval, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal documentCounter As Integer = 0) As String
        Return CheckApprovalRequired("", "", Form_Id, Doc_Code, Doc_Date, Description, Remarks, Doc_Amount, Doc_Qty, DepartmentCode, obj, trans, documentCounter)
    End Function
    Public Shared Function CheckApprovalRequired(ByVal strLocationCode As String, ByVal strCapexCategory As String, ByVal Form_Id As String, ByVal Doc_Code As String, ByVal Doc_Date As DateTime?, ByVal Description As String, ByVal Remarks As String, ByVal Doc_Amount As Double, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, ByVal obj As clsApply_Approval, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal documentCounter As Integer = 0, Optional ByVal isAmendment As Boolean = False) As String
        Dim msg As Integer = 0
        Dim NewLimt As Double = 0
        Dim str As String = ""
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim coll As New Hashtable()

        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, trans) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            str = ""
            Return str
        End If
        Try
            If clsCommon.myLen(Description) > 300 Then
                Description = Description.Substring(0, 300)
            End If

            If clsCommon.myLen(Remarks) > 300 Then
                Remarks = Remarks.Substring(0, 300)
            End If

            Dim qry As String = ""

            qry = "select Status,all_level_approval  from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where 2=2 and isnull(Is_Posted,0) =0 and is_reverse=0 and No_Of_Level in (select max(no_of_level) from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where comp_code='" & objCommonVar.CurrentCompanyCode & "' and TRANS_Code='" & Form_Id & "' and Document_Code='" & Doc_Code & "') and TRANS_Code ='" & Form_Id & "' and Document_Code ='" & Doc_Code & "' and Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            ''if approved then message send and if rejected then no message throw,because in rejection case from higher authtority document is reopend for modification.
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Status")), "Rejected") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(dt.Rows(0)("all_level_approval")) = 1 Then
                        qry = "delete from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and trans_code='" + Form_Id + "' and document_code='" + Doc_Code + "' and is_reverse=0  "
                        clsDBFuncationality.getSingleValue(qry, trans)
                        dt = Nothing
                    Else
                        qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set Status =NULL,Amount='" + clsCommon.myCstr(Doc_Amount) + "',Qty='" + clsCommon.myCstr(Doc_Qty) + "' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and trans_code='" + Form_Id + "' and document_code='" + Doc_Code + "' and is_reverse=0  "
                        clsDBFuncationality.getSingleValue(qry, trans)
                        dt = Nothing
                        Return qry
                    End If
                End If
            End If


            'PO Amendment , Again go to approval
            If isAmendment = True Then
                qry = "update tspl_approval_level_transaction_detail set status='Amend' where trans_code='" + Form_Id + "' and Document_Code='" & Doc_Code & "' "
                If Doc_Date IsNot Nothing AndAlso IsDate(Doc_Date) Then
                    qry += " and document_date='" + clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") + "'"
                End If
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                dt = Nothing
            Else
                qry = "select count(*) as totalCOunt,(select (case when isnull(status,'')='' then min(no_of_level) else 0 end) as app_level from " &
                                  "TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL as trans_app where trans_app.trans_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code " &
                                  "and trans_app.document_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.document_code group by trans_app.status,trans_app.TRANS_Code,trans_app.Document_Code) as app_level from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL " &
                                  "where trans_code='" + Form_Id + "' and document_code='" + Doc_Code + "' and is_reverse=0  "
                If Doc_Date IsNot Nothing AndAlso IsDate(Doc_Date) Then
                    qry += " and document_date='" + clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") + "'"
                End If
                qry += " group by TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code "
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
            End If



            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim counter As Decimal = clsCommon.myCdbl(dt.Rows(0)("totalCOunt"))
                Dim no_of_level As Decimal = clsCommon.myCdbl(dt.Rows(0)("app_level"))

                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "TRANS_Code", Form_Id)
                clsCommon.AddColumnsForChange(coll, "Document_Code", Doc_Code)
                If Doc_Date IsNot Nothing AndAlso IsDate(Doc_Date) Then
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Description", Description)
                clsCommon.AddColumnsForChange(coll, "Remarks", Remarks)
                clsCommon.AddColumnsForChange(coll, "Amount", Doc_Amount)
                clsCommon.AddColumnsForChange(coll, "Qty", Doc_Qty)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL", OMInsertOrUpdate.Update, "TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code='" + Form_Id + "' and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code='" + Doc_Code + "' and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and is_reverse=0 ", trans)
                If counter > 0 AndAlso no_of_level > 0 Then
                    str = "Current document pending for approval at level " + clsCommon.myCstr(no_of_level) + ""
                    Return str
                ElseIf counter > 0 AndAlso no_of_level <= 0 Then
                    str = "Current document pending for approval."
                    Return str
                End If
            Else ''if not found in transaction table then find setting exist,if exist then insert into transaction table
                qry = "select * from TSPL_APPROVAL_LEVEL_SCREEN where trans_code='" + Form_Id + "' and 1= case when Is_DepartmentWise=1 and coalesce(Department_Code,'')<>'' and Department_Code='" + DepartmentCode + "' then 1 when Is_DepartmentWise=0 then 1 else 0 end"
                If clsCommon.myLen(strLocationCode) > 0 Then
                    qry += " and Loc_Code='" + strLocationCode + "'"
                End If
                If clsCommon.myLen(strCapexCategory) > 0 Then
                    qry += " and Capex_Category='" + strCapexCategory + "'"
                End If
                qry += " order by No_Of_Level"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                Dim chkcunter As Decimal = 0
                Dim arrLowerUser As New List(Of clsTempLvlUser)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        coll = New Hashtable()
                        Dim blAllLvlApprovalRequired As Boolean = (clsCommon.myCdbl(dr("All_Level_Approval_Required")) = 1)
                        If blAllLvlApprovalRequired Then
                            Dim objLU As New clsTempLvlUser
                            objLU.intLevel = clsCommon.myCdbl(dr("No_Of_Level"))
                            objLU.strUser = clsCommon.myCstr(dr("user_code"))
                            objLU.blCondition = True ''UDL/05/06/18-000182 by balwinder on 06/06/2018
                            arrLowerUser.Add(objLU)
                        End If


                        If clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Amount") = CompairStringResult.Equal Then
                            If (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Equal") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dr("amount_limit")) = Doc_Amount) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "GreaterEqual") = CompairStringResult.Equal AndAlso Doc_Amount >= clsCommon.myCdbl(dr("amount_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "LessEqual") = CompairStringResult.Equal AndAlso Doc_Amount <= clsCommon.myCdbl(dr("amount_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Less") = CompairStringResult.Equal AndAlso Doc_Amount < clsCommon.myCdbl(dr("amount_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Greater") = CompairStringResult.Equal AndAlso Doc_Amount > clsCommon.myCdbl(dr("amount_limit"))) Then
                                ''then save data
                                chkcunter += 1
                            Else
                                If blAllLvlApprovalRequired Then
                                    arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                End If
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Qty") = CompairStringResult.Equal Then
                            If (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Equal") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dr("qty_limit")) = Doc_Qty) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "GreaterEqual") = CompairStringResult.Equal AndAlso Doc_Qty >= clsCommon.myCdbl(dr("qty_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Less") = CompairStringResult.Equal AndAlso Doc_Qty < clsCommon.myCdbl(dr("qty_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "LessEqual") = CompairStringResult.Equal AndAlso Doc_Qty <= clsCommon.myCdbl(dr("qty_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Greater") = CompairStringResult.Equal AndAlso Doc_Qty > clsCommon.myCdbl(dr("qty_limit"))) Then
                                ''then save data
                                chkcunter += 1
                            Else
                                If blAllLvlApprovalRequired Then
                                    arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                End If
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "CrLmt") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmCSADeliveryOrder) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CustCode) > 0 Then
                                    NewLimt = clsCSADeliveryOrder.CustomerOutstandingAmount(clsCommon.myCstr(obj.ToLoc), obj.CustCode, obj.TotAmt, Nothing, obj.DocCode, obj.DocDate)
                                    If NewLimt <= 0 Then
                                        msg = 1
                                        chkcunter += 1
                                    Else
                                        If blAllLvlApprovalRequired Then
                                            arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                        End If
                                        Continue For
                                    End If
                                Else
                                    If blAllLvlApprovalRequired Then
                                        arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                    End If
                                    Continue For
                                End If

                                '--------------------------------------------------------------
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmDeliveryNoteFreshSale) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CustCode) > 0 Then
                                    NewLimt = clsDeliveryNoteFreshSale.CustomerOutstandingAmount(obj.DocCode, obj.CustCode, obj.TotAmt)
                                    If NewLimt < obj.TotAmt Then
                                        NewLimt = NewLimt - obj.TotAmt
                                        msg = 1
                                        chkcunter += 1
                                    Else
                                        If blAllLvlApprovalRequired Then
                                            arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                        End If
                                        Continue For
                                    End If
                                Else
                                    If blAllLvlApprovalRequired Then
                                        arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                    End If
                                    Continue For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.FrmDispatchBulkSale) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CustCode) > 0 Then
                                    NewLimt = ClsDispatchBulkSale.CheckCustomerOutstandingAmount(obj.DocCode, obj.CustCode, obj.TotAmt)
                                    If NewLimt < obj.TotAmt Then
                                        NewLimt = NewLimt - obj.TotAmt
                                        msg = 1
                                        chkcunter += 1
                                    Else
                                        If blAllLvlApprovalRequired Then
                                            arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                        End If
                                        Continue For
                                    End If
                                Else
                                    If blAllLvlApprovalRequired Then
                                        arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                    End If
                                    Continue For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Rate") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmBulkMilkSRN) = CompairStringResult.Equal Then
                                chkcunter += 1
                            Else
                                If Not clsCommon.myCdbl(obj.Item_Rate) = clsCommon.myCdbl(obj.Comparision_Rate) Then
                                    chkcunter += 1
                                Else
                                    If blAllLvlApprovalRequired Then
                                        arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                    End If
                                    Continue For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "IncAmt") = CompairStringResult.Equal Then
                            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.LCRequestNo) > 0 Then
                                Dim LCAmount As Double = clsDBFuncationality.getSingleValue("select LCAmount from TSPL_LC_REQUEST_MT where LCRequestNo='" & clsCommon.myCstr(obj.LCRequestNo) & "'", trans)
                                If clsCommon.myCdbl(obj.TotAmt) > LCAmount Then
                                    msg = 2
                                    chkcunter += 1
                                Else
                                    If blAllLvlApprovalRequired Then
                                        arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                    End If
                                    Continue For
                                End If
                            Else
                                If blAllLvlApprovalRequired Then
                                    arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                End If
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Other") = CompairStringResult.Equal Then
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "ARcpt") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmShipmentProductSale) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DeliveryCode) > 0 Then
                                    If clsPSShipmentHead.AdvanceReceived(obj.DeliveryCode, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 0 Then
                                        qry = "Update TSPL_SD_SHIPMENT_HEAD set Advance_Approval_Reqd=1 where Document_Code='" & obj.DocCode & "' "
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                        msg = 3
                                        chkcunter += 1
                                    ElseIf clsPSShipmentHead.AdvanceReceived(obj.DeliveryCode, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 1 Then
                                        msg = 4
                                        chkcunter += 1
                                    Else
                                        If blAllLvlApprovalRequired Then
                                            arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                        End If
                                        Continue For
                                    End If
                                Else
                                    If blAllLvlApprovalRequired Then
                                        arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                    End If
                                    Continue For
                                End If
                            Else
                                If blAllLvlApprovalRequired Then
                                    arrLowerUser(arrLowerUser.Count - 1).blCondition = False
                                End If
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "") = CompairStringResult.Equal Then
                            chkcunter += 1
                        End If

                        If chkcunter = 1 AndAlso msg = 0 And documentCounter = 0 Then
                            If Not clsCommon.MyMessageBoxShow("Want to send document for approval?", "Attention", Windows.Forms.MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                str = ""
                                documentsendforApprovalScreenfromQuickBookEntry = False
                                Return str
                            Else
                                documentsendforApprovalScreenfromQuickBookEntry = True
                            End If
                        ElseIf chkcunter = 1 AndAlso msg = 1 Then
                            If Not clsCommon.MyMessageBoxShow("Document need approval by " + clsCommon.myCstr(NewLimt) + " credit limit," + Environment.NewLine + "Are you sure want to send document for approval?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                                str = ""
                                Return str
                            Else
                                Description = Description + "Document required approval for credit limit,increase credit limit by " + clsCommon.myCstr(NewLimt) + " " + Environment.NewLine + "for customer [" + obj.CustCode + "/ " + clsCommon.myCstr(clsCustomerMaster.GetName(obj.CustCode, trans)) + "]"
                            End If
                        ElseIf chkcunter = 1 AndAlso msg = 2 Then
                            If Not clsCommon.MyMessageBoxShow("Document need approval for increasing LC Amount," + Environment.NewLine + "Are you sure want to send document for approval?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                                str = ""
                                Return str
                            Else
                                Description = Description + "Document required approval for Increasing LC amount."
                            End If
                        ElseIf chkcunter = 1 AndAlso msg = 3 Then
                            If Not clsCommon.MyMessageBoxShow("Do you want approval for dispatch w/o advance against booking,Are you sure want to send document for approval?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                                str = ""
                                Return str
                            Else
                                Description = Description + "Document required approval for Advanced Receipt."
                            End If
                        ElseIf chkcunter = 1 AndAlso msg = 4 Then
                            If Not clsCommon.MyMessageBoxShow("Document required approval,Are you sure want to send document for approval?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                                str = ""
                                Return str
                            Else
                                Description = Description + "Document required approval for Advanced Receipt."
                            End If
                        ElseIf chkcunter < 1 Then
                            Continue For
                        End If
                        If documentsendforApprovalScreenfromQuickBookEntry = True Then
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "TRANS_Code", Form_Id)
                            clsCommon.AddColumnsForChange(coll, "Document_Code", Doc_Code)
                            If Doc_Date IsNot Nothing AndAlso IsDate(Doc_Date) Then
                                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
                            End If
                            clsCommon.AddColumnsForChange(coll, "Description", Description)
                            clsCommon.AddColumnsForChange(coll, "Remarks", Remarks)
                            clsCommon.AddColumnsForChange(coll, "Amount", Doc_Amount)
                            clsCommon.AddColumnsForChange(coll, "Qty", Doc_Qty)
                            clsCommon.AddColumnsForChange(coll, "No_Of_Level", clsCommon.myCdbl(dr("No_Of_Level")))
                            clsCommon.AddColumnsForChange(coll, "User_Code", clsCommon.myCstr(dr("user_code")))
                            clsCommon.AddColumnsForChange(coll, "Status", "")

                            clsCommon.AddColumnsForChange(coll, "All_Level_Approval", clsCommon.myCdbl(dr("All_Level_Approval_Required")))
                            clsCommon.AddColumnsForChange(coll, "Approval_Remark", "")
                            clsCommon.AddColumnsForChange(coll, "Is_Posted", "0")
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        End If
                        If arrLowerUser IsNot Nothing AndAlso arrLowerUser.Count > 0 Then
                            Dim arrRemoveIndex As New List(Of Integer)
                            Dim index As Integer = 0
                            For Each objLV As clsTempLvlUser In arrLowerUser
                                If (Not objLV.blCondition Or (objLV.intLevel = clsCommon.myCdbl(dr("No_Of_Level")) AndAlso objLV.strUser = clsCommon.myCstr(dr("user_code")))) Then
                                    arrRemoveIndex.Add(index)
                                Else
                                    coll.Remove("No_Of_Level")
                                    coll.Remove("User_Code")
                                    clsCommon.AddColumnsForChange(coll, "No_Of_Level", objLV.intLevel)
                                    clsCommon.AddColumnsForChange(coll, "User_Code", objLV.strUser)
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                    arrRemoveIndex.Add(index)
                                End If
                                index += 1
                            Next
                            If arrRemoveIndex IsNot Nothing AndAlso arrRemoveIndex.Count > 0 Then
                                For ii As Integer = arrRemoveIndex.Count - 1 To 0 Step -1
                                    arrLowerUser.RemoveAt(ii)
                                Next
                            End If

                        End If

                    Next
                End If ''end dt1
            End If ''end dt

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            dt1 = Nothing
            coll = Nothing
        End Try
    End Function
    ''end here
    ''below function is call on update of document,when document update then check if send for approval then updation not allowed
    Public Shared Function CheckUpdate_Doc_Valid(ByVal Form_Id As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, trans) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            Return True
        End If
        Try
            Dim qry As String = ""
            qry = "select Status  from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where 2=2 and isnull(Is_Posted,0) =0 and is_reverse=0 and No_Of_Level in (select max(no_of_level) from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where comp_code='" & objCommonVar.CurrentCompanyCode & "' and TRANS_Code='" & Form_Id & "' and Document_Code='" & Doc_Code & "') and TRANS_Code ='" & Form_Id & "' and Document_Code ='" & Doc_Code & "' and Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' "
            Dim Status As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            ''if approved then message send and if rejected then no message throw,because in rejection case from higher authtority document is reopend for modification.
            If clsCommon.CompairString(Status, "Rejected") <> CompairStringResult.Equal Then
                qry = "select count(*) as totalCOunt from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL " & _
                 "where trans_code='" + Form_Id + "' and document_code='" + Doc_Code + "' and isnull(Is_Posted,0) =0 and is_reverse=0 "
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                    Throw New Exception("Invalid action,Document pending for approval." + Environment.NewLine + "[goto--> Approval Alert]")
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ApprovalCondCheck_Doc(ByVal Form_Id As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, trans) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            Return True
        End If
        Try
            Dim qry As String = ""
            Dim Status As String = ""
            qry = "select Status  from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where 2=2 and is_reverse=0 and No_Of_Level in (select max(no_of_level) from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where comp_code='" & objCommonVar.CurrentCompanyCode & "' and TRANS_Code='" & Form_Id & "' and Document_Code='" & Doc_Code & "' and is_reverse=0) and TRANS_Code ='" & Form_Id & "' and Document_Code ='" & Doc_Code & "' and Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' "
            Status = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            ''if approved then message send and if rejected then no message throw,because in rejection case from higher authtority document is reopend for modification.
            If clsCommon.CompairString(Status, "Rejected") = CompairStringResult.Equal Then
                Return ERPTransactionStatus.Reject
            ElseIf clsCommon.CompairString(Status, "Approved") = CompairStringResult.Equal Then
                Return ERPTransactionStatus.Approved
            Else
                Return ERPTransactionStatus.Pending
            End If
            Return ERPTransactionStatus.Pending
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function Visibility_PostButtonForApproval(ByVal Form_Id As String, ByVal Document_Code As String, ByVal Doc_Amount As Decimal, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Return Visibility_PostButtonForApproval(Form_Id, Document_Code, Doc_Amount, Doc_Qty, DepartmentCode, Nothing, trans)
    End Function

    Public Shared Function Visibility_PostButtonForApproval(ByVal strLocationCode As String, ByVal strCapexCategory As String, ByVal Form_Id As String, ByVal Document_Code As String, ByVal Doc_Amount As Decimal, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Return Visibility_PostButtonForApproval(strLocationCode, strCapexCategory, Form_Id, Document_Code, Doc_Amount, Doc_Qty, DepartmentCode, Nothing, trans)
    End Function

    Public Shared Function Visibility_PostButtonForApproval(ByVal Form_Id As String, ByVal Document_Code As String, ByVal Doc_Amount As Decimal, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, ByVal obj As clsApply_Approval, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Return Visibility_PostButtonForApproval("", "", Form_Id, Document_Code, Doc_Amount, Doc_Qty, DepartmentCode, obj, trans)
    End Function
    Public Shared Function Visibility_PostButtonForApproval(ByVal strLocationCode As String, ByVal strCapexCategory As String, ByVal Form_Id As String, ByVal Document_Code As String, ByVal Doc_Amount As Decimal, ByVal Doc_Qty As Decimal, ByVal DepartmentCode As String, ByVal obj As clsApply_Approval, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, trans) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            Return True
        End If
        Try
            Dim qry As String = ""
            Dim NewLimt As Double = 0
            qry = "select count(*) as totalCOunt from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL " & _
                  "where comp_code='" + objCommonVar.CurrentCompanyCode + "' and trans_code='" + Form_Id + "' and document_code='" + Document_Code + "' and is_reverse=0 "
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                Return False
            Else
                '='============================Update by preeti gupta Against Ticket No[BM00000008295]
                ''if any setting for current use is exist then also post button not seen,becuase mandatory to forward documet for approval
                qry = "select * from TSPL_APPROVAL_LEVEL_SCREEN where trans_code='" + Form_Id + "' and 1= case when Is_DepartmentWise=1 and coalesce(Department_Code,'')<>'' and Department_Code='" + DepartmentCode + "' then 1 when Is_DepartmentWise=0 then 1 else 0 end" ''and user_code='" + objCommonVar.CurrentUserCode + "'"
                If clsCommon.myLen(strLocationCode) > 0 Then
                    qry += " and Loc_Code='" + strLocationCode + "'"
                End If
                If clsCommon.myLen(strCapexCategory) > 0 Then
                    qry += " and Capex_Category='" + strCapexCategory + "'"
                End If
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim countr As Integer = 0

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Amount") = CompairStringResult.Equal Then
                            If (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Equal") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dr("amount_limit")) = Doc_Amount) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "GreaterEqual") = CompairStringResult.Equal AndAlso Doc_Amount >= clsCommon.myCdbl(dr("amount_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "LessEqual") = CompairStringResult.Equal AndAlso Doc_Amount <= clsCommon.myCdbl(dr("amount_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Less") = CompairStringResult.Equal AndAlso Doc_Amount < clsCommon.myCdbl(dr("amount_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Greater") = CompairStringResult.Equal AndAlso Doc_Amount > clsCommon.myCdbl(dr("amount_limit"))) Then
                                countr += 1
                            Else
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Qty") = CompairStringResult.Equal Then
                            If (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Equal") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dr("qty_limit")) = Doc_Qty) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "GreaterEqual") = CompairStringResult.Equal AndAlso Doc_Qty >= clsCommon.myCdbl(dr("qty_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Less") = CompairStringResult.Equal AndAlso Doc_Qty < clsCommon.myCdbl(dr("qty_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "LessEqual") = CompairStringResult.Equal AndAlso Doc_Qty <= clsCommon.myCdbl(dr("qty_limit"))) OrElse (clsCommon.CompairString(clsCommon.myCstr(dr("amount_qty_type")), "Greater") = CompairStringResult.Equal AndAlso Doc_Qty > clsCommon.myCdbl(dr("qty_limit"))) Then
                                countr += 1
                            Else
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "CrLmt") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmCSADeliveryOrder) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CustCode) > 0 Then
                                    NewLimt = clsCSADeliveryOrder.CustomerOutstandingAmount(clsCommon.myCstr(obj.ToLoc), obj.CustCode, obj.TotAmt, Nothing, obj.DocCode, obj.DocDate)
                                    If NewLimt <= 0 Then
                                        countr += 1
                                    Else
                                        Continue For
                                    End If
                                Else
                                    Continue For
                                End If

                                '--------------------------------------------------------------
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmDeliveryNoteFreshSale) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CustCode) > 0 Then
                                    NewLimt = clsDeliveryNoteFreshSale.CustomerOutstandingAmount(obj.DocCode, obj.CustCode, obj.TotAmt)
                                    If NewLimt < obj.TotAmt Then
                                        NewLimt = NewLimt - obj.TotAmt
                                        countr += 1
                                    Else
                                        Continue For
                                    End If
                                Else
                                    Continue For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.FrmDispatchBulkSale) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CustCode) > 0 Then
                                    NewLimt = ClsDispatchBulkSale.CheckCustomerOutstandingAmount(obj.DocCode, obj.CustCode, obj.TotAmt)
                                    If NewLimt < obj.TotAmt Then
                                        NewLimt = NewLimt - obj.TotAmt
                                        countr += 1
                                    Else
                                        Continue For
                                    End If
                                Else
                                    Continue For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Rate") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmBulkMilkSRN) = CompairStringResult.Equal Then
                                countr += 1
                            Else
                                If Not clsCommon.myCdbl(obj.Item_Rate) = clsCommon.myCdbl(obj.Comparision_Rate) Then
                                    countr += 1
                                Else
                                    Continue For
                                End If
                            End If

                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "IncAmt") = CompairStringResult.Equal Then
                            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.LCRequestNo) > 0 Then
                                Dim LCAmount As Double = clsDBFuncationality.getSingleValue("select LCAmount from TSPL_LC_REQUEST_MT where LCRequestNo='" & clsCommon.myCstr(obj.LCRequestNo) & "'", trans)
                                If clsCommon.myCdbl(obj.TotAmt) > LCAmount Then
                                    countr += 1
                                Else
                                    Continue For
                                End If
                            Else
                                Continue For
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "Other") = CompairStringResult.Equal Then
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("approval_type")), "ARcpt") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("trans_code")), clsUserMgtCode.frmShipmentProductSale) = CompairStringResult.Equal Then
                                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DeliveryCode) > 0 Then
                                    If clsPSShipmentHead.AdvanceReceived(obj.DeliveryCode, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 0 Then
                                        countr += 1
                                    ElseIf clsPSShipmentHead.AdvanceReceived(obj.DeliveryCode, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 1 Then
                                        countr += 1
                                    Else
                                        Continue For
                                    End If
                                Else
                                    Continue For
                                End If
                            Else
                                Continue For
                            End If
                        Else
                            countr += 1
                        End If
                    Next

                    If countr > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If ''end dt1
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Public Class clsApprovalAlert_Child
#Region "variables"
    Public Trans_Code As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime? = Nothing
    Public No_Of_Level As Decimal = Nothing
    Public User_Code As String = Nothing
    Public Status As String = Nothing
    Public Approval_Remark As String = Nothing
    Public Max_App_Level As Decimal = Nothing
    Public is_Posted As Integer = Nothing

    Public Auto_Post As Boolean = False
#End Region

    Public Shared Function UpdateData(ByVal obj As clsApprovalAlert_Child) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UpdateData(obj, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UpdateData(ByVal obj As clsApprovalAlert_Child, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Dim whrcls As String = Nothing
        Try
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                coll = New Hashtable()

                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                clsCommon.AddColumnsForChange(coll, "Approval_Remark", obj.Approval_Remark)
                clsCommon.AddColumnsForChange(coll, "is_Posted", obj.is_Posted)
                clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "SendBack", "0")
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                whrcls = " trans_code='" + obj.Trans_Code + "' and document_code='" + obj.Document_Code + "' and no_of_level='" + clsCommon.myCstr(obj.No_Of_Level) + "' and user_code='" + obj.User_Code + "' and is_reverse=0 and Status<>'Amend' "

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL", OMInsertOrUpdate.Update, whrcls, trans)


                If clsCommon.CompairString(obj.Status, "Rejected") = CompairStringResult.Equal Then
                    Dim qry As String = "select all_level_approval from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where " + whrcls
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                        qry = "delete  FROM TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL WHERE trans_code='" + obj.Trans_Code + "' and document_code='" + obj.Document_Code + "' and no_of_level>'" + clsCommon.myCstr(obj.No_Of_Level) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If

                If clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmBulkMilkSRN) = CompairStringResult.Equal Then
                    clsDBFuncationality.ExecuteNonQuery("update tspl_bulk_milk_srn set isApproved=1 where SRN_NO='" & obj.Document_Code & "'", trans)
                End If


                ''if auto post setting on and document is approved by higher level authorizer then document post.
                If obj.Auto_Post AndAlso clsCommon.CompairString(obj.Status, "Approved") = CompairStringResult.Equal AndAlso obj.No_Of_Level = obj.Max_App_Level Then
                    Postdata(obj, trans)
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function Postdata(ByVal obj As clsApprovalAlert_Child) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Postdata(obj, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function Postdata(ByVal obj As clsApprovalAlert_Child, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                If obj.No_Of_Level = obj.Max_App_Level Then ''only higher authorized user can post data
                    If clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
                        isSaved = isSaved AndAlso clsRequistionHead.PostData(obj.Document_Code, trans)

                    ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal Then
                        Dim objPO As New clsPurchaseOrderHead
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) as cc from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where Status='Amend' and TRANS_Code='" + obj.Trans_Code + "'  and Document_Code='" + obj.Document_Code + "'", trans)) > 0 Then
                            isSaved = isSaved AndAlso objPO.PostData(clsUserMgtCode.mbtnPurchaseOrder, obj.Document_Code, "", False, False, trans)
                        Else
                            isSaved = isSaved AndAlso objPO.PostData(clsUserMgtCode.mbtnPurchaseOrder, obj.Document_Code, "", True, False, trans)
                        End If
                        objPO = Nothing
                    ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmSNShipment) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsSNShipmentHead.PostData(clsUserMgtCode.frmSNShipment, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmSNSalesOrder) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsSNSalesOrderHead.PostData(clsUserMgtCode.frmSNSalesOrder, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmDeliveryPrderProductSale) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsPSDeliveryOrder.PostData(clsUserMgtCode.frmDeliveryPrderProductSale, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmDeliveryNoteFreshSale) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsDeliveryNoteFreshSale.PostData(clsUserMgtCode.frmDeliveryNoteFreshSale, obj.Document_Code, trans, 0)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmCSASaleInvoice) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsCSASaleInvoice.PostData(clsUserMgtCode.frmCSASaleInvoice, LOCATIONRIGTHS(trans), obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmVSPItemIssue) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsVSPItemIssue.PostData(obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmMCCMaterial) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsMCCMaterialSale.PostData(clsUserMgtCode.frmMCCMaterial, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmShipmentProductSale) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsPSShipmentHead.PostData(clsUserMgtCode.frmShipmentProductSale, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmbookingdairy) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso ClsDispatchBulkSale.PostData(clsUserMgtCode.frmbookingdairy, Nothing, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmCSADeliveryOrder) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsCSADeliveryOrder.PostData(obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.FrmLCCreation) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso ClsLCCreation.PostData(clsUserMgtCode.FrmLCCreation, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.FrmDispatchBulkSale) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso ClsDispatchBulkSale.PostData(clsUserMgtCode.FrmDispatchBulkSale, LOCATIONRIGTHS(trans), obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmBulkMilkSRN) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsBulkMilkSRN.postData(obj.Document_Code, clsUserMgtCode.frmBulkMilkSRN, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmSaleReturnProductSale) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsPSSalesReturnHead.PostData(clsUserMgtCode.frmSaleReturnProductSale, obj.Document_Code, trans)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.mbtnPurchaseInvoice) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsPurchaseInvoiceHead.PostData(clsUserMgtCode.mbtnPurchaseInvoice, obj.Document_Code, "", True, trans, False)

                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmSaleReturndairy) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsPSSalesReturnHead.PostData(clsUserMgtCode.frmSaleReturndairy, obj.Document_Code, trans)
                            ' ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmMCCMaterial) = CompairStringResult.Equal Then
                            '     isSaved = isSaved AndAlso clsMCCMaterialSale.PostData(clsUserMgtCode.frmMCCMaterial, obj.Document_Code, True)
                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.ReceiptEntry) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsRcptEntryHeader.funRcptPost(obj.Document_Code, trans, "MReceivable")
                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.mbtnARInvoiceEntry) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsCustomerInvoiceHead.PostData("AR-INVOICE", obj.Document_Code, "", trans)
                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.mbtnAPInvoiceEntry) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("AP-INVOICE", obj.Document_Code, "", trans)
                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.PaymentEntryNew) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsPaymentHeader.PostData(obj.Document_Code, "MPayable", trans)
                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.Transfer) = CompairStringResult.Equal Then
                            Dim ProvisionAllow As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PROVISIONENTRYONSTOCKTRANSFER, clsFixedParameterCode.PROVISIONENTRYONSTOCKTRANSFER, trans)) = 1, True, False))
                            isSaved = isSaved AndAlso clsTransferDCC.postTransfer(obj.Document_Code, trans, ProvisionAllow, "")
                        ElseIf clsCommon.CompairString(obj.Trans_Code, clsUserMgtCode.frmMilkJobWorkTransferOther) = CompairStringResult.Equal Then
                            isSaved = isSaved AndAlso clsJWOTransferOtherHead.PostData(obj.Document_Code, trans)
                    End If

                    If isSaved Then ''if higher authorizer do approval then at post document status posted on approval table
                        Dim qry As String = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_posted=1,SendBack=0 where trans_code='" + obj.Trans_Code + "' and document_code='" + obj.Document_Code + "' and is_reverse=0 "
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If ''end level cond.

            End If ''end obj cond.

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function LOCATIONRIGTHS(ByVal trans As SqlTransaction) As String
        Dim arrloc As String = ""
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(trans)
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrloc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arrloc
    End Function

End Class

Class clsTempLvlUser
    Public intLevel As Integer
    Public strUser As String
    Public blCondition As Boolean
End Class






'=============BM00000003058,updated by Rohit,Create new Setting(Return Without Invoice).=============
Imports common
Imports System.Data.SqlClient

Public Class clsPurchaseSettings
#Region "variables"
    Public CREATE_ABATEMENT_BASED_PO As String = Nothing
    Public CREATE_PO_WITH_REQ As String = Nothing
    Public ENABLE_POPUP_REORDERLEVEL As String = Nothing
    Public enable_mail_sms As String = Nothing
    Public MANDATE_BATCHNO_RM As String
    Public MANDATE_BATCHNO_FG As String
    Public MANDATE_BATCHNO_ASSET As String
    Public MANDATE_BATCHNO_OTHERS As String

    Public MANDATE_MFG_RM As String
    Public MANDATE_MFG_FG As String
    Public MANDATE_MFG_ASSET As String
    Public MANDATE_MFG_OTHERS As String

    Public MANDATE_EXP_RM As String
    Public MANDATE_EXP_FG As String
    Public MANDATE_EXP_ASSET As String
    Public MANDATE_EXP_OTHERS As String

    Public REQUIRED_SECURITY_AMOUNT As String
    Public REQUIRED_FOC As String
    Public AllowLargerPriceThenVendorPrice As String

    Public PurchaseOrderAutomaticallyItemQtyBelowReorderLevel As String = Nothing


    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing

    Public Return_Without_Invoice As String
    Public SRN_Rejected_Store As String
    Public Job_Work_Account As String
    Public SRN_Limit As Double = 0
    Public GRN_Limit As Double = 0

#End Region
#Region "Functions"
    Public Shared Function SaveData(ByVal obj As clsPurchaseSettings, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete  from TSPL_PURCHASE_SETTINGS  ", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CREATE_ABATEMENT_BASED_PO", obj.CREATE_ABATEMENT_BASED_PO)
            clsCommon.AddColumnsForChange(coll, "CREATE_PO_WITH_REQ", obj.CREATE_PO_WITH_REQ)
            clsCommon.AddColumnsForChange(coll, "ENABLE_POPUP_REORDERLEVEL", obj.ENABLE_POPUP_REORDERLEVEL)

            '' save batch no
            clsCommon.AddColumnsForChange(coll, "MANDATE_BATCHNO_RM", obj.MANDATE_BATCHNO_RM)
            clsCommon.AddColumnsForChange(coll, "MANDATE_BATCHNO_FG", obj.MANDATE_BATCHNO_FG)
            clsCommon.AddColumnsForChange(coll, "MANDATE_BATCHNO_ASSET", obj.MANDATE_BATCHNO_ASSET)
            clsCommon.AddColumnsForChange(coll, "MANDATE_BATCHNO_OTHERS", obj.MANDATE_BATCHNO_OTHERS)

            '' save mfg date
            clsCommon.AddColumnsForChange(coll, "MANDATE_MFG_RM", obj.MANDATE_MFG_RM)
            clsCommon.AddColumnsForChange(coll, "MANDATE_MFG_FG", obj.MANDATE_MFG_FG)
            clsCommon.AddColumnsForChange(coll, "MANDATE_MFG_ASSET", obj.MANDATE_MFG_ASSET)
            clsCommon.AddColumnsForChange(coll, "MANDATE_MFG_OTHERS", obj.MANDATE_MFG_OTHERS)

            '' save exp date
            clsCommon.AddColumnsForChange(coll, "MANDATE_EXP_RM", obj.MANDATE_EXP_RM)
            clsCommon.AddColumnsForChange(coll, "MANDATE_EXP_FG", obj.MANDATE_EXP_FG)
            clsCommon.AddColumnsForChange(coll, "MANDATE_EXP_ASSET", obj.MANDATE_EXP_ASSET)
            clsCommon.AddColumnsForChange(coll, "MANDATE_EXP_OTHERS", obj.MANDATE_EXP_OTHERS)

            clsCommon.AddColumnsForChange(coll, "REQUIRED_SECURITY_AMOUNT", obj.REQUIRED_SECURITY_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "REQUIRED_FOC", obj.REQUIRED_FOC)

            clsCommon.AddColumnsForChange(coll, "PurchaseOrderAutomaticallyItemQtyBelowReorderLevel", obj.PurchaseOrderAutomaticallyItemQtyBelowReorderLevel)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Return_without_Invoice", obj.Return_Without_Invoice)
            clsCommon.AddColumnsForChange(coll, "SRN_Rejected_Store", obj.SRN_Rejected_Store)
            '' Anubhooti 12-Nov-2014 BM00000003662
            clsCommon.AddColumnsForChange(coll, "Job_Work_Account", obj.Job_Work_Account, True)
            clsCommon.AddColumnsForChange(coll, "SRN_Limit", obj.SRN_Limit, True)
            clsCommon.AddColumnsForChange(coll, "GRN_Limit", obj.GRN_Limit, True) ' Add by Prabhakar 24/11/2016
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_SETTINGS", OMInsertOrUpdate.Insert, "", trans)


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(Optional ByVal trans As SqlTransaction = Nothing) As clsPurchaseSettings

        Dim obj As clsPurchaseSettings = Nothing
        Dim Arr As List(Of clsPurchaseSettings) = Nothing
        Dim qry As String = "select * from TSPL_PURCHASE_SETTINGS "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPurchaseSettings()
            obj.CREATE_ABATEMENT_BASED_PO = clsCommon.myCstr(dt.Rows(0)("CREATE_ABATEMENT_BASED_PO"))
            obj.CREATE_PO_WITH_REQ = clsCommon.myCstr(dt.Rows(0)("CREATE_PO_WITH_REQ"))
            obj.ENABLE_POPUP_REORDERLEVEL = clsCommon.myCstr(dt.Rows(0)("ENABLE_POPUP_REORDERLEVEL"))
            obj.MANDATE_BATCHNO_RM = clsCommon.myCstr(dt.Rows(0)("MANDATE_BATCHNO_RM"))
            obj.MANDATE_BATCHNO_FG = clsCommon.myCstr(dt.Rows(0)("MANDATE_BATCHNO_FG"))
            obj.MANDATE_BATCHNO_ASSET = clsCommon.myCstr(dt.Rows(0)("MANDATE_BATCHNO_ASSET"))
            obj.MANDATE_BATCHNO_OTHERS = clsCommon.myCstr(dt.Rows(0)("MANDATE_BATCHNO_OTHERS"))

            obj.MANDATE_MFG_RM = clsCommon.myCstr(dt.Rows(0)("MANDATE_MFG_RM"))
            obj.MANDATE_MFG_FG = clsCommon.myCstr(dt.Rows(0)("MANDATE_MFG_FG"))
            obj.MANDATE_MFG_ASSET = clsCommon.myCstr(dt.Rows(0)("MANDATE_MFG_ASSET"))
            obj.MANDATE_MFG_OTHERS = clsCommon.myCstr(dt.Rows(0)("MANDATE_MFG_OTHERS"))

            obj.MANDATE_EXP_RM = clsCommon.myCstr(dt.Rows(0)("MANDATE_EXP_RM"))
            obj.MANDATE_EXP_FG = clsCommon.myCstr(dt.Rows(0)("MANDATE_EXP_FG"))
            obj.MANDATE_EXP_ASSET = clsCommon.myCstr(dt.Rows(0)("MANDATE_EXP_ASSET"))
            obj.MANDATE_EXP_OTHERS = clsCommon.myCstr(dt.Rows(0)("MANDATE_EXP_OTHERS"))
            obj.REQUIRED_SECURITY_AMOUNT = clsCommon.myCstr(dt.Rows(0)("REQUIRED_SECURITY_AMOUNT"))
            obj.REQUIRED_FOC = clsCommon.myCstr(dt.Rows(0)("REQUIRED_FOC"))

            obj.Return_Without_Invoice = clsCommon.myCstr(dt.Rows(0)("Return_Without_Invoice"))
            obj.SRN_Rejected_Store = clsCommon.myCstr(dt.Rows(0)("SRN_Rejected_Store"))
            '' Anubhooti 12-Nov-2014 BM00000003662
            obj.Job_Work_Account = clsCommon.myCstr(dt.Rows(0)("Job_Work_Account"))
            obj.SRN_Limit = clsCommon.myCdbl(dt.Rows(0)("SRN_Limit"))
            obj.GRN_Limit = clsCommon.myCdbl(dt.Rows(0)("GRN_Limit"))
            obj.PurchaseOrderAutomaticallyItemQtyBelowReorderLevel = clsCommon.myCstr(dt.Rows(0)("PurchaseOrderAutomaticallyItemQtyBelowReorderLevel"))

        End If
        Return obj
    End Function
    Public Shared Function Get_MO_BO_Setting(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' this function returns true if MO screen is on else false.
        Dim qry As String = "select ENABLE_POPUP_REORDERLEVEL from TSPL_PURCHASE_SETTINGS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("ENABLE_POPUP_REORDERLEVEL") = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
#End Region
End Class

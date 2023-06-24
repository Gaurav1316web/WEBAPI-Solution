Imports System.Data.SqlClient
Imports System.IO
Public Class clsNewDCSScreen

#Region "Variables"
    Public VLCCode As String = Nothing
    Public VLCName As String = Nothing
    Public VLCUploaderCode As String = Nothing
    Public Registered As Boolean = False
    Public RegistrationNo As String = Nothing
    Public RegistrationDate As DateTime
    Public DCSRouteCode As String = Nothing
    Public OwnBMC As Boolean = False
    Public OwnBMCDate As DateTime
    Public HeadLoad As String = Nothing
    Public HeadLoadBasi As String = Nothing
    Public HeadLoadRate As String = Nothing
    Public Inactive As Boolean = False
    Public DefaultValue As Boolean = False
    Public PDCS As Boolean = False
    Public Gender As String = False
    Public ApplyCowPrice As Boolean = False
    Public StartDate As DateTime
    Public SupervisorCode As String = Nothing
    Public District As String = Nothing
    Public Zone As String = Nothing
    Public Block As String = Nothing
    Public RevenueVillage As String = Nothing
    Public GramPanchayat As String = Nothing
    Public PanchayatSamiti As String = Nothing
    Public VidhanSabha As String = Nothing
    Public CompanyBank1 As String = Nothing
    Public DCSCurrentBankDetails1 As String = Nothing
    Public BankName1 As String = Nothing
    Public BankAccountNo1 As String = Nothing
    Public BankBranch1 As String = Nothing
    Public BankIFSCCode1 As String = Nothing
    Public CompanyBank2 As String = Nothing
    Public DCSCurrentBankDetails2 As String = Nothing
    Public BankName2 As String = Nothing
    Public BankAccountNo2 As String = Nothing
    Public BankBranch2 As String = Nothing
    Public BankIFSCCode2 As String = Nothing
    Public PanNo As String = Nothing
    Public LoyaltyRate As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsNewDCSScreen, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsNewDCSScreen, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.VLCCode)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.VLCName)
            clsCommon.AddColumnsForChange(coll, "Bank_Name2", obj.BankName2)
            clsCommon.AddColumnsForChange(coll, "IFSCCode2", obj.BankIFSCCode2)
            clsCommon.AddColumnsForChange(coll, "AccNo2", obj.BankAccountNo2)
            clsCommon.AddColumnsForChange(coll, "BankBranch2", obj.BankBranch2)
            clsCommon.AddColumnsForChange(coll, "Gender", obj.Gender)
            clsCommon.AddColumnsForChange(coll, "RegistrationNo", obj.RegistrationNo)
            clsCommon.AddColumnsForChange(coll, "RegistrationDate", obj.RegistrationDate)
            clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone)
            clsCommon.AddColumnsForChange(coll, "Block_Code", obj.Block)
            clsCommon.AddColumnsForChange(coll, "CompanyBank", obj.CompanyBank2)
            clsCommon.AddColumnsForChange(coll, "REVENUE_VILLAGE_CODE", obj.RevenueVillage)
            clsCommon.AddColumnsForChange(coll, "GRAMPANCHAYAT_CODE", obj.RevenueVillage)
            clsCommon.AddColumnsForChange(coll, "PANCHAYAT_SAMITI_CODE", obj.GramPanchayat)
            clsCommon.AddColumnsForChange(coll, "VIDHAN_SABHA_CODE", obj.VidhanSabha)

            'clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.RegistrationDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                If obj.PDCS = True Then
                    obj.VLCCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.PDCS, "")
                Else
                    obj.VLCCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
                End If
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLCCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_vendor_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_vendor_master", OMInsertOrUpdate.Update, "tspl_vendor_master.Vendor_Code='" + obj.VLCCode + "'", trans)
            End If
            SaveData1(obj, isNewEntry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData1(ByVal obj As clsNewDCSScreen, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Village_Code", obj.RevenueVillage)
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.DCSRouteCode)
            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", obj.VLCUploaderCode)
            clsCommon.AddColumnsForChange(coll, "Registered_PDCS_CLUSTER", obj.Registered)
            clsCommon.AddColumnsForChange(coll, "isOwnBMC", obj.OwnBMC)
            clsCommon.AddColumnsForChange(coll, "Apply_Cow_Price", obj.ApplyCowPrice)
            clsCommon.AddColumnsForChange(coll, "ApplyCowPriceDate", obj.StartDate)
            clsCommon.AddColumnsForChange(coll, "Loyalty_Rate", obj.LoyaltyRate)
            If isNewEntry Then
                If obj.PDCS = True Then
                    obj.VLCCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.PDCS, "")
                Else
                    obj.VLCCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
                End If
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLCCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, "TSPL_VLC_MASTER_HEAD.Vendor_Code='" + obj.VLCUploaderCode + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


End Class

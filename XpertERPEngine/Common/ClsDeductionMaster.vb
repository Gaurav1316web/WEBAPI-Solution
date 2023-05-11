Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsDeductionMaster
#Region "Variable"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Ded_Grp_Code As String = Nothing
    Public GL_Account_Code As String = Nothing
    Public Security As Integer = Nothing

    Public Is_Default_Company_Deduction As Boolean
    Public Is_Default_VSP_Deduction As Boolean
    Public Is_Default_VSP_Quality_Deduction As Boolean
    Public Is_Default_Transporter_Deduction As Boolean
    Public Is_Default_Security_Deduction As Boolean
    Public Is_Default_Asset_Installment As Boolean
    Public Is_Default_Asset_Lost As Boolean
    Public Is_Default_Advance_Interest As Boolean
    Public Is_Default_TIP As Boolean
    Public Is_Default_PRO_Data As Boolean
    Public Is_Default_Std_Deduction As Boolean
    Public Is_Default_Local_Sale As Boolean
    Public Is_Default_Pashu_Vikash_Kos As Boolean
    Public Is_Own_BMC_Shortage As Boolean
    Public Is_Own_BMC_Excess As Boolean

    Public Show_FAT_SNF As Boolean
    Public HO_TYPE As Boolean
    Public VLC_TYPE As Boolean
    Public OTHERS_TYPE As Boolean
    Public Sequence_No As Int64 = 0
    'Public Trans_Type As String = String.Empty
    Public Is_MILK As Boolean
    Public Is_FEED As Boolean
    Public IS_GHEE As Boolean
    Public Is_Addition As Boolean
#End Region
    Public Shared Function SaveData(ByVal obj As ClsDeductionMaster, ByVal isnewentry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""

        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Ded_Grp_Code", obj.Ded_Grp_Code)
            clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
            clsCommon.AddColumnsForChange(coll, "Security", obj.Security)
            clsCommon.AddColumnsForChange(coll, "Show_FAT_SNF", IIf(obj.Show_FAT_SNF, 1, 0))
            clsCommon.AddColumnsForChange(coll, "HO_TYPE", IIf(obj.HO_TYPE, 1, 0))
            clsCommon.AddColumnsForChange(coll, "VLC_TYPE", IIf(obj.VLC_TYPE, 1, 0))
            clsCommon.AddColumnsForChange(coll, "OTHERS_TYPE", IIf(obj.OTHERS_TYPE, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Sequence_No", obj.Sequence_No)
            'If clsCommon.myLen(obj.Trans_Type) > 0 Then
            '    clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type, True)
            'End If
            clsCommon.AddColumnsForChange(coll, "Is_MILK", IIf(obj.Is_MILK, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_FEED", IIf(obj.Is_FEED, 1, 0))
            clsCommon.AddColumnsForChange(coll, "IS_GHEE", IIf(obj.IS_GHEE, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Addition", IIf(obj.Is_Addition, 1, 0))
            qry = ""
            If obj.Is_Default_Company_Deduction Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Company_Deduction=0 "
            End If
            If obj.Is_Default_VSP_Deduction Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_VSP_Deduction=0 "
            End If
            If obj.Is_Default_VSP_Quality_Deduction Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_VSP_Quality_Deduction=0 "
            End If
            If obj.Is_Default_Transporter_Deduction Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Transporter_Deduction=0 "
            End If
            If obj.Is_Default_Security_Deduction Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Security_Deduction=0 "
            End If
            If obj.Is_Default_Asset_Installment Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Asset_Installment=0 "
            End If
            If obj.Is_Default_Asset_Lost Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Asset_Lost=0 "
            End If
            If obj.Is_Default_Advance_Interest Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Advance_Interest=0 "
            End If
            If obj.Is_Default_TIP Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_TIP=0 "
            End If
            If obj.Is_Default_PRO_Data Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_PRO_Data=0 "
            End If
            If obj.Is_Default_Std_Deduction Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Std_Deduction=0 "
            End If
            If obj.Is_Default_Local_Sale Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Local_Sale=0 "
            End If
            If obj.Is_Default_Pashu_Vikash_Kos Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Default_Pashu_Vikash_Kos=0 "
            End If
            If obj.Is_Own_BMC_Shortage Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Own_BMC_Shortage=0 "
            End If
            If obj.Is_Own_BMC_Excess Then
                If clsCommon.myLen(qry) > 0 Then
                    qry += ","
                End If
                qry += " Is_Own_BMC_Excess=0 "
            End If
            If clsCommon.myLen(qry) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_DEDUCTION_MASTER set " + qry, trans)
            End If
            clsCommon.AddColumnsForChange(coll, "Is_Default_Company_Deduction", IIf(obj.Is_Default_Company_Deduction, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_VSP_Deduction", IIf(obj.Is_Default_VSP_Deduction, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_VSP_Quality_Deduction", IIf(obj.Is_Default_VSP_Quality_Deduction, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Transporter_Deduction", IIf(obj.Is_Default_Transporter_Deduction, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Security_Deduction", IIf(obj.Is_Default_Security_Deduction, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Asset_Installment", IIf(obj.Is_Default_Asset_Installment, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Asset_Lost", IIf(obj.Is_Default_Asset_Lost, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Advance_Interest", IIf(obj.Is_Default_Advance_Interest, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_TIP", IIf(obj.Is_Default_TIP, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_PRO_Data", IIf(obj.Is_Default_PRO_Data, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Std_Deduction", IIf(obj.Is_Default_Std_Deduction, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Local_Sale", IIf(obj.Is_Default_Local_Sale, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Default_Pashu_Vikash_Kos", IIf(obj.Is_Default_Pashu_Vikash_Kos, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Own_BMC_Shortage", IIf(obj.Is_Own_BMC_Shortage, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Own_BMC_Excess", IIf(obj.Is_Own_BMC_Excess, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMMM/yyyy "))
            If isnewentry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DEDUCTION_MASTER where Code='" & obj.Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.DeductionMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMMM/yyyy "))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_MASTER", OMInsertOrUpdate.Update, "TSPL_DEDUCTION_MASTER.Code='" + obj.Code + "' ", trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getdata(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsDeductionMaster
        Try
            Dim obj As ClsDeductionMaster = Nothing
            Dim qry As String = "select TSPL_DEDUCTION_MASTER.* from TSPL_DEDUCTION_MASTER  where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_DEDUCTION_MASTER.Code = (select MIN(Code) from TSPL_DEDUCTION_MASTER)"
                Case NavigatorType.Last
                    qry += " and TSPL_DEDUCTION_MASTER.Code = (select Max(Code) from TSPL_DEDUCTION_MASTER)"
                Case NavigatorType.Next
                    qry += " and TSPL_DEDUCTION_MASTER.Code = (select Min(Code) from TSPL_DEDUCTION_MASTER where  Code >'" + strcode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_DEDUCTION_MASTER.Code = (select Max(Code) from TSPL_DEDUCTION_MASTER where Code <'" + strcode + "')"
                Case NavigatorType.Current
                    qry += " and TSPL_DEDUCTION_MASTER.Code = '" + strcode + "'"
            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New ClsDeductionMaster
                obj.Code = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                obj.Ded_Grp_Code = clsCommon.myCstr(dt1.Rows(0)("Ded_Grp_Code"))
                obj.GL_Account_Code = clsCommon.myCstr(dt1.Rows(0)("GL_Account_Code"))
                obj.Security = clsCommon.myCstr(dt1.Rows(0)("Security"))
                obj.Is_Default_Company_Deduction = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Company_Deduction")) > 0, True, False)
                obj.Is_Default_VSP_Deduction = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_VSP_Deduction")) > 0, True, False)
                obj.Is_Default_VSP_Quality_Deduction = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_VSP_Quality_Deduction")) > 0, True, False)
                obj.Is_Default_Transporter_Deduction = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Transporter_Deduction")) > 0, True, False)
                obj.Is_Default_Security_Deduction = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Security_Deduction")) > 0, True, False)
                obj.Is_Default_Asset_Installment = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Asset_Installment")) > 0, True, False)
                obj.Is_Default_Asset_Lost = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Asset_Lost")) > 0, True, False)
                obj.Is_Default_Advance_Interest = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Advance_Interest")) > 0, True, False)
                obj.Is_Default_TIP = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_TIP")) > 0, True, False)
                obj.Is_Default_PRO_Data = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_PRO_Data")) > 0, True, False)
                obj.Is_Default_Std_Deduction = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Std_Deduction")) > 0, True, False)
                obj.Is_Default_Local_Sale = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Local_Sale")) > 0, True, False)
                obj.Is_Default_Pashu_Vikash_Kos = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Default_Pashu_Vikash_Kos")) > 0, True, False)
                obj.Is_Own_BMC_Shortage = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Own_BMC_Shortage")) > 0, True, False)
                obj.Is_Own_BMC_Excess = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Own_BMC_Excess")) > 0, True, False)
                obj.Show_FAT_SNF = IIf(clsCommon.myCdbl(dt1.Rows(0)("Show_FAT_SNF")) > 0, True, False)
                obj.HO_TYPE = IIf(clsCommon.myCdbl(dt1.Rows(0)("HO_TYPE")) > 0, True, False)
                obj.VLC_TYPE = IIf(clsCommon.myCdbl(dt1.Rows(0)("VLC_TYPE")) > 0, True, False)
                obj.OTHERS_TYPE = IIf(clsCommon.myCdbl(dt1.Rows(0)("OTHERS_TYPE")) > 0, True, False)
                obj.Sequence_No = clsCommon.myCdbl(dt1.Rows(0)("Sequence_No"))
                'obj.Trans_Type = clsCommon.myCstr(dt1.Rows(0)("Trans_Type"))

                obj.Is_MILK = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_MILK")) > 0, True, False)
                obj.Is_FEED = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_FEED")) > 0, True, False)
                obj.IS_GHEE = IIf(clsCommon.myCdbl(dt1.Rows(0)("IS_GHEE")) > 0, True, False)
                obj.Is_Addition = IIf(clsCommon.myCdbl(dt1.Rows(0)("Is_Addition")) > 0, True, False)

            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetName(ByVal strCode As String, ByVal tran As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_DEDUCTION_MASTER.Description from TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Code = '" + strCode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

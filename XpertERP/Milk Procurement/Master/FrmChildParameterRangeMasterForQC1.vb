Imports System.Data.SqlClient
Imports common

Public Class FrmChildParameterRangeMasterForQC1
    Inherits FrmMainTranScreen
#Region "Variables"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = True
    Public code As String = Nothing
    Public desc As String = Nothing
    Public description As String = Nothing
    Public Lrange As Decimal = 0
    Public Urange As Decimal = 0
    Public Eff_date As Date = Nothing
    Public Status1 As String = Nothing
    Public value1 As String = Nothing
    Public value2 As String = Nothing
    Public Qc_Status As String = Nothing
    Public created_by As String = String.Empty
    Public created_date As String = String.Empty
    Public modified_by As String = String.Empty
    Public modified_date As String = String.Empty
    Public comp_code As String = String.Empty
    Public QC_Param_Code As String = Nothing
    Public Trans_Id As String = Nothing
    Public ShowinDigitalAnalyzer As String = Nothing
    Public TextinDigitalAnalyzer As String = Nothing
    Public Analyzer_Index As String = Nothing
    Public Deduction_Per As Decimal = 0
    Dim IsLoadData As Boolean = False
    Dim FormLoadData As Boolean = False
    Public FORMTYPE As String = Nothing
    Public Lrange_Prev As Decimal = 0
    Public Urange_Prev As Decimal = 0
    Public Qc_Status_prev As String = Nothing
    'Public Deduction_lower_range As Decimal = 0
    'Public Deduction_upper_range As Decimal = 0
    'Public Deduction_Ratio As Decimal = 0
    Dim SetItemWiseQualityCheckInGeneralPurchase As Boolean = False
#End Region
#Region "User Defined Functions and Subroutines"

#End Region
    Private Sub FrmChildParameterRangeMasterForQC1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetItemWiseQualityCheckInGeneralPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, Nothing)) = 1)
        LoadStatus()
        LoadQCStatus()
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        LoadData()
    End Sub
    Private Sub LoadData()
        If clsCommon.myLen(QC_Param_Code) > 0 Then
            FndParameterCode.Value = QC_Param_Code
            lblParamDesc.Text = desc
            txtlRange.Text = Lrange
            txtUrange.Text = Urange
            txtvalue.Text = value1
            'txtDescription.Text = desc
            txtDeductionPer.Text = Deduction_Per
            txtDescription.Text = description
            'txtDeductionLRange.Text = Deduction_lower_range
            'txtDeductionURange.Text = Deduction_upper_range
            'txtDeductionRatio.Text = Deduction_Ratio
            cboStatus.SelectedValue = Status1
            cboQcStatus.SelectedValue = Qc_Status
            txtDate.Value = Eff_date
            btnSave.Text = "Update"
            FndParameterCode.Enabled = False
            isNewEntry = False
        Else
            FndParameterCode.Enabled = True
            txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        End If

    End Sub
    Private Sub LoadStatus()
        cboStatus.DataSource = Nothing
        Dim qry As String = ""
        qry = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        cboStatus.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboStatus.ValueMember = "Code"
        cboStatus.DisplayMember = "Name"
    End Sub
    Private Sub LoadQCStatus()
        cboQcStatus.DataSource = Nothing
        Dim qry As String = ""
        qry = "select 'OK' as Code,'OK' as Name union all select 'NOT OK' as Code,'NOT OK' as Name "
        cboQcStatus.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboQcStatus.ValueMember = "Code"
        cboQcStatus.DisplayMember = "Name"
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then SaveData()
    End Sub
    Function AllowToSave() As Boolean
        Try
            Dim table_name As String = "tspl_parameter_master"
            If clsCommon.CompairString(clsUserMgtCode.FrmChildParameterRangeMasterForQC1, FORMTYPE) = CompairStringResult.Equal Then
                table_name = "TSPL_QC_LOG_SHEET_MASTER"
            End If
            Dim ShowinDigitalAnalyzer As String = Nothing
            Dim Analyzer_index As Integer = 0
            Dim TextinDigitalAnalyzer As String = Nothing
            Dim ShowinDigitalAnalyzer1 As String = Nothing
            Dim TextinDigitalAnalyzer1 As String = Nothing
            Dim Analyzer_Index1 As Integer = 0
            code = FndParameterCode.Value
            If clsCommon.myLen(code) <= 0 Then
                Throw New Exception("Please Fill Atleast One Parameter Range")
            End If
            Dim III As Double = Lrange

            ' For ii As Integer = 0 To gv.Rows.Count - 1
            'code = FndParameterCode.Value
            ' QC_Param_Code = FndParameterCode.Value
            description = clsCommon.myCstr(txtDescription.Text)
            Lrange = clsCommon.myCdbl(txtlRange.Text)
            urange = clsCommon.myCdbl(txtUrange.Text)
            Status1 = clsCommon.myCstr(cboStatus.SelectedValue)
            value1 = clsCommon.myCstr(txtvalue.Text)
            Qc_Status = clsCommon.myCstr(cboQcStatus.SelectedValue)
            Eff_date = clsCommon.myCDate(txtDate.Value)
            Deduction_Per = clsCommon.myCdbl(txtDeductionPer.Text)
            'Deduction_lower_range = clsCommon.myCdbl(txtDeductionLRange.Text)
            'Deduction_upper_range = clsCommon.myCdbl(txtDeductionURange.Text)
            'Deduction_Ratio = clsCommon.myCdbl(txtDeductionRatio.Text)
            modified_by = clsCommon.myCstr(objCommonVar.CurrentUserCode)
            ShowinDigitalAnalyzer = ""
            TextinDigitalAnalyzer = ""
            Analyzer_index = 0

            ' value2 = clsCommon.myCstr(gv.Rows(ii).Cells(colValue2).Value)

            Dim qry As String = "select nature from " + table_name + " where code='" + code + "'"
            Dim nature As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            'If nature = "A" AndAlso clsCommon.myLen(code) > 0 AndAlso (clsCommon.myLen(value1) <= 0 AndAlso clsCommon.myLen(value2) <= 0) Then
            '    Throw New Exception("Fill Value Of Parameter Nature(Alphanumeric)")
            'End If

            If nature = "R" AndAlso clsCommon.myLen(code) > 0 AndAlso (clsCommon.myLen(lrange) = 0 Or clsCommon.myLen(urange) = 0) Then
                Throw New Exception("Fill Lower/Upper Range Of Parameter Nature(Range)")
            End If
            If nature = "B" AndAlso clsCommon.myLen(code) > 0 AndAlso (clsCommon.myLen(status) <= 0) Then
                Throw New Exception("Fill Status Of Parameter Nature(Boolean)")
            End If

            If clsCommon.myLen(code) > 0 AndAlso clsCommon.myCdbl(lrange) = 0 AndAlso clsCommon.myCdbl(urange) = 0 AndAlso clsCommon.myLen(status) <= 0 AndAlso clsCommon.myLen(value1) <= 0 AndAlso clsCommon.myLen(value2) <= 0 Then
                Throw New Exception("Please Fill All Information Likes Lower Range/Upper Range Or Value-1/Value-2 Or Status,Effective Date")
            End If

            If clsCommon.myLen(code) > 0 AndAlso (clsCommon.myCdbl(lrange) <> 0 OrElse clsCommon.myCdbl(urange) <> 0) AndAlso clsCommon.myCdbl(lrange) > clsCommon.myCdbl(urange) Then
                Throw New Exception("Lower Range Should Be Less Than Upper Range ")
            End If

            If clsCommon.myLen(Qc_Status) <= 0 AndAlso clsCommon.myLen(code) > 0 Then
                Throw New Exception("Please Select Qc Status (Ok/Not Ok)")
            End If

            If clsCommon.CompairString(clsCommon.myCstr(ShowinDigitalAnalyzer), "NO") = CompairStringResult.Equal AndAlso clsCommon.myLen(TextinDigitalAnalyzer) > 0 Then
                Throw New Exception("Please Select Show in Digital Analyzer (Yes/No) ")
            End If

            If clsCommon.myLen(TextinDigitalAnalyzer) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(ShowinDigitalAnalyzer), "YES") = CompairStringResult.Equal Then
                Throw New Exception("Please Type Name in Text in Digital Analyzer Field")
            End If
            If SetItemWiseQualityCheckInGeneralPurchase = True Then
            Else
                Dim strsQry As String = "Select lower_range,upper_range,Qc_Status,QC_Param_Code from tspl_parameter_range_master_QC where upper_range='" + clsCommon.myCstr(Lrange) + "' and Qc_Status='" + Qc_Status + "' and QC_Param_Code ='" + code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strsQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("upper_range")), Lrange) = CompairStringResult.Equal Then
                        Throw New Exception("Lower Range value is Already as upper Range value in database witn same details")
                    End If
                    If (clsCommon.myCdbl(dt.Rows(0)("lower_range")) >= Lrange) AndAlso (clsCommon.myCdbl(dt.Rows(0)("upper_range")) <= Lrange) Then
                        Throw New Exception("Lower Range value is Already Contain in database with same details ")
                    End If
                    If True Then
                    End If
                End If
                Dim Chk As Integer = 0
                Chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(1) from tspl_parameter_range_master_QC where (lower_range>='" + clsCommon.myCstr(Lrange) + "' and upper_range<='" + clsCommon.myCstr(Urange) + "') and Qc_Status='" + Qc_Status + "' and QC_Param_Code ='" + code + "'", Nothing))
                If Chk > 0 Then
                    Throw New Exception("Parameter Range is Already in database with Same Details ")
                End If
            End If

            'Deduction Range
            'If Deduction_lower_range > Deduction_upper_range Then
            '    Throw New Exception("Deduction Lower Range Value Should Not be Greater than Upper Range Value.")
            'End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Sub SaveData()
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmQualityModuleParameterRangeMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim obj As New clsParameterRangeMasterForQC
        Dim arr As New List(Of clsParameterRangeMasterForQC)
        Try
            obj = New clsParameterRangeMasterForQC()

            If clsCommon.CompairString(clsUserMgtCode.FrmChildParameterRangeMasterForQC1, FORMTYPE) = CompairStringResult.Equal Then
                obj.QC_Param_Code = FndParameterCode.Value
            Else
                obj.code = FndParameterCode.Value
            End If

            obj.Trans_Id = Trans_Id
            obj.Lrange = Lrange
            obj.Urange = Urange
            obj.status = Status1
            obj.value1 = value1
            obj.Qc_Status = Qc_Status
            obj.Deduction_Per = Deduction_Per
            obj.Lrange_Prev = Lrange_Prev
            obj.Urange_Prev = Urange_Prev
            obj.Qc_Status_prev = Qc_Status_prev
            obj.Description = description
            'obj.Deduction_lower_range = Deduction_lower_range
            'obj.Deduction_upper_range = Deduction_upper_range
            'obj.Deduction_Ratio = Deduction_Ratio
            obj.is_NewEntry = isNewEntry
            Try
                obj.Eff_date = Eff_date
            Catch exx As Exception
                obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
            End Try

            Try
                If obj.Eff_date.ToString().Substring(6, 4) = "0001" Then
                    obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                End If
            Catch exx As Exception
                obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
            End Try


            If clsCommon.myLen(obj.code) > 0 OrElse clsCommon.myLen(obj.QC_Param_Code) > 0 Then
                arr.Add(obj)
            End If
            '  Next
            ' If isNewEntry Then
            If clsParameterRangeMasterForQC.SaveData(arr, Trans_Id) Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                LoadDataSaveData()
            End If
            ' Else
            'Dim qry As String

            'qry = "update TSPL_PARAMETER_RANGE_MASTER_QC set Lower_range='" + clsCommon.myCstr(obj.Lrange) + "',Upper_range='" + clsCommon.myCstr(obj.Urange) + "',Qc_Status='" + obj.Qc_Status + "',Effective_Date='" + clsCommon.GetPrintDate(obj.Eff_date, "dd/MMM/yyyy") + "',Value1='" + obj.value1 + "',Status='" + obj.status + "',Deduction_Per='" + clsCommon.myCstr(obj.Deduction_Per) + "' where lower_range='" + clsCommon.myCstr(Lrange_Prev) + "' and upper_range='" + clsCommon.myCstr(Urange_Prev) + "' and Qc_Status='" + Qc_Status_prev + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'End If
            ' trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            arr = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub Reset()
        FndParameterCode.Value = ""
        txtlRange.Text = 0
        txtUrange.Text = 0
        txtDeductionPer.Text = 0
        'txtDeductionLRange.Text = 0
        'txtDeductionURange.Text = 0
        'txtDeductionRatio.Text = 0
        txtvalue.Text = ""
        LoadQCStatus()
        LoadStatus()
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        isNewEntry = True
        FndParameterCode.Enabled = True
        btnSave.Text = "Save"
    End Sub
    Private Sub LoadDataSaveData()
        Lrange_Prev = clsCommon.myCdbl(txtlRange.Text)
        Urange_Prev = clsCommon.myCdbl(txtUrange.Text)
        Qc_Status_prev = clsCommon.myCstr(cboQcStatus.SelectedValue)
        description = clsCommon.myCdbl(txtDescription.Text)
    End Sub
    Private Sub FndParameterCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndParameterCode._MYValidating

        'Dim qry As String = "Select Code,Description,Type,Nature,(Case when IsMandatory=0 then 'Not Mandatory' else 'Mandatory' end) as IsMandatory,department_code as Department,created_by as [Created By],created_date as [Created Date],modified_by as [Modified By],modified_date as [Modified Date] from TSPL_QC_LOG_SHEET_MASTER"
        'FndParameterCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("PMRFND", qry, "Code", "", FndParameterCode.Value, "Code", isButtonClicked))
        'lblParamDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_parameter_master where code='" + FndParameterCode.Value + "'"))

        If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
            FndParameterCode.Value = clsPPLogSheetMaster.GetFinder(" trans_id='" + Trans_Id + "' ", "Code", isButtonClicked)
            lblParamDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_QC_LOG_SHEET_MASTER where code='" + FndParameterCode.Value + "'"))
            If SetItemWiseQualityCheckInGeneralPurchase = True Then
                QC_Param_Code = FndParameterCode.Value
                LoadData()
            End If
        Else
            Dim qrry As String = "select Code,Description,Type,Nature from tspl_parameter_master"
            FndParameterCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("PMRFND", qrry, "Code", "", FndParameterCode.Value, "Code", isButtonClicked))
            lblParamDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_parameter_master where code='" + FndParameterCode.Value + "'"))
        End If


    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        Try
            If clsCommon.myLen(FndParameterCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Parameter Code")
            End If
            Dim qry As String = "delete from tspl_parameter_range_master_qc where "
            If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                qry += " qc_param_code='" & FndParameterCode.Value & "' "
            Else
                qry += " Code='" & FndParameterCode.Value & "'  "
            End If
            qry += " and lower_range='" & txtlRange.Text & "' and Upper_range='" & txtUrange.Text & "' and Deduction_Per='" & txtDeductionPer.Text & "' and Qc_Status='" & cboQcStatus.SelectedText & "'"
            'qry += " and Deduction_lower_range='" & txtDeductionLRange.Text & "' and Deduction_upper_range='" & txtDeductionURange.Text & "' and Deduction_Per='" & txtDeductionRatio.Text & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved = True Then
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Delete Successfully")
                Me.Close()
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MyLabel9_Click(sender As Object, e As EventArgs) Handles MyLabel9.Click

    End Sub
End Class

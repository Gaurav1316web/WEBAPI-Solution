Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Public Class frmDA_Arrear
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmDA_Arrear
        MyBase.SetUserMgmt(clsUserMgtCode.frmDA_Arrear)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmDA_Arrear_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateTab()
        AddNew()
    End Sub
    Private Sub AddNew()
        isNewEntry = True
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtArrearDate.Value = txtDate.Value
        txtLocationCode.Value = ""
        lblLocationDesc.Text = ""
        txtEmpCode.arrValueMember = Nothing
        txtEmpCode.arrValueMember = Nothing
        txtDAper.Text = ""
        txtPeriodFrom.Value = txtDate.Value
        txtPeriodTo.Value = txtDate.Value
        txtPayPeriod.Value = ""
        lblPayPeriod.Text = ""
        chkApplyLeaveIncashment.Checked = False
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DA_ARREAR where Document_Code='" + txtDocNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_DA_ARREAR.Document_Code as DocumentCode,convert(varchar(12),TSPL_DA_ARREAR.Document_date,103) as DocumentDate,TSPL_DA_ARREAR.Arrear_Date,TSPL_DA_ARREAR.Location_Code from TSPL_DA_ARREAR "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("DAArreardocfnd", qry, "DocumentCode", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DA_ARREAR.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocationCode._MYValidating
        Try
            Dim strQuery As String = "select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            txtLocationCode.Value = clsCommon.ShowSelectForm("DALocation", strQuery, "Code", WhrCls, txtLocationCode.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocationCode.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayPeriod._MYValidating
        Try
            Dim strQuery As String = "select PAY_PERIOD_CODE as Code,PAY_PERIOD_NAME as Name,DESCRIPTION,DATE_FROM as [From Date],DATE_TO as [Date To] from TSPL_PAYPERIOD_MASTER"
            txtPayPeriod.Value = clsCommon.ShowSelectForm("PayperiodSearch", strQuery, "Code", "POSTED='1'", txtPayPeriod.Value, "Code", isButtonClicked)
            lblPayPeriod.Text = clsDBFuncationality.getSingleValue("select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & txtPayPeriod.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtEmpCode__My_Click(sender As Object, e As EventArgs) Handles txtEmpCode._My_Click
        Try
            Dim qry As String = " select EMP_CODE as Code,Emp_Name as Name,Designation  from TSPL_EMPLOYEE_MASTER where Emp_Status='Active' "
            txtEmpCode.arrValueMember = clsCommon.ShowMultipleSelectForm("mulEmpSearch", qry, "Code", "Code", txtEmpCode.arrValueMember, txtEmpCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CreateTab()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("Document_Code", "Varchar(30) not null  PRIMARY KEY")
            coll.Add("Document_Date", "datetime not NULL")
            coll.Add("Arrear_Date", "datetime not NULL")
            coll.Add("PeriodFrom_Date", "datetime not NULL")
            coll.Add("PeriodTo_Date", "datetime not NULL")
            coll.Add("Location_Code", "VARCHAR(12) NULL references TSPL_LOCATION_MASTER(Location_Code)")
            coll.Add("DA_Per", "varchar(12) NOT NULL")
            coll.Add("IsApplyLeaveIncashment", "int NOT NULL default 0")
            coll.Add("PAY_PERIOD_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_PAYPERIOD_MASTER(PAY_PERIOD_CODE)")
            coll.Add("Created_By", "varchar(12) NOT NULL")
            coll.Add("Created_Date", "Datetime NOT NULL")
            coll.Add("Modified_By", "varchar(12) NOT NULL")
            coll.Add("Modified_Date", "Datetime NOT NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DA_Arrear", coll, "", True, False, "", "Document_Code", "Document_Date", True)
            coll = New Dictionary(Of String, String)()
            coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
            coll.Add("Document_Code", "Varchar(30) not null  REFERENCES TSPL_DA_Arrear(Document_Code)")
            coll.Add("EMP_CODE", "VARCHAR(12) NULL REFERENCES TSPL_EMPLOYEE_MASTER(EMP_CODE)")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DA_Arrear_Employee", coll, "", True, False, "TSPL_DA_Arrear", "Document_Code", "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtLocationCode.Value) <= 0 Then
                Throw New Exception("Please select Locaiton")
            End If
            If clsCommon.myLen(txtPayPeriod.Value) <= 0 Then
                Throw New Exception("Please select Pay Period")
            End If
            If clsCommon.myCdbl(txtDAper.Text) <= 0 Then
                Throw New Exception("Please Enter DA percentage must be > 0")
            End If
            If clsCommon.myCDate(txtPeriodFrom.Value, "dd/MMM/yyyy") > clsCommon.myCDate(txtPeriodTo.Value, "dd/MMM/yyyy") Then
                Throw New Exception("'Period From date' Cann't Be Greater Than 'Period To Date'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsDA_Arrear()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Arrear_Date = txtArrearDate.Value
                obj.PeriodFrom_Date = txtPeriodFrom.Value
                obj.PeriodTo_Date = txtPeriodTo.Value
                obj.Location_Code = txtLocationCode.Value
                obj.PAY_PERIOD_CODE = txtPayPeriod.Value
                obj.DA_Per = clsCommon.myCdbl(txtDAper.Text)
                obj.IsApplyLeaveIncashment = IIf(chkApplyLeaveIncashment.Checked, 1, 0)
                If txtEmpCode.arrValueMember Is Nothing Then
                    Dim strItems As String = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where Emp_Status='Active'  "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strItems)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Emp_Code = New ArrayList()
                        For Each dr As DataRow In dt.Rows
                            obj.Emp_Code.Add(clsCommon.myCstr(dr("EMP_CODE")))
                        Next
                    End If
                Else
                    obj.Emp_Code = txtEmpCode.arrValueMember
                End If
                If (obj.SaveData(obj, isNewEntry)) = True Then
                    clsCommon.MyMessageBoxShow(Me, "Data Save Successfully ", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsDA_Arrear
            obj = clsDA_Arrear.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                AddNew()
                isNewEntry = False
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtArrearDate.Value = obj.Arrear_Date
                txtPeriodFrom.Value = obj.PeriodFrom_Date
                txtPeriodTo.Value = obj.PeriodTo_Date
                txtLocationCode.Value = obj.Location_Code
                txtPayPeriod.Value = obj.PAY_PERIOD_CODE
                txtDAper.Text = obj.DA_Per
                chkApplyLeaveIncashment.Checked = IIf(obj.IsApplyLeaveIncashment = 1, True, False)
                txtEmpCode.arrValueMember = obj.Emp_Code
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim Reason As String = ""
                If (myMessages.deleteConfirm()) Then
                    If clsCancelLog.CheckForReasonOnDelete() Then
                        '' REASON FOR DELETE 
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Delete"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                    End If
                    If clsDA_Arrear.DeleteData(txtDocNo.Value) Then
                        saveCancelLog(Reason, "Delete", Nothing)
                        clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        AddNew()
                    End If
                End If
            Else
                Throw New Exception("Please Select Document")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
End Class
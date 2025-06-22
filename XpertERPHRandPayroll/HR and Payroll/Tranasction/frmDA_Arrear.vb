Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmDA_Arrear
    Inherits FrmMainTranScreen
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

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try

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
            Dim qry As String = " select EMP_CODE as Code,Emp_Name as Name,Designation  from TSPL_EMPLOYEE_MASTER "
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
            coll.Add("PreiodFrom_Date", "datetime not NULL")
            coll.Add("PreiodTo_Date", "datetime not NULL")
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
            coll.Add("Document_Code", "Varchar(30) not null  PRIMARY KEY")
            coll.Add("EMP_CODE", "VARCHAR(12) NULL REFERENCES TSPL_EMPLOYEE_MASTER(EMP_CODE)")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DA_Arrear_Employee", coll, "", True, False, "TSPL_DA_Arrear", "Document_Code", "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub AllowToSave()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub
End Class
'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmLabelPrinting
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub frmLabelPrinting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        Qry = ""
        Qry = " SELECT DISTINCT EMP_CODE, Emp_Name FROM TSPL_EMPLOYEE_MASTER "
        'Qry += " where Emp_Status= 'Active' "
        Qry += " ORDER BY EMP_CODE "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLabelPrinting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti 23-July-2014 (BM00000003141)
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub PrintData()
        Try
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
                Return
            End If
            Dim Qry As String = ""
            Qry = ""

            Qry += " SELECT  EMP_CODE as Emp_Id, Emp_Name,[FATHERS_NAME],"
            If rdoPresentAddress.Checked Then
                Qry += " CONVERT (VARCHAR(MAX),isnull(TSPL_EMPLOYEE_MASTER.[Add1],'')+ isnull((SELECT  City_Name FROM TSPL_CITY_MASTER WHERE City_Code= [PRESENT_CITY_CODE]),'') + ' '+isnull((SELECT STATE_NAME  FROM TSPL_STATE_MASTER WHERE STATE_CODE =[PRESENT_STATE_CODE]),'')+ ' '+isnull((SELECT COUNTRY_NAME  FROM TSPL_COUNTRY_MASTER  WHERE COUNTRY_CODE  =[PRESENT_COUNTRY_CODE]),'')+' ' +isnull([Pin_Code],'')) AS 'Address',TSPL_EMPLOYEE_MASTER.[Phone] as 'Phone No' , [PRESENT_MOBILE_NO] AS 'Mobile No',"
            Else
                Qry += " CONVERT (VARCHAR(MAX),isnull(TSPL_EMPLOYEE_MASTER.[Add2],'')+ ' ' + isnull((SELECT  City_Name FROM TSPL_CITY_MASTER WHERE City_Code= [PERMA_CITY_CODE]),'')+' '+isnull((SELECT STATE_NAME  FROM TSPL_STATE_MASTER WHERE STATE_CODE =[PERMA_STATE_CODE]),'')+' '+isnull((SELECT COUNTRY_NAME  FROM TSPL_COUNTRY_MASTER  WHERE COUNTRY_CODE  =[PERMA_COUNTRY_CODE]),'')+' '+isnull([PERMA_PIN_CODE],'')) as 'Address',[PERMA_PHONE_NO] as 'Phone no' ,[PERMA_MOBILE_NO] as 'Mobile No' ,"
            End If
            Qry += "  TSPL_EMPLOYEE_MASTER.[EMail_ID] as 'Email Id' "
            Qry += "  FROM TSPL_EMPLOYEE_MASTER "
            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " WHERE EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, DT, "crptLabelPrinting", "Employee Id Card")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

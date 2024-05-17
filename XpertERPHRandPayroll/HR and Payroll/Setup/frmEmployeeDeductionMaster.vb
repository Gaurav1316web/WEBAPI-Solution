Imports common
Imports XpertERPEngine

Public Class frmEmployeeDeductionMaster


    Private Sub txtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "' and Emp_Status<>'Inactive'"
                End If
            Else
                whrcls += " Emp_Status<>'Inactive'"
            End If

            Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name,TSPL_EMPLOYEE_MASTER.PF_NO as [PF No] FROM TSPL_EMPLOYEE_MASTER "
            txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER1", qry, "Code", whrcls, txtEmpCode.Value, "", isButtonClicked)
            Dim clsemp As clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
            If Not clsemp Is Nothing Then
                lblEmpName.Text = clsemp.Emp_Name
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
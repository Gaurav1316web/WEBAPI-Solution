Imports common
Imports System.Data.SqlClient
Public Class FrmLineMaster
    Inherits FrmMainTranScreen
    Public isLoadData As Boolean = False
    Dim obj As clsLineMaster = Nothing
    Dim isPostData As Integer = 0
    Dim isNewEntry As Boolean = True
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub FrmLineMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                                              "TSPL_LINE_MASTER ")
        End If
    End Sub
    ' Ticket No : BHA/02/07/18-000117 By Prabhakar Create New Screen
    Private Sub FrmLineMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub
    ' Select LINE_NO,MACHINE_NAME,MACHINE_RATED,CAPACITY,TIME_FRAME from TSPL_LINE_MASTER
    Sub Reset()
        fndLine.Value = ""
        txtMachineName.Text = ""
        txtMachineRated.Text = ""
        txtCapacity.Text = ""
        txtTimeFrame.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnSave.Text = "Save"
        fndLine.MyReadOnly = False
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndLine.Value) <= 0 Then
                Throw New Exception("Line No Can't left blank")
            End If
            If clsCommon.myLen(txtMachineName.Text) <= 0 Then
                Throw New Exception("Machine No Can't left blank")
            End If
            If clsCommon.myLen(txtMachineRated.Text) <= 0 Then
                Throw New Exception("Machine Rated Can't left blank")
            End If
            If clsCommon.myLen(txtCapacity.Text) <= 0 Then
                Throw New Exception("Capacity Can't left blank")
            End If
            If clsCommon.myLen(txtTimeFrame.Text) <= 0 Then
                Throw New Exception("Time Frame Can't left blank")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Sub SaveData()
        Try
            obj = New clsLineMaster
            'If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
            '    obj.isNewEntry = True
            'Else
            '    obj.isNewEntry = False
            'End If
            Dim dt As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy hh:mm tt")
            'LINE_NO , MACHINE_NAME,  MACHINE_RATED , CAPACITY , TIME_FRAME

            obj.LINE_NO = clsCommon.myCstr(fndLine.Value)
            Dim isNewEntry As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from tspl_line_master where line_no = '" + obj.LINE_NO + "'"))
            If isNewEntry > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
            End If
            obj.MACHINE_NAME = clsCommon.myCstr(txtMachineName.Text)
            obj.MACHINE_RATED = clsCommon.myCstr(txtMachineRated.Text)
            obj.CAPACITY = clsCommon.myCstr(txtCapacity.Text)
            obj.TIME_FRAME = clsCommon.myCstr(txtTimeFrame.Text)
            'obj.Modify_By = objCommonVar.CurrentUserCode
            'obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm tt")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
           
            If clsLineMaster.saveData(obj, isNewEntry) Then
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully")
                End If
                loadLineNoData(obj.LINE_NO, NavigatorType.Current)
                btnSave.Text = "Update"
                fndLine.MyReadOnly = True
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Update"
            btnDelete.Enabled = False
            fndLine.MyReadOnly = False
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Sub loadLineNoData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsLineMaster.getLineNoData(str, navtype)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LINE_NO) > 0) Then
            Reset()
            isLoadData = True
            fndLine.Value = obj.LINE_NO
            txtMachineName.Text = obj.MACHINE_NAME
            txtMachineRated.Text = obj.MACHINE_RATED
            txtCapacity.Text = obj.CAPACITY
            txtTimeFrame.Text = obj.TIME_FRAME
        End If
        isLoadData = False
        fndLine.MyReadOnly = True
        btnSave.Text = "Update"
        btnDelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub
    Sub deleteData()

        Try
            If clsCommon.myLen(fndLine.Value) <= 0 Then
                Throw New Exception("No document found to Delete")
            End If

            ' If myMessages.deleteConfirm() Then
            clsLineMaster.deleteData(fndLine.Value)
            myMessages.delete()
            Reset()

            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'Try
        '    If clsCommon.myLen(fndLine.Value) > 0 Then

        '        If clsLineMaster.deleteData(fndLine.Value, trans) Then
        '            myMessages.delete()
        '            trans.Commit()
        '            Reset()
        '        Else
        '            clsCommon.MyMessageBoxShow("Can't delete the record")
        '            trans.Rollback()
        '        End If
        '    Else

        '        clsCommon.MyMessageBoxShow("Please Select a Line No to delete")
        '        trans.Rollback()
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        '    trans.Rollback()
        'End Try
    End Sub

   
   
    Private Sub fndLine__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLine._MYValidating
        Dim qry As String = "  select TSPL_LINE_MASTER.LINE_NO ,TSPL_LINE_MASTER.MACHINE_NAME as [MACHINE NAME],TSPL_LINE_MASTER.MACHINE_RATED as [MACHINE RATED],TSPL_LINE_MASTER.CAPACITY,TSPL_LINE_MASTER.TIME_FRAME as [TIME FRAME] From TSPL_LINE_MASTER "
        Dim chkLine As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LINE_MASTER where LINE_NO = '" + fndLine.Value + "' "))
        If chkLine > 0 Or isButtonClicked = True Then
            loadLineNoData(clsCommon.ShowSelectForm("Line@Finder", qry, "LINE_NO", "", fndLine.Value, "LINE_NO", isButtonClicked), NavigatorType.Current)
        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub fndLine__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndLine._MYNavigator
        loadLineNoData(fndLine.Value, NavType)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim qry As String = "Select LINE_NO as [LINE NO] ,MACHINE_NAME as [MACHINE NAME] ,MACHINE_RATED as [MACHINE RATED],CAPACITY,TIME_FRAME as [TIME FRAME] from TSPL_LINE_MASTER"
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsLineMaster = New clsLineMaster()
        If transportSql.importExcel(gv, "LINE NO", "MACHINE NAME", "MACHINE RATED", "CAPACITY", "TIME FRAME") Then
            Try
                Dim counter As Integer = 1
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim lineNo As String = ""
                    Dim machineName As String = ""
                    Dim machineRated As String = ""
                    Dim capacity As String = ""
                    Dim timeframe As String = ""
                    Dim qry As String = ""

                    lineNo = clsCommon.myCstr(grow.Cells("LINE NO").Value).Replace("'", "`")
                    If clsCommon.myLen(lineNo) <= 0 Then
                        Throw New Exception("Please Fill Line No At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    Dim checkEntry As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from tspl_line_master where line_no = '" + lineNo + "'", trans))
                    If checkEntry > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    ' isNewEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from tspl_line_master where line_no = '" + lineNo + "'", trans))
                    If clsCommon.myLen(lineNo) > 30 Then
                        Throw New Exception("Length of Line No Should Not Exceed Max.30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    machineName = clsCommon.myCstr(grow.Cells("MACHINE NAME").Value).Replace("'", "`")
                    If clsCommon.myLen(machineName) <= 0 Then
                        Throw New Exception("Please Fill Machine Name At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    If clsCommon.myLen(machineName) > 200 Then
                        Throw New Exception("Length of Machine Name Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    machineRated = clsCommon.myCstr(grow.Cells("MACHINE RATED").Value).Replace("'", "`")

                    If clsCommon.myLen(machineRated) <= 0 Then
                        Throw New Exception("Please Fill Machine Rated  At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    capacity = clsCommon.myCstr(grow.Cells("CAPACITY").Value).Replace("'", "`")
                    If clsCommon.myLen(machineRated) <= 0 Then
                        Throw New Exception("Please Fill Capacity At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    timeframe = clsCommon.myCstr(grow.Cells("TIME FRAME").Value).Replace("'", "`")
                    If clsCommon.myLen(machineRated) <= 0 Then
                        Throw New Exception("Please Fill Time Frame At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    obj = New clsLineMaster()
                    obj.LINE_NO = lineNo
                    obj.MACHINE_NAME = machineName
                    obj.MACHINE_RATED = machineName
                    obj.MACHINE_RATED = machineRated
                    obj.CAPACITY = capacity
                    obj.TIME_FRAME = timeframe
                    If clsLineMaster.SaveData(obj, isNewEntry, trans) Then
                        counter += 1
                    End If

                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)

                trans.Commit()
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message)
                trans.Rollback()
            Finally
                clsCommon.ProgressBarHide()
                Reset()
                obj = Nothing
            End Try
        End If

        Me.Controls.Remove(gv)
    End Sub
End Class

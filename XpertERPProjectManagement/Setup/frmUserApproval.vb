Imports common
Imports System.Data.SqlClient

Public Class FrmUserApproval
    Inherits FrmMainTranScreen

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colUserCode As String = "colUserCode"
    Const colCreate As String = "colCreate"
    Const colOpen As String = "colOpen"
    Const colApprove As String = "colApprove"
    Const colOnhold As String = "colOnhold"
    Const colClose As String = "colClose"
    Const colComplete As String = "colComplete"
    Const colInactive As String = "colInactive"
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmUserApproval)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmUserApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        funFillGrid()
        btndelete.Visible = False
    End Sub

    Sub LoadBlankGrid()
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim colUser As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        colUser.FormatString = ""
        colUser.HeaderText = "User"
        colUser.Name = colUserCode
        colUser.Width = 100
        colUser.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(colUser)

        Dim gvCreate As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvCreate.FormatString = ""
        gvCreate.Name = colCreate
        gvCreate.HeaderText = "Create"
        gvCreate.Width = 70
        gvCreate.ReadOnly = False
        gvCreate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvCreate)

        Dim gvOpen As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvOpen.FormatString = ""
        gvOpen.Name = colOpen
        gvOpen.HeaderText = "Open"
        gvOpen.Width = 70
        gvOpen.ReadOnly = False
        gvOpen.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvOpen)

        Dim gvApprove As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvApprove.FormatString = ""
        gvApprove.Name = colApprove
        gvApprove.HeaderText = "Approve"
        gvApprove.Width = 70
        gvApprove.ReadOnly = False
        gvApprove.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvApprove)

        Dim gvOnhold As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvOnhold.FormatString = ""
        gvOnhold.Name = colOnhold
        gvOnhold.HeaderText = "OnHold"
        gvOnhold.Width = 70
        gvOnhold.ReadOnly = False
        gvOnhold.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvOnhold)


        Dim gvClose As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvClose.FormatString = ""
        gvClose.Name = colClose
        gvClose.HeaderText = "Close"
        gvClose.Width = 70
        gvClose.ReadOnly = False
        gvClose.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvClose)

        Dim gvComlpete As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvComlpete.FormatString = ""
        gvComlpete.Name = colComplete
        gvComlpete.HeaderText = "Complete"
        gvComlpete.Width = 70
        gvComlpete.ReadOnly = False
        gvComlpete.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvComlpete)

        Dim gvInactive As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvInactive.FormatString = ""
        gvInactive.Name = colInactive
        gvInactive.HeaderText = "Inactive"
        gvInactive.Width = 70
        gvInactive.ReadOnly = False
        gvInactive.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvInactive)


        Gv1.AllowAddNewRow = True
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub funFillGrid()
        Try
            Dim strQuery As String
            Dim dt As DataTable
            LoadBlankGrid()
            isInsideLoadData = True
            strQuery = "select USER_CODE,Created,Approved,Opened,OnHold,Closed,Completed,Inactive from tspl_user_approval "
            '"union all " & _
            '"select USER_CODE,'N' as Created,'N' as Approved,'N' as Opened,'N' as OnHold,'N' as Closed,'N' as Completed, " & _
            '"'N' as Inactive from TSPL_USER_MASTER where User_Code not in (select USER_CODE from TSPL_USER_APPROVAL)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUserCode).Value = clsCommon.myCstr(dr("USER_CODE"))
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Created")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCreate).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCreate).Value = False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Opened")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOpen).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOpen).Value = False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Approved")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colApprove).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colApprove).Value = False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("OnHold")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOnhold).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOnhold).Value = False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Closed")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colClose).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colClose).Value = False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Completed")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colComplete).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colComplete).Value = False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Inactive")), "Y") = CompairStringResult.Equal Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colInactive).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colInactive).Value = False
                    End If

                Next
            End If

            isInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "User Approval", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try

            If Gv1.Rows.Count > 0 Then
                Dim Arr As New List(Of clsUserApproval)
                For ii As Integer = 0 To Gv1.RowCount - 1
                    Dim obj As New clsUserApproval()
                    obj.USER_CODE = clsCommon.myCstr(Gv1.Rows(ii).Cells(colUserCode).Value)
                    obj.Created = IIf(clsCommon.myCstr(Gv1.Rows(ii).Cells(colCreate).Value), "Y", "N")
                    obj.Opened = IIf(clsCommon.myCstr(Gv1.Rows(ii).Cells(colOpen).Value), "Y", "N")
                    obj.Approved = IIf(clsCommon.myCstr(Gv1.Rows(ii).Cells(colApprove).Value), "Y", "N")
                    obj.OnHold = IIf(clsCommon.myCstr(Gv1.Rows(ii).Cells(colOnhold).Value), "Y", "N")
                    obj.Closed = IIf(clsCommon.myCBool(Gv1.Rows(ii).Cells(colClose).Value), "Y", "N")
                    obj.Completed = IIf(clsCommon.myCBool(Gv1.Rows(ii).Cells(colComplete).Value), "Y", "N")
                    obj.Inactive = IIf(clsCommon.myCBool(Gv1.Rows(ii).Cells(colInactive).Value), "Y", "N")
                    Arr.Add(obj)

                Next
                clsUserApproval.SaveData(Arr)

            Else
                common.clsCommon.MyMessageBoxShow("No data found to save")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click

    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub Gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            Dim strCode, strtemp, strUser As String
            strUser = ""
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is Gv1.Columns(colUserCode) Then
                        Dim qry As String = "select User_Code,User_Name from TSPL_USER_MASTER "

                        For ii As Integer = 0 To Gv1.RowCount - 1
                            strtemp = clsCommon.myCstr(Gv1.Rows(ii).Cells(colUserCode).Value)
                            If clsCommon.myLen(strtemp) > 0 Then
                                strtemp += "',"
                            End If
                            strtemp = "'" & strtemp
                            strUser += strtemp

                        Next

                        If strUser = "" Then
                            strUser = "''"
                        Else
                            strUser = strUser.Substring(0, strUser.Length - 1)
                        End If
                        Gv1.CurrentRow.Cells(colUserCode).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("User", qry, "USER_CODE", "User_Code not in (" & strUser & ")", Gv1.CurrentRow.Cells(colUserCode).Value, "", False))
                        strCode = Gv1.CurrentRow.Cells(colUserCode).Value
                        If clsCommon.myLen(strCode) > 0 Then
                            qry = "select USER_CODE,Created,Approved,Opened,OnHold,Closed,Completed,Inactive from tspl_user_approval where USER_CODE='" & strCode & "' "
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Created")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colCreate).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colCreate).Value = False
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Approved")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colOpen).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colOpen).Value = False
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Opened")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colApprove).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colApprove).Value = False
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("OnHold")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colOnhold).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colOnhold).Value = False
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Closed")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colClose).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colClose).Value = False
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Completed")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colComplete).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colComplete).Value = False
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Inactive")), "Y") = CompairStringResult.Equal Then
                                    Gv1.CurrentRow.Cells(colInactive).Value = True
                                Else
                                    Gv1.CurrentRow.Cells(colInactive).Value = False
                                End If
                                'Else
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCreate).Value = False
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colApprove).Value = False
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOpen).Value = False
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colComplete).Value = False
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colClose).Value = False
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOnhold).Value = False
                                '    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colInactive).Value = False
                            End If
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class

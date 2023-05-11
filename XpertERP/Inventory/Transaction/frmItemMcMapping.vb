
'........created by Usha Reddy.......
'...........20/7/2012......
Imports common


Public Class FrmItemMcMapping
    Inherits FrmMainTranScreen

    Const colICode As String = "COLICODE"
    Const colIName As String = "INAME"
    Const colDMC As String = "DMC"
    Const colVMOH As String = "VMOH"
    Const colRoyality As String = "ROYALTY"
    Const colPFreight As String = "PRIMARYFREIGHT"
    Const colSFreight As String = "SECONDARYFREIGHT"
    Const colVSDRouter As String = "ROUTERHELPER"
    Const colVSDLoULo As String = "LOADINGUNLOADING"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isformLoad As Boolean = False



    Private Sub FrmItemMcMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        LoadData()
        gv1.Rows.AddNew()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmItemMcMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag

    End Sub
    Sub LoadBlankGrid()
        'gv1.AddNewRowPosition = SystemRowPosition.Bottom
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)


        Dim repodmc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repodmc = New GridViewDecimalColumn()
        repodmc.FormatString = ""
        repodmc.HeaderText = "DMC"
        repodmc.Name = colDMC
        repodmc.ShowUpDownButtons = False
        repodmc.Width = 50
        repodmc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repodmc)

        Dim repovmoh As GridViewDecimalColumn = New GridViewDecimalColumn()
        repovmoh = New GridViewDecimalColumn()
        repovmoh.FormatString = ""
        repovmoh.HeaderText = "VMOH"
        repovmoh.Name = colVMOH
        repovmoh.ShowUpDownButtons = False
        repovmoh.Width = 50
        repovmoh.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repovmoh)

        Dim reporoyalty As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporoyalty = New GridViewDecimalColumn()
        reporoyalty.FormatString = ""
        reporoyalty.HeaderText = "ROYALTY"
        reporoyalty.Name = colRoyality
        reporoyalty.ShowUpDownButtons = False
        reporoyalty.Width = 100
        reporoyalty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(reporoyalty)

        Dim repopfreight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopfreight = New GridViewDecimalColumn()
        repopfreight.FormatString = ""
        repopfreight.HeaderText = "Primary Freight"
        repopfreight.Name = colPFreight
        repopfreight.ShowUpDownButtons = False
        repopfreight.Width = 100
        repopfreight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopfreight)


        Dim reposfreight As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposfreight = New GridViewDecimalColumn()
        reposfreight.FormatString = ""
        reposfreight.HeaderText = "Secondary Freight"
        reposfreight.Name = colSFreight
        reposfreight.ShowUpDownButtons = False
        reposfreight.Width = 100
        reposfreight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(reposfreight)

        Dim reporouter As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporouter = New GridViewDecimalColumn()
        reporouter.FormatString = ""
        reporouter.HeaderText = "VS&D Router Helper"
        reporouter.Name = colVSDRouter
        reporouter.WrapText = True
        reporouter.ShowUpDownButtons = False
        reporouter.Width = 100
        reporouter.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(reporouter)

        Dim repoloading As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoloading = New GridViewDecimalColumn()
        repoloading.FormatString = ""
        repoloading.HeaderText = "VS&D Loading & Unloading"
        repoloading.Name = colVSDLoULo
        repoloading.ShowUpDownButtons = False
        repoloading.WrapText = True
        repoloading.Width = 100
        repoloading.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoloading)

        gv1.MasterTemplate.AllowAddNewRow = False
        gv1.MasterTemplate.AllowDeleteRow = True

    End Sub
    Private Sub setGridFocus()
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If

    End Sub


    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Public Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim str As String = gv1.Rows(ii).Cells(colICode).Value
            For jj As Integer = 0 To gv1.Rows.Count - 1
                If (ii = jj) Then
                    Continue For
                End If
                If (clsCommon.CompairString(str, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal) Then

                    common.clsCommon.MyMessageBoxShow("item code " + str + " Exist at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1))
                    Return False
                End If

            Next
        Next
        Return True
    End Function

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()

    End Sub
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim objtr As New ClsItemMcMapping()

                Dim Arr As New List(Of ClsItemMcMapping)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim obj As New ClsItemMcMapping()

                    'For ii As Integer = 0 To gv1.Rows.Count - 1
                    obj.itemcode = clsCommon.myCstr(grow.Cells(colICode).Value)
                    obj.name = clsCommon.myCstr(grow.Cells(colIName).Value)
                    obj.dmc = clsCommon.myCdbl(grow.Cells(colDMC).Value)
                    obj.vmoh = clsCommon.myCdbl(grow.Cells(colVMOH).Value)
                    obj.royality = clsCommon.myCdbl(grow.Cells(colRoyality).Value)
                    obj.pfreight = clsCommon.myCdbl(grow.Cells(colPFreight).Value)
                    obj.sfreight = clsCommon.myCdbl(grow.Cells(colSFreight).Value)
                    obj.vsdrouter = clsCommon.myCdbl(grow.Cells(colVSDRouter).Value)
                    obj.vsdLoUnlo = clsCommon.myCdbl(grow.Cells(colVSDLoULo).Value)

                    'obj.itemcode = gv1.Rows(ii).Cells(colICode).Value
                    'obj.name = gv1.Rows(ii).Cells(colIName).Value
                    'obj.dmc = gv1.Rows(ii).Cells(colDMC).Value
                    'obj.vmoh = gv1.Rows(ii).Cells(colVMOH).Value
                    'obj.royality = gv1.Rows(ii).Cells(colRoyality).Value
                    'obj.pfreight = gv1.Rows(ii).Cells(colPFreight).Value
                    'obj.sfreight = gv1.Rows(ii).Cells(colSFreight).Value
                    'obj.vsdrouter = gv1.Rows(ii).Cells(colVSDRouter).Value
                    'obj.vsdLoUnlo = gv1.Rows(ii).Cells(colVSDLoULo).Value
                    If (clsCommon.myLen(obj.itemcode) > 0) Then
                        Arr.Add(obj)
                    End If
                Next
                If btnsave.Text = "Save" Then
                    isNewEntry = True
                End If

                If (objtr.SaveData(Arr, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        setGridFocus()
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If Not isformLoad Then
            If e.Column Is gv1.Columns(colICode) Then
                Dim qry As String = "select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER"
                gv1.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("mcitemMaster", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", False)
                qry = "SELECT Item_Desc from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Active=1 and Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                End If
            End If
        End If
    End Sub

    Sub LoadData()
        Try
            isformLoad = True
            ' LoadBlankGrid()

            Dim Arr As List(Of ClsItemMcMapping) = ClsItemMcMapping.GetData()
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As ClsItemMcMapping In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.itemcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMC).Value = objTr.dmc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVMOH).Value = objTr.vmoh
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRoyality).Value = objTr.royality
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPFreight).Value = objTr.pfreight
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSFreight).Value = objTr.sfreight
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVSDRouter).Value = objTr.vsdrouter
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVSDLoULo).Value = objTr.vsdLoUnlo

                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        isformLoad = False
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
End Class

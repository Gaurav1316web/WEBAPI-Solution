Imports common
Imports System.Data.SqlClient

Public Class FrmPurchaseOrderAuto
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variables"
    Dim dt As DataTable
    Dim qry As String
#End Region

    Private Sub FrmPurchaseOrderAuto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ItemReorderLevel)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean
        'If clsCommon.myLen(txtCategory.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select Category Code")
        '    txtCategory.Focus()
        '    Return False
        'End If
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim Arr As New List(Of clsItemReorderLevel)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim obj As New clsItemReorderLevel()
                    obj.Apply = IIf(grow.Cells("Apply").Value = True, "Y", "N")
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    obj.Item_Description = clsCommon.myCstr(grow.Cells("Item_Desc").Value)
                    obj.Min_Level = clsCommon.myCdbl(grow.Cells("Min_Level").Value)
                    obj.Min_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Min_Level_Tollerence").Value)
                    obj.Max_Level = clsCommon.myCdbl(grow.Cells("Max_Level").Value)
                    obj.Max_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Max_Level_Tollerence").Value)
                    obj.Reorder_Level = clsCommon.myCdbl(grow.Cells("Reorder_Level").Value)
                    obj.Reorder_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Reorder_Level_Tollerence").Value)
                    Arr.Add(obj)
                Next
                If (clsItemReorderLevel.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(clsCommon.myCstr(txtCategory.Value))
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndcategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        qry = "select ITEM_CATEGORY_CODE as Code,DESCRIPTION,CATEGORY_LEVEL as Level from TSPL_ITEM_CATEGORY_LEVEL"
        txtCategory.Value = clsCommon.ShowSelectForm("CATREORDERLVL", qry, "Code", "", txtCategory.Value, "Code", isButtonClicked)
        txtcatdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE ='" + txtCategory.Value + "'"))
        LoadData(clsCommon.myCstr(txtCategory.Value))
    End Sub

    Sub LoadData(ByVal strCategoryCode As String)
        Try
            dt = clsItemReorderLevel.GetData(strCategoryCode)
            gv1.DataSource = dt
            If gv1.Rows.Count > 0 Then
                FormatGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        gv1.Columns("Apply").HeaderText = "Apply"
        gv1.Columns("Apply").Width = 50

        gv1.Columns("Item_Code").HeaderText = "Item Code"
        gv1.Columns("Item_Code").Width = 100

        gv1.Columns("Item_Desc").HeaderText = "Item Description"
        gv1.Columns("Item_Desc").Width = 200

        gv1.Columns("Min_Level").HeaderText = "Min Level"
        gv1.Columns("Min_Level").Width = 100

        gv1.Columns("Min_Level_Tollerence").HeaderText = "Min Level Tollerence"
        gv1.Columns("Min_Level_Tollerence").Width = 110

        gv1.Columns("Max_Level").HeaderText = "Max Level"
        gv1.Columns("Max_Level").Width = 100

        gv1.Columns("Max_Level_Tollerence").HeaderText = "Max Level Tollerence"
        gv1.Columns("Max_Level_Tollerence").Width = 110

        gv1.Columns("Reorder_Level").HeaderText = "Reorder Level"
        gv1.Columns("Reorder_Level").Width = 100

        gv1.Columns("Reorder_Level_Tollerence").HeaderText = "Reorder Level Tollerence"
        gv1.Columns("Reorder_Level_Tollerence").Width = 120
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        txtCategory.Value = ""
        txtcatdesc.Text = ""
        gv1.DataSource = Nothing
    End Sub

    Private Sub frmPurchaseOrderAuto_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub


    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            qry = "Select TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code, TSPL_ITEM_MASTER.Item_Desc, Min_Level, Min_Level_Tollerence, Max_Level, Max_Level_Tollerence, Reorder_Level, Reorder_Level_Tollerence " & _
            " from TSPL_ITEM_REORDER_LEVEL_NEW" & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code WHERE Apply='Y'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                qry = "Select '' As Item_Code, '' as Item_Desc, '' As Min_Level, 0 as Min_Level_Tollerence, 0 as Max_Level, 0 as Max_Level_Tollerence, 0 as Reorder_Level, 0 as Reorder_Level_Tollerence"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ImportItems()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ImportItems()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Item_Code", "Item_Desc", "Min_Level", "Min_Level_Tollerence", "Max_Level", "Max_Level_Tollerence", "Reorder_Level", "Reorder_Level_Tollerence") Then
            clsCommon.ProgressBarShow()
            ' Dim trans As SqlTransaction
            Try
                Dim Item_Code As String
                Dim Arr As New List(Of clsItemReorderLevel)
                Dim LineNo As String
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    Dim obj As New clsItemReorderLevel()
                    obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER WHERE Item_Code='" + Item_Code + "'")
                    If clsCommon.CompairString(obj.Item_Code, Item_Code) <> CompairStringResult.Equal Then
                        Throw New Exception("Item Code at line '" + LineNo + "' does not exist.")
                    End If
                    obj.Apply = "Y"
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    obj.Min_Level = clsCommon.myCdbl(grow.Cells("Min_Level").Value)
                    obj.Min_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Min_Level_Tollerence").Value)
                    obj.Max_Level = clsCommon.myCdbl(grow.Cells("Max_Level").Value)
                    obj.Max_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Max_Level_Tollerence").Value)
                    obj.Reorder_Level = clsCommon.myCdbl(grow.Cells("Reorder_Level").Value)
                    obj.Reorder_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Reorder_Level_Tollerence").Value)
                    Arr.Add(obj)
                Next
                If (clsItemReorderLevel.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
End Class

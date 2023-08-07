''Created By----Sanjay

Imports common
Imports System.Data.SqlClient



Public Class frmDisplaySquenece
    Inherits FrmMainTranScreen
    Dim dt1 As DataTable = New DataTable()
    Dim qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As New ToolTip()
    Dim dr As DataRow
    Dim isInsideLoad As Boolean = False
    Dim StrQuery As String = Nothing
    Dim IsInsideLoadData As Boolean = True
    Dim Prev As Integer = 0
    Dim IsInsieLoadData As Boolean
    Dim isSelected As Boolean = True
    Private Sub frmDisplaySquenece_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        SetUserMgmtNew()
        LoadModuleType()

    End Sub



    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isPostFlag
    End Sub

    Public Sub LoadModuleType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Item"
        dr("Name") = "Item"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Customer"
        dr("Name") = "Customer"
        dt.Rows.Add(dr)


        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"

    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Item") = CompairStringResult.Equal Then
            GB_ROUTE.Visible = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Customer") = CompairStringResult.Equal Then
            GB_ROUTE.Visible = True
        End If

    End Sub

       Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        gv1.AllowAddNewRow = False

    End Sub

    ''
    ''Format The GridView
    ''
    Sub gv1Format()

        Me.gv1.MasterTemplate.Columns("Sno").Width = 50      ''First Column
        Me.gv1.MasterTemplate.Columns("Sno").ReadOnly = False
        Me.gv1.MasterTemplate.Columns("Particlar Code").Width = 100    ''Second Column
        Me.gv1.MasterTemplate.Columns("Particlar Code").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Particlar Name").Width = 250    ''Third Column
        Me.gv1.MasterTemplate.Columns("Particlar Name").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Display Demand").Width = 100
        Me.gv1.MasterTemplate.Columns("Display Demand").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Display Demand").IsVisible = True
        If isSelected Then
            Dim checkBoxColumn As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            checkBoxColumn.HeaderText = ""
            checkBoxColumn.Width = 30
            checkBoxColumn.Name = "checkBoxColumn1"
            checkBoxColumn.FieldName = "Display Demand"
            gv1.Columns.Insert(0, checkBoxColumn)
            isSelected = False
        End If

        'gv1.MasterTemplate.Columns.Add(checkBoxColumn)

        'dataGridView1.Columns.Insert(0, checkBoxColumn)

        gv1.AllowAddNewRow = False
        gv1.AllowEditRow = True
        gv1.AllowDeleteRow = False
        gv1.AllowRowResize = False
        gv1.EnableSorting = False
        gv1.AllowRowReorder = False
        gv1.AllowColumnResize = True
        gv1.AllowColumnChooser = False
        gv1.AllowAutoSizeColumns = True
        gv1.ShowGroupPanel = False

    End Sub


#Region "Showing Details on GRID"
    'done by stuti on 18/10/2016 against ticket no - BM00000010089
    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(Purchase Order)---------------
    ''-------------------------------------------------------------------

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
       If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Item") = CompairStringResult.Equal Then
            ShowData()         
       ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Customer") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                Throw New Exception("Please select Route")
                txtRouteNo.Focus()
            End If
            
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                ShowData()   
            End If
                   
        End If
    End Sub

    Sub ShowData()
        Try
            'LoadBlank_Grid()
            FillData()
            gv1Format()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Sub FillData()
        Try
            qry = Nothing
            'gv1.Rows.Clear()
            'gv1.Columns.Clear()
            IsInsieLoadData = True
            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Item") = CompairStringResult.Equal Then
                qry = "SELECT CAST(isnull(TSPL_ITEM_MASTER.Sku_Seq,0) as integer) as Sno ,TSPL_ITEM_MASTER.Item_code [Particlar Code] 
                    ,TSPL_ITEM_MASTER.Alies_Name [Particlar Name],TSPL_ITEM_MASTER.Is_DisplayDemand as [Display Demand] from TSPL_ITEM_MASTER
                     ORDER BY TSPL_ITEM_MASTER.Sku_Seq  "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Customer") = CompairStringResult.Equal Then
                qry = "SELECT CAST(isnull(Display_Seq,0) as integer) as Sno ,tspl_customer_master.cust_code [Particlar Code] 
                        ,tspl_customer_master.Customer_Name [Particlar Name] from tspl_customer_master
                       where tspl_customer_master.route_no='" + txtRouteNo.Value + "'
                         ORDER BY tspl_customer_master.Display_Seq  "
            End If
            If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

                dt = clsDBFuncationality.GetDataTable(qry)
                gv1.DataSource = dt
                For i As Int16 = 0 To gv1.Rows.Count - 1
                    gv1.Rows(i).Cells("Sno").Value = i + 1
                    If gv1.Rows(i).Cells("Display Demand").Value = 1 Then
                        'gv1.Rows(i).Cells("isDisplayDemand").Value = True
                    End If
                Next
            End If
            IsInsieLoadData = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


#End Region



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub


    Private Sub frmDisplaySquenece_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub

     
       Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
        IsInsieLoadData = False
        gv1.DataSource = Nothing
    End Sub
   
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            txtRouteNo.Value = clsCommon.ShowSelectForm("DSeqRouteFin", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRouteNo.Value, Nothing))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            qry = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Item") = CompairStringResult.Equal Then
                For irow As Integer = 0 To gv1.Rows.Count - 1
                    qry = "update TSPL_ITEM_MASTER set Sku_Seq ='" + clsCommon.myCstr(gv1.Rows(irow).Cells("Sno").Value) + "'"
                    qry += ", Is_DisplayDemand='" + clsCommon.myCstr(gv1.Rows(irow).Cells("Display Demand").Value) + "' "
                    'If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(irow).Cells("checkBoxColumn1").Value), "True") = CompairStringResult.Equal Then
                    '    qry += ", Is_DisplayDemand=1 "
                    'Else
                    '    qry += ", Is_DisplayDemand=0 "

                    'End If
                    qry += " where Item_code='" + clsCommon.myCstr(gv1.Rows(irow).Cells("Particlar Code").Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                Next

                common.clsCommon.MyMessageBoxShow("Update Successfully")
                btnShow.PerformClick()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Customer") = CompairStringResult.Equal Then
                For irow As Integer = 0 To gv1.Rows.Count - 1
                    qry = "update tspl_customer_master set Display_Seq ='" + clsCommon.myCstr(gv1.Rows(irow).Cells("Sno").Value) + "' where Cust_code='" + clsCommon.myCstr(gv1.Rows(irow).Cells("Particlar Code").Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                Next

                common.clsCommon.MyMessageBoxShow("Update Successfully")
                btnShow.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
         

        
                   
    End Sub

    Private Sub gv1_CellBeginEdit(sender As Object, e As GridViewCellCancelEventArgs) Handles gv1.CellBeginEdit
         Try
            If gv1.CurrentRow.Cells("Sno").Value IsNot Nothing Then
                Prev = gv1.CurrentRow.Cells("Sno").Value
            End If
            If gv1.CurrentRow.Cells("Sno").Value Is Nothing Then
                Dim lastIndex = gv1.Rows.Count - 1
                Prev = lastIndex + 1
                If lastIndex >= 0 Then
                    gv1.Rows(lastIndex).Cells("Sno").Value = Prev
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
         Try
            If Not IsInsieLoadData Then
                IsInsieLoadData = True
                If e.Column Is gv1.Columns("Sno") Then
                    If gv1.CurrentRow.Index < 0 Then
                        gv1.CurrentRow.Cells("Sno").Value = gv1.RowCount + 1
                    Else
                        Dim CurrSNO As Integer = clsCommon.myCdbl(gv1.CurrentRow.Cells("Sno").Value)
                        If CurrSNO > gv1.RowCount Then
                            gv1.CurrentRow.Cells("Sno").Value = Prev
                        Else
                            For ii As Integer = 0 To gv1.RowCount - 1
                                Dim RunSNO As Integer = clsCommon.myCdbl(gv1.Rows(ii).Cells("Sno").Value)
                                If gv1.CurrentRow.Index = ii Then
                                    Continue For
                                End If
                                If RunSNO >= CurrSNO AndAlso RunSNO <= Prev Then
                                    gv1.Rows(ii).Cells("Sno").Value = clsCommon.myCdbl(gv1.Rows(ii).Cells("Sno").Value) + 1
                                ElseIf RunSNO <= CurrSNO AndAlso RunSNO >= Prev Then
                                    gv1.Rows(ii).Cells("Sno").Value = clsCommon.myCdbl(gv1.Rows(ii).Cells("Sno").Value) - 1

                                End If
                            Next
                        End If
                    End If
                End If
                IsInsieLoadData = False
            End If
        Catch ex As Exception
            IsInsieLoadData = False
        End Try
    End Sub
End Class





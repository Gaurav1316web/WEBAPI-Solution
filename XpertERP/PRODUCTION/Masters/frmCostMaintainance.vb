Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common
Public Class FrmCostMaintainance
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    ''''''''''''''''''''''''''''''''''''''''''Ticket No:BM00000000479''''''''''''''''''''''''''''''''''''''''''''''''Created by Shipra on 13/09/13''''''

#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.COSTMAINTAIN)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 09/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rdbtnsave.Visible = True Then
            RMImport.Enabled = True
            RMExport.Enabled = True
        Else
            RMImport.Enabled = False
            RMExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsCostMaintainance.DeleteData(fndItemCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As ClsCostMaintainance = ClsCostMaintainance.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            'fndItemCode.Value = obj.ITEM_CODE
            txtMaterialCost.Text = clsCommon.myCdbl(obj.MATERIAL_COST)
            txtPackagingCost.Text = clsCommon.myCdbl(obj.PACKAGING_COST)
            txtSetupCost.Text = clsCommon.myCdbl(obj.SETUP_COST)
            txtLabourCost.Text = clsCommon.myCdbl(obj.LABOR_COST)
            txtOverheadCost.Text = clsCommon.myCdbl(obj.OVERHEAD_COST)
            txtSubcontractCost.Text = clsCommon.myCdbl(obj.SUBCONTRACT_COST)
            txtToolCost.Text = clsCommon.myCdbl(obj.TOOL_COST)
            txtTotalStandardCost.Text = clsCommon.myCdbl(obj.TOTAL_COST)
            txtQtyOnHand.Text = clsCommon.myCdbl(obj.QTY_ON_HAND)
            txtAverageCost.Text = clsCommon.myCdbl(obj.AVERAGE_COST)
            fndItemCode.MyReadOnly = True
        End If
    End Sub
    Sub openuom(ByVal isButtonClick As Boolean)
        fnduom.Value = clsItemMaster.FinderForuom(clsCommon.myCstr(fnduom.Value), clsCommon.myCstr(fndItemCode.Value), isButtonClick)
    End Sub
    Sub Reset()
        txtMaterialCost.Text = "0.0"
        txtPackagingCost.Text = "0.0"
        txtSetupCost.Text = "0.0"
        txtLabourCost.Text = "0.0"
        txtOverheadCost.Text = "0.0"
        txtSubcontractCost.Text = "0.0"
        txtToolCost.Text = "0.0"
        txtTotalStandardCost.Text = "0.0"
        txtQtyOnHand.Text = "0.0"
        txtAverageCost.Text = "0.0"
        fndItemCode.Value = ""
        txtdescription.Text = ""
        fnduom.Value = ""
        txtuom.Text = ""
        fndItemCode.MyReadOnly = False
    End Sub
    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New ClsCostMaintainance()
                obj.ITEM_CODE = fndItemCode.Value
                obj.MATERIAL_COST = clsCommon.myCdbl(txtMaterialCost.Text)
                obj.PACKAGING_COST = clsCommon.myCdbl(txtPackagingCost.Text)
                obj.SETUP_COST = clsCommon.myCdbl(txtSetupCost.Text)
                obj.LABOR_COST = clsCommon.myCdbl(txtLabourCost.Text)
                obj.OVERHEAD_COST = clsCommon.myCdbl(txtOverheadCost.Text)
                obj.SUBCONTRACT_COST = clsCommon.myCdbl(txtSubcontractCost.Text)
                obj.TOOL_COST = clsCommon.myCdbl(txtToolCost.Text)
                obj.TOTAL_COST = clsCommon.myCdbl(txtTotalStandardCost.Text)
                obj.QTY_ON_HAND = clsCommon.myCdbl(txtQtyOnHand.Text)
                obj.AVERAGE_COST = clsCommon.myCdbl(txtAverageCost.Text)
                fndItemCode.MyReadOnly = True

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(ITEM_CODE) from TSPL_MF_ITEM_COST_MAINTENANCE where ITEM_CODE='" + obj.ITEM_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsCostMaintainance.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.ITEM_CODE, NavigatorType.Current)

                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(clsCommon.myCstr(fndItemCode.Value)) <= 0 Then
            fndItemCode.Focus()
            Throw New Exception("Please Fill Item Code")
        End If
        'If clsCommon.myLen(clsCommon.myCstr(txtAccdescription.Text)) <= 0 Then
        '    txtAccdescription.Focus()
        '    Throw New Exception("Please Fill Description")
        'End If
        Return True
    End Function
    Public Sub TotalVAl(ByVal ischanges As Boolean)
        If ischanges Then


            txtTotalStandardCost.Value = txtMaterialCost.Value + txtPackagingCost.Value + txtSetupCost.Value + txtLabourCost.Value + txtOverheadCost.Value + txtSubcontractCost.Value + txtToolCost.Value
        End If
    End Sub
#End Region

#Region "Finders"

    Private Sub fndItemCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndItemCode._MYValidating
        Reset()
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(fndItemCode.Value), "", isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            fndItemCode.Value = obj.Item_Code
            txtdescription.Text = obj.Item_Desc
            fnduom.Value = obj.Unit_Code
            txtuom.Text = clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + fnduom.Value + "' ")
            LoadData(fndItemCode.Value, NavigatorType.Current)
        Else
            'fndItemCode.Value = ""
            'txtdescription.Text = ""
            'fnduom.Value = ""
            'txtuom.Text = clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + fnduom.Value + "' ")

        End If

    End Sub
    Private Sub fnduom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnduom._MYValidating
        openuom(isButtonClicked)
    End Sub
#End Region
#Region "Events"
    Private Sub FrmCostMaintainance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        rdbtnsave.Enabled = True
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Ctrl+D  to Delete Record")
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S to Save")
    End Sub



    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        Reset()
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub


    Private Sub FrmCostMaintainance_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region



    Private Sub txtMaterialCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaterialCost.TextChanged
        TotalVAl(True)
    End Sub

    Private Sub txtPackagingCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackagingCost.TextChanged, txtSubcontractCost.TextChanged
        TotalVAl(True)
    End Sub

    Private Sub txtSetupCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSetupCost.TextChanged
        TotalVAl(True)
    End Sub

    Private Sub txtLabourCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLabourCost.TextChanged, txtToolCost.TextChanged
        TotalVAl(True)
    End Sub

    Private Sub txtOverheadCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOverheadCost.TextChanged
        TotalVAl(True)
    End Sub
    'richa Ticket No BM00000002986 23/06/2014
    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "Select ITEM_CODE As [ITEM CODE] ,MATERIAL_COST As [MATERIAL COST],PACKAGING_COST As [PACKAGING COST],SETUP_COST As [SETUP COST],LABOR_COST As [LABOR COST],OVERHEAD_COST As [OVERHEAD COST],SUBCONTRACT_COST As [SUBCONTRACT COST],TOOL_COST As [TOOL COST] from TSPL_MF_ITEM_COST_MAINTENANCE"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "ITEM CODE", "MATERIAL COST", "PACKAGING COST", "SETUP COST", "LABOR COST", "OVERHEAD COST", "SUBCONTRACT COST", "TOOL COST") Then

            Dim linno As Integer = 0
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()
                'clsCommon.ProgressBarShow()

                For Each grow1 As GridViewRowInfo In gv.Rows
                    Dim strItemCode As String = clsCommon.myCstr(grow1.Cells("ITEM CODE").Value)


                    If (String.IsNullOrEmpty(strItemCode)) Or clsCommon.myLen(strItemCode) > 0 Then
                        If CheckItemCode(strItemCode, trans) = False Then
                            Exit Sub
                        End If
                    End If

                Next



                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsCostMaintainance

                    linno += 1
                    Dim dblstrcode As String = clsCommon.myCstr(grow.Cells("ITEM CODE").Value)
                    If (String.IsNullOrEmpty(dblstrcode)) Or clsCommon.myLen(dblstrcode) > 50 Then
                        Throw New Exception("Length of Item Code should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.ITEM_CODE = dblstrcode

                    Dim dblmaterialcost As Double
                    If clsCommon.myLen(grow.Cells("MATERIAL COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("MATERIAL COST").Value) Then
                            Throw New Exception("Please insert decimal data in MATERIAL COST at Line No '" + linno + "' ")
                        Else
                            dblmaterialcost = clsCommon.myCdbl(grow.Cells("MATERIAL COST").Value)
                        End If
                    Else
                        dblmaterialcost = clsCommon.myCdbl(grow.Cells("MATERIAL COST").Value)
                    End If

                    obj.MATERIAL_COST = dblmaterialcost

                    Dim dblpackagingcost As Double
                    If clsCommon.myLen(grow.Cells("PACKAGING COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("PACKAGING COST").Value) Then
                            Throw New Exception("Please insert decimal data in PACKAGING COST at Line No '" + linno + "' ")
                        Else
                            dblpackagingcost = clsCommon.myCdbl(grow.Cells("PACKAGING COST").Value)
                        End If
                    Else
                        dblpackagingcost = clsCommon.myCdbl(grow.Cells("PACKAGING COST").Value)
                    End If


                    obj.PACKAGING_COST = dblpackagingcost

                    Dim dblsetupcost As Double
                    If clsCommon.myLen(grow.Cells("SETUP COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("SETUP COST").Value) Then
                            Throw New Exception("Please insert decimal data in SETUP COST at Line No '" + linno + "' ")
                        Else
                            dblsetupcost = clsCommon.myCdbl(grow.Cells("SETUP COST").Value)
                        End If
                    Else
                        dblsetupcost = clsCommon.myCdbl(grow.Cells("SETUP COST").Value)
                    End If

                    obj.SETUP_COST = dblsetupcost

                    Dim dblaborcost As Double
                    If clsCommon.myLen(grow.Cells("LABOR COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("LABOR COST").Value) Then
                            Throw New Exception("Please insert decimal data in LABOR COST at Line No '" + linno + "' ")
                        Else
                            dblaborcost = clsCommon.myCdbl(grow.Cells("LABOR COST").Value)
                        End If
                    Else
                        dblaborcost = clsCommon.myCdbl(grow.Cells("LABOR COST").Value)
                    End If

                    obj.LABOR_COST = dblaborcost

                    Dim dbloverheadcost As Double
                    If clsCommon.myLen(grow.Cells("OVERHEAD COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("OVERHEAD COST").Value) Then
                            Throw New Exception("Please insert decimal data in OVERHEAD COST at Line No '" + linno + "' ")
                        Else
                            dbloverheadcost = clsCommon.myCdbl(grow.Cells("OVERHEAD COST").Value)
                        End If
                    Else
                        dbloverheadcost = clsCommon.myCdbl(grow.Cells("OVERHEAD COST").Value)
                    End If

                    obj.OVERHEAD_COST = dbloverheadcost


                    Dim dblsubcontractcost As Double
                    If clsCommon.myLen(grow.Cells("SUBCONTRACT COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("SUBCONTRACT COST").Value) Then
                            Throw New Exception("Please insert decimal data in SUBCONTRACT COST at Line No '" + linno + "' ")
                        Else
                            dblsubcontractcost = clsCommon.myCdbl(grow.Cells("SUBCONTRACT COST").Value)
                        End If
                    Else
                        dblsubcontractcost = clsCommon.myCdbl(grow.Cells("SUBCONTRACT COST").Value)
                    End If
                    obj.SUBCONTRACT_COST = dblsubcontractcost

                    Dim dbltoolcost As Double
                    If clsCommon.myLen(grow.Cells("TOOL COST").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("TOOL COST").Value) Then
                            Throw New Exception("Please insert decimal data in TOOL COST at Line No '" + linno + "' ")
                        Else
                            dbltoolcost = clsCommon.myCdbl(grow.Cells("TOOL COST").Value)
                        End If
                    Else
                        dbltoolcost = clsCommon.myCdbl(grow.Cells("TOOL COST").Value)
                    End If
                    obj.TOOL_COST = dbltoolcost

                    Dim totalcost As Double = 0
                   
                    totalcost = dblmaterialcost + dblpackagingcost + dblsetupcost + dblaborcost + dbloverheadcost + dblsubcontractcost + dbltoolcost
                    obj.TOTAL_COST = totalcost

                    If clsCommon.myLen(dblstrcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MF_ITEM_COST_MAINTENANCE where ITEM_CODE='" + dblstrcode + "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If


                    ClsCostMaintainance.SaveData(obj, IsNewEntry, trans)
                Next
                trans.Commit()
                ' clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                'clsCommon.ProgressBarHide()
                'myMessages.myExceptions(ex)
                RadMessageBox.Show(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Function CheckItemCode(ByVal ItemCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If clsCommon.myLen(ItemCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("Item Code is Invalid")
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
  
    '-------------------- richa code ends-----------------------
End Class

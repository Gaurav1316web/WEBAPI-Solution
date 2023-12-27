Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'=================Created by Preeti gupta========05/05/2015
Public Class FrmSalesHierarchyMapping
    Inherits FrmMainTranScreen




    Const colCustCode As String = "Customer Code"
    Const colCustName As String = "CustName"
    Const colLevel As String = "Level Code"
    Const colLevelName As String = "Level Name"
    Const colSelect As String = "Select"

    Private isInsideLoadData As Boolean = False
    Private isFromLoad As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim isNewEntry As Boolean = True
    Sub LoadBlankItemGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 100
        repoSelect.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSelect)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Customer Code"
        repoICode.Name = colCustCode

        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Customer Name"
        repoIName.Name = colCustName
        repoIName.Width = 200
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoILevel As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoILevel.FormatString = ""
        repoILevel.HeaderText = "Struct Code"
        repoILevel.Name = colLevel
        repoILevel.Width = 200
        repoILevel.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoILevel.TextImageRelation = TextImageRelation.TextBeforeImage
        repoILevel.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoILevel)

        Dim repoILevelName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoILevelName.FormatString = ""
        repoILevelName.HeaderText = "Struct Name"
        repoILevelName.Name = colLevelName
        repoILevelName.Width = 200
        repoILevelName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoILevelName)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Public Sub Load_Report()
        LoadBlankItemGrid()
        Dim squery As String = " 	 select TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Struct_Code,TSPL_Sales_Hierarchy_Structure.Description    from TSPL_CUSTOMER_MASTER left outer join TSPL_Sales_Hierarchy_Structure on TSPL_Sales_Hierarchy_Structure.Struct_Code =TSPL_CUSTOMER_MASTER.Struct_Code "

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(squery)
        For Each dr As DataRow In dtgv.Rows

            gv1.Rows.AddNew()
            isInsideLoadData = True

            gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("Cust_Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel).Value = clsCommon.myCstr(dr("Struct_Code"))
            If clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel).Value) > 0 Then
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = True
            End If

            gv1.Rows(gv1.Rows.Count - 1).Cells(colLevelName).Value = clsCommon.myCstr(dr("Description"))
            isInsideLoadData = False
            'End If

        Next

    End Sub

    Sub OpenCodeList(ByVal isButtonClick As Boolean)
        isInsideLoadData = True
        gv1.CurrentRow.Cells(colLevel).Value = ""
        Dim qry As String = "select TSPL_Sales_Hierarchy_Structure.Struct_Code  as Code ,TSPL_Sales_Hierarchy_Structure.Description    from TSPL_Sales_Hierarchy_Structure left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code "
        gv1.CurrentRow.Cells(colLevel).Value = clsCommon.ShowSelectForm("SLSHIERMAPNG", qry, "Code", "TSPL_Sales_Hierarchy_Levels.Is_Last_Level=1", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colLevel).Value) > 0 Then
            gv1.CurrentRow.Cells(colLevelName).Value = clsDBFuncationality.getSingleValue("select TSPL_Sales_Hierarchy_Structure.Description  FROM TSPL_Sales_Hierarchy_Structure  where TSPL_Sales_Hierarchy_Structure.struct_code='" + gv1.CurrentRow.Cells(colLevel).Value + "' ")
        Else
            gv1.CurrentRow.Cells(colLevelName).Value = ""

        End If
        isInsideLoadData = False
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim obj As New clsSalesHierarchyMapping()
        Dim ara As New List(Of clsSalesHierarchyMapping)
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colSelect).Value) > 0 Then
                ' Dim Obj As New clsSalesHierarchyMapping
                obj = New clsSalesHierarchyMapping
                obj.CustCode = clsCommon.myCstr(gv1.Rows(i).Cells(colCustCode).Value)
                obj.StructCode = clsCommon.myCstr(gv1.Rows(i).Cells(colLevel).Value)

                ara.Add(obj)
            End If
        Next

        If (clsSalesHierarchyMapping.SaveData(ara, Nothing)) Then
            'trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
            Load_Report()

            btnUnSelect.Enabled = True
        Else

            btnUnSelect.Enabled = False
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                isInsideLoadData = True
                If e.Column Is gv1.Columns(colLevel) Then
                    OpenCodeList(False)
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmSalesHierarchyMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Load_Report()
    End Sub
End Class


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
Public Class FrmsalesHierarchy
    Inherits FrmMainTranScreen

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Dim frm As New FrmSalesHierarchyMain()
        frm.CreateNewTransaction = True
        frm.Show()

    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click

    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        LoadData(False)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim frm As New FrmSalesHierarchyMain()
        frm.Code = clsCommon.myCstr(gv1.CurrentRow.Cells("Code").Value)
        frm.Show()
        LoadData(False)


    End Sub
    Sub LoadData(ByVal isShowMsg As Boolean)
        Try
            Dim qry As String = ""
            'Dim dt1 As DataTable = clsSalesHierarchy.GetDesignationLevel()
            'For i As Integer = 0 To dt1.Rows.Count - 1
            '    qry += dt1.Rows(i)(0) + " as " + dt1.Rows(i)(0) + ""
            '    If i <> dt1.Rows.Count - 1 Then
            '        qry += ","
            '    End If
            'Next
            'qry = "Level1_Desg as Level1,Level2_Desg as Level2,Level3_Desg as Level3,Level4_Desg as Level4"
            Dim qry1 As String
            qry1 = "	select TSPL_Sales_Hierarchy_Structure.Struct_Code as [Code],TSPL_Sales_Hierarchy_Structure.Description ,TSPL_Sales_Hierarchy_Structure.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Levels.Description as [Level Description],TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code as [Parent Struct Code],TSPL_Sales_Hierarchy_Structure_for_parent.Description as [Parent Struct Name],TSPL_Sales_Hierarchy_Levels.Seq_No  as [Seq No],TSPL_Sales_Hierarchy_Structure.Applicable_From as [Applicable From],TSPL_Sales_Hierarchy_Levels.Level_Type as [Level Type],TSPL_Sales_Hierarchy_Levels.Sub_Type as [Sub Type],TSPL_Sales_Hierarchy_Structure.Source_Doc as [Source Doc] from TSPL_Sales_Hierarchy_Structure "
            qry1 += " left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code "
            qry1 += " left outer join TSPL_Sales_Hierarchy_Structure as TSPL_Sales_Hierarchy_Structure_for_parent on TSPL_Sales_Hierarchy_Structure_for_parent.Struct_Code=TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            'If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            '    If isShowMsg Then
            '        clsCommon.MyMessageBoxShow("No Data found to Display", Me.Text)
            '    End If

            'End If

            gv1.DataSource = dt

            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).Width = 100
            Next

            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            gv1.AllowDeleteRow = False
            gv1.EnableAlternatingRowColor = True
            gv1.MasterView.TableFilteringRow.IsCurrent = True
            gv1.Columns(0).IsCurrent = True
            gv1.Focus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub ReStoreGridLayout()
    '    Try
    '        If clsCommon.myLen(ReportID) > 0 Then
    '            Dim obj As clsGridLayout = New clsGridLayout()
    '            obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
    '            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
    '                Dim ii As Integer
    '                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
    '                    gv1.Columns(ii).IsVisible = False
    '                    gv1.Columns(ii).VisibleInColumnChooser = True
    '                Next

    '                gv1.LoadLayout(obj.GridLayout)
    '                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            End If
    '        End If
    '    Catch err As Exception
    '        MessageBox.Show(err.Message)
    '    End Try
    'End Sub

    Private Sub FrmsalesHierarchy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData(True)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim qry1 As String
        qry1 = "	select TSPL_Sales_Hierarchy_Structure.Struct_Code as [Code],TSPL_Sales_Hierarchy_Structure.Description ,TSPL_Sales_Hierarchy_Structure.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code as [Parent Struct Code]," & _
            " TSPL_Sales_Hierarchy_Structure.Applicable_From as [Applicable From],TSPL_Sales_Hierarchy_Levels.Level_Type as [Level Type]," & _
            " TSPL_Sales_Hierarchy_Levels.Sub_Type as [Sub Type],TSPL_Sales_Hierarchy_Structure.Source_Doc as [Source Doc] from TSPL_Sales_Hierarchy_Structure "
        qry1 += " left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code "
        qry1 += " left outer join TSPL_Sales_Hierarchy_Structure as TSPL_Sales_Hierarchy_Structure_for_parent on TSPL_Sales_Hierarchy_Structure_for_parent.Struct_Code=TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code"
        transportSql.ExporttoExcel(qry1, Me)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Level Code", "Parent Struct Code", "Applicable From", "Level Type", "Sub Type", "Source Doc") Then
            ' Dim trans As SqlTransaction
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsSalesHierarchy()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)

                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect at line no-" & grow.Index + 1 & ".")
                    End If
                    obj.DOC_CODE = strCode

                    Dim strDes As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strDes.Length > 200 Then
                        Throw New Exception("Description can not be blank or incorrect at line no-" & grow.Index + 1 & ".")
                    End If
                    obj.Description = strDes

                    Dim LevelCode As String = clsCommon.myCstr(grow.Cells("Level Code").Value)

                    If LevelCode.Length > 30 Or LevelCode.Length <= 0 Then
                        Throw New Exception("Level Code can not be blank or incorrect at line no-" & grow.Index + 1 & ".")
                    End If
                    Dim objLevel As ClsSaleLevelHierarchy = ClsSaleLevelHierarchy.GetData(LevelCode, Nothing, NavigatorType.Current)
                    If clsCommon.myLen(objLevel.Level_Code) <= 0 Then
                        Throw New Exception("Level Code- " & LevelCode & " does not exists at line no-" & grow.Index + 1 & ".")
                    End If
                    obj.LevelCode = LevelCode


                    strDes = clsCommon.myCstr(grow.Cells("Parent Struct Code").Value)
                    'If strDes.Length > 30 Then
                    '    Throw New Exception("Parent_Struct_Code can not be blank or incorrect.")
                    'End If                    
                    obj.ParentStructCode = strDes
                    If clsCommon.myLen(grow.Cells("Applicable From").Value) <= 0 Then
                        Throw New Exception("Please enter Applicable From at line no " & grow.Index + 1 & ".")
                    End If
                    Dim APPFrom As Date = clsCommon.GetPrintDate(grow.Cells("Applicable From").Value, "dd/MMM/yyyy")
                    obj.Applicable_From = APPFrom

                    '' source doc
                    strDes = clsCommon.myCstr(grow.Cells("Source Doc").Value)
                    '' apply validation
                    If clsCommon.CompairString(objLevel.Level_Type, "EMP") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsEmployeeMaster.CheckExistence(strDes, Nothing)) <= 0 Then
                            Throw New Exception("Source Doc- " & strDes & " at line no " & grow.Index + 1 & " does not exists as an Employee.")
                        End If
                    ElseIf clsCommon.CompairString(objLevel.Level_Type, "COMP") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCompanyMaster.CheckExistence(strDes, Nothing)) <= 0 Then
                            Throw New Exception("Source Doc- " & strDes & " at line no " & grow.Index + 1 & " does not exists as a Company.")
                        End If
                    ElseIf clsCommon.CompairString(objLevel.Level_Type, "C") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCountryMaster.CheckNewEntry(strDes, Nothing)) <= 0 Then
                            Throw New Exception("Source Doc- " & strDes & " at line no " & grow.Index + 1 & " does not exists as a Country.")
                        End If
                    ElseIf clsCommon.CompairString(objLevel.Level_Type, "ST") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsStateMaster.CheckNewEntry(strDes, Nothing)) <= 0 Then
                            Throw New Exception("Source Doc- " & strDes & " at line no " & grow.Index + 1 & " does not exists as a State.")
                        End If
                    ElseIf clsCommon.CompairString(objLevel.Level_Type, "CT") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCityMaster.GetCodeByName(strDes, Nothing)) <= 0 Then
                            Throw New Exception("Source Doc- " & strDes & " at line no " & grow.Index + 1 & " does not exists as a City.")
                        End If
                    ElseIf clsCommon.CompairString(objLevel.Level_Type, "Z") = CompairStringResult.Equal Then
                        If clsCommon.myLen(ClsZoneMaster.GetName(strDes)) <= 0 Then
                            Throw New Exception("Source Doc- " & strDes & " at line no " & grow.Index + 1 & " does not exists as a City.")
                        End If
                    ElseIf clsCommon.CompairString(objLevel.Level_Type, "O") = CompairStringResult.Equal Then
                    End If
                    obj.Source_Doc = strDes
                    clsSalesHierarchy.savedata(obj, clsSalesHierarchy.CheckNewEntry(obj.DOC_CODE, Nothing))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
End Class

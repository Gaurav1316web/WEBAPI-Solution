
'''''''''''''' New Customer Aging Drill Down Report added by Panch Raj
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'-------------BM00000001804---------------08/07/2014------Monika
''-----richa agarwal ticket no. BM00000008258 on 19/11/2015

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
'Imports Telerik.WinControls.UI.Export6
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
'======================updated by preeti gupta ===ticket no-[BM00000007393]
Public Class rptAPAgeingDrillDown
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim settingAllowtoShowDebitBalanceonVendorAgeing As Boolean = False
    Dim ConsiderOpeningDocintoBucketsInAgeing As Boolean = False

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Reset()

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rptAPAgeingDrillDown_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        settingAllowtoShowDebitBalanceonVendorAgeing = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoShowDebitBalanceonVendorAgeing, clsFixedParameterCode.AllowtoShowDebitBalanceonVendorAgeing, Nothing)) = 1, True, False)
        ConsiderOpeningDocintoBucketsInAgeing = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderOpeningDocintoBucketsInAgeing, clsFixedParameterCode.ConsiderOpeningDocintoBucketsInAgeing, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        LoadCurrencyType()
        'LoadLocation()
        'LoadVendorGroup()
        'LoadVendor()
        'chkVGAll.IsChecked = True
        'chkVendorAll.IsChecked = True
        'chkLocAll.IsChecked = True
        chkType.Checked = True
        dtpAgeof.Value = Date.Today
        dtpCutoffDate.Value = Date.Today
        cbgVendor.Enabled = False
        ' ddlAgedPayble.Text = "Aged Trial Balance By Due Date"
        ddlAgedPayble.Text = "Aged Payble by Due Date"
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txt5th.Text = ""
        txt7th.Text = ""
        txt8th.Text = ""
        txt6th.Text = ""
        txtOver.Text = ""
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAgingPayble)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExp.Visible = MyBase.isExport
    End Sub

    Sub Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox4.Enabled = True
        'LoadLocation()
        'chkLocAll.IsChecked = True
        'LoadVendor()
    End Sub

    Function GetGridDT(ByVal dt As DataTable) As DataTable
        Dim dtGrid As New DataTable
        Dim Over As Integer = 0
        If chkFifo.Checked Then
            dtGrid.Columns.Add(dt.Columns("Customer Id").ColumnName, dt.Columns("Customer Id").DataType)
            dtGrid.Columns.Add(dt.Columns("Customer Name").ColumnName, dt.Columns("Customer Name").DataType)
            dtGrid.Columns.Add(dt.Columns("Document Id").ColumnName, dt.Columns("Document Id").DataType)
            dtGrid.Columns.Add(dt.Columns("Document Date").ColumnName, dt.Columns("Document Date").DataType)
            dtGrid.Columns.Add("Document Type", dt.Columns("Document_Type").DataType)
            dtGrid.Columns.Add("Location", dt.Columns("Location").DataType)
            dtGrid.Columns.Add("Terms_Code", dt.Columns("Terms_Code").DataType)
            dtGrid.Columns.Add("Currency", dt.Columns("Currency").DataType)
            dtGrid.Columns.Add(New DataColumn("" & Val(Me.txtCurrent.Text) & "-" & Val(Me.txtIst.Text) & " Days", dt.Columns("Due Amount").DataType))
            Over = Val(Me.txtIst.Text)
            If clsCommon.myLen(txtIst.Text) > 0 And clsCommon.myLen(txt2nd.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txtIst.Text) + 1 & "-" & Val(Me.txt2nd.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt2nd.Text)
            End If
            If clsCommon.myLen(txt2nd.Text) > 0 And clsCommon.myLen(txt3rd.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt2nd.Text) + 1 & "-" & Val(Me.txt3rd.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt3rd.Text)
            End If
            If clsCommon.myLen(txt3rd.Text) > 0 And clsCommon.myLen(txt4th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt3rd.Text) + 1 & "-" & Val(Me.txt4th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt4th.Text)
            End If
            If clsCommon.myLen(txt4th.Text) > 0 And clsCommon.myLen(txt5th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt4th.Text) + 1 & "-" & Val(Me.txt5th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt5th.Text)
            End If
            If clsCommon.myLen(txt5th.Text) > 0 And clsCommon.myLen(txt6th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt5th.Text) + 1 & "-" & Val(Me.txt6th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt6th.Text)
            End If
            If clsCommon.myLen(txt6th.Text) > 0 And clsCommon.myLen(txt7th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt6th.Text) + 1 & "-" & Val(Me.txt7th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt7th.Text)
            End If
            If clsCommon.myLen(txt7th.Text) > 0 And clsCommon.myLen(txt8th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt7th.Text) + 1 & "-" & Val(Me.txt8th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt8th.Text)
            End If
            If clsCommon.myLen(txt8th.Text) > 0 And clsCommon.myLen(txtOver.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt8th.Text) + 1 & "-" & Val(Me.txtOver.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txtOver.Text)
            End If
            dtGrid.Columns.Add(New DataColumn("Over " & Over & " Days", dt.Columns("Due Amount").DataType))
            dtGrid.Columns.Add(New DataColumn("Total Amount", dt.Columns("Due Amount").DataType))
            For Each dr As DataRow In dt.Rows
                dtGrid.Rows.Add()
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Id") = dr.Item("Customer Id")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Name") = dr.Item("Customer Name")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Id") = dr.Item("Document Id")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Date") = dr.Item("Document Date")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Type") = dr.Item("Document_Type")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Location") = dr.Item("Location")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Terms_Code") = dr.Item("Terms_Code")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Currency") = dr.Item("Currency")
                Dim total As Decimal = 0

                If dr.Item("Ageing_Days") <= Val(dr.Item("Second_Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtCurrent.Text) & "-" & Val(Me.txtIst.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Third Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtIst.Text) + 1 & "-" & Val(Me.txt2nd.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fourth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt2nd.Text) + 1 & "-" & Val(Me.txt3rd.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fifth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt3rd.Text) + 1 & "-" & Val(Me.txt4th.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Sixth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt4th.Text) + 1 & "-" & Val(Me.txt5th.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Seventh Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt5th.Text) + 1 & "-" & Val(Me.txt6th.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Eight Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt6th.Text) + 1 & "-" & Val(Me.txt7th.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Nineth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt7th.Text) + 1 & "-" & Val(Me.txt8th.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Over")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt8th.Text) + 1 & "-" & Val(Me.txtOver.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") > Val(dr.Item("Over")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Over " & Over & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                End If
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = total
            Next
        Else
            dtGrid.Columns.Add(dt.Columns("Customer Id").ColumnName, dt.Columns("Customer Id").DataType)
            dtGrid.Columns.Add(dt.Columns("Customer Name").ColumnName, dt.Columns("Customer Name").DataType)
            If chkType.Checked = False Then
                dtGrid.Columns.Add(dt.Columns("Document Date").ColumnName, dt.Columns("Document Date").DataType)
                dtGrid.Columns.Add(dt.Columns("Document Id").ColumnName, dt.Columns("Document Id").DataType)
                dtGrid.Columns.Add("Document Type", dt.Columns("Document_Type").DataType)
                dtGrid.Columns.Add(dt.Columns("Due Date").ColumnName, dt.Columns("Due Date").DataType)
                dtGrid.Columns.Add(dt.Columns("Location").ColumnName, dt.Columns("Location").DataType)
                dtGrid.Columns.Add(dt.Columns("Terms_Code").ColumnName, dt.Columns("Terms_Code").DataType)
                dtGrid.Columns.Add(dt.Columns("Currency").ColumnName, dt.Columns("Currency").DataType)
            End If
            dtGrid.Columns.Add(dt.Columns("Current").ColumnName, dt.Columns("Current").DataType)
            dtGrid.Columns.Add(New DataColumn("" & Val(Me.txtCurrent.Text) & "-" & Val(Me.txtIst.Text) & " Days", dt.Columns("Due Amount").DataType))
            Over = Val(Me.txtIst.Text)
            If clsCommon.myLen(txtIst.Text) > 0 And clsCommon.myLen(txt2nd.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txtIst.Text) + 1 & "-" & Val(Me.txt2nd.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt2nd.Text)
            End If
            If clsCommon.myLen(txt2nd.Text) > 0 And clsCommon.myLen(txt3rd.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt2nd.Text) + 1 & "-" & Val(Me.txt3rd.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt3rd.Text)
            End If
            If clsCommon.myLen(txt3rd.Text) > 0 And clsCommon.myLen(txt4th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt3rd.Text) + 1 & "-" & Val(Me.txt4th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt4th.Text)
            End If
            If clsCommon.myLen(txt4th.Text) > 0 And clsCommon.myLen(txt5th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt4th.Text) + 1 & "-" & Val(Me.txt5th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt5th.Text)
            End If
            If clsCommon.myLen(txt5th.Text) > 0 And clsCommon.myLen(txt6th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt5th.Text) + 1 & "-" & Val(Me.txt6th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt6th.Text)
            End If
            If clsCommon.myLen(txt6th.Text) > 0 And clsCommon.myLen(txt7th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt6th.Text) + 1 & "-" & Val(Me.txt7th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt7th.Text)
            End If
            If clsCommon.myLen(txt7th.Text) > 0 And clsCommon.myLen(txt8th.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt7th.Text) + 1 & "-" & Val(Me.txt8th.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt8th.Text)
            End If
            If clsCommon.myLen(txt8th.Text) > 0 And clsCommon.myLen(txtOver.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt8th.Text) + 1 & "-" & Val(Me.txtOver.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txtOver.Text)
            End If
            dtGrid.Columns.Add(New DataColumn("Over " & Over & " Days", dt.Columns("Due Amount").DataType))
            dtGrid.Columns.Add(New DataColumn("Total Amount", dt.Columns("Due Amount").DataType))

            If chkType.Checked = False Then
                For Each dr As DataRow In dt.Rows
                    dtGrid.Rows.Add()
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Id") = dr.Item("Customer Id")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Name") = dr.Item("Customer Name")

                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Date") = dr.Item("Document Date")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Id") = dr.Item("Document Id")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Type") = dr.Item("Document_Type")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Due Date") = dr.Item("Due Date")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Location") = dr.Item("Location")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Terms_Code") = dr.Item("Terms_Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Currency") = dr.Item("Currency")

                    Dim total As Decimal = 0

                    If clsCommon.myCdbl(dr.Item("Ageing_Days")) > 0 AndAlso ((clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "CR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "RC") = CompairStringResult.Equal) OrElse ConsiderOpeningDocintoBucketsInAgeing = True) Then
                        'If clsCommon.myCdbl(dr.Item("Ageing_Days")) > 0 Then
                        If clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Second_Period")) Then
                            'dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtCurrent.Text) + 1 & "-" & Val(Me.txtIst.Text) & " Days") = dr.Item("Due Amount")
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtCurrent.Text) & "-" & Val(Me.txtIst.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Third Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtIst.Text) + 1 & "-" & Val(Me.txt2nd.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Fourth Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt2nd.Text) + 1 & "-" & Val(Me.txt3rd.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Fifth Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt3rd.Text) + 1 & "-" & Val(Me.txt4th.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Sixth Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt4th.Text) + 1 & "-" & Val(Me.txt5th.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Seventh Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt5th.Text) + 1 & "-" & Val(Me.txt6th.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Eight Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt6th.Text) + 1 & "-" & Val(Me.txt7th.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Nineth Period")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt7th.Text) + 1 & "-" & Val(Me.txt8th.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Over")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt8th.Text) + 1 & "-" & Val(Me.txtOver.Text) & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) > Val(dr.Item("Over")) Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Over " & Over & " Days") = dr.Item("Due Amount")
                            total = total + dr.Item("Due Amount")
                        End If
                    Else
                        ' dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = dr.Item("Current")

                        If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "CR") = CompairStringResult.Equal Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = dr.Item("Current")
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = (-(dr.Item("Current")))
                        Else
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = dr.Item("Current")
                        End If
                    End If
                    If settingAllowtoShowDebitBalanceonVendorAgeing Then
                        If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = 0
                        Else
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = IIf(total = 0, (dr.Item("Current")), total)
                        End If
                    Else
                        'If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "CR") = CompairStringResult.Equal Then
                        '    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = (dr.Item("Current"))
                        'ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                        '    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = -(dr.Item("Current"))
                        'Else
                        '    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = IIf(total = 0, (dr.Item("Current")), total)
                        'End If
                        If ConsiderOpeningDocintoBucketsInAgeing = True Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = (dr.Item("Due Amount"))
                        Else
                            If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = -(dr.Item("Current"))
                            Else
                                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = IIf(total = 0, (dr.Item("Current")), total)
                            End If
                        End If

                    End If


                Next
            Else
                ' dtGrid.Rows.Add()
                Dim i As Integer = 0
                Dim strcountno As String = ""
                Dim intCounter As Integer = 0
                For Each dr As DataRow In dt.Rows
                    Dim intCurrVendorNo As String = clsCommon.myCstr(dr.Item("Customer Id"))
                    If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr.Item("Customer Id"))) <> CompairStringResult.Equal Then
                        dtGrid.Rows.Add()
                        i = dtGrid.Rows.Count - 1
                        'Else

                        '    i = dtGrid.Rows.Count - 1
                    End If
                    dtGrid.Rows(i).Item("Customer Id") = dr.Item("Customer Id")
                    dtGrid.Rows(i).Item("Customer Name") = dr.Item("Customer Name")


                    Dim total As Decimal = 0

                    If clsCommon.myCdbl(dr.Item("Ageing_Days")) > 0 AndAlso ((clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "CR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "RC") = CompairStringResult.Equal) OrElse ConsiderOpeningDocintoBucketsInAgeing = True) Then
                        If clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Second_Period")) Then
                            'dtGrid.Rows(i).Item("" & Val(Me.txtCurrent.Text) + 1 & "-" & Val(Me.txtIst.Text) & " Days") = clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txtCurrent.Text) + 1 & "-" & Val(Me.txtIst.Text) & " Days")) + dr.Item("Due Amount")
                            dtGrid.Rows(i).Item("" & Val(Me.txtCurrent.Text) & "-" & Val(Me.txtIst.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txtCurrent.Text) & "-" & Val(Me.txtIst.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Third Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txtIst.Text) + 1 & "-" & Val(Me.txt2nd.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txtIst.Text) + 1 & "-" & Val(Me.txt2nd.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Fourth Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt2nd.Text) + 1 & "-" & Val(Me.txt3rd.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt2nd.Text) + 1 & "-" & Val(Me.txt3rd.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Fifth Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt3rd.Text) + 1 & "-" & Val(Me.txt4th.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt3rd.Text) + 1 & "-" & Val(Me.txt4th.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Sixth Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt4th.Text) + 1 & "-" & Val(Me.txt5th.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt4th.Text) + 1 & "-" & Val(Me.txt5th.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Seventh Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt5th.Text) + 1 & "-" & Val(Me.txt6th.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt5th.Text) + 1 & "-" & Val(Me.txt6th.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Eight Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt6th.Text) + 1 & "-" & Val(Me.txt7th.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt6th.Text) + 1 & "-" & Val(Me.txt7th.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Nineth Period")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt7th.Text) + 1 & "-" & Val(Me.txt8th.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt7th.Text) + 1 & "-" & Val(Me.txt8th.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) <= Val(dr.Item("Over")) Then
                            dtGrid.Rows(i).Item("" & Val(Me.txt8th.Text) + 1 & "-" & Val(Me.txtOver.Text) & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("" & Val(Me.txt8th.Text) + 1 & "-" & Val(Me.txtOver.Text) & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        ElseIf clsCommon.myCdbl(dr.Item("Ageing_Days")) > Val(dr.Item("Over")) Then
                            dtGrid.Rows(i).Item("Over " & Over & " Days") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(i)("Over " & Over & " Days")) + dr.Item("Due Amount"), 2)
                            total = total + dr.Item("Due Amount")
                        End If
                    Else
                        'dtGrid.Rows(i).Item("Current") = clsCommon.myCdbl(dtGrid.Rows(i)("Current")) + dr.Item("Current")

                        If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "CR") = CompairStringResult.Equal Then
                            dtGrid.Rows(i).Item("Current") = clsCommon.myCdbl(dtGrid.Rows(i)("Current")) + dr.Item("Current")
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                            dtGrid.Rows(i).Item("Current") = clsCommon.myCdbl(dtGrid.Rows(i)("Current")) + (-(dr.Item("Current")))
                        Else
                            dtGrid.Rows(i).Item("Current") = clsCommon.myCdbl(dtGrid.Rows(i)("Current")) + dr.Item("Current")
                        End If

                    End If

                    If settingAllowtoShowDebitBalanceonVendorAgeing Then
                        If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")), 2)
                        Else
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")) + IIf(total = 0, (dr.Item("Current")), total), 2)
                        End If
                    Else
                        If ConsiderOpeningDocintoBucketsInAgeing = True Then

                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")) + ((dr.Item("Due Amount"))), 2)
                        Else
                            If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "DR") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Item("Document_Type")), "AV") = CompairStringResult.Equal Then
                                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")) + (-(dr.Item("Current"))), 2)
                                ' dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current")) + (-(dr.Item("Current"))), 2)
                            Else
                                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")) + IIf(total = 0, (dr.Item("Current")), total), 2)
                                ' dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = Math.Round(clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current")) + IIf(total = 0, (dr.Item("Current")), total), 2)
                            End If

                        End If



                    End If
                    strcountno = intCurrVendorNo
                    Dim intNextInvNo As Integer = -1

                Next
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim TotalAmt As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)

            If settingAllowtoShowDebitBalanceonVendorAgeing Then
                Dim current As New GridViewSummaryItem("Current", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(current)

                Dim i As Integer = 0
                If chkType.Checked Then
                    For i = 3 To dtGrid.Columns.Count - 2
                        Dim bucketcolumn As New GridViewSummaryItem(dtGrid.Columns(i).ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(bucketcolumn)
                    Next
                Else
                    For i = 10 To dtGrid.Columns.Count - 2
                        Dim bucketcolumn As New GridViewSummaryItem(dtGrid.Columns(i).ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(bucketcolumn)
                    Next
                End If


            End If

            gv1.MasterTemplate.AutoExpandGroups = True
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If

        dtGrid.AcceptChanges()
        Return dtGrid
    End Function

    Sub PrintNew1(ByVal Isgrid As Boolean)
        Try
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please Select atleast one Location.")
                Return
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please Select atleast one Vendor.")
                Return
            End If
            If chkVGSelect.IsChecked AndAlso cbgVendorGroup.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please Select atleast one Vendor.")
                Return
            End If
            Dim txtOvr As String
            Dim strNo As String
            Dim type As String = Me.ddlAgedPayble.Text
            Dim strType As String = ""
            Dim IsFifoBased As String = "N"

            Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")
            Dim AgeOfDate As String = clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy")
            Dim asofdate As String = clsCommon.GetPrintDate(dtpAgeof.Value, "yyyy-MM-dd")
            Dim cutoffdate As String = clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy")

            If chkType.Checked = True Then
                strType = "SMry"
            End If
            If chkFifo.Checked Then
                IsFifoBased = "Y"
            End If
            '------------------------------------------------------------
            Dim Arr As New ArrayList
            Arr.Add("I")
            Arr.Add("C")
            Arr.Add("D")
            Arr.Add("AV")
            Arr.Add("OA")
            Arr.Add("P")
            Arr.Add("RC")
            Arr.Add("Adjustment")
            If Me.txtIst.Text = "" Or Me.txt2nd.Text = "" Or Me.txt3rd.Text = "" Then
                RadMessageBox.Show("Select Atleast 3 Buckets!")
                Exit Sub
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text = "" And Me.txt5th.Text = "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "0"
                txtOvr = Me.txt3rd.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text = "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "1"
                txtOvr = Me.txt4th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "2"
                txtOvr = Me.txt5th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "3"
                txtOvr = Me.txt6th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text <> "" And Me.txt8th.Text = "" Then
                strNo = "4"
                txtOvr = Me.txt7th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text <> "" And Me.txt8th.Text <> "" Then
                strNo = ""
                txtOvr = Me.txtOver.Text
            Else
                RadMessageBox.Show("Selection Criteria Not In Order")
                Exit Sub
            End If

            Dim datedifference As Double = clsCommon.myCdbl(txt2nd.Text) - clsCommon.myCdbl(txtIst.Text)
             
            Dim dt As DataTable
            Dim strUpperQry As String
            Dim strLowerQry As String
            datedifference = clsCommon.myCdbl(txt2nd.Text) - clsCommon.myCdbl(txtIst.Text)

            strUpperQry = "select Vendor_Code,DocDate,docPosted,REFDOCNO,RefDocType,'" + RunDate + "' as RunDate, '" + AgeOfDate + "' as AgeOfDate, '" + cutoffdate + "' as CutOfDate, '' as rptHeading, '0' AS First_Period, '" + Me.txtIst.Text + "' AS Second_Period, '" + Me.txt2nd.Text + "' AS [Third Period], '" + Me.txt3rd.Text + "' AS [Fourth Period], '" + Me.txt4th.Text + "' AS [Fifth Period]," + Environment.NewLine & _
             " '" + Me.txt5th.Text + "' AS [Sixth Period], '" + Me.txt6th.Text + "' AS [Seventh Period],'" + Me.txt7th.Text + "' AS [Eight Period], '" + Me.txt8th.Text + "' AS [Nineth Period], '" + txtOvr + "' AS [Over], " + Environment.NewLine & _
            " YYY.Vendor_Group_Code , YYY.Vendor_Group_Code_Desc as Cust_Group_Desc, YYY.Vendor_Code as [Customer Id], '' as [Parent Code], YYY.Vendor_Name AS [Customer Name], " + Environment.NewLine & _
            " YYY.DocNo as [Document Id], '' as [Desc], Currency, ConvRate,"

            If clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Due Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " case when isnull(Due_Date,'')='' then case when ( DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else convert(decimal(18,2), [Document_Total]) *-1 End else 0 end else case when ( DATEDIFF (day,convert(date, Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else convert(decimal(18,2), [Document_Total]) *-1  End else 0 end end as [Due Amount], "
                Else
                    strUpperQry += " case when isnull(Due_Date,'')='' then case when ( DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else 0 End else 0 end else case when ( DATEDIFF (day,convert(date, Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else 0 End else 0 end end as [Due Amount], "
                End If

            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Document Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " case when ( DATEDIFF (day,convert(date, DocDate,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1)>0 then Case when Document_Type IN ('I','C','RC') then  convert(decimal(18,2),  [Document_Total]) else convert(decimal(18,2),  [Document_Total]) * -1  End else 0 end as [Due Amount],"
                Else
                    strUpperQry += " case when ( DATEDIFF (day,convert(date, DocDate,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1)>0 then Case when Document_Type IN ('I','C','RC') then  convert(decimal(18,2),  [Document_Total]) else 0 End else 0 end as [Due Amount],"
                End If
            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Vendor Invoice Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " case when isnull(Vendor_Invoice_Date,'')='' then case when ( DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else convert(decimal(18,2), [Document_Total]) * -1  End else 0 end else case when ( DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else convert(decimal(18,2), [Document_Total]) * -1  End else 0 end end as [Due Amount], "
                Else
                    'strUpperQry += " case when ( DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then  convert(decimal(18,2),  [Document_Total]) else 0 End else 0 end as [Due Amount],"
                    strUpperQry += " case when isnull(Vendor_Invoice_Date,'')='' then case when ( DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else 0 End else 0 end else case when ( DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else 0 End else 0 end end as [Due Amount], "
                End If

            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble By Vendor Invoice Due Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " case when isnull(VI_Due_Date,'')='' then case when ( DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else convert(decimal(18,2), [Document_Total]) * -1  End else 0 end else case when ( DATEDIFF (day,convert(date, VI_Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else convert(decimal(18,2), [Document_Total]) * -1  End else 0 end end as [Due Amount], "
                Else
                    strUpperQry += " case when isnull(VI_Due_Date,'')='' then case when ( DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else 0 End else 0 end else case when ( DATEDIFF (day,convert(date, VI_Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Document_Type IN ('I','C','RC') then convert(decimal(18,2), [Document_Total]) else 0 End else 0 end end as [Due Amount], "
                End If

            End If

            strUpperQry += " case when Document_Type IN ('I','C','RC') then convert(varchar,Due_Date,103) else convert(varchar, YYY.DocDate,103) end as [Due Date],convert(varchar, YYY.DocDate,103) as [Document Date], "
            If clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Due Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " 0  as [Current], case when isnull(Due_Date,'')='' then DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1  else DATEDIFF (day,convert(date, Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1  end  as [Ageing_Days], " + Environment.NewLine
                Else
                    strUpperQry += " case when  Document_Type IN ('I','C','RC') and ( DATEDIFF (day,convert(date, Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )<=0 then convert(decimal(18,2), isnull([Document_Total],0)) else case when Document_Type NOT IN ('I','C','RC') then  convert(decimal(18,2), isnull([Document_Total],0)) else 0 End End  as [Current], " + Environment.NewLine &
                " case when Document_Type IN ('I','C','RC') then  case when isnull(Due_Date,'')='' then DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1  else DATEDIFF (day,convert(date, Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1  end  else 0 end  as [Ageing_Days], "
                End If
            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Document Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " 0 as [Current],DATEDIFF (day,convert(date, DocDate,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  as [Ageing_Days], " + Environment.NewLine
                Else
                    strUpperQry += " case when Document_Type NOT IN ('I','C','RC') then  convert(decimal(18,2),isnull([Document_Total],0)) else 0 End  as [Current], " + Environment.NewLine &
              " case when Document_Type IN ('I','C','RC') then  DATEDIFF (day,convert(date, DocDate,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  else 0 end  as [Ageing_Days], "
                End If
            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Vendor Invoice Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += " 0  as [Current],case when isnull(Vendor_Invoice_Date,'')='' then DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1 else DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 end  as [Ageing_Days], " + Environment.NewLine
                Else
                    'strUpperQry += " case when Document_Type NOT IN ('I','C','RC') then  convert(decimal(18,2),isnull([Document_Total],0)) else 0 End  as [Current], " + Environment.NewLine &
                    '" case when Document_Type IN ('I','C','RC') then  DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  else 0 end  as [Ageing_Days], "
                    strUpperQry += " case when  Document_Type IN ('I','C','RC') and ( DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )<=0 then convert(decimal(18,2), isnull([Document_Total],0)) else case when Document_Type NOT IN ('I','C','RC') then  convert(decimal(18,2), isnull([Document_Total],0)) else 0 End end as [Current], " + Environment.NewLine &
               " case when Document_Type IN ('I','C','RC') then case when isnull(Vendor_Invoice_Date,'')='' then DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1 else DATEDIFF (day,convert(date, Vendor_Invoice_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 end else 0 end  as [Ageing_Days], "
                End If

            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble By Vendor Invoice Due Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsInAgeing = True Then
                    strUpperQry += "0 as [Current],case when isnull(VI_Due_Date,'')='' then DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1 else DATEDIFF (day,convert(date, VI_Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 end  as [Ageing_Days], " + Environment.NewLine
                Else
                    strUpperQry += " case when  Document_Type IN ('I','C','RC') and ( DATEDIFF (day,convert(date, VI_Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )<=0 then convert(decimal(18,2), isnull([Document_Total],0)) else case when Document_Type NOT IN ('I','C','RC') then  convert(decimal(18,2), isnull([Document_Total],0)) else 0 End end as [Current], " + Environment.NewLine &
               " case when Document_Type IN ('I','C','RC') then case when isnull(VI_Due_Date,'')='' then DATEDIFF (day,convert(date, DocDate ,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') +1 else DATEDIFF (day,convert(date, VI_Due_Date,101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 end else 0 end  as [Ageing_Days], "
                End If

            End If

            strUpperQry += " case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type], " + Environment.NewLine & _
            " '' AS From_Vendor, '' AS To_Vendor,  '" + ddlAgedPayble.Text + "' AS Report_Type, '" + strType + "' as [Summary], 'N' as [IsFifoBased] "


            If chkType.Checked = False Then
                strUpperQry += " ,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Add1+case  when isnull(TSPL_COMPANY_MASTER.Add2,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add2 +case  when isnull(TSPL_COMPANY_MASTER.Add3,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 end end as comp_address "
            End If

            strUpperQry += " ,Location ,Terms_Code FROM ( "
            Dim isonduedate As String = String.Empty
            If clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Due Date") = CompairStringResult.Equal Then
                isonduedate = "DueDate"
            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Document Date") = CompairStringResult.Equal Then
                isonduedate = "DocumentDate"
            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Vendor Invoice Date") = CompairStringResult.Equal Then
                isonduedate = "VIDate"
            ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble By Vendor Invoice Due Date") = CompairStringResult.Equal Then
                isonduedate = "VIDueDate"
            End If

            Dim strInnerQry As String = clsVendorMaster.GetOutStandingQry(dtpAgeof.Value.Date, clsCommon.GetPrintDate(dtpCutoffDate.Value.Date, "dd/MMM/yyyy"), Arr, ddlCurrencyType.SelectedValue, isonduedate, IIf(txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0, txtVendor.arrValueMember, Nothing), IIf(txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0, txtLocation.arrValueMember, Nothing), IIf(txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0, txtVendorGroup.arrValueMember, Nothing))
            If chkType.Checked = True Then
                strLowerQry = ") YYY WHERE Convert(Date, DocDate, 103) <=Convert(Date,'" + cutoffdate + "', 103) and docPosted =1 and isnull(refdocno,'') not in (Select document_no from TSPL_REVALUATION_detail where isnull(ap_invoice_no,'')<>'') and YYY.DocNo not in (Select tspl_vendor_invoice_head.Document_No from tspl_vendor_invoice_head inner join tspl_pr_head on tspl_pr_head.pr_no=tspl_vendor_invoice_head.Against_PurchaseReturn_No where   isnull(tspl_pr_head.Against_PI,'')<>'' and tspl_vendor_invoice_head.Vendor_Code =YYY.Vendor_Code  and tspl_vendor_invoice_head.document_type='D' UNION ALL  Select tspl_vendor_invoice_head.Document_No from tspl_vendor_invoice_head where   isnull(tspl_vendor_invoice_head.Against_POInvoice_No ,'')<>'' and tspl_vendor_invoice_head.Vendor_Code =YYY.Vendor_Code  and tspl_vendor_invoice_head.document_type='D' ) "
            Else
                strLowerQry = " ) YYY Left Outer Join TSPL_COMPANY_MASTER ON YYY.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code WHERE Convert(Date, DocDate, 103) <=Convert(Date,'" + cutoffdate + "', 103) and docPosted =1 and isnull(refdocno,'') not in (Select document_no from TSPL_REVALUATION_detail where isnull(ap_invoice_no,'')<>'') and YYY.DocNo not in (Select tspl_vendor_invoice_head.Document_No from tspl_vendor_invoice_head inner join tspl_pr_head on tspl_pr_head.pr_no=tspl_vendor_invoice_head.Against_PurchaseReturn_No where   isnull(tspl_pr_head.Against_PI,'')<>'' and tspl_vendor_invoice_head.Vendor_Code =YYY.Vendor_Code  and tspl_vendor_invoice_head.document_type='D'   UNION ALL  Select tspl_vendor_invoice_head.Document_No from tspl_vendor_invoice_head where   isnull(tspl_vendor_invoice_head.Against_POInvoice_No ,'')<>'' and tspl_vendor_invoice_head.Vendor_Code =YYY.Vendor_Code  and tspl_vendor_invoice_head.document_type='D' ) "
            End If
            ' Dim dtTemp As DataTable
            If chkFifo.Checked Then
                dt = clsDBFuncationality.GetDataTable(strUpperQry + strInnerQry + strLowerQry + "AND 1=2")
                Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable("Select Distinct Vendor_Code from ( " + strInnerQry + strLowerQry + "")
                For Each drVendor As DataRow In dtVendor.Rows
                    '--------------------FIFO(-ve balance)-------------------
                    Dim strFifoQry As String = strUpperQry + strInnerQry + strLowerQry
                    strFifoQry += " and Document_Type  in ('D','AV','OA')"
                    strFifoQry += " and Vendor_Code = '" + clsCommon.myCstr(drVendor("Vendor_Code")) + "'"
                    Dim dtFifo As DataTable = clsDBFuncationality.GetDataTable(strFifoQry)
                    '--------------------------------------------------------
                    '--------------------FIFO(+ve balance)-------------------
                    Dim strFifoQry1 As String = strUpperQry + strInnerQry + strLowerQry
                    strFifoQry1 += " and Vendor_Code = '" + clsCommon.myCstr(drVendor("Vendor_Code")) + "'"
                    strFifoQry1 += " and Document_Type NOT in ('D','AV','OA')"
                    Dim dtFifo1 As DataTable = clsDBFuncationality.GetDataTable(strFifoQry1)
                    '--------------------------------------------------------
                    If dtFifo1.Rows.Count <= 0 And dtFifo.Rows.Count > 0 And Not chkCreditBalance.Checked Then  '--Insert only -Ve Balance
                        For Each dr As DataRow In dtFifo.Rows
                            Dim dRow As DataRow = dt.NewRow()
                            dRow.ItemArray = dr.ItemArray
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count <= 0 Then                                   '--Insert only +Ve Balance
                        For Each dr As DataRow In dtFifo1.Rows
                            Dim dRow As DataRow = dt.NewRow()
                            dRow.ItemArray = dr.ItemArray
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count > 0 Then
                        Dim NegativeAmt As Double = Math.Round(clsCommon.myCdbl(dtFifo.Compute("Sum([Due Amount])", "") * -1), 0)
                        Dim PositiveAmt As Double = Math.Round(clsCommon.myCdbl(dtFifo1.Compute("Sum([Due Amount])", "")), 0)
                        If NegativeAmt > PositiveAmt And Not chkCreditBalance.Checked Then
                            Dim AppliedAmt As Double = clsCommon.myCdbl(dtFifo1.Compute("Sum([Due Amount])", ""))
                            For Each dr As DataRow In dtFifo.Rows
                                If AppliedAmt > 0 Then
                                    If (clsCommon.myCdbl(dr("Due Amount")) * -1) <= AppliedAmt Then
                                        AppliedAmt = AppliedAmt + clsCommon.myCdbl(dr("Due Amount"))
                                    Else
                                        dr.Item("Due Amount") = clsCommon.myCdbl(dr("Due Amount")) + AppliedAmt
                                        AppliedAmt = 0
                                        Dim dRow As DataRow = dt.NewRow()
                                        dRow.ItemArray = dr.ItemArray
                                        dt.Rows.Add(dRow)
                                    End If
                                Else
                                    Dim dRow As DataRow = dt.NewRow()
                                    dRow.ItemArray = dr.ItemArray
                                    dt.Rows.Add(dRow)
                                End If
                            Next
                        ElseIf NegativeAmt < PositiveAmt Then
                            Dim AppliedAmt As Double = clsCommon.myCdbl(dtFifo.Compute("Sum([Due Amount])", "") * -1)
                            For Each dr As DataRow In dtFifo1.Rows
                                If AppliedAmt > 0 Then
                                    If clsCommon.myCdbl(dr("Due Amount")) <= AppliedAmt Then
                                        AppliedAmt = AppliedAmt - clsCommon.myCdbl(dr("Due Amount"))
                                    Else
                                        dr.Item("Due Amount") = clsCommon.myCdbl(dr("Due Amount")) - AppliedAmt
                                        AppliedAmt = 0
                                        Dim dRow As DataRow = dt.NewRow()
                                        dRow.ItemArray = dr.ItemArray
                                        dt.Rows.Add(dRow)
                                    End If
                                Else
                                    Dim dRow As DataRow = dt.NewRow()
                                    dRow.ItemArray = dr.ItemArray
                                    dt.Rows.Add(dRow)
                                End If
                            Next
                        End If
                    End If
                Next
            Else
                If chkType.Checked = False Then
                    strLowerQry += " and Document_Type  in (" + clsCommon.GetMulcallString(Arr) + " )  "
                End If

                Dim qry As String = "select docPosted,REFDOCNO,RefDocType, RunDate,  AgeOfDate,  CutOfDate,  rptHeading,  First_Period,  Second_Period,  [Third Period],  [Fourth Period],  [Fifth Period]," + Environment.NewLine + _
                " [Sixth Period],  [Seventh Period], [Eight Period],[Nineth Period], [Over], Vendor_Group_Code ,  Cust_Group_Desc,  [Customer Id],  [Parent Code],  [Customer Name],  " + Environment.NewLine + _
                " [Document Id],  [Desc], Currency, ConvRate,    "
                If settingAllowtoShowDebitBalanceonVendorAgeing Then
                    qry += " case when ABS([Due Amount])>0.99 then [Due Amount] else 0 end as  [Due Amount], "
                Else
                    qry += "[Due Amount], "
                End If
                qry += " [Due Date], [Document Date],   [Current], [Ageing_Days],   [Document_Type],  " + Environment.NewLine + _
                " From_Vendor, To_Vendor,  Report_Type,  AgeofDate, [Summary], [IsFifoBased]  ,Location ,Terms_Code  from (" + strUpperQry + strInnerQry + strLowerQry + ")xx "
                If chkType.Checked = True Then
                    qry += " Order By Vendor_Code"
                Else
                    qry += " order by Vendor_Code,DocDate"
                End If

                dt = clsDBFuncationality.GetDataTable(qry)
            End If



            If chkType.Checked AndAlso Not chkShowZeroBalance.Checked Then
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = dt.Rows.Count - 1 To 0 Step -1
                        If clsCommon.myCdbl(dt.Rows(ii)("Due Amount")) = 0 AndAlso clsCommon.myCdbl(dt.Rows(ii)("Current")) = 0 Then
                            dt.Rows.RemoveAt(ii)
                        End If
                    Next
                End If

            Else
                If chkShowZeroBalance.Checked = False Then
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For ii As Integer = dt.Rows.Count - 1 To 0 Step -1
                            If clsCommon.myCdbl(dt.Rows(ii)("Due Amount")) = 0 AndAlso clsCommon.myCdbl(dt.Rows(ii)("Current")) = 0 Then
                                dt.Rows.RemoveAt(ii)
                            End If
                        Next
                    End If
                End If
            End If
            If Isgrid = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "crptAPAge" + strNo + "", "A/P Aged Paybles Report")
                frmCRV = Nothing
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = GetGridDT(dt)
                SetGridFormationOFGV1()
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub


    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False

        gv1.Columns("Customer Id").HeaderText = "Vendor Id"
        gv1.Columns("Customer Name").HeaderText = "Vendor Name"

        If chkType.Checked = False Then
            gv1.Columns("Document Date").HeaderText = "Document/Vendor Invoice Date"
            gv1.Columns("Due Date").HeaderText = "Due/Vendor Invoice Due Date"
        End If

        If chkType.Checked = True Then
            gv1.Columns("Current").HeaderText = "Adv/On-Ac/Debit Note"
        End If
        If ConsiderOpeningDocintoBucketsInAgeing = True Then
            gv1.Columns("Current").IsVisible = False
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()

        RadPageView1.SelectedPage = RadPageViewPage2
        gv1.BestFitColumns()
    End Sub

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        gv1.EnableFiltering = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        PrintNew1(True)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Public Function GetReportID() As String
        Dim strReportid As String = String.Empty
        If clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Due Date") = CompairStringResult.Equal Then
            strReportid = clsUserMgtCode.frmAgingPayble + "Due"
        ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Document Date") = CompairStringResult.Equal Then
            strReportid = clsUserMgtCode.frmAgingPayble + "Doc"
        ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble by Vendor Invoice Date") = CompairStringResult.Equal Then
            strReportid = clsUserMgtCode.frmAgingPayble + "VInv"
        ElseIf clsCommon.CompairString(ddlAgedPayble.Text, "Aged Payble By Vendor Invoice Due Date") = CompairStringResult.Equal Then
            strReportid = clsUserMgtCode.frmAgingPayble + "VDue"
        End If
        Return strReportid
    End Function

    Private Sub btnDeleteLayour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.CellDoubleClick
        ' If chkFifo.IsChecked Then
        If chkType.Checked = False Then
            If gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Document Type").Value)
                Dim strDoc = gv1.CurrentRow.Cells("Document Id").Value

                Select Case strTransType
                    Case "DR"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDoc)
                    Case "CR"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDoc)
                    Case "RC"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDoc)
                    Case "P"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDoc)
                    Case "AV"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDoc)
                    Case "OA"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDoc)
                    Case "IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDoc)
                End Select
            End If
        End If
       
        ' End If
    End Sub

    Private Sub chkVendorSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        ' cbgCustomer.Enabled = Not chkCustomerSelect.IsChecked
    End Sub

    Private Sub txt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIst.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt2nd.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt3rd.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt4th.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt5th.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt6th.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt7_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt7th.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub
    Private Sub txt8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt8th.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txtIst.Focus()
            Return
        End If
    End Sub

    Private Sub txt8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt8th.TextChanged
        Me.txtOver.Text = Me.txt8th.Text
    End Sub

    Private Sub chkCusSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLOcAll.IsChecked
    End Sub

    'Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    LoadCustomer()
    'End Sub

    'Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    LoadCustomer()
    'End Sub

    'Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    LoadCustomer()
    'End Sub

    Private Sub chkCGAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVGAll.ToggleStateChanged
        cbgVendorGroup.Enabled = False
    End Sub

    Private Sub chkCGSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVGSelect.ToggleStateChanged
        cbgVendorGroup.Enabled = True
    End Sub


    Private Sub chkType_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkType.ToggleStateChanged
        chkFifo.Checked = False
        If chkType.Checked Then
            chkFifo.Enabled = False
        Else
            chkFifo.Enabled = True
        End If
        chkShowZeroBalance.Visible = chkType.Checked
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        PrintNew1(False)
    End Sub

    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        LoadVendorGroup()
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        LoadVendor()
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        LoadLocation()

    End Sub

    Private Sub LoadVendorGroup()
        strQuery = "Select Ven_Group_Code as Code, Group_Desc as Description from TSPL_VENDOR_GROUP"
        'cbgVendorGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgVendorGroup.ValueMember = "Code"
        'cbgVendorGroup.DisplayMember = "Description"
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorAging", strQuery, "Code", "Description", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub

    Sub LoadVendor()
        strQuery = "select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        'cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgVendor.ValueMember = "Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@VendorAging", strQuery, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub LoadLocation()
        strQuery = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from" & _
         " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx" & _
        " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'" & _
        " order by xxx.Loc_Segment_Code"
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Name"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationSelector@VendorAging", strQuery, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub LoadCurrencyType()
        dt = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("ConvRate", "Functional Currency")
        dt.Rows.Add("1", "Vendor Currency")
        ddlCurrencyType.DataSource = dt
        ddlCurrencyType.ValueMember = "Code"
        ddlCurrencyType.DisplayMember = "Name"
    End Sub

    Private Sub FunExport(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmAgingPayble & "'"))
                arrHeader.Add("Age as of: " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy") + " cutoff Date " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))

                If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendorGroup.arrDispalyMember))
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If


                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF("Vendor Aging", gv1, arrHeader, "Vendor Aging", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub dtpAgeof_ValueChanged(sender As Object, e As EventArgs) Handles dtpAgeof.ValueChanged
        dtpCutoffDate.Value = dtpAgeof.Value
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        FunExport(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        FunExport(EnumExportTo.PDF)
    End Sub
End Class

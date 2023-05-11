Imports common
Imports System.Data.SqlClient

Public Class frmVerifyMPIFSC
    Inherits FrmMainTranScreen
#Region "variable"
    Public Const colMPCode As String = "Code"
    Public Const colMPUploader As String = "Uploader"
    Public Const colMPName As String = "Name"
    Public Const colSocietyUploader As String = "Society Uploader"
    Public Const colSocietyName As String = "Society"
    Public Const colIFSC As String = "IFSC Code"
    Public Const colBankName As String = "Bank Name"
    Public Const colBankBranch As String = "Bank Branch"
    Public Const colBankCity As String = "Bank City"
    Public Const colBankState As String = "Bank State"
    Public Const colError As String = "Error"
    Public Const colOK As String = "OK"
    Dim SettBankIFSCCodeValidateByService As Boolean = False
#End Region

    Private Sub FrmPrefixImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnSave.Visible = MyBase.isUpdateFlag
        SettBankIFSCCodeValidateByService = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.BankIFSCCodeValidateByService, clsFixedParameterCode.BankIFSCCodeValidateByService, Nothing)) > 1) ''Means 2 ERP or 3 Service And ERP
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where 2=2 "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@VMPIFSC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshVLC()
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub
    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("VLC@VMPIFSC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        AllowToSave()
    End Sub

    Function AllowToSave() As Boolean
        Try
            clsCommon.ProgressBarPercentShow()
            For ii As Integer = 0 To gv1.RowCount - 1
                clsCommon.ProgressBarPercentUpdate(((ii * 100) / (gv1.RowCount - 1)), "Validating IFSC " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
                gv1.Rows(ii).Cells(colError).Value = ""
                gv1.Rows(ii).Cells(colOK).Value = 0
                If clsCommon.myLen(gv1.Rows(ii).Cells(colIFSC).Value) > 0 Then
                    If SettBankIFSCCodeValidateByService Then
                        Dim arrFilter As New Dictionary(Of String, String)
                        arrFilter.Add("IFSC", clsCommon.myCstr(gv1.Rows(ii).Cells(colIFSC).Value))
                        arrFilter.Add("OutPutType", "1")
                        Dim dt As DataTable = Xtra.GetDataFromAPI(EnumAPI.BankIFSC, "GetIFSC", arrFilter)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            gv1.Rows(ii).Cells(colError).Value = "Invalid IFSC Code"
                        Else
                            If clsCommon.CompairString(dt.Rows(0)("Result"), "true") = CompairStringResult.Equal Then
                                If Not clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colBankName).Value), clsCommon.myCstr(dt.Rows(0)("BANK"))) = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells(colError).Value += "Correct Bank Name [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colBankName).Value) + "]"
                                ElseIf Not clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colBankBranch).Value), clsCommon.myCstr(dt.Rows(0)("BRANCH"))) = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells(colError).Value += "Correct Branch [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colBankName).Value) + "]"
                                ElseIf Not clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colBankState).Value), clsCommon.myCstr(dt.Rows(0)("STATE"))) = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells(colError).Value += "Correct State [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colBankName).Value) + "]"
                                ElseIf Not clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colBankCity).Value), clsCommon.myCstr(dt.Rows(0)("CITY"))) = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells(colError).Value += "Correct City [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colBankName).Value) + "]"
                                End If
                                gv1.Rows(ii).Cells(colBankName).Value = clsCommon.myCstr(dt.Rows(0)("BANK"))
                                gv1.Rows(ii).Cells(colBankBranch).Value = clsCommon.myCstr(dt.Rows(0)("BRANCH"))
                                gv1.Rows(ii).Cells(colBankState).Value = clsCommon.myCstr(dt.Rows(0)("STATE"))
                                gv1.Rows(ii).Cells(colBankCity).Value = clsCommon.myCstr(dt.Rows(0)("CITY"))
                                gv1.Rows(ii).Cells(colOK).Value = 1
                            Else
                                gv1.Rows(ii).Cells(colError).Value += clsCommon.myCstr(dt.Rows(0)("Response"))
                            End If
                        End If
                    Else
                        gv1.Rows(ii).Cells(colError).Value = "This facility is not for you"
                    End If
                Else
                    gv1.Rows(ii).Cells(colError).Value = "Blank IFSC Code"
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If gv1.Rows.Count > 0 Then
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colOK).Value) = 1 Then
                        Try
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "BankName", clsCommon.myCstr(gv1.Rows(ii).Cells(colBankName).Value))
                            clsCommon.AddColumnsForChange(coll, "BankBranch", clsCommon.myCstr(gv1.Rows(ii).Cells(colBankBranch).Value))
                            clsCommon.AddColumnsForChange(coll, "BankCityCode", clsCommon.myCstr(gv1.Rows(ii).Cells(colBankCity).Value))
                            clsCommon.AddColumnsForChange(coll, "BankStateCode", clsCommon.myCstr(gv1.Rows(ii).Cells(colBankState).Value))
                            clsCommon.AddColumnsForChange(coll, "IFCICode", clsCommon.myCstr(gv1.Rows(ii).Cells(colIFSC).Value))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_MASTER", OMInsertOrUpdate.Update, "MP_CODE='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colMPCode).Value) + "'")
                            gv1.Rows(ii).Cells(colError).Value = "Saved"
                        Catch ex As Exception
                            gv1.Rows(ii).Cells(colError).Value = ex.Message
                        End Try
                    End If
                Next
            End If
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Data Saved successfully", Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
            'If clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 1 Then
            '    e.RowElement.DrawFill = True
            '    e.RowElement.GradientStyle = GradientStyles.Solid
            '    e.RowElement.ForeColor = Color.Black
            '    e.RowElement.BackColor = Color.LightGreen
            'ElseIf clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 2 Then
            '    e.RowElement.DrawFill = True
            '    e.RowElement.GradientStyle = GradientStyles.Solid
            '    e.RowElement.ForeColor = Color.Black
            '    e.RowElement.BackColor = Color.MistyRose
            'Else
            '    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
            '    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
            '    e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
            '    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            'End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            loadBlankGrid()
            Dim qry As String = "select TSPL_MP_MASTER.MP_Code as [" + colMPCode + "],TSPL_MP_MASTER.MP_Code_VLC_Uploader as [" + colMPUploader + "],TSPL_MP_MASTER.MP_Name  as [" + colMPName + "],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [" + colSocietyUploader + "],TSPL_VLC_MASTER_HEAD.VLC_Name  as [" + colSocietyName + "],TSPL_MP_MASTER.IFCICode as [" + colIFSC + "],TSPL_MP_MASTER.BankName as [" + colBankName + "],TSPL_MP_MASTER.BankBranch as [" + colBankBranch + "],TSPL_MP_MASTER.BankCityCode as [" + colBankCity + "],TSPL_MP_MASTER.BankStateCode as [" + colBankState + "],'' as [" + colError + "],0 as [" + colOK + "] from TSPL_MP_MASTER
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
where 2=2  "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                qry += "and TSPL_MP_MASTER.VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found")
            End If

            gv1.DataSource = dt
            FormatGrid()

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For ii As Integer = 0 To dt.Rows.Count - 1
            '        gv1.Rows.AddNew()

            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colMPCode).Value = clsCommon.myCstr(dt.Rows(ii)("MP_Code"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colMPUploader).Value = clsCommon.myCstr(dt.Rows(ii)("MP_Code_VLC_Uploader"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colMPName).Value = clsCommon.myCstr(dt.Rows(ii)("MP_Name"))

            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyUploader).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Code_VLC_Uploader"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyName).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Name"))

            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colIFSC).Value = clsCommon.myCstr(dt.Rows(ii)("IFCICode"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankName).Value = clsCommon.myCstr(dt.Rows(ii)("BankName"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankBranch).Value = clsCommon.myCstr(dt.Rows(ii)("BankBranch"))

            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankCity).Value = clsCommon.myCstr(dt.Rows(ii)("BankCityCode"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankState).Value = clsCommon.myCstr(dt.Rows(ii)("BankStateCode"))
            '    Next
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub FormatGrid()
        Try

            gv1.Columns(colMPCode).ReadOnly = True
            gv1.Columns(colMPCode).IsVisible = True
            gv1.Columns(colMPCode).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colMPCode).Width = 100
            gv1.Columns(colMPCode).HeaderText = colMPCode

            gv1.Columns(colMPUploader).FormatString = ""
            gv1.Columns(colMPUploader).Width = 100
            gv1.Columns(colMPUploader).ReadOnly = True
            gv1.Columns(colMPUploader).IsVisible = True
            gv1.Columns(colMPUploader).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colMPUploader).HeaderText = colMPUploader


            gv1.Columns(colMPName).FormatString = ""
            gv1.Columns(colMPName).ReadOnly = True
            gv1.Columns(colMPName).IsVisible = True
            gv1.Columns(colMPName).Width = 150
            gv1.Columns(colMPName).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colMPName).HeaderText = colMPName


            gv1.Columns(colSocietyUploader).FormatString = ""
            gv1.Columns(colSocietyUploader).ReadOnly = True
            gv1.Columns(colSocietyUploader).IsVisible = True
            gv1.Columns(colSocietyUploader).Width = 100
            gv1.Columns(colSocietyUploader).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colSocietyUploader).HeaderText = colSocietyUploader



            gv1.Columns(colSocietyName).FormatString = ""
            gv1.Columns(colSocietyName).ReadOnly = True
            gv1.Columns(colSocietyName).IsVisible = True
            gv1.Columns(colSocietyName).Width = 150
            gv1.Columns(colSocietyName).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colSocietyName).HeaderText = colSocietyName


            gv1.Columns(colIFSC).FormatString = ""
            gv1.Columns(colIFSC).Width = 100
            gv1.Columns(colIFSC).ReadOnly = True
            gv1.Columns(colIFSC).Width = 150
            gv1.Columns(colIFSC).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colIFSC).HeaderText = colIFSC

            gv1.Columns(colBankName).FormatString = ""
            gv1.Columns(colBankName).ReadOnly = True
            gv1.Columns(colBankName).IsVisible = True
            gv1.Columns(colBankName).Width = 150
            gv1.Columns(colBankName).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colBankName).HeaderText = colBankName

            gv1.Columns(colBankBranch).FormatString = ""
            gv1.Columns(colBankBranch).Width = 150
            gv1.Columns(colBankBranch).ReadOnly = True
            gv1.Columns(colBankBranch).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colBankBranch).HeaderText = colBankBranch

            gv1.Columns(colBankCity).FormatString = ""
            gv1.Columns(colBankCity).Width = 100
            gv1.Columns(colBankCity).ReadOnly = True
            gv1.Columns(colBankCity).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colBankCity).HeaderText = colBankCity

            gv1.Columns(colBankState).FormatString = ""
            gv1.Columns(colBankState).Name = colBankState
            gv1.Columns(colBankState).Width = 150
            gv1.Columns(colBankState).ReadOnly = True
            gv1.Columns(colBankState).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colBankState).HeaderText = colBankState

            gv1.Columns(colError).FormatString = ""
            gv1.Columns(colError).Width = 200
            gv1.Columns(colError).ReadOnly = True
            gv1.Columns(colError).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colError).HeaderText = colError

            gv1.Columns(colOK).FormatString = ""
            gv1.Columns(colOK).ReadOnly = True
            gv1.Columns(colOK).IsVisible = False
            gv1.Columns(colOK).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.Columns(colOK).HeaderText = colOK





            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = True
            gv1.AllowRowReorder = False
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            gv1.EnableSorting = False
            gv1.EnableGrouping = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.GridBehavior = New MyBehavior()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            transportSql.exportdata(gv1, "", Me.Text, False, Nothing, False, False, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub loadBlankGrid()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            Dim gvImport As New RadGridView()
            Me.Controls.Add(gvImport)
            loadBlankGrid()
            If transportSql.importExcel(gvImport, colMPCode, colMPUploader, colMPName, colSocietyUploader, colSocietyName, colIFSC, colBankName, colBankBranch, colBankCity, colBankState) Then

                Dim ii As Integer = 0
                Try
                    Dim qry As String = ""
                    Dim ErrCount As Integer = 0
                    clsCommon.ProgressBarShow()
                    For ii = 0 To gvImport.RowCount - 1
                        If clsCommon.myLen(gvImport.Rows(ii).Cells(colMPCode).Value) > 0 Then
                            qry = "select MP_Code,MP_Name,MP_Code_VLC_Uploader from TSPL_MP_MASTER where MP_Code='" + clsCommon.myCstr(gvImport.Rows(ii).Cells(colMPCode).Value) + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt Is Nothing AndAlso dt.Rows.Count <= 0 Then
                                Throw New Exception("Invalid MP [" + clsCommon.myCstr(gvImport.Rows(ii).Cells(colMPCode).Value) + "]")
                            End If
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMPCode).Value = clsCommon.myCstr(dt.Rows(0)("MP_Code"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMPUploader).Value = clsCommon.myCstr(dt.Rows(0)("MP_Code_VLC_Uploader"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMPName).Value = clsCommon.myCstr(dt.Rows(0)("MP_Name"))

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyUploader).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colSocietyUploader).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colSocietyName).Value)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIFSC).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colIFSC).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBankName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankName).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBankBranch).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankBranch).Value)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBankCity).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankCity).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBankState).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankState).Value)
                        End If
                    Next
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Row No" + clsCommon.myCstr(ii + 1) + Environment.NewLine + ex.Message)
                Finally
                    clsCommon.ProgressBarHide()
                End Try
            End If
            Me.Controls.Remove(gvImport)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class

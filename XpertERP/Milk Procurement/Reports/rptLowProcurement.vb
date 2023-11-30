Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'created by preeti gupta ticket no[BM00000004214]
Public Class RptLowProcurement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    'Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptLowProcurement)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptLowProcurement & "'"))
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))


                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add((" MCC Name: " + strMCCName + " "))
                End If
                If chkStateSelect.IsChecked Then
                    Dim strStateName As String = ""
                    For Each StrName As String In cbgState.CheckedDisplayMember
                        If clsCommon.myLen(strStateName) > 0 Then
                            strStateName += ", "
                        End If
                        strStateName += StrName
                    Next
                    Dim strStateCode As String = ""
                    For Each StrCode As String In cbgState.CheckedValue
                        If clsCommon.myLen(strStateCode) > 0 Then
                            strStateCode += ", "
                        End If
                        strStateCode += StrCode
                    Next
                    arrHeader.Add(("State Name: " + strStateName + " "))
                End If

                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' and Location_Category ='MCC'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If

        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"

    End Sub
    Sub LoadState()
        Dim qry As String = "select STATE_CODE as [Code],STATE_NAME as [Name] from TSPL_STATE_MASTER  "
        cbgState.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgState.ValueMember = "Code"
        cbgState.DisplayMember = "Name"


    End Sub
    Sub Reset()
        LOCATIONRIGTHS()

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        LoadState()
        cboUnit.SelectedValue = "Kg"
        txtFromKg.Text = 0
        txtToKg.Text = 0
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        chkStateAll.CheckState = CheckState.Checked

        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub RptLowProcurement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
       
        'Loadunit()
        Reset()
    End Sub

    Private Sub chkStateAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStateAll.ToggleStateChanged
        cbgState.Enabled = Not chkStateAll.IsChecked
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Public Sub Load_Report()
        Try
            If clsCommon.myCdbl(txtFromKg.Text) >= clsCommon.myCdbl(txtToKg.Text) Then
                common.clsCommon.MyMessageBoxShow(Me, "From kg is not greater then To kg  or equal", Me.Text)
                lblMilkProcurementBetwwen.Focus()
                Exit Sub
            End If
            If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
                Exit Sub
            End If
            If chkStateSelect.IsChecked AndAlso cbgState.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single state or select all.", Me.Text)
                Exit Sub
            End If

            Dim whrcls As String = " where 2=2 "
            If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                whrcls += " and TSPL_MILK_RECEIPT_DETAIL.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            If chkStateSelect.IsChecked And cbgState.CheckedValue.Count > 0 Then
                whrcls += " and TSPL_MCC_MASTER.State_Code in (" + clsCommon.GetMulcallString(cbgState.CheckedValue) + ")  "
            End If


            whrcls += "  and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,('" + txtToDate.Value + "'),103) "

            ' Ticket No : BHA/21/11/18-000686 By Prabhakar - for Devided by Zero error
            ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
            'Sanjay , Data pick from table(Ltr/Kg)
            '" select State_Code  ,STATE_NAME  ,VLC_CODE  ,VLC_Name ,Vendor_Name ,MCC_CODE ,MCC_NAME  ,DOC_DATE ,Period_days   ,SHIFT  as shift  ,Weight as NewQty from(" & Environment.NewLine &
            Dim sQuery As String = "select '' as SNo,max(State_Code ) as State_Code,MAX(STATE_NAME ) as STATE_NAME,(VLC_CODE ) as VLC_CODE,MAX(VLC_Name ) as VLC_Name,max(VSP_Name ) as VSP_Name,MAX(MCC_CODE ) as MCC_CODE,MAX(MCC_NAME ) as MCC_NAME,convert(decimal(18,2),sum(NewQty )) as NewQty,count(shift) as No_of_Shift,convert(DECIMAL(18,2),sum(NewQty )/count(SHIFT)) as [Av.Per Shift],convert(DECIMAL(18,2),sum(NewQty )/MAX(Period_days)) as [Av.Per day] ,max([VLC Uploader Code]) as [VLC Uploader Code]  from (select max(State_Code ) as State_Code,MAX(STATE_NAME ) as STATE_NAME,(VLC_CODE ) as VLC_CODE,MAX(VLC_Name ) as VLC_Name,max(Vendor_Name) as VSP_Name ," & Environment.NewLine &
            " MAX(MCC_CODE ) as MCC_CODE,MAX(MCC_NAME ) as MCC_NAME,MAX(DOC_DATE) as Date,sum(NewQty ) as NewQty,shift  ,max(Period_days )as Period_days ,max([VLC Uploader Code]) as [VLC Uploader Code] from (" & Environment.NewLine &
            " select TSPL_MILK_RECEIPT_DETAIL.item_code, TSPL_MCC_MASTER.State_Code  ,TSPL_STATE_MASTER.STATE_NAME ,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE ,TSPL_VLC_MASTER_HEAD.VLC_Name  ,TSPL_MILK_RECEIPT_DETAIL.VSP_CODE ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_MILK_RECEIPT_DETAIL.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME ," & Environment.NewLine &
            " TSPL_MILK_RECEIPT_HEAD.DOC_DATE  ," & Environment.NewLine &
            " DateDiff(day,convert(date,'" + txtFromDate.Value + "',103),convert(date,'" + txtToDate.Value + "',103))+1  as Period_days  " & Environment.NewLine
            If clsCommon.CompairString(cboUnit.Text, "ltr") = CompairStringResult.Equal Then
                sQuery += " ,(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR) as [NewQty]"
            ElseIf clsCommon.CompairString(cboUnit.Text, "Kg") = CompairStringResult.Equal Then
                sQuery += " ,(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT) as [NewQty]"
            Else
                sQuery += " ,(TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT) as [NewQty]"
            End If
            sQuery += ",TSPL_MILK_RECEIPT_DETAIL.UOM_Code ,TSPL_MILK_RECEIPT_HEAD .SHIFT,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code]" & Environment.NewLine &
            " from TSPL_MILK_RECEIPT_DETAIL " & Environment.NewLine &
            " left outer join TSPL_MILK_RECEIPT_HEAD  on TSPL_MILK_RECEIPT_HEAD .DOC_CODE =TSPL_MILK_RECEIPT_DETAIL .DOC_CODE  " & Environment.NewLine &
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & Environment.NewLine &
            " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Form_Type='VSP' and TSPL_MILK_RECEIPT_DETAIL .VSP_CODE =TSPL_VENDOR_MASTER .Vendor_Code" & Environment.NewLine &
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & Environment.NewLine &
            " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_MCC_MASTER.State_Code " & Environment.NewLine &
            " " + whrcls + " " & Environment.NewLine &
            " )xx " & Environment.NewLine
            '" LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

            'If ItemStructureMandatoryOnWeightConversion = True Then
            '    sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code )tt "
            'Else
            '    sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code and lower(zzz.TOUOM)='" + cboUnit.Text + "' )tt "
            'End If

            If clsCommon.myCdbl(txtFromKg.Text) >= 0 AndAlso clsCommon.myCdbl(txtToKg.Text) > 0 Then
                sQuery += " where  xx.NewQty >=" + txtFromKg.Text + " and xx.NewQty <=" + txtToKg.Text + " "
            End If
            sQuery += "group by DOC_DATE ,VLC_CODE ,shift  ) as xxxxx group by VLC_CODE,MCC_CODE "
            'If clsCommon.myCdbl(txtFromKg.Text) >= 0 AndAlso clsCommon.myCdbl(txtToKg.Text) > 0 Then
            '    'sQuery += " where  tt.NewQty >=" + txtFromKg.Text + " and tt.NewQty <=" + txtToKg.Text + " "
            '    sQuery += " having ( convert(decimal(18,2),sum(NewQty )) >" + txtFromKg.Text + " and convert(decimal(18,2),sum(NewQty )) <=" + txtToKg.Text + ") "
            'End If
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                For i As Integer = 0 To gv.Rows.Count - 1
                    gv.Rows(i).Cells(0).Value = i + 1
                Next
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            
        Next
        gv.Columns("SNo").IsVisible = True
        gv.Columns("SNo").Width = 50
        gv.Columns("SNo").HeaderText = "S.NO."

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC Name"

        gv.Columns("VSP_Name").IsVisible = True
        gv.Columns("VSP_Name").Width = 100
        gv.Columns("VSP_Name").HeaderText = " VSP Name"

        gv.Columns("NewQty").IsVisible = True
        gv.Columns("NewQty").Width = 100
        gv.Columns("NewQty").HeaderText = "Qty in period"


        gv.Columns("No_of_Shift").IsVisible = True
        gv.Columns("No_of_Shift").Width = 100
        gv.Columns("No_of_Shift").HeaderText = "No. of Shift"


        gv.Columns("Av.Per Shift").IsVisible = True
        gv.Columns("Av.Per Shift").Width = 100
        gv.Columns("Av.Per Shift").HeaderText = "Av.Per Shift"


        gv.Columns("Av.Per Day").IsVisible = True
        gv.Columns("Av.Per Day").Width = 100
        gv.Columns("Av.Per Day").HeaderText = " Av.Per Day"

        gv.Columns("State_Code").IsVisible = False
        gv.Columns("State_Code").Width = 100
        gv.Columns("State_Code").HeaderText = "State Code"

        gv.Columns("STATE_NAME").IsVisible = False
        gv.Columns("STATE_NAME").Width = 100
        gv.Columns("STATE_NAME").HeaderText = "State Name"

        gv.Columns("VLC_CODE").IsVisible = False
        gv.Columns("VLC_CODE").Width = 100
        gv.Columns("VLC_CODE").HeaderText = "VLC Code"

        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = "MCC Name"

        gv.Columns("MCC_CODE").IsVisible = False
        gv.Columns("MCC_CODE").Width = 100
        gv.Columns("MCC_CODE").HeaderText = "MCC Code"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("NewQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim intCount As Integer = 0
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptLowProcurement & "'"))
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add((" MCC Name: " + strMCCName + " "))
                End If
                If chkStateSelect.IsChecked Then
                    Dim strStateName As String = ""
                    For Each StrName As String In cbgState.CheckedDisplayMember
                        If clsCommon.myLen(strStateName) > 0 Then
                            strStateName += ", "
                        End If
                        strStateName += StrName
                    Next
                    Dim strStateCode As String = ""
                    For Each StrCode As String In cbgState.CheckedValue
                        If clsCommon.myLen(strStateCode) > 0 Then
                            strStateCode += ", "
                        End If
                        strStateCode += StrCode
                    Next
                    arrHeader.Add(("State Name: " + strStateName + " "))
                End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Low Procurement", gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("Low Procurement", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptLowProcurement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub txtFromKg_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFromKg.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtToKg_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtToKg.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub


    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class

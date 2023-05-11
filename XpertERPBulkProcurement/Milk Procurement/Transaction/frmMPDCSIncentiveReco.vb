Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmMPDCSIncentiveReco
    Inherits FrmMainTranScreen
#Region "Variables"
    Public Const colPKID As String = "colPKID"
    Public Const colSlNo As String = "SLNO"
    Public Const colRecoStatus As String = "colRecoStatus"
    Public Const colBMCCode As String = "colBMCCode"
    Public Const colBMCName As String = "colBMCName"
    Public Const colRouteCode As String = "colRouteCode"
    Public Const colRouteName As String = "colRouteName"
    Public Const colZoneCode As String = "colZoneCode"
    Public Const colZoneName As String = "colZoneName"
    Public Const colVLCCode As String = "colVLCCode"
    Public Const colVLCUploaderCode As String = "colVLCUploaderCode"
    Public Const colVLCName As String = "colVLCName"
    Public Const colYear As String = "Year"
    Public Const colMonth As String = "Month"
    Public Const colCycleNo As String = "Cycle No"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "colUOM"
    Public Const colFAT As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colAmount As String = "colAmount"
    Public Const colMPCount As String = "colMPCount"
    Public Const colMPQty As String = "colMPQty"
    Public Const colMPFAT As String = "colMPFAT"
    Public Const colMPSNF As String = "colMPSNF"
    Public Const colMPAmt As String = "colMPAmt"

    Public Const colDiffQty As String = "colDiffQty"
    Public Const colDiffFAT As String = "colDiffFAT"
    Public Const colDiffSNF As String = "colDiffSNF"
    Public Const colDiffAmt As String = "colDiffAmt"



    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim IsinsideLoadData As Boolean = False
    Dim Qry As String
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing

    Dim strICode As String = ""
    Dim SettDCSMPIncetiveReco As Boolean = False
    Dim SettMatchingUOM As String = ""
    Dim SettAllowMPQtyGreaterThanDCSQty As Boolean = False
    Dim SettAllowMPQtyLessThanDCSQty As Boolean = False
    Dim SettAllowMPQtyEqualToDCSQty As Boolean = False
    Dim SettMPIncentiveEntryApplyMonthly As Boolean = False
    Dim SettApplyZoneOnDBT As Boolean = False
    Dim SettPickMilkPurchaseInvoiceQtyOrRecoQty As Boolean = False
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmVLCDataUploaderManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SettMPIncentiveEntryApplyMonthly = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryApplyMonthly, clsFixedParameterCode.MPIncentiveEntryApplyMonthly, Nothing)) > 0)

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.isDeleteTheAttachment = False
        UcAttachment1.settAutoAttachment = True

        strICode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)
        SettApplyZoneOnDBT = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyZoneInDBT, clsFixedParameterCode.ApplyZoneInDBT, Nothing)) > 0)
        txtZone.MendatroryField = SettApplyZoneOnDBT
        SettDCSMPIncetiveReco = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DCSRecoCondition, clsFixedParameterCode.MandatoryDCSMPIncetiveReco, Nothing)) = 1)
        SettMatchingUOM = clsFixedParameter.GetData(clsFixedParameterType.DCSRecoCondition, clsFixedParameterCode.MatchingUOM, Nothing)
        SettPickMilkPurchaseInvoiceQtyOrRecoQty = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.PickMilkPurchaseInvoiceQtyOrRecoQty, clsFixedParameterCode.PickMilkPurchaseInvoiceQtyOrRecoQty, Nothing)) = 1)
        SettAllowMPQtyGreaterThanDCSQty = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DCSRecoCondition, clsFixedParameterCode.AllowMPQtyGreaterThanDCSQty, Nothing)) = 1)
        SettAllowMPQtyLessThanDCSQty = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DCSRecoCondition, clsFixedParameterCode.AllowMPQtyLessThanDCSQty, Nothing)) = 1)
        SettAllowMPQtyEqualToDCSQty = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DCSRecoCondition, clsFixedParameterCode.AllowMPQtyEqualToDCSQty, Nothing)) = 1)

        SetUserMgmtNew()
        Reset()

        RadButton4.Visible = SettPickMilkPurchaseInvoiceQtyOrRecoQty
        RadButton1.Visible = Not SettPickMilkPurchaseInvoiceQtyOrRecoQty
        RadButton2.Visible = Not SettPickMilkPurchaseInvoiceQtyOrRecoQty
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        If SettApplyZoneOnDBT Then
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.Panel2Collapsed = True
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
            SplitContainer3.Panel1Collapsed = True
            SplitContainer3.Panel2Collapsed = False

            RadButton4.Visible = False
            RadButton1.Visible = False
            RadButton2.Visible = False
        Else
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.Panel2Collapsed = True
        End If

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Me.Focus()
        txtdate.Focus()
    End Sub
    Private Sub FrmVLCDataUploaderManual_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnclose.Enabled Then
            'PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                      "========Table Name=========" + Environment.NewLine +
                      "TSPL_DCS_MP_INCENTIVE_RECO_HEAD" + Environment.NewLine +
                      "TSPL_DCS_MP_INCENTIVE_RECO_DETAIL" + Environment.NewLine)
        End If
    End Sub
    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            Qry = "select TSPL_VLC_MASTER_HEAD.MCC as [BMC Code],TSPL_MCC_MASTER.MCC_NAME as  [BMC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as  [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name as  [DCS Name],DATEPART(YEAR, GETDATE()) as [Year],DATEPART(MONTH, GETDATE()) [Month],0 as [Cycle No],0 as Qty,'' as UOM,0 as FAT,0 as  SNF,0 as  Amount
from TSPL_VLC_MASTER_HEAD
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC "
            transportSql.ExporttoExcel(Qry, "", "[BMC Name],[DCS Code]", Me)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        rmimport_Click()
    End Sub
    Private Sub rmimport_Click()
        UcAttachment1.BlankAllControls()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Dim ii As Integer = 0
        Try
            If Not SettDCSMPIncetiveReco Then
                Throw New Exception("This facility is not for you")
            End If
            loadBlankGrid()
            Dim FileName As String = ""
            Dim SafeFileName As String = ""
            If transportSql.importExcel(FileName, SafeFileName, dgv, "BMC Code", "BMC Name", "DCS Code", "DCS Name", "Year", "Month", "Cycle No", "Qty", "UOM", "FAT", "SNF", "Amount") Then
                UcAttachment1.AddAttachment(FileName, SafeFileName)
                For ii = 0 To dgv.Rows.Count - 1
                    If clsCommon.myLen(dgv.Rows(ii).Cells("BMC Code").Value) > 0 AndAlso clsCommon.myLen(dgv.Rows(ii).Cells("DCS Code").Value) > 0 AndAlso clsCommon.myCDecimal(dgv.Rows(ii).Cells("Qty").Value) > 0 Then
                        Qry = "select TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VENDOR_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description as Zone_Name from TSPL_VLC_MASTER_HEAD 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
where TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(dgv.Rows(ii).Cells("BMC Code").Value) + "' and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(dgv.Rows(ii).Cells("DCS Code").Value) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Invalid combinition of BMC [" + clsCommon.myCstr(dgv.Rows(ii).Cells("BMC Code").Value) + "] and DCS [" + clsCommon.myCstr(dgv.Rows(ii).Cells("DCS Code").Value) + "]")
                        End If
                        If clsCommon.myCDecimal(dgv.Rows(ii).Cells("Year").Value) <> txtFromDate.Value.Year Then
                            Throw New Exception("Year is Mismatched")
                        End If
                        If clsCommon.myCDecimal(dgv.Rows(ii).Cells("Month").Value) <> txtFromDate.Value.Month Then
                            Throw New Exception("Month is Mismatched")
                        End If
                        If clsCommon.myCDecimal(dgv.Rows(ii).Cells("Cycle No").Value) <> CycleNo Then
                            Throw New Exception("Cycle No is Mismatched")
                        End If
                        Qry = "select PK_Id from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL  where TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code not in ('" + txtDocumentNo.Value + "') and Cycle_Year='" + clsCommon.myCstr(dgv.Rows(ii).Cells("Year").Value) + "' and Cycle_Month='" + clsCommon.myCstr(dgv.Rows(ii).Cells("Month").Value) + "' and Cycle_No='" + clsCommon.myCstr(dgv.Rows(ii).Cells("Cycle No").Value) + "' and  VLC_Code='" + clsCommon.myCstr(dt.Rows(0)("VLC_Code")) + "'"
                        Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(Qry)
                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                            Continue For
                        End If


                        gvItem.Rows.AddNew()
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBMCCode).Value = clsCommon.myCstr(dt.Rows(0)("MCC"))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBMCName).Value = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colZoneCode).Value = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colZoneName).Value = clsCommon.myCstr(dt.Rows(0)("Zone_Name"))

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCUploaderCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader"))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCName).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colYear).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Year").Value)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMonth).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Month").Value)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colCycleNo).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Cycle No").Value)

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Qty").Value)
                        If clsCommon.myLen(dgv.Rows(ii).Cells("UOM").Value) <= 0 Then
                            Throw New Exception("Please define UOM")
                        End If
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dgv.Rows(ii).Cells("UOM").Value)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFAT).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("FAT").Value)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("SNF").Value)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount).Value = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Amount").Value)

                        FillFarmerInfo(gvItem.Rows.Count - 1)
                    End If
                Next
            End If
            clsCommon.MyMessageBoxShow(Me, "Data Imported", Me.Text, MessageBoxButtons.OK)
        Catch ex As Exception
            loadBlankGrid()
            clsCommon.MyMessageBoxShow(Me, "Error at Row No [" + clsCommon.myCstr(ii) + "]" + Environment.NewLine + ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub FillFarmerInfo(ByVal Indx As Integer)
        Dim Qry As String = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" + clsCommon.myCstr(gvItem.Rows(Indx).Cells(colUOM).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Invalid UOM [" + clsCommon.myCstr(gvItem.Rows(Indx).Cells(colUOM).Value) + "] for Item [" + strICode + "]")
        End If
        Dim MulConvFat As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Conversion_Factor"))
        gvItem.Rows(Indx).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("UOM_Code"))

        Qry = "select sum(isnull(Qty,0)) as Qty,sum(FAT_Kg) as FAT_Kg,sum(SNF_Kg) as SNF_Kg,sum(Amount) as Amount,count(MP_Code) as No_MP_Code from (select Qty,Amount,FAT_Kg,SNF_Kg,MP_Code from (
select  '" + strICode + "' as Item,TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code,Qty,case when len(isnull(UOM,''))<=0 then 'KG' else UOM end as UOM ,Amount,isnull(TSPL_MP_INCENTIVE_ENTRY_DETAIL.FAT_Kg,0) as FAT_Kg,isnull(TSPL_MP_INCENTIVE_ENTRY_DETAIL.SNF_Kg,0) as SNF_Kg from TSPL_MP_INCENTIVE_ENTRY_DETAIL  where VLC_Code='" + clsCommon.myCstr(gvItem.Rows(Indx).Cells(colVLCCode).Value) + "' and Cycle_Month=" + clsCommon.myCstr(clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMonth).Value)) + " and Cycle_Year=" + clsCommon.myCstr(clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colYear).Value)) + " and Cycle_No=" + clsCommon.myCstr(clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colCycleNo).Value)) + " 
)xx  )xxx"
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvItem.Rows(Indx).Cells(colMPCount).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0)("No_MP_Code")), 0, MidpointRounding.ToEven)
            gvItem.Rows(Indx).Cells(colMPQty).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0)("Qty")), 2, MidpointRounding.ToEven)
            gvItem.Rows(Indx).Cells(colMPFAT).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0)("FAT_Kg")), 2, MidpointRounding.ToEven)
            gvItem.Rows(Indx).Cells(colMPSNF).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0)("SNF_Kg")), 2, MidpointRounding.ToEven)
            gvItem.Rows(Indx).Cells(colMPAmt).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0)("Amount")), 2, MidpointRounding.ToEven)
        End If
        If clsCommon.myLen(SettMatchingUOM) > 0 Then
            If Not clsCommon.CompairString(clsCommon.myCstr(gvItem.Rows(Indx).Cells(colUOM).Value), SettMatchingUOM) = CompairStringResult.Equal Then
                Qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" + SettMatchingUOM + "'"
                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Invalid UOM [" + clsCommon.myCstr(gvItem.Rows(Indx).Cells(colUOM).Value) + "] for Item [" + strICode + "]")
                End If
                Dim DivConvFat As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Conversion_Factor"))
                If DivConvFat <= 0 Then
                    Throw New Exception("Convertsion Factor Can't be [" + clsCommon.myCstr(DivConvFat) + "]  For Item [" + strICode + "] and UOM [" + clsCommon.myCstr(gvItem.Rows(Indx).Cells(colUOM).Value) + "] ")
                End If
                gvItem.Rows(Indx).Cells(colQty).Value = Math.Round(clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colQty).Value) * MulConvFat / DivConvFat, 2, MidpointRounding.AwayFromZero)
                gvItem.Rows(Indx).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("UOM_Code"))
            End If
            Qry += " ((Qty*MulConv.Conversion_Factor)/DivConv.Conversion_Factor) as Qty "
        End If


        gvItem.Rows(Indx).Cells(colDiffQty).Value = clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colQty).Value) - clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPQty).Value)
        gvItem.Rows(Indx).Cells(colDiffFAT).Value = clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colFAT).Value) - clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPFAT).Value)
        gvItem.Rows(Indx).Cells(colDiffSNF).Value = clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colSNF).Value) - clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPSNF).Value)
        gvItem.Rows(Indx).Cells(colDiffAmt).Value = clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colAmount).Value) - clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPAmt).Value)


        Dim RecoStatus As Boolean = False
        If clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPQty).Value) > 0 Then
            If SettAllowMPQtyGreaterThanDCSQty And (clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPQty).Value) > clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colQty).Value)) Then
                RecoStatus = True
            End If
            If SettAllowMPQtyLessThanDCSQty And (clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPQty).Value) < clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colQty).Value)) Then
                RecoStatus = True
            End If
            If SettAllowMPQtyEqualToDCSQty And (clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colMPQty).Value) = clsCommon.myCDecimal(gvItem.Rows(Indx).Cells(colQty).Value)) Then
                RecoStatus = True
            End If
        End If
        gvItem.Rows(Indx).Cells(colRecoStatus).Value = RecoStatus
    End Sub

    Sub loadBlankGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing
        Dim farmercode As New GridViewTextBoxColumn()

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "PKID."
        lineNo.Name = colPKID
        lineNo.IsVisible = False
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(lineNo)

        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SNo."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(lineNo)

        Dim repoIsSurTax1 As New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Status"
        repoIsSurTax1.Name = colRecoStatus
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = True
        repoIsSurTax1.Width = 50
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax1) '30



        farmercode = New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "BMC Code"
        farmercode.Name = colBMCCode
        farmercode.ReadOnly = True
        farmercode.Width = 100
        farmercode.IsVisible = True
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)



        Dim farmername As New GridViewTextBoxColumn()
        farmername.FormatString = ""
        farmername.HeaderText = "BMC Name"
        farmername.Name = colBMCName
        farmername.Width = 150
        farmername.ReadOnly = True
        farmername.IsVisible = True
        farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmername)

        Dim routecode As New GridViewTextBoxColumn()
        routecode.FormatString = ""
        routecode.HeaderText = "Route Code"
        routecode.Name = colRouteCode
        routecode.Width = 150
        routecode.ReadOnly = True
        routecode.IsVisible = True
        routecode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(routecode)

        Dim routename As New GridViewTextBoxColumn()
        routename.FormatString = ""
        routename.HeaderText = "Route Name"
        routename.Name = colRouteName
        routename.Width = 150
        routename.ReadOnly = True
        routename.IsVisible = True
        routename.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(routename)

        farmercode = New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "Zone Code"
        farmercode.Name = colZoneCode
        farmercode.ReadOnly = True
        farmercode.IsVisible = False
        farmercode.Width = 150
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)

        farmercode = New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "Zone"
        farmercode.Name = colZoneName
        farmercode.ReadOnly = True
        farmercode.IsVisible = True
        farmercode.Width = 150
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)

        farmercode = New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "DCS"
        farmercode.Name = colVLCUploaderCode
        farmercode.ReadOnly = True
        farmercode.IsVisible = True
        farmercode.Width = 80
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)

        farmercode = New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "DCS Code"
        farmercode.Name = colVLCCode
        farmercode.ReadOnly = True
        farmercode.IsVisible = False
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)

        farmercode = New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "DCS Name"
        farmercode.Name = colVLCName
        farmercode.ReadOnly = True
        farmercode.IsVisible = True
        farmercode.Width = 150
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)

        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Year"
        lineNo.Name = colYear
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(lineNo)

        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Month"
        lineNo.Name = colMonth
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(lineNo)

        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Cycle No"
        lineNo.Name = colCycleNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(lineNo)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "Qty"
        Qty.Name = colQty
        Qty.Width = 100
        Qty.ReadOnly = True
        Qty.FormatString = "{0:n2}"
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        Dim UOM As New GridViewTextBoxColumn()
        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 80
        UOM.ReadOnly = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(UOM)

        Qty = New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "FAT"
        Qty.Name = colFAT
        Qty.Width = 80
        Qty.ReadOnly = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        Qty = New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "SNF"
        Qty.Name = colSNF
        Qty.Width = 80
        Qty.ReadOnly = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = ""
        Amount.HeaderText = "Amount"
        Amount.Name = colAmount
        Amount.Width = 100
        Amount.FormatString = "{0:n2}"
        Amount.ReadOnly = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Amount)

        Dim AmountActual As New GridViewDecimalColumn
        AmountActual.FormatString = ""
        AmountActual.HeaderText = "No Of Farmers"
        AmountActual.Name = colMPCount
        AmountActual.Width = 100
        AmountActual.FormatString = "{0:n0}"
        AmountActual.ReadOnly = True
        AmountActual.IsVisible = True
        AmountActual.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(AmountActual)


        AmountActual = New GridViewDecimalColumn
        AmountActual.FormatString = ""
        AmountActual.HeaderText = "MP Qty"
        AmountActual.Name = colMPQty
        AmountActual.Width = 100
        AmountActual.FormatString = "{0:n2}"
        AmountActual.ReadOnly = True
        AmountActual.IsVisible = True
        AmountActual.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(AmountActual)

        Qty = New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "MP FAT"
        Qty.Name = colMPFAT
        Qty.Width = 80
        Qty.ReadOnly = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        Qty = New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "MP SNF"
        Qty.Name = colMPSNF
        Qty.Width = 80
        Qty.ReadOnly = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        AmountActual = New GridViewDecimalColumn
        AmountActual.FormatString = ""
        AmountActual.HeaderText = "MP Amount"
        AmountActual.Name = colMPAmt
        AmountActual.Width = 100
        AmountActual.FormatString = "{0:n2}"
        AmountActual.ReadOnly = True
        AmountActual.IsVisible = True
        AmountActual.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(AmountActual)


        AmountActual = New GridViewDecimalColumn
        AmountActual.FormatString = ""
        AmountActual.HeaderText = "Diff Qty"
        AmountActual.Name = colDiffQty
        AmountActual.Width = 100
        AmountActual.FormatString = "{0:n2}"
        AmountActual.ReadOnly = True
        AmountActual.IsVisible = True
        AmountActual.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(AmountActual)

        Qty = New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "Diff FAT"
        Qty.Name = colDiffFAT
        Qty.Width = 80
        Qty.ReadOnly = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        Qty = New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "Diff SNF"
        Qty.Name = colDiffSNF
        Qty.Width = 80
        Qty.ReadOnly = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Qty)

        AmountActual = New GridViewDecimalColumn
        AmountActual.FormatString = ""
        AmountActual.HeaderText = "Diff Amount"
        AmountActual.Name = colDiffAmt
        AmountActual.Width = 100
        AmountActual.FormatString = "{0:n2}"
        AmountActual.ReadOnly = True
        AmountActual.IsVisible = True
        AmountActual.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(AmountActual)


        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = True
        gvItem.ShowFilteringRow = True
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvItem.GridBehavior = New MyBehavior()

        ReStoreGridLayout()

        gvItem.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Smitem As New GridViewSummaryItem(colQty, "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)

        Smitem = New GridViewSummaryItem(colAmount, "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)



        Smitem = New GridViewSummaryItem(colMPCount, "{0:n0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)

        Smitem = New GridViewSummaryItem(colMPQty, "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)

        Smitem = New GridViewSummaryItem(colMPAmt, "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)

        Smitem = New GridViewSummaryItem(colDiffQty, "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)

        Smitem = New GridViewSummaryItem(colDiffAmt, "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        gvItem.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            '  If rbtnNEFT.IsChecked Then
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Me.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
                        gvItem.Columns(ii).IsVisible = False
                        gvItem.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItem.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmVLCDataUploaderManual)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub Reset()
        loadBlankGrid()
        UcAttachment1.BlankAllControls()
        Dim dt As Date = clsCommon.GETSERVERDATE()

        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        If SettMPIncentiveEntryApplyMonthly Then
            txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
        Else
            txtToDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        End If


        txtDocumentNo.Value = ""
        txtdate.Value = dt
        txtZone.Value = ""
        lblZone.Text = ""
        txtDocumentNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        btnPost.Enabled = False
        txtdate.Focus()
        EnableInputDataField()
        isNewEntry = True
        IsinsideLoadData = False
        lblPending.Status = ERPTransactionStatus.Pending

    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtdate.Value, Nothing) = False Then
            txtdate.Focus()
            Return False
        End If
        If SettApplyZoneOnDBT Then
            If clsCommon.myLen(txtZone.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select " + txtZone.MyLinkLable2.Text)
                txtZone.Focus()
                Return False
            End If
        End If
        UcAttachment1.AllowToSave()
        SetToDate()
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsMPDCSInsentiveReco()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_Date = txtdate.Value
                obj.Zone_Code = txtZone.Value
                obj.Reco_Date = txtFromDate.Value
                obj.Reco_Date_To = txtToDate.Value
                Dim objTr As New clsMPDCSInsentiveRecoDetail
                obj.arr = New List(Of clsMPDCSInsentiveRecoDetail)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    If clsCommon.myLen(grow.Cells(colQty).Value) > 0 Then
                        objTr = New clsMPDCSInsentiveRecoDetail()
                        objTr.PK_Id = clsCommon.myCDecimal(grow.Cells(colPKID).Value)
                        objTr.SNo = obj.arr.Count + 1
                        objTr.Reco_Staus = clsCommon.myCBool(grow.Cells(colRecoStatus).Value)
                        objTr.MCC_Code = clsCommon.myCstr(grow.Cells(colBMCCode).Value)
                        objTr.VLC_Code = clsCommon.myCstr(grow.Cells(colVLCCode).Value)
                        objTr.Cycle_Year = clsCommon.myCstr(grow.Cells(colYear).Value)
                        objTr.Cycle_Month = clsCommon.myCstr(grow.Cells(colMonth).Value)
                        objTr.Cycle_No = clsCommon.myCstr(grow.Cells(colCycleNo).Value)
                        objTr.Qty = clsCommon.myCDecimal(grow.Cells(colQty).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.FAT = clsCommon.myCDecimal(grow.Cells(colFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(grow.Cells(colSNF).Value)
                        objTr.Amount = clsCommon.myCDecimal(grow.Cells(colAmount).Value)
                        objTr.MP_Count = clsCommon.myCDecimal(grow.Cells(colMPCount).Value)
                        objTr.MP_Qty = clsCommon.myCDecimal(grow.Cells(colMPQty).Value)
                        objTr.MP_FAT = clsCommon.myCDecimal(grow.Cells(colMPFAT).Value)
                        objTr.MP_SNF = clsCommon.myCDecimal(grow.Cells(colMPSNF).Value)
                        objTr.MP_Amount = clsCommon.myCDecimal(grow.Cells(colMPAmt).Value)
                        objTr.Diff_Qty = clsCommon.myCDecimal(grow.Cells(colDiffQty).Value)
                        objTr.Diff_FAT = clsCommon.myCDecimal(grow.Cells(colDiffFAT).Value)
                        objTr.Diff_SNF = clsCommon.myCDecimal(grow.Cells(colDiffSNF).Value)
                        objTr.Diff_Amount = clsCommon.myCDecimal(grow.Cells(colDiffAmt).Value)
                        If SettApplyZoneOnDBT Then
                            If Not clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells(colZoneCode).Value)), txtZone.Value) = CompairStringResult.Equal Then
                                Throw New Exception("Zone Should be [" + txtZone.Value + "] at [" + clsCommon.myCstr(obj.arr.Count + 1) + "]")
                            End If
                        End If
                        obj.arr.Add(objTr)
                    End If
                Next
                If obj.arr.Count <= 0 Then
                    Throw New Exception("No data found to save")
                End If
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsMPDCSInsentiveReco.SaveData(obj, isNewEntry, trans)
                    UcAttachment1.SaveData(obj.Document_Code, True, trans)
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsMPDCSInsentiveReco.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        IsinsideLoadData = True
        Reset()
        Dim obj As clsMPDCSInsentiveReco = clsMPDCSInsentiveReco.GetData(strCode, NavTyep, objCommonVar.strCurrUserZones, Nothing)
        If obj IsNot Nothing Then
            DisableInputDataField()
            isNewEntry = False
            txtDocumentNo.Value = obj.Document_Code
            txtdate.Value = obj.Document_Date
            txtFromDate.Value = obj.Reco_Date
            txtToDate.Value = obj.Reco_Date_To
            lblPending.Status = obj.Status
            txtZone.Value = obj.Zone_Code
            lblZone.Text = ClsZoneMaster.GetName(obj.Zone_Code)
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                CycleNo = obj.arr(0).Cycle_No
                For Each objTr As clsMPDCSInsentiveRecoDetail In obj.arr
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colPKID).Value = objTr.PK_Id

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRecoStatus).Value = objTr.Reco_Staus

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBMCCode).Value = objTr.MCC_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBMCName).Value = objTr.MCC_Name

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRouteCode).Value = objTr.Route_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRouteName).Value = objTr.Route_Name

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colZoneCode).Value = objTr.Zone_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colZoneName).Value = objTr.Zone_Name

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCUploaderCode).Value = objTr.VLC_Uploader_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCName).Value = objTr.VLC_Name

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colYear).Value = objTr.Cycle_Year
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMonth).Value = objTr.Cycle_Month
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colCycleNo).Value = objTr.Cycle_No

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFAT).Value = objTr.FAT
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.SNF
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMPCount).Value = objTr.MP_Count
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMPQty).Value = objTr.MP_Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMPFAT).Value = objTr.MP_FAT
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMPSNF).Value = objTr.MP_SNF
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMPAmt).Value = objTr.MP_Amount

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDiffQty).Value = objTr.Diff_Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDiffFAT).Value = objTr.Diff_FAT
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDiffSNF).Value = objTr.Diff_SNF
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDiffAmt).Value = objTr.Diff_Amount

                    'FillFarmerInfo(gvItem.Rows.Count - 1)
                Next
            End If
            UcAttachment1.LoadData(obj.Document_Code)
            txtDocumentNo.MyReadOnly = True
            If obj.Status = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnPost.Enabled = False

            Else
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
            End If
        End If
        IsinsideLoadData = False
    End Sub
    Private Sub txtDocumentNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DCS_MP_INCENTIVE_RECO_HEAD where Document_Code='" + txtDocumentNo.Value + "' "
            Dim check As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocumentNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocumentNo.MyReadOnly = False
            End If

            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocumentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            txtDocumentNo.Value = clsMPDCSInsentiveReco.getFinder("", txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DisableInputDataField()
        txtdate.Enabled = False
        txtFromDate.Enabled = False
        txtZone.Enabled = False
    End Sub
    Sub EnableInputDataField()
        txtdate.Enabled = True
        txtFromDate.Enabled = True
        txtZone.Enabled = True
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                clsMPDCSInsentiveReco.PostData(txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            transportSql.exportdata(gvItem, Me.Text, "")
            'clsCommon.MyExportToExcel("", gvItem, Nothing, Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        If txtFromDate.Enabled Then
            SetToDate()
            GetDocNoAndLoad()
        End If
    End Sub

    Sub GetDocNoAndLoad()
        Dim qry As String = "select max(Document_Code) as Document_Code from TSPL_DCS_MP_INCENTIVE_RECO_HEAD where Reco_Date='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'"
        If SettApplyZoneOnDBT Then
            qry += " and TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Zone_Code='" + txtZone.Value + "'"
        End If
        Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.myLen(strDocNo) > 0 Then
            LoadData(strDocNo, NavigatorType.Current)
        End If
    End Sub

    Dim CycleNo As Integer = 0
    Sub SetToDate()
        If Not IsinsideLoadData Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            If SettMPIncentiveEntryApplyMonthly Then
                txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
                txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
                CycleNo = 1
            Else
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select top 1 TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE is not null") ''TSPL_MCC_MASTER.MCC_Code  in ('" + txtMCC.Value + "') 
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                    Exit Sub
                End If
                PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                        txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                        txtToDate.Value = txtFromDate.Value
                        Exit Sub
                    End If
                    txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
                    If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                    If txtFromDate.Value.Month <> dtNxtPay.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    CycleNo = (txtFromDate.Value.Day / PaymentCycleValue) + 1
                ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Exit Sub
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
                    CycleNo = 1
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Exit Sub
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                    Dim today As Date = txtFromDate.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    txtFromDate.Value = today.AddDays(-dayDiff)
                    txtToDate.Value = txtFromDate.Value.AddDays(6)
                End If
            End If
        End If
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        For ii As Integer = 0 To gvItem.Rows.Count - 1
            FillFarmerInfo(ii)
        Next
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Dim ii As Integer = 0
        Try
            If Not SettDCSMPIncetiveReco Then
                Throw New Exception("This facility is not for you")
            End If
            loadBlankGrid()
            Dim qry As String = "select xx.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,xx.CycleYear,xx.CycleMonth,xx.Qty,xx.UOM_Code,xx.FAT_KG,xx.SNF_KG,xx.AMOUNT,TSPL_VENDOR_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description as Zone_Name from (
select MCC_CODE,VSP_CODE,CycleYear,CycleMonth,sum(Qty) as Qty,max(UOM_Code) as UOM_Code,sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG,sum(AMOUNT) as AMOUNT  from (
select TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE,DATEPART(YEAR, DOC_DATE) as CycleYear,DATEPART(MONTH, DOC_DATE) as CycleMonth,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG,TSPL_MILK_SRN_DETAIL.AMOUNT from 
TSPL_MILK_PURCHASE_INVOICE_DETAIL
left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
where convert(Date, DOC_DATE,103) ='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
)X Group by MCC_CODE,VSP_CODE,CycleYear,CycleMonth
)xx 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xx.MCC_CODE
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xx.VSP_CODE
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii = 0 To dt.Rows.Count - 1


                    If clsCommon.myCDecimal(dt.Rows(ii)("CycleYear")) <> txtFromDate.Value.Year Then
                        Throw New Exception("Year is Mismatched")
                    End If
                    If clsCommon.myCDecimal(dt.Rows(ii)("CycleMonth")) <> txtFromDate.Value.Month Then
                        Throw New Exception("Month is Mismatched")
                    End If
                    'If clsCommon.myCDecimal(dt.Rows(ii)("Cycle No")) <> CycleNo Then
                    '    Throw New Exception("Cycle No is Mismatched")
                    'End If
                    qry = "select PK_Id from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL  where TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code not in ('" + txtDocumentNo.Value + "') and Cycle_Year='" + clsCommon.myCstr(dt.Rows(ii)("CycleYear")) + "' and Cycle_Month='" + clsCommon.myCstr(dt.Rows(ii)("CycleMonth")) + "' and Cycle_No='" + clsCommon.myCstr(CycleNo) + "' and  VLC_Code='" + clsCommon.myCstr(dt.Rows(ii)("VLC_Code")) + "'"
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        Continue For
                    End If


                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBMCCode).Value = clsCommon.myCstr(dt.Rows(ii)("MCC_CODE"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBMCName).Value = clsCommon.myCstr(dt.Rows(ii)("MCC_NAME"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colZoneCode).Value = clsCommon.myCstr(dt.Rows(ii)("Zone_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colZoneName).Value = clsCommon.myCstr(dt.Rows(ii)("Zone_Name"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCCode).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCUploaderCode).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Code_VLC_Uploader"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVLCName).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Name"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colYear).Value = clsCommon.myCDecimal(dt.Rows(ii)("CycleYear"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMonth).Value = clsCommon.myCDecimal(dt.Rows(ii)("CycleMonth"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colCycleNo).Value = CycleNo

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dt.Rows(ii)("Qty"))
                    If clsCommon.myLen(dt.Rows(ii)("UOM_Code")) <= 0 Then
                        Throw New Exception("Please define UOM")
                    End If

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(ii)("UOM_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFAT).Value = clsCommon.myCDecimal(dt.Rows(ii)("FAT_KG"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myCDecimal(dt.Rows(ii)("SNF_KG"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount).Value = clsCommon.myCDecimal(dt.Rows(ii)("AMOUNT"))


                    FillFarmerInfo(gvItem.Rows.Count - 1)

                Next
            End If
        Catch ex As Exception
            loadBlankGrid()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        Try
            If Not lblPending.Status = ERPTransactionStatus.Pending Then
                Throw New Exception("Trasanction should be pending for update")
            End If
            If SettApplyZoneOnDBT Then
                If clsCommon.myLen(txtZone.Value) <= 0 Then
                    Throw New Exception("Please select " + txtZone.MyLinkLable2.Text)
                End If
            End If
            Dim Arr As New List(Of clsMPDCSInsentiveRecoDetail)
            For Each grow As GridViewRowInfo In gvItem.Rows
                If clsCommon.myLen(grow.Cells(colQty).Value) > 0 Then
                    Dim objTr = New clsMPDCSInsentiveRecoDetail()
                    objTr.PK_Id = clsCommon.myCDecimal(grow.Cells(colPKID).Value)
                    'objTr.SNo = obj.arr.Count + 1
                    objTr.Reco_Staus = clsCommon.myCBool(grow.Cells(colRecoStatus).Value)
                    objTr.MCC_Code = clsCommon.myCstr(grow.Cells(colBMCCode).Value)
                    objTr.VLC_Code = clsCommon.myCstr(grow.Cells(colVLCCode).Value)
                    objTr.Cycle_Year = clsCommon.myCstr(grow.Cells(colYear).Value)
                    objTr.Cycle_Month = clsCommon.myCstr(grow.Cells(colMonth).Value)
                    objTr.Cycle_No = clsCommon.myCstr(grow.Cells(colCycleNo).Value)
                    objTr.Qty = clsCommon.myCDecimal(grow.Cells(colQty).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    objTr.FAT = clsCommon.myCDecimal(grow.Cells(colFAT).Value)
                    objTr.SNF = clsCommon.myCDecimal(grow.Cells(colSNF).Value)
                    objTr.Amount = clsCommon.myCDecimal(grow.Cells(colAmount).Value)
                    objTr.MP_Count = clsCommon.myCDecimal(grow.Cells(colMPCount).Value)
                    objTr.MP_Qty = clsCommon.myCDecimal(grow.Cells(colMPQty).Value)
                    objTr.MP_FAT = clsCommon.myCDecimal(grow.Cells(colMPFAT).Value)
                    objTr.MP_SNF = clsCommon.myCDecimal(grow.Cells(colMPSNF).Value)
                    objTr.MP_Amount = clsCommon.myCDecimal(grow.Cells(colMPAmt).Value)
                    objTr.Diff_Qty = clsCommon.myCDecimal(grow.Cells(colDiffQty).Value)
                    objTr.Diff_FAT = clsCommon.myCDecimal(grow.Cells(colDiffFAT).Value)
                    objTr.Diff_SNF = clsCommon.myCDecimal(grow.Cells(colDiffSNF).Value)
                    objTr.Diff_Amount = clsCommon.myCDecimal(grow.Cells(colDiffAmt).Value)
                    If SettApplyZoneOnDBT Then
                        If Not clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells(colZoneCode).Value)), txtZone.Value) = CompairStringResult.Equal Then
                            Throw New Exception("Zone Should be [" + txtZone.Value + "] at [" + clsCommon.myCstr(clsCommon.myCDecimal(grow.Cells(colSlNo).Value)) + "]")
                        End If
                    End If
                    Arr.Add(objTr)
                End If
            Next
            If Arr.Count <= 0 Then
                Throw New Exception("No data found to save")
            End If
            clsMPDCSInsentiveRecoDetail.saveDataZone(txtDocumentNo.Value, objCommonVar.strCurrUserZones, Arr)
            clsCommon.MyMessageBoxShow(Me, "Data updated successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                gvItem.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = Me.Form_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvItem.SaveLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = gvItem.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(Me.Form_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Dim whr As String = "2=3"
        If SettApplyZoneOnDBT Then
            whr = "2=2"
        End If
        txtZone.Value = ClsZoneMaster.getFinder(whr, txtZone.Value, isButtonClicked)
        lblZone.Text = ClsZoneMaster.GetName(txtZone.Value)
        GetDocNoAndLoad()
    End Sub
End Class


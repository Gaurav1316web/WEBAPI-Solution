'' Issue resolved agaist ticket no. SWA/07/08/18-000039

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptDailyGainDay

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim exporter As ExportToExcelML = New ExportToExcelML(Me.gv)

    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptDailyGainDay)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
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
    Sub LoadMCC()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            If clsCommon.myLen(arrLoc) > 0 Then
                qry = "select tspl_location_master.Location_Code as [Code],tspl_location_master.Location_Desc as [Name] from tspl_location_master " & _
                    " inner join TSPL_MCC_MASTER on tspl_location_master.Location_Code= TSPL_MCC_MASTER.MCC_Code where tspl_location_master.Location_Code in (" + arrLoc + ") "
                dt = clsDBFuncationality.GetDataTable(qry)
            End If
        Else
            If clsCommon.myLen(arrLoc) > 0 Then
                qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + arrLoc + ") "
                dt = clsDBFuncationality.GetDataTable(qry)
            End If
        End If


        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbgMCC.DataSource = dt
            cbgMCC.ValueMember = "Code"
            cbgMCC.DisplayMember = "Name"
        End If
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        cboUnit.Text = "KG"
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub RptDailyGainDay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        cboUnit.Text = "KG"
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadMCC()

        Reset()
    End Sub
    Public Sub Load_Report_KDIL()
        Try
            If clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") > clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If
            If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
                Exit Sub
            End If
           
            Dim UOM As String = clsDBFuncationality.getSingleValue("select LEFT(  xx.Fields,len(xx.Fields)-1) as Param   from (select     ( select  distinct '[' + Description +'],'    from TSPL_PARAMETER_MASTER FOR XML PATH ('')) Fields) xx ")
            ' Ticket No : BHA/21/11/18-000686 By Prabhakar - for Devided by Zero error
            ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
            Dim sQuery As String = "select coalesce(Recp .Rec_Date,coalesce(desp.Dispatch_Date,coalesce(trans.Trans_Date,''))) as Date,convert(varchar,coalesce(Recp .Rec_Date,coalesce(desp.Dispatch_Date,coalesce(trans.Trans_Date,''))),103) as Rec_Date ,coalesce(Recp .MCC_CODE,coalesce(desp.mcc_code,coalesce(Trans.mcc_Code,'')))  as MCC_CODE ,TSPL_MCC_MASTER .MCC_NAME ,Recp .Morning,Recp.Mor_Rec_Fat_Per ,Recp .Mor_Rec_Fat_Kg ,Recp.Mor_Rec_SNF_Per ,Recp .Mor_Rec_SNF_Kg ,Recp .Mor_Rec_Qty,Recp.Evening ,Recp .Eve_Rec_Fat_Per ,Recp.Eve_Rec_Fat_Kg ,Recp.Eve_Rec_SNF_Per ,Recp.Eve_Rec_SNF_Kg,Recp.Eve_REC_QTY  ,convert(decimal(18,1),Desp .Dis_Fat_per) as Dis_Fat_per ,Desp.Dis_FAT_KG ,convert(decimal(18,1),Desp.Dis_SNF_per) as Dis_SNF_per,Desp.Dis_SNF_KG ,Desp.Dis_Qty,convert(decimal(18,1),Trans.Trans_FAT_per) as Trans_FAT_per ,Trans.Trans_Fat_kg ,convert(decimal(18,1),Trans.Trans_SNf_per) as Trans_SNf_per ,Trans.Trans_Snf_kg ,Trans.Trans_QTY  , (Recp .Mor_Rec_Qty+Recp.Eve_REC_QTY+Trans.Trans_QTY)-(Desp.Dis_Qty) as Balance  from (((select t_morning .MCC_CODE ,t_morning.MCC_NAME ,t_morning.Rec_Shift as [Morning],t_morning .Rec_Date  ,convert(Decimal(18,1),t_morning.Rec_Fat_Per ) as Mor_Rec_Fat_Per, convert(Decimal(18,2),t_morning.Rec_Fat_Kg)  as Mor_Rec_Fat_Kg,convert(Decimal(18,1),t_morning .Rec_SNF_Per)  as Mor_Rec_SNF_Per,convert(decimal(18,2),t_morning .Rec_SNF_Kg) as Mor_Rec_SNF_Kg ,convert(Decimal(18,2),t_morning .Rec_Qty) as Mor_Rec_Qty,t_Evening .Evening,convert(Decimal(18,1),t_Evening .Rec_Fat_Per)  as Eve_Rec_Fat_Per,Convert(Decimal(18,2),t_Evening .Rec_Fat_Kg)  as Eve_Rec_Fat_Kg,convert(Decimal(18,1),t_Evening .Rec_SNF_Per)  as Eve_Rec_SNF_Per,Convert(decimal(18,2),t_Evening .Rec_SNF_Kg  ) as Eve_Rec_SNF_Kg,convert(Decimal(18,2),t_Evening .Rec_Qty ) as Eve_REC_QTY from ((" & Environment.NewLine & _
            " select  Rec1 .MCC_CODE as MCC_CODE,max(Rec1.MCC_NAME) as MCC_NAME,Rec1.Rec_Date  as Rec_Date,Rec1.Rec_Shift as Rec_Shift,case when sum(Rec1 .Rec_Qty) =0 then 1 else sum((Rec1 .Rec_Fat_Kg)) *100/(nullif(sum(Rec1 .Rec_Qty),0)) end  as Rec_Fat_Per, sum(Rec1 .Rec_Fat_Kg) as Rec_Fat_Kg ,case when sum(Rec1 .Rec_Qty) =0 then 1 else sum((Rec1 .Rec_SNF_Kg )) *100/(nullif(sum(Rec1 .Rec_Qty),0)) end  as Rec_SNF_Per,sum(Rec1 .Rec_SNF_Kg) as Rec_SNF_Kg,sum(Rec_Qty)  as Rec_Qty from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty from TSPL_MILK_SAMPLE_DETAIL " & Environment.NewLine & _
            " left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE " & Environment.NewLine & _
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =TSPL_MILK_SAMPLE_DETAIL.Item_Code "
            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty *  Contained_Qty as CF,Structure_code from TSPL_WEIGHT_CONVERSION UNION All  Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2=2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code and TSPL_MILK_SAMPLE_HEAD.SHIFT='M'    and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty *  Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2=2  and TSPL_MILK_SAMPLE_HEAD.SHIFT='M'    and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "
            End If


            If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            sQuery += " ) Rec1 " & Environment.NewLine & _
            " group by Rec1  .Rec_Shift ,convert(date,Rec1.Rec_Date,103),Rec1.MCC_CODE)t_morning " & Environment.NewLine & _
            "   full Join " & Environment.NewLine & _
            " (( " & Environment.NewLine & _
            " select  Rec2 .MCC_CODE as MCC_CODE,max(Rec2.MCC_NAME) as MCC_NAME,Rec2.Rec_Date  as Rec_Date,Rec2.Rec_Shift as Evening,case when sum(Rec2 .Rec_Qty  )=0 then 1 else sum((Rec2 .Rec_Fat_Kg)) *100/(nullif(sum (Rec2 .Rec_Qty  ),0))  end  as Rec_Fat_Per, sum(Rec2 .Rec_Fat_Kg) as Rec_Fat_Kg ,case when sum(Rec2 .Rec_Qty) =0 then 1 else sum((Rec2 .Rec_SNF_Kg )) *100/(nullif (sum (Rec2 .Rec_Qty),0))  end  as Rec_SNF_Per,sum(Rec2 .Rec_SNF_Kg) as Rec_SNF_Kg,sum(Rec_Qty)  as Rec_Qty from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty from TSPL_MILK_SAMPLE_DETAIL " & Environment.NewLine & _
            " left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE " & Environment.NewLine & _
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =TSPL_MILK_SAMPLE_DETAIL.Item_Code "

            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code and   TSPL_MILK_SAMPLE_HEAD.SHIFT='E'"
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and   TSPL_MILK_SAMPLE_HEAD.SHIFT='E'"
            End If

            sQuery += " and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

            If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If

            sQuery += " ) Rec2 " & Environment.NewLine & _
            " group by Rec2  .Rec_Shift ,convert(date,Rec2.Rec_Date,103),Rec2.MCC_CODE)) " & Environment.NewLine & _
            " t_Evening on t_Evening.Rec_Date=t_morning.Rec_Date and t_Evening .MCC_CODE =t_morning .MCC_CODE )) Recp" & Environment.NewLine & _
            "  Full Join " & Environment.NewLine & _
            " (" & Environment.NewLine & _
            " select DIS1.MCC_Code ,max(DIS1 .MCC_NAME)as MCC_NAME ,DIS1 .Dispatch_Date ,sum(DIS1 .Dis_FAT_KG )*100/sum(DIS1 .Dis_Qty ) as Dis_Fat_per,sum(DIS1.Dis_FAT_KG ) as Dis_FAT_KG,sum(DIS1 .Dis_SNF_KG  )*100/sum(DIS1 .Dis_Qty ) as Dis_SNF_per,sum(DIS1 .Dis_SNF_KG) as Dis_SNF_KG,SUM(DIS1.Dis_Qty ) as  Dis_Qty from (select (TSPL_MCC_Dispatch_Challan.MCC_Code) as MCC_Code ,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,(TSPL_MCC_Dispatch_Challan.FAT_KG) as Dis_FAT_KG ,(TSPL_MCC_Dispatch_Challan.SNF_KG ) as Dis_SNF_KG   ,(TSPL_MCC_Dispatch_Challan.Net_Qty*cf) as Dis_Qty from TSPL_MCC_Dispatch_Challan" & Environment.NewLine & _
            " left outer join " & Environment.NewLine & _
            " TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MCC_Dispatch_Challan.MCC_Code " & Environment.NewLine & _
            " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*    From TSPL_MCC_Dispatch_Challan      Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =        TSPL_MCC_Dispatch_Challan.Chalan_NO And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO    Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,      TSPL_MCC_Dispatch_Challan.MCC_Code    From TSPL_MCC_Dispatch_Challan      Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =        TSPL_MCC_Dispatch_Challan.Chalan_NO And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNf      On t_SNf.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =TSPL_MCC_Dispatch_Challan.Item_Code "

            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code "
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif( Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 "
            End If

            sQuery += "and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

            If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If

            sQuery += " ) DIS1 " & Environment.NewLine & _
            " group by convert(date,DIS1.Dispatch_Date,103) ,DIS1.MCC_Code " & Environment.NewLine & _
            " )  Desp on desp.Dispatch_Date=Recp .Rec_Date and Desp .MCC_Code =Recp.MCC_CODE  ) " & Environment.NewLine & _
            " Full Join " & Environment.NewLine & _
            "  (" & Environment.NewLine & _
            " select Trans1 .Mcc_code  as Mcc_code, max(Trans1 .MCC_NAME)as Mcc_Name ,Trans1 .Trans_Date as Trans_Date,sum(Trans1.Trans_Snf_kg )*100/SUM(Trans1 .Trans_QTY )  as Trans_SNf_per ,sum(Trans_Snf_kg) as Trans_Snf_kg,sum(Trans1.Trans_Fat_kg  )*100/SUM(Trans1 .Trans_QTY )  as Trans_FAT_per ,sum(Trans_Fat_kg) as Trans_Fat_kg ,sum(Trans_QTY ) as Trans_QTY from(select TSPL_Weighment_Detail.UOM as UOM_Code ,Tspl_Gate_Entry_Details.Dispatched_From_Mcc  as Mcc_code, (TSPL_MCC_MASTER.MCC_NAME)as Mcc_Name ,convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as Trans_Date,(TSPL_QUALITY_CHECK.SNF_KG) as Trans_Snf_kg ,(TSPL_QUALITY_CHECK.fat_KG) as Trans_Fat_kg ,(TSPL_Weighment_Detail.Net_Weight*cf ) as Trans_QTY from TSPL_MILK_TRANSFER_IN " & Environment.NewLine & _
            " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_MILK_TRANSFER_IN.Qc_No" & Environment.NewLine & _
            " left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_MILK_TRANSFER_IN.Gate_Entry_no" & Environment.NewLine & _
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =Tspl_Gate_Entry_Details.Dispatched_From_Mcc" & Environment.NewLine & _
            " left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =TSPL_Weighment_Detail.Item_Code "

            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "
            End If


            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                sQuery += "  and Tspl_Gate_Entry_Details.Gate_Entry_Type <> 'J'"
            End If


            If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If

            sQuery += ") Trans1" & Environment.NewLine & _
            " group by convert(date,Trans1 .Trans_Date ,103),Trans1 .Mcc_code  " & Environment.NewLine & _
            " ) Trans on Trans .Trans_Date =Recp .Rec_Date and Trans .Mcc_code =Recp.MCC_CODE  )   left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =Recp .MCC_CODE  or (TSPL_MCC_MASTER.MCC_Code) =desp .MCC_Code or (TSPL_MCC_MASTER.MCC_Code) =trans .MCC_CODE" & Environment.NewLine & _
            " order by date "




            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
              
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGridKDIL()

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
            ViewKDIL()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub
    Sub FormatGridKDIL()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("Rec_Date").IsVisible = True
        gv.Columns("Rec_Date").Width = 100
        gv.Columns("Rec_Date").HeaderText = " Date"
        gv.Columns("Rec_Date").FormatString = "{0:d}"


        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = " MCC "

        gv.Columns("Morning").IsVisible = True
        gv.Columns("Morning").Width = 80
        gv.Columns("Morning").HeaderText = " Morning"


        gv.Columns("Mor_Rec_Fat_Per").IsVisible = True
        gv.Columns("Mor_Rec_Fat_Per").Width = 80
        gv.Columns("Mor_Rec_Fat_Per").HeaderText = "Fat%"

        gv.Columns("Mor_Rec_Fat_Kg").IsVisible = True
        gv.Columns("Mor_Rec_Fat_Kg").Width = 80
        gv.Columns("Mor_Rec_Fat_Kg").HeaderText = "Fat KG"

        gv.Columns("Mor_Rec_SNF_Per").IsVisible = True
        gv.Columns("Mor_Rec_SNF_Per").Width = 80
        gv.Columns("Mor_Rec_SNF_Per").HeaderText = "SNF%"

        gv.Columns("Mor_Rec_SNF_Kg").IsVisible = True
        gv.Columns("Mor_Rec_SNF_Kg").Width = 80
        gv.Columns("Mor_Rec_SNF_Kg").HeaderText = "SNF KG"

        gv.Columns("Mor_Rec_Qty").IsVisible = True
        gv.Columns("Mor_Rec_Qty").Width = 80
        gv.Columns("Mor_Rec_Qty").HeaderText = "Total(M)"

        gv.Columns("Evening").IsVisible = True
        gv.Columns("Evening").Width = 80
        gv.Columns("Evening").HeaderText = "Evening"

        gv.Columns("Eve_Rec_Fat_Per").IsVisible = True
        gv.Columns("Eve_Rec_Fat_Per").Width = 80
        gv.Columns("Eve_Rec_Fat_Per").HeaderText = "Fat%"

        gv.Columns("Eve_Rec_Fat_Kg").IsVisible = True
        gv.Columns("Eve_Rec_Fat_Kg").Width = 80
        gv.Columns("Eve_Rec_Fat_Kg").HeaderText = "Fat KG"


        gv.Columns("Eve_Rec_SNF_Per").IsVisible = True
        gv.Columns("Eve_Rec_SNF_Per").Width = 80
        gv.Columns("Eve_Rec_SNF_Per").HeaderText = "SNF%"

        gv.Columns("Eve_Rec_SNF_Kg").IsVisible = True
        gv.Columns("Eve_Rec_SNF_Kg").Width = 80
        gv.Columns("Eve_Rec_SNF_Kg").HeaderText = "SNF KG"

        gv.Columns("Eve_REC_QTY").IsVisible = True
        gv.Columns("Eve_REC_QTY").Width = 80
        gv.Columns("Eve_REC_QTY").HeaderText = "Total(E)"

        gv.Columns("Dis_Fat_per").IsVisible = True
        gv.Columns("Dis_Fat_per").Width = 80
        gv.Columns("Dis_Fat_per").HeaderText = "FAT%"

        gv.Columns("Dis_FAT_KG").IsVisible = True
        gv.Columns("Dis_FAT_KG").Width = 80
        gv.Columns("Dis_FAT_KG").HeaderText = "FAT Kg"

        gv.Columns("Dis_SNF_per").IsVisible = True
        gv.Columns("Dis_SNF_per").Width = 80
        gv.Columns("Dis_SNF_per").HeaderText = "SNF%"

        gv.Columns("Dis_SNF_KG").IsVisible = True
        gv.Columns("Dis_SNF_KG").Width = 80
        gv.Columns("Dis_SNF_KG").HeaderText = "SNF Kg"

        gv.Columns("Dis_Qty").IsVisible = True
        gv.Columns("Dis_Qty").Width = 80
        gv.Columns("Dis_Qty").HeaderText = "Total(D)"

        gv.Columns("Trans_FAT_per").IsVisible = True
        gv.Columns("Trans_FAT_per").Width = 80
        gv.Columns("Trans_FAT_per").HeaderText = "FAT%"

        gv.Columns("Trans_Fat_kg").IsVisible = True
        gv.Columns("Trans_Fat_kg").Width = 80
        gv.Columns("Trans_Fat_kg").HeaderText = "FAT kg"

        gv.Columns("Trans_SNf_per").IsVisible = True
        gv.Columns("Trans_SNf_per").Width = 80
        gv.Columns("Trans_SNf_per").HeaderText = "SNF%"

        gv.Columns("Trans_Snf_kg").IsVisible = True
        gv.Columns("Trans_Snf_kg").Width = 80
        gv.Columns("Trans_Snf_kg").HeaderText = "SNF Kg"

        gv.Columns("Trans_QTY").IsVisible = True
        gv.Columns("Trans_QTY").Width = 80
        gv.Columns("Trans_QTY").HeaderText = "Total(T)"

        gv.Columns("Balance").IsVisible = True
        gv.Columns("Balance").Width = 80
        gv.Columns("Balance").HeaderText = "Balance"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub ViewKDIL()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Rec_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_NAME").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Sample (M)"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Morning").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_Fat_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_Fat_Kg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_SNF_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_SNF_Kg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_Qty").Name)
            'gv.ViewDefinition = view

            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Sample (E)"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Evening").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_Fat_Per").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_Fat_Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_SNF_Per").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_SNF_Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Eve_REC_QTY").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Dis_Fat_per").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Dis_FAT_KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Dis_SNF_per").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Dis_SNF_KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Dis_Qty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Transfer"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_FAT_per").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_Fat_kg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_SNf_per").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_Snf_kg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_QTY").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Balance").Name)

            gv.ViewDefinition = view
        End If

    End Sub
    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
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

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Load_Report_UDL()
        Else
            Load_Report_KDIL()
        End If

    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptDailyGainDay_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then

            Else
                Load_Report_KDIL()
            End If
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptDailyGainDay & "'"))
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

                    arrHeader.Add(("MCC Name: " + strMCCName + " "))

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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Public Sub Load_Report_UDL()
        If clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") > clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        'If PlantSelect.IsChecked AndAlso cbgPlant.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Plant or select all.")    
        '    Exit Sub
        'End If
        'If TankerSelect.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.")
        '    Exit Sub
        'End If
        Dim UOM As String = clsDBFuncationality.getSingleValue("select LEFT(  xx.Fields,len(xx.Fields)-1) as Param   from (select     ( select  distinct '[' + Description +'],'    from TSPL_PARAMETER_MASTER FOR XML PATH ('')) Fields) xx ")


        '' Start New Qry

        Dim sQuery As String = ""
        sQuery = "  select distinct *,convert(decimal(18,3),(isnull(Trans_QTY,0)+isnull(closing_Qty,0))-(isnull(Opening_Stock,0)+isnull(Mor_Rec_Qty,0)+isnull(Eve_REC_QTY,0))) as varation_Qty"
        sQuery += " , convert(decimal(18,3),(isnull(Trans_Fat_kg,0)+isnull(Closing_FAT_kG,0))-(isnull(opening_FAT_kG,0)+isnull(Mor_Rec_Fat_Kg,0)+isnull(Eve_Rec_Fat_Kg,0))) as Varation_FAT_KG, convert(decimal(18,3),(isnull(Trans_Snf_kg,0)+isnull(closing_SNF_kG,0))-(isnull(opening_SNF_kG,0)+isnull(Mor_Rec_SNF_Kg,0)+isnull(Eve_Rec_SNF_Kg,0))) as Varation_SNF_KG from (select Recp.opening_FAT_kG ,Recp.opening_SNF_kG ,Recp.Closing_FAT_kG ,Recp.closing_SNF_kG , Recp.Opening_Stock ,Recp.closing_Qty , coalesce(Recp .Rec_Date,coalesce(desp.Dispatch_Date,coalesce(trans.Trans_Date,''))) as Date,convert(varchar,coalesce(Recp .Rec_Date,coalesce(desp.Dispatch_Date,coalesce(trans.Trans_Date,''))),103) as Rec_Date ,coalesce(Recp .MCC_CODE,coalesce(desp.mcc_code,coalesce(Trans.mcc_Code,'')))  as MCC_CODE ,TSPL_MCC_MASTER .MCC_NAME ,Recp .Morning,Recp.Mor_Rec_Fat_Per ,Recp .Mor_Rec_Fat_Kg ,Recp.Mor_Rec_SNF_Per ,Recp .Mor_Rec_SNF_Kg ,Recp .Mor_Rec_Qty,Recp.Evening ,Recp .Eve_Rec_Fat_Per ,Recp.Eve_Rec_Fat_Kg ,Recp.Eve_Rec_SNF_Per ,Recp.Eve_Rec_SNF_Kg,Recp.Eve_REC_QTY  ,convert(decimal(18,1),Desp .Dis_Fat_per) as Dis_Fat_per ,Desp.Dis_FAT_KG ,convert(decimal(18,1),Desp.Dis_SNF_per) as Dis_SNF_per,Desp.Dis_SNF_KG ,Desp.Dis_Qty,0 as SRN_FAT_per ,0 as SRN_Fat_kg  ,0 as SRN_SNf_per ,0 as SRN_Snf_kg  ,0 as SRN_QTY,'' as SRN_Tanker_No ,convert(decimal(18,2),Trans.Trans_FAT_per) as Trans_FAT_per ,Trans.Trans_Fat_kg ,convert(decimal(18,2),Trans.Trans_SNf_per) as Trans_SNf_per ,Trans.Trans_Snf_kg ,Trans.Trans_QTY ,trans.Trans_Tanker_No , (Recp .Mor_Rec_Qty+Recp.Eve_REC_QTY+Trans.Trans_QTY)-(Desp.Dis_Qty) as Balance "
        sQuery += " from (((select row_number() over(partition by t_morning.mcc_code order by t_morning .Rec_Date) as RecpSrNo,t_morning.Opening_Stock,t_morning.opening_FAT_kG as opening_FAT_kG,t_morning.opening_SNF_kG as opening_SNF_kG  ,t_Evening.closing_Qty,t_Evening.closing_Manual_FAT  as Closing_FAT_kG,t_Evening.closing_Manual_SNF  as Closing_SNF_kG  ,  t_morning .MCC_CODE ,t_morning.MCC_NAME ,t_morning.Rec_Shift as [Morning],t_morning .Rec_Date  ,convert(Decimal(18,2),t_morning.Rec_Fat_Per ) as Mor_Rec_Fat_Per, convert(Decimal(18,2),t_morning.Rec_Fat_Kg)  as Mor_Rec_Fat_Kg,convert(Decimal(18,2),t_morning .Rec_SNF_Per)  as Mor_Rec_SNF_Per,convert(decimal(18,2),t_morning .Rec_SNF_Kg) as Mor_Rec_SNF_Kg ,convert(Decimal(18,2),t_morning .Rec_Qty) as Mor_Rec_Qty,t_Evening .Evening,convert(Decimal(18,2),t_Evening .Rec_Fat_Per)  as Eve_Rec_Fat_Per,Convert(Decimal(18,2),t_Evening .Rec_Fat_Kg)  as Eve_Rec_Fat_Kg,convert(Decimal(18,2),t_Evening .Rec_SNF_Per)  as Eve_Rec_SNF_Per,Convert(decimal(18,2),t_Evening .Rec_SNF_Kg  ) as Eve_Rec_SNF_Kg,convert(Decimal(18,2),t_Evening .Rec_Qty ) as Eve_REC_QTY,t_Evening.Tanker_No from (( select max(Opening.Manual_FAT ) as opening_FAT_kG,max(Opening.Manual_SNF ) as opening_SNF_kG, max(Opening.shift) as Opening_shift,max(Opening.manual_stock) as Opening_Stock,max(Opening .MCC_CODE) as opening_MCC,max(Opening .MCC_SHIFT_DATE) as Opening_Date , Rec1 .MCC_CODE as MCC_CODE,max(Rec1.MCC_NAME) as MCC_NAME,Rec1.Rec_Date  as Rec_Date,Rec1.Rec_Shift as Rec_Shift,case when sum(Rec1 .Rec_Qty) =0 then 1 else sum((Rec1 .Rec_Fat_Kg)) *100/(sum(Rec1 .Rec_Qty)) end  as Rec_Fat_Per, sum(Rec1 .Rec_Fat_Kg) as Rec_Fat_Kg ,case when sum(Rec1 .Rec_Qty) =0 then 1 else sum((Rec1 .Rec_SNF_Kg )) *100/(sum(Rec1 .Rec_Qty)) end  as Rec_SNF_Per,sum(Rec1 .Rec_SNF_Kg) as Rec_SNF_Kg,sum(Rec_Qty)  as Rec_Qty,'' as Tanker_No from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty from TSPL_MILK_SAMPLE_DETAIL  left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE "
        sQuery += "  left outer join ( select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2=2  and TSPL_MILK_SAMPLE_HEAD.SHIFT='M'    and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "   and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        sQuery += " ) Rec1 "
        sQuery += " left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE =Rec1.MCC_CODE and TSPL_MILK_Shift_End_HEAD.MCC_DATE =convert(date,Rec1.Rec_Date ,103) and TSPL_MILK_Shift_End_HEAD.SHIFT =Rec1 .Rec_Shift  left join (select mcc_code,mcc_shift_date,sum(manual_stock) as manual_stock,max(shift) as shift ,sum(Manual_FAT) as Manual_FAT ,sum(Manual_SNF) as Manual_SNF from TSPL_OPEN_MCC_SHIFT where 2=2 and shift='M'  group by mcc_code,mcc_shift_date)Opening on Opening.mcc_code=Rec1 .MCC_CODE and convert(date,Opening.mcc_shift_date,103)=convert(date,Rec1.Rec_Date ,103) group by Rec1  .Rec_Shift ,convert(date,Rec1.Rec_Date,103),Rec1.MCC_CODE)t_morning   full Join ( select max(closing.Manual_FAT) as closing_Manual_FAT,max(closing.Manual_SNF) as closing_Manual_SNF, max(closing.shift) as Closing_Shift,max(closing.manual_stock) as closing_Qty,max(closing .MCC_CODE) as Closing_mcc,max(closing .DOC_DATE )as Closing_date , Rec2 .MCC_CODE as MCC_CODE,max(Rec2.MCC_NAME) as MCC_NAME,Rec2.Rec_Date  as Rec_Date,Rec2.Rec_Shift as Evening,case when sum(Rec2 .Rec_Qty  )=0 then 0 else sum((Rec2 .Rec_Fat_Kg)) *100/(sum (Rec2 .Rec_Qty  ))  end  as Rec_Fat_Per, sum(Rec2 .Rec_Fat_Kg) as Rec_Fat_Kg ,case when sum(Rec2 .Rec_Qty) =0 then 0 else sum((Rec2 .Rec_SNF_Kg )) *100/(sum (Rec2 .Rec_Qty))  end  as Rec_SNF_Per,sum(Rec2 .Rec_SNF_Kg) as Rec_SNF_Kg,sum(Rec_Qty)  as Rec_Qty,'' as Tanker_No from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty "
        '' add this column for closing data 
        sQuery += "  ,0 as WithoutSamplingFat,0 as WithoutSamplingSNF,0 as WithoutSamplingStock "
        '' end
        sQuery += "  from TSPL_MILK_SAMPLE_DETAIL  left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE  left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code "
        sQuery += " and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and   TSPL_MILK_SAMPLE_HEAD.SHIFT='E' and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "   and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        '' Changes for Without Sampling direct closing value show
        sQuery += " Union all "
        sQuery += " select '' as UOM_Code,TSPL_MILK_Shift_End_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_Shift_End_HEAD.SHIFT as Rec_Shift, 0 as Rec_Fat_Kg ,0 as Rec_SNF_Kg,0  as Rec_Qty ,TSPL_MILK_Shift_End_HEAD.Manual_FAT as WithoutSamplingFat,TSPL_MILK_Shift_End_HEAD.Manual_SNF as WithoutSamplingSNF,TSPL_MILK_Shift_End_HEAD.Manual_Stock as WithoutSamplingStock "
        sQuery += " from TSPL_MILK_Shift_End_HEAD  "
        sQuery += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_Shift_End_HEAD.MCC_CODE  "
        sQuery += " left outer join TSPL_MILK_Shift_End_DETAIL on TSPL_MILK_Shift_End_DETAIL.DOC_CODE=TSPL_MILK_Shift_End_HEAD.DOC_CODE"
        sQuery += " where 2 = 2 and   TSPL_MILK_Shift_End_HEAD.SHIFT='E' and convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "   and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        '' End Function
        sQuery += " ) Rec2 "
        sQuery += "  left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE =Rec2.MCC_CODE and TSPL_MILK_Shift_End_HEAD.MCC_DATE =convert(date,Rec2.Rec_Date ,103) and TSPL_MILK_Shift_End_HEAD.SHIFT =Rec2 .Rec_Shift  left join (select max(shift) as shift, mcc_code,convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) as DOC_DATE ,sum(manual_stock) as manual_stock,sum(Manual_FAT) as Manual_FAT ,sum(Manual_SNF) as Manual_SNF from TSPL_MILK_Shift_End_HEAD where 2=2 and shift='E'    group by mcc_code,DOC_DATE )closing on closing.mcc_code=Rec2 .MCC_CODE and convert(date,closing.DOC_DATE,103)=convert(date,Rec2.Rec_Date ,103) group by Rec2  .Rec_Shift ,convert(date,Rec2.Rec_Date,103),Rec2.MCC_CODE) "

        '' changes closing data
        'sQuery += "Union All"
        'sQuery += "( select max(closing.Manual_FAT) as closing_Manual_FAT,max(closing.Manual_SNF) as closing_Manual_SNF, max(closing.shift) as Closing_Shift,max(closing.manual_stock) as closing_Qty,max(closing .MCC_CODE) as Closing_mcc,max(closing .DOC_DATE )as Closing_date , Rec2 .MCC_CODE as MCC_CODE,max(Rec2.MCC_NAME) as MCC_NAME,Rec2.Rec_Date  as Rec_Date,'' as Evening ,0  as Rec_Fat_Per, 0 as Rec_Fat_Kg ,0  as Rec_SNF_Per,0 as Rec_SNF_Kg,0  as Rec_Qty,'' as Tanker_No from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty from TSPL_MILK_SAMPLE_DETAIL  left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE  left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='KG' where 2 = 2 and   TSPL_MILK_SAMPLE_HEAD.SHIFT='M' and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'30/11/2017',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103) <=convert(date,'01/12/2017 9:54:12 AM' ,103)    and TSPL_MCC_MASTER.MCC_Code  IN ('MCCUK/000001')  ) Rec2   left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE =Rec2.MCC_CODE and TSPL_MILK_Shift_End_HEAD.MCC_DATE =convert(date,Rec2.Rec_Date ,103) and TSPL_MILK_Shift_End_HEAD.SHIFT =Rec2 .Rec_Shift  left outer join "
        'sQuery += " (select max(shift) as shift, mcc_code,convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) as DOC_DATE ,sum(manual_stock) as manual_stock,sum(Manual_FAT) as Manual_FAT ,sum(Manual_SNF) as Manual_SNF from TSPL_MILK_Shift_End_HEAD where 2=2 and shift='M'    group by mcc_code,DOC_DATE )"
        'sQuery += " closing on closing.mcc_code=Rec2 .MCC_CODE and convert(date,closing.DOC_DATE,103)=convert(date,Rec2.Rec_Date ,103) group by Rec2  .Rec_Shift ,convert(date,Rec2.Rec_Date,103),Rec2.MCC_CODE)) "

        sQuery += " t_Evening on t_Evening.Rec_Date=t_morning.Rec_Date and t_Evening .MCC_CODE =t_morning .MCC_CODE )) Recp "
        sQuery += " full Join ( select DIS1.MCC_Code ,max(DIS1 .MCC_NAME)as MCC_NAME ,DIS1 .Dispatch_Date ,sum(DIS1 .Dis_FAT_KG )*100/sum(DIS1 .Dis_Qty ) as Dis_Fat_per,sum(DIS1.Dis_FAT_KG ) as Dis_FAT_KG,sum(DIS1 .Dis_SNF_KG  )*100/sum(DIS1 .Dis_Qty ) as Dis_SNF_per,sum(DIS1 .Dis_SNF_KG) as Dis_SNF_KG,SUM(DIS1.Dis_Qty ) as  Dis_Qty, '' as Tanker_No from (select (TSPL_MCC_Dispatch_Challan.MCC_Code) as MCC_Code ,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,(TSPL_MCC_Dispatch_Challan.FAT_KG) as Dis_FAT_KG ,(TSPL_MCC_Dispatch_Challan.SNF_KG ) as Dis_SNF_KG   ,(TSPL_MCC_Dispatch_Challan.Net_Qty*cf) as Dis_Qty from TSPL_MCC_Dispatch_Challan left outer join  TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MCC_Dispatch_Challan.MCC_Code  Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*    From TSPL_MCC_Dispatch_Challan      Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =        TSPL_MCC_Dispatch_Challan.Chalan_NO And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO    Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,      TSPL_MCC_Dispatch_Challan.MCC_Code    From TSPL_MCC_Dispatch_Challan      Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =        TSPL_MCC_Dispatch_Challan.Chalan_NO And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNf      On t_SNf.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code "
        sQuery += " and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date ,103) <=convert(date,'" + txtToDate.Value + "',103) "

        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "   and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        '' check only
        sQuery += " ) DIS1 group by convert(date,DIS1.Dispatch_Date,103) ,DIS1.MCC_Code  )  Desp on desp.Dispatch_Date=Recp .Rec_Date and Desp .MCC_Code =Recp.MCC_CODE  )"
        sQuery += " full join  ( select Trans1 .Mcc_code  as Mcc_code, (Trans1 .MCC_NAME)as Mcc_Name ,Trans1 .Trans_Date as Trans_Date,Trans_SNf_per ,(Trans_Snf_kg) as Trans_Snf_kg,Trans_FAT_per ,(Trans_Fat_kg) as Trans_Fat_kg ,(Trans_QTY ) as Trans_QTY, Tanker_No as Trans_Tanker_No,TransSrNo  from(select TSPL_Weighment_Detail.UOM as UOM_Code,TSPL_Weighment_Detail.Tanker_No as Tanker_No ,TSPL_MCC_Dispatch_Challan.mcc_code  as Mcc_code, (TSPL_MCC_MASTER.MCC_NAME)as Mcc_Name ,convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as Trans_Date,(TSPL_Weighment_Detail.fat_per) as Trans_FAT_per,(TSPL_Weighment_Detail.snf_Per)   as Trans_SNf_per,(TSPL_Weighment_Detail.SNF_KG) as Trans_Snf_kg ,(TSPL_Weighment_Detail.FAT_KG) as Trans_Fat_kg ,(TSPL_Weighment_Detail.Net_Weight*cf ) as Trans_QTY,row_number() over(partition by TSPL_MCC_Dispatch_Challan.mcc_code order by convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)) as TransSrNo from TSPL_MILK_TRANSFER_IN  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_MILK_TRANSFER_IN.Qc_No left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_MILK_TRANSFER_IN.Gate_Entry_no left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =Tspl_Gate_Entry_Details.Dispatched_From_Mcc left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No  left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM "
        sQuery += "  and lower(zzz.TOUOM)='" + cboUnit.Text + "' left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.chalan_no=tspl_milk_transfer_in.dispatch_challan_no "
        sQuery += " where 2 = 2 and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + txtToDate.Value + "',103)  and Tspl_Gate_Entry_Details.Doc_Type='MccProc'"

        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "  and TSPL_MCC_Dispatch_Challan.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If

        sQuery += " ) Trans1    ) Trans on Trans.TransSrNo =Recp.RecpSrNo and Trans .Mcc_code =Recp.MCC_CODE  )   left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =Recp .MCC_CODE  or (TSPL_MCC_MASTER.MCC_Code) =desp .MCC_Code or (TSPL_MCC_MASTER.MCC_Code) =trans .MCC_CODE "

        sQuery += " Union All "
        sQuery += " select * from (  select 0 as opening_FAT_KG,0 as Opening_SNF_KG,0 as Closing_FAT_KG,0 as closing_SNF_KG ,0 as Opening_Stock ,0 as closing_Qty ,'' as Date ,'' as Rec_Date ,  coalesce(milksrn .MCC_CODE,'')  as MCC_CODE ,case when isnull(milksrn.Mcc_code,'')='' then '' else TSPL_LOCATION_MASTER.Location_Desc end as MCC_NAME ,'' as Morning,0 as Mor_Rec_Fat_Per , 0 as Mor_Rec_Fat_Kg ,0 as Mor_Rec_SNF_Per ,0 as Mor_Rec_SNF_Kg ,0 as Mor_Rec_Qty,'' as Evening ,0 as Eve_Rec_Fat_Per ,0 as Eve_Rec_Fat_Kg , 0 as Eve_Rec_SNF_Per ,0 as Eve_Rec_SNF_Kg,0 as Eve_REC_QTY  ,0 as Dis_Fat_per ,0 as Dis_FAT_KG ,0 as Dis_SNF_per,0 as Dis_SNF_KG , 0 as Dis_Qty,milksrn.SRN_FAT_per  as SRN_FAT_per ,milksrn.SRN_Fat_kg  as SRN_Fat_kg  ,milksrn.SRN_SNf_per  as SRN_SNf_per , milksrn.SRN_Snf_kg  as SRN_Snf_kg  ,milksrn.SRN_QTY  as SRN_QTY , milksrn.SRN_Tanker_No as SRN_Tanker_No  ,Trans.SRNTrans_FAT_per   as Trans_FAT_per , Trans.SRNTrans_Fat_kg   AS SRNTrans_Fat_kg  ,Trans .SRNTrans_SNf_per   as Trans_SNf_per ,Trans .SRNTrans_Snf_kg   AS SRNTrans_Snf_kg  ,Trans.SRNTrans_QTY  AS SRNTrans_QTY   ,Trans.SRNTrans_Tanker_No as  SRNTrans_Tanker_No ,(milksrn.SRN_QTY +Trans.SRNTrans_QTY) as Balance  from (( select SRN .Mcc_code  as Mcc_code, max(SRN .MCC_NAME)as Mcc_Name ,SRN .SRN_Date as SRN_Date,sum(SRN.SRN_Snf_kg )*100/SUM(SRN .SRN_QTY )  as SRN_SNf_per , sum(SRN_Snf_kg) as SRN_Snf_kg,sum(SRN.SRN_Fat_kg  )*100/SUM(SRN .SRN_QTY )  as SRN_FAT_per ,sum(SRN_Fat_kg) as SRN_Fat_kg ,sum(SRN_QTY ) as SRN_QTY ,max(SRN.Tanker_No) as SRN_Tanker_No from (select TSPL_Bulk_MILK_SRN.SRN_NO  ,TSPL_Weighment_Detail.UOM as UOM_Code,TSPL_Weighment_Detail.Tanker_No as Tanker_No ,Tspl_Gate_Entry_Details.location_Code  as Mcc_code, (TSPL_LOCATION_MASTER.Location_Desc  )as Mcc_Name ,convert(date,TSPL_Bulk_MILK_SRN.SRN_Date ,103) as SRN_Date,(TSPL_Bulk_MILK_SRN.SNF_KG) as SRN_Snf_kg ,(TSPL_Bulk_MILK_SRN.fat_KG) as SRN_Fat_kg ,(TSPL_Bulk_MILK_SRN.NET_Weight ) as SRN_QTY from TSPL_Bulk_MILK_SRN left join TSPL_Bulk_MILK_SRN_Chember_Details on TSPL_Bulk_MILK_SRN_Chember_Details.SRN_NO =TSPL_Bulk_MILK_SRN.SRN_NO  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_Bulk_MILK_SRN.Qc_No  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_Bulk_MILK_SRN.Gate_Entry_no  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =Tspl_Gate_Entry_Details.location_Code  left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No   left outer join (select * from View_GetConversion)   zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM "
        sQuery += " and lower(zzz.TOUOM)='" + cboUnit.Text + "' "
        sQuery += " left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code=Tspl_Gate_Entry_Details.location_Code "
        sQuery += " where 2 = 2  and convert(date,TSPL_Bulk_MILK_SRN.SRN_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Bulk_MILK_SRN.SRN_Date,103) <=convert(date,'" + txtToDate.Value + "',103)   and Tspl_Gate_Entry_Details.Doc_Type='MccProc' "
        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_MCC_Dispatch_Challan.MCC_Code  IN  (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        sQuery += "  ) as SRN group by convert(date,SRN .SRN_Date  ,103),SRN .Mcc_code ) MilkSRN Full Join  ( select SRNTrans1 .Mcc_code  as SRNMcc_code, (SRNTrans1 .MCC_NAME)as SRNMcc_Name ,SRNTrans1 .Trans_Date as SRNTrans_Date, SRNTrans_SNf_per ,(Trans_Snf_kg) as SRNTrans_Snf_kg, SRNTrans_FAT_per ,(Trans_Fat_kg) as SRNTrans_Fat_kg , (Trans_QTY ) as SRNTrans_QTY,SRNTrans1.Tanker_No as SRNTrans_Tanker_No from(select distinct TSPL_Weighment_Detail.UOM as UOM_Code,TSPL_Weighment_Detail.Tanker_No as Tanker_No ,Tspl_Gate_Entry_Details.location_Code   as Mcc_code , (TSPL_LOCATION_MASTER.Location_Desc  )as Mcc_Name ,convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as Trans_Date ,(TSPL_Weighment_Detail.fat_per) as SRNTrans_FAT_per,(TSPL_Weighment_Detail.snf_Per)   as SRNTrans_SNf_per,(TSPL_Weighment_Detail.SNF_KG) as Trans_Snf_kg ,(TSPL_Weighment_Detail.FAT_Kg) as Trans_Fat_kg ,(TSPL_Weighment_Detail.Net_Weight*cf ) as Trans_QTY from TSPL_MILK_TRANSFER_IN   left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_MILK_TRANSFER_IN.Qc_No  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_MILK_TRANSFER_IN.Gate_Entry_no  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =Tspl_Gate_Entry_Details.location_Code   left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No   left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM "
        sQuery += " and lower(zzz.TOUOM)='" + cboUnit.Text + "' "
        sQuery += " left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code=Tspl_Gate_Entry_Details.location_Code "
        sQuery += " where 2 = 2  and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + txtFromDate.Value + "',103) "
        If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_MCC_Dispatch_Challan.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        sQuery += "  ) SRNTrans1     ) Trans on Trans .SRNTrans_Date  =MilkSRN  .SRN_Date  and Trans .SRNMcc_code  =MilkSRN .MCC_CODE  )   left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =MilkSRN  .MCC_CODE  or (TSPL_LOCATION_MASTER.Location_Code) =trans .SRNMcc_code "
        sQuery += " ) as xx   ) as final where isnull(MCC_CODE,'')<>'' and isnull(Morning,'')<>'' order by date "

        '' End of New Qry


        'sQuery = " select *,(isnull(Trans_QTY,0)+isnull(closing_Qty,0))-(isnull(Opening_Stock,0)+isnull(Mor_Rec_Qty,0)+isnull(Eve_REC_QTY,0)) as varation_Qty,"
        'sQuery += " (isnull(Trans_Fat_kg,0)+isnull(Closing_FAT_kG,0))-(isnull(opening_FAT_kG,0)+isnull(Mor_Rec_Fat_Kg,0)+isnull(Eve_Rec_Fat_Kg,0)) as Varation_FAT_KG,"
        'sQuery += " (isnull(Trans_Snf_kg,0)+isnull(closing_SNF_kG,0))-(isnull(opening_SNF_kG,0)+isnull(Mor_Rec_SNF_Kg,0)+isnull(Eve_Rec_SNF_Kg,0)) as Varation_SNF_KG"
        'sQuery += " from (select Recp.opening_FAT_kG ,Recp.opening_SNF_kG ,Recp.Closing_FAT_kG ,Recp.closing_SNF_kG , Recp.Opening_Stock ,Recp.closing_Qty , coalesce(Recp .Rec_Date,coalesce(desp.Dispatch_Date,coalesce(trans.Trans_Date,''))) as Date,convert(varchar,coalesce(Recp .Rec_Date,coalesce(desp.Dispatch_Date,coalesce(trans.Trans_Date,''))),103) as Rec_Date ,coalesce(Recp .MCC_CODE,coalesce(desp.mcc_code,coalesce(Trans.mcc_Code,'')))  as MCC_CODE ,TSPL_MCC_MASTER .MCC_NAME ,Recp .Morning,Recp.Mor_Rec_Fat_Per ,Recp .Mor_Rec_Fat_Kg ,Recp.Mor_Rec_SNF_Per ,Recp .Mor_Rec_SNF_Kg ,Recp .Mor_Rec_Qty,Recp.Evening ,Recp .Eve_Rec_Fat_Per ,Recp.Eve_Rec_Fat_Kg ,Recp.Eve_Rec_SNF_Per ,Recp.Eve_Rec_SNF_Kg,Recp.Eve_REC_QTY  ,convert(decimal(18,1),Desp .Dis_Fat_per) as Dis_Fat_per ,Desp.Dis_FAT_KG ,convert(decimal(18,1),Desp.Dis_SNF_per) as Dis_SNF_per,Desp.Dis_SNF_KG ,Desp.Dis_Qty,0 as SRN_FAT_per ,0 as SRN_Fat_kg  ,0 as SRN_SNf_per ,0 as SRN_Snf_kg  ,0 as SRN_QTY,'' as SRN_Tanker_No ,convert(decimal(18,1),Trans.Trans_FAT_per) as Trans_FAT_per ,Trans.Trans_Fat_kg ,convert(decimal(18,1),Trans.Trans_SNf_per) as Trans_SNf_per ,Trans.Trans_Snf_kg ,Trans.Trans_QTY ,trans.Trans_Tanker_No , (Recp .Mor_Rec_Qty+Recp.Eve_REC_QTY+Trans.Trans_QTY)-(Desp.Dis_Qty) as Balance  from (((select t_morning.Opening_Stock,t_morning.opening_FAT_kG as opening_FAT_kG,t_morning.opening_SNF_kG as opening_SNF_kG  ,t_Evening.closing_Qty,t_Evening.closing_Manual_FAT  as Closing_FAT_kG,t_Evening.closing_Manual_SNF  as Closing_SNF_kG  ,  t_morning .MCC_CODE ,t_morning.MCC_NAME ,t_morning.Rec_Shift as [Morning],t_morning .Rec_Date  ,convert(Decimal(18,1),t_morning.Rec_Fat_Per ) as Mor_Rec_Fat_Per, convert(Decimal(18,2),t_morning.Rec_Fat_Kg)  as Mor_Rec_Fat_Kg,convert(Decimal(18,1),t_morning .Rec_SNF_Per)  as Mor_Rec_SNF_Per,convert(decimal(18,2),t_morning .Rec_SNF_Kg) as Mor_Rec_SNF_Kg ,convert(Decimal(18,2),t_morning .Rec_Qty) as Mor_Rec_Qty,t_Evening .Evening,convert(Decimal(18,1),t_Evening .Rec_Fat_Per)  as Eve_Rec_Fat_Per,Convert(Decimal(18,2),t_Evening .Rec_Fat_Kg)  as Eve_Rec_Fat_Kg,convert(Decimal(18,1),t_Evening .Rec_SNF_Per)  as Eve_Rec_SNF_Per,Convert(decimal(18,2),t_Evening .Rec_SNF_Kg  ) as Eve_Rec_SNF_Kg,convert(Decimal(18,2),t_Evening .Rec_Qty ) as Eve_REC_QTY,t_Evening.Tanker_No from (("
        'sQuery += " select max(Opening.Manual_FAT ) as opening_FAT_kG,max(Opening.Manual_SNF ) as opening_SNF_kG, max(Opening.shift) as Opening_shift,max(Opening.manual_stock) as Opening_Stock,max(Opening .MCC_CODE) as opening_MCC,max(Opening .MCC_SHIFT_DATE) as Opening_Date , Rec1 .MCC_CODE as MCC_CODE,max(Rec1.MCC_NAME) as MCC_NAME,Rec1.Rec_Date  as Rec_Date,Rec1.Rec_Shift as Rec_Shift,case when sum(Rec1 .Rec_Qty) =0 then 1 else sum((Rec1 .Rec_Fat_Kg)) *100/(sum(Rec1 .Rec_Qty)) end  as Rec_Fat_Per, sum(Rec1 .Rec_Fat_Kg) as Rec_Fat_Kg ,case when sum(Rec1 .Rec_Qty) =0 then 1 else sum((Rec1 .Rec_SNF_Kg )) *100/(sum(Rec1 .Rec_Qty)) end  as Rec_SNF_Per,sum(Rec1 .Rec_SNF_Kg) as Rec_SNF_Kg,sum(Rec_Qty)  as Rec_Qty,'' as Tanker_No from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty from TSPL_MILK_SAMPLE_DETAIL "
        'sQuery += " left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE "
        'sQuery += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE "
        'sQuery += " left outer join ( select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2=2  and TSPL_MILK_SAMPLE_HEAD.SHIFT='M'    and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "

        'If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
        '    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'sQuery += " ) Rec1"
        ''========================
        'sQuery += " left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE =Rec1.MCC_CODE and TSPL_MILK_Shift_End_HEAD.MCC_DATE =convert(date,Rec1.Rec_Date ,103) and TSPL_MILK_Shift_End_HEAD.SHIFT =Rec1 .Rec_Shift "
        'sQuery += " left join (select mcc_code,mcc_shift_date,sum(manual_stock) as manual_stock,max(shift) as shift ,sum(Manual_FAT) as Manual_FAT ,sum(Manual_SNF) as Manual_SNF from TSPL_OPEN_MCC_SHIFT where 2=2 and shift='M'  group by mcc_code,mcc_shift_date)Opening on Opening.mcc_code=Rec1 .MCC_CODE and convert(date,Opening.mcc_shift_date,103)=convert(date,Rec1.Rec_Date ,103)"
        ''=========================
        'sQuery += " group by Rec1  .Rec_Shift ,convert(date,Rec1.Rec_Date,103),Rec1.MCC_CODE)t_morning"

        'sQuery += "   full Join"

        'sQuery += " ("

        'sQuery += " select max(closing.Manual_FAT) as closing_Manual_FAT,max(closing.Manual_SNF) as closing_Manual_SNF, max(closing.shift) as Closing_Shift,max(closing.manual_stock) as closing_Qty,max(closing .MCC_CODE) as Closing_mcc,max(closing .DOC_DATE )as Closing_date , Rec2 .MCC_CODE as MCC_CODE,max(Rec2.MCC_NAME) as MCC_NAME,Rec2.Rec_Date  as Rec_Date,Rec2.Rec_Shift as Evening,case when sum(Rec2 .Rec_Qty  )=0 then 1 else sum((Rec2 .Rec_Fat_Kg)) *100/(sum (Rec2 .Rec_Qty  ))  end  as Rec_Fat_Per, sum(Rec2 .Rec_Fat_Kg) as Rec_Fat_Kg ,case when sum(Rec2 .Rec_Qty) =0 then 1 else sum((Rec2 .Rec_SNF_Kg )) *100/(sum (Rec2 .Rec_Qty))  end  as Rec_SNF_Per,sum(Rec2 .Rec_SNF_Kg) as Rec_SNF_Kg,sum(Rec_Qty)  as Rec_Qty,'' as Tanker_No from (select UOM_Code,TSPL_MILK_SAMPLE_HEAD.MCC_CODE as MCC_CODE,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(Date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  as Rec_Date,TSPL_MILK_SAMPLE_HEAD.SHIFT as Rec_Shift, (TSPL_MILK_SAMPLE_DETAIL.FAT_KG) as Rec_Fat_Kg ,(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) as Rec_SNF_Kg,(TSPL_MILK_SAMPLE_DETAIL.Qty*cf)  as Rec_Qty from TSPL_MILK_SAMPLE_DETAIL "
        'sQuery += " left outer join TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_DETAIL.DOC_CODE "
        'sQuery += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_SAMPLE_HEAD.MCC_CODE "
        'sQuery += " left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and   TSPL_MILK_SAMPLE_HEAD.SHIFT='E'"
        'sQuery += " and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SAMPLE_HEAD.DOC_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        'If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
        '    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'sQuery += " ) Rec2"
        ''=================================
        'sQuery += "  left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE =Rec2.MCC_CODE and TSPL_MILK_Shift_End_HEAD.MCC_DATE =convert(date,Rec2.Rec_Date ,103) and TSPL_MILK_Shift_End_HEAD.SHIFT =Rec2 .Rec_Shift "
        'sQuery += " left join (select max(shift) as shift, mcc_code,convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) as DOC_DATE ,sum(manual_stock) as manual_stock,sum(Manual_FAT) as Manual_FAT ,sum(Manual_SNF) as Manual_SNF from TSPL_MILK_Shift_End_HEAD where 2=2 and shift='E'    group by mcc_code,DOC_DATE )closing on closing.mcc_code=Rec2 .MCC_CODE and convert(date,closing.DOC_DATE,103)=convert(date,Rec2.Rec_Date ,103)"
        ''=================================
        'sQuery += " group by Rec2  .Rec_Shift ,convert(date,Rec2.Rec_Date,103),Rec2.MCC_CODE) t_Evening on t_Evening.Rec_Date=t_morning.Rec_Date and t_Evening .MCC_CODE =t_morning .MCC_CODE )) Recp"

        'sQuery += "  Full Join"
        'sQuery += " ("




        'sQuery += " select DIS1.MCC_Code ,max(DIS1 .MCC_NAME)as MCC_NAME ,DIS1 .Dispatch_Date ,sum(DIS1 .Dis_FAT_KG )*100/sum(DIS1 .Dis_Qty ) as Dis_Fat_per,sum(DIS1.Dis_FAT_KG ) as Dis_FAT_KG,sum(DIS1 .Dis_SNF_KG  )*100/sum(DIS1 .Dis_Qty ) as Dis_SNF_per,sum(DIS1 .Dis_SNF_KG) as Dis_SNF_KG,SUM(DIS1.Dis_Qty ) as  Dis_Qty, '' as Tanker_No from (select (TSPL_MCC_Dispatch_Challan.MCC_Code) as MCC_Code ,(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,(TSPL_MCC_Dispatch_Challan.FAT_KG) as Dis_FAT_KG ,(TSPL_MCC_Dispatch_Challan.SNF_KG ) as Dis_SNF_KG   ,(TSPL_MCC_Dispatch_Challan.Net_Qty*cf) as Dis_Qty from TSPL_MCC_Dispatch_Challan"
        'sQuery += " left outer join "
        'sQuery += " TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MCC_Dispatch_Challan.MCC_Code "
        'sQuery += " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*    From TSPL_MCC_Dispatch_Challan      Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =        TSPL_MCC_Dispatch_Challan.Chalan_NO And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO    Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,      TSPL_MCC_Dispatch_Challan.MCC_Code    From TSPL_MCC_Dispatch_Challan      Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =        TSPL_MCC_Dispatch_Challan.Chalan_NO And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNf      On t_SNf.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO "
        'sQuery += " left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 "
        'sQuery += "and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103)"


        'If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
        '    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'sQuery += " ) DIS1"
        'sQuery += " group by convert(date,DIS1.Dispatch_Date,103) ,DIS1.MCC_Code "
        'sQuery += " )  Desp on desp.Dispatch_Date=Recp .Rec_Date and Desp .MCC_Code =Recp.MCC_CODE  )"

        'sQuery += " Full Join"

        'sQuery += "  ("

        'sQuery += " select Trans1 .Mcc_code  as Mcc_code, max(Trans1 .MCC_NAME)as Mcc_Name ,Trans1 .Trans_Date as Trans_Date,sum(Trans1.Trans_Snf_kg )*100/SUM(Trans1 .Trans_QTY )  as Trans_SNf_per ,sum(Trans_Snf_kg) as Trans_Snf_kg,sum(Trans1.Trans_Fat_kg  )*100/SUM(Trans1 .Trans_QTY )  as Trans_FAT_per ,sum(Trans_Fat_kg) as Trans_Fat_kg ,sum(Trans_QTY ) as Trans_QTY, Tanker_No as Trans_Tanker_No  from(select TSPL_Weighment_Detail.UOM as UOM_Code,TSPL_Weighment_Detail.Tanker_No as Tanker_No ,TSPL_MCC_Dispatch_Challan.mcc_code   as Mcc_code, (TSPL_MCC_MASTER.MCC_NAME)as Mcc_Name ,convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as Trans_Date,(TSPL_QUALITY_CHECK.SNF_KG) as Trans_Snf_kg ,(TSPL_QUALITY_CHECK.fat_KG) as Trans_Fat_kg ,(TSPL_Weighment_Detail.Net_Weight*cf ) as Trans_QTY from TSPL_MILK_TRANSFER_IN "
        'sQuery += " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_MILK_TRANSFER_IN.Qc_No"
        'sQuery += " left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_MILK_TRANSFER_IN.Gate_Entry_no"
        'sQuery += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =Tspl_Gate_Entry_Details.Dispatched_From_Mcc"
        'sQuery += " left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No "
        'sQuery += " left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "

        'sQuery += "  and Tspl_Gate_Entry_Details.Gate_Entry_Type = 'P' "



        'If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
        '    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'sQuery += ") Trans1"
        'sQuery += " group by convert(date,Trans1 .Trans_Date ,103),Trans1 .Mcc_code ,Trans1.Tanker_No  "
        'sQuery += " ) Trans on Trans .Trans_Date =Recp .Rec_Date and Trans .Mcc_code =Recp.MCC_CODE  )   left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =Recp .MCC_CODE  or (TSPL_MCC_MASTER.MCC_Code) =desp .MCC_Code or (TSPL_MCC_MASTER.MCC_Code) =trans .MCC_CODE"
        ' ''======================================Added by preeti gupta===========================================
        'sQuery += " union all "
        'sQuery += " select * from ( "
        'sQuery += " select 0 as opening_FAT_KG,0 as Opening_SNF_KG,0 as Closing_FAT_KG,0 as closing_SNF_KG ,0 as Opening_Stock ,0 as closing_Qty ,coalesce(milksrn.SRN_Date,'') as Date ,convert(varchar,coalesce(milksrn .SRN_Date,''),103) as Rec_Date , "
        'sQuery += " coalesce(milksrn .MCC_CODE,'')  as MCC_CODE ,TSPL_LOCATION_MASTER.Location_Desc  as MCC_NAME ,'' as Morning,0 as Mor_Rec_Fat_Per ,"
        'sQuery += " 0 as Mor_Rec_Fat_Kg ,0 as Mor_Rec_SNF_Per ,0 as Mor_Rec_SNF_Kg ,0 as Mor_Rec_Qty,'' as Evening ,0 as Eve_Rec_Fat_Per ,0 as Eve_Rec_Fat_Kg ,"
        'sQuery += " 0 as Eve_Rec_SNF_Per ,0 as Eve_Rec_SNF_Kg,0 as Eve_REC_QTY  ,0 as Dis_Fat_per ,0 as Dis_FAT_KG ,0 as Dis_SNF_per,0 as Dis_SNF_KG ,"
        'sQuery += " 0 as Dis_Qty,milksrn.SRN_FAT_per  as SRN_FAT_per ,milksrn.SRN_Fat_kg  as SRN_Fat_kg  ,milksrn.SRN_SNf_per  as SRN_SNf_per ,"
        'sQuery += " milksrn.SRN_Snf_kg  as SRN_Snf_kg  ,milksrn.SRN_QTY  as SRN_QTY , milksrn.SRN_Tanker_No as SRN_Tanker_No  ,Trans.SRNTrans_FAT_per   as Trans_FAT_per , Trans.SRNTrans_Fat_kg   AS SRNTrans_Fat_kg  ,Trans .SRNTrans_SNf_per   as Trans_SNf_per ,Trans .SRNTrans_Snf_kg   AS SRNTrans_Snf_kg  ,Trans.SRNTrans_QTY  AS SRNTrans_QTY  "
        'sQuery += " ,Trans.SRNTrans_Tanker_No as  SRNTrans_Tanker_No ,(milksrn.SRN_QTY +Trans.SRNTrans_QTY) as Balance "
        'sQuery += " from (("
        'sQuery += " select SRN .Mcc_code  as Mcc_code, max(SRN .MCC_NAME)as Mcc_Name ,SRN .SRN_Date as SRN_Date,sum(SRN.SRN_Snf_kg )*100/SUM(SRN .SRN_QTY )  as SRN_SNf_per ,"
        'sQuery += " sum(SRN_Snf_kg) as SRN_Snf_kg,sum(SRN.SRN_Fat_kg  )*100/SUM(SRN .SRN_QTY )  as SRN_FAT_per ,sum(SRN_Fat_kg) as SRN_Fat_kg ,sum(SRN_QTY ) as SRN_QTY ,max(SRN.Tanker_No) as SRN_Tanker_No from"
        'sQuery += " (select TSPL_Bulk_MILK_SRN.SRN_NO  ,TSPL_Weighment_Detail.UOM as UOM_Code,TSPL_Weighment_Detail.Tanker_No as Tanker_No ,Tspl_Gate_Entry_Details.location_Code  as Mcc_code, (TSPL_LOCATION_MASTER.Location_Desc  )as Mcc_Name ,convert(date,TSPL_Bulk_MILK_SRN.SRN_Date ,103) as SRN_Date,(TSPL_Bulk_MILK_SRN.SNF_KG) as SRN_Snf_kg ,(TSPL_Bulk_MILK_SRN.fat_KG) as SRN_Fat_kg ,(TSPL_Bulk_MILK_SRN.NET_Weight ) as SRN_QTY from TSPL_Bulk_MILK_SRN"
        'sQuery += " left join TSPL_Bulk_MILK_SRN_Chember_Details on TSPL_Bulk_MILK_SRN_Chember_Details.SRN_NO =TSPL_Bulk_MILK_SRN.SRN_NO "
        'sQuery += " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_Bulk_MILK_SRN.Qc_No "
        'sQuery += " left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_Bulk_MILK_SRN.Gate_Entry_no "
        'sQuery += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =Tspl_Gate_Entry_Details.location_Code "
        'sQuery += " left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No  "
        'sQuery += " left outer join (select * from View_GetConversion) "
        'sQuery += "  zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 "
        'sQuery += " and convert(date,TSPL_Bulk_MILK_SRN.SRN_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Bulk_MILK_SRN.SRN_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and "
        'sQuery += " Tspl_Gate_Entry_Details.Gate_Entry_Type ='P' "
        'If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
        '    sQuery += " and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'sQuery += " ) as SRN group by convert(date,SRN .SRN_Date  ,103),SRN .Mcc_code ) MilkSRN"
        'sQuery += " Full Join "
        'sQuery += " ( select SRNTrans1 .Mcc_code  as SRNMcc_code, max(SRNTrans1 .MCC_NAME)as SRNMcc_Name ,SRNTrans1 .Trans_Date as SRNTrans_Date,"
        'sQuery += " sum(SRNTrans1.Trans_Snf_kg )*100/SUM(SRNTrans1 .Trans_QTY )  as SRNTrans_SNf_per ,sum(Trans_Snf_kg) as SRNTrans_Snf_kg,"
        'sQuery += " sum(SRNTrans1.Trans_Fat_kg  )*100/SUM(SRNTrans1 .Trans_QTY )  as SRNTrans_FAT_per ,sum(Trans_Fat_kg) as SRNTrans_Fat_kg ,"
        'sQuery += " sum(Trans_QTY ) as SRNTrans_QTY,SRNTrans1.Tanker_No as SRNTrans_Tanker_No from(select TSPL_Weighment_Detail.UOM as UOM_Code,TSPL_Weighment_Detail.Tanker_No as Tanker_No ,Tspl_Gate_Entry_Details.location_Code   as Mcc_code"
        'sQuery += " , (TSPL_LOCATION_MASTER.Location_Desc  )as Mcc_Name ,convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as Trans_Date"
        'sQuery += " ,(TSPL_QUALITY_CHECK.SNF_KG) as Trans_Snf_kg ,(TSPL_QUALITY_CHECK.fat_KG) as Trans_Fat_kg "
        'sQuery += " ,(TSPL_Weighment_Detail.Net_Weight*cf ) as Trans_QTY from TSPL_MILK_TRANSFER_IN "
        'sQuery += "  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No =TSPL_MILK_TRANSFER_IN.Qc_No"
        'sQuery += "  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSPL_MILK_TRANSFER_IN.Gate_Entry_no "
        'sQuery += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =Tspl_Gate_Entry_Details.location_Code "
        'sQuery += "  left outer join TSPL_Weighment_Detail  on TSPL_QUALITY_CHECK.Weighment_No =TSPL_Weighment_Detail.Weighment_No  "
        'sQuery += " left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2  and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        'If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
        '    sQuery += " and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If

        'sQuery += " ) SRNTrans1 group by convert(date,SRNTrans1 .Trans_Date ,103),SRNTrans1 .Mcc_code,SRNTrans1.Tanker_No     ) Trans on Trans .SRNTrans_Date  =MilkSRN  .SRN_Date  and Trans .SRNMcc_code  =MilkSRN .MCC_CODE"



        'sQuery += "  )   left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =MilkSRN  .MCC_CODE  or (TSPL_LOCATION_MASTER.Location_Code) =trans .SRNMcc_code  "
        ''=========================================================================================================
        'sQuery += " ) as xx   ) as final where isnull(MCC_CODE,'')<>'' order by date "




        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            'For i As Integer = 0 To gv.Rows.Count - 1
            '    gv.Rows(i).Cells(0).Value = i + 1
            'Next
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGridUDL()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
        ViewUDL()
    End Sub
    Sub FormatGridUDL()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("Rec_Date").IsVisible = True
        gv.Columns("Rec_Date").Width = 100
        gv.Columns("Rec_Date").HeaderText = " Date"
        gv.Columns("Rec_Date").FormatString = "{0:d}"


        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = " MCC "

        gv.Columns("Morning").IsVisible = True
        gv.Columns("Morning").Width = 80
        gv.Columns("Morning").HeaderText = " Morning"


        gv.Columns("Mor_Rec_Fat_Per").IsVisible = True
        gv.Columns("Mor_Rec_Fat_Per").Width = 80
        gv.Columns("Mor_Rec_Fat_Per").HeaderText = "Fat%"

        gv.Columns("Mor_Rec_Fat_Kg").IsVisible = True
        gv.Columns("Mor_Rec_Fat_Kg").Width = 80
        gv.Columns("Mor_Rec_Fat_Kg").HeaderText = "Fat KG"

        gv.Columns("Mor_Rec_SNF_Per").IsVisible = True
        gv.Columns("Mor_Rec_SNF_Per").Width = 80
        gv.Columns("Mor_Rec_SNF_Per").HeaderText = "SNF%"

        gv.Columns("Mor_Rec_SNF_Kg").IsVisible = True
        gv.Columns("Mor_Rec_SNF_Kg").Width = 80
        gv.Columns("Mor_Rec_SNF_Kg").HeaderText = "SNF KG"

        gv.Columns("Mor_Rec_Qty").IsVisible = True
        gv.Columns("Mor_Rec_Qty").Width = 80
        gv.Columns("Mor_Rec_Qty").HeaderText = "Total(M)"

        gv.Columns("Evening").IsVisible = True
        gv.Columns("Evening").Width = 80
        gv.Columns("Evening").HeaderText = "Evening"

        gv.Columns("Eve_Rec_Fat_Per").IsVisible = True
        gv.Columns("Eve_Rec_Fat_Per").Width = 80
        gv.Columns("Eve_Rec_Fat_Per").HeaderText = "Fat%"

        gv.Columns("Eve_Rec_Fat_Kg").IsVisible = True
        gv.Columns("Eve_Rec_Fat_Kg").Width = 80
        gv.Columns("Eve_Rec_Fat_Kg").HeaderText = "Fat KG"


        gv.Columns("Eve_Rec_SNF_Per").IsVisible = True
        gv.Columns("Eve_Rec_SNF_Per").Width = 80
        gv.Columns("Eve_Rec_SNF_Per").HeaderText = "SNF%"

        gv.Columns("Eve_Rec_SNF_Kg").IsVisible = True
        gv.Columns("Eve_Rec_SNF_Kg").Width = 80
        gv.Columns("Eve_Rec_SNF_Kg").HeaderText = "SNF KG"

        gv.Columns("Eve_REC_QTY").IsVisible = True
        gv.Columns("Eve_REC_QTY").Width = 80
        gv.Columns("Eve_REC_QTY").HeaderText = "Total(E)"

        gv.Columns("Dis_Fat_per").IsVisible = True
        gv.Columns("Dis_Fat_per").Width = 80
        gv.Columns("Dis_Fat_per").HeaderText = "FAT%"

        gv.Columns("Dis_FAT_KG").IsVisible = True
        gv.Columns("Dis_FAT_KG").Width = 80
        gv.Columns("Dis_FAT_KG").HeaderText = "FAT Kg"

        gv.Columns("Dis_SNF_per").IsVisible = True
        gv.Columns("Dis_SNF_per").Width = 80
        gv.Columns("Dis_SNF_per").HeaderText = "SNF%"

        gv.Columns("Dis_SNF_KG").IsVisible = True
        gv.Columns("Dis_SNF_KG").Width = 80
        gv.Columns("Dis_SNF_KG").HeaderText = "SNF Kg"

        gv.Columns("Dis_Qty").IsVisible = True
        gv.Columns("Dis_Qty").Width = 80
        gv.Columns("Dis_Qty").HeaderText = "Total(D)"
        '====================================
        'gv.Columns("SRN_FAT_per").IsVisible = True
        'gv.Columns("SRN_FAT_per").Width = 80
        'gv.Columns("SRN_FAT_per").HeaderText = "FAT%"
        'gv.Columns("SRN_FAT_per").FormatString = "{0:F1}"


        'gv.Columns("SRN_Fat_kg").IsVisible = True
        'gv.Columns("SRN_Fat_kg").Width = 80
        'gv.Columns("SRN_Fat_kg").HeaderText = "FAT Kg"

        'gv.Columns("SRN_SNf_per").IsVisible = True
        'gv.Columns("SRN_SNf_per").Width = 80
        'gv.Columns("SRN_SNf_per").HeaderText = "SNF%"
        'gv.Columns("SRN_SNf_per").FormatString = "{0:F1}"

        'gv.Columns("SRN_Snf_kg").IsVisible = True
        'gv.Columns("SRN_Snf_kg").Width = 80
        'gv.Columns("SRN_Snf_kg").HeaderText = "SNF Kg"

        'gv.Columns("SRN_QTY").IsVisible = True
        'gv.Columns("SRN_QTY").Width = 80
        'gv.Columns("SRN_QTY").HeaderText = "Total(D)"

        'gv.Columns("SRN_Tanker_No").IsVisible = False
        'gv.Columns("SRN_Tanker_No").Width = 100
        'gv.Columns("SRN_Tanker_No").HeaderText = "Tanker No"

        '=====================================

        gv.Columns("Trans_FAT_per").IsVisible = True
        gv.Columns("Trans_FAT_per").Width = 80
        gv.Columns("Trans_FAT_per").HeaderText = "FAT%"
        gv.Columns("Trans_FAT_per").FormatString = "{0:n2}"

        gv.Columns("Trans_Fat_kg").IsVisible = True
        gv.Columns("Trans_Fat_kg").Width = 80
        gv.Columns("Trans_Fat_kg").HeaderText = "FAT kg"
        gv.Columns("Trans_Fat_kg").FormatString = "{0:n2}"

        gv.Columns("Trans_SNf_per").IsVisible = True
        gv.Columns("Trans_SNf_per").Width = 80
        gv.Columns("Trans_SNf_per").HeaderText = "SNF%"
        gv.Columns("Trans_SNf_per").FormatString = "{0:n2}"

        gv.Columns("Trans_Snf_kg").IsVisible = True
        gv.Columns("Trans_Snf_kg").Width = 80
        gv.Columns("Trans_Snf_kg").HeaderText = "SNF Kg"
        gv.Columns("Trans_Snf_kg").FormatString = "{0:n2}"

        gv.Columns("Trans_QTY").IsVisible = True
        gv.Columns("Trans_QTY").Width = 80
        gv.Columns("Trans_QTY").HeaderText = "Total(T)"

        gv.Columns("Trans_Tanker_No").IsVisible = True
        gv.Columns("Trans_Tanker_No").Width = 100
        gv.Columns("Trans_Tanker_No").HeaderText = "Tanker No"

        gv.Columns("Balance").IsVisible = True
        gv.Columns("Balance").Width = 80
        gv.Columns("Balance").HeaderText = "Balance"

        gv.Columns("Opening_Stock").IsVisible = True
        gv.Columns("Opening_Stock").Width = 80
        gv.Columns("Opening_Stock").HeaderText = "Opening Stock"

        gv.Columns("closing_Qty").IsVisible = True
        gv.Columns("closing_Qty").Width = 80
        gv.Columns("closing_Qty").HeaderText = "closing Stock"

        gv.Columns("opening_FAT_kG").IsVisible = True
        gv.Columns("opening_FAT_kG").Width = 80
        gv.Columns("opening_FAT_kG").HeaderText = "FAT KG"

        gv.Columns("opening_SNF_kG").IsVisible = True
        gv.Columns("opening_SNF_kG").Width = 80
        gv.Columns("opening_SNF_kG").HeaderText = "SNF KG"

        gv.Columns("Closing_FAT_kG").IsVisible = True
        gv.Columns("Closing_FAT_kG").Width = 80
        gv.Columns("Closing_FAT_kG").HeaderText = "FAT KG"

        gv.Columns("closing_SNF_kG").IsVisible = True
        gv.Columns("closing_SNF_kG").Width = 80
        gv.Columns("closing_SNF_kG").HeaderText = "SNF KG"

        gv.Columns("varation_Qty").IsVisible = True
        gv.Columns("varation_Qty").Width = 80
        gv.Columns("varation_Qty").HeaderText = "Total(D)"

        gv.Columns("Varation_FAT_KG").IsVisible = True
        gv.Columns("Varation_FAT_KG").Width = 80
        gv.Columns("Varation_FAT_KG").HeaderText = "FAT KG"

        gv.Columns("Varation_SNF_KG").IsVisible = True
        gv.Columns("Varation_SNF_KG").Width = 80
        gv.Columns("Varation_SNF_KG").HeaderText = "SNF KG"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("SRN_QTY", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Opening_Stock", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("opening_FAT_kG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("opening_SNF_kG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("closing_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Closing_FAT_kG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("closing_SNF_kG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("varation_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Varation_FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Varation_SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Mor_Rec_Fat_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Mor_Rec_SNF_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Mor_Rec_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("Eve_Rec_Fat_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("Eve_Rec_SNF_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("Eve_REC_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        'Dim item17 As New GridViewSummaryItem("SRN_Fat_kg", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item17)
        'Dim item18 As New GridViewSummaryItem("SRN_Snf_kg", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item18)
        Dim item19 As New GridViewSummaryItem("Trans_Fat_kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item19)
        Dim item20 As New GridViewSummaryItem("Trans_Snf_kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)
        Dim item21 As New GridViewSummaryItem("Trans_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub ViewUDL()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Rec_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_NAME").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Opening"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Opening_Stock").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("opening_FAT_kG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("opening_SNF_kG").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Sample (M)"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Morning").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_Fat_Per").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_Fat_Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_SNF_Per").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_SNF_Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Mor_Rec_Qty").Name)
            'gv.ViewDefinition = view

            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Sample (E)"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Evening").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_Fat_Per").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_Fat_Kg").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_SNF_Per").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Eve_Rec_SNF_Kg").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Eve_REC_QTY").Name)


            'view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch"))
            'view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Dis_Fat_per"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Dis_FAT_KG"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Dis_SNF_per"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Dis_SNF_KG"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Dis_Qty"))

            'view.ColumnGroups.Add(New GridViewColumnGroup("Bulk Milk SRN"))
            'view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("SRN_FAT_per"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("SRN_Fat_kg"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("SRN_SNf_per"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("SRN_Snf_kg"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("SRN_QTY"))
            'view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("SRN_Tanker_No"))


            view.ColumnGroups.Add(New GridViewColumnGroup("Transfer"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_FAT_per").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_Fat_kg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_SNf_per").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_Snf_kg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_QTY").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Trans_Tanker_No").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Closing"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("closing_Qty").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Closing_FAT_kG").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("closing_SNF_kG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Variation"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("Varation_FAT_KG").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("Varation_SNF_KG").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("varation_Qty").Name)

            'view.ColumnGroups.Add(New GridViewColumnGroup(""))
            'view.ColumnGroups(9).Rows.Add(New GridViewColumnGroupRow())
            'view.ColumnGroups(9).Rows(0).ColumnNames.Add(gv.Columns("Balance"))

            gv.ViewDefinition = view
        End If

    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class

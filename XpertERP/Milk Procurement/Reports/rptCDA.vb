Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=========added tree and shift by shivani==========='
Public Class RptCDA
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptCDA)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
    End Sub
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        
        If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        Dim whrcls As String = " where 2=2 "

        Dim arr As List(Of String) = Nothing
        If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
            arr = cbtMCCRouteVLCC.CheckedValue(1)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                whrcls += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
            Else
                Throw New Exception("Please select at least one MCC")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
            arr = cbtMCCRouteVLCC.CheckedValue(2)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                whrcls += " and final.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one Route")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
            arr = cbtMCCRouteVLCC.CheckedValue(3)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                whrcls += " and TSPL_VLC_MASTER_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one Route")
            End If
        End If

        whrcls += "  and convert(date,Final .DOC_DATE,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,Final .DOC_DATE,103) <=convert(date,('" + txtToDate.Value + "'),103) "
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            whrcls += " and 2=( case when Final.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            whrcls += " and 2=( case when Final.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
        End If

        Dim sQuery As String = "SELECT VLC,max(MCC_CODE)  as MCC_CODE,max(VLC_Name) as VLC_NAME ,convert(DECIMAL(18,2),sum(QTY)) as QTY,convert(decimal(18,1),sum(FAT_PER)) as FAT_PER,convert(decimal(18,1),sum(SNF_PER)) as SNF_PER,convert(decimal(18,2),sum(FAT_KG)) as FAT_KG,convert(Decimal(18,2),sum(SNF_KG)) as SNF_KG,convert(decimal(18,2),sum(VLCQTY)) as VLCQTY,convert(decimal(18,1),sum(VLC_FAT_PER)) as VLC_FAT_PER,convert(decimal(18,1),sum(VLC_SNF_PER)) as VLC_SNF_PER,convert(Decimal(18,2),sum(VLC_FAT_KG)) as VLC_FAT_KG ,convert(decimal(18,2),sum(VLC_SNF_KG)) as VLC_SNF_KG,convert(decimal(18,2),sum(MCCQTY)) as MCCQTY,convert(decimal(18,1),sum(MCC_FAT_PER)) as MCC_FAT_PER,convert(decimal(18,1),sum(MCC_SNF_PER)) as MCC_SNF_PER,convert(decimal(18,2),sum(MCC_FAT_KG)) as MCC_FAT_KG,convert(decimal(18,2),sum(MCC_SNF_KG)) as MCC_SNF_KG ,max(ROUTE_CODE)as ROUTE_CODE from" & Environment.NewLine & _
        " ( SELECT VLC,(MCC)+' -'+(TSPL_MCC_MASTER .MCC_NAME )  as MCC_CODE,(DOC_DATE) as DOC_DATE ,(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_NAME ,(QTY) as QTY,(FAT_PER) as FAT_PER,(SNF_PER) as SNF_PER,(FAT_KG) as FAT_KG,(SNF_KG) as SNF_KG,(VLCQTY) as VLCQTY,(VLC_FAT_PER) as VLC_FAT_PER,(VLC_SNF_PER) as VLC_SNF_PER,(VLC_FAT_KG) as VLC_FAT_KG ,(VLC_SNF_KG) as VLC_SNF_KG,(MCCQTY) as MCCQTY,(MCC_FAT_PER) as MCC_FAT_PER,(MCC_SNF_PER) as MCC_SNF_PER,(MCC_FAT_KG) as MCC_FAT_KG,(MCC_SNF_KG) as MCC_SNF_KG,final.ROUTE_CODE" & Environment.NewLine & _
        " FROM( select VLC,DOC_DATE ,0 as QTY,0 as FAT_PER,0 as SNF_PER,0 AS FAT_KG,0 AS SNF_KG, VLCQTY  ,VLC_FAT_PER   ,VLC_SNF_PER   ,Convert(Decimal(18,2),(VLC_FAT_PER   *VLCQTY  /100)) as VLC_FAT_KG,Convert(DECIMAL(18,2),(VLC_SNF_PER   * VLCQTY  /100)) as VLC_SNF_KG ,MCCQTY  ,MCC_FAT_PER    ,MCC_SNF_PER   ,Convert(Decimal(18,2),(MCC_FAT_PER    *MCCQTY   /100)) as MCC_FAT_KG,Convert(DECIMAL(18,2),(MCC_SNF_PER    * MCCQTY   /100)) as MCC_SNF_KG,ROUTE_CODE   from (select MAX(TOUOM ) as UOM,VLC_CODE as VLC,Shift,max(DOC_DATE) as DOC_DATE,convert(Decimal(18,2),sum(VLCFATQTY )/(case when sum(VLCNewQty ) <= 0 then 1 else sum(VLCNewQty  ) end)*100) as VLC_FAT_PER, convert(DECIMAL(18,2),sum(VLCSNFQTY  )/(case when sum(VLCNewQty ) <= 0 then 1 else sum(VLCNewQty  ) end)*100) as VLC_SNF_PER,SUM(isnull(VLCNewQty ,0) ) as VLCQTY , convert(Decimal(18,2),(sum(MCCFATQTY  )/(case when sum(MCCNewQty) <= 0 then 1 else sum(MCCNewQty  ) end) *100)) as MCC_FAT_PER,convert(Decimal(18,2),(sum(MCCSNFQTY   )/(case when sum(MCCNewQty) <= 0 then 1 else sum(MCCNewQty  ) end) *100)) as MCC_SNF_PER,SUM(isnull(MCCNewQty ,0) ) as MCCQTY,max(Route_Code)as Route_Code from" & Environment.NewLine & _
        " (select UOM_Code,VLC_CODE,SHIFT,DOC_DATE ,VLCFATQTY *CF as VLCFATQTY,VLCSNFQTY *CF as VLCSNFQTY,Vlc_Qty *CF as VLCNewQty,MCCFATQTY  *CF as MCCFATQTY,MCCSNFQTY  *CF as MCCSNFQTY,Mcc_Qty  *CF as MCCNewQty,FromUOM,TOUOM,CF,Route_Code from(select TSPL_MILK_RECEIPT_DETAIL.Item_Code , Tspl_Milk_Truck_Sheet_Detail .VLC_CODE,Tspl_Milk_Truck_Sheet_Head .SHIFT  ,Tspl_Milk_Truck_Sheet_Head.DOC_DATE ,TSPL_MILK_RECEIPT_DETAIL.UOM_Code ,Tspl_Milk_Truck_Sheet_Detail.Vlc_Qty ,Tspl_Milk_Truck_Sheet_Detail.Vlc_FAT,(Tspl_Milk_Truck_Sheet_Detail.Vlc_FAT *Tspl_Milk_Truck_Sheet_Detail.Vlc_Qty /100) as VLCFATQTY ,Tspl_Milk_Truck_Sheet_Detail.Vlc_SNF,(Tspl_Milk_Truck_Sheet_Detail.Vlc_SNF  *Tspl_Milk_Truck_Sheet_Detail.Vlc_Qty /100) as VLCSNFQTY  ,Tspl_Milk_Truck_Sheet_Detail.Mcc_Qty ,Tspl_Milk_Truck_Sheet_Detail.Mcc_FAT,(Tspl_Milk_Truck_Sheet_Detail.Mcc_FAT  *Tspl_Milk_Truck_Sheet_Detail.Mcc_Qty  /100) as MCCFATQTY ,Tspl_Milk_Truck_Sheet_Detail.Mcc_SNF,(Tspl_Milk_Truck_Sheet_Detail.Mcc_SNF  *Tspl_Milk_Truck_Sheet_Detail.Mcc_Qty  /100) as MCCSNFQTY ,TSPL_MILK_RECEIPT_DETAIL.Route_Code  from Tspl_Milk_Truck_Sheet_Detail  left outer join Tspl_Milk_Truck_Sheet_Head on Tspl_Milk_Truck_Sheet_Head.DOC_CODE =Tspl_Milk_Truck_Sheet_Detail.DOC_CODE   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =Tspl_Milk_Truck_Sheet_Detail.VLC_Code  left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD .DOC_CODE =Tspl_Milk_Truck_Sheet_Detail .Milk_Receipt_Code left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and Tspl_Milk_Truck_Sheet_Detail.VLC_DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE ) xx  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code " & Environment.NewLine

        ''richa agarwal 04 jUN,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
        Else
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and zzz.TOUOM='" + cboUnit.Text + "' )" & Environment.NewLine
        End If

        sQuery += " tt group by VLC_Code,SHIFT   )ff " & Environment.NewLine & _
        " union all " & Environment.NewLine & _
        " select VLC, vlc_DOC_DATE  ,QTY,FAT_PER  ,SNF_PER  ,Convert(Decimal(18,2),(FAT_PER  *QTY /100)) as FAT_KG,Convert(DECIMAL(18,2),(SNF_PER  *QTY /100)) as SNF_KG ,0 as VLCQTY,0 as VLC_FAT_PER,0 as VLC_SNF_PER,0 as VLC_FAT_KG,0 as VLC_SNF_KG,0 as MCCQTY,0 as MCC_FAT_PER,0 as MCC_SNF_PER,0 as MCC_FAT_KG,0 as MCC_SNF_KG,Route_Code from(select MAX(TOUOM ) as UOM,VLC_CODE as VLC,MAX(DOC_DATE )as vlc_DOC_DATE,shift,max(Route_Code) as Route_Code, " & Environment.NewLine & _
        " convert(Decimal(18,2),sum(FATQTY)/(case when sum(NewQty ) <= 0 then 1 else sum(NewQty  ) end)*100) as FAT_PER, convert(DECIMAL(18,2),sum(SNFQTY )/(case when sum(NewQty ) <= 0 then 1 else sum(NewQty  ) end)*100) as SNF_PER,SUM(isnull(NewQty,0) ) as QTY from (select UOM_Code,VLC_CODE,DOC_DATE,Shift,Route_Code ,FATQTY*CF as FATQTY,SNFQTY*CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF  from " & Environment.NewLine & _
        " (select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code, TSPL_VLC_DATA_UPLOADER_MASTER .VLC_Code,convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as DOC_DATE  ,VLC_Name  ,TSPL_VLC_DATA_UPLOADER_MASTER.Shift,TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code  ,TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code as UOM_Code , " & Environment.NewLine & _
        " TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer,(TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer*TSPL_VLC_DATA_UPLOADER_DETAIL.Qty/100) as SNFQTY ,TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer,(TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer *TSPL_VLC_DATA_UPLOADER_DETAIL.Qty/100) as FATQTY  from TSPL_VLC_DATA_UPLOADER_DETAIL   left outer join TSPL_VLC_DATA_UPLOADER_MASTER  on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code =TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code " & Environment.NewLine & _
        "  left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_VLC_DATA_UPLOADER_MASTER.ROUTE_CODE  ) xx  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code " & Environment.NewLine

        ''richa agarwal 04 jUN,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
        Else
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and lower(zzz.TOUOM)='" + cboUnit.Text + "' ) "
        End If


        sQuery += " tt group by VLC_Code,Shift   ) ff)Final  " & Environment.NewLine & _
        " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =Final.VLC  left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER.MCC_Code =TSPL_VLC_MASTER_HEAD .MCC and TSPL_VLC_MASTER_HEAD.VLC_Code =Final.VLC" & Environment.NewLine & _
        " left outer join TSPL_VLC_DATA_UPLOADER_MASTER on  TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code  =Final .VLC " & Environment.NewLine & _
        " and Document_Date=Final.DOC_DATE" & Environment.NewLine & _
        " " + whrcls + "" & Environment.NewLine & _
        "  )preeti group by vlc" & Environment.NewLine

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
        view()
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
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
    End Sub

    Sub FormatGrid()
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("VLC").IsVisible = True
        gv.Columns("VLC").Width = 100
        gv.Columns("VLC").HeaderText = " VLC Code"

        gv.Columns("MCC_CODE").IsVisible = True
        gv.Columns("MCC_CODE").Width = 100
        gv.Columns("MCC_CODE").HeaderText = " MCC Code"

        gv.Columns("VLC_NAME").IsVisible = True
        gv.Columns("VLC_NAME").Width = 30
        gv.Columns("VLC_NAME").HeaderText = " VLC NAME"

        gv.Columns("QTY").IsVisible = True
        gv.Columns("QTY").Width = 100
        gv.Columns("QTY").HeaderText = "QTY"

        gv.Columns("FAT_PER").IsVisible = True
        gv.Columns("FAT_PER").Width = 100
        gv.Columns("FAT_PER").HeaderText = "FAT%"

        gv.Columns("SNF_PER").IsVisible = True
        gv.Columns("SNF_PER").Width = 100
        gv.Columns("SNF_PER").HeaderText = "SNF%"

        gv.Columns("FAT_KG").IsVisible = True
        gv.Columns("FAT_KG").Width = 100
        gv.Columns("FAT_KG").HeaderText = " FAT KG"

        gv.Columns("SNF_KG").IsVisible = True
        gv.Columns("SNF_KG").Width = 100
        gv.Columns("SNF_KG").HeaderText = "SNF KG"

        gv.Columns("VLCQTY").IsVisible = True
        gv.Columns("VLCQTY").Width = 100
        gv.Columns("VLCQTY").HeaderText = "QTY"

        gv.Columns("VLC_FAT_PER").IsVisible = True
        gv.Columns("VLC_FAT_PER").Width = 100
        gv.Columns("VLC_FAT_PER").HeaderText = "FAT%"

        gv.Columns("VLC_SNF_PER").IsVisible = True
        gv.Columns("VLC_SNF_PER").Width = 100
        gv.Columns("VLC_SNF_PER").HeaderText = "SNF%"

        gv.Columns("VLC_FAT_KG").IsVisible = True
        gv.Columns("VLC_FAT_KG").Width = 100
        gv.Columns("VLC_FAT_KG").HeaderText = "FAT KG"

        gv.Columns("VLC_SNF_KG").IsVisible = True
        gv.Columns("VLC_SNF_KG").Width = 100
        gv.Columns("VLC_SNF_KG").HeaderText = "SNF KG"

        gv.Columns("MCCQTY").IsVisible = True
        gv.Columns("MCCQTY").Width = 100
        gv.Columns("MCCQTY").HeaderText = "QTY"

        gv.Columns("MCC_FAT_PER").IsVisible = True
        gv.Columns("MCC_FAT_PER").Width = 100
        gv.Columns("MCC_FAT_PER").HeaderText = "FAT%"

        gv.Columns("MCC_SNF_PER").IsVisible = True
        gv.Columns("MCC_SNF_PER").Width = 100
        gv.Columns("MCC_SNF_PER").HeaderText = "SNF%"

        gv.Columns("MCC_FAT_KG").IsVisible = True
        gv.Columns("MCC_FAT_KG").Width = 100
        gv.Columns("MCC_FAT_KG").HeaderText = "FAT KG"

        gv.Columns("MCC_SNF_KG").IsVisible = True
        gv.Columns("MCC_SNF_KG").Width = 100
        gv.Columns("MCC_SNF_KG").HeaderText = "SNF KG"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("VLCQTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("VLC_FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("VLC_SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("MCCQTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("MCC_FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("MCC_SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
      
        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_CODE as Item format ""{0}: {1}"" Group By MCC_CODE"))
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub view()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("VILLAGE PURCHASE"))

            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("QTY").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("FAT_PER").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNF_PER").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("FAT_KG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNF_KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("MCC RECEIPT"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLCQTY").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_FAT_PER").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_SNF_PER").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_FAT_KG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_SNF_KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("VILLAGE DISPATCH"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCCQTY").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_FAT_PER").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_SNF_PER").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_FAT_KG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_SNF_KG").Name)
            gv.ViewDefinition = view
        End If
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        cboUnit.Text = "KG"
        LoadMCCRouteVLCTree()
        LoadShiftFrom()
        LoadShiftTo()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                Dim arr As List(Of String)
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("CDA", gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("CDA", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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
    Private Sub RptCDA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Private Sub RptCDA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Sub LoadMCCRouteVLCTree()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If
    End Sub
    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.Excel)
    End Sub
End Class

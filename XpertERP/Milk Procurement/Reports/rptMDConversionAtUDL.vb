Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptMDConversionAtUDL
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim TankerFromMaster As Integer
    Dim isShowTreeView As Boolean = True
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.MccSummaryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
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
    Sub Reset()
        gv1.DataSource = Nothing
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        rdDetail.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
    Public Sub Load_Report()
        Try
            Dim sQueryDetail As String = String.Empty
            Dim sQuerySummary As String = String.Empty
            Dim sQueryStock As String = String.Empty
            Dim QryFinal As String = String.Empty
            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            '=====================================[Query for only detail part (if any change for another ticket plz change in this section)]
            sQueryDetail = " select  convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)  as [Date],Tspl_Gate_Entry_Details.Supplier_Code as PartyCode,TSPL_SUPPLIER_MASTER.DESCRIPTION as PartyName,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE as Milk_Type_Code," & _
                            " Tspl_Gate_Entry_Details.Challan_No  as Challan_No,convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103)   as Challan_Date," & _
                            " Tspl_Gate_Entry_Details.Gate_Entry_No as Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103)  as Gate_Entry_Date,Tspl_Gate_Entry_Details.Date_And_Time  as Gate_Entry_Date_Time," & _
                            " Tspl_Gate_Entry_Details.Tanker_No as Tanker_No,Tspl_Gate_Entry_Chember_Details.Chamber_Qty*CF  as Challan_Qty," & _
                            " Tspl_Gate_Entry_Chember_Details.Chamber_Qty * Tspl_Gate_Entry_Chember_Details.fat_per /  100 as FAT_KG,Tspl_Gate_Entry_Chember_Details.fat_per as FAT_PER,Tspl_Gate_Entry_Chember_Details.Chamber_Qty * Tspl_Gate_Entry_Chember_Details.snf_Per /  100 as SNF_KG," & _
                            " Tspl_Gate_Entry_Chember_Details.snf_Per as SNF_PER,TSPL_MILK_UNLOADING.Unloading_No ,TSPL_MILK_UNLOADING.Unloading_Date_Time ," & _
                            " TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight*CF as Net_Weight  ,t_FAT.Param_Field_Value as Weighment_fat_per ," & _
                            " t_SNF.Param_Field_Value as Weighment_snf_per ,t_FAT.Param_Field_Value*TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight/100 as Weighment_Fat_kg," & _
                            " t_SNF.Param_Field_Value*TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight/100 as Weighment_SNF_kg , " & _
                            " (t_SNF.Param_Field_Value*TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight/100)*0.975/0.965 as SMP_In_Kg," & _
                            " (t_FAT.Param_Field_Value*TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight/100)*0.9825/0.865 as Butter_In_kg " & _
                             " from Tspl_Gate_Entry_Details" & _
                             " left join Tspl_Gate_Entry_Chember_Details on Tspl_Gate_Entry_Chember_Details.GE_Code=Tspl_Gate_Entry_Details.Gate_Entry_No " & _
                             " left join TSPL_SUPPLIER_MASTER on TSPL_SUPPLIER_MASTER.SUPPLIER_CODE=Tspl_Gate_Entry_Details.Supplier_Code " & _
                            " left outer join TSPL_Weighment_Detail  on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_Weighment_Detail.Gate_Entry_No  " & _
                            " left join  TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No =TSPL_Weighment_Detail.Weighment_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc " & _
                            " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.Gate_Entry_No  =TSPL_Weighment_Detail.Gate_Entry_No " & _
                            " left join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Weighment_No = TSPL_QUALITY_CHECK.Weighment_No " & _
                            " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =Tspl_Gate_Entry_Details.location_Code  " & _
                             " Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " & _
                             " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " & _
                            " left outer join (select * from View_GetConversion) zzz on zzz.FromUOM =TSPL_Weighment_Detail.UOM   and lower(zzz.TOUOM)='KG' " & _
                            " where 2 = 2 and Tspl_Gate_Entry_Details.Doc_Type='BulkProc' and Gate_Entry_Type ='J'"
            If rdDetail.IsChecked OrElse rdSummary.IsChecked Then
                sQueryDetail += "   and convert(date,Tspl_Gate_Entry_Details.Date_And_Time ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time ,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "
            End If



            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                sQueryDetail += " and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If rdDetail.IsChecked Then
                sQueryDetail += " order by convert(date,Tspl_Gate_Entry_Details.Date_And_Time ,103)"
            End If

            '====================================================[END of detail part]=======================================================================================
            '============================================================[Query for summary]=================================================================================
            sQuerySummary = "select convert(date,Gate_Entry_Date_Time,103) as Gate_Entry_Date,convert(varchar,max(Gate_Entry_Date_Time),103) as Gate_Entry_Date1,sum(Challan_Qty ) as Challan_Qty," & _
                            " sum(FAT_KG) as challan_Fat_kg,sum(SNF_KG ) as Challan_SNF_KG ,sum(Net_Weight) as Weighment_Net_Weight,sum(Weighment_Fat_kg ) as Weighment_Fat_kg," & _
                            " sum(Weighment_SNF_kg ) as Weighment_SNF_kg,sum(SMP_In_Kg ) as SMP_In_Kg,sum(Butter_In_kg ) as Butter_In_kg,sum(SMP_In_Kg)*0.975/0.965 as SMP_Formula," & _
                            "  sum(Butter_In_kg)*0.9825/0.865 as Butter_Formula " & _
                            " from ( " & sQueryDetail & " )" & _
                            " as Summary group by convert(date,Gate_Entry_Date_Time,103)"
            '===========================================================[END for Summary Part]======================================================================================================

            sQueryStock = "With CTETemp as (" & _
                          " select Row_Number() OVER ( ORDER BY  Gate_Entry_Date_Time) as RowNo, " & _
                          " Gate_Entry_Date_Time,Gate_Entry_Date,Opening_SMP_In_Kg,SMP_In_Kg,SMP_Dispatch,sum(Closing_SMP_In_Kg)OVER ( ORDER BY Gate_Entry_Date_Time) as Closing_SMP_In_Kg," & _
                          " Opening_Butter_In_kg,Butter_In_kg,Butter_Dispatch,sum(Closing_Butter_In_kg)OVER ( ORDER BY Gate_Entry_Date_Time) as Closing_Butter_In_kg from " & _
                          " ( " & _
                          " select Gate_Entry_Date_Time ,max(Gate_Entry_Date) as Gate_Entry_Date,sum(final.Opening_SMP_In_Kg ) as Opening_SMP_In_Kg,sum(SMP_In_Kg ) as SMP_In_Kg," & _
                          " sum(SMP_Dispatch ) as SMP_Dispatch,sum(final.Opening_SMP_In_Kg )+sum(SMP_In_Kg )+sum(SMP_Dispatch ) as Closing_SMP_In_Kg," & _
                          " sum(Opening_Butter_In_kg)as Opening_Butter_In_kg,sum(Butter_In_kg ) as Butter_In_kg,sum(Butter_Dispatch ) as Butter_Dispatch," & _
                          " sum(Opening_Butter_In_kg)+sum(Butter_In_kg )+sum(Butter_Dispatch ) as Closing_Butter_In_kg " & _
                          " from (" & _
                          " select convert(date,'" + txtFromDate.Value + "',103) as Gate_Entry_Date_Time,max('" + txtFromDate.Value + "') as Gate_Entry_Date," & _
                        " sum(summary .SMP_In_Kg ) as Opening_SMP_In_Kg,0 as SMP_In_Kg,0 as SMP_Dispatch ,sum(summary .Butter_In_kg ) as Opening_Butter_In_kg," & _
                        " 0 as Butter_In_kg,0 as Butter_Dispatch from (" & _
                        "" & sQueryDetail & "" & _
                        " ) as summary  WHERE convert(date,Gate_Entry_Date_Time,103)<(convert(date,'" + txtFromDate.Value + "',103)) " & _
                        " group by convert(date,Gate_Entry_Date_Time,103)" & _
                        " Union all " & _
                        "  select convert(date,Gate_Entry_Date_Time,103) as Gate_Entry_Date_Time,max(Gate_Entry_Date) as Gate_Entry_Date,0 as opening_SMP_In_Kg,sum(SMP_In_Kg ) as SMP_In_Kg,0 as SMP_Dispatch," & _
                        "  0 as opening_Butter_In_kg,sum(Butter_In_kg ) as Butter_In_kg,0 as Butter_Dispatch " & _
                        " from (" & _
                       "" & sQueryDetail & "" & _
                        " ) as summary where 2 = 2 and convert(date,summary .Gate_Entry_Date_Time  ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,summary.Gate_Entry_Date_Time  ,103) <=convert(date,'" + txtToDate.Value + "' ,103) " & _
                         " group by convert(date,Gate_Entry_Date_Time,103)" & _
                         " ) as final group by convert(date,Gate_Entry_Date_Time,103)" & _
                         " ) as xx" & _
                         " )" & _
                        " (Select CTETemp.Gate_Entry_Date_Time ,CTETemp.Gate_Entry_Date ," & _
                        " CTETemp.Opening_SMP_In_Kg+ISNULL(CT1.Closing_SMP_In_Kg,0) as  Opening_SMP_In_Kg  ,CTETemp.SMP_In_Kg , CTETemp.SMP_Dispatch , CTETemp.Closing_SMP_In_Kg ," & _
                        " CTETemp.Opening_Butter_In_kg +ISNULL(CT1.Closing_Butter_In_kg ,0) as  Opening_Butter_In_Kg  ,CTETemp.Butter_In_kg  , CTETemp.Butter_Dispatch  , CTETemp.Closing_Butter_In_kg   " & _
                        " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  (CTETemp.RowNo-CT1.RowNo)=1 ) "
            If rdSummary.IsChecked Then
                QryFinal = "" & sQuerySummary & ""
            ElseIf rdDetail.IsChecked Then
                QryFinal = "" & sQueryDetail & ""
            ElseIf rdStock.IsChecked Then
                QryFinal = "" & sQueryStock & ""
            End If
            Dim dtgv1 As New DataTable
            Dim dtcomp As New DataTable
            Dim dtgv1Stock As New DataTable
            dtgv1 = clsDBFuncationality.GetDataTable(QryFinal)
            Dim qrycompany As String = "SELECT TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3, "
            qrycompany += " '" + txtFromDate.Value + "' AS FROM_DATE,'" + txtToDate.Value + "' AS TO_DATE FROM TSPL_COMPANY_MASTER "
            dtcomp = clsDBFuncationality.GetDataTable(qrycompany)

            If dtgv1 IsNot Nothing And dtgv1.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv1
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()

                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If rdDetail.IsChecked = True Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv1, dtcomp, "MDConversionDetail", "MD Conversion", "RPTCOMPANYADDMDCONVERSION.rpt")
                    ElseIf rdSummary.IsChecked Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv1, dtcomp, "rptMDCONVERSIONSUMMARY", "MD Conversion", "RPTCOMPANYADDMDCONVERSION.rpt")
                    ElseIf rdStock.IsChecked Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv1, dtcomp, "rptMDCONVERSIONSTOCK", "MD Conversion", "RPTCOMPANYADDMDCONVERSION.rpt")
                    End If
                    frmCRV = Nothing
                End If
                RadPageView1.SelectedPage = RadPageViewPage2

            Else
                tmpValLoad = False
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
            gv1.BestFitColumns()
            ReStoreGridLayout()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub FormatGrid()
        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdDetail.IsChecked Then
            gv1.Columns("PartyName").IsVisible = True
            gv1.Columns("PartyName").Width = 150
            gv1.Columns("PartyName").HeaderText = "Party Name"

            gv1.Columns("Milk_Type_Code").IsVisible = True
            gv1.Columns("Milk_Type_Code").Width = 200
            gv1.Columns("Milk_Type_Code").HeaderText = "Milk Type"

            gv1.Columns("Challan_No").IsVisible = True
            gv1.Columns("Challan_No").Width = 100
            gv1.Columns("Challan_No").HeaderText = " Challan No."

            gv1.Columns("Challan_Date").IsVisible = True
            gv1.Columns("Challan_Date").Width = 100
            gv1.Columns("Challan_Date").HeaderText = " Challan Date"


            gv1.Columns("Gate_Entry_No").IsVisible = True
            gv1.Columns("Gate_Entry_No").Width = 100
            gv1.Columns("Gate_Entry_No").HeaderText = "Doc No."

            gv1.Columns("Gate_Entry_Date_Time").IsVisible = True
            gv1.Columns("Gate_Entry_Date_Time").Width = 100
            gv1.Columns("Gate_Entry_Date_Time").HeaderText = "RECD date Time"

            gv1.Columns("Date").IsVisible = False
            gv1.Columns("Date").Width = 100
            gv1.Columns("Date").HeaderText = "Date"

            gv1.Columns("Gate_Entry_Date").IsVisible = False
            gv1.Columns("Gate_Entry_Date").Width = 100
            gv1.Columns("Gate_Entry_Date").HeaderText = "Gate_Entry_Date"

            gv1.Columns("Tanker_No").IsVisible = True
            gv1.Columns("Tanker_No").Width = 100
            gv1.Columns("Tanker_No").HeaderText = "Tanker No"

            gv1.Columns("Challan_Qty").IsVisible = True
            gv1.Columns("Challan_Qty").Width = 120
            gv1.Columns("Challan_Qty").HeaderText = "Qty in KG"
            gv1.Columns("Challan_Qty").FormatString = "{0:F3}"

            gv1.Columns("FAT_KG").IsVisible = True
            gv1.Columns("FAT_KG").Width = 120
            gv1.Columns("FAT_KG").HeaderText = "Disp FAT(KG)"
            gv1.Columns("FAT_KG").FormatString = "{0:F2}"

            gv1.Columns("SNF_KG").IsVisible = True
            gv1.Columns("SNF_KG").Width = 150
            gv1.Columns("SNF_KG").HeaderText = "Disp SNF(KG)"
            gv1.Columns("SNF_KG").FormatString = "{0:F2}"

            gv1.Columns("FAT_PER").IsVisible = True
            gv1.Columns("FAT_PER").Width = 120
            gv1.Columns("FAT_PER").HeaderText = "Disp FAT%"

            gv1.Columns("SNF_PER").IsVisible = True
            gv1.Columns("SNF_PER").Width = 150
            gv1.Columns("SNF_PER").HeaderText = "Disp SNF%"

            gv1.Columns("Unloading_No").IsVisible = True
            gv1.Columns("Unloading_No").Width = 120
            gv1.Columns("Unloading_No").HeaderText = "Unloading No."

            gv1.Columns("Unloading_Date_Time").IsVisible = True
            gv1.Columns("Unloading_Date_Time").Width = 150
            gv1.Columns("Unloading_Date_Time").HeaderText = "Unloading Date Time"

            gv1.Columns("Net_Weight").IsVisible = True
            gv1.Columns("Net_Weight").Width = 120
            gv1.Columns("Net_Weight").HeaderText = "Received Qty in KG"
            gv1.Columns("Net_Weight").FormatString = "{0:F3}"

            gv1.Columns("Weighment_fat_per").IsVisible = True
            gv1.Columns("Weighment_fat_per").Width = 120
            gv1.Columns("Weighment_fat_per").HeaderText = "Rec FAT %"

            gv1.Columns("Weighment_snf_per").IsVisible = True
            gv1.Columns("Weighment_snf_per").Width = 150
            gv1.Columns("Weighment_snf_per").HeaderText = "Rec SNF %"

            gv1.Columns("Weighment_Fat_kg").IsVisible = True
            gv1.Columns("Weighment_Fat_kg").Width = 120
            gv1.Columns("Weighment_Fat_kg").HeaderText = "Rec FAT(KG)"
            gv1.Columns("Weighment_Fat_kg").FormatString = "{0:F2}"

            gv1.Columns("Weighment_SNF_kg").IsVisible = True
            gv1.Columns("Weighment_SNF_kg").Width = 150
            gv1.Columns("Weighment_SNF_kg").HeaderText = "Rec SNF(KG)"
            gv1.Columns("Weighment_SNF_kg").FormatString = "{0:F2}"

            gv1.Columns("SMP_In_Kg").IsVisible = True
            gv1.Columns("SMP_In_Kg").Width = 120
            gv1.Columns("SMP_In_Kg").HeaderText = "SMP In Kg"
            gv1.Columns("SMP_In_Kg").FormatString = "{0:F3}"


            gv1.Columns("Butter_In_kg").IsVisible = True
            gv1.Columns("Butter_In_kg").Width = 150
            gv1.Columns("Butter_In_kg").HeaderText = "Butter In kg"
            gv1.Columns("Butter_In_kg").FormatString = "{0:F3}"

            Dim item1 As New GridViewSummaryItem("Challan_Qty", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Net_Weight", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Weighment_Fat_kg", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Weighment_SNF_kg", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("SMP_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("Butter_In_kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            gv1.GroupDescriptors.Add(New GridGroupByExpression("Date as Item format ""{0}: {1}"" Group By Date"))

        ElseIf rdSummary.IsChecked Then
            gv1.Columns("Gate_Entry_Date").IsVisible = False
            gv1.Columns("Gate_Entry_Date").Width = 150
            gv1.Columns("Gate_Entry_Date").HeaderText = "Date"

            gv1.Columns("Gate_Entry_Date1").IsVisible = True
            gv1.Columns("Gate_Entry_Date1").Width = 150
            gv1.Columns("Gate_Entry_Date1").HeaderText = "Date"

            gv1.Columns("Weighment_Net_Weight").IsVisible = True
            gv1.Columns("Weighment_Net_Weight").Width = 150
            gv1.Columns("Weighment_Net_Weight").HeaderText = "Qty In KG"
            gv1.Columns("Weighment_Net_Weight").FormatString = "{0:F3}"

            gv1.Columns("Weighment_Fat_kg").IsVisible = True
            gv1.Columns("Weighment_Fat_kg").Width = 150
            gv1.Columns("Weighment_Fat_kg").HeaderText = "FAT(KG)"
            gv1.Columns("Weighment_Fat_kg").FormatString = "{0:F2}"

            gv1.Columns("Weighment_SNF_kg").IsVisible = True
            gv1.Columns("Weighment_SNF_kg").Width = 150
            gv1.Columns("Weighment_SNF_kg").HeaderText = "SNF(KG)"
            gv1.Columns("Weighment_SNF_kg").FormatString = "{0:F2}"

            gv1.Columns("SMP_In_Kg").IsVisible = True
            gv1.Columns("SMP_In_Kg").Width = 150
            gv1.Columns("SMP_In_Kg").HeaderText = "SMP Production In kG"
            gv1.Columns("SMP_In_Kg").FormatString = "{0:F3}"

            gv1.Columns("Butter_In_kg").IsVisible = True
            gv1.Columns("Butter_In_kg").Width = 150
            gv1.Columns("Butter_In_kg").HeaderText = "WB Production In KG"
            gv1.Columns("Butter_In_kg").FormatString = "{0:F3}"

            Dim item1 As New GridViewSummaryItem("Weighment_Net_Weight", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Weighment_Fat_kg", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Weighment_SNF_kg", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("SMP_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Butter_In_kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)

        ElseIf rdStock.IsChecked Then
            gv1.Columns("Gate_Entry_Date").IsVisible = True
            gv1.Columns("Gate_Entry_Date").Width = 150
            gv1.Columns("Gate_Entry_Date").HeaderText = "Date"

            gv1.Columns("Opening_SMP_In_Kg").IsVisible = True
            gv1.Columns("Opening_SMP_In_Kg").Width = 150
            gv1.Columns("Opening_SMP_In_Kg").HeaderText = "Opening Production SMP Bal."
            gv1.Columns("Opening_SMP_In_Kg").FormatString = "{0:F3}"

            gv1.Columns("SMP_In_Kg").IsVisible = True
            gv1.Columns("SMP_In_Kg").Width = 150
            gv1.Columns("SMP_In_Kg").HeaderText = "Production SMP In KG"
            gv1.Columns("SMP_In_Kg").FormatString = "{0:F3}"

            gv1.Columns("SMP_Dispatch").IsVisible = True
            gv1.Columns("SMP_Dispatch").Width = 150
            gv1.Columns("SMP_Dispatch").HeaderText = "Dispatch SMP In KG"
            gv1.Columns("SMP_Dispatch").FormatString = "{0:F3}"

            gv1.Columns("Closing_SMP_In_Kg").IsVisible = True
            gv1.Columns("Closing_SMP_In_Kg").Width = 150
            gv1.Columns("Closing_SMP_In_Kg").HeaderText = "Closing Production SMP Bal."
            gv1.Columns("Closing_SMP_In_Kg").FormatString = "{0:F3}"

            gv1.Columns("Opening_Butter_In_Kg").IsVisible = True
            gv1.Columns("Opening_Butter_In_Kg").Width = 150
            gv1.Columns("Opening_Butter_In_Kg").HeaderText = "Opening production Butter Bal."
            gv1.Columns("Opening_Butter_In_Kg").FormatString = "{0:F3}"

            gv1.Columns("Butter_In_kg").IsVisible = True
            gv1.Columns("Butter_In_kg").Width = 150
            gv1.Columns("Butter_In_kg").HeaderText = "Production Butter In KG"
            gv1.Columns("Butter_In_kg").FormatString = "{0:F3}"

            gv1.Columns("Butter_Dispatch").IsVisible = True
            gv1.Columns("Butter_Dispatch").Width = 150
            gv1.Columns("Butter_Dispatch").HeaderText = "Dispatch Butter In KG"
            gv1.Columns("Butter_Dispatch").FormatString = "{0:F3}"

            gv1.Columns("Closing_Butter_In_kg").IsVisible = True
            gv1.Columns("Closing_Butter_In_kg").Width = 150
            gv1.Columns("Closing_Butter_In_kg").HeaderText = "Closing Production Butter In KG"
            gv1.Columns("Closing_Butter_In_kg").FormatString = "{0:F3}"
           

            Dim item1 As New GridViewSummaryItem("Opening_SMP_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("SMP_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("SMP_Dispatch", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Closing_SMP_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Opening_Butter_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Butter_In_kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Butter_Dispatch", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("Closing_Butter_In_kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

        End If

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True


        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "   " + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)


             If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
            End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            If rdDetail.IsChecked Then
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Receipt Of Milk Tanker's For M.D. Conversion", gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("Receipt Of Milk Tanker's For M.D. Conversion", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            ElseIf rdSummary.IsChecked Then
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("MDFVPL Milk Receipt and Converssion at UDL", gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("MDFVPL Milk Receipt and Converssion at UDL", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            ElseIf rdStock.IsChecked Then
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("MD Stock Report", gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("MD Stock Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
                

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Function GetReportID() As String
        Dim ReportID As String = "MD_CONV_"
        If rdSummary.IsChecked Then
            ReportID += "SUM"
        ElseIf rdDetail.IsChecked Then
            ReportID += "DET"
        ElseIf rdStock.IsChecked Then
            ReportID += "STK"
       
        End If
        Return ReportID
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv1
        Load_Report()
    End Sub


    Private Sub RptMDConversionAtUDL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        Reset()
        
    End Sub


    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select Location_Code as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "Code", "Name", txtMCC.arrValueMember, Nothing)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Dim ReportID As String = GetReportID()
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class

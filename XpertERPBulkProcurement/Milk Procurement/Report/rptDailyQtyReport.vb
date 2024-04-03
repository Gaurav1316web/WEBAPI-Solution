Imports System.IO
Imports common

Public Class rptDailyQtyReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim FatSnfRoundOff As Integer
    Dim corrFactor As Decimal = 0
    Dim AreaWiseBilling As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        'LoadShiftName()
        txtMCC.arrValueMember = Nothing
        TxtTankerNo.Value = ""
        fndArea.Value = ""
        ControlEnableDisable(True)
        LoadShif()
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        txtMCC.Enabled = isEnable
        fromDate.Enabled = isEnable
        ToDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
        cboItemType.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        txtMCC_Code.Enabled = isEnable
        fndArea.Enabled = isEnable
        lblArea.Enabled = isEnable
        lblToleranceFAT.Enabled = isEnable
        lblToleranceSNF.Enabled = isEnable
        txtToleranceFat.Enabled = isEnable
        txtToleranceSNF.Enabled = isEnable
        rbtnBMCDock.Enabled = isEnable
        rbtnBMCDock.Checked = False
        rbtnDockSummary.Checked = False
        rbtnDockSummary.Enabled = isEnable
        rbtnSummary.Checked = False
        rbtnSummary.Enabled = isEnable
        txtMCC_Code.Value = Nothing
        ddlShift.Visible = False
        MyLabel6.Visible = False
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        FatSnfRoundOff = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DailyQtyReport, clsFixedParameterCode.FATKGSNFKGRoundOff, Nothing))
        corrFactor = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))

        If FatSnfRoundOff = 0 Then
            Try

                If rbtnTranpoterGainLoss.Checked Then
                    Dim checkRate As String
                    Dim dt1 As New DataTable
                    checkRate = "(SELECT top 1  TSPL_OWN_BMC_GAIN_LOSS_RATE.Code FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 and TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 and 
                                 CONVERT(date, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "', 103))"
                    dt1 = clsDBFuncationality.GetDataTable(checkRate) 'GetDataTable(qry)
                    If dt1.Rows.Count <= 0 Then
                        Throw New Exception("FAT/SNF RATE NOT FOUND!")
                    End If
                End If
                If rdbSummary.Checked = True Then
                    PageSetupReport_ID = MyBase.Form_ID + "_S"
                End If
                PageSetupReport_ID = MyBase.Form_ID
                TemplateGridview = Gv1
                Dim BaseQry As String = ""
                Dim qry As String = ""
                Dim DockDateQry As String = ""
                Dim dt As New DataTable
                Dim whre As String = ""

                'qry = " select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No , TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo ,TSPL_MILK_COLLECTION_MCC.Route_Code  ,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME ,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No
                '       ,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty , ROUND(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,1,0) as MCC_FAT,round (TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,1,0) as MCC_SNF,round (TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG,0,0) as MCC_FATKG , round (TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG,0,0) as MCC_SNFKG,
                '       isnull(XXXDCS.qty,0) as DCS_Qty,isnull(XXXDCS.FAT,0) as DCS_FAT , isnull(XXXDCS.SNF,0) as DCS_SNF, isnull(XXXDCS.FATKG,0) as DCS_FATKG, isnull(XXXDCS.SNFKG,0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty,0) as Diff_Qty, ROUND(TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,1,0) - isnull(XXXDCS.FAT,0)  as Diff_FAT, round (TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,1,0) - isnull(XXXDCS.SNF,0)  as Diff_SNF, round (TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG,0,0) - isnull(XXXDCS.FATKG,0)  as Diff_FATKG, round (TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG,0,0) - isnull(XXXDCS.SNFKG,0)  as Diff_SNFKG
                '       from TSPL_MILK_COLLECTION_MCC_DETAIL
                '       left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                '       left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
                '       left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.Route_Code
                '       left outer join (  
                '       select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty ) as qty  , round( sum (TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG), 0,0) as FATKG ,  round( sum (TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG), 0,0) as SNFKG ,
                '       ROUND(((sum (TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG) / sum (TSPL_MILK_COLLECTION_DCS_DETAIL.qty )) * 100),1,0) as FAT , ROUND(((sum (TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG) / sum (TSPL_MILK_COLLECTION_DCS_DETAIL.qty )) * 100),1,0) as SNF 
                '       from TSPL_MILK_COLLECTION_DCS_DETAIL 
                '       left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                '       left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                '       where convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) = convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and TSPL_MILK_COLLECTION_DCS.Status = 1
                '       group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                '       where convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) = convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and TSPL_MILK_COLLECTION_MCC.Status = 1"
                Dim strMilkCollectionMCCStatus As String = ""
                Dim strMilkcollectionDCSStatus As String = ""
                If rbtnDock.Checked = False AndAlso rbtnDockDateWise.Checked = False AndAlso rbtnDockShiftWise.Checked = False Then
                    If clsCommon.CompairString(cboItemType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                        strMilkCollectionMCCStatus = " and TSPL_MILK_COLLECTION_MCC.Status = 1 "
                        strMilkcollectionDCSStatus = " and TSPL_MILK_COLLECTION_DCS.Status = 1 "
                    ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                        strMilkCollectionMCCStatus = " and TSPL_MILK_COLLECTION_MCC.Status = 0 "
                        strMilkcollectionDCSStatus = " and TSPL_MILK_COLLECTION_DCS.Status = 0 "
                    End If
                End If
                'qry = "  select max(Entered_Qty) as Entered_Qty, max(Entered_FATKg) as Entered_FATKg , max(Entered_SNFKg) as Entered_SNFKg , max(PK_Id) as PK_Id, Document_No ,  max(Document_Date) as Document_Date ,max(MCC_Code) as MCC_Code , max(MCC_NAME) as MCC_NAME, max(UploaderNo) as  UploaderNo,max(Route_Code) as Route_Code, max(ROUTE_NAME) as ROUTE_NAME ,max(Tanker_No) as Tanker_No ,max(Vehicle_No) as Vehicle_No

                '            , sum(MCC_Qty) as MCC_Qty , ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as MCC_FAT ,ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as MCC_SNF ,sum(MCC_FATKG) as MCC_FATKG , sum(MCC_SNFKG) as MCC_SNFKG,

                '            max(DCS_Qty) as DCS_Qty, max(DCS_FAT) as DCS_FAT , max(DCS_SNF) as DCS_SNF,  max(DCS_FATKG) as DCS_FATKG ,max(DCS_SNFKG) as DCS_SNFKG ,

                'max(DCS_Qty) - sum(MCC_Qty)  as Diff_Qty,

                'max(DCS_FAT) - ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as Diff_FAT 

                ',max(DCS_SNF) - ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as Diff_SNF

                ', max(DCS_FATKG) - sum(MCC_FATKG)  as  Diff_FATKG, 

                '    max(DCS_SNFKG) - sum(MCC_SNFKG) as Diff_SNFKG  
                '            from   (select TSPL_MILK_COLLECTION_MCC.Entered_Qty, TSPL_MILK_COLLECTION_MCC.Entered_FATKg , TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No ,convert (varchar,TSPL_MILK_COLLECTION_MCC.Document_Date,103) as  Document_Date ,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo ,TSPL_MILK_COLLECTION_MCC.Route_Code  ,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME ,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No
                '            ,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty , TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCC_FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCC_SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCC_FATKG , TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCC_SNFKG,
                '            isnull(XXXDCS.qty,0) as DCS_Qty,isnull(XXXDCS.FAT,0) as DCS_FAT , isnull(XXXDCS.SNF,0) as DCS_SNF, isnull(XXXDCS.FATKG,0) as DCS_FATKG, isnull(XXXDCS.SNFKG,0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty,0) as Diff_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT - isnull(XXXDCS.FAT,0)  as Diff_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF - isnull(XXXDCS.SNF,0)  as Diff_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG - isnull(XXXDCS.FATKG,0)  as Diff_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG - isnull(XXXDCS.SNFKG,0)  as Diff_SNFKG
                '            from TSPL_MILK_COLLECTION_MCC_DETAIL
                '            left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                '            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
                '            left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.Route_Code
                '            left outer join (  
                '            select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty ) as qty  ,  sum (TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG) as FATKG ,   sum (TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG) as SNFKG ,
                '            ((sum (TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG) / sum (TSPL_MILK_COLLECTION_DCS_DETAIL.qty )) * 100) as FAT , ((sum (TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG) / sum (TSPL_MILK_COLLECTION_DCS_DETAIL.qty )) * 100) as SNF 
                'from TSPL_MILK_COLLECTION_DCS_DETAIL 
                '            left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                '            left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                '            where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) " + strMilkcollectionDCSStatus + "
                '            group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                '            where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) xyz group by Document_No  " + strMilkCollectionMCCStatus + " "


                'qry = "select max(Entered_Qty) as Entered_Qty, max(Entered_FATKg) as Entered_FATKg, max(Entered_SNFKg) as Entered_SNFKg, max(PK_Id) as PK_Id, Document_No, max(Document_Date) as Document_Date, max(MCC_Code) as MCC_Code, max(MCC_NAME) as MCC_NAME, max(UploaderNo) as UploaderNo, max(Route_Code) as Route_Code, max(ROUTE_NAME) as ROUTE_NAME, max(Tanker_No) as Tanker_No, max(Vehicle_No) as Vehicle_No, sum(MCC_Qty) as MCC_Qty, ROUND( ( ( sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 1, 0 ) as MCC_FAT, ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 1, 0 ) as MCC_SNF, sum(MCC_FATKG) as MCC_FATKG, sum(MCC_SNFKG) as MCC_SNFKG, sum(DCS_Qty) as DCS_Qty, (sum(DCS_FATKG)/sum(DCS_Qty)*100) as DCS_FAT, (sum(DCS_SNFKG)/sum(DCS_Qty)*100) as DCS_SNF, sum(DCS_FATKG) as DCS_FATKG, sum(DCS_SNFKG) as DCS_SNFKG, sum(DCS_Qty) - sum(MCC_Qty) as Diff_Qty, (sum(DCS_FATKG)/sum(DCS_Qty)*100)- ROUND( ( ( sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 1, 0 ) as Diff_FAT, (sum(DCS_SNFKG)/sum(DCS_Qty)*100)-ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 1, 0 ) as Diff_SNF, sum(DCS_FATKG) - sum(MCC_FATKG) as Diff_FATKG, sum(DCS_SNFKG) - sum(MCC_SNFKG) as Diff_SNFKG from ( select TSPL_MILK_COLLECTION_MCC.Entered_Qty, TSPL_MILK_COLLECTION_MCC.Entered_FATKg, TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id, TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No, convert ( varchar, TSPL_MILK_COLLECTION_MCC.Document_Date, 103 ) as Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo, TSPL_MILK_COLLECTION_MCC.Route_Code, TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_MILK_COLLECTION_MCC.Tanker_No, TSPL_MILK_COLLECTION_MCC.Vehicle_No, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCC_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCC_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCC_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCC_SNFKG, isnull(XXXDCS.qty, 0) as DCS_Qty, isnull(XXXDCS.FAT, 0) as DCS_FAT, isnull(XXXDCS.SNF, 0) as DCS_SNF, isnull(XXXDCS.FATKG, 0) as DCS_FATKG, isnull(XXXDCS.SNFKG, 0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty, 0) as Diff_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT - isnull(XXXDCS.FAT, 0) as Diff_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF - isnull(XXXDCS.SNF, 0) as Diff_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG - isnull(XXXDCS.FATKG, 0) as Diff_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG - isnull(XXXDCS.SNFKG, 0) as Diff_SNFKG from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code left outer join ( select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty) as qty, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) as FATKG, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) as SNFKG, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) / sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ) ) * 100 ) as FAT, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) / sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ) ) * 100 ) as SNF
                '                       from TSPL_MILK_COLLECTION_DCS_DETAIL 
                '                        left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                '            left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                '            where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) " + strMilkcollectionDCSStatus + "
                '            group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                '            where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) xyz group by Document_No  " + strMilkCollectionMCCStatus + " "
                If rbtnBmcSummary.Checked = True Then
                    qry = "select max(Entered_Qty) as Entered_Qty, max(Entered_FATKg) as Entered_FATKg, max(Entered_SNFKg) as Entered_SNFKg, max(PK_Id) as PK_Id,  max(Document_No)Document_No, max(Document_Date) as Document_Date, max(MCC_Code) as MCC_Code, max(MCC_NAME) as MCC_NAME, max(UploaderNo) as UploaderNo, max(Route_Code) as Route_Code, max(ROUTE_NAME) as ROUTE_NAME, max(Tanker_No) as Tanker_No,max(Vehicle_No) as Vehicle_No, sum(MCC_Qty) as MCC_Qty, ROUND(((sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_FAT, ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_SNF, sum(MCC_FATKG) as MCC_FATKG, sum(MCC_SNFKG) as MCC_SNFKG, max(DCS_Qty) as DCS_Qty, isnull( (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_FAT, isnull( (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_SNF, isnull(max(DCS_FATKG),0) as DCS_FATKG,isnull( max(DCS_SNFKG),0) as DCS_SNFKG, max(DCS_Qty) - sum(MCC_Qty) as Diff_Qty, (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100)- ROUND( ( ( sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_FAT, (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100)-ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_SNF, sum(MCC_FATKG) -max(DCS_FATKG) as Diff_FATKG, sum(MCC_SNFKG)-max(DCS_SNFKG) as Diff_SNFKG from ( select TSPL_MILK_COLLECTION_MCC.Entered_Qty, TSPL_MILK_COLLECTION_MCC.Entered_FATKg, TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id, TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No, convert ( varchar, TSPL_MILK_COLLECTION_MCC.Document_Date, 103 ) as Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo, TSPL_MILK_COLLECTION_MCC.Route_Code, TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCC_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCC_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCC_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCC_SNFKG, isnull(XXXDCS.qty, 0) as DCS_Qty, isnull(XXXDCS.FAT, 0) as DCS_FAT, isnull(XXXDCS.SNF, 0) as DCS_SNF, isnull(XXXDCS.FATKG, 0) as DCS_FATKG, isnull(XXXDCS.SNFKG, 0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty, 0) as Diff_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT - isnull(XXXDCS.FAT, 0) as Diff_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF - isnull(XXXDCS.SNF, 0) as Diff_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG - isnull(XXXDCS.FATKG, 0) as Diff_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG - isnull(XXXDCS.SNFKG, 0) as Diff_SNFKG from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code left outer join ( select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty) as qty, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) as FATKG, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) as SNFKG, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) /nullif( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as FAT, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) / nullif(sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as SNF
                        from TSPL_MILK_COLLECTION_DCS_DETAIL 
                        left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                        left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) " + strMilkcollectionDCSStatus + "
                        group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                        where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) xyz group by UploaderNo  " + strMilkCollectionMCCStatus + " "
                Else
                    qry = "select max(Entered_Qty) as Entered_Qty, max(Entered_FATKg) as Entered_FATKg, max(Entered_SNFKg) as Entered_SNFKg, max(PK_Id) as PK_Id, Document_No, max(Document_Date) as Document_Date, max(MCC_Code) as MCC_Code, max(MCC_NAME) as MCC_NAME, max(UploaderNo) as UploaderNo, max(Route_Code) as Route_Code, max(ROUTE_NAME) as ROUTE_NAME, max(Tanker_No) as Tanker_No,max(Vehicle_No) as Vehicle_No, sum(MCC_Qty) as MCC_Qty, ROUND(((sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_FAT, ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_SNF, sum(MCC_FATKG) as MCC_FATKG, sum(MCC_SNFKG) as MCC_SNFKG, max(DCS_Qty) as DCS_Qty, isnull( (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_FAT, isnull( (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_SNF, isnull(max(DCS_FATKG),0) as DCS_FATKG,isnull( max(DCS_SNFKG),0) as DCS_SNFKG, max(DCS_Qty) - sum(MCC_Qty) as Diff_Qty, (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100)- ROUND( ( ( sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_FAT, (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100)-ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_SNF, sum(MCC_FATKG) -max(DCS_FATKG) as Diff_FATKG, sum(MCC_SNFKG)-max(DCS_SNFKG) as Diff_SNFKG from ( select TSPL_MILK_COLLECTION_MCC.Entered_Qty, TSPL_MILK_COLLECTION_MCC.Entered_FATKg, TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id, TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No, convert ( varchar, TSPL_MILK_COLLECTION_MCC.Document_Date, 103 ) as Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo, TSPL_MILK_COLLECTION_MCC.Route_Code, TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCC_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCC_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCC_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCC_SNFKG, isnull(XXXDCS.qty, 0) as DCS_Qty, isnull(XXXDCS.FAT, 0) as DCS_FAT, isnull(XXXDCS.SNF, 0) as DCS_SNF, isnull(XXXDCS.FATKG, 0) as DCS_FATKG, isnull(XXXDCS.SNFKG, 0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty, 0) as Diff_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT - isnull(XXXDCS.FAT, 0) as Diff_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF - isnull(XXXDCS.SNF, 0) as Diff_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG - isnull(XXXDCS.FATKG, 0) as Diff_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG - isnull(XXXDCS.SNFKG, 0) as Diff_SNFKG from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code left outer join ( select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty) as qty, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) as FATKG, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) as SNFKG, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) /nullif( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as FAT, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) / nullif(sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as SNF
                        from TSPL_MILK_COLLECTION_DCS_DETAIL 
                        left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                        left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) " + strMilkcollectionDCSStatus + "
                        group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                        where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) xyz group by UploaderNo, Document_No  " + strMilkCollectionMCCStatus + " "
                End If

                If rdbSummary.Checked = True Then
                    qry = " select XXXFinal.Document_No, max(XXXFinal.Document_Date) as Document_Date ,max( XXXFinal.Route_Code) as Route_Code, max(XXXFinal.ROUTE_NAME) as ROUTE_NAME ,
                            max(XXXFinal.Tanker_No ) as Tanker_No, max(XXXFinal.Vehicle_No) as Vehicle_No, max(Entered_Qty) as Entered_Qty,((max (Entered_FATKg) / nullif (max (Entered_Qty),0)) * 100) as Entered_FAT, ((max (Entered_SNFKg) / nullif (max (Entered_Qty),0)) * 100) as Entered_SNF , max(Entered_FATKg) as Entered_FATKg,max(Entered_SNFKg) as Entered_SNFKg , 
                            sum( XXXFinal.MCC_Qty) as  MCC_Qty, ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as MCC_FAT, ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as MCC_SNF,sum (MCC_FATKG) as MCC_FATKG , sum (MCC_SNFKG) as MCC_SNFKG , sum (DCS_Qty) as DCS_Qty , ROUND(((sum (DCS_FATKG) / nullif( sum (DCS_Qty),0)) * 100),1,0) as DCS_FAT,ROUND(((sum (DCS_SNFKG) / nullif (sum (DCS_Qty),0)) * 100),1,0) as DCS_SNF , sum(DCS_FATKG) as DCS_FATKG , sum(DCS_SNFKG) as DCS_SNFKG, sum (Diff_Qty) as Diff_Qty ,
                            max(DCS_FAT) - ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as Diff_FAT, max(DCS_SNF) - ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as Diff_SNF,sum(Diff_FATKG) as Diff_FATKG, sum(Diff_SNFKG) as Diff_SNFKG,  max(Entered_Qty) - sum( XXXFinal.MCC_Qty)  as DiffEnteredVsMCC_Qty ,
                            ((max (Entered_FATKg) / nullif (max (Entered_Qty),0)) * 100) - ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0)  as DiffEnteredVsMCC_FAT ,
                            ((max (Entered_SNFKg) / nullif (max (Entered_Qty),0)) * 100) - ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as DiffEnteredVsMCC_SNF ,
                            max(Entered_FATKg) -  sum (MCC_FATKG)  as DiffEnteredVsMCC_FATKG , max(Entered_SNFKg) - sum (MCC_SNFKG) as DiffEnteredVsMCC_SNFKG from ( " + qry + " ) XXXFinal group by XXXFinal.Document_No order by convert (datetime, max(XXXFinal.Document_Date),103) asc "
                ElseIf rdbDetails.Checked = True Then


                    '                qry = "select XXXFinal.PK_Id as PK_Id, XXXFinal.Document_No as Document_No, XXXFinal.Document_Date As Document_Date, XXXFinal.MCC_Code As MCC_Code, XXXFinal.MCC_NAME As MCC_NAME, XXXFinal.UploaderNo As UploaderNo, XXXFinal.Route_Code As Route_Code, XXXFinal.ROUTE_NAME As ROUTE_NAME, XXXFinal.Tanker_No As Tanker_No, XXXFinal.Vehicle_No As Vehicle_No, Entered_Qty As Entered_Qty, ((Entered_FATKg / nullif(Entered_Qty, 0)) * 100) As Entered_FAT, ((Entered_SNFKg / nullif(Entered_Qty, 0)) * 100) As Entered_SNF, Entered_FATKg As Entered_FATKg, Entered_SNFKg As Entered_SNFKg, XXXFinal.MCC_Qty As MCC_Qty, ROUND(((MCC_FATKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As MCC_FAT, ROUND(((MCC_SNFKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As MCC_SNF, MCC_FATKG As MCC_FATKG, MCC_SNFKG As MCC_SNFKG, DCS_Qty As DCS_Qty, ROUND(((DCS_FATKG / nullif(DCS_Qty, 0)) * 100), 1, 0) As DCS_FAT, ROUND(((DCS_SNFKG / nullif(DCS_Qty, 0)) * 100), 1, 0) As DCS_SNF, DCS_FATKG As DCS_FATKG, DCS_SNFKG As DCS_SNFKG, Diff_Qty As Diff_Qty, DCS_FAT - ROUND(((MCC_FATKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_FAT, DCS_SNF - ROUND(((MCC_SNFKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_SNF, Diff_FATKG As Diff_FATKG, Diff_SNFKG As Diff_SNFKG, Entered_Qty - XXXFinal.MCC_Qty As DiffEnteredVsMCC_Qty, ((Entered_FATKg / nullif(Entered_Qty, 0)) * 100) - ROUND(((MCC_FATKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As DiffEnteredVsMCC_FAT, ((Entered_SNFKg / nullif(Entered_Qty, 0)) * 100) - ROUND(((MCC_SNFKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As DiffEnteredVsMCC_SNF, Entered_FATKg - MCC_FATKG As DiffEnteredVsMCC_FATKG, Entered_SNFKg - MCC_SNFKG as DiffEnteredVsMCC_SNFKG from( " + qry + " ) XXXFinal  
                    'left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=XXXFinal.Document_No"
                    qry = "select XXXFinal.PK_Id as PK_Id, XXXFinal.Document_No as Document_No, XXXFinal.Document_Date As Document_Date, XXXFinal.MCC_Code As MCC_Code, XXXFinal.MCC_NAME As MCC_NAME, XXXFinal.UploaderNo As UploaderNo, XXXFinal.Route_Code As Route_Code, XXXFinal.ROUTE_NAME As ROUTE_NAME, XXXFinal.Tanker_No As Tanker_No, XXXFinal.Vehicle_No As Vehicle_No, XXXFinal.MCC_Qty As MCC_Qty, Convert(Decimal(18,2),(MCC_FATKG / nullif(MCC_Qty, 0)) * 100) As MCC_FAT, convert(decimal(18,2),(MCC_SNFKG / nullif(MCC_Qty, 0)) * 100) As MCC_SNF, MCC_FATKG As MCC_FATKG, MCC_SNFKG As MCC_SNFKG, DCS_Qty As DCS_Qty,isnull( ROUND(((DCS_FATKG / nullif(DCS_Qty, 0)) * 100), 1, 0),0) As DCS_FAT, isnull(ROUND(((DCS_SNFKG / nullif(DCS_Qty, 0)) * 100), 1, 0),0) As DCS_SNF, isnull(DCS_FATKG,0) As DCS_FATKG, isnull(DCS_SNFKG,0) As DCS_SNFKG, Diff_Qty As Diff_Qty, DCS_FAT - ROUND(((MCC_FATKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_FAT, DCS_SNF - ROUND(((MCC_SNFKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_SNF, Diff_FATKG As Diff_FATKG, Diff_SNFKG As Diff_SNFKG from( " + qry + " ) XXXFinal  
                           left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=XXXFinal.Document_No"
                ElseIf rbtnBmcSummary.Checked = True Then
                    qry = "select XXXFinal.PK_Id as PK_Id, XXXFinal.Document_No as Document_No, XXXFinal.Document_Date As Document_Date, XXXFinal.MCC_Code As MCC_Code, XXXFinal.MCC_NAME As MCC_NAME, XXXFinal.UploaderNo As UploaderNo, XXXFinal.Route_Code As Route_Code, XXXFinal.ROUTE_NAME As ROUTE_NAME, XXXFinal.Tanker_No As Tanker_No, XXXFinal.Vehicle_No As Vehicle_No, XXXFinal.MCC_Qty As MCC_Qty, Convert(Decimal(18,2),(MCC_FATKG / nullif(MCC_Qty, 0)) * 100) As MCC_FAT, convert(decimal(18,2),(MCC_SNFKG / nullif(MCC_Qty, 0)) * 100) As MCC_SNF, MCC_FATKG As MCC_FATKG, MCC_SNFKG As MCC_SNFKG, DCS_Qty As DCS_Qty,isnull( ROUND(((DCS_FATKG / nullif(DCS_Qty, 0)) * 100), 1, 0),0) As DCS_FAT, isnull(ROUND(((DCS_SNFKG / nullif(DCS_Qty, 0)) * 100), 1, 0),0) As DCS_SNF, isnull(DCS_FATKG,0) As DCS_FATKG, isnull(DCS_SNFKG,0) As DCS_SNFKG, Diff_Qty As Diff_Qty, DCS_FAT - ROUND(((MCC_FATKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_FAT, DCS_SNF - ROUND(((MCC_SNFKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_SNF, Diff_FATKG As Diff_FATKG, Diff_SNFKG As Diff_SNFKG from( " + qry + " ) XXXFinal  
                            left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=XXXFinal.Document_No"

                    'qry = qry + " order by TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No asc "
                ElseIf rbtnDock.Checked = True OrElse rbtnDockDateWise.Checked OrElse rbtnDockShiftWise.Checked Then
                    qry = ""
                    Dim strFilterQry As String = ""
                    Dim strQryStatus1 As String = ""
                    Dim strQryStatus2 As String = ""
                    Dim Area As String = ""
                    Dim AreaName As String = Nothing
                    AreaName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from TSPL_MCC_MASTER WHERE Area_Location_Code = '" + fndArea.Value + "' "))


                    If txtMCC_Code.Value.Length > 0 Then
                        strFilterQry = " and TSPL_MCC_MASTER.MCC_Code in ('" + txtMCC_Code.Value + "') "
                    End If

                    If clsCommon.myLen(fndArea.Value) > 0 Then
                        Area += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    End If

                    If clsCommon.CompairString(cboItemType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                        strQryStatus1 = "where TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1"
                        strQryStatus2 = "where TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1"
                    ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                        strQryStatus1 = "where TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=0"
                        strQryStatus2 = "where TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=0"
                    End If


                    qry = "Select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SNo,CONVERT(varchar(10), tmp.Shift_Date, 103) As SDate ,tmp.Shift As Shift,No_Of_Cans As Qty,Milk_Weight As Milk_Wtd,FAT as FAT,SNF as SNF,convert(decimal(18,2),((Milk_Weight*FAT)/100)) As Fat_KG,convert(decimal(18,2),((Milk_Weight*SNF)/100)) As SNF_KG,'' As Rate,'' As Amt,tmp.VLC_Code, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As DCS_Uploader_Code,TSPL_VLC_MASTER_HEAD.VLC_Code As DCS_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  As DCS_Name
                        ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader  As BMC_Uploader_Code,TSPL_MCC_MASTER.MCC_Code As BMC_Code,TSPL_MCC_MASTER.MCC_NAME As BMC_Name ,'" + AreaName + "' as Area, TSPL_VLC_MASTER_HEAD.Route_Code
                        from(
                        Select Shift_Date,Shift,No_Of_Cans,Milk_Weight,FAT,SNF,VLC_Code  from TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        Left Outer Join TSPL_MILK_SHIFT_UPLOADER_HEAD ON TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
	                    " + strQryStatus1 + "
                        Union All
                        Select Shift_Date,Shift,No_Of_Cans,Milk_Weight,FAT,SNF,VLC_Code from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
	                    Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_HEAD ON TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
	                    " + strQryStatus2 + ")tmp
                        Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code=tmp.VLC_Code
                        Left Outer Join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        where convert (date,Shift_Date,103) >= convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,Shift_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)" + strFilterQry + Area

                ElseIf (rbtnTranpoterGainLoss.Checked = True) Then

                    If txtToleranceFat.Value < 0 Or txtToleranceFat.Value >= 100 Then

                        clsCommon.MyMessageBoxShow(Me, "Invalid Input !, Please enter valid Tolerance Fat%.", Me.Text)
                        txtToleranceFat.Focus()
                        Exit Sub
                    End If

                    If txtToleranceSNF.Value < 0 Or txtToleranceSNF.Value >= 100 Then
                        clsCommon.MyMessageBoxShow(Me, "Invalid Input !, Please enter valid Tolerance Fat%.", Me.Text)
                        txtToleranceSNF.Focus()
                        Exit Sub
                    End If

                    qry = "select  XXGetAllRecords.Document_No,XXGetAllRecords.Document_Date,XXGetAllRecords.Route_Code,XXGetAllRecords.ROUTE_NAME,XXGetAllRecords.Tanker_No,XXGetAllRecords.Vehicle_No,
                        XXGetAllRecords.Entered_Qty,XXGetAllRecords.Entered_FATKg,XXGetAllRecords.Entered_SNFKg,
                        XXGetAllRecords.MCC_Qty,XXGetAllRecords.MCC_FATKG,XXGetAllRecords.MCC_SNFKG,
                        XXGetAllRecords.DiffMCCVsEntered_Qty,
                        XXGetAllRecords.DiffMCCVsEntered_FATKG," + clsCommon.myCstr(txtToleranceFat.Value) + " as FAT_Tolerence,case when (isnull(XXGetAllRecords.DiffMCCVsEntered_FATKG - " + clsCommon.myCstr(txtToleranceFat.Value) + ",0))<0 then 0 else isnull(XXGetAllRecords.DiffMCCVsEntered_FATKG - " + clsCommon.myCstr(txtToleranceFat.Value) + ",0) end as FATKG_Recovered,Round(XXGetAllRecords.FAT_AMT,2,0) as FAT_AMT,
                        XXGetAllRecords.DiffMCCVsEntered_SNFKG," + clsCommon.myCstr(txtToleranceSNF.Value) + " as SNF_Tolerence,case when (isnull(XXGetAllRecords.DiffMCCVsEntered_SNFKG - " + clsCommon.myCstr(txtToleranceSNF.Value) + ",0))<0 then 0 else isnull(XXGetAllRecords.DiffMCCVsEntered_SNFKG - " + clsCommon.myCstr(txtToleranceSNF.Value) + ",0) end as SNFKG_Recovered,Round(XXGetAllRecords.SNF_AMT,2,0) as SNF_AMT,Round((XXGetAllRecords.FAT_AMT + XXGetAllRecords.SNF_AMT),2,0) as AMOUNT,XXGetAllRecords.GainLossCode as GainLoss_Code,
                           XXGetAllRecords.Loss_FAT_Rate as Loss_FAT_Rate,
                           XXGetAllRecords.Loss_SNF_Rate as Loss_SNF_Rate,
                           XXGetAllRecords.Start_Date as Start_Date
                         from( select GetAllGainLossRate.*,
                         case when GetAllGainLossRate.DiffMCCVsEntered_FATKG >= 0 then (
                         case when (GetAllGainLossRate.DiffMCCVsEntered_FATKG - " + clsCommon.myCstr(txtToleranceFat.Value) + ") > 0 then ((GetAllGainLossRate.DiffMCCVsEntered_FATKG -" + clsCommon.myCstr(txtToleranceFat.Value) + ") *TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate) else 0 end)
                         else 0 end as FAT_AMT,case when GetAllGainLossRate.DiffMCCVsEntered_SNFKG > 0 then (case when (GetAllGainLossRate.DiffMCCVsEntered_SNFKG - " + clsCommon.myCstr(txtToleranceSNF.Value) + ") > 0 then ((GetAllGainLossRate.DiffMCCVsEntered_SNFKG -" + clsCommon.myCstr(txtToleranceSNF.Value) + ") *TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate) else 0 end) else 0 end as SNF_AMT,
                         TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date from ( select GetRateCode.*,( SELECT top 1 TSPL_OWN_BMC_GAIN_LOSS_RATE.Code FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 and TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 and CONVERT(date, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date, 103) <= CONVERT(date, GetRateCode.Document_Date, 103) order by TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date desc ) as GainLossCode from ( SELECT XXXFinal.Document_No, MAX(XXXFinal.Document_Date) AS Document_Date, MAX(XXXFinal.Route_Code) AS Route_Code, MAX(XXXFinal.ROUTE_NAME) AS ROUTE_NAME, MAX(XXXFinal.Tanker_No) AS Tanker_No, MAX(XXXFinal.Vehicle_No) AS Vehicle_No, MAX(Entered_Qty) AS Entered_Qty, ((MAX(Entered_FATKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS Entered_FAT, ((MAX(Entered_SNFKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS Entered_SNF, MAX(Entered_FATKg) AS Entered_FATKg, MAX(Entered_SNFKg) AS Entered_SNFKg, SUM(XXXFinal.MCC_Qty) AS MCC_Qty, ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS MCC_FAT, ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS MCC_SNF, SUM(MCC_FATKG) AS MCC_FATKG, SUM(MCC_SNFKG) AS MCC_SNFKG, SUM(DCS_Qty) AS DCS_Qty, ROUND(((SUM(DCS_FATKG) / NULLIF(SUM(DCS_Qty), 0)) * 100), 3, 0) AS DCS_FAT, ROUND(((SUM(DCS_SNFKG) / NULLIF(SUM(DCS_Qty), 0)) * 100), 3, 0) AS DCS_SNF, SUM(DCS_FATKG) AS DCS_FATKG, SUM(DCS_SNFKG) AS DCS_SNFKG, SUM(Diff_Qty) AS Diff_Qty, MAX(DCS_FAT) - ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS Diff_FAT, MAX(DCS_SNF) - ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS Diff_SNF, SUM(Diff_FATKG) AS Diff_FATKG, SUM(Diff_SNFKG) AS Diff_SNFKG, SUM(XXXFinal.MCC_Qty)- MAX(Entered_Qty) AS DiffMCCVsEntered_Qty, ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0)- ((MAX(Entered_FATKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS DiffMCCVsEntered_FAT, ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0)- ((MAX(Entered_SNFKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS DiffMCCVsEntered_SNF, SUM(MCC_FATKG)-MAX(Entered_FATKg) AS DiffMCCVsEntered_FATKG, SUM(MCC_SNFKG)-MAX(Entered_SNFKg) AS DiffMCCVsEntered_SNFKG from ( " + qry + " ) XXXFinal group by XXXFinal.Document_No )GetRateCode )GetAllGainLossRate left outer join TSPL_OWN_BMC_GAIN_LOSS_RATE on TSPL_OWN_BMC_GAIN_LOSS_RATE.Code=GetAllGainLossRate.GainLossCode ) XXGetAllRecords"
                    If clsCommon.myLen(TxtTankerNo.Value) > 0 Then
                        qry = qry + " where tanker_no ='" + TxtTankerNo.Value + "'"
                    End If

                ElseIf rdbTankerWise.Checked = True Then
                    qry = "SELECT  Max(XXXFinal.tanker_no) AS Tanker_No, Sum (MCC_Qty) AS MCC_Qty,  Sum (MCC_FATKG) AS MCC_FATKG, Sum (MCC_SNFKG) AS MCC_SNFKG, 
                          sum(entered_qty) AS Entered_Qty, Sum (entered_fatkg)  AS Entered_FATKg, Sum (entered_snfkg) AS Entered_SNFKg, Sum(entered_qty) - Sum(MCC_Qty) AS DiffEnteredVsMCC_Qty, Sum (entered_fatkg) - Sum (MCC_FATKG)  AS
                          DiffEnteredVsMCC_FAT, Sum (entered_snfkg)- Sum (MCC_SNFKG) AS DiffEnteredVsMCC_SNF FROM   (SELECT Max(entered_qty) AS Entered_Qty, Max(entered_fatkg) AS Entered_FATKg, Max(entered_snfkg) AS Entered_SNFKg, Max(document_date) AS Document_Date, document_no, Max(tanker_no)
                          AS Tanker_No, sum(MCC_Qty) AS MCC_Qty, Isnull(Sum(MCC_FATKG), 0) AS MCC_FATKG, Isnull(Sum(MCC_SNFKG), 0) AS MCC_SNFKG
                          FROM   (SELECT tspl_milk_collection_mcc.entered_qty, tspl_milk_collection_mcc.entered_fatkg, tspl_milk_collection_mcc.entered_snfkg,
                          tspl_milk_collection_mcc_detail.document_no, tspl_milk_collection_mcc.tanker_no, CONVERT (VARCHAR, tspl_milk_collection_mcc.document_date,103)
                          AS Document_Date, Isnull(qty, 0) AS MCC_Qty, Isnull(fat, 0) AS MCC_FAT, ISNULL(SNF, 0) AS MCC_SNF, ISNULL(FATKG, 0) AS MCC_FATKG, 
                          ISNULL(SNFKG, 0) AS MCC_SNFKG  FROM   tspl_milk_collection_mcc_detail
                          LEFT OUTER JOIN tspl_milk_collection_mcc ON tspl_milk_collection_mcc.document_no = tspl_milk_collection_mcc_detail.document_no 
                          LEFT OUTER JOIN tspl_mcc_master ON tspl_mcc_master.mcc_code = tspl_milk_collection_mcc_detail.mcc_code 
                          LEFT OUTER JOIN tspl_bulk_route_master ON tspl_bulk_route_master.route_no = tspl_milk_collection_mcc.route_code
                          WHERE  CONVERT(DATE, tspl_milk_collection_mcc.document_date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) AND CONVERT (DATE, tspl_milk_collection_mcc.document_date, 103)
                         <= CONVERT (DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) ) xyz GROUP  BY  document_no) XXXFinal GROUP  BY  Tanker_No
                         ORDER  BY Tanker_No asc "

                ElseIf rbtnRouteWise.Checked = True Then
                    qry = "SELECT ROW_NUMBER() OVER(ORDER BY ROUTE_NO) AS 'S.No.',  CONVERT (VARCHAR, Document_Date,103) AS Date , ROUTE_NO, max(ROUTE_NAME)ROUTE_NAME,  Sum (MCC_Qty) AS MCC_Qty, convert(decimal(18,2),(sum(MCC_FATKG)*100)/sum(MCC_Qty)) MCC_FAT_Per, convert(decimal(18,2),(sum(MCC_SNFKG)*100)/sum(MCC_Qty)) MCC_SNF_Per,
                          Sum (MCC_FATKG) AS MCC_FATKG, Sum (MCC_SNFKG) AS MCC_SNFKG, sum(entered_qty) AS Entered_Qty, convert(decimal(18,2),(sum(Entered_FATKg)*100)/sum(Entered_Qty)) Entered_FAT_Per,convert(decimal(18,2),(sum(Entered_SNFKg)*100)/sum(Entered_Qty)) Entered_SNF_Per, Sum (entered_fatkg)  AS Entered_FATKg,
                          Sum (entered_snfkg) AS Entered_SNFKg, Sum(entered_qty) - Sum(MCC_Qty) AS DiffEnteredVsMCC_Qty, Sum (entered_fatkg) - Sum (MCC_FATKG)  AS DiffEnteredVsMCC_FAT, Sum (entered_snfkg)- Sum (MCC_SNFKG) AS DiffEnteredVsMCC_SNF FROM   (
						  SELECT ROUTE_NO,max(ROUTE_NAME)ROUTE_NAME,  sum(entered_qty) AS Entered_Qty, Isnull(sum(entered_fatkg),0) AS Entered_FATKg, Isnull(sum(entered_snfkg),0) AS Entered_SNFKg, (document_date) AS Document_Date,	 
                          sum(MCC_Qty) AS MCC_Qty, Isnull(Sum(MCC_FATKG), 0) AS MCC_FATKG, Isnull(Sum(MCC_SNFKG), 0) AS MCC_SNFKG FROM   (
						  SELECT tspl_bulk_route_master.ROUTE_NO,tspl_bulk_route_master.ROUTE_NAME, tspl_milk_collection_mcc.entered_qty, tspl_milk_collection_mcc.entered_fatkg, tspl_milk_collection_mcc.entered_snfkg,
                          tspl_milk_collection_mcc_detail.document_no, tspl_milk_collection_mcc.document_date
                          AS Document_Date, Isnull(qty, 0) AS MCC_Qty, Isnull(fat, 0) AS MCC_FAT, ISNULL(SNF, 0) AS MCC_SNF, ISNULL(FATKG, 0) AS MCC_FATKG, 
                          ISNULL(SNFKG, 0) AS MCC_SNFKG  FROM   tspl_milk_collection_mcc_detail
                          LEFT OUTER JOIN tspl_milk_collection_mcc ON tspl_milk_collection_mcc.document_no = tspl_milk_collection_mcc_detail.document_no 
                          LEFT OUTER JOIN tspl_mcc_master ON tspl_mcc_master.mcc_code = tspl_milk_collection_mcc_detail.mcc_code 
                          LEFT OUTER JOIN tspl_bulk_route_master ON tspl_bulk_route_master.route_no = tspl_milk_collection_mcc.route_code
                           WHERE  CONVERT(DATE, tspl_milk_collection_mcc.document_date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) AND CONVERT (DATE, tspl_milk_collection_mcc.document_date, 103)
                         <= CONVERT (DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) ) xyz GROUP  BY ROUTE_NO, Document_Date) XXXFinal GROUP  BY  ROUTE_NO , Document_Date
                         ORDER  BY ROUTE_NO , Document_Date asc "
                ElseIf rdbCollectionWise.Checked = True Then
                    qry = " select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No,FORMAT(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date, 'dd/MM/yyyy') AS Document_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code,
                            TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code,
                            TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No,Vehicle_No,Entered_Qty,Entered_FATKg,Entered_SNFKg,
                            CONVERT(DECIMAL(10, 2),( CLR/4 + (0.2*TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.FAT) + " + clsCommon.myCstr(corrFactor) + "),2) AS SNFPer,
                            TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.CLR ,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.FAT,FORMAT(Collection_Date, 'dd/MM/yyyy') AS Collection_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,
                            TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,Qty,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FAT AS DCS_FAT,SNF,FATKG,SNFKG,CONVERT(DECIMAL(10, 2),(( (SNFKG) * 100 / NULLIF( (Qty),0))),2) as DCS_SNFPER
                            from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL
                            left outer join TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No
                            LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code
                            left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
                            left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
                            WHERE convert(date, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) 
                            and convert (date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) "
                ElseIf rdbMultiple.Checked = True Then
                    qry = "  SELECT Document_No,max(FORMAT(final.Document_Date, 'dd/MM/yyyy'))Document_Date,max(final.MCC_Code)MCC_Code,max(final.MCC_NAME)MCC_NAME,max(final.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,
                             max(final.Route_Code)Route_Code,max(final.ROUTE_NAME)ROUTE_NAME,max(final.Tanker_No)Tanker_No,max(final.Vehicle_No)Vehicle_No,max(final.Entered_Qty)Entered_Qty,
                             max(final.Entered_FATKg)Entered_FATKg,max(final.Entered_SNFKg)Entered_SNFKg,max(final.CLR)CLR,max(final.FATDAYS)FAT,max(final.VLC_Code)VLC_Code,max(final.VLC_Name)VLC_Name,max(final.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,sum(final.Qty)Qty,sum(final.FATKG)FATKG,sum(final.SNFKG)SNFKG, 
                             CONVERT(DECIMAL(10, 2),ROUND(((sum (FATKG) / NULLIF(sum (Qty),0)) * 100),2)) as DCS_FAT,CONVERT(DECIMAL(10, 2),ROUND(((sum (SNFKG) / nullif (sum (Qty),0)) * 100),2)) as DCS_SNF,
                             MAX(Entered_Qty) - SUM(Qty) AS Diff_Qty,MAX(Entered_FATKg) - SUM(FATKG) AS Diff_FAT,MAX(Entered_SNFKg) - SUM(SNFKG) AS Diff_SNF
                             FROM 
                            (select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code,
                            TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.CLR,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.FAT as FATDAYS,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,
                            TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No,Vehicle_No,Entered_Qty,Entered_FATKg,Entered_SNFKg,Collection_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code,
                            TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,Qty,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FAT,SNF,FATKG,SNFKG from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL
                            left outer join TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No
                            LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code
                            left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
                            left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
                            WHERE convert(date, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) 
                            and convert (date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) FINAL  group by Document_No"
                ElseIf rbtnBMCTankerCollection.Checked Then
                    qry = "Select Convert(varchar(10),xxx.Document_Date,103) As [BMC Date],xxx.ROUTE_NO As [Route No],(xxx.ROUTE_NAME) As [Route Name],(xxx.MCC_NAME) As [MCC Name],
                            (xxx.VLC_Code_VLC_Uploader)[VLC Uploader Code],Convert(int,xxx.Qty)Qty,Convert(Decimal(18,1),xxx.FAT_Per) As [FAT(%)],Convert(Decimal(18,1),xxx.SNF_Per) As [SNF(%)],Convert(Decimal(18,3),xxx.Entered_FATKg) As [FAT KG],Convert(Decimal(18,3),xxx.Entered_SNFKg) As [SNF KG],xxx.TankerDocument_Date As [Tanker Date],xxx.TankerTanker_No AS[Tanker No],
                            Convert(int,xxx.TankerQty) AS [Tanker Qty],Convert(Decimal(18,1),xxx.TankerFAT_Per)[Tanker Fat(%)],Convert(decimal(18,1),xxx.TankerSNF_Per)[Tanker SNF(%)],Convert(Decimal(18,3),xxx.TankerEntered_FATKg) As [Tanker FAT KG],Convert(Decimal(18,3),xxx.TankerEntered_SNFKg) As [Tanker SNF KG],Convert(int,(xxx.TankerQty-xxx.Qty))[Diff. Qty],
                            Convert(Decimal(18,1),(xxx.TankerFAT_Per-xxx.FAT_Per))[Diff. FAT(%)],Convert(Decimal(18,1),(xxx.TankerSNF_Per-xxx.SNF_Per))[Diff. SNF(%)],
                            Convert(Decimal(18,3),(xxx.TankerEntered_FATKg-xxx.Entered_FATKg))[Diff. FAT KG],Convert(decimal(18,3),(xxx.TankerEntered_SNFKg-xxx.Entered_SNFKg))[Diff. SNF KG] 
							from 	
							(Select BMCData.*,TankerData.Document_Date As TankerDocument_Date,TankerData.ROUTE_NO As TankerROUTE_NO,TankerData.Tanker_No As TankerTanker_No,IsNull(TankerData.Qty,0)TankerQty,Isnull(TankerData.FAT_Per,0)TankerFAT_Per,IsNull(TankerData.SNF_Per,0)TankerSNF_Per,IsNull(TankerData.Entered_FATKg,0)TankerEntered_FATKg,IsNull(TankerData.Entered_SNFKg,0)TankerEntered_SNFKg from
							
							(select  (tspl_gate_entry_details.Date_And_Time)Document_Date,TSPL_BULK_ROUTE_MASTER.ROUTE_NO,Max(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME)ROUTE_NAME,
                             Max(tspl_gate_entry_details.Tanker_No)Tanker_No,Sum(Qty_In_Kg)Qty,Avg(FAT_Per)FAT_Per,Avg(SNF_Per)SNF_Per, sum((tspl_gate_entry_details.fat_per*tspl_gate_entry_details.Qty_In_Kg)/100) As Entered_FATKg,
                             sum((tspl_gate_entry_details.snf_Per*tspl_gate_entry_details.Qty_In_Kg)/100) As Entered_SNFKg  From tspl_gate_entry_details
                            Left Outer Join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=tspl_gate_entry_details.ROUTE_NO
							Where Convert(Date,Date_And_Time,103)>= convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) And Convert(Date,Date_And_Time,103)<= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)
                            Group By TSPL_BULK_ROUTE_MASTER.ROUTE_NO,tspl_gate_entry_details.Date_And_Time )TankerData
							left outer join
							(Select max(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date)Document_Date,TSPL_BULK_ROUTE_MASTER.ROUTE_NO,Max(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME)ROUTE_NAME,Max(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,Max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,Sum(Entered_Qty)Qty,(sum(Entered_FATKg)*100)/sum(Entered_Qty) FAT_Per,
							(sum(Entered_SNFKg)*100)/sum(Entered_Qty) SNF_Per,Sum(Entered_FATKg)Entered_FATKg,Sum(Entered_SNFKg)Entered_SNFKg from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS 
                            Left Outer Join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
                            Left Outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
                            Left Outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader 
							Where Convert(Date,Document_Date,103)>= convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) And Convert(Date,Document_Date,103)<= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)
                            Group By TSPL_BULK_ROUTE_MASTER.ROUTE_NO,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date) As BMCData
							 On TankerData.Document_Date=BMCData.Document_Date And 
							 TankerData.ROUTE_NO=BMCData.ROUTE_NO)xxx"

                    'qry = "Select Convert(varchar(10),xxx.Document_Date,103) As [BMC Date],xxx.ROUTE_NO As [Route No],(xxx.ROUTE_NAME) As [Route Name],(xxx.MCC_NAME) As [MCC Name],
                    '        (xxx.VLC_Code_VLC_Uploader)[VLC Uploader Code],Convert(int,xxx.Qty)Qty,Convert(Decimal(18,1),xxx.FAT_Per) As [FAT(%)],Convert(Decimal(18,1),xxx.SNF_Per) As [SNF(%)],Convert(Decimal(18,3),xxx.Entered_FATKg) As [FAT KG],Convert(Decimal(18,3),xxx.Entered_SNFKg) As [SNF KG],xxx.TankerDocument_Date As [Tanker Date],xxx.TankerTanker_No AS[Tanker No],
                    '        Convert(int,xxx.TankerQty) AS [Tanker Qty],Convert(Decimal(18,1),xxx.TankerFAT_Per)[Tanker Fat(%)],Convert(decimal(18,1),xxx.TankerSNF_Per)[Tanker SNF(%)],Convert(Decimal(18,3),xxx.TankerEntered_FATKg) As [Tanker FAT KG],Convert(Decimal(18,3),xxx.TankerEntered_SNFKg) As [Tanker SNF KG],Convert(int,(xxx.TankerQty-xxx.Qty))[Diff. Qty],
                    '        Convert(Decimal(18,1),(xxx.TankerFAT_Per-xxx.FAT_Per))[Diff. FAT(%)],Convert(Decimal(18,1),(xxx.TankerSNF_Per-xxx.SNF_Per))[Diff. SNF(%)],
                    '        Convert(Decimal(18,3),(xxx.TankerEntered_FATKg-xxx.Entered_FATKg))[Diff. FAT KG],Convert(decimal(18,3),(xxx.TankerEntered_SNFKg-xxx.Entered_SNFKg))[Diff. SNF KG] from 				(Select BMCData.*,TankerData.Document_Date As TankerDocument_Date,TankerData.ROUTE_NO As TankerROUTE_NO,TankerData.Tanker_No As TankerTanker_No,IsNull(TankerData.Qty,0)TankerQty,Isnull(TankerData.FAT_Per,0)TankerFAT_Per,IsNull(TankerData.SNF_Per,0)TankerSNF_Per,IsNull(TankerData.Entered_FATKg,0)TankerEntered_FATKg,IsNull(TankerData.Entered_SNFKg,0)TankerEntered_SNFKg from (Select (TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date)Document_Date,TSPL_BULK_ROUTE_MASTER.ROUTE_NO,Max(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME)ROUTE_NAME,Max(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,Max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,Sum(Entered_Qty)Qty,Max((Entered_FATKg*100)/Entered_Qty)FAT_Per,Max((Entered_SNFKg*100)/Entered_Qty)SNF_Per,Sum(Entered_FATKg)Entered_FATKg,Sum(Entered_SNFKg)Entered_SNFKg from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS 
                    '        Left Outer Join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
                    '        Left Outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
                    '        Left Outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader 
                    '        Group By TSPL_BULK_ROUTE_MASTER.ROUTE_NO,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date) As BMCData
                    '        Left Outer Join(select  (tspl_gate_entry_details.Date_And_Time)Document_Date,TSPL_BULK_ROUTE_MASTER.ROUTE_NO,Max(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME)ROUTE_NAME,
                    '         Max(tspl_gate_entry_details.Tanker_No)Tanker_No,Sum(Qty_In_Kg)Qty,Avg(FAT_Per)FAT_Per,Avg(SNF_Per)SNF_Per, Max(((fat_per*Qty_In_Kg)/100)) As Entered_FATKg,
                    '         Max(((snf_Per*Qty_In_Kg)/100)) As Entered_SNFKg From tspl_gate_entry_details
                    '        Left Outer Join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=tspl_gate_entry_details.ROUTE_NO
                    '        Group By TSPL_BULK_ROUTE_MASTER.ROUTE_NO,tspl_gate_entry_details.Date_And_Time )TankerData On TankerData.Document_Date=BMCData.Document_Date And TankerData.ROUTE_NO=BMCData.ROUTE_NO)xxx
                    'Where Convert(Date,xxx.Document_Date,103)>= convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) And Convert(Date,xxx.Document_Date,103)<= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)"

                End If

                If rbtnDockDateWise.Checked Then
                    qry = "Select ROW_NUMBER() OVER(ORDER BY (Select 1)) As SNo, SDate , sum(Qty)Qty , sum(Milk_Wtd)Milk_Wtd ,convert(Decimal(18,2),(sum(Fat_KG * 100)/SUM(Milk_Wtd))) As FAT,convert(Decimal(18,2),(sum(SNF_KG * 100)/SUM(Milk_Wtd))) As SNF, sum(Fat_KG)Fat_KG , sum(SNF_KG)SNF_KG , max(Rate)Rate , max(Amt)Amt , max(VLC_Code)VLC_Code , max(DCS_Uploader_Code)DCS_Uploader_Code , max(DCS_Code)DCS_Code , max(DCS_Name)DCS_Name , max(BMC_Uploader_Code)BMC_Uploader_Code , max(BMC_Code)BMC_Code , max(BMC_Name)BMC_Name , max(Area)Area , max(Route_Code)Route_Code from (
                       " & qry & " )xx group by SDate order by SDate"

                ElseIf rbtnDockShiftWise.Checked Then
                    qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SNo, SDate,Shift , sum(Qty)Qty , sum(Milk_Wtd)Milk_Wtd ,convert(decimal(18,2),(sum(Fat_KG * 100)/SUM(Milk_Wtd))) as FAT,convert(decimal(18,2),(sum(SNF_KG * 100)/SUM(Milk_Wtd))) AS SNF, sum(Fat_KG)Fat_KG , sum(SNF_KG)SNF_KG , max(Rate)Rate , max(Amt)Amt , max(VLC_Code)VLC_Code , max(DCS_Uploader_Code)DCS_Uploader_Code , max(DCS_Code)DCS_Code , max(DCS_Name)DCS_Name , max(BMC_Uploader_Code)BMC_Uploader_Code , max(BMC_Code)BMC_Code , max(BMC_Name)BMC_Name , max(Area)Area , max(Route_Code)Route_Code from (
                       " & qry & " )xx group by SDate , Shift order by SDate, shift desc"
                End If

                dt = clsDBFuncationality.GetDataTable(qry)
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    Gv1.DataSource = dt
                    If rbtnDock.Checked = True OrElse rbtnDockDateWise.Checked OrElse rbtnDockShiftWise.Checked Then
                        SetGridFormationOFGV1Dock()
                    ElseIf rdbMultiple.Checked = True Then
                        SetGridFormationOFGV1Multiple()
                    ElseIf rdbCollectionWise.Checked = True Then
                        SetGridFormationOFGV1Collection()
                    Else
                        SetGridFormationOFGV1()
                    End If

                    For ii As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.BestFitColumns()
                    Gv1.EnableFiltering = True
                    Dim summaryRowItem As New GridViewSummaryRowItem()

                    If rdbSummary.Checked = True Then
                        '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
                        ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
                        Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(EnteredQty)
                        Dim EnteredFatKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(EnteredFatKg)
                        Dim EnteredSnfKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(EnteredSnfKg)
                    End If
                    If rbtnBMCTankerCollection.Checked = False Then
                        Dim MCCQty As New GridViewSummaryItem("MCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(MCCQty)
                        Dim MCCFatKg As New GridViewSummaryItem("MCC_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(MCCFatKg)
                        Dim MCCSnfKg As New GridViewSummaryItem("MCC_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(MCCSnfKg)

                        Dim DCSQty As New GridViewSummaryItem("DCS_Qty", "{0:F0}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(DCSQty)
                        Dim DCSFatKg As New GridViewSummaryItem("DCS_FATKG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(DCSFatKg)
                        Dim DCSSnfKg As New GridViewSummaryItem("DCS_SNFKG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(DCSSnfKg)

                        Dim DiffQty As New GridViewSummaryItem("Diff_Qty", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(DiffQty)
                        Dim DiffFatKg As New GridViewSummaryItem("Diff_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(DiffFatKg)
                        Dim DiffSnfKg As New GridViewSummaryItem("Diff_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(DiffSnfKg)
                    End If
                    If rbtnDock.Checked = False AndAlso rbtnDockShiftWise.Checked = False AndAlso rbtnDockDateWise.Checked = False Then
                        If rdbSummary.Checked = True Then
                            '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
                            ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
                            Dim DiffEnteredVsMCCQty As New GridViewSummaryItem("DiffEnteredVsMCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCQty)
                            Dim DiffEnteredVsMCCFatKg As New GridViewSummaryItem("DiffEnteredVsMCC_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCFatKg)
                            Dim DiffEnteredVsMCCSnfKg As New GridViewSummaryItem("DiffEnteredVsMCC_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCSnfKg)
                        End If
                        If rbtnTranpoterGainLoss.Checked = True Then
                            Dim Entered_Qty As New GridViewSummaryItem("Entered_Qty", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_Qty)
                            Dim Entered_FATKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_FATKg)
                            Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_SNFKg)
                            Dim DiffMCCVsEnteredQty As New GridViewSummaryItem("DiffMCCVsEntered_Qty", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffMCCVsEnteredQty)
                            Dim DiffEnteredVsMCCFatKg As New GridViewSummaryItem("DiffMCCVsEntered_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCFatKg)
                            Dim DiffMCCVsEnteredSnfKg As New GridViewSummaryItem("DiffMCCVsEntered_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffMCCVsEnteredSnfKg)
                            Dim FATAmt As New GridViewSummaryItem("FAT_AMT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(FATAmt)
                            Dim SNFAmt As New GridViewSummaryItem("SNF_AMT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(SNFAmt)
                            Dim FATKG_Recovered As New GridViewSummaryItem("FATKG_Recovered", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(FATKG_Recovered)
                            Dim SNFKG_Recovered As New GridViewSummaryItem("SNFKG_Recovered", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(SNFKG_Recovered)
                            Dim Amount As New GridViewSummaryItem("AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Amount)
                        End If
                        If rdbTankerWise.Checked Then
                            'Dim Tanker_No As New GridViewSummaryItem("Tanker_No", "{0:F2}", "Total")
                            'summaryRowItem.Add(Tanker_No)
                            Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredQty)
                            Dim EnteredFatKg As New GridViewSummaryItem("Entered_FATKg", "{0:F3}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredFatKg)
                            Dim EnteredSnfKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F3}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredSnfKg)
                            Dim DiffDCSVsEnteredQty As New GridViewSummaryItem("DiffEnteredVsMCC_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffDCSVsEnteredQty)
                            Dim DiffEnteredVsDCSFatKg As New GridViewSummaryItem("DiffEnteredVsMCC_FAT", "{0:F3}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsDCSFatKg)
                            Dim DiffEnteredVsDCSSnfKg As New GridViewSummaryItem("DiffEnteredVsMCC_SNF", "{0:F3}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsDCSSnfKg)

                        End If

                        If rbtnRouteWise.Checked Then
                            Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredQty)
                            Dim EnteredFatKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredFatKg)
                            Dim EnteredSnfKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredSnfKg)
                            Dim DiffDCSVsEnteredQty As New GridViewSummaryItem("DiffEnteredVsMCC_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffDCSVsEnteredQty)
                            Dim DiffEnteredVsDCSFatKg As New GridViewSummaryItem("DiffEnteredVsMCC_FAT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsDCSFatKg)
                            Dim DiffEnteredVsDCSSnfKg As New GridViewSummaryItem("DiffEnteredVsMCC_SNF", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsDCSSnfKg)

                        End If

                        If rdbMultiple.Checked Then
                            Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredQty)
                            Dim Entered_FATKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_FATKg)
                            Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_SNFKg)
                            Dim Qty As New GridViewSummaryItem("Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Qty)
                            Dim FATKG As New GridViewSummaryItem("FATKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(FATKG)
                            Dim SNFKG As New GridViewSummaryItem("SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(SNFKG)
                            Dim DiffQty1 As New GridViewSummaryItem("Diff_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffQty1)
                            Dim DiffFat1 As New GridViewSummaryItem("Diff_FAT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffFat1)
                            Dim DiffSnf1 As New GridViewSummaryItem("Diff_SNF", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffSnf1)
                        End If


                        If rdbCollectionWise.Checked Then
                            'Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            'summaryRowItem.Add(EnteredQty)
                            'Dim Entered_FATKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                            'summaryRowItem.Add(Entered_FATKg)
                            'Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                            'summaryRowItem.Add(Entered_SNFKg)
                            Dim Qty As New GridViewSummaryItem("Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Qty)
                            Dim FATKG As New GridViewSummaryItem("FATKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(FATKG)
                            Dim SNFKG As New GridViewSummaryItem("SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(SNFKG)
                        End If

                        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    End If
                    If rbtnBMCTankerCollection.Checked Then
                        Dim Qty As New GridViewSummaryItem("Qty", "{0:F0}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Qty)
                        Dim BMC_FAT As New GridViewSummaryItem("FAT(%)", "{0:F1}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(BMC_FAT)
                        Dim BMC_SNF As New GridViewSummaryItem("SNF(%)", "{0:F1}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(BMC_SNF)
                        Dim BMC_FATKG As New GridViewSummaryItem("FAT KG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(BMC_FATKG)
                        Dim BMC_SNFKG As New GridViewSummaryItem("SNF KG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(BMC_SNFKG)
                        Dim Tanker_Qty As New GridViewSummaryItem("Tanker Qty", "{0:F0}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Tanker_Qty)
                        Dim Tanker_FAT As New GridViewSummaryItem("Tanker FAT(%)", "{0:F1}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(Tanker_FAT)
                        Dim Tanker_SNF As New GridViewSummaryItem("Tanker SNF(%)", "{0:F1}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(Tanker_SNF)
                        Dim Tanker_FATKG As New GridViewSummaryItem("Tanker FAT KG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Tanker_FATKG)
                        Dim Tanker_SNFKG As New GridViewSummaryItem("Tanker SNF KG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Tanker_SNFKG)
                        Dim Diff_Qty As New GridViewSummaryItem("Diff. Qty", "{0:F0}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Diff_Qty)
                        Dim Diff_FAT As New GridViewSummaryItem("Diff. FAT(%)", "{0:F1}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(Diff_FAT)
                        Dim Diff_SNF As New GridViewSummaryItem("Diff. SNF(%)", "{0:F1}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(Diff_SNF)
                        Dim Diff_FATKG As New GridViewSummaryItem("Diff. FAT KG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Diff_FATKG)
                        Dim Diff_SNFKG As New GridViewSummaryItem("Diff. SNF KG", "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Diff_SNFKG)
                    End If
                    ControlEnableDisable(False)

                    '=========================


                    'Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Document_Date] as [Document Date] format ""{0}: {1}"" Group By [Document_Date]"))
                    'Gv1.AutoExpandGroups = True
                    '    Gv1.ShowGroupPanel = False
                    '    Gv1.ShowRowHeaderColumn = False
                    '    Gv1.AllowAddNewRow = False
                    '    Gv1.AllowDeleteRow = False
                    '    Gv1.EnableFiltering = True
                    '    Gv1.ShowFilteringRow = True



                    '==========================
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                ReStoreGridLayout()
                If rbtnDock.Checked = False AndAlso rbtnDockDateWise.Checked = False AndAlso rbtnDockShiftWise.Checked = False Then
                    View()
                End If

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Else
            Try
                If rbtnTranpoterGainLoss.Checked Then
                    Dim checkRate As String
                    Dim dt1 As New DataTable
                    checkRate = "(SELECT top 1  TSPL_OWN_BMC_GAIN_LOSS_RATE.Code FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 and TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 and 
 CONVERT(date, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "', 103))"

                    dt1 = clsDBFuncationality.GetDataTable(checkRate) 'GetDataTable(qry)
                    If dt1.Rows.Count <= 0 Then

                        Throw New Exception("FAT/SNF RATE NOT FOUND!")
                    End If

                End If
                If rdbSummary.Checked = True Then
                    PageSetupReport_ID = MyBase.Form_ID + "_S"
                End If
                PageSetupReport_ID = MyBase.Form_ID
                TemplateGridview = Gv1
                Dim BaseQry As String = ""
                Dim qry As String = ""
                Dim dt As New DataTable
                Dim whre As String = ""
                Dim strMilkCollectionMCCStatus As String = ""
                Dim strMilkcollectionDCSStatus As String = ""
                If rbtnDock.Checked = False AndAlso rbtnDockDateWise.Checked = False AndAlso rbtnDockShiftWise.Checked = False Then
                    If clsCommon.CompairString(cboItemType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                        strMilkCollectionMCCStatus = " and TSPL_MILK_COLLECTION_MCC.Status = 1 "
                        strMilkcollectionDCSStatus = " and TSPL_MILK_COLLECTION_DCS.Status = 1 "
                    ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                        strMilkCollectionMCCStatus = " and TSPL_MILK_COLLECTION_MCC.Status = 0 "
                        strMilkcollectionDCSStatus = " and TSPL_MILK_COLLECTION_DCS.Status = 0 "
                    End If
                End If

                qry = "select max(Entered_Qty) as Entered_Qty, max(Entered_FATKg) as Entered_FATKg, max(Entered_SNFKg) as Entered_SNFKg, max(PK_Id) as PK_Id, Document_No, max(Document_Date) as Document_Date, max(MCC_Code) as MCC_Code, max(MCC_NAME) as MCC_NAME, max(UploaderNo) as UploaderNo, max(Route_Code) as Route_Code, max(ROUTE_NAME) as ROUTE_NAME, max(Tanker_No) as Tanker_No,max(Vehicle_No) as Vehicle_No, sum(MCC_Qty) as MCC_Qty, ROUND(((sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_FAT, ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_SNF, sum(MCC_FATKG) as MCC_FATKG, sum(MCC_SNFKG) as MCC_SNFKG, max(DCS_Qty) as DCS_Qty, isnull( (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_FAT, isnull( (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_SNF, isnull(max(DCS_FATKG),0) as DCS_FATKG,isnull( max(DCS_SNFKG),0) as DCS_SNFKG, max(DCS_Qty) - sum(MCC_Qty) as Diff_Qty, (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100)- ROUND( ( ( sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_FAT, (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100)-ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_SNF, sum(MCC_FATKG) -max(DCS_FATKG) as Diff_FATKG, sum(MCC_SNFKG)-max(DCS_SNFKG) as Diff_SNFKG from ( select TSPL_MILK_COLLECTION_MCC.Entered_Qty, TSPL_MILK_COLLECTION_MCC.Entered_FATKg, TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id, TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No, convert ( varchar, TSPL_MILK_COLLECTION_MCC.Document_Date, 103 ) as Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo, TSPL_MILK_COLLECTION_MCC.Route_Code, TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCC_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCC_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCC_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCC_SNFKG, isnull(XXXDCS.qty, 0) as DCS_Qty, isnull(XXXDCS.FAT, 0) as DCS_FAT, isnull(XXXDCS.SNF, 0) as DCS_SNF, isnull(XXXDCS.FATKG, 0) as DCS_FATKG, isnull(XXXDCS.SNFKG, 0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty, 0) as Diff_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT - isnull(XXXDCS.FAT, 0) as Diff_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF - isnull(XXXDCS.SNF, 0) as Diff_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG - isnull(XXXDCS.FATKG, 0) as Diff_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG - isnull(XXXDCS.SNFKG, 0) as Diff_SNFKG from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code left outer join ( select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty) as qty, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) as FATKG, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) as SNFKG, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) /nullif( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as FAT, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) / nullif(sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as SNF
                         from TSPL_MILK_COLLECTION_DCS_DETAIL 
                                    left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                        left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) " + strMilkcollectionDCSStatus + "
                        group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                        where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) xyz group by UploaderNo, Document_No  " + strMilkCollectionMCCStatus + " "


                If rdbSummary.Checked = True Then
                    qry = " select XXXFinal.Document_No, max(XXXFinal.Document_Date) as Document_Date ,max( XXXFinal.Route_Code) as Route_Code, max(XXXFinal.ROUTE_NAME) as ROUTE_NAME ,
                            max(XXXFinal.Tanker_No ) as Tanker_No, max(XXXFinal.Vehicle_No) as Vehicle_No, max(Entered_Qty) as Entered_Qty,((max (Entered_FATKg) / nullif (max (Entered_Qty),0)) * 100) as Entered_FAT, ((max (Entered_SNFKg) / nullif (max (Entered_Qty),0)) * 100) as Entered_SNF , CAST(ROUND(max(Entered_FATKg), 2) AS DECIMAL(10, 2)) as Entered_FATKg,CAST(ROUND( max(Entered_SNFKg), 2) AS DECIMAL(10, 2)) as Entered_SNFKg , 
                            sum( XXXFinal.MCC_Qty) as  MCC_Qty, ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as MCC_FAT, ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as MCC_SNF,CAST(ROUND(sum (MCC_FATKG), 2) AS DECIMAL(10, 2)) as MCC_FATKG ,CAST(ROUND( sum (MCC_SNFKG), 2) AS DECIMAL(10, 2)) as MCC_SNFKG , sum (DCS_Qty) as DCS_Qty , ROUND(((sum (DCS_FATKG) / nullif( sum (DCS_Qty),0)) * 100),1,0) as DCS_FAT,ROUND(((sum (DCS_SNFKG) / nullif (sum (DCS_Qty),0)) * 100),1,0) as DCS_SNF , CAST(ROUND(sum(DCS_FATKG), 2) AS DECIMAL(10, 2))  as DCS_FATKG ,CAST(ROUND(sum(DCS_SNFKG), 2) AS DECIMAL(10, 2))  as DCS_SNFKG, sum (Diff_Qty) as Diff_Qty ,
                            max(DCS_FAT) - ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as Diff_FAT, max(DCS_SNF) - ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as Diff_SNF,CAST(ROUND(sum(Diff_FATKG), 2) AS DECIMAL(10, 2)) as Diff_FATKG, CAST(ROUND( sum(Diff_SNFKG), 2) AS DECIMAL(10, 2)) as Diff_SNFKG,  max(Entered_Qty) - sum( XXXFinal.MCC_Qty)  as DiffEnteredVsMCC_Qty ,
                            ((max (Entered_FATKg) / nullif (max (Entered_Qty),0)) * 100) - ROUND(((sum (MCC_FATKG) / nullif (sum (MCC_Qty),0)) * 100),1,0)  as DiffEnteredVsMCC_FAT ,
                            ((max (Entered_SNFKg) / nullif (max (Entered_Qty),0)) * 100) - ROUND(((sum (MCC_SNFKG) / nullif (sum (MCC_Qty),0)) * 100),1,0) as DiffEnteredVsMCC_SNF ,
                            CAST(ROUND( max(Entered_FATKg) -  sum (MCC_FATKG), 2) AS DECIMAL(10, 2))  as DiffEnteredVsMCC_FATKG , CAST(ROUND(  max(Entered_SNFKg) - sum (MCC_SNFKG), 2) AS DECIMAL(10, 2)) as DiffEnteredVsMCC_SNFKG  from ( " + qry + " ) XXXFinal group by XXXFinal.Document_No order by convert (datetime, max(XXXFinal.Document_Date),103) asc "
                ElseIf rdbDetails.Checked = True Then

                    qry = "select XXXFinal.PK_Id as PK_Id, XXXFinal.Document_No as Document_No, XXXFinal.Document_Date As Document_Date, XXXFinal.MCC_Code As MCC_Code, XXXFinal.MCC_NAME As MCC_NAME, XXXFinal.UploaderNo As UploaderNo, XXXFinal.Route_Code As Route_Code, XXXFinal.ROUTE_NAME As ROUTE_NAME, XXXFinal.Tanker_No As Tanker_No, XXXFinal.Vehicle_No As Vehicle_No, XXXFinal.MCC_Qty As MCC_Qty, Convert(Decimal(18,2),(MCC_FATKG / nullif(MCC_Qty, 0)) * 100) As MCC_FAT, convert(decimal(18,2),(MCC_SNFKG / nullif(MCC_Qty, 0)) * 100) As MCC_SNF, CAST(ROUND(MCC_FATKG, 2) AS DECIMAL(10, 2))  As MCC_FATKG, CAST(ROUND(MCC_SNFKG, 2) AS DECIMAL(10, 2)) As MCC_SNFKG, DCS_Qty As DCS_Qty,isnull( ROUND(((DCS_FATKG / nullif(DCS_Qty, 0)) * 100), 1, 0),0) As DCS_FAT, isnull(ROUND(((DCS_SNFKG / nullif(DCS_Qty, 0)) * 100), 1, 0),0) As DCS_SNF, CAST(ROUND(isnull(DCS_FATKG,0), 2) AS DECIMAL(10, 2)) As DCS_FATKG,CAST(ROUND(isnull(DCS_SNFKG,0), 2) AS DECIMAL(10, 2))  As DCS_SNFKG, Diff_Qty As Diff_Qty, DCS_FAT - ROUND(((MCC_FATKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_FAT, DCS_SNF - ROUND(((MCC_SNFKG / nullif(MCC_Qty, 0)) * 100), 1, 0) As Diff_SNF, CAST(ROUND(Diff_FATKG, 2) AS DECIMAL(10, 2)) As Diff_FATKG, CAST(ROUND(Diff_SNFKG, 2) AS DECIMAL(10, 2)) As Diff_SNFKG from( " + qry + " ) XXXFinal  
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=XXXFinal.Document_No"

                    'qry = qry + " order by TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No asc "
                ElseIf rbtnDock.Checked = True OrElse rbtnDockDateWise.Checked OrElse rbtnDockShiftWise.Checked Then
                    qry = ""
                    Dim strFilterQry As String = ""
                    Dim strQryStatus1 As String = ""
                    Dim strQryStatus2 As String = ""
                    Dim Area As String = ""
                    If txtMCC_Code.Value.Length > 0 Then
                        strFilterQry = " and TSPL_MCC_MASTER.MCC_Code in ('" + txtMCC_Code.Value + "') "
                    End If
                    Dim AreaName As String = Nothing
                    AreaName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from TSPL_MCC_MASTER WHERE Area_Location_Code = '" + fndArea.Value + "' "))


                    If clsCommon.myLen(fndArea.Value) > 0 Then
                        Area += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    End If

                    If clsCommon.CompairString(cboItemType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                        strQryStatus1 = "where TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1"
                        strQryStatus2 = "where TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1"
                    ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                        strQryStatus1 = "where TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=0"
                        strQryStatus2 = "where TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=0"
                    End If


                    qry = "Select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SNo,CONVERT(varchar(10), tmp.Shift_Date, 103) As SDate ,tmp.Shift As Shift,No_Of_Cans As Qty,Milk_Weight As Milk_Wtd,FAT as FAT,SNF as SNF,convert(decimal(18,2),((Milk_Weight*FAT)/100)) As Fat_KG,convert(decimal(18,2),((Milk_Weight*SNF)/100)) As SNF_KG,'' As Rate,'' As Amt,tmp.VLC_Code, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As DCS_Uploader_Code,TSPL_VLC_MASTER_HEAD.VLC_Code As DCS_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  As DCS_Name
                        ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader  As BMC_Uploader_Code,TSPL_MCC_MASTER.MCC_Code As BMC_Code,TSPL_MCC_MASTER.MCC_NAME As BMC_Name,'" + AreaName + "' as Area,TSPL_VLC_MASTER_HEAD.Route_Code
                        from(
                        Select Shift_Date,Shift,No_Of_Cans,Milk_Weight,FAT,SNF,VLC_Code  from TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        Left Outer Join TSPL_MILK_SHIFT_UPLOADER_HEAD ON TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
	                    " + strQryStatus1 + "
                        Union All
                        Select Shift_Date,Shift,No_Of_Cans,Milk_Weight,FAT,SNF,VLC_Code from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
	                    Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_HEAD ON TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
	                    " + strQryStatus2 + ")tmp
                        Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code=tmp.VLC_Code
                        Left Outer Join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        where convert (date,Shift_Date,103) >= convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) and convert (date,Shift_Date,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)" + strFilterQry
                ElseIf (rbtnTranpoterGainLoss.Checked = True) Then

                    If txtToleranceFat.Value < 0 Or txtToleranceFat.Value >= 100 Then

                        clsCommon.MyMessageBoxShow(Me, "Invalid Input !, Please enter valid Tolerance Fat%.", Me.Text)
                        txtToleranceFat.Focus()
                        Exit Sub
                    End If

                    If txtToleranceSNF.Value < 0 Or txtToleranceSNF.Value >= 100 Then
                        clsCommon.MyMessageBoxShow(Me, "Invalid Input !, Please enter valid Tolerance Fat%.", Me.Text)
                        txtToleranceSNF.Focus()
                        Exit Sub
                    End If

                    qry = "select  XXGetAllRecords.Document_No,XXGetAllRecords.Document_Date,XXGetAllRecords.Route_Code,XXGetAllRecords.ROUTE_NAME,XXGetAllRecords.Tanker_No,XXGetAllRecords.Vehicle_No,
XXGetAllRecords.Entered_Qty,CAST(ROUND( XXGetAllRecords.Entered_FATKg, 2) AS DECIMAL(10, 2)) as Entered_FATKg ,CAST(ROUND( XXGetAllRecords.Entered_SNFKg, 2) AS DECIMAL(10, 2))as Entered_SNFKg,
XXGetAllRecords.MCC_Qty,CAST(ROUND( XXGetAllRecords.MCC_FATKG, 2) AS DECIMAL(10, 2)) as MCC_FATKG,CAST(ROUND( XXGetAllRecords.MCC_SNFKG, 2) AS DECIMAL(10, 2)) as MCC_SNFKG,
XXGetAllRecords.DiffMCCVsEntered_Qty,
CAST(ROUND( XXGetAllRecords.DiffMCCVsEntered_FATKG, 2) AS DECIMAL(10, 2))
as DiffMCCVsEntered_FATKG," + clsCommon.myCstr(txtToleranceFat.Value) + " as FAT_Tolerence,CAST(ROUND( case when (isnull(XXGetAllRecords.DiffMCCVsEntered_FATKG - " + clsCommon.myCstr(txtToleranceFat.Value) + ",0))<0 then 0 else isnull(XXGetAllRecords.DiffMCCVsEntered_FATKG - " + clsCommon.myCstr(txtToleranceFat.Value) + ",0)  end , 2) AS DECIMAL(10, 2)) as FATKG_Recovered,Round(XXGetAllRecords.FAT_AMT,2,0) as FAT_AMT,
CAST(ROUND( XXGetAllRecords.DiffMCCVsEntered_SNFKG, 2) AS DECIMAL(10, 2))as DiffMCCVsEntered_SNFKG," + clsCommon.myCstr(txtToleranceSNF.Value) + " as SNF_Tolerence,CAST(ROUND( case when (isnull(XXGetAllRecords.DiffMCCVsEntered_SNFKG - " + clsCommon.myCstr(txtToleranceSNF.Value) + ",0))<0 then 0 else isnull(XXGetAllRecords.DiffMCCVsEntered_SNFKG - " + clsCommon.myCstr(txtToleranceSNF.Value) + ",0) end, 2) AS DECIMAL(10, 2))  as SNFKG_Recovered,Round(XXGetAllRecords.SNF_AMT,2,0) as SNF_AMT,Round((XXGetAllRecords.FAT_AMT + XXGetAllRecords.SNF_AMT),2,0) as AMOUNT,XXGetAllRecords.GainLossCode as GainLoss_Code,
   XXGetAllRecords.Loss_FAT_Rate as Loss_FAT_Rate,
   XXGetAllRecords.Loss_SNF_Rate as Loss_SNF_Rate,
   XXGetAllRecords.Start_Date as Start_Date
 from( select GetAllGainLossRate.*,
              case when GetAllGainLossRate.DiffMCCVsEntered_FATKG >= 0 then (
        case when (GetAllGainLossRate.DiffMCCVsEntered_FATKG - " + clsCommon.myCstr(txtToleranceFat.Value) + ") > 0 then ((GetAllGainLossRate.DiffMCCVsEntered_FATKG -" + clsCommon.myCstr(txtToleranceFat.Value) + ") *TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate) else 0 end)
        else 0 end as FAT_AMT,
        case when GetAllGainLossRate.DiffMCCVsEntered_SNFKG > 0 then (case when (GetAllGainLossRate.DiffMCCVsEntered_SNFKG - " + clsCommon.myCstr(txtToleranceSNF.Value) + ") > 0 then ((GetAllGainLossRate.DiffMCCVsEntered_SNFKG -" + clsCommon.myCstr(txtToleranceSNF.Value) + ") *TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate) else 0 end)
      else 0 end as SNF_AMT,
      TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date from ( select GetRateCode.*,( SELECT top 1 TSPL_OWN_BMC_GAIN_LOSS_RATE.Code FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 and TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 and CONVERT(date, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date, 103) <= CONVERT(date, GetRateCode.Document_Date, 103) order by TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date desc ) as GainLossCode from ( SELECT XXXFinal.Document_No, MAX(XXXFinal.Document_Date) AS Document_Date, MAX(XXXFinal.Route_Code) AS Route_Code, MAX(XXXFinal.ROUTE_NAME) AS ROUTE_NAME, MAX(XXXFinal.Tanker_No) AS Tanker_No, MAX(XXXFinal.Vehicle_No) AS Vehicle_No, MAX(Entered_Qty) AS Entered_Qty, ((MAX(Entered_FATKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS Entered_FAT, ((MAX(Entered_SNFKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS Entered_SNF, MAX(Entered_FATKg) AS Entered_FATKg, MAX(Entered_SNFKg) AS Entered_SNFKg, SUM(XXXFinal.MCC_Qty) AS MCC_Qty, ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS MCC_FAT, ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS MCC_SNF, SUM(MCC_FATKG) AS MCC_FATKG, SUM(MCC_SNFKG) AS MCC_SNFKG, SUM(DCS_Qty) AS DCS_Qty, ROUND(((SUM(DCS_FATKG) / NULLIF(SUM(DCS_Qty), 0)) * 100), 3, 0) AS DCS_FAT, ROUND(((SUM(DCS_SNFKG) / NULLIF(SUM(DCS_Qty), 0)) * 100), 3, 0) AS DCS_SNF, SUM(DCS_FATKG) AS DCS_FATKG, SUM(DCS_SNFKG) AS DCS_SNFKG, SUM(Diff_Qty) AS Diff_Qty, MAX(DCS_FAT) - ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS Diff_FAT, MAX(DCS_SNF) - ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS Diff_SNF, SUM(Diff_FATKG) AS Diff_FATKG, SUM(Diff_SNFKG) AS Diff_SNFKG, SUM(XXXFinal.MCC_Qty)- MAX(Entered_Qty) AS DiffMCCVsEntered_Qty, ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0)- ((MAX(Entered_FATKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS DiffMCCVsEntered_FAT, ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0)- ((MAX(Entered_SNFKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS DiffMCCVsEntered_SNF, SUM(MCC_FATKG)-MAX(Entered_FATKg) AS DiffMCCVsEntered_FATKG, SUM(MCC_SNFKG)-MAX(Entered_SNFKg) AS DiffMCCVsEntered_SNFKG from ( " + qry + " ) XXXFinal group by XXXFinal.Document_No )GetRateCode )GetAllGainLossRate left outer join TSPL_OWN_BMC_GAIN_LOSS_RATE on TSPL_OWN_BMC_GAIN_LOSS_RATE.Code=GetAllGainLossRate.GainLossCode ) XXGetAllRecords"
                    If clsCommon.myLen(TxtTankerNo.Value) > 0 Then
                        qry = qry + " where tanker_no ='" + TxtTankerNo.Value + "'"
                    End If

                ElseIf rdbTankerWise.Checked = True Then
                    qry = "SELECT  Max(XXXFinal.tanker_no) AS Tanker_No, Sum (MCC_Qty) AS MCC_Qty, CAST(ROUND( Sum (MCC_FATKG), 2) AS DECIMAL(10, 2))  AS MCC_FATKG,  CAST(ROUND( Sum (MCC_SNFKG), 2) AS DECIMAL(10, 2))  AS MCC_SNFKG, 
              sum(entered_qty) AS Entered_Qty, CAST(ROUND( Sum (entered_fatkg), 2) AS DECIMAL(10, 2))   AS Entered_FATKg, CAST(ROUND( Sum (entered_snfkg), 2) AS DECIMAL(10, 2))  AS Entered_SNFKg, Sum(entered_qty) - Sum(MCC_Qty) AS DiffEnteredVsMCC_Qty, Sum (entered_fatkg) - Sum (MCC_FATKG)  AS
              DiffEnteredVsMCC_FAT, Sum (entered_snfkg)- Sum (MCC_SNFKG) AS DiffEnteredVsMCC_SNF FROM   (SELECT Max(entered_qty) AS Entered_Qty, Max(entered_fatkg) AS Entered_FATKg, Max(entered_snfkg) AS Entered_SNFKg, Max(document_date) AS Document_Date, document_no, Max(tanker_no)
              AS Tanker_No, sum(MCC_Qty) AS MCC_Qty, Isnull(Sum(MCC_FATKG), 0) AS MCC_FATKG, Isnull(Sum(MCC_SNFKG), 0) AS MCC_SNFKG
              FROM   (SELECT tspl_milk_collection_mcc.entered_qty, tspl_milk_collection_mcc.entered_fatkg, tspl_milk_collection_mcc.entered_snfkg,
              tspl_milk_collection_mcc_detail.document_no, tspl_milk_collection_mcc.tanker_no, CONVERT (VARCHAR, tspl_milk_collection_mcc.document_date,103)
              AS Document_Date, Isnull(qty, 0) AS MCC_Qty, Isnull(fat, 0) AS MCC_FAT, ISNULL(SNF, 0) AS MCC_SNF, ISNULL(FATKG, 0) AS MCC_FATKG, 
              ISNULL(SNFKG, 0) AS MCC_SNFKG  FROM   tspl_milk_collection_mcc_detail
              LEFT OUTER JOIN tspl_milk_collection_mcc ON tspl_milk_collection_mcc.document_no = tspl_milk_collection_mcc_detail.document_no 
              LEFT OUTER JOIN tspl_mcc_master ON tspl_mcc_master.mcc_code = tspl_milk_collection_mcc_detail.mcc_code 
              LEFT OUTER JOIN tspl_bulk_route_master ON tspl_bulk_route_master.route_no = tspl_milk_collection_mcc.route_code
              WHERE  CONVERT(DATE, tspl_milk_collection_mcc.document_date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) AND CONVERT (DATE, tspl_milk_collection_mcc.document_date, 103)
             <= CONVERT (DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) ) xyz GROUP  BY  document_no) XXXFinal GROUP  BY  Tanker_No
             ORDER  BY Tanker_No asc "
                End If

                If rbtnDockDateWise.Checked Then
                    qry = "Select ROW_NUMBER() OVER(ORDER BY (Select 1)) As SNo, SDate , sum(Qty)Qty , sum(Milk_Wtd)Milk_Wtd ,convert(Decimal(18,2),(sum(Fat_KG * 100)/SUM(Milk_Wtd))) As FAT,convert(Decimal(18,2),(sum(SNF_KG * 100)/SUM(Milk_Wtd))) As SNF, sum(Fat_KG)Fat_KG , sum(SNF_KG)SNF_KG , max(Rate)Rate , max(Amt)Amt , max(VLC_Code)VLC_Code , max(DCS_Uploader_Code)DCS_Uploader_Code , max(DCS_Code)DCS_Code , max(DCS_Name)DCS_Name , max(BMC_Uploader_Code)BMC_Uploader_Code , max(BMC_Code)BMC_Code , max(BMC_Name)BMC_Name , max(Area)Area , max(Route_Code)Route_Code from (
                       " & qry & " )xx group by SDate order by SDate"

                ElseIf rbtnDockShiftWise.Checked Then
                    qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SNo, SDate,Shift , sum(Qty)Qty , sum(Milk_Wtd)Milk_Wtd ,convert(decimal(18,2),(sum(Fat_KG * 100)/SUM(Milk_Wtd))) as FAT,convert(decimal(18,2),(sum(SNF_KG * 100)/SUM(Milk_Wtd))) AS SNF, sum(Fat_KG)Fat_KG , sum(SNF_KG)SNF_KG , max(Rate)Rate , max(Amt)Amt , max(VLC_Code)VLC_Code , max(DCS_Uploader_Code)DCS_Uploader_Code , max(DCS_Code)DCS_Code , max(DCS_Name)DCS_Name , max(BMC_Uploader_Code)BMC_Uploader_Code , max(BMC_Code)BMC_Code , max(BMC_Name)BMC_Name , max(Area)Area , max(Route_Code)Route_Code from (
                       " & qry & " )xx group by SDate , Shift order by SDate, shift desc"
                End If
                dt = clsDBFuncationality.GetDataTable(qry)
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    Gv1.DataSource = dt
                    If rbtnDock.Checked = True OrElse rbtnDockDateWise.Checked OrElse rbtnDockShiftWise.Checked Then
                        SetGridFormationOFGV1Dock()
                    Else
                        SetGridFormationOFGV1()
                    End If

                    For ii As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.BestFitColumns()
                    Gv1.EnableFiltering = True
                    Dim summaryRowItem As New GridViewSummaryRowItem()

                    If rdbSummary.Checked = True Then
                        Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(EnteredQty)
                        Dim EnteredFatKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(EnteredFatKg)
                        Dim EnteredSnfKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(EnteredSnfKg)
                    End If
                    Dim MCCQty As New GridViewSummaryItem("MCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(MCCQty)
                    Dim MCCFatKg As New GridViewSummaryItem("MCC_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(MCCFatKg)
                    Dim MCCSnfKg As New GridViewSummaryItem("MCC_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(MCCSnfKg)

                    Dim DCSQty As New GridViewSummaryItem("DCS_Qty", "{0:F0}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(DCSQty)
                    Dim DCSFatKg As New GridViewSummaryItem("DCS_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(DCSFatKg)
                    Dim DCSSnfKg As New GridViewSummaryItem("DCS_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(DCSSnfKg)

                    Dim DiffQty As New GridViewSummaryItem("Diff_Qty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(DiffQty)
                    Dim DiffFatKg As New GridViewSummaryItem("Diff_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(DiffFatKg)
                    Dim DiffSnfKg As New GridViewSummaryItem("Diff_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(DiffSnfKg)
                    If rbtnDock.Checked = False AndAlso rbtnDockDateWise.Checked = False AndAlso rbtnDockShiftWise.Checked = False Then
                        If rdbSummary.Checked = True Then
                            '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
                            ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
                            Dim DiffEnteredVsMCCQty As New GridViewSummaryItem("DiffEnteredVsMCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCQty)
                            Dim DiffEnteredVsMCCFatKg As New GridViewSummaryItem("DiffEnteredVsMCC_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCFatKg)
                            Dim DiffEnteredVsMCCSnfKg As New GridViewSummaryItem("DiffEnteredVsMCC_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCSnfKg)
                        End If
                        If rbtnTranpoterGainLoss.Checked = True Then
                            Dim Entered_Qty As New GridViewSummaryItem("Entered_Qty", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_Qty)
                            Dim Entered_FATKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_FATKg)
                            Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Entered_SNFKg)
                            Dim DiffMCCVsEnteredQty As New GridViewSummaryItem("DiffMCCVsEntered_Qty", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffMCCVsEnteredQty)
                            Dim DiffEnteredVsMCCFatKg As New GridViewSummaryItem("DiffMCCVsEntered_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsMCCFatKg)
                            Dim DiffMCCVsEnteredSnfKg As New GridViewSummaryItem("DiffMCCVsEntered_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffMCCVsEnteredSnfKg)
                            Dim FATAmt As New GridViewSummaryItem("FAT_AMT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(FATAmt)
                            Dim SNFAmt As New GridViewSummaryItem("SNF_AMT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(SNFAmt)
                            Dim FATKG_Recovered As New GridViewSummaryItem("FATKG_Recovered", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(FATKG_Recovered)
                            Dim SNFKG_Recovered As New GridViewSummaryItem("SNFKG_Recovered", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(SNFKG_Recovered)
                            Dim Amount As New GridViewSummaryItem("AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Amount)
                        End If
                        If rdbTankerWise.Checked Then
                            'Dim Tanker_No As New GridViewSummaryItem("Tanker_No", "{0:F2}", "Total")
                            'summaryRowItem.Add(Tanker_No)
                            Dim EnteredQty As New GridViewSummaryItem("Entered_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredQty)
                            Dim EnteredFatKg As New GridViewSummaryItem("Entered_FATKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredFatKg)
                            Dim EnteredSnfKg As New GridViewSummaryItem("Entered_SNFKg", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(EnteredSnfKg)
                            Dim DiffDCSVsEnteredQty As New GridViewSummaryItem("DiffEnteredVsMCC_Qty", "{0:F0}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffDCSVsEnteredQty)
                            Dim DiffEnteredVsDCSFatKg As New GridViewSummaryItem("DiffEnteredVsMCC_FAT", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsDCSFatKg)
                            Dim DiffEnteredVsDCSSnfKg As New GridViewSummaryItem("DiffEnteredVsMCC_SNF", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(DiffEnteredVsDCSSnfKg)

                        End If

                        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    End If
                    ControlEnableDisable(False)

                    '=========================


                    'Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Document_Date] as [Document Date] format ""{0}: {1}"" Group By [Document_Date]"))
                    'Gv1.AutoExpandGroups = True
                    '    Gv1.ShowGroupPanel = False
                    '    Gv1.ShowRowHeaderColumn = False
                    '    Gv1.AllowAddNewRow = False
                    '    Gv1.AllowDeleteRow = False
                    '    Gv1.EnableFiltering = True
                    '    Gv1.ShowFilteringRow = True



                    '==========================
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                ReStoreGridLayout()
                If rbtnDock.Checked = False AndAlso rbtnDockDateWise.Checked = False AndAlso rbtnDockShiftWise.Checked = False Then
                    View()
                End If

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'PK_Id,Document_No,MCC_Code,MCC_NAME,
            'UploaderNo,Route_Code,ROUTE_NAME,Tanker_No,Vehicle_No,MCC_Qty,MCC_FAT,MCC_FATKG,MCC_SNF,MCC_SNFKG,
            'DCS_Qty,DCS_FAT,DCS_SNF,DCS_FATKG,DCS_SNF_KG
            If rdbDetails.Checked = True Then
                Gv1.Columns("PK_Id").HeaderText = "PK Id"
                Gv1.Columns("PK_Id").IsVisible = False
            End If

            If rbtnTranpoterGainLoss.Checked Then
                Gv1.Columns("Document_No").IsVisible = False
                Gv1.Columns("Vehicle_No").IsVisible = False
                Gv1.Columns("GainLoss_Code").HeaderText = "Gain Loss Code"
                Gv1.Columns("GainLoss_Code").IsVisible = False
                Gv1.Columns("Loss_FAT_Rate").HeaderText = "Loss FAT Rate"
                Gv1.Columns("Loss_FAT_Rate").IsVisible = False
                Gv1.Columns("Loss_SNF_Rate").HeaderText = "Loss SNF Rate"
                Gv1.Columns("Loss_SNF_Rate").IsVisible = False
                Gv1.Columns("Start_Date").HeaderText = "Start Date"
                Gv1.Columns("Start_Date").IsVisible = False
            End If
            If rdbTankerWise.Checked = False AndAlso rbtnBMCTankerCollection.Checked = False AndAlso rbtnRouteWise.Checked = False Then
                Gv1.Columns("Document_No").HeaderText = "Document No"
                Gv1.Columns("Route_Code").HeaderText = "Route Code"
                Gv1.Columns("ROUTE_NAME").HeaderText = "Route Name"
                Gv1.Columns("Document_Date").HeaderText = "Document Date"
                Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"
            End If

            If rbtnBMCTankerCollection.Checked = False AndAlso rbtnRouteWise.Checked = False Then
                Gv1.Columns("Tanker_No").HeaderText = "Tanker No"
            End If

            If rdbDetails.Checked = True Then
                Gv1.Columns("MCC_Code").HeaderText = "MCC Code"
                Gv1.Columns("MCC_NAME").HeaderText = "MCC Name"
                Gv1.Columns("UploaderNo").HeaderText = "UploaderNo"
            End If
            If rbtnBmcSummary.Checked = True Then
                Gv1.Columns("Tanker_No").HeaderText = "Vehicle No"
                Gv1.Columns("Tanker_No").IsVisible = False
                Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"
                Gv1.Columns("Vehicle_No").IsVisible = False
                Gv1.Columns("Route_Code").HeaderText = "Route Code"
                Gv1.Columns("Route_Code").IsVisible = False
                Gv1.Columns("ROUTE_NAME").HeaderText = "Route Name"
                Gv1.Columns("ROUTE_NAME").IsVisible = False
                Gv1.Columns("Document_No").HeaderText = "Document No"
                Gv1.Columns("Document_No").IsVisible = False
                Gv1.Columns("MCC_Code").HeaderText = "MCC Code"
                Gv1.Columns("MCC_Code").IsVisible = True
                Gv1.Columns("MCC_NAME").HeaderText = "MCC Name"
                Gv1.Columns("MCC_NAME").IsVisible = True
                Gv1.Columns("UploaderNo").HeaderText = "UploaderNo"
                Gv1.Columns("UploaderNo").IsVisible = True
            End If
            If rdbSummary.Checked = True Then
                '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
                Gv1.Columns("Entered_Qty").HeaderText = "Qty"
                Gv1.Columns("Entered_Qty").FormatString = "{0:n3}"

                Gv1.Columns("Entered_FAT").HeaderText = "FAT %"
                Gv1.Columns("Entered_FAT").FormatString = "{0:n2}"

                Gv1.Columns("Entered_SNF").HeaderText = "SNF %"
                Gv1.Columns("Entered_SNF").FormatString = "{0:n2}"

                Gv1.Columns("Entered_FATKg").HeaderText = "FAT KG"
                Gv1.Columns("Entered_SNFKg").HeaderText = "SNF KG"
                If FatSnfRoundOff = 0 Then
                    Gv1.Columns("Entered_FATKg").FormatString = "{0:n3}"
                    Gv1.Columns("Entered_SNFKg").FormatString = "{0:n3}"
                Else
                    Gv1.Columns("Entered_FATKg").FormatString = "{0:n2}"
                    Gv1.Columns("Entered_SNFKg").FormatString = "{0:n2}"
                End If


            ElseIf rbtnTranpoterGainLoss.Checked Then
                Gv1.Columns("Entered_Qty").HeaderText = "Qty"
                Gv1.Columns("Entered_Qty").FormatString = "{0:n3}"
                Gv1.Columns("Entered_FATKg").HeaderText = "FAT KG"
                Gv1.Columns("Entered_SNFKg").HeaderText = "SNF KG"
                Gv1.Columns("MCC_FATKG").HeaderText = "FAT KG"
                Gv1.Columns("MCC_SNFKG").HeaderText = "SNF KG"
                If FatSnfRoundOff = 0 Then
                    Gv1.Columns("Entered_FATKg").FormatString = "{0:n3}"
                    Gv1.Columns("Entered_SNFKg").FormatString = "{0:n3}"
                    Gv1.Columns("MCC_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("MCC_SNFKG").FormatString = "{0:n3}"

                Else
                    Gv1.Columns("Entered_FATKg").FormatString = "{0:n2}"
                    Gv1.Columns("Entered_SNFKg").FormatString = "{0:n2}"
                    Gv1.Columns("MCC_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("MCC_SNFKG").FormatString = "{0:n2}"

                End If

                Gv1.Columns("MCC_Qty").HeaderText = "Qty"
                Gv1.Columns("MCC_Qty").FormatString = "{0:n3}"
                'Gv1.Columns("MCC_FAT").HeaderText = "FAT %"
                'Gv1.Columns("MCC_FAT").FormatString = "{0:n2}"
                'Gv1.Columns("MCC_SNF").HeaderText = "SNF %"
                'Gv1.Columns("MCC_SNF").FormatString = "{0:n2}"



            ElseIf (rbtnDock.Checked = True) Then
                Gv1.Columns("Entered_Qty").HeaderText = "Qty"
                'Gv1.Columns("Entered_FAT").HeaderText = "FAT %"
                'Gv1.Columns("Entered_SNF").HeaderText = "SNF %"
                Gv1.Columns("Entered_FATKg").HeaderText = "FAT KG"
                Gv1.Columns("Entered_SNFKg").HeaderText = "SNF KG" 'IsVisible = False

                Gv1.Columns("Entered_Qty").IsVisible = False
                'Gv1.Columns("Entered_FAT").IsVisible = False
                'Gv1.Columns("Entered_SNF").IsVisible = False
                Gv1.Columns("Entered_FATKg").IsVisible = False
                Gv1.Columns("Entered_SNFKg").IsVisible = False

            ElseIf rdbTankerWise.Checked Then
                Gv1.Columns("Tanker_No").HeaderText = "Tankerno"

                Gv1.Columns("MCC_Qty").HeaderText = "Quantity"
                Gv1.Columns("MCC_Qty").FormatString = "{0:n0}"
                Gv1.Columns("MCC_FATKG").HeaderText = "KGFAT"
                Gv1.Columns("MCC_SNFKG").HeaderText = "KGSNF"
                Gv1.Columns("Entered_Qty").HeaderText = "Quantity"
                Gv1.Columns("Entered_Qty").FormatString = "{0:n0}"
                Gv1.Columns("Entered_FATKg").HeaderText = "KGFAT"
                Gv1.Columns("Entered_SNFKg").HeaderText = "KGSNF"
                Gv1.Columns("DiffEnteredVsMCC_Qty").HeaderText = "Quantity"
                Gv1.Columns("DiffEnteredVsMCC_Qty").FormatString = "{0:n0}"
                Gv1.Columns("DiffEnteredVsMCC_FAT").HeaderText = "KGFAT"
                Gv1.Columns("DiffEnteredVsMCC_SNF").HeaderText = "KGSNF"
                If FatSnfRoundOff = 0 Then
                    Gv1.Columns("MCC_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("MCC_SNFKG").FormatString = "{0:n3}"
                    Gv1.Columns("Entered_FATKg").FormatString = "{0:n3}"
                    Gv1.Columns("Entered_SNFKg").FormatString = "{0:n3}"
                    Gv1.Columns("DiffEnteredVsMCC_FAT").FormatString = "{0:n3}"
                    Gv1.Columns("DiffEnteredVsMCC_SNF").FormatString = "{0:n3}"

                Else
                    Gv1.Columns("MCC_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("MCC_SNFKG").FormatString = "{0:n2}"
                    Gv1.Columns("Entered_FATKg").FormatString = "{0:n2}"
                    Gv1.Columns("Entered_SNFKg").FormatString = "{0:n2}"
                    Gv1.Columns("DiffEnteredVsMCC_FAT").FormatString = "{0:n2}"
                    Gv1.Columns("DiffEnteredVsMCC_SNF").FormatString = "{0:n2}"

                End If



            End If


            If rbtnTranpoterGainLoss.Checked = False AndAlso rdbTankerWise.Checked = False AndAlso rbtnBMCTankerCollection.Checked = False AndAlso rbtnRouteWise.Checked = False Then
                Gv1.Columns("MCC_Qty").HeaderText = "Qty"
                Gv1.Columns("MCC_Qty").FormatString = "{0:n3}"
                Gv1.Columns("MCC_FAT").HeaderText = "FAT %"
                Gv1.Columns("MCC_FAT").FormatString = "{0:n2}"
                Gv1.Columns("MCC_SNF").HeaderText = "SNF %"
                Gv1.Columns("MCC_SNF").FormatString = "{0:n2}"
                Gv1.Columns("MCC_FATKG").HeaderText = "FAT KG"
                Gv1.Columns("MCC_SNFKG").HeaderText = "SNF KG"
                Gv1.Columns("DCS_FATKG").HeaderText = "FAT KG"
                Gv1.Columns("DCS_SNFKG").HeaderText = "SNF KG"
                Gv1.Columns("Diff_FATKG").HeaderText = "FAT KG"
                Gv1.Columns("Diff_SNFKG").HeaderText = "SNF KG"
                If FatSnfRoundOff = 0 Then
                    Gv1.Columns("MCC_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("MCC_SNFKG").FormatString = "{0:n3}"
                    Gv1.Columns("DCS_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("DCS_SNFKG").FormatString = "{0:n3}"
                    Gv1.Columns("Diff_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("Diff_SNFKG").FormatString = "{0:n3}"

                Else
                    Gv1.Columns("MCC_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("MCC_SNFKG").FormatString = "{0:n2}"
                    Gv1.Columns("DCS_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("DCS_SNFKG").FormatString = "{0:n2}"
                    Gv1.Columns("Diff_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("Diff_SNFKG").FormatString = "{0:n2}"

                End If

                Gv1.Columns("DCS_Qty").HeaderText = "Qty"
                Gv1.Columns("DCS_Qty").FormatString = "{0:n3}"
                Gv1.Columns("DCS_FAT").HeaderText = "FAT %"
                Gv1.Columns("DCS_FAT").FormatString = "{0:n2}"
                Gv1.Columns("DCS_SNF").HeaderText = "SNF %"
                Gv1.Columns("DCS_SNF").FormatString = "{0:n2}"

                'Diff
                Gv1.Columns("Diff_Qty").HeaderText = "Qty"
                Gv1.Columns("Diff_Qty").FormatString = "{0:n3}"
                Gv1.Columns("Diff_FAT").HeaderText = "FAT %"
                Gv1.Columns("Diff_FAT").FormatString = "{0:n2}"
                Gv1.Columns("Diff_SNF").HeaderText = "SNF %"
                Gv1.Columns("Diff_SNF").FormatString = "{0:n2}"

            End If
            If rbtnTranpoterGainLoss.Checked = True Then
                Gv1.Columns("DiffMCCVsEntered_Qty").HeaderText = "Qty"
                Gv1.Columns("DiffMCCVsEntered_Qty").FormatString = "{0:n3}"

                Gv1.Columns("DiffMCCVsEntered_FATKG").HeaderText = "FAT Difference"
                Gv1.Columns("FATKG_Recovered").HeaderText = "FAT KG to be Recovered"
                Gv1.Columns("DiffMCCVsEntered_SNFKG").HeaderText = "SNF Difference"
                Gv1.Columns("SNFKG_Recovered").HeaderText = "SNF KG to be Recovered"
                If FatSnfRoundOff = 0 Then
                    Gv1.Columns("DiffMCCVsEntered_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("FATKG_Recovered").FormatString = "{0:n3}"
                    Gv1.Columns("DiffMCCVsEntered_SNFKG").FormatString = "{0:n3}"
                    Gv1.Columns("SNFKG_Recovered").FormatString = "{0:n3}"

                Else
                    Gv1.Columns("DiffMCCVsEntered_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("FATKG_Recovered").FormatString = "{0:n2}"
                    Gv1.Columns("DiffMCCVsEntered_SNFKG").FormatString = "{0:n2}"
                    Gv1.Columns("SNFKG_Recovered").FormatString = "{0:n2}"

                End If
                Gv1.Columns("FAT_Tolerence").HeaderText = "Allow FAT Tolerence"
                Gv1.Columns("FAT_Tolerence").FormatString = "{0:n3}"

                Gv1.Columns("FAT_AMT").HeaderText = "FAT Amount Recovered"
                Gv1.Columns("FAT_AMT").FormatString = "{0:n2}"

                Gv1.Columns("SNF_Tolerence").HeaderText = "Allow SNF Tolerence"
                Gv1.Columns("SNF_Tolerence").FormatString = "{0:n3}"

                Gv1.Columns("SNF_AMT").HeaderText = "SNF Amount Recovered"
                Gv1.Columns("SNF_AMT").FormatString = "{0:n2}"

                Gv1.Columns("AMOUNT").HeaderText = "AMOUNT"
                Gv1.Columns("AMOUNT").FormatString = "{0:n2}"
            End If
            If rdbSummary.Checked = True Then

                Gv1.Columns("DiffEnteredVsMCC_Qty").HeaderText = "Qty"
                Gv1.Columns("DiffEnteredVsMCC_Qty").FormatString = "{0:n3}"
                Gv1.Columns("DiffEnteredVsMCC_FAT").HeaderText = "FAT %"
                Gv1.Columns("DiffEnteredVsMCC_FAT").FormatString = "{0:n2}"
                Gv1.Columns("DiffEnteredVsMCC_SNF").HeaderText = "SNF %"
                Gv1.Columns("DiffEnteredVsMCC_SNF").FormatString = "{0:n2}"
                Gv1.Columns("DiffEnteredVsMCC_FATKG").HeaderText = "FAT KG"
                Gv1.Columns("DiffEnteredVsMCC_SNFKG").HeaderText = "SNF KG"
                If FatSnfRoundOff = 0 Then
                    Gv1.Columns("DiffEnteredVsMCC_FATKG").FormatString = "{0:n3}"
                    Gv1.Columns("DiffEnteredVsMCC_SNFKG").FormatString = "{0:n3}"

                Else
                    Gv1.Columns("DiffEnteredVsMCC_FATKG").FormatString = "{0:n2}"
                    Gv1.Columns("DiffEnteredVsMCC_SNFKG").FormatString = "{0:n2}"

                End If



            End If
            '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
            ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
            If rbtnRouteWise.Checked = True Then
                Gv1.Columns("ROUTE_NO").HeaderText = "Route Code"
                Gv1.Columns("ROUTE_NAME").HeaderText = "Route Name"
                Gv1.Columns("MCC_Qty").HeaderText = "Quantity"
                Gv1.Columns("MCC_Qty").FormatString = "{0:n0}"
                Gv1.Columns("MCC_FAT_Per").HeaderText = "%FAT"
                Gv1.Columns("MCC_SNF_Per").HeaderText = "%SNF"
                Gv1.Columns("MCC_FATKG").HeaderText = "KGFAT"
                Gv1.Columns("MCC_FATKG").FormatString = "{0:n2}"
                Gv1.Columns("MCC_SNFKG").HeaderText = "KGSNF"
                Gv1.Columns("MCC_SNFKG").FormatString = "{0:n2}"
                Gv1.Columns("Entered_Qty").HeaderText = "Quantity"
                Gv1.Columns("Entered_Qty").FormatString = "{0:n0}"
                Gv1.Columns("Entered_FAT_Per").HeaderText = "%FAT"
                Gv1.Columns("Entered_SNF_Per").HeaderText = "%SNF"
                Gv1.Columns("Entered_FATKg").HeaderText = "KGFAT"
                Gv1.Columns("Entered_SNFKg").HeaderText = "KGSNF"
                Gv1.Columns("Entered_FATKg").FormatString = "{0:n2}"
                Gv1.Columns("Entered_SNFKg").FormatString = "{0:n2}"
                Gv1.Columns("DiffEnteredVsMCC_Qty").HeaderText = "Quantity"
                Gv1.Columns("DiffEnteredVsMCC_Qty").FormatString = "{0:n0}"
                Gv1.Columns("DiffEnteredVsMCC_FAT").HeaderText = "KGFAT"
                Gv1.Columns("DiffEnteredVsMCC_SNF").HeaderText = "KGSNF"
                Gv1.Columns("DiffEnteredVsMCC_FAT").FormatString = "{0:n2}"
                Gv1.Columns("DiffEnteredVsMCC_SNF").FormatString = "{0:n2}"
            End If
            Gv1.Columns(ii).BestFit()
        Next

    End Sub

    Sub SetGridFormationOFGV1Multiple()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True

            Gv1.Columns("Document_No").HeaderText = "Document No."
            Gv1.Columns("Document_Date").HeaderText = "Document Date"
            Gv1.Columns("MCC_Code").HeaderText = "MCC Code"
            Gv1.Columns("MCC_NAME").HeaderText = "MCC NAME"
            Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "Uploader No."
            Gv1.Columns("Route_Code").HeaderText = "Route Code"
            Gv1.Columns("ROUTE_NAME").HeaderText = "Route Description"
            Gv1.Columns("Tanker_No").HeaderText = "Tanker No."
            Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No."
            Gv1.Columns("Entered_Qty").HeaderText = "Entered Qty"
            Gv1.Columns("Entered_Qty").FormatString = "{0:n2}"
            Gv1.Columns("Entered_FATKg").HeaderText = "FATKG"
            Gv1.Columns("Entered_FATKg").FormatString = "{0:n2}"
            Gv1.Columns("Entered_SNFKg").HeaderText = "SNFKG"
            Gv1.Columns("Entered_SNFKg").FormatString = "{0:n2}"
            Gv1.Columns("CLR").HeaderText = "CLR"
            Gv1.Columns("CLR").FormatString = "{0:n0}"
            Gv1.Columns("FAT").HeaderText = "FAT"
            Gv1.Columns("FAT").FormatString = "{0:n2}"
            Gv1.Columns("VLC_Code").HeaderText = "VLC Code"
            Gv1.Columns("VLC_Name").HeaderText = "VLC Name"
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "Uploader No."
            Gv1.Columns("Qty").HeaderText = "Qty"
            Gv1.Columns("Qty").FormatString = "{0:n2}"
            Gv1.Columns("FATKG").HeaderText = "FATKG"
            Gv1.Columns("FATKG").FormatString = "{0:n2}"
            Gv1.Columns("SNFKG").HeaderText = "SNFKG"
            Gv1.Columns("SNFKG").FormatString = "{0:n2}"
            Gv1.Columns("DCS_FAT").HeaderText = "FAT%"
            Gv1.Columns("DCS_FAT").FormatString = "{0:n2}"
            Gv1.Columns("DCS_SNF").HeaderText = "SNF%"
            Gv1.Columns("DCS_SNF").FormatString = "{0:n2}"
            Gv1.Columns("Diff_Qty").HeaderText = "Qty"
            Gv1.Columns("Diff_Qty").FormatString = "{0:n2}"
            Gv1.Columns("Diff_FAT").HeaderText = "FATKG"
            Gv1.Columns("Diff_FAT").FormatString = "{0:n2}"
            Gv1.Columns("Diff_SNF").HeaderText = "SNFKG"
            Gv1.Columns("Diff_SNF").FormatString = "{0:n2}"
        Next
    End Sub

    Sub SetGridFormationOFGV1Collection()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True

            Gv1.Columns("Document_No").HeaderText = "Document No."
            Gv1.Columns("Document_Date").HeaderText = "Document Date"
            Gv1.Columns("MCC_Code").HeaderText = "MCC Code"
            Gv1.Columns("MCC_NAME").HeaderText = "MCC NAME"
            Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "Uploader No."
            Gv1.Columns("Route_Code").HeaderText = "Route Code"
            Gv1.Columns("ROUTE_NAME").HeaderText = "Route Description"
            Gv1.Columns("Tanker_No").HeaderText = "Tanker No."
            Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No."
            Gv1.Columns("Entered_Qty").HeaderText = "Entered Qty"
            Gv1.Columns("Entered_Qty").FormatString = "{0:n2}"
            Gv1.Columns("Entered_FATKg").HeaderText = "FATKG"
            Gv1.Columns("Entered_FATKg").FormatString = "{0:n2}"
            Gv1.Columns("Entered_SNFKg").HeaderText = "SNFKG"
            Gv1.Columns("Entered_SNFKg").FormatString = "{0:n2}"
            Gv1.Columns("CLR").HeaderText = "CLR"
            Gv1.Columns("CLR").FormatString = "{0:n2}"
            Gv1.Columns("DCS_FAT").HeaderText = "FAT"
            Gv1.Columns("DCS_FAT").FormatString = "{0:n2}"
            Gv1.Columns("DCS_SNFPER").HeaderText = "SNF%"
            Gv1.Columns("DCS_SNFPER").FormatString = "{0:n2}"
            Gv1.Columns("VLC_Code").HeaderText = "VLC Code"
            Gv1.Columns("VLC_Name").HeaderText = "VLC Name"
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "Uploader No."
            Gv1.Columns("Collection_Date").HeaderText = "Collection Date"
            Gv1.Columns("Qty").HeaderText = "Qty"
            Gv1.Columns("Qty").FormatString = "{0:n2}"
            Gv1.Columns("FAT").HeaderText = "FAT"
            Gv1.Columns("FAT").FormatString = "{0:n2}"
            Gv1.Columns("SNF").HeaderText = "CLR"
            Gv1.Columns("SNF").FormatString = "{0:n2}"
            Gv1.Columns("FATKG").HeaderText = "FATKG"
            Gv1.Columns("FATKG").FormatString = "{0:n2}"
            Gv1.Columns("SNFKG").HeaderText = "SNFKG"
            Gv1.Columns("SNFKG").FormatString = "{0:n2}"
            Gv1.Columns("SNFPER").HeaderText = "SNF%"
            Gv1.Columns("SNFPER").FormatString = "{0:n2}"
            'Gv1.Columns("DCS_FAT").HeaderText = "DCS_FAT"
            'Gv1.Columns("DCS_FAT").FormatString = "{0:n2}"
            'Gv1.Columns("DCS_SNF").HeaderText = "DCS_SNF"
            'Gv1.Columns("DCS_SNF").FormatString = "{0:n2}"
        Next
    End Sub
    Sub SetGridFormationOFGV1Dock()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True

            Gv1.Columns("SNo").HeaderText = "S.No."
            Gv1.Columns("SDate").HeaderText = "Date"

            If rbtnDockDateWise.Checked = False Then
                Gv1.Columns("Shift").HeaderText = "Shift"
            End If
            Gv1.Columns("Milk_Wtd").HeaderText = "QTY"

            Gv1.Columns("BMC_Uploader_Code").HeaderText = "BMC Uploader Code"
            Gv1.Columns("BMC_Code").HeaderText = "BMC Code"
            Gv1.Columns("BMC_Code").IsVisible = False

            Gv1.Columns("BMC_Name").HeaderText = "BMC Name"
            Gv1.Columns("BMC_Name").IsVisible = False
            If AreaWiseBilling Then
                Gv1.Columns("Area").HeaderText = "Area"
            End If
            Gv1.Columns("Route_Code").HeaderText = "Route No"
            Gv1.Columns("Route_Code").IsVisible = True

            Gv1.Columns("DCS_Uploader_Code").HeaderText = "DCS Uploader Code"
            Gv1.Columns("DCS_Code").HeaderText = "DCS Code"
            Gv1.Columns("DCS_Code").IsVisible = False

            Gv1.Columns("DCS_Name").HeaderText = "DCS Name"
            Gv1.Columns("DCS_Name").IsVisible = False


            Gv1.Columns("Qty").HeaderText = "Can"
            Gv1.Columns("Qty").FormatString = "{0:n3}"
            Gv1.Columns("FAT").HeaderText = "FAT %"
            Gv1.Columns("FAT").FormatString = "{0:n2}"
            Gv1.Columns("SNF").HeaderText = "SNF %"
            Gv1.Columns("SNF").FormatString = "{0:n2}"
            Gv1.Columns("FAT_KG").HeaderText = "FAT KG"
            Gv1.Columns("SNF_KG").HeaderText = "SNF KG"

            If FatSnfRoundOff = 0 Then
                Gv1.Columns("FAT_KG").FormatString = "{0:n3}"
                Gv1.Columns("SNF_KG").FormatString = "{0:n3}"
            Else
                Gv1.Columns("FAT_KG").FormatString = "{0:n2}"
                Gv1.Columns("SNF_KG").FormatString = "{0:n2}"

            End If


            Gv1.Columns("RATE").HeaderText = "RATE"
            Gv1.Columns("RATE").FormatString = "{0:n3}"
            Gv1.Columns("RATE").IsVisible = False

            Gv1.Columns("Amt").HeaderText = "Amt"
            Gv1.Columns("Amt").FormatString = "{0:n3}"
            Gv1.Columns("Amt").IsVisible = False

            Gv1.Columns(ii).BestFit()
        Next


        Dim EnteredQty As New GridViewSummaryItem("Milk_Wtd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(EnteredQty)
        Dim EnteredFatKg As New GridViewSummaryItem("Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(EnteredFatKg)
        Dim EnteredSnfKg As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(EnteredSnfKg)

        Dim summaryItem1 As New GridViewSummaryItem()
        summaryItem1.FormatString = "{0:F2}"
        summaryItem1.Name = "FAT"
        summaryItem1.AggregateExpression = "sum(Fat_KG)*100/sum(Milk_Wtd)"
        summaryRowItem.Add(summaryItem1)

        Dim summaryItem2 As New GridViewSummaryItem()
        summaryItem2.FormatString = "{0:F2}"
        summaryItem2.Name = "SNF"
        summaryItem2.AggregateExpression = "sum(SNF_KG)*100/sum(Milk_Wtd)"
        summaryRowItem.Add(summaryItem2)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub


    Sub View()

        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())

            If rdbDetails.Checked = True Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("UploaderNo").Name)
            End If
            If rbtnBmcSummary.Checked = True Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("UploaderNo").Name)
            End If
            If rdbTankerWise.Checked = False AndAlso rbtnBMCTankerCollection.Checked = False AndAlso rbtnRouteWise.Checked = False Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Document_No").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Document_Date").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("ROUTE_NAME").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker_No").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Vehicle_No").Name)
            End If

            If rbtnTranpoterGainLoss.Checked Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("GainLoss_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Loss_FAT_Rate").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Loss_SNF_Rate").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Start_Date").Name)
            End If


            '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
            ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
            If rdbSummary.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Entered Data"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FAT").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNF").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FAT").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNF").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNF").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FATKG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data (BMC-DCS)"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FAT").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNF").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FATKG").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data(Entered - BMC)"))
                view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_Qty").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_FAT").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_SNF").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_FATKG").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_SNFKG").Name)

            ElseIf rbtnTranpoterGainLoss.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Entered Data"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FAT").Name)
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNF").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FAT").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNF").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data(BMC - Entered)"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DiffMCCVsEntered_Qty").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("FAT"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffMCCVsEntered_FATKG").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("FAT_Tolerence").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("FATKG_Recovered").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("FAT_AMT").Name)


                view.ColumnGroups.Add(New GridViewColumnGroup("SNF"))
                view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffMCCVsEntered_SNFKG").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_Tolerence").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("SNFKG_Recovered").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_AMT").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Total Recovery Amount"))
                view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("AMOUNT").Name)

            ElseIf rdbMultiple.Checked = True Then
                'view.ColumnGroups.Add(New GridViewColumnGroup(""))
                'view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Document_No").Name)
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Document_Date").Name)

                'view.ColumnGroups.Add(New GridViewColumnGroup(""))
                'view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Mcc_Code_VLC_Uploader").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("ROUTE_NAME").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker_No").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Vehicle_No").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("FAT").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("CLR").Name)


                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Code").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Name").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Code_VLC_Uploader").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("FATKG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SNFKG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNF").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data (BMC-DCS)"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FAT").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNF").Name)
                'view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FATKG").Name)
                'view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNFKG").Name)

            ElseIf rdbCollectionWise.Checked = True Then
                'view.ColumnGroups.Add(New GridViewColumnGroup(""))
                'view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Document_No").Name)
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Document_Date").Name)

                'view.ColumnGroups.Add(New GridViewColumnGroup(""))
                'view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Mcc_Code_VLC_Uploader").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("ROUTE_NAME").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker_No").Name)
                'view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Vehicle_No").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("CLR").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("FAT").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("SNFPer").Name)


                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Code").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Name").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Code_VLC_Uploader").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Collection_Date").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SNF").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("FATKG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SNFKG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNFPER").Name)
                'view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
                'view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNF").Name)


            ElseIf rdbTankerWise.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker_No").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Details Received from Tanker at BMC"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Details Received at Dairy Dock"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_FAT").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_SNF").Name)

            ElseIf rbtnRouteWise.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("S.No.").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Date").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("ROUTE_NO").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("ROUTE_NAME").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("As per Tanker Receipt as Dairy Dock"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FAT_Per").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNF_Per").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("As per Milk Despatch"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FAT_Per").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNF_Per").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_FAT").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_SNF").Name)
            ElseIf rbtnBMCTankerCollection.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("BMC Date").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Route No").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Route Name").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC Name").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("VLC Uploader Code").Name)


                view.ColumnGroups.Add(New GridViewColumnGroup("TANKER Data"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker Qty").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker FAT(%)").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker SNF(%)").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker FAT KG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker SNF KG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("FAT(%)").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SNF(%)").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("FAT KG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SNF KG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data (TANKER-BMC)"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff. Qty").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff. FAT(%)").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff. SNF(%)").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff. FAT KG").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff. SNF KG").Name)
            Else
                view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FAT").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNF").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_Qty").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNF").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FATKG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data (BMC-DCS)"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Qty").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FAT").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNF").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FATKG").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNFKG").Name)

            End If
            Gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDailyQtyReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(fromDate.Text) + "  To " + clsCommon.myCstr(dtpToDate.Text))
                If rdbSummary.Checked = True Then
                    arrHeader.Add("Report Type : " & "Summary")
                End If
                If rdbDetails.Checked = True Then
                    arrHeader.Add("Report Type : " & "Details")
                End If
                If clsCommon.CompairString(cboItemType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                    arrHeader.Add("Document Status : " & "Posted")
                ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                    arrHeader.Add("Document Status : " & "Unposted")
                Else
                    arrHeader.Add("Document Status : " & "All")
                End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                If rbtnDock.Checked Then

                    transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                Else
                    transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, True)
                End If


                'clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If rdbTankerWise.Checked Then

                    arrHeader.Add("                                   " & objCommonVar.CurrentCompanyName)
                    arrHeader.Add("                                       Tanker KGFAT-KGSNF-Quantity of period " & clsCommon.myCstr(fromDate.Text) + "-" + clsCommon.myCstr(dtpToDate.Text))
                Else
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                    arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDailyQtyReport & "'"))
                    arrHeader.Add("Date : " & clsCommon.myCstr(fromDate.Text) + "  To " + clsCommon.myCstr(dtpToDate.Text))

                End If

                If rdbSummary.Checked = True Then
                    arrHeader.Add("Report Type : " & "Summary")
                End If
                If rdbDetails.Checked = True Then
                    arrHeader.Add("Report Type : " & "Details")
                End If
                If clsCommon.CompairString(cboItemType.SelectedValue, "Posted") = CompairStringResult.Equal Then
                    arrHeader.Add("Document Status : " & "Posted")
                ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                    arrHeader.Add("Document Status : " & "Unposted")
                ElseIf rdbTankerWise.Checked = False Then
                    arrHeader.Add("Document Status : " & "All")
                End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                If rdbTankerWise.Checked Then
                    Dim style As New GridPrintStyle()
                    style.PrintGrouping = True
                    style.HeaderCellBackColor = Color.White
                    style.GroupRowBackColor = Color.White
                    style.SummaryCellBackColor = Color.White
                    style.PrintSummaries = True
                    Gv1.PrintStyle = style

                    Dim doc As New clsMyPrintDocument()

                    doc.Margins.Top = 50
                    doc.Margins.Bottom = 50
                    doc.Margins.Left = 50
                    doc.Margins.Right = 50
                    doc.HeaderHeight = 90
                    doc.Landscape = True
                    doc.AssociatedObject = Gv1

                    doc.DocumentName = objCommonVar.CurrentCompanyName

                    doc.MiddleHeader = objCommonVar.CurrentCompanyName + Environment.NewLine + "    Tanker KGFAT-KGSNF-Quantity of period " & clsCommon.myCstr(fromDate.Text) + "-" + clsCommon.myCstr(dtpToDate.Text)
                    doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                    doc.AssociatedObject = Gv1

                    doc.RightFooter = "Page [Page #] Of [Total Pages]"

                    Dim dialog As New RadPrintPreviewDialog
                    dialog.Document = doc
                    dialog.ToolMenu.Visible = True
                    dialog.Show()

                    doc.Print()
                    'doc = Nothing
                Else
                    'Gv1.Columns("Milk_Wtd").Width = 60
                    clsCommon.MyExportToPDF("Daily Quantity Report", Gv1, arrHeader, "", PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub rptMccCollectionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE() 'ToDate.Value.AddMonths(-1)
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        'fndArea.Visible = AreaWiseBilling
        'lblArea.Visible = AreaWiseBilling
        LoadShiftName()
        TranspoterBoxhandler(False)
        Reset()

    End Sub
    Sub LoadShiftName()
        cboItemType.DataSource = LoadShift()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 0
    End Sub
    Public Function LoadShift() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "ALL"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Posted"
        dr("Name") = "Posted"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Unposted"
        dr("Name") = "Unposted"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = " select TSPL_MCC_MASTER.MCC_Code as Code , TSPL_MCC_MASTER.MCC_NAME as Name from TSPL_MCC_MASTER "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelMCC@MCCCollectionRPT", qry, "Code", "Code", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub rbtnDock_Click(sender As Object, e As EventArgs) Handles rbtnDock.Click
        MyLabel4.Visible = True
        txtMCC_Code.Visible = True
        MyLabel5.Visible = False
        TxtTankerNo.Visible = False
        If AreaWiseBilling Then
            fndArea.Visible = True
            lblArea.Visible = True
        End If
        TranspoterBoxhandler(False)
    End Sub

    Private Sub txtMCC_Code__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC_Code._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code,MCC_NAME as Name from TSPL_MCC_MASTER"
            txtMCC_Code.Value = clsCommon.ShowSelectForm("vbaMccm", qry, "Code", "", txtMCC_Code.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rdbDetails_Click(sender As Object, e As EventArgs) Handles rdbDetails.Click
        MyLabel4.Visible = False
        txtMCC_Code.Visible = False
        MyLabel5.Visible = False
        TxtTankerNo.Visible = False
        TranspoterBoxhandler(False)
    End Sub

    Private Sub rdbSummary_Click(sender As Object, e As EventArgs) Handles rdbSummary.Click
        MyLabel4.Visible = False
        txtMCC_Code.Visible = False
        MyLabel5.Visible = False
        TxtTankerNo.Visible = False
        TranspoterBoxhandler(False)
    End Sub

    Private Sub rbtnTranpoterGainLoss_Click(sender As Object, e As EventArgs) Handles rbtnTranpoterGainLoss.Click
        MyLabel4.Visible = False
        txtMCC_Code.Visible = False
        MyLabel5.Visible = True
        TxtTankerNo.Visible = True
        fndArea.Visible = False
        lblArea.Visible = False
        TranspoterBoxhandler(True)
    End Sub

    Sub TranspoterBoxhandler(ByVal flag As Boolean)
        lblToleranceFAT.Visible = flag
        lblToleranceSNF.Visible = flag
        txtToleranceFat.Visible = flag
        txtToleranceSNF.Visible = flag
    End Sub

    Private Sub TxtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + TxtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    TxtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            TxtTankerNo.Value = clsfrmTankerMaster.GetFinder("", TxtTankerNo.Value, isButtonClicked)
            'txtVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" & TxtTankerNo.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnTranpoterGainLoss_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnTranpoterGainLoss.CheckedChanged
        If rbtnTranpoterGainLoss.Checked = True Then
            MyLabel5.Visible = True
            TxtTankerNo.Visible = True
            TxtTankerNo.Value = ""
        Else
            MyLabel5.Visible = False
            TxtTankerNo.Visible = False
            TxtTankerNo.Value = ""
            fndArea.Visible = False
            lblArea.Visible = False
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = Nothing
            Dim Area As String = Nothing
            Area = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from TSPL_MCC_MASTER WHERE Area_Location_Code = '" + fndArea.Value + "' "))

            If rbtnDockSummary.Checked Then
                qry = "Select '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As [FromDate],'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "' As [ToDate],'" & objCommonVar.CurrentUser & "' as User_Name,xxx.SNO,Convert(Date,(xxx.Shift_Date),103)Shift_Date,Convert(int,xxx.Uploader_Code)Uploader_Code,Max(xxx.VLC_Name)VLC_Name,Max(xxx.MCC)MCC,'" + Area + "' as Area,
                        xxx.Shift,Max(xxx.Bulk_Route_Code)Bulk_Route_Code,Case When Max(xxx.Dock_Collection_Milk_Type)='M' Then 'Mix' Else '' End As Dock_Collection_Milk_Type,Max(xxx.Reject_Type)Reject_Type,
                        Sum(xxx.No_Of_Cans)No_Of_Cans,                        
                        Sum(IsNull(xxx.No_Of_Cans,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) As [Sweet_No_Of_Cans],
                        Sum(IsNull(xxx.Milk_Weight,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_Weight],
                        Max(IsNull(xxx.FAT,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_FAT],
                        Sum(IsNull(xxx.SNF,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_SNF],
                        Sum(IsNull(xxx.No_Of_Cans,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_No_Of_Cans],
                        Sum(IsNull(xxx.Milk_Weight,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_Weight],
                        Sum(IsNull(xxx.FAT,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_FAT],
                        Sum(IsNull(xxx.SNF,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_SNF],
                        Sum(IsNull(xxx.No_Of_Cans,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_No_Of_Cans],
                        Sum(IsNull(xxx.Milk_Weight,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_Weight],
                        Sum(IsNull(xxx.FAT,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as[CURD_Milk_FAT],
                        Sum(IsNull(xxx.SNF,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_SNF],  
                        Max(xxx.Comp_Name)Comp_Name from
                        (SELECT TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code],TSPL_MCC_MASTER.MCC_NAME As MCC,TSPL_COMPANY_MASTER.Comp_Name 
                        FROM TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL 
                        Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_HEAD On TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code  
                        Left Outer Join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        Left outer join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "'
                        where Convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103)  And Convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<=convert (date,'" + dtpToDate.Value + "',103)  "

                If AreaWiseBilling Then
                    If clsCommon.myLen(fndArea.Value) > 0 Then
                        qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    End If
                Else
                    If txtMCC_Code.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC_Code.Value) > 0 Then
                        qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC_Code.Value) + "'"
                    End If
                End If

                'If txtMCC_Code.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC_Code.Value) > 0 Then
                '    qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC_Code.Value) + "'"
                'End If
                If ddlShift.SelectedValue IsNot Nothing AndAlso clsCommon.CompairString(ddlShift.SelectedValue, "Morning") = CompairStringResult.Equal Then
                    qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift='M'"
                ElseIf ddlShift.SelectedValue IsNot Nothing AndAlso clsCommon.CompairString(ddlShift.SelectedValue, "Evening") = CompairStringResult.Equal Then
                    qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift='E'"
                End If
                qry += " ) xxx Group By xxx.Uploader_Code,xxx.Shift_Date,xxx.Shift,xxx.SNO ORDER BY  Convert(int,xxx.Uploader_Code)"
            ElseIf rbtnSummary.Checked Then
                qry = "Select '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As [FromDate],'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "' As [ToDate],'" & objCommonVar.CurrentUser & "' as User_Name,Convert(Date,(xxx.Shift_Date),103)Shift_Date,Max(xxx.VLC_Name)VLC_Name,Max(xxx.MCC)MCC,'" + Area + "' as Area,
                        xxx.Shift,Max(xxx.Bulk_Route_Code)Bulk_Route_Code,Case When Max(xxx.Dock_Collection_Milk_Type)='M' Then 'Mix' Else '' End As Dock_Collection_Milk_Type,Max(xxx.Reject_Type)Reject_Type,
                        Sum(xxx.No_Of_Cans)No_Of_Cans,
                        Sum(IsNull(xxx.No_Of_Cans,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_No_Of_Cans],
                        Sum(IsNull(xxx.Milk_Weight,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_Weight],
                        Sum(IsNull(xxx.FAT,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_FAT],
                        Sum(((IsNull(xxx.FAT,0)*IsNull(xxx.Milk_Weight,0))/100) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_FATKG],
                        Sum(IsNull(xxx.SNF,0) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_SNF],
                        Sum(((IsNull(xxx.SNF,0)*IsNull(xxx.Milk_Weight,0))/100) * case when Isnull(xxx.Reject_Type,'')='' then 1 else 0 end  ) as [Sweet_Milk_SNFKG],
                        Sum(IsNull(xxx.No_Of_Cans,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_No_Of_Cans],
                        Sum(IsNull(xxx.Milk_Weight,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_Weight],
                        Sum(IsNull(xxx.FAT,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_FAT],
                        Sum(((IsNull(xxx.FAT,0)*IsNull(xxx.Milk_Weight,0))/100) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_FATKG],
                        Sum(IsNull(xxx.SNF,0) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_SNF],
                        Sum(((IsNull(xxx.SNF,0)*IsNull(xxx.Milk_Weight,0))/100) * case when Isnull(xxx.Reject_Type,'')='SOUR' then 1 else 0 end  ) as [SOUR_Milk_SNFKG],
                        Sum(IsNull(xxx.No_Of_Cans,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_No_Of_Cans],
                        Sum(IsNull(xxx.Milk_Weight,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_Weight],
                        Sum(IsNull(xxx.FAT,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_FAT],
                        Sum(((IsNull(xxx.FAT,0)*IsNull(xxx.Milk_Weight,0))/100) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_FATKG],
                        Sum(IsNull(xxx.SNF,0) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_SNF],
                        Sum(((IsNull(xxx.SNF,0)*IsNull(xxx.Milk_Weight,0))/100) * case when Isnull(xxx.Reject_Type,'')='CURD' then 1 else 0 end  ) as [CURD_Milk_SNFKG],
                        Max(xxx.Comp_Name)Comp_Name from
                        (SELECT TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code],TSPL_MCC_MASTER.MCC_NAME As MCC,TSPL_COMPANY_MASTER.Comp_Name 
                        FROM TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL 
                        Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_HEAD On TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code  
                        Left Outer Join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        Left outer join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "'
                        where Convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103)  And Convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<=convert (date,'" + dtpToDate.Value + "',103)  "

                If AreaWiseBilling Then
                    If clsCommon.myLen(fndArea.Value) > 0 Then
                        qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    End If
                Else
                    If txtMCC_Code.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC_Code.Value) > 0 Then
                        qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC_Code.Value) + "'"
                    End If
                End If

                'If txtMCC_Code.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC_Code.Value) > 0 Then
                '    qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC_Code.Value) + "'"
                'End If
                If ddlShift.SelectedValue IsNot Nothing AndAlso clsCommon.CompairString(ddlShift.SelectedValue, "Morning") = CompairStringResult.Equal Then
                    qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift='M'"
                ElseIf ddlShift.SelectedValue IsNot Nothing AndAlso clsCommon.CompairString(ddlShift.SelectedValue, "Evening") = CompairStringResult.Equal Then
                    qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift='E'"
                End If
                qry += " ) xxx Group By xxx.Shift_Date,xxx.Shift ORDER BY xxx.Shift_Date"
            ElseIf rbtnBMCDock.Checked Then
                qry = "Select '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As [From Date],'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "' As [To Date],'" & objCommonVar.CurrentUser & "' as User_Name,Max(Convert(Date,xxx.Shift_Date,103))Shift_Date,Case When xxx.Shift='M' Then 'Morning' Else 'Evening' End As Shift,"
                If AreaWiseBilling = True Then
                    qry += " Max(xxx.Location_Desc)MCC_NAME,"
                Else
                    qry += " Max(xxx.MCC_NAME)MCC_NAME,"
                End If
                qry += "Max(xxx.VLC)VLC,'" + Area + "' as Area,Max(xxx.[VLC Name])[VLC Name],Max(xxx.Route)Route,(Convert(int,xxx.[Route Code]))[Route Code],Sum(xxx.[No of Cans])[No of Cans],Sum(xxx.[Good Qty])[Good Qty],Sum(xxx.[Good FAT %])[Good FAT %],Sum(xxx.[Good FATKg])[Good FATKg],Sum(xxx.[Good SNF %])[Good SNF %],Sum(xxx.[Good SNFKG])[Good SNFKG],Sum(xxx.[SOUR Qty])[SOUR Qty],Sum(xxx.[SOUR FAT %])[SOUR FAT %],Sum(xxx.[SOUR FATKg])[SOUR FATKg],Sum(xxx.[SOUR SNF %])[SOUR SNF %],Sum(xxx.[SOUR SNFKG])[SOUR SNFKG],Sum(xxx.[CURD Qty])[CURD Qty],Sum([CURD FAT %])[CURD FAT %],Sum(xxx.[CURD FATKg])[CURD FATKg],Sum(xxx.[CURD SNF %])[CURD SNF %],Sum(xxx.[CURD SNFKG])[CURD SNFKG],Max(xxx.Comp_Name)Comp_Name
                        from (select TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No,"
                If AreaWiseBilling =True
                    qry += " xxxSetLocation.Location_Desc,"
                End If
                qry += "TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNo,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC, TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name],TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans as [No of Cans],TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO as [Route Code],TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as [Route],TSPL_COMPANY_MASTER.Comp_Name,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date
                        ,case When isnull(Reject_Type,'')='' then Milk_Weight else 0 end as [Good Qty]
                        ,case When isnull(Reject_Type,'')='' then FAT else 0 end as [Good FAT %]
                        ,case When isnull(Reject_Type,'')='' then cast(Milk_Weight*FAT/100 as decimal(18,3)) else 0 end as [Good FATKg]
                        ,case When isnull(Reject_Type,'')='' then SNF else 0 end as [Good SNF %]
                        ,case When isnull(Reject_Type,'')='' then cast (Milk_Weight*SNF/100 as decimal(18,3)) else 0 end as [Good SNFKG],case When isnull(Reject_Type,'')='SOUR' then Milk_Weight else 0 end as [SOUR Qty]
                        ,case When isnull(Reject_Type,'')='SOUR' then FAT else 0 end as [SOUR FAT %]
                        ,case When isnull(Reject_Type,'')='SOUR' then cast (Milk_Weight*FAT/100 as decimal(18,3)) else 0 end as [SOUR FATKg]
                        ,case When isnull(Reject_Type,'')='SOUR' then SNF else 0 end as [SOUR SNF %]
                        ,case When isnull(Reject_Type,'')='SOUR' then cast (Milk_Weight*SNF/100 as decimal(18,3)) else 0 end as [SOUR SNFKG],case When isnull(Reject_Type,'')='CURD' then Milk_Weight else 0 end as [CURD Qty]
                        ,case When isnull(Reject_Type,'')='CURD' then FAT else 0 end as [CURD FAT %]
                        ,case When isnull(Reject_Type,'')='CURD' then cast (Milk_Weight*FAT/100 as decimal(18,3)) else 0 end as [CURD FATKg]
                        ,case When isnull(Reject_Type,'')='CURD' then SNF else 0 end as [CURD SNF %]
                        ,case When isnull(Reject_Type,'')='CURD' then cast (Milk_Weight*SNF/100 as decimal(18,3)) else 0 end as [CURD SNFKG] ,TSPL_MILK_SHIFT_UPLOADER_DETAIL.PageNo 
                        from TSPL_MILK_SHIFT_UPLOADER_DETAIL 
                        left outer Join TSPL_MILK_SHIFT_UPLOADER_HEAD On TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
                        left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO
                        Left Outer Join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC"
                If AreaWiseBilling =True Then
                    qry += " Left Outer Join( select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,tspl_location_master.Location_Desc,tspl_location_master.Location_Code   From TSPL_PAYMENT_PROCESS_HEAD left  join tspl_location_master on tspl_location_master.Location_Code=TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code)  xxxSetLocation On xxxSetLocation.Location_Code=TSPL_MCC_MASTER.area_Location_code "
                End If

                qry += "Left outer join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "'                        
                        where Convert(date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>=convert(date,'" + fromDate.Value + "',103)  And Convert(date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<=convert (date,'" + dtpToDate.Value + "',103) "

                If AreaWiseBilling Then
                    If clsCommon.myLen(fndArea.Value) > 0 Then
                        qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    End If
                Else
                    If txtMCC_Code.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC_Code.Value) > 0 Then
                        qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC_Code.Value) + "'"
                    End If
                End If

                If ddlShift.SelectedValue IsNot Nothing AndAlso clsCommon.CompairString(ddlShift.SelectedValue, "Morning") = CompairStringResult.Equal Then
                    qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift='M'"
                ElseIf ddlShift.SelectedValue IsNot Nothing AndAlso clsCommon.CompairString(ddlShift.SelectedValue, "Evening") = CompairStringResult.Equal Then
                    qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift='E'"
                End If
                qry += ") xxx Group By xxx.[Route Code],xxx.Shift_Date,xxx.shift order by Convert(int,xxx.[Route Code]),xxx.shift desc"
            Else
                qry = " select '" + fromDate.Value + "' As [From Date],'" + dtpToDate.Value + "' As [To Date],'" & objCommonVar.CurrentUser & "' as User_Name, XXGetAllRecords.Document_No,XXGetAllRecords.Document_Date,XXGetAllRecords.Route_Code,XXGetAllRecords.ROUTE_NAME,XXGetAllRecords.Tanker_No,XXGetAllRecords.Comp_Name,XXGetAllRecords.Vehicle_No,
                                XXGetAllRecords.Entered_Qty,XXGetAllRecords.Entered_FATKg,XXGetAllRecords.Entered_SNFKg,
                                XXGetAllRecords.MCC_Qty,XXGetAllRecords.MCC_FATKG,XXGetAllRecords.MCC_SNFKG,
                                XXGetAllRecords.DiffMCCVsEntered_Qty,
                                XXGetAllRecords.DiffMCCVsEntered_FATKG,0 as FAT_Tolerence,case when (isnull(XXGetAllRecords.DiffMCCVsEntered_FATKG - 0,0))<0 then 0 else isnull(XXGetAllRecords.DiffMCCVsEntered_FATKG - 0,0) end as FATKG_Recovered,Round(XXGetAllRecords.FAT_AMT,2,0) as FAT_AMT,
                                XXGetAllRecords.DiffMCCVsEntered_SNFKG,0 as SNF_Tolerence,case when (isnull(XXGetAllRecords.DiffMCCVsEntered_SNFKG - 0,0))<0 then 0 else isnull(XXGetAllRecords.DiffMCCVsEntered_SNFKG - 0,0) end as SNFKG_Recovered,Round(XXGetAllRecords.SNF_AMT,2,0) as SNF_AMT,Round((XXGetAllRecords.FAT_AMT + XXGetAllRecords.SNF_AMT),2,0) as AMOUNT,XXGetAllRecords.GainLossCode as GainLoss_Code,
                                   XXGetAllRecords.Loss_FAT_Rate as Loss_FAT_Rate,
                                   XXGetAllRecords.Loss_SNF_Rate as Loss_SNF_Rate,
                                   XXGetAllRecords.Start_Date as Start_Date
                                 from( select GetAllGainLossRate.*,
                                              case when GetAllGainLossRate.DiffMCCVsEntered_FATKG >= 0 then (
                                        case when (GetAllGainLossRate.DiffMCCVsEntered_FATKG - 0) > 0 then ((GetAllGainLossRate.DiffMCCVsEntered_FATKG -0) *TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate) else 0 end)
                                        else 0 end as FAT_AMT,
                                        case when GetAllGainLossRate.DiffMCCVsEntered_SNFKG > 0 then (case when (GetAllGainLossRate.DiffMCCVsEntered_SNFKG - 0) > 0 then ((GetAllGainLossRate.DiffMCCVsEntered_SNFKG -0) *TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate) else 0 end)
                                      else 0 end as SNF_AMT,
                                      TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date from ( select GetRateCode.*,( SELECT top 1 TSPL_OWN_BMC_GAIN_LOSS_RATE.Code FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 and TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 and CONVERT(date, TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date, 103) <= CONVERT(date, GetRateCode.Document_Date, 103) order by TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date desc ) as GainLossCode from ( SELECT XXXFinal.Document_No, MAX(XXXFinal.Document_Date) AS Document_Date, MAX(XXXFinal.Route_Code) AS Route_Code, MAX(XXXFinal.ROUTE_NAME) AS ROUTE_NAME, MAX(XXXFinal.Tanker_No) AS Tanker_No,MAX(XXXFinal.Comp_Name) AS Comp_Name, MAX(XXXFinal.Vehicle_No) AS Vehicle_No, MAX(Entered_Qty) AS Entered_Qty, ((MAX(Entered_FATKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS Entered_FAT, ((MAX(Entered_SNFKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS Entered_SNF, MAX(Entered_FATKg) AS Entered_FATKg, MAX(Entered_SNFKg) AS Entered_SNFKg, SUM(XXXFinal.MCC_Qty) AS MCC_Qty, ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS MCC_FAT, ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS MCC_SNF, SUM(MCC_FATKG) AS MCC_FATKG, SUM(MCC_SNFKG) AS MCC_SNFKG, SUM(DCS_Qty) AS DCS_Qty, ROUND(((SUM(DCS_FATKG) / NULLIF(SUM(DCS_Qty), 0)) * 100), 3, 0) AS DCS_FAT, ROUND(((SUM(DCS_SNFKG) / NULLIF(SUM(DCS_Qty), 0)) * 100), 3, 0) AS DCS_SNF, SUM(DCS_FATKG) AS DCS_FATKG, SUM(DCS_SNFKG) AS DCS_SNFKG, SUM(Diff_Qty) AS Diff_Qty, MAX(DCS_FAT) - ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS Diff_FAT, MAX(DCS_SNF) - ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0) AS Diff_SNF, SUM(Diff_FATKG) AS Diff_FATKG, SUM(Diff_SNFKG) AS Diff_SNFKG, SUM(XXXFinal.MCC_Qty)- MAX(Entered_Qty) AS DiffMCCVsEntered_Qty, ROUND(((SUM(MCC_FATKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0)- ((MAX(Entered_FATKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS DiffMCCVsEntered_FAT, ROUND(((SUM(MCC_SNFKG) / NULLIF(SUM(MCC_Qty), 0)) * 100), 3, 0)- ((MAX(Entered_SNFKg) / NULLIF(MAX(Entered_Qty), 0)) * 100) AS DiffMCCVsEntered_SNF, SUM(MCC_FATKG)-MAX(Entered_FATKg) AS DiffMCCVsEntered_FATKG, SUM(MCC_SNFKG)-MAX(Entered_SNFKg) AS DiffMCCVsEntered_SNFKG from ( select max(Entered_Qty) as Entered_Qty, max(Entered_FATKg) as Entered_FATKg, max(Entered_SNFKg) as Entered_SNFKg, max(PK_Id) as PK_Id, Document_No, max(Document_Date) as Document_Date, max(MCC_Code) as MCC_Code, max(MCC_NAME) as MCC_NAME, max(UploaderNo) as UploaderNo, max(Route_Code) as Route_Code, max(ROUTE_NAME) as ROUTE_NAME, max(Tanker_No) as Tanker_No,max(Comp_Name) as Comp_Name, max(Vehicle_No) as Vehicle_No, sum(MCC_Qty) as MCC_Qty, ROUND(((sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_FAT, ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as MCC_SNF, sum(MCC_FATKG) as MCC_FATKG, sum(MCC_SNFKG) as MCC_SNFKG, max(DCS_Qty) as DCS_Qty, isnull( (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_FAT, isnull( (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100),0) as DCS_SNF, isnull(max(DCS_FATKG),0) as DCS_FATKG,isnull( max(DCS_SNFKG),0) as DCS_SNFKG, max(DCS_Qty) - sum(MCC_Qty) as Diff_Qty, (max(DCS_FATKG)/ nullif(max(DCS_Qty),0)*100)- ROUND( ( ( sum (MCC_FATKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_FAT, (max(DCS_SNFKG)/ nullif(max(DCS_Qty),0)*100)-ROUND( ( ( sum (MCC_SNFKG) / nullif ( sum (MCC_Qty), 0 ) ) * 100 ), 3, 0 ) as Diff_SNF, sum(MCC_FATKG) -max(DCS_FATKG) as Diff_FATKG, sum(MCC_SNFKG)-max(DCS_SNFKG) as Diff_SNFKG from ( select TSPL_MILK_COLLECTION_MCC.Entered_Qty, TSPL_MILK_COLLECTION_MCC.Entered_FATKg, TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id, TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No, convert ( varchar, TSPL_MILK_COLLECTION_MCC.Document_Date, 103 ) as Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code, TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo, TSPL_MILK_COLLECTION_MCC.Route_Code, TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_COMPANY_MASTER.Comp_Name,TSPL_MILK_COLLECTION_MCC.Vehicle_No, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCC_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCC_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCC_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCC_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCC_SNFKG, isnull(XXXDCS.qty, 0) as DCS_Qty, isnull(XXXDCS.FAT, 0) as DCS_FAT, isnull(XXXDCS.SNF, 0) as DCS_SNF, isnull(XXXDCS.FATKG, 0) as DCS_FATKG, isnull(XXXDCS.SNFKG, 0) as DCS_SNFKG, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty - isnull(XXXDCS.qty, 0) as Diff_Qty, TSPL_MILK_COLLECTION_MCC_DETAIL.FAT - isnull(XXXDCS.FAT, 0) as Diff_FAT, TSPL_MILK_COLLECTION_MCC_DETAIL.SNF - isnull(XXXDCS.SNF, 0) as Diff_SNF, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG - isnull(XXXDCS.FATKG, 0) as Diff_FATKG, TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG - isnull(XXXDCS.SNFKG, 0) as Diff_SNFKG from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_BULK_ROUTE_MASTER.Comp_Code left outer join ( select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail, sum (qty) as qty, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) as FATKG, sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) as SNFKG, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) /nullif( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as FAT, ( ( sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) / nullif(sum ( TSPL_MILK_COLLECTION_DCS_DETAIL.qty ),0) ) * 100 ) as SNF
                                                         from TSPL_MILK_COLLECTION_DCS_DETAIL 
                                        left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
                            left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                            where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and convert (date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= convert (date,'" + dtpToDate.Value + "',103) 
                            group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail ) XXXDCS on XXXDCS.Against_Milk_Collection_MCC_Detail = TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                            where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert (date,'" + dtpToDate.Value + "',103)) xyz group by UploaderNo, Document_No    ) XXXFinal group by XXXFinal.Document_No )GetRateCode )GetAllGainLossRate left outer join TSPL_OWN_BMC_GAIN_LOSS_RATE on TSPL_OWN_BMC_GAIN_LOSS_RATE.Code=GetAllGainLossRate.GainLossCode ) XXGetAllRecords
    						 "
                If clsCommon.myLen(TxtTankerNo.Value) > 0 Then
                    qry += " where Tanker_No='" + TxtTankerNo.Value + "'"
                End If
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnDockSummary.Checked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDockSummaryReport", "DCS Summary Report", clsCommon.myCDate(fromDate.Value))
                ElseIf rbtnSummary.Checked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDSummary", "Dock Summary", clsCommon.myCDate(fromDate.Value))
                ElseIf rbtnBMCDock.Checked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptBMCDockReport", "BMC Dock Report", clsCommon.myCDate(fromDate.Value))
                Else
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptdailyqtyreport", "Daily Quantity Report", clsCommon.myCDate(fromDate.Value))
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnBMCDock_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnBMCDock.CheckedChanged
        Try
            If rbtnBMCDock.Checked Then
                MyLabel4.Visible = True
                txtMCC_Code.Visible = True
                MyLabel6.Visible = True
                ddlShift.Visible = True
                If AreaWiseBilling Then
                    fndArea.Visible = True
                    lblArea.Visible = True
                End If
            Else
                MyLabel4.Visible = False
                txtMCC_Code.Visible = False
                ddlShift.Visible = False
                MyLabel6.Visible = False
                fndArea.Visible = False
                lblArea.Visible = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnDockSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDockSummary.CheckedChanged
        Try
            If rbtnDockSummary.Checked Then
                MyLabel4.Visible = True
                txtMCC_Code.Visible = True
                ddlShift.Visible = True
                MyLabel6.Visible = True
                If AreaWiseBilling Then
                    fndArea.Visible = True
                    lblArea.Visible = True
                End If
            Else
                MyLabel4.Visible = False
                txtMCC_Code.Visible = False
                ddlShift.Visible = False
                MyLabel6.Visible = False
                fndArea.Visible = False
                lblArea.Visible = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadShif()
        ddlShift.DataSource = Shift()
        ddlShift.ValueMember = "Code"
        ddlShift.DisplayMember = "Name"
        ddlShift.SelectedIndex = 0
    End Sub
    Public Function Shift() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Morning"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Evening"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Private Sub rbtnSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSummary.CheckedChanged
        Try
            If rbtnSummary.Checked Then
                MyLabel4.Visible = True
                txtMCC_Code.Visible = True
                ddlShift.Visible = True
                MyLabel6.Visible = True
                If AreaWiseBilling Then
                    fndArea.Visible = True
                    lblArea.Visible = True
                End If
            Else
                MyLabel4.Visible = False
                txtMCC_Code.Visible = False
                ddlShift.Visible = False
                MyLabel6.Visible = False
                fndArea.Visible = False
                lblArea.Visible = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub
End Class


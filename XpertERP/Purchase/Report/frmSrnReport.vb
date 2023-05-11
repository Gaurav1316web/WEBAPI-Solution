Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmSrnReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSrnReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnprint1.Visible = MyBase.isPrintFlag

    End Sub
    Private Sub FrmSrnReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate1.Value = clsCommon.GETSERVERDATE()
        LoadDocumentNo()
        LoadVendor()
        LoadLocation()

        chkLocAll.IsChecked = True

        chkall.IsChecked = True
        chk_Vendor_All.IsChecked = True
        rdbtnFinishedGood.IsChecked = True
        chkLocAll.IsChecked = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SRN-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

   
    Sub LoadDocumentNo()
        Dim qry As String = "select Srn_No as Code,CONVERT(Varchar(12),SRN_Date,103) as [Date] from TSPL_Srn_HEAD where item_type='F' "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Date"


    End Sub
    Public Sub LoadDocNo()
        Dim qry As String = "select Srn_No as Code,CONVERT(Varchar(12),SRN_Date,103) as [Date] from TSPL_Srn_HEAD where item_type not in ('F')  "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Date"
    End Sub
    Private Sub rdbtnOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnOther.Click
        LoadDocNo()
        cbgDoc.CheckedAll()
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER WHERE  Status='N'  order by Vendor_Code "
        cbgvendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgvendor1.ValueMember = "Vendor_Code"
        cbgvendor1.DisplayMember = "Vendor_Name"
    End Sub
    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Reset()
    End Sub
    Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate1.Value = clsCommon.GETSERVERDATE()
        LoadDocumentNo()
        LoadVendor()
        chkall.IsChecked = True
        chk_Vendor_All.IsChecked = True
        rdbtnFinishedGood.IsChecked = True
        chkLocAll.IsChecked = True
        cbgDoc.CheckedAll()
        cbgVendor1.CheckedAll()
    End Sub
    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        Printdata()
    End Sub
    Public Sub Printdata()
        Try

            Dim locationArr As ArrayList = Nothing
            If cbgLocation.CheckedValue.Count > 0 Then
                locationArr = cbgLocation.CheckedValue
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    Dim strLoc As String = " select segment_code from tspl_gl_segment_code where 
                     segment_code in (" + objCommonVar.strCurrUserLocationsSegment + ")"

                    Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(strLoc)
                    Dim arrGLLocCode As New ArrayList
                    If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                        For Each dr As DataRow In dtLoc.Rows
                            arrGLLocCode.Add(clsCommon.myCstr(dr("segment_code")))
                        Next
                    End If
                    locationArr = arrGLLocCode
                End If
            End If

            ' Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate1.Value, "dd/MM/yyyy")
            Dim DocArr As ArrayList = cbgDoc.CheckedValue
            Dim VendorArr As ArrayList = cbgVendor1.CheckedValue
            If rdbtnFinishedGood.IsChecked Then
                '' Added By Abhishek kuamr As on 13 july 2012 Through One Function ----

                frmSRN.SRNPrintOut(dtpFromdate1.Value, dtpToDate1.Value, False, cbgDoc.CheckedValue, cbgVendor1.CheckedValue, locationArr)

            ElseIf rdbtnOther.IsChecked Then
                frmSRN.SRNPrintOut(dtpFromdate1.Value, dtpToDate1.Value, False, cbgDoc.CheckedValue, cbgVendor1.CheckedValue, locationArr)

                '' Codes Ends Here ---
            End If


        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub print(ByVal DocArr As ArrayList, ByVal VendorArr As ArrayList)
        Dim locationArr As ArrayList = cbgLocation.CheckedValue

        '        ''
        '        ''Calculation Of Total Rejected_Quantity
        '        ''
        '        Dim sqlRejctdQTY As String = "(select SUM(rejected_qty) from tspl_srn_detail where srn_no=txtDocNo.Value ) " '"
        '        Dim strDocTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sqlRejctdQTY))

        '        ''
        '        ''Calculation Of Total MRN_Quantity   
        '        ''
        '        Dim TotalMRNQTY As String = "select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SRN_DETAIl left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code where SRN_No='" + txtDocNo.Value + "'"
        '        Dim MRNQtyTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(TotalMRNQTY))

        '        ''
        '        ''Calculation Of SRN_Qty
        '        ''
        '        Dim TotalSRNQTY As String = "select SUM(SRN_qty) from tspl_srn_detail where srn_no='" + txtDocNo.Value + "'"
        '        Dim SRNQtyTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(TotalSRNQTY))


        '        ''
        '        ''Checking Of PI_No. that it is single or more than single
        '        ''
        '        Dim chkPINo As String = "select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
        '"( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= '" + txtDocNo.Value + "'" & _
        '"GROUP by TSPL_PI_DETAIL.PI_No)xxx"
        '        Dim PInvNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(chkPINo))



        Dim strquery As String = "SELECT TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Vendor_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else SRN_Date end ) as Challan_Date, TSPL_SRN_HEAD.Ref_No  " & _
                      "as Challan_No, TSPL_SRN_HEAD.Inv_No, TSPL_SRN_HEAD.Inv_Date, TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.Amount_Less_Discount ,TSPL_SRN_HEAD.GENo,TSPL_SRN_HEAD.SRN_Total_Amt, " & _
                      "TSPL_SRN_HEAD.GEDate, TSPL_SRN_HEAD.VehicleNo, TSPL_SRN_HEAD.Carrier,TSPL_SRN_HEAD.Remarks,TSPL_SRN_HEAD.Total_Landed_Cost as Total_Landed_Cost, TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.Row_Type,TSPL_SRN_DETAIL.Amt_Less_Discount," & _
"TSPL_SRN_DETAIL.Item_Cost as basicRate,TSPL_SRN_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SRN_DETAIL.Unit_Cost_Tax_Rate as UCTR," & _
"TSPL_SRN_DETAIL.Unit_Cost_Tax as uctax,TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.SRN_Qty,TSPL_SRN_DETAIL.Rejected_Qty,TSPL_SRN_HEAD.Vendor_Code,TSPL_SRN_HEAD.SRN_Total_Amt,TSPL_SRN_DETAIL.ITEM_COST," & _
 "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " & _
"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SRN_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," & _
"isnull (TSPL_SRN_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SRN_HEAD.tax3_amt,0) as txt3amt," & _
"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SRN_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," & _
"isnull (TSPL_SRN_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SRN_HEAD.tax6_amt,0) as txt6amt " & _
",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SRN_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," & _
"isnull (TSPL_SRN_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SRN_HEAD.tax9_amt,0) as txt9amt," & _
"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SRN_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SRN_DETAIL.SRN_Qty," & _
"case when tax1.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax1_amt else null end as Tax1Recoverable," & _
"case when tax2.Tax_Recoverable='Y' then TSPL_SRN_HEAD.TAX2_Amt else null end as Tax2Recoverable, " & _
"case when tax3.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax3_amt else null end as Tax3Recoverable, " & _
"case when tax4.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax4_amt else null end as Tax4Recoverable, " & _
"case when tax5.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax5_amt else null end as Tax5Recoverable, " & _
"case when tax6.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax6_amt else null end as Tax6Recoverable," & _
"case when tax7.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax7_amt else null end as Tax7Recoverable, " & _
"case when tax8.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax8_amt else null end as Tax8Recoverable, " & _
"case when tax9.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax9_amt else null end as Tax9Recoverable," & _
"case when tax10.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax10_amt else null end as Tax10Recoverable, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " & _
"convert(varchar,isnull (TSPL_SRN_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," & _
"TSPL_SRN_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from tspl_srn_detail where srn_no=TSPL_SRN_HEAD.SRN_No) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SRN_DETAIl left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code where SRN_No =TSPL_SRN_HEAD.SRN_No)as MrnTotQty, (select SUM(SRN_qty) from tspl_srn_detail where srn_no=TSPL_SRN_HEAD.SRN_No) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_SRN_HEAD.SRN_No " & _
" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " & _
       " TSPL_SRN_HEAD.Add_Charge_Name1 as Add1Name, " & _
     " TSPL_SRN_HEAD.Add_Charge_Amt1 as Add1 , " & _
     "     TSPL_SRN_HEAD.Add_Charge_Name2 as Add2Name, " & _
     "   TSPL_SRN_HEAD.Add_Charge_Amt2 as Add2 , " & _
     "    TSPL_SRN_HEAD.Add_Charge_Name3 as Add3Name, " & _
     "   TSPL_SRN_HEAD.Add_Charge_Amt3 as Add3 , " & _
     "    TSPL_SRN_HEAD.Add_Charge_Name4 as Add4Name, " & _
     "    TSPL_SRN_HEAD.Add_Charge_Amt4 as Add4 , " & _
     "     TSPL_SRN_HEAD.Add_Charge_Name5 as Add5Name, " & _
      "     TSPL_SRN_HEAD.Add_Charge_Amt5 as Add5 , " & _
      "     TSPL_SRN_HEAD.Add_Charge_Name6 as Add6Name, " & _
      "    TSPL_SRN_HEAD.Add_Charge_Amt6 as Add6 , " & _
      "    TSPL_SRN_HEAD.Add_Charge_Name7 as Add7Name, " & _
      "     TSPL_SRN_HEAD.Add_Charge_Amt7 as Add7 , " & _
      "       TSPL_SRN_HEAD.Add_Charge_Name8 as Add8Name, " & _
      "      TSPL_SRN_HEAD.Add_Charge_Amt8 as Add8 , " & _
       "      TSPL_SRN_HEAD.Add_Charge_Name9 as Add9Name, " & _
       "      TSPL_SRN_HEAD.Add_Charge_Amt9 as Add9 , " & _
       "      TSPL_SRN_HEAD.Add_Charge_Name10 as Add10Name, " & _
       "     TSPL_SRN_HEAD.Add_Charge_Amt10 as Add10,TSPL_SRN_HEAD.Against_RGP   " & _
 " FROM  TSPL_SRN_DETAIL INNER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No " & _
 "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " & _
 "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SRN_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " & _
 "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SRN_HEAD.tax1  " & _
 "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SRN_HEAD.tax2 " & _
 "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SRN_HEAD .TAX3 " & _
 "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SRN_HEAD .tax4 " & _
 "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SRN_HEAD .tax5 " & _
 "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SRN_HEAD .TAX6  " & _
 "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SRN_HEAD .TAX7  " & _
 "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SRN_HEAD .TAX8 " & _
 "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SRN_HEAD .TAX9 " & _
 " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SRN_HEAD .TAX10  " & _
 "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location  " & _
 " where TSPL_SRN_HEAD .Item_Type not in('F')and  Convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=Convert(date,'" + dtpFromdate1.Value + "',103)and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=Convert(date,'" + dtpToDate1.Value + "',103)"

        If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select one location ")
                Exit Sub
            End If
            strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + objCommonVar.strCurrUserLocationsSegment + ") "
            End If
        End If
        If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
        ElseIf cbgDoc.CheckedValue.Count > 0 Then
            strquery += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(DocArr) + ")  "

        End If
        If chk_Vendor_Select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
        ElseIf cbgVendor1.CheckedValue.Count > 0 Then
            strquery += " and TSPL_SRN_HEAD.Vendor_Name in (" + clsCommon.GetMulcallString(VendorArr) + ")  "

        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Record Found")
        Else
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "SRNReportThroughReport", "Store Receipt Report")
            frmCRV = Nothing
        End If



    End Sub
    Public Sub oldcodeOfFinisGood()
        '     qry = "select SRN_No,MAX(ItemType )as ItemType,MAX(MRN_Date) as SRN_Date,MAX(Vendor_Name) as Vendor_Name,MAX(GRNo) as GRNo,MAX(GENo) as GENo,MAX(GEDate) as GEDate,Item_Code,MAX(Item_Desc) as Item_Desc,MAX(VehicleNo) as VehicleNo, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Burst_Qty) as Burst,SUM(Short_Qty) as Short,MAX(Remarks) as Remarks,max(Ref_No)as Ref_No from( " & _
        '"select TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD .Item_Type as ItemType," & _
        '"(replace( CONVERT(varchar(11), TSPL_SRN_HEAD.SRN_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_SRN_HEAD.SRN_Date,108) )as MRN_Date,TSPL_SRN_HEAD.Vendor_Name,TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.GENo," & _
        '"(case when LEN(TSPL_SRN_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_SRN_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_SRN_HEAD.VehicleNo,TSPL_SRN_HEAD.Remarks ,TSPL_SRN_HEAD.Ref_No,TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.Unit_code," & _
        '"case when Unit_code='FC' then SRN_Qty end as FCS, " & _
        '"case when Unit_code='FB' then SRN_Qty end as FBS, " & _
        '"case when Unit_code='SH' then SRN_Qty end as FSH, " & _
        '"case when Unit_code='EC' then SRN_Qty end as ECS," & _
        '"case when Unit_code='EB' then SRN_Qty end as EBS, " & _
        '"TSPL_SRN_DETAIL.Leak_Qty,TSPL_SRN_DETAIL.Burst_Qty,TSPL_SRN_DETAIL.Short_Qty from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No= TSPL_SRN_DETAIL.SRN_No " & _
        '" left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location   where Item_Type ='F' and   " & _
        '" Convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=Convert(date,'" + dtpFromdate1.Value + "',103)and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=Convert(date,'" + dtpToDate1.Value + "',103) "
        '     If chkLocSelect.IsChecked Then
        '         If cbgLocation.CheckedValue.Count <= 0 Then
        '             common.clsCommon.MyMessageBoxShow("Please select one location ")
        '             Return
        '         End If
        '         qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
        '     End If
        '     If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
        '         common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
        '         Return
        '     ElseIf chk_Doc_Select.IsChecked = True AndAlso cbgDoc.CheckedValue.Count > 0 Then
        '         qry += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(DocArr) + ")  "

        '     End If
        '     If chk_Vendor_Select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
        '         common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor Number")
        '         Return
        '     ElseIf chk_Vendor_Select.IsChecked = True AndAlso cbgVendor1.CheckedValue.Count > 0 Then
        '         qry += " and Vendor_Name in (" + clsCommon.GetMulcallString(VendorArr) + ")"

        '     End If
        '     qry += " )xxx group by SRN_No,Item_Code order by Item_Desc"
        '     Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '     If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '         common.clsCommon.MyMessageBoxShow("No Record Found")
        '     Else
        '         PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")

        '     End If
    End Sub

    Private Sub chkall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkall.ToggleStateChanged
        cbgDoc.Enabled = Not chkall.IsChecked
        cbgDoc.CheckedAll()
    End Sub
    Private Sub chk_Vendor_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_Vendor_All.ToggleStateChanged
        cbgVendor1.Enabled = Not chk_Vendor_All.IsChecked
        cbgVendor1.CheckedAll()
    End Sub
    Private Sub btnClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub
    Private Sub rdbtnFinishedGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnFinishedGood.Click
        LoadDocumentNo()
        cbgDoc.CheckedAll()
    End Sub

    Private Sub FrmSrnReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            Printdata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class

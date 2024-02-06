Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class frmVendorBankAdvice
    Inherits FrmMainTranScreen
    Dim MultipleFinderFillAuto As Boolean = False
    Dim AreaWiseBilling As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Dim StrPermission As String
    Dim dtREJECT As DataTable
    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        Reset()
        'RadGroupBox1.Visible = Not MultipleFinderFillAuto
        txtPaymentCycleFrom.Enabled = Not MultipleFinderFillAuto
        txtPaymentCycleTo.Enabled = Not MultipleFinderFillAuto
        'RadGroupBox3.Visible =MultipleFinderFillAuto
        If MultipleFinderFillAuto = False Then
            RadGroupBox3.Visible = True
        Else
            RadGroupBox3.Visible = MultipleFinderFillAuto
        End If
        'RadGroupBox3.Visible = True
        txtMCC.Enabled = Not MultipleFinderFillAuto
        rbtnSaving.Visible = MultipleFinderFillAuto
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        fndArea.Enabled = AreaWiseBilling
        txtPaymentCycleFrom.Enabled = Not AreaWiseBilling
        txtPaymentCycleTo.Enabled = Not AreaWiseBilling
    End Sub
    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        dtREJECT = Nothing
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        txtPaymentCycleFrom.Value = ""
        txtPaymentCycleTo.Value = ""
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        rbtnSaving.IsChecked = False
        rbtnSavingBankWiseSummary.IsChecked = False
        rbtnSavingCompulsory.IsChecked = False
        rbtnBankAdvice.IsChecked = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtMCC.Value = ""
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If rbtnBankAdvice.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_CBA"
        ElseIf rbtnBankWiseSummary.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_CBS"
        ElseIf rbtnCurrentBankWiseSummary.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_CCBS"
        End If

        If rbtnSaving.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_SA"
        ElseIf rbtnSavingCompulsory.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_SC"
        ElseIf rbtnSavingBankWiseSummary.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_SBS"
        Else
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        End If

        If ChkIFSCCode.Checked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "IFSC"
        End If

        Print(False)
    End Sub

    Sub Print(ByVal isPrint As Boolean)
        Try

            If clsCommon.myLen(txtMCC.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select MCC.", Me.Text)
                txtMCC.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(fndArea.Value) <= 0 AndAlso AreaWiseBilling = False Then

            End If
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleFrom.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                'clsCommon.MyMessageBoxShow("Plz Select Payment Cycle From First.", Me.Text)
                'txtPaymentCycleFrom.Focus()
                'Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleTo.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                'clsCommon.MyMessageBoxShow("Plz Select Payment Cycle To First.", Me.Text)
                'txtPaymentCycleTo.Focus()
                'Exit Sub
            End If

            If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) AndAlso MultipleFinderFillAuto = False Then
                common.clsCommon.MyMessageBoxShow(Me, "From Payment Cycle can not be greater then to Payment Cycle")
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            If MultipleFinderFillAuto = False Then
                Patment_Cycle_changed()
            End If
            If AreaWiseBilling = False Then
                Patment_Cycle_changed()

            End If
            If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) AndAlso AreaWiseBilling = False Then
                common.clsCommon.MyMessageBoxShow(Me, "From Payment Cycle can not be greater then to Payment Cycle")
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            Dim strCycleRange As String = txtPaymentCycleTo.Value
            If clsCommon.myCdbl(txtPaymentCycleTo.Value) > clsCommon.myCdbl(txtPaymentCycleFrom.Value) Then
                strCycleRange = txtPaymentCycleTo.Value + " To  " + txtPaymentCycleFrom.Value
            End If
            Dim BaseQry As String = ""
            Dim Doc_No_Value As String = "select Doc_No from TSPL_PAYMENT_PROCESS_Invoice"
            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT Area_Location_Code FROM TSPL_PAYMENT_PROCESS_HEAD")
            'Dim AreaLocationCode As String = ""

            'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            '    ' Assuming you want the first value in the DataTable
            '    AreaLocationCode = clsCommon.myCstr(dt1.Rows(0)("Area_Location_Code"))
            'End If

            If rbtnSavingBankWiseSummary.IsChecked AndAlso rbtnCurrentBankWiseSummary.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, "You have to select only option at a time ", Me.Text)
                Exit Sub
            End If
            If rbtnSaving.IsChecked Then
                BaseQry = " select  '' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end)  as GRPColumn,
                            TSPL_COMPANY_MASTER.Comp_Name
                            ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name
                            ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
  TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name, 
  TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc, 
  case when isnull(
    coalesce(
      TSPL_VENDOR_MASTER.vsp_payment, 
      Mp_V.vsp_payment
    ), 
    ''
  )= 'Self' then coalesce(
    TSPL_VENDOR_MASTER.IFSCCode2, mp_V.IFSC_Code
  ) else coalesce(
    TSPL_VENDOR_MASTER.Joint_IFSC_Code, 
    mp_v.Joint_IFSC_Code
  ) end as Payee_Joint_IFSC_Code, 
  case when isnull(
    coalesce(
      TSPL_VENDOR_MASTER.vsp_payment, 
      Mp_V.vsp_payment
    ), 
    ''
  )= 'Self' then coalesce( 
    TSPL_VENDOR_MASTER.AccNo2, mp_V.Account_No
  ) else coalesce(
    TSPL_VENDOR_MASTER.Joint_Account_No, 
    mp_V.Joint_Account_No
  ) end as Payee_Joint_Account_No,"

                If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                    BaseQry += " Round(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) as Payable_Amount "
                Else
                    BaseQry += " TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount "

                End If

                BaseQry += "from TSPL_PAYMENT_PROCESS_SAVING 
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No
                            left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
                            left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
                            Left outer join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  
                            left outer join ( select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_SAVING.VSP_CODE
                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Bank_Code
                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected


                            where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0  "

            ElseIf rbtnSavingCompulsory.IsChecked Then

                BaseQry = " select  '' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end)  as GRPColumn,TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],
                            TSPL_COMPANY_MASTER.Comp_Name
                            ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name
                            ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
  TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name, 
  TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc, 
  case when isnull(
    coalesce(
      TSPL_VENDOR_MASTER.vsp_payment, 
      Mp_V.vsp_payment
    ), 
    ''
  )= 'Self' then coalesce(
    TSPL_VENDOR_MASTER.IFSCCode2, mp_V.IFSC_Code
  ) else coalesce(
    TSPL_VENDOR_MASTER.Joint_IFSC_Code, 
    mp_v.Joint_IFSC_Code
  ) end as Payee_Joint_IFSC_Code, 
  case when isnull(
    coalesce(
      TSPL_VENDOR_MASTER.vsp_payment, 
      Mp_V.vsp_payment
    ), 
    ''
  )= 'Self' then coalesce( 
    TSPL_VENDOR_MASTER.AccNo2, mp_V.Account_No
  ) else coalesce(
    TSPL_VENDOR_MASTER.Joint_Account_No, 
    mp_V.Joint_Account_No
  ) end as Payee_Joint_Account_No,"

                If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                    BaseQry += " Round(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) as Payable_Amount "
                Else
                    BaseQry += " TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount  "

                End If

                BaseQry += " from TSPL_PAYMENT_PROCESS_COMPULSORY 
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No
                            left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No
                            left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
                            Left outer join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  
                            left outer join ( select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_COMPULSORY.VSP_CODE
                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank
                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected

                            where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0  "

            ElseIf rbtnSavingBankWiseSummary.IsChecked Then

                BaseQry = "select  '" + strCycleRange + "' AS CycleRange,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                Else
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                End If

                BaseQry += "TSPL_COMPANY_MASTER.Comp_Name
,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME
,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(AreaWiseBilling = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc,TSPL_BANK_MASTER.DESCRIPTION as Bank_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"

                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " Round(isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                Else
                    If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                        BaseQry += " Round(isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                    Else
                        BaseQry += " isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0) as Payable_Amount  "

                    End If

                End If
                BaseQry += " from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENTs_PROCESS_DETAIL.VSP_CODE
left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected

" + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + IIf(clsCommon.myLen(txtMCC.Value) > 0, "and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code='" + txtMCC.Value + "' And TSPL_MCC_MASTER.MCC_Code='" + txtMCC.Value + "' ", "") + "   and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount>0 "

            End If

            If rbtnBankAdvice.IsChecked OrElse rbtnBankWiseSummary.IsChecked Then
                If rbtnSaving.IsChecked = False AndAlso rbtnSavingCompulsory.IsChecked = False AndAlso rbtnSavingBankWiseSummary.IsChecked = False Then

                    BaseQry = "select  '" + strCycleRange + "' AS CycleRange,"

                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                    Else
                        BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                    End If
                    BaseQry += "TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],
TSPL_COMPANY_MASTER.Comp_Name
,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME
,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(MultipleFinderFillAuto = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"

                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        BaseQry += " Round((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)),0) as Payable_Amount "
                    Else
                        If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                            BaseQry += " Round((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)),0) as Payable_Amount  "
                        Else
                            ' BaseQry += " Cast((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)) as decimal(18)) as Payable_Amount "
                            BaseQry += " (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0))  as Payable_Amount  "
                        End If

                    End If
                    BaseQry += " from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected

" + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + IIf(clsCommon.myLen(txtMCC.Value) > 0, "and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code='" + txtMCC.Value + "' And TSPL_MCC_MASTER.MCC_Code='" + txtMCC.Value + "' ", "") + "   and (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0))>0 "
                End If
            ElseIf rbtnCurrentBankWiseSummary.IsChecked Then
                BaseQry = "select  '" + strCycleRange + "' AS CycleRange,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                Else
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                End If


                BaseQry += "TSPL_COMPANY_MASTER.Comp_Name
,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME
,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(MultipleFinderFillAuto = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc , TSPL_BANK_MASTER.DESCRIPTION as Bank_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " Round(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                Else
                    If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                        BaseQry += " Round(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                    Else
                        BaseQry += " (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)) as Payable_Amount "
                    End If

                End If
                BaseQry += " from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
" + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + IIf(clsCommon.myLen(txtMCC.Value) > 0, "and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code='" + txtMCC.Value + "' And TSPL_MCC_MASTER.MCC_Code='" + txtMCC.Value + "' ", "") + "   and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount>0 "
            End If

            If clsCommon.myLen(txtBankGroup.Value) > 0 Then
                BaseQry = BaseQry + " AND TSPL_BANK_MASTER.BANK_GROUP_CODE='" + txtBankGroup.Value + "' "
            End If
            If AreaWiseBilling Then
                BaseQry += " And TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code ='" + clsCommon.myCstr(fndArea.Value) + "' "

            End If
            Dim FinalQuery As String = ""
            If rbtnBankAdvice.IsChecked OrElse rbtnSavingCompulsory.IsChecked OrElse rbtnSaving.IsChecked Then


                'BaseQry += "and  TSPL_PAYMENT_PROCESS_Invoice.Doc_No='" + clsCommon.myCstr(Doc_No_Value) + "'"
                'BaseQry += "And TSPL_PAYMENT_PROCESS_Detail.MCC_Code='" + clsCommon.myCstr(txtMCC.Value) + "'"
                FinalQuery = BaseQry + " order by TSPL_Vendor_MASTER.Bank_Code"
            ElseIf rbtnBankWiseSummary.IsChecked Then
                FinalQuery += "select ROW_NUMBER() over ( order by GRPColumn) as SNO , * from ( select max(CycleRange) as CycleRange, max(GRPColumn) as GRPColumn,max(Comp_Name) as Comp_Name,max(Comp_address) as Comp_address, max(From_Date) as From_Date,max(GSTReg_No) as GSTReg_No,max(Fiscal_Name) as Fiscal_Name,max(CycleNo) as CycleNo,max(Date_Range) as Date_Range,Bank_Code,Branch_Name,max(Bank_Code_Desc) as Bank_Code_Desc, max (Payee_Joint_IFSC_Code) as Payee_Joint_IFSC_Code,max(Payee_Joint_Account_No) as Payee_Joint_Account_No ,sum(Payable_Amount) as Payable_Amount
,max(CompPhone) as CompPhone,max(Regn_No) as Regn_No,max(MCC_NAME) as MCC_NAME
from (" + Environment.NewLine + BaseQry + Environment.NewLine + " )xxx group by Bank_Code,Branch_Name )xxxx order by GRPColumnM "
            ElseIf rbtnCurrentBankWiseSummary.IsChecked OrElse rbtnSavingBankWiseSummary.IsChecked Then
                FinalQuery += "select ROW_NUMBER() over ( order by GRPColumn) as SNO , * from ( select  max(GRPColumn) as GRPColumn,sum(Payable_Amount) as Payable_Amount , Bank_Desc
from (" + Environment.NewLine + BaseQry + Environment.NewLine + " )xxx group by Bank_Desc )xxxx order by GRPColumn "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2

                SetGridFormat()
                EnableDisaableControls(False)
            End If

            If isPrint Then
                If rbtnBankAdvice.IsChecked Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptBankAdvice", "Bank Advice")
                    Else
                        frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceNew", "Bank Advice")
                        frmCRV = Nothing
                    End If
                ElseIf rbtnBankWiseSummary.CheckState Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptBankWiseSummary", "Bank Wise Summary")
                    frmCRV = Nothing
                End If
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try





    End Sub
    Sub SetGridFormat()
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next


        If rbtnBankAdvice.IsChecked OrElse rbtnSavingCompulsory.IsChecked OrElse rbtnSaving.IsChecked Then
            Gv1.Columns("CycleRange").HeaderText = "Cycle Range"
            Gv1.Columns("CycleRange").IsVisible = False

            Gv1.Columns("GRPColumn").HeaderText = "Group Range"
            Gv1.Columns("GRPColumn").IsVisible = False

            Gv1.Columns("Comp_Name").HeaderText = "Company Name"
            Gv1.Columns("Comp_Name").IsVisible = False

            Gv1.Columns("Comp_address").HeaderText = "Company Address"
            Gv1.Columns("Comp_address").IsVisible = False

            Gv1.Columns("From_Date").HeaderText = "Date"
            Gv1.Columns("From_Date").IsVisible = False

            Gv1.Columns("GSTReg_No").HeaderText = "GSTIN"
            Gv1.Columns("GSTReg_No").IsVisible = False

            Gv1.Columns("Doc_No").HeaderText = "Documant No"
            Gv1.Columns("Doc_No").IsVisible = False

            Gv1.Columns("CompPhone").HeaderText = "Phone No"
            Gv1.Columns("CompPhone").IsVisible = False

            Gv1.Columns("Regn_No").HeaderText = "Regn No"
            Gv1.Columns("Regn_No").IsVisible = False

            Gv1.Columns("MCC_NAME").HeaderText = "Area"
            Gv1.Columns("MCC_NAME").IsVisible = False

            If MultipleFinderFillAuto Then
            Else
                If clsCommon.myLen(Gv1.Columns("Location_Code")) Then
                    Gv1.Columns("Location_Code").HeaderText = "Location"
                    Gv1.Columns("Location_Code").IsVisible = False
                End If
                If clsCommon.myLen(Gv1.Columns("Location_Desc")) Then
                    Gv1.Columns("Location_Desc").HeaderText = "Location Name"
                    Gv1.Columns("Location_Desc").IsVisible = False
                End If
            End If


            Gv1.Columns("Fiscal_Name").HeaderText = "Fiscal Year"
            Gv1.Columns("Fiscal_Name").IsVisible = False

            Gv1.Columns("CycleNo").HeaderText = "Cycle No"
            Gv1.Columns("CycleNo").IsVisible = False

            Gv1.Columns("Date_Range").HeaderText = "Date Range"
            Gv1.Columns("Date_Range").IsVisible = False

            Gv1.Columns("VLC_CODE_Uploader").HeaderText = "DCS Code"
            Gv1.Columns("VLC_CODE_Uploader").IsVisible = True

            Gv1.Columns("Payee_Joint_Name").HeaderText = "Society Name"
            Gv1.Columns("Payee_Joint_Name").IsVisible = True

            Gv1.Columns("Bank_Code").HeaderText = "Bank"
            Gv1.Columns("Bank_Code").IsVisible = False

            Gv1.Columns("Branch_Name").HeaderText = "Branch"
            Gv1.Columns("Branch_Name").IsVisible = False


            Gv1.Columns("Bank_Code_Desc").HeaderText = "Bank Name"
            Gv1.Columns("Bank_Code_Desc").IsVisible = MultipleFinderFillAuto

            Gv1.Columns("Payee_Joint_IFSC_Code").HeaderText = "IFSC Code"
            Gv1.Columns("Payee_Joint_IFSC_Code").IsVisible = MultipleFinderFillAuto

            Gv1.Columns("Payee_Joint_Account_No").HeaderText = "Account No"
            Gv1.Columns("Payee_Joint_Account_No").IsVisible = True

            Gv1.Columns("Payable_Amount").HeaderText = "Amount"
            Gv1.Columns("Payable_Amount").IsVisible = True
            'Gv1.Columns("Payable_Amount").FormatString = "{0:n2}"


        ElseIf rbtnBankWiseSummary.IsChecked Then

            Gv1.Columns("SNO").HeaderText = "SNo"
            Gv1.Columns("SNO").IsVisible = True

            Gv1.Columns("CycleRange").HeaderText = "Cycle Range"
            Gv1.Columns("CycleRange").IsVisible = False

            Gv1.Columns("GRPColumn").HeaderText = "Group Range"
            Gv1.Columns("GRPColumn").IsVisible = False

            Gv1.Columns("Comp_Name").HeaderText = "Company Name"
            Gv1.Columns("Comp_Name").IsVisible = False

            Gv1.Columns("Comp_address").HeaderText = "Company Address"
            Gv1.Columns("Comp_address").IsVisible = False

            Gv1.Columns("From_Date").HeaderText = "Date"
            Gv1.Columns("From_Date").IsVisible = False

            Gv1.Columns("GSTReg_No").HeaderText = "GSTIN"
            Gv1.Columns("GSTReg_No").IsVisible = False

            Gv1.Columns("Fiscal_Name").HeaderText = "Fiscal Year"
            Gv1.Columns("Fiscal_Name").IsVisible = False

            Gv1.Columns("CycleNo").HeaderText = "Cycle No"
            Gv1.Columns("CycleNo").IsVisible = False

            Gv1.Columns("Date_Range").HeaderText = "Date Range"
            Gv1.Columns("Date_Range").IsVisible = False

            Gv1.Columns("Bank_Code").HeaderText = "Bank"
            Gv1.Columns("Bank_Code").IsVisible = True

            Gv1.Columns("Branch_Name").HeaderText = "Branch"
            Gv1.Columns("Branch_Name").IsVisible = True

            Gv1.Columns("Bank_Code_Desc").HeaderText = "Bank Name"
            Gv1.Columns("Bank_Code_Desc").IsVisible = MultipleFinderFillAuto

            Gv1.Columns("Payee_Joint_IFSC_Code").HeaderText = "IFSC Code"
            Gv1.Columns("Payee_Joint_IFSC_Code").IsVisible = MultipleFinderFillAuto

            Gv1.Columns("Payee_Joint_Account_No").HeaderText = "Account No"
            Gv1.Columns("Payee_Joint_Account_No").IsVisible = MultipleFinderFillAuto

            Gv1.Columns("Payable_Amount").HeaderText = "Amount"
            Gv1.Columns("Payable_Amount").IsVisible = True
            Gv1.Columns("Payable_Amount").FormatString = "{0:n2}"
            'Gv1.Columns("Payable_Amount").FormatString = "{0:n2}"
        ElseIf rbtnCurrentBankWiseSummary.IsChecked OrElse rbtnSavingBankWiseSummary.IsChecked Then
            Gv1.Columns("Payable_Amount").HeaderText = "Amount"
            Gv1.Columns("Payable_Amount").IsVisible = True
            Gv1.Columns("Payable_Amount").FormatString = "{0:n2}"
            'Gv1.Columns("Payable_Amount").FormatString = "{0:n2}"
            Gv1.Columns("Bank_Desc").IsVisible = True
            Gv1.Columns("Bank_Desc").HeaderText = "Bank Description"
            Gv1.Columns("GRPColumn").IsVisible = False
        End If
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)
        Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(MilkTypeB)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)



        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        txtMCC.Enabled = flag
        txtFiscalYear.Enabled = flag
        txtPaymentCycleFrom.Enabled = flag
        txtPaymentCycleTo.Enabled = flag
        RadGroupBox1.Enabled = flag
        txtbankgroupname.Enabled = flag
        txtBankGroup.Enabled = flag
        RadGroupBox3.Enabled = flag
        RadGroupBox2.Enabled = flag
        ChkIFSCCode.Enabled = flag
        fndArea.Enabled = flag
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select City_Code from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
            Dim strHeading As String = ""
            If MultipleFinderFillAuto Then
                strHeading = clsCommon.myCstr("BankAdvice Report(OHC) from " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " for '" + strUnit + "' unit  ")
            Else
                strHeading = clsCommon.myCstr("Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            If MultipleFinderFillAuto Then
                If rbtnSaving.IsChecked = True Then
                    arrHeader.Add("Data: Saving ")
                End If
                If rbtnBankAdvice.IsChecked = True Then
                    arrHeader.Add("Report Type: Bank Advice")
                End If
                If rbtnBankWiseSummary.IsChecked = True Then
                    arrHeader.Add("Report Type: Bank Wise Summary")
                End If
            Else
                arrHeader.Add("Mcc : " + txtMCC.Value)
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub




    'Private Sub Txtmccode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim whrcls As String = ""

    '    txtmccode.Value = clsMccMaster.getFinder(whrcls, txtmccode.Value, isButtonClicked)
    '    If clsCommon.myLen(txtmccode.Value) > 0 Then
    '        lblmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmccode.Value + "'"))
    '    Else
    '        txtmccode.Value = ""
    '        lblmccname.Text = ""
    '    End If
    'End Sub

    Private Sub TxtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            'fromDate.Value = clsCommon.GETSERVERDATE()
            'ToDate.Value = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleFrom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleFrom._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleFrom.Value = clsCommon.ShowSelectForm("LRPCF", qry, "Code", whrcls, txtPaymentCycleFrom.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtPaymentCycleFrom.Value) > 0 Then
                txtPaymentCycleTo.Value = txtPaymentCycleFrom.Value
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Patment_Cycle_changed()
        Try
            Dim dt As DataTable
            Dim qry As String = "SELECT Name ,From_Date,To_Date FROM TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                'clsCommon.MyMessageBoxShow("No Payment Cycle found for Selected Fiscal Year")
                Exit Sub
            End If

            fromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "))
            ToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select To_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleTo.Value + "' "))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleTo._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleTo.Value = clsCommon.ShowSelectForm("LRPCT", qry, "Code", whrcls, txtPaymentCycleTo.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select City_Code from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
            Dim strHeading As String = ""
            If MultipleFinderFillAuto Then
                strHeading = clsCommon.myCstr("BankAdvice Report(OHC) from " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " for '" + strUnit + "' unit  ")
            Else
                strHeading = clsCommon.myCstr("Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            End If
            'Dim strHeading As String = clsCommon.myCstr("Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            If MultipleFinderFillAuto Then
            Else
                arrHeader.Add("Mcc : " + txtMCC.Value)
            End If

            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"

            ' Dim qry As String = "select TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER 
            'Left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code  where location_code in (" + clsCommon.myCstr(txtMCC.Value) + "))"
            If fndArea.Value IsNot Nothing AndAlso fndArea.Value.Count > 0 Then
                qry += " and TSPL_MCC_MASTER.Area_Location_Code ='" + clsCommon.myCstr(fndArea.Value) + "' "
            End If
            txtMCC.Value = clsCommon.ShowSelectForm("vendorBadvice", qry, "Code", "", txtMCC.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If ChkIFSCCode.Checked Then
            Printt()
        Else
            Print(True)
        End If
    End Sub

    Sub SetToDate()
        'If MultipleFinderFillAuto Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0

        'If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
        '    clsCommon.MyMessageBoxShow("Please select the Location first")
        '    Exit Sub
        'End If
        'If MultipleFinderFillAuto = True Then
        '    If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please select the Location first")
        '        Exit Sub
        '    End If
        'End If
        Dim strMCCcode = ""
        If clsCommon.myLen(txtMCC.Value) Then
            strMCCcode = " location_Code in ( '" + clsCommon.myCstr(txtMCC.Value) + "')  and "
        End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + "  Location_Category='MCC' and Rejected_Type='N') ")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                ToDate.Value = fromDate.Value
                Exit Sub
            End If
            ToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

            If fromDate.Value.Month <> ToDate.Value.Month Then
                ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = ToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If fromDate.Value.Month <> dtNxtPay.Month Then
                ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            ToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = fromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            fromDate.Value = today.AddDays(-dayDiff)
            ToDate.Value = fromDate.Value.AddDays(6)
        End If
        ' End If
        'If clsCommon.myLen(txtMCC.Text) > 0 Then
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
        'Else
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
        'End If
        'End If
    End Sub

    Private Sub fromDate_Leave(sender As Object, e As EventArgs) Handles fromDate.Leave
        If MultipleFinderFillAuto Then
            SetToDate()
        Else
            SetToDate()
        End If
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As CancelEventArgs) Handles fromDate.Validating
        If MultipleFinderFillAuto Then
            SetToDate()
        Else
            SetToDate()
        End If
    End Sub

    Private Sub txtBankGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankGroup._MYValidating
        Try
            Dim qry As String = "select BANK_GROUP_CODE as Code,DESCRIPTION as Name from TSPL_BANK_GROUP_MASTER"
            txtBankGroup.Value = clsCommon.ShowSelectForm("fmVBA", qry, "Code", "", txtBankGroup.Value, "BANK_GROUP_CODE", isButtonClicked)
            qry = "select TSPL_BANK_GROUP_MASTER.DESCRIPTION from TSPL_BANK_GROUP_MASTER where BANK_GROUP_CODE ='" + txtBankGroup.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtbankgroupname.Text = clsCommon.myCstr(dt.Rows(0)("description"))
            Else
                txtbankgroupname.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Dim flagSaving As Boolean = False
    Dim flagCurrent As Boolean = False
    Private Sub rbtnBankAdvice_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnBankAdvice.ToggleStateChanged, rbtnBankWiseSummary.ToggleStateChanged, rbtnCurrentBankWiseSummary.ToggleStateChanged
        If Not flagSaving Then
            flagCurrent = True
            rbtnSaving.IsChecked = False
            rbtnSavingCompulsory.IsChecked = False
            rbtnSavingBankWiseSummary.IsChecked = False
            flagCurrent = False
        End If
    End Sub

    Private Sub rbtnSaving_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnSaving.ToggleStateChanged, rbtnSavingCompulsory.ToggleStateChanged, rbtnSavingBankWiseSummary.ToggleStateChanged
        If Not flagCurrent Then
            flagSaving = True
            rbtnBankAdvice.IsChecked = False
            rbtnBankWiseSummary.IsChecked = False
            rbtnCurrentBankWiseSummary.IsChecked = False
            flagSaving = False
        End If
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
            common.clsCommon.MyMessageBoxShow(err.Message)
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
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Sub Printt()
        Try


            If clsCommon.myLen(txtMCC.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select MCC.", Me.Text)
                txtMCC.Focus()
                Exit Sub
            End If

            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleFrom.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                'clsCommon.MyMessageBoxShow("Plz Select Payment Cycle From First.", Me.Text)
                'txtPaymentCycleFrom.Focus()
                'Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleTo.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                'clsCommon.MyMessageBoxShow("Plz Select Payment Cycle To First.", Me.Text)
                'txtPaymentCycleTo.Focus()
                'Exit Sub
            End If

            If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) AndAlso MultipleFinderFillAuto = False Then
                common.clsCommon.MyMessageBoxShow(Me, "From Payment Cycle can not be greater then to Payment Cycle")
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            If MultipleFinderFillAuto = False Then
                Patment_Cycle_changed()
            End If


            Dim strCycleRange As String = txtPaymentCycleTo.Value
            If clsCommon.myCdbl(txtPaymentCycleTo.Value) > clsCommon.myCdbl(txtPaymentCycleFrom.Value) Then
                strCycleRange = txtPaymentCycleTo.Value + " To  " + txtPaymentCycleFrom.Value
            End If
            Dim BaseQry As String = ""

            If rbtnSavingBankWiseSummary.IsChecked AndAlso rbtnCurrentBankWiseSummary.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, "You have to select only option at a time ", Me.Text)
                Exit Sub
            End If
            If rbtnSaving.IsChecked Then
                BaseQry = " select  '' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end)  as GRPColumn,
                                        TSPL_COMPANY_MASTER.Comp_Name
                                        ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name
                                        ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
              TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name, 
              TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc, 
              case when isnull(
                coalesce(
                  TSPL_VENDOR_MASTER.vsp_payment, 
                  Mp_V.vsp_payment
                ), 
                ''
              )= 'Self' then coalesce(
                TSPL_VENDOR_MASTER.IFSCCode2, mp_V.IFSC_Code
              ) else coalesce(
                TSPL_VENDOR_MASTER.Joint_IFSC_Code, 
                mp_v.Joint_IFSC_Code
              ) end as Payee_Joint_IFSC_Code, 
              case when isnull(
                coalesce(
                  TSPL_VENDOR_MASTER.vsp_payment, 
                  Mp_V.vsp_payment
                ), 
                ''
              )= 'Self' then coalesce( 
                TSPL_VENDOR_MASTER.AccNo2, mp_V.Account_No
              ) else coalesce(
                TSPL_VENDOR_MASTER.Joint_Account_No, 
                mp_V.Joint_Account_No
              ) end as Payee_Joint_Account_No,"
                If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                    BaseQry += " Round(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) as Payable_Amount "
                Else
                    BaseQry += " TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount "

                End If

                BaseQry += "from TSPL_PAYMENT_PROCESS_SAVING 
                                        left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No
                                        left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
                                        left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
                                        Left outer join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  
                                        left outer join ( select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                        left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
                                        left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_SAVING.VSP_CODE
                                        left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
                                        left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                                        left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Bank_Code
                                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                                        where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0  "

            ElseIf rbtnSavingCompulsory.IsChecked Then

                BaseQry = " select  '' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end)  as GRPColumn,TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],
                                        TSPL_COMPANY_MASTER.Comp_Name
                                        ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name
                                        ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
              TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name, 
              TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc, 
              case when isnull(
                coalesce(
                  TSPL_VENDOR_MASTER.vsp_payment, 
                  Mp_V.vsp_payment
                ), 
                ''
              )= 'Self' then coalesce(
                TSPL_VENDOR_MASTER.IFSCCode2, mp_V.IFSC_Code
              ) else coalesce(
                TSPL_VENDOR_MASTER.Joint_IFSC_Code, 
                mp_v.Joint_IFSC_Code
              ) end as Payee_Joint_IFSC_Code, 
              case when isnull(
                coalesce(
                  TSPL_VENDOR_MASTER.vsp_payment, 
                  Mp_V.vsp_payment
                ), 
                ''
              )= 'Self' then coalesce( 
                TSPL_VENDOR_MASTER.AccNo2, mp_V.Account_No
              ) else coalesce(
                TSPL_VENDOR_MASTER.Joint_Account_No, 
                mp_V.Joint_Account_No
              ) end as Payee_Joint_Account_No,"
                If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                    BaseQry += " Round(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) as Payable_Amount "
                Else
                    BaseQry += " TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount "

                End If

                BaseQry += "from TSPL_PAYMENT_PROCESS_COMPULSORY 
                                        left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No
                                        left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No
                                        left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
                                        Left outer join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  
                                        left outer join ( select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                        left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
                                        left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_COMPULSORY.VSP_CODE
                                        left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
                                        left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                                        left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank
                                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                                        where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0  "

            ElseIf rbtnSavingBankWiseSummary.IsChecked Then

                BaseQry = "select  '" + strCycleRange + "' AS CycleRange,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                Else
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                End If

                BaseQry += "SPL_COMPANY_MASTER.Comp_Name
            ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
            ,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME
            ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(MultipleFinderFillAuto = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
            ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc,TSPL_BANK_MASTER.DESCRIPTION as Bank_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " Round(isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                Else
                    If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                        BaseQry += " Round(isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                    Else
                        BaseQry += " isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0) as Payable_Amount "

                    End If

                End If
                BaseQry += " from TSPL_PAYMENT_PROCESS_DETAIL 
            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank 
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
            " + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
            where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + IIf(clsCommon.myLen(txtMCC.Value) > 0, "and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code='" + txtMCC.Value + "' And TSPL_MCC_MASTER.MCC_Code='" + txtMCC.Value + "' ", "") + "   and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount>0 "

            End If

            If rbtnBankAdvice.IsChecked OrElse rbtnBankWiseSummary.IsChecked Then
                If rbtnSaving.IsChecked = False AndAlso rbtnSavingCompulsory.IsChecked = False AndAlso rbtnSavingBankWiseSummary.IsChecked = False Then
                    BaseQry = "select  '" + strCycleRange + "' AS CycleRange,"
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                    Else
                        BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                    End If
                    BaseQry += "TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],
            TSPL_COMPANY_MASTER.Comp_Name
            ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
            ,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME
            ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(MultipleFinderFillAuto = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
            ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"
                    BaseQry += " And tspl_mcc_master.Area_Location_Code ='" + clsCommon.myCstr(fndArea.Value) + "' "

                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        BaseQry += " Round((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)),0) as Payable_Amount "
                    Else
                        If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                            BaseQry += " Round((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)),0) as Payable_Amount "
                        Else
                            'BaseQry += " Cast((isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)) as decimal(18)) as Payable_Amount "
                            BaseQry += " (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0)) as Payable_Amount "
                        End If

                    End If
                    BaseQry += " from TSPL_PAYMENT_PROCESS_DETAIL 
            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
            " + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
            where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + IIf(clsCommon.myLen(txtMCC.Value) > 0, "and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code='" + txtMCC.Value + "' And TSPL_MCC_MASTER.MCC_Code='" + txtMCC.Value + "' ", "") + "   and (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0))>0 "
                End If
            ElseIf rbtnCurrentBankWiseSummary.IsChecked Then
                BaseQry = "select  '" + strCycleRange + "' AS CycleRange,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code+TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code as GRPColumn,"
                Else
                    BaseQry += " TSPL_Vendor_MASTER.Bank_Code as GRPColumn,"
                End If

                BaseQry += "TSPL_COMPANY_MASTER.Comp_Name
            ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
            ,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,TSPL_MCC_MASTER.MCC_NAME
            ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_No," + IIf(MultipleFinderFillAuto = True, "", " TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, ") + " TSPL_Fiscal_Year_Master.Fiscal_Name
            ,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc , TSPL_BANK_MASTER.DESCRIPTION as Bank_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,"
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    BaseQry += " Round(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                Else
                    If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                        BaseQry += " Round(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0),0) as Payable_Amount "
                    Else
                        BaseQry += " isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DETAIL.Compulsory_Amount,0) as Payable_Amount "
                    End If

                End If
                BaseQry += " from TSPL_PAYMENT_PROCESS_DETAIL 
            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
            " + IIf(MultipleFinderFillAuto = True, "    ", " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code and  TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC' ") + "
            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) " + IIf(MultipleFinderFillAuto = True, "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected  ", " and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code=TSPL_Location_MASTER.Location_Code ") + " 
            where  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + IIf(clsCommon.myLen(txtMCC.Value) > 0, "and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code='" + txtMCC.Value + "' And TSPL_MCC_MASTER.MCC_Code='" + txtMCC.Value + "' ", "") + "   and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount>0 "

            End If

            If clsCommon.myLen(txtBankGroup.Value) > 0 Then
                BaseQry = BaseQry + " AND TSPL_BANK_MASTER.BANK_GROUP_CODE='" + txtBankGroup.Value + "' "
            End If
            Dim FinalQuery As String = ""

            If rbtnBankAdvice.IsChecked OrElse rbtnSavingCompulsory.IsChecked OrElse rbtnSaving.IsChecked Then
                FinalQuery = BaseQry + " order by TSPL_Vendor_MASTER.Bank_Code"
            ElseIf rbtnBankWiseSummary.IsChecked Then
                FinalQuery += "select ROW_NUMBER() over ( order by GRPColumn) as SNO , * from ( select max(CycleRange) as CycleRange, max(GRPColumn) as GRPColumn,max(Comp_Name) as Comp_Name,max(Comp_address) as Comp_address, max(From_Date) as From_Date,max(GSTReg_No) as GSTReg_No,max(Fiscal_Name) as Fiscal_Name,max(CycleNo) as CycleNo,max(Date_Range) as Date_Range,Bank_Code,Branch_Name,max(Bank_Code_Desc) as Bank_Code_Desc, max (Payee_Joint_IFSC_Code) as Payee_Joint_IFSC_Code,max(Payee_Joint_Account_No) as Payee_Joint_Account_No ,sum(Payable_Amount) as Payable_Amount
            ,max(CompPhone) as CompPhone,max(Regn_No) as Regn_No,max(MCC_NAME) as MCC_NAME
            from (" + Environment.NewLine + BaseQry + Environment.NewLine + " )xxx group by Bank_Code,Branch_Name )xxxx order by GRPColumn "
            ElseIf rbtnCurrentBankWiseSummary.IsChecked OrElse rbtnSavingBankWiseSummary.IsChecked Then
                FinalQuery += "select ROW_NUMBER() over ( order by GRPColumn) as SNO , * from ( select  max(GRPColumn) as GRPColumn,sum(Payable_Amount) as Payable_Amount , Bank_Desc
            from (" + Environment.NewLine + BaseQry + Environment.NewLine + " )xxx group by Bank_Desc )xxxx order by GRPColumn "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)


            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub

            End If


            Dim frmCRV As New frmCrystalReportViewer()
            'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceNeww", "Bank Advice New")
            'End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'============changes by shivani against[BM00000008663]
'' Liner qty add in report using Setting -> UpdateCrateLinerQty against ticket no. SWA/14/05/18-000022 Client Swadesh
Public Class RptCrateAccountingReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim UpdateCrateLinerQty As Boolean = False
    'Update by preeti gupta ticket no[BM00000007520,BM00000007709,BM00000007737,BM00000007709,BM00000007902,BM00000007963,BM00000007992,BM00000008986,BM00000009062,BM00000009978,KDI/15/06/18-000369]

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCrateAccountingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag

    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick

    End Sub


    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub Print(ByVal IsPrint As Exporter)
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            fromDate.Focus()
            Exit Sub
        End If


        Dim squeryClosing As String = String.Empty
        Dim MainQuery As String = String.Empty
        Dim strWhrClause As String = ""


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strWhrClause += "and Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strWhrClause += "and Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
        End If

        If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
            strWhrClause += "and Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
        End If

        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            strWhrClause += "and Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ")  "
        End If

        'Dim squery As String = "select TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CUSTOMER_CRATE_ACCOUNTING.Location_Code, TSPL_CUSTOMER_CRATE_ACCOUNTING.Crate_Opening_Date as DocDate, TSPL_CUSTOMER_CRATE_ACCOUNTING.Crate_Opening as CrateQty, 1 as [Type] from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_CRATE_ACCOUNTING ON TSPL_CUSTOMER_CRATE_ACCOUNTING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
        '" UNION ALL" + Environment.NewLine & _
        '" select '' as Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as DocDate, TSPL_SD_SALE_INVOICE_HEAD.CrateQty as CrateQty, 1 as [Type] from TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where Trans_Type ='FS'" + Environment.NewLine & _
        '" UNION ALL" + Environment.NewLine & _
        '" select '' as Cust_Group_Code,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as Adjustment,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty  as OutQtyNew,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CRATE_RECEIVED_Head_FRESHSALE.Location_Code, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as DocDate, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQty, -1 as [Type] from TSPL_CRATE_RECEIVED_head_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_detail_FRESHSALE ON TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_Head_FRESHSALE.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code"

        'squery = "Select max(Cust_Group_Code) as Cust_Group_Code,sum(Adjustment) as Adjustment,sum(OutQtyNew) as OutQtyNew, Cust_Code, MAX(Customer_Name) as Customer_Name, max(Location_Code) as Location_Code, CONVERT(DATE,'" & fromDate.Value & "',103) as DocDate, SUM(CrateQty*Type) as Opening, 0 as [OutQty], 0 as [InQty] from (" + Environment.NewLine & _
        '"" & squery & "" + Environment.NewLine & _
        '" ) as Opening WHERE convert(date,DocDate,103)<(convert(date,'" & fromDate.Value & "',103)) " & strWhrClause & " GROUP BY Cust_Code" + Environment.NewLine & _
        '" UNION ALL" + Environment.NewLine & _
        '" Select Cust_Group_Code, Adjustment, OutQtyNew,Cust_Code, Customer_Name, Location_Code, CONVERT(DATE,DocDate,103) as DocDate, 0 as Opening, Case When [Type]=1 Then CrateQty Else 0 End as OutQty, Case When [Type]=-1 Then CrateQty Else 0 End as InQty from (" + Environment.NewLine & _
        '"" & squery & "" + Environment.NewLine & _
        '" ) as Opening WHERE convert(date,DocDate,103)>=convert(date,'" & fromDate.Value & "',103) AND convert(date,DocDate,103)<=convert(date,'" & ToDate.Value & "',103) " & strWhrClause & ""

        'If rdbSummary.IsChecked Then
        '    squery = "Select  max(Cust_Group_Code) as Cust_Group_Code, Cust_Code, MAX(Customer_Name) as Customer_Name, sum(Opening) as Opening,SUM(OutQty) as OutQty, SUM(InQty) as InQty,SUM(OutQty)-SUM(InQty) as Short_Excess,sum(convert(decimal(18,0),OutQtyNew) )as OutQtyNew ,sum(convert(decimal(18,0),Adjustment)) as Adjustment, (SUM(Opening)+SUM(OutQty)-SUM(InQty)) as oldClosing, isnull(case when sum(Adjustment)=0 then (sum(Opening)+sum(OutQty)-sum(InQty)+sum(OutQtyNew)) else case when sum(OutQtyNew)=0 then (sum(Opening)+sum(OutQty)-sum(InQty)-sum(Adjustment) ) end end,0) as Closing from (" + Environment.NewLine & _
        '        "" & squery & "" + Environment.NewLine & _
        '        ") ZZZ Group  By Cust_Code"
        'Else
        '    squery = "With CTETemp as (" + Environment.NewLine & _
        '        " Select Adjustment ,OutQtyNew,Cust_Code, Customer_Name, Location_Code, convert(varchar,DocDate,103) as DocDate, Opening, OutQty, InQty, SUM(Closing) OVER (Partition BY Cust_code ORDER BY Cust_code, DocDate) as Closing, Row_Number() OVER (Partition BY Cust_code ORDER BY Cust_code, DocDate) as RowNo from (" + Environment.NewLine & _
        '        " Select sum(xxx.Adjustment) as Adjustment ,sum(xxx.OutQtyNew) as OutQtyNew , XXX.Cust_Code, max(XXX .Customer_Name) as Customer_Name, max(xxx.Location_Code) as Location_Code, max(XXX.DocDate) as DocDate, sum(xxx.Opening) as Opening, sum(XXX.OutQty) as OutQty, sum(xxx.InQty ) as InQty, (sum(xxx.Opening)+sum(XXX.OutQty)-sum(xxx.InQty)) as Closing  from (" + Environment.NewLine & _
        '        "" & squery & "" + Environment.NewLine & _
        '        " ) XXX GROUP BY Cust_Code, DocDate" + Environment.NewLine & _
        '        " ) YYY )" + Environment.NewLine & _
        '        "Select RW_P,RW_C,Cust_Code, Customer_Name, Location_Code, DocDate,Opening,OutQty, InQty, (OutQty-InQty) as Short_Excess,convert(decimal(18,0),OutQtyNew) as OutQtyNew ,convert(decimal(18,0),Adjustment) as Adjustment,(Opening+OutQty-InQty) as oldClosing ,(case  when Adjustment=0 and OutQtyNew=0 then  (Opening+OutQty-InQty ) when Adjustment=0 then (Opening+OutQty-InQty+OutQtyNew)   when OutQtyNew=0 then (Opening+OutQty-InQty-Adjustment )  else 0 end) as Closing from (" & _
        '        "Select CTETemp.Adjustment ,CTETemp.OutQtyNew ,CTETemp.Cust_Code, CTETemp.Customer_Name, CTETemp.Location_Code, CTETemp.DocDate, " & _
        '        "CTETemp.Opening+ISNULL(CT1.Closing,0)-coalesce((select sum(adjustment) from CTETemp prevv where prevv.rowno" & _
        '        "<CTETemp.rowno),0)-coalesce(ctetemp.adjustment,0) as Opening,CT1.Adjustment as Prev_adj,CTETemp.RowNo as RW_C,CT1.RowNo as RW_P," & _
        '        "CTETemp.OutQty, CTETemp.InQty from CTETemp LEFt OUTER JOIN CTETemp CT1 ON CT1.Cust_Code=CTETemp.Cust_Code AND (CTETemp.RowNo-CT1.RowNo)=1 " & _
        '        ") ZZZ ORDER BY  Cust_Code,convert(date,DocDate,103)"
        'End If
        Dim squeryOpening As String = "select TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CUSTOMER_CRATE_ACCOUNTING.Location_Code, TSPL_CUSTOMER_CRATE_ACCOUNTING.Crate_Opening_Date as DocDate, TSPL_CUSTOMER_CRATE_ACCOUNTING.Crate_Opening as CrateQty, 1 as [Type] , '' Vehicle_Code,'' as Remarks"
        If UpdateCrateLinerQty = True Then
            squeryOpening += " ,0 as LINER  "
        End If
        squeryOpening += "  from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_CRATE_ACCOUNTING ON TSPL_CUSTOMER_CRATE_ACCOUNTING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
             " select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as DocDate, TSPL_SD_SALE_INVOICE_HEAD.CrateQty as CrateQty, 1 as [Type] ,TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,'' as Remarks "

        If UpdateCrateLinerQty = True Then
            squeryOpening += " ,isnull(TSPL_SD_SALE_INVOICE_HEAD.LINER,0) as LINER "
        End If

        squeryOpening += " from TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  where Trans_Type ='FS'" + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
             " select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code,0  as Adjustment,0   as OutQtyNew,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CRATE_RECEIVED_Head_FRESHSALE.Location_Code, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as DocDate, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQty, -1 as [Type],TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Remarks as Remarks "

        If UpdateCrateLinerQty = True Then
            squeryOpening += " ,0 as LINER "
        End If

        squeryOpening += " from TSPL_CRATE_RECEIVED_head_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_detail_FRESHSALE ON TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_Head_FRESHSALE.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code" + Environment.NewLine & _
             " union all" + Environment.NewLine & _
             " select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew,  TSPL_SD_SALE_RETURN_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location as Location_Code, TSPL_SD_SALE_RETURN_HEAD.Document_Date as DocDate, TSPL_SD_SALE_RETURN_HEAD.CrateQty as CrateQty, -1 as [Type], TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,'' as Remarks "

        If UpdateCrateLinerQty = True Then
            squeryOpening += " ,isnull(TSPL_SD_SALE_INVOICE_HEAD.LINER,0) as LINER "
        End If

        squeryOpening += "from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='FS' and TSPL_SD_SALE_RETURN_HEAD.CrateQty >0" + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
             "select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code,0  as Adjustment,0   as OutQtyNew,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CRATE_RECEIVED_Head_FRESHSALE.Location_Code, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as DocDate,Adjustment as CrateQty, -1 as [Type],TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Remarks as Remarks"

        If UpdateCrateLinerQty = True Then
            squeryOpening += " ,0 as LINER "
        End If

        squeryOpening += " from TSPL_CRATE_RECEIVED_head_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_detail_FRESHSALE ON TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_Head_FRESHSALE.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code" + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
             "select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code,0  as Adjustment,0   as OutQtyNew,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CRATE_RECEIVED_Head_FRESHSALE.Location_Code, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as DocDate,OutQty  as CrateQty, 1 as [Type], TSPL_CRATE_RECEIVED_Head_FRESHSALE.Vehicle_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Remarks as Remarks"

        If UpdateCrateLinerQty = True Then
            squeryOpening += " ,0 as LINER "
        End If
        squeryOpening += "  from TSPL_CRATE_RECEIVED_head_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_detail_FRESHSALE ON TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_Head_FRESHSALE.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code"



        squeryClosing = "select TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CUSTOMER_CRATE_ACCOUNTING.Location_Code, TSPL_CUSTOMER_CRATE_ACCOUNTING.Crate_Opening_Date as DocDate, TSPL_CUSTOMER_CRATE_ACCOUNTING.Crate_Opening as CrateQty, 1 as [Type],'' Vehicle_Code,'' as Remarks"
        If UpdateCrateLinerQty = True Then
            squeryClosing += " ,0 as LINER  "
        End If
        squeryClosing += " from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_CRATE_ACCOUNTING ON TSPL_CUSTOMER_CRATE_ACCOUNTING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
         " UNION ALL" + Environment.NewLine & _
         " select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as DocDate, TSPL_SD_SALE_INVOICE_HEAD.CrateQty as CrateQty, 1 as [Type], TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,'' as Remarks"

        If UpdateCrateLinerQty = True Then
            squeryClosing += " ,isnull(TSPL_SD_SALE_INVOICE_HEAD.LINER,0) as LINER "
        End If
        squeryClosing += " from TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  where Trans_Type ='FS'" + Environment.NewLine & _
          " union all" + Environment.NewLine & _
          "select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code, 0 as Adjustment,0 as OutQtyNew,  TSPL_SD_SALE_RETURN_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location as Location_Code, TSPL_SD_SALE_RETURN_HEAD.Document_Date as DocDate, TSPL_SD_SALE_RETURN_HEAD.CrateQty as CrateQty, -1 as [Type],TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code,'' as Remarks"
        If UpdateCrateLinerQty = True Then
            squeryClosing += " ,isnull(TSPL_SD_SALE_INVOICE_HEAD.LINER,0) as LINER "
        End If
        squeryClosing += " from TSPL_SD_SALE_INVOICE_HEAD " & _
  "left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
  "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  where TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='FS' and TSPL_SD_SALE_RETURN_HEAD.CrateQty >0" & _
         " UNION ALL" + Environment.NewLine & _
         "  select TSPL_CUSTOMER_MASTER.Cust_Group_Code  as Cust_Group_Code,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as Adjustment,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty  as OutQtyNew,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CRATE_RECEIVED_Head_FRESHSALE.Location_Code, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as DocDate, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQty, -1 as [Type],TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Remarks as Remarks"
        If UpdateCrateLinerQty = True Then
            squeryClosing += " ,0 as LINER "
        End If
        squeryClosing += " from TSPL_CRATE_RECEIVED_head_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_detail_FRESHSALE ON TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_Head_FRESHSALE.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code "




        Dim squery As String = "Select max(Cust_Group_Code) as Cust_Group_Code,sum(Adjustment) as Adjustment,sum(OutQtyNew) as OutQtyNew, Cust_Code, MAX(Customer_Name) as Customer_Name, max(Location_Code) as Location_Code, CONVERT(DATE,'" & fromDate.Value & "',103) as DocDate, SUM(CrateQty*Type) as Opening, 0 as [OutQty], 0 as [InQty],max(Remarks) as Remarks"
        If UpdateCrateLinerQty = True Then
            squery += " ,sum(LINER) as LINER "
        End If
        squery += " from (" + Environment.NewLine & _
        "" & squeryOpening & "" + Environment.NewLine & _
        " ) as Opening WHERE convert(date,DocDate,103)<(convert(date,'" & fromDate.Value & "',103)) " & strWhrClause & " GROUP BY Cust_Code" + Environment.NewLine & _
        " UNION ALL" + Environment.NewLine & _
        " Select Cust_Group_Code, Adjustment, OutQtyNew,Cust_Code, Customer_Name, Location_Code, CONVERT(DATE,DocDate,103) as DocDate, 0 as Opening, Case When [Type]=1 Then CrateQty Else 0 End as OutQty, Case When [Type]=-1 Then CrateQty Else 0 End as InQty,Remarks as Remarks"

        If UpdateCrateLinerQty = True Then
            squery += " ,LINER as LINER"
        End If

        squery += "  from (" + Environment.NewLine & _
        "" & squeryClosing & "" + Environment.NewLine & _
        " ) as Closing WHERE convert(date,DocDate,103)>=convert(date,'" & fromDate.Value & "',103) AND convert(date,DocDate,103)<=convert(date,'" & ToDate.Value & "',103) " & strWhrClause & ""

        If rdbSummary.IsChecked Then
            MainQuery = "Select '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") & "'  as From_Daete,'" & clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") & "' as To_Date, max(Cust_Group_Code) as Cust_Group_Code, Cust_Code, MAX(Customer_Name) as Customer_Name, sum(Opening) as Opening,SUM(OutQty) as OutQty, SUM(InQty) as InQty,SUM(OutQty)-SUM(InQty) as Short_Excess,sum(convert(decimal(18,0),OutQtyNew) )as OutQtyNew ,sum(convert(decimal(18,0),Adjustment)) as Adjustment, (SUM(Opening)+SUM(OutQty)-SUM(InQty)) as oldClosing, isnull(case when sum(Adjustment)=0 then (sum(Opening)+sum(OutQty)-sum(InQty)+sum(OutQtyNew)) else case when sum(OutQtyNew)=0 then (sum(Opening)+sum(OutQty)-sum(InQty)-sum(Adjustment) ) end end,0) as Closing,max(Remarks) as Remarks"
            If UpdateCrateLinerQty = True Then
                MainQuery += " ,sum(LINER) as LINER "
            End If
            MainQuery += " from (" + Environment.NewLine & _
                "" & squery & "" + Environment.NewLine & _
                ") ZZZ  where not(ZZZ.opening =0   and ZZZ.outqty=0  and ZZZ.inQty=0 and ZZZ.Adjustment=0 and ZZZ.outqtynew=0) Group  By Cust_Code"
        Else
            MainQuery = "With CTETemp as (" + Environment.NewLine & _
                 "Select Adjustment ,OutQtyNew,Cust_Code, Customer_Name, Location_Code, convert(varchar,DocDate,103) as DocDate, Opening, OutQty, InQty, SUM(Closing) OVER (Partition BY Cust_code ORDER BY Cust_code, DocDate) as Closing, Row_Number() OVER (Partition BY Cust_code ORDER BY Cust_code, DocDate) as RowNo,Remarks as Remarks"
            If UpdateCrateLinerQty = True Then
                MainQuery += " ,LINER as LINER  "
            End If

            MainQuery += " from (" & _
                "Select sum(xxx.Adjustment) as Adjustment ,sum(xxx.OutQtyNew) as OutQtyNew , XXX.Cust_Code, max(XXX .Customer_Name) as Customer_Name, max(xxx.Location_Code) as Location_Code, max(XXX.DocDate) as DocDate, sum(xxx.Opening) as Opening, sum(XXX.OutQty) as OutQty, sum(xxx.InQty ) as InQty, (sum(xxx.Opening)+sum(XXX.OutQty)-sum(xxx.InQty)-sum(xxx.Adjustment )+sum(xxx.OutQtyNew )) as Closing,max(Remarks) as Remarks "
            If UpdateCrateLinerQty = True Then
                MainQuery += ",sum(LINER) as LINER "
            End If

            MainQuery += " from (" + Environment.NewLine & _
                "" & squery & "" + Environment.NewLine & _
                " ) XXX GROUP BY Cust_Code, DocDate" + Environment.NewLine & _
                " ) YYY  where not(YYY.opening =0   and YYY.outqty=0  and YYY.inQty=0 and YYY.Adjustment=0 and YYY.outqtynew=0))" + Environment.NewLine & _
               "Select '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") & "' as From_Daete,'" & clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") & "' as To_Date, Cust_Code, Customer_Name, Location_Code, DocDate,Opening,OutQty, InQty, (OutQty-InQty) as Short_Excess,convert(decimal(18,0),OutQtyNew) as OutQtyNew ,convert(decimal(18,0),Adjustment) as Adjustment,(Opening+OutQty-InQty-Adjustment +OutQtyNew ) as Closing,Remarks as Remarks"
            If UpdateCrateLinerQty = True Then
                MainQuery += " ,Liner  "
            End If
            MainQuery += " from (Select CTETemp.Adjustment ,CTETemp.OutQtyNew ,CTETemp.Cust_Code, CTETemp.Customer_Name, CTETemp.Location_Code, CTETemp.DocDate, " & _
                "CTETemp.Opening+ISNULL(CT1.Closing,0) as Opening," & _
                "CTETemp.OutQty, CTETemp.InQty,CTETemp.Remarks as Remarks"
            If UpdateCrateLinerQty = True Then
                MainQuery += " ,CTETemp.LINER as Liner "
            End If
            MainQuery += " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON CT1.Cust_Code=CTETemp.Cust_Code AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Cust_Code,convert(date,DocDate,103)"
        End If

        Dim dtgv As New DataTable
        ' Ticket No  KDI/02/05/18-000283 By Prabhakar Add From Date and To Date in print formte 

        dtgv = clsDBFuncationality.GetDataTable(MainQuery)
        'If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
        Gv1.DataSource = Nothing

        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.DataSource = dtgv

        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        FormatGridDetails()

        If rdbSummary.IsChecked Then
            Gv1.Tag = "Summary"
        Else
            Gv1.Tag = "Details"
        End If

        If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
        Gv1.MasterTemplate.AllowAddNewRow = False


        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
        End If

        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Crate Accounting Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Crate Accounting Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Sale Register", True)
        End If

        'clsCommon.MyMessageBoxShow("No Data Found")
        'End If
        If btnReferesh = False Then
            Dim frmCRV As New frmCrystalReportViewer()
            If rdbSummary.IsChecked = True Then
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dtgv, "crptCrateAccountingForSummary", "Crate Accounting")
            Else
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dtgv, "crptCrateAccountingForDetail", "Crate Accounting")
            End If
            frmCRV = Nothing
        End If
        'ReStoreGridLayout()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.IsChecked = True, "S", "D")
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub FormatGrid()


        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("document_date").IsVisible = False
        Gv1.Columns("document_date").Width = 100
        Gv1.Columns("document_date").HeaderText = "Doc Date"


        Gv1.Columns("customer_code").IsVisible = False
        Gv1.Columns("customer_code").Width = 100
        Gv1.Columns("customer_code").HeaderText = "Customer Code"


        Gv1.Columns("Customer_Name").IsVisible = True
        Gv1.Columns("Customer_Name").Width = 100
        Gv1.Columns("Customer_Name").HeaderText = " Customer Name"


        Gv1.Columns("opening_balance").IsVisible = True
        Gv1.Columns("opening_balance").Width = 100
        Gv1.Columns("opening_balance").HeaderText = "Opening Balance"


        Gv1.Columns("crateqty").IsVisible = True
        Gv1.Columns("crateqty").Width = 150
        Gv1.Columns("crateqty").HeaderText = "Issue Milk & Dahi Crates Qty"


        Gv1.Columns("crateqtyrecd").IsVisible = True
        Gv1.Columns("crateqtyrecd").Width = 100
        Gv1.Columns("crateqtyrecd").HeaderText = "Received Empty Crates"


        Gv1.Columns("Short_Excess").IsVisible = True
        Gv1.Columns("Short_Excess").Width = 100
        Gv1.Columns("Short_Excess").HeaderText = "Short / Excess "


        Gv1.Columns("Closing_balance").IsVisible = True
        Gv1.Columns("Closing_balance").Width = 100
        Gv1.Columns("Closing_balance").HeaderText = "Closing Balance"

        Gv1.Columns("Remarks").IsVisible = True
        Gv1.Columns("Remarks").Width = 100
        Gv1.Columns("Remarks").HeaderText = "Remarks"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)


        'gv.GroupDescriptors.Add(New GridGroupByExpression("DOC_DATE as Item format ""{0}: {1}"" Group By DOC_DATE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True


        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom


    End Sub
    Sub FormatGridDetails()


        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        If rdbDetail.IsChecked Then
            Gv1.Columns("DocDate").IsVisible = True
            Gv1.Columns("DocDate").Width = 100
            Gv1.Columns("DocDate").HeaderText = "Doc Date"
        End If

        'Gv1.Columns("From_Daete").IsVisible = False
        'Gv1.Columns("From_Daete").Width = 100
        'Gv1.Columns("From_Daete").HeaderText = "From Date"

        'Gv1.Columns("To_Date").IsVisible = False
        'Gv1.Columns("To_Date").Width = 100
        'Gv1.Columns("To_Date").HeaderText = "To Date"

        Gv1.Columns("Cust_Code").IsVisible = False
        Gv1.Columns("Cust_Code").Width = 100
        Gv1.Columns("Cust_Code").HeaderText = "Customer Code"


        Gv1.Columns("Customer_Name").IsVisible = True
        Gv1.Columns("Customer_Name").Width = 200
        Gv1.Columns("Customer_Name").HeaderText = " Customer Name"


        Gv1.Columns("opening").IsVisible = True
        Gv1.Columns("opening").Width = 100
        Gv1.Columns("opening").HeaderText = "Opening Balance"


        Gv1.Columns("OutQty").IsVisible = True
        Gv1.Columns("OutQty").Width = 150
        Gv1.Columns("OutQty").HeaderText = "Issue Milk & Dahi Crates Qty"


        Gv1.Columns("InQty").IsVisible = True
        Gv1.Columns("InQty").Width = 100
        Gv1.Columns("InQty").HeaderText = "Received Empty Crates"


        Gv1.Columns("Short_Excess").IsVisible = True
        Gv1.Columns("Short_Excess").Width = 100
        Gv1.Columns("Short_Excess").HeaderText = "Short / Excess "


        Gv1.Columns("Closing").IsVisible = True
        Gv1.Columns("Closing").Width = 100
        Gv1.Columns("Closing").HeaderText = "Closing Balance"

        Gv1.Columns("Remarks").IsVisible = True
        Gv1.Columns("Remarks").Width = 100
        Gv1.Columns("Remarks").HeaderText = "Remarks"

        Gv1.Columns("OutQtyNew").IsVisible = True
        Gv1.Columns("OutQtyNew").Width = 100
        Gv1.Columns("OutQtyNew").HeaderText = "Out Qty"

        Gv1.Columns("Adjustment").IsVisible = True
        Gv1.Columns("Adjustment").Width = 100
        Gv1.Columns("Adjustment").HeaderText = "Adjustment"

        If UpdateCrateLinerQty = True Then
            Gv1.Columns("LINER").IsVisible = True
            Gv1.Columns("LINER").Width = 100
            Gv1.Columns("LINER").HeaderText = "LINER"
        End If

      


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("crateqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("crateqtyrecd", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Short_Excess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("OutQtyNew", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Adjustment", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("DOC_DATE as Item format ""{0}: {1}"" Group By DOC_DATE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom


    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)


        rdbSummary.IsChecked = True
        Gv1.DataSource = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember))
            End If
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
    '    If (Gv1.Rows.Count <= 0) Then
    '        common.clsCommon.MyMessageBoxShow("No Data To Export")
    '        Exit Sub
    '    End If
    '    Print(Exporter.PDF)
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    'Private Function Form_ID() As String
    '    Throw New NotImplementedException
    'End Function

    Private Sub RptCrateAccountingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        UpdateCrateLinerQty = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UpdateCrateLinerQty, clsFixedParameterCode.UpdateCrateLinerQty, Nothing)) = "1", True, False))
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        Reset()
    End Sub


    Private Sub RptCrateAccountingReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Print(Exporter.Refresh)

    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        strQry = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,  TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER"
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

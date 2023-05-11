Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'------------------created by preeti gupta against ticket no [BM00000009941]

Public Class FrmMultipleCustomerforDo
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Dim qry As String = Nothing
    Dim dtgv As New DataTable
    Dim strBookingNo As String = Nothing
    Public Sub loadReportMultipleCustomer(strDocumentNo)
        qry = "select distinct Cast(1 as BIT) as 'Check', TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name   from TSPL_BOOKING_DETAIL" & _
                " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
                " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " & _
                " where TSPL_BOOKING_DETAIL.Document_No='" + strDocumentNo + "'"
        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dtgv
            gv1.GroupDescriptors.Clear()
            gv1.BestFitColumns()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub
   
    Public Function LoadPrintQuery(ByVal strCustomer As String) As String
        Dim Qry As String = " select  tspl_location_master.GSTNO as Loc_GSt_No,TSPL_STATE_MASTER.STATE_NAME as Loc_State_code,TSPL_STATE_MASTER.STATE_NAME as Loc_State_Name,TSPL_STATE_MASTER.GST_STATE_Code as Loc_GST_state_Code,TSPL_CUSTOMER_MASTER.GSTNO as cust_GSTIT,Cust_State.GST_STATE_Code as Cust_GST_State,Cust_State.STATE_CODE as Cust_State_Code,Cust_State.STATE_NAME as Cust_state_Name, tspl_item_master.HSN_Code ,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.logo_Img,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,TSPL_COMPANY_MASTER.Comp_Name AS CompName ," & _
                           " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP," & _
                           " TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER.STATE_NAME   )>0 then ', '+ TSPL_STATE_MASTER.STATE_NAME  else ' ' end    as Location_Address_GP, " & _
                           " TSPL_BOOKING_DETAIL.item_code,tspl_item_master.item_desc,TSPL_BOOKING_DETAIL.booking_Qty,TSPL_BOOKING_DETAIL.unit_Code,TSPL_BOOKING_DETAIL.item_Rate,TSPL_BOOKING_DETAIL.documentAmount,TSPL_BOOKING_DETAIL.vehicle_code,tspl_vehicle_master.Number as Vechicle_Name,TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date,TSPL_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.customer_name " & _
                          " ,TSPL_CUSTOMER_MASTER.add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.add2 as Cust_Add2,TSPL_CUSTOMER_MASTER.add3 as Cust_Add3,TSPL_CUSTOMER_MASTER.tin_no as Cust_TinNo,TSPL_CUSTOMER_MASTER.PIN_NO as Cust_PINNo ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PINCode" & _
                            " ,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_phone" & _
                             " ,TSPL_CUSTOMER_MASTER.City_Code ,Cust_City.city_name as Cust_City_name,TSPL_BOOKING_DETAIL.delivery_no" & _
                            " ,tspl_transport_master.transporter_name,tspl_delivery_note_master_freshsale.road_permit_no,tspl_delivery_note_master_freshsale.lorry_no,TSPL_CUSTOMER_MASTER.contact_person_fax,TSPL_BOOKING_DETAIL.Performance_Invoice_no,isnull(tspl_item_master.IsTaxable,0) as IsTaxable" & _
                           " from TSPL_BOOKING_DETAIL  left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
                           " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_BOOKING_MATSER.comp_code " & _
                           " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
                           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
                           " left join tspl_location_master on TSPL_LOCATION_MASTER .Location_Code =TSPL_BOOKING_MATSER.location_code " & _
                           " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER .STATE_CODE =tspl_location_master.State " & _
                           " left join tspl_item_master on tspl_item_master.item_code=TSPL_BOOKING_DETAIL.item_code " & _
                           " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code" & _
                          " left join tspl_transport_master on tspl_transport_master.transport_id=tspl_vehicle_master.transport_id" & _
                           " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .cust_code=TSPL_BOOKING_DETAIL.cust_code " & _
                          " left join TSPL_STATE_MASTER as Cust_State  on Cust_State .STATE_CODE =TSPL_CUSTOMER_MASTER.State  " & _
                            " left outer join TSPL_CITY_MASTER  as Cust_City on Cust_City .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                     " left join tspl_delivery_note_master_freshsale on tspl_delivery_note_master_freshsale.document_no=TSPL_BOOKING_DETAIL.delivery_no "
        Qry += " where TSPL_BOOKING_DETAIL.cust_code in ('" + strCustomer + "') and TSPL_BOOKING_DETAIL.Document_No='" + strBookingNo + "'"
        Return Qry
    End Function
    


    Public Function LoadPrintQueryGMD(ByVal strCustomer As String) As String
        Dim Qry As String = " select  tspl_location_master.GSTNO as Loc_GSt_No,TSPL_STATE_MASTER.STATE_NAME as Loc_State_code,TSPL_STATE_MASTER.STATE_NAME as Loc_State_Name,TSPL_STATE_MASTER.GST_STATE_Code as Loc_GST_state_Code,TSPL_CUSTOMER_MASTER.GSTNO as cust_GSTIT,Cust_State.GST_STATE_Code as Cust_GST_State,Cust_State.STATE_CODE as Cust_State_Code,Cust_State.STATE_NAME as Cust_state_Name, tspl_item_master.HSN_Code ,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.logo_Img,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,TSPL_COMPANY_MASTER.Comp_Name AS CompName ," & _
                           " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP," & _
                           " TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER.STATE_NAME   )>0 then ', '+ TSPL_STATE_MASTER.STATE_NAME  else ' ' end    as Location_Address_GP " & _
                           " ,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.Email as Loc_Email" & _
                           ",convert(varchar,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End) as  P_Phn" & _
                          " ,TSPL_BOOKING_DETAIL.item_code,tspl_item_master.item_desc,TSPL_BOOKING_DETAIL.booking_Qty,TSPL_BOOKING_DETAIL.unit_Code,TSPL_BOOKING_DETAIL.item_Rate,TSPL_BOOKING_DETAIL.documentAmount,TSPL_BOOKING_DETAIL.vehicle_code,tspl_vehicle_master.Number as Vechicle_Name,TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date,TSPL_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.customer_name " & _
                          " ,TSPL_CUSTOMER_MASTER.add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.add2 as Cust_Add2,TSPL_CUSTOMER_MASTER.add3 as Cust_Add3,TSPL_CUSTOMER_MASTER.tin_no as Cust_TinNo,TSPL_CUSTOMER_MASTER.PIN_NO as Cust_PINNo ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PINCode" & _
                            " ,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_phone" & _
                             " ,TSPL_CUSTOMER_MASTER.City_Code ,Cust_City.city_name as Cust_City_name,TSPL_BOOKING_DETAIL.delivery_no" & _
                            " ,tspl_transport_master.transporter_name,tspl_delivery_note_master_freshsale.road_permit_no,tspl_delivery_note_master_freshsale.lorry_no,TSPL_CUSTOMER_MASTER.contact_person_fax,TSPL_BOOKING_DETAIL.Performance_Invoice_no,isnull(tspl_item_master.IsTaxable,0) as IsTaxable" & _
                           ",isnull(TSPL_BOOKING_DETAIL.Tax_Amount,0) Tax_Amount,TSPL_BOOKING_DETAIL.Item_Selling_Price from TSPL_BOOKING_DETAIL  left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
                           " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_BOOKING_MATSER.comp_code " & _
                           " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
                           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
                           " left join tspl_location_master on TSPL_LOCATION_MASTER .Location_Code =TSPL_BOOKING_MATSER.location_code " & _
                           " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER .STATE_CODE =tspl_location_master.State " & _
                           " left join tspl_item_master on tspl_item_master.item_code=TSPL_BOOKING_DETAIL.item_code " & _
                           " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code" & _
                          " left join tspl_transport_master on tspl_transport_master.transport_id=tspl_vehicle_master.transport_id" & _
                           " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .cust_code=TSPL_BOOKING_DETAIL.cust_code " & _
                          " left join TSPL_STATE_MASTER as Cust_State  on Cust_State .STATE_CODE =TSPL_CUSTOMER_MASTER.State  " & _
                            " left outer join TSPL_CITY_MASTER  as Cust_City on Cust_City .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                     " left join tspl_delivery_note_master_freshsale on tspl_delivery_note_master_freshsale.document_no=TSPL_BOOKING_DETAIL.delivery_no "
        Qry += " where TSPL_BOOKING_DETAIL.cust_code in ('" + strCustomer + "') and TSPL_BOOKING_DETAIL.Document_No='" + strBookingNo + "'"
        Return Qry
    End Function



    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = False
            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
    Sub loadData()


        ArrInvoice_Arr = New ArrayList


        Dim strCustomerNo As String = ""

        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                strCustomerNo = strCustomerNo + "','" + clsCommon.myCstr(grow.Cells("Cust_Code").Value)
            End If
        Next

        If clsCommon.myLen(strCustomerNo) > 0 AndAlso clsCommon.myCstr(strCustomerNo).Substring(0, 3) = "','" Then
            strCustomerNo = strCustomerNo.Substring(3, strCustomerNo.Length - 3)
        End If
        'Ticket No-MIL/02/07/19-000103,sanjay
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
            Dim Qry As String = LoadPrintQueryGMD(strCustomerNo)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()

                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("IsTaxable")), "1") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDSBokingTaxable", "Performa Invoice", "rptCompanyAddress.rpt")
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDSBoking", "Performa Invoice", "rptCompanyAddress.rpt")
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Else
            Dim Qry As String = LoadPrintQuery(strCustomerNo)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDSBoking", "Performa Invoice", "rptCompanyAddress.rpt")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        loadData()
    End Sub

    Public Sub New(ByVal strDoc As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If strDoc IsNot Nothing AndAlso strDoc.Length > 0 Then
            loadReportMultipleCustomer(strDoc)
            strBookingNo = strDoc
        End If

    End Sub

    Private Sub FrmMultipleCustomerforDo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=============================added by preeti gupta=========================='ticket no [BM00000009682]
Public Class FrmCrateJaliReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCrateAccountingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub FormatGridDetails()

        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("CustomerCode").IsVisible = True
        Gv1.Columns("CustomerCode").Width = 100
        Gv1.Columns("CustomerCode").HeaderText = "Customer Code"

        Gv1.Columns("CustomerName").IsVisible = True
        Gv1.Columns("CustomerName").Width = 100
        Gv1.Columns("CustomerName").HeaderText = "Customer Name"

        Gv1.Columns("VehicleCode").IsVisible = True
        Gv1.Columns("VehicleCode").Width = 100
        Gv1.Columns("VehicleCode").HeaderText = "Vehicle Code"


        Gv1.Columns("Opening").IsVisible = True
        Gv1.Columns("Opening").Width = 100
        Gv1.Columns("Opening").HeaderText = "Opening"

        Gv1.Columns("Issue").IsVisible = True
        Gv1.Columns("Issue").Width = 100
        Gv1.Columns("Issue").HeaderText = "Issue"

        Gv1.Columns("Receive").IsVisible = True
        Gv1.Columns("Receive").Width = 100
        Gv1.Columns("Receive").HeaderText = "Receive"

        Gv1.Columns("Closing").IsVisible = True
        Gv1.Columns("Closing").Width = 100
        Gv1.Columns("Closing").HeaderText = "Closing"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Issue", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Receive", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        strQry = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,  TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER"
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub


    Private Sub FrmCrateJaliReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        Reset()
    End Sub

   

    'Sub Print(ByVal IsPrint As Exporter)
    '    FormatGridDetails()
    'End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        loaddata()

    End Sub
   
    '===========================added by preeti gupta 04/10/2016=====================
    Public Sub loaddata()
        Dim dt As DataTable
       
        Dim QryForCustomerOpening As String = Nothing
        Dim QryForCustomerclosing As String = Nothing
        Dim finalQueryForCustomer As String = Nothing
        Dim qry As String = Nothing
      

      

        QryForCustomerOpening = "select convert(date,'" + fromDate.Value + "',103) as Doc_Date,Opening.Vehicle_Code ,Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty from " & _
                   " (" & _
                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty   from TSPL_SD_SHIPMENT_HEAD  where trans_type='PS' " & _
                    " union all " & _
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty ," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty," & _
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
                    " 0 BoxQtyRecd ," & _
                    " 0 as CrateOutQty," & _
                    " 0 jaaliOutQty," & _
                    " 0 boxoutqty," & _
                    " 0  as CrateAdjQty," & _
                    " 0  as JaaliAdjQty," & _
                    " 0  as BoxAdjQty" & _
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' " & _
                    " union all" & _
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty ," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty," & _
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
                    " 0 BoxQtyRecd ," & _
                    " 0 as CrateOutQty," & _
                    " 0 jaaliOutQty," & _
                    " 0 boxoutqty," & _
                    " 0  as CrateAdjQty," & _
                     " 0  as JaaliAdjQty," & _
                    " 0  as BoxAdjQty" & _
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' " & _
                    " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + fromDate.Value + "',103))" & _
                    " group by Vehicle_Code,Customer_Code " + Environment.NewLine '----------Qry for Branch opening'



        QryForCustomerclosing = "select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd," + Environment.NewLine & _
                    " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty " + Environment.NewLine & _
                     " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I')" & _
                    " union all " + Environment.NewLine & _
                    " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'" & _
                    " union all " + Environment.NewLine & _
                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty," & _
                    " 0 as CrateQtyRecd, 0 JaaliQtyRecd , " & _
                    " 0  as BoxQtyRecd ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty   " & _
                    " from TSPL_SD_SHIPMENT_HEAD  where trans_type='PS') " & _
                    " ) as Closing " + Environment.NewLine & _
                    " WHERE convert(date,Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Environment.NewLine '----------Qry for Branch Closing'

        finalQueryForCustomer = "select convert(date,Doc_Date,103)  as Doc_Date,xx.Vehicle_Code ,xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing," & _
                    " (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing," & _
                    " (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing " & _
                    " from (" & _
                    "" & QryForCustomerOpening & "" & _
                    " UNION All " + Environment.NewLine '---------------------bada wala Union(between opening and closing) 
        finalQueryForCustomer += "" & QryForCustomerclosing & "" & _
                    "   ) as xx where 2=2  "
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        finalQueryForCustomer += " GROUP BY Vehicle_Code,Customer_Code,convert(date,Doc_Date,103) "

        '==========================================END CUSTOMER=========================================================================


        qry = "select  pp.Doc_Date  as Doc_Date,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty from ( " + Environment.NewLine & _
                     " " & finalQueryForCustomer & "" + Environment.NewLine & _
                    " ) as pp  "
        qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " + Environment.NewLine & _
             " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2"


        qry = " With CTETemp as (" & _
                   " Select convert(varchar,Doc_Date,103) as Doc_Date,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name, OpencrateQty, OpenJaaliQty, OpenBoxQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd,CrateOutQty," & _
                   " jaaliOutQty,boxOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as CrateQtyClosing, " & _
                   " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as JaaliQtyClosing, " & _
                   " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as BoxQtyClosing," & _
                   " Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as RowNo" & _
                   " from(" + Environment.NewLine & _
                   " " & qry & " " & _
                   " ) YYY )" & _
                   " Select convert(varchar,Doc_Date,103) as Date, Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Customer_Code as [Customer Code],Customer_Name as [Customer Name], OpencrateQty,OpenJaaliQty,OpenBoxQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd ,CrateOutQty," & _
                   " jaaliOutQty,boxOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty," & _
                   " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," & _
                   " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," & _
                   " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing" & _
                   " from (Select CTETemp.Doc_Date ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name,  CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " & _
                   " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " & _
                   " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," & _
                   " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd," & _
                   " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty " & _
                   " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and " & _
                   " CT1.Vehicle_Code = CTETemp.Vehicle_Code " & _
                   " AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Customer_Code,convert(date,Doc_Date,103),Vehicle_Code"



        dt = clsDBFuncationality.GetDataTable(qry)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.DataSource = dt
        Gv1.BestFitColumns()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        FormatGrid()
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
        Next

        Gv1.Columns("Date").IsVisible = True
        Gv1.Columns("Date").Width = 100
        Gv1.Columns("Date").HeaderText = "Date"

        Gv1.Columns("Vehicle Code").IsVisible = True
        Gv1.Columns("Vehicle Code").Width = 100
        Gv1.Columns("Vehicle Code").HeaderText = "Vehicle Code"

        Gv1.Columns("Vehicle Name").IsVisible = True
        Gv1.Columns("Vehicle Name").Width = 100
        Gv1.Columns("Vehicle Name").HeaderText = "Vehicle Name"

        Gv1.Columns("Customer Code").IsVisible = True
        Gv1.Columns("Customer Code").Width = 100
        Gv1.Columns("Customer Code").HeaderText = "Customer Code"

        Gv1.Columns("Customer Name").IsVisible = True
        Gv1.Columns("Customer Name").Width = 100
        Gv1.Columns("Customer Name").HeaderText = "Customer Name"

       
        Gv1.Columns("OpencrateQty").IsVisible = True
        Gv1.Columns("OpencrateQty").Width = 100
        Gv1.Columns("OpencrateQty").HeaderText = "Crate"
        Gv1.Columns("OpencrateQty").FormatString = "{0:F0}"

        Gv1.Columns("OpenJaaliQty").IsVisible = True
        Gv1.Columns("OpenJaaliQty").Width = 100
        Gv1.Columns("OpenJaaliQty").HeaderText = "Jaali"
        Gv1.Columns("OpenJaaliQty").FormatString = "{0:F0}"


        Gv1.Columns("OpenBoxQty").IsVisible = True
        Gv1.Columns("OpenBoxQty").Width = 100
        Gv1.Columns("OpenBoxQty").HeaderText = "BOX"
        Gv1.Columns("OpenBoxQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateQtyRecd").IsVisible = True
        Gv1.Columns("CrateQtyRecd").Width = 100
        Gv1.Columns("CrateQtyRecd").HeaderText = "Crate"
        Gv1.Columns("CrateQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("JaaliQtyRecd").IsVisible = True
        Gv1.Columns("JaaliQtyRecd").Width = 100
        Gv1.Columns("JaaliQtyRecd").HeaderText = "Jaali"
        Gv1.Columns("JaaliQtyRecd").FormatString = "{0:F0}"


        Gv1.Columns("BoxQtyRecd").IsVisible = True
        Gv1.Columns("BoxQtyRecd").Width = 100
        Gv1.Columns("BoxQtyRecd").HeaderText = "BOX"
        Gv1.Columns("BoxQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("CrateOutQty").IsVisible = True
        Gv1.Columns("CrateOutQty").Width = 100
        Gv1.Columns("CrateOutQty").HeaderText = "Crate"
        Gv1.Columns("CrateOutQty").FormatString = "{0:F0}"

        Gv1.Columns("jaaliOutQty").IsVisible = True
        Gv1.Columns("jaaliOutQty").Width = 100
        Gv1.Columns("jaaliOutQty").HeaderText = "Jaali"
        Gv1.Columns("jaaliOutQty").FormatString = "{0:F0}"


        Gv1.Columns("boxOutQty").IsVisible = True
        Gv1.Columns("boxOutQty").Width = 100
        Gv1.Columns("boxOutQty").HeaderText = "BOX"
        Gv1.Columns("boxOutQty").FormatString = "{0:F0}"



        Gv1.Columns("CrateAdjQty").IsVisible = True
        Gv1.Columns("CrateAdjQty").Width = 100
        Gv1.Columns("CrateAdjQty").HeaderText = "Crate"
        Gv1.Columns("CrateAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("JaaliAdjQty").IsVisible = True
        Gv1.Columns("JaaliAdjQty").Width = 100
        Gv1.Columns("JaaliAdjQty").HeaderText = "Jaali"
        Gv1.Columns("JaaliAdjQty").FormatString = "{0:F0}"


        Gv1.Columns("BoxAdjQty").IsVisible = True
        Gv1.Columns("BoxAdjQty").Width = 100
        Gv1.Columns("BoxAdjQty").HeaderText = "BOX"
        Gv1.Columns("BoxAdjQty").FormatString = "{0:F0}"


        Gv1.Columns("CrateQtyClosing").IsVisible = True
        Gv1.Columns("CrateQtyClosing").Width = 100
        Gv1.Columns("CrateQtyClosing").HeaderText = "Crate"
        Gv1.Columns("CrateQtyClosing").FormatString = "{0:F0}"

        Gv1.Columns("JaaliQtyClosing").IsVisible = True
        Gv1.Columns("JaaliQtyClosing").Width = 100
        Gv1.Columns("JaaliQtyClosing").HeaderText = "Jaali"
        Gv1.Columns("JaaliQtyClosing").FormatString = "{0:F0}"


        Gv1.Columns("BoxQtyClosing").IsVisible = True
        Gv1.Columns("BoxQtyClosing").Width = 100
        Gv1.Columns("BoxQtyClosing").HeaderText = "BOX"
        Gv1.Columns("BoxQtyClosing").FormatString = "{0:F0}"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("OpencrateQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("OpenJaaliQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("OpenBoxQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("CrateQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("JaaliQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("BoxQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("CrateOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("jaaliOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("boxOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("CrateQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("JaaliQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("BoxQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)

        Dim item13 As New GridViewSummaryItem("CrateAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("JaaliAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("BoxAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        View()
    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Date"))
            'view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Type"))
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Vehicle Code"))
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Vehicle Name"))
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Customer Code"))
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Customer Name"))
          

            view.ColumnGroups.Add(New GridViewColumnGroup("OPENING"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpencrateQty"))
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpenJaaliQty"))
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpenBoxQty"))

            view.ColumnGroups.Add(New GridViewColumnGroup("RECEIVE"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("CrateQtyRecd"))
            view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("JaaliQtyRecd"))
            view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("BoxQtyRecd"))

            view.ColumnGroups.Add(New GridViewColumnGroup("ISSUE"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("CrateOutQty"))
            view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("jaaliOutQty"))
            view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("boxOutQty"))

            view.ColumnGroups.Add(New GridViewColumnGroup("ADJUSTMENT"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("CrateAdjQty"))
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("JaaliAdjQty"))
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("BoxAdjQty"))

            view.ColumnGroups.Add(New GridViewColumnGroup("CLOSING"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("CrateQtyClosing"))
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("JaaliQtyClosing"))
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("BoxQtyClosing"))


            Gv1.ViewDefinition = view
        End If

    End Sub
   
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Print(Exporter.Excel)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)

    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtVehicle.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtVehicle.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Plant Customer Demand Report", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Plant Customer Demand Report", Gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

End Class

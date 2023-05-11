Imports common
Imports System.Data.SqlClient

Public Class FrmTransfer3rdDoc


    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Emp_Name  from TSPL_LOCATION_MASTER"
        txtSalesman.Value = clsCommon.ShowSelectForm("transferSale", qry, "Code", "Location_Type='Logical'", txtSalesman.Value, "Code", isButtonClicked)
        lblSalesman.Text = connectSql.RunScalar("select Location_Desc as Emp_Name from TSPL_LOCATION_MASTER where Location_Code= '" + txtSalesman.Value + "'")

    End Sub

    Private Sub txtVehicle__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicle._MYValidating
        Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
        txtVehicle.Value = clsCommon.ShowSelectForm("Vehicle No", qry, "vehicle_id", "", txtVehicle.Value, "vehicle_id", isButtonClicked)
        'txtVehicle.Text = txtVehicle.Value
        lblVehicle.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicle.Value) + "'")

    End Sub

    Function GetExcludedTransfer() As String
        Dim qry As String = "SELECT  top 1 case when len(ISNULL(xxx.files,''))>0 then SUBSTRING(xxx.files,0,len(ISNULL(xxx.files,''))) else '' end as FilesAttached    "
        qry += " FROM TSPL_TRANSFER_HEAD "
        qry += " CROSS APPLY(select Reference_Doc_No_DPL+',' as [text()]  from TSPL_TRANSFER_HEAD where LEN(ISNULL(Reference_Doc_No_DPL,''))>0 FOR XML PATH('')) xxx (files)"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
    Sub LoadData()
        Try
            If clsCommon.myLen(txtSalesman.Value) <= 0 Then
                Throw New Exception("Please enter salesman")
            End If
            If clsCommon.myLen(txtVehicle.Value) <= 0 Then
                Throw New Exception("Please enter vehicle")
            End If



            Dim qry As String = "select  Route_No,max(Route_Desc) as Route_Desc  from TSPL_TRANSFER_HEAD where Trans_Type in ('Excise','Depot') and LEN(Route_No)>0 and Transfer_Date='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and Salesmancode='" + txtSalesman.Value + "' and Vehicle_Code='" + txtVehicle.Value + "' and Transfer_Type='LO' and Post='Y' and LEN( Reference_Doc_No)<=0 "
            Dim strExcludedTransfer As String = GetExcludedTransfer()
            If clsCommon.myLen(strExcludedTransfer) > 0 Then
                qry += " and Transfer_No not in (" + strExcludedTransfer + ")"
            End If
            qry += " group by Route_No"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv2.DataSource = Nothing
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.DataSource = dt

            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 100
            gv1.Columns("Route_No").HeaderText = "Route Code"
            ' gv1.MasterTableView.Columns[3].OrderIndex=5

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 250
            gv1.Columns("Route_Desc").HeaderText = "Route"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    'Private Function EnableDisable(ByVal val As Boolean)
    '    txtDate.Enabled = val
    '    txtVehicle.Enabled = val
    '    txtSalesman.Enabled = val
    'End Function

    Sub LoadTransfer()
        gv2.DataSource = Nothing
        gv2.Columns.Clear()
        gv2.Rows.Clear()

        If gv1.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells("Route_No").Value)) > 0 Then
                Dim qry As String = "select  Transfer_No  from TSPL_TRANSFER_HEAD where Trans_Type in ('Excise','Depot') and LEN(Route_No)>0 and Transfer_Date='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and Salesmancode='" + txtSalesman.Value + "' and Vehicle_Code='" + txtVehicle.Value + "' and Route_No='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Route_No").Value) + "' and Transfer_Type='LO' and Post='Y'"
                Dim strExcludedTransfer As String = GetExcludedTransfer()
                If clsCommon.myLen(strExcludedTransfer) > 0 Then
                    qry += " and Transfer_No not in (" + strExcludedTransfer + ")"
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                gv2.DataSource = dt

                gv2.TableElement.TableHeaderHeight = 40
                gv2.MasterTemplate.ShowRowHeaderColumn = False
                For ii As Integer = 0 To gv2.Columns.Count - 1
                    gv2.Columns(ii).ReadOnly = True
                    gv2.Columns(ii).IsVisible = False
                Next

                gv2.Columns("Transfer_No").IsVisible = True
                gv2.Columns("Transfer_No").Width = 300
                gv2.Columns("Transfer_No").HeaderText = "Transfer No"
            End If
        End If
    End Sub

    Private Sub FrmTransfer3rdDoc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSalesman.Enabled = True
        txtVehicle.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub


    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click

        Try
            LoadTransfer()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_RowsChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCollectionChangingEventArgs) Handles gv1.RowsChanging

    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        Try
            If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
                LoadTransfer()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCreateTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateTransfer.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strSalesmanLocation As String = clsFixedParameter.GetData(clsFixedParameterType.SalesmanPhysicalLocation, clsFixedParameterCode.SalesmanPhysicalLocation, trans)
            If clsCommon.myLen(strSalesmanLocation) <= 0 Then
                Throw New Exception("Please set Salesman physical location fixed parameter")
            End If
            Dim obj As clsTransferMaster = createLoadout(strSalesmanLocation, trans)
            trans.Commit()
            If clsCommon.MyMessageBoxShow("Transfer No " + obj.Transfer_No + " Saved successfully." + Environment.NewLine + "Open the current Document", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim frm As New frmTransferNew(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                frm.strTrnasfer = obj.Transfer_No
                frm.Show()
            End If
            LoadData()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Function createLoadout(ByVal strFromLocation As String, ByVal trans As SqlTransaction) As clsTransferMaster
        ''Create Load Out Transaction
        Dim obj As clsTransferMaster = Nothing
        Dim arr As List(Of clsTransferMaster) = Nothing
        If gv2.Rows.Count > 1 Then
            arr = New List(Of clsTransferMaster)
        End If
        Dim strRefDocNo As String = ""
        Dim strRefDocNoDB As String = ""
        For ii As Integer = 0 To gv2.Rows.Count - 1
            Dim strCode As String = clsCommon.myCstr(gv2.Rows(ii).Cells("Transfer_No").Value)
            If ii = 0 Then
                obj = clsTransferMaster.GetData(strCode, trans)
                strRefDocNo = strCode
                strRefDocNoDB = "'" + strCode + "'"
            Else
                arr.Add(clsTransferMaster.GetData(strCode, trans))
                strRefDocNo += "," + strCode
                strRefDocNoDB += ",'" + strCode + "'"
            End If
        Next

        Dim objSMLO As clsTransferMaster = obj.DeepCopyObject(obj, arr)
        objSMLO.Transfer_No = "" ''obj.Reference_Doc_No New Entrt Generate
        objSMLO.Reference_Doc_No = strRefDocNo
        objSMLO.Reference_Doc_No_DPL = strRefDocNoDB
        objSMLO.Post = "N"
        objSMLO.From_Location = strFromLocation
        objSMLO.FromLoc_Desc = clsLocation.GetName(strFromLocation, trans)
        Dim qry As String = "Select employee_code,NonPrice_Code,Type from TSPL_ROUTE_MASTER where route_no = '" + obj.Route_No + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Route details not found for route " + obj.Route_No)
        End If
        objSMLO.Route_Type_Id = clsCommon.myCstr(dt.Rows(0)("Type"))
        objSMLO.To_Location = obj.Salesmancode

        If clsCommon.myLen(objSMLO.To_Location) <= 0 Then
            Throw New Exception("Salesman not found ")
        End If
        objSMLO.ToLoc_Desc = clsEmployeeMaster.GetName(obj.Salesmancode, trans)


        objSMLO.Price_Code = clsCommon.myCstr(dt.Rows(0)("NonPrice_Code"))
        If clsCommon.myLen(objSMLO.Price_Code) <= 0 Then
            Throw New Exception("Non Excisable Price not found for route No " + obj.Route_No)
        End If
        objSMLO.Location_Type = "Logical"
        objSMLO.Trans_Type = "Route"
        objSMLO.Item_Amount = 0
        objSMLO.Total_Tax_Amount = 0
        objSMLO.Total_Item_Amount = 0
        objSMLO.ManualTransferNo = ""
        objSMLO.Tax_Group = ""
        objSMLO.Against_Indent_No = ""
        objSMLO.TAX1 = ""
        objSMLO.TAX1_Amt = 0
        objSMLO.Tax1_Assessable_Amt = 0
        objSMLO.TAX1_Rate = 0

        objSMLO.TAX2 = ""
        objSMLO.TAX2_Amt = 0
        objSMLO.Tax2_Assessable_Amt = 0
        objSMLO.TAX2_Rate = 0

        objSMLO.TAX3 = ""
        objSMLO.TAX3_Amt = 0
        objSMLO.Tax3_Assessable_Amt = 0
        objSMLO.TAX3_Rate = 0

        objSMLO.TAX4 = ""
        objSMLO.TAX4_Amt = 0
        objSMLO.Tax4_Assessable_Amt = 0
        objSMLO.TAX4_Rate = 0

        objSMLO.TAX5 = ""
        objSMLO.TAX5_Amt = 0
        objSMLO.Tax5_Assessable_Amt = 0
        objSMLO.TAX5_Rate = 0

        objSMLO.TAX6 = ""
        objSMLO.TAX6_Amt = 0
        objSMLO.Tax6_Assessable_Amt = 0
        objSMLO.TAX6_Rate = 0
        objSMLO.Total_Basic_Amt = 0
        objSMLO.Total_Transfer_Amount = 0
        objSMLO.is_Auto_Created_Trans = True
        For ii As Integer = 0 To objSMLO.Arr.Count - 1
            objSMLO.Arr(ii).LoadIn_Qty = 0
            objSMLO.Arr(ii).TAX1 = ""
            objSMLO.Arr(ii).TAX1_Amt = 0
            objSMLO.Arr(ii).Tax1_Assessable_Amt = 0
            objSMLO.Arr(ii).TAX1_Rate = 0

            objSMLO.Arr(ii).TAX2 = ""
            objSMLO.Arr(ii).TAX2_Amt = 0
            objSMLO.Arr(ii).Tax2_Assessable_Amt = 0
            objSMLO.Arr(ii).TAX2_Rate = 0

            objSMLO.Arr(ii).TAX3 = ""
            objSMLO.Arr(ii).TAX3_Amt = 0
            objSMLO.Arr(ii).Tax3_Assessable_Amt = 0
            objSMLO.Arr(ii).TAX3_Rate = 0

            objSMLO.Arr(ii).TAX4 = ""
            objSMLO.Arr(ii).TAX4_Amt = 0
            objSMLO.Arr(ii).Tax4_Assessable_Amt = 0
            objSMLO.Arr(ii).TAX4_Rate = 0

            objSMLO.Arr(ii).TAX5 = ""
            objSMLO.Arr(ii).TAX5_Amt = 0
            objSMLO.Arr(ii).Tax5_Assessable_Amt = 0
            objSMLO.Arr(ii).TAX5_Rate = 0

            objSMLO.Arr(ii).TAX6 = ""
            objSMLO.Arr(ii).TAX6_Amt = 0
            objSMLO.Arr(ii).Tax6_Assessable_Amt = 0
            objSMLO.Arr(ii).TAX6_Rate = 0

            objSMLO.Arr(ii).Total_Tax = 0

            objSMLO.Arr(ii).Amount = objSMLO.Arr(ii).Item_Qty * objSMLO.Arr(ii).MRP
            objSMLO.Arr(ii).Net_Amount = objSMLO.Arr(ii).Amount
            objSMLO.Arr(ii).Total_Item_Amt = objSMLO.Arr(ii).Amount
            objSMLO.Arr(ii).Total_Item_Cost = 0

            qry = "select top 1 TSPL_ITEM_PRICE_MASTER.Start_Date,TSPL_ITEM_PRICE_MASTER.Abatement,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,"
            qry += " case when TSPL_ITEM_MASTER.NoMRP=1 then  NetLTPT+(isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0) +isnull(TAX3_Amt,0) +isnull(TAX4_Amt,0) +isnull(TAX5_Amt,0) +isnull(TAX6_Amt,0) +isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) else NetLTPT end as BasicPrice_WithTax,"
            qry += " isnull((TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle+TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell),0) as Empty_Value,"
            qry += " (case when PC1.TPT_Type='Y' then ISNULL(Price_Amount1,0) else 0 end"
            qry += " +case when PC2.TPT_Type='Y' then ISNULL(Price_Amount2,0) else 0 end "
            qry += " + case when PC3.TPT_Type='Y' then ISNULL(Price_Amount3,0) else 0 end "
            qry += " + case when PC4.TPT_Type='Y' then ISNULL(Price_Amount4,0) else 0 end "
            qry += " + case when PC5.TPT_Type='Y' then ISNULL(Price_Amount5,0) else 0 end "
            qry += " + case when PC6.TPT_Type='Y' then ISNULL(Price_Amount6,0) else 0 end "
            qry += " + case when PC7.TPT_Type='Y' then ISNULL(Price_Amount7,0) else 0 end "
            qry += " + case when PC8.TPT_Type='Y' then ISNULL(Price_Amount8,0) else 0 end "
            qry += " + case when PC9.TPT_Type='Y' then ISNULL(Price_Amount9,0) else 0 end "
            qry += " + case when PC10.TPT_Type='Y' then ISNULL(Price_Amount10,0) else 0 end ) as TPT_Value"
            qry += " from TSPL_ITEM_PRICE_MASTER "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC1 on PC1.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp1"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC2 on PC2.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp2"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC3 on PC3.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp3"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC4 on PC4.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp4"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC5 on PC5.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp5"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC6 on PC6.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp6"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC7 on PC7.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp7"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC8 on PC8.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp8"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC9 on PC9.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp9"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC10 on PC10.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp10"
            qry += " where TSPL_ITEM_PRICE_MASTER.Item_Code='" + objSMLO.Arr(ii).Item_Code + "' and TSPL_ITEM_PRICE_MASTER.Price_Code='" + objSMLO.Price_Code + "' and TSPL_ITEM_PRICE_MASTER.UOM='" + objSMLO.Arr(ii).Uom + "' and TSPL_ITEM_PRICE_MASTER.Start_Date<='" + clsCommon.GetPrintDate(objSMLO.Transfer_Date, "dd/MMM/yyyy") + "' AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net='" + clsCommon.myCstr(objSMLO.Arr(ii).MRP) + "' order by TSPL_ITEM_PRICE_MASTER.Start_Date desc"
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Price Details not found for item " + objSMLO.Arr(ii).Item_Code + " and price code" + objSMLO.Price_Code + " and UOM " + objSMLO.Arr(ii).Uom + " start date on or before " + clsCommon.GetPrintDate(objSMLO.Transfer_Date, "dd/MMM/yyyy"))
            End If
            objSMLO.Arr(ii).Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Abatement"))

            objSMLO.Arr(ii).Basic_Price = clsCommon.myCdbl(dt.Rows(0)("Item_Basic_Price"))
            objSMLO.Arr(ii).Price_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))


            objSMLO.Arr(ii).BasicPrice_WithTax = clsCommon.myCdbl(dt.Rows(0)("BasicPrice_WithTax"))
            objSMLO.Arr(ii).Basic_Amt = objSMLO.Arr(ii).Item_Qty * objSMLO.Arr(ii).BasicPrice_WithTax
            objSMLO.Arr(ii).Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
            objSMLO.Arr(ii).TPT_Value = clsCommon.myCdbl(dt.Rows(0)("TPT_Value"))


            ''Calculate Header amount
            objSMLO.Item_Amount += objSMLO.Arr(ii).Amount
            objSMLO.Total_Item_Amount += objSMLO.Arr(ii).Amount
            objSMLO.Total_Basic_Amt += objSMLO.Arr(ii).Basic_Amt
            objSMLO.Total_Transfer_Amount += objSMLO.Arr(ii).Item_Qty * (objSMLO.Arr(ii).BasicPrice_WithTax + objSMLO.Arr(ii).Empty_Value)
            ''End of Calculate Header amount
        Next
        objSMLO.SaveData(objSMLO, True, trans)

        qry = "Update TSPL_TRANSFER_HEAD set Reference_Doc_No='" + objSMLO.Transfer_No + "' where Transfer_No in (" + strRefDocNoDB + ")"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return objSMLO
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class

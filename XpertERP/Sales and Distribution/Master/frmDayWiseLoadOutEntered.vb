Imports System
Imports System.Data.SqlClient
Imports common


'DEVELOPED by Abhishek kumar
'Created date - 5/june/2012
' End Date = 5/June/2012


Public Class FrmDayWiseLoadOutEntered
    Inherits FrmMainTranScreen
    Dim dt As New DataTable()
    Dim obj As New ClsDayWiseLoadOutEntered()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim QRY As String
    Dim userCode, companyCode As String
    Private isInsideLoadData As Boolean = False
    Public NoOfLoadOutMade As Double = 0
    Public Balance As Double = 0

    Private Sub FrmDayWiseLoadOutEntered_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmDayWiseLoadOutEntered_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddNew()
        Type()
        txtBalance.ReadOnly = True
        txtLoadOutMade.ReadOnly = True
        txtTotalCount.ReadOnly = True
        txttobePosted.ReadOnly = True

        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S Save the Data")
    End Sub
    Public Sub Type()
        DrpType.Items.Add("Load Out")
        DrpType.Items.Add("Load IN")
        DrpType.Items.Add("Settlement")
        DrpType.Items.Add("Empty Settlement")
        DrpType.Items.Add("Sale Invoice")
    End Sub
   
   
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDayWiseLoadOutEntered)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub blankAllControl()
        txtNoOfLoadOutMake.Value = 0
        txtBalance.Value = 0
        txtLoadOutMade.Value = 0
        txtTotalCount.Value = 0
        txttobePosted.Value = 0

    End Sub
    Sub AddNew()
        DrpType.Text = "Sale Invoice"
        blankAllControl()
        dtpLoadoutNo.Value = clsCommon.GETSERVERDATE()
        dtpLoadoutNo.Focus()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        DayPanel.Enabled = True
        FndLocation.Value = Nothing
        txtRemarks.Text = ""
        lblLocationname.Text = ""
        btnPrint.Enabled = False
    End Sub
    Function AllowToSave() As Boolean

        If (txtNoOfLoadOutMake.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter No Of Load Out ")
            txtNoOfLoadOutMake.Focus()
            Return False
        End If
        Return True
    End Function
    Sub SaveData()

        Try
            If (AllowToSave()) Then
                Dim obj As New ClsDayWiseLoadOutEntered()
                obj.LoadOutDate = clsCommon.myCDate(dtpLoadoutNo.Value)
                obj.NoOfLoadOutMake = clsCommon.myCdbl(txtNoOfLoadOutMake.Text)
                obj.location = clsCommon.myCstr(FndLocation.Value)
                obj.type = clsCommon.myCstr(DrpType.Text)
                obj.remarks = clsCommon.myCstr(txtRemarks.Text)
                NoOfLoadOutMade = txtLoadOutMade.Value
                txtLoadOutMade.Value = NoOfLoadOutMade
                Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(NoOfLoadOutMade)
                txtBalance.Value = Balance
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.LoadOutDate, obj.location, obj.type)
                    btnPrint.Enabled = True
                    btnSave.Enabled = False
                Else

                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal dtpLoadOutDate As String, ByVal location As String, ByVal type As String)
        Try
            btnSave.Enabled = True
            isInsideLoadData = True
            isNewEntry = True
            ' btnSave.Text = "Update"
            'blankAllControl()
            Dim obj As New ClsDayWiseLoadOutEntered()
            obj = ClsDayWiseLoadOutEntered.GetData(dtpLoadOutDate, location, type)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LoadOutDate) > 0) Then
                blankAllControl()
                dtpLoadoutNo.Value = clsCommon.myCDate(dtpLoadOutDate, "dd/MMM/yyyy")
                txtNoOfLoadOutMake.Value = obj.NoOfLoadOutMake
                FndLocation.Value = obj.location
                DrpType.Text = obj.type
                txtRemarks.Text = obj.remarks
                txtLoadOutMade.Value = obj.NoOfLoadOutmade
                txtBalance.Value = obj.Balance
                txtTotalCount.Value = obj.NoofTotalCount
                txttobePosted.Value = obj.NoOfToBePosted
                isNewEntry = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        AddNew()
    End Sub

    Private Sub dtpLoadoutNo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpLoadoutNo.ValueChanged

        'Dim LoadOutdate As String = dtpLoadoutNo.Value
        'Dim location As String = FndLocation.Value
        'Dim type As String = DrpType.SelectedValue
        'LoadData(LoadOutdate, location, type)
        'NoOfLoadOutMade = GetNoOfLoadOut(LoadOutdate)
        'txtLoadOutMade.Value = NoOfLoadOutMade
        'Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(NoOfLoadOutMade)
        'txtBalance.Value = Balance

    End Sub

    Private Sub txtNoOfLoadOutMake_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoOfLoadOutMake.Leave
        Dim qry As String = "select Count(*) from TSPL_DayWiseEnteredLoadOut where  LoadOutDate = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and location_code='" & FndLocation.Value & "' and type='" & DrpType.Text & "' "
        Dim Count As Integer = clsDBFuncationality.getSingleValue(qry)
        If Count = 0 Then
            isNewEntry = True
        Else
            isNewEntry = False
        End If
        If txtNoOfLoadOutMake.Value > 0 Then
            Dim LoadOutdate As String = dtpLoadoutNo.Value
            NoOfLoadOutMade = txtLoadOutMade.Value
            txtLoadOutMade.Value = NoOfLoadOutMade
            Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(txtTotalCount.Value)
            txtBalance.Value = Balance
            txttobePosted.Value = clsCommon.myCdbl(txtTotalCount.Value - txtLoadOutMade.Value)



        End If
    End Sub

    Private Sub FndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndLocation._MYValidating
        Dim qry As String = "select Location_Code as Code ,Location_Desc as [Description]  from TSPL_LOCATION_MASTER "
        FndLocation.Value = clsCommon.ShowSelectForm("LocatnNamFND", qry, "Code", "Location_Type ='Physical' and Excisable ='F'", FndLocation.Value, "Code", isButtonClicked)
        Dim qry1 As String = "select Location_Desc from TSPL_LOCATION_MASTER where  Location_Code ='" + FndLocation.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            lblLocationname.Text = dt.Rows(0)("Location_Desc")
        Else
            lblLocationname.Text = ""
        End If
    End Sub

    'Public Function GetNoOfLoadOut(ByVal LoadOutdate As String) As Double
    '    Dim qry As String = " select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where Transfer_Type ='LO' and  To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and Convert(Date,Transfer_Date,103) =Convert(date,'" & LoadOutdate & "',103)"
    '    Dim NoOfLoadOut As Integer = clsDBFuncationality.getSingleValue(qry)
    '    Return NoOfLoadOut
    'End Function
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        If allowRefresh() Then
            If DrpType.Text = "Load Out" Then
                Dim NoOfLoadOUt As Double
                Dim NoOfTotalCount As Double
                Dim qry As String = "select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where   To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and  Transfer_Date  = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .From_Location ='" & FndLocation.Value & "' and Transfer_Type ='LO' and Post ='Y'"
                NoOfLoadOUt = clsDBFuncationality.getSingleValue(qry)
                txtLoadOutMade.Value = NoOfLoadOUt
                Dim NoofTotalLoadout As String = "select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where   To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and  Transfer_Date  = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .From_Location ='" & FndLocation.Value & "' and Transfer_Type ='LO'"
                NoOfTotalCount = clsDBFuncationality.getSingleValue(NoofTotalLoadout)
                txtTotalCount.Value = NoOfTotalCount
                LoadData(dtpLoadoutNo.Value, FndLocation.Value, DrpType.Text)
                btnPrint.Enabled = True
                btnSave.Enabled = True
                Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(txtTotalCount.Value)
                txtBalance.Value = Balance
                txttobePosted.Value = clsCommon.myCdbl(txtTotalCount.Value - txtLoadOutMade.Value)
            ElseIf DrpType.Text = "Load IN" Then
                Dim NoOfLoadIn As Double
                Dim NoOftotalCount As Double
                Dim LoadInqry As String = "select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Physical')and  Transfer_Date  =  '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .To_Location ='" & FndLocation.Value & "' and Transfer_Type ='LI' and Post ='Y' "
                NoOfLoadIn = clsDBFuncationality.getSingleValue(LoadInqry)
                Dim LoadInTotoal As String = "select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Physical')and  Transfer_Date = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .To_Location ='" & FndLocation.Value & "' and Transfer_Type ='LI'  "
                NoOftotalCount = clsDBFuncationality.getSingleValue(LoadInTotoal)
                txtTotalCount.Value = NoOftotalCount
                LoadData(dtpLoadoutNo.Value, FndLocation.Value, DrpType.Text)
                btnPrint.Enabled = True
                btnSave.Enabled = True
                txtLoadOutMade.Value = NoOfLoadIn
                txtLoadOutMade.Value = txtLoadOutMade.Value
                Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(txtTotalCount.Value)
                txtBalance.Value = Balance
                txttobePosted.Value = clsCommon.myCdbl(txtTotalCount.Value - txtLoadOutMade.Value)
            ElseIf DrpType.Text = "Settlement" Then
                Dim NoOfsettlement As Double
                Dim NoOfTotalSetlement As Double
                Dim settlementqry As String = "select COUNT(Payment_No ) from TSPL_PAYMENT_HEADER where Convert(Date,Payment_Date,103) =Convert(date,'" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyy") & "',103) and Payment_Code ='SETTLEMENT'and Location_Code ='" & FndLocation.Value & "'and Posted ='P'"
                NoOfsettlement = clsDBFuncationality.getSingleValue(settlementqry)
                Dim settlementqryTotal As String = "select COUNT(Payment_No ) from TSPL_PAYMENT_HEADER where Convert(Date,Payment_Date,103) =Convert(date,'" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "',103) and Payment_Code ='SETTLEMENT'and Location_Code ='" & FndLocation.Value & "'and Posted ='P'"
                NoOfTotalSetlement = clsDBFuncationality.getSingleValue(settlementqryTotal)
                txtTotalCount.Value = NoOfTotalSetlement
                LoadData(dtpLoadoutNo.Value, FndLocation.Value, DrpType.Text)
                btnPrint.Enabled = True
                btnSave.Enabled = True
                txtLoadOutMade.Value = NoOfsettlement
                txtLoadOutMade.Value = txtLoadOutMade.Value
                Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(txtTotalCount.Value)
                txtBalance.Value = Balance
                txttobePosted.Value = clsCommon.myCstr(txtTotalCount.Value - txtLoadOutMade.Value)
            ElseIf DrpType.Text = "Empty Settlement" Then
                Dim NoOfemptySettlement As Double
                Dim NoOfEmptyTotal As Double
                Dim NoOfEmptysettlment As String = "select COUNT(Adjustment_No ) from TSPL_ADJUSTMENT_HEADER where Convert(Date,Adjustment_Date,103) =Convert(date,'" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "',103) and Loc_Code ='" & FndLocation.Value & "' and Reference_Document ='Load Out/Transfer' and ItemType ='E'and  Trans_Type ='In' and Posted ='Y'"
                NoOfemptySettlement = clsDBFuncationality.getSingleValue(NoOfEmptysettlment)
                Dim NoOfEmptysettlmentTotal As String = "select COUNT(Adjustment_No ) from TSPL_ADJUSTMENT_HEADER where Convert(Date,Adjustment_Date,103) =Convert(date,'" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "',103) and Loc_Code ='" & FndLocation.Value & "' and Reference_Document ='Load Out/Transfer' and ItemType ='E'and  Trans_Type ='In'"
                NoOfEmptyTotal = clsDBFuncationality.getSingleValue(NoOfEmptysettlmentTotal)
                txtTotalCount.Value = NoOfEmptyTotal
                LoadData(dtpLoadoutNo.Value, FndLocation.Value, DrpType.Text)
                btnPrint.Enabled = True
                btnSave.Enabled = True
                txtLoadOutMade.Value = NoOfemptySettlement
                txtLoadOutMade.Value = txtLoadOutMade.Value
                Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(txtLoadOutMade.Value)
                txtBalance.Value = Balance
                txttobePosted.Value = clsCommon.myCstr(txtTotalCount.Value - txtLoadOutMade.Value)
            ElseIf DrpType.Text = "Sale Invoice" Then
                Dim NoOfsaleInvoice As Double
                Dim NoOftotalcount As Double
                Dim SaleInvoice As String = "select Count(Sale_Invoice_No)   from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and Location ='" & FndLocation.Value & "'and  is_post='Y' "
                NoOfsaleInvoice = clsDBFuncationality.getSingleValue(SaleInvoice)
                Dim SaleInvoiceTotal As String = "select Count(Sale_Invoice_No)   from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and Location ='" & FndLocation.Value & "'  "
                NoOftotalcount = clsDBFuncationality.getSingleValue(SaleInvoiceTotal)
                txtTotalCount.Value = NoOftotalcount
                LoadData(dtpLoadoutNo.Value, FndLocation.Value, DrpType.Text)
                btnPrint.Enabled = True
                btnSave.Enabled = True
                txtLoadOutMade.Value = NoOfsaleInvoice
                txtLoadOutMade.Value = txtLoadOutMade.Value
                Balance = clsCommon.myCdbl(txtNoOfLoadOutMake.Value) - clsCommon.myCdbl(txtTotalCount.Value)
                txtBalance.Value = Balance
                txttobePosted.Value = clsCommon.myCstr(txtTotalCount.Value - txtLoadOutMade.Value)
            End If
            DayPanel.Enabled = False
        End If

    End Sub

    Public Function allowRefresh() As Boolean
        If clsCommon.myLen(FndLocation.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Location ")
            FndLocation.Focus()
            DayPanel.Enabled = True
            Return False
        End If
        If DrpType.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please Select Type ")
            DrpType.Focus()
            DayPanel.Enabled = True
            Return False
        End If
        Return True
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()
        If allowRefresh() Then
            If DrpType.Text = "Load Out" Then
                Dim qry As String = "select Convert(varchar(12),LoadOutdate,103)as LoadOutdate,NoOf_loadOuttoBemake as ToBeCount,(select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where   To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and  Transfer_Date  = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .From_Location ='" & FndLocation.Value & "' and Transfer_Type ='LO' and Post ='Y')as PostedCount,(select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where   To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and  Transfer_Date  = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD.From_Location ='" & FndLocation.Value & "' and Transfer_Type ='LO')as TotalOfCount,location_code as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_DayWiseEnteredLoadOut.Location_Code )as LocationName,TYPE,Remarks  from TSPL_DayWiseEnteredLoadOut where loadOutDate= '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "'  and Location_Code='" & FndLocation.Value & "'and TYPE = 'Load Out' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else

                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.UtilityReports, dt, "DayEntryStatus", "Day Entry Status")
                End If
            ElseIf DrpType.Text = "Load IN" Then
                Dim qry As String = "select Convert(varchar(12),LoadOutdate,103)as LoadOutdate,NoOf_loadOuttoBemake as ToBeCount,(select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Physical')and  Transfer_Date  = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .To_Location ='" & FndLocation.Value & "' and Transfer_Type ='LI' and Post ='Y')as PostedCount,(select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Physical')and  Transfer_Date = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "' and TSPL_TRANSFER_HEAD .To_Location ='" & FndLocation.Value & "' and Transfer_Type ='LI')as TotalOfCount,location_code as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_DayWiseEnteredLoadOut.Location_Code )as LocationName,TYPE,Remarks  from TSPL_DayWiseEnteredLoadOut where loadOutDate= '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "'  and Location_Code='" & FndLocation.Value & "' and TYPE = 'Load In' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else

                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.UtilityReports, dt, "DayEntryStatus", "Day Entry Status")
                End If
            ElseIf DrpType.Text = "Settlement" Then
                Dim qry As String = "select Convert(varchar(12),LoadOutdate,103)as LoadOutdate,NoOf_loadOuttoBemake as ToBeCount,(select COUNT(Payment_Code ) from TSPL_PAYMENT_HEADER where Payment_Code ='Settlement'and Payment_Date ='" & dtpLoadoutNo.Value & "' and Posted ='P'and Location_Code ='" & FndLocation.Value & "')as PostedCount,(select COUNT(Payment_No) from TSPL_PAYMENT_HEADER where Convert(date,Payment_Date,103) =Convert(date,'" & dtpLoadoutNo.Value & "',103) and Payment_Code ='SETTLEMENT'and Location_Code ='" & FndLocation.Value & "')as TotalOfCount,location_code as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_DayWiseEnteredLoadOut.Location_Code )as LocationName,TYPE,Remarks  from TSPL_DayWiseEnteredLoadOut where loadOutDate=Convert(Date,'" & dtpLoadoutNo.Value & "',103) and Location_Code='" & FndLocation.Value & "'and TYPE = 'SETTLEMENT' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.UtilityReports, dt, "DayEntryStatus", "Day Entry Status")
                End If
            ElseIf DrpType.Text = "Empty Settlement" Then
                Dim qry As String = "select Convert(varchar(12),LoadOutdate,103)as LoadOutdate,NoOf_loadOuttoBemake as ToBeCount,(select COUNT(Adjustment_No ) from TSPL_ADJUSTMENT_HEADER where Convert(Date,Adjustment_Date,103) =Convert(date,'" & dtpLoadoutNo.Value & "',103) and Loc_Code ='" & FndLocation.Value & "' and Reference_Document ='Load Out/Transfer' and ItemType ='E'and  Trans_Type ='In')as TotalOfCount,(select COUNT(Adjustment_No ) from TSPL_ADJUSTMENT_HEADER where Convert(Date,Adjustment_Date,103) =Convert(date,'" & dtpLoadoutNo.Value & "',103) and Loc_Code ='" & FndLocation.Value & "' and Reference_Document ='Load Out/Transfer' and ItemType ='E'and  Trans_Type ='In'and Posted ='Y')as PostedCount,location_code as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_DayWiseEnteredLoadOut.Location_Code )as LocationName,TYPE,Remarks  from TSPL_DayWiseEnteredLoadOut where loadOutDate=Convert(Date,'" & dtpLoadoutNo.Value & "',103) and Location_Code='" & FndLocation.Value & "'and TYPE = 'Empty Settlement'  "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else

                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.UtilityReports, dt, "DayEntryStatus", "Day Entry Status")
                End If
            ElseIf DrpType.Text = "Sale Invoice" Then
                Dim qry As String = " select Convert(varchar(12),LoadOutdate,103)as LoadOutdate,NoOf_loadOuttoBemake as ToBeCount,(select Count(Sale_Invoice_No)   from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "'  and Location ='" & FndLocation.Value & "'and  is_post='Y')as PostedCount,(select Count(Sale_Invoice_No)   from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date = '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "'  and Location ='" & FndLocation.Value & "' )as TotalOfCount,location_code as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_DayWiseEnteredLoadOut.Location_Code )as LocationName,TYPE,Remarks  from TSPL_DayWiseEnteredLoadOut where loadOutDate= '" & clsCommon.GetPrintDate(dtpLoadoutNo.Value, "dd/MMM/yyyy") & "'  and Location_Code='" & FndLocation.Value & "'and TYPE = 'Sale Invoice' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else

                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCrystalReportViewer.funreport(CrystalReportFolder.UtilityReports, dt, "DayEntryStatus", "Day Entry Status")
                End If
            End If
        End If
    End Sub



  
    
  
  

  


  
End Class

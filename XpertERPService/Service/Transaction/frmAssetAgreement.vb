'Created By---> Pankaj jha
'Created Date--->02/Apr/2014
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common
Imports XpertERPEngine


Public Class FrmAssetAgreement
    Inherits FrmMainTranScreen
    '    colOutletNo
    'colOutletName
    'colAssetID
    'colAssetDesc
    'colAgreementNo
    'colAgreementDate
    'colAgreementValidFrom
    'colAgreementValidTo
    'colReceivedStatus
    'colAgreementReceivedDate

    Public Const colOutletNo As String = "colOutletNo"
    Public Const colOutletName As String = "colOutletName"
    Public Const colAssetID As String = "colAssetID"
    Public Const colAssetDesc As String = "colAssetDesc"
    Public Const colAgreementNo As String = "colAgreementNo"
    Public Const colAgreementDate As String = "colAgreementDate"
    Public Const colAgreementValidFrom As String = "colAgreementValidFrom"
    Public Const colAgreementValidTo As String = "colAgreementValidTo"
    Public Const colReceivedStatus As String = "colReceivedStatus"
    Public Const colAgreementReceivedDate As String = "colAgreementReceivedDate"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAssetAgreement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGrid()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            Dim colOutletNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            colOutletNo.FormatString = ""
            colOutletNo.HeaderText = "Outlet No."
            colOutletNo.Name = "colOutletNo"
            colOutletNo.HeaderImage = My.Resources.search4
            colOutletNo.TextImageRelation = TextImageRelation.TextBeforeImage
            colOutletNo.Width = 100
            colOutletNo.ReadOnly = False
            colOutletNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colOutletNo)

            Dim colOutletName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            colOutletName.FormatString = ""
            colOutletName.HeaderText = "Outlet Name"
            colOutletName.Name = "colOutletName"
            colOutletName.Width = 250
            colOutletName.ReadOnly = True
            colOutletName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.MasterTemplate.Columns.Add(colOutletName)

            Dim colAssetID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            colAssetID.FormatString = ""
            colAssetID.HeaderText = "Asset ID"
            colAssetID.Name = "colAssetID"
            colAssetID.HeaderImage = My.Resources.search4
            colAssetID.TextImageRelation = TextImageRelation.TextBeforeImage
            colAssetID.Width = 100
            colAssetID.ReadOnly = False
            colAssetID.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colAssetID)
            Dim colAssetDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            colAssetDesc.FormatString = ""
            colAssetDesc.HeaderText = "Asset Description"
            colAssetDesc.Name = "colAssetDesc"
            colAssetDesc.Width = 250
            colAssetDesc.ReadOnly = True
            colAssetDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.MasterTemplate.Columns.Add(colAssetDesc)
            Dim colAgreementNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            colAgreementNo.FormatString = ""
            colAgreementNo.HeaderText = "Agreement No."
            colAgreementNo.Name = "colAgreementNo"
            colAgreementNo.Width = 100
            colAgreementNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colAgreementNo)

            Dim colAgreementDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            colAgreementDate.CustomFormat = "dd/MM/yyyy"
            colAgreementDate.FormatString = "{0:d}"
            colAgreementDate.HeaderText = "Agreement Date"
            colAgreementDate.Name = "colAgreementDate"
            colAgreementDate.Width = 150
            colAgreementDate.ReadOnly = False
            colAgreementDate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colAgreementDate)

            Dim colAgreementValidFrom As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            colAgreementValidFrom.CustomFormat = "dd/MM/yyyy"
            colAgreementValidFrom.FormatString = "{0:d}"
            colAgreementValidFrom.HeaderText = "Agreement Valid From"
            colAgreementValidFrom.Name = "colAgreementValidFrom"
            colAgreementValidFrom.Width = 180
            colAgreementValidFrom.ReadOnly = False
            colAgreementValidFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colAgreementValidFrom)

            Dim colAgreementValidTo As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            colAgreementValidTo.CustomFormat = "dd/MM/yyyy"
            colAgreementValidTo.FormatString = "{0:d}"
            colAgreementValidTo.HeaderText = "Agreement Valid Till"
            colAgreementValidTo.Name = "colAgreementValidTo"
            colAgreementValidTo.Width = 180
            colAgreementValidTo.ReadOnly = False
            colAgreementValidTo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colAgreementValidTo)

            Dim colReceivedStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            colReceivedStatus.FormatString = ""
            colReceivedStatus.DataSource = loadStatusValues()
            colReceivedStatus.DisplayMember = "Status"
            colReceivedStatus.ValueMember = "Status"
            colReceivedStatus.HeaderText = "Received Status(YES/NO)"
            colReceivedStatus.Name = "colReceivedStatus"
            colReceivedStatus.Width = 180
            colReceivedStatus.ReadOnly = False
            colReceivedStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colReceivedStatus)

            Dim colAgreementReceivedDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            colAgreementReceivedDate.CustomFormat = "dd/MM/yyyy"
            colAgreementReceivedDate.FormatString = "{0:d}"
            colAgreementReceivedDate.HeaderText = "Agreement Received Date"
            colAgreementReceivedDate.Name = "colAgreementReceivedDate"
            colAgreementReceivedDate.Width = 150
            colAgreementReceivedDate.ReadOnly = False
            colAgreementReceivedDate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(colAgreementReceivedDate)

            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = True
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function loadStatusValues() As DataTable
        Dim dt As New DataTable
        Try

            dt.Columns.Add("Status", GetType(String))
            Dim dr As DataRow = dt.NewRow()
            dr("Status") = "YES"
            dt.Rows.Add(dr)
            dr = dt.NewRow()
            dr("Status") = "NO"
            dt.Rows.Add(dr)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return dt
    End Function
    Private Sub FrmAssetAgreement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        blankAllControls()
        If clsCommon.myLen(Me.Tag) > 0 Then
            txtDocNo.Value = clsCommon.myCstr(Me.Tag)
            loadData(NavigatorType.Current)
        End If
    End Sub
    Sub OpenOutletList()
        Try
            Dim strOutletNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("colOutletNo").Value)
            If clsCommon.myLen(strOutletNo) > 0 Then
                Dim qry As String = "select Cust_code as 'Code' ,customer_name as 'Outlet Name ', add1 + ',' + add2 + ',' + add3 as 'Address', city_Code as [City],phone1 as [Phone No],Route_no + ',' + Route_Desc as 'Route ',Cust_Group_Code as 'Customer Group Code',Terms_Code as 'Terms Code',Cust_Account as 'Customer Account' from TSPL_CUSTOMER_MASTER "
                'Dim whrCls As String = "Cust_code='" + strOutletNo + "'"
                gv1.CurrentRow.Cells("colOutletNo").Value = clsCommon.ShowSelectForm("OUTLETfndnder", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells("colOutletNo").Value), "Code", False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub openAssetList()
        Try
            Dim strAssetID As String = clsCommon.myCstr(gv1.CurrentRow.Cells("colAssetID").Value)
            If clsCommon.myLen(strAssetID) > 0 Then
                Dim qry As String = "Select    TSPL_ITEM_MASTER.item_code as Code,TSPL_ITEM_MASTER.item_desc as 'Description', visimakecode.DESCRIPTION as [Asset Make], assettypecode.DESCRIPTION   as [Asset Type], visimodeNoCode.DESCRIPTION as [Model No], visisizeCode.DESCRIPTION as [Asset Size], TSPL_VISI_MASTER.Serial_No  as [SerialNo],TSPL_VISI_MASTER.Tag_No as [Tag No]  from TSPL_VISI_MASTER LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visimakecode on visimakecode.CODE =TSPL_VISI_MASTER.VisiMake             left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visiModeNoCode on visiModeNoCode .CODE =TSPL_VISI_MASTER.Model_No                  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visisizeCode on visisizeCode.CODE =TSPL_VISI_MASTER.Visi_Size     left outer join  TSPL_ITEM_CATEGORY_LEVEL_VALUES visiAssetTypeCode on visiAssetTypeCode.CODE =TSPL_VISI_MASTER.asset_type left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_VISI_MASTER.Asset_No  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES AssetTypeCode on AssetTypeCode.CODE  =TSPL_VISI_MASTER.asset_type "
                'Dim whrCls As String = "Cust_code='" + strOutletNo + "'"
                'gv1.CurrentRow.Cells("colAssetID").Value = clsCommon.ShowSelectForm("ASSETfndnder", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells("colAssetID").Value), "Code", False)
                gv1.CurrentRow.Cells("colAssetID").Value = clsCommon.ShowSelectForm("ASSETfndnder", qry, "SerialNo", "", clsCommon.myCstr(gv1.CurrentRow.Cells("colAssetID").Value), "Code", False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub loadData(Optional ByVal navtype As NavigatorType = NavigatorType.Current)
        Try
            'blankAllControls()
            Dim obj As New clsAssetAgreementHead
            Dim arr As New List(Of clsAssetAgreementDetails)
            obj = clsAssetAgreementHead.getData(clsCommon.myCstr(txtDocNo.Value), navtype)
            If (obj IsNot Nothing) Then
                arr = clsAssetAgreementDetails.getData(clsCommon.myCstr(obj.docNo))
                txtDocNo.Value = obj.docNo
                txtDate.Value = clsCommon.myCDate(obj.docDate)
                txtEmpNo.Value = clsCommon.myCstr(obj.empCode)
                lblEMPName.Text = clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" & txtEmpNo.Value & "'")
                txtEmpContactNo.Text = clsCommon.myCstr(obj.empContactNo)
                txtLocCode.Value = clsCommon.myCstr(obj.locationCode)
                lblLocDesc.Text = clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" & txtLocCode.Value & "'")
                txtCourierNo.Text = clsCommon.myCstr(obj.courierNo)
                txtCourierCompanyName.Text = clsCommon.myCstr(obj.courierComapnyName)
                dtpCourierDate.Value = clsCommon.myCDate(obj.courierDate)
                txtRemarks.Text = clsCommon.myCstr(obj.remarks)
                gv1.Rows.Clear()
                Dim row As clsAssetAgreementDetails = Nothing
                For Each obj1 As clsAssetAgreementDetails In arr
                    row = New clsAssetAgreementDetails()
                    row = obj1
                    '"select item_desc from tspl_item_master where item_code='" & ASTNO  & "'"
                    Dim ASTNO As String = clsDBFuncationality.getSingleValue("select asset_no from tspl_visi_master where visi_ID='" & row.AssetId & "'")
                    gv1.Rows.Add(clsCommon.myCstr(row.outletNo), clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" & row.outletNo & "'")), clsCommon.myCstr(row.AssetId), clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" & ASTNO & "'")), clsCommon.myCstr(row.agreementNo), clsCommon.myCDate(row.agreementDate), clsCommon.myCDate(row.AgreementFrom), clsCommon.myCDate(row.AgreementTo), clsCommon.myCstr(row.receivedYN), row.receivedDate)
                    gv1.CurrentRow.Cells("colAgreementNo").ReadOnly = True
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub blankAllControls()

        Try
            txtDocNo.Value = ""
            txtDate.Value = clsCommon.GetPrintDate(Today)
            txtEmpNo.Value = ""
            lblEMPName.Text = ""
            txtEmpContactNo.Text = ""
            txtLocCode.Value = ""
            lblLocDesc.Text = ""
            txtCourierNo.Text = ""
            txtCourierCompanyName.Text = ""
            dtpCourierDate.Value = clsCommon.GetPrintDate(Today)
            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnDelete.Enabled = False
            txtRemarks.Text = ""
            LoadBlankGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            blankAllControls()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.CompairString(txtDate.Value.ToString, "") = CompairStringResult.Equal Then
                Throw New Exception("Invalid Document Date")
            End If
            If clsCommon.CompairString(txtEmpNo.Value, "") = CompairStringResult.Equal Then
                Throw New Exception("Employee Code Can Not Be Left Blank")
            End If
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_employee_master where emp_code='" & txtEmpNo.Value & "'") = 0 Then
                Throw New Exception("Invalid Employee Code")
            End If
            If clsCommon.CompairString(txtLocCode.Value.ToString.Trim, "") = CompairStringResult.Equal Then
                Throw New Exception("Location Code Can Not be Left Blank")
            End If
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_Location_master where location_code='" & txtLocCode.Value & "'") = 0 Then
                Throw New Exception("Invalid Location Code")
            End If
            If clsCommon.CompairString(txtCourierNo.Text.Trim, "") = CompairStringResult.Equal Then
                Throw New Exception("Please Enter a valid  Courier No.")
            End If
            If clsCommon.CompairString(txtCourierCompanyName.Text.Trim, "") = CompairStringResult.Equal Then
                Throw New Exception("Please Enter a valid  Courier Company Name")
            End If
            For j As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(j).Cells(colOutletNo).Value) > 0 Then
                Else
                    Throw New Exception("Blank Outlet No at Row No. " & (j + 1))
                End If
                If clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where cust_code='" & clsCommon.myCstr(gv1.Rows(j).Cells(colOutletNo).Value) & "'") = 0 Then
                    Throw New Exception("Invalid Outlet No at Row No. " & (j + 1))
                End If
                If clsCommon.myLen(gv1.Rows(j).Cells(colAssetID).Value) > 0 Then
                Else
                    Throw New Exception("Blank Asset ID at Row No. " & (j + 1))
                End If
                If clsDBFuncationality.getSingleValue("select count(*) from tspl_visi_master where visi_id='" & clsCommon.myCstr(gv1.Rows(j).Cells(colAssetID).Value) & "'") = 0 Then
                    Throw New Exception("Invalid Asset ID at Row No. " & (j + 1))
                End If

                If clsCommon.myLen(gv1.Rows(j).Cells(colAgreementNo).Value) > 0 Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_asset_agreement_details where agreement_no='" & clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementNo).Value) & "'") > 0 Then
                            Throw New Exception("The Agreement No " & clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementNo).Value) & " Specified at Row No " & (j + 1) & "  is Alredy Exist for other Asset")
                        End If
                    End If
                    'For iii As Integer = 0 To gv1.Rows.Count - 1
                    '    If iii <> j And clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementNo).Value), clsCommon.myCstr(gv1.Rows(iii).Cells(colAgreementNo).Value)) = CompairStringResult.Equal Then
                    '        Throw New Exception("Duplicate Agreement No at Row No " & (j + 1) & " and At Row No " & (iii + 1))
                    '    End If
                    'Next
                Else
                    Throw New Exception("Blank Agreement No. At Row No. " & (j + 1))
                End If
                If clsCommon.myLen(gv1.Rows(j).Cells(colAgreementDate).Value) > 0 Then
                Else
                    Throw New Exception("Blank Agreement Date At Row No. " & (j + 1))
                End If
                If clsCommon.myLen(gv1.Rows(j).Cells(colReceivedStatus).Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(j).Cells(colReceivedStatus).Value), "YES") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementReceivedDate).Value), "") = CompairStringResult.Equal Then
                        Throw New Exception("Blank Agreement Received Date Against Received Status is YES At Row No. " & (j + 1))
                    End If
                Else

                End If

                'If clsCommon.myLen(gv1.Rows(j).Cells(colAgreementValidFrom).Value) <= 0 Then
                '    Throw New Exception("Please select Agreement Valid From Date at Row No " & (j + 1))
                'End If
                'If clsCommon.myLen(gv1.Rows(j).Cells(colAgreementValidTo).Value) <= 0 Then
                '    Throw New Exception("Please select Agreement Valid Till Date at Row No " & (j + 1))
                'End If
                If clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementValidTo).Value) <> "" And clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementValidFrom).Value) <> "" Then
                    If clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidTo).Value) < clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidFrom).Value) Then
                        Throw New Exception("The Valid Till Date Should be on or after valid from date at Row No " & (j + 1))
                    End If
                End If
            Next
            If gv1.Rows.Count < 1 Then
                Throw New Exception("Please Fill the Agreement Details Against this Document No in Below Grid !!! There Must be minimum One Agreement Details in Each Document")
            End If
            If chkDup() Then
                Throw New Exception("Duplicate Row Found, Please Check Out [There are two or more  Rows having Same outlet number and same asset ID twice]")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Function chkDup() As Boolean

        For i As Integer = 0 To gv1.Rows.Count - 1
            For j As Integer = 0 To gv1.Rows.Count - 1
                If i <> j And (clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colOutletNo).Value), clsCommon.myCstr(gv1.Rows(j).Cells(colOutletNo).Value)) = CompairStringResult.Equal) And (clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colAssetID).Value), clsCommon.myCstr(gv1.Rows(j).Cells(colAssetID).Value)) = CompairStringResult.Equal) Then
                    Return True
                End If
            Next

        Next
        Return False
    End Function
    Sub saveData()
        Dim trans As SqlTransaction
        trans = clsDBFuncationality.GetTransactin()
        Try
            Dim obj1 As clsAssetAgreementHead = Nothing
            Dim obj2 As clsAssetAgreementDetails = Nothing
            Dim arr As New List(Of clsAssetAgreementDetails)
            obj1 = New clsAssetAgreementHead()
            obj1.docNo = clsCommon.myCstr(txtDocNo.Value)
            obj1.docDate = clsCommon.myCDate(txtDate.Value)
            obj1.empCode = clsCommon.myCstr(txtEmpNo.Value)
            obj1.empContactNo = clsCommon.myCstr(txtEmpContactNo.Text)
            obj1.locationCode = clsCommon.myCstr(txtLocCode.Value)
            obj1.courierNo = clsCommon.myCstr(txtCourierNo.Text)
            obj1.courierComapnyName = clsCommon.myCstr(txtCourierCompanyName.Text)
            obj1.courierDate = clsCommon.myCDate(dtpCourierDate.Value)
            obj1.remarks = clsCommon.myCstr(txtRemarks.Text)
            For j As Integer = 0 To gv1.Rows.Count - 1
                obj2 = New clsAssetAgreementDetails()
                obj2.outletNo = clsCommon.myCstr(gv1.Rows(j).Cells(colOutletNo).Value)
                obj2.AssetId = clsCommon.myCstr(gv1.Rows(j).Cells(colAssetID).Value)
                obj2.agreementNo = clsCommon.myCstr(gv1.Rows(j).Cells(colAgreementNo).Value)
                obj2.docNo = clsCommon.myCstr(txtDocNo.Value)
                obj2.agreementDate = clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementDate).Value)
                obj2.AgreementFrom = IIf(IsNothing(gv1.Rows(j).Cells(colAgreementValidFrom).Value), clsCommon.myCDate(CDate("01-Jan-0001")), clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidFrom).Value)) 'clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidFrom).Value)
                obj2.AgreementTo = IIf(IsNothing(gv1.Rows(j).Cells(colAgreementValidTo).Value), clsCommon.myCDate(CDate("01-Dec-9999")), clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidTo).Value)) 'clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidTo).Value)
                obj2.receivedYN = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(j).Cells(colReceivedStatus).Value), "") = CompairStringResult.Equal, "NO", clsCommon.myCstr(gv1.Rows(j).Cells(colReceivedStatus).Value))
                If obj2.receivedYN = "YES" Then
                    obj2.receivedDate = clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementReceivedDate).Value)
                End If
                '  obj2.AgreementFrom = clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidFrom).Value)
                ' obj2.AgreementTo = clsCommon.myCDate(gv1.Rows(j).Cells(colAgreementValidTo).Value)
                arr.Add(obj2)
            Next
            clsAssetAgreementHead.saveData(obj1, IIf(clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal, True, False), trans)
            clsAssetAgreementDetails.saveData(clsCommon.myCstr(obj1.docNo), arr, IIf(clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal, True, False), trans)
            btnSave.Text = "Update"
            txtDocNo.Value = obj1.docNo
            trans.Commit()
            clsCommon.MyMessageBoxShow("Data Saved Successfully")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
        txtDocNo.Focus()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then saveData()
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        '        select TSPL_RGP_HEAD.Vendor_Code ,TSPL_RGP_DETAIL.Agreement_no from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.serial_no='000092' and TSPL_RGP_HEAD.Vendor_Code='10002'

        If e.Column Is gv1.Columns("colOutletNo") Then
            OpenOutletList()
            If gv1.CurrentRow.Cells("colOutletNo").Value IsNot Nothing And gv1.CurrentRow.Cells("colOutletNo").Value <> "" Then
                gv1.CurrentRow.Cells("colOutletName").Value = clsDBFuncationality.getSingleValue("select customer_Name from tspl_customer_master where cust_code='" & gv1.CurrentRow.Cells("colOutletNo").Value & "'")
                If clsCommon.myLen(gv1.CurrentRow.Cells("colOutletNo").Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells("colAssetID").Value) Then
                    Dim ono As String = clsCommon.myCstr(gv1.CurrentRow.Cells("colOutletNo").Value)
                    Dim astid As String = clsCommon.myCstr(gv1.CurrentRow.Cells("colAssetID").Value)
                    Dim agrno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_RGP_DETAIL.Agreement_no from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.serial_no='" & astid & "' and TSPL_RGP_HEAD.Vendor_Code='" & ono & "'"))
                    If clsCommon.myLen(agrno) > 0 Then
                        gv1.CurrentRow.Cells("colAgreementNo").Value = agrno
                        gv1.CurrentRow.Cells("colAgreementNo").ReadOnly = True


                    Else
                        gv1.CurrentRow.Cells("colAgreementNo").Value = ""
                        gv1.CurrentRow.Cells("colAgreementNo").ReadOnly = False
                    End If

                End If
            Else
                gv1.CurrentRow.Cells("colOutletName").Value = ""
            End If
        End If
        If e.Column Is gv1.Columns("colAssetID") Then
            openAssetList()
            If gv1.CurrentRow.Cells("colAssetID").Value IsNot Nothing And gv1.CurrentRow.Cells("colAssetID").Value <> "" Then
                Dim strastno As String = clsDBFuncationality.getSingleValue("select asset_no from tspl_visi_master where visi_id='" & gv1.CurrentRow.Cells("colAssetID").Value & "'")
                gv1.CurrentRow.Cells(3).Value = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" & strastno & "'")
                If clsCommon.myLen(gv1.CurrentRow.Cells("colOutletNo").Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells("colAssetID").Value) Then
                    Dim ono As String = clsCommon.myCstr(gv1.CurrentRow.Cells("colOutletNo").Value)
                    Dim astid As String = clsCommon.myCstr(gv1.CurrentRow.Cells("colAssetID").Value)
                    Dim agrno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_RGP_DETAIL.Agreement_no from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.serial_no='" & astid & "' and TSPL_RGP_HEAD.Vendor_Code='" & ono & "'"))
                    If clsCommon.myLen(agrno) > 0 Then
                        gv1.CurrentRow.Cells("colAgreementNo").Value = agrno
                        gv1.CurrentRow.Cells("colAgreementNo").ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells("colAgreementNo").Value = ""
                        gv1.CurrentRow.Cells("colAgreementNo").ReadOnly = False
                    End If

                End If
            Else
                gv1.CurrentRow.Cells(3).Value = ""
            End If
        End If
    End Sub

    Private Sub txtEmpNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpNo._MYValidating
        Try
            If isButtonClicked Then
                Dim qry As String = "select Emp_Code as 'Code' ,emp_name as 'Name ', add1 + ',' + add2  as 'Address', designation as  'Designation' from TSPL_EMPLOYEE_MASTER "
                txtEmpNo.Value = clsCommon.ShowSelectForm("EMPfndnder", qry, "Code", "", clsCommon.myCstr(txtEmpNo.Value), "Code", isButtonClicked)
                lblEMPName.Text = clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" & txtEmpNo.Value & "'")
            Else
                lblEMPName.Text = clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" & txtEmpNo.Value & "'")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocCode._MYValidating
        Try
            If isButtonClicked Then
                Dim qry As String = "select Location_Code as 'Code' ,Location_desc as 'Description ', add1 + ',' + add2 + ',' + add3 as 'Address', state  as  'State',Pin_code  as 'Pin Code',country as 'Country',telphone as 'Telephone',Email,location_type as 'Location Type',loc_status as 'Location Status' from TSPL_LOCATION_MASTER "
                txtLocCode.Value = clsCommon.ShowSelectForm("LOCfndnder", qry, "Code", "", clsCommon.myCstr(txtLocCode.Value), "Code", isButtonClicked)
                lblLocDesc.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_Location_master where Location_code='" & txtLocCode.Value & "'")
            Else
                lblLocDesc.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_Location_master where Location_code='" & txtLocCode.Value & "'")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            loadData(NavType)
            If clsCommon.CompairString(clsCommon.myCstr(txtDocNo.Value), "") = CompairStringResult.Equal Then
                txtDocNo.MyReadOnly = False
                btnDelete.Enabled = False
                btnSave.Text = "Save"

            Else
                txtDocNo.MyReadOnly = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_asset_Agreement_head where document_no ='" + txtDocNo.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtDocNo.MyReadOnly = False
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            Else
                txtDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnSave.Text = "Update"
            End If
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim qry As String = "select distinct TSPL_Asset_agreement_Head.document_no as 'Document No',document_date as 'Document Date',employee_Code as 'Employee Code',loc_code as " _
                & " 'Location Code',courier_no as 'Courier No', courier_comp_name as 'Courier Company Name' ,Courier_date as 'Courier Date' " _
                & " ,OUTLET_NO as [Outlet No],Customer_Name as [Outlet Name], coalesce(Add1,'') +  coalesce(',' + add2,'') +  coalesce(',' + Add3,'') +  coalesce('-' + pin_code,'') as [Address],City_Code as [City], Phone1 as [Phone No] " _
                & " ,case when RECEIVED_YN='Y' then 'Yes' else 'No' end as [Received Status] from   TSPL_Asset_agreement_Head inner join TSPL_ASSET_AGREEMENT_DETAILS on TSPL_Asset_agreement_Head.DOCUMENT_NO=TSPL_ASSET_AGREEMENT_DETAILS.DOCUMENT_NO inner join TSPL_CUSTOMER_MASTER cm on TSPL_ASSET_AGREEMENT_DETAILS.OUTLET_NO=cm.Cust_Code"
                txtDocNo.Value = clsCommon.ShowSelectForm("ASTAGRFND", qry, "Document No", "", txtDocNo.Value, "", isButtonClicked)
                loadData(NavigatorType.Current)
            End If
            If clsCommon.CompairString(clsCommon.myCstr(txtDocNo.Value), "") = CompairStringResult.Equal Then
                txtDocNo.MyReadOnly = False
                btnDelete.Enabled = False
                btnSave.Text = "Save"

            Else
                txtDocNo.MyReadOnly = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        deleteData()

    End Sub
    Sub deleteData()
        Dim trans As SqlTransaction
        trans = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Document found to Delete", Me.Name)
            ElseIf (common.clsCommon.MyMessageBoxShow("Delete the Document No.  " + txtDocNo.Value + Environment.NewLine + "Are you sure?", Me.Name, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                For i As Integer = 0 To gv1.Rows.Count - 1
                    Dim strcode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colAssetID).Value)
                    If clsCommon.myLen(strcode) > 0 Then
                        strcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_visi_master where visi_id='" & strcode & "'", trans))
                        If clsCommon.myLen(strcode) > 0 Then
                            Throw New Exception("Can Not Delete This Asset Agreement Document.  The Asset at Row No " & (i + 1) & "  is currently installed at Outelet " & strcode & "'")
                        End If
                    End If
                Next
                clsAssetAgreementDetails.deleteData(txtDocNo.Value, trans)
                clsAssetAgreementHead.deleteData(txtDocNo.Value, trans)
                blankAllControls()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Deleted Successfully..")
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rMenueExport.Click

        Try
            Dim str As String
            str = "select a.document_no as 'Document No',a.document_date as 'Document Date',a.employee_code as 'Employee Code',c.Emp_Name as 'Employee Name',a.loc_code as 'Location Code',d.Location_Desc as 'Location Description',a.courier_no as 'Courier No',a.courier_comp_name as 'Courier Company Name',a.courier_date as 'Courier Date',b.OUTLET_NO as 'Outlet No',e.Customer_Name as 'Outlet Name',b.Asset_id as 'Asset ID',b.AGREEMENT_NO as 'Agreement No',b.AGREEMENT_DATE as 'Agreement Date',b.AGREEMENT_FROM_DATE as 'Agreement Valid From',b.AGREEMENT_TO_DATE as 'Agreement Valid Till',b.RECEIVED_YN as 'Received Status',b.RECEIVED_DATE as 'Received Date' from TSPL_ASSET_AGREEMENT_HEAD as a left outer join TSPL_ASSET_AGREEMENT_DETAILS as b on a.document_no=b.DOCUMENT_NO left outer join TSPL_EMPLOYEE_MASTER as c on c.EMP_CODE =a.employee_code left outer join TSPL_LOCATION_MASTER as d on d.Location_Code =a.loc_code left outer join TSPL_CUSTOMER_MASTER as e on e.Cust_Code =b.OUTLET_NO "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rMenueImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rMenueImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        'Document No	Document Date	Employee Code	Employee Name	Location Code	Location Description	Courier No	Courier Company Name	Courier Date	Outlet No	Outlet Name	Agreement No	Agreement Date	Received Status	Received Date
        Dim dtt As New DataTable
        dtt.Columns.Add("Doc", GetType(String))
        Dim dr As DataRow
        Dim obj1 As clsAssetAgreementHead = Nothing
        Dim obj2 As clsAssetAgreementDetails = Nothing
        Dim arr As List(Of clsAssetAgreementDetails) = Nothing
        Dim strDocNo As String = Nothing
        If transportSql.importExcel(gv, "Document No", "Document Date", "Employee Code", "Location Code", "Courier No", "Courier Company Name", "Courier Date", "Outlet No", "Asset ID", "Agreement No", "Agreement Date", "Agreement Valid From", "Agreement Valid Till", "Received Status", "Received Date") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim lineno As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    lineno = lineno + 1
                    obj1 = New clsAssetAgreementHead()
                    obj2 = New clsAssetAgreementDetails()
                    arr = New List(Of clsAssetAgreementDetails)
                    strDocNo = clsCommon.myCstr(grow.Cells("Document No").Value)
                    If (String.IsNullOrEmpty(strDocNo)) Then
                        Throw New Exception("Document No  can not be blank at Row No " & lineno)
                    End If
                    If clsCommon.myLen(strDocNo) > 12 Then
                        Throw New Exception("Length of Document No Can Not Be More Then 12  at Row No " & lineno)
                    End If
                    obj1.docNo = strDocNo

                    Dim strDocDate As String = clsCommon.myCstr(grow.Cells("Document Date").Value)
                    If (String.IsNullOrEmpty(strDocDate)) Then
                        Throw New Exception("Document Date  can not be blank at Row No " & lineno)
                    End If
                    obj1.docDate = clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy")


                    Dim strEMPCode As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    If (String.IsNullOrEmpty(strEMPCode)) Then
                        Throw New Exception("Employee Code  can not be blank at Row No " & lineno)
                    End If
                    If clsCommon.myLen(strEMPCode) > 12 Then
                        Throw New Exception("Length of Employee Code Can Not Be More Then 12  at Row No " & lineno)
                    End If
                    obj1.empCode = strEMPCode

                    Dim strLOCCode As String = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If (String.IsNullOrEmpty(strLOCCode)) Then
                        Throw New Exception("Location Code  can not be blank at Row No " & lineno)
                    End If
                    If clsCommon.myLen(strLOCCode) > 12 Then
                        Throw New Exception("Length of Location Code Can Not Be More Then 12  at Row No " & lineno)
                    End If
                    obj1.locationCode = strLOCCode

                    Dim strCourierNo As String = clsCommon.myCstr(grow.Cells("Courier No").Value)
                    If (String.IsNullOrEmpty(strCourierNo)) Then
                        Throw New Exception("Courier No  can not be blank at Row No " + lineno)
                    End If
                    If clsCommon.myLen(strCourierNo) > 30 Then
                        Throw New Exception("Length of Courier No Can Not Be More Then 30  at Row No " & lineno)
                    End If
                    obj1.courierNo = strCourierNo

                    Dim strCourierCompName As String = clsCommon.myCstr(grow.Cells("Courier Company Name").Value)
                    If (String.IsNullOrEmpty(strCourierCompName)) Then
                        Throw New Exception("Courier Company Name  can not be blank at Row No " & lineno)
                    End If
                    If clsCommon.myLen(strCourierCompName) > 50 Then
                        Throw New Exception("Length of Courier Company Name  Can Not Be More Then 50  at Row No " & lineno)
                    End If
                    obj1.courierComapnyName = strCourierCompName

                    Dim strCourierDate As String = clsCommon.myCstr(grow.Cells("Courier Date").Value)
                    If (String.IsNullOrEmpty(strCourierDate)) Then
                        Throw New Exception("Courier Date  can not be blank at Row No " & lineno)
                    End If
                    obj1.courierDate = clsCommon.GetPrintDate(clsCommon.myCDate(strCourierDate), "dd/MMM/yyyy")


                    Dim strOutletNo As String = clsCommon.myCstr(grow.Cells("Outlet No").Value)
                    If (String.IsNullOrEmpty(strOutletNo)) Then
                        Throw New Exception("Outlet No  can not be blank at Row No " & lineno)
                    End If

                    If clsCommon.myLen(strOutletNo) > 12 Then
                        Throw New Exception("Length of Outlet No  Can Not Be More Then 12  at Row No " & lineno)
                    End If
                    If (clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where cust_code='" & strOutletNo & "'", trans)) = 0 Then

                        Throw New Exception("Invalid Outlet No at row no :  " & lineno)
                    End If
                    obj2.outletNo = strOutletNo

                    Dim strAssetID As String = clsCommon.myCstr(grow.Cells("Asset ID").Value)
                    If (String.IsNullOrEmpty(strAssetID)) Then
                        Throw New Exception("Asset ID  can not be blank at Row No " & lineno)
                    End If

                    If clsCommon.myLen(strAssetID) > 12 Then
                        Throw New Exception("Length of Asset ID  Can Not Be More Then 12  at Row No " & lineno)
                    End If

                    If (clsDBFuncationality.getSingleValue("select count(*) from tspl_visi_master where visi_id='" & strAssetID & "'", trans)) = 0 Then

                        Throw New Exception("Invalid Asset ID at row no :  " & lineno)
                    End If

                    obj2.AssetId = strAssetID



                    Dim strAgreementNo As String = clsCommon.myCstr(grow.Cells("Agreement No").Value)
                    If (String.IsNullOrEmpty(strAgreementNo)) Then
                        Throw New Exception("Agreement No  can not be blank at Row No " & lineno)
                    End If

                    If clsCommon.myLen(strAgreementNo) > 30 Then
                        Throw New Exception("Length of Agreement No  Can Not Be More Then 30  at Row No " & lineno)
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_Asset_Agreement_Details where agreement_No='" & strAgreementNo & "'", trans) > 0 Then
                        Throw New Exception("Duplicate Agreement No   at Row No " & lineno & "  This Agreement No already Exist in Table for Some Other Asset and Outlet")
                    End If
                    obj2.agreementNo = strAgreementNo
                    Dim strAgreementDate As String = clsCommon.myCstr(grow.Cells("Agreement Date").Value)
                    If (String.IsNullOrEmpty(strAgreementDate)) Then
                        Throw New Exception("Agreement Date  can not be blank at Row No " & lineno)
                    End If
                    obj2.agreementDate = clsCommon.GetPrintDate(clsCommon.myCDate(strAgreementDate), "dd/MMM/yyyy")
                    Dim strStatus As String = clsCommon.myCstr(grow.Cells("Received Status").Value)
                    If clsCommon.myLen(strStatus) > 0 Then
                        obj2.receivedYN = strStatus
                    End If

                    Dim stragreementfrom As String = clsCommon.myCstr(grow.Cells("Agreement Valid From").Value)
                    If (String.IsNullOrEmpty(stragreementfrom)) Then
                        Throw New Exception("Agreement Valid From Date  can not be blank at Row No " & lineno)
                    End If
                    obj2.AgreementFrom = clsCommon.GetPrintDate(clsCommon.myCDate(stragreementfrom), "dd/MMM/yyyy")
                    Dim stragreementto As String = clsCommon.myCstr(grow.Cells("Agreement Valid Till").Value)
                    If (String.IsNullOrEmpty(stragreementto)) Then
                        Throw New Exception("Agreement Valid Till Date  can not be blank at Row No " & lineno)
                    End If
                    obj2.AgreementTo = clsCommon.GetPrintDate(clsCommon.myCDate(stragreementto), "dd/MMM/yyyy")

                    If clsCommon.CompairString(strStatus, "Y") = CompairStringResult.Equal And clsCommon.myLen(clsCommon.myCstr(grow.Cells("Received Date").Value)) <= 0 Then
                        Throw New Exception("Received Date can not be blank when received status is Y at Row No " & lineno)
                    Else
                        Dim strRcvdDate As String = clsCommon.myCstr(grow.Cells("Received Date").Value)
                        obj2.receivedDate = strRcvdDate
                    End If

                    arr.Add(obj2)

                    If Not findDupDoc(dtt, strDocNo) Then
                        dr = dtt.NewRow()
                        dr("Doc") = obj1.docNo
                        dtt.Rows.Add(dr)
                        clsAssetAgreementHead.saveData(obj1, clsAssetAgreementHead.CheckNewEntry(obj1.docNo, trans), trans)
                        clsAssetAgreementDetails.deleteData(obj1.docNo, trans)
                        clsAssetAgreementDetails.saveData(obj1.docNo, arr, True, trans)
                    Else
                        clsAssetAgreementDetails.saveData(obj1.docNo, arr, True, trans)
                    End If


                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                loadData(NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    Function findDupDoc(ByVal dtble As DataTable, ByVal strdocno As String) As Boolean
        Try
            For i As Integer = 0 To dtble.Rows.Count - 1
                If clsCommon.CompairString(dtble.Rows(i)(0), strdocno) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
            'Return False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If (common.clsCommon.MyMessageBoxShow("Delete the Row !!! Are you sure?", Me.Name, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.No) Then
            e.Cancel = True
        Else
            Dim strcode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetID).Value)
            If clsCommon.myLen(strcode) <= 0 Then
                e.Cancel = False
            Else
                strcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_visi_master where visi_id='" & strcode & "'"))
                If clsCommon.myLen(strcode) <= 0 Then
                    e.Cancel = False
                Else
                    clsCommon.MyMessageBoxShow("Can Not Delete this asset agreement. It is currently installed at Outelet " & strcode & "'")
                    e.Cancel = True
                End If
            End If

        End If
    End Sub
End Class

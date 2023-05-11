'-------Created by Monika 18/06/2014-------------
Imports common
Imports Telerik.WinControls.UI
Imports System

Public Class ucVendorItemDetail
#Region "Variables"
    Private _TransNo As String = Nothing
    Private _TransDate As DateTime = Nothing
    Private _VendorCode As String = Nothing
    Private _ItemCode As String = Nothing
    Private _ItemName As String = Nothing
    Private _UOM As String = Nothing
    Private _FormId As String = Nothing

    Dim colTransno As String = "TransId"
    Dim colDate As String = "Date"
    Dim colRate As String = "Rate"
    Dim coluom As String = "UOM"
    Dim colvendorcode As String = "VendorCode"
    Dim colvendorname As String = "VendorName"

#End Region

#Region "Load Event"
    Private Sub ucVendorItemDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LoadBlankGrid()
    End Sub

#End Region

#Region "Public Properties"
    Public Property FormID() As String
        Get
            Return _FormId
        End Get
        Set(ByVal value As String)
            _FormId = value
        End Set
    End Property

    Public Property TransNo() As String
        Get
            Return _TransNo
        End Get
        Set(ByVal value As String)
            _TransNo = value
        End Set
    End Property

    Public Property TransDate() As DateTime
        Get
            Return _TransDate
        End Get
        Set(ByVal value As DateTime)
            _TransDate = value
        End Set
    End Property

    Public Property VendorCode() As String
        Get
            Return _VendorCode
        End Get
        Set(ByVal value As String)
            _VendorCode = value
        End Set
    End Property

    Public Property ItemCode() As String
        Get
            Return _ItemCode
        End Get
        Set(ByVal value As String)
            _ItemCode = value
        End Set
    End Property

    Public Property ItemName() As String
        Get
            Return _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
        End Set
    End Property

    Public Property Uom() As String
        Get
            Return _UOM
        End Get
        Set(ByVal value As String)
            _UOM = value
        End Set
    End Property
#End Region

    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim gvwidth As Integer = gv1.Width

        Dim repono As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repono.FormatString = ""
        repono.HeaderText = "Trans No."
        repono.Name = colTransno
        repono.Width = CInt(gvwidth / 4.5)
        repono.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repono)

        Dim repodate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodate.FormatString = ""
        repodate.HeaderText = "Trans Date"
        repodate.Name = colDate
        repodate.Width = CInt(gvwidth / 5.7)
        repodate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repodate)

        Dim repovndr As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovndr.FormatString = ""
        repovndr.HeaderText = "Vendor Code"
        repovndr.Name = colvendorcode
        repovndr.Width = CInt(gvwidth / 4.5)
        repovndr.ReadOnly = True
        repovndr.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repovndr)

        Dim repovndrname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovndrname.FormatString = ""
        repovndrname.HeaderText = "Vendor Name"
        repovndrname.Name = colvendorname
        repovndrname.Width = CInt(gvwidth / 3)
        repovndrname.ReadOnly = True
        repovndrname.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repovndrname)

        Dim reporate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporate.FormatString = ""
        reporate.HeaderText = "Unit Cost"
        reporate.Name = colRate
        reporate.Width = CInt(gvwidth / 7.6)
        reporate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reporate)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.HeaderText = "UOM"
        repouom.Name = coluom
        repouom.Width = CInt(gvwidth / 7.6)
        repouom.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repouom)

        If clsCommon.CompairString(btnothers.Text, "Same Vendor") <> CompairStringResult.Equal Then
            repovndrname.IsVisible = True
            repovndr.IsVisible = True
        End If

        gv1.TableElement.TableHeaderHeight = 13

    End Sub
    Public Sub RefreshData()
        Try
            LoadBlankGrid()
            btnothers.Text = "Other Vendors"
            If clsCommon.myLen(_ItemCode) > 0 AndAlso clsCommon.myLen(_VendorCode) > 0 Then
                RadGroupBox1.Text = _ItemCode + " (" + _ItemName + ")"
                Dim qry As String = ""
                If clsCommon.CompairString(_FormId, "PO") = CompairStringResult.Equal Then
                    qry = "select top 2 TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_UNIT_MASTER on TSPL_PURCHASE_ORDER_DETAIL.Unit_code=TSPL_UNIT_MASTER.Unit_Code where convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(_TransDate, "dd/MMM/yyyy") + "',103) and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" + clsCommon.myCstr(_VendorCode) + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(_ItemCode) + "' and TSPL_PURCHASE_ORDER_HEAD.purchaseorder_no<>'" + clsCommon.myCstr(_TransNo) + "' order by TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date desc,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No desc"
                ElseIf clsCommon.CompairString(_FormId, "SRN") = CompairStringResult.Equal Then
                    qry = "select top 2 TSPL_SRN_DETAIL.SRN_No as PurchaseOrder_No,TSPL_SRN_HEAD.SRN_Date as PurchaseOrder_Date,TSPL_SRN_DETAIL.Item_Cost,TSPL_UNIT_MASTER.Unit_Desc from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No left outer join TSPL_UNIT_MASTER on TSPL_SRN_DETAIL.Unit_code=TSPL_UNIT_MASTER.Unit_Code where convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(_TransDate, "dd/MMM/yyyy") + "',103) and TSPL_SRN_HEAD.Vendor_Code='" + clsCommon.myCstr(_VendorCode) + "' and TSPL_SRN_DETAIL.Item_Code='" + clsCommon.myCstr(_ItemCode) + "' and TSPL_SRN_HEAD.SRN_No<>'" + clsCommon.myCstr(_TransNo) + "' order by TSPL_SRN_HEAD.SRN_Date desc,TSPL_SRN_DETAIL.SRN_No desc"
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each dr As DataRow In dt.Rows
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTransno).Value = clsCommon.myCstr(dr("PurchaseOrder_No")) 'no.
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(dr("PurchaseOrder_Date")) 'date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCstr(dr("Item_Cost")) 'cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coluom).Value = clsCommon.myCstr(dr("Unit_Desc")) 'uom
                    Next
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub OtherVendorData()
        Try
            LoadBlankGrid()
            If clsCommon.myLen(_ItemCode) > 0 AndAlso clsCommon.myLen(_VendorCode) > 0 Then
                RadGroupBox1.Text = _ItemCode + " (" + _ItemName + ")"
                Dim qry As String = ""

                If clsCommon.CompairString(_FormId, "PO") = CompairStringResult.Equal Then
                    qry = "select a.* from (select  top 100 percent ROW_NUMBER() over(partition by TSPL_PURCHASE_ORDER_HEAD.Vendor_Code order by TSPL_PURCHASE_ORDER_HEAD.Vendor_Code) as sno,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_UNIT_MASTER on TSPL_PURCHASE_ORDER_DETAIL.Unit_code=TSPL_UNIT_MASTER.Unit_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.Vendor_Code where convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(_TransDate, "dd/MMM/yyyy") + "',103) and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(_ItemCode) + "' and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code<>'" + clsCommon.myCstr(_VendorCode) + "' order by TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date desc,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No desc)a where a.sno<3 order by a.PurchaseOrder_Date desc,a.PurchaseOrder_No desc"
                ElseIf clsCommon.CompairString(_FormId, "SRN") = CompairStringResult.Equal Then
                    qry = "select a.* from (select  top 100 percent ROW_NUMBER() over(partition by TSPL_SRN_HEAD.Vendor_Code order by TSPL_SRN_HEAD.Vendor_Code) as sno, TSPL_SRN_DETAIL.SRN_No as PurchaseOrder_No,TSPL_SRN_HEAD.SRN_Date as PurchaseOrder_Date,TSPL_SRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_SRN_DETAIL.Item_Cost,TSPL_UNIT_MASTER.Unit_Desc from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No left outer join TSPL_UNIT_MASTER on TSPL_SRN_DETAIL.Unit_code=TSPL_UNIT_MASTER.Unit_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SRN_HEAD.Vendor_Code where convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(_TransDate, "dd/MMM/yyyy") + "',103) and TSPL_SRN_DETAIL.Item_Code='" + clsCommon.myCstr(_ItemCode) + "' and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code<>'" + clsCommon.myCstr(_VendorCode) + "' order by TSPL_SRN_HEAD.SRN_Date desc,TSPL_SRN_DETAIL.SRN_No desc)a where a.sno<3 order by a.PurchaseOrder_Date desc,a.PurchaseOrder_No desc"
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each dr As DataRow In dt.Rows
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTransno).Value = clsCommon.myCstr(dr("PurchaseOrder_No")) 'no.
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(dr("PurchaseOrder_Date")) 'date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorcode).Value = clsCommon.myCstr(dr("vendor_code")) 'vendorcode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorname).Value = clsCommon.myCstr(dr("vendor_name")) 'vendorname
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCstr(dr("Item_Cost")) 'cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coluom).Value = clsCommon.myCstr(dr("Unit_Desc")) 'uom
                    Next
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnothers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothers.Click
        If clsCommon.CompairString(btnothers.Text, "Other Vendors") = CompairStringResult.Equal Then
            OtherVendorData()
            btnothers.Text = "Same Vendor"
        ElseIf clsCommon.CompairString(btnothers.Text, "Same Vendor") = CompairStringResult.Equal Then
            RefreshData()
            btnothers.Text = "Other Vendors"
        End If
    End Sub
End Class

Imports common
Imports System.Data.SqlClient

Public Class FrmCustomerSelector
    Public Arr As New List(Of clsCustomerMaster)
    Public ArrIn As List(Of String)
    Const colCustId As String = "CustId"
    Dim ArrCustid As ArrayList
    Private Sub FrmCustomerSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dgvCustomer.Rows.Clear()
            dgvCustomer.Columns.Clear()
            dgvCustomer.DataSource = Nothing
            Dim Qry As String = "select CAST(0 as BIT ) as [Select], TSPL_CUSTOMER_MASTER.Cust_Code as [Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Name], Case when len(TSPL_CUSTOMER_MASTER.Add1)>0 then TSPL_CUSTOMER_MASTER.Add1 else '' end +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then TSPL_CUSTOMER_MASTER.Add2  else  '' end + case when len(TSPL_CUSTOMER_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_CUSTOMER_MASTER.Add3)>0 then TSPL_CUSTOMER_MASTER.Add3  else  '' end as [Address], TSPL_CUSTOMER_MASTER.Route_No as [Route] ,Route_Desc as [Route Description],Closing_Date as [Closing Date], Cust_Category_Code as [Customer Category],  Cust_Group_Code as [Group],Contact_Person_Name [Contact Person],Contact_Person_Phone as [Contact No.],Phone1 as [Contact No. alternate],Contact_Person_Email as [Contact Person Email ID]  ,Terms_Code as [Terms Code], Cust_Account as [Customer Account],Payment_Code as [Payment Type],Channel_Code as [Channel Code]  ,Channel_Desc as [Channel Description], OnHold ,Remarks1,Additional1  ,Salesman_Code as [Salesman Code], Salesman_Desc as [Salesman Description]  ,Visi_Id as [Visi ID], Visi_Desc as [Visi Description], OutLet_Commossion as [Outlet Commition], Balance_ToDate as [Balance ToDate]  ,Credit_Limit as [Credit Limit], Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By], Modify_Date as [Modify Date]  ,Comp_Code as [Company Code],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer]  ,Inter_Branch as [Inter Branch] from TSPL_CUSTOMER_MASTER where Cust_Code not in (" + clsCommon.GetMulcallString(ArrIn) + ") "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            dgvCustomer.DataSource = dt
            FormatGrid()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()
        Me.dgvCustomer.MasterTemplate.Columns("Select").ReadOnly = False
        Me.dgvCustomer.MasterTemplate.Columns("Code").Width = 71
        Me.dgvCustomer.MasterTemplate.Columns("Code").ReadOnly = True
        Me.dgvCustomer.MasterTemplate.Columns("Name").Width = 201
        Me.dgvCustomer.MasterTemplate.Columns("Name").ReadOnly = True
        Me.dgvCustomer.MasterTemplate.Columns("Address").Width = 301
        Me.dgvCustomer.MasterTemplate.Columns("Address").ReadOnly = True
        Me.dgvCustomer.MasterTemplate.Columns("Route").Width = 51
        Me.dgvCustomer.MasterTemplate.Columns("Route").ReadOnly = True

        dgvCustomer.ShowFilteringRow = True
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Dim i As Integer
            For i = 0 To dgvCustomer.Rows.Count - 1
                If dgvCustomer.Rows(i).Cells("Select").Value = True Then
                    Dim obj As New clsCustomerMaster
                    obj.Cust_Code = clsCommon.myCstr(dgvCustomer.Rows(i).Cells("Code").Value)
                    obj.Customer_Name = clsCommon.myCstr(dgvCustomer.Rows(i).Cells("Name").Value)
                    obj.address = clsCommon.myCstr(dgvCustomer.Rows(i).Cells("Address").Value)
                    obj.Route_No = clsCommon.myCstr(dgvCustomer.Rows(i).Cells("Route").Value)
                    Arr.Add(obj)
                End If
            Next
            If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                Return
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Close()
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class

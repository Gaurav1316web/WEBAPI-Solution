Imports XpertERPEngine
Imports common
Imports System.Data.SqlClient

Public Class FrmItemMasterTax
    Inherits FrmMainTranScreen
#Region "Variables"
    Public stritemCode As String = ""
#End Region
    Private Sub FrmItemMasterTax_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CreateTable()
        LoadData()
        txtDate.Value = clsCommon.GETSERVERDATE
        btnSave.Enabled = True

    End Sub
    Private Sub LoadData()
        Try
            txtitemCode.Text = stritemCode
            Dim strqry As String = "select ITEM_CODE as [Item Code],EFFECTIVE_DATE as [EFFECTIVE DATE],case when IS_TAXABLE=0 then 'NO' else 'YES' end as [Taxable] from TSPL_ITEM_MASTER_TAXABLE where ITEM_CODE='" & stritemCode & "' order by EFFECTIVE_DATE desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.AllowDeleteRow = False
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                chkIsTaxable.Checked = IIf(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Taxable")), "YES") = CompairStringResult.Equal, True, False)
            Else
                Throw New Exception("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub CreateTable()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
            coll.Add("ITEM_CODE", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
            coll.Add("EFFECTIVE_DATE", "datetime not NULL")
            coll.Add("IS_TAXABLE", "integer not null DEFAULT 0")
            coll.Add("Created_By", "varchar(12) NOT NULL")
            coll.Add("Created_Date", "Datetime NOT NULL")

            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ITEM_MASTER_TAXABLE", coll, "", True, False, "", "ITEM_CODE", "EFFECTIVE_DATE", True)

            Dim itemCount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Item_Code) as noOfRecord from TSPL_ITEM_MASTER_TAXABLE "))
            If itemCount <= 0 Then
                Dim str As String = "INSERT INTO TSPL_ITEM_MASTER_TAXABLE (ITEM_CODE, IS_TAXABLE, EFFECTIVE_DATE,Created_By,Created_Date)
SELECT Item_Code,IsTaxable, '2022-07-01 00:00:00.000' as EFFECTIVE_DATE, Created_By,Created_Date
FROM TSPL_ITEM_MASTER"
                clsDBFuncationality.ExecuteNonQuery(str)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do you want to Save Item Code [" & txtitemCode.Text & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

            End If
            Dim strQry As String = "select IS_TAXABLE,EFFECTIVE_DATE from TSPL_ITEM_MASTER_TAXABLE where ITEM_CODE='" & txtitemCode.Text & "' order by EFFECTIVE_DATE desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("IS_TAXABLE")) = clsCommon.myCdbl(IIf(chkIsTaxable.Checked, 1, 0)) OrElse clsCommon.GetPrintDate(dt.Rows(0)("EFFECTIVE_DATE"), "dd/MMM/yyyy") = clsCommon.myCDate(txtDate.Value, "dd/MMM/yyyy") Then
                    Throw New Exception("Already Exits!")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ITEM_CODE", txtitemCode.Text)
            clsCommon.AddColumnsForChange(coll, "IS_TAXABLE", IIf(chkIsTaxable.Checked, 1, 0))
            clsCommon.AddColumnsForChange(coll, "EFFECTIVE_DATE", clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER_TAXABLE", OMInsertOrUpdate.Insert, "", trans)
            'strQry = "Update TSPL_ITEM_MASTER set istaxable='" & clsCommon.myCstr(IIf(chkIsTaxable.Checked, 1, 0)) & "' where Item_Code='" & clsCommon.myCstr(txtitemCode.Text) & "'"
            'clsDBFuncationality.ExecuteNonQuery("")
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
            btnSave.Enabled = False
            LoadData()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
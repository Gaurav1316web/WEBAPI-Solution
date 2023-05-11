Imports common
Imports System.Data.SqlClient

Public Class frmItemPrice
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemPrice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmItemPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funprint()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If
    End Sub
    Private Sub frmItemPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpitem.Value = Date.Today
        dtptodate.Value = Date.Today
        'fillddl()
        reset()
    End Sub

    'Public Sub fnditemCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strItem_Code As String = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + fndItemCode.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        dr = connectSql.RunSqlReturnDR(strItem_Code)
    '        Dim strvalue As String
    '        If dr.Read() Then
    '            strvalue = dr(0).ToString()
    '        End If
    '        If (strvalue <> "") Then
    '            txtDescription.Text = dr(1).ToString()

    '        Else
    '            txtDescription.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'Private Sub fnditemCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fnditemCode.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funprint()
    End Sub
    Sub funprint()
        Try
            Dim todate As String = dtptodate.Value
            Dim fromdate As String = dtpitem.Value
            Dim pricedate As String = Format(dtpitem.Value, "yyyy/MM/dd")
            Dim pricetodate As String = Format(dtptodate.Value, "yyyy/MM/dd")
            'Dim ddl As String = cmbuom.Text
            'If fndprice.Value = "" Then
            '    common.clsCommon.MyMessageBoxShow("Select the Price Code")
            '    Exit Sub
            'End If

            'Dim qry As String = "  select  * ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 from (  SELECT  Distinct '" + dtpitem.Value + "' as fdate,'" + dtptodate.Value + "' as todate,i.Item_Code, ISNULL(i.Item_Basic_Net, 0) AS item_MRP, i.Price_Comp1, ISNULL(i.Price_Amount1, 0) AS price_Amount1, i.Price_Comp2,    ISNULL(i.Price_Amount2, 0) AS price_Amount2, i.Price_Comp3, ISNULL(i.Price_Amount3, 0) AS price_Amount3, i.Price_Comp4, ISNULL(i.Price_Amount4,   0) AS price_Amount4, i.Price_Comp5, ISNULL(i.Price_Amount5, 0) AS price_Amount5, ISNULL(i.Item_Basic_Price, 0) AS item_baisc_price,   ( case when t.Excisable='Y' then  ISNULL(i.TAX1_Amt, 0) else 0 end) AS tax1_amt,  ( case when t.Excisable='Y'then ISNULL(i.TAX2_Amt, 0) else 0 end) AS tax2_amt,( case when t.Excisable='Y'then ISNULL(i.TAX3_Amt, 0) else 0 end) AS tax3_amt,      ( case when t.Excisable='Y'then  ISNULL(i.TAX4_Amt, 0) else ISNULL(i.TAX1_Amt, 0) end) AS tax4_amt,  ( case when t.Excisable='Y'then  ISNULL(i.TAX5_Amt, 0) else ISNULL(i.TAX2_Amt, 0) end) AS tax5_amt, m.Item_Desc, ISNULL(i.Item_Basic_Price, 0) + ISNULL(i.TAX1_Amt, 0) + ISNULL(i.TAX2_Amt, 0) + ISNULL(i.TAX3_Amt, 0) + ISNULL(i.TAX4_Amt, 0) + ISNULL(i.Price_Amount5, 0) AS Total, ISNULL(i.Item_Basic_Net, 0) - i.Price_Amount1 AS Retail_Price, ISNULL(i.Item_Basic_Net, 0) - (ISNULL(i.Price_Amount1, 0) + ISNULL(i.Price_Amount2, 0) + ISNULL(i.Price_Amount3, 0) + ISNULL(i.Price_Amount4, 0) + ISNULL(i.Price_Amount5, 0)) AS Net, i.Price_Amount5 AS TDT, i.Price_Comp_Desc1 AS desc1, i.Price_Comp_Desc2 AS desc2, i.Price_Comp_Desc3 AS desc3, i.Price_Comp_Desc4 AS desc4, i.Price_Comp_Desc5 AS desc5, CONVERT(VARCHAR(10), i.Start_Date, 103) AS start_date, '02/10/2011' AS From_date, '02/10/2011' AS To_date,i.UOM as UOM, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,isnull((i.Empty_Value_Shell+i.Empty_Value_Bottle),0) as Emty, i.price_code,i.Price_Code_Desc,i.Abatement,i.Comp_Code,(select Price_Amount1 from TSPL_ITEM_PRICE_MASTER where Price_Comp1 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT1,(select Price_Amount2 from TSPL_ITEM_PRICE_MASTER where Price_Comp2 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT2,(select Price_Amount3 from TSPL_ITEM_PRICE_MASTER where Price_Comp3 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT3,(select Price_Amount4 from TSPL_ITEM_PRICE_MASTER where Price_Comp4 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT4,(select Price_Amount5 from TSPL_ITEM_PRICE_MASTER where Price_Comp5 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT5,(select Price_Amount6 from TSPL_ITEM_PRICE_MASTER where Price_Comp6 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT6,(select Price_Amount7 from TSPL_ITEM_PRICE_MASTER where Price_Comp7 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT7,(select Price_Amount8 from TSPL_ITEM_PRICE_MASTER where Price_Comp8 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT8,(select Price_Amount9 from TSPL_ITEM_PRICE_MASTER where Price_Comp9 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT9,(select Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Price_Comp10 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT10       FROM     TSPL_ITEM_PRICE_MASTER AS i INNER JOIN                      TSPL_ITEM_MASTER AS m ON i.Item_Code = m.Item_Code   left Outer Join TSPL_TAX_GROUP_MASTER as t on i.Tax_group=t.Tax_Group_Code INNER JOIN TSPL_COMPANY_MASTER ON i.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code      WHERE(2 = 2) and  i.price_code='" + fndprice.Value + "' and convert(date,i.start_date,103) >= convert(date, '" & dtpitem.Value & "',103) AND convert(date,i.start_date,103) <= convert(date, '" & dtptodate.Value & "',103) "
            'Dim qry As String = "  select  *  from (  SELECT  Distinct '" + dtpitem.Value + "' as fdate,'" + dtptodate.Value + "' as todate,i.Item_Code, ISNULL(i.Item_Basic_Net, 0) AS item_MRP, i.Price_Comp1, ISNULL(i.Price_Amount1, 0) AS price_Amount1, i.Price_Comp2,    ISNULL(i.Price_Amount2, 0) AS price_Amount2, i.Price_Comp3, ISNULL(i.Price_Amount3, 0) AS price_Amount3, i.Price_Comp4, ISNULL(i.Price_Amount4,   0) AS price_Amount4, i.Price_Comp5, ISNULL(i.Price_Amount5, 0) AS price_Amount5, ISNULL(i.Item_Basic_Price, 0) AS item_baisc_price,   ( case when t.Excisable='Y' then  ISNULL(i.TAX1_Amt, 0) else 0 end) AS tax1_amt,  ( case when t.Excisable='Y'then ISNULL(i.TAX2_Amt, 0) else 0 end) AS tax2_amt,( case when t.Excisable='Y'then ISNULL(i.TAX3_Amt, 0) else 0 end) AS tax3_amt,      ( case when t.Excisable='Y'then  ISNULL(i.TAX4_Amt, 0) else ISNULL(i.TAX1_Amt, 0) end) AS tax4_amt,  ( case when t.Excisable='Y'then  ISNULL(i.TAX5_Amt, 0) else ISNULL(i.TAX2_Amt, 0) end) AS tax5_amt, m.Item_Desc, ISNULL(i.Item_Basic_Price, 0) + ISNULL(i.TAX1_Amt, 0) + ISNULL(i.TAX2_Amt, 0) + ISNULL(i.TAX3_Amt, 0) + ISNULL(i.TAX4_Amt, 0) + ISNULL(i.Price_Amount5, 0) AS Total, ISNULL(i.Item_Basic_Net, 0) - i.Price_Amount1 AS Retail_Price, ISNULL(i.Item_Basic_Net, 0) - (ISNULL(i.Price_Amount1, 0) + ISNULL(i.Price_Amount2, 0) + ISNULL(i.Price_Amount3, 0) + ISNULL(i.Price_Amount4, 0) + ISNULL(i.Price_Amount5, 0)) AS Net, i.Price_Amount5 AS TDT, i.Price_Comp_Desc1 AS desc1, i.Price_Comp_Desc2 AS desc2, i.Price_Comp_Desc3 AS desc3, i.Price_Comp_Desc4 AS desc4, i.Price_Comp_Desc5 AS desc5, CONVERT(VARCHAR(10), i.Start_Date, 103) AS start_date, '02/10/2011' AS From_date, '02/10/2011' AS To_date,i.UOM as UOM, isnull((i.Empty_Value_Shell+i.Empty_Value_Bottle),0) as Emty, i.price_code,i.Price_Code_Desc,i.Abatement,i.Comp_Code,(select Price_Amount1 from TSPL_ITEM_PRICE_MASTER where Price_Comp1 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT1,(select Price_Amount2 from TSPL_ITEM_PRICE_MASTER where Price_Comp2 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT2,(select Price_Amount3 from TSPL_ITEM_PRICE_MASTER where Price_Comp3 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT3,(select Price_Amount4 from TSPL_ITEM_PRICE_MASTER where Price_Comp4 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT4,(select Price_Amount5 from TSPL_ITEM_PRICE_MASTER where Price_Comp5 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT5,(select Price_Amount6 from TSPL_ITEM_PRICE_MASTER where Price_Comp6 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT6,(select Price_Amount7 from TSPL_ITEM_PRICE_MASTER where Price_Comp7 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT7,(select Price_Amount8 from TSPL_ITEM_PRICE_MASTER where Price_Comp8 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT8,(select Price_Amount9 from TSPL_ITEM_PRICE_MASTER where Price_Comp9 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT9,(select Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Price_Comp10 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT10       FROM     TSPL_ITEM_PRICE_MASTER AS i INNER JOIN                      TSPL_ITEM_MASTER AS m ON i.Item_Code = m.Item_Code   left Outer Join TSPL_TAX_GROUP_MASTER as t on i.Tax_group=t.Tax_Group_Code      WHERE(2 = 2) and  i.price_code='" + fndprice.Value + "' and convert(date,i.start_date,103) >= convert(date, '" & dtpitem.Value & "',103) AND convert(date,i.start_date,103) <= convert(date, '" & dtptodate.Value & "',103) "
            'Dim qry As String = "  select  *  from (  SELECT  Distinct '" + dtpitem.Value + "' as fdate,'" + dtptodate.Value + "' as todate,i.Item_Code, ISNULL(i.Item_Basic_Net, 0) AS item_MRP, i.Price_Comp1, ISNULL(i.Price_Amount1, 0) AS price_Amount1, i.Price_Comp2,    ISNULL(i.Price_Amount2, 0) AS price_Amount2, i.Price_Comp3, ISNULL(i.Price_Amount3, 0) AS price_Amount3, i.Price_Comp4, ISNULL(i.Price_Amount4,   0) AS price_Amount4, i.Price_Comp5, ISNULL(i.Price_Amount5, 0) AS price_Amount5, ISNULL(i.Item_Basic_Price, 0) AS item_baisc_price,   ( case when t.Excisable='Y' then  ISNULL(i.TAX1_Amt, 0) else 0 end) AS tax1_amt,  ( case when t.Excisable='Y'then ISNULL(i.TAX2_Amt, 0) else 0 end) AS tax2_amt,( case when t.Excisable='Y'then ISNULL(i.TAX3_Amt, 0) else 0 end) AS tax3_amt,      ( case when t.Excisable='Y'then  ISNULL(i.TAX4_Amt, 0) else ISNULL(i.TAX1_Amt, 0) end) AS tax4_amt,  ( case when t.Excisable='Y'then  ISNULL(i.TAX5_Amt, 0) else ISNULL(i.TAX2_Amt, 0) end) AS tax5_amt, m.Item_Desc, ISNULL(i.Item_Basic_Price, 0) + ISNULL(i.TAX1_Amt, 0) + ISNULL(i.TAX2_Amt, 0) + ISNULL(i.TAX3_Amt, 0) + ISNULL(i.TAX4_Amt, 0) + ISNULL(i.Price_Amount5, 0) AS Total, ISNULL(i.Item_Basic_Net, 0) - i.Price_Amount1 AS Retail_Price, ISNULL(i.Item_Basic_Net, 0) - (ISNULL(i.Price_Amount1, 0) + ISNULL(i.Price_Amount2, 0) + ISNULL(i.Price_Amount3, 0) + ISNULL(i.Price_Amount4, 0) + ISNULL(i.Price_Amount5, 0)) AS Net, i.Price_Amount5 AS TDT, i.Price_Comp_Desc1 AS desc1, i.Price_Comp_Desc2 AS desc2, i.Price_Comp_Desc3 AS desc3, i.Price_Comp_Desc4 AS desc4, i.Price_Comp_Desc5 AS desc5, CONVERT(VARCHAR(10), i.Start_Date, 103) AS start_date, '02/10/2011' AS From_date, '02/10/2011' AS To_date,i.UOM as UOM, isnull((i.Empty_Value_Shell+i.Empty_Value_Bottle),0) as Emty, i.price_code,i.Price_Code_Desc,i.Abatement,i.Comp_Code,(select Price_Amount1 from TSPL_ITEM_PRICE_MASTER where Price_Comp1 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT1,(select Price_Amount2 from TSPL_ITEM_PRICE_MASTER where Price_Comp2 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT2,(select Price_Amount3 from TSPL_ITEM_PRICE_MASTER where Price_Comp3 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT3,(select Price_Amount4 from TSPL_ITEM_PRICE_MASTER where Price_Comp4 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT4,(select Price_Amount5 from TSPL_ITEM_PRICE_MASTER where Price_Comp5 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT5,(select Price_Amount6 from TSPL_ITEM_PRICE_MASTER where Price_Comp6 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT6,(select Price_Amount7 from TSPL_ITEM_PRICE_MASTER where Price_Comp7 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT7,(select Price_Amount8 from TSPL_ITEM_PRICE_MASTER where Price_Comp8 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT8,(select Price_Amount9 from TSPL_ITEM_PRICE_MASTER where Price_Comp9 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT9,(select Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Price_Comp10 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT10       FROM     TSPL_ITEM_PRICE_MASTER AS i INNER JOIN                      TSPL_ITEM_MASTER AS m ON i.Item_Code = m.Item_Code   left Outer Join TSPL_TAX_GROUP_MASTER as t on i.Tax_group=t.Tax_Group_Code      WHERE(2 = 2) and Item_Price_ID=(select top 1 Item_Price_ID from TSPL_ITEM_PRICE_MASTER as InnerQry where InnerQry.Item_Code=i.Item_Code and InnerQry.UOM=i.UOM   order by Start_Date desc) and  i.price_code='' and convert(date,i.start_date,103) >= convert(date, '" & dtpitem.Value & "',103) AND convert(date,i.start_date,103) <= convert(date, '" & dtptodate.Value & "',103) "

            'Dim qry As String = "  select  *  from (  SELECT  Distinct '" + dtpitem.Value + "' as fdate,'" + dtptodate.Value + "' as todate,i.Item_Code, ISNULL(i.Item_Basic_Net, 0) AS item_MRP, i.Price_Comp1, ISNULL(i.Price_Amount1, 0) AS price_Amount1, i.Price_Comp2,    ISNULL(i.Price_Amount2, 0) AS price_Amount2, i.Price_Comp3, ISNULL(i.Price_Amount3, 0) AS price_Amount3, i.Price_Comp4, ISNULL(i.Price_Amount4,   0) AS price_Amount4, i.Price_Comp5, ISNULL(i.Price_Amount5, 0) AS price_Amount5, ISNULL(i.Item_Basic_Price, 0) AS item_baisc_price,   ( case when t.Excisable='Y' then  ISNULL(i.TAX1_Amt, 0) else 0 end) AS tax1_amt,  ( case when t.Excisable='Y'then ISNULL(i.TAX2_Amt, 0) else 0 end) AS tax2_amt,( case when t.Excisable='Y'then ISNULL(i.TAX3_Amt, 0) else 0 end) AS tax3_amt,      ( case when t.Excisable='Y'then  ISNULL(i.TAX4_Amt, 0) else ISNULL(i.TAX1_Amt, 0) end) AS tax4_amt,  ( case when t.Excisable='Y'then  ISNULL(i.TAX5_Amt, 0) else ISNULL(i.TAX2_Amt, 0) end) AS tax5_amt, m.Item_Desc, ISNULL(i.Item_Basic_Price, 0) + ISNULL(i.TAX1_Amt, 0) + ISNULL(i.TAX2_Amt, 0) + ISNULL(i.TAX3_Amt, 0) + ISNULL(i.TAX4_Amt, 0) + ISNULL(i.Price_Amount5, 0) AS Total, ISNULL(i.Item_Basic_Net, 0) - i.Price_Amount1 AS Retail_Price, ISNULL(i.Item_Basic_Net, 0) - (ISNULL(i.Price_Amount1, 0) + ISNULL(i.Price_Amount2, 0) + ISNULL(i.Price_Amount3, 0) + ISNULL(i.Price_Amount4, 0) + ISNULL(i.Price_Amount5, 0)) AS Net, i.Price_Amount5 AS TDT, i.Price_Comp_Desc1 AS desc1, i.Price_Comp_Desc2 AS desc2, i.Price_Comp_Desc3 AS desc3, i.Price_Comp_Desc4 AS desc4, i.Price_Comp_Desc5 AS desc5, CONVERT(VARCHAR(10), i.Start_Date, 103) AS start_date, '02/10/2011' AS From_date, '02/10/2011' AS To_date,i.UOM as UOM, isnull((i.Empty_Value_Shell+i.Empty_Value_Bottle),0) as Emty, i.price_code,i.Price_Code_Desc,i.Abatement,i.Comp_Code,(select Price_Amount1 from TSPL_ITEM_PRICE_MASTER where Price_Comp1 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT1,(select Price_Amount2 from TSPL_ITEM_PRICE_MASTER where Price_Comp2 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT2,(select Price_Amount3 from TSPL_ITEM_PRICE_MASTER where Price_Comp3 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT3,(select Price_Amount4 from TSPL_ITEM_PRICE_MASTER where Price_Comp4 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT4,(select Price_Amount5 from TSPL_ITEM_PRICE_MASTER where Price_Comp5 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT5,(select Price_Amount6 from TSPL_ITEM_PRICE_MASTER where Price_Comp6 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT6,(select Price_Amount7 from TSPL_ITEM_PRICE_MASTER where Price_Comp7 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT7,(select Price_Amount8 from TSPL_ITEM_PRICE_MASTER where Price_Comp8 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT8,(select Price_Amount9 from TSPL_ITEM_PRICE_MASTER where Price_Comp9 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT9,(select Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Price_Comp10 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT10       FROM     TSPL_ITEM_PRICE_MASTER AS i INNER JOIN                      TSPL_ITEM_MASTER AS m ON i.Item_Code = m.Item_Code   left Outer Join TSPL_TAX_GROUP_MASTER as t on i.Tax_group=t.Tax_Group_Code      WHERE(2 = 2) and Item_Price_ID=(select top 1 Item_Price_ID from TSPL_ITEM_PRICE_MASTER as InnerQry where InnerQry.Item_Code=i.Item_Code and InnerQry.UOM=i.UOM  and InnerQry.Item_Basic_Net=i.Item_Basic_Net   and InnerQry.Price_Code=i.Price_Code   order by Start_Date desc) "

            If chkpriceselect.IsChecked = True AndAlso cbgPrice.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Price Code or select ALL")
                Exit Sub
            End If

            If chkselect.IsChecked = True AndAlso cgvitems.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Item or select ALL")
                Exit Sub
            End If
            If chktypeSelect.IsChecked = True AndAlso cbgtype.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one UOM or select ALL")
                Exit Sub
            End If


            Dim qry As String = "  select  *  from (  SELECT  Distinct '" + dtpitem.Value + "' as fdate,'" + dtptodate.Value + "' as todate,i.Item_Code, ISNULL(i.Item_Basic_Net, 0) AS item_MRP, i.Price_Comp1, ISNULL(i.Price_Amount1, 0) AS price_Amount1, i.Price_Comp2,    ISNULL(i.Price_Amount2, 0) AS price_Amount2, i.Price_Comp3, ISNULL(i.Price_Amount3, 0) AS price_Amount3, i.Price_Comp4, ISNULL(i.Price_Amount4,   0) AS price_Amount4, i.Price_Comp5, ISNULL(i.Price_Amount5, 0) AS price_Amount5, ISNULL(i.Item_Basic_Price, 0) AS item_baisc_price,   ( case when t.Excisable='Y' then  ISNULL(i.TAX1_Amt, 0) else 0 end) AS tax1_amt,  ( case when t.Excisable='Y'then ISNULL(i.TAX2_Amt, 0) else 0 end) AS tax2_amt,( case when t.Excisable='Y'then ISNULL(i.TAX3_Amt, 0) else 0 end) AS tax3_amt,      ( case when t.Excisable='Y'then  ISNULL(i.TAX4_Amt, 0) else ISNULL(i.TAX1_Amt, 0) end) AS tax4_amt,  ( case when t.Excisable='Y'then  ISNULL(i.TAX5_Amt, 0) else ISNULL(i.TAX2_Amt, 0) end) AS tax5_amt, m.Item_Desc, ISNULL(i.Item_Basic_Price, 0) + ISNULL(i.TAX1_Amt, 0) + ISNULL(i.TAX2_Amt, 0) + ISNULL(i.TAX3_Amt, 0) + ISNULL(i.TAX4_Amt, 0) + ISNULL(i.Price_Amount5, 0) AS Total, ISNULL(i.Item_Basic_Net, 0) - i.Price_Amount1 AS Retail_Price, ISNULL(i.Item_Basic_Net, 0) - (ISNULL(i.Price_Amount1, 0) + ISNULL(i.Price_Amount2, 0) + ISNULL(i.Price_Amount3, 0) + ISNULL(i.Price_Amount4, 0) + ISNULL(i.Price_Amount5, 0)) AS Net, i.Price_Amount5 AS TDT, i.Price_Comp_Desc1 AS desc1, i.Price_Comp_Desc2 AS desc2, i.Price_Comp_Desc3 AS desc3, i.Price_Comp_Desc4 AS desc4, i.Price_Comp_Desc5 AS desc5, CONVERT(VARCHAR(10), i.Start_Date, 103) AS start_date, '02/10/2011' AS From_date, '02/10/2011' AS To_date,i.UOM as UOM, isnull((i.Empty_Value_Shell+i.Empty_Value_Bottle),0) as Emty, i.price_code,i.Price_Code_Desc,i.Abatement,i.Comp_Code," & _
          "  (case when  i.Price_Comp1='TPT' then  i.Price_Amount1 else null end ) as TPT1 , " & _
  " (case when  i.Price_Comp2='TPT' then  i.Price_Amount2 else null end ) as TPT2 , " & _
  " (case when  i.Price_Comp3='TPT' then  i.Price_Amount3 else null end ) as TPT3 , " & _
  " (case when  i.Price_Comp4='TPT' then  i.Price_Amount4 else null end ) as TPT4 , " & _
  " (case when  i.Price_Comp5='TPT' then  i.Price_Amount5 else null end ) as TPT5 , " & _
  " (case when  i.Price_Comp6='TPT' then  i.Price_Amount6 else null end ) as TPT6 , " & _
  " (case when  i.Price_Comp7='TPT' then  i.Price_Amount7 else null end ) as TPT7 , " & _
  " (case when  i.Price_Comp8='TPT' then  i.Price_Amount8 else null end ) as TPT8 , " & _
  " (case when  i.Price_Comp9='TPT' then  i.Price_Amount9 else null end ) as TPT9 , " & _
   " (case when  i.Price_Comp10='TPT' then  i.Price_Amount10 else null end ) as TPT10 " & _
"  FROM     TSPL_ITEM_PRICE_MASTER AS i INNER JOIN  TSPL_ITEM_MASTER AS m ON i.Item_Code = m.Item_Code   left Outer Join TSPL_TAX_GROUP_MASTER as t on i.Tax_group=t.Tax_Group_Code      WHERE(2 = 2) and Item_Price_ID=(select top 1 Item_Price_ID from TSPL_ITEM_PRICE_MASTER as InnerQry where InnerQry.Item_Code=i.Item_Code and InnerQry.UOM=i.UOM  and InnerQry.Item_Basic_Net=i.Item_Basic_Net   and InnerQry.Price_Code=i.Price_Code   order by Start_Date desc) "






            If chkpriceselect.IsChecked = True Then
                qry += " and  i.price_code in (" + (clsCommon.GetMulcallString(cbgPrice.CheckedValue)) + ") "
            End If

            qry += "and convert(date,i.start_date,103) >= convert(date, '" & dtpitem.Value & "',103) AND convert(date,i.start_date,103) <= convert(date, '" & dtptodate.Value & "',103) "



            If chkselect.IsChecked = True Then

                qry += " and   i.item_code  in(" + (clsCommon.GetMulcallString(cgvitems.CheckedValue)) + ") "

            End If

            If chktypeSelect.IsChecked = True Then

                qry += " and   i.uom in(" + (clsCommon.GetMulcallString(cbgtype.CheckedValue)) + ") "

            End If

            qry += " ) xxx left outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=xxx.Comp_Code  order by xxx.Start_Date desc "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "ItemPrice", "Item Price Report")
            frmCRV = Nothing
            'frmInventoryReportViewer.proShowReport("Item Price", fndItemCode.txtValue.Text, pricedate, pricetodate, fromdate, todate, ddl)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Private Sub fndItemCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndItemCode.ConnectionString = connectSql.SqlCon()
    '    fndItemCode.Query = "select  Item_Code as [Item Code],Item_Desc as [Item Desc] from TSPL_ITEM_MASTER"
    '    fndItemCode.ValueToSelect = "Item Code"
    '    fndItemCode.Caption = "Item Master"
    '    fndItemCode.txtValue.MaxLength = 50
    '    fndItemCode.ValueToSelect1 = "Item Desc"
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Public Sub fillddl()

    '    Try
    '        Dim qry As String = "select distinct uom from tspl_item_price_master"
    '        transportSql.FillComboBox(qry, cmbuom, "UOM", "UOM")
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try

    'End Sub

    Sub LoadUOM()
        Dim qry As String = " select distinct uom as UOM from tspl_item_price_master  "
        cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgtype.ValueMember = "UOM"
        cbgtype.DisplayMember = "UOM"
    End Sub


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Public Sub reset()
        'fndItemCode.txtValue.Text = ""
        'txtDescription.Text = ""
        dtpitem.Value = Date.Today()
        dtptodate.Value = Date.Today()
        'cmbuom.Text = "Select"
        chkitemall.IsChecked = True
        chktypeAll.IsChecked = True
        chkPriceall.IsChecked = True
        LoadItem()
        LoadUOM()
        LoadPriceCode()
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITM-PRIC-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    'Private Sub fndprice__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = "select distinct Price_Code as Code,Price_Code_Desc from TSPL_ITEM_PRICE_MASTER "
    '    'Dim WhrCls As String = "Location_Type='Physical'"
    '    fndprice.Value = clsCommon.ShowSelectForm("Price Code", qry, "Code", "", fndprice.Value, "Code", isButtonClicked)
    'End Sub

    Sub LoadItem()
        Dim qry As String = " select Item_Code as [Item Code],Item_Desc as [Description] from TSPL_ITEM_MASTER  where Item_Type='F'  "
        cgvitems.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvitems.ValueMember = "Item Code"
        cgvitems.DisplayMember = "Description"
    End Sub


    Sub LoadPriceCode()
        Dim qry As String = " select distinct Price_Code as Code,Price_Code_Desc as Description from TSPL_ITEM_PRICE_MASTER "
        cbgPrice.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgPrice.ValueMember = "Code"
        cbgPrice.DisplayMember = "Description"
    End Sub

    Private Sub chkitemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkitemall.ToggleStateChanged
        cgvitems.Enabled = Not chkitemall.IsChecked
    End Sub

    Private Sub chktypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeAll.ToggleStateChanged
        cbgtype.Enabled = Not chktypeAll.IsChecked
    End Sub

    Private Sub chkPriceall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPriceall.ToggleStateChanged
        cbgPrice.Enabled = Not chkPriceall.IsChecked
    End Sub
End Class

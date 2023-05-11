Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
' Ticket No : ERO/11/07/18-000369 By Prabhakar - Create new screen 
'' work done agaist ticket no. ERO/12/12/18-000439 
Public Class FrmItemProjection
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Public isLoadData As Boolean = False
    Dim obj As clsItemProjectionHead = Nothing
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIAvarage As String = "COLAVARAGE"
    Const colTolerance As String = "COLTOLERANCE"
    Const colTotal As String = "COLTOTAL"
    Const colUOM As String = "COLUOM"

    Private Sub FrmItemProjection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
   
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        'LoadBlankGrid()

        Dim strProjectionDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Value), "dd/MM/yyyy")
        Dim strPivotItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + QUOTENAME( VirtualTable.Item_Code) FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreePivotItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + QUOTENAME( VirtualTable.Item_Code+'_Free') FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 ) as VirtualTable   order by VirtualTable.Item_Code   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        '=======================================================================
        Dim strPivotDocumentDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + QUOTENAME( VirtualTable.Document_Date) FROM (Select distinct convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as  Document_Date From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 ) as VirtualTable   order by VirtualTable.Document_Date  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strPivotDocumentDateSum As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+'  + QUOTENAME( VirtualTable.Document_Date) FROM (Select distinct convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as  Document_Date From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 ) as VirtualTable   order by VirtualTable.Document_Date  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))


        Dim strAllPivotItemCode As String = ""
        If clsCommon.myLen(strPivotDocumentDate) > 0 Then
            strAllPivotItemCode = strPivotDocumentDate
        Else
            strAllPivotItemCode = ""
            clsCommon.MyMessageBoxShow("No Data Found to Display")
            Exit Sub
        End If
        If clsCommon.myLen(strAllPivotItemCode) > 0 AndAlso clsCommon.myLen(strFreePivotItemCode) > 0 Then
            strAllPivotItemCode = strAllPivotItemCode + "," + strFreePivotItemCode
        ElseIf clsCommon.myLen(strAllPivotItemCode) <= 0 AndAlso clsCommon.myLen(strFreePivotItemCode) > 0 Then
            strAllPivotItemCode = strFreePivotItemCode
        End If
        '============================================================================
        Dim strFinalQeryItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'FinalQry.' + VirtualTable.Item_Code FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeFinalQeryItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'FinalQry.'+ VirtualTable.Item_Code+'_Free' FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strAllFinalQeryItemCode As String = ""
        If clsCommon.myLen(strFinalQeryItemCode) Then
            strAllFinalQeryItemCode = strFinalQeryItemCode
        Else
            strAllFinalQeryItemCode = ""
        End If
        If clsCommon.myLen(strAllFinalQeryItemCode) > 0 AndAlso clsCommon.myLen(strFreeFinalQeryItemCode) > 0 Then
            strAllFinalQeryItemCode = strAllFinalQeryItemCode + "," + strFreeFinalQeryItemCode
        ElseIf clsCommon.myLen(strAllFinalQeryItemCode) <= 0 AndAlso clsCommon.myLen(strFreeFinalQeryItemCode) > 0 Then
            strAllFinalQeryItemCode = strFreeFinalQeryItemCode
        End If

        '=============================================================================
        Dim strVarcharNullItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'convert (varchar,isnull( ' + VirtualTable.Item_Code+ ',0)) as ' + VirtualTable.Item_Code FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeVarcharNullItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'convert (varchar,isnull( ' + VirtualTable.Item_Code+'_Free' + ',0)) as ' + VirtualTable.Item_Code+'_Free'  FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strAllVarcharNullItemCode As String = ""
        If clsCommon.myLen(strVarcharNullItemCode) > 0 Then
            strAllVarcharNullItemCode = strVarcharNullItemCode
        Else
            strAllVarcharNullItemCode = ""
        End If
        If clsCommon.myLen(strAllVarcharNullItemCode) > 0 AndAlso clsCommon.myLen(strFreeVarcharNullItemCode) > 0 Then
            strAllVarcharNullItemCode = strAllVarcharNullItemCode + "," + strFreeVarcharNullItemCode
        ElseIf clsCommon.myLen(strAllVarcharNullItemCode) <= 0 AndAlso clsCommon.myLen(strFreeVarcharNullItemCode) > 0 Then
            strAllVarcharNullItemCode = strAllVarcharNullItemCode
        End If

        '=====================Total========================================================
        Dim strVarcharNullItemCodeTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+'  +' convert (decimal(10,2),isnull( ' + VirtualTable.Item_Code+ ',0)) '  FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeVarcharNullItemCodeTotalFree As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+'  + ' convert (decimal(10,2),isnull( ' + VirtualTable.Item_Code+'_Free' + ',0)) '   FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strTotal As String = "0"
        Dim strTotalFree As String = "0"
        If clsCommon.myLen(strVarcharNullItemCodeTotal) > 0 Then
            strTotal = " Cast(( " + strVarcharNullItemCodeTotal + " ) as Varchar)"
        End If
        If clsCommon.myLen(strFreeVarcharNullItemCodeTotalFree) > 0 Then
            strTotalFree = " Cast(( " + strFreeVarcharNullItemCodeTotalFree + " ) as Varchar)"
        End If
        '==============================================================================
        Dim strMaxFinalItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'max (Final . ' + VirtualTable.Item_Code+ ') as ' + VirtualTable.Item_Code FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeMaxFinalItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'max (Final .' + VirtualTable.Item_Code+'_Free' + ') as ' + VirtualTable.Item_Code+'_Free'  FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllMaxFinalItemCode As String = ""
        If clsCommon.myLen(strMaxFinalItemCode) > 0 Then
            strAllMaxFinalItemCode = strMaxFinalItemCode
        Else
            strAllMaxFinalItemCode = ""
        End If
        If clsCommon.myLen(strAllMaxFinalItemCode) > 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCode) > 0 Then
            strAllMaxFinalItemCode = strAllMaxFinalItemCode + " ," + strFreeMaxFinalItemCode
        ElseIf clsCommon.myLen(strAllMaxFinalItemCode) <= 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCode) > 0 Then
            strAllMaxFinalItemCode = strFreeMaxFinalItemCode
        End If
        ' ======================================Avg/3===================================
        Dim strMaxFinalItemCodeAvg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'convert (varchar,Cast(( sum (  isnull(convert (decimal(10,2),Final . ' + VirtualTable.Item_Code+ '),0))/3) as decimal(10,2)) ) as ' + VirtualTable.Item_Code FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeMaxFinalItemCodeAvg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'convert (varchar,Cast(( sum (  isnull(convert (decimal(10,2),Final . ' + VirtualTable.Item_Code+'_Free' + '),0))/3) as decimal(10,2)) ) as ' + VirtualTable.Item_Code+'_Free'  FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllMaxFinalItemCodeAvg As String = ""
        If clsCommon.myLen(strMaxFinalItemCodeAvg) > 0 Then
            strAllMaxFinalItemCodeAvg = strMaxFinalItemCodeAvg
        Else
            strAllMaxFinalItemCodeAvg = ""
        End If
        If clsCommon.myLen(strAllMaxFinalItemCodeAvg) > 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCodeAvg) > 0 Then
            strAllMaxFinalItemCodeAvg = strAllMaxFinalItemCodeAvg + " ," + strFreeMaxFinalItemCodeAvg
        ElseIf clsCommon.myLen(strAllMaxFinalItemCodeAvg) <= 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCodeAvg) > 0 Then
            strAllMaxFinalItemCodeAvg = strFreeMaxFinalItemCodeAvg
        End If
        '===============================================================================
        Dim strItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','   + VirtualTable.Item_Code FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +  VirtualTable.Item_Code+'_Free'   FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1   ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllItemCode As String = ""
        If clsCommon.myLen(strItemCode) > 0 Then
            strAllItemCode = strItemCode
        Else
            strAllItemCode = ""
        End If
        If clsCommon.myLen(strAllItemCode) > 0 AndAlso clsCommon.myLen(strFreeItemCode) > 0 Then
            strAllItemCode = strAllItemCode + " , " + strFreeItemCode
        ElseIf clsCommon.myLen(strAllItemCode) <= 0 AndAlso clsCommon.myLen(strFreeItemCode) > 0 Then
            strAllItemCode = strFreeItemCode
        End If
        '===============================================================================
        Dim strAvgItmecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ',' +' '+ VirtualTable.Item_Code +' as ' + VirtualTable.Item_Code  FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeAvgItmecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +' '+  VirtualTable.Item_Code+'_Free' +' as ' + VirtualTable.Item_Code + '_Free'  FROM (Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  From TSPL_SD_SALE_INVOICE_DETAIL  Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y'  and TSPL_SD_SALE_INVOICE_HEAD.Status = 1  ) as VirtualTable   order by VirtualTable.Item_Code FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strAllAvgItmecode As String = ""
        If clsCommon.myLen(strAvgItmecode) > 0 Then
            strAllAvgItmecode = strAvgItmecode
        Else
            strAllAvgItmecode = ""
        End If
        If clsCommon.myLen(strAllAvgItmecode) > 0 AndAlso clsCommon.myLen(strFreeAvgItmecode) > 0 Then
            strAllAvgItmecode = strAllAvgItmecode + " , " + strFreeAvgItmecode
        ElseIf clsCommon.myLen(strAllAvgItmecode) <= 0 AndAlso clsCommon.myLen(strFreeAvgItmecode) > 0 Then
            strAllAvgItmecode = strFreeAvgItmecode
        End If
        '================================================================================
        'Dim Qry As String = "  select FinalQry.SNo ,FinalQry.Document_Date, " + strAllFinalQeryItemCode + " ,FinalQry.Total,FinalQry.Total_Free from (  " & _
        '                            "  select 2 as SNo, Document_Date, " + strAllVarcharNullItemCode + " , " + strTotal + " as Total , " + strTotalFree + " as Total_Free from (  " & _
        '                            "  select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,  Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                            "  Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                            "  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                            "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                               "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code " & _
        '                            "  where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                            "  Union all " & _
        '                            "  select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                            "  Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                            "  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " & _
        '                            "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                               "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code " & _
        '                            "  where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                            "  ) As SourceTable " & _
        '                            "  Pivot " & _
        '                            "    (  " & _
        '                            "  sum(Qty) " & _
        '                            "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                            "  ) AS PivotTable " & _
        '                    "  Union All " & _
        '                    " select  Final.SNo ,max(Final.Document_Date) as Document_Date, " + strAllMaxFinalItemCodeAvg + " , '' as Total , '' as Total_Free from ( " & _
        '                           " select 3 as SNo, 'AVERAGE' as  Document_Date, " + strAllAvgItmecode + " from ( " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                             "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " Union all " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty  from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                              "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " ) As SourceTable " & _
        '                           " PIVOT " & _
        '                           "  ( " & _
        '                           "   sum(Qty) " & _
        '                           "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNo " & _
        '                    " Union All " & _
        '                    " select  Final.SNo ,max(Final.Document_Date) as Document_Date, " + strAllMaxFinalItemCodeAvg + " , '' as Total , '' as Total_Free from ( " & _
        '                           " select 4 as SNo, ' PROJECTION for " + strProjectionDate + "' as  Document_Date, " + strAllAvgItmecode + " from ( " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,  Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                              "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103)   and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " Union all " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                              "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103)   and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " ) As SourceTable " & _
        '                           "  Pivot " & _
        '                           "   ( " & _
        '                           "  sum(Qty) " & _
        '                           " FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNo " & _
        '                    " Union All " & _
        '                    " select  Final.SNO ,max(Final.Document_Date) as Document_Date ," + strAllMaxFinalItemCode + " , '' as Total , '' as Total_Free from ( " & _
        '                           " select 0 as SNO, 'Item Desc' as Document_Date , " + strAllItemCode + "  from ( " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,  TSPL_ITEM_MASTER .Item_Desc as Item_Desc from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " Union all " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' ,   TSPL_ITEM_MASTER .Item_Desc as Item_Desc from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " ) As SourceTable " & _
        '                           " Pivot " & _
        '                           " ( " & _
        '                           " max(Item_Desc) " & _
        '                           " FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNO  " & _
        '                     " Union All " & _
        '                     " select  Final.SNO ,max(Final.Document_Date) as Document_Date , " + strAllMaxFinalItemCode + " , '' as Total , '' as Total_Free from (  " & _
        '                           " select SNO , Document_Date ," + strAllItemCode + " from ( " & _
        '                           " select  1 as SNO , 'Default UOM'  as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_UOM_DETAIL.UOM_Code  from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM = 1  " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " Union all " & _
        '                           " select 1 as SNO , 'Default UOM'  as Document_Date  ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , TSPL_ITEM_UOM_DETAIL.UOM_Code  from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM = 1  " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & _
        '                           " ) As SourceTable " & _
        '                           " Pivot " & _
        '                           " ( " & _
        '                           "  max(UOM_Code) " & _
        '                           "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNO  " & _
        '                           " ) FinalQry order by FinalQry.SNO "

        Dim qry As String = " select *,convert(decimal(18,2),(" + strPivotDocumentDateSum + ")/3) as [Projection for currrent date " + strProjectionDate + "],Tolerance1 as Tolerance,Actual_Projection as [Actual Projection] from (select * from ( select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [Item Code] ,  Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty "
        qry += " ,TSPL_ITEM_MASTER.Item_Desc as [Description],TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,0 as Tolerance1,0 as Actual_Projection"
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL   Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code   left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code   where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1   "
        qry += " Union all   select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , Cast( ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_SU2. Conversion_Factor )/ nullif( Defalt_SU.Conversion_Factor,0)) as Decimal(18,2) )as Qty "
        qry += " ,TSPL_ITEM_MASTER.Item_Desc as [Description],TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,0 as Tolerance,0 as Actual_Projection"
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL   Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Defalt_SU.Item_Code left outer join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL  ) as Stocking_SU2 on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=Stocking_SU2.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_SU2.UOM_Code   where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1   ) "
        qry += " as xxx "
        qry += " pivot(sum(qty) for Document_Date in (" + strPivotDocumentDate + ")) as pvt)final"


        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        gv.DataSource = Nothing

        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dtgv

        If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display")
            Exit Sub
        Else
            'gv.DataSource = Nothing
            'gv.Rows.Clear()
            'gv.Columns.Clear()
            'LoadBlankGrid()
            'For Each row As DataRow In dtgv.Rows
            '    gv.Rows.AddNew()
            '    gv.Rows(gv.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(row("Item_Code").ToString().Trim())
            '    gv.Rows(gv.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(row("Description").ToString().Trim())
            '    gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(row("UOM").ToString().Trim())
            'Next
        End If
        'If gv.Rows.Count > 0 Then
        '    gv.Columns(0).IsVisible = False
        '    gv.Columns(0).IsPinned = True
        '    gv.Columns(1).IsPinned = True
        '    'gv.Columns(gv.Columns.Count - 1).IsPinned = True
        '    'gv.Columns(gv.Columns.Count - 1).PinPosition = PinnedColumnPosition.Right
        '    'clsCommon.MyMessageBoxShow(gv.Columns(1).Name.ToString())
        '    ' clsCommon.MyMessageBoxShow(gv.Columns(2).Name.ToString())
        '    'clsCommon.MyMessageBoxShow(gv.Columns(3).Name.ToString())
        'End If
        btnGo.Enabled = False
        txtDate.Enabled = False
        gv.AllowColumnReorder = False

        FormatGrid()
        gv.Columns("Tolerance").ReadOnly = False
        gv.Columns("Tolerance1").IsVisible = False
        gv.Columns("Actual_Projection").IsVisible = False


    End Sub
    Private Sub FormatGrid()

        gv.TableElement.TableHeaderHeight = 80
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
        Next


        gv.EnableFiltering = True
        gv.BestFitColumns()
        gv.AllowColumnChooser = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(txtDate.Value) <= 0 Then
                Throw New Exception("Projection Date Can't left blank")
            End If
            If gv.Rows.Count <= 0 Then
                Throw New Exception("No Data Found For Save")
            End If
           
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()
        Try
            obj = New clsItemProjectionHead
            obj.Projection_Code = clsCommon.myCstr(txtDocNo.Value)
            obj.Projection_Date = txtDate.Value
            obj.Post_Status = 0
            obj.Arr = New List(Of clsItemProjectionDetails)
            Dim rowCount As Integer = gv.Rows.Count
            Dim avgRow As Integer = gv.Columns.Count - 2
            Dim avgProjRow As Integer = gv.Columns.Count - 1
            For ii As Integer = 5 To gv.Columns.Count - 3

                For rr As Integer = 0 To gv.Rows.Count - 1
                    Dim objTr As New clsItemProjectionDetails()
                    Dim strItemCodeWithFreeDate As String = clsCommon.myCstr(gv.Columns(ii).Name)
                    Dim words As String = strItemCodeWithFreeDate
                    If words.ToString.Length > 38 Then
                        words = clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Value), "dd/MM/yyyy")
                    End If
                    objTr.Item_Code = clsCommon.myCstr(gv.Rows(rr).Cells(0).Value)
                    objTr.Item_Desc = clsCommon.myCstr(gv.Rows(rr).Cells(1).Value) 'clsCommon.myCstr(gv.Columns(ii).Name)
                    objTr.Item_Qty = clsCommon.myCdbl(gv.Rows(rr).Cells(ii).Value)
                    objTr.UOM = clsCommon.myCstr(gv.Rows(rr).Cells(2).Value)
                    objTr.Doc_Date = words
                    'objTr.Average = clsCommon.myCdbl(gv.Rows(avgRow).Cells(ii).Value)
                    'objTr.Projection_Average = clsCommon.myCdbl(gv.Rows(avgProjRow).Cells(ii).Value)
                    If words.Count > 1 Then
                        If clsCommon.CompairString(clsCommon.myCstr(words(1)), "Free") = CompairStringResult.Equal Then
                            objTr.Is_Item_Free = 1
                        Else
                            objTr.Is_Item_Free = 0
                        End If
                    Else
                        objTr.Is_Item_Free = 0
                    End If
                    objTr.Tollence = clsCommon.myCdbl(gv.Rows(rr).Cells(avgRow).Value)
                    objTr.Actual_Projection = clsCommon.myCdbl(gv.Rows(rr).Cells(avgProjRow).Value)

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        If clsCommon.CompairString(objTr.Item_Code, "Total") <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(objTr.Item_Code, "Total_Free") <> CompairStringResult.Equal Then
                                obj.Arr.Add(objTr)
                            End If
                        End If
                    End If
                Next

            Next

            'For Each grow As GridViewRowInfo In gv.Rows
            '    Dim objTr As New clsItemProjectionDetails()
            '    For values As Integer = 2 To gv.Columns.Count
            '        objTr.SNO = clsCommon.myCdbl(grow.Cells(0).Value)
            '        objTr.Doc_Date = clsCommon.myCstr(grow.Cells(1).Value)
            '        objTr.Item_Code = gv.Columns(values).Name.ToString()

            '        objTr.Defalt_Uom = clsCommon.myCstr(grow.Cells(values).Value)
            '        objTr.Is_Item_Free = 0
            '        objTr.Item_Qty = clsCommon.myCstr(grow.Cells(values).Value)
            '        objTr.Average = clsCommon.myCstr(grow.Cells(values).Value)
            '        objTr.Projection_Average = clsCommon.myCstr(grow.Cells(values).Value)

            '    Next

            'Next
            If obj.SaveData(obj, isNewEntry) Then
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully")
                End If
                LoadData(obj.Projection_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Sub LoadBlankGrid()
    '    gv.Rows.Clear()
    '    gv.Columns.Clear()





    '    Dim repoTotal As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoTotal.FormatString = ""
    '    repoTotal.HeaderText = "Actual Projection"
    '    repoTotal.Name = colTotal
    '    repoTotal.Width = 150
    '    repoTotal.ReadOnly = False
    '    gv.MasterTemplate.Columns.Add(repoTotal)

    '    gv.AllowDeleteRow = True
    '    gv.AllowAddNewRow = False
    '    gv.ShowGroupPanel = False
    '    gv.AllowColumnReorder = False
    '    gv.AllowRowReorder = False
    '    gv.EnableSorting = False
    '    gv.AllowSearchRow = True
    '    gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gv.MasterTemplate.ShowRowHeaderColumn = False
    '    gv.TableElement.TableHeaderHeight = 40

    '    gv.AutoSizeRows = True


    'End Sub
    Sub LoadData(ByVal str As String, ByVal navtype As NavigatorType)
        Try
            Reset()
            obj = clsItemProjectionHead.GetData(str, navtype)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Projection_Code) > 0 Then
                isLoadData = True
                isNewEntry = False
                txtDocNo.Value = obj.Projection_Code
                txtDate.Value = obj.Projection_Date
                lblPending.Status = obj.Post_Status

                If obj.Post_Status = common.ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If

                btnSave.Text = "Update"
                txtDocNo.MyReadOnly = True
                isLoadData = False
                btnGo.Enabled = False
                txtDate.Enabled = False
                gv.AllowColumnReorder = False
                '===========================
                Dim qry As String = LoadDataDetailsQry(obj.Projection_Code, obj.Projection_Date)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv.DataSource = Nothing

                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dt
                    gv.BestFitColumns()
                    'If gv.Rows.Count > 0 Then
                    '    gv.Columns(0).IsVisible = False
                    '    gv.Columns(0).IsPinned = True
                    '    gv.Columns(1).IsPinned = True
                    'End If

                    FormatGrid()
                    gv.Columns("Tolerance").ReadOnly = False
                    gv.Columns("Tollence").IsVisible = False
                    gv.Columns("Actual_Projection").IsVisible = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        isNewEntry = True
        btnSave.Text = "Save"
        txtDocNo.MyReadOnly = False
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        btnGo.Enabled = True
        txtDate.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
    End Sub
   
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub
    Sub deleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No Projection Code found to Delete")
            End If

            If myMessages.deleteConfirm() Then
                clsItemProjectionHead.deleteData(txtDocNo.Value)
                myMessages.delete()
                Reset()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No Projection Code found to post")
            End If

            If clsCommon.MyMessageBoxShow("Post the current Projection Code - " + txtDocNo.Value + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsItemProjectionHead.PostData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow("Sucessfully Posted", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Function LoadDataDetailsQry(ByVal strProjectionCode As String, ByVal strDocDate As Date) As String
        Dim strProjectionDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MM/yyyy")
        Dim strPivotItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + QUOTENAME( VirtualTable.Item_Code) FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreePivotItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + QUOTENAME( VirtualTable.Item_Code+'_Free') FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        '=======================================================================

        Dim strPivotDocumentDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + QUOTENAME( VirtualTable.Document_Date) FROM (Select distinct convert(varchar,TSPL_ITEM_PROJECTION_DETAILS.Doc_Date,103) as  Document_Date From TSPL_ITEM_PROJECTION_DETAILS where Projection_Code='" & txtDocNo.Value & "') as VirtualTable   order by VirtualTable.Document_Date  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strPivotDocumentDateSum As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+'  + QUOTENAME( VirtualTable.Document_Date) FROM (Select distinct convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as  Document_Date From TSPL_SD_SALE_INVOICE_DETAIL Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where (Item_Code !='' or Item_Code is null) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 ) as VirtualTable   order by VirtualTable.Document_Date  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))


        Dim strAllPivotItemCode As String = ""
        If clsCommon.myLen(strPivotItemCode) > 0 Then
            strAllPivotItemCode = strPivotItemCode
        Else
            strAllPivotItemCode = ""
            ' clsCommon.MyMessageBoxShow("No Data Found to Display")
            'Exit Function
        End If
        If clsCommon.myLen(strAllPivotItemCode) > 0 AndAlso clsCommon.myLen(strFreePivotItemCode) > 0 Then
            strAllPivotItemCode = strAllPivotItemCode + "," + strFreePivotItemCode
        ElseIf clsCommon.myLen(strAllPivotItemCode) <= 0 AndAlso clsCommon.myLen(strFreePivotItemCode) > 0 Then
            strAllPivotItemCode = strFreePivotItemCode
        End If
        '============================================================================
        Dim strFinalQeryItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'FinalQry.' + VirtualTable.Item_Code FROM  (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free= '0' ) as VirtualTable   order by VirtualTable.Item_Code     FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeFinalQeryItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'FinalQry.'+ VirtualTable.Item_Code+'_Free' FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strAllFinalQeryItemCode As String = ""
        If clsCommon.myLen(strFinalQeryItemCode) Then
            strAllFinalQeryItemCode = strFinalQeryItemCode
        Else
            strAllFinalQeryItemCode = ""
        End If
        If clsCommon.myLen(strAllFinalQeryItemCode) > 0 AndAlso clsCommon.myLen(strFreeFinalQeryItemCode) > 0 Then
            strAllFinalQeryItemCode = strAllFinalQeryItemCode + "," + strFreeFinalQeryItemCode
        ElseIf clsCommon.myLen(strAllFinalQeryItemCode) <= 0 AndAlso clsCommon.myLen(strFreeFinalQeryItemCode) > 0 Then
            strAllFinalQeryItemCode = strFreeFinalQeryItemCode
        End If

        '=============================================================================
        Dim strVarcharNullItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'convert (varchar,isnull( ' + VirtualTable.Item_Code+ ',0)) as ' + VirtualTable.Item_Code FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeVarcharNullItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'convert (varchar,isnull( ' + VirtualTable.Item_Code+'_Free' + ',0)) as ' + VirtualTable.Item_Code+'_Free'  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strAllVarcharNullItemCode As String = ""
        If clsCommon.myLen(strVarcharNullItemCode) > 0 Then
            strAllVarcharNullItemCode = strVarcharNullItemCode
        Else
            strAllVarcharNullItemCode = ""
        End If
        If clsCommon.myLen(strAllVarcharNullItemCode) > 0 AndAlso clsCommon.myLen(strFreeVarcharNullItemCode) > 0 Then
            strAllVarcharNullItemCode = strAllVarcharNullItemCode + "," + strFreeVarcharNullItemCode
        ElseIf clsCommon.myLen(strAllVarcharNullItemCode) <= 0 AndAlso clsCommon.myLen(strFreeVarcharNullItemCode) > 0 Then
            strAllVarcharNullItemCode = strAllVarcharNullItemCode
        End If
        '==============================================================================
        Dim strMaxFinalItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'max (Final . ' + VirtualTable.Item_Code+ ') as ' + VirtualTable.Item_Code FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeMaxFinalItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'max (Final .' + VirtualTable.Item_Code+'_Free' + ') as ' + VirtualTable.Item_Code+'_Free'  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strAllMaxFinalItemCode As String = ""
        If clsCommon.myLen(strMaxFinalItemCode) > 0 Then
            strAllMaxFinalItemCode = strMaxFinalItemCode
        Else
            strAllMaxFinalItemCode = ""
        End If
        If clsCommon.myLen(strAllMaxFinalItemCode) > 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCode) > 0 Then
            strAllMaxFinalItemCode = strAllMaxFinalItemCode + " ," + strFreeMaxFinalItemCode
        ElseIf clsCommon.myLen(strAllMaxFinalItemCode) <= 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCode) > 0 Then
            strAllMaxFinalItemCode = strFreeMaxFinalItemCode
        End If
        '===============================================================================
        Dim strItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','   + VirtualTable.Item_Code FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +  VirtualTable.Item_Code+'_Free'   FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllItemCode As String = ""
        If clsCommon.myLen(strItemCode) > 0 Then
            strAllItemCode = strItemCode
        Else
            strAllItemCode = ""
        End If
        If clsCommon.myLen(strAllItemCode) > 0 AndAlso clsCommon.myLen(strFreeItemCode) > 0 Then
            strAllItemCode = strAllItemCode + " , " + strFreeItemCode
        ElseIf clsCommon.myLen(strAllItemCode) <= 0 AndAlso clsCommon.myLen(strFreeItemCode) > 0 Then
            strAllItemCode = strFreeItemCode
        End If
        '===============================================================================
        Dim strAvgItmecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ',' +'convert (varchar,cast ((isnull( '+ VirtualTable.Item_Code +',0)/3) as decimal (10,2))) as ' + VirtualTable.Item_Code  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code     FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeAvgItmecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'convert (varchar,cast ((isnull( '+  VirtualTable.Item_Code+'_Free' +',0)/3) as decimal (10,2))) as ' + VirtualTable.Item_Code + '_Free'  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllAvgItmecode As String = ""
        If clsCommon.myLen(strAvgItmecode) > 0 Then
            strAllAvgItmecode = strAvgItmecode
        Else
            strAllAvgItmecode = ""
        End If
        If clsCommon.myLen(strAllAvgItmecode) > 0 AndAlso clsCommon.myLen(strFreeAvgItmecode) > 0 Then
            strAllAvgItmecode = strAllAvgItmecode + " , " + strFreeAvgItmecode
        ElseIf clsCommon.myLen(strAllAvgItmecode) <= 0 AndAlso clsCommon.myLen(strFreeAvgItmecode) > 0 Then
            strAllAvgItmecode = strFreeAvgItmecode
        End If
        '===============================================================================
        Dim strAvgItmecode1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ',' +'convert (varchar,cast ((isnull( '+ VirtualTable.Item_Code +',0)) as decimal (10,2))) as ' + VirtualTable.Item_Code  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS  Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeAvgItmecode1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'convert (varchar,cast ((isnull( '+  VirtualTable.Item_Code+'_Free' +',0)) as decimal (10,2))) as ' + VirtualTable.Item_Code + '_Free'  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllAvgItmecode1 As String = ""
        If clsCommon.myLen(strAvgItmecode1) > 0 Then
            strAllAvgItmecode1 = strAvgItmecode1
        Else
            strAllAvgItmecode1 = ""
        End If
        If clsCommon.myLen(strAllAvgItmecode1) > 0 AndAlso clsCommon.myLen(strFreeAvgItmecode1) > 0 Then
            strAllAvgItmecode1 = strAllAvgItmecode1 + " , " + strFreeAvgItmecode1
        ElseIf clsCommon.myLen(strAllAvgItmecode1) <= 0 AndAlso clsCommon.myLen(strFreeAvgItmecode1) > 0 Then
            strAllAvgItmecode1 = strFreeAvgItmecode1
        End If

        '=====================Total========================================================
        Dim strVarcharNullItemCodeTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+'  +' convert (decimal(10,2),isnull( ' + VirtualTable.Item_Code+ ',0)) '  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0'  ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeVarcharNullItemCodeTotalFree As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  '+'  + ' convert (decimal(10,2),isnull( ' + VirtualTable.Item_Code+'_Free' + ',0)) '   FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' ) as VirtualTable   order by VirtualTable.Item_Code     FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
        Dim strTotal As String = "0"
        Dim strTotalFree As String = "0"

        If clsCommon.myLen(strVarcharNullItemCodeTotal) > 0 Then
            strTotal = " Cast(( " + strVarcharNullItemCodeTotal + " ) as Varchar)"
        End If
        If clsCommon.myLen(strFreeVarcharNullItemCodeTotalFree) > 0 Then
            strTotalFree = " Cast(( " + strFreeVarcharNullItemCodeTotalFree + " ) as Varchar)"
        End If
        '==============================================================================
        ' ======================================Avg/3===================================
        Dim strMaxFinalItemCodeAvg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +'convert (varchar,Cast(( sum (  isnull(convert (decimal(10,2),Final . ' + VirtualTable.Item_Code+ '),0))/3) as decimal(10,2)) ) as ' + VirtualTable.Item_Code FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and  TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code     FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   "))
        Dim strFreeMaxFinalItemCodeAvg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  + 'convert (varchar,Cast(( sum (  isnull(convert (decimal(10,2),Final . ' + VirtualTable.Item_Code+'_Free' + '),0))/3) as decimal(10,2)) ) as ' + VirtualTable.Item_Code+'_Free'  FROM (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and  TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllMaxFinalItemCodeAvg As String = ""
        If clsCommon.myLen(strMaxFinalItemCodeAvg) > 0 Then
            strAllMaxFinalItemCodeAvg = strMaxFinalItemCodeAvg
        Else
            strAllMaxFinalItemCodeAvg = ""
        End If
        If clsCommon.myLen(strAllMaxFinalItemCodeAvg) > 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCodeAvg) > 0 Then
            strAllMaxFinalItemCodeAvg = strAllMaxFinalItemCodeAvg + " ," + strFreeMaxFinalItemCodeAvg
        ElseIf clsCommon.myLen(strAllMaxFinalItemCodeAvg) <= 0 AndAlso clsCommon.myLen(strFreeMaxFinalItemCodeAvg) > 0 Then
            strAllMaxFinalItemCodeAvg = strFreeMaxFinalItemCodeAvg
        End If

        '===============================================================================
        Dim strAvgItmecode3 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ',' +' '+ VirtualTable.Item_Code +' as ' + VirtualTable.Item_Code  FROM  (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' ) as VirtualTable   order by VirtualTable.Item_Code    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strFreeAvgItmecode3 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT  ','  +' '+  VirtualTable.Item_Code+'_Free' +' as ' + VirtualTable.Item_Code + '_Free'  FROM  (	Select distinct TSPL_ITEM_PROJECTION_DETAILS.Item_Code from TSPL_ITEM_PROJECTION_DETAILS Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code where (Item_Code !='' or Item_Code is null) and TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'  and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1'  ) as VirtualTable   order by VirtualTable.Item_Code   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
        Dim strAllAvgItmecode3 As String = ""
        If clsCommon.myLen(strAvgItmecode3) > 0 Then
            strAllAvgItmecode3 = strAvgItmecode3
        Else
            strAllAvgItmecode3 = ""
        End If
        If clsCommon.myLen(strAllAvgItmecode3) > 0 AndAlso clsCommon.myLen(strFreeAvgItmecode3) > 0 Then
            strAllAvgItmecode3 = strAllAvgItmecode3 + " , " + strFreeAvgItmecode3
        ElseIf clsCommon.myLen(strAllAvgItmecode3) <= 0 AndAlso clsCommon.myLen(strFreeAvgItmecode3) > 0 Then
            strAllAvgItmecode3 = strFreeAvgItmecode3
        End If
        '================================================================================
        'Dim Qry As String = "  select FinalQry.SNo ,FinalQry.Document_Date, " + strAllFinalQeryItemCode + "  from (  " & _
        '                            "  select 2 as SNo, Document_Date, " + strAllVarcharNullItemCode + " from (  " & _
        '                            "  select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                            "  Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                            "  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                            "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                            "  where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' " & _
        '                            "  Union all " & _
        '                            "  select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                            "  Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                            "  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " & _
        '                            "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                            "  where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' " & _
        '                            "  ) As SourceTable " & _
        '                            "  Pivot " & _
        '                            "    (  " & _
        '                            "  sum(Qty) " & _
        '                            "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                            "  ) AS PivotTable " & _
        '                    "  Union All " & _
        '                    " select  Final.SNo ,max(Final.Document_Date) as Document_Date, " + strAllMaxFinalItemCode + " from ( " & _
        '                           " select 3 as SNo, 'AVERAGE' as  Document_Date, " + strAllAvgItmecode + " from ( " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' " & _
        '                           " Union all " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , TSPL_SD_SALE_INVOICE_DETAIL.Qty  from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' " & _
        '                           " ) As SourceTable " & _
        '                           " PIVOT " & _
        '                           "  ( " & _
        '                           "   sum(Qty) " & _
        '                           "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNo " & _
        '                    " Union All " & _
        '                    " select  Final.SNo ,max(Final.Document_Date) as Document_Date, " + strAllMaxFinalItemCode + " from ( " & _
        '                           " select 4 as SNo, ' PROJECTION for " + strProjectionDate + "' as  Document_Date, " + strAllAvgItmecode + " from ( " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103)   and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N'" & _
        '                           " Union all " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103)   and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' " & _
        '                           " ) As SourceTable " & _
        '                           "  Pivot " & _
        '                           "   ( " & _
        '                           "  sum(Qty) " & _
        '                           " FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNo " & _
        '                    " Union All " & _
        '                    " select  Final.SNO ,max(Final.Document_Date) as Document_Date ," + strAllMaxFinalItemCode + " from ( " & _
        '                           " select 0 as SNO, 'Item Desc' as Document_Date , " + strAllItemCode + "  from ( " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,  TSPL_ITEM_MASTER .Item_Desc as Item_Desc from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' " & _
        '                           " Union all " & _
        '                           " select convert(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' ,   TSPL_ITEM_MASTER .Item_Desc as Item_Desc from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' " & _
        '                           " ) As SourceTable " & _
        '                           " Pivot " & _
        '                           " ( " & _
        '                           " max(Item_Desc) " & _
        '                           " FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNO  " & _
        '                     " Union All " & _
        '                     " select  Final.SNO ,max(Final.Document_Date) as Document_Date , " + strAllMaxFinalItemCode + " from (  " & _
        '                           " select SNO , Document_Date ," + strAllItemCode + " from ( " & _
        '                           " select  1 as SNO , 'Default UOM'  as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_UOM_DETAIL.UOM_Code  from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM = 1  " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' " & _
        '                           " Union all " & _
        '                           " select 1 as SNO , 'Default UOM'  as Document_Date  ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code+'_Free' , TSPL_ITEM_UOM_DETAIL.UOM_Code  from TSPL_SD_SALE_INVOICE_DETAIL " & _
        '                           " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM = 1  " & _
        '                           " where convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= dateAdd(day,-3 , convert(date,'" + strProjectionDate + "',103)) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < convert (date,'" + strProjectionDate + "',103) and  TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' " & _
        '                           " ) As SourceTable " & _
        '                           " Pivot " & _
        '                           " ( " & _
        '                           "  max(UOM_Code) " & _
        '                           "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNO  " & _
        '                           " ) FinalQry order by FinalQry.SNO "

        'Dim Qry As String = "  select FinalQry.SNo ,FinalQry.Document_Date, " + strAllFinalQeryItemCode + " ,FinalQry.Total,FinalQry.Total_Free  from (  " & _
        '                            "  select 2 as SNo, Document_Date, " + strAllVarcharNullItemCode + " , " + strTotal + " as Total , " + strTotalFree + " as Total_Free from (  " & _
        '                            "  select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code , TSPL_ITEM_PROJECTION_DETAILS.Item_Qty as Qty from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                            "  Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                            "  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                            "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                            "  where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' " & _
        '                            "  Union all " & _
        '                            "  select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code+'_Free' , TSPL_ITEM_PROJECTION_DETAILS.Item_Qty as Qty from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                            "  Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                            "  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code  " & _
        '                            "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                            "  where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                            "  ) As SourceTable " & _
        '                            "  Pivot " & _
        '                            "    (  " & _
        '                            "  sum(Qty) " & _
        '                            "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                            "  ) AS PivotTable " & _
        '                    "  Union All " & _
        '                    " select  Final.SNo ,max(Final.Document_Date) as Document_Date, " + strAllMaxFinalItemCodeAvg + " , '' as Total , '' as Total_Free from ( " & _
        '                           " select 3 as SNo, 'AVERAGE' as  Document_Date, " + strAllAvgItmecode3 + " from ( " & _
        '                           " select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code , TSPL_ITEM_PROJECTION_DETAILS.Item_Qty as Qty from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code  " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' " & _
        '                           " Union all " & _
        '                           " select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code+'_Free' , TSPL_ITEM_PROJECTION_DETAILS.Item_Qty as Qty  from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " ) As SourceTable " & _
        '                           " PIVOT " & _
        '                           "  ( " & _
        '                           "   sum(Qty) " & _
        '                           "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNo " & _
        '                    " Union All " & _
        '                    " select  Final.SNo ,max(Final.Document_Date) as Document_Date, " + strAllMaxFinalItemCode + " , '' as Total , '' as Total_Free from ( " & _
        '                           " select 4 as SNo, ' PROJECTION for " + strProjectionDate + "' as  Document_Date, " + strAllAvgItmecode1 + " from ( " & _
        '                           " select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code , TSPL_ITEM_PROJECTION_DETAILS.Projection_Average as Qty from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'   and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0'" & _
        '                           " Union all " & _
        '                           " select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code+'_Free' , TSPL_ITEM_PROJECTION_DETAILS.Projection_Average as Qty from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "'   and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " ) As SourceTable " & _
        '                           "  Pivot " & _
        '                           "   ( " & _
        '                           "  sum(Qty) " & _
        '                           " FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNo " & _
        '                    " Union All " & _
        '                    " select  Final.SNO ,max(Final.Document_Date) as Document_Date ," + strAllMaxFinalItemCode + " , '' as Total , '' as Total_Free from ( " & _
        '                           " select 0 as SNO, 'Item Desc' as Document_Date , " + strAllItemCode + "  from ( " & _
        '                           " select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code ,  TSPL_ITEM_MASTER .Item_Desc as Item_Desc from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' " & _
        '                           " Union all " & _
        '                           " select TSPL_ITEM_PROJECTION_DETAILS.Doc_Date as Document_Date,TSPL_ITEM_PROJECTION_DETAILS.Item_Code+'_Free' ,   TSPL_ITEM_MASTER .Item_Desc as Item_Desc from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " ) As SourceTable " & _
        '                           " Pivot " & _
        '                           " ( " & _
        '                           " max(Item_Desc) " & _
        '                           " FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNO  " & _
        '                     " Union All " & _
        '                     " select  Final.SNO ,max(Final.Document_Date) as Document_Date , " + strAllMaxFinalItemCode + " , '' as Total , '' as Total_Free from (  " & _
        '                           " select SNO , Document_Date ," + strAllItemCode + " from ( " & _
        '                           " select  1 as SNO , 'Default UOM'  as Document_Date ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code , TSPL_ITEM_UOM_DETAIL.UOM_Code  from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM = 1  " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '0' " & _
        '                           " Union all " & _
        '                           " select 1 as SNO , 'Default UOM'  as Document_Date  ,TSPL_ITEM_PROJECTION_DETAILS.Item_Code+'_Free' , TSPL_ITEM_UOM_DETAIL.UOM_Code  from TSPL_ITEM_PROJECTION_DETAILS " & _
        '                           " Left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_HEAD.Projection_Code = TSPL_ITEM_PROJECTION_DETAILS.Projection_Code " & _
        '                           " left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code " & _
        '                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM = 1  " & _
        '                           " where TSPL_ITEM_PROJECTION_HEAD.Projection_Code ='" + strProjectionCode + "' and  TSPL_ITEM_PROJECTION_DETAILS.Is_Item_Free = '1' " & _
        '                           " ) As SourceTable " & _
        '                           " Pivot " & _
        '                           " ( " & _
        '                           "  max(UOM_Code) " & _
        '                           "  FOR Item_Code IN (" + strAllPivotItemCode + ") " & _
        '                           " ) AS PivotTable " & _
        '                           " ) Final group by Final.SNO  " & _
        '                           " ) FinalQry order by FinalQry.SNO "

        Dim qry As String = " select *,Tollence as Tolerance,Actual_Projection as [Actual Projection]  from (select TSPL_ITEM_PROJECTION_DETAILS.Item_Code as [Item Code],TSPL_ITEM_PROJECTION_DETAILS.Item_Desc as [Description]"
        qry += " ,TSPL_ITEM_PROJECTION_DETAILS.UOM as UOM"
        qry += " ,TSPL_ITEM_PROJECTION_DETAILS.Doc_Date,TSPL_ITEM_PROJECTION_DETAILS.Item_Qty,Tollence,Actual_Projection from TSPL_ITEM_PROJECTION_DETAILS "
        qry += " left outer join TSPL_ITEM_PROJECTION_HEAD on TSPL_ITEM_PROJECTION_DETAILS.Projection_Code=TSPL_ITEM_PROJECTION_HEAD.Projection_Code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PROJECTION_DETAILS.Item_Code"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_PROJECTION_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'"
        qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Default_UOM = 1 ) as Defalt_SU on TSPL_ITEM_PROJECTION_DETAILS.Item_Code=Defalt_SU.Item_Code"
        qry += " where TSPL_ITEM_PROJECTION_DETAILS.Projection_Code='" & txtDocNo.Value & "'"
        qry += " )"
        qry += " as xxx "
        qry += " pivot(sum(item_qty) for Doc_Date in (" + strPivotDocumentDate + ")) as pvt "

        Return Qry
    End Function

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        'gv.ReadOnly = True

        'For ii As Integer = 0 To gv.Columns.Count - 1
        '    For rr As Integer = 0 To gv.Rows.Count - 1
        '        gv.Rows(rr).Cells(ii).ReadOnly = True
        '    Next
        'Next
        Dim avgProjRow As Integer = gv.Rows.Count - 1
        For ii As Integer = 2 To gv.Columns.Count - 1
            gv.Rows(avgProjRow).Cells(ii).ReadOnly = False
        Next
        
        
        'Dim rowCount As Integer = gv.Rows.Count
        'Dim avgRow As Integer = gv.Rows.Count - 2
        'Dim avgProjRow As Integer = gv.Rows.Count - 1

        'For ii As Integer = 2 To gv.Columns.Count - 1
        '    For rr As Integer = 2 To gv.Rows.Count - 3
        '        gv.Rows(rr).Cells(ii).ReadOnly = True
        '        gv.Rows(rr).Cells(1).ReadOnly = True
        '        gv.Rows(avgProjRow).Cells(ii).ReadOnly = False
        '    Next
        'Next
        'gv.Columns(0).ReadOnly = True
        'gv.Columns(1).ReadOnly = True
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Projection_Code as Code, convert (varchar,Projection_Date,103) as Date, case when  Post_Status =1 then 'Posted' else 'Not Posted' end as Status  from TSPL_ITEM_PROJECTION_HEAD "
        LoadData(clsCommon.ShowSelectForm("ItemProjectionFinder", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        If e.Column Is gv.Columns("Tolerance") Then
            Dim strProjectionDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Value), "dd/MM/yyyy")
            Dim Projectionforcurrrentdate As String = "Projection for currrent date " + strProjectionDate
            If btnSave.Text = "Update" Then
                gv.CurrentRow.Cells("Actual Projection").Value = clsCommon.myCdbl(gv.CurrentRow.Cells("Tolerance").Value) + clsCommon.myCdbl(gv.CurrentRow.Cells(strProjectionDate).Value)
            Else
                gv.CurrentRow.Cells("Actual Projection").Value = clsCommon.myCdbl(gv.CurrentRow.Cells("Tolerance").Value) + clsCommon.myCdbl(gv.CurrentRow.Cells(Projectionforcurrrentdate).Value)
            End If

        End If
    End Sub
End Class

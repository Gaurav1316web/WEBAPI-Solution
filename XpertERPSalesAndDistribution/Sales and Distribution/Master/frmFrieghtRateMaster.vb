Imports common
Public Class frmFrieghtRateMaster
    Inherits FrmMainTranScreen

    Private Sub frmFrieghtRateMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim coll As Dictionary(Of String, String)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        'coll.Add("From_Date", "datetime Not null")
        'coll.Add("To_Date", "datetime null")
        'coll.Add("Description", "varchar(100) null")
        'coll.Add("Inactive", "integer Not null")
        'coll.Add("Inactive_By", "varchar(50) Not null")
        'coll.Add("Location_Code", "varchar(50) null references TSPL_LOCATION_MASTER(Location_Code)")
        'coll.Add("Inactive_Date", "datetime  Not NULL")
        'coll.Add("Created_By", "varchar(50)  Not NULL")
        'coll.Add("Created_Date", "datetime  Not NULL")
        'coll.Add("Modify_By", "varchar(12)  Not NULL")
        'coll.Add("Modify_Date", "datetime  Not NULL")
        'clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_DCS_FOR_SALE_Frieght", coll, "", True)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("REF_PK_ID", "integer NOT NULL references TSPL_DCS_FOR_SALE_Frieght(PK_ID)")
        'coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        'coll.Add("Customer_Code", "varchar(50) null references TSPL_CUSTOMER_MASTER(Cust_Code)")
        'coll.Add("Zone_Code", "varchar(50) null references TSPL_ZONE_MASTER(Zone_Code)")
        'coll.Add("UOM_Code", "varchar(50) null references TSPL_ZONE_MASTER(Zone_Code)")
        'coll.Add("Frieght_Rate", "Decimal(18,2) Not null")
        'clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_DCS_FOR_SALE_Frieght_Detail", coll, "", True)


    End Sub
End Class
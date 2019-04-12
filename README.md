# WebForms++

## For when you have to use WebForms instead of MVC

## 1. Download the project from this GitHub site and unzip

## Prepare the Data and then you can delete everything under App_Data

2.  Unzip App_Data\WebForms++DDL.zip
3.  Open Microsoft SQL Server Management Studio (SSMS you can download this for free)
4.  Open .\sqlexpress (you can download this for free)
5.  Create a new database named WEBFORMSPLUSPLUS
6.  Open WebForms++DDL.sql in SSMS and run it
7.  Right click WEBFORMSPLUSPLUS->Tasks->Import Data... Next Choose Flat File Source Browse...
select bzSODtoImport.csv Next Next Destination is SQL Server Native Client 11.0
8.  Change file name Destination from [dbo].[bzSODtoImport] to [dbo].[bzSOD] Next Next Finish
9.  Set primary key in bzSOD to SalesOrderDetailID
10.  Open the capture.png and change the design of bzSOD to match set Identity on SalesOrderDetailID
11.  run this 'update  [WEBFORMSPLUSPLUS].[dbo].[bzSOD] set ForeignToNames = 2'
12.  Do each field at a time and save; do the line total field last
13.  Set line total to computed column specification (isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))
14.  Open WebForms++DDL2.sql and runit it.  Ignore errors for now, just make sure we a pk to fk between two tables
Happy Programming.  I will do updates to this github project to make it easier; especially with data.

## Modify the web.config Connections if necessary

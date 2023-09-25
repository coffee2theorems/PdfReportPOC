IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'PdfReportPOC')
	BEGIN
    CREATE DATABASE PdfReportPOC;
    END
    GO
    USE PdfReportPOC;
    GO;

Create table EasyData
(
    EasyDataId INT IDENTITY(1,1) NOT NULL,
    DataName varchar(200) NOT NULL,
    DataValueInt INT NOT NULL,
    CONSTRAINT PK_EasyData PRIMARY KEY CLUSTERED (EasyDataId)
);

Create Table GraphData
(
    GraphDataId INT IDENTITY(1,1) NOT NULL,
    GraphName varchar(200) NOT NULL,
    DataName VARCHAR(200) NOT NULL,
    DataValue DECIMAL NOT NULL,
    CONSTRAINT PK_GraphData PRIMARY KEY CLUSTERED (GraphDataId)
);

INSERT INTO EasyData (DataName, DataValueInt) VALUES
    ('Total Users', 321), 
    ('Total Support Emails', 542), 
    ('Total Support Calls', 258), 
    ('Total Unique Products', 15);

INSERT INTO GraphData (GraphName, DataName, DataValue) VALUES
    ('Revenue By Product', 'Widget A', 120.99), 
    ('Revenue By Product', 'Widget B', 220.49), 
    ('Revenue By Product', 'Widget C', 87.23), 
    ('Revenue By Product', 'Widget D', 140.00), 
    ('Revenue By Product', 'Widget E', 120.99),
    ('User Role Count', 'Admin', 20), 
    ('User Role Count', 'Premium User', 42), 
    ('User Role Count', 'Basic Users', 114);
-- Created by Redgate Data Modeler (https://datamodeler.redgate-platform.com)
-- Last modification date: 2026-05-08 09:31:22.319

-- tables
-- Table: ComponentManufacturers
CREATE TABLE ComponentManufacturers (
    Id int  NOT NULL,
    Abbreviation nvarchar(30)  NOT NULL,
    FullName nvarchar(300)  NOT NULL,
    FoundationDate date  NOT NULL,
    CONSTRAINT ComponentManufacturers_pk PRIMARY KEY  (Id)
);

-- Table: ComponentTypes
CREATE TABLE ComponentTypes (
    Id int  NOT NULL,
    Abbreviation nvarchar(30)  NOT NULL,
    Name nvarchar(150)  NOT NULL,
    CONSTRAINT ComponentTypes_pk PRIMARY KEY  (Id)
);

-- Table: Components
CREATE TABLE Components (
    Code char(10)  NOT NULL,
    Name nvarchar(300)  NOT NULL,
    Description nvarchar(max)  NOT NULL,
    ComponentManufacturersId int  NOT NULL,
    ComponentTypesId int  NOT NULL,
    CONSTRAINT Components_pk PRIMARY KEY  (Code)
);

-- Table: PCComponents
CREATE TABLE PCComponents (
    PCId int  NOT NULL,
    ComponentCode char(10)  NOT NULL,
    Amount int  NOT NULL,
    CONSTRAINT PCComponents_pk PRIMARY KEY  (PCId,ComponentCode)
);

-- Table: PCs
CREATE TABLE PCs (
    Id int  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    Weight float(5)  NOT NULL,
    Warranty int  NOT NULL,
    CreatedAt datetime  NOT NULL,
    Stock int  NOT NULL,
    CONSTRAINT PCs_pk PRIMARY KEY  (Id)
);

-- foreign keys
-- Reference: Components_ComponentManufacturers (table: Components)
ALTER TABLE Components ADD CONSTRAINT Components_ComponentManufacturers
    FOREIGN KEY (ComponentManufacturersId)
    REFERENCES ComponentManufacturers (Id);

-- Reference: Components_ComponentTypes (table: Components)
ALTER TABLE Components ADD CONSTRAINT Components_ComponentTypes
    FOREIGN KEY (ComponentTypesId)
    REFERENCES ComponentTypes (Id);

-- Reference: PCComponents_Components (table: PCComponents)
ALTER TABLE PCComponents ADD CONSTRAINT PCComponents_Components
    FOREIGN KEY (ComponentCode)
    REFERENCES Components (Code);

-- Reference: PCComponents_PCs (table: PCComponents)
ALTER TABLE PCComponents ADD CONSTRAINT PCComponents_PCs
    FOREIGN KEY (PCId)
    REFERENCES PCs (Id);

-- End of file.


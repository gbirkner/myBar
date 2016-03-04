

CREATE TABLE ProductTypes (
	ProductTypeID int NOT NULL IDENTITY,
	Title			nvarchar(128) NOT NULL,
	[Description]	nvarchar(512) NULL,
	Margin			float NOT NULL DEFAULT 0,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_ProductTypes PRIMARY KEY (ProductTypeID),
	CONSTRAINT fk_ProductTypesCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_ProductTypesModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);


CREATE TABLE ProductStates(
	ProductStateID int NOT NULL IDENTITY,
	Title			nvarchar(128) NOT NULL,
	[Description]	nvarchar(512) NULL,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_ProductStates PRIMARY KEY (ProductStateID),
	CONSTRAINT fk_ProductStatesCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_ProductStatesModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);


CREATE TABLE Places (
	PlaceID int NOT NULL IDENTITY,
	Title			nvarchar(128) NOT NULL,
	[Description]	nvarchar(512) NULL,
	Capacity		int NOT NULL,
	smoking			bit,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_Places PRIMARY KEY (PlaceID),
	CONSTRAINT fk_PlacesCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_PlacesModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);


CREATE TABLE Units (
	UnitID			int NOT NULL IDENTITY,
	Title			nvarchar(128) NOT NULL,
	[Description]	nvarchar(512) NULL,
	BaseFactor		float NOT NULL DEFAULT 1,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_Units PRIMARY KEY (UnitID),
	CONSTRAINT fk_UnitsCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_UnitsModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);

CREATE TABLE OrderStates (
	OrderStateID int NOT NULL IDENTITY,
	Title			nvarchar(128) NOT NULL,
	[Description]	nvarchar(512) NULL,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_OrderStates PRIMARY KEY (OrderStateID),
	CONSTRAINT fk_OrderStatesCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_OrderStatesModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)

);

CREATE TABLE Invoices (
	InvoiceID		int NOT NULL IDENTITY,
	InvoiceDate		datetime NOT NULL,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_Invoices PRIMARY KEY (InvoiceID),
	CONSTRAINT fk_InvoicesCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_InvoicesModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);


CREATE TABLE InvoicePos (
	InvoiceID		int NOT NULL,
	PositionNr		int NOT NULL,
	Price			float NOT NULL DEFAULT 0,
	Amount			int NOT NULL DEFAULT 1,
	ProductID		int NOT NULL,
	ProdTitle		nvarchar(128),
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_InvoicePos PRIMARY KEY (InvoiceID, PositionNr),
	CONSTRAINT fk_InvoicePosCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_InvoicePosModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);


CREATE TABLE Products (
	ProductID		int NOT NULL IDENTITY,
	Title			nvarchar(128) NOT NULL,
	[Description]	nvarchar(512) NOT NULL,
	Picture			varbinary(MAX) NULL,
	DayExpire		int NOT NULL DEFAULT 0,
	StockUnitID		int NOT NULL,
	ProductTypeID	int NOT NULL,
	ProductStateID	int NOT NULL,
	Margin			float NOT NULL DEFAULT 0,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_Products PRIMARY KEY (ProductID),
	CONSTRAINT fk_ProductsStockUnitID FOREIGN KEY (StockUnitID) REFERENCES Units(UnitID),
	CONSTRAINT fk_ProductsProductTypeID FOREIGN KEY (ProductTypeID) REFERENCES ProductTypes(ProductTypeID),
	CONSTRAINT fk_ProductsProductStateID FOREIGN KEY (ProductStateID) REFERENCES ProductStates(ProductStateID),
	CONSTRAINT fk_ProductsCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_ProductsModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);


CREATE TABLE Orders (
	OrderID			int NOT NULL IDENTITY,
	Notes			nvarchar(512) NULL,
	Price			float NOT NULL,
	PlaceID			int NOT NULL,
	ProductID		int NOT NULL,
	OrderStateID	int NOT NULL,
	UnitID			int NOT NULL,
	InvoiceID		int NULL,
	InvoicePos		int NULL,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_Orders PRIMARY KEY (OrderID),
	CONSTRAINT fk_OrdersPlaceID FOREIGN KEY (PlaceID) REFERENCES Places(PlaceID),
	CONSTRAINT fk_OrdersProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	CONSTRAINT fk_OrdersOrderStateID FOREIGN KEY (OrderStateID) REFERENCES OrderStates(OrderStateID),
	CONSTRAINT fk_OrdersUnitID FOREIGN KEY (UnitID) REFERENCES Units(UnitID),
	CONSTRAINT fk_OrdersInvoicePosID FOREIGN KEY (InvoiceID, InvoicePos) REFERENCES InvoicePos(InvoiceID, PositionNr),
	CONSTRAINT fk_OrdersCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_OrdersModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);

CREATE TABLE ProductServings (
	ProductID		int NOT NULL,
	UnitID			int NOT NULL,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_ProductServings PRIMARY KEY (ProductID, UnitID),
	CONSTRAINT fk_ProductServingsProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	CONSTRAINT fk_ProductServingsUnitID FOREIGN KEY (UnitID) REFERENCES Units(UnitID),
	CONSTRAINT fk_ProductServingsCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_ProductServingsModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);

CREATE TABLE Stocks (
	StockID			int NOT NULL IDENTITY,
	ProductID		int NOT NULL,
	Amount			int NOT NULL DEFAULT 1,
	Expires			smalldatetime NOT NULL,
	Delivered		smalldatetime NOT NULL,
	PPrice			float NOT NULL DEFAULT 0,
	CreatedBy		nvarchar(128) NOT NULL,
	CreatedDate		datetime,
	ModifiedBy		nvarchar(128) NULL,
	ModifiedDate	datetime,

	CONSTRAINT pk_Stocks PRIMARY KEY (StockID),
	CONSTRAINT fk_StocksProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	CONSTRAINT fk_StocksCreatedBy FOREIGN KEY (CreatedBy) REFERENCES AspNetUsers(Id),
	CONSTRAINT fk_StocksModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES AspNetUsers(Id)
);
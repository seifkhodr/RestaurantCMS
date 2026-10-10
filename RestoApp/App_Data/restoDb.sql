-- 1. Users Table
CREATE TABLE Users (
    UserId          INT IDENTITY(1,1) PRIMARY KEY,
    FullName        NVARCHAR(100)   NOT NULL,
    Email           NVARCHAR(150)   NOT NULL UNIQUE,
    PasswordHash    NVARCHAR(256)   NOT NULL,
    Phone           NVARCHAR(20)    NULL,
    Role            NVARCHAR(20)    NOT NULL DEFAULT 'Client',
    IsEmailVerified BIT             NOT NULL DEFAULT 0,
    VerificationToken NVARCHAR(256) NULL,
    ResetToken      NVARCHAR(256)   NULL,
    ResetTokenExpiry DATETIME       NULL,
    VerificationExpiry DATETIME     NULL,
    AuthToken       NVARCHAR(256)   NULL,
    AuthTokenExpiry DATETIME        NULL,
    CreatedAt       DATETIME        NOT NULL DEFAULT GETDATE(),
    IsActive        BIT             NOT NULL DEFAULT 1
);

-- 2. Menus Table
CREATE TABLE Menus (
    MenuId      INT IDENTITY(1,1) PRIMARY KEY,
    MenuName    NVARCHAR(100)   NOT NULL,
    Description NVARCHAR(500)   NULL,
    DisplayOrder INT            NOT NULL DEFAULT 0,
    IsActive    BIT             NOT NULL DEFAULT 1,
    CreatedAt   DATETIME        NOT NULL DEFAULT GETDATE()
);

-- 3. RestaurantTables Table
CREATE TABLE RestaurantTables (
    TableId         INT IDENTITY(1,1) PRIMARY KEY,
    TableNumber     NVARCHAR(50)    NOT NULL UNIQUE, 
    SeatingCapacity INT             NOT NULL,       
    Location        NVARCHAR(100)   NULL,            
    PhotoPath       NVARCHAR(500)   NULL,            
    IsActive        BIT             NOT NULL DEFAULT 1
);

-- 4. Items Table
CREATE TABLE Items (
    ItemId      INT IDENTITY(1,1) PRIMARY KEY,
    MenuId      INT             NOT NULL,
    ItemName    NVARCHAR(150)   NOT NULL,
    Description NVARCHAR(1000)  NULL,
    Price       DECIMAL(10,2)   NOT NULL,
    Currency    NVARCHAR(10)    NOT NULL DEFAULT '$',
    Ingredients NVARCHAR(2000)  NULL,
    Origin      NVARCHAR(500)   NULL,
    IsAvailable BIT             NOT NULL DEFAULT 1,
    CreatedAt   DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Items_Menus FOREIGN KEY (MenuId) REFERENCES Menus(MenuId) ON DELETE CASCADE
);

-- 5. ItemPhotos Table
CREATE TABLE ItemPhotos (
    PhotoId     INT IDENTITY(1,1) PRIMARY KEY,
    ItemId      INT             NOT NULL,
    PhotoPath   NVARCHAR(500)   NOT NULL,
    IsPrimary   BIT             NOT NULL DEFAULT 0,
    UploadedAt  DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ItemPhotos_Items FOREIGN KEY (ItemId) REFERENCES Items(ItemId) ON DELETE CASCADE
);

-- 6. Reservations Table 
CREATE TABLE Reservations (
    ReservationId   INT IDENTITY(1,1) PRIMARY KEY,
    UserId          INT             NULL,
    TableId         INT             NULL,
    GuestName       NVARCHAR(100)   NULL,
    GuestEmail      NVARCHAR(150)   NULL,
    GuestPhone      NVARCHAR(20)    NULL,
    ReservationDate DATETIME        NOT NULL,
    EndTime         DATETIME        NULL,
    NumberOfGuests  INT             NOT NULL,
    Status          NVARCHAR(20)    NOT NULL DEFAULT 'Pending',
    Notes           NVARCHAR(500)   NULL,
    CreatedAt       DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Reservations_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE SET NULL,
    CONSTRAINT FK_Reservations_Tables FOREIGN KEY (TableId) REFERENCES RestaurantTables(TableId) ON DELETE SET NULL
);

-- 7. Feedbacks Table (Anyone can review, ReservationId is completely optional)
CREATE TABLE Feedbacks (
    FeedbackId    INT IDENTITY(1,1) PRIMARY KEY,
    UserId        INT             NULL,  -- NULL means it was an anonymous guest
    ReservationId INT             NULL,  -- NULL means they didn't link it to a specific booking
    GuestName     NVARCHAR(100)   NULL,
    Comment       NVARCHAR(2000)  NOT NULL,
    VisitRating   INT             NOT NULL CHECK (VisitRating BETWEEN 1 AND 5),
    CreatedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    IsApproved    BIT             NOT NULL DEFAULT 0,    -- Great for moderating spam before it shows on the site!
    CONSTRAINT FK_Feedbacks_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE SET NULL,
    CONSTRAINT FK_Feedbacks_Reservations FOREIGN KEY (ReservationId) REFERENCES Reservations(ReservationId) ON DELETE SET NULL
);

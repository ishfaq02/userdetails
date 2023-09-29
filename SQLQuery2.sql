CREATE TABLE userDetails (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255),
    email NVARCHAR(255),
    address NVARCHAR(255),
    description TEXT
);

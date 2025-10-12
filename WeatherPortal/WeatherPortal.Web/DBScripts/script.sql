
select * from aspnetusers;
select * from AspNetRoles;
--insert into AspNetRoles values(1,'Admin','Admin',getdate());
select * from AspNetUserRoles;
--insert into AspNetUserRoles values('id or admin user',1);
--insert into AspNetUserRoles values('ccc72695-95a5-41a3-8889-477799d505f1',2);

CREATE TABLE Regions (
    Id CHAR(36) PRIMARY KEY,
    RegionNameInMyanmar NVARCHAR(Max),
    RegionNameInEnglish NVARCHAR(Max),
    RegionType CHAR(1),
    Code INT NOT NULL,
    IsActive bit NOT NULL default(1),
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NULL
);

CREATE TABLE Cities (
    Id CHAR(36) PRIMARY KEY,
    CityNameInMyanmar NVARCHAR(100),
    CityNameInEnglish NVARCHAR(100),
    IsActive bit  NOT NULL default(1),
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NULL,
    RegionId CHAR(36) NOT NULL,
    FOREIGN KEY (RegionId) REFERENCES Regions(Id)
);

CREATE TABLE Townships (
    Id CHAR(36) PRIMARY KEY,
    TownshipNameInMyanmar NVARCHAR(100),
    TownshipNameInEnglish NVARCHAR(100),
    IsActive bit NOT NULL default(1),
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NULL,
    CityId CHAR(36) NOT NULL,
    FOREIGN KEY (CityId) REFERENCES Cities(Id)
);


CREATE TABLE WeatherStations (
    Id CHAR(36) PRIMARY KEY,
    StationName NVARCHAR(100),
    TownshipId  CHAR(36),
    Latitude DECIMAL(10,6),
    Longitude DECIMAL(10,6),
    IsActive bit NOT NULL default(1),
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NULL,
    CityId CHAR(36) NOT NULL,
    FOREIGN KEY (TownshipId) REFERENCES Townships(Id),
    FOREIGN KEY (CityId ) REFERENCES Cities(Id)
);

CREATE TABLE WeatherReadings (
    Id CHAR(36) PRIMARY KEY,
    StationId  CHAR(36) NOT NULL,
    WhenReadAt DATETIME,
    TemperatureMax DECIMAL(5,2) NULL,
    TemperatureMin DECIMAL(5,2) NULL,
    Pressure DECIMAL(6,2) NULL,
    Humidity DECIMAL(5,2) NULL,
    WindSpeed DECIMAL(5,2) NULL,
    WindDirection NVARCHAR(10) NULL,
    Rainfall DECIMAL(5,2) NULL,
    PresentWeather VARCHAR(100) NULL,
    SeaWeather NVARCHAR(100) NULL,
    IsActive bit NOT NULL default(1),
    CreatedAt DateTime NOT NULL,
    UpdatedAt DateTime NULL,
    FOREIGN KEY (StationId) REFERENCES WeatherStations(Id)
);
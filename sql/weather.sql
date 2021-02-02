create database Weather
go

use Weather
go

create schema stage
go

create table stage.Condition
(
	Id smallint not null primary key,
	Description nvarchar(1024) not null,
	Title nvarchar(255) not null,
	Icon nvarchar(50) not null
)
go

create table stage.Location
(
	Id bigint not null
			primary key nonclustered,
	RegionId int not null,
	Name nvarchar(255) not null,
	Latitude float not null,
	Longitude float not null
)
go

create table dbo.Region
(
	RegionId int identity
		constraint Region_pk
			primary key,
	Name nvarchar(255) not null
)
go

insert into dbo.Region(Name)
values ('Iowa')
go

create table dbo.Location
(
	Id bigint not null
		constraint Location_pk
			primary key nonclustered,
	RegionId int not null,
	Name nvarchar(255) not null,
	Latitude float not null,
	Longitude float not null
)
go

alter table dbo.Location
	with check add constraint FK_Location_Region_RegionId
		foreign key (RegionId) references dbo.Region(RegionId)
go

alter table dbo.Location
    check constraint FK_Location_Region_RegionId
go

create table dbo.Condition
(
	Id smallint not null
		constraint Condition_pk
			primary key,
	Description nvarchar(1024) not null,
	Title nvarchar(255) not null,
	Icon nvarchar(50) not null
)
go

create table dbo.AggregateType(
    Id smallint not null primary key,
	Name nvarchar(25) not null,
)
go

insert into dbo.AggregateType(Name, Id)
values
        ('Temperature',1)
       ,('FeelsLikeTemperature',2)
       ,('PercentCloudiness',3)
       ,('Pressure',4)
       ,('Humidity',5)
       ,('Rain',6)
       ,('Snow',7)
       ,('WindSpeed',8)
       ,('Visibility',9)
go

create table dbo.Aggregate
(
	Id uniqueidentifier not null primary key,
	RegionId int not null,
	AggregateTypeId smallint not null,
	MinimumMeasurement float,
	AverageMeasurement float,
	MaximumMeasurement float,
	RecordCount int not null,
    Timestamp bigint not null
)
go

alter table dbo.Aggregate
	with check add constraint FK_Aggregate_Region_RegionId
		foreign key (RegionId) references dbo.Region(RegionId)
go

alter table dbo.Aggregate
    check constraint FK_Aggregate_Region_RegionId
go

alter table dbo.Aggregate
	with check add constraint FK_Aggregate_AggregateType_AggregateTypeId
		foreign key (AggregateTypeId) references dbo.AggregateType(Id)
go

alter table dbo.Aggregate
    check constraint FK_Aggregate_AggregateType_AggregateTypeId
go

create table dbo.Weather
(
	Id uniqueidentifier not null
		constraint Weather_pk
			primary key nonclustered,
	LocationId bigint not null,
	Timestamp bigint not null,
	PercentCloudiness smallint,
	Temperature float,
	FeelsLikeTemperature float,
	MinimumTemperature float,
	MaximumTemperature float,
	Pressure smallint,
	Humidity smallint,
	RainInPastHour float,
	RainInPastThreeHours float,
	SnowInPastHour float,
	SnowInPastThreeHours float,
	WindSpeed float,
	WindDirectionDegrees smallint,
	Visibility int,
	Sunrise bigint not null,
	Sunset bigint not null,
	TimezoneOffset int not null
)
go

alter table dbo.Weather
	with check add constraint FK_Weather_Location_LocationId
		foreign key (LocationId) references dbo.Location(Id)
go

alter table dbo.Weather
    check constraint FK_Weather_Location_LocationId
go

create table dbo.WeatherCondition
(
	WeatherId uniqueidentifier not null,
	ConditionId smallint not null,
	constraint PK_WeatherCondition PRIMARY KEY (WeatherId, ConditionId)
)
go

alter table dbo.WeatherCondition
	with check add constraint FK_WeatherCondition_Weather_WeatherId
		foreign key (WeatherId) references dbo.Weather(Id)
go

alter table dbo.WeatherCondition
    check constraint FK_WeatherCondition_Weather_WeatherId
go

alter table dbo.WeatherCondition
	with check add constraint FK_WeatherCondition_Condition_ConditionId
		foreign key (ConditionId) references dbo.Condition(Id)
go

alter table dbo.WeatherCondition
    check constraint FK_WeatherCondition_Condition_ConditionId
go

CREATE PROCEDURE dbo.ImportConditions
AS
SET XACT_ABORT, NOCOUNT ON
BEGIN TRY
    BEGIN TRANSACTION;

        -- updates
        UPDATE dest
        SET Description = src.Description,
            Title = src.Title,
            Icon = src.Icon
        FROM stage.Condition src
        INNER JOIN dbo.Condition dest on src.Id = dest.Id

        -- inserts
        INSERT INTO dbo.Condition(Id, Description, Title, Icon)
        SELECT src.Id, src.Description, src.Title, src.Icon
        FROM stage.Condition src
        LEFT OUTER JOIN dbo.Condition dest on src.Id = dest.Id
        WHERE dest.Id IS NULL

        -- clear table
        TRUNCATE TABLE stage.Condition;

    COMMIT TRANSACTION;

END TRY
BEGIN CATCH
    IF @@trancount > 0 ROLLBACK TRANSACTION
    DECLARE @msg NVARCHAR(2048) = ERROR_MESSAGE()
    RAISERROR (@msg, 16, 1)
END CATCH;
go

CREATE PROCEDURE dbo.ImportLocations
AS
SET XACT_ABORT, NOCOUNT ON
BEGIN TRY
    BEGIN TRANSACTION;

        -- inserts
        INSERT INTO dbo.Location(Id, RegionId, Name, Latitude, Longitude)
        SELECT src.Id, src.RegionId, src.Name, src.Latitude, src.Longitude
        FROM stage.Location src
        LEFT OUTER JOIN dbo.Location dest on src.Id = dest.Id
        WHERE dest.Id IS NULL

        -- clear table
        TRUNCATE TABLE stage.Location;

    COMMIT TRANSACTION;

END TRY
BEGIN CATCH
    IF @@trancount > 0 ROLLBACK TRANSACTION
    DECLARE @msg NVARCHAR(2048) = ERROR_MESSAGE()
    RAISERROR (@msg, 16, 1)
END CATCH;
go

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

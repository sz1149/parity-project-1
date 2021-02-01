create table dbo.WeatherCondition
(
	WeatherId uniqueidentifier not null,
	ConditionId smallint not null,
	constraint PK_WeatherCondition PRIMARY KEY (WeatherId, ConditionId)
)
go

alter table dbo.WeatherCondition
	add constraint FK_WeatherCondition_Weather_WeatherId
		foreign key (WeatherId) references dbo.Weather(Id)
go

alter table dbo.WeatherCondition
	add constraint FK_WeatherCondition_Condition_ConditionId
		foreign key (ConditionId) references dbo.Condition(Id)
go

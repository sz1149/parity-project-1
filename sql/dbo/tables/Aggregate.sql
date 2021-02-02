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
	add constraint FK_Aggregate_Region_RegionId
		foreign key (RegionId) references dbo.Region(RegionId)
go

alter table dbo.Aggregate
	add constraint FK_Aggregate_AggregateType_AggregateTypeId
		foreign key (AggregateTypeId) references dbo.AggregateType(Id)
go

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

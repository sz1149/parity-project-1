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
	add constraint FK_Location_Region_RegionId
		foreign key (RegionId) references dbo.Region(RegionId)
go

/*testing insert*/
insert into dbo.Location(Id, RegionId, Name, Latitude, Longitude)
values (1, 1, 'testName', 41.6005, -93.6091)
go

select *
from dbo.Location
go

delete from dbo.Location
where Id = 1
go

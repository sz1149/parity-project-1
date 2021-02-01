create table staging.Location
(
	Id bigint not null
			primary key nonclustered,
	RegionId int not null,
	Name nvarchar(255) not null,
	Latitude float not null,
	Longitude float not null
)
go

/*testing insert*/
insert into staging.Location(Id, RegionId, Name, Latitude, Longitude)
values (1, 1, 'testName', 41.6005, -93.6091)
go

select *
from staging.Location
go

delete from staging.Location
where Id = 1
go

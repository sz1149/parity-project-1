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

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

select *
from dbo.Region
go

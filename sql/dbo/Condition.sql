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

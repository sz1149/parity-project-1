create table staging.Condition
(
	Id smallint not null primary key,
	Description nvarchar(1024) not null,
	Title nvarchar(255) not null,
	Icon nvarchar(50) not null
)
go


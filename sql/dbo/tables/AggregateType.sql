create table dbo.AggregateType(
    Id smallint not null primary key,
	Name nvarchar(25) not null,
)
go

insert into dbo.AggregateType(Name, Id)
values
        ('Temperature',1)
       ,('FeelsLikeTemperature',2)
       ,('PercentCloudiness',3)
       ,('Pressure',4)
       ,('Humidity',5)
       ,('Rain',6)
       ,('Snow',7)
       ,('WindSpeed',8)
       ,('Visibility',9)
go

select *
from dbo.AggregateType
go

if exists(select * from sys.databases where name = 'Billing')
drop database Billing
create database Billing
go

use Billing
go

if exists(select * from information_schema.tables where table_name = 'contract')
	drop table contract
create table contract
(
	contract int identity(1, 1) primary key,
	client int,
	status int
)

if exists(select * from information_schema.tables where table_name = 'status')
	drop table status
create table status
(
	status int identity(1, 1) primary key,
	name varchar(max)
)

if exists(select * from information_schema.tables where table_name = 'favour')
	drop table favour
create table favour
(
	favour int identity(1, 1) primary key,
	name varchar(max)
)

if exists(select * from information_schema.tables where table_name = 'contract_favour')
	drop table contract_favour
create table contract_favour
(
	contract_favour int identity(1, 1) primary key,
	contract int,
	favour int
)
go

if exists(select * from information_schema.tables where table_name = 'user')
	drop table [user]
create table [user]
(
	[user] int identity(1, 1) primary key,
	email varchar(max),
	name varchar(max),
	password varchar(max),
	contacts varchar(max),
	role varchar(max)
)
go

-- добавление менеджера
insert [user] (email, name, password, contacts, role)
values('m@m', 'Steve Bulman', 'm', '88007555032', 'Manager')

-- SELECT * FROM [user]

-- грфик исполнения услуги
-- сторону с нашей стороны добавить

-- сделать роли менеджер и клиент

-- поиск сделать
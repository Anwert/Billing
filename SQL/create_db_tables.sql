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
	manager int,
	client int,
	favour int,
	status int
)

if exists(select * from information_schema.tables where table_name = 'favour')
	drop table favour
create table favour
(
	favour int identity(1, 1) primary key,
	name nvarchar(max)
)

if exists(select * from information_schema.tables where table_name = 'status')
	drop table status
create table status
(
	status int identity(1, 1) primary key,
	name nvarchar(max)
)

if exists(select * from information_schema.tables where table_name = 'user')
	drop table [user]
create table [user]
(
	[user] int identity(1, 1) primary key,
	name nvarchar(max),
	password nvarchar(max),
	contacts nvarchar(max),
	role nvarchar(max)
)
go
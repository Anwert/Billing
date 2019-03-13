if exists(select * from sys.databases where name = 'Testing')
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

if exists(select * from information_schema.tables where table_name = 'client')
drop table client
create table client
(
	client int identity(1, 1) primary key,
	name varchar(max),
	email varchar(max),
	address varchar(max),
	phone varchar(max)
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
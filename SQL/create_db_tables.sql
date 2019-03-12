create database Billing
go

use Billing
go

create table contract
(
	contract int identity(1, 1) primary key,
	client int,
	status int
)

create table client
(
	client int identity(1, 1) primary key,
	name varchar(max)
)

create table status
(
	status int identity(1, 1) primary key,
	name varchar(max)
)

create table favour
(
	favour int identity(1, 1) primary key,
	name varchar(max)
)


create table contract_favour
(
	contract_favour int identity(1, 1) primary key,
	contract int,
	favour int
)
go
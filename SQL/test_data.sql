use Billing
go

declare @favour_id int,
	@status_id int,
	@client_id int,
	@manager_id int

-- добавление менеджера
insert [user] (name, password, contacts, role)
values('m', 'm', '88007555032', 'Manager')
select @manager_id = scope_identity()

-- добавление договора
insert favour (name)
values ('favour1')
select @favour_id = scope_identity()

insert status (name)
values ('Обрабатывается')
select @status_id = scope_identity()

insert [user] (name, password, contacts, role)
values ('client1', 'client1', 'client contact 1', 'Client')
select @client_id = scope_identity()

insert contract (manager, client, favour, [status])
values (@manager_id, @client_id, @favour_id, @status_id)

-- добавление договора
insert favour (name)
values ('favour2')
select @favour_id = scope_identity()

insert status (name)
values ('Обслуживается')
select @status_id = scope_identity()

insert [user] (name, password, contacts, role)
values ('client2', 'client2', 'client contact 2', 'Client')
select @client_id = scope_identity()

insert contract (manager, client, favour, [status])
values (@manager_id, @client_id, @favour_id, @status_id)

insert status (name)
values ('Расторгнут')
select @status_id = scope_identity()
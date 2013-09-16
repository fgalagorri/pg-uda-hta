-- User with password 'password'
insert into user(`login`, `password`, `rol`, `name`)
values ('test', '5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8', '', 'Federico Galagorri');


insert into drugtype(`type`)
values ('Antihipertensivos'),
		('Betabloqueantes'),
		('Calcio-antagonistas'),
		('Diuréticos'),
		('Inhibidores');
		
		
INSERT INTO `udahta_db`.`drug` (`name`, `active`, `drugtype_idDrugType`)
VALUES ('Nisirol', 'Lisinopril', 1),
		('Medicamento1', 'Activo1', 2),
		('Medicamento1', 'Activo2', 3),


insert into limitmeasure(`maxdiasday`,`maxdiasnight`,`maxdiastotal`,
						 `maxsysday`,`maxsysnight`,`maxsystotal`)
values (300, 300, 300, 300, 300, 300);

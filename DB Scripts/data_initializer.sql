-- User with password 'password'
insert into user(`login`, `password`, `rol`, `name`, `enabled`)
values ('admin', '5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8', 'Administrador', 'Administrador', 1);


insert into drugtype(`type`)
values ('Antihipertensivos'),
		('Betabloqueantes'),
		('Calcio-antagonistas'),
		('Diuréticos'),
		('Inhibidores');
		
		
INSERT INTO `udahta_db`.`drug` (`name`, `active`, `drugtype_idDrugType`)
VALUES ('Nisirol', 'Lisinopril', 1);


insert into limitmeasure(`maxdiasday`,`maxdiasnight`,`maxdiastotal`, `maxsysday`,`maxsysnight`,
						`maxsystotal`, `highsystotal`, `highsysday`, `highsysnight`, 
						`highdiastotal`, `highdiasday`, `highdiasnight`)
values (300, 300, 300, 300, 300, 300,  130, 135, 120,  80, 85, 75);

DELIMITER $$
DROP PROCEDURE IF EXISTS insertPatient$$
CREATE PROCEDURE insertPatient(OUT id BIGINT, IN idInDev BIGINT, IN name VARCHAR(45), IN surname VARCHAR(45), IN addr TEXT, IN dni VARCHAR(45), IN birth DATETIME, IN sex ENUM('F','M'), IN neighbour VARCHAR(45), IN city VARCHAR(45), IN phone VARCHAR(45), IN cell VARCHAR(45), IN email VARCHAR(45), IN register_number BIGINT)
BEGIN
INSERT INTO `patient_info_db`.`patient`(`patientReference`, `name`, `surname`, `document`, `gender`, `telephone`, `cell_phone`, `address`, `city`, `neighborhood`, `birthday`, `e_mail`)
VALUES (idInDev, name, surname, dni, sex, phone, cell, addr, city, neighbour, birth, email);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertPatientUda$$
CREATE PROCEDURE insertPatientUda(IN id BIGINT)
BEGIN
INSERT INTO `udahta_db`.`patientuda`(`idPatientUda`)
VALUES (id);
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertReport$$
CREATE PROCEDURE insertReport(OUT id BIGINT, IN begin_date DATETIME, IN end_date DATETIME, IN doctor VARCHAR(45), IN diagnosis TEXT, IN request_doctor VARCHAR (45), IN specialty VARCHAR(45), IN day_avg_sys INT, IN night_avg_sys INT, IN total_avg_sys INT, IN day_max_sys INT, IN night_max_sys INT, IN day_avg_dias INT, IN night_avg_dias INT, total_avg_dias INT, IN day_max_dias INT, IN night_max_dias INT, IN idDev INT, IN devReportId INT, IN idTemporaryData INT, IN idDailyCarnet BIGINT, IN idPatient BIGINT)
BEGIN
INSERT INTO `udahta_db`.`report` (`begin_date`, `end_date`, `doctor`, `diagnosis`, `request_doctor`, `specialty`, `day_avg_sys`, `night_avg_sys`, `total_avg_sys`, `day_max_sys`, `night_max_sys`, `day_avg_dias`, `night_avg_dias`, `total_avg_dias`, `day_max_dias`, `night_max_dias`, `idDevice`, `deviceReportId`, `temporarydata_idTemporaryData`, `dailycarnet_idDailyCarnet`, `patientuda_idPatientUda`) 
VALUES (begin_date, end_date, doctor, diagnosis, request_doctor, specialty, day_avg_sys, night_avg_sys, total_avg_sys, day_max_sys, night_max_sys, day_avg_dias, night_avg_dias, total_avg_dias, day_max_dias, night_max_dias, idDev, devReportId, idTemporaryData, idDailyCarnet, idPatient);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertMeasurement$$
CREATE PROCEDURE insertMeasurement(IN dateM DATETIME, IN systolic INT, IN average INT, IN diastolic INT, IN heart_rate INT, IN sleep BIT, IN comm TEXT, IN idReport BIGINT, IN idPatient BIGINT)
BEGIN
INSERT INTO `udahta_db`.`measurement` (`date`, `systolic`, `average`, `diastolic`, `heart_rate`, `sleep`, `comment`, `report_idReport`, `report_patientuda_idPatientUda`) 
VALUES (dateM, systolic, average, diastolic, heart_rate, sleep, comm, idReport, idPatient);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertDailyCarnet$$
CREATE PROCEDURE insertDailyCarnet(OUT id BIGINT, IN technical VARCHAR(45), IN initial_dias1 INT, IN initial_dias2 INT, IN initial_dias3 INT, IN initial_hr1 INT, IN initial_hr2 INT, IN initial_hr3 INT, IN final_dias1 INT, IN final_dias2 INT, IN final_dias3 INT, IN final_hr1 INT, IN final_hr2 INT, IN final_hr3 INT, IN begin_sleep_time DATETIME, IN end_sleep_time DATETIME, IN how_sleep VARCHAR(45), IN main_meal_time DATETIME, IN init_sys1 INT, IN init_sys2 INT, IN init_sys3 INT, IN final_sys1 INT, IN final_sys2 INT, IN final_sys3 INT)
BEGIN
INSERT INTO `udahta_db`.`dailycarnet` (`technical`, `initial_dias1`, `initial_dias2`, `initial_dias3`, `initial_hr1`, `initial_hr2`, `initial_hr3`, `final_dias1`, `final_dias2`, `final_dias3`, `final_hr1`, `final_hr2`, `final_hr3`, `begin_sleep_time`, `end_sleep_time`, `how_sleep`, `main_meal_time`, `init_sys1`, `init_sys2`, `init_sys3`, `final_sys1`, `final_sys2`, `final_sys3`) 
VALUES (technical, initial_dias1, initial_dias2, initial_dias3, initial_hr1, initial_hr2, initial_hr3, final_dias1, final_dias2, final_dias3, final_hr1, final_hr2, final_hr3, begin_sleep_time, end_sleep_time, how_sleep, main_meal_time, init_sys1, init_sys2, init_sys3, final_sys1, final_sys2, final_sys3);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertTemporaryData$$
CREATE PROCEDURE insertTemporaryData(OUT id INT, IN weight DECIMAL(2), IN height DECIMAL(2), IN age INT, IN body_mass_index DECIMAL(2), IN smoker BIT, IN dyslipidemia BIT, IN diabetic BIT, IN known_hypertensive BIT, IN fat_percentage DECIMAL(2), IN muscle_percentage DECIMAL(2), IN kcal INT)
BEGIN
INSERT INTO `udahta_db`.`temporarydata` (`weight`, `height`, `age`, `body_mass_index`, `smoker`, `dyslipidemia`, `diabetic`, `known_hypertensive`, `fat_percentage`, `muscle_percentage`, `kcal`) 
VALUES (weight, height, age, body_mass_index, smoker, dyslipidemia, diabetic, known_hypertensive, fat_percentage, muscle_percentage, kcal);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertUser$$
CREATE PROCEDURE insertUser(IN id INT, IN log VARCHAR(45), IN p VARCHAR(45), IN r VARCHAR(45))
BEGIN
INSERT INTO `udahta_db`.`user`(`idUsuario`, `login`, `pass`, `rol`)
VALUES(id, log, p, r);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updatePassword$$
CREATE PROCEDURE updatePassword(IN login_var VARCHAR(45), IN pass_var VARCHAR(45))
BEGIN
UPDATE `udahta_db`.`user`
SET pass = pass_var
WHERE login = login_var;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertDrugType$$
CREATE PROCEDURE insertDrugType(IN typ VARCHAR(45))
BEGIN
INSERT INTO `udahta_db`.`drugtype`(`type`)
VALUES(typ);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertDrug$$
CREATE PROCEDURE insertDrug(IN nam VARCHAR(45), IN idDrugType INT)
BEGIN
INSERT INTO `udahta_db`.`drug`(`name`, `drugtype_idDrugType`)
VALUES(nam, idDrugType);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertInvestigation$$
CREATE PROCEDURE insertInvestigation(IN id INT, IN nam VARCHAR(45), IN createDat DATETIME)
BEGIN
INSERT 
INTO `udahta_db`.`investigation`(idInvestigation, name, creation_date)
VALUES(id, nam, createDat);
END$$
DELIMITER ;

SELECT * FROM User;

SELECT * FROM DrugType;

SELECT * FROM Drug;

SELECT * FROM Investigation;

SELECT * FROM Report;

SELECT * FROM DailyCarnet;

SELECT * FROM TemporaryData;

SELECT * FROM Patientuda;

SELECT * FROM Measurement;

SET foreign_key_checks = 0;
TRUNCATE DailyCarnet;
TRUNCATE TemporaryData;
TRUNCATE Report;
TRUNCATE Measurement;
TRUNCATE PatientUda;
SET foreign_key_checks = 1;
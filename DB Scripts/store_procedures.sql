DELIMITER $$
DROP PROCEDURE IF EXISTS insertPatient$$
CREATE PROCEDURE insertPatient(IN idInDev INT, IN name VARCHAR(45), IN surname VARCHAR(45), IN addr VARCHAR(45), IN dni VARCHAR(45), IN birth DATETIME, IN sex ENUM('F','M'), IN neighbour VARCHAR(45), IN city VARCHAR(45), IN phone VARCHAR(45), IN cell VARCHAR(45), IN email VARCHAR(45))
BEGIN
INSERT INTO `patient_info_db`.`Patient`(`patientReference`, `name`, `surname`, `document`, `gender`, `telephone`, `cell_phone`, `address`, `city`, `neighborhood`, `birthday`, `e_mail`)
VALUES (idInDev, name, surname, dni, sex, phone, cell, addr, city, neighbour, birth, email);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertReport$$
CREATE PROCEDURE insertReport(OUT id BIGINT, IN begin_date DATETIME, IN end_date DATETIME, IN doctor VARCHAR(45), IN diagnosis TEXT, IN request_doctor VARCHAR (45), IN specialty VARCHAR(45), IN day_avg_sys INT, IN night_avg_sys INT, IN total_avg_sys INT, IN day_max_sys INT, IN night_max_sys INT, IN day_avg_dias INT, IN night_avg_dias INT, total_avg_dias INT, IN day_max_dias INT, IN night_max_dias INT, IN idDev INT, IN devReportId INT, IN idTemporaryData INT, IN idDailyCarnet INT, IN idPatient INT)
BEGIN
INSERT INTO `udahta_db`.`Report` (`begin_date`, `end_date`, `doctor`, `diagnosis`, `request_doctor`, `specialty`, `day_avg_sys`, `night_avg_sys`, `total_avg_sys`, `day_max_sys`, `night_max_sys`, `day_avg_dias`, `night_avg_dias`, `total_avg_dias`, `day_max_dias`, `night_max_dias`, `idDevice`, `deviceReportId`, `TemporaryData_idTemporaryData`, `DailyCarnet_idDailyCarnet`, `Patient_idPatient`) 
VALUES (begin_date, end_date, doctor, diagnosis, request_doctor, specialty, day_avg_sys, night_avg_sys, total_avg_sys, day_max_sys, night_max_sys, day_avg_dias, night_avg_dias, total_avg_dias, day_max_dias, night_max_dias, idDev, devReportId, idTemporaryData, idDailyCarnet, idPatient);
SET id = (SELECT LastInsertedId());
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertMeasurement$$
CREATE PROCEDURE insertMeasurement(IN dateM DATETIME, IN systolic INT, IN average INT, IN diastolic INT, IN heart_rate INT, IN sleep BIT, IN idReport BIGINT, IN idPatient INT)
BEGIN
INSERT INTO `udahta_db`.`Measurement` (`date`, `systolic`, `average`, `diastolic`, `heart_rate`, `sleep`, `Report_idReport`, `Report_Patient_idPatient`) 
VALUES (dateM, systolic, average, diastolic, heart_rate, sleep, idReport, idPatient);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertDailyCarnet$$
CREATE PROCEDURE insertDailyCarnet(IN technical, IN initial_bp1, IN initial_bp2, IN initial_bp3, IN initial_hr1, IN initial_hr2, IN initial_hr3, IN final_bp1, IN final_bp2, IN final_bp3, IN final_hr1, IN final_hr2, IN final_hr3, IN begin_sleep_time, IN end_sleep_time, IN how_sleep, IN main_meal_time)
BEGIN
INSERT INTO `udahta_db`.`DailyCarnet` (`technical`, `initial_bp1`, `initial_bp2`, `initial_bp3`, `initial_hr1`, `initial_hr2`, `initial_hr3`, `final_bp1`, `final_bp2`, `final_bp3`, `final_hr1`, `final_hr2`, `final_hr3`, `begin_sleep_time`, `end_sleep_time`, `how_sleep`, `main_meal_time`) 
VALUES (technical, initial_bp1, initial_bp2, initial_bp3, initial_hr1, initial_hr2, initial_hr3, final_bp1, final_bp2, final_bp3, final_hr1, final_hr2, final_hr3, begin_sleep_time, end_sleep_time, how_sleep, main_meal_time);
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertUser$$
CREATE PROCEDURE insertUser(IN id INT, IN log VARCHAR(45), IN p VARCHAR(45), IN r VARCHAR(45))
BEGIN
INSERT INTO `udahta_db`.`User`(`idUsuario`, `login`, `pass`, `rol`)
VALUES(id, log, p, r);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updatePassword$$
CREATE PROCEDURE updatePassword(IN login_var VARCHAR(45), IN pass_var VARCHAR(45))
BEGIN
UPDATE udahta_db.User
SET pass = pass_var
WHERE login = login_var;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertDrugType$$
CREATE PROCEDURE insertDrugType(IN typ VARCHAR(45))
BEGIN
INSERT INTO `udahta_db`.`DrugType`(`type`)
VALUES(typ);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertDrug$$
CREATE PROCEDURE insertDrug(IN nam VARCHAR(45), IN idDrugType INT)
BEGIN
INSERT INTO `udahta_db`.`Drug`(`name`, `DrugType_idDrugType`)
VALUES(nam, idDrugType);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertInvestigation$$
CREATE PROCEDURE insertInvestigation(IN id INT, IN nam VARCHAR(45), IN createDat DATETIME)
BEGIN
INSERT 
INTO udahta_db.Investigation(idInvestigation, name, creation_date)
VALUES(id, nam, createDat);
END$$
DELIMITER ;

SELECT * FROM User;

SELECT * FROM DrugType;

SELECT * FROM Drug;

SELECT * FROM Investigation;
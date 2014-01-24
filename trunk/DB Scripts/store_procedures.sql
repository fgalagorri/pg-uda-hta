-- -----------------------------------------------------
--   PATIENT_INFO_DB SCRIPTS
-- -----------------------------------------------------
USE `patient_info_db` ;

-- INSERTS --

DELIMITER $$
DROP PROCEDURE IF EXISTS insertPatient$$
CREATE PROCEDURE insertPatient(OUT id BIGINT, IN name VARCHAR(45), IN surname VARCHAR(45), 
							   IN addr TEXT, IN dni VARCHAR(45), IN birth DATETIME, 
							   IN sex ENUM('F','M'), IN neighbour VARCHAR(45), IN city VARCHAR(45), 
							   IN department VARCHAR(45), IN phone VARCHAR(45), IN cell VARCHAR(45), 
							   IN phone2 VARCHAR(45), IN email VARCHAR(45), 
							   IN register_number VARCHAR(45))
BEGIN
SET id = 0;
INSERT INTO `patient`(`name`, `surname`, `document`, `gender`, `telephone`, `cell_phone`, 
					  `telephone_alt`,`address`, `city`, `neighborhood`, `department`, `birthday`,
					  `e_mail`,`register_number`)
VALUES (name, surname, dni, sex, phone, cell, phone2, addr, city, neighbour, department, 
		birth, email, register_number);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertDeviceReference$$
CREATE PROCEDURE insertDeviceReference(OUT id BIGINT, IN device_type INT, IN device_ref VARCHAR(45),
									   IN idPatient BIGINT)
BEGIN
INSERT INTO `device_reference`(`device_type`, `device_ref`, `patient_idPatient`)
VALUES (device_type, device_ref, idPatient);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertEmergencyContact$$
CREATE PROCEDURE insertEmergencyContact(OUT id BIGINT, IN name VARCHAR(45), IN surname VARCHAR(45), 
										IN phone VARCHAR(45), IN idPatient BIGINT)
BEGIN
INSERT INTO `emergency_contact` (`name`, `surname`, `phone`, `patient_idPatient`) 
VALUES (name, surname, phone, idPatient);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;

-- DELETES --
DELIMITER $$
DROP PROCEDURE IF EXISTS deleteEmergencyContact$$
CREATE PROCEDURE deleteEmergencyContact(IN idEc BIGINT, IN idPatient BIGINT)
BEGIN
DELETE
FROM `emergency_contact`
WHERE (`idemergency_contact` = idEc AND
       `patient_idPatient` = idPatient);
END$$
DELIMITER ;

-- UPDATES --
DELIMITER $$
DROP PROCEDURE IF EXISTS editPatient$$
CREATE PROCEDURE editPatient(IN id BIGINT, IN name_ VARCHAR(45), IN surname VARCHAR(45), 
							 IN addr TEXT, IN dni VARCHAR(45), IN birth DATETIME, 
							 IN sex ENUM('F','M'), IN neighbour VARCHAR(45), IN city VARCHAR(45), 
							 IN department VARCHAR(45), IN phone VARCHAR(45), IN cell VARCHAR(45), 
							 IN phone2 VARCHAR(45), IN email VARCHAR(45), 
							 IN register_number VARCHAR(45))
BEGIN
UPDATE `patient`
SET `name` = name_, 
    `surname` = surname, 
	`document` = dni, 
	`gender`= sex, 
	`telephone` = phone, 
	`cell_phone` = cell, 
	`telephone_alt` = phone2,
	`address` = addr, 
	`city` = city, 
	`neighborhood` = neighbour, 
	`department` = department, 
	`birthday` = birth,
	`e_mail` = email,
	`register_number` = register_number
WHERE `idPatient` = id;
END$$
DELIMITER ;

-- -----------------------------------------------------
--   UDAHTA_DB SCRIPTS
-- -----------------------------------------------------
USE `udahta_db` ;

-- INSERTS --

DELIMITER $$
DROP PROCEDURE IF EXISTS insertPatientUda$$
CREATE PROCEDURE insertPatientUda(IN id BIGINT)
BEGIN
INSERT INTO `patientuda`(`idPatientUda`)
VALUES (id);
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertReport$$
CREATE PROCEDURE insertReport(OUT id BIGINT, IN begin_date DATETIME, IN end_date DATETIME, 
							  IN doctor VARCHAR(45), IN diagnosis TEXT, IN diagnosis_dt DATETIME,
							  IN requester VARCHAR (45), IN specialty VARCHAR(45), 
							  IN day_avg_sys INT, IN night_avg_sys INT, IN total_avg_sys INT, 
							  IN day_max_sys INT, IN night_max_sys INT, IN day_avg_dias INT, 
							  IN night_avg_dias INT, total_avg_dias INT, IN day_max_dias INT, 
							  IN night_max_dias INT, IN idDev INT, IN devReportId VARCHAR(45), 
							  IN idTemporaryData INT, IN idDailyCarnet BIGINT, IN idPatient BIGINT,
							  IN day_min_sis INT, IN day_min_dias INT, IN night_min_sis INT, 
							  IN night_min_dias INT, IN tot_avg_hr INT, IN day_avg_hr INT, 
							  IN night_avg_hr INT, IN max_day_hr INT, IN max_night_hr INT, 
							  IN min_day_hr INT, IN min_night_hr INT, IN tot_sd_sis DECIMAL(5,2), 
							  IN tot_sd_dias DECIMAL(5,2), IN day_sd_sis DECIMAL(5,2), 
							  IN day_sd_dias DECIMAL(5,2), IN night_sd_sis DECIMAL(5,2), 
							  IN night_sd_dias DECIMAL(5,2), IN tot_sd_tam DECIMAL(5,2), 
							  IN day_sd_tam DECIMAL(5,2), IN night_sd_tam DECIMAL(5,2), 
							  IN tot_sd_hr DECIMAL(5,2), IN day_sd_hr DECIMAL(5,2), 
							  IN night_sd_hr DECIMAL(5,2), IN tot_tam_avg INT, 
							  IN day_tam_avg INT, IN night_tam_avg INT,
							  IN n_sys_dipping DECIMAL(7,4), IN n_dias_dipping DECIMAL(7,4))
BEGIN
INSERT INTO `report` (`begin_date`, `end_date`, `doctor`, `diagnosis`, `diagnosis_date`, 
					  `requester`, `specialty`, `day_avg_sys`, `night_avg_sys`, 
					  `total_avg_sys`, `day_max_sys`, `night_max_sys`, `day_avg_dias`, 
					  `night_avg_dias`, `total_avg_dias`, `day_max_dias`, `night_max_dias`, 
					  `idDevice`, `deviceReportId`, `temporarydata_idTemporaryData`, 
					  `dailycarnet_idDailyCarnet`, `patientuda_idPatientUda`, `day_min_sis`, 
					  `day_min_dias`, `night_min_sis`, `night_min_dias`, `tot_avg_hr`, `day_avg_hr`,
					  `night_avg_hr`, `max_day_hr`, `max_night_hr`, `min_day_hr`, `min_night_hr`, 
					  `tot_sd_sis`, `tot_sd_dias`, `day_sd_sis`, `day_sd_dias`, `night_sd_sis`, 
					  `night_sd_dias`, `tot_sd_tam`,`day_sd_tam`, `night_sd_tam`, `tot_sd_hr`, 
					  `day_sd_hr`, `night_sd_hr`, `tot_tam_avg`, `day_tam_avg`, `night_tam_avg`,
					  `sys_dipping`, `dias_dipping`) 
VALUES (begin_date, end_date, doctor, diagnosis, diagnosis_dt, requester, specialty, 
		day_avg_sys, night_avg_sys, total_avg_sys, day_max_sys, night_max_sys, day_avg_dias, 
		night_avg_dias, total_avg_dias, day_max_dias, night_max_dias, idDev, devReportId, 
		idTemporaryData, idDailyCarnet, idPatient, day_min_sis, day_min_dias, night_min_sis, 
		night_min_dias, tot_avg_hr, day_avg_hr, night_avg_hr, max_day_hr, max_night_hr, min_day_hr, 
		min_night_hr, tot_sd_sis, tot_sd_dias, day_sd_sis, day_sd_dias, night_sd_sis, night_sd_dias,
		tot_sd_tam, day_sd_tam, night_sd_tam, tot_sd_hr, day_sd_hr, night_sd_hr, tot_tam_avg, 
		day_tam_avg, night_tam_avg, n_sys_dipping, n_dias_dipping);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertMeasurement$$
CREATE PROCEDURE insertMeasurement(OUT id BIGINT, IN dateM DATETIME, IN systolic INT, IN average INT, 
								   IN diastolic INT, IN heart_rate INT, IN sleep BIT, 
								   IN isValid BIT, IN isRetry BIT, IN isEnabled BIT, 
								   IN comm TEXT, IN idReport BIGINT, IN idPatient BIGINT)
BEGIN
INSERT INTO `measurement` (`date`, `systolic`, `average`, `diastolic`, `heart_rate`, `sleep`, 
						   `is_valid`, `is_retry`, `is_enabled`, `comment`, `report_idReport`, 
						   `report_patientuda_idPatientUda`) 
VALUES (dateM, systolic, average, diastolic, heart_rate, sleep, isValid, isRetry, isEnabled,
		comm, idReport, idPatient);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertDailyCarnet$$
CREATE PROCEDURE insertDailyCarnet(OUT id BIGINT, IN technical VARCHAR(45), IN initial_dias1 INT, 
								   IN initial_dias2 INT, IN initial_dias3 INT, IN initial_hr1 INT, 
								   IN initial_hr2 INT, IN initial_hr3 INT, IN final_dias1 INT, 
								   IN final_dias2 INT, IN final_dias3 INT, IN final_hr1 INT, 
								   IN final_hr2 INT, IN final_hr3 INT, IN begin_sleep_time DATETIME,
								   IN end_sleep_time DATETIME, IN how_sleep VARCHAR(45), 
								   IN sleep_comments TEXT, IN main_meal_time DATETIME, 
								   IN init_sys1 INT, IN init_sys2 INT, IN init_sys3 INT, 
								   IN final_sys1 INT, IN final_sys2 INT, IN final_sys3 INT)
BEGIN
INSERT INTO `dailycarnet` (`technical`, `initial_dias1`, `initial_dias2`, `initial_dias3`, 
						   `initial_hr1`, `initial_hr2`, `initial_hr3`, `final_dias1`, 
						   `final_dias2`, `final_dias3`, `final_hr1`, `final_hr2`, `final_hr3`, 
						   `begin_sleep_time`, `end_sleep_time`, `how_sleep`, `sleep_comments`, 
						   `main_meal_time`, `init_sys1`, `init_sys2`, `init_sys3`, `final_sys1`, 
						   `final_sys2`, `final_sys3`) 
VALUES (technical, initial_dias1, initial_dias2, initial_dias3, initial_hr1, initial_hr2, 
		initial_hr3, final_dias1, final_dias2, final_dias3, final_hr1, final_hr2, final_hr3, 
		begin_sleep_time, end_sleep_time, how_sleep, sleep_comments, main_meal_time, init_sys1, 
		init_sys2, init_sys3, final_sys1, final_sys2, final_sys3);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertComplications_Activities$$
CREATE PROCEDURE insertComplications_Activities(OUT id BIGINT, IN time_ DATETIME, 
												IN specification TEXT, IN idDailyCarnet BIGINT, 
												IN description TEXT)
BEGIN
INSERT INTO `complications_activities` (`time`, `specification`, 
										`dailycarnet_idDailyCarnet`, `description`)
VALUES (time_, specification, idDailyCarnet, description);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertTemporaryData$$
CREATE PROCEDURE insertTemporaryData(OUT id INT, IN weight DECIMAL(5,2), IN height DECIMAL(5,2), 
									 IN age INT, IN body_mass_index DECIMAL(5,2), IN smoker BIT, 
									 IN dyslipidemia BIT, IN diabetic BIT, 
									 IN known_hypertensive BIT, IN fat_percentage DECIMAL(5,2), 
									 IN muscle_percentage DECIMAL(5,2), IN kcal INT)
BEGIN
INSERT INTO `temporarydata` (`weight`, `height`, `age`, `body_mass_index`, `smoker`, 
							 `dyslipidemia`, `diabetic`, `known_hypertensive`, `fat_percentage`, 
							 `muscle_percentage`, `kcal`) 
VALUES (weight, height, age, body_mass_index, smoker, dyslipidemia, diabetic, known_hypertensive, 
		fat_percentage, muscle_percentage, kcal);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertMedicalHistory$$
CREATE PROCEDURE insertMedicalHistory(OUT id BIGINT, IN illness TEXT, IN comm TEXT, IN idPatientUda BIGINT)
BEGIN
INSERT INTO `medicalhistory` (`illness`, `comment`, `patientuda_idPatientUda`)
VALUES (illness, comm, idPatientUda);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertUser$$
CREATE PROCEDURE insertUser(OUT id INT, IN log VARCHAR(45), IN p TEXT, IN r VARCHAR(45), IN nam TEXT)
BEGIN
INSERT INTO `user`( `login`, `password`, `rol`,`name`)
VALUES(log, p, r, nam);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertDrugType$$
CREATE PROCEDURE insertDrugType(IN typ VARCHAR(45))
BEGIN
INSERT INTO `drugtype`(`type`)
VALUES(typ);
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertDrug$$
CREATE PROCEDURE insertDrug(IN nam VARCHAR(45), IN active VARCHAR(45), IN idDrugType INT)
BEGIN
INSERT INTO `drug`(`name`, `active`, `drugtype_idDrugType`)
VALUES(nam, active, idDrugType);
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertMedicineDose$$
CREATE PROCEDURE insertMedicineDose(OUT id INT, IN dose TEXT, IN time_ DATETIME, IN idDrug INT, IN idTemporaryData INT)
BEGIN
INSERT INTO `medicinedose` (`dose`, `time`, `drug_idDrug`, `temporarydata_idTemporaryData`)
VALUES(dose, time_, idDrug, idTemporaryData);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertInvestigation$$
CREATE PROCEDURE insertInvestigation(OUT id INT, IN nam VARCHAR(45), IN createDat DATETIME, IN comm TEXT)
BEGIN
INSERT 
INTO `investigation`(`name`, `creation_date`, `comment`)
VALUES(nam, createDat, comm);
SET id = (SELECT Last_Insert_Id());
END$$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS insertInvestigationHasReport$$
CREATE PROCEDURE insertInvestigationHasReport(IN idInvestigation INT, IN idReport BIGINT, 
											  IN idPatientUda BIGINT)
BEGIN
INSERT 
INTO `investigation_has_report` (`investigation_idInvestigation`, `report_idReport`, 
								 `report_patientuda_idPatientUda`) 
VALUES (idInvestigation, idReport, idPatientUda);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS insertLimitMeasures$$
CREATE PROCEDURE insertLimitMeasures(IN maxdiasday INT, IN maxdiasdayavg INT, IN maxdiasnight INT, 
									 IN maxdiasnightavg INT, IN maxdiastotal INT, IN maxsysday INT,
									 IN maxsysdayavg INT, IN maxsysnight INT, 
									 IN maxsysnightavg INT, IN maxsystotal INT)
BEGIN
INSERT 
INTO `udahta_db`.`limitmeasure` (`idlimitmeasure`, `maxdiasday`, `maxdiasdayavg`, `maxdiasnight`,
								 `maxdiasnightavg`, `maxdiastotal`, `maxsysday`, `maxsysdayavg`, 
								 `maxsysnight`, `maxsysnightavg`, `maxsystotal`) 
VALUES (maxdiasday, maxdiasdayavg, maxdiasnight, maxdiasnightavg, maxdiastotal, maxsysday, 
		maxsysdayavg, maxsysnight, maxsysnightavg, maxsystotal);
END$$
DELIMITER ;

-- DELETES --

DELIMITER $$
DROP PROCEDURE IF EXISTS deleteUser$$
CREATE PROCEDURE deleteUser(IN login VARCHAR(45))
BEGIN
DELETE 
FROM `user` 
WHERE (`login` = login);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS deleteInvestigationHasReport$$
CREATE PROCEDURE deleteInvestigationHasReport(IN idInvestigation INT, IN idReport BIGINT, 
											  IN idPatientUda BIGINT)
BEGIN
DELETE 
FROM `investigation_has_report` 
WHERE (`investigation_idInvestigation` = idInvestigation AND 
	  `report_idReport` = idReport AND
	  `report_patientuda_idPatientUda` = idPatientUda);
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS deleteMedicineDose$$
CREATE PROCEDURE deleteMedicineDose(IN idMedicine INT)
BEGIN
DELETE 
FROM `medicinedose`
WHERE (`idMedicineDosis` = idMedicine);
END$$
DELIMITER ;

-- UPDATES --

DELIMITER $$
DROP PROCEDURE IF EXISTS updateReport$$
CREATE PROCEDURE updateReport(IN id BIGINT, IN n_sys_total_avg INT, IN n_sys_day_avg INT, IN n_sys_night_avg INT, 
							  IN n_dias_total_avg INT, IN n_dias_day_avg INT, IN n_dias_night_avg INT,
							  IN n_tam_total_avg INT, IN n_tam_day_avg INT, IN n_tam_night_avg INT,
							  IN n_hr_total_avg INT, IN n_hr_day_avg INT, IN n_hr_night_avg INT,
							  
							  IN n_sys_total_sd DECIMAL(5,2), IN n_sys_day_sd DECIMAL(5,2), IN n_sys_night_sd DECIMAL(5,2), 
							  IN n_dias_total_sd DECIMAL(5,2), IN n_dias_day_sd DECIMAL(5,2), IN n_dias_night_sd DECIMAL(5,2), 
							  IN n_tam_total_sd DECIMAL(5,2), IN n_tam_day_sd DECIMAL(5,2), IN n_tam_night_sd DECIMAL(5,2),
							  IN n_hr_total_sd DECIMAL(5,2), IN n_hr_day_sd DECIMAL(5,2), IN n_hr_night_sd DECIMAL(5,2),

							  IN n_sys_day_max INT, IN n_sys_night_max INT,
							  IN n_dias_day_max INT, IN n_dias_night_max INT,
							  IN n_hr_day_max INT, IN n_hr_night_max INT,
							  
							  IN n_sys_day_min INT, IN n_sys_night_min INT,
							  IN n_dias_day_min INT, IN n_dias_night_min INT,
							  IN n_hr_day_min INT, IN n_hr_night_min INT,
							  
							  /*IN n_sys_day_max_dt DATETIME, IN n_sys_night_max_dt DATETIME,
							  IN n_dias_day_max_dt DATETIME, IN n_dias_night_max_dt DATETIME,
							  IN n_hr_day_max_dt DATETIME, IN n_hr_night_max_dt DATETIME,
							  
							  IN n_sys_day_min_dt DATETIME, IN n_sys_night_min_dt DATETIME,
							  IN n_dias_day_min_dt DATETIME, IN n_dias_night_min_dt DATETIME,
							  IN n_hr_day_min_dt DATETIME, IN n_hr_night_min_dt DATETIME,*/
							  
							  IN n_sys_dipping DECIMAL(7,4), IN n_dias_dipping DECIMAL(7,4))
BEGIN
UPDATE `report`
SET `total_avg_sys` = n_sys_total_avg,
	`day_avg_sys`  = n_sys_day_avg,
    `night_avg_sys` = n_sys_night_avg,
	`total_avg_dias` = n_dias_total_avg,
    `day_avg_dias` = n_dias_day_avg,
    `night_avg_dias` = n_dias_night_avg,
    `tot_tam_avg` = n_tam_total_avg,
	`day_tam_avg` = n_tam_day_avg,
	`night_tam_avg` = n_tam_night_avg,
	`tot_avg_hr` = n_hr_total_avg,
	`day_avg_hr` = n_hr_day_avg,
	`night_avg_hr` = n_hr_night_avg,
	
	`tot_sd_sis` = n_sys_total_sd,
    `day_sd_sis` = n_sys_day_sd,
    `night_sd_sis` = n_sys_night_sd,
	`tot_sd_dias` = n_dias_total_sd,
    `day_sd_dias` = n_dias_day_sd,
    `night_sd_dias` = n_dias_night_sd,
    `tot_sd_tam` = n_tam_total_sd,
    `day_sd_tam` = n_tam_day_sd,
    `night_sd_tam` = n_tam_night_sd,
    `tot_sd_hr` = n_hr_total_sd,
    `day_sd_hr` = n_hr_day_sd,
    `night_sd_hr` = n_hr_night_sd,
	
    `day_max_sys` = n_sys_day_max,
    `night_max_sys` = n_sys_night_max,
    `day_max_dias` = n_dias_day_max,
    `night_max_dias` = n_dias_night_max,
	`max_day_hr` = n_hr_day_max,
	`max_night_hr` = n_hr_night_max,
	
	`day_min_sis` = n_sys_day_min,
	`night_min_sis` = n_sys_night_min,
    `day_min_dias` = n_dias_day_min,
	`night_min_dias` = n_dias_night_min,
	`min_day_hr` = n_hr_day_min,
	`min_night_hr` = n_hr_night_min,
	
    /*`day_max_sys_dt` = n_sys_day_max_dt,
    `night_max_sys_dt` = n_sys_night_max_dt,
    `day_max_dias_dt` = n_dias_day_max_dt,
    `night_max_dias_dt` = n_dias_night_max_dt,
	`max_day_hr_dt` = n_hr_day_max_dt,
	`max_night_hr_dt` = n_hr_night_max_dt,
	
	`day_min_sis_dt` = n_sys_day_min_dt,
	`night_min_sis_dt` = n_sys_night_min_dt,
    `day_min_dias_dt` = n_dias_day_min_dt,
	`night_min_dias_dt` = n_dias_night_min_dt,
	`min_day_hr_dt` = n_hr_day_min_dt,
	`min_night_hr_dt` = n_hr_night_min_dt,*/
	
	`sys_dipping` = n_sys_dipping,
	`dias_dipping` = n_dias_dipping
WHERE `idReport` = id;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updateMeasure$$
CREATE PROCEDURE updateMeasure(IN idMeasure BIGINT, IN isEnabled BIT, IN comment_ TEXT)
BEGIN
UPDATE `measurement` SET `is_enabled` = isEnabled, `comment` = comment_
WHERE `idMeasurement` = idMeasure;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updateInvestigation$$
CREATE PROCEDURE updateInvestigation(IN id INT, IN name_ VARCHAR(45), IN creationdate_ DATETIME, IN comment_ TEXT)
BEGIN
UPDATE `investigation`
SET `name` = name_,
	`creation_date` = creationdate_,
	`comment` = comment_ 
WHERE idInvestigation = id;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updateDiagnosis$$
CREATE PROCEDURE updateDiagnosis(IN reportId BIGINT, IN doctor VARCHAR(45), IN diagnosis TEXT, IN diagnosis_dt DATETIME)
BEGIN
UPDATE `report` SET `doctor` = doctor, `diagnosis` = diagnosis, `diagnosis_date` = diagnosis_dt
WHERE `idReport` = reportId;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updateMedicalRecord$$
CREATE PROCEDURE updateMedicalRecord(IN id BIGINT, IN patient_id BIGINT, IN illness TEXT, IN comment_ TEXT)
BEGIN
UPDATE `medicalhistory`
SET `illness` = illness, 
	`comment` = comment_
WHERE `idMedicalHistory` = id AND `idPatientUda` = patient_id;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updateUser$$
CREATE PROCEDURE updateUser(IN idUsr INT, IN login_ VARCHAR(45), IN name_ TEXT, IN rol VARCHAR(45))
BEGIN
UPDATE `user`
SET `login` = login_,
	`name` = name_,
	`rol` = rol 
WHERE idUser = idUsr;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updatePassword$$
CREATE PROCEDURE updatePassword(IN login_var VARCHAR(45), IN pass_var TEXT)
BEGIN
UPDATE `user`
SET `password` = pass_var
WHERE `login` = login_var;
END$$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS updateDrug$$
CREATE PROCEDURE updateDrug(IN id INT, IN name_ VARCHAR(45), IN active VARCHAR(45), IN idType INT)
BEGIN
UPDATE `drug` 
SET `name` = name_, `active` = active, `drugtype_idDrugType` = idType
WHERE `idDrug` = id;
END$$
DELIMITER ;


-- GETS -- 

DELIMITER $$
DROP PROCEDURE IF EXISTS getPassword$$
CREATE PROCEDURE getPassword(OUT pass_var TEXT, IN login_var VARCHAR(45))
BEGIN
SET pass_var = (SELECT `pass` FROM `user` WHERE `login` = login_var LIMIT 1);
END$$
DELIMITER ;

/*

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

*/
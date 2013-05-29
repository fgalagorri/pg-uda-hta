SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `udahta_db` ;
CREATE SCHEMA IF NOT EXISTS `udahta_db` ;
USE `udahta_db` ;

-- -----------------------------------------------------
-- Table `udahta_db`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`User` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`User` (
  `idUser` INT NOT NULL AUTO_INCREMENT ,
  `login` VARCHAR(45) NOT NULL ,
  `password` VARCHAR(45) NOT NULL ,
  `rol` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`idUser`) ,
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`DrugType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`DrugType` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`DrugType` (
  `idDrugType` INT NOT NULL AUTO_INCREMENT ,
  `type` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`idDrugType`) ,
  UNIQUE INDEX `type_UNIQUE` (`type` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`Drug`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`Drug` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`Drug` (
  `idDrug` INT NOT NULL AUTO_INCREMENT ,
  `name` VARCHAR(45) NOT NULL ,
  `DrugType_idDrugType` INT NOT NULL ,
  PRIMARY KEY (`idDrug`) ,
  INDEX `fk_Drug_DrugType_idx` (`DrugType_idDrugType` ASC) ,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) ,
  CONSTRAINT `fk_Drug_DrugType`
    FOREIGN KEY (`DrugType_idDrugType` )
    REFERENCES `udahta_db`.`DrugType` (`idDrugType` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`DailyCarnet`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`DailyCarnet` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`DailyCarnet` (
  `idDailyCarnet` BIGINT NOT NULL AUTO_INCREMENT ,
  `technical` VARCHAR(45) NULL ,
  `initial_dias1` INT NULL ,
  `initial_dias2` INT NULL ,
  `initial_dias3` INT NULL ,
  `initial_hr1` INT NULL ,
  `initial_hr2` INT NULL ,
  `initial_hr3` INT NULL ,
  `final_dias1` INT NULL ,
  `final_dias2` INT NULL ,
  `final_dias3` INT NULL ,
  `final_hr1` INT NULL ,
  `final_hr2` INT NULL ,
  `final_hr3` INT NULL ,
  `begin_sleep_time` DATETIME NULL ,
  `end_sleep_time` DATETIME NULL ,
  `how_sleep` VARCHAR(45) NULL ,
  `main_meal_time` DATETIME NULL ,
  `init_sys1` INT NULL ,
  `init_sys2` INT NULL ,
  `init_sys3` INT NULL ,
  `final_sys1` INT NULL ,
  `final_sys2` INT NULL ,
  `final_sys3` INT NULL ,
  PRIMARY KEY (`idDailyCarnet`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`PatientUda`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`PatientUda` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`PatientUda` (
  `idPatientUda` INT NOT NULL ,
  PRIMARY KEY (`idPatientUda`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`TemporaryData`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`TemporaryData` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`TemporaryData` (
  `idTemporaryData` INT NOT NULL AUTO_INCREMENT ,
  `weight` DECIMAL(2) NULL ,
  `height` DECIMAL(2) NULL ,
  `age` INT NULL ,
  `body_mass_index` DECIMAL(2) NULL ,
  `smoker` BIT NULL ,
  `dyslipidemia` BIT NULL ,
  `diabetic` BIT NULL ,
  `known_hypertensive` BIT NULL ,
  `fat_percentage` DECIMAL(2) NULL ,
  `muscle_percentage` DECIMAL(2) NULL ,
  `kcal` INT NULL ,
  PRIMARY KEY (`idTemporaryData`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`Report`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`Report` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`Report` (
  `idReport` BIGINT NOT NULL AUTO_INCREMENT ,
  `begin_date` DATETIME NULL ,
  `end_date` DATETIME NULL ,
  `doctor` VARCHAR(45) NULL ,
  `diagnosis` TEXT NULL ,
  `request_doctor` VARCHAR(45) NULL ,
  `specialty` VARCHAR(45) NULL ,
  `day_avg_sys` INT NULL ,
  `night_avg_sys` INT NULL ,
  `total_avg_sys` INT NULL ,
  `day_max_sys` INT NULL ,
  `night_max_sys` INT NULL ,
  `day_avg_dias` INT NULL ,
  `night_avg_dias` INT NULL ,
  `total_avg_dias` INT NULL ,
  `day_max_dias` INT NULL ,
  `night_max_dias` INT NULL ,
  `idDevice` INT NOT NULL ,
  `deviceReportId` TEXT NOT NULL ,
  `DailyCarnet_idDailyCarnet` BIGINT NOT NULL ,
  `Patient_idPatient` INT NOT NULL ,
  `TemporaryData_idTemporaryData` INT NOT NULL ,
  PRIMARY KEY (`idReport`, `Patient_idPatient`) ,
  INDEX `fk_Report_DailyCarnet1_idx` (`DailyCarnet_idDailyCarnet` ASC) ,
  INDEX `fk_Report_Patient1_idx` (`Patient_idPatient` ASC) ,
  INDEX `fk_Report_TemporaryData1_idx` (`TemporaryData_idTemporaryData` ASC) ,
  CONSTRAINT `fk_Report_DailyCarnet1`
    FOREIGN KEY (`DailyCarnet_idDailyCarnet` )
    REFERENCES `udahta_db`.`DailyCarnet` (`idDailyCarnet` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Report_Patient1`
    FOREIGN KEY (`Patient_idPatient` )
    REFERENCES `udahta_db`.`PatientUda` (`idPatientUda` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Report_TemporaryData1`
    FOREIGN KEY (`TemporaryData_idTemporaryData` )
    REFERENCES `udahta_db`.`TemporaryData` (`idTemporaryData` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`Measurement`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`Measurement` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`Measurement` (
  `idMeasurement` INT NOT NULL AUTO_INCREMENT ,
  `date` DATETIME NULL ,
  `systolic` INT NULL ,
  `average` INT NULL ,
  `diastolic` INT NULL ,
  `heart_rate` INT NULL ,
  `sleep` BIT NULL ,
  `comment` TEXT NULL ,
  `Report_idReport` BIGINT NOT NULL ,
  `Report_Patient_idPatient` INT NOT NULL ,
  PRIMARY KEY (`idMeasurement`, `Report_idReport`, `Report_Patient_idPatient`) ,
  INDEX `fk_Measurement_Report1_idx` (`Report_idReport` ASC, `Report_Patient_idPatient` ASC) ,
  CONSTRAINT `fk_Measurement_Report1`
    FOREIGN KEY (`Report_idReport` , `Report_Patient_idPatient` )
    REFERENCES `udahta_db`.`Report` (`idReport` , `Patient_idPatient` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`Investigation`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`Investigation` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`Investigation` (
  `idInvestigation` INT NOT NULL AUTO_INCREMENT ,
  `name` VARCHAR(45) NOT NULL ,
  `creation_date` DATETIME NOT NULL ,
  PRIMARY KEY (`idInvestigation`) ,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`MedicineDose`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`MedicineDose` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`MedicineDose` (
  `idMedicineDosis` INT NOT NULL AUTO_INCREMENT ,
  `time` DATETIME NOT NULL ,
  `dose` VARCHAR(45) NOT NULL ,
  `Drug_idDrug` INT NOT NULL ,
  `TemporaryData_idTemporaryData` INT NOT NULL ,
  PRIMARY KEY (`idMedicineDosis`) ,
  INDEX `fk_MedicineDosis_Drug1_idx` (`Drug_idDrug` ASC) ,
  INDEX `fk_MedicineDose_TemporaryData1_idx` (`TemporaryData_idTemporaryData` ASC) ,
  CONSTRAINT `fk_MedicineDosis_Drug1`
    FOREIGN KEY (`Drug_idDrug` )
    REFERENCES `udahta_db`.`Drug` (`idDrug` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_MedicineDose_TemporaryData1`
    FOREIGN KEY (`TemporaryData_idTemporaryData` )
    REFERENCES `udahta_db`.`TemporaryData` (`idTemporaryData` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`Investigation_has_Report`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`Investigation_has_Report` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`Investigation_has_Report` (
  `Investigation_idInvestigation` INT NOT NULL ,
  `Report_idReport` BIGINT NOT NULL ,
  `Report_Patient_idPatient` INT NOT NULL ,
  PRIMARY KEY (`Investigation_idInvestigation`, `Report_idReport`, `Report_Patient_idPatient`) ,
  INDEX `fk_Investigation_has_Report_Investigation1_idx` (`Investigation_idInvestigation` ASC) ,
  INDEX `fk_Investigation_has_Report_Report1_idx` (`Report_idReport` ASC, `Report_Patient_idPatient` ASC) ,
  CONSTRAINT `fk_Investigation_has_Report_Investigation1`
    FOREIGN KEY (`Investigation_idInvestigation` )
    REFERENCES `udahta_db`.`Investigation` (`idInvestigation` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Investigation_has_Report_Report1`
    FOREIGN KEY (`Report_idReport` , `Report_Patient_idPatient` )
    REFERENCES `udahta_db`.`Report` (`idReport` , `Patient_idPatient` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`ExtrasID`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`ExtrasID` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`ExtrasID` (
  `idExtrasID` INT NOT NULL AUTO_INCREMENT ,
  `description` TEXT(100) NOT NULL ,
  PRIMARY KEY (`idExtrasID`) ,
  UNIQUE INDEX `idExtrasID_UNIQUE` (`idExtrasID` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`Complications_Activities`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`Complications_Activities` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`Complications_Activities` (
  `idComplications_Activities` INT NOT NULL AUTO_INCREMENT ,
  `hour` INT NULL ,
  `minutes` INT NULL ,
  `specification` TEXT NULL ,
  `DailyCarnet_idDailyCarnet` BIGINT NOT NULL ,
  `ExtrasID_idExtrasID` INT NOT NULL ,
  PRIMARY KEY (`idComplications_Activities`, `ExtrasID_idExtrasID`) ,
  INDEX `fk_Complications_Activities_DailyCarnet1_idx` (`DailyCarnet_idDailyCarnet` ASC) ,
  INDEX `fk_Complications_Activities_ExtrasID1_idx` (`ExtrasID_idExtrasID` ASC) ,
  CONSTRAINT `fk_Complications_Activities_DailyCarnet1`
    FOREIGN KEY (`DailyCarnet_idDailyCarnet` )
    REFERENCES `udahta_db`.`DailyCarnet` (`idDailyCarnet` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Complications_Activities_ExtrasID1`
    FOREIGN KEY (`ExtrasID_idExtrasID` )
    REFERENCES `udahta_db`.`ExtrasID` (`idExtrasID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`MedicalHistory`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`MedicalHistory` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`MedicalHistory` (
  `idMedical History` INT NOT NULL ,
  `Illness` VARCHAR(45) NULL ,
  `From` DATETIME NULL ,
  `To` DATETIME NULL ,
  `Comment` TEXT NULL ,
  `Patient_idPatient` INT NOT NULL ,
  PRIMARY KEY (`idMedical History`, `Patient_idPatient`) ,
  INDEX `fk_MedicalHistory_Patient1_idx` (`Patient_idPatient` ASC) ,
  CONSTRAINT `fk_MedicalHistory_Patient1`
    FOREIGN KEY (`Patient_idPatient` )
    REFERENCES `udahta_db`.`PatientUda` (`idPatientUda` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

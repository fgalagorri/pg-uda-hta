SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `udahta_db` ;
CREATE SCHEMA IF NOT EXISTS `udahta_db` ;
USE `udahta_db` ;

-- -----------------------------------------------------
-- Table `udahta_db`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`user` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`user` (
  `idUser` INT NOT NULL AUTO_INCREMENT ,
  `login` VARCHAR(45) NOT NULL ,
  `password` VARCHAR(45) NOT NULL ,
  `rol` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`idUser`) ,
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`drugtype`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`drugtype` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`drugtype` (
  `idDrugType` INT NOT NULL AUTO_INCREMENT ,
  `type` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`idDrugType`) ,
  UNIQUE INDEX `type_UNIQUE` (`type` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`drug`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`drug` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`drug` (
  `idDrug` INT NOT NULL AUTO_INCREMENT ,
  `name` VARCHAR(45) NOT NULL ,
  `drugtype_idDrugType` INT NOT NULL ,
  PRIMARY KEY (`idDrug`) ,
  INDEX `fk_Drug_DrugType_idx` (`drugtype_idDrugType` ASC) ,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) ,
  CONSTRAINT `fk_Drug_DrugType`
    FOREIGN KEY (`drugtype_idDrugType` )
    REFERENCES `udahta_db`.`drugtype` (`idDrugType` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`dailycarnet`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`dailycarnet` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`dailycarnet` (
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
-- Table `udahta_db`.`patientuda`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`patientuda` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`patientuda` (
  `idPatientUda` BIGINT NOT NULL ,
  PRIMARY KEY (`idPatientUda`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`temporarydata`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`temporarydata` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`temporarydata` (
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
-- Table `udahta_db`.`report`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`report` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`report` (
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
  `dailycarnet_idDailyCarnet` BIGINT NOT NULL ,
  `patientuda_idPatientUda` BIGINT NOT NULL ,
  `temporarydata_idTemporaryData` INT NOT NULL ,
  PRIMARY KEY (`idReport`, `patientuda_idPatientUda`) ,
  INDEX `fk_Report_DailyCarnet1_idx` (`dailycarnet_idDailyCarnet` ASC) ,
  INDEX `fk_Report_Patient1_idx` (`patientuda_idPatientUda` ASC) ,
  INDEX `fk_Report_TemporaryData1_idx` (`temporarydata_idTemporaryData` ASC) ,
  CONSTRAINT `fk_Report_DailyCarnet1`
    FOREIGN KEY (`dailycarnet_idDailyCarnet` )
    REFERENCES `udahta_db`.`dailycarnet` (`idDailyCarnet` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Report_Patient1`
    FOREIGN KEY (`patientuda_idPatientUda` )
    REFERENCES `udahta_db`.`patientuda` (`idPatientUda` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Report_TemporaryData1`
    FOREIGN KEY (`temporarydata_idTemporaryData` )
    REFERENCES `udahta_db`.`temporarydata` (`idTemporaryData` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`measurement`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`measurement` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`measurement` (
  `idMeasurement` INT NOT NULL AUTO_INCREMENT ,
  `date` DATETIME NULL ,
  `systolic` INT NULL ,
  `average` INT NULL ,
  `diastolic` INT NULL ,
  `heart_rate` INT NULL ,
  `sleep` BIT NULL ,
  `comment` TEXT NULL ,
  `report_idReport` BIGINT NOT NULL ,
  `report_patientuda_idPatientUda` BIGINT NOT NULL ,
  PRIMARY KEY (`idMeasurement`, `report_idReport`, `report_patientuda_idPatientUda`) ,
  INDEX `fk_Measurement_Report1_idx` (`report_idReport` ASC, `report_patientuda_idPatientUda` ASC) ,
  CONSTRAINT `fk_Measurement_Report1`
    FOREIGN KEY (`report_idReport` , `report_patientuda_idPatientUda` )
    REFERENCES `udahta_db`.`report` (`idReport` , `patientuda_idPatientUda` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`investigation`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`investigation` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`investigation` (
  `idInvestigation` INT NOT NULL AUTO_INCREMENT ,
  `name` VARCHAR(45) NOT NULL ,
  `creation_date` DATETIME NOT NULL ,
  PRIMARY KEY (`idInvestigation`) ,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`medicinedose`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`medicinedose` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`medicinedose` (
  `idMedicineDosis` INT NOT NULL AUTO_INCREMENT ,
  `time` DATETIME NOT NULL ,
  `dose` TEXT NOT NULL ,
  `drug_idDrug` INT NOT NULL ,
  `temporarydata_idTemporaryData` INT NOT NULL ,
  PRIMARY KEY (`idMedicineDosis`) ,
  INDEX `fk_MedicineDosis_Drug1_idx` (`drug_idDrug` ASC) ,
  INDEX `fk_MedicineDose_TemporaryData1_idx` (`temporarydata_idTemporaryData` ASC) ,
  CONSTRAINT `fk_MedicineDosis_Drug1`
    FOREIGN KEY (`drug_idDrug` )
    REFERENCES `udahta_db`.`drug` (`idDrug` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_MedicineDose_TemporaryData1`
    FOREIGN KEY (`temporarydata_idTemporaryData` )
    REFERENCES `udahta_db`.`temporarydata` (`idTemporaryData` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`investigation_has_report`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`investigation_has_report` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`investigation_has_report` (
  `investigation_idInvestigation` INT NOT NULL ,
  `report_idReport` BIGINT NOT NULL ,
  `report_patientuda_idPatientUda` BIGINT NOT NULL ,
  PRIMARY KEY (`investigation_idInvestigation`, `report_idReport`, `report_patientuda_idPatientUda`) ,
  INDEX `fk_Investigation_has_Report_Investigation1_idx` (`investigation_idInvestigation` ASC) ,
  INDEX `fk_Investigation_has_Report_Report1_idx` (`report_idReport` ASC, `report_patientuda_idPatientUda` ASC) ,
  CONSTRAINT `fk_Investigation_has_Report_Investigation1`
    FOREIGN KEY (`investigation_idInvestigation` )
    REFERENCES `udahta_db`.`investigation` (`idInvestigation` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Investigation_has_Report_Report1`
    FOREIGN KEY (`report_idReport` , `report_patientuda_idPatientUda` )
    REFERENCES `udahta_db`.`report` (`idReport` , `patientuda_idPatientUda` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`complications_activities`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`complications_activities` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`complications_activities` (
  `idComplications_Activities` INT NOT NULL AUTO_INCREMENT ,
  `hour` INT NULL ,
  `minutes` INT NULL ,
  `specification` TEXT NULL ,
  `dailycarnet_idDailyCarnet` BIGINT NOT NULL ,
  `description` TEXT NULL ,
  PRIMARY KEY (`idComplications_Activities`) ,
  INDEX `fk_Complications_Activities_DailyCarnet1_idx` (`dailycarnet_idDailyCarnet` ASC) ,
  CONSTRAINT `fk_Complications_Activities_DailyCarnet1`
    FOREIGN KEY (`dailycarnet_idDailyCarnet` )
    REFERENCES `udahta_db`.`dailycarnet` (`idDailyCarnet` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `udahta_db`.`medicalhistory`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `udahta_db`.`medicalhistory` ;

CREATE  TABLE IF NOT EXISTS `udahta_db`.`medicalhistory` (
  `idMedicalHistory` INT NOT NULL ,
  `illness` VARCHAR(45) NULL ,
  `from` DATETIME NULL ,
  `to` DATETIME NULL ,
  `comment` TEXT NULL ,
  `patientuda_idPatientUda` BIGINT NOT NULL ,
  PRIMARY KEY (`idMedicalHistory`, `patientuda_idPatientUda`) ,
  INDEX `fk_MedicalHistory_Patient1_idx` (`patientuda_idPatientUda` ASC) ,
  CONSTRAINT `fk_MedicalHistory_Patient1`
    FOREIGN KEY (`patientuda_idPatientUda` )
    REFERENCES `udahta_db`.`patientuda` (`idPatientUda` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

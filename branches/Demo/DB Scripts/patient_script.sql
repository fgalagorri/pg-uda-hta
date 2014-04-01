SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `patient_info_db` ;
CREATE SCHEMA IF NOT EXISTS `patient_info_db` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci ;
USE `patient_info_db` ;

-- -----------------------------------------------------
-- Table `patient_info_db`.`patient`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `patient_info_db`.`patient` ;

CREATE  TABLE IF NOT EXISTS `patient_info_db`.`patient` (
  `idPatient` BIGINT NOT NULL AUTO_INCREMENT ,
  `name` VARCHAR(45) NOT NULL ,
  `surname` VARCHAR(45) NOT NULL ,
  `document` VARCHAR(45) NOT NULL ,
  `gender` ENUM('F','M','') NOT NULL DEFAULT 'M' ,
  `telephone` VARCHAR(45) NULL ,
  `cell_phone` VARCHAR(45) NULL ,
  `telephone_alt` VARCHAR(45) NULL ,
  `address` TEXT NULL ,
  `city` VARCHAR(45) NULL ,
  `neighborhood` VARCHAR(45) NULL ,
  `department` VARCHAR(45) NULL ,
  `birthday` DATETIME NULL ,
  `e_mail` TEXT NULL ,
  `register_number` VARCHAR(45) NULL ,
  `modified_date` DATETIME NULL ,
  PRIMARY KEY (`idPatient`) ,
  UNIQUE INDEX `document_UNIQUE` (`document` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `patient_info_db`.`emergency_contact`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `patient_info_db`.`emergency_contact` ;

CREATE  TABLE IF NOT EXISTS `patient_info_db`.`emergency_contact` (
  `idemergency_contact` BIGINT NOT NULL AUTO_INCREMENT ,
  `name` VARCHAR(45) NULL ,
  `surname` VARCHAR(45) NULL ,
  `phone` VARCHAR(45) NULL ,
  `patient_idPatient` BIGINT NOT NULL ,
  PRIMARY KEY (`idemergency_contact`) ,
  INDEX `fk_emergency_contact_Patient_idx` (`patient_idPatient` ASC) ,
  CONSTRAINT `fk_emergency_contact_Patient`
    FOREIGN KEY (`patient_idPatient` )
    REFERENCES `patient_info_db`.`patient` (`idPatient` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `patient_info_db`.`device_reference`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `patient_info_db`.`device_reference` ;

CREATE  TABLE IF NOT EXISTS `patient_info_db`.`device_reference` (
  `iddevice_reference` BIGINT NOT NULL AUTO_INCREMENT ,
  `device_type` INT NULL ,
  `device_ref` VARCHAR(45) NULL ,
  `patient_idPatient` BIGINT NOT NULL ,
  PRIMARY KEY (`iddevice_reference`) ,
  INDEX `fk_device_reference_patient1_idx` (`patient_idPatient` ASC) ,
  CONSTRAINT `fk_device_reference_patient1`
    FOREIGN KEY (`patient_idPatient` )
    REFERENCES `patient_info_db`.`patient` (`idPatient` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

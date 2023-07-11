CREATE DATABASE taskify_api;
USE taskify_api;

CREATE TABLE `taskify_api`.`users` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `State` INT NOT NULL DEFAULT 1,
  `Email` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(50) NOT NULL,
  `FirstName` VARCHAR(50) NOT NULL,
  `LastName` VARCHAR(50) NOT NULL,
  `Created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Updated` DATETIME NULL DEFAULT NULL,
  `Deleted` DATETIME NULL DEFAULT NULL,
  `CreatedUserId` INT NOT NULL,
  `UpdatedUserId` INT NULL DEFAULT NULL,
  `DeletedUserId` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
  AUTO_INCREMENT = 100 ;

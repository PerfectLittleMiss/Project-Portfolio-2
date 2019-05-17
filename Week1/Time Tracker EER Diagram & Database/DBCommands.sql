-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema KinaoleLau_MDV229_Database_201905
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema KinaoleLau_MDV229_Database_201905
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `KinaoleLau_MDV229_Database_201905` DEFAULT CHARACTER SET utf8 ;
USE `KinaoleLau_MDV229_Database_201905` ;

-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`time_tracker_users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`time_tracker_users` (
  `user_id` INT NOT NULL,
  `user_password` VARCHAR(10) NOT NULL,
  `user_firstname` VARCHAR(25) NOT NULL,
  `user_lastname` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`user_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (
  `calendar_day_id` INT NOT NULL,
  `calendar_numerical_day` INT NOT NULL,
  PRIMARY KEY (`calendar_day_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (
  `calendar_date_id` INT NOT NULL,
  `calendar_date` DATE NOT NULL,
  PRIMARY KEY (`calendar_date_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`days_of_week`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`days_of_week` (
  `day_id` INT NOT NULL,
  `day_name` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`day_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`activity_categories`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`activity_categories` (
  `activity_category_id` INT NOT NULL,
  `category_description` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`activity_category_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`activity_descriptions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (
  `activity_description_id` INT NOT NULL,
  `activity_description` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`activity_description_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`activity_times`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`activity_times` (
  `activity_time_id` INT NOT NULL,
  `time_spent_on_actvitiy` DOUBLE NOT NULL,
  PRIMARY KEY (`activity_time_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `KinaoleLau_MDV229_Database_201905`.`activity_log`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `KinaoleLau_MDV229_Database_201905`.`activity_log` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NULL,
  `calendar_day` INT NULL,
  `calendar_date` INT NULL,
  `day_name` INT NULL,
  `category_description` INT NULL,
  `activity_description` INT NULL,
  `time_spent_on_activity` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `activityCategory_idx` (`category_description` ASC),
  INDEX `activityDescription_idx` (`activity_description` ASC),
  INDEX `timeSpent_idx` (`time_spent_on_activity` ASC),
  INDEX `dayOfWeek_idx` (`day_name` ASC),
  INDEX `user_idx` (`user_id` ASC),
  INDEX `date_idx` (`calendar_date` ASC),
  INDEX `numericDay_idx` (`calendar_day` ASC),
  CONSTRAINT `activityCategory`
    FOREIGN KEY (`category_description`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `activityDescription`
    FOREIGN KEY (`activity_description`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `timeSpent`
    FOREIGN KEY (`time_spent_on_activity`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `dayOfWeek`
    FOREIGN KEY (`day_name`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `user`
    FOREIGN KEY (`user_id`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`time_tracker_users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `date`
    FOREIGN KEY (`calendar_date`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `numericDay`
    FOREIGN KEY (`calendar_day`)
    REFERENCES `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`time_tracker_users`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`time_tracker_users` (`user_id`, `user_password`, `user_firstname`, `user_lastname`) VALUES (1, 'password', 'Kinaole', 'Lau');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`time_tracker_users` (`user_id`, `user_password`, `user_firstname`, `user_lastname`) VALUES (2, 'password', 'admin', 'admin');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`time_tracker_users` (`user_id`, `user_password`, `user_firstname`, `user_lastname`) VALUES (3, 'password', 'instructor', 'instructor');

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (1, 1);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (2, 2);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (3, 3);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (4, 4);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (5, 5);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (6, 6);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (7, 7);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (8, 8);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (9, 9);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (10, 10);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (11, 11);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (12, 12);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (13, 13);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (14, 14);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (15, 15);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (16, 16);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (17, 17);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (18, 18);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (19, 19);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (20, 20);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (21, 21);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (22, 22);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (23, 23);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (24, 24);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (25, 25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`) VALUES (26, 26);

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (1, '2019-05-06');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (2, '2019-05-07');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (3, '2019-05-08');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (4, '2019-05-09');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (5, '2019-05-10');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (6, '2019-05-11');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (7, '2019-05-12');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (8, '2019-05-13');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (9, '2019-05-14');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (10, '2019-05-15');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (11, '2019-05-16');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (12, '2019-05-17');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (13, '2019-05-18');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (14, '2019-05-19');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (15, '2019-05-20');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (16, '2019-05-21');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (17, '2019-05-22');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (18, '2019-05-23');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (19, '2019-05-24');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (20, '2019-05-25');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (21, '2019-05-26');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (22, '2019-05-27');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (23, '2019-05-28');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (24, '2019-05-29');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (25, '2019-05-30');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`tracked_calendar_dates` (`calendar_date_id`, `calendar_date`) VALUES (26, '2019-05-31');

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`days_of_week`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (1, 'Monday');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (2, 'Tuesday');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (3, 'Wednesday');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (4, 'Thursday');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (5, 'Friday');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (6, 'Saturday');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`days_of_week` (`day_id`, `day_name`) VALUES (7, 'Sunday');

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`activity_categories`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`, `category_description`) VALUES (1, 'Sleep');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`, `category_description`) VALUES (2, 'Eat');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`, `category_description`) VALUES (3, 'Work');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`, `category_description`) VALUES (4, 'Relax');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`, `category_description`) VALUES (5, 'Project & Portfolio 2');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_categories` (`activity_category_id`, `category_description`) VALUES (6, 'Exercise');

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`activity_descriptions`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (1, 'Playing Games');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (2, 'Watching WWE');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (3, 'Writing');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (4, 'Researching');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (5, 'Coding');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (6, 'Reading');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (7, 'World-Building');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (8, 'Drawing');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (9, 'Dancing');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (10, 'Walking the dog');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (11, 'Making/Getting Food');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (12, 'Bookkeeping');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (13, 'Design Work');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (14, 'Driving/In the car');
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_descriptions` (`activity_description_id`, `activity_description`) VALUES (15, 'Data Entry');

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`activity_times`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (1, .25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (2, .50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (3, .75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (4, 1);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (5, 1.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (6, 1.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (7, 1.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (8, 2);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (9, 2.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (10, 2.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (11, 2.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (12, 3);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (13, 3.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (14, 3.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (15, 3.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (16, 4);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (17, 4.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (18, 4.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (19, 4.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (20, 5);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (21, 5.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (22, 5.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (23, 5.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (24, 6);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (25, 6.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (26, 6.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (27, 6.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (28, 7);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (29, 7.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (30, 7.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (31, 7.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (32, 8);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (33, 8.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (34, 8.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (35, 8.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (36, 9);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (37, 9.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (38, 9.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (39, 9.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (40, 10);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (41, 10.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (42, 10.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (43, 10.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (44, 11);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (45, 11.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (46, 11.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (47, 11.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (48, 12);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (49, 12.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (50, 12.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (51, 12.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (52, 13);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (53, 13.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (54, 13.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (55, 13.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (56, 14);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (57, 14.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (58, 14.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (59, 14.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (60, 15);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (61, 15.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (62, 15.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (63, 15.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (64, 16);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (65, 16.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (66, 16.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (67, 16.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (68, 17);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (69, 17.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (70, 17.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (71, 17.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (72, 18);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (73, 18.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (74, 18.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (75, 18.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (76, 19);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (77, 19.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (78, 19.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (79, 19.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (80, 20);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (81, 20.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (82, 20.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (83, 20.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (84, 21);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (85, 21.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (86, 21.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (87, 21.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (88, 22);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (89, 22.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (90, 22.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (91, 22.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (92, 23);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (93, 23.25);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (94, 23.50);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (95, 23.75);
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_times` (`activity_time_id`, `time_spent_on_actvitiy`) VALUES (96, 24);

COMMIT;


-- -----------------------------------------------------
-- Data for table `KinaoleLau_MDV229_Database_201905`.`activity_log`
-- -----------------------------------------------------
START TRANSACTION;
USE `KinaoleLau_MDV229_Database_201905`;
INSERT INTO `KinaoleLau_MDV229_Database_201905`.`activity_log` (`id`, `user_id`, `calendar_day`, `calendar_date`, `day_name`, `category_description`, `activity_description`, `time_spent_on_activity`) VALUES (1, 1, 1, 1, 1, 1, 1, 1);

COMMIT;


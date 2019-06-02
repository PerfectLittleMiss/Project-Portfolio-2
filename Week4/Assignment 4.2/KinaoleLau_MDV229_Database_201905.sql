# ************************************************************
# Sequel Pro SQL dump
# Version 4541
#
# http://www.sequelpro.com/
# https://github.com/sequelpro/sequelpro
#
# Host: localhost (MySQL 5.7.23)
# Database: KinaoleLau_MDV229_Database_201905
# Generation Time: 2019-06-01 03:52:31 +0000
# ************************************************************


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


# Dump of table activity_categories
# ------------------------------------------------------------

DROP TABLE IF EXISTS `activity_categories`;

CREATE TABLE `activity_categories` (
  `activity_category_id` int(11) NOT NULL,
  `category_description` varchar(25) NOT NULL,
  PRIMARY KEY (`activity_category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `activity_categories` WRITE;
/*!40000 ALTER TABLE `activity_categories` DISABLE KEYS */;

INSERT INTO `activity_categories` (`activity_category_id`, `category_description`)
VALUES
	(1,'Work'),
	(2,'Relax'),
	(3,'Project & Portfolio 2'),
	(4,'Other');

/*!40000 ALTER TABLE `activity_categories` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table activity_descriptions
# ------------------------------------------------------------

DROP TABLE IF EXISTS `activity_descriptions`;

CREATE TABLE `activity_descriptions` (
  `activity_description_id` int(11) NOT NULL,
  `activity_description` varchar(25) NOT NULL,
  PRIMARY KEY (`activity_description_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `activity_descriptions` WRITE;
/*!40000 ALTER TABLE `activity_descriptions` DISABLE KEYS */;

INSERT INTO `activity_descriptions` (`activity_description_id`, `activity_description`)
VALUES
	(1,'Playing Games'),
	(2,'Watching WWE'),
	(3,'Writing'),
	(4,'Researching'),
	(5,'Coding'),
	(6,'Reading'),
	(7,'World-Building'),
	(8,'Drawing'),
	(9,'Dancing'),
	(10,'Walking the dog'),
	(11,'Eating'),
	(12,'Bookkeeping'),
	(13,'Design Work'),
	(14,'Driving/In the car'),
	(15,'Data Entry'),
	(16,'Sleep'),
	(17,'Troubleshooting'),
	(18,'Doing Homework'),
	(19,'Meetings');

/*!40000 ALTER TABLE `activity_descriptions` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table activity_log
# ------------------------------------------------------------

DROP TABLE IF EXISTS `activity_log`;

CREATE TABLE `activity_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `calendar_day` int(11) DEFAULT NULL,
  `calendar_date` int(11) DEFAULT NULL,
  `day_name` int(11) DEFAULT NULL,
  `category_description` int(11) DEFAULT NULL,
  `activity_description` int(11) DEFAULT NULL,
  `time_spent_on_activity` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `activityCategory_idx` (`category_description`),
  KEY `activityDescription_idx` (`activity_description`),
  KEY `timeSpent_idx` (`time_spent_on_activity`),
  KEY `dayOfWeek_idx` (`day_name`),
  KEY `user_idx` (`user_id`),
  KEY `date_idx` (`calendar_date`),
  KEY `numericDay_idx` (`calendar_day`),
  CONSTRAINT `activityCategory` FOREIGN KEY (`category_description`) REFERENCES `activity_categories` (`activity_category_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `activityDescription` FOREIGN KEY (`activity_description`) REFERENCES `activity_descriptions` (`activity_description_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `date` FOREIGN KEY (`calendar_date`) REFERENCES `tracked_calendar_dates` (`calendar_date_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `dayOfWeek` FOREIGN KEY (`day_name`) REFERENCES `days_of_week` (`day_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `numericDay` FOREIGN KEY (`calendar_day`) REFERENCES `tracked_calendar_days` (`calendar_day_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `timeSpent` FOREIGN KEY (`time_spent_on_activity`) REFERENCES `activity_times` (`activity_time_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `user` FOREIGN KEY (`user_id`) REFERENCES `time_tracker_users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `activity_log` WRITE;
/*!40000 ALTER TABLE `activity_log` DISABLE KEYS */;

INSERT INTO `activity_log` (`id`, `user_id`, `calendar_day`, `calendar_date`, `day_name`, `category_description`, `activity_description`, `time_spent_on_activity`)
VALUES
	(18,2,1,1,1,1,1,1),
	(19,2,26,26,5,3,18,3),
	(20,1,1,1,1,4,16,50),
	(21,1,2,2,2,3,4,2),
	(22,1,2,2,2,3,5,6),
	(23,1,2,2,2,3,17,2),
	(24,1,2,2,2,4,16,38),
	(25,1,2,2,2,4,11,16),
	(26,1,3,3,3,4,16,48),
	(27,1,4,4,4,3,5,16),
	(28,1,4,4,4,3,17,6),
	(29,1,4,4,4,3,3,16),
	(30,1,4,4,4,3,15,16),
	(31,1,4,4,4,4,16,36),
	(32,1,4,4,4,1,7,32),
	(33,1,5,5,5,3,6,16),
	(34,1,5,5,5,1,7,48),
	(35,1,5,5,5,4,16,44),
	(36,1,5,5,5,4,11,16),
	(37,1,5,5,5,2,2,32),
	(38,1,6,6,6,3,4,32),
	(39,1,6,6,6,3,3,2),
	(40,1,6,6,6,3,6,16),
	(41,1,6,6,6,3,17,2),
	(42,1,6,6,6,3,15,16),
	(43,1,6,6,6,4,16,36),
	(44,1,6,6,6,1,7,6),
	(45,1,6,6,6,2,2,32),
	(46,1,7,7,7,3,4,32),
	(47,1,7,7,7,3,3,16),
	(48,1,7,7,7,3,15,16),
	(49,1,7,7,7,4,16,34),
	(50,1,7,7,7,4,11,2),
	(51,1,7,7,7,2,14,6),
	(52,1,8,8,1,3,15,16),
	(53,1,8,8,1,3,17,16),
	(54,1,8,8,1,3,18,6),
	(55,1,8,8,1,4,16,34),
	(56,1,8,8,1,2,14,2),
	(57,1,8,8,1,1,7,32),
	(58,1,8,8,1,3,4,16),
	(59,1,8,8,1,3,3,6),
	(60,1,8,8,1,2,2,32),
	(61,1,8,8,1,4,11,16),
	(62,1,8,8,1,1,13,2),
	(63,1,9,9,2,4,16,28),
	(64,1,9,9,2,1,12,48),
	(65,1,9,9,2,4,11,16),
	(66,1,9,9,2,1,14,16),
	(67,1,9,9,2,3,18,32),
	(68,1,10,10,3,4,16,36),
	(69,1,10,10,3,3,5,32),
	(70,1,10,10,3,3,17,15),
	(71,1,10,10,3,4,11,16),
	(72,1,10,10,3,4,10,6),
	(73,1,10,10,3,3,18,6),
	(74,1,10,10,3,3,15,16),
	(75,1,10,10,3,3,3,16),
	(76,1,10,10,3,2,2,6),
	(77,1,10,10,3,1,7,16),
	(78,1,11,11,4,4,16,28),
	(79,1,11,11,4,3,18,32),
	(80,1,11,11,4,3,5,6),
	(81,1,11,11,4,3,17,32),
	(82,1,11,11,4,3,15,16),
	(83,1,11,11,4,3,4,16),
	(84,1,11,11,4,1,13,2),
	(85,1,11,11,4,1,8,10),
	(86,1,11,11,4,1,3,16),
	(87,1,11,11,4,1,7,16),
	(88,1,11,11,4,4,11,2),
	(89,1,12,12,5,4,16,32),
	(90,1,12,12,5,3,4,32),
	(91,1,12,12,5,3,3,16),
	(92,1,12,12,5,3,18,16),
	(93,1,12,12,5,4,11,16),
	(94,1,12,12,5,4,9,48),
	(95,1,12,12,5,4,14,2),
	(96,1,12,12,5,1,3,16),
	(97,1,12,12,5,3,15,16),
	(98,1,13,13,6,4,16,32),
	(99,1,13,13,6,3,5,20),
	(100,1,13,13,6,3,17,48),
	(101,1,13,13,6,3,15,16),
	(102,1,13,13,6,4,9,48),
	(103,1,13,13,6,2,6,2),
	(104,1,13,13,6,1,19,6),
	(105,1,14,14,7,4,16,28),
	(106,1,14,14,7,1,3,6),
	(107,1,14,14,7,3,5,14),
	(108,1,14,14,7,3,17,11),
	(109,1,14,14,7,3,15,16),
	(110,1,14,14,7,2,2,32),
	(111,1,14,14,7,4,10,9),
	(112,1,15,15,1,3,6,16),
	(113,1,15,15,1,4,16,32),
	(114,1,15,15,1,1,3,32),
	(115,1,15,15,1,2,2,16),
	(116,1,15,15,1,4,10,16),
	(117,1,15,15,1,3,18,16),
	(118,1,15,15,1,3,15,16),
	(119,1,16,16,2,4,16,36),
	(120,1,16,16,2,3,5,32),
	(121,1,16,16,2,3,17,11),
	(122,1,16,16,2,4,11,16),
	(123,1,16,16,2,3,4,16),
	(124,1,16,16,2,2,2,6),
	(125,1,16,16,2,3,18,16),
	(126,1,17,17,3,4,16,32),
	(127,1,17,17,3,4,14,6),
	(128,1,17,17,3,3,5,48),
	(129,1,17,17,3,3,17,6),
	(130,1,17,17,3,3,15,16),
	(131,1,17,17,3,3,18,2),
	(132,1,17,17,3,4,10,32),
	(133,1,17,17,3,4,11,16),
	(134,1,17,17,3,1,6,16),
	(135,1,17,17,3,2,2,6),
	(136,1,18,18,4,4,16,36),
	(137,1,18,18,4,1,3,32),
	(138,1,18,18,4,4,11,10),
	(139,1,18,18,4,3,15,16),
	(140,1,19,19,5,4,16,32),
	(141,1,19,19,5,4,14,6),
	(142,1,19,19,5,3,4,16),
	(143,1,19,19,5,3,3,16),
	(144,1,19,19,5,2,9,48),
	(145,1,19,19,5,2,2,6),
	(146,1,19,19,5,4,11,6),
	(147,1,20,20,6,4,16,32),
	(148,1,20,20,6,3,15,16),
	(149,1,20,20,6,3,4,6),
	(150,1,20,20,6,3,3,16),
	(151,1,20,20,6,4,10,5),
	(152,1,20,20,6,4,11,16),
	(153,1,20,20,6,4,17,2),
	(154,1,20,20,6,3,18,2),
	(155,1,21,21,7,4,16,26),
	(156,1,21,21,7,4,14,3),
	(157,1,21,21,7,4,11,16),
	(158,1,21,21,7,3,4,16),
	(159,1,21,21,7,3,3,3),
	(160,1,21,21,7,3,15,16),
	(161,1,21,21,7,3,5,32),
	(162,1,21,21,7,3,17,6),
	(163,1,21,21,7,3,6,16),
	(164,1,22,22,1,4,16,28),
	(165,1,22,22,1,4,14,7),
	(166,1,22,22,1,4,11,32),
	(167,1,22,22,1,1,7,16),
	(168,1,22,22,1,4,10,7),
	(169,1,22,22,1,3,15,16),
	(170,1,22,22,1,2,2,6),
	(171,1,23,23,2,4,16,36),
	(172,1,23,23,2,3,3,6),
	(173,1,23,23,2,3,13,2),
	(174,1,23,23,2,3,18,14),
	(175,1,23,23,2,4,11,16),
	(176,1,23,23,2,3,15,16),
	(177,1,23,23,2,3,17,2),
	(178,1,24,24,3,4,16,32),
	(179,1,24,24,3,3,18,48),
	(180,1,24,24,3,1,6,2),
	(181,1,24,24,3,1,3,32),
	(182,1,24,24,3,4,11,6),
	(183,1,24,24,3,4,10,7),
	(184,1,24,24,3,2,2,16),
	(185,1,25,25,4,4,16,40),
	(186,1,25,25,4,4,14,2),
	(187,1,25,25,4,4,11,10),
	(188,1,25,25,4,4,10,5),
	(189,1,25,25,4,3,15,2),
	(190,1,25,25,4,3,4,6),
	(191,1,25,25,4,3,3,9),
	(192,1,26,26,5,4,16,28),
	(193,1,26,26,5,4,14,3),
	(194,1,26,26,5,4,11,6),
	(195,1,26,26,5,3,18,6),
	(196,1,26,26,5,3,15,16),
	(197,1,26,26,5,3,5,15),
	(198,1,26,26,5,3,17,7);

/*!40000 ALTER TABLE `activity_log` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table activity_times
# ------------------------------------------------------------

DROP TABLE IF EXISTS `activity_times`;

CREATE TABLE `activity_times` (
  `activity_time_id` int(11) NOT NULL,
  `time_spent_on_activity` double NOT NULL,
  PRIMARY KEY (`activity_time_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `activity_times` WRITE;
/*!40000 ALTER TABLE `activity_times` DISABLE KEYS */;

INSERT INTO `activity_times` (`activity_time_id`, `time_spent_on_activity`)
VALUES
	(1,0.25),
	(2,0.5),
	(3,0.75),
	(4,1),
	(5,1.25),
	(6,1.5),
	(7,1.75),
	(8,2),
	(9,2.25),
	(10,2.5),
	(11,2.75),
	(12,3),
	(13,3.25),
	(14,3.5),
	(15,3.75),
	(16,4),
	(17,4.25),
	(18,4.5),
	(19,4.75),
	(20,5),
	(21,5.25),
	(22,5.5),
	(23,5.75),
	(24,6),
	(25,6.25),
	(26,6.5),
	(27,6.75),
	(28,7),
	(29,7.25),
	(30,7.5),
	(31,7.75),
	(32,8),
	(33,8.25),
	(34,8.5),
	(35,8.75),
	(36,9),
	(37,9.25),
	(38,9.5),
	(39,9.75),
	(40,10),
	(41,10.25),
	(42,10.5),
	(43,10.75),
	(44,11),
	(45,11.25),
	(46,11.5),
	(47,11.75),
	(48,12),
	(49,12.25),
	(50,12.5),
	(51,12.75),
	(52,13),
	(53,13.25),
	(54,13.5),
	(55,13.75),
	(56,14),
	(57,14.25),
	(58,14.5),
	(59,14.75),
	(60,15),
	(61,15.25),
	(62,15.5),
	(63,15.75),
	(64,16),
	(65,16.25),
	(66,16.5),
	(67,16.75),
	(68,17),
	(69,17.25),
	(70,17.5),
	(71,17.75),
	(72,18),
	(73,18.25),
	(74,18.5),
	(75,18.75),
	(76,19),
	(77,19.25),
	(78,19.5),
	(79,19.75),
	(80,20),
	(81,20.25),
	(82,20.5),
	(83,20.75),
	(84,21),
	(85,21.25),
	(86,21.5),
	(87,21.75),
	(88,22),
	(89,22.25),
	(90,22.5),
	(91,22.75),
	(92,23),
	(93,23.25),
	(94,23.5),
	(95,23.75),
	(96,24);

/*!40000 ALTER TABLE `activity_times` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table days_of_week
# ------------------------------------------------------------

DROP TABLE IF EXISTS `days_of_week`;

CREATE TABLE `days_of_week` (
  `day_id` int(11) NOT NULL,
  `day_name` varchar(10) NOT NULL,
  PRIMARY KEY (`day_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `days_of_week` WRITE;
/*!40000 ALTER TABLE `days_of_week` DISABLE KEYS */;

INSERT INTO `days_of_week` (`day_id`, `day_name`)
VALUES
	(1,'Monday'),
	(2,'Tuesday'),
	(3,'Wednesday'),
	(4,'Thursday'),
	(5,'Friday'),
	(6,'Saturday'),
	(7,'Sunday');

/*!40000 ALTER TABLE `days_of_week` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table time_tracker_users
# ------------------------------------------------------------

DROP TABLE IF EXISTS `time_tracker_users`;

CREATE TABLE `time_tracker_users` (
  `user_id` int(11) NOT NULL,
  `user_password` varchar(10) NOT NULL,
  `user_firstname` varchar(25) NOT NULL,
  `user_lastname` varchar(25) NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `time_tracker_users` WRITE;
/*!40000 ALTER TABLE `time_tracker_users` DISABLE KEYS */;

INSERT INTO `time_tracker_users` (`user_id`, `user_password`, `user_firstname`, `user_lastname`)
VALUES
	(1,'password','Kinaole','Lau'),
	(2,'password','admin','admin'),
	(3,'password','instructor','instructor');

/*!40000 ALTER TABLE `time_tracker_users` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table tracked_calendar_dates
# ------------------------------------------------------------

DROP TABLE IF EXISTS `tracked_calendar_dates`;

CREATE TABLE `tracked_calendar_dates` (
  `calendar_date_id` int(11) NOT NULL,
  `calendar_date` date NOT NULL,
  PRIMARY KEY (`calendar_date_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `tracked_calendar_dates` WRITE;
/*!40000 ALTER TABLE `tracked_calendar_dates` DISABLE KEYS */;

INSERT INTO `tracked_calendar_dates` (`calendar_date_id`, `calendar_date`)
VALUES
	(1,'2019-05-06'),
	(2,'2019-05-07'),
	(3,'2019-05-08'),
	(4,'2019-05-09'),
	(5,'2019-05-10'),
	(6,'2019-05-11'),
	(7,'2019-05-12'),
	(8,'2019-05-13'),
	(9,'2019-05-14'),
	(10,'2019-05-15'),
	(11,'2019-05-16'),
	(12,'2019-05-17'),
	(13,'2019-05-18'),
	(14,'2019-05-19'),
	(15,'2019-05-20'),
	(16,'2019-05-21'),
	(17,'2019-05-22'),
	(18,'2019-05-23'),
	(19,'2019-05-24'),
	(20,'2019-05-25'),
	(21,'2019-05-26'),
	(22,'2019-05-27'),
	(23,'2019-05-28'),
	(24,'2019-05-29'),
	(25,'2019-05-30'),
	(26,'2019-05-31');

/*!40000 ALTER TABLE `tracked_calendar_dates` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table tracked_calendar_days
# ------------------------------------------------------------

DROP TABLE IF EXISTS `tracked_calendar_days`;

CREATE TABLE `tracked_calendar_days` (
  `calendar_day_id` int(11) NOT NULL,
  `calendar_numerical_day` int(11) NOT NULL,
  PRIMARY KEY (`calendar_day_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `tracked_calendar_days` WRITE;
/*!40000 ALTER TABLE `tracked_calendar_days` DISABLE KEYS */;

INSERT INTO `tracked_calendar_days` (`calendar_day_id`, `calendar_numerical_day`)
VALUES
	(1,1),
	(2,2),
	(3,3),
	(4,4),
	(5,5),
	(6,6),
	(7,7),
	(8,8),
	(9,9),
	(10,10),
	(11,11),
	(12,12),
	(13,13),
	(14,14),
	(15,15),
	(16,16),
	(17,17),
	(18,18),
	(19,19),
	(20,20),
	(21,21),
	(22,22),
	(23,23),
	(24,24),
	(25,25),
	(26,26);

/*!40000 ALTER TABLE `tracked_calendar_days` ENABLE KEYS */;
UNLOCK TABLES;



/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

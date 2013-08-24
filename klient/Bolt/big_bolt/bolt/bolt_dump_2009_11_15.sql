-- MySQL dump 10.11
--
-- Host: localhost    Database: bolt
-- ------------------------------------------------------
-- Server version	5.0.75-0ubuntu10.2

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bills`
--

DROP TABLE IF EXISTS `bills`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `bills` (
  `id` int(10) unsigned NOT NULL auto_increment COMMENT 'id of current info',
  `comp_id` int(10) unsigned default NULL COMMENT 'Computer ID (from "Computers" table)',
  `time_used` int(10) unsigned default NULL COMMENT 'time (in seconds) from which computer was used',
  `money` int(10) unsigned default NULL COMMENT 'Money in RUR',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COMMENT='Billing info from comps';
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `bills`
--

--
-- Table structure for table `computers`
--

DROP TABLE IF EXISTS `computers`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `computers` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `ip` varchar(15) NOT NULL,
  `lifepercent` smallint(6) NOT NULL default '0',
  `lastupdate` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `state` tinyint(1) NOT NULL default '0',
  `broken` int(1) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `computers`
--

-- LOCK TABLES `computers` WRITE;
-- /*!40000 ALTER TABLE `computers` DISABLE KEYS */;
-- INSERT INTO `computers` VALUES (6,'10.42.43.12',0,'2009-11-14 20:36:11',0,NULL),(5,'10.42.43.11',7,'2009-11-14 21:27:42',1,1),(7,'10.42.43.13',91,'2009-11-14 21:13:36',1,NULL),(8,'10.42.43.14',0,'2009-11-14 20:30:38',0,NULL),(9,'10.42.43.15',77,'2009-11-14 21:30:00',1,0),(10,'10.42.43.3',100,'2009-11-14 20:34:05',0,0);
-- /*!40000 ALTER TABLE `computers` ENABLE KEYS */;
-- UNLOCK TABLES;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2009-11-15 21:32:53

-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: taskify_api
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `taskitems`
--

CREATE DATABASE taskify_api;
USE taskify_api;

DROP TABLE IF EXISTS `taskitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `taskitems` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `State` int NOT NULL,
  `Title` varchar(255) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `StartDateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `EndDateTime` datetime NOT NULL,
  `IsAllDay` tinyint(1) NOT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Updated` datetime DEFAULT NULL,
  `Deleted` datetime DEFAULT NULL,
  `CreatedUserId` int NOT NULL,
  `UpdatedUserId` int DEFAULT NULL,
  `DeletedUserId` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `taskitems`
--

LOCK TABLES `taskitems` WRITE;
/*!40000 ALTER TABLE `taskitems` DISABLE KEYS */;
INSERT INTO `taskitems` VALUES (100,1,'Primer tarea','Modificada por Goyo.','2023-07-12 15:30:01','2023-07-12 16:30:01',0,'2023-07-12 16:00:34','2023-07-14 16:11:22',NULL,1,113,NULL),(101,0,'Segunda tarea','Segundo POST de tarea de Taskify API. Fecha de inicio y fin completas.','2023-07-12 16:00:00','2023-07-12 17:00:00',0,'2023-07-12 16:26:26','2023-07-13 11:33:56','2023-07-13 12:41:20',1,1,1),(102,1,'Tercer tarea','Terce POST de tarea de Taskify API. Fecha de inicio completa y fin vacia.','2023-07-12 16:00:00','2023-07-12 20:00:00',0,'2023-07-13 12:04:24',NULL,NULL,1,NULL,NULL),(103,1,'Cuarta tarea','Cuarto POST de tarea de Taskify API. Fecha de inicio completa y fin vacia.','2023-07-12 16:00:00','2023-07-12 20:00:00',0,'2023-07-13 12:15:15',NULL,NULL,1,NULL,NULL),(104,1,'Quinta tarea','Cuarto POST de tarea de Taskify API. Fecha de inicio completa y fin vacia.','2023-07-12 16:00:00','2023-07-12 20:00:00',0,'2023-07-13 12:25:34',NULL,NULL,1,NULL,NULL),(105,1,'Sexta tarea','Cuarto POST de tarea de Taskify API. Fecha de inicio completa y fin vacia.','2023-07-12 16:00:00','2023-07-12 20:00:00',0,'2023-07-13 12:27:26',NULL,NULL,1,NULL,NULL),(106,1,'Septima tarea','Cuarto POST de tarea de Taskify API. Fecha de inicio completa y fin vacia.','2023-07-12 00:00:00','2023-07-13 00:00:00',1,'2023-07-13 12:37:53','2023-07-13 12:40:25',NULL,1,1,NULL);
/*!40000 ALTER TABLE `taskitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `State` int NOT NULL DEFAULT '1',
  `LastName` varchar(50) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `Salt` varchar(100) NOT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Updated` datetime DEFAULT NULL,
  `Deleted` datetime DEFAULT NULL,
  `CreatedUserId` int NOT NULL,
  `UpdatedUserId` int DEFAULT NULL,
  `DeletedUserId` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=115 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (113,1,'Degano','Goyo','gdegano@bandaloschinos.com','JOS62X1JAEMBAVx+yaPyzgvQ+Kom1qXjJvdZgvXPR0FA3qX2kSro3kD54yNI1ZSK','JOS62X1JAEMBAVx+yaPyzg==','2023-07-11 15:28:46','2023-07-11 16:00:54',NULL,1,1,NULL),(114,1,'Mollo','Ricardo','rmollo@divididos.com','hYJar7dpR/LWN3LY/VlNNFXs54V56b5/zFPurv9GjKcrZG/WvSGcvl+qdClLovbN','hYJar7dpR/LWN3LY/VlNNA==','2023-07-11 15:39:14','2023-07-11 15:56:16',NULL,1,1,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-14 16:14:45

-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           11.5.2-MariaDB - mariadb.org binary distribution
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para desafio
CREATE DATABASE IF NOT EXISTS `desafio` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci */;
USE `desafio`;

-- Copiando estrutura para tabela desafio.tarefas
CREATE TABLE IF NOT EXISTS `tarefas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `titulo` varchar(40) DEFAULT NULL,
  `mensagem` varchar(300) DEFAULT NULL,
  `funcResp` varchar(40) DEFAULT NULL,
  `estado` varchar(30) DEFAULT NULL,
  `prazo` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Copiando dados para a tabela desafio.tarefas: ~0 rows (aproximadamente)

-- Copiando estrutura para tabela desafio.usuario
CREATE TABLE IF NOT EXISTS `usuario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(60) NOT NULL,
  `nasec` date NOT NULL,
  `telefone` int(11) NOT NULL,
  `celular` int(11) NOT NULL,
  `email` varchar(45) NOT NULL,
  `senha` varchar(30) NOT NULL,
  `rua` varchar(45) NOT NULL,
  `num` int(11) NOT NULL,
  `tipo` tinyint(1) DEFAULT NULL,
  `foto` longblob NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UC_Email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Copiando dados para a tabela desafio.usuario: ~1 rows (aproximadamente)
INSERT INTO `usuario` (`id`, `nome`, `nasec`, `telefone`, `celular`, `email`, `senha`, `rua`, `num`, `tipo`, `foto`) VALUES
	(1, 'Gestor Pastelaria', '2024-09-19', 0, 900000000, 'oportunidades@smn.com.br', 'teste123', 'Rua dos Gestores', 90, 0, _binary '');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

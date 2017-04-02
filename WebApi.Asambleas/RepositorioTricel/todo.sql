/*
Navicat MySQL Data Transfer

Source Server         : sql
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : db_9dac90_cole

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-01-28 22:38:55
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for arc_archivos_proyecto
-- ----------------------------
DROP TABLE IF EXISTS `arc_archivos_proyecto`;
CREATE TABLE `arc_archivos_proyecto` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RUTA_ARCHIVO` varchar(500) DEFAULT NULL,
  `PRO_ID` int(11) DEFAULT '0',
  `ELIMINADO` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of arc_archivos_proyecto
-- ----------------------------
INSERT INTO `arc_archivos_proyecto` VALUES ('1', '~/ArchivosProyectos/ajj0optt.hqj DTO-732 11-FEB-1998-Estatutos Tipos.pdf', '1', '0');
INSERT INTO `arc_archivos_proyecto` VALUES ('2', '~/ArchivosProyectos/0uglmfgv.l2e guia-propiedad-intelectual.pdf', '7', '0');

-- ----------------------------
-- Table structure for arti_articulo
-- ----------------------------
DROP TABLE IF EXISTS `arti_articulo`;
CREATE TABLE `arti_articulo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `VISIBLE` int(11) NOT NULL DEFAULT '0',
  `USA_IMAGEN` int(11) NOT NULL DEFAULT '0',
  `URL_IMAGEN` varchar(500) NOT NULL DEFAULT '0',
  `FECHA` varchar(250) NOT NULL DEFAULT '0',
  `USA_FECHA` int(11) NOT NULL DEFAULT '0',
  `USA_TITULO` int(11) NOT NULL DEFAULT '0',
  `TITULO` varchar(1000) NOT NULL DEFAULT '0',
  `CONTENIDO` varchar(5000) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `TIPO_ARTICULO` int(11) DEFAULT '1',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of arti_articulo
-- ----------------------------
INSERT INTO `arti_articulo` VALUES ('1', '0', '1', '~/img/imgArticulo_1.png', '14-12-2015', '0', '1', '¿QUIENES SOMOS?', 'Una plataforma de gestión para los centros de padres y apoderados de cualquier tipo de instituciones o establecimiento. \nEl foco fundamental esta centrado en la gestión de las actividades del centro de padres y la trasnparencia de los estados de cuentas y flujos economicos producto de la gestion del centro de padres y apoderados. ', '3', '1', '0');
INSERT INTO `arti_articulo` VALUES ('2', '0', '1', '~/img/imgArticulo_2.png', '14-12-2015', '0', '1', 'NUESTRA VISIÓN', 'Ser la mejor mejor herramienta de gestion y transparencia para los centros de padres y apoderados asi también un mecanismo moderno de interacción educativa entre los establecimientos y la comunidad estudiantil. ', '3', '1', '0');
INSERT INTO `arti_articulo` VALUES ('3', '0', '1', '~/img/imgArticulo_3.png', '14-12-2015', '0', '1', 'NUESTROS SERVICIOS', 'Nuestro servico consta de perfiles y roles de acuerdo a la orgánica de administración de los centros de padres y apoderados así como también un canal directo de transparencia e información al apoderado y el establecimiento.', '3', '1', '0');
INSERT INTO `arti_articulo` VALUES ('4', '0', '1', '~/img/imgArticulo_4.png', '14-12-2015', '0', '1', 'NUESTRO COLEGIO', 'El Colegio Santa Elena fue fundado el año 1913 por las Hermanas Carmelitas de la Caridad. Estos 100 años de existencia nos avalan como una institución que ha estado permanentemente preocupada por la educación de niñas y jóvenes, formando mujeres líderes de nuestra sociedad, lo que hoy significa seguir aspirando a que ellas logren la excelencia humana, mediante el desarrollo de sus habilidades y capacidades personales.', '4', '1', '0');
INSERT INTO `arti_articulo` VALUES ('5', '0', '1', '~/img/imgArticulo_5.png', '14-12-2015', '0', '1', 'CENTRO DE PADRES', 'El Centro de Padres y Apoderados del Colegio (CPA) nace el año 2003, producto de la fusión de los Centros de Padres y Apoderados de las distintas sedes.', '4', '1', '0');
INSERT INTO `arti_articulo` VALUES ('6', '0', '1', '~/img/imgArticulo_6.png', '14-12-2015', '0', '1', 'MISIÓN', 'Nuestra misión es formar mujeres líderes, comprometidas con los valores del Evangelio, con un profundo sentimiento de gratitud y amor por la vida, que aspiren a la excelencia humana, mediante el desarrollo de las habilidades y capacidades personales. Ésta se promueve en un estilo de vida comunitario, favorecido por un clima afectivo-familiar.', '4', '1', '0');
INSERT INTO `arti_articulo` VALUES ('7', '0', '1', '~/img/imgArticulo_4.png', '14-12-2015', '0', '1', 'NUESTRO COLEGIO', 'El Colegio Santa Elena fue fundado el año 1913 por las Hermanas Carmelitas de la Caridad. Estos 100 años de existencia nos avalan como una institución que ha estado permanentemente preocupada por la educación de niñas y jóvenes, formando mujeres líderes de nuestra sociedad, lo que hoy significa seguir aspirando a que ellas logren la excelencia humana, mediante el desarrollo de sus habilidades y capacidades personales.', '5', '1', '0');
INSERT INTO `arti_articulo` VALUES ('8', '0', '1', '~/img/imgArticulo_5.png', '14-12-2015', '0', '1', 'CENTRO DE PADRES', 'El Centro de Padres y Apoderados del Colegio (CPA) nace el año 2003, producto de la fusión de los Centros de Padres y Apoderados de las distintas sedes.', '5', '1', '0');
INSERT INTO `arti_articulo` VALUES ('9', '0', '1', '~/img/imgArticulo_6.png', '14-12-2015', '0', '1', 'MISIÓN', 'Nuestra misión es formar mujeres líderes, comprometidas con los valores del Evangelio, con un profundo sentimiento de gratitud y amor por la vida, que aspiren a la excelencia humana, mediante el desarrollo de las habilidades y capacidades personales. Ésta se promueve en un estilo de vida comunitario, favorecido por un clima afectivo-familiar.', '5', '1', '0');

-- ----------------------------
-- Table structure for atr_archivos_tricel
-- ----------------------------
DROP TABLE IF EXISTS `atr_archivos_tricel`;
CREATE TABLE `atr_archivos_tricel` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RUTA_ARCHIVO` varchar(500) DEFAULT NULL,
  `TRI_ID` int(11) DEFAULT '0',
  `ELIMINADO` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of atr_archivos_tricel
-- ----------------------------

-- ----------------------------
-- Table structure for au_autentificacion_usuario
-- ----------------------------
DROP TABLE IF EXISTS `au_autentificacion_usuario`;
CREATE TABLE `au_autentificacion_usuario` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NOMBRE_USUARIO` varchar(250) NOT NULL DEFAULT '0',
  `PASSWORD` varchar(250) NOT NULL DEFAULT '0',
  `CORREO_ELECTRONICO` varchar(250) NOT NULL DEFAULT '0',
  `ROL_ID` int(11) NOT NULL DEFAULT '0',
  `ES_VIGENTE` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of au_autentificacion_usuario
-- ----------------------------
INSERT INTO `au_autentificacion_usuario` VALUES ('1', 'vcoronado', 'MQAyADMANAA1ADYA', 'vcoronado.alarcon@gmail.com', '1', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('4', 'Vgarcia', 'MQAyADMANAA1ADYA', 'turck182@gmail.com', '5', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('5', 'vcoronado3', '123456', 'vcoronado.alarcon@gmail.com', '3', '0', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('6', 'vgarciaadm', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '2', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('7', 'vgarcia1', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '3', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('8', 'vgarcia2', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '4', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('9', 'vgarcia3', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '5', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('10', 'vgarcia4', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '9', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('11', 'vgarcia5', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '9', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('12', 'vgarcia6', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.com', '9', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('13', 'ppprueba', 'MQAwADQANQAyADYAOQAxADgA', 'vcoronado@saydex.cl', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('14', 'iretamalesPrueba', 'MQAyADEAOQAzADAAMQA0ADUA', 'vcoronado.alarcon@gmail.com', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('15', 'otroPrueba', 'MQAwADQANQAyADYAOQAxADgA', 'vcoronado@saydex.cl', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('16', 'ppablo', 'MQAwADQANQAyADYAOQAxADgA', 'vcoronado@saydex.cl', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('17', 'iretamales', 'MQAyADEAOQAzADAAMQA0ADUA', 'vcoronado.alarcon@gmail.com', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('18', 'vgarciasuper', 'MQAyADMANAA1ADYA', 'vgarcia@saydex.cl', '1', '1', '5', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('19', 'dsalazar', 'MQAzADUAOQA3ADQAMwA0AGsA', 'danny_salazaro@hotmail.com', '9', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('20', 'vigarci', 'MQAxADEAMQAxADEAMQAxADEA', 'turck182@gmail.com', '9', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('21', 'SecretariaCGPA', 'MQAyADMANAA1ADYA', 'danny_salaro@hotmail.com', '5', '1', '4', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('22', 'mabarca', 'OQAxADYANgAyADEAMwAyAA==', 'abarcamiguel63@gmail.com', '3', '1', '5', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('23', 'mquiroga', 'MQAyADIAMwA5ADIANAA0ADkA', 'marcia_quiroga_m@hotmail.com', '4', '1', '5', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('24', 'srojas', 'MQAyADEAMgA2ADUAOQA0AGsA', 'rojassandr@gmail.com', '5', '1', '5', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('25', 'dlabbe', 'MQA0ADAANQAyADUANwA4ADIA', 'daniela_labbe@hotmail.com', '10', '1', '5', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('26', 'cABmAHUAZQBuAHQAZQBzAG8AcgB0AGUAZwBhAA==', '123456', 'pfuentesortega98@gmail.com', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('27', 'cABmAHUAZQBuAHQAZQBzAG8AcgB0AGUAZwBhAA==', '123456', 'pfuentesortega98@gmail.com', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('28', 'cABmAHUAZQBuAHQAZQBzAG8AcgB0AGUAZwBhAA==', '123456', 'pfuentesortega98@gmail.com', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('29', 'dgBjAG8AcgBvAG4AYQBkAG8AYwBwAGEAcwA=', '123456', 'vcoronado.alarcon@gmail.com', '9', '1', '3', '0');
INSERT INTO `au_autentificacion_usuario` VALUES ('30', 'dgBjAG8AcgBvAG4AYQBkAG8AYwBwAGEAcwA=', '', 'vcoronado.alarcon@gmail.com', '9', '1', '3', '0');

-- ----------------------------
-- Table structure for cal_calendario
-- ----------------------------
DROP TABLE IF EXISTS `cal_calendario`;
CREATE TABLE `cal_calendario` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TITULO` varchar(250) DEFAULT ' ',
  `URL` varchar(250) DEFAULT ' ',
  `FECHA_INICIO` datetime DEFAULT CURRENT_TIMESTAMP,
  `FECHA_TERMINO` datetime DEFAULT CURRENT_TIMESTAMP,
  `DETALLE` varchar(500) DEFAULT ' ',
  `ELIMINADO` int(11) DEFAULT '0',
  `INST_ID` int(11) DEFAULT '0',
  `ASUNTO` varchar(500) DEFAULT ' ',
  `UBICACION` varchar(500) DEFAULT ' ',
  `ETIQUETA` int(11) DEFAULT '0',
  `DESCRIPCION` varchar(500) DEFAULT ' ',
  `STATUS` int(11) DEFAULT '0',
  `TIPO` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cal_calendario
-- ----------------------------
INSERT INTO `cal_calendario` VALUES ('1', '', '', '2015-12-16 01:00:00', '2015-12-16 03:00:00', '', '0', '3', 'Prueba', 'Sala 213', '1', 'Prueba', '0', '0');
INSERT INTO `cal_calendario` VALUES ('2', '', '', '2015-12-17 15:00:00', '2015-12-17 19:00:00', '', '0', '3', 'Prueba', 'Local', '3', 'Detalle ', '0', '0');
INSERT INTO `cal_calendario` VALUES ('3', '', '', '2015-12-18 15:00:00', '2015-12-18 18:00:00', '', '0', '3', 'nueva cita', 'local 2', '1', 'prueba de detalle', '0', '0');
INSERT INTO `cal_calendario` VALUES ('4', 'Prueba de calendario', '', '2015-12-17 00:00:00', '2015-12-18 00:00:00', 'Prueba de calendario', '1', '4', 'pruebs', 'local', '1', 'Prueba de calendario', '0', '0');
INSERT INTO `cal_calendario` VALUES ('5', 'breve', '', '2015-12-15 14:00:00', '2015-12-15 15:00:00', 'breve', '0', '3', 'prueba de asunto nuevo', 'en la empresa', '2', 'breve', '0', '0');
INSERT INTO `cal_calendario` VALUES ('6', 'Tricel 1', '', '2016-02-14 00:00:00', '2016-02-29 00:00:00', 'Tricel 1', '0', '3', 'Tricel 1', 'NO DEFINIDA', '1', 'Objetivo', '1', '1');
INSERT INTO `cal_calendario` VALUES ('7', 'Tricel 1', '', '2016-02-17 00:00:00', '2016-02-29 00:00:00', 'Tricel 1', '0', '4', 'Tricel 1', 'NO DEFINIDA', '1', 'objetivo 1', '1', '1');
INSERT INTO `cal_calendario` VALUES ('8', 'prueba', '', '2016-03-06 00:00:00', '2016-03-30 00:00:00', 'prueba', '0', '3', 'prueba', 'NO DEFINIDA', '1', 'Objetivo prueba', '1', '1');
INSERT INTO `cal_calendario` VALUES ('9', 'proyecto coro', '', '2016-03-09 00:00:00', '2016-03-30 00:00:00', 'Desc', '0', '3', 'proyecto coro', 'NO DEFINIDA', '1', 'Desc', '1', '1');
INSERT INTO `cal_calendario` VALUES ('10', 'Triple coro', '', '2016-03-08 00:00:00', '2016-03-29 00:00:00', 'Triple coro', '0', '3', 'Triple coro', 'NO DEFINIDA', '1', 'Obj', '1', '1');
INSERT INTO `cal_calendario` VALUES ('11', 'pppp', '', '2016-03-08 00:00:00', '2016-03-30 00:00:00', 'pppp', '0', '3', 'pppp', 'NO DEFINIDA', '1', 'Obj', '1', '1');
INSERT INTO `cal_calendario` VALUES ('12', 'coro', '', '2016-03-11 00:00:00', '2016-03-29 00:00:00', 'coro', '0', '3', 'coro', 'NO DEFINIDA', '1', 'Objetivo', '1', '1');
INSERT INTO `cal_calendario` VALUES ('13', ' presentar los lineamientos y énfasis para este año escolar .', '', '2016-03-19 09:00:00', '2016-03-19 12:00:00', ' presentar los lineamientos y énfasis para este año escolar .', '0', '4', 'Asamblea General 2016', 'Colegio de niñas Santa Elena', '0', ' presentar los lineamientos y énfasis para este año escolar .', '0', '0');
INSERT INTO `cal_calendario` VALUES ('14', '', '', '2016-04-02 10:00:00', '2016-04-02 16:00:00', '', '0', '4', 'Tallarinata Ramas deportivas Santa Elena ', 'Colegio Santa Elena', '0', 'Feria de las pulgas y 1ra tallarinata en pro de las ramas deportivas del Colegio Santa Elena', '0', '0');
INSERT INTO `cal_calendario` VALUES ('15', 'TABLA:\n1. Rendición de cuentas directiva anterior\n2. Tallarinata\n3. Comunicación directa con las ramas\n4. Mejorar la comunicación actualizando los correos de las directivas actuales, para que no haya malos entendidos\n\nNOTA:EL COLEGIO DISPUSO UNA HORA', '', '2016-04-04 19:30:00', '2016-04-04 20:30:00', 'TABLA:\n1. Rendición de cuentas directiva anterior\n2. Tallarinata\n3. Comunicación directa con las ramas\n4. Mejorar la comunicación actualizando los correos de las directivas actuales, para que no haya malos entendidos\n\nNOTA:EL COLEGIO DISPUSO UNA HORA DE REUNIÓN  DESDE LAS 19:30 A LAS 20:30 POR PROBLEMAS DE POCO PERSONAL  POR ESO SE RUEGA \nPUNTUALIDAD\n', '0', '4', ' 2da Reunión  CGPA', 'Colegio Santa Elena', '0', 'TABLA:\n1. Rendición de cuentas directiva anterior\n2. Tallarinata\n3. Comunicación directa con las ramas\n4. Mejorar la comunicación actualizando los correos de las directivas actuales, para que no haya malos entendidos\n\nNOTA:EL COLEGIO DISPUSO UNA HORA DE REUNIÓN  DESDE LAS 19:30 A LAS 20:30 POR PROBLEMAS DE POCO PERSONAL  POR ESO SE RUEGA \nPUNTUALIDAD\n', '0', '0');
INSERT INTO `cal_calendario` VALUES ('16', 'prueba', '', '2016-04-07 00:00:00', '2016-04-08 00:00:00', 'prueba', '0', '3', 'prueba', 'local', '1', 'prueba', '0', '0');
INSERT INTO `cal_calendario` VALUES ('17', 'evento de prueba', '', '2016-04-07 10:00:00', '2016-04-07 12:00:00', 'evento de prueba', '0', '4', 'otro evento', 'Colegio Santa Elena', '0', 'evento de prueba', '0', '0');
INSERT INTO `cal_calendario` VALUES ('18', 'Instalación de graderías en patio principal', '', '2016-04-14 00:00:00', '2016-04-22 00:00:00', 'Juegos de graderías para el patio', '0', '4', 'Instalación de graderías en patio principal', 'NO DEFINIDA', '1', 'Juegos de graderías para el patio', '1', '1');
INSERT INTO `cal_calendario` VALUES ('19', 'Prueba de cita', '', '2016-04-16 13:00:00', '2016-04-16 14:00:00', 'Prueba de cita', '0', '4', 'Prueba de cita', 'En el establecimiento', '1', 'Prueba de cita', '0', '0');
INSERT INTO `cal_calendario` VALUES ('20', 'prueba', '', '2016-04-16 13:00:00', '2016-04-16 14:00:00', 'prueba', '0', '4', 'Otra Prueba', 'en el local', '1', 'prueba', '0', '0');
INSERT INTO `cal_calendario` VALUES ('21', 'Hola', '', '2016-04-15 18:30:00', '2016-04-15 20:00:00', 'Hola', '0', '4', 'Presentación plataforma cpas', 'Santa elena', '2', 'Hola', '1', '0');
INSERT INTO `cal_calendario` VALUES ('22', 'asasasas', '', '2016-06-28 00:00:00', '2016-06-29 00:00:00', 'asasasas', '0', '3', 'asasasas', 'asasasas', '0', 'asasasas', '0', '0');
INSERT INTO `cal_calendario` VALUES ('23', 'DEMO proyecto', '', '2016-11-04 00:00:00', '2016-11-11 00:00:00', 'Este proyecto tiene como foco poder entregar funciones de valor que puedan hacer match con CitiZen', '0', '4', 'DEMO proyecto', 'NO DEFINIDA', '1', 'Este proyecto tiene como foco poder entregar funciones de valor que puedan hacer match con CitiZen', '1', '1');
INSERT INTO `cal_calendario` VALUES ('24', 'prueba de desc', '', '2016-11-24 00:00:00', '2016-11-25 00:00:00', 'prueba de desc', '0', '3', 'Prueba', 'en mi casa', '0', 'prueba de desc', '0', '0');
INSERT INTO `cal_calendario` VALUES ('25', 'Votación PRIMERA LISTA', '', '2017-01-01 00:00:00', '2017-01-14 00:00:00', 'Votación PRIMERA LISTA', '0', '3', 'Votación PRIMERA LISTA', 'NO DEFINIDA', '1', 'votar por la lista', '1', '1');
INSERT INTO `cal_calendario` VALUES ('26', 'Prueba de creación de evento\n', '', '2016-12-05 00:00:00', '2016-12-20 00:00:00', 'Prueba de creación de evento\n', '0', '3', 'prueba de evento', 'local', '1', 'Prueba de creación de evento\n', '1', '0');
INSERT INTO `cal_calendario` VALUES ('27', 'Proyecto de Prueba', '', '2017-01-01 00:00:00', '2017-01-31 00:00:00', 'Prueba', '0', '3', 'Proyecto de Prueba', 'NO DEFINIDA', '1', 'Proyecto de Prueba', '1', '1');
INSERT INTO `cal_calendario` VALUES ('28', 'Proyecto de Prueba', '', '2017-01-07 00:00:00', '2017-01-31 00:00:00', 'Prueba', '0', '3', 'Proyecto de Prueba', 'NO DEFINIDA', '1', 'Prueba', '1', '1');

-- ----------------------------
-- Table structure for com_comuna
-- ----------------------------
DROP TABLE IF EXISTS `com_comuna`;
CREATE TABLE `com_comuna` (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID unico de la comuna',
  `PROV_ID` int(11) NOT NULL COMMENT 'ID de la provincia asociada',
  `NOMBRE` varchar(30) COLLATE latin1_spanish_ci DEFAULT NULL COMMENT 'Nombre descriptivo de la comuna',
  PRIMARY KEY (`ID`,`PROV_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=347 DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci COMMENT='Lista de comunas por provincias';

-- ----------------------------
-- Records of com_comuna
-- ----------------------------
INSERT INTO `com_comuna` VALUES ('1', '1', 'ARICA');
INSERT INTO `com_comuna` VALUES ('2', '1', 'CAMARONES');
INSERT INTO `com_comuna` VALUES ('3', '2', 'PUTRE');
INSERT INTO `com_comuna` VALUES ('4', '2', 'GENERAL LAGOS');
INSERT INTO `com_comuna` VALUES ('5', '3', 'IQUIQUE');
INSERT INTO `com_comuna` VALUES ('6', '3', 'ALTO HOSPICIO');
INSERT INTO `com_comuna` VALUES ('7', '4', 'POZO ALMONTE');
INSERT INTO `com_comuna` VALUES ('8', '4', 'CAMIÑA');
INSERT INTO `com_comuna` VALUES ('9', '4', 'COLCHANE');
INSERT INTO `com_comuna` VALUES ('10', '4', 'HUARA');
INSERT INTO `com_comuna` VALUES ('11', '4', 'PICA');
INSERT INTO `com_comuna` VALUES ('12', '5', 'ANTOFAGASTA');
INSERT INTO `com_comuna` VALUES ('13', '5', 'MEJILLONES');
INSERT INTO `com_comuna` VALUES ('14', '5', 'SIERRA GORDA');
INSERT INTO `com_comuna` VALUES ('15', '5', 'TALTAL');
INSERT INTO `com_comuna` VALUES ('16', '6', 'CALAMA');
INSERT INTO `com_comuna` VALUES ('17', '6', 'OLLAGÜE');
INSERT INTO `com_comuna` VALUES ('18', '6', 'SAN PEDRO DE ATACAMA');
INSERT INTO `com_comuna` VALUES ('19', '7', 'TOCOPILLA');
INSERT INTO `com_comuna` VALUES ('20', '7', 'MARÍA ELENA');
INSERT INTO `com_comuna` VALUES ('21', '8', 'COPIAPÓ');
INSERT INTO `com_comuna` VALUES ('22', '8', 'CALDERA');
INSERT INTO `com_comuna` VALUES ('23', '8', 'TIERRA AMARILLA');
INSERT INTO `com_comuna` VALUES ('24', '9', 'CHAÑARAL');
INSERT INTO `com_comuna` VALUES ('25', '9', 'DIEGO DE ALMAGRO');
INSERT INTO `com_comuna` VALUES ('26', '10', 'VALLENAR');
INSERT INTO `com_comuna` VALUES ('27', '10', 'ALTO DEL CARMEN');
INSERT INTO `com_comuna` VALUES ('28', '10', 'FREIRINA');
INSERT INTO `com_comuna` VALUES ('29', '10', 'HUASCO');
INSERT INTO `com_comuna` VALUES ('30', '11', 'LA SERENA');
INSERT INTO `com_comuna` VALUES ('31', '11', 'COQUIMBO');
INSERT INTO `com_comuna` VALUES ('32', '11', 'ANDACOLLO');
INSERT INTO `com_comuna` VALUES ('33', '11', 'LA HIGUERA');
INSERT INTO `com_comuna` VALUES ('34', '11', 'PAIGUANO');
INSERT INTO `com_comuna` VALUES ('35', '11', 'VICUÑA');
INSERT INTO `com_comuna` VALUES ('36', '12', 'ILLAPEL');
INSERT INTO `com_comuna` VALUES ('37', '12', 'CANELA');
INSERT INTO `com_comuna` VALUES ('38', '12', 'LOS VILOS');
INSERT INTO `com_comuna` VALUES ('39', '12', 'SALAMANCA');
INSERT INTO `com_comuna` VALUES ('40', '13', 'OVALLE');
INSERT INTO `com_comuna` VALUES ('41', '13', 'COMBARBALÁ');
INSERT INTO `com_comuna` VALUES ('42', '13', 'MONTE PATRIA');
INSERT INTO `com_comuna` VALUES ('43', '13', 'PUNITAQUI');
INSERT INTO `com_comuna` VALUES ('44', '13', 'RÍO HURTADO');
INSERT INTO `com_comuna` VALUES ('45', '14', 'VALPARAÍSO');
INSERT INTO `com_comuna` VALUES ('46', '14', 'CASABLANCA');
INSERT INTO `com_comuna` VALUES ('47', '14', 'CONCÓN');
INSERT INTO `com_comuna` VALUES ('48', '14', 'JUAN FERNÁNDEZ');
INSERT INTO `com_comuna` VALUES ('49', '14', 'PUCHUNCAVÍ');
INSERT INTO `com_comuna` VALUES ('50', '14', 'QUINTERO');
INSERT INTO `com_comuna` VALUES ('51', '14', 'VIÑA DEL MAR');
INSERT INTO `com_comuna` VALUES ('52', '15', 'ISLA DE PASCUA');
INSERT INTO `com_comuna` VALUES ('53', '16', 'LOS ANDES');
INSERT INTO `com_comuna` VALUES ('54', '16', 'CALLE LARGA');
INSERT INTO `com_comuna` VALUES ('55', '16', 'RINCONADA');
INSERT INTO `com_comuna` VALUES ('56', '16', 'SAN ESTEBAN');
INSERT INTO `com_comuna` VALUES ('57', '17', 'LA LIGUA');
INSERT INTO `com_comuna` VALUES ('58', '17', 'CABILDO');
INSERT INTO `com_comuna` VALUES ('59', '17', 'PAPUDO');
INSERT INTO `com_comuna` VALUES ('60', '17', 'PETORCA');
INSERT INTO `com_comuna` VALUES ('61', '17', 'ZAPALLAR');
INSERT INTO `com_comuna` VALUES ('62', '18', 'QUILLOTA');
INSERT INTO `com_comuna` VALUES ('63', '18', 'CALERA');
INSERT INTO `com_comuna` VALUES ('64', '18', 'HIJUELAS');
INSERT INTO `com_comuna` VALUES ('65', '18', 'LA CRUZ');
INSERT INTO `com_comuna` VALUES ('66', '18', 'NOGALES');
INSERT INTO `com_comuna` VALUES ('67', '19', 'SAN ANTONIO');
INSERT INTO `com_comuna` VALUES ('68', '19', 'ALGARROBO');
INSERT INTO `com_comuna` VALUES ('69', '19', 'CARTAGENA');
INSERT INTO `com_comuna` VALUES ('70', '19', 'EL QUISCO');
INSERT INTO `com_comuna` VALUES ('71', '19', 'EL TABO');
INSERT INTO `com_comuna` VALUES ('72', '19', 'SANTO DOMINGO');
INSERT INTO `com_comuna` VALUES ('73', '20', 'SAN FELIPE');
INSERT INTO `com_comuna` VALUES ('74', '20', 'CATEMU');
INSERT INTO `com_comuna` VALUES ('75', '20', 'LLAILLAY');
INSERT INTO `com_comuna` VALUES ('76', '20', 'PANQUEHUE');
INSERT INTO `com_comuna` VALUES ('77', '20', 'PUTAENDO');
INSERT INTO `com_comuna` VALUES ('78', '20', 'SANTA MARÍA');
INSERT INTO `com_comuna` VALUES ('79', '21', 'LIMACHE');
INSERT INTO `com_comuna` VALUES ('80', '21', 'QUILPUÉ');
INSERT INTO `com_comuna` VALUES ('81', '21', 'VILLA ALEMANA');
INSERT INTO `com_comuna` VALUES ('82', '21', 'OLMUÉ');
INSERT INTO `com_comuna` VALUES ('83', '22', 'RANCAGUA');
INSERT INTO `com_comuna` VALUES ('84', '22', 'CODEGUA');
INSERT INTO `com_comuna` VALUES ('85', '22', 'COINCO');
INSERT INTO `com_comuna` VALUES ('86', '22', 'COLTAUCO');
INSERT INTO `com_comuna` VALUES ('87', '22', 'DOÑIHUE');
INSERT INTO `com_comuna` VALUES ('88', '22', 'GRANEROS');
INSERT INTO `com_comuna` VALUES ('89', '22', 'LAS CABRAS');
INSERT INTO `com_comuna` VALUES ('90', '22', 'MACHALÍ');
INSERT INTO `com_comuna` VALUES ('91', '22', 'MALLOA');
INSERT INTO `com_comuna` VALUES ('92', '22', 'MOSTAZAL');
INSERT INTO `com_comuna` VALUES ('93', '22', 'OLIVAR');
INSERT INTO `com_comuna` VALUES ('94', '22', 'PEUMO');
INSERT INTO `com_comuna` VALUES ('95', '22', 'PICHIDEGUA');
INSERT INTO `com_comuna` VALUES ('96', '22', 'QUINTA DE TILCOCO');
INSERT INTO `com_comuna` VALUES ('97', '22', 'RENGO');
INSERT INTO `com_comuna` VALUES ('98', '22', 'REQUÍNOA');
INSERT INTO `com_comuna` VALUES ('99', '22', 'SAN VICENTE');
INSERT INTO `com_comuna` VALUES ('100', '23', 'PICHILEMU');
INSERT INTO `com_comuna` VALUES ('101', '23', 'LA ESTRELLA');
INSERT INTO `com_comuna` VALUES ('102', '23', 'LITUECHE');
INSERT INTO `com_comuna` VALUES ('103', '23', 'MARCHIHUE');
INSERT INTO `com_comuna` VALUES ('104', '23', 'NAVIDAD');
INSERT INTO `com_comuna` VALUES ('105', '23', 'PAREDONES');
INSERT INTO `com_comuna` VALUES ('106', '24', 'SAN FERNANDO');
INSERT INTO `com_comuna` VALUES ('107', '24', 'CHÉPICA');
INSERT INTO `com_comuna` VALUES ('108', '24', 'CHIMBARONGO');
INSERT INTO `com_comuna` VALUES ('109', '24', 'LOLOL');
INSERT INTO `com_comuna` VALUES ('110', '24', 'NANCAGUA');
INSERT INTO `com_comuna` VALUES ('111', '24', 'PALMILLA');
INSERT INTO `com_comuna` VALUES ('112', '24', 'PERALILLO');
INSERT INTO `com_comuna` VALUES ('113', '24', 'PLACILLA');
INSERT INTO `com_comuna` VALUES ('114', '24', 'PUMANQUE');
INSERT INTO `com_comuna` VALUES ('115', '24', 'SANTA CRUZ');
INSERT INTO `com_comuna` VALUES ('116', '25', 'TALCA');
INSERT INTO `com_comuna` VALUES ('117', '25', 'CONSTITUCIÓN');
INSERT INTO `com_comuna` VALUES ('118', '25', 'CUREPTO');
INSERT INTO `com_comuna` VALUES ('119', '25', 'EMPEDRADO');
INSERT INTO `com_comuna` VALUES ('120', '25', 'MAULE');
INSERT INTO `com_comuna` VALUES ('121', '25', 'PELARCO');
INSERT INTO `com_comuna` VALUES ('122', '25', 'PENCAHUE');
INSERT INTO `com_comuna` VALUES ('123', '25', 'RÍO CLARO');
INSERT INTO `com_comuna` VALUES ('124', '25', 'SAN CLEMENTE');
INSERT INTO `com_comuna` VALUES ('125', '25', 'SAN RAFAEL');
INSERT INTO `com_comuna` VALUES ('126', '26', 'CAUQUENES');
INSERT INTO `com_comuna` VALUES ('127', '26', 'CHANCO');
INSERT INTO `com_comuna` VALUES ('128', '26', 'PELLUHUE');
INSERT INTO `com_comuna` VALUES ('129', '27', 'CURICÓ');
INSERT INTO `com_comuna` VALUES ('130', '27', 'HUALAÑÉ');
INSERT INTO `com_comuna` VALUES ('131', '27', 'LICANTÉN');
INSERT INTO `com_comuna` VALUES ('132', '27', 'MOLINA');
INSERT INTO `com_comuna` VALUES ('133', '27', 'RAUCO');
INSERT INTO `com_comuna` VALUES ('134', '27', 'ROMERAL');
INSERT INTO `com_comuna` VALUES ('135', '27', 'SAGRADA FAMILIA');
INSERT INTO `com_comuna` VALUES ('136', '27', 'TENO');
INSERT INTO `com_comuna` VALUES ('137', '27', 'VICHUQUÉN');
INSERT INTO `com_comuna` VALUES ('138', '28', 'LINARES');
INSERT INTO `com_comuna` VALUES ('139', '28', 'COLBÚN');
INSERT INTO `com_comuna` VALUES ('140', '28', 'LONGAVÍ');
INSERT INTO `com_comuna` VALUES ('141', '28', 'PARRAL');
INSERT INTO `com_comuna` VALUES ('142', '28', 'RETIRO');
INSERT INTO `com_comuna` VALUES ('143', '28', 'SAN JAVIER');
INSERT INTO `com_comuna` VALUES ('144', '28', 'VILLA ALEGRE');
INSERT INTO `com_comuna` VALUES ('145', '28', 'YERBAS BUENAS');
INSERT INTO `com_comuna` VALUES ('146', '29', 'CONCEPCIÓN');
INSERT INTO `com_comuna` VALUES ('147', '29', 'CORONEL');
INSERT INTO `com_comuna` VALUES ('148', '29', 'CHIGUAYANTE');
INSERT INTO `com_comuna` VALUES ('149', '29', 'FLORIDA');
INSERT INTO `com_comuna` VALUES ('150', '29', 'HUALQUI');
INSERT INTO `com_comuna` VALUES ('151', '29', 'LOTA');
INSERT INTO `com_comuna` VALUES ('152', '29', 'PENCO');
INSERT INTO `com_comuna` VALUES ('153', '29', 'SAN PEDRO DE LA PAZ');
INSERT INTO `com_comuna` VALUES ('154', '29', 'SANTA JUANA');
INSERT INTO `com_comuna` VALUES ('155', '29', 'TALCAHUANO');
INSERT INTO `com_comuna` VALUES ('156', '29', 'TOMÉ');
INSERT INTO `com_comuna` VALUES ('157', '29', 'HUALPÉN');
INSERT INTO `com_comuna` VALUES ('158', '30', 'LEBU');
INSERT INTO `com_comuna` VALUES ('159', '30', 'ARAUCO');
INSERT INTO `com_comuna` VALUES ('160', '30', 'CAÑETE');
INSERT INTO `com_comuna` VALUES ('161', '30', 'CONTULMO');
INSERT INTO `com_comuna` VALUES ('162', '30', 'CURANILAHUE');
INSERT INTO `com_comuna` VALUES ('163', '30', 'LOS ALAMOS');
INSERT INTO `com_comuna` VALUES ('164', '30', 'TIRÚA');
INSERT INTO `com_comuna` VALUES ('165', '31', 'LOS ANGELES');
INSERT INTO `com_comuna` VALUES ('166', '31', 'ANTUCO');
INSERT INTO `com_comuna` VALUES ('167', '31', 'CABRERO');
INSERT INTO `com_comuna` VALUES ('168', '31', 'LAJA');
INSERT INTO `com_comuna` VALUES ('169', '31', 'MULCHÉN');
INSERT INTO `com_comuna` VALUES ('170', '31', 'NACIMIENTO');
INSERT INTO `com_comuna` VALUES ('171', '31', 'NEGRETE');
INSERT INTO `com_comuna` VALUES ('172', '31', 'QUILACO');
INSERT INTO `com_comuna` VALUES ('173', '31', 'QUILLECO');
INSERT INTO `com_comuna` VALUES ('174', '31', 'SAN ROSENDO');
INSERT INTO `com_comuna` VALUES ('175', '31', 'SANTA BÁRBARA');
INSERT INTO `com_comuna` VALUES ('176', '31', 'TUCAPEL');
INSERT INTO `com_comuna` VALUES ('177', '31', 'YUMBEL');
INSERT INTO `com_comuna` VALUES ('178', '31', 'ALTO BIOBÍO');
INSERT INTO `com_comuna` VALUES ('179', '32', 'CHILLÁN');
INSERT INTO `com_comuna` VALUES ('180', '32', 'BULNES');
INSERT INTO `com_comuna` VALUES ('181', '32', 'COBQUECURA');
INSERT INTO `com_comuna` VALUES ('182', '32', 'COELEMU');
INSERT INTO `com_comuna` VALUES ('183', '32', 'COIHUECO');
INSERT INTO `com_comuna` VALUES ('184', '32', 'CHILLÁN VIEJO');
INSERT INTO `com_comuna` VALUES ('185', '32', 'EL CARMEN');
INSERT INTO `com_comuna` VALUES ('186', '32', 'NINHUE');
INSERT INTO `com_comuna` VALUES ('187', '32', 'ÑIQUÉN');
INSERT INTO `com_comuna` VALUES ('188', '32', 'PEMUCO');
INSERT INTO `com_comuna` VALUES ('189', '32', 'PINTO');
INSERT INTO `com_comuna` VALUES ('190', '32', 'PORTEZUELO');
INSERT INTO `com_comuna` VALUES ('191', '32', 'QUILLÓN');
INSERT INTO `com_comuna` VALUES ('192', '32', 'QUIRIHUE');
INSERT INTO `com_comuna` VALUES ('193', '32', 'RÁNQUIL');
INSERT INTO `com_comuna` VALUES ('194', '32', 'SAN CARLOS');
INSERT INTO `com_comuna` VALUES ('195', '32', 'SAN FABIÁN');
INSERT INTO `com_comuna` VALUES ('196', '32', 'SAN IGNACIO');
INSERT INTO `com_comuna` VALUES ('197', '32', 'SAN NICOLÁS');
INSERT INTO `com_comuna` VALUES ('198', '32', 'TREGUACO');
INSERT INTO `com_comuna` VALUES ('199', '32', 'YUNGAY');
INSERT INTO `com_comuna` VALUES ('200', '33', 'TEMUCO');
INSERT INTO `com_comuna` VALUES ('201', '33', 'CARAHUE');
INSERT INTO `com_comuna` VALUES ('202', '33', 'CUNCO');
INSERT INTO `com_comuna` VALUES ('203', '33', 'CURARREHUE');
INSERT INTO `com_comuna` VALUES ('204', '33', 'FREIRE');
INSERT INTO `com_comuna` VALUES ('205', '33', 'GALVARINO');
INSERT INTO `com_comuna` VALUES ('206', '33', 'GORBEA');
INSERT INTO `com_comuna` VALUES ('207', '33', 'LAUTARO');
INSERT INTO `com_comuna` VALUES ('208', '33', 'LONCOCHE');
INSERT INTO `com_comuna` VALUES ('209', '33', 'MELIPEUCO');
INSERT INTO `com_comuna` VALUES ('210', '33', 'NUEVA IMPERIAL');
INSERT INTO `com_comuna` VALUES ('211', '33', 'PADRE LAS CASAS');
INSERT INTO `com_comuna` VALUES ('212', '33', 'PERQUENCO');
INSERT INTO `com_comuna` VALUES ('213', '33', 'PITRUFQUÉN');
INSERT INTO `com_comuna` VALUES ('214', '33', 'PUCÓN');
INSERT INTO `com_comuna` VALUES ('215', '33', 'SAAVEDRA');
INSERT INTO `com_comuna` VALUES ('216', '33', 'TEODORO SCHMIDT');
INSERT INTO `com_comuna` VALUES ('217', '33', 'TOLTÉN');
INSERT INTO `com_comuna` VALUES ('218', '33', 'VILCÚN');
INSERT INTO `com_comuna` VALUES ('219', '33', 'VILLARRICA');
INSERT INTO `com_comuna` VALUES ('220', '33', 'CHOLCHOL');
INSERT INTO `com_comuna` VALUES ('221', '34', 'ANGOL');
INSERT INTO `com_comuna` VALUES ('222', '34', 'COLLIPULLI');
INSERT INTO `com_comuna` VALUES ('223', '34', 'CURACAUTÍN');
INSERT INTO `com_comuna` VALUES ('224', '34', 'ERCILLA');
INSERT INTO `com_comuna` VALUES ('225', '34', 'LONQUIMAY');
INSERT INTO `com_comuna` VALUES ('226', '34', 'LOS SAUCES');
INSERT INTO `com_comuna` VALUES ('227', '34', 'LUMACO');
INSERT INTO `com_comuna` VALUES ('228', '34', 'PURÉN');
INSERT INTO `com_comuna` VALUES ('229', '34', 'RENAICO');
INSERT INTO `com_comuna` VALUES ('230', '34', 'TRAIGUÉN');
INSERT INTO `com_comuna` VALUES ('231', '34', 'VICTORIA');
INSERT INTO `com_comuna` VALUES ('232', '35', 'VALDIVIA');
INSERT INTO `com_comuna` VALUES ('233', '35', 'CORRAL');
INSERT INTO `com_comuna` VALUES ('234', '35', 'LANCO');
INSERT INTO `com_comuna` VALUES ('235', '35', 'LOS LAGOS');
INSERT INTO `com_comuna` VALUES ('236', '35', 'MÁFIL');
INSERT INTO `com_comuna` VALUES ('237', '35', 'MARIQUINA');
INSERT INTO `com_comuna` VALUES ('238', '35', 'PAILLACO');
INSERT INTO `com_comuna` VALUES ('239', '35', 'PANGUIPULLI');
INSERT INTO `com_comuna` VALUES ('240', '36', 'LA UNIÓN');
INSERT INTO `com_comuna` VALUES ('241', '36', 'FUTRONO');
INSERT INTO `com_comuna` VALUES ('242', '36', 'LAGO RANCO');
INSERT INTO `com_comuna` VALUES ('243', '36', 'RÍO BUENO');
INSERT INTO `com_comuna` VALUES ('244', '37', 'PUERTO MONTT');
INSERT INTO `com_comuna` VALUES ('245', '37', 'CALBUCO');
INSERT INTO `com_comuna` VALUES ('246', '37', 'COCHAMÓ');
INSERT INTO `com_comuna` VALUES ('247', '37', 'FRESIA');
INSERT INTO `com_comuna` VALUES ('248', '37', 'FRUTILLAR');
INSERT INTO `com_comuna` VALUES ('249', '37', 'LOS MUERMOS');
INSERT INTO `com_comuna` VALUES ('250', '37', 'LLANQUIHUE');
INSERT INTO `com_comuna` VALUES ('251', '37', 'MAULLÍN');
INSERT INTO `com_comuna` VALUES ('252', '37', 'PUERTO VARAS');
INSERT INTO `com_comuna` VALUES ('253', '38', 'CASTRO');
INSERT INTO `com_comuna` VALUES ('254', '38', 'ANCUD');
INSERT INTO `com_comuna` VALUES ('255', '38', 'CHONCHI');
INSERT INTO `com_comuna` VALUES ('256', '38', 'CURACO DE VÉLEZ');
INSERT INTO `com_comuna` VALUES ('257', '38', 'DALCAHUE');
INSERT INTO `com_comuna` VALUES ('258', '38', 'PUQUELDÓN');
INSERT INTO `com_comuna` VALUES ('259', '38', 'QUEILÉN');
INSERT INTO `com_comuna` VALUES ('260', '38', 'QUELLÓN');
INSERT INTO `com_comuna` VALUES ('261', '38', 'QUEMCHI');
INSERT INTO `com_comuna` VALUES ('262', '38', 'QUINCHAO');
INSERT INTO `com_comuna` VALUES ('263', '39', 'OSORNO');
INSERT INTO `com_comuna` VALUES ('264', '39', 'PUERTO OCTAY');
INSERT INTO `com_comuna` VALUES ('265', '39', 'PURRANQUE');
INSERT INTO `com_comuna` VALUES ('266', '39', 'PUYEHUE');
INSERT INTO `com_comuna` VALUES ('267', '39', 'RÍO NEGRO');
INSERT INTO `com_comuna` VALUES ('268', '39', 'SAN JUAN DE LA COSTA');
INSERT INTO `com_comuna` VALUES ('269', '39', 'SAN PABLO');
INSERT INTO `com_comuna` VALUES ('270', '40', 'CHAITÉN');
INSERT INTO `com_comuna` VALUES ('271', '40', 'FUTALEUFÚ');
INSERT INTO `com_comuna` VALUES ('272', '40', 'HUALAIHUÉ');
INSERT INTO `com_comuna` VALUES ('273', '40', 'PALENA');
INSERT INTO `com_comuna` VALUES ('274', '41', 'COIHAIQUE');
INSERT INTO `com_comuna` VALUES ('275', '41', 'LAGO VERDE');
INSERT INTO `com_comuna` VALUES ('276', '42', 'AISÉN');
INSERT INTO `com_comuna` VALUES ('277', '42', 'CISNES');
INSERT INTO `com_comuna` VALUES ('278', '42', 'GUAITECAS');
INSERT INTO `com_comuna` VALUES ('279', '43', 'COCHRANE');
INSERT INTO `com_comuna` VALUES ('280', '43', 'O\'HIGGINS');
INSERT INTO `com_comuna` VALUES ('281', '43', 'TORTEL');
INSERT INTO `com_comuna` VALUES ('282', '44', 'CHILE CHICO');
INSERT INTO `com_comuna` VALUES ('283', '44', 'RÍO IBÁÑEZ');
INSERT INTO `com_comuna` VALUES ('284', '45', 'PUNTA ARENAS');
INSERT INTO `com_comuna` VALUES ('285', '45', 'LAGUNA BLANCA');
INSERT INTO `com_comuna` VALUES ('286', '45', 'RÍO VERDE');
INSERT INTO `com_comuna` VALUES ('287', '45', 'SAN GREGORIO');
INSERT INTO `com_comuna` VALUES ('288', '46', 'CABO DE HORNOS');
INSERT INTO `com_comuna` VALUES ('289', '46', 'ANTÁRTICA');
INSERT INTO `com_comuna` VALUES ('290', '47', 'PORVENIR');
INSERT INTO `com_comuna` VALUES ('291', '47', 'PRIMAVERA');
INSERT INTO `com_comuna` VALUES ('292', '47', 'TIMAUKEL');
INSERT INTO `com_comuna` VALUES ('293', '48', 'NATALES');
INSERT INTO `com_comuna` VALUES ('294', '48', 'TORRES DEL PAINE');
INSERT INTO `com_comuna` VALUES ('295', '49', 'SANTIAGO');
INSERT INTO `com_comuna` VALUES ('296', '49', 'CERRILLOS');
INSERT INTO `com_comuna` VALUES ('297', '49', 'CERRO NAVIA');
INSERT INTO `com_comuna` VALUES ('298', '49', 'CONCHALÍ');
INSERT INTO `com_comuna` VALUES ('299', '49', 'EL BOSQUE');
INSERT INTO `com_comuna` VALUES ('300', '49', 'ESTACIÓN CENTRAL');
INSERT INTO `com_comuna` VALUES ('301', '49', 'HUECHURABA');
INSERT INTO `com_comuna` VALUES ('302', '49', 'INDEPENDENCIA');
INSERT INTO `com_comuna` VALUES ('303', '49', 'LA CISTERNA');
INSERT INTO `com_comuna` VALUES ('304', '49', 'LA FLORIDA');
INSERT INTO `com_comuna` VALUES ('305', '49', 'LA GRANJA');
INSERT INTO `com_comuna` VALUES ('306', '49', 'LA PINTANA');
INSERT INTO `com_comuna` VALUES ('307', '49', 'LA REINA');
INSERT INTO `com_comuna` VALUES ('308', '49', 'LAS CONDES');
INSERT INTO `com_comuna` VALUES ('309', '49', 'LO BARNECHEA');
INSERT INTO `com_comuna` VALUES ('310', '49', 'LO ESPEJO');
INSERT INTO `com_comuna` VALUES ('311', '49', 'LO PRADO');
INSERT INTO `com_comuna` VALUES ('312', '49', 'MACUL');
INSERT INTO `com_comuna` VALUES ('313', '49', 'MAIPÚ');
INSERT INTO `com_comuna` VALUES ('314', '49', 'ÑUÑOA');
INSERT INTO `com_comuna` VALUES ('315', '49', 'PEDRO AGUIRRE CERDA');
INSERT INTO `com_comuna` VALUES ('316', '49', 'PEÑALOLÉN');
INSERT INTO `com_comuna` VALUES ('317', '49', 'PROVIDENCIA');
INSERT INTO `com_comuna` VALUES ('318', '49', 'PUDAHUEL');
INSERT INTO `com_comuna` VALUES ('319', '49', 'QUILICURA');
INSERT INTO `com_comuna` VALUES ('320', '49', 'QUINTA NORMAL');
INSERT INTO `com_comuna` VALUES ('321', '49', 'RECOLETA');
INSERT INTO `com_comuna` VALUES ('322', '49', 'RENCA');
INSERT INTO `com_comuna` VALUES ('323', '49', 'SAN JOAQUÍN');
INSERT INTO `com_comuna` VALUES ('324', '49', 'SAN MIGUEL');
INSERT INTO `com_comuna` VALUES ('325', '49', 'SAN RAMÓN');
INSERT INTO `com_comuna` VALUES ('326', '49', 'VITACURA');
INSERT INTO `com_comuna` VALUES ('327', '50', 'PUENTE ALTO');
INSERT INTO `com_comuna` VALUES ('328', '50', 'PIRQUE');
INSERT INTO `com_comuna` VALUES ('329', '50', 'SAN JOSÉ DE MAIPO');
INSERT INTO `com_comuna` VALUES ('330', '51', 'COLINA');
INSERT INTO `com_comuna` VALUES ('331', '51', 'LAMPA');
INSERT INTO `com_comuna` VALUES ('332', '51', 'TILTIL');
INSERT INTO `com_comuna` VALUES ('333', '52', 'SAN BERNARDO');
INSERT INTO `com_comuna` VALUES ('334', '52', 'BUIN');
INSERT INTO `com_comuna` VALUES ('335', '52', 'CALERA DE TANGO');
INSERT INTO `com_comuna` VALUES ('336', '52', 'PAINE');
INSERT INTO `com_comuna` VALUES ('337', '53', 'MELIPILLA');
INSERT INTO `com_comuna` VALUES ('338', '53', 'ALHUÉ');
INSERT INTO `com_comuna` VALUES ('339', '53', 'CURACAVÍ');
INSERT INTO `com_comuna` VALUES ('340', '53', 'MARÍA PINTO');
INSERT INTO `com_comuna` VALUES ('341', '53', 'SAN PEDRO');
INSERT INTO `com_comuna` VALUES ('342', '54', 'TALAGANTE');
INSERT INTO `com_comuna` VALUES ('343', '54', 'EL MONTE');
INSERT INTO `com_comuna` VALUES ('344', '54', 'ISLA DE MAIPO');
INSERT INTO `com_comuna` VALUES ('345', '54', 'PADRE HURTADO');
INSERT INTO `com_comuna` VALUES ('346', '54', 'PEÑAFLOR');

-- ----------------------------
-- Table structure for coni_configuracion_institucion
-- ----------------------------
DROP TABLE IF EXISTS `coni_configuracion_institucion`;
CREATE TABLE `coni_configuracion_institucion` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `INST_ID` int(11) DEFAULT '0',
  `ENVIA_DOCUMENTOS` int(11) DEFAULT '0',
  `ENVIA_PROYECTOS` int(11) DEFAULT '0',
  `ENVIA_RENDICIONES` int(11) DEFAULT '0',
  `ENVIA_CORREO_EVENTOS` int(11) DEFAULT '0',
  `ELIMINADO` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of coni_configuracion_institucion
-- ----------------------------
INSERT INTO `coni_configuracion_institucion` VALUES ('1', '3', '0', '0', '0', '0', '0');
INSERT INTO `coni_configuracion_institucion` VALUES ('2', '4', '1', '1', '1', '1', '0');
INSERT INTO `coni_configuracion_institucion` VALUES ('3', '5', '0', '0', '0', '0', '0');

-- ----------------------------
-- Table structure for cua_curso_apoderado
-- ----------------------------
DROP TABLE IF EXISTS `cua_curso_apoderado`;
CREATE TABLE `cua_curso_apoderado` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `INST_ID` int(11) DEFAULT '0',
  `USU_ID` int(11) DEFAULT '0',
  `CUR_ID` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cua_curso_apoderado
-- ----------------------------
INSERT INTO `cua_curso_apoderado` VALUES ('1', '3', '4', '14');
INSERT INTO `cua_curso_apoderado` VALUES ('5', '3', '1', '1');
INSERT INTO `cua_curso_apoderado` VALUES ('6', '4', '19', '115');
INSERT INTO `cua_curso_apoderado` VALUES ('7', '4', '20', '115');
INSERT INTO `cua_curso_apoderado` VALUES ('8', '4', '21', '115');
INSERT INTO `cua_curso_apoderado` VALUES ('9', '5', '22', '134');
INSERT INTO `cua_curso_apoderado` VALUES ('10', '5', '23', '137');
INSERT INTO `cua_curso_apoderado` VALUES ('13', '5', '25', '130');
INSERT INTO `cua_curso_apoderado` VALUES ('16', '5', '24', '7');
INSERT INTO `cua_curso_apoderado` VALUES ('17', '3', '30', '1');

-- ----------------------------
-- Table structure for cur_curso
-- ----------------------------
DROP TABLE IF EXISTS `cur_curso`;
CREATE TABLE `cur_curso` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `INST_ID` int(11) DEFAULT '0',
  `NOMBRE` varchar(500) DEFAULT '0',
  `ID_USU_RESPONSABLE` int(11) DEFAULT '0',
  `ELIMINADO` int(11) DEFAULT '0',
  `ORDEN` int(11) DEFAULT '0',
  `TIPO` int(11) DEFAULT '0',
  `GRUPO` varchar(500) DEFAULT NULL,
  `SUB_GRUPO` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=141 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cur_curso
-- ----------------------------
INSERT INTO `cur_curso` VALUES ('1', '3', 'Sala Cuna Menor A', '0', '0', '1', '1', 'Parvularia ', 'Sala Cuna Menor');
INSERT INTO `cur_curso` VALUES ('2', '3', 'Sala Cuna Menor B', '0', '0', '2', '1', 'Parvularia ', 'Sala Cuna Menor');
INSERT INTO `cur_curso` VALUES ('3', '3', 'Sala Cuna Menor C', '0', '0', '3', '1', 'Parvularia ', 'Sala Cuna Menor');
INSERT INTO `cur_curso` VALUES ('4', '3', 'Sala Cuna Mayor A', '0', '0', '4', '1', 'Parvularia ', 'Sala Cuna Mayor');
INSERT INTO `cur_curso` VALUES ('5', '3', 'Sala Cuna Mayor B', '0', '0', '5', '1', 'Parvularia ', 'Sala Cuna Mayor');
INSERT INTO `cur_curso` VALUES ('6', '3', 'Sala Cuna Mayor C', '0', '0', '6', '1', 'Parvularia ', 'Sala Cuna Mayor');
INSERT INTO `cur_curso` VALUES ('7', '3', 'Primer Nivel Transición (Pre-Kinder) A', '0', '0', '7', '1', 'Parvularia ', 'Prekinder');
INSERT INTO `cur_curso` VALUES ('8', '3', 'Primer Nivel Transición (Pre-Kinder) B', '0', '0', '8', '1', 'Parvularia ', 'Prekinder');
INSERT INTO `cur_curso` VALUES ('9', '3', 'Primer Nivel Transición (Pre-Kinder) C', '0', '0', '9', '1', 'Parvularia ', 'Prekinder');
INSERT INTO `cur_curso` VALUES ('10', '3', 'Segundo Nivel Transición (Kinder) A', '0', '0', '10', '1', 'Parvularia ', 'Kinder');
INSERT INTO `cur_curso` VALUES ('11', '3', 'Segundo Nivel Transición (Kinder) B', '0', '0', '11', '1', 'Parvularia ', 'Kinder');
INSERT INTO `cur_curso` VALUES ('12', '3', 'Segundo Nivel Transición (Kinder) C', '0', '0', '12', '1', 'Parvularia ', 'Kinder');
INSERT INTO `cur_curso` VALUES ('13', '3', 'Primer Año Básico A', '0', '0', '13', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('14', '3', 'Primer Año Básico B', '0', '0', '14', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('15', '3', 'Primer Año Básico C', '0', '0', '15', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('16', '3', 'Primer Año Básico D', '0', '0', '16', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('17', '3', 'Primer Año Básico E', '0', '0', '17', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('18', '3', 'Primer Año Básico F', '0', '0', '18', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('19', '3', 'Primer Año Básico G', '0', '0', '19', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('20', '3', 'Primer Año Básico H', '0', '0', '20', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('21', '3', 'Segundo Año Básico A', '0', '0', '21', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('22', '3', 'Segundo Año Básico B', '0', '0', '22', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('23', '3', 'Segundo Año Básico C', '0', '0', '23', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('24', '3', 'Segundo Año Básico D', '0', '0', '24', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('25', '3', 'Segundo Año Básico E', '0', '0', '25', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('26', '3', 'Segundo Año Básico F', '0', '0', '26', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('27', '3', 'Segundo Año Básico G', '0', '0', '27', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('28', '3', 'Segundo Año Básico H', '0', '0', '28', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('29', '3', 'Tercer Año Básico A', '0', '0', '29', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('30', '3', 'Tercer Año Básico B', '0', '0', '30', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('31', '3', 'Tercer Año Básico C', '0', '0', '31', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('32', '3', 'Tercer Año Básico D', '0', '0', '32', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('33', '3', 'Tercer Año Básico E', '0', '0', '33', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('34', '3', 'Tercer Año Básico F', '0', '0', '34', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('35', '3', 'Tercer Año Básico G', '0', '0', '35', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('36', '3', 'Tercer Año Básico H', '0', '0', '36', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('37', '3', 'Cuarto Año Básico A', '0', '0', '37', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('38', '3', 'Cuarto Año Básico B', '0', '0', '38', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('39', '3', 'Cuarto Año Básico C', '0', '0', '39', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('40', '3', 'Cuarto Año Básico D', '0', '0', '40', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('41', '3', 'Cuarto Año Básico E', '0', '0', '41', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('42', '3', 'Cuarto Año Básico F', '0', '0', '42', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('43', '3', 'Cuarto Año Básico G', '0', '0', '43', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('44', '3', 'Cuarto Año Básico H', '0', '0', '44', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('45', '3', 'Quinto Año Básico A', '0', '0', '45', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('46', '3', 'Quinto Año Básico B', '0', '0', '46', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('47', '3', 'Quinto Año Básico C', '0', '0', '47', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('48', '3', 'Quinto Año Básico D', '0', '0', '48', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('49', '3', 'Quinto Año Básico E', '0', '0', '49', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('50', '3', 'Quinto Año Básico F', '0', '0', '50', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('51', '3', 'Quinto Año Básico G', '0', '0', '51', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('52', '3', 'Quinto Año Básico H', '0', '0', '52', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('53', '3', 'Sexto Año Básico A', '0', '0', '53', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('54', '3', 'Sexto Año Básico B', '0', '0', '54', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('55', '3', 'Sexto Año Básico C', '0', '0', '55', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('56', '3', 'Sexto Año Básico D', '0', '0', '56', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('57', '3', 'Sexto Año Básico E', '0', '0', '57', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('58', '3', 'Sexto Año Básico F', '0', '0', '58', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('59', '3', 'Sexto Año Básico G', '0', '0', '59', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('60', '3', 'Sexto Año Básico H', '0', '0', '60', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('61', '3', 'Séptimo Año Básico A', '0', '0', '61', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('62', '3', 'Séptimo Año Básico B', '0', '0', '62', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('63', '3', 'Séptimo Año Básico C', '0', '0', '63', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('64', '3', 'Séptimo Año Básico D', '0', '0', '64', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('65', '3', 'Séptimo Año Básico E', '0', '0', '65', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('66', '3', 'Séptimo Año Básico F', '0', '0', '66', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('67', '3', 'Séptimo Año Básico G', '0', '0', '67', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('68', '3', 'Séptimo Año Básico H', '0', '0', '68', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('69', '3', 'Octavo Año Básico A', '0', '0', '69', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('70', '3', 'Octavo Año Básico B', '0', '0', '70', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('71', '3', 'Octavo Año Básico C', '0', '0', '71', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('72', '3', 'Octavo Año Básico D', '0', '0', '72', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('73', '3', 'Octavo Año Básico E', '0', '0', '73', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('74', '3', 'Octavo Año Básico F', '0', '0', '74', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('75', '3', 'Octavo Año Básico G', '0', '0', '75', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('76', '3', 'Octavo Año Básico H', '0', '0', '76', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('77', '3', 'Primer Año Medio A', '0', '0', '77', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('78', '3', 'Primer Año Medio B', '0', '0', '78', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('79', '3', 'Primer Año Medio C', '0', '0', '79', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('80', '3', 'Primer Año Medio D', '0', '0', '80', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('81', '3', 'Primer Año Medio E', '0', '0', '81', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('82', '3', 'Primer Año Medio F', '0', '0', '82', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('83', '3', 'Primer Año Medio G', '0', '0', '83', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('84', '3', 'Primer Año Medio H', '0', '0', '84', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('85', '3', 'Segundo Año Medio A', '0', '0', '85', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('86', '3', 'Segundo Año Medio B', '0', '0', '86', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('87', '3', 'Segundo Año Medio C', '0', '0', '87', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('88', '3', 'Segundo Año Medio D', '0', '0', '88', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('89', '3', 'Segundo Año Medio E', '0', '0', '89', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('90', '3', 'Segundo Año Medio F', '0', '0', '90', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('91', '3', 'Segundo Año Medio G', '0', '0', '91', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('92', '3', 'Segundo Año Medio H', '0', '0', '92', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('93', '3', 'Tercer Año Medio A', '0', '0', '93', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('94', '3', 'Tercer Año Medio B', '0', '0', '94', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('95', '3', 'Tercer Año Medio C', '0', '0', '95', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('96', '3', 'Tercer Año Medio D', '0', '0', '96', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('97', '3', 'Tercer Año Medio E', '0', '0', '97', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('98', '3', 'Tercer Año Medio F', '0', '0', '98', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('99', '3', 'Tercer Año Medio G', '0', '0', '99', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('100', '3', 'Tercer Año Medio H', '0', '0', '100', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('101', '3', 'Cuarto Año Medio A', '0', '0', '101', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('102', '3', 'Cuarto Año Medio B', '0', '0', '102', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('103', '3', 'Cuarto Año Medio C', '0', '0', '103', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('104', '3', 'Cuarto Año Medio D', '0', '0', '104', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('105', '3', 'Cuarto Año Medio E', '0', '0', '105', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('106', '3', 'Cuarto Año Medio F', '0', '0', '106', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('107', '3', 'Cuarto Año Medio G', '0', '0', '107', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('108', '3', 'Cuarto Año Medio H', '0', '0', '108', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('109', '4', 'Sala Cuna Menor', '0', '0', '1', '1', 'Parvularia', 'Sala Cuna Menor');
INSERT INTO `cur_curso` VALUES ('110', '4', 'Sala Cuna Mayor', '0', '0', '2', '1', 'Parvularia', 'Sala Cuna Mayor');
INSERT INTO `cur_curso` VALUES ('111', '4', 'Primer Nivel Transición (Pre-Kinder)', '0', '0', '3', '1', 'Parvularia', 'Prekinder');
INSERT INTO `cur_curso` VALUES ('112', '4', 'Segundo Nivel Transición (Kinder)', '0', '0', '4', '1', 'Parvularia', 'Kinder');
INSERT INTO `cur_curso` VALUES ('113', '4', 'Primer Año Básico', '0', '0', '5', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('114', '4', 'Segundo Año Básico', '0', '0', '6', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('115', '4', 'Tercer Año Básico', '0', '0', '7', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('116', '4', 'Cuarto Año Básico', '0', '0', '8', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('117', '4', 'Quinto Año Básico', '0', '0', '9', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('118', '4', 'Sexto Año Básico', '0', '0', '10', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('119', '4', 'Séptimo Año Básico', '0', '0', '11', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('120', '4', 'Octavo Año Básico', '0', '0', '12', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('121', '4', 'Primer Año Medio', '0', '0', '13', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('122', '4', 'Segundo Año Medio', '0', '0', '14', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('123', '4', 'Tercer Año Medio', '0', '0', '15', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('124', '4', 'Cuarto Año Medio', '0', '0', '16', '3', 'Media', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('125', '5', 'Sala Cuna Menor', '0', '0', '1', '1', 'Parvularia', 'Sala Cuna Menor');
INSERT INTO `cur_curso` VALUES ('126', '5', 'Sala Cuna Mayor', '0', '0', '2', '1', 'Parvularia', 'Sala Cuna Mayor');
INSERT INTO `cur_curso` VALUES ('127', '5', 'Primer Nivel Transición (Pre-Kinder)', '0', '0', '3', '1', 'Parvularia', 'Prekinder');
INSERT INTO `cur_curso` VALUES ('128', '5', 'Segundo Nivel Transición (Kinder)', '0', '0', '4', '1', 'Parvularia', 'Kinder');
INSERT INTO `cur_curso` VALUES ('129', '5', 'Primer Año Básico', '0', '0', '5', '2', 'Básica', 'Primeros');
INSERT INTO `cur_curso` VALUES ('130', '5', 'Segundo Año Básico', '0', '0', '6', '2', 'Básica', 'Segundos');
INSERT INTO `cur_curso` VALUES ('131', '5', 'Tercer Año Básico', '0', '0', '7', '2', 'Básica', 'Terceros');
INSERT INTO `cur_curso` VALUES ('132', '5', 'Cuarto Año Básico', '0', '0', '8', '2', 'Básica', 'Cuartos');
INSERT INTO `cur_curso` VALUES ('133', '5', 'Quinto Año Básico', '0', '0', '9', '2', 'Básica', 'Quintos');
INSERT INTO `cur_curso` VALUES ('134', '5', 'Sexto Año Básico', '0', '0', '10', '2', 'Básica', 'Sextos');
INSERT INTO `cur_curso` VALUES ('135', '5', 'Séptimo Año Básico', '0', '0', '11', '2', 'Básica', 'Séptimos');
INSERT INTO `cur_curso` VALUES ('136', '5', 'Octavo Año Básico', '0', '0', '12', '2', 'Básica', 'Octavos');
INSERT INTO `cur_curso` VALUES ('137', '5', 'Primer Año Medio', '0', '0', '13', '3', 'Media', 'Primeros');
INSERT INTO `cur_curso` VALUES ('138', '5', 'Segundo Año Medio', '0', '0', '14', '3', 'Media', 'Segundos');
INSERT INTO `cur_curso` VALUES ('139', '5', 'Tercer Año Medio', '0', '0', '15', '3', 'Media', 'Terceros');
INSERT INTO `cur_curso` VALUES ('140', '5', 'Cuarto Año Medio', '0', '0', '16', '3', 'Media', 'Cuartos');

-- ----------------------------
-- Table structure for doc_documentos
-- ----------------------------
DROP TABLE IF EXISTS `doc_documentos`;
CREATE TABLE `doc_documentos` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `NOMBRE_ARCHIVO` varchar(250) NOT NULL DEFAULT '0',
  `URL` varchar(1000) NOT NULL DEFAULT '0',
  `TAMANO` int(11) NOT NULL DEFAULT '0',
  `FECHA_SUBIDA` varchar(50) DEFAULT NULL,
  `EXTENSION` varchar(250) DEFAULT NULL,
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of doc_documentos
-- ----------------------------
INSERT INTO `doc_documentos` VALUES ('3', '1', '3', 'g5qz5bgl.l5m LA OFERTA Y DEMANDA.docx', '~/Repositorio/g5qz5bgl.l5m LA OFERTA Y DEMANDA.docx', '13', '10/27/2015 8:57 AM', '~/img/word.png', '0');
INSERT INTO `doc_documentos` VALUES ('4', '1', '3', 'dzxjvwf4.gru Ideas y herramientas para mejorar la organizacion.pdf', '~/Repositorio/dzxjvwf4.gru Ideas y herramientas para mejorar la organizacion.pdf', '866', '10/27/2015 11:13 AM', '~/img/pdf.png', '0');
INSERT INTO `doc_documentos` VALUES ('5', '1', '3', 'fsdrt32u.mhp Acta Segunda Reunion cgpa 18 6 2015.pdf', '~/Repositorio/fsdrt32u.mhp Acta Segunda Reunion cgpa 18 6 2015.pdf', '802', '10/27/2015 1:22 PM', '~/img/pdf.png', '0');
INSERT INTO `doc_documentos` VALUES ('6', '6', '4', 'lriylpeo.ioj acta 2 mayo 2013.pdf', '~/Repositorio/lriylpeo.ioj acta 2 mayo 2013.pdf', '245', '11/11/2015 12:36 PM', '~/img/pdf.png', '0');
INSERT INTO `doc_documentos` VALUES ('7', '1', '3', 'vzewr1z4.3gp marcel duchamp.docx', '~/Repositorio/vzewr1z4.3gp marcel duchamp.docx', '24', '12-03-2016 15:59', '~/img/word.png', '0');
INSERT INTO `doc_documentos` VALUES ('8', '6', '4', '1zglrxu3.40p BOLETIN-INFORMATIVO-MARZO2016.pdf', '~/Repositorio/1zglrxu3.40p BOLETIN-INFORMATIVO-MARZO2016.pdf', '578', '02-04-2016 18:20', '~/img/pdf.png', '1');

-- ----------------------------
-- Table structure for doc_documentos_usuario
-- ----------------------------
DROP TABLE IF EXISTS `doc_documentos_usuario`;
CREATE TABLE `doc_documentos_usuario` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `NOMBRE_ARCHIVO` varchar(250) NOT NULL DEFAULT '0',
  `URL` varchar(1000) NOT NULL DEFAULT '0',
  `TAMANO` int(11) NOT NULL DEFAULT '0',
  `FECHA_SUBIDA` varchar(50) DEFAULT NULL,
  `EXTENSION` varchar(250) DEFAULT NULL,
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of doc_documentos_usuario
-- ----------------------------
INSERT INTO `doc_documentos_usuario` VALUES ('1', '6', '4', 'spppigx0.ery 1zglrxu3.40p BOLETIN-INFORMATIVO-MARZO2016.pdf', '~/Repositorio/spppigx0.ery 1zglrxu3.40p BOLETIN-INFORMATIVO-MARZO2016.pdf', '578', '14-04-2016 18:50', '~/img/pdf.png', '1');
INSERT INTO `doc_documentos_usuario` VALUES ('2', '6', '4', '0tifeglf.mkw 1zglrxu3.40p BOLETIN-INFORMATIVO-MARZO2016.pdf', '~/Repositorio/0tifeglf.mkw 1zglrxu3.40p BOLETIN-INFORMATIVO-MARZO2016.pdf', '0', '14-04-2016 18:55', '~/img/pdf.png', '0');
INSERT INTO `doc_documentos_usuario` VALUES ('3', '1', '3', 'x2sblepa.535 ajax-loader.gif', '~/Repositorio/x2sblepa.535 ajax-loader.gif', '7', '23-04-2016 22:41', '~/img/gif.png', '0');

-- ----------------------------
-- Table structure for eg_elementos_grupo
-- ----------------------------
DROP TABLE IF EXISTS `eg_elementos_grupo`;
CREATE TABLE `eg_elementos_grupo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ID_GRUPO` int(11) NOT NULL DEFAULT '0',
  `HREF_ITEM` varchar(250) DEFAULT '0',
  `CLASS_ITEM` varchar(250) DEFAULT '0',
  `NOMBRE` varchar(250) DEFAULT '0',
  `ROLES_ID` varchar(500) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of eg_elementos_grupo
-- ----------------------------
INSERT INTO `eg_elementos_grupo` VALUES ('1', '1', '#', '', 'Gestión Contable', '1');
INSERT INTO `eg_elementos_grupo` VALUES ('2', '1', '#', '', 'Ajuste de Precios', '1');
INSERT INTO `eg_elementos_grupo` VALUES ('3', '1', '#', '', 'Auditoria de Uso', '1');
INSERT INTO `eg_elementos_grupo` VALUES ('4', '1', '#', '', 'Precio y Condiciones Servicio', '1');
INSERT INTO `eg_elementos_grupo` VALUES ('5', '1', '#', '', 'Suscripción Gratuita', '1');
INSERT INTO `eg_elementos_grupo` VALUES ('6', '1', '#', '', 'Suscripción Pagada', '1');
INSERT INTO `eg_elementos_grupo` VALUES ('7', '2', 'Usuario/VistaDocumentos.aspx', '', 'Descarga Asambleas', '1,2,3,4,5,6,7,8');
INSERT INTO `eg_elementos_grupo` VALUES ('8', '2', 'Usuario/VistaRendiciones.aspx', '', 'Visualización de Rendiciones', '1,2,3,4,5,6,7,8');
INSERT INTO `eg_elementos_grupo` VALUES ('9', '2', '#', '', 'Directiva CPAS', '1,2,3,4,5,6,7,8,9');
INSERT INTO `eg_elementos_grupo` VALUES ('10', '2', 'Usuario/VistaCalendario.aspx', '', 'Calendario', '1,2,3,4,5,6,7,8');
INSERT INTO `eg_elementos_grupo` VALUES ('11', '6', 'Administracion/Usuarios.aspx', '', 'Usuarios CPAS', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('12', '6', 'Administracion/cargaMasiva.aspx', '', 'Carga Masiva Padres/Apoderados', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('13', '6', 'Administracion/documentos.aspx', '', 'Actas de Asamblea', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('14', '6', 'Administracion/IngresoEgreso.aspx', '', 'Rendiciones', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('15', '7', 'Proyectos/Listado.aspx', '', 'Proyectos', '1,2,3,4,5,6,7,10');
INSERT INTO `eg_elementos_grupo` VALUES ('16', '7', 'Tricel/ListadoTricel.aspx', '', 'Gestión TRICEL', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('17', '8', 'Usuario/VistaCalendario.aspx', '', 'Eventos y Actividades', '1,2,3,4,5,6,7,10');
INSERT INTO `eg_elementos_grupo` VALUES ('18', '9', '#', '', 'Generación de Mailing Proximamente!', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('19', '10', 'Contacto.aspx', '', 'Formulario de Ingreso', '2,3,4,5,6,7,8,9,10');
INSERT INTO `eg_elementos_grupo` VALUES ('20', '11', 'RecuperarClave/Recuperar.aspx', '', 'Cambiar mi clave', '1,2,3,4,5,6,7,8,9,10');
INSERT INTO `eg_elementos_grupo` VALUES ('21', '6', 'Administracion/EditorEncabezado.aspx', '', 'Configuración Institución', '1,2,3,4,5,6,7');
INSERT INTO `eg_elementos_grupo` VALUES ('22', '7', 'Tricel/Listado.aspx', '', 'Listas TRICEL', '1,2,3,4,5,6,7,8,9');
INSERT INTO `eg_elementos_grupo` VALUES ('23', '2', 'Usuario/VistaDocumentosUsuario.aspx', '', 'Descarga Documentos', '1,2,3,4,5,6,7,8');
INSERT INTO `eg_elementos_grupo` VALUES ('24', '6', 'Administracion/documentosUsuario.aspx', '', 'Documentos de Usuario', '1,2,3,4,5,6,7,10');
INSERT INTO `eg_elementos_grupo` VALUES ('25', '6', 'Administracion/Monitoreo.aspx', '', 'Monitoreo', '1');

-- ----------------------------
-- Table structure for enc_encabezado
-- ----------------------------
DROP TABLE IF EXISTS `enc_encabezado`;
CREATE TABLE `enc_encabezado` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USA_IMAGEN_SUPERIOR` int(11) NOT NULL DEFAULT '0',
  `URL_IMAGEN_SUPERIOR` varchar(500) NOT NULL DEFAULT '0',
  `TITULO_ENCABEZADO` varchar(1000) NOT NULL DEFAULT '0',
  `SUBTITULO_ENCABEZADO` varchar(1000) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of enc_encabezado
-- ----------------------------
INSERT INTO `enc_encabezado` VALUES ('1', '1', '~/img/encabezadoCPAS_1.png', 'CPAS', 'Centro de Padres y Apoderados', '0', '3');
INSERT INTO `enc_encabezado` VALUES ('2', '1', '~/img/encabezadoCPAS_2.png', 'Colegio Santa Elena ', 'CGPA', '0', '4');
INSERT INTO `enc_encabezado` VALUES ('3', '1', '~/img/encabezadoCPAS_2.png', 'Colegio Santa Elena ', 'CGPA', '0', '5');

-- ----------------------------
-- Table structure for gi_grupo_item
-- ----------------------------
DROP TABLE IF EXISTS `gi_grupo_item`;
CREATE TABLE `gi_grupo_item` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ES_GRUPO` int(11) NOT NULL DEFAULT '0',
  `HREF_GRUPO` varchar(250) DEFAULT '0',
  `CLASS_GRUPO` varchar(250) DEFAULT '0',
  `NOMBRE_GRUPO` varchar(250) DEFAULT '0',
  `ROLES_ID` varchar(500) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of gi_grupo_item
-- ----------------------------
INSERT INTO `gi_grupo_item` VALUES ('1', '1', '#', '', 'Adm. General', '1');
INSERT INTO `gi_grupo_item` VALUES ('2', '1', '#', '', 'Usuario', '1,2,3,4,5,6,7,8');
INSERT INTO `gi_grupo_item` VALUES ('6', '1', '#', '', 'Adm. CPAS', '1,2,3,4,5,6,7,10');
INSERT INTO `gi_grupo_item` VALUES ('7', '1', '#', '', 'Democrática CPAS', '1,2,3,4,5,6,7,9,10');
INSERT INTO `gi_grupo_item` VALUES ('8', '1', '#', '', 'Calendario', '1,2,3,4,5,6,7,10');
INSERT INTO `gi_grupo_item` VALUES ('9', '1', '#', '', 'Comunicaciones', '1,2,3,4,5,6,7');
INSERT INTO `gi_grupo_item` VALUES ('10', '1', '#', '', 'Contacto', '2,3,4,5,6,7,8,9,10');
INSERT INTO `gi_grupo_item` VALUES ('11', '1', '#', '', 'Ayuda', '1,2,3,4,5,6,7,8,9,10');
INSERT INTO `gi_grupo_item` VALUES ('12', '0', 'Usuario/VistaDocumentos.aspx', '', 'Documentos', '9,10');
INSERT INTO `gi_grupo_item` VALUES ('13', '0', 'Usuario/VistaRendiciones.aspx', '', 'Rendiciones', '9,10');
INSERT INTO `gi_grupo_item` VALUES ('14', '0', 'Usuario/VistaCalendario.aspx', '', 'Calendario', '9,10');
INSERT INTO `gi_grupo_item` VALUES ('15', '0', 'Reportes/SeleccionReporte.aspx', '', 'Reportes', '1,2,3,4,5,6,7,8,9');

-- ----------------------------
-- Table structure for ieg_ingreso_egreso
-- ----------------------------
DROP TABLE IF EXISTS `ieg_ingreso_egreso`;
CREATE TABLE `ieg_ingreso_egreso` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `FECHA_MOVIMIENTO` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `TIPO_MOVIMIENTO` int(11) NOT NULL DEFAULT '0',
  `NUMERO_COMPROBANTE` varchar(100) NOT NULL DEFAULT '0',
  `DETALLE` varchar(500) NOT NULL DEFAULT '0',
  `MONTO` int(11) NOT NULL DEFAULT '0',
  `URL_DOCUMENTO` varchar(250) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ieg_ingreso_egreso
-- ----------------------------
INSERT INTO `ieg_ingreso_egreso` VALUES ('1', '3', '1', '2015-12-11 00:00:00', '2', '12534', 'compra de prueba', '55000', '~/Boletas/ke0gl4cs.gtk boleta.png', '1');
INSERT INTO `ieg_ingreso_egreso` VALUES ('2', '3', '1', '2015-12-11 00:00:00', '1', 'No hay', 'Prueba 2', '33000', '~/Boletas/5zmgu0mg.ajv Screenshot 2015-10-26-10-29-12.png', '1');
INSERT INTO `ieg_ingreso_egreso` VALUES ('3', '3', '1', '2015-12-11 00:00:00', '1', 'No hay', 'Reposición se dinero', '345000', '', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('4', '3', '1', '2015-12-11 00:00:00', '2', '1234', 'Compra de tortas', '49000', '', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('5', '4', '6', '2015-12-11 00:00:00', '1', '001', 'Ingreso por pago de CGPA colegio', '5000000', '', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('6', '4', '6', '2015-12-10 23:00:00', '2', '002', 'Boleta de reparación CASINO', '150000', '~/Boletas/lsxj152c.dfk Boleta Compra sopapo.jpg', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('7', '4', '6', '2015-12-11 00:00:00', '2', '002', 'Instalación de gradería ', '40000000', '', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('8', '3', '1', '2016-03-12 20:27:20', '1', '5487999', 'pago de mensualidad coronado', '650000', '', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('9', '4', '6', '2016-04-16 19:40:34', '2', '00134', 'Honorarios por auditoria contable', '1500000', '', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('10', '4', '7', '2016-05-16 14:08:11', '2', '37321', 'Comida para la directiva', '12600', '~/Boletas/qyw1ogcy.rzb 1463432839391-1884845293.jpg', '0');
INSERT INTO `ieg_ingreso_egreso` VALUES ('11', '3', '1', '2016-08-10 08:55:07', '1', '154879', 'Compra de materiales', '125876', '~/Boletas/wia1du44.tsr entrar.jpg', '0');

-- ----------------------------
-- Table structure for inst_institucion
-- ----------------------------
DROP TABLE IF EXISTS `inst_institucion`;
CREATE TABLE `inst_institucion` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NOMBRE` varchar(500) NOT NULL DEFAULT '0',
  `REG_ID` int(11) NOT NULL,
  `COM_ID` int(11) NOT NULL,
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of inst_institucion
-- ----------------------------
INSERT INTO `inst_institucion` VALUES ('3', 'CPAS', '15', '295', '0');
INSERT INTO `inst_institucion` VALUES ('4', 'Colegio de Prueba', '15', '295', '0');
INSERT INTO `inst_institucion` VALUES ('5', 'Colegio Santa Elena', '15', '295', '0');

-- ----------------------------
-- Table structure for lgu_login_usuario
-- ----------------------------
DROP TABLE IF EXISTS `lgu_login_usuario`;
CREATE TABLE `lgu_login_usuario` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `FECHA_MOVIMIENTO` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `TIPO_MOVIMIENTO` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=337 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of lgu_login_usuario
-- ----------------------------
INSERT INTO `lgu_login_usuario` VALUES ('1', '1', '2016-02-18 10:32:41', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('2', '1', '2016-02-18 10:33:08', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('3', '6', '2016-02-18 10:33:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('4', '6', '2016-02-18 10:33:53', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('5', '1', '2016-02-18 11:32:45', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('6', '1', '2016-02-18 13:22:55', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('7', '1', '2016-02-18 13:23:52', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('8', '1', '2016-02-19 06:14:02', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('9', '1', '2016-02-19 06:15:16', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('10', '1', '2016-02-19 12:48:06', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('11', '7', '2016-02-19 07:56:56', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('12', '7', '2016-02-19 07:58:00', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('13', '1', '2016-02-25 03:49:23', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('14', '1', '2016-02-25 03:49:49', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('15', '6', '2016-02-25 03:49:59', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('16', '6', '2016-02-25 03:51:38', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('17', '6', '2016-03-03 07:29:50', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('18', '9', '2016-03-03 07:55:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('19', '9', '2016-03-03 07:56:28', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('20', '1', '2016-03-08 07:31:42', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('21', '1', '2016-03-08 07:38:20', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('22', '1', '2016-03-08 07:38:45', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('23', '1', '2016-03-08 07:39:55', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('24', '1', '2016-03-08 07:52:09', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('25', '1', '2016-03-08 09:03:45', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('26', '1', '2016-03-08 09:27:39', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('27', '1', '2016-03-08 09:49:01', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('28', '1', '2016-03-08 09:54:06', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('29', '1', '2016-03-08 10:17:17', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('30', '1', '2016-03-08 10:59:22', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('31', '1', '2016-03-08 11:03:19', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('32', '1', '2016-03-08 11:53:54', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('33', '1', '2016-03-08 12:27:36', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('34', '1', '2016-03-08 13:03:16', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('35', '1', '2016-03-08 13:13:51', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('36', '1', '2016-03-09 07:12:36', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('37', '1', '2016-03-09 07:52:36', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('38', '1', '2016-03-09 07:55:48', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('39', '1', '2016-03-09 09:26:42', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('40', '1', '2016-03-09 10:45:58', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('41', '1', '2016-03-09 11:25:54', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('42', '1', '2016-03-09 16:12:23', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('43', '1', '2016-03-10 09:24:48', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('44', '1', '2016-03-10 09:25:32', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('45', '1', '2016-03-10 09:25:35', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('46', '1', '2016-03-10 09:29:01', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('47', '1', '2016-03-10 09:29:22', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('48', '1', '2016-03-10 11:11:18', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('49', '1', '2016-03-10 12:34:33', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('50', '1', '2016-03-10 17:54:27', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('51', '1', '2016-03-10 18:26:53', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('52', '1', '2016-03-10 18:27:16', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('53', '1', '2016-03-10 18:27:19', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('54', '1', '2016-03-11 08:04:43', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('55', '1', '2016-03-11 08:08:37', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('56', '1', '2016-03-11 08:09:18', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('57', '1', '2016-03-11 08:09:24', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('58', '1', '2016-03-11 08:49:47', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('59', '6', '2016-03-11 09:10:05', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('60', '1', '2016-03-12 13:32:55', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('61', '1', '2016-03-12 15:19:05', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('62', '1', '2016-03-12 15:59:02', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('63', '1', '2016-03-12 17:07:53', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('64', '1', '2016-03-12 17:47:53', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('65', '1', '2016-03-12 18:35:31', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('66', '1', '2016-03-12 18:50:24', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('67', '1', '2016-03-12 18:54:44', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('68', '1', '2016-03-12 18:56:38', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('69', '8', '2016-03-14 07:36:19', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('70', '1', '2016-03-14 08:57:59', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('71', '1', '2016-03-15 11:20:03', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('72', '1', '2016-03-15 11:26:05', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('73', '6', '2016-03-15 18:53:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('74', '6', '2016-03-15 18:56:47', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('75', '1', '2016-03-22 04:52:43', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('76', '1', '2016-03-22 04:55:48', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('77', '1', '2016-03-22 04:56:46', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('78', '1', '2016-03-22 04:59:20', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('79', '1', '2016-03-22 04:59:38', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('80', '1', '2016-03-22 05:00:09', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('81', '1', '2016-03-22 05:03:29', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('82', '1', '2016-03-22 05:04:18', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('83', '1', '2016-03-22 05:06:33', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('84', '1', '2016-03-22 05:10:25', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('85', '1', '2016-03-23 07:00:20', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('86', '1', '2016-03-23 10:28:36', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('87', '6', '2016-04-02 13:48:29', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('88', '6', '2016-04-02 13:50:39', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('89', '6', '2016-04-02 13:51:13', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('90', '6', '2016-04-02 13:51:36', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('91', '6', '2016-04-02 18:07:04', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('92', '6', '2016-04-02 18:25:05', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('93', '6', '2016-04-02 18:25:12', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('94', '1', '2016-04-02 18:37:42', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('95', '6', '2016-04-02 18:51:50', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('96', '6', '2016-04-02 18:53:52', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('97', '6', '2016-04-02 19:07:37', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('98', '6', '2016-04-02 19:07:44', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('99', '6', '2016-04-04 12:40:10', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('100', '6', '2016-04-04 12:56:25', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('101', '1', '2016-04-07 05:52:41', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('102', '1', '2016-04-07 05:54:02', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('103', '6', '2016-04-07 05:54:28', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('104', '6', '2016-04-07 06:05:19', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('105', '6', '2016-04-07 06:23:09', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('106', '6', '2016-04-07 06:27:02', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('107', '9', '2016-04-07 06:27:11', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('108', '9', '2016-04-07 07:21:40', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('109', '6', '2016-04-12 05:25:52', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('110', '6', '2016-04-12 05:30:24', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('111', '8', '2016-04-14 17:56:51', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('112', '1', '2016-04-14 18:33:08', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('113', '8', '2016-04-14 18:46:54', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('114', '8', '2016-04-14 18:48:19', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('115', '6', '2016-04-14 18:49:29', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('116', '6', '2016-04-14 19:06:36', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('117', '10', '2016-04-14 19:07:11', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('118', '10', '2016-04-14 19:09:55', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('119', '6', '2016-04-14 19:10:07', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('120', '6', '2016-04-14 19:19:19', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('121', '10', '2016-04-14 19:19:31', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('122', '10', '2016-04-14 19:20:57', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('123', '11', '2016-04-14 19:21:10', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('124', '11', '2016-04-14 19:21:54', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('125', '8', '2016-04-14 19:22:06', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('126', '8', '2016-04-14 19:22:44', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('127', '11', '2016-04-14 19:23:00', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('128', '11', '2016-04-14 19:23:34', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('129', '10', '2016-04-14 19:23:43', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('130', '10', '2016-04-14 19:24:04', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('131', '9', '2016-04-14 19:24:22', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('132', '9', '2016-04-14 19:26:03', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('133', '7', '2016-04-14 19:26:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('134', '7', '2016-04-14 19:27:21', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('135', '6', '2016-04-14 19:27:31', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('136', '6', '2016-04-14 19:29:12', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('137', '6', '2016-04-14 19:41:02', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('138', '7', '2016-04-15 08:05:29', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('139', '6', '2016-04-15 08:13:11', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('140', '7', '2016-04-15 08:21:32', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('141', '8', '2016-04-15 08:21:46', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('142', '8', '2016-04-15 08:22:10', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('143', '9', '2016-04-15 08:22:25', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('144', '9', '2016-04-15 08:22:44', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('145', '10', '2016-04-15 08:22:55', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('146', '9', '2016-04-15 08:29:22', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('147', '9', '2016-04-15 08:29:39', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('148', '8', '2016-04-15 08:29:54', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('149', '8', '2016-04-15 08:30:53', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('150', '10', '2016-04-15 08:31:04', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('151', '10', '2016-04-15 09:50:44', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('152', '6', '2016-04-15 10:27:47', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('153', '6', '2016-04-15 10:44:16', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('154', '6', '2016-04-15 10:45:57', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('155', '7', '2016-04-15 12:57:23', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('156', '7', '2016-04-15 13:10:23', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('157', '8', '2016-04-15 13:10:33', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('158', '8', '2016-04-15 13:11:58', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('159', '9', '2016-04-15 13:12:12', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('160', '10', '2016-04-15 13:13:48', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('161', '10', '2016-04-15 13:16:34', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('162', '7', '2016-04-15 13:16:51', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('163', '6', '2016-04-15 13:23:31', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('164', '6', '2016-04-15 13:25:42', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('165', '7', '2016-04-15 13:33:38', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('166', '6', '2016-04-15 13:33:48', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('167', '7', '2016-04-15 13:34:57', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('168', '6', '2016-04-15 13:35:52', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('169', '7', '2016-04-15 13:36:13', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('170', '9', '2016-04-15 13:47:34', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('171', '7', '2016-04-15 14:50:08', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('172', '7', '2016-04-15 14:58:02', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('173', '7', '2016-04-15 14:58:59', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('174', '7', '2016-04-15 15:00:00', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('175', '7', '2016-04-15 15:05:19', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('176', '6', '2016-04-16 09:57:35', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('177', '6', '2016-04-16 10:05:54', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('178', '4', '2016-04-16 10:06:53', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('179', '6', '2016-04-16 10:35:58', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('180', '6', '2016-04-16 18:51:35', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('181', '6', '2016-04-16 19:16:45', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('182', '6', '2016-04-16 19:41:34', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('183', '7', '2016-04-16 19:41:47', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('184', '1', '2016-04-18 11:36:44', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('185', '6', '2016-04-18 08:58:32', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('186', '1', '2016-04-18 09:54:50', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('187', '1', '2016-04-18 09:56:37', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('188', '1', '2016-04-18 10:10:58', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('189', '1', '2016-04-18 10:26:27', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('190', '18', '2016-04-18 10:59:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('191', '18', '2016-04-18 12:03:27', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('192', '18', '2016-04-18 12:04:47', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('193', '18', '2016-04-18 12:17:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('194', '18', '2016-04-18 12:22:32', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('195', '18', '2016-04-18 12:38:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('196', '18', '2016-04-18 12:38:58', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('197', '18', '2016-04-18 14:45:07', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('198', '18', '2016-04-18 14:54:44', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('199', '18', '2016-04-18 15:44:46', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('200', '18', '2016-04-18 15:49:59', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('201', '1', '2016-04-22 08:02:19', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('202', '1', '2016-04-22 08:22:25', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('203', '1', '2016-04-22 17:44:37', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('204', '1', '2016-04-23 20:19:12', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('205', '1', '2016-04-23 20:54:16', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('206', '1', '2016-04-23 21:01:35', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('207', '1', '2016-04-23 21:08:17', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('208', '1', '2016-04-23 21:16:34', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('209', '1', '2016-04-23 22:29:59', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('210', '1', '2016-04-23 22:31:27', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('211', '1', '2016-04-23 22:38:48', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('212', '1', '2016-04-23 22:42:29', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('213', '1', '2016-04-23 22:44:20', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('214', '1', '2016-04-23 22:46:25', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('215', '7', '2016-04-23 22:47:06', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('216', '1', '2016-04-23 23:19:03', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('217', '1', '2016-04-23 23:19:12', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('218', '1', '2016-04-25 13:55:06', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('219', '1', '2016-04-25 13:57:45', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('220', '1', '2016-04-25 13:57:59', 'Ingresar Android');
INSERT INTO `lgu_login_usuario` VALUES ('221', '1', '2016-04-25 14:01:34', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('222', '8', '2016-05-05 19:14:41', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('223', '7', '2016-05-05 19:20:39', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('224', '7', '2016-05-05 19:29:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('225', '7', '2016-05-05 19:35:00', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('226', '20', '2016-05-05 19:36:38', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('227', '20', '2016-05-05 19:37:49', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('228', '18', '2016-05-05 19:39:14', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('229', '18', '2016-05-05 19:48:45', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('230', '7', '2016-05-05 19:49:36', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('231', '7', '2016-05-05 19:52:49', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('232', '7', '2016-05-05 20:02:40', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('233', '21', '2016-05-05 20:03:04', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('234', '21', '2016-05-05 20:03:38', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('235', '18', '2016-05-06 07:19:17', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('236', '18', '2016-05-06 07:39:03', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('237', '22', '2016-05-06 07:39:10', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('238', '1', '2016-05-06 07:41:01', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('239', '22', '2016-05-06 07:41:04', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('240', '18', '2016-05-06 07:41:12', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('241', '18', '2016-05-06 07:45:32', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('242', '23', '2016-05-06 07:45:40', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('243', '23', '2016-05-06 07:45:50', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('244', '18', '2016-05-06 07:46:01', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('245', '18', '2016-05-06 07:51:37', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('246', '24', '2016-05-06 07:51:54', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('247', '24', '2016-05-06 07:52:21', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('248', '1', '2016-05-06 07:55:04', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('249', '1', '2016-05-06 07:55:21', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('250', '1', '2016-05-06 07:59:08', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('251', '18', '2016-05-06 08:06:20', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('252', '1', '2016-05-06 08:09:40', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('253', '18', '2016-05-06 08:20:47', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('254', '8', '2016-05-06 08:29:10', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('255', '1', '2016-05-06 14:59:50', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('256', '1', '2016-05-06 15:00:42', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('257', '18', '2016-05-07 16:53:27', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('258', '7', '2016-05-07 16:55:25', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('259', '18', '2016-05-08 14:32:42', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('260', '1', '2016-05-10 07:51:35', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('261', '1', '2016-05-10 07:54:00', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('262', '18', '2016-05-12 09:33:01', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('263', '18', '2016-05-12 10:20:02', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('264', '18', '2016-05-15 10:47:59', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('265', '18', '2016-05-15 10:49:55', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('266', '1', '2016-05-15 17:37:17', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('267', '1', '2016-05-15 17:38:00', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('268', '22', '2016-05-15 20:24:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('269', '7', '2016-05-16 08:52:55', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('270', '1', '2016-05-16 13:58:53', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('271', '1', '2016-05-16 14:01:27', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('272', '7', '2016-05-16 14:05:40', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('273', '1', '2016-05-16 14:13:17', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('274', '1', '2016-05-16 14:15:17', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('275', '18', '2016-05-18 12:18:53', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('276', '18', '2016-05-18 12:28:19', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('277', '9', '2016-05-18 12:28:38', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('278', '9', '2016-05-18 12:28:54', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('279', '9', '2016-05-18 12:29:57', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('280', '22', '2016-05-21 15:13:59', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('281', '1', '2016-05-24 21:54:37', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('282', '18', '2016-06-05 07:39:39', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('283', '6', '2016-06-08 17:41:52', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('284', '6', '2016-06-08 17:43:34', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('285', '1', '2016-06-08 17:44:06', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('286', '1', '2016-06-08 17:47:58', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('287', '1', '2016-06-29 19:41:26', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('288', '18', '2016-06-30 05:43:27', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('289', '18', '2016-07-04 15:08:59', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('290', '18', '2016-07-05 10:44:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('291', '18', '2016-08-01 14:58:20', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('292', '18', '2016-08-09 10:57:29', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('293', '1', '2016-08-10 08:43:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('294', '1', '2016-08-10 08:45:50', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('295', '1', '2016-08-10 08:46:00', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('296', '1', '2016-08-10 08:58:01', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('297', '1', '2016-08-10 18:09:41', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('298', '1', '2016-08-10 18:12:49', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('299', '22', '2016-08-17 16:00:44', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('300', '1', '2016-08-25 17:29:21', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('301', '1', '2016-08-25 17:33:53', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('302', '1', '2016-09-28 12:35:15', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('303', '18', '2016-10-31 10:26:52', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('304', '18', '2016-10-31 10:28:37', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('305', '7', '2016-11-04 12:09:26', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('306', '9', '2016-11-04 12:53:23', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('307', '7', '2016-11-08 08:41:14', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('308', '1', '2016-11-24 13:16:43', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('309', '1', '2016-11-24 13:20:39', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('310', '1', '2016-12-01 03:54:27', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('311', '1', '2016-12-01 05:28:43', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('312', '1', '2016-12-01 05:50:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('313', '1', '2016-12-01 11:28:32', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('314', '1', '2016-12-03 14:12:31', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('315', '1', '2016-12-03 16:02:17', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('316', '1', '2016-12-05 13:25:18', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('317', '1', '2016-12-05 13:52:04', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('318', '1', '2017-01-08 08:34:04', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('319', '1', '2017-01-08 13:57:01', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('320', '1', '2017-01-08 13:58:07', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('321', '1', '2017-01-08 14:01:13', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('322', '1', '2017-01-08 14:01:50', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('323', '1', '2017-01-08 14:06:55', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('324', '1', '2017-01-08 14:07:01', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('325', '1', '2017-01-08 14:09:45', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('326', '10', '2017-01-08 14:09:55', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('327', '10', '2017-01-08 14:10:08', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('328', '1', '2017-01-08 14:10:11', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('329', '1', '2017-01-08 14:11:33', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('330', '1', '2017-01-08 14:11:50', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('331', '1', '2017-01-08 14:13:10', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('332', '1', '2017-01-08 14:13:31', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('333', '1', '2017-01-08 14:31:56', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('334', '1', '2017-01-08 14:32:31', 'Ingresar');
INSERT INTO `lgu_login_usuario` VALUES ('335', '1', '2017-01-08 14:34:17', 'Salir');
INSERT INTO `lgu_login_usuario` VALUES ('336', '1', '2017-01-12 23:36:52', 'Ingresar');

-- ----------------------------
-- Table structure for ltr_lista_tricel
-- ----------------------------
DROP TABLE IF EXISTS `ltr_lista_tricel`;
CREATE TABLE `ltr_lista_tricel` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TRI_ID` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `ROL` varchar(500) NOT NULL,
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  `NOMBRE` varchar(500) DEFAULT NULL,
  `OBJETIVO` varchar(500) DEFAULT NULL,
  `DESCRIPCION` varchar(500) DEFAULT NULL,
  `BENEFICIOS` varchar(500) DEFAULT NULL,
  `FECHA_INICIO` datetime DEFAULT CURRENT_TIMESTAMP,
  `FECHA_TERMINO` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ltr_lista_tricel
-- ----------------------------
INSERT INTO `ltr_lista_tricel` VALUES ('1', '1', '4', '6', 'Administrador', '0', 'Lista 1', 'objetivo Lista 1', 'Descripcion Lista 1', 'Beneficios Lista 1', '2016-02-17 00:00:00', '2016-02-26 00:00:00');
INSERT INTO `ltr_lista_tricel` VALUES ('2', '1', '4', '7', 'Administrador', '0', 'Lista 2', 'Objetivo lista 2', 'Descripcion lista 2', 'beneficios lista 2', '2016-02-17 00:00:00', '2016-02-28 00:00:00');
INSERT INTO `ltr_lista_tricel` VALUES ('3', '2', '3', '1', 'Administrador', '0', 'lista de prueba ', 'Objetivo de prueba', 'Descripción de pruebs', 'Beneficia', '2016-03-06 00:00:00', '2016-03-29 00:00:00');
INSERT INTO `ltr_lista_tricel` VALUES ('4', '3', '3', '1', 'Administrador', '0', 'lista coro', 'Obj', 'Desc', 'Bene', '2016-03-09 00:00:00', '2016-03-28 00:00:00');
INSERT INTO `ltr_lista_tricel` VALUES ('5', '4', '3', '1', 'Administrador', '0', 'victor', 'Obj', 'Desc', 'Bene', '2016-03-08 00:00:00', '2016-03-28 00:00:00');
INSERT INTO `ltr_lista_tricel` VALUES ('6', '5', '3', '1', 'Administrador', '1', 'lista favorita', 'Obj', 'Descripción', 'Beneficia', '2016-03-11 00:00:00', '2016-03-28 00:00:00');
INSERT INTO `ltr_lista_tricel` VALUES ('7', '6', '3', '1', 'Administrador', '0', 'Lista Tricel de Votación', 'Votar', 'Prueba', 'Ayudar a los usuarios', '2016-12-01 00:00:00', '2016-12-27 00:00:00');

-- ----------------------------
-- Table structure for per_persona
-- ----------------------------
DROP TABLE IF EXISTS `per_persona`;
CREATE TABLE `per_persona` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RUT` varchar(50) NOT NULL,
  `NOMBRES` varchar(250) DEFAULT NULL,
  `APELLIDO_PATERNO` varchar(250) DEFAULT NULL,
  `APELLIDO_MATERNO` varchar(250) DEFAULT NULL,
  `PAIS_ID` int(11) NOT NULL,
  `REG_ID` int(11) NOT NULL,
  `COM_ID` int(11) NOT NULL,
  `DIRECCION_COMPLETA` varchar(500) DEFAULT NULL,
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `USU_ID` int(11) NOT NULL,
  `TELEFONOS` varchar(250) DEFAULT NULL,
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of per_persona
-- ----------------------------
INSERT INTO `per_persona` VALUES ('3', '12535306-1', 'VICTOR HUGO', 'CORONADO', 'ALARCON', '0', '15', '327', 'Pasaje Cordón Roma 0621 Puente Alto', '3', '1', '85006988', '0');
INSERT INTO `per_persona` VALUES ('6', '13671095-8', 'González', 'Garcia', '', '0', '15', '295', 'Prueba', '3', '4', '+56994359451', '0');
INSERT INTO `per_persona` VALUES ('7', '11111111-1', 'Prueba', 'CORONADO', 'ALARCON', '0', '15', '300', 'Prueba', '3', '5', '76890000', '0');
INSERT INTO `per_persona` VALUES ('8', '11111111-1', 'Victor', 'Garcia', 'G', '0', '15', '295', 'asas', '4', '6', '', '0');
INSERT INTO `per_persona` VALUES ('9', '11111111-1', 'victor', 'garcia', '', '0', '15', '295', 'pepepep', '4', '7', '', '0');
INSERT INTO `per_persona` VALUES ('10', '22222222-2', 'victor', 'garcia', 'Dos', '0', '15', '295', 'asasas', '4', '8', '', '0');
INSERT INTO `per_persona` VALUES ('11', '55555555-5', 'victor', 'garcia', 'Tres', '0', '15', '295', 'asasasas', '4', '9', '', '0');
INSERT INTO `per_persona` VALUES ('12', '55555555-5', 'victor', 'garcia', 'Cuatro', '0', '15', '295', 'asasas', '4', '10', '', '0');
INSERT INTO `per_persona` VALUES ('13', '99999999-9', 'victor', 'garcia', 'Seis', '0', '15', '295', 'asas', '4', '12', '', '0');
INSERT INTO `per_persona` VALUES ('14', '10452691-8', 'Pedro Pablo', 'Contreras', '', '0', '15', '327', '', '3', '13', '569874521', '0');
INSERT INTO `per_persona` VALUES ('15', '12193014-5', 'Ines', 'Retamales', 'Contreras', '0', '15', '327', '', '3', '14', '5698745121', '0');
INSERT INTO `per_persona` VALUES ('16', '10452691-8', 'Otro', 'Otro', '', '0', '15', '327', '', '3', '15', '56985006981', '0');
INSERT INTO `per_persona` VALUES ('17', '10452691-8', 'Pedro Pablo', 'Contreras', '', '0', '15', '327', 'prueba de direccion', '3', '16', '569874521', '0');
INSERT INTO `per_persona` VALUES ('18', '12193014-5', 'Ines', 'Retamales', 'Contreras', '0', '15', '327', '', '3', '17', '5698745121', '0');
INSERT INTO `per_persona` VALUES ('19', '77777777-7', 'Victor ', 'Garcia', 'Cinco', '0', '15', '295', 'asas', '4', '11', '', '0');
INSERT INTO `per_persona` VALUES ('20', '11111111-1', 'Victor', 'García', '', '0', '15', '295', 'No registrada', '5', '18', '', '0');
INSERT INTO `per_persona` VALUES ('21', '13597434-k', 'Danitza', 'Salazar', 'Ojeda', '0', '15', '295', '', '4', '19', '96278728', '0');
INSERT INTO `per_persona` VALUES ('22', '11111111-1', 'Victor', 'García', 'Gonzalez', '0', '15', '295', '', '4', '20', '94359451', '0');
INSERT INTO `per_persona` VALUES ('23', '12312312-3', 'Danitza ', 'Salazar ', 'Ojeda ', '0', '15', '295', 'Gral.  Bulnes', '4', '21', '227868835', '0');
INSERT INTO `per_persona` VALUES ('24', '09166213-2', 'Miguel', 'Abarca', 'Soto', '0', '15', '295', 'Avda. Compañía #2398', '5', '22', '975778905', '0');
INSERT INTO `per_persona` VALUES ('25', '12239244-9', 'Marcia ', 'Quiroga', 'Bastias', '0', '15', '295', 'Avda. Compañía #2398', '5', '23', '975382632', '0');
INSERT INTO `per_persona` VALUES ('26', '12126594-k', 'Sandra', 'Rojas', 'Castillo', '0', '15', '295', 'Avda. Compañía #2398', '5', '24', '977579808', '0');
INSERT INTO `per_persona` VALUES ('27', '14052578-2', 'Daniela', 'Labbe', 'Fernandez', '0', '15', '295', 'Avda. Compañía #2398', '5', '25', '985968431', '0');
INSERT INTO `per_persona` VALUES ('28', '12535306-1', 'victor', 'coronado', 'a', '0', '15', '298', 'asasasas', '3', '30', '56985006988', '0');

-- ----------------------------
-- Table structure for pro_proyectos
-- ----------------------------
DROP TABLE IF EXISTS `pro_proyectos`;
CREATE TABLE `pro_proyectos` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FECHA_CREACION` datetime DEFAULT NULL,
  `USU_ID_CREADOR` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `NOMBRE` varchar(500) NOT NULL DEFAULT '0',
  `OBJETIVO` varchar(1500) DEFAULT '0',
  `DESCRIPCION` varchar(1500) DEFAULT '0',
  `BENEFICIOS` varchar(1500) DEFAULT '0',
  `COSTO` int(11) DEFAULT '0',
  `FECHA_INICIO` datetime DEFAULT NULL,
  `FECHA_TERMINO` datetime DEFAULT NULL,
  `ENVIA_CORREO` int(11) DEFAULT '0',
  `NOTIFICA_POPUP` int(11) DEFAULT '0',
  `ES_VIGENTE` int(11) DEFAULT '0',
  `ELIMINADO` int(11) DEFAULT '0',
  `FUE_APROBADO` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pro_proyectos
-- ----------------------------
INSERT INTO `pro_proyectos` VALUES ('1', '0001-01-01 00:00:00', '1', '3', 'Proyecto de Prueba Numero 1', 'El objetivo es probar la aplicacion', 'La idea es describir principal mente cual es el detalle del proyecto y cual es el alcance del mismo ', 'Aca se destaca el impacto y los beneficios esperados que resultaran de la ejecución del proyecto', '587000', '2015-11-08 00:00:00', '2015-11-29 00:00:00', '1', '1', '1', '0', '0');
INSERT INTO `pro_proyectos` VALUES ('2', '2015-11-10 18:29:56', '6', '4', 'Mejoramiento baños', 'Mejorar la infraestructura de los baños de niños', 'Breve descripción ', 'Breve descripción ', '8500000', '2015-11-10 00:00:00', '2016-05-31 00:00:00', '1', '1', '1', '1', '0');
INSERT INTO `pro_proyectos` VALUES ('3', '2015-11-10 18:38:02', '6', '4', 'Mejoramiento Acceso Principal', 'Mejorar el acceso principal del Establecimiento', 'Breve Descripcion', 'Para todos los alumnos', '3250000', '2015-11-10 00:00:00', '2015-11-24 00:00:00', '1', '1', '1', '1', '0');
INSERT INTO `pro_proyectos` VALUES ('4', '2015-12-13 20:04:57', '1', '3', 'prueba de proyecto 1', 'Objetivo', 'Descripción ', 'Beneficios ', '1548777', '2015-12-16 00:00:00', '2016-01-10 00:00:00', '0', '0', '1', '0', '0');
INSERT INTO `pro_proyectos` VALUES ('5', '2016-03-11 08:06:38', '1', '3', 'proyecto coro', 'Obj', 'Desc', 'Bene', '7000000', '2016-03-09 00:00:00', '2016-03-30 00:00:00', '0', '0', '1', '0', '0');
INSERT INTO `pro_proyectos` VALUES ('6', '2016-04-14 19:19:02', '6', '4', 'Instalación de graderías en patio principal', 'Instalación de graderias en patio principal', 'Juegos de graderías para el patio', 'Comodidad en los actos y Eventos deportivos del colegio', '2500000', '2016-04-12 00:00:00', '2016-04-22 00:00:00', '1', '0', '1', '1', '0');
INSERT INTO `pro_proyectos` VALUES ('7', '2016-11-04 12:15:58', '7', '4', 'DEMO proyecto', 'El objetivo es  demostrar la funcionalidad democrática de proyectos.', 'Este proyecto tiene como foco poder entregar funciones de valor que puedan hacer match con CitiZen', 'Entregar funciones de votaciones online par aproyectos y releccion de directivas', '2500000', '2016-11-04 00:00:00', '2016-11-11 00:00:00', '1', '0', '1', '0', '0');
INSERT INTO `pro_proyectos` VALUES ('8', '2017-01-08 13:59:29', '1', '3', 'Prueba de Proyecto', 'Demostrar el tiempo necesario que tarda esta operación', 'Proyecto de Prueba', 'Para todos los Apoderados', '600000', '2017-01-07 00:00:00', '2017-01-31 00:00:00', '0', '0', '1', '1', '0');
INSERT INTO `pro_proyectos` VALUES ('9', '2017-01-08 14:02:47', '1', '3', 'Proyecto de Prueba', 'Demostrar el tiempo necesario que tarda la operación', 'Prueba', 'Para todos los apoderados', '600000', '2017-01-01 00:00:00', '2017-01-31 00:00:00', '0', '0', '1', '0', '0');

-- ----------------------------
-- Table structure for prov_provincia
-- ----------------------------
DROP TABLE IF EXISTS `prov_provincia`;
CREATE TABLE `prov_provincia` (
  `ID` int(11) NOT NULL COMMENT 'ID provincia',
  `REG_ID` int(11) NOT NULL COMMENT 'ID region asociada',
  `NOMBRE` varchar(30) COLLATE latin1_spanish_ci NOT NULL COMMENT 'Nombre descriptivo',
  `NUMERO_COMUNAS` int(11) NOT NULL COMMENT 'Numero de comunas',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci COMMENT='Lista de comunas por regiones';

-- ----------------------------
-- Records of prov_provincia
-- ----------------------------
INSERT INTO `prov_provincia` VALUES ('1', '1', 'ARICA', '2');
INSERT INTO `prov_provincia` VALUES ('2', '1', 'PARINACOTA', '2');
INSERT INTO `prov_provincia` VALUES ('3', '2', 'IQUIQUE', '2');
INSERT INTO `prov_provincia` VALUES ('4', '2', 'TAMARUGAL', '5');
INSERT INTO `prov_provincia` VALUES ('5', '3', 'ANTOFAGASTA', '4');
INSERT INTO `prov_provincia` VALUES ('6', '3', 'EL LOA', '3');
INSERT INTO `prov_provincia` VALUES ('7', '3', 'TOCOPILLA', '2');
INSERT INTO `prov_provincia` VALUES ('8', '4', 'COPIAPÓ', '3');
INSERT INTO `prov_provincia` VALUES ('9', '4', 'CHAÑARAL', '2');
INSERT INTO `prov_provincia` VALUES ('10', '4', 'HUASCO', '4');
INSERT INTO `prov_provincia` VALUES ('11', '5', 'ELQUI', '6');
INSERT INTO `prov_provincia` VALUES ('12', '5', 'CHOAPA', '4');
INSERT INTO `prov_provincia` VALUES ('13', '5', 'LIMARÍ', '5');
INSERT INTO `prov_provincia` VALUES ('14', '6', 'VALPARAÍSO', '7');
INSERT INTO `prov_provincia` VALUES ('15', '6', 'ISLA DE PASCUA', '1');
INSERT INTO `prov_provincia` VALUES ('16', '6', 'LOS ANDES', '4');
INSERT INTO `prov_provincia` VALUES ('17', '6', 'PETORCA', '5');
INSERT INTO `prov_provincia` VALUES ('18', '6', 'QUILLOTA', '5');
INSERT INTO `prov_provincia` VALUES ('19', '6', 'SAN ANTONIO', '6');
INSERT INTO `prov_provincia` VALUES ('20', '6', 'SAN FELIPE DE ACONCAGUA', '6');
INSERT INTO `prov_provincia` VALUES ('21', '6', 'MARGA MARGA', '4');
INSERT INTO `prov_provincia` VALUES ('22', '7', 'CACHAPOAL', '17');
INSERT INTO `prov_provincia` VALUES ('23', '7', 'CARDENAL CARO', '6');
INSERT INTO `prov_provincia` VALUES ('24', '7', 'COLCHAGUA', '10');
INSERT INTO `prov_provincia` VALUES ('25', '8', 'TALCA', '10');
INSERT INTO `prov_provincia` VALUES ('26', '8', 'CAUQUENES', '3');
INSERT INTO `prov_provincia` VALUES ('27', '8', 'CURICÓ', '9');
INSERT INTO `prov_provincia` VALUES ('28', '8', 'LINARES', '8');
INSERT INTO `prov_provincia` VALUES ('29', '9', 'CONCEPCIÓN', '12');
INSERT INTO `prov_provincia` VALUES ('30', '9', 'ARAUCO', '7');
INSERT INTO `prov_provincia` VALUES ('31', '9', 'BIOBÍO', '14');
INSERT INTO `prov_provincia` VALUES ('32', '9', 'ÑUBLE', '21');
INSERT INTO `prov_provincia` VALUES ('33', '10', 'CAUTÍN', '21');
INSERT INTO `prov_provincia` VALUES ('34', '10', 'MALLECO', '11');
INSERT INTO `prov_provincia` VALUES ('35', '11', 'VALDIVIA', '8');
INSERT INTO `prov_provincia` VALUES ('36', '11', 'RANCO', '4');
INSERT INTO `prov_provincia` VALUES ('37', '12', 'LLANQUIHUE', '9');
INSERT INTO `prov_provincia` VALUES ('38', '12', 'CHILOÉ', '10');
INSERT INTO `prov_provincia` VALUES ('39', '12', 'OSORNO', '7');
INSERT INTO `prov_provincia` VALUES ('40', '12', 'PALENA', '4');
INSERT INTO `prov_provincia` VALUES ('41', '13', 'COIHAIQUE', '2');
INSERT INTO `prov_provincia` VALUES ('42', '13', 'AISÉN', '3');
INSERT INTO `prov_provincia` VALUES ('43', '13', 'CAPITÁN PRAT', '3');
INSERT INTO `prov_provincia` VALUES ('44', '13', 'GENERAL CARRERA', '2');
INSERT INTO `prov_provincia` VALUES ('45', '14', 'MAGALLANES', '4');
INSERT INTO `prov_provincia` VALUES ('46', '14', 'ANTÁRTICA CHILENA', '2');
INSERT INTO `prov_provincia` VALUES ('47', '14', 'TIERRA DEL FUEGO', '3');
INSERT INTO `prov_provincia` VALUES ('48', '14', 'ULTIMA ESPERANZA', '2');
INSERT INTO `prov_provincia` VALUES ('49', '15', 'SANTIAGO', '32');
INSERT INTO `prov_provincia` VALUES ('50', '15', 'CORDILLERA', '3');
INSERT INTO `prov_provincia` VALUES ('51', '15', 'CHACABUCO', '3');
INSERT INTO `prov_provincia` VALUES ('52', '15', 'MAIPO', '4');
INSERT INTO `prov_provincia` VALUES ('53', '15', 'MELIPILLA', '5');
INSERT INTO `prov_provincia` VALUES ('54', '15', 'TALAGANTE', '5');

-- ----------------------------
-- Table structure for reg_region
-- ----------------------------
DROP TABLE IF EXISTS `reg_region`;
CREATE TABLE `reg_region` (
  `ID` int(11) NOT NULL COMMENT 'ID unico',
  `NOMBRE` varchar(60) COLLATE latin1_spanish_ci NOT NULL COMMENT 'Nombre extenso',
  `ROMANO` varchar(5) COLLATE latin1_spanish_ci NOT NULL COMMENT 'Número de región',
  `NUMERO_PROVINCIAS` int(11) NOT NULL COMMENT 'total provincias',
  `NUMERO_COMUNAS` int(11) NOT NULL COMMENT 'Total de comunas',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci COMMENT='Lista de regiones de Chile';

-- ----------------------------
-- Records of reg_region
-- ----------------------------
INSERT INTO `reg_region` VALUES ('1', 'ARICA Y PARINACOTA', 'XV', '2', '4');
INSERT INTO `reg_region` VALUES ('2', 'TARAPACÁ', 'I', '2', '7');
INSERT INTO `reg_region` VALUES ('3', 'ANTOFAGASTA', 'II', '3', '9');
INSERT INTO `reg_region` VALUES ('4', 'ATACAMA ', 'III', '3', '9');
INSERT INTO `reg_region` VALUES ('5', 'COQUIMBO ', 'IV', '3', '15');
INSERT INTO `reg_region` VALUES ('6', 'VALPARAÍSO ', 'V', '8', '38');
INSERT INTO `reg_region` VALUES ('7', 'DEL LIBERTADOR GRAL. BERNARDO O\'HIGGINS ', 'VI', '3', '33');
INSERT INTO `reg_region` VALUES ('8', 'DEL MAULE', 'VII', '4', '30');
INSERT INTO `reg_region` VALUES ('9', 'DEL BIOBÍO ', 'VIII', '4', '54');
INSERT INTO `reg_region` VALUES ('10', 'DE LA ARAUCANÍA', 'IX', '2', '32');
INSERT INTO `reg_region` VALUES ('11', 'DE LOS RÍOS', 'XIV', '2', '12');
INSERT INTO `reg_region` VALUES ('12', 'DE LOS LAGOS', 'X', '4', '30');
INSERT INTO `reg_region` VALUES ('13', 'AISÉN DEL GRAL. CARLOS IBAÑEZ DEL CAMPO ', 'XI', '4', '10');
INSERT INTO `reg_region` VALUES ('14', 'MAGALLANES Y DE LA ANTÁRTICA CHILENA', 'XII', '4', '11');
INSERT INTO `reg_region` VALUES ('15', 'METROPOLITANA DE SANTIAGO', 'RM', '6', '52');

-- ----------------------------
-- Table structure for rol_rol
-- ----------------------------
DROP TABLE IF EXISTS `rol_rol`;
CREATE TABLE `rol_rol` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NOMBRE` varchar(250) NOT NULL DEFAULT '0',
  `DESCRIPCION` varchar(500) DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rol_rol
-- ----------------------------
INSERT INTO `rol_rol` VALUES ('1', 'Super Administrador', 'Rol encargado de administrar todo el sistema con permisos especiales', '0');
INSERT INTO `rol_rol` VALUES ('2', 'Administrador Centro Educacional', 'Administra los usuarios del establecimiento', '0');
INSERT INTO `rol_rol` VALUES ('3', 'Presidente CPAS', 'Gestiona actividades del Centro de Padres', '0');
INSERT INTO `rol_rol` VALUES ('4', 'Tesorero CPAS', 'Solo lectura', '0');
INSERT INTO `rol_rol` VALUES ('5', 'Secretario CPAS', 'Secretario', '0');
INSERT INTO `rol_rol` VALUES ('6', 'Vicepresidente CPAS', 'Tesorero', '0');
INSERT INTO `rol_rol` VALUES ('7', 'Director CPAS', 'DIRECTOR', '0');
INSERT INTO `rol_rol` VALUES ('8', 'Director Establecimiento', 'Director del Establecimiento', '0');
INSERT INTO `rol_rol` VALUES ('9', 'Apoderado', 'Apoderado', '0');
INSERT INTO `rol_rol` VALUES ('10', 'Delegado de Cultura y Deportes', 'Delegado Cultural y Deportivo del Establecimiento', '0');

-- ----------------------------
-- Table structure for rpt_responsable_tricel
-- ----------------------------
DROP TABLE IF EXISTS `rpt_responsable_tricel`;
CREATE TABLE `rpt_responsable_tricel` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TRI_ID` int(11) NOT NULL DEFAULT '0',
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rpt_responsable_tricel
-- ----------------------------
INSERT INTO `rpt_responsable_tricel` VALUES ('1', '1', '6', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('2', '1', '7', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('5', '2', '1', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('6', '2', '7', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('7', '3', '1', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('8', '3', '6', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('9', '4', '1', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('10', '4', '6', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('11', '5', '1', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('12', '5', '6', '0');
INSERT INTO `rpt_responsable_tricel` VALUES ('14', '6', '1', '0');

-- ----------------------------
-- Table structure for tri_tricel
-- ----------------------------
DROP TABLE IF EXISTS `tri_tricel`;
CREATE TABLE `tri_tricel` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FECHA_CREACION` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `USU_ID_CREADOR` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `NOMBRE` varchar(500) NOT NULL,
  `OBJETIVO` varchar(500) NOT NULL,
  `FECHA_INICIO` datetime NOT NULL,
  `FECHA_TERMINO` datetime NOT NULL,
  `ES_VIGENTE` int(11) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tri_tricel
-- ----------------------------
INSERT INTO `tri_tricel` VALUES ('1', '2016-02-17 17:26:59', '6', '4', 'Tricel 1', 'objetivo 1', '2016-02-17 00:00:00', '2016-02-29 00:00:00', '1', '0');
INSERT INTO `tri_tricel` VALUES ('2', '2016-03-08 07:33:47', '1', '3', 'prueba', 'Objetivo prueba', '2016-03-06 00:00:00', '2016-03-30 00:00:00', '1', '0');
INSERT INTO `tri_tricel` VALUES ('3', '2016-03-11 08:11:54', '1', '3', 'Triple coro', 'Obj', '2016-03-08 00:00:00', '2016-03-29 00:00:00', '1', '0');
INSERT INTO `tri_tricel` VALUES ('4', '2016-03-11 08:52:55', '1', '3', 'pppp', 'Obj', '2016-03-08 00:00:00', '2016-03-30 00:00:00', '1', '0');
INSERT INTO `tri_tricel` VALUES ('5', '2016-03-12 13:35:04', '1', '3', 'coro', 'Objetivo', '2016-03-11 00:00:00', '2016-03-29 00:00:00', '1', '0');
INSERT INTO `tri_tricel` VALUES ('6', '2016-12-03 16:21:45', '1', '3', 'Votación PRIMERA LISTA', 'votar por la lista', '2017-01-01 00:00:00', '2017-01-14 00:00:00', '1', '0');

-- ----------------------------
-- Table structure for usl_usuario_lista
-- ----------------------------
DROP TABLE IF EXISTS `usl_usuario_lista`;
CREATE TABLE `usl_usuario_lista` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USU_ID` int(11) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  `LTR_ID` int(11) NOT NULL DEFAULT '0',
  `ROL` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of usl_usuario_lista
-- ----------------------------
INSERT INTO `usl_usuario_lista` VALUES ('1', '8', '0', '1', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('2', '9', '0', '1', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('3', '10', '0', '1', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('4', '11', '0', '1', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('5', '8', '0', '2', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('6', '9', '0', '2', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('7', '10', '0', '2', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('8', '12', '0', '2', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('9', '4', '0', '3', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('10', '6', '0', '3', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('11', '8', '0', '3', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('12', '9', '0', '3', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('13', '10', '0', '3', 'Otro1');
INSERT INTO `usl_usuario_lista` VALUES ('14', '4', '0', '4', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('15', '7', '0', '4', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('16', '10', '0', '4', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('17', '8', '0', '4', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('18', '4', '0', '5', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('19', '7', '0', '5', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('20', '8', '0', '5', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('21', '9', '0', '5', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('22', '4', '0', '6', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('23', '7', '0', '6', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('24', '8', '0', '6', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('25', '9', '0', '6', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('26', '4', '0', '7', 'Presidente');
INSERT INTO `usl_usuario_lista` VALUES ('27', '16', '0', '7', 'Vicepresidente');
INSERT INTO `usl_usuario_lista` VALUES ('28', '15', '0', '7', 'Secretario');
INSERT INTO `usl_usuario_lista` VALUES ('29', '23', '0', '7', 'Tesorero');
INSERT INTO `usl_usuario_lista` VALUES ('30', '14', '0', '7', 'Otro1');
INSERT INTO `usl_usuario_lista` VALUES ('31', '19', '0', '7', 'Otro2');

-- ----------------------------
-- Table structure for vi_vinculos_institucion
-- ----------------------------
DROP TABLE IF EXISTS `vi_vinculos_institucion`;
CREATE TABLE `vi_vinculos_institucion` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `IMAGEN_VINCULO_1` varchar(200) NOT NULL DEFAULT '0',
  `URL_VINCULO_1` varchar(200) NOT NULL DEFAULT '0',
  `TEXTO_VINCULO_1` varchar(200) NOT NULL DEFAULT '0',
  `VISIBLE_VINCULO_1` int(11) NOT NULL DEFAULT '0',
  `VISIBLE_VINCULO_2` int(11) NOT NULL DEFAULT '0',
  `IMAGEN_VINCULO_2` varchar(200) NOT NULL DEFAULT '0',
  `URL_VINCULO_2` varchar(200) NOT NULL DEFAULT '0',
  `TEXTO_VINCULO_2` varchar(200) NOT NULL DEFAULT '0',
  `VISIBLE_VINCULO_3` int(11) NOT NULL DEFAULT '0',
  `IMAGEN_VINCULO_3` varchar(200) NOT NULL DEFAULT '0',
  `URL_VINCULO_3` varchar(200) NOT NULL DEFAULT '0',
  `TEXTO_VINCULO_3` varchar(200) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vi_vinculos_institucion
-- ----------------------------
INSERT INTO `vi_vinculos_institucion` VALUES ('1', '3', '~/img/facebook.png', 'https://www.facebook.com/cpas.cl/', 'http://facebook.com/', '1', '1', '~/img/twitter.png', 'https://twitter.com/CPAScl', 'http://twitter.com', '1', '~/img/email.png', 'mailto:contacto@cpas.cl', 'contacto@cpas.cl');

-- ----------------------------
-- Table structure for vot_votaciones
-- ----------------------------
DROP TABLE IF EXISTS `vot_votaciones`;
CREATE TABLE `vot_votaciones` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PRO_ID` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `USU_ID_VOTADOR` int(11) NOT NULL DEFAULT '0',
  `FECHA_VOTACION` datetime NOT NULL,
  `VALOR` int(11) NOT NULL DEFAULT '0',
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vot_votaciones
-- ----------------------------
INSERT INTO `vot_votaciones` VALUES ('1', '2', '4', '6', '2015-11-10 18:40:09', '1', '0');
INSERT INTO `vot_votaciones` VALUES ('2', '3', '4', '6', '2015-11-10 18:41:06', '1', '0');
INSERT INTO `vot_votaciones` VALUES ('3', '2', '4', '7', '2015-11-10 18:58:06', '0', '0');
INSERT INTO `vot_votaciones` VALUES ('4', '6', '4', '10', '2016-04-14 19:20:24', '1', '0');
INSERT INTO `vot_votaciones` VALUES ('5', '6', '4', '11', '2016-04-14 19:21:38', '0', '0');
INSERT INTO `vot_votaciones` VALUES ('6', '6', '4', '8', '2016-04-14 19:22:26', '1', '0');
INSERT INTO `vot_votaciones` VALUES ('7', '6', '4', '9', '2016-04-14 19:24:39', '1', '0');

-- ----------------------------
-- Table structure for vtr_vot_tricel
-- ----------------------------
DROP TABLE IF EXISTS `vtr_vot_tricel`;
CREATE TABLE `vtr_vot_tricel` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LTR_ID` int(11) NOT NULL DEFAULT '0',
  `TRI_ID` int(11) NOT NULL DEFAULT '0',
  `INST_ID` int(11) NOT NULL DEFAULT '0',
  `USU_ID_VOTADOR` int(11) NOT NULL DEFAULT '0',
  `FECHA_VOTACION` datetime NOT NULL,
  `ELIMINADO` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vtr_vot_tricel
-- ----------------------------
INSERT INTO `vtr_vot_tricel` VALUES ('1', '1', '1', '4', '7', '2016-02-17 17:31:51', '0');
INSERT INTO `vtr_vot_tricel` VALUES ('2', '2', '1', '4', '6', '2016-02-17 17:32:40', '0');
INSERT INTO `vtr_vot_tricel` VALUES ('3', '3', '2', '3', '1', '2016-03-08 07:39:47', '0');
INSERT INTO `vtr_vot_tricel` VALUES ('4', '4', '3', '4', '6', '2016-03-11 09:11:06', '0');
INSERT INTO `vtr_vot_tricel` VALUES ('5', '7', '6', '3', '1', '2016-12-03 16:28:37', '0');
SET FOREIGN_KEY_CHECKS=1;

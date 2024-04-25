-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 25-04-2024 a las 04:05:23
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliariaortiz_pascuali`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) DEFAULT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `valor` double NOT NULL,
  `multa` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInquilino`, `idInmueble`, `fecha_inicio`, `fecha_fin`, `valor`, `multa`, `estado`) VALUES
(19, 2, NULL, '2024-04-11', '2024-07-11', 200000, 0, 0),
(20, 5, NULL, '2024-04-24', '2024-11-24', 250000, 0, 0),
(21, 2, 24, '2024-04-24', '2024-10-24', 200000, 0, 0),
(22, 5, 25, '2024-04-24', '2024-11-24', 250000, 0, 0),
(23, 7, 26, '2024-04-24', '2024-11-24', 150000, 0, 0),
(24, 8, 27, '2024-04-24', '2025-02-24', 340000, 0, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `tipo` varchar(100) NOT NULL,
  `uso` varchar(100) NOT NULL,
  `cant_ambientes` int(11) NOT NULL,
  `precio` double NOT NULL,
  `coordenadas` varchar(100) NOT NULL,
  `fechas_disponibles` varchar(100) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `idPropietario`, `direccion`, `tipo`, `uso`, `cant_ambientes`, `precio`, `coordenadas`, `fechas_disponibles`, `estado`) VALUES
(24, 11, 'Juana Koslay', '', '', 0, 200000, '2324.3423.4234', '', 0),
(25, 12, 'La florida', '', '', 0, 250000, '3434.45.65654.65', '', 0),
(26, 13, 'El volcan', '', '', 0, 150000, '2312.2312.213123', '', 0),
(27, 14, 'San Luis', '', '', 0, 340000, '12.323.434.555', '', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `dni` varchar(100) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `nombre`, `apellido`, `dni`, `estado`) VALUES
(2, 'Dario', 'Arabes', '23453657', 0),
(5, 'Wilson', 'Gonzalo', '4234234234', 0),
(7, 'Maria', 'Sumer', '34567890', 0),
(8, 'Miryam', 'Clementina', '12345678', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `monto` double NOT NULL,
  `fecha_pago` date NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`idPago`, `idContrato`, `monto`, `fecha_pago`, `estado`) VALUES
(38, 19, 200000, '2024-04-11', 0),
(39, 19, 200000, '2024-05-11', 0),
(40, 19, 200000, '2024-06-11', 0),
(41, 20, 250000, '2024-04-24', 0),
(42, 20, 250000, '2024-05-24', 0),
(43, 20, 250000, '2024-06-24', 0),
(44, 20, 250000, '2024-07-24', 0),
(45, 20, 250000, '2024-08-24', 0),
(46, 20, 250000, '2024-09-24', 0),
(47, 20, 250000, '2024-10-24', 0),
(48, 21, 200000, '2024-04-24', 1),
(49, 21, 200000, '2024-05-24', 1),
(50, 21, 200000, '2024-06-24', 1),
(51, 21, 200000, '2024-07-24', 1),
(52, 21, 200000, '2024-08-24', 0),
(53, 21, 200000, '2024-09-24', 0),
(54, 22, 250000, '2024-04-24', 1),
(55, 22, 250000, '2024-05-24', 0),
(56, 22, 250000, '2024-06-24', 0),
(57, 22, 250000, '2024-07-24', 0),
(58, 22, 250000, '2024-08-24', 0),
(59, 22, 250000, '2024-09-24', 0),
(60, 22, 250000, '2024-10-24', 0),
(61, 23, 150000, '2024-04-24', 0),
(62, 23, 150000, '2024-05-24', 0),
(63, 23, 150000, '2024-06-24', 0),
(64, 23, 150000, '2024-07-24', 0),
(65, 23, 150000, '2024-08-24', 0),
(66, 23, 150000, '2024-09-24', 0),
(67, 23, 150000, '2024-10-24', 0),
(68, 24, 340000, '2024-04-24', 0),
(69, 24, 340000, '2024-05-24', 0),
(70, 24, 340000, '2024-06-24', 0),
(71, 24, 340000, '2024-07-24', 0),
(72, 24, 340000, '2024-08-24', 0),
(73, 24, 340000, '2024-09-24', 0),
(74, 24, 340000, '2024-10-24', 0),
(75, 24, 340000, '2024-11-24', 0),
(76, 24, 340000, '2024-12-24', 0),
(77, 24, 340000, '2025-01-24', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `nombre`, `apellido`, `dni`, `estado`) VALUES
(11, 'Jose', 'Delmonte', '23456345', 0),
(12, 'Oscar', 'Pereyra', '32456546', 0),
(13, 'Miguel', 'Espindola', '12345678', 0),
(14, 'Viviana', 'Canosa', '23456789', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `clave` varchar(1000) NOT NULL,
  `rol` int(11) NOT NULL,
  `avatar` varchar(1000) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `apellido`, `nombre`, `email`, `clave`, `rol`, `avatar`, `estado`) VALUES
(29, 'Perez', 'Luciana', 'luciana@gmail.com', 'V3eS9jJaOwO8EzrO6aD1B9sGI3TGGd4jnG0hTIn22R0=', 1, '/Uploads/usuario.jpg', 0),
(30, 'Pascuali', 'Dario', 'dario@gmail.com', 'V3eS9jJaOwO8EzrO6aD1B9sGI3TGGd4jnG0hTIn22R0=', 2, '/Uploads\\avatar_30.webp', 0),
(31, 'Pascuali', 'Wilson', 'wilson@gmail.com', 'V3eS9jJaOwO8EzrO6aD1B9sGI3TGGd4jnG0hTIn22R0=', 1, '/Uploads\\avatar_31.jpg', 0),
(32, 'Escudero', 'Marito', 'marito@gmail.com', 'V3eS9jJaOwO8EzrO6aD1B9sGI3TGGd4jnG0hTIn22R0=', 2, '/Uploads\\avatar_32.jpg', 0),
(33, 'Contreras', 'Magali', 'maga@gmail.com', 'V3eS9jJaOwO8EzrO6aD1B9sGI3TGGd4jnG0hTIn22R0=', 1, '/Uploads\\avatar_33.jpg', 0);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `idInquilino` (`idInquilino`),
  ADD KEY `idInmueble` (`idInmueble`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `idPropietario` (`idPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `idContrato` (`idContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=78;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `ContratoInmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`) ON DELETE SET NULL,
  ADD CONSTRAINT `ContratoInquilino` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

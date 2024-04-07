-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 07-04-2024 a las 02:14:17
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
  `multa` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInquilino`, `idInmueble`, `fecha_inicio`, `fecha_fin`, `multa`, `estado`) VALUES
(16, 6, 21, '2024-04-06', '2024-11-06', 0, 0),
(17, 5, 22, '2024-04-06', '2025-01-06', 0, 0),
(18, 2, 23, '2024-04-06', '2025-07-06', 0, 0);

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
(21, 3, 'Juana Koslay', '', '', 0, 200000, '3434.45.65654.6', '', 0),
(22, 4, 'Trapiche', '', '', 0, 250000, '34234.324.32423.34', '', 0),
(23, 5, 'San Luis', '', '', 0, 150000, '234.3434.354.46', '', 0);

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
(6, 'Joaquin', 'Fredi', '2312312312', 0);

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
(7, 16, 200000, '2024-04-06', 0),
(8, 16, 200000, '2024-05-06', 0),
(9, 16, 200000, '2024-06-06', 0),
(10, 16, 200000, '2024-07-06', 0),
(11, 16, 200000, '2024-08-06', 0),
(12, 16, 200000, '2024-09-06', 0),
(13, 16, 200000, '2024-10-06', 0),
(14, 17, 250000, '2024-04-06', 0),
(15, 17, 250000, '2024-05-06', 0),
(16, 17, 250000, '2024-06-06', 0),
(17, 17, 250000, '2024-07-06', 0),
(18, 17, 250000, '2024-08-06', 0),
(19, 17, 250000, '2024-09-06', 0),
(20, 17, 250000, '2024-10-06', 0),
(21, 17, 250000, '2024-11-06', 0),
(22, 17, 250000, '2024-12-06', 0),
(23, 18, 150000, '2024-04-06', 0),
(24, 18, 150000, '2024-05-06', 0),
(25, 18, 150000, '2024-06-06', 0),
(26, 18, 150000, '2024-07-06', 0),
(27, 18, 150000, '2024-08-06', 0),
(28, 18, 150000, '2024-09-06', 0),
(29, 18, 150000, '2024-10-06', 0),
(30, 18, 150000, '2024-11-06', 0),
(31, 18, 150000, '2024-12-06', 0),
(32, 18, 150000, '2025-01-06', 0),
(33, 18, 150000, '2025-02-06', 0),
(34, 18, 150000, '2025-03-06', 0),
(35, 18, 150000, '2025-04-06', 0),
(36, 18, 150000, '2025-05-06', 0),
(37, 18, 150000, '2025-06-06', 0);

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
(3, 'Cardona', 'Pedrozo', '23423423', 0),
(4, 'Walter', 'Gomez', '34567567', 0),
(5, 'Carlos', 'Frank', '34234234', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL,
  `dni` int(11) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `contrasena` varchar(100) NOT NULL,
  `nick` varchar(100) NOT NULL,
  `rol` varchar(100) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

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
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

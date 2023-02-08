-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 08 Feb 2023 pada 15.28
-- Versi server: 10.4.20-MariaDB
-- Versi PHP: 7.3.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `project`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `kendaraandinas`
--

CREATE TABLE `kendaraandinas` (
  `nama` varchar(25) NOT NULL,
  `nrp` varchar(25) NOT NULL,
  `satker` varchar(20) NOT NULL,
  `nomer_al` varchar(20) NOT NULL,
  `nomer_pusat` varchar(50) NOT NULL,
  `merktype_kendaraan` varchar(10) NOT NULL,
  `tahun_pembuatan` varchar(10) NOT NULL,
  `nomer_mesin` varchar(30) NOT NULL,
  `kondisi` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `kendaraandinas`
--

INSERT INTO `kendaraandinas` (`nama`, `nrp`, `satker`, `nomer_al`, `nomer_pusat`, `merktype_kendaraan`, `tahun_pembuatan`, `nomer_mesin`, `kondisi`) VALUES
('JUJU', '2018230107', 'Diskes l', 'EW210967', 'VB231287', 'BMW', '2018', 'TY-201834', 'BAIK'),
('BATARA', '2019230107', 'Irkolinlamil', 'RE342109', 'TR432678', 'HONDA', '2019', 'RE-210986', 'BAIK'),
('ZAENAL', '2109219835', 'Asrena Pangkolinlami', 'WE092854', 'RE210982', 'TOYOTA', '2022', 'LO-983209', 'BAIK');

-- --------------------------------------------------------

--
-- Struktur dari tabel `service`
--

CREATE TABLE `service` (
  `nrp` varchar(20) NOT NULL,
  `jenis_service` varchar(20) NOT NULL,
  `sparepart` varchar(20) NOT NULL,
  `kode_barang` varchar(20) NOT NULL,
  `jumlah` varchar(20) NOT NULL,
  `keluhan` varchar(20) NOT NULL,
  `tanggal_masuk` varchar(20) NOT NULL,
  `tanggal_kembali` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `service`
--

INSERT INTO `service` (`nrp`, `jenis_service`, `sparepart`, `kode_barang`, `jumlah`, `keluhan`, `tanggal_masuk`, `tanggal_kembali`) VALUES
('2018230107', 'Ringan', 'oli', 'brg02', '1', 'ban', '02/02/2023 06:53:07', '10/04/2023'),
('2019230107', 'Ringan', 'lampu', 'brg03', '2', 'tidak', '02/02/2023 06:47:16', '08/03/2023');

--
-- Trigger `service`
--
DELIMITER $$
CREATE TRIGGER `stok` AFTER INSERT ON `service` FOR EACH ROW BEGIN

   UPDATE stok  SET qty = qty - NEW.jumlah

   WHERE kode_barang = NEW.kode_barang;

END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Struktur dari tabel `stok`
--

CREATE TABLE `stok` (
  `kode_barang` varchar(20) NOT NULL,
  `sparepart` varchar(25) NOT NULL,
  `qty` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `stok`
--

INSERT INTO `stok` (`kode_barang`, `sparepart`, `qty`) VALUES
('brg01', 'rem', 10),
('brg02', 'oli', 10),
('brg03', 'lampu', 9);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_anggota`
--

CREATE TABLE `tbl_anggota` (
  `nrp` varchar(20) NOT NULL,
  `nama` varchar(20) NOT NULL,
  `satker` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_anggota`
--

INSERT INTO `tbl_anggota` (`nrp`, `nama`, `satker`) VALUES
('2018230107', 'JUJU', 'Kadiskomlek '),
('2019230107', 'BATARA', 'Dandenmako');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_status`
--

CREATE TABLE `tbl_status` (
  `nrp` varchar(20) NOT NULL,
  `nomer_al` varchar(20) NOT NULL,
  `merk` varchar(20) NOT NULL,
  `tahun` varchar(15) NOT NULL,
  `kondisi` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_status`
--

INSERT INTO `tbl_status` (`nrp`, `nomer_al`, `merk`, `tahun`, `kondisi`) VALUES
('2018230107', 'EW210967', 'BMW', '2018', 'BAIK'),
('2019230107', 'RE342109', 'HONDA', '2019', 'BAIK');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_user`
--

CREATE TABLE `tbl_user` (
  `nrp` varchar(20) NOT NULL,
  `admin_master` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_user`
--

INSERT INTO `tbl_user` (`nrp`, `admin_master`, `password`, `status`) VALUES
('2018230107', 'user', '123', 'SLOG'),
('2019230107', 'admin', '123', 'ADMIN');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `kendaraandinas`
--
ALTER TABLE `kendaraandinas`
  ADD PRIMARY KEY (`nrp`);

--
-- Indeks untuk tabel `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`nrp`),
  ADD KEY `kode_barang` (`kode_barang`);

--
-- Indeks untuk tabel `stok`
--
ALTER TABLE `stok`
  ADD PRIMARY KEY (`kode_barang`);

--
-- Indeks untuk tabel `tbl_anggota`
--
ALTER TABLE `tbl_anggota`
  ADD PRIMARY KEY (`nrp`);

--
-- Indeks untuk tabel `tbl_status`
--
ALTER TABLE `tbl_status`
  ADD PRIMARY KEY (`nrp`);

--
-- Indeks untuk tabel `tbl_user`
--
ALTER TABLE `tbl_user`
  ADD PRIMARY KEY (`nrp`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `service`
--
ALTER TABLE `service`
  ADD CONSTRAINT `service_ibfk_1` FOREIGN KEY (`nrp`) REFERENCES `kendaraandinas` (`nrp`),
  ADD CONSTRAINT `service_ibfk_2` FOREIGN KEY (`kode_barang`) REFERENCES `stok` (`kode_barang`);

--
-- Ketidakleluasaan untuk tabel `tbl_anggota`
--
ALTER TABLE `tbl_anggota`
  ADD CONSTRAINT `tbl_anggota_ibfk_1` FOREIGN KEY (`nrp`) REFERENCES `tbl_status` (`nrp`),
  ADD CONSTRAINT `tbl_anggota_ibfk_2` FOREIGN KEY (`nrp`) REFERENCES `kendaraandinas` (`nrp`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

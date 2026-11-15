CREATE DATABASE IF NOT EXISTS edutrack_db;
USE edutrack_db;

CREATE TABLE IF NOT EXISTS users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS students (
    student_id VARCHAR(20) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    gender VARCHAR(20) NOT NULL,
    year_level VARCHAR(20) NOT NULL,
    program VARCHAR(50) NOT NULL,
    contact_number VARCHAR(20) NOT NULL,
    photo_path LONGBLOB DEFAULT NULL
);


UPDATE students
SET photo_path = NULL
WHERE photo_path LIKE '%:\\%' OR photo_path LIKE '%/%';

ALTER TABLE students
MODIFY COLUMN photo_path LONGBLOB DEFAULT NULL;


-- Default Account (Username: admin | Password: admin123)
INSERT IGNORE INTO users (username, password) VALUES ('admin', 'admin123');

-- Sample Student Directory Records
INSERT IGNORE INTO students (student_id, name, gender, year_level, program, contact_number, photo_path) VALUES
('2026-0001', 'Juan Dela Cruz', 'Male', '2nd Year', 'BSIT', '09171234567', NULL),
('2026-0002', 'Maria Clara', 'Female', '1st Year', 'BSCS', '09189876543', NULL),
('2026-0003', 'Jose Rizal', 'Male', '4th Year', 'BSIT', '09205550199', NULL),
('2026-0004', 'Gabriela Silang', 'Female', '3rd Year', 'BSHM', '09982345678', NULL),
('2026-0005', 'Andres Bonifacio', 'Male', '2nd Year', 'BSIS', '09158765432', NULL),
('2026-0006', 'Melchora Aquino', 'Female', '4th Year', 'BSED', '09173334455', NULL),
('2026-0007', 'Emilio Aguinaldo', 'Male', '1st Year', 'BSIT', '09192223344', NULL),
('2026-0008', 'Apolinario Mabini', 'Male', '3rd Year', 'BSCS', '09228887766', NULL),
('2026-0009', 'Teresa Magbanua', 'Female', '2nd Year', 'BSBA', '09164445566', NULL),
('2026-0010', 'Antonio Luna', 'Male', '1st Year', 'BSIT', '09181112233', NULL),
('2026-0011', 'Gregoria De Jesus', 'Female', '3rd Year', 'BSIS', '09216667788', NULL),
('2026-0012', 'Marcelo Del Pilar', 'Male', '4th Year', 'BSCS', '09179998877', NULL);

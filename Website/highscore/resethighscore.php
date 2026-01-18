<?php
require 'config.php';

$password = $_GET['password'] ?? null;

if (empty($password)) {
    echo "Not good, password is missing.";
    exit;
}

if ($password != password) {
    echo "Not good, wrong password.";
    exit;
}

$host = host;
$db = db;
$user = user;
$pass = pass;
$charset = charset;
$conn = new mysqli($host, $user, $pass, $charset);
$sql = "TRUNCATE TABLE highscore;
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (20, '1980-01-01', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (19, '1980-01-02', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (18, '1980-01-03', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (17, '1980-01-04', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (16, '1980-01-05', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (15, '1980-01-06', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (14, '1980-01-07', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (13, '1980-01-08', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (12, '1980-01-09', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (11, '1980-01-10', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (10, '1980-01-11', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (9, '1980-01-12', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (8, '1980-01-13', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (7, '1980-01-14', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (6, '1980-01-15', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (5, '1980-01-16', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (4, '1980-01-17', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (3, '1980-01-18', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (2, '1980-01-19', 'aaa');
INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES (1, '1980-01-20', 'aaa');
";

if ($conn->query($sql) === TRUE) {
    echo "Kommandot kördes utan fel.";
}
else {
    echo "Fel vid körning: " . $conn->error;
}
$conn->close();
?>
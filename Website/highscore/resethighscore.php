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

try {
    $conn = mysqli_connect($host, $user, $pass, $db);
}
catch (Exception $e) {
    echo "Connection error: " . $e->getMessage();
    exit;
}

try {
    if ($conn->query("TRUNCATE TABLE highscore;") === TRUE) {
        echo "Truncate ok.";
    }
    else {
        echo "Truncate failed: " . $conn->error;
        exit;
    }
}
catch (Exception $e) {
    echo "Truncate exception: " . $e->getMessage();
    exit;
}

$sql = "INSERT INTO highscore (score, playertimestamp, playerusername)
VALUES
(20, '1980-01-01', 'aaa'),
(19, '1980-01-02', 'aaa'),
(18, '1980-01-03', 'aaa'),
(17, '1980-01-04', 'aaa'),
(16, '1980-01-05', 'aaa'),
(15, '1980-01-06', 'aaa'),
(14, '1980-01-07', 'aaa'),
(13, '1980-01-08', 'aaa'),
(12, '1980-01-09', 'aaa'),
(11, '1980-01-10', 'aaa'),
(10, '1980-01-11', 'aaa'),
(9, '1980-01-12', 'aaa'),
(8, '1980-01-13', 'aaa'),
(7, '1980-01-14', 'aaa'),
(6, '1980-01-15', 'aaa'),
(5, '1980-01-16', 'aaa'),
(4, '1980-01-17', 'aaa'),
(3, '1980-01-18', 'aaa'),
(2, '1980-01-19', 'aaa'),
(1, '1980-01-20', 'aaa');";

try {
    if ($conn->query($sql) === TRUE) {
        echo "Insert ok.";
    }
    else {
        echo "Insert failed: " . $conn->error;
        exit;
    }
}
catch (Exception $e) {
    echo "Insert exception: " . $e->getMessage();
    exit;
}

try {
    $conn->close();
}
catch (Exception $e) {
    echo "Close failed: " . $e->getMessage();
}
?>
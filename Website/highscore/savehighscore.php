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
$score = $_GET['score'] ?? null;
$player = $_GET['user'] ?? null;
$date = date("Y-m-d");

if (empty($score)) {
    echo "Not good, missing score.";
    exit;
}

if (!is_numeric($score)) {
    echo "Not good, score is bad.";
    exit;
}

if (empty($user)) {
    echo "Not good, missing user.";
    exit;
}

if (strlen($player) != 3) {
    echo "Not good, user is bad.";
    exit;
}

try {
    $conn = mysqli_connect($host, $user, $pass, $db);
}
catch (Exception $e) {
    echo "Connection error: " . $e->getMessage();
    exit;
}

try {
    $sql = "INSERT INTO highscore (playertimestamp, playerusername, score) VALUES (?, ?, ?);";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("ssi",$date,$player, $score);
    $stmt->execute();
    echo "Good! Score stored.";
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
<?php
require 'config.php';
echo password;

$password = $_GET['password'] ?? null;

if (empty($password)) {
    exit;
}

echo 'ok so far';

?>
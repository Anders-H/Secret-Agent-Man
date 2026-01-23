<?php
require 'config.php';
$format = $_GET['format'] ?? null;
$host = host;
$db = db;
$user = user;
$pass = pass;
$charset = charset;
$query = "SELECT score, playertimestamp, playerusername FROM highscore ORDER BY score DESC, playertimestamp DESC LIMIT 100;";

try {
    $conn = mysqli_connect($host, $user, $pass, $db);
}
catch (Exception $e) {
    echo "Connection error: " . $e->getMessage();
    exit;
}

try {
    $result = $conn->query($query);
}
catch (Exception $e) {
    echo "Query error: " . $e->getMessage();
    exit;
}

switch ($format) {
    case "json":
        echo "[";
        $i = 0;

        try {

            while ($row = $result->fetch_row()) {
                $i++;

                if ($i > 1) {
                    echo ",";
                }

                printf ('{"score": "%s",
"time": "%s",
"user": "%s"}', $row[0], $row[1], $row[2]);
            }
        }
        catch (Exception $e) {
            echo "Read error (json): " . $e->getMessage();
            exit;
        }
        echo "]";
        break;
    case "xml":
        echo '<?xml version="1.0" encoding="UTF-8" standalone="yes" ?>';
        echo "\n<table>";

        try {

            while ($row = $result->fetch_row()) {
                printf ('<score>%s</score><time>%s</time><user>%s</user>', $row[0], $row[1], $row[2]);
            }
        }
        catch (Exception $e) {
            echo "Read error (json): " . $e->getMessage();
            exit;
        }
        echo "</table>";
        break;
    case "html":
        echo "<table>";
        echo '<tr><td style="font-weight: bold; text-align: center">POS</td><td style="font-weight: bold; text-align: center">SCORE</td><td style="font-weight: bold; text-align: center">DATE</td><td style="font-weight: bold; text-align: center">USR</td></tr>';
        try {
           $position = 0;

            while ($row = $result->fetch_row()) {
                $position++;
                echo "<tr>";
                echo '<td style="text-align: center">' . $position . '</td>';
                echo '<td style="text-align: center">' . $row[0] . '</td>';
                echo '<td style="text-align: center">' . $row[1] . '</td>';
                echo '<td style="text-align: center">' . strtoupper($row[2]) . '</td>';
                echo "<tr>";
            }
        }
        catch (Exception $e) {
            echo "Read error (json): " . $e->getMessage();
            exit;
        }
        echo "</table>";
        break;
    case "website":
        echo '
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta charset="utf-8" />
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />
    <link rel="apple-touch-icon" sizes="180x180" href="apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="favicon-16x16.png">
    <link rel="manifest" href="site.webmanifest">
    <title>Secret Agent Man - The Highscore List</title>
    <style>
        html, body {
            margin: 0;
            padding: 0;
            height: 100%;
            width: 100%;
            background-color: #0b1f0b;
            color: #00dd00;
            font-family: "Courier New", Courier, monospace;
            font-size: 16px;
            cursor: default;
        }

        p {
            margin: 0;
            padding: 12px 0 0 0;
            cursor: default;
        }

        a {
            text-decoration: none;
            color: #66ff66;
            cursor: pointer;
        }

        a:hover {
            color: #55ff88;
            text-shadow: 0 0 3px rgba(255, 255, 255, 0.5);
            cursor: pointer;
        }

        .container {
            width: 75%;
            min-width: 300px;
            max-width: 2000px;
            margin: 0 auto;
            background-color: #0b1f0b;
            border-left: #000000 1px solid;
            border-right: #000000 1px solid;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.6);
            padding: 10px 20px 30px 20px;
        }

        .imageContainer {
            margin: 0 auto;
            text-align: center;
        }

        img {
            margin: 0 auto;
            max-width: 100%;
        }

        .imgShadow {
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.6);
        }
    </style>
    </head>
    <body>';
        echo '<table style="margin: 0 auto 0 auto;">';
        echo '<tr><td style="font-weight: bold; text-align: center">POS</td><td style="padding-left: 10px; font-weight: bold; text-align: center">SCORE</td><td style="padding-left: 10px; font-weight: bold; text-align: center">DATE</td><td style="padding-left: 10px; font-weight: bold; text-align: center">USER</td></tr>';
        try {
            $position = 0;

            while ($row = $result->fetch_row()) {
                $position++;
                echo "<tr>";
                echo '<td style="text-align: center">' . $position . '</td>';
                echo '<td style="padding-left: 10px; text-align: center">' . $row[0] . '</td>';
                echo '<td style="padding-left: 10px; text-align: center">' . $row[1] . '</td>';
                echo '<td style="padding-left: 10px; text-align: center">' . strtoupper($row[2]) . '</td>';
                echo "<tr>";
            }
        }
        catch (Exception $e) {
            echo "Read error (json): " . $e->getMessage();
            exit;
        }
        echo "</table></body></html>";
        break;
    default:
        try {
            while ($row = $result->fetch_row()) {
            printf ("%s|%s|%s;", $row[0], $row[1], $row[2]);
            }
        }
        catch (Exception $e) {
            echo "Read error (default format): " . $e->getMessage();
            exit;
        }
        break;
}

try {
    $conn->close();
}
catch (Exception $e) {
    echo "Close failed: " . $e->getMessage();
}
?>
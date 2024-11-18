<?php

$servername = "127.0.0.1";
$username = "root";
$password = "";
$db_name = "unitybackend";
$port = 3307;

// Variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $db_name, $port);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password FROM users WHERE username = '" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Output data of each row
    while ($row = $result->fetch_assoc()) {
        if ($row["password"] == $loginPass) {
            echo "Loggin Success!";
            exit; // Exit after success
        } else {
            echo "Wrong Credentials";
            exit; // Exit after wrong credentials
        }
    }
} else {
    echo "Username does not exist";
    exit; // Exit if username doesn't exist
}

$conn->close();
?>

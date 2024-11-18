<?php

  $servername = "127.0.0.1";
  $username = "root";
  $password = "";
  $confirmpassword = "";
  $db_name = "unitybackend";
  $port = 3307; 

  //variables submited by user
  $loginUser = $_POST["loginUser"];
  $loginPass = $_POST["loginPass"];
  $confirmPass = $_POST["confirmPass"];


  // Create connection
  $conn = new mysqli($servername, $username, $password , $db_name, $port);

  // Check connection
  if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
  }
  echo "Connected successfully . <br>" ;

  $sql = "SELECT username FROM users WHERE username = '" . $loginUser . "'";

  $result = $conn->query($sql);


  if ($result->num_rows > 0) {
    //Tell user that that name is already taken
    echo "Username is already taken.";
    }
  
   else {
        echo "Creating user....";
        //insert the user and password into the database
        $sql2 = "INSERT INTO users (username, password,confirmpassword, level, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "', '" . $confirmPass . "', 1, 0)";
        if ($conn->query($sql2) === TRUE) {
          
            echo "New record created successfully";
          } else {
            echo "Error: " . $sql2 . "<br>" . $conn->error;
          }
    }

  $conn->close();
?>

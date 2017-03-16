<?php

$database_server = '';
$database_user = '';
$database_pass = '';

$dbc = mysql_connect($database_server, $databse_user, $database_pass);
if (!$dbc) {
    die('Connection error. Please try again.');
}
$db_selected = mysql_select_db($database, $dbc);
if (!$database) {
    die ('Selection error. Please try again.');
}


$success = 0;


if (isset($_POST['submit'])) {
	
	//This should eventually be checked with a regular expression instead of using trim
	$username = mysql_real_escape_string(trim(htmlspecialchars($_POST['username'])));
	$password = mysql_real_escape_string(trim(htmlspecialcahrs($_POST['password'])));

	//checking length
	if (strlen($username) < 13  && strlen($password) < 13) {
		
		//checking if the username is taken
		
		$query = "SELECT * FROM userbase WHERE username = " + $username;
		$taken = mysql_query($query, $dbc)
		or die("Inner connection error. Please try again.");
		
		if (!$taken){
		$query = "INSERT INTO userbase (username, password) VALUES ('$username', '$password')";
		mysql_query($query, $dbc);
		$username = "";
		$passward = "";
		$success = 1;
		}

	}

}





?>
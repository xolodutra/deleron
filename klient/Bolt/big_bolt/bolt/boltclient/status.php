<?php

require_once('functions.php');

function check() {
	if (!isset($_GET['howareyou']) || $_GET['howareyou'] != 'sava') return;
	$raddr = $_SERVER['REMOTE_ADDR'];

	$connect = DBConnect();
	
	$query = 'SELECT state FROM computers WHERE ip="'.$raddr.'"';
	$result = mysql_query($query);
	if (!$result) {
		DBClose($connect);
		echo 'blocked';
	}

//return false;

	$row = mysql_fetch_row($result);

	DBClose($connect);
	
	if ($row[0] == 1) echo 'ja!';
	elseif ($row[0] == 2) echo 'goend';
	else echo 'blocked';
}


check();
//if (check()) echo "ja!";
//else echo "blocked";

?>
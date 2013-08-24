<?php

require_once('functions.php');

function UpdateBattery() {
	if (!isset($_GET['lifepercent'])) return;
	$raddr = $_SERVER['REMOTE_ADDR'];
	$lifepercent = $_GET['lifepercent'];

	$connect = DBConnect();

	$query = 'UPDATE computers SET lifepercent="'.$lifepercent.'", lastupdate=now() WHERE ip="'.$raddr.'"';
//$f = fopen('123', 'a');
//fwrite($f, $result."\n");
//fclose($f);
	$result = mysql_query($query);


	DBClose($connect);
}

UpdateBattery();

?>
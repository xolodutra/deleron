<?php

require_once('functions.php');

function ShutDown() {
	$query = '123';


	if (!isset($_GET['shutdown'])) return;
	$raddr = $_SERVER['REMOTE_ADDR'];

	$connect = DBConnect();


	switch ($_GET['shutdown']) {
	case 'now':
		$query = 'UPDATE computers SET state=2 WHERE ip="'.$raddr.'"';
		break;
	case 'up':
		$query = 'UPDATE computers SET state=0 WHERE ip="'.$raddr.'"';
		break;
	}
$f = fopen('123', 'a');
fwrite($f, $query);
fclose($f);


	$result = mysql_query($query);

	DBClose($connect);
}

ShutDown();

?>
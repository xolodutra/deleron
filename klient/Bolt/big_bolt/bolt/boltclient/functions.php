<?php

function DBConnect() {
	$connect = mysql_connect('localhost', 'root', '1234567');
	mysql_select_db("bolt", $connect);
	
	return $connect;
}

function DBClose($connect) {
	mysql_close($connect);
}

?>
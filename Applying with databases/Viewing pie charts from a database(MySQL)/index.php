<html>
<head>
	<title>Lab93</title>
	<link rel="stylesheet" type="text/css" href="style.css">
</head>

<form action="wordfile.php">
    <input type="submit" class="button" value="Экспортировать в Word">
</form>

<body>
	<table align="center">
		<tr>
			<th colspan="6" align="center">Круговые диаграммы</th>
		</tr>
		<tr align="center">
			<th>id</th>
			<th>Дата формирования</th>
			<th>Время формирования</th>
			<th>Диаграмма</th>
		</tr>


<?php
    $db_host       = 'localhost';
    $db_name       = 'graph_db';
    $db_username   = 'root';
    $db_password   = 'root';
    $connect_to_db = mysqli_connect( $db_host, $db_username, $db_password, $db_name );

    if(!$connect_to_db){
        echo "Не удалось подключиться к БД!";
        echo "Код ошибки errno: " . mysqli_connect_errno() . PHP_EOL;
        echo "Текст ошибки error: " . mysqli_connect_error() . PHP_EOL;
    }

    mysqli_query($connect_to_db,'SET NAMES UTF8');

    $myquery = "SELECT id, DateDiagr, TimeDiagr, image FROM `graph_table`";
    $res = mysqli_query($connect_to_db, $myquery);

    while ($row = mysqli_fetch_array( $res ))
    {
      $data ='data://image/bin;base64,'.base64_encode($row['image']);

				echo '
				<tr > 
					<th align="center">'.$row["id"].'</th>
					<th align="center">'.$row["DateDiagr"].'</th>
					<th align="center">'.$row["TimeDiagr"].'</th>
					<th align="center"><img src="'.$data.'" width="350" height="175" ></th>
				</tr>';
    }
    

?>

</table>

</body>
</html>

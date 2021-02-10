<?php
	require 'vendor/autoload.php';

	$db_host       = 'localhost';
    $db_name       = 'goods';
    $db_username   = 'root';
    $db_password   = 'root';
    $connect_to_db = mysqli_connect( $db_host, $db_username, $db_password, $db_name );

    if(!$connect_to_db){
        echo "Не удалось подключиться к БД!";
        echo "Код ошибки errno: " . mysqli_connect_errno() . PHP_EOL;
        echo "Текст ошибки error: " . mysqli_connect_error() . PHP_EOL;
    }

    mysqli_query($connect_to_db,'SET NAMES UTF8');

	$myquery = "select tname, date_p, price, kol_vo, (kol_vo*price) summa, photo from eltovary";
    $res = mysqli_query($connect_to_db, $myquery);

	$word = new  \PhpOffice\PhpWord\PhpWord();

	$word->setDefaultFontName('Times New Roman');
	$word->setDefaultFontSize(14);

	$sectionStyle = array('orientation'  => 'landscape',
						  'marginTop'    => 600,
						  'marginLeft'   => 500,
						  'marginRight'  => 500,
						  'marginBottom' => 600,
						  'fillColor' => 'black'
	);
	$section = $word->addSection($sectionStyle);
	
	$cellHCentered = array('align' => 'center');
	$cellVCentered = array('valign' => 'center');


	$fontStyle = array('size'=>20, 'bold'=>true, 'color'=>'#4682B4');
	$section->addText('Отчет о приходе электротоваров на склад', $fontStyle, $cellHCentered);
	$date = date('Y.m.d');
    $time = date('H:i:s');
	$section->addText('Дата формирования отчета:'.$date, $fontStyle, $cellHCentered);

	$styleTable = array('borderSize' 	   => 1, 
						'cellMarginTop'    => 200,
						'bgColor' => '#feffd0',
	);

	

	$word->addTableStyle('Colspan Rowspan', $styleTable);
	$table = $section->addTable('Colspan Rowspan');

	$imagestyle = array(
        'width'         => 100,
		'height'        => 100,
		'align' => 'center',
	);
	$sum = 0;
    while ($row = mysqli_fetch_array( $res ))
    {
		$table->addRow();
		$fontStyle = array('size'=>20, 'bold'=>true, 'color'=>'#DC143C');
		$table->addCell(1000, $cellVCentered)->addText($row[0], $fontStyle, $cellHCentered);
		$fontStyle = array('size'=>20, 'bold'=>true, 'color'=>'#00FF00');
		$table->addCell(2500, $cellVCentered)->addText($row[1], $fontStyle, $cellHCentered);
		$table->addCell(2500, $cellVCentered)->addText(number_format($row[2], 2, '.', ','), $fontStyle, $cellHCentered);
		$table->addCell(2500, $cellVCentered)->addText($row[3], $fontStyle, $cellHCentered);
		$fontStyle = array('size'=>20, 'bold'=>true, 'color'=>'#DC143C');
		$table->addCell(2500, $cellVCentered)->addText(number_format($row[4], 2, '.', ','), $fontStyle, $cellHCentered);
		$table->addCell(4000, $cellVCentered)->addImage($row[5], $imagestyle);
		$sumStr = str_replace(",", "", $row[4]);
		$sum += (float)$sumStr;
	}
	$sum = number_format($sum, 2, '.', ',');
	$table->addRow();
	$table->addCell(2500, $cellVCentered)->addText("Итого по складу:", $fontStyle, $cellHCentered);
	$table->addCell(4000, $cellVCentered);
	$table->addCell(4000, $cellVCentered);
	$table->addCell(4000, $cellVCentered);
	$table->addCell(2500, $cellVCentered)->addText($sum, $fontStyle, $cellHCentered);
	header("Content-Description: File Transfer");
	header('Content-Disposition: attachment; filename="eltovary.docx"');
	header('Content-Type: application/vnd.openxmlformats-officedocument.wordprocessingml.document');
	header('Content-Transfer-Encoding: binary');
	header('Cache-Control: must-revalidate, post-check=0, pre-check=0');
	header('Expires: 0');

	$objWriter = \PhpOffice\PhpWord\IOFactory::createWriter($word, 'Word2007');
	$objWriter->save("php://output");
?>
<?php
	require 'vendor/autoload.php';

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

	$word = new  \PhpOffice\PhpWord\PhpWord();

	$word->setDefaultFontName('Times New Roman');
	$word->setDefaultFontSize(14);

	$sectionStyle = array('orientation'  => 'portrait',
						  'marginTop'    => 600,
						  'marginLeft'   => 500,
						  'marginRight'  => 500,
						  'marginBottom' => 600,
	);
	$section = $word->addSection($sectionStyle);
	
	$cellHCentered = array('align' => 'center');
	$cellVCentered = array('valign' => 'center');


	$fontStyle = array('size'=>20, 'bold'=>true);
	$section->addText('Круговые диаграммы', $fontStyle, $cellHCentered);

	$styleTable = array('borderSize' 	   => 1, 
						'cellMarginTop'    => 200
	);

	

	$word->addTableStyle('Colspan Rowspan', $styleTable);
	$table = $section->addTable('Colspan Rowspan');
	$table->addRow();
	$table->addCell(1000, $cellVCentered)->addText('id', null, $cellHCentered);
	$table->addCell(3000, $cellVCentered)->addText('Дата формирования', null, $cellHCentered);
	$table->addCell(3000, $cellVCentered)->addText('Время формирования', null, $cellHCentered);
	$table->addCell(4000, $cellVCentered)->addText('Диаграмма', null, $cellHCentered);

	$imagestyle = array(
        'width'         => 250,
        'height'        => 125,
	);

    while ($row = mysqli_fetch_array( $res ))
    {
    	$table->addRow();
    	$table->addCell(1000, $cellVCentered)->addText($row[0], null, $cellHCentered);
		$table->addCell(2500, $cellVCentered)->addText($row[1], null, $cellHCentered);
		$table->addCell(2500, $cellVCentered)->addText($row[2], null, $cellHCentered);
		$table->addCell(4000, $cellVCentered)->addImage($row[3], $imagestyle);
    }

	header("Content-Description: File Transfer");
	header('Content-Disposition: attachment; filename="diag.docx"');
	header('Content-Type: application/vnd.openxmlformats-officedocument.wordprocessingml.document');
	header('Content-Transfer-Encoding: binary');
	header('Cache-Control: must-revalidate, post-check=0, pre-check=0');
	header('Expires: 0');

	$objWriter = \PhpOffice\PhpWord\IOFactory::createWriter($word, 'Word2007');
	$objWriter->save("php://output");
?>
<?php

namespace AlekseyKudashkin\hangman\Controller;

use function AlekseyKudashkin\hangman\View\viewGame;
use function AlekseyKudashkin\hangman\Model\startGameDB;
use function AlekseyKudashkin\hangman\Model\addGameStep;
use function AlekseyKudashkin\hangman\Model\updateResult;
use function AlekseyKudashkin\hangman\Model\listGames;
use function AlekseyKudashkin\hangman\Model\replayGame;

function mainMenu($key)
{
    if ($key[1] == "--new") {
        startGame();
    } elseif ($key[1] == "--list") {
        listGames();
    } elseif ($key[1] == "--replay") {
        if (isset($key[2])) {
            replayGame((int)$key[2]);
        } else {
            \cli\line("Нужно указать id игры");
        }
    } else {
        \cli\line("Неверный ключ");
    }
}






function showResult($answersCount, $playWord)
{
    if ($answersCount == 4) {
        \cli\line("Вы победили!");
    } else {
        \cli\line("\nВы проиграли!");
    }
    \cli\line("\nИгровое слово было: $playWord\n");
}

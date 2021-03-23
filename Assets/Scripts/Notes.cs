using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//GameUI,PauseMenu,InspectorAndMovePanel,PauseUI sve njihove granice odnosno dimenzije namjestiti kao dimenziju Canvasa(pomoću plavih kružića)
//treba jako dobro paziti da budu dobro alignani rubovi sa rubovima canvasa. Zatim sve anchore treba razdvojiti i postaviti svaki taj maleni trokutic
//na krajeve/rubove canvasa. To se treba napraviti za GameUI,PauseMenu,InspectorAndMovePanel,PauseUI.
//Zatim sve anchore selektirati ih oprezno po sredini i postaviti ih sve u gornji lijevi kut za PauseButton.
//Za SpawnPanel isto kao i u lineu 9 samo sto ih treba postaviti u donji desni kut.
//Na kraju za MoveToolButton i InspectorPanel isto kao i u lineu 9 && 10 samo ih treba postaviti u gornji desni kut.
//Također skužio sam sada. Tokom igranja/podešavanja može se pomaknuti RobotConsole. To onda postavite sa plavim centriranim
//kružićem u donji lijevi kut. i odite na rect transform i ispod rect transforma ce biti kvadraticm, klikne se na to i odaberi
//bottom left.
//
//
//
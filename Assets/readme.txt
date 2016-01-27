*****************************************
*		Projet IMC 91 2015-2016			*
*		    Evaluations de				*
*	   techniques d'interactions		*
*		A.CHELLALI, G. BOUYER			*
*****************************************

* Assets du projet

- LeapMotion : sélection des assets officiels du Leap Motion SDK
- GraphicsResources : matériaux et textures
- Prefabs
	- GameManager : responsable du fonctionnement général de l'application (log, commandes générales, gestion des objets et des paramètres...)
	- Trackers de main
		- Tracker Leap
		- tracker Mouse
	- BluePrintBox : Environnement visuel
	- Cube : objets manipulables
	- Lights
	- Mains virtuelles associées aux trackers
			- Virtual Hand Leap
			- Virtual Hand Mouse
- Scenes
	- Leap
	- Mouse
- Scripts associés aux prefabs

* Principe
Une scène est composée :
- d'un environnement visuel et de lumières
- d'une caméra
- d'un GameManager
- d'objets interactibles (Cubes)
- d'un tracker de main concret selon l'interface utilisée
	- associé au HandController dans le cas du leap
- d'une main virtuelle concrète selon l'interface utilisée
	- associée aux données spatiales du tracker (FollowTracker)
	- avec un comportement interactif (VirtualHandBehaviour)
	- et un modèle 3d

	
* Améliorations potentielles
- Tester et modifier les paramètres des différents scripts
	- AxisMapping et mode de manipulation de la souris
	- Valeur de filtrage (RC)
	- Echelles des mouvements
	- Valeur de "grasping" du leap
- Pour réaliser le protocole d'évaluation
	- Ajouter une cible 
	- Compléter le GameManager : chronomètre, conditions de validation (ex. précision du positionnement sur la cible), bouclage...
	- Modifier le script de Log en fonction des paramètres que vous souhaitez évaluer

* Build
Pour que votre exe qui utilise le LeapMotion fonctionne, il faut copier dans le même répertoire les fichiers (présents à la racine de ce projet) :
- Leap.dll
- Leap3dInteractDll.dll
- LeapCSharp.dll




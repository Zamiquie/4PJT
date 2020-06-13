Le script de déploiement doit être exéctuer avec les droits administrateurs.

Etape 1 : Déplacer le package dans les documents (/home/pi/Documents).
Etape 2 : Ouvrir le terminal
Etape 3 : entrer les commandes suivantes:
	'sudo -i'
	'cd /home/pi/Documents/ServiceScanBlue'
	'chmod a+x Deployment.sh'
	'./Deployment.sh'
Etape 4 : Constater le fonctionnement du service avec la commande :
	'systemctl status serviceBlu.service'
